namespace OmanSOS.Core.Models;

public class Category : Base
{
    public string Name { get; set; } = string.Empty;
    public bool IsEmergency { get; set; }
}
