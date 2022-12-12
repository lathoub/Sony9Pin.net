namespace sony9Pin_http_api.Models
{
	public class Link 
	{
		public Uri href { get; internal set; }
		public string rel { get; internal set; }
		public string title { get; internal set; }
		public string type { get; internal set; }
	}
}
