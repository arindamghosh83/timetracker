using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Timetracker.Core.Domain.Interface;
using TimeTracker.Core.Domain.Model;
using TimeTracker.Core.Infrastructure.Data;

namespace Timetracker.Core.Infrastructure
{


    public class EFReadOnlyRepository : IReadOnlyRepositry
    {
        public readonly TimeTrackerContext _context;

        public EFReadOnlyRepository(TimeTrackerContext context)
        {
            this._context = context;
        }


        public async Task<IEnumerable<TEntity>> GetAllAsync<TEntity>(Expression<Func<TEntity, bool>> filter = null) where TEntity : class, IEntity
        {
            return GetQueryable<TEntity>(filter,"").ToList();
        }

        protected virtual IQueryable<TEntity> GetQueryable<TEntity>(
        Expression<Func<TEntity, bool>> filter = null,
        string includeProperties = null)
        where TEntity : class, IEntity
        {
            includeProperties = includeProperties ?? string.Empty;
            IQueryable<TEntity> query = this._context.Set<TEntity>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            return query;
        }

        public async Task<IEnumerable<Effort>> GetEffortForDateRange(string userID, DateTime startDate, DateTime endDate)
        {

            Expression<Func<Effort, bool>> predicate = e => e.UserId == userID && e.StartDate >= startDate && e.EndDate <= endDate;
            var combinedEffort = await this._context.Effort.Where(predicate).Include(e => e.Project).ToListAsync();
            return combinedEffort;


        }


    }


}
