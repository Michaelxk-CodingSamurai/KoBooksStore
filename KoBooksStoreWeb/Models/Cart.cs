using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KoBooksStoreWeb.Models
{
    public class Cart
    {
        [Key]
        public int CartID { get; set; }
    }
}
