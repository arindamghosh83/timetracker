using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Timetracker.Core.Domain.Model;

namespace TimeTracker.Core.Domain.Model
{
    public class Person : Entity<int>
    {
        [JsonProperty("id")] public int PersonId => this.Id;
        public string AdLogin { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string TeamName { get; set; }
        public bool Billable { get; set; }
        public bool Active { get; set; }
    }
}
