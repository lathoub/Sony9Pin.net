using Microsoft.AspNetCore.Mvc;

namespace sony9Pin_http_api.Controllers
{
    [Route("/servers")]
    public class Servers : Controller
    {
        [HttpGet]
        public IActionResult Get([FromQuery] string f, [FromQuery] int? offset, [FromQuery] int? limit, [FromQuery] string filter, [FromQuery] string orderBy)
        {
	        // Format defaults to json
	        f ??= "json";

	        //var limit = 10;
	        //var offset = 0;
	        //var filter = "";
	        //var orderBy = "";
	        //var sLimit = queryParameters["limit"];
	        //if (!string.IsNullOrEmpty(sLimit)) limit = Convert.ToInt32(sLimit);
	        //var sOffset = queryParameters["offset"];
	        //if (!string.IsNullOrEmpty(sOffset)) offset = Convert.ToInt32(sOffset);
	        //var sFilter = queryParameters["filter"];
	        //if (!string.IsNullOrEmpty(sFilter)) filter = sFilter;
	        //var sOrderBy = queryParameters["orderby"];
	        //if (!string.IsNullOrEmpty(sOrderBy)) orderBy = sOrderBy;

			return f switch
	        {
		        "html" => View(),
		        "json" => new JsonResult(new Models.Servers(0,1000,"","")),
		        _ => Problem(statusCode: StatusCodes.Status400BadRequest, title: $"Invalid value for parameter 'f': {f}. Valid values: json, html.")
	        };
        }
	}
}
