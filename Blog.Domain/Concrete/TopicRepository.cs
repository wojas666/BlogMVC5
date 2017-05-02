using Blog.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Domain.Entity;

namespace Blog.Domain.Concrete
{
    public class TopicRepository : ITopicRepository
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<Topic> TopicsRepository
        {
            get { return context.Topics; }
        }

        public void Delete(Topic topicToDelete)
        {
            context.Topics.Remove(topicToDelete);
            context.SaveChanges();
        }

        public void Insert(Topic topic)
        {
            context.Topics.Add(topic);
            context.SaveChanges();
        }

        public void Update(Topic editedTopic)
        {
            Topic _temp = context.Topics.Where(e => e.TitleID == editedTopic.TitleID).FirstOrDefault();
            _temp = editedTopic;
        }
    }
}
