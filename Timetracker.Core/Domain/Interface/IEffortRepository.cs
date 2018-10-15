using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.Core.Domain.Model;

namespace TimeTracker.Core.Domain.Interface
{
    public interface IEffortRepository
    {

        Task<IEnumerable<Effort>> GetEffortForDateRange(string userID, DateTime startDate, DateTime endDate);
        

    }
}
