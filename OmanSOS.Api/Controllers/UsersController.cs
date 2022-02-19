using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OmanSOS.Api.Interfaces;
using OmanSOS.Core;
using OmanSOS.Core.Models;
using OmanSOS.Core.ViewModels;
using System.Net;

namespace OmanSOS.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UsersController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UsersController(IAuthService authService, IConfiguration configuration, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _authService = authService;
        _configuration = configuration;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    [HttpPost("Add")]
    public async Task<IActionResult> Add(UserViewModel userViewModel)
    {
        try
        {
            if (userViewModel == null)
            {
                return Ok(new ResponseViewModel<bool>
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = "Data sent incomplete"
                });
            }

            var user = _mapper.Map<User>(userViewModel);
            _authService.CreatePasswordHash(_configuration.GetSection("DefaultPassword").Value, out var passwordHash, out var passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            var insertedId = await _unitOfWork.Users.AddAsync(user);

            if (insertedId != 0)
            {
                return Ok(new ResponseViewModel<bool>
                {
                    StatusCode = HttpStatusCode.Created,
                    Message = "A new user has been added successfully",
                    Data = true
                });
            }
            else
            {
                return Ok(new ResponseViewModel<bool>
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Message = "Cannot add the user",
                    Data = false
                });
            }
        }
        catch (Exception e)
        {
            return Ok(new ResponseViewModel<bool>
            {
                StatusCode = HttpStatusCode.NotFound,
                Message = "An error occured while adding a new user",
                Data = false,
                ErrorMessage = e.Message,
                ErrorStackTrace = e.StackTrace
            });
        }
    }

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            // 1. Get users
            var usersResults = await _unitOfWork.Users.GetAllAsync();
            var users = _mapper.Map<IEnumerable<UserViewModel>>(usersResults);

            // 2. Get user locations
            foreach (var user in users)
            {
                var locationResult = await _unitOfWork.Locations.GetByIdAsync(user.LocationId);
                if (locationResult != null)
                    users.First(p => p.Id == user.Id).Location = _mapper.Map<LocationViewModel>(locationResult);
            }

            return Ok(new ResponseViewModel<IEnumerable<UserViewModel>>
            {
                Data = users
            });
        }
        catch (Exception e)
        {
            return Ok(new ResponseViewModel<IEnumerable<UserViewModel>>
            {
                StatusCode = HttpStatusCode.NotFound,
                Message = "An error occured while getting list of users",
                ErrorMessage = e.Message,
                ErrorStackTrace = e.StackTrace
            });
        }
    }

    [HttpDelete("Delete/{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var isDeleted = await _unitOfWork.Users.DeleteAsync(id);

            if (isDeleted > 0)
            {
                return Ok(new ResponseViewModel<bool>
                {
                    Message = "User deleted successfully",
                    Data = true
                });
            }
            else
            {
                return Ok(new ResponseViewModel<bool>
                {
                    Message = "User not deleted",
                    Data = false
                });
            }
        }
        catch (Exception e)
        {
            return Ok(new ResponseViewModel<bool>
            {
                StatusCode = HttpStatusCode.NotFound,
                Message = "An error occured while deleting user",
                Data = false,
                ErrorMessage = e.Message,
                ErrorStackTrace = e.StackTrace
            });
        }
    }
}
