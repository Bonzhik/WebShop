namespace WebShop.Dtos.Read
{
    public class CartR
    {
        public List<OrderItemR> CartItems { get; set; } = new List<OrderItemR>();
        public decimal TotalPrice { get; set; }
    }
}
