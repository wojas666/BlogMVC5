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
    public class BlogAdminController : Controller
    {
        private const int Initial_Like = 0;
        private const string Category_Name = "UserBlog";

        // GET: UserBlog
        private ITopicRepository topicRepository;
        private ICategoryRepository categoryRepository;

        public BlogAdminController(ITopicRepository topicRepository, ICategoryRepository categoryRepository)
        {
            this.topicRepository = topicRepository;
            this.categoryRepository = categoryRepository;
        }

        [Authorize(Roles = "User, Administrator")]
        public ViewResult Index()
        {
            var categoryID = categoryRepository.CategoryRepository.Where(e => e.Name.Equals(Category_Name)).Select(e => e.CategoryID).FirstOrDefault();
            var topics = topicRepository
                .TopicsRepository.Where(e => e.UserAddedID.Equals(GetUserId()))
                .OrderByDescending(e => e.AddedDate)
                .ToList();

            return View(topics);
        }

        [Authorize(Roles = "User, Administrator")]
        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CreateTopicViewModel model, String returnUrl)
        {
            if (ModelState.IsValid)
            {
                Topic newTopic = new Topic()
                {
                    Title = model.Title,
                    Text = model.Text,
                    AddedDate = DateTime.Now,
                    Like = 0,
                    UserAddedID = GetUserId(),
                    CategoryID = categoryRepository.CategoryRepository
                                    .Where(e => e.Name.Equals("UserBlog"))
                                    .Select(e => e.CategoryID)
                                    .FirstOrDefault()
                };

                topicRepository.Insert(newTopic);

                return Redirect(returnUrl ?? Url.Action("Index", "UserBlog", new { userID = GetUserId() }));
            }
            else
            {
                return View();
            }
        }
        
        [Authorize(Roles = "User, Administrator")]
        public ActionResult Edit(int topicID)
        {
            Topic topicToEdit = topicRepository
                .TopicsRepository
                .Where(e => e.TitleID == topicID)
                .FirstOrDefault();

            return View(new EditTopicViewModel()
            {
                TopicID = topicToEdit.TitleID,
                Title = topicToEdit.Title,
                Text = topicToEdit.Text,
                AddedDate = topicToEdit.AddedDate
            });
        }

        [HttpPost]
        public ActionResult Edit(EditTopicViewModel model)
        {
            if (ModelState.IsValid)
            {
                Topic topic = topicRepository.TopicsRepository
                    .Where(e => e.TitleID == model.TopicID)
                    .FirstOrDefault();

                if (topic != null)
                {
                    topic.Title = model.Title;
                    topic.Text = model.Text;
                    topicRepository.Update(topic);
                    return RedirectToAction("Index");
                }
                else
                {
                    return View("Error", new string[] { "Wybrany temat nie istnieje!" });
                }
            }
            else
            {
                return View("Error", new string[] { "Model jest nieprawidłowy!" });
            }
        }

        [Authorize(Roles = "User, Administrator")]
        public ActionResult Remove(int topicID)
        {
            Topic topicToRemove = topicRepository.TopicsRepository
                .Where(e => e.TitleID == topicID).FirstOrDefault();

            if (topicToRemove != null)
            {
                topicRepository.Delete(topicToRemove);
                return RedirectToAction("Index");
            }
            else
                return View("Błąd", "Nie rozpoznano tematu!");
        }

        [HttpPost]
        public ActionResult Remove(RemoveTopicViewModel model, String returnUrl)
        {
            if (ModelState.IsValid)
            {
                var topicToRemove = topicRepository.TopicsRepository.Select(e => e.TitleID == model.TopicID).FirstOrDefault();

                Topic topic = ((object)topicToRemove.GetType()) as Topic;

                topicRepository.Delete(topic);

                return Redirect(returnUrl ?? Url.Action("Index", "UserBlog", new { userID = GetUserId() }));
            }
            else
            {
                return View();
            }
        }

        private string GetUserId()
        {
            var userManager = HttpContext.GetOwinContext().GetUserManager<AppUserManager>();

            return userManager.FindByName(User.Identity.Name).Id;
        }
    }
}