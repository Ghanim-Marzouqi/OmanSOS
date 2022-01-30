using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OmanSOS.Core;
using OmanSOS.Core.ViewModels;
using System.Net;

namespace OmanSOS.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RequestsController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public RequestsController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
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
}
