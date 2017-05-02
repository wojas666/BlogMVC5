using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Blog.WebUI.Models
{
    public class ModyficationRoleViewModel
    {
        [Required]
        public string RoleName { get; set; }
        public string[] UsersToAds { get; set; }
        public string[] UsersToRemoves { get; set; }
    }
}