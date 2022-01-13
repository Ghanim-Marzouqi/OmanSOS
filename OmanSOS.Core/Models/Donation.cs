namespace OmanSOS.Core.Models
{
    public class Donation : Base
    {
        // Base Properties
        public int UserId { get; set; }
        public int RequestId { get; set; }
        public decimal Amount { get; set; }

        // Navigation Properties
        public User? User { get; set; }
        public Request? Request { get; set; }
    }
}
