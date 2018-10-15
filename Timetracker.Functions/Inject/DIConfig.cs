using Autofac;
using AzureFunctions.Autofac.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using Timetracker.Core.Domain.Interface;
using Timetracker.Core.Infrastructure;
using Timetracker.Core.Infrastructure.Data;
using TimeTracker.Core.Domain.Interface;
using TimeTracker.Core.Infrastructure.Data;

namespace Timetracker.Functions.Inject
{
    public class DIConfig
    {
        public DIConfig(string functionName)
        {
            DependencyInjection.Initialize(builder =>
            {
                builder.RegisterType<EffortRepository>().As<IEffortRepository>().InstancePerLifetimeScope();
                builder.RegisterType<EFRepository>().As<IRepository>().InstancePerLifetimeScope();
                builder.RegisterType<EFReadOnlyRepository>().As<IReadOnlyRepositry>().InstancePerLifetimeScope();
            }, functionName);
        }
    }
}
