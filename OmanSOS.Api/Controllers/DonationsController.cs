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
public class DonationsController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public DonationsController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    [HttpPost("Add")]
    public async Task<IActionResult> Add(DonationViewModel donationViewModel)
    {
        if (donationViewModel == null)
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
            var donation = _mapper.Map<Donation>(donationViewModel);

            if (donationViewModel.RequestId == 0)
            {
                donation.RequestId = null;
            }

            var insertedId = await _unitOfWork.Donations.AddAsync(donation);

            if (insertedId > 0)
            {
                return Ok(new ResponseViewModel<bool>
                {
                    StatusCode = HttpStatusCode.Created,
                    Message = "Thank you for your donation",
                    Data = true
                });
            }
            else
            {
                return Ok(new ResponseViewModel<bool>
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Message = "Cannot add your donation",
                    Data = true
                });
            }
        }
        catch (Exception e)
        {
            return NotFound(new ResponseViewModel<bool>
            {
                StatusCode = HttpStatusCode.NotFound,
                Message = "An error occurred while adding a new donation",
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
            var donationsResult = await _unitOfWork.Donations.GetAllAsync();

            var donations = _mapper.Map<IEnumerable<DonationViewModel>>(donationsResult);

            if (donations.Count() > 0)
            {
                foreach (var donation in donations)
                {
                    var userResult = await _unitOfWork.Users.GetByIdAsync(donation.UserId);
                    donations.Where(p => p.Id == donation.Id).First().User = _mapper.Map<UserViewModel>(userResult);

                    if (donation.RequestId != 0)
                    {
                        var requestResult = await _unitOfWork.Requests.GetByIdAsync(donation.RequestId);
                        donations.Where(p => p.Id == donation.Id).First().Request = _mapper.Map<RequestViewModel>(requestResult);
                    }      
                }
            }

            return Ok(new ResponseViewModel<IEnumerable<DonationViewModel>>
            {
                Data = donations
            });
        }
        catch (Exception e)
        {
            return Ok(new ResponseViewModel<IEnumerable<DonationViewModel>>
            {
                StatusCode = HttpStatusCode.NotFound,
                Message = "An error occured while getting list of donations",
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
            var donationResult = await _unitOfWork.Donations.GetByIdAsync(id);

            if (donationResult == null)
            {
                return NotFound(new ResponseViewModel<DonationViewModel>
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Message = "Request not found",
                    Data = null
                });
            }

            var userResult = await _unitOfWork.Users.GetByIdAsync(donationResult.UserId);
            var requestResult = await _unitOfWork.Requests.GetByIdAsync(donationResult.RequestId.GetValueOrDefault());

            var donation = _mapper.Map<DonationViewModel>(donationResult);
            donation.User = _mapper.Map<UserViewModel>(userResult);
            donation.Request = _mapper.Map<RequestViewModel>(requestResult);

            var response = new ResponseViewModel<DonationViewModel>
            {
                Data = donation
            };

            return Ok(response);
        }
        catch (Exception e)
        {
            return NotFound(new ResponseViewModel<DonationViewModel>
            {
                StatusCode = HttpStatusCode.NotFound,
                Message = "An error occurred while fetching donation details",
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
            var isDeleted = await _unitOfWork.Donations.DeleteAsync(id);

            if (isDeleted > 0)
            {
                return Ok(new ResponseViewModel<bool>
                {
                    Message = "Donation deleted successfully",
                    Data = true
                });
            }
            else
            {
                return Ok(new ResponseViewModel<bool>
                {
                    Message = "Donation not deleted",
                    Data = false
                });
            }
        }
        catch (Exception e)
        {
            return Ok(new ResponseViewModel<bool>
            {
                StatusCode = HttpStatusCode.NotFound,
                Message = "An error occured while deleting donation",
                Data = false,
                ErrorMessage = e.Message,
                ErrorStackTrace = e.StackTrace
            });
        }
    }
}
