using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Blog.WebUI.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Podany użytkownik nie istenieje.")]
        [Display(Name = "Nazwa Użytkownika: ")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Hasło jest nieprawidłowe.")]
        [Display(Name = "Hasło: ")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}