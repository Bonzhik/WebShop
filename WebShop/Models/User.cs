using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WebShop.Models
{
    public class User: IdentityUser
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string SurName { get; set; }
        [Required]
        public string MiddleName { get; set; }
        [MaxLength(11)]
        public string PhoneNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public string Avatar {  get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<Feedback> Feedbacks { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
