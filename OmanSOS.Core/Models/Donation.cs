namespace OmanSOS.Core.Models;

public class Donation : Base
{
    public int UserId { get; set; }
    public int? RequestId { get; set; }
    public int? LocationId { get; set; }
    public decimal Amount { get; set; }
    public string Remarks { get; set; } = string.Empty;
}
