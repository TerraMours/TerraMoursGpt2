using System.Text;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using TerraMours_Gpt.Framework.Infrastructure.Contracts.Commons;
using TerraMours.Framework.Infrastructure.EFCore;
using TerraMours.Framework.Infrastructure.Redis;

namespace TerraMours_Gpt.Framework.Infrastructure.Middlewares {
    public class KeyMiddleware {
        private readonly RequestDelegate _next;
        private readonly IServiceProvider _serviceProvider;
        private readonly IOptions<GptOptions> _options;
        private Dictionary<string, int> _indexDict = new Dictionary<string, int>();

        public KeyMiddleware(RequestDelegate next, IOptions<GptOptions> options, IServiceProvider serviceProvider) {
            _next = next;
            _serviceProvider = serviceProvider;
            _options = options;
        }

        public async Task InvokeAsync(HttpContext context) {
            var endpoint = context.GetEndpoint();
            var isEnabled = endpoint?.Metadata.GetMetadata<KeyMiddlewareEnabledAttribute>()?.Enabled ?? false;
            if (isEnabled)
            {
                var modelValue = "gpt-3.5-turbo";
                using (var document = JsonDocument.Parse(await GetRequestBody(context.Request)))
                {
                    if (document.RootElement.TryGetProperty("model", out var modelProperty))
                    {
                         modelValue = modelProperty.GetString();
                    }
                }
                var keyList = GetKeyList();
                if (!_indexDict.ContainsKey(modelValue)) 
                {
                    _indexDict.Add(modelValue,0);
                }
                keyList = keyList.Where(m =>m.IsEnable==true && m.ModelTypes.Contains(modelValue)).ToArray();
                if (keyList.Length == 0)
                {
                    // 返回“未配置对应模型的key”的错误信息
                    context.Response.StatusCode = 500;
                    await context.Response.WriteAsync("系统未配置对应模型");
                    return;
                }
                var index = _indexDict[modelValue] > keyList.Length ? 0 : _indexDict[modelValue];
                var item = keyList[index];
                _indexDict[modelValue] = (index + 1) % keyList.Length;
                context.Items["key"] = item.Key;
                context.Items["baseUrl"] = item.BaseUrl;
                context.Items["baseType"] = item.Type;
            }
            await _next(context);
        }

        private KeyOption[] GetKeyList() {
            using (var scope = _serviceProvider.CreateScope()) {
                var _helper = scope.ServiceProvider.GetRequiredService<IDistributedCacheHelper>();
                //key池缓存
                var keyList =  _helper.GetOrCreate("GetKey",  options =>
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<FrameworkDbContext>();
                    Console.WriteLine("创建dbContext");
                    var gptOptions = dbContext.GptOptions
                        .AsNoTracking()
                        .OrderBy(m => m.GptOptionsId)
                        .FirstOrDefault();

                    if (gptOptions != null && gptOptions.OpenAIOptions?.OpenAI?.KeyList != null) {
                        return gptOptions.OpenAIOptions.OpenAI.KeyList;
                    }
                    return GetDefaultKeyList();
                });
                return keyList;
            }
        }

        private KeyOption[] GetDefaultKeyList() {
            var list = _options.Value.OpenAIOptions?.OpenAI?.KeyList;
            return list;
        }
        
        private async Task<string> GetRequestBody(HttpRequest request)
        {
            // 开启数据缓存
            request.EnableBuffering();
            using (MemoryStream memoryStream = new())
            {
                // 复制Body数据到缓存
                await request.Body.CopyToAsync(memoryStream);
                request.Body.Position = 0;
                memoryStream.Position = 0;
                using (StreamReader streamReader = new StreamReader(memoryStream,Encoding.UTF8))
                {
                    // 读取Body数据
                    return await streamReader.ReadToEndAsync();
                }
            }
        }
    }

}
