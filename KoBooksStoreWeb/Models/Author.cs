using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace KoBooksStoreWeb.Models
{
    public class Author
    {
        [Key]
        public int AuthorID { get; set; }
        [Required]
        public string? Surname { get; set; }
        public string? Forename { get; set; }
    }
}
