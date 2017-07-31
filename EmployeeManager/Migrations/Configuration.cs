namespace EmployeeManager.Migrations
{
    using EmployeeManager.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<EmployeeManager.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "EmployeeManager.Models.ApplicationDbContext";
        }

        protected override void Seed(EmployeeManager.Models.ApplicationDbContext context)
        {
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            if (!context.Users.Any(t => t.UserName == "admin@empmanager.com"))
            {
                var user = new ApplicationUser
                {
                    UserName = "admin@empmanager.com",
                    Email = "admin@empmanager.com",
                };

                userManager.Create(user, "!Q2w3e4r");

                new Employee()
                {
                    ApplicationUserId = user.Id,
                    FirstName = "admin",
                    LastName = "admin"
                };

                context.Roles.AddOrUpdate(r => r.Name, new IdentityRole { Name = "Admin" });
                context.SaveChanges();
                userManager.AddToRole(user.Id, "Admin");
            }

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
     