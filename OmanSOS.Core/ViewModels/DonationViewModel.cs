namespace OmanSOS.Core.ViewModels;

public class DonationViewModel : BaseViewModel
{
    // Main Properties
    public int UserId { get; set; }
    public int RequestId { get; set; }
    public int LocationId { get; set; }
    public decimal Amount { get; set; }
    public string Remarks { get; set; } = string.Empty;

    // Navigation Properties
    public UserViewModel? User { get; set; }
    public RequestViewModel? Request { get; set; }
    public LocationViewModel? Location { get; set; }
}
