namespace OmanSOS.Core.ViewModels
{
    public class UserTypeViewModel : BaseViewModel
    {
        // Main Properties
        public string Name { get; set; } = string.Empty;

        // Navigation Properties
        public IEnumerable<UserViewModel> Users { get; set; } = new List<UserViewModel>();
    }
}
