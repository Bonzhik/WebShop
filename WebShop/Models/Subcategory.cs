wnamespace WebShop.Models
{
    public class Subcategory
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public Category Category { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
