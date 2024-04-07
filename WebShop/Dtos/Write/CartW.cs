namespace WebShop.Dtos.Write
{
    public class CartW
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public Dictionary<int, int> CartProducts { get; set; }
    }
}
