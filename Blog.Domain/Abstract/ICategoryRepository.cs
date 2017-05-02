using Blog.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Abstract
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> CategoryRepository { get; }

        void Insert(Category category);
        void Update(Category categoryToUpdate);
        void Delete(Category categoryToDelete);
    }
}
