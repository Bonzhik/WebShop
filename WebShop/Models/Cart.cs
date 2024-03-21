namespace WebShop.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public User User { get; set; }
        public ICollection<CartProduct> CartProducts { get; set; }

    }
}
