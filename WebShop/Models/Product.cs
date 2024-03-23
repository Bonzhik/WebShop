using System.ComponentModel.DataAnnotations;

namespace WebShop.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public Subcategory Subcategory { get; set; }
        public ICollection<Feedback> Feedbacks { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<OrderProduct> OrderProducts { get; set; }
        public ICollection<CartProduct> CartProducts { get; set; }
    }
}
