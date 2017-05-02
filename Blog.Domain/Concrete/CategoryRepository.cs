using Blog.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Domain.Entity;

namespace Blog.Domain.Concrete
{
    public class CategoryRepository : ICategoryRepository
    {
        EFDbContext dbContext = new EFDbContext();

        IEnumerable<Category> ICategoryRepository.CategoryRepository
        {
            get
            {
                return dbContext.Categories;
            }
        }

        public void Delete(Category categoryToDelete)
        {
            dbContext.Categories.Remove(categoryToDelete);
            dbContext.SaveChanges();
        }

        public void Insert(Category category)
        {
            dbContext.Categories.Add(category);
            dbContext.SaveChanges();
        }

        public void Update(Category categoryToUpdate)
        {
            Category category = dbContext.Categories.Where(e => e.CategoryID == categoryToUpdate.CategoryID).FirstOrDefault();
            category = categoryToUpdate;
            dbContext.SaveChanges();
        }
    }
}
