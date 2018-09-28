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
        public DateTime DateTime { get; set; }
        public double EffortPercent { get; set; }
        [ForeignKey("ProjectForeignKey")]
        public Project Project { get; set; }
        [ForeignKey("PersonForeignKey")]
        public Person Person { get; set; }
    }
}
