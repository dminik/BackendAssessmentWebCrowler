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

        public Settings(Uri httpEndpointUrl, string redisEndpoint)
        {
            Contract.Requires(httpEndpointUrl != null);
            Contract.Requires(httpEndpointUrl.IsAbsoluteUri);
            Contract.Requires(!string.IsNullOrEmpty(redisEndpoint));

            this.httpEndpointUrl = httpEndpointUrl;
            this.redisEndpoint = redisEndpoint;
        }

        public static Settings FromAppSettings(NameValueCollection settings)
        {
            Contract.Requires(settings != null);

            return new Settings(
                new Uri(settings["HttpEndpoint.Url"]),
                settings["Redis.Host"]
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
    }
}
