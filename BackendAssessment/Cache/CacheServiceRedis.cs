namespace BackendAssessment.Cache
{
	using System;
	using System.Collections.Generic;
	using System.Configuration;
	using System.Linq;

	using BackendAssessment.Model;

	using Newtonsoft.Json;

	using StackExchange.Redis;
	

	public class CacheServiceRedis : ICacheService
	{
		public readonly ConnectionMultiplexer Client;

		public CacheServiceRedis()
		{
			// todo move settings from there
			var settings = Settings.FromAppSettings(ConfigurationManager.AppSettings);			
			Client = ConnectionMultiplexer.Connect(settings.RedisEndpoint);
		}


		public T GetCached<T>(string pKey, Func<string, T> getter, string @group) 
			where T : class
		{
			var key = @group + pKey;

			var redisDb = Client.GetDatabase();
			T result = null;

			var value = redisDb.StringGet(key);
			if (!value.IsNullOrEmpty)
			{
				result = JsonConvert.DeserializeObject<T>(value);
				return result;
			}

			result = Set(pKey, getter, @group);

			return result;
		}

		public T Get<T>(string pKey, string @group)
			where T : class
		{
			var key = @group + pKey;

			var redisDb = Client.GetDatabase();

			var value = redisDb.StringGet(key);
			if (!value.IsNullOrEmpty)
			{
				if (typeof(T).FullName == typeof(string).FullName)
				{
					return value.ToString() as T;
				}
				else
				{
					var result = JsonConvert.DeserializeObject<T>(value);
					return result;
				}
			}
			else
			{
				return null;
			}
		}

		public bool ContainsKey(string pKey, string @group)			
		{
			var key = @group + pKey;

			var redisDb = Client.GetDatabase();			

			return redisDb.KeyExists(key);			
		}

		public T Set<T>(string pKey, Func<string, T> getter, string @group)
			where T : class
		{
			var key = @group + pKey;

			var redisDb = Client.GetDatabase();
			T result = getter(pKey);

			redisDb.StringSet(key, JsonConvert.SerializeObject(result));

			return result;
		}

		public void SetValue<T>(string pKey, T value, string @group)
			where T : class
		{
			var key = @group + pKey;

			var redisDb = Client.GetDatabase();
			
			redisDb.StringSet(key, JsonConvert.SerializeObject(value));			
		}

		public IEnumerable<string> GetKeys()			
		{
			var redisDb = Client.GetServer(Client.GetEndPoints().FirstOrDefault());
			return redisDb.Keys().Select(s=>s.ToString());
		}
		
		public void DeleteKey(string key)
		{
			var redisDb = Client.GetDatabase();
			redisDb.KeyDelete(key);
		}
	}
}
