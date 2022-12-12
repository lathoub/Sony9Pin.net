using Microsoft.AspNetCore.Mvc;

namespace sony9Pin_http_api.Controllers
{
    [Route("/api")]
    public class Api : Controller
    {
	    private readonly ILogger<Api> _logger;

	    public Api(ILogger<Api> logger) =>
		    _logger = logger;

		[HttpGet]
        public IActionResult Get([FromQuery] string f)
        {
	        _logger.LogCritical("Hello World");

	        // Format defaults to json
			f ??= "json";

	        return f switch
	        {
		        "html" => View(),
				"json" => new JsonResult("Hello World!"),
				_ => Problem(statusCode: StatusCodes.Status400BadRequest, title: $"Invalid value for parameter 'f': {f}. Valid values: json, html.")
			};
        }
	}
}
