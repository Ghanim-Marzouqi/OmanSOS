namespace OmanSOS.Core.Models;

public class Campaign : Base
{
    public string Title { get; set; } = string.Empty;
    public DateTime CampaignDate { get; set; }
    public string CampaignTime { get; set; } = string.Empty;
    public string Remarks { get; set; } = string.Empty;
}
