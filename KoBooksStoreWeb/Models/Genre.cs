using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace KoBooksStoreWeb.Models
{
    public class Genre
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int GenreID { get; set; }
        //[Key]
        //public int TempID { get; set; }
        [Required]
        public string? GenreName { get; set; }
    }
}
