namespace BackendAssessment.Services
{
	using System;
	using System.Collections.Generic;

	public interface ISearchService
	{
		List<string> Search(string text);

		void Init(Uri baseUrl);
	}
}