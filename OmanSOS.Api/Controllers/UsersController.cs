using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OmanSOS.Core;
using OmanSOS.Core.Models;
using OmanSOS.Core.ViewModels;
using System.Net;

namespace OmanSOS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UsersController(IMapper mapper, IUnitOfWork unitOfWork)
        {
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
                var users = await _unitOfWork.Users.GetAllAsync();

                return Ok(new ResponseViewModel<IEnumerable<UserViewModel>>
                {
                    Data = _mapper.Map<IEnumerable<UserViewModel>>(users)
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
}
