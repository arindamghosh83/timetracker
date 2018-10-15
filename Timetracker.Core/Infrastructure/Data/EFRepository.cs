using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Timetracker.Core.Domain.Interface;

namespace Timetracker.Core.Infrastructure.Data
{
    public class EFRepository: EFReadOnlyRepository, IRepository
    {
        public virtual void Create<TEntity>(TEntity entity) where TEntity : class, IEntity
        {
            //context.Set<TEntity>().Attach(entity);
            //context.Entry(entity).State = EntityState.Added;
            context.Set<TEntity>().Add(entity);
        }

        public virtual void Update<TEntity>(TEntity entity) where TEntity : class, IEntity
        {
            context.Set<TEntity>().Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete<TEntity>(object id) where TEntity : class, IEntity
        {
            TEntity entity = context.Set<TEntity>().Find(id);
            if (entity != null)
            {
                Delete(entity);
            }
        }

        public virtual void Delete<TEntity>(TEntity entity)
            where TEntity : class, IEntity
        {
            var dbSet = context.Set<TEntity>();
            if (context.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            dbSet.Remove(entity);
        }


        public virtual void Save()
        {
            try
            {
                context.SaveChanges();
            }
            catch (Exception e)
            {
               
            }
        }

        public async Task<int> SaveAsync()
        {
            try
            {
                return  await context.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                return 0;
            }
        }
    }
}
