﻿namespace WebShop.Dtos.Read
{
    public class FeedbackR
    {
        public int Id { get; set; }
        public int Rating { get; set; }
        public string Text { get; set; }
        public UserR User { get; set; }
        public bool HaveComments { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
