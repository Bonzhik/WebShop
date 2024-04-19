using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebShop.Models
{
    public class Cart
    {
        [Key]
        public string UserId { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<CartProduct> CartProducts { get; set; } = new List<CartProduct>();
    }
}
