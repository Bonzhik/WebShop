using System.ComponentModel.DataAnnotations;
using WebShop.Models;

namespace WebShop.Dtos.Write
{
    public class OrderW
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string UserId { get; set; }
        public int StatusId { get; set; }
        public Dictionary<int, int> OrderProducts { get; set; }
    }
}
