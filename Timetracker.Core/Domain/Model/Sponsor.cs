using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Timetracker.Core.Domain.Model;

namespace Timetracker.Core.Domain
{
	public class Sponsor:Entity<int>
	{
		 
		public int SponsorId { get; set; }

		public string Name { get; set; }

		public List<Project> Projects { get; set; }

		public bool IsActive { get; set; }

		public string ContactEmail { get; set; }

 
	}
}
