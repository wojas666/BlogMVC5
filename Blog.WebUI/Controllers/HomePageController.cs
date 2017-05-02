using Blog.Domain.Abstract;
using Blog.Domain.Concrete;
using Blog.WebUI.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static System.Net.WebRequestMethods;

namespace Blog.WebUI.Controllers
{
    public class HomePageController : Controller
    {
        ITopicRepository topics;
        ICategoryRepository category;

        // GET: HomePage
        public HomePageController(ITopicRepository topics, ICategoryRepository category)
        {
            this.topics = topics;
            this.category = category;
        }

        public ActionResult Index()
        {
            int categoryID = -1;

            using (EFDbContext db = new EFDbContext())
            {
                categoryID = db.Categories.Where(e => e.Name.Equals("HomePage")).Select(e => e.CategoryID).FirstOrDefault();
            }

            var topicToView = topics.TopicsRepository.Where(e => e.CategoryID == categoryID).OrderByDescending(e => e.AddedDate).ToList();

            return View(topicToView);
        }

        [Authorize(Roles = "Administrator")]
        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CreateTopicViewModel model)
        {
            if(model.Title != string.Empty &&
               model.Text != string.Empty &&
               model.Text.Length > 100)
            {
                // Add logged user id do new Topic.
                topics.Insert(new Domain.Entity.Topic() { Title = model.Title, Text = model.Text, AddedDate = DateTime.Now, UserAddedID = User.Identity.GetUserId(), Like = 0, CategoryID = category.CategoryRepository.Where(e=>e.Name.Equals("HomePage")).FirstOrDefault().CategoryID});

                return RedirectToAction("Index");
            }

            return View();
        }
    }
}