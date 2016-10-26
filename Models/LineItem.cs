using System.ComponentModel.DataAnnotations;

namespace BangazonWeb.Models
{
    public class LineItem
    {
        [Key]
        public int LineItemId { get; set; }
        
        [Required]
        public int OrderId { get; set; }
        public Order Order { get; set; }

        [Required]
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
