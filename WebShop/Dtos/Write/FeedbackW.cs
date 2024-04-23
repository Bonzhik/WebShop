namespace WebShop.Dtos.Write
{
    public class FeedbackW
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int Rating { get; set; }
        public string UserId { get; set; }
        public int ProductId { get; set; }
    }
}
