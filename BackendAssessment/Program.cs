namespace BackendAssessment
{
	using Autofac;
	using BackendAssessment.App_Start;

	class Program
	{
		static void Main(string[] args)
		{
			IoCConfig.Register();

			var application = IoCConfig.Container.Resolve<Application>();

			application.Run();
		}
	}
}
