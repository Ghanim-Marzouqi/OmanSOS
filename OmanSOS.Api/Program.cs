using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using OmanSOS.Api.Interfaces;
using OmanSOS.Api.Services;
using OmanSOS.Core;
using OmanSOS.Core.Constants;
using OmanSOS.Data;
using System.Text;

const string origins = "AllowedOrigins";
var builder = WebApplication.CreateBuilder(args);

// Access App Configuration & Environment
ConfigurationManager configuration = builder.Configuration;
IWebHostEnvironment environment = builder.Environment;

// AutoMapper setup
var mappingConfig = new MapperConfiguration(config => { config.AddProfile<MappingService>(); });
IMapper mapper = mappingConfig.CreateMapper();

// Add services to the container.
builder.Services.AddCors(policy =>
{
    policy.AddPolicy(origins, config => config
        .WithOrigins(
            "http://localhost:4000", "https://localhost:4001", "http://localhost:27472",
            "http://localhost:5000", "https://localhost:5001")
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials());
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration.GetSection("TokenSecret").Value)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});
builder.Services.AddControllers();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddSingleton<IFileService, FileService>();
builder.Services.AddSingleton(mapper);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Oman SOS Web API", Version = "v1" });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization Header",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Oman SOS Web API v1"));
}

// app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(app.Environment.ContentRootPath, Config.DefaultFileDirectory)),
    RequestPath = new PathString($"/{Config.DefaultFileDirectory}")
});

app.UseRouting();

app.UseCors(origins);

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
