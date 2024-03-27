﻿using System.ComponentModel.DataAnnotations;
using WebShop.Models;

namespace WebShop.Dtos.Write
{
    public class CommentW
    {
        public string Text { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int FeedbackId { get; set; }
        public int ParentCommentId { get; set; }

    }
}
