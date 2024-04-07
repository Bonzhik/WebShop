using System.ComponentModel.DataAnnotations;
using WebShop.Models;

namespace WebShop.Dtos.Write
{
    public class ProductW
    {
        public int Id { get; set; } 
        public string Title { get; set; }
        public string Description { get; set; }
        //public IFormFile Image { get; set; }
        public string ImageUrl { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int SubcategoryId { get; set; }
        public Dictionary<int, string> AttributeValues { get; set; }
    }
}
