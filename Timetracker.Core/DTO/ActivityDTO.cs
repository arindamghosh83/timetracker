using System;
using System.Collections.Generic;
using System.Text;
using Timetracker.Core.DTO;
using TimeTracker.Core.Domain.Model;

namespace Timetracker.Core.Domain.Model
{
    public class ActivityDTO
    {
        public DateTime WeekStartDate { get; set; }
        public DateTime WeekEndDate { get; set; }
        public IEnumerable<EffortDTO> Efforts = new List<EffortDTO>();


    }
}
