using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Baud.Deployment.BusinessLogic.DataAccess;
using Baud.Deployment.BusinessLogic.DataAccess.Contracts;
using Baud.Deployment.BusinessLogic.Domain.Configuration.Entities;

namespace Baud.Deployment.Database
{
    public class BusinessContext : DbContext, IBusinessContext
    {
        public IDbSet<Project> Projects { get; set; }

        public void AttachAsModified<TEntity>(TEntity entity) where TEntity : class
        {
            var dbEntityEntry = this.Entry(entity);
            if (dbEntityEntry.State == EntityState.Detached)
            {
                Set<TEntity>().Attach(entity);
            }

            // marking all properties as modified
            dbEntityEntry.State = EntityState.Modified;
        }

        public void AttachAsModified<TEntity>(TEntity entity, params System.Linq.Expressions.Expression<Func<TEntity, object>>[] modifiedProperties) where TEntity : class
        {
            var dbEntityEntry = this.Entry(entity);
            if (dbEntityEntry.State == EntityState.Detached)
            {
                Set<TEntity>().Attach(entity);
            }

            if (modifiedProperties.Length > 0)
            {
                for (int i = 0; i < modifiedProperties.Length; i++)
                {
                    var prop = dbEntityEntry.Property(modifiedProperties[i]);
                    prop.IsModified = true;
                }
            }
            else
            {
                // marking all properties as modified
                dbEntityEntry.State = EntityState.Modified;
            }
        }

        public void MarkReferenceModified<TEntity, TProperty>(TEntity entity, System.Linq.Expressions.Expression<Func<TEntity, TProperty>> navigationProperty)
            where TEntity : class
            where TProperty : class
        {
            var reference = Entry(entity).Reference(navigationProperty);
            reference.CurrentValue = reference.CurrentValue;
        }
    }
}