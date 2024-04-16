using Api.Models;

namespace Api.Dtos.CommentDtos
{
    public class CommentDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedtOn { get; set; } = DateTime.UtcNow;
        public string CreatedBy { get; set; } = string.Empty;

        // Stock 
        public int? StockId { get; set; }
    }
}
