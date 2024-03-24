using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace WebShop.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public User User { get; set; }
        [Required]
        public Status Status { get; set; }
        [Required]
        public ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
