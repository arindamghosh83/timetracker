using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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

        public void Configure(IWebJobsBuilder builder)
        {

            _config = new ConfigurationBuilder()
                .SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            builder.AddDependencyInjection(ConfigureServices);
        }

        private async void ConfigureServices(IServiceCollection services)
        {
            

            services.AddScoped<IEffortRepository, EffortRepository>();
            services.AddScoped<IRepository, EFRepository>();
            services.AddSingleton<IReadOnlyRepositry, EFReadOnlyRepository>();
            var connectionString = _config.GetConnectionString("SqlConnectionString");
            services.AddDbContext<TimeTrackerContext>(options => options.UseSqlServer(connectionString));

            IServiceProvider serviceProvider = services.BuildServiceProvider();
            var context = serviceProvider.GetService(typeof(TimeTrackerContext)) as DbContext;
            await context.Database.MigrateAsync().ConfigureAwait(false);
        }


    }
}
