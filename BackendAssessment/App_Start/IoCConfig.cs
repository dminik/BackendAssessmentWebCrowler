namespace BackendAssessment
{
	using Autofac;

	using BackendAssessment.App_Start;
	using BackendAssessment.Services;

	using Nancy.Security;

	public class IoCConfig
	{
		public static IContainer Container { get; set; }
		public static void Register()
		{			
			var builder = new ContainerBuilder();

			builder.RegisterType<Application>();
			
			builder.RegisterType(typeof(SearchService)).As(typeof(ISearchService)).SingleInstance();

			Container = builder.Build();			
		}
	}
}