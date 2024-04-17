namespace WebShop.Dtos.Write
{
    public class CartW
    {
        public string UserId { get; set; }
        public Dictionary<int, int> CartProducts { get; set; }
    }
}
