using Blog.Domain.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Concrete
{
    public class AppUserManager : UserManager<User>
    {
        public AppUserManager(IUserStore<User> store) : base(store)
        {
        }



        public static AppUserManager Create(IdentityFactoryOptions<AppUserManager> options, Microsoft.Owin.IOwinContext context)
        {
            var manager = new AppUserManager(new UserStore<User>(context.Get<ApplicationDbContext>()));

            return manager;
        }
    }
}
