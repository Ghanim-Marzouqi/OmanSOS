using MudBlazor;
using MudBlazor.Services;
using OmanSOS.Mobile.Services;

namespace OmanSOS.Mobile;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        var http = new HttpClient
        {
            BaseAddress = new Uri("http://10.0.2.2:5000/api")
        };

        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        builder.Services.AddMauiBlazorWebView();
        builder.Services.AddOptions();
        builder.Services.AddScoped(_ => http);
        builder.Services.AddAuthorizationCore();
        builder.Services.AddScoped<IAuthService, AuthService>();
        builder.Services.AddScoped<IBrowserStorageService, BrowserStorageService>();
        builder.Services.AddScoped<IMetadataService, MetadataService>();
        builder.Services.AddScoped<IDonationsService, DonationsService>();
        builder.Services.AddScoped<IRequestsService, RequestsService>();
        builder.Services.AddScoped<IUsersService, UsersService>();
        builder.Services.AddMudServices(config =>
        {
            config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomCenter;
            config.SnackbarConfiguration.PreventDuplicates = false;
            config.SnackbarConfiguration.NewestOnTop = false;
            config.SnackbarConfiguration.ShowCloseIcon = true;
            config.SnackbarConfiguration.VisibleStateDuration = 3000;
            config.SnackbarConfiguration.HideTransitionDuration = 500;
            config.SnackbarConfiguration.ShowTransitionDuration = 500;
            config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
        });

        return builder.Build();
    }
}
