using System.ComponentModel.DataAnnotations;

namespace WebShop.Dtos.Read
{
    public class ProductR
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public double Rating { get; set; }
        public string Category {  get; set; }
        public string Subcategory { get; set; }
        public List<AttributeItem> Attributes { get; set; } = new List<AttributeItem>();
    }
}
