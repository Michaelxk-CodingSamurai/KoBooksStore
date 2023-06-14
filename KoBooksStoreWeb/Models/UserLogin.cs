using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace KoBooksStoreWeb.Models
{
    public class UserLogin
    {
        [Key]
        public int UserLoginID { get; set; }
        [Required(ErrorMessage = "Please Enter Username")]
        [Display(Name = "Please Enter Username")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "Please Enter Password")]
        [Display(Name = "Please Enter Password")]
        public string? Passcode { get; set; }
        public int IsActive { get; set; }
    }
}
