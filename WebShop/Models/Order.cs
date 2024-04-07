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
        public virtual User User { get; set; }
        [Required]
        public virtual Status Status { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public decimal TotalPrice {  get; set; }
        public virtual ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
    }
}
