namespace WebShop.Dtos.Read
{
    public class CartR
    {
        public int Id { get; set; }
        public List<OrderItemR> CartItems { get; set; } = new List<OrderItemR>();
        public decimal TotalPrice { get; set; }
    }
}
