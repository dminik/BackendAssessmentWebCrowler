namespace BackendAssessment.Repositories
{
	using System;
	using System.Collections.Generic;

	using BackendAssessment.Cache;
	using BackendAssessment.Services;
	using BackendAssessment.Utilities.Web;
	using log4net;

	public class WebPagesRepository : IWebPagesRepository
	{		
		IMyLog Log { get; set; }
		IPageDownloader PageDownloader { get; set; }

		public ICacheService Storage { get; set; }

		private string containsKey = "containsKey";

		public bool IsInit
		{
			get { return Storage.ContainsKey(containsKey); }

			set { Storage.SetValue(containsKey, "true"); }
		}
		
		public WebPagesRepository(IPageDownloader pageDownloader, IMyLog log, ICacheService storage)
		{					
			Log = log;
			PageDownloader = pageDownloader;
			Storage = storage;
		}

		public void Init(Uri baseUri)
		{
			if (!IsInit)
			{
				PageDownloader.GetPagesToStorage(baseUri);
				IsInit = true;
			}
		}
	}
}
