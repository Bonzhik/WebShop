namespace WebShop.Dtos.Read
{
    public class CategoryR
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<SubcategoryR> Subcategories { get; set; }
    }
}