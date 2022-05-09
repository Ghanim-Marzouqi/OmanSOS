using System.ComponentModel.DataAnnotations;

namespace OmanSOS.Mobile.Models;

public class ClothDonationModel
{
    [Required(ErrorMessage = "Please choose location")]
    public string Location { get; set; } = string.Empty;

    public string Remarks { get; set; } = string.Empty;
}
