﻿using System.ComponentModel.DataAnnotations;

namespace WebShop.Models
{
    public class Feedback
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MinLength(20)]
        public string Text { get; set; }
        public int Rating { get; set; }
        [Required]
        public User User { get; set; }
        [Required]
        public Product Product { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
