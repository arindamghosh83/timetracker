using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using Timetracker.Core.Domain;
using Timetracker.Core.Domain.Model;

namespace TimeTracker.Core.Domain.Model
{
    public class Effort : Entity<int>
    {
        [JsonProperty("id")]
        public int EffortId => this.Id;
        public string UserId { get; set; }
        [ForeignKey("ProjectId")]
        public Project Project { get; set; }
        public double EffortPercent { get; set; }
	    public DateTime StartDate { get; set; }
	    public DateTime EndDate { get; set; }
	    public DateTime CreatedOn { get; set; }
	    public DateTime UpdatedOn { get; set; }
	    public string CreatedBy { get; set; }
	    public string UpdatedBy { get; set; }
	}
}
