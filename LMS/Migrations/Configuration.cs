namespace LMS.Migrations
{
    using LMS.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<LMS.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(LMS.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            // Initialize UserManager and RoleManager
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            // Create the "Admin" role if it doesn't exist
            if (!roleManager.RoleExists("Admin"))
            {
                var role = new IdentityRole { Name = "Admin" };
                roleManager.Create(role);
            }

            // Create the default user
            if (userManager.FindByName("admin@schedule.com") == null)
            {
                var user = new ApplicationUser
                {
                    UserName = "admin@schedule.com",
                    Email = "admin@schedule.com",
                    EmailConfirmed = true
                };

                // Create the user with a password
                var result = userManager.Create(user, "Admin@123");

                // Assign user to the "Admin" role
                if (result.Succeeded)
                {
                    userManager.AddToRole(user.Id, "Admin");
                }
            }

            //// Seed data into the Designations table
            //if (!context.Designations.Any())
            //{
            //    context.Designations.AddOrUpdate(
            //        d => d.Name,  // Unique constraint on the Name field to avoid duplicates
            //        new Designation { Name = "Professor", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
            //        new Designation { Name = "Associate Professor", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
            //        new Designation { Name = "Assistant Professor", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
            //        new Designation { Name = "Lecturer", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
            //        new Designation { Name = "Tutor", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now }
            //    );
            //}

            base.Seed(context);
        }
    }
}
