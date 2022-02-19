using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OmanSOS.Core;
using OmanSOS.Core.ViewModels;
using System.Net;

namespace OmanSOS.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class MetadataController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public MetadataController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    [HttpGet("GetCategories")]
    public async Task<IActionResult> GetCategories()
    {
        try
        {
            var categoriesResult = await _unitOfWork.Categories.GetAllAsync();

            var categories = _mapper.Map<IEnumerable<CategoryViewModel>>(categoriesResult);

            return Ok(new ResponseViewModel<IEnumerable<CategoryViewModel>>
            {
                Data = categories
            });
        }
        catch (Exception e)
        {
            return Ok(new ResponseViewModel<IEnumerable<CategoryViewModel>>
            {
                StatusCode = HttpStatusCode.NotFound,
                Message = "An error occured while getting list of categories",
                ErrorMessage = e.Message,
                ErrorStackTrace = e.StackTrace
            });
        }
    }

    [HttpGet("GetLocations")]
    [AllowAnonymous]
    public async Task<IActionResult> GetLocations()
    {
        try
        {
            var locationsResult = await _unitOfWork.Locations.GetAllAsync();

            var locations = _mapper.Map<IEnumerable<LocationViewModel>>(locationsResult);

            return Ok(new ResponseViewModel<IEnumerable<LocationViewModel>>
            {
                Data = locations
            });
        }
        catch (Exception e)
        {
            return Ok(new ResponseViewModel<IEnumerable<LocationViewModel>>
            {
                StatusCode = HttpStatusCode.NotFound,
                Message = "An error occured while getting list of locations",
                ErrorMessage = e.Message,
                ErrorStackTrace = e.StackTrace
            });
        }
    }

    [HttpGet("GetPriorities")]
    public async Task<IActionResult> GetPriorities()
    {
        try
        {
            var prioritiesResult = await _unitOfWork.Priorities.GetAllAsync();

            var priorities = _mapper.Map<IEnumerable<PriorityViewModel>>(prioritiesResult);

            return Ok(new ResponseViewModel<IEnumerable<PriorityViewModel>>
            {
                Data = priorities
            });
        }
        catch (Exception e)
        {
            return Ok(new ResponseViewModel<IEnumerable<PriorityViewModel>>
            {
                StatusCode = HttpStatusCode.NotFound,
                Message = "An error occured while getting list of categories",
                ErrorMessage = e.Message,
                ErrorStackTrace = e.StackTrace
            });
        }
    }
}
