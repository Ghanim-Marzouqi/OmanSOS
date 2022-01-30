namespace OmanSOS.Core.ViewModels;

public class DonationViewModel : BaseViewModel
{
    // Main Properties
    public int UserId { get; set; }
    public int RequestId { get; set; }
    public decimal Amount { get; set; }

    // Navigation Properties
    public UserViewModel? User { get; set; }
    public RequestViewModel? Request { get; set; }
}
