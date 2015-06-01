using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Baud.Deployment.BusinessLogic.Domain.Security.Entities;

namespace Baud.Deployment.Database
{
    public class SecurityContext : ContextBase
    {
        protected override void OnModelCreating(System.Data.Entity.DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>()
                .ToTable("Role", "Security");
            modelBuilder.Entity<Role>()
                .HasMany(r => r.PositionLinks);

            modelBuilder.Entity<RolePositionLink>()
                .ToTable("Role_Position", "Security")
                .HasKey(x => new { x.RoleID, x.PositionID });

            modelBuilder.Entity<Position>()
                .ToTable("Position", "Security");
            modelBuilder.Entity<Position>()
                .HasMany(p => p.RoleLinks);
            modelBuilder.Entity<Position>()
                .HasMany(p => p.UserLinks);

            modelBuilder.Entity<UserPositionLink>()
                .ToTable("User_Position", "Security")
                .HasKey(x => new { x.UserID, x.PositionID });

            modelBuilder.Entity<User>()
                .ToTable("User", "Security");
            modelBuilder.Entity<User>()
                .HasMany(u => u.PositionLinks);
        }
    }
}