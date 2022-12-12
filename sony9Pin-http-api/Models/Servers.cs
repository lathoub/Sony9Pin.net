using System.Linq.Dynamic.Core;

namespace sony9Pin_http_api.Models
{
	public class Servers
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="offset"></param>
		/// <param name="limit"></param>
		/// <param name="filter"></param>
		/// <param name="orderBy"></param>
		public Servers(int offset, int limit, string filter, string orderBy)
		{
			var serviceUrl = "http://localhost:5000";

#if ATHOME
			var server1 = new Models.Server
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
			var server2 = new Models.Server
			{
				id = "123457",
				name = "jane",
				serialNumber = "654321",
				serverType = "XT-GO",
				softwareVersion = "12.12.13",
				localDateTime = DateTime.UtcNow.ToString("o"),
				serverStatus = "available", // available, temporaryUnavailable, unavailable
				links = new List<Link> {
					new Link() { rel = "self", type = "application/json", title = "This document",
						href = new Uri(serviceUrl + $"/servers/{"123457"}?f=json") },
					new Link() { rel = "alternate", type = "text/html", title = "This document as HTML",
						href = new Uri(serviceUrl + $"/servers/{"123457"}?f=html") },
					new Link() { rel = "clips", type = "application/json", title = "This document",
						href = new Uri(serviceUrl + $"/servers/{"123457"}/clips?f=json") },
					new Link() { rel = "clips", type = "text/html", title = "This document as HTML",
						href = new Uri(serviceUrl + $"/servers/{"123457"}/clips?f=html") },
					new Link() { rel = "playlists", type = "application/json", title = "This document",
						href = new Uri(serviceUrl + $"/servers/{"123457"}/playlists?f=json") },
					new Link() { rel = "playlists", type = "text/html", title = "This document as HTML",
						href = new Uri(serviceUrl + $"/servers/{"123457"}/playlists?f=html") },
					new Link() { rel = "programs", type = "application/json", title = "This document",
						href = new Uri(serviceUrl + $"/servers/{"123457"}/programs?f=json") },
					new Link() { rel = "programs", type = "text/html", title = "This document as HTML",
						href = new Uri(serviceUrl + $"/servers/{"123457"}/programs?f=html") },
					new Link() { rel = "recorders", type = "application/json", title = "This document",
						href = new Uri(serviceUrl + $"/servers/{"123457"}/recorders?f=json") },
					new Link() { rel = "recorders", type = "text/html", title = "This document as HTML",
						href = new Uri(serviceUrl + $"/servers/{"123457"}/recorders?f=html") },
				}
			};
			var server3 = new Models.Server
			{
				id = "654321",
				name = "euphegenia",
				serialNumber = "987654",
				serverType = "XsNeo",
				softwareVersion = "12.12.13",
				localDateTime = DateTime.UtcNow.ToString("o"),
				serverStatus = "available", // available, temporaryUnavailable, unavailable
				links = new List<Link> {
					new Link() { rel = "self", type = "application/json", title = "This document",
						href = new Uri(serviceUrl + $"/servers/{"654321"}?f=json") },
					new Link() { rel = "alternate", type = "text/html", title = "This document as HTML",
						href = new Uri(serviceUrl + $"/servers/{"654321"}?f=html") },
					new Link() { rel = "clips", type = "application/json", title = "This document",
						href = new Uri(serviceUrl + $"/servers/{"654321"}/clips?f=json") },
					new Link() { rel = "clips", type = "text/html", title = "This document as HTML",
						href = new Uri(serviceUrl + $"/servers/{"654321"}/clips?f=html") },
					new Link() { rel = "playlists", type = "application/json", title = "This document",
						href = new Uri(serviceUrl + $"/servers/{"654321"}/playlists?f=json") },
					new Link() { rel = "playlists", type = "text/html", title = "This document as HTML",
						href = new Uri(serviceUrl + $"/servers/{"654321"}/playlists?f=html") },
					new Link() { rel = "programs", type = "application/json", title = "This document",
						href = new Uri(serviceUrl + $"/servers/{"654321"}/programs?f=json") },
					new Link() { rel = "programs", type = "text/html", title = "This document as HTML",
						href = new Uri(serviceUrl + $"/servers/{"654321"}/programs?f=html") },
					new Link() { rel = "recorders", type = "application/json", title = "This document",
						href = new Uri(serviceUrl + $"/servers/{"654321"}/recorders?f=json") },
					new Link() { rel = "recorders", type = "text/html", title = "This document as HTML",
						href = new Uri(serviceUrl + $"/servers/{"654321"}/recorders?f=html") },
				}
			};

			var list = new List<Models.Server>() { server1, server2, server3 };
#else
            var list = new List<Models.Server>();// Program.AudioVideoServersManager.GetAudioVideoServers()
                //.Select(audioVideoServer => new DataModels.Server(audioVideoServer))
               // .ToList();
#endif

			if (!string.IsNullOrEmpty(filter))
				list = list.AsQueryable().Where(filter).ToList();
			if (!string.IsNullOrEmpty(orderBy))
				list = list.AsQueryable().OrderBy(orderBy).ToList();

			var servers = list.Skip(offset).Take(limit).ToList();

			var offsetLimit = "";
			if (offset > 0 || limit != 10)
			{
				offsetLimit = $"&offset={offset}";
				if (limit != 10)
					offsetLimit += $"&limit={limit}";
			}

			foreach (var server in servers)
			{
				server.links = new List<Link>
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

			}

			var audioVideoServers = servers.ToList();

			this.links = new List<Link>
			{
				new Link
				{
					rel = "self", type = "application/json", title = "This document",
					href = new Uri(serviceUrl + $"/servers?f=json{offsetLimit}")
				},
				new Link
				{
					rel = "alternate", type = "text/html", title = "This document as HTML",
					href = new Uri(serviceUrl + $"/servers?f=html{offsetLimit}")
				}
			};

			if (offset + limit < list.Count)
				this.links.Add(new Link
				{
					rel = "next",
					title = "Next page",
					type = "text/html",
					href = new Uri(serviceUrl + $"/servers?f=html&offset={offset + limit}&limit={limit}")
				});

			this.title = "Tinkertools audio video server";
			this.description = "Tinkertools video and audio server";
			this.numberMatched = servers.Count;
			this.numberReturned = audioVideoServers.Count;
			this.timeStamp = DateTime.UtcNow.ToString("o");
			this.servers = audioVideoServers;
		}

		public dynamic links { get; }
		public dynamic servers {get; }
		public int numberMatched { get; }
		public int numberReturned { get; }
		public string timeStamp { get; }
		public string title { get; }
		public string description { get; }
	}
}
