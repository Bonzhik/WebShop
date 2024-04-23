namespace WebShop.Dtos.Read
{
    public class OrderR
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string Status { get; set; }
        public string UserId { get; set; }
        public List<OrderItemR> OrderItems { get; set; } = new List<OrderItemR>();
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
