namespace WebShop.Dtos.Read
{
    public class OrderR
    {
        public int Id { get; set; }
        public string Address {  get; set; }    
        public StatusR Status { get; set; }
        public List<OrderItemR> OrderItems { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public decimal TotalPrice { get; set; } 
    }
}
