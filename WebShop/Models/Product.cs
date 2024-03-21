namespace WebShop.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public Subcategory Subcategory { get; set; }
        public ICollection<Feedback> Feedbacks { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<OrderProduct> OrderProducts { get; set; }
        public ICollection<CartProduct> CartProducts { get; set; }
    }
}
