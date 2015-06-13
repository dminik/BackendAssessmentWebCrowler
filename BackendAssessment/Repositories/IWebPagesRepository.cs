namespace BackendAssessment.Repositories
{
	using System;
	using System.Collections.Generic;

	public interface IWebPagesRepository
	{
		Dictionary<string, string> Pages { get; set; }

		void Init(Uri baseUri);
	}
}