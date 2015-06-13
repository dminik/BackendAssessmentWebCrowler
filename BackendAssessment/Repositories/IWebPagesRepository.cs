namespace BackendAssessment.Repositories
{
	using System;
	using System.Collections.Generic;

	using BackendAssessment.Cache;

	public interface IWebPagesRepository
	{
		ICacheService Storage { get; set; }

		bool IsInit { get; set; }

		void Init(Uri baseUri);
	}
}