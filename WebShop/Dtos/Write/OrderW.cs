using System.ComponentModel.DataAnnotations;
using WebShop.Models;

namespace WebShop.Dtos.Write
{
    public class OrderW
    {
        public string Address { get; set; }
        public int UserId { get; set; }
        public int[,] OrderProducts { get; set; }
    }
}
