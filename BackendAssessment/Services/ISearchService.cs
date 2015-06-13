namespace BackendAssessment.Services
{
	using System.Collections.Generic;

	public interface ISearchService
	{
		List<string> Search(string text);
	}
}