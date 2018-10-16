using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Timetracker.Core.Domain.Interface;
using TimeTracker.Core.Infrastructure.Data;

namespace Timetracker.Core.Infrastructure.Data
{
    public class EFRepository: IRepository
    {
        public readonly TimeTrackerContext _context;

        public EFRepository(TimeTrackerContext context)
        {
            this._context = context;
        }


        public virtual void Create<TEntity>(TEntity entity) where TEntity : class, IEntity
        {
            //context.Set<TEntity>().Attach(entity);
            //context.Entry(entity).State = EntityState.Added;
            _context.Set<TEntity>().Add(entity);
        }

        public virtual void Update<TEntity>(TEntity entity) where TEntity : class, IEntity
        {
            _context.Set<TEntity>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete<TEntity>(object id) where TEntity : class, IEntity
        {
            TEntity entity = _context.Set<TEntity>().Find(id);
            if (entity != null)
            {
                Delete(entity);
            }
        }

        public virtual void Delete<TEntity>(TEntity entity)
            where TEntity : class, IEntity
        {
            var dbSet = _context.Set<TEntity>();
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            dbSet.Remove(entity);
        }


        public virtual void Save()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (Exception e)
            {
               
            }
        }

        public async Task<int> SaveAsync()
        {
            try
            {
                return  await _context.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                return 0;
            }
        }
    }
}
