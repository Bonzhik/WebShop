using System.ComponentModel.DataAnnotations;

namespace WebShop.Models
{
    public class CartProduct
    {
        public string UserId { get; set; }
        [Required]
        public virtual Cart Cart { get; set; }
        public int ProductId { get; set; }
        [Required]
        public virtual Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
