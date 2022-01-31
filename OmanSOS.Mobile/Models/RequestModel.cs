using System.ComponentModel.DataAnnotations;

namespace OmanSOS.Mobile.Models;

public class RequestModel
{
    [Range(1, int.MaxValue, ErrorMessage = "Please select a category")]
    public int CategoryId { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Please select a priority")]
    public int PriorityId { get; set; }

    [Required(ErrorMessage = "Please enter request description")]
    public string Description { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please enter location")]
    public string Location { get; set; } = string.Empty;
}
