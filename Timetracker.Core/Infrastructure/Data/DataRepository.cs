using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Timetracker.Core.Domain.Interface;
using Timetracker.Core.Domain.Model;

namespace Timetracker.Core.Infrastructure
{
	//todo cosmos
	public abstract class DataRepository<T, TI> : IRepository<T, TI> where T : Entity<TI>, new()
	{
		private readonly DbSet<T> _dbSet;

		public DbContext DbContext { get; }

		protected DataRepository(DbContext db)
		{
			DbContext = db;

			_dbSet = DbContext.Set<T>();
		}

		public async Task<T> GetByIdAsync(TI id)
		{
			return await _dbSet.FindAsync(id).ConfigureAwait(false);
		}

		public async Task<List<T>> ListAllAsync()
		{
			return await _dbSet.ToListAsync().ConfigureAwait(false);
		}

		public async Task<List<T>> ListAsync(ISpecification<T> spec)
		{

			var queryableResultWithIncludes = spec.Includes
				.Aggregate(_dbSet.AsQueryable(),
					(current, include) => current.Include(include));


			var secondaryResult = spec.IncludeStrings
				.Aggregate(queryableResultWithIncludes,
					(current, include) => current.Include(include));


			return await secondaryResult
				.Where(spec.Criteria)
				.ToListAsync().ConfigureAwait(false);
		}

		public async Task<T> AddAsync(T entity)
		{
			DbContext.Set<T>().Add(entity);

			return entity;
		}

		public async Task UpdateAsync(T entity)
		{
			DbContext.Entry(entity).State = EntityState.Modified;

		}

		public async Task DeleteAsync(T entity)
		{
			DbContext.Set<T>().Remove(entity);

		}

		public async Task<int> Commit()
		{
			return await DbContext.SaveChangesAsync().ConfigureAwait(false);
		}

		public void Dispose()
		{
			DbContext.Dispose();
			GC.SuppressFinalize(this);
		}
	}

}
