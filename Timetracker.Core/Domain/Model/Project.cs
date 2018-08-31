using System.Collections.Generic;
using Timetracker.Core.Domain.Model;

namespace Timetracker.Core.Domain
{
	public class Project:Entity
	{
		
		public int ProjectId { get; set; }

		public List<Activity> Activities { get; set; }
	}
}
