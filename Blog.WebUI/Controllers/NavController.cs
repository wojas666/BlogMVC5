using Blog.Domain.Concrete;
using Blog.Domain.Entity;
using Blog.WebUI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static System.Net.WebRequestMethods;

namespace Blog.WebUI.Controllers
{
    public class NavController : Controller
    {
        public PartialViewResult Menu()
        {
            var userManager = HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            var user = userManager.FindByName(User.Identity.Name);

            List<NavigationItem> list = new List<NavigationItem>();

            if (user != null)
            {
                list.Add(new NavigationItem { LinkAddress = "/UserBlog/Index", LinkName = "Mój Blog" });
                list.Add(new NavigationItem { LinkAddress = "/BlogAdmin/Index", LinkName = "Zarządzaj Blogiem" });
                list.Add(new NavigationItem { LinkAddress = "/HomePage/Index", LinkName = "Strona Domowa" });

                if (userManager.IsInRole(user.Id, "Administrator"))
                {
                    list.Add(new NavigationItem { LinkAddress = "/RoleAdmin/Index", LinkName = "Zarządzaj Rolami" });
                }
            }
            else
            {
                list.Add(new NavigationItem { LinkAddress = "/Access/SignIn", LinkName = "Zaloguj" });
                list.Add(new NavigationItem { LinkAddress = "/Account/CreateNewAccount", LinkName = "Zarejestruj" });
            }

            return PartialView(list);
        }
    }
}