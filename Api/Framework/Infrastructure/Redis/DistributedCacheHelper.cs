using Microsoft.Extensions.Caching.Distributed;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TerraMours.Framework.Infrastructure.Redis
{
    public class DistributedCacheHelper : IDistributedCacheHelper
    {
        private readonly IDistributedCache _distCache;

        public DistributedCacheHelper(IDistributedCache distCache)
        {
            _distCache = distCache;
        }

        private static DistributedCacheEntryOptions CreateOptions(int baseExpireSeconds)
        {
            //过期时间.Random.Shared 是.NET6新增的
            double sec = Random.Shared.Next(baseExpireSeconds, baseExpireSeconds * 2);
            TimeSpan expiration = TimeSpan.FromSeconds(sec);
            DistributedCacheEntryOptions options = new DistributedCacheEntryOptions();
            options.AbsoluteExpirationRelativeToNow = expiration;
            return options;
        }

        /// <summary>
        /// redis同步方法，默认一天过期
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="cacheKey"></param>
        /// <param name="valueFactory"></param>
        /// <param name="expireSeconds"></param>
        /// <returns></returns>
        public TResult? GetOrCreate<TResult>(string cacheKey, Func<DistributedCacheEntryOptions, TResult?> valueFactory, int expireSeconds = 86400)
        {
            string jsonStr = _distCache.GetString(cacheKey);
            //缓存中不存在
            if (string.IsNullOrEmpty(jsonStr))
            {
                var options = CreateOptions(expireSeconds);
                TResult? result = valueFactory(options);//如果数据源中也没有查到，可能会返回null
                //null会被json序列化为字符串"null"，所以可以防范“缓存穿透”
                string jsonOfResult = JsonSerializer.Serialize(result,
                    typeof(TResult));
                _distCache.SetString(cacheKey, jsonOfResult, options);
                return result;
            }
            else
            {
                //"null"会被反序列化为null
                //TResult如果是引用类型，就有为null的可能性；如果TResult是值类型
                //在写入的时候肯定写入的是0、1之类的值，反序列化出来不会是null
                //所以如果obj这里为null，那么存进去的时候一定是引用类型
                _distCache.Refresh(cacheKey);//刷新，以便于滑动过期时间延期
                return JsonSerializer.Deserialize<TResult>(jsonStr)!;
            }
        }

        /// <summary>
        /// redis异步方法，默认一天过期
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="cacheKey"></param>
        /// <param name="valueFactory"></param>
        /// <param name="expireSeconds"></param>
        /// <returns></returns>
        public async Task<TResult?> GetOrCreateAsync<TResult>(string cacheKey, Func<DistributedCacheEntryOptions, Task<TResult?>> valueFactory, int expireSeconds = 86400)
        {
            string jsonStr = await _distCache.GetStringAsync(cacheKey);
            var jsonOptions = new JsonSerializerOptions
            {
                //支持循环调用，EF Core 使用include 之后会报错
                //ReferenceHandler = ReferenceHandler.Preserve,
                //WriteIndented = true,
                //不设置这个，那么符号和汉字都会变成Unicode
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                //通过设置 JsonSerializerOptions 的 IgnoreReadOnlyFields 或 IgnoreNullValues 属性来忽略只读字段或空值。
                /*IgnoreReadOnlyFields = true,
                IgnoreNullValues = true*/
            };
            if (string.IsNullOrEmpty(jsonStr))
            {
                var options = CreateOptions(expireSeconds);
                TResult? result = await valueFactory(options);

                string jsonOfResult = JsonSerializer.Serialize(result,
                    typeof(TResult), jsonOptions);
                await _distCache.SetStringAsync(cacheKey, jsonOfResult, options);
                return result;
            }
            else
            {
                await _distCache.RefreshAsync(cacheKey);
                return JsonSerializer.Deserialize<TResult>(jsonStr, jsonOptions)!;
            }
        }

        public void Remove(string cacheKey)
        {
            _distCache.Remove(cacheKey);
        }

        public Task RemoveAsync(string cacheKey)
        {
            return _distCache.RemoveAsync(cacheKey);
        }
    }
}
