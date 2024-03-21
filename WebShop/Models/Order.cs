using System.Numerics;

namespace WebShop.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public User User { get; set; }
        public ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
