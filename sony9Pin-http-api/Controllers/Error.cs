using Microsoft.AspNetCore.Mvc;

namespace sony9Pin_http_api.Controllers
{
    [Route("/error")]
    public class ErrorController : ControllerBase
	{
		public IActionResult Error() => Problem();
	}
}
