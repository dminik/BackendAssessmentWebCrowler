namespace BackendAssessment.Cache
{
	using System;
	using System.Collections.Generic;

	using StackExchange.Redis;

	public interface ICacheService
	{
		T GetCached<T>(string pKey, Func<string, T> getter, string @group = null)
			where T : class;

		T Get<T>(string pKey, string @group = null)
			where T : class;

		bool ContainsKey(string pKey, string @group = null);

		T Set<T>(string pKey, Func<string, T> getter, string @group = null)
			where T : class;

		void SetValue<T>(string pKey, T value, string @group = null) 
			where T : class;

		void DeleteKey(string key);

		IEnumerable<string> GetKeys();
	}
}
