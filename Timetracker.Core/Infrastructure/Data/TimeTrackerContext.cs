using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using TimeTracker.Core.Domain.Model;

namespace TimeTracker.Core.Infrastructure.Data
{
    public class TimeTrackerContext : DbContext
    {
        public TimeTrackerContext(DbContextOptions<TimeTrackerContext> options) : base(options)
        {
        }

        public DbSet<Effort> Effort { get; set; }
        public DbSet<Project> Project { get; set; }
        public DbSet<Person> Person { get; set; }
    }
}
