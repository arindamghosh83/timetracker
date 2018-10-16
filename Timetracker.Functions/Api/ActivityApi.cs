
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using Timetracker.Core.Domain.Interface;
using Timetracker.Core.Domain.Model;
using Timetracker.Core.DTO;
using Timetracker.Core.Utilities;
using TimeTracker.Core.Domain.Interface;
using TimeTracker.Core.Domain.Model;
using Willezone.Azure.WebJobs.Extensions.DependencyInjection;

namespace Timetracker.Functions.Api
{
    //[DependencyInjectionConfig(typeof(DIConfig))]
    public static class ActivityApi
    {
        /// <summary>
        /// get list of activities/efforts
        /// </summary>
        /// <param name="req"></param>
        /// <param name="log"></param>
        /// <param name="effortRepository"></param>
        /// <returns></returns>
        [FunctionName("GetActivities")]
        public static async Task<IActionResult> GetActivities([HttpTrigger(AuthorizationLevel.Anonymous, "get",  Route = "GetActivities/{personId}")]HttpRequest req, ILogger log, string personId, [Inject]IEffortRepository effortRepository)
        {

            ValueTuple<DateTime, DateTime>[] dateRanges = new ValueTuple<DateTime, DateTime>[4];
            var today = DateTime.Today;
            var firstWeekStartDate = today.Current().startDate; // Current week
            dateRanges[0] = today.Current();
            var secondWeekStartDate = firstWeekStartDate.Previous().startDate; // Previous/Last Monday 2nd week
            dateRanges[1] = firstWeekStartDate.Previous();
            var thirdWeekStartDate = secondWeekStartDate.Previous().startDate; // Previous/Last Monday 3rdweek
            dateRanges[2] = secondWeekStartDate.Previous();
            var fourthWeekStartDate = thirdWeekStartDate.Previous().startDate; // Previous/Last Monday 4thweek 
            dateRanges[3] = thirdWeekStartDate.Previous();
            var combinedEffort = await effortRepository.GetEffortForDateRange(personId, dateRanges[3].Item1, dateRanges[0].Item2);

            var currentWeekEffort = combinedEffort.Where(effort => effort.StartDate >= dateRanges[0].Item1 && effort.EndDate <= dateRanges[0].Item2);
            var secondWeekEffort = combinedEffort.Where(effort => effort.StartDate >= dateRanges[1].Item1 && effort.EndDate <= dateRanges[1].Item2);
            var thirdWeekEffort = combinedEffort.Where(effort => effort.StartDate >= dateRanges[2].Item1 && effort.EndDate <= dateRanges[2].Item2);
            var fourthWeekEffort = combinedEffort.Where(effort => effort.StartDate >= dateRanges[3].Item1 && effort.EndDate <= dateRanges[3].Item2);

            ActivitiesDTO dto = new ActivitiesDTO()
            {
                Activities = new List<ActivityDTO>()
                {
                    new ActivityDTO
                    {
                        WeekStartDate = dateRanges[0].Item1,
                        WeekEndDate = dateRanges[1].Item2,
                        Efforts = currentWeekEffort.Select(effort => EffortDTO.EffortDTOBuilder(effort))
                    },
                    new ActivityDTO
                    {
                        WeekStartDate = dateRanges[0].Item1,
                        WeekEndDate = dateRanges[1].Item2,
                        Efforts = secondWeekEffort.Select(effort => EffortDTO.EffortDTOBuilder(effort))
                    },
                    new ActivityDTO
                    {
                        WeekStartDate = dateRanges[0].Item1,
                        WeekEndDate = dateRanges[1].Item2,
                        Efforts = thirdWeekEffort.Select(effort => EffortDTO.EffortDTOBuilder(effort))
                    },
                    new ActivityDTO
                    {
                        WeekStartDate = dateRanges[0].Item1,
                        WeekEndDate = dateRanges[1].Item2,
                        Efforts = fourthWeekEffort.Select(effort => EffortDTO.EffortDTOBuilder(effort))
                    }
                }
            };

            return new OkObjectResult(dto);

        }

        /// <summary>
        /// Insert/Update/Delete efforts
        /// </summary>
        /// <param name="req"></param>
        /// <param name="log"></param>
        /// <returns></returns>
	    [FunctionName("UpsertActivities")]
	    public static async Task<IActionResult> UpsertActivities([HttpTrigger(AuthorizationLevel.Function, "post", Route = "upsertactivities")]HttpRequest req, [Inject]IRepository effortRepository, ILogger log)
	    {
		    string requestBody = new StreamReader(req.Body).ReadToEnd();
	        var activitiesToBeUpserted = JsonConvert.DeserializeObject<ActivityUpsertDTO>(requestBody);
	        var effortsToBeInserted = activitiesToBeUpserted.Efforts.Where(e => e.Id == 0);
	        var effortsToBeUpdated = activitiesToBeUpserted.Efforts.Where(e => e.Id != 0 && !e.IsDeleted);
	        var effortsToBeDeleted = activitiesToBeUpserted.Efforts.Where(e => e.Id != 0 && e.IsDeleted);

            // Insert effort collection
            foreach (var insertedEffortDTO in effortsToBeInserted)
            {
                Effort insertedEffort  = EffortUpsertDTO.EffortBuilder(insertedEffortDTO, activitiesToBeUpserted.WeekStartDate, activitiesToBeUpserted.WeekEndDate);
                effortRepository.Create(insertedEffort);
            }

            // Update effort collection
	        foreach (var updatedEffortDTO in effortsToBeUpdated)
	        {
	            Effort updatedEffort = EffortUpsertDTO.EffortBuilder(updatedEffortDTO, activitiesToBeUpserted.WeekStartDate, activitiesToBeUpserted.WeekEndDate);
	            effortRepository.Update(updatedEffort);
	        }

            // Delete effort collection
	        foreach (var deletedEffortDTO in effortsToBeDeleted)
	        {
	            Effort deletedEffort = EffortUpsertDTO.EffortBuilder(deletedEffortDTO, activitiesToBeUpserted.WeekStartDate, activitiesToBeUpserted.WeekEndDate);
	            effortRepository.Delete<Effort>(deletedEffort.Id);
	        }

            int numberofOperations = await  effortRepository.SaveAsync();
	       

		    return numberofOperations > 0
                ? (ActionResult)new OkObjectResult($"{numberofOperations} succeeded")
			    : new BadRequestObjectResult("Something went wrong in payload");
		}


	 
	}
}
