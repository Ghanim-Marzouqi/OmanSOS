using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OmanSOS.Api.Interfaces;
using OmanSOS.Core;
using OmanSOS.Core.Constants;
using OmanSOS.Core.Models;
using OmanSOS.Core.ViewModels;
using System.Net;

namespace OmanSOS.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public AuthController(IUnitOfWork unitOfWork, IMapper mapper, IAuthService authService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _authService = authService;
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login(UserViewModel viewModel)
    {
        try
        {
            // 1. Check if user exists
            var user = await _unitOfWork.Users.GetByEmailAsync(viewModel.Email);

            // 2. Verify user credentials
            var verified = _authService.VerifyPasswordHash(viewModel.Password, user?.PasswordHash, user?.PasswordSalt);

            if (user == null || !verified)
            {
                return Ok(new ResponseViewModel<UserViewModel>
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = "Invalid Credentials"
                });
            }

            // 3. Create a new token
            var userType = await _unitOfWork.UserTypes.GetByIdAsync(user.UserTypeId);
            var loggedInUser = _mapper.Map<UserViewModel>(user);
            loggedInUser.UserType = _mapper.Map<UserTypeViewModel>(userType) ?? null;
            loggedInUser.AccessToken = _authService.CreateAccessToken(loggedInUser);

            return Ok(new ResponseViewModel<UserViewModel>
            {
                Message = "Login successful",
                Data = loggedInUser
            });
        }
        catch (Exception e)
        {
            // TODO: Log Error
            return Ok(new ResponseViewModel<UserViewModel>
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = "An error occurred while user logging in",
                ErrorMessage = e.Message,
                ErrorStackTrace = e.StackTrace,
                Data = null
            });
        }
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register(UserViewModel viewModel)
    {
        try
        {
            // 1. Check if user exists
            var existingUser = await _unitOfWork.Users.GetByNationalIdAsync(viewModel.NationalId);

            if (existingUser != null)
            {
                return Ok(new ResponseViewModel<UserViewModel>
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = "User already exists"
                });
            }

            // 2. Create a password hash & salt
            var entity = _mapper.Map<User>(viewModel);
            _authService.CreatePasswordHash(entity.Password, out var passwordHash, out var passwordSalt);
            entity.PasswordHash = passwordHash;
            entity.PasswordSalt = passwordSalt;

            // 3. Set Default User Type
            entity.UserTypeId = Config.DefaultUserTypeId;

            // 4. Insert user in database
            var insertedId = await _unitOfWork.Users.AddAsync(entity);

            if (insertedId <= 0)
            {
                return Ok(new ResponseViewModel<UserViewModel>
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = "Couldn't create user"
                });
            }

            // 5. Get newly created user
            var createdUser = await _unitOfWork.Users.GetByIdAsync(insertedId);

            return Ok(new ResponseViewModel<UserViewModel>
            {
                StatusCode = HttpStatusCode.Created,
                Message = "New user registered successfully",
                Data = _mapper.Map<UserViewModel>(createdUser)
            });
        }
        catch (Exception e)
        {
            // TODO: Log Error
            return Ok(new ResponseViewModel<UserViewModel>
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = "An error occurred While registering a new user",
                ErrorMessage = e.Message,
                ErrorStackTrace = e.StackTrace,
                Data = null
            });
        }
    }
}
