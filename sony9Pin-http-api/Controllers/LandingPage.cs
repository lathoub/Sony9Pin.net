using Microsoft.AspNetCore.Mvc;

namespace sony9Pin_http_api.Controllers
{
    [Route("/")]
    public class LandingPage : Controller
    {
        [HttpGet]
        public IActionResult Get([FromQuery] string f)
        {
            f ??= "json"; // Format defaults to json

            return f switch
            {
                "html" => View(new Models.LandingPage()),
                "json" => new JsonResult(new Models.LandingPage()),
                _ => Problem(statusCode: StatusCodes.Status400BadRequest, title: $"Invalid value for parameter 'f': {f}. Valid values: json, html.")
            };
        }
    }
}
