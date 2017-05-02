using Blog.Domain.Concrete;
using Blog.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Abstract
{
    public interface ITopicRepository
    {
        IEnumerable<Topic> TopicsRepository { get; }

        void Insert(Topic topicToAdd);
        void Update(Topic editedTopic);
        void Delete(Topic topicToDelete);
    }
}
