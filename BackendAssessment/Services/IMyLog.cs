namespace BackendAssessment.Services
{	
	using log4net;

	public interface IMyLog
	{
		void Print(string text);
	}

	public class MyLog	 : IMyLog
	{
		ILog Log { get; set; }
		public MyLog()
		{
			Log = LogManager.GetLogger(typeof(Program));
		}

		public void Print(string text)
		{
			Log.Info(text);
		}
	}
}