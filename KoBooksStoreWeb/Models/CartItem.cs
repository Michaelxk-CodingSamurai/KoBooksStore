using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace KoBooksStoreWeb.Models
{
    public class CartItem
    {
        [Key]
        public int CartItemID { get; set; }
        public int Quantity { get; set; }
        public int BookIDRef { get; set; }
        public int CartIDRef { get; set; }

    }
}
