using System.ComponentModel.DataAnnotations;

namespace OmanSOS.Admin.Models;

public class AddCampaignModel
{
    [Required(ErrorMessage = "Please enter title")]
    public string Title { get; set; } = string.Empty;

    public DateTime? CampaignDate { get; set; }

    public TimeSpan? CampaignTime { get; set; }

    public string Remarks { get; set; } = string.Empty;
}
