using Microsoft.Extensions.Caching.Memory;

namespace TerraMours.Framework.Infrastructure.Utils;

public class CacheHelper
{
    private static IMemoryCache _cache;

    static CacheHelper()
    {
        _cache = new MemoryCache(new MemoryCacheOptions());
    }

    /// <summary>  
    /// 获取数据缓存  
    /// </summary>  
    /// <param name="cacheKey">键</param>  
    public static object GetCache(string cacheKey)
    {
        _cache.TryGetValue(cacheKey, out object objCache);
        return objCache;
    }

    /// <summary>  
    /// 设置数据缓存  
    /// </summary>  
    public static void SetCache(string cacheKey, object objObject)
    {
        if (objObject != null)
        {
            _cache.Set(cacheKey, objObject);
        }
    }

    /// <summary>  
    /// 设置数据缓存  
    /// </summary>  
    public static void SetCache(string cacheKey, object objObject, int timeout = 7200)
    {
        if (objObject != null)
        {
            var options = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromSeconds(timeout));
            _cache.Set(cacheKey, objObject, options);
        }
    }

    /// <summary>  
    /// 移除指定数据缓存  
    /// </summary>  
    public static void RemoveAllCache(string cacheKey)
    {
        _cache.Remove(cacheKey);
    }

    /// <summary>  
    /// 移除全部缓存  
    /// </summary>  
    public static void RemoveAllCache()
    {
        _cache.Dispose();
        _cache = new MemoryCache(new MemoryCacheOptions());
    }
}