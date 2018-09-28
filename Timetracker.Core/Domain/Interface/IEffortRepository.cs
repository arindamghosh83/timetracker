using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.Core.Domain.Model;

namespace TimeTracker.Core.Domain.Interface
{
    interface IEffortRepository
    {
        Task<List<Effort>> GetEffortForDateRange(int personId, DateTime startDate, DateTime endDate);
    }
}
