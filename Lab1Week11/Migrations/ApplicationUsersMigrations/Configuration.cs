namespace Lab1Week11.Migrations.ApplicationUsersMigrations
{
    using Lab1Week11.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Lab1Week11.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations\ApplicationUsersMigrations";
        }

        protected override void Seed(ApplicationDbContext context)
        {
            var manager =
                 new UserManager<ApplicationUser>(
                     new UserStore<ApplicationUser>(context));

            var roleManager =
                new RoleManager<IdentityRole>(
                    new RoleStore<IdentityRole>(context));



            roleManager.Create(new IdentityRole { Name = "Admin" });
            roleManager.Create(new IdentityRole { Name = "ClubAdmin" });
            roleManager.Create(new IdentityRole { Name = "Member" });

            context.Users.AddOrUpdate(u => u.Email, new ApplicationUser
            {
                Email = "S12345678@mail.itsligo.ie",
                EmailConfirmed = true,
                UserName = "S12345678@mail.itsligo.ie",
                PasswordHash = new PasswordHasher().HashPassword("Ss1234567$1"),
                SecurityStamp = Guid.NewGuid().ToString(),
            });

            context.Users.AddOrUpdate(u => u.Email, new ApplicationUser
            {
                Email = "powell.paul@itsligo.ie",
                EmailConfirmed = true,
                UserName = "powell.paul@itsligo.ie",
                PasswordHash = new PasswordHasher().HashPassword("Ppowell$1"),
                SecurityStamp = Guid.NewGuid().ToString(),
            });

            context.Users.AddOrUpdate(u => u.Email, new ApplicationUser
            {

                Email = "S00000001@mail.itsligo.ie",
                EmailConfirmed = true,
                UserName = "S00000001@mail.itsligo.ie",
                PasswordHash = new PasswordHasher().HashPassword("SS00000001$1"),
                SecurityStamp = Guid.NewGuid().ToString(),
            });
            context.SaveChanges();
            ApplicationUser admin = manager.FindByEmail("powell.paul@itsligo.ie");
            if (admin != null)
            {
                manager.AddToRoles(admin.Id, new string[] { "Admin", "Member", "ClubAdmin" });
            }
            else
            {
                throw new Exception { Source = "Did not find user" };
            }

            ApplicationUser member = manager.FindByEmail("S12345678@mail.itsligo.ie");
            if (member != null)
            {
                manager.AddToRoles(member.Id, new string[] { "Member" });
            }

            ApplicationUser clubAdmin = manager.FindByEmail("S00000001@mail.itsligo.ie");
            if (manager.FindByEmail("S00000001@mail.itsligo.ie") != null)
            {
                manager.AddToRoles(clubAdmin.Id, new string[] { "ClubAdmin" });
            }

        }

    }
}
