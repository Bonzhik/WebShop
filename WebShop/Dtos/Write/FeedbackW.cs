using System.ComponentModel.DataAnnotations;
using WebShop.Models;

namespace WebShop.Dtos.Write
{
    public class FeedbackW
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int Rating { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
    }
}
