using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendAssessment.App_Start
{
	using System.Configuration;

	using BackendAssessment.Model;
	using BackendAssessment.Services;
	using BackendAssessment.Utilities;

	using log4net;

	using Nancy.Hosting.Self;

	using StackExchange.Redis;

	public class Application
	{
		IMyLog Log { get; set; }
		ISearchService SearchService { get; set; }

		public Application(ISearchService searchService, IMyLog log)
		{
			Log = log;
			SearchService = searchService;
		}

		

		public void Run()
		{
			//instruction: restore nuget packages
			DefaultApplicationSettings.ApplyToCurrentEnvironment();

			var settings = Settings.FromAppSettings(ConfigurationManager.AppSettings);
			var redis = ConnectionMultiplexer.Connect(settings.RedisEndpoint);
			var http = new NancyHost(settings.HttpEndpointUrl);
			

			http.Start();
			Log.Print(string.Format("HTTP endpoint listening on {0}", settings.HttpEndpointUrl));

			///* redis test */
			var database = redis.GetDatabase();
			database.StringSet("foo", "bar");
			var stored = database.StringGet("foo") == "bar";

			Console.WriteLine("Program is initing...");
			SearchService.Init(settings.BaseUrl);


			Console.WriteLine("Program is running, press any key to exit");

			Console.ReadKey();

			http.Stop();
		}
	}
}
