namespace WebShop.Dtos.Read
{
    public class CartR
    {
        public List<OrderItemR> CartItems { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
