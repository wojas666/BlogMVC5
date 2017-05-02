using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Blog.WebUI.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Podany login jest już w użyciu!")]
        [Display(Name = "Nazwa Użytkownika: ")]
        public string Login { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Błędne hasło!")]
        [Display(Name = "Hasło: ")]
        public string Password { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Podany adress E-Mail jest nieprawidłowy bądź jest już w użyciu!")]
        [Display(Name = "Adres E-Mail: ")]
        public string EMail { get; set; }

        [Display(Name = "Imię: ")]
        public string FirstName { get; set; }

        [Display(Name = "Nazwisko: ")]
        public string LastName { get; set; }

        [Display(Name = "Wiek: ")]
        public int Age { get; set; }

        [Display(Name = "Płeć: ")]
        public string Gender { get; set; }
    }
}