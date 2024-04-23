namespace WebShop.Models
{
    public class AttributeValue
    {
        public int AttributeId { get; set; }
        public virtual Attribute Attribute { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public string Value { get; set; }
    }
}
