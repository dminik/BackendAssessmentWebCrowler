namespace BackendAssessment.Utilities.Web
{
	using System;
	using System.Collections.Generic;	
	using System.Linq;	
	using log4net;

	public class PageDownloader : IPageDownloader
	{
		Dictionary<string, string> Pages = new Dictionary<string, string>();

		private Uri InitialUrl { get; set; }
		ILog Log { get; set; }

		IWebClient WebClient { get; set; }

		public PageDownloader(IWebClient webClient, ILog log)
		{					
			Log = log;
			WebClient = webClient;
		}

		public virtual Dictionary<string, string> GetPages(Uri url)
		{
			if(Pages.ContainsKey(url.OriginalString))
				return Pages;

			Log.InfoFormat("Parsing page {0}", url.AbsoluteUri);

			if (InitialUrl == null)
				InitialUrl = url;

			var pageSrc = WebClient.DownloadString(url);

			var pageLinks = HtmlHelper.FindLinks(pageSrc, InitialUrl);
			var pageLocalLinks = pageLinks.Where(x => x.IsLocal);

			var plainText = HtmlHelper.HtmlToText(pageSrc);
			Pages[url.OriginalString] = plainText;

			foreach (var currentLink in pageLocalLinks)
			{				
				GetPages(currentLink.Href);				
			}

			return Pages;
		}	
	}
}
