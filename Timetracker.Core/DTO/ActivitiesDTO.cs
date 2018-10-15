using System;
using System.Collections.Generic;
using System.Text;
using Timetracker.Core.Domain.Model;

namespace Timetracker.Core.DTO
{
    public class ActivitiesDTO
    {
        public List<ActivityDTO> Activities { get; set; }

        //public ActivitiesDTO()
        //{
        //    Activities = new List<EffortDTO>();
        //}
    }
}
