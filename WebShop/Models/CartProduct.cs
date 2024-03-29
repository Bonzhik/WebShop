﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebShop.Models
{
    public class CartProduct
    {
        public int CartId { get; set; }
        [Required]
        public Cart Cart { get; set; }
        public int ProductId { get; set; }
        [Required]
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
