using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Timetracker.Core.Domain.Interface;
using Timetracker.Core.DTO;
using TimeTracker.Core.Domain.Interface;
using TimeTracker.Core.Domain.Model;

namespace Timetracker.API.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    [EnableCors("AllowAllHeaders")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IReadOnlyRepositry _projectRepository;
        private readonly IRepository _repository;

        public ProjectsController(IReadOnlyRepositry projectRepository, IRepository repository)
        {
            _projectRepository = projectRepository;
            _repository = repository;
        }

        [HttpGet]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        public async Task<ActionResult<IActionResult>> Get()
        {
            var projects = await _projectRepository.GetAllAsync<Project>();
            return new OkObjectResult(projects);
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<IActionResult>> Post(ProjectDTO projectDTO)
        {
            var project = ProjectDTO.ProjectBuilder(projectDTO);
            int numberofOperations = 0;
            if (project.Id == 0)
            {
                project.CreatedOn = DateTime.Now;
                project.UpdatedOn = DateTime.Now;

                _repository.Create(project);
                numberofOperations = await _repository.SaveAsync();
                return numberofOperations > 0
                    ? (ActionResult)new OkObjectResult($"Project created with id {project.Id}")
                    : new BadRequestObjectResult("Something went wrong");
            }

            var existingProject = await _projectRepository.GetByIdAsync<Project>(project.Id);
            if (existingProject != null)
            {
                project.Id = existingProject.Id;
                project.UpdatedOn = DateTime.Now;
                _repository.Update(project);
                numberofOperations = await _repository.SaveAsync();
                return numberofOperations > 0
                    ? (ActionResult) new OkObjectResult($"Project updated with id {project.Id}")
                    : new BadRequestObjectResult("Something went wrong");
            }

            return new NotFoundResult();
        }
    }
}
