namespace WebShop.Dtos.Read
{
    public class OrderItemR
    {
        public int ProductId { get; set; }
        public string ProductTitle { get; set; }
        public string ImageUrl {  get; set; }
        public decimal Price {  get; set; }
        public int Quantity { get; set; }
    }
}
