using System;
using System.Collections.Generic;
using System.Text;
using Timetracker.Core.Domain.Model;

namespace Timetracker.Core.DTO
{
    public class ActivityUpsertDTO
    {
        public DateTime WeekStartDate { get; set; }
        public DateTime WeekEndDate { get; set; }
        public IEnumerable<EffortUpsertDTO> Efforts = new List<EffortUpsertDTO>();
    }
    
}
