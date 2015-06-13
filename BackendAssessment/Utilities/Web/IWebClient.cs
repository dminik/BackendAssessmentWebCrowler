namespace BackendAssessment.Utilities.Web
{
	using System;

	public interface IWebClient
	{
		string DownloadString(Uri url);
	}
}