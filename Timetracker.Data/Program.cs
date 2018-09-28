using TimeTracker.Core.Infrastructure.Data;

namespace TimeTracker.Data
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var context = new TimeTrackerContextFactory().CreateDbContext(args);

            DbInitializer.Initialize(context).Wait();
        }
    }
}
