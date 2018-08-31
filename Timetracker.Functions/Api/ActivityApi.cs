
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;

namespace Timetracker.Functions.Api
{
    public static class ActivityApi
    {
        [FunctionName("GetActivities")]
        public static IActionResult GetActivities([HttpTrigger(AuthorizationLevel.Function, "get",  Route = "api/Activities/{user}/{from}/{To}")]HttpRequest req,string user,string from,string to, ILogger log)
        {
 
            string name = req.Query["name"];

            

            return name != null
                ? (ActionResult)new OkObjectResult($"Hello, {name}")
                : new BadRequestObjectResult("Please pass a name on the query string or in the request body");
        }


	    [FunctionName("UpsertActivities")]
	    public static IActionResult UpsertActivities([HttpTrigger(AuthorizationLevel.Function, "post", Route = "api/Activities")]HttpRequest req, ILogger log)
	    {
		    log.LogInformation("C# HTTP trigger function processed a request.");
		    string requestBody = new StreamReader(req.Body).ReadToEnd();
		    dynamic data = JsonConvert.DeserializeObject(requestBody);
		    var name = data?.name;

		    return name != null
			    ? (ActionResult)new OkObjectResult($"Hello, {name}")
			    : new BadRequestObjectResult("Please pass a name on the query string or in the request body");
		}


	 
	}
}
