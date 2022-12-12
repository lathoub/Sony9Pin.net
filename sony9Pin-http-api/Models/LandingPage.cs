namespace sony9Pin_http_api.Models
{
	public class LandingPage
	{
		public LandingPage()
		{

		}

		public dynamic links
		{
			get
			{
				var serviceUrl = "http://localhost:5000";

				return new List<Models.Link>
				{
					new Models.Link
					{
						rel = "self", type = "application/json", title = "This document",
						href = new Uri(serviceUrl + "?f=json")
					},
					new Models.Link
					{
						rel = "alternate", type = "text/html", title = "This document as HTML",
						href = new Uri(serviceUrl + "?f=html")
					},
					new Models.Link
					{
						rel = "conformance", type = "", title = "conformance classes implemented by this service",
						href = new Uri(serviceUrl + "/conformance")
					},
					new Models.Link
					{
						rel = "service-doc", type = "text/html", title = "Documentation of the API",
						href = new Uri(serviceUrl + "/api?f=html")
					},
					new Models.Link
					{
						rel = "service-desc", type = "application/vnd.oai.openapi+json;version=3.0",
						title = "Definition of the API in OpenAPI 3.0",
						href = new Uri(serviceUrl + "/api?f=json")
					},
					new Models.Link
					{
						rel = "service-desc", type = "application/vnd.oai.openapi;version=3.0",
						title = "Definition of the API in OpenAPI 3.0",
						href = new Uri(serviceUrl + "/api?f=yaml")
					},
					new Models.Link
					{
						rel = "data", type = "", title = "Access the data",
						href = new Uri(serviceUrl + "/servers")
					}
				};
			}
		}

		public string name => "my name";

		public string description => "my description";
	}
}
