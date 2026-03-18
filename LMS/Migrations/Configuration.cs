namespace LMS.Migrations
{
    using LMS.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<LMS.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }
        protected void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<JobSchedule>()
                .HasRequired(js => js.Location)   // or HasOptional if nullable
                .WithMany(l => l.JobSchedules)    // or .WithMany() if no navigation property
                .HasForeignKey(js => js.LocationId)
                .WillCascadeOnDelete(false);     

              modelBuilder.Entity<EmployeeJobSchedule>()
                .HasRequired(js => js.JobSchedule)   // or HasOptional if nullable
                .WithMany(l => l.)    // or .WithMany() if no navigation property
                .HasForeignKey(js => js.LocationId)
                .WillCascadeOnDelete(false);     

           // base.OnModelCreating(modelBuilder);
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

            //// Seed data into the Branches table
            if (!context.Locations.Any())
            {
                context.Locations.AddOrUpdate(
                    d => d.Name,  // Unique constraint on the Name field to avoid duplicates
                    new Location {Id=1, Name = "Brehnam",IsActive=true, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                    new Location { Id=2, Name = "Location 2", IsActive = true, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                    new Location { Id=3,Name = "Location 3", IsActive = true, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now }
                );
            }

            //// Seed data into the Employees table
            List<string> Employees = new List<string> { "Matt", "Francine", "Katt", "Alejandro", "Nicole", "Gladys", "Kayla", "Gia", "Grace", "Mary", "Makayla", "Marlon", "Terronda", "Bradley", "Melissa", "jose", "Destiny" };

            if (!context.Employees.Any())
            {
                int Id = 1;
                foreach (string employee in Employees)
                {
                    context.Employees.AddOrUpdate(
                    d => d.Name,  // Unique constraint on the Name field to avoid duplicates
                    new Employee { Id = Id, Name = employee, LocationId = 1, Email=employee + "@dennys.com",Phone="12345678",IsActive=true,CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now }
                ) ;
                    Id++;
                }
            }


            //// Seed data into the Job title table
            List<string> JobTitles = new List<string> {"Cook", "Dish Washer", "Waiter", "Manager" };

            if (!context.JobTitles.Any())
            {
                int Id = 1;
                foreach (string jobtitle in JobTitles)
                {
                    context.JobTitles.AddOrUpdate(
                    d => d.Title,  // Unique constraint on the Name field to avoid duplicates
                    new JobTitle { Id = Id, Title = jobtitle,IsActive=true}
                );
                    Id++;
                }
            }

            if (!context.SalaryTypes.Any())
            {
                context.SalaryTypes.AddOrUpdate(
                d => d.Description,  // Unique constraint on the Name field to avoid duplicates
                new SalaryType { Id = 1, Description = "Hourly", IsActive = true },
                 new SalaryType { Id = 2, Description = "Weekly", IsActive = true }
                 );
            }



            base.Seed(context);
        }
    }
}
