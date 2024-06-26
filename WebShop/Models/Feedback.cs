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
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        [Required]
        public virtual User User { get; set; }
        [Required]
        public virtual Product Product { get; set; }
        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public bool IsDeleted { get; set; } = false;
    }
}
