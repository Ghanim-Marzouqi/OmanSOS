namespace OmanSOS.Core.Models
{
    public class Request : Base
    {
        // Base Properties
        public int CategoryId { get; set; }
        public int PriorityId { get; set; }
        public int UserId { get; set; }
        public string Description { get; set; } = string.Empty;
        public string? Location { get; set; }

        // Navigation Properties
        public Category? Category { get; set; }
        public Priority? Priority { get; set; }
        public User? User { get; set; }
    }
}
