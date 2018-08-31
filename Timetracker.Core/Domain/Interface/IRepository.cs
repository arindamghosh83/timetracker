using Timetracker.Core.Domain.Model;

namespace Timetracker.Core.Domain.Interface
{
	public interface IRepository<T,TI> where T : Entity<TI>, new()
	{
		
	}

	
}
