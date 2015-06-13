namespace BackendAssessment
{	
	using System.Net;

	using Autofac;

	using BackendAssessment.App_Start;
	using BackendAssessment.Repositories;
	using BackendAssessment.Services;
	using BackendAssessment.Utilities.TextScaner;
	using BackendAssessment.Utilities.Web;

	using log4net;
	using log4net.Core;
	
	public class IoCConfig
	{
		public static IContainer Container { get; set; }
		public static void Register()
		{			
			var builder = new ContainerBuilder();

			builder.RegisterType<Application>();
			
			//builder.RegisterType(typeof(SearchService)).As(typeof(ISearchService)).SingleInstance();
			//builder.RegisterType(typeof(TextScaner)).As(typeof(ITextScaner)).SingleInstance();
			//builder.RegisterType(typeof(MyWebClient)).As(typeof(IWebClient)).SingleInstance();
			//builder.RegisterType(typeof(PageDownloader)).As(typeof(IPageDownloader)).SingleInstance();
			//builder.RegisterType(typeof(WebPagesRepository)).As(typeof(IWebPagesRepository)).SingleInstance();
			

			
			//builder.Register(c => LogManager.GetLogger(typeof(Program))).As<ILog>().SingleInstance();
			
			Container = builder.Build();			
		}
	}
}