using System.ComponentModel.DataAnnotations;

namespace WebShop.Models
{
    public class OrderProduct
    {
        public int OrderId { get; set; }
        [Required]
        public virtual Order Order { get; set; }
        public int ProductId { get; set; }
        [Required]
        public virtual Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
