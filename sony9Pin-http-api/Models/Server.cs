namespace sony9Pin_http_api.Models
{
	public class Server 
	{
		public string id { get; set; }
		public string name { get; set; }
		public string serialNumber { get; set; }
		public string serverType { get; set; }
		public string softwareVersion { get; set; }
		public string localDateTime { get; set; }
		public string serverStatus { get; set; }
		public dynamic links { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Server()
		{

		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="serverId"></param>
		public Server(string serverId)
		{
			var serviceUrl = "http://localhost:5000";

#if ATHOME
			var server = new Models.Server
			{
				id = "123456",
				name = "john",
				serialNumber = "123456",
				serverType = "XT-VIA",
				softwareVersion = "12.12.13",
				localDateTime = DateTime.UtcNow.ToString("o"),
				serverStatus = "available", // available, temporaryUnavailable, unavailable
				links = new List<Link> {
					new Link() { rel = "self", type = "application/json", title = "This document",
						href = new Uri(serviceUrl + $"/servers/{"123456"}?f=json") },
					new Link() { rel = "alternate", type = "text/html", title = "This document as HTML",
						href = new Uri(serviceUrl + $"/servers/{"123456"}?f=html") },
					new Link() { rel = "clips", type = "application/json", title = "This document",
						href = new Uri(serviceUrl + $"/servers/{"123456"}/clips?f=json") },
					new Link() { rel = "clips", type = "text/html", title = "This document as HTML",
						href = new Uri(serviceUrl + $"/servers/{"123456"}/clips?f=html") },
					new Link() { rel = "playlists", type = "application/json", title = "This document",
						href = new Uri(serviceUrl + $"/servers/{"123456"}/playlists?f=json") },
					new Link() { rel = "playlists", type = "text/html", title = "This document as HTML",
						href = new Uri(serviceUrl + $"/servers/{"123456"}/playlists?f=html") },
					new Link() { rel = "programs", type = "application/json", title = "This document",
						href = new Uri(serviceUrl + $"/servers/{"123456"}/programs?f=json") },
					new Link() { rel = "programs", type = "text/html", title = "This document as HTML",
						href = new Uri(serviceUrl + $"/servers/{"123456"}/programs?f=html") },
					new Link() { rel = "recorders", type = "application/json", title = "This document",
						href = new Uri(serviceUrl + $"/servers/{"123456"}/recorders?f=json") },
					new Link() { rel = "recorders", type = "text/html", title = "This document as HTML",
						href = new Uri(serviceUrl + $"/servers/{"123456"}/recorders?f=html") },
				}
			};
#else
  //          var connection = Program.AudioVideoServersManager.GetAudioVideoServer(serverId);
    //        if (null == connection) throw new KeyNotFoundException($"AudioVideoServer {serverId} not found");

            var server = new Models.Server();
#endif

			this.links = new List<Link>
			{
				new Link
				{
					rel = "self", type = "application/json", title = "This document",
					href = new Uri(serviceUrl + $"/servers/{server.id}?f=json")
				},
				new Link
				{
					rel = "alternate", type = "text/html", title = "This document as HTML",
					href = new Uri(serviceUrl + $"/servers/{server.id}?f=html")
				},
				new Link
				{
					rel = "clips", type = "application/json", title = "This document",
					href = new Uri(serviceUrl + $"/servers/{server.id}/clips?f=json")
				},
				new Link
				{
					rel = "clips", type = "text/html", title = "This document as HTML",
					href = new Uri(serviceUrl + $"/servers/{server.id}/clips?f=html")
				},
				new Link
				{
					rel = "playlists", type = "application/json", title = "This document",
					href = new Uri(serviceUrl + $"/servers/{server.id}/playlists?f=json")
				},
				new Link
				{
					rel = "playlists", type = "text/html", title = "This document as HTML",
					href = new Uri(serviceUrl + $"/servers/{server.id}/playlists?f=html")
				},
				new Link
				{
					rel = "programs", type = "application/json", title = "This document",
					href = new Uri(serviceUrl + $"/servers/{server.id}/programs?f=json")
				},
				new Link
				{
					rel = "programs", type = "text/html", title = "This document as HTML",
					href = new Uri(serviceUrl + $"/servers/{server.id}/programs?f=html")
				},
				new Link
				{
					rel = "recorders", type = "application/json", title = "This document",
					href = new Uri(serviceUrl + $"/servers/{server.id}/recorders?f=json")
				},
				new Link
				{
					rel = "recorders", type = "text/html", title = "This document as HTML",
					href = new Uri(serviceUrl + $"/servers/{server.id}/recorders?f=html")
				}
			};
	//		this.id = server.id

		}
	}
}
