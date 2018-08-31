using System;
using Timetracker.Core.Domain.Model;

namespace Timetracker.Core.Domain
{
	public class Activity:Entity<int>
	{
		
		public int ActivityId { get; set; }

		public Project Project { get; set; }

		public DateTime CurrentDate { get; set; }

		public  double PercentEffort { get; set; }
	}
}
