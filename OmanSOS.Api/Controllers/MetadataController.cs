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

    [HttpGet("GetNextCategoryId")]
    public async Task<IActionResult> GetNextCategoryId()
    {
        try
        {
            var id = await _unitOfWork.Categories.GetNextId();

            return Ok(new ResponseViewModel<int>
            {
                Data = id
            });
        }
        catch (Exception e)
        {
            return Ok(new ResponseViewModel<int>
            {
                StatusCode = HttpStatusCode.NotFound,
                Message = "An error occured while next category id",
                ErrorMessage = e.Message,
                ErrorStackTrace = e.StackTrace
            });
        }
    }

    [HttpGet("GetNextLocationId")]
    public async Task<IActionResult> GetNextLocationId()
    {
        try
        {
            var id = await _unitOfWork.Locations.GetNextId();

            return Ok(new ResponseViewModel<int>
            {
                Data = id
            });
        }
        catch (Exception e)
        {
            return Ok(new ResponseViewModel<int>
            {
                StatusCode = HttpStatusCode.NotFound,
                Message = "An error occured while next location id",
                ErrorMessage = e.Message,
                ErrorStackTrace = e.StackTrace
            });
        }
    }

    [HttpGet("GetNextPriorityId")]
    public async Task<IActionResult> GetNextPriorityId()
    {
        try
        {
            var id = await _unitOfWork.Priorities.GetNextId();

            return Ok(new ResponseViewModel<int>
            {
                Data = id
            });
        }
        catch (Exception e)
        {
            return Ok(new ResponseViewModel<int>
            {
                StatusCode = HttpStatusCode.NotFound,
                Message = "An error occured while next priority id",
                ErrorMessage = e.Message,
                ErrorStackTrace = e.StackTrace
            });
        }
    }

    [HttpPost("AddCategory")]
    public async Task<IActionResult> AddCategory(CategoryViewModel categoryViewModel)
    {
        if (categoryViewModel == null)
        {
            return BadRequest(new ResponseViewModel<int>
            {
                StatusCode = HttpStatusCode.NotFound,
                Message = "Incomplete data"
            });
        }

        try
        {
            var category = _mapper.Map<Category>(categoryViewModel);
            var insertedId = await _unitOfWork.Categories.AddAsync(category);

            if (insertedId > 0)
            {
                return Ok(new ResponseViewModel<int>
                {
                    StatusCode = HttpStatusCode.Created,
                    Message = "Category added successfully"
                });
            }
            else
            {
                return NotFound(new ResponseViewModel<int>
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Message = "Cannot add category"
                });
            }
        }
        catch (Exception e)
        {
            return NotFound(new ResponseViewModel<int>
            {
                StatusCode = HttpStatusCode.NotFound,
                Message = "An error occurred while adding new category",
                Data = 0,
                ErrorMessage = e.Message,
                ErrorStackTrace = e.StackTrace
            });
        }
    }

    [HttpPost("AddLocation")]
    public async Task<IActionResult> AddLocation(LocationViewModel locationViewModel)
    {
        if (locationViewModel == null)
        {
            return BadRequest(new ResponseViewModel<int>
            {
                StatusCode = HttpStatusCode.NotFound,
                Message = "Incomplete data"
            });
        }

        try
        {
            var location = _mapper.Map<Location>(locationViewModel);
            var insertedId = await _unitOfWork.Locations.AddAsync(location);

            if (insertedId > 0)
            {
                return Ok(new ResponseViewModel<int>
                {
                    StatusCode = HttpStatusCode.Created,
                    Message = "Location added successfully"
                });
            }
            else
            {
                return NotFound(new ResponseViewModel<int>
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Message = "Cannot add location"
                });
            }
        }
        catch (Exception e)
        {
            return NotFound(new ResponseViewModel<int>
            {
                StatusCode = HttpStatusCode.NotFound,
                Message = "An error occurred while adding new location",
                Data = 0,
                ErrorMessage = e.Message,
                ErrorStackTrace = e.StackTrace
            });
        }
    }

    [HttpPost("AddPriority")]
    public async Task<IActionResult> AddPriority(PriorityViewModel priorityViewModel)
    {
        if (priorityViewModel == null)
        {
            return BadRequest(new ResponseViewModel<int>
            {
                StatusCode = HttpStatusCode.NotFound,
                Message = "Incomplete data"
            });
        }

        try
        {
            var priority = _mapper.Map<Priority>(priorityViewModel);
            var insertedId = await _unitOfWork.Priorities.AddAsync(priority);

            if (insertedId > 0)
            {
                return Ok(new ResponseViewModel<int>
                {
                    StatusCode = HttpStatusCode.Created,
                    Message = "Priority added successfully"
                });
            }
            else
            {
                return NotFound(new ResponseViewModel<int>
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Message = "Cannot add priority"
                });
            }
        }
        catch (Exception e)
        {
            return NotFound(new ResponseViewModel<int>
            {
                StatusCode = HttpStatusCode.NotFound,
                Message = "An error occurred while adding new priority",
                Data = 0,
                ErrorMessage = e.Message,
                ErrorStackTrace = e.StackTrace
            });
        }
    }

    [HttpDelete("DeleteCategory/{id:int}")]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        try
        {
            var isDeleted = await _unitOfWork.Categories.DeleteAsync(id);

            if (isDeleted > 0)
            {
                return Ok(new ResponseViewModel<bool>
                {
                    Message = "Category deleted successfully",
                    Data = true
                });
            }
            else
            {
                return Ok(new ResponseViewModel<bool>
                {
                    Message = "Category not deleted",
                    Data = false
                });
            }
        }
        catch (Exception e)
        {
            return Ok(new ResponseViewModel<bool>
            {
                StatusCode = HttpStatusCode.NotFound,
                Message = "An error occured while deleting category",
                Data = false,
                ErrorMessage = e.Message,
                ErrorStackTrace = e.StackTrace
            });
        }
    }

    [HttpDelete("DeleteLocation/{id:int}")]
    public async Task<IActionResult> DeleteLocation(int id)
    {
        try
        {
            var isDeleted = await _unitOfWork.Locations.DeleteAsync(id);

            if (isDeleted > 0)
            {
                return Ok(new ResponseViewModel<bool>
                {
                    Message = "Location deleted successfully",
                    Data = true
                });
            }
            else
            {
                return Ok(new ResponseViewModel<bool>
                {
                    Message = "Location not deleted",
                    Data = false
                });
            }
        }
        catch (Exception e)
        {
            return Ok(new ResponseViewModel<bool>
            {
                StatusCode = HttpStatusCode.NotFound,
                Message = "An error occured while deleting location",
                Data = false,
                ErrorMessage = e.Message,
                ErrorStackTrace = e.StackTrace
            });
        }
    }

    [HttpDelete("DeletePriority/{id:int}")]
    public async Task<IActionResult> DeletePriority(int id)
    {
        try
        {
            var isDeleted = await _unitOfWork.Priorities.DeleteAsync(id);

            if (isDeleted > 0)
            {
                return Ok(new ResponseViewModel<bool>
                {
                    Message = "Priority deleted successfully",
                    Data = true
                });
            }
            else
            {
                return Ok(new ResponseViewModel<bool>
                {
                    Message = "Priority not deleted",
                    Data = false
                });
            }
        }
        catch (Exception e)
        {
            return Ok(new ResponseViewModel<bool>
            {
                StatusCode = HttpStatusCode.NotFound,
                Message = "An error occured while deleting priority",
                Data = false,
                ErrorMessage = e.Message,
                ErrorStackTrace = e.StackTrace
            });
        }
    }
}
