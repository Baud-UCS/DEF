using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Baud.Deployment.Database.Contracts
{
    public interface IDbContext : IDisposable
    {
        void AttachAsModified<TEntity>(TEntity entity) where TEntity : class;

        void AttachAsModified<TEntity>(TEntity entity, params System.Linq.Expressions.Expression<Func<TEntity, object>>[] modifiedProperties) where TEntity : class;

        int SaveChanges();

        Task<int> SaveChangesAsync();

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        void MarkReferenceModified<TEntity, TProperty>(TEntity entity, Expression<Func<TEntity, TProperty>> navigationProperty)
            where TEntity : class
            where TProperty : class;
    }
}