using Blog.Domain.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Concrete
{
    public class EFDbContext : DbContext
    {
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
