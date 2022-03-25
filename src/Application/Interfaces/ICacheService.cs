using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
	public interface ICacheService
	{
		void Set(string key, object data, TimeSpan cacheTime);
		Task SetAsync(string key, object data, TimeSpan cacheTime);
		string Get(string key);
		T Get<T>(string key);
		Task<string> GetAsync(string key);
		Task<TEntity> GetAsync<TEntity>(string key);
		Task<bool> ExistAsync(string key);
		bool Exits(string key);
		Task RemoveAsync(string key);
		void Remove(string key);
		IEnumerable<RedisKey> KeysByPattern(string pattern);

	}
}
