using System.Threading.Tasks;

namespace Timetracker.Core.Domain.Interface
{
    public interface IRepository
    {
        void Create<TEntity>(TEntity entity)
            where TEntity : class, IEntity;

        void Update<TEntity>(TEntity entity)
            where TEntity : class, IEntity;

        void Delete<TEntity>(object id)
            where TEntity : class, IEntity;

        void Delete<TEntity>(TEntity entity)
            where TEntity : class, IEntity;

        void Save();

        Task<int> SaveAsync();
    }
}