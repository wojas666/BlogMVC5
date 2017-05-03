using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.WebUI.Models
{
    public class EditTopicViewModel
    {
        public int TopicID { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime AddedDate { get; set; }
    }
}