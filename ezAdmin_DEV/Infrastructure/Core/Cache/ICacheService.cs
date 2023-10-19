namespace Infrastructure.Core.Cache
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICacheService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        T Get<T>(string key, string field);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <param name="data"></param>
        void AddOrUpdate(string key, string field, object data);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        void Remove(string key, string field);

        /// <summary>
        /// 
        /// </summary>
        void Clear();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        bool Any(string key, string field);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hKeyName"></param>
        /// <returns></returns>
        string GetHashKey(string key, string hKeyName);
    }
}
