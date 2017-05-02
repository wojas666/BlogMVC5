using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Blog.WebUI.Models
{
    public class CreateRoleViewModel
    {
        [Required(ErrorMessage = "Nazwa roli jest nieprawidłowa.")]
        [Display(Name = "Nazwa Roli")]
        public string RoleName { get; set; }
    }
}