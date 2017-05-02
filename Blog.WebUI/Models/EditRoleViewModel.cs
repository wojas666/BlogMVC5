using Blog.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.WebUI.Models
{
    public class EditRoleViewModel
    {
        public AppRole Role { get; set; }
        public IEnumerable<User> Member { get; set; }
        public IEnumerable<User> NonMember { get; set; }
    }
}