using Microsoft.AspNetCore.Mvc;

namespace sony9Pin_http_api.Controllers
{
    [Route("/servers/{serverId}")]
    public class Server : Controller
    {
        [HttpGet]
        public IActionResult Get([FromQuery] string f, string serverId)
        {
	        // Format defaults to json
	        f ??= "json";

	        return f switch
	        {
		        "html" => View(),
		        "json" => new JsonResult(new Models.Server(null)),
		        _ => Problem(statusCode: StatusCodes.Status400BadRequest, title: $"Invalid value for parameter 'f': {f}. Valid values: json, html.")
	        };
        }
	}
}
