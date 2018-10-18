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
        public int Id { get; set; }
        public string CreatedBy { get; set; }
        public static Effort EffortBuilder(EffortUpsertDTO effortUpsertDTO, DateTime startDate, DateTime endDate, Effort existingEffort)
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
                if (effortUpsertDTO.Id == 0)
                {
                    effort.CreatedBy = effortUpsertDTO.CreatedBy;
                    effort.UserId = effortUpsertDTO.CreatedBy;
                }
                else
                {
                    effort.CreatedOn = existingEffort.CreatedOn;
                    effort.UpdatedOn = DateTime.UtcNow;
                    effort.CreatedBy = existingEffort.CreatedBy;
                    effort.UserId = existingEffort.UserId;
                    effort.UpdatedBy = existingEffort.UpdatedBy;
                }
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