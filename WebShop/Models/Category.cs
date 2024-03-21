namespace WebShop.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public ICollection<Subcategory> Subcategories { get; set; }
    }
}
