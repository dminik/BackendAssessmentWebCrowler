using System.Collections.Generic;

namespace BackendAssessment.Services
{
	public class SearchService : ISearchService
	{
		public List<string> Search(string text)
		{
			return new List<string>() { "1", "2", "3" };
		}
	}
}
