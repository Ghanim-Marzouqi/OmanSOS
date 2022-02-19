namespace OmanSOS.Core.ViewModels;

public class LocationViewModel : BaseViewModel
{
    // Main Properties
    public string Name { get; set; } = string.Empty;

    public override string ToString()
    {
        return Name;
    }
}
