using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Timetracker.Core.Domain.Interface
{
    public interface IReadOnlyRepositry
    {

        Task<IEnumerable<TEntity>> GetAllAsync<TEntity>(
            Expression<Func<TEntity, bool>> filter = null)
            where TEntity : class, IEntity;

    }

}