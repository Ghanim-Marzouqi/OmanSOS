namespace OmanSOS.Core.Models
{
    public class Request : Base
    {
        public int CategoryId { get; set; }
        public int PriorityId { get; set; }
        public int UserId { get; set; }
        public string Description { get; set; } = string.Empty;
        public string? Location { get; set; }
    }
}
