using System.ComponentModel.DataAnnotations;

namespace WebShop.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }
        public virtual ICollection<Subcategory> Subcategories { get; set; } = new List<Subcategory>();
        public virtual ICollection<Attribute> Attributes { get; set; } = new List<Attribute>();
    }
}
