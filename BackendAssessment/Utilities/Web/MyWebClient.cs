namespace BackendAssessment.Utilities.Web
{
	using System;
	using System.Net;

	public class MyWebClient : IWebClient
	{
		public string DownloadString(Uri url)
		{
			return new WebClient().DownloadString(url);
		}
	}
}