using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models
{
    [Table("Comments")]
    public class Comment
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedtOn { get; set; } = DateTime.UtcNow;

        // Stock 

        public int? StockId { get; set; }
        public Stock? Stock { get; set; }

        public string UserId {  get; set; }
        public User User { get; set; }
    }
}
