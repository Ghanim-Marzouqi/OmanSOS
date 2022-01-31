using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OmanSOS.Core;
using OmanSOS.Core.Models;
using OmanSOS.Core.ViewModels;
using System.Net;

namespace OmanSOS.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class RequestsController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public RequestsController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    [HttpPost("Add")]
    public async Task<IActionResult> Add(RequestViewModel requestViewModel)
    {
        if (requestViewModel == null)
        {
            return BadRequest(new ResponseViewModel<bool>
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = "Data sent incomplete",
                Data = false
            });
        }

        try
        {
            var request = _mapper.Map<Request>(requestViewModel);

            var insertedId = await _unitOfWork.Requests.AddAsync(request);

            if (insertedId > 0)
            {
                return Ok(new ResponseViewModel<bool>
                {
                    StatusCode = HttpStatusCode.Created,
                    Message = "Your request has been added successfully",
                    Data = true
                });
            }
            else
            {
                return Ok(new ResponseViewModel<bool>
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Message = "Cannot add your request",
                    Data = true
                });
            }
        }
        catch (Exception e)
        {
            return NotFound(new ResponseViewModel<bool>
            {
                StatusCode = HttpStatusCode.NotFound,
                Message = "An error occurred while adding a new request",
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
            var requestsResult = await _unitOfWork.Requests.GetAllAsync();

            var requests = _mapper.Map<IEnumerable<RequestViewModel>>(requestsResult);
            
            if (requests.Count() > 0)
            {
                foreach (var request in requests)
                {
                    var categoryResult = await _unitOfWork.Requests.GetCategoryByRequestIdAsync(request.Id);
                    var priorityResult = await _unitOfWork.Requests.GetPriorityByRequestIdAsync(request.Id);
                    var userResult = await _unitOfWork.Requests.GetRequestorByRequestIdAsync(request.Id);
                    requests.Where(p => p.Id == request.Id).First().Category = _mapper.Map<CategoryViewModel>(categoryResult);
                    requests.Where(p => p.Id == request.Id).First().Priority = _mapper.Map<PriorityViewModel>(priorityResult);
                    requests.Where(p => p.Id == request.Id).First().User = _mapper.Map<UserViewModel>(userResult);
                }
            }

            return Ok(new ResponseViewModel<IEnumerable<RequestViewModel>>
            {
                Data = requests
            });
        }
        catch (Exception e)
        {
            return Ok(new ResponseViewModel<IEnumerable<RequestViewModel>>
            {
                StatusCode = HttpStatusCode.NotFound,
                Message = "An error occured while getting list of requests",
                ErrorMessage = e.Message,
                ErrorStackTrace = e.StackTrace
            });
        }
    }

    [HttpGet("GetById/{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var requestResult = await _unitOfWork.Requests.GetByIdAsync(id);

            if (requestResult == null)
            {
                return NotFound(new ResponseViewModel<RequestViewModel>
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Message = "Request not found",
                    Data = null
                });
            }

            var categoryResult = await _unitOfWork.Categories.GetByIdAsync(requestResult.CategoryId);
            var priorityResult = await _unitOfWork.Priorities.GetByIdAsync(requestResult.PriorityId);
            var userResult = await _unitOfWork.Users.GetByIdAsync(requestResult.UserId);

            var request = _mapper.Map<RequestViewModel>(requestResult);
            request.Category = _mapper.Map<CategoryViewModel>(categoryResult);
            request.Priority = _mapper.Map<PriorityViewModel>(priorityResult);
            request.User = _mapper.Map<UserViewModel>(userResult);

            var response = new ResponseViewModel<RequestViewModel>
            {
                Data = request
            };

            return Ok(response);
        }
        catch (Exception e)
        {
            return NotFound(new ResponseViewModel<RequestViewModel>
            {
                StatusCode = HttpStatusCode.NotFound,
                Message = "An error occurred while fetching user request details",
                Data = null,
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
            var isDeleted = await _unitOfWork.Requests.DeleteAsync(id);

            if (isDeleted > 0)
            {
                return Ok(new ResponseViewModel<bool>
                {
                    Message = "Request deleted successfully",
                    Data = true
                });
            }
            else
            {
                return Ok(new ResponseViewModel<bool>
                {
                    Message = "Request not deleted",
                    Data = false
                });
            }
        }
        catch (Exception e)
        {
            return Ok(new ResponseViewModel<bool>
            {
                StatusCode = HttpStatusCode.NotFound,
                Message = "An error occured while deleting request",
                Data = false,
                ErrorMessage = e.Message,
                ErrorStackTrace = e.StackTrace
            });
        }
    }
}
