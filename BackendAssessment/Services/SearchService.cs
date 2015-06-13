using System.Collections.Generic;

namespace BackendAssessment.Services
{
	using System;
	using System.Linq;

	using BackendAssessment.Repositories;
	using BackendAssessment.Utilities.TextScaner;

	using log4net;

	public class SearchService : ISearchService
	{
		IWebPagesRepository WebPagesRepository { get; set; }
		ITextScaner TextScaner { get; set; }
		public bool IsInited { get; set; }
		public Uri BaseUri { get; set; }
		ILog Log { get; set; }

		public SearchService(IWebPagesRepository webPagesRepository, ITextScaner textScaner, ILog log)
		{
			WebPagesRepository = webPagesRepository;
			TextScaner = textScaner;
			Log = log;
		}

		public virtual void Init(Uri baseUri)
		{
			Log.InfoFormat("Start Init for {0}...", baseUri.AbsoluteUri);

			WebPagesRepository.Init(baseUri);
			IsInited = true;

			Log.InfoFormat("Finish Init for {0}.", baseUri.AbsoluteUri);
		}
		
		public virtual List<string> Search(string text)
		{
			if(!IsInited)
				throw new Exception("SearchService is not initialized.");

			var resultStrings = new List<string>();

			foreach (var currentPage in WebPagesRepository.Pages)
			{
				var resultStringsForPage = TextScaner.SearchPhrase(text, currentPage.Value).ToList();
				resultStrings.AddRange(resultStringsForPage);
			}

			return resultStrings;
		}
	}
}
