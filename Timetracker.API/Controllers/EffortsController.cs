using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Timetracker.Core.Domain.Interface;
using Timetracker.Core.Domain.Model;
using Timetracker.Core.DTO;
using Timetracker.Core.Utilities;
using TimeTracker.Core.Domain.Interface;
using TimeTracker.Core.Domain.Model;

namespace Timetracker.API.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    [EnableCors("AllowAllHeaders")]
    [ApiController] 
    public class EffortsController : ControllerBase
    {
        private readonly IEffortRepository _effortRepository;
        private readonly IReadOnlyRepositry _readOnlyRepositry;
        private readonly IRepository _repository;

        public EffortsController(IEffortRepository effortRepository, IRepository repository, IReadOnlyRepositry readOnlyRepositry)
        {
            _effortRepository = effortRepository;
            _readOnlyRepositry = readOnlyRepositry;
            _repository = repository;
        }

        [HttpGet("{personId}")] //todo sync with arindam then remove
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<IActionResult>> Get(string personId)
        {
            ValueTuple<DateTime, DateTime>[] dateRanges = new ValueTuple<DateTime, DateTime>[4];
            var today = DateTime.Today;
            var firstWeekStartDate = today.Current().startDate; // Current week
            dateRanges[0] = today.Current();
            var secondWeekStartDate = firstWeekStartDate.Previous().startDate; // Previous/Last Monday 2nd week
            //dateRanges[1] = firstWeekStartDate.Previous();
            dateRanges[1] = dateRanges[0].Item1.PreviousWeek(dateRanges[0].Item2);
            var thirdWeekStartDate = secondWeekStartDate.Previous().startDate; // Previous/Last Monday 3rdweek
            //dateRanges[2] = secondWeekStartDate.Previous();
            dateRanges[2] = dateRanges[1].Item1.PreviousWeek(dateRanges[1].Item2);
            var fourthWeekStartDate = thirdWeekStartDate.Previous().startDate; // Previous/Last Monday 4thweek 
            //dateRanges[3] = thirdWeekStartDate.Previous();
            dateRanges[3] = dateRanges[2].Item1.PreviousWeek(dateRanges[2].Item2);
            var combinedEffort = await _effortRepository.GetEffortForDateRange(personId, dateRanges[3].Item1, dateRanges[0].Item2);

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
                        WeekEndDate = dateRanges[0].Item2,
                        Efforts = currentWeekEffort.Select(effort => EffortDTO.EffortDTOBuilder(effort))
                    },
                    new ActivityDTO
                    {
                        WeekStartDate = dateRanges[1].Item1,
                        WeekEndDate = dateRanges[1].Item2,
                        Efforts = secondWeekEffort.Select(effort => EffortDTO.EffortDTOBuilder(effort))
                    },
                    new ActivityDTO
                    {
                        WeekStartDate = dateRanges[2].Item1,
                        WeekEndDate = dateRanges[2].Item2,
                        Efforts = thirdWeekEffort.Select(effort => EffortDTO.EffortDTOBuilder(effort))
                    },
                    new ActivityDTO
                    {
                        WeekStartDate = dateRanges[3].Item1,
                        WeekEndDate = dateRanges[3].Item2,
                        Efforts = fourthWeekEffort.Select(effort => EffortDTO.EffortDTOBuilder(effort))
                    }
                }
            };

            return new OkObjectResult(dto.Activities);
        }

        [HttpPost]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        public async Task<ActionResult<IActionResult>> Post(ActivityUpsertDTO activitiesToBeUpserted)
        {
            var effortsToBeInserted = activitiesToBeUpserted.Efforts.Where(e => e.Id == 0 && e.ProjectId > 0);
            var effortsToBeUpdated = activitiesToBeUpserted.Efforts.Where(e => e.Id != 0 && !e.IsDeleted && e.ProjectId > 0);
            var effortsToBeDeleted = activitiesToBeUpserted.Efforts.Where(e => e.Id != 0 && e.IsDeleted && e.ProjectId > 0);

            // Insert effort collection
            foreach (var insertedEffortDTO in effortsToBeInserted)
            {
                Effort insertedEffort = EffortUpsertDTO.EffortBuilder(insertedEffortDTO, activitiesToBeUpserted.WeekStartDate, activitiesToBeUpserted.WeekEndDate, null);
                _repository.Create(insertedEffort);
            }

            // Update effort collection
            foreach (var updatedEffortDTO in effortsToBeUpdated)
            {
                var existingEffort = await _readOnlyRepositry.GetByIdAsync<Effort>(updatedEffortDTO.Id);

                if (existingEffort != null)
                {
                    Effort updatedEffort = EffortUpsertDTO.EffortBuilder(updatedEffortDTO,
                        activitiesToBeUpserted.WeekStartDate, activitiesToBeUpserted.WeekEndDate, existingEffort);
                    _repository.Update(updatedEffort);
                }
            }

            // Delete effort collection
            foreach (var deletedEffortDTO in effortsToBeDeleted)
            {
                Effort deletedEffort = EffortUpsertDTO.EffortBuilder(deletedEffortDTO, activitiesToBeUpserted.WeekStartDate, activitiesToBeUpserted.WeekEndDate, null);
                _repository.Delete<Effort>(deletedEffort.Id);
            }

            int numberofOperations = await _repository.SaveAsync();


            return numberofOperations > 0
                ? (ActionResult)new OkObjectResult($"{numberofOperations} succeeded")
                : new BadRequestObjectResult("Something went wrong in payload");
        }

    }
}
