namespace OmanSOS.Core.Models;

public class Donation : Base
{
    public int UserId { get; set; }
    public int RequestId { get; set; }
    public decimal Amount { get; set; }
}
