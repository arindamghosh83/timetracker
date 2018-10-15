using System;
using System.Collections.Generic;
using System.Text;
using TimeTracker.Core.Domain.Model;

namespace Timetracker.Core.DTO
{
    public class EffortDTO
    {
        public int  Id { get; set; }
        public DateTime WeekStartDate { get; set; }
        public DateTime WeekEndDate { get; set; }
        public double EffortPercent { get; set; }

        public ProjectDTO Project { get; set; }


        public static EffortDTO EffortDTOBuilder(Effort effort)
        {
            EffortDTO effortDTO = new EffortDTO()
            {
                WeekStartDate = effort.StartDate,
                WeekEndDate = effort.EndDate,
                EffortPercent = effort.EffortPercent,
                Id = effort.Id,
                Project = new ProjectDTO { Id = effort.Project.Id, Description = effort.Project.Description, Active = effort.Project.Active}
            };
            return effortDTO;
        }
    }
}
