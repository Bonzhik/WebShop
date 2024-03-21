using System.Net.Http.Headers;

namespace WebShop.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public User User { get; set; }
        public Product Product { get; set; }
        public Feedback Feedback { get; set; }
        public Comment? ParentComment { get; set; }
        public ICollection<Comment> Comments { get; set;}
    }
}
