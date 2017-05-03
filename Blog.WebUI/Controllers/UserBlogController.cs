using Blog.Domain.Abstract;
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
    public class UserBlogController : Controller
    {
        private const int Initial_Like = 0;
        private const string Category_Name = "UserBlog";

        // GET: UserBlog
        private ITopicRepository topicRepository;
        private ICategoryRepository categoryRepository;

        public UserBlogController(ITopicRepository topicRepository, ICategoryRepository categoryRepository)
        {
            this.topicRepository = topicRepository;
            this.categoryRepository = categoryRepository;
        }
        
        public ActionResult Index(string userID)
        {
            var userManager = HttpContext.GetOwinContext().GetUserManager<AppUserManager>();

            if (userID == null)
                return RedirectToAction("Index", "UserBlog", new { userID = userManager.FindByName(User.Identity.Name).Id });

            var topic = topicRepository.TopicsRepository
                .Where(e => e.UserAddedID == userID)
                .OrderByDescending(e=>e.AddedDate).ToList();


            if (topic != null)
                return View(topic);
            
            return View();
        }

        [HttpPost]
        public ActionResult Comment()
        {

            // Implemant action...
            return View();
        }
    }
}