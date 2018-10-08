using Newtonsoft.Json;
using System;
using Timetracker.Core.Domain.Model;

namespace TimeTracker.Core.Domain.Model
{
    public class Project : Entity<int>
    {
        [JsonProperty("id")] public int ProjectId => this.Id;
        public string ProjectNumber { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public bool Funded { get; set; }
	    public DateTime CreatedOn { get; set; }
	    public DateTime UpdatedOn { get; set; }
	    public string CreatedBy { get; set; }
	    public string UpdatedBy { get; set; }
    }
}
