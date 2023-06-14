using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace KoBooksStoreWeb.Models
{

    public class Book
    {
        [Key]
        public int BookID { get; set; }
        [Required]
        public string? Title { get; set; }
        public string? Description { get; set; }
        [Required]
        [Column(TypeName = "decimal(8, 2)")]
        public decimal Price { get; set; }
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
        [ForeignKey("AuthorID")] //Does this foreign key command require another table? 
        public int? AuthorIDRef { get; set; }
        public int? GenreIDRef { get; set; }
    }
}