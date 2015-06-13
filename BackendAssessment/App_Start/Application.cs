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
		ISearchService SearchService { get; set; }

		public Application(ISearchService searchService)
		{
			SearchService = searchService;
		}

		public void Run()
		{
			//instruction: restore nuget packages
			DefaultApplicationSettings.ApplyToCurrentEnvironment();

			var settings = Settings.FromAppSettings(ConfigurationManager.AppSettings);
			var redis = ConnectionMultiplexer.Connect(settings.RedisEndpoint);
			var http = new NancyHost(settings.HttpEndpointUrl);
			var log = LogManager.GetLogger(typeof(Program));

			http.Start();
			log.InfoFormat("HTTP endpoint listening on", settings.HttpEndpointUrl);

			///* redis test */
			var database = redis.GetDatabase();
			database.StringSet("foo", "bar");
			var stored = database.StringGet("foo") == "bar";

			Console.WriteLine("Program is running, press any key to exit");


			//var downloader = new PageDownloader();
			//downloader.GetListings(new Uri("http://autofac.org/"));


			//var phrase = "components included your"; // Autofac core components are always included, but if your application

			//var resultStrings = new List<string>();

			//foreach (var currentPage in downloader.Pages)
			//{
			//	var resultStringsForPage = TextScaner.SearchPhrase(phrase, currentPage.Value).ToList();
			//	resultStrings.AddRange(resultStringsForPage);
			//}

			//foreach (var currentStr in resultStrings)
			//{
			//	Console.WriteLine(currentStr);
			//}

			Console.ReadKey();

			http.Stop();
		}
	}
}
