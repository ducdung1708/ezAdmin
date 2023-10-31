using Infrastructure.ConstantsDefine.AppSetting;
using Infrastructure.Helpers;
using Newtonsoft.Json;

namespace Infrastructure.Core.Cache
{
    /// <summary>
    /// 
    /// </summary>
    public class CacheService : ICacheService
    {
        private RedisConnection _redisConnection;
        private string? _fromSourceUri;

        /// <summary>
        /// 
        /// </summary>
        public CacheService(IConfiguration configuration)
        {
            _redisConnection = new RedisConnection(configuration);
            _fromSourceUri = configuration.GetValue<string>(ezIDKeys.FROM_SOURCE_URI);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <param name="data"></param>
        public void AddOrUpdate(string key, string field, object data)
        {
            string jsonData = JsonConvert.SerializeObject(data);
            _redisConnection.Database.HashSet(key, field, jsonData);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public bool Any(string key, string field)
        {
            return _redisConnection.Database.HashExists(key, field);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Clear()
        {
            _redisConnection.FlushDatabase();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public T Get<T>(string key, string field)
        {
            if (Any(key, field))
            {
                string jsonData = _redisConnection.Database.HashGet(key, field);
                return JsonConvert.DeserializeObject<T>(jsonData);
            }
            return default(T);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        public void Remove(string key, string field)
        {
            _redisConnection.Database.HashDelete(key, field);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hKeyName"></param>
        /// <returns></returns>
        public string GetHashKey(string key, string hKeyName)
        {
            return $"{hKeyName}_{Helper.GetSubDomain(_fromSourceUri)}";
        }
    }
}