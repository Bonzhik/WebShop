namespace WebShop.Models
{
    public class Feedback
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int Rating { get; set; }
        public User User { get; set; }
        public Product Product { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
