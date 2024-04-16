using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models
{
    [Table("Portfolios")]

    public class Portfolio
    {

        public string UserId { get; set; }
        public User? User { get; set; }

        public int StockId { get; set; }
        public Stock? Stock { get; set; }
    }
}
