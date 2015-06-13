using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendAssessment.Model
{
	public class Settings
	{
		private readonly Uri httpEndpointUrl;
		private readonly string redisEndpoint;
		private readonly Uri baseUrl;

		public Settings(Uri httpEndpointUrl, string redisEndpoint, Uri baseUrl)
		{
			Contract.Requires(httpEndpointUrl != null);
			Contract.Requires(httpEndpointUrl.IsAbsoluteUri);
			Contract.Requires(baseUrl.IsAbsoluteUri);
			Contract.Requires(!string.IsNullOrEmpty(redisEndpoint));

			this.httpEndpointUrl = httpEndpointUrl;
			this.redisEndpoint = redisEndpoint;
			this.baseUrl = baseUrl;
		}

		public static Settings FromAppSettings(NameValueCollection settings)
		{
			Contract.Requires(settings != null);

			return new Settings(
				new Uri(settings["HttpEndpoint.Url"]),
				settings["Redis.Host"],
				new Uri(settings["baseUrl"])
			);
		}

		public Uri HttpEndpointUrl
		{
			get { return httpEndpointUrl; }
		}

		public string RedisEndpoint
		{
			get { return redisEndpoint; }
		}

		public Uri BaseUrl
		{
			get { return baseUrl; }
		}
	}
}
