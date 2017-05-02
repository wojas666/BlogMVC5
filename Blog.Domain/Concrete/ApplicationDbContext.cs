using Blog.Domain.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Concrete
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        /*public ApplicationDbContext() : base("IdentityDb") { }
        
        public static ApplicationDbContext Create() { return new ApplicationDbContext(); }
        */
    }
}
