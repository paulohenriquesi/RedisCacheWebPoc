namespace RedisCacheWebPoc.Services
{
    public interface ICacheService
    {
        void Add(string key, object value);
        T Get<T>(string key);
    }
}