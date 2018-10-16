using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Timetracker.Core.Domain.Interface;
using Timetracker.Core.Infrastructure;
using Timetracker.Core.Infrastructure.Data;
using Timetracker.Functions;
using TimeTracker.Core.Domain.Interface;
using TimeTracker.Core.Infrastructure.Data;
using Willezone.Azure.WebJobs.Extensions.DependencyInjection;

[assembly: WebJobsStartup(typeof(Startup))]
namespace Timetracker.Functions
{
	public class Startup : IWebJobsStartup
    {
        private IConfigurationRoot _config;
		private static readonly string key = TelemetryConfiguration.Active.InstrumentationKey = "2d972f58-1571-4308-ada3-652f7cb8cb9a";
	    private static readonly TelemetryClient TelemetryClient =
		    new TelemetryClient() { InstrumentationKey = key };

		public void Configure(IWebJobsBuilder builder)
        {
			 
            builder.AddDependencyInjection(ConfigureServices);
        }

        private async void ConfigureServices(IServiceCollection services)
        {
	        
			try
			{
				TelemetryClient.TrackTrace("Running Configuration");


				services.AddScoped<IEffortRepository, EffortRepository>();
		        services.AddScoped<IRepository, EFRepository>();
		        services.AddSingleton<IReadOnlyRepositry, EFReadOnlyRepository>();
				TelemetryClient.TrackTrace("Registered Repository");

				var connectionString = Environment.GetEnvironmentVariable("TimeTrackerConnectionString");
 				services.AddDbContext<TimeTrackerContext>(options => options.UseSqlServer(connectionString));
				TelemetryClient.TrackTrace($"Connectionstring { connectionString}");

				IServiceProvider serviceProvider = services.BuildServiceProvider();
		        var context = serviceProvider.GetService(typeof(TimeTrackerContext)) as DbContext;
		        await context.Database.MigrateAsync().ConfigureAwait(false);
				TelemetryClient.TrackTrace($"Run database context");

			}
			catch (Exception e)
	        {
		        TelemetryClient.TrackTrace($"Error { e.Message}");
				throw;
	        }
	        


           
        }

 
	 
	}
}
