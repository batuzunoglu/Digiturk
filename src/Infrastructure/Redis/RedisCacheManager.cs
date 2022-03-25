using Application.Interfaces;
using Domain.Settings;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Redis
{
	public class RedisCacheManager : ICacheService
	{
		private readonly ConnectionMultiplexer _redisClient;
		private readonly IDatabase _database;
		private readonly IServer _server;
		public RedisCacheManager(RedisConfiguration redisConfiguration)
		{
			var connectionString = redisConfiguration.ConnectionString;
			_redisClient = ConnectionMultiplexer.Connect(connectionString);
			_database = _redisClient.GetDatabase();
			_server = _redisClient.GetServer(connectionString);
		}

		public string Get(string key) => _database.StringGet(key);

		public TEntity Get<TEntity>(string key)
		{
			var value = _database.StringGet(key);
			if (value.HasValue)
			{
				return Deserialize<TEntity>(value);
			}
			else
			{
				return default(TEntity);
			}
		}

		public async Task<string> GetAsync(string key) => await _database.StringGetAsync(key);

		public async Task<TEntity> GetAsync<TEntity>(string key)
		{
			var value = await _database.StringGetAsync(key);
			if (value.HasValue)
			{
				return Deserialize<TEntity>(value);
			}
			else
			{
				return default(TEntity);
			}
		}

		public void Set(string key, object data, TimeSpan cacheTime) => _database.StringSet(key, Serialize(data), cacheTime);

		public async Task SetAsync(string key, object data, TimeSpan cacheTime) => await _database.StringSetAsync(key, Serialize(data), cacheTime);

		public async Task<bool> ExistAsync(string key) => await _database.KeyExistsAsync(key);

		public bool Exits(string key) => _database.KeyExists(key);

		public async Task RemoveAsync(string key) => await _database.KeyDeleteAsync(key);

		public void Remove(string key) => _database.KeyDelete(key);

		public IEnumerable<RedisKey> KeysByPattern(string pattern)
		{
			return _server.Keys(pattern: pattern);
		}

		private byte[] Serialize(object item)
		{
			var jsonString = JsonConvert.SerializeObject(item);
			return Encoding.UTF8.GetBytes(jsonString);
		}

		private TEntity Deserialize<TEntity>(byte[] value)
		{
			if (value == null)
			{
				return default(TEntity);
			}
			var jsonString = Encoding.UTF8.GetString(value);
			return JsonConvert.DeserializeObject<TEntity>(jsonString);
		}
	}
}
