namespace OmanSOS.Core.ViewModels;

public class RequestViewModel : BaseViewModel
{
    // Main Properties
    public int CategoryId { get; set; }
    public int PriorityId { get; set; }
    public int UserId { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;

    // Navigation Properties
    public CategoryViewModel? Category { get; set; }
    public PriorityViewModel? Priority { get; set; }
    public UserViewModel? User { get; set; }
}
