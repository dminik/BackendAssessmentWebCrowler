namespace BackendAssessment.Utilities.Web
{
	using System;
	using System.Collections.Generic;

	public interface IPageDownloader
	{
		void GetPagesToStorage(Uri url);
	}
}