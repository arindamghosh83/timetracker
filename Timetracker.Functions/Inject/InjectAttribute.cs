using System;
using Microsoft.Azure.WebJobs.Description;

namespace Timetracker.Functions.Inject
{
	//https://blog.wille-zone.de/post/azure-functions-dependency-injection/

	[Binding]
	[AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false)]
	public class InjectAttribute : Attribute
	{
		public InjectAttribute(Type type)
		{
			Type = type;
		}
		public Type Type { get; }
	}
}