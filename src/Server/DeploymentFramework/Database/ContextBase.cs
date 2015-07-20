using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Baud.Deployment.BusinessLogic.Contracts;
using Baud.Deployment.Database.Contracts;

namespace Baud.Deployment.Database
{
    public abstract class ContextBase : DbContext, IDbContext
    {
        private ICurrentUserProvider _currentUserProvider;

        public ICurrentUserProvider CurrentUserProvider
        {
            get
            {
                if (_currentUserProvider == null)
                {
                    throw new InvalidOperationException("CurrentUserProvider was not set on DbContext.");
                }
                return _currentUserProvider;
            }
            set
            {
                _currentUserProvider = value;
            }
        }

        public ContextBase()
            : base()
        {
        }

        public ContextBase(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }

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

        public override int SaveChanges()
        {
            OnSaveChanges();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync()
        {
            OnSaveChanges();
            return base.SaveChangesAsync();
        }

        public override Task<int> SaveChangesAsync(System.Threading.CancellationToken cancellationToken)
        {
            OnSaveChanges();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void OnSaveChanges()
        {
            var currentUserID = CurrentUserProvider.GetCurrentUserID();

            var stateManager = ((IObjectContextAdapter)this).ObjectContext.ObjectStateManager;

            var createdEntries = stateManager.GetObjectStateEntries(EntityState.Added);
            foreach (var item in createdEntries)
            {
                SetCreatedProperties(item, currentUserID);
            }

            var changedEntries = stateManager.GetObjectStateEntries(EntityState.Modified);
            foreach (var item in changedEntries)
            {
                SetModifiedProperties(item, currentUserID);
            }
        }

        private void SetCreatedProperties(ObjectStateEntry entry, int currentUserID)
        {
            var now = DateTime.Now;
            SetDateTimeValue(entry, "Created", now);

            SetInt32Value(entry, "CreatedBy", currentUserID);
        }

        private void SetModifiedProperties(ObjectStateEntry entry, int currentUserID)
        {
            var now = DateTime.Now;
            SetDateTimeValue(entry, "Modified", now);

            SetInt32Value(entry, "ModifiedBy", currentUserID);
        }

        private static void SetInt32Value(ObjectStateEntry entry, string propertyName, int value)
        {
            var propertyMetadata = entry.CurrentValues.DataRecordInfo.FieldMetadata.SingleOrDefault(x => x.FieldType.Name == propertyName);
            if (propertyMetadata.FieldType != null)
            {
                entry.CurrentValues.SetInt32(propertyMetadata.Ordinal, value);
            }
        }

        private static void SetDateTimeValue(ObjectStateEntry entry, string propertyName, DateTime value)
        {
            var propertyMetadata = entry.CurrentValues.DataRecordInfo.FieldMetadata.SingleOrDefault(x => x.FieldType.Name == propertyName);
            if (propertyMetadata.FieldType != null)
            {
                entry.CurrentValues.SetDateTime(propertyMetadata.Ordinal, value);
            }
        }
    }
}