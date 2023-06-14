using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace KoBooksStoreWeb.Models
{
    public class ViewModel
    {
        public Book book { get; set; }
        public Author author { get; set; }
        public Genre genre { get; set; }
        public CartItem cartitem { get; set; }
        public Cart cart { get; set; }

    }
}
