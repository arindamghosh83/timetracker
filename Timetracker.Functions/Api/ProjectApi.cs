
using System;
using System.IO;
using System.Threading.Tasks;
using AzureFunctions.Autofac;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Timetracker.Core.Domain.Interface;
using Timetracker.Functions.Inject;
using TimeTracker.Core.Domain.Model;

namespace Timetracker.Functions.Api
{
    [DependencyInjectionConfig(typeof(DIConfig))]
    public static class ProjectApi
    {
        [FunctionName("GetProjects")]
        public static async Task<IActionResult> GetProjects([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "projects")]HttpRequest req, ILogger log, [Inject]IReadOnlyRepositry projectRepository)
        {
            var projects =  await projectRepository.GetAllAsync<Project>();
            return new OkObjectResult(projects);

        }
    }
}
