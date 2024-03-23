using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebShop.Models
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public User User { get; set; }
        public ICollection<CartProduct> CartProducts { get; set; }

    }
}
