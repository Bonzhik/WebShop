namespace WebShop.Dtos.Write
{
    public class CommentW
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string UserId { get; set; }
        public int ProductId { get; set; }
        public int FeedbackId { get; set; }
        public int ParentCommentId { get; set; }

    }
}
