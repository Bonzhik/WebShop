using System.ComponentModel.DataAnnotations;
using System.Net.Http.Headers;

namespace WebShop.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MinLength(20)]
        public string Text { get; set; }
        [Required]
        public User User { get; set; }
        [Required]
        public Product Product { get; set; }
        [Required]
        public Feedback Feedback { get; set; }
        public Comment? ParentComment { get; set; }
        public ICollection<Comment> Comments { get; set;}
    }
}
