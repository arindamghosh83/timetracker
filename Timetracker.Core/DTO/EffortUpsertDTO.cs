using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Claims;
using System.Text;
using TimeTracker.Core.Domain.Model;

namespace Timetracker.Core.DTO
{
    public class EffortUpsertDTO
    {
        //public DateTime WeekStartDate { get; set; }
        //public DateTime WeekEndDate { get; set; }
        public double EffortPercent { get; set; }

        public bool IsDeleted { get; set; }

        public int ProjectId { get; set; }
        public  int Id { get; set; }
        public static Effort EffortBuilder(EffortUpsertDTO effortUpsertDTO, DateTime startDate, DateTime endDate)
        {
            Effort effort;
            if (!effortUpsertDTO.IsDeleted)  // Insert/Update effort
            {
                effort = new Effort() 
                {
                    StartDate = startDate,
                    EndDate = endDate,
                    EffortPercent = effortUpsertDTO.EffortPercent,
                    ProjectId = effortUpsertDTO.ProjectId,
                    Id = effortUpsertDTO.Id


                };
            }
            else  // Delete Effort
            {
                effort = new Effort() 
                {
                    StartDate = DateTime.UtcNow,
                    EndDate = DateTime.UtcNow,
                    EffortPercent = 0,
                    ProjectId = 0,
                    Id = effortUpsertDTO.Id


                };
            }

            return effort;
        }
    }
}
