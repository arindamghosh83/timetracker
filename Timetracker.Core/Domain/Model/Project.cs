using Newtonsoft.Json;
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
    }
}
