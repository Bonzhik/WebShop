using System.ComponentModel.DataAnnotations;

namespace WebShop.Models
{
    public class OrderProduct
    {
        public int OrderId { get; set; }
        [Required]
        public Order Order { get; set; }
        public int ProductId { get; set; }
        [Required]
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
