namespace BackendAssessment
{
	using System.Configuration;

	using Autofac;

	using BackendAssessment.App_Start;
	using BackendAssessment.Cache;
	using BackendAssessment.Model;
	using BackendAssessment.Repositories;
	using BackendAssessment.Services;
	using BackendAssessment.Utilities.TextScaner;
	using BackendAssessment.Utilities.Web;

	public class IoCConfig
	{
		public static IContainer Container { get; set; }
		public static void Register()
		{
			var settings = Settings.FromAppSettings(ConfigurationManager.AppSettings);

			var builder = new ContainerBuilder();

			builder.RegisterType<Application>();

			builder.RegisterType(typeof(SearchService)).As(typeof(ISearchService)).SingleInstance();
			builder.RegisterType(typeof(TextScaner)).As(typeof(ITextScaner)).SingleInstance();
			builder.RegisterType(typeof(MyWebClient)).As(typeof(IWebClient)).SingleInstance();
			builder.RegisterType(typeof(PageDownloader)).As(typeof(IPageDownloader)).SingleInstance();
			builder.RegisterType(typeof(WebPagesRepository)).As(typeof(IWebPagesRepository)).SingleInstance();
			builder.RegisterType(typeof(MyLog)).As(typeof(IMyLog)).SingleInstance();

			builder.RegisterType(typeof(CacheServiceRedis)).As(typeof(ICacheService)).SingleInstance();
				//.WithParameter("redisEndpointUrl", settings.RedisEndpoint).SingleInstance();
			
			Container = builder.Build();			
		}
	}
}