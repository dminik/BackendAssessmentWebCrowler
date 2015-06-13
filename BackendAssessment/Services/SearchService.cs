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
		public Uri BaseUri { get; set; }
		IMyLog Log { get; set; }

		public SearchService(IWebPagesRepository webPagesRepository, ITextScaner textScaner, IMyLog log)
		{
			WebPagesRepository = webPagesRepository;
			TextScaner = textScaner;
			Log = log;
		}

		public virtual void Init(Uri baseUri)
		{
			Log.Print("Start Init for " + baseUri.AbsoluteUri);

			WebPagesRepository.Init(baseUri);

			Log.Print("Finish Init for " + baseUri.AbsoluteUri);
		}
		
		public virtual List<string> Search(string phrase)
		{
			if (!WebPagesRepository.IsInit)
				throw new Exception("SearchService is not initialized.");
			
			var resultStrings = WebPagesRepository.Storage.GetCached<List<string>>(phrase, SearchInPageSources);
			return resultStrings;
		}

		private List<string> SearchInPageSources(string text)
		{
			var resultStrings = new List<string>();
			foreach (var currentPageKey in WebPagesRepository.Storage.GetKeys())
			{
				var pageText = WebPagesRepository.Storage.Get<string>(currentPageKey);

				var resultStringsForPage = TextScaner.SearchPhrase(text, pageText).ToList();

				foreach (var curVal in resultStringsForPage.Where(curVal => !resultStrings.Contains(curVal)))
				{
					resultStrings.Add(curVal);
				}
			}
			return resultStrings;
		}
	}
}
