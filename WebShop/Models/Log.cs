using System.ComponentModel.DataAnnotations;

namespace WebShop.Models
{
    public class Log
    {
        [Key]
        public int Id { get; set; }
        public string Status { get; set; }
        public string UserName { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;

    }
}
