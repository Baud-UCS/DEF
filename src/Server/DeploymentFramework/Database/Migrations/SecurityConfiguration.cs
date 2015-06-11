using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using Baud.Deployment.BusinessLogic.Domain.Security.Entities;

namespace Baud.Deployment.Database.Migrations
{
    internal sealed class SecurityConfiguration : DbMigrationsConfiguration<Baud.Deployment.Database.Security.SecurityDbContext>
    {
        public SecurityConfiguration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Baud.Deployment.Database.Security.SecurityDbContext context)
        {
            var now = DateTime.Now;

            context.Users.AddOrUpdate(
                u => u.Login,
                new User { Login = "test", FirstName = "John", LastName = "Tester", ActiveFrom = new DateTime(2015, 5, 31), Email = "test@baud.cz", Created = now },
                new User { Login = "demo", FirstName = "Jack", LastName = "Presenter", ActiveFrom = new DateTime(2015, 5, 31), Email = "demo@baud.cz", Created = now });
        }
    }
}