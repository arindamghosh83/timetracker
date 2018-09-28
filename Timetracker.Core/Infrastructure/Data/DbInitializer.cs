using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TimeTracker.Core.Infrastructure.Data
{
    public static class DbInitializer
    {
        public static async Task Initialize(TimeTrackerContext context)
        {
            await context.Database.MigrateAsync().ConfigureAwait(false);
        }
    }
}
