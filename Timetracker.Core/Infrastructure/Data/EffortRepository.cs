using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Timetracker.Core.Infrastructure;
using TimeTracker.Core.Domain.Interface;
using TimeTracker.Core.Domain.Model;

namespace TimeTracker.Core.Infrastructure.Data
{
    public class EffortRepository : DataRepository<Effort, int>, IEffortRepository
    {
        private DbContext context;
        public EffortRepository(TimeTrackerContext db) : base(db)
        {
            context = db;
        }

        public async Task<List<Effort>> GetEffortForDateRange(int personId, DateTime startDate, DateTime endDate)
        {

            return null;
        }
    }
}
