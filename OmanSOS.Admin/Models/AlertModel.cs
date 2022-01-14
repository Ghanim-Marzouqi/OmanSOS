using MudBlazor;

namespace OmanSOS.Admin.Models
{
    public class AlertModel
    {
        public bool IsVisible { get; set; }
        public string Message { get; set; } = string.Empty;
        public Severity AlertType { get; set; }
    }
}
