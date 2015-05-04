using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Baud.Deployment.BusinessLogic.DataAccess.Contracts
{
    public interface IDbContext : IDisposable
    {
        IDbSet<TEntity> GetDbSet<TEntity>() where TEntity : class;

        bool IsAttached<TEntity>(TEntity entity) where TEntity : class;

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