namespace WebShop.Models
{
    public class Subcategory
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
