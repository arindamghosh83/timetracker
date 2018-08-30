using System;
using System.IO;
using Microsoft.Azure.WebJobs.Host.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Timetracker.Functions.Inject
{
	public class InjectConfiguration : IExtensionConfigProvider
	{
		private IServiceProvider _serviceProvider;
		public void Initialize(ExtensionConfigContext context)
		{
			

			var services = new ServiceCollection();
			RegisterServices(services);
			_serviceProvider = services.BuildServiceProvider(true);

			context
				.AddBindingRule<InjectAttribute>()
				.BindToInput<dynamic>(i => _serviceProvider.GetRequiredService(i.Type));
		}

		private static void RegisterServices(IServiceCollection services)
		{
			
		}
	}
}