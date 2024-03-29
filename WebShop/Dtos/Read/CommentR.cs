namespace WebShop.Dtos.Read
{
    public class CommentR
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<CommentR> Comments { get; set; }
    }
}
