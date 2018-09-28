using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TimeTracker.Core.Infrastructure.Data
{
    public class TimeTrackerContextFactory : IDesignTimeDbContextFactory<TimeTrackerContext>
    {
        public TimeTrackerContextFactory()
        {
        }
        public TimeTrackerContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<TimeTrackerContext>();
            builder.UseSqlServer(
                "Server=(LocalDb)\\MSSQLLocalDB;Integrated Security=true;Initial Catalog=TimeTrackerDB;");
            return new TimeTrackerContext(builder.Options);
        }
    }
}
