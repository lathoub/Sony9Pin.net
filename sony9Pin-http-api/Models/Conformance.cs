namespace sony9Pin_http_api.Models
{
	public class Conformance
	{
		public dynamic conformsTo
		{
			get
			{
				return new[]
				{
					"http://www.tinkertools.tv/spec/opengate/1.0/conf/core",
					"http://www.tinkertools.tv/spec/opengate/1.0/conf/oas30",
					"http://www.tinkertools.tv/spec/opengate/1.0/conf/html",
					"http://www.tinkertools.tv/spec/opengate/1.0/conf/json"
				};
			}
		}
	}
}
