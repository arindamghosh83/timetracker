using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TimeTracker.Core.Domain.Interface;
using TimeTracker.Core.Domain.Model;
using TimeTracker.Core.Infrastructure.Data;

namespace Timetracker.Core.Infrastructure.Data
{
    public class EffortRepository : EFReadOnlyRepository, IEffortRepository
    {
        public EffortRepository() : base()
        {

        }

        public async Task<IEnumerable<Effort>> GetEffortForDateRange(string userID, DateTime startDate, DateTime endDate)
        {

            Expression<Func<Effort, bool>> predicate = e => e.UserId == userID && e.StartDate >= startDate && e.EndDate <= endDate;
            var timeTrackerContext = context as TimeTrackerContext;
            var combinedEffort = await timeTrackerContext.Effort.Where(predicate).Include(e => e.Project).ToListAsync();
            return combinedEffort;


        }
    }
}
