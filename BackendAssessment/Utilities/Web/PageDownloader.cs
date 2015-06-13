namespace BackendAssessment.Utilities.Web
{
	using System;
	using System.Collections.Generic;	
	using System.Linq;

	using BackendAssessment.Cache;
	using BackendAssessment.Services;

	using log4net;

	public class PageDownloader : IPageDownloader
	{
		//Dictionary<string, string> Pages = new Dictionary<string, string>();

		ICacheService Storage { get; set; }
		
		private Uri InitialUrl { get; set; }
		IMyLog Log { get; set; }

		IWebClient WebClient { get; set; }

		public PageDownloader(IWebClient webClient, IMyLog log, ICacheService storage)
		{
			Log = log;
			WebClient = webClient;
			Storage = storage;
		}

		public virtual void GetPagesToStorage(Uri url)
		{
			if (Storage.ContainsKey(url.OriginalString)) 
				return;

			Log.Print(string.Format("Parsing page {0}", url.AbsoluteUri));

			if (InitialUrl == null)
				InitialUrl = url;

			var pageSrc = WebClient.DownloadString(url);

			var pageLinks = HtmlHelper.FindLinks(pageSrc, InitialUrl);
			var pageLocalLinks = pageLinks.Where(x => x.IsLocal);

			var plainText = HtmlHelper.HtmlToText(pageSrc);
			Storage.SetValue(url.OriginalString, plainText);

			foreach (var currentLink in pageLocalLinks)
			{				
				GetPagesToStorage(currentLink.Href);				
			}			
		}	
	}
}
