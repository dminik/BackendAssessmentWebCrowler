namespace BackendAssessment.Utilities.Web
{
	using System;
	using System.Collections.Generic;

	public interface IPageDownloader
	{
		Dictionary<string, string> GetPages(Uri url);
	}
}