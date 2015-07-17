using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Baud.Deployment.BusinessLogic.Domain.Security.Entities;

namespace Baud.Deployment.Database.Security
{
    public class SecurityDbContext : ContextBase
    {
        public SecurityDbContext()
            : base("name=Entities")
        {
        }

        public IDbSet<User> Users { get; set; }
        public IDbSet<Role> Roles { get; set; }
        public IDbSet<Position> Positions { get; set; }
        public IDbSet<RolePositionLink> RolePositionLinks { get; set; }
        public IDbSet<UserPositionLink> UserPositionLinks { get; set; }

        protected override void OnModelCreating(System.Data.Entity.DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().ToTable("Role", "Security");
            modelBuilder.Entity<Role>().HasMany(r => r.PositionLinks).WithRequired(x => x.Role);

            modelBuilder.Entity<RolePositionLink>().ToTable("Role_Position", "Security")
                .HasKey(x => new { x.RoleID, x.PositionID });

            modelBuilder.Entity<Position>().ToTable("Position", "Security");
            modelBuilder.Entity<Position>().HasMany(p => p.RoleLinks).WithRequired(x => x.Position);
            modelBuilder.Entity<Position>().HasMany(p => p.UserLinks).WithRequired(x => x.Position);

            modelBuilder.Entity<UserPositionLink>().ToTable("User_Position", "Security")
                .HasKey(x => new { x.UserID, x.PositionID });

            modelBuilder.Entity<User>().ToTable("User", "Security")
                .HasMany(u => u.PositionLinks).WithRequired(x => x.User);
        }
    }
}