namespace WebShop.Models
{
    public class Attribute
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public virtual ICollection<AttributeValue> AttributeValues { get; set; } = new List<AttributeValue>();
        public virtual ICollection<Category> Categories { get; set; } = new List<Category>();
    }
}
