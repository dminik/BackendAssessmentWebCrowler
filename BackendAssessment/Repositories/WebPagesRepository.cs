namespace BackendAssessment.Repositories
{
	using System;
	using System.Collections.Generic;	
	using BackendAssessment.Utilities.Web;
	using log4net;

	public class WebPagesRepository : IWebPagesRepository
	{
		public Dictionary<string, string> Pages { get; set; }
		ILog Log { get; set; }
		IPageDownloader PageDownloader { get; set; }

		public WebPagesRepository(IPageDownloader pageDownloader, ILog log)
		{					
			Log = log;
			PageDownloader = pageDownloader;
		}

		public void Init(Uri baseUri)
		{
			Pages = PageDownloader.GetPages(baseUri);
		}
	}
}
