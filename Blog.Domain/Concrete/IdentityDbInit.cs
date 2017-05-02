using System;
using System.Data.Entity;

namespace Blog.Domain.Concrete
{
    public class IdentityDbInit : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            PerformInitialSetup(context);
            base.Seed(context);
        }

        private void PerformInitialSetup(ApplicationDbContext context)
        {
            // initial configuration...
        }
    }
}