namespace GoProShop.DAL.Migrations
{
    using GoProShop.DAL.Entities;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<GoProShop.DAL.EF.GoProShopContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(GoProShop.DAL.EF.GoProShopContext context)
        {
             if (!context.Roles.Any(r => r.Name == "admin" && r.Name == "manager" && r.Name == "user"))
            {
                var store = new RoleStore<ApplicationRole>(context);
                var manager = new RoleManager<ApplicationRole>(store);
                var adminRole = new ApplicationRole { Name = "admin" };
                var managerRole = new ApplicationRole { Name = "manager" };
                var userRole = new ApplicationRole { Name = "user" };
                manager.Create(adminRole);
                manager.Create(managerRole);
                manager.Create(userRole);
            }

            if (!context.Users.Any(u => u.UserName == "admin"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser { UserName = "admin" };

                manager.Create(user, "admin123456");
                manager.AddToRole(user.Id, "admin");
            }
        }
    }
}
