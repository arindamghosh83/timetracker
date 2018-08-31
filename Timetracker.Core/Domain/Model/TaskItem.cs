using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Timetracker.Core.Domain.Model
{
	public class TaskItem : Entity<int>
	{
		 
		public int TaskId { get; set; }

		public string Name { get; set; }

		public bool IsActive { get; set; }

		public string Description { get; set; }
	}
}
