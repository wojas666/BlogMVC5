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

namespace Blog.WebUI.Controllers
{
    /// <summary>
    /// This controller is responsible for handling
    /// authentication actions.
    /// </summary>
    public class AccessController : Controller
    {
        public AccessController()
        {
           
        }

        // GET: Access
        public ViewResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignIn(LoginViewModel model, String returnUrl)
        {
            var userManager = HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            var authManager = HttpContext.GetOwinContext().Authentication;

            User user = userManager.Find(model.Login, model.Password);

            if (user != null)
            {
                var ident = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                authManager.SignIn(new Microsoft.Owin.Security.AuthenticationProperties { IsPersistent = false }, ident);

                return Redirect(returnUrl ?? Url.Action("Index", "UserBlog", new { userID = user.Id } ));
            }
            else
            {
                ModelState.AddModelError("", "Nieprawidłowy login lub hasło!");
                return View(model);
            }
        }
    }
}