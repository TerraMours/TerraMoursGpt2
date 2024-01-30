using Essensoft.Paylink.Alipay;
using Essensoft.Paylink.Alipay.Domain;
using Microsoft.AspNetCore.Builder.Extensions;
using Microsoft.EntityFrameworkCore;
using TerraMours.Domains.LoginDomain.Contracts.Common;
using TerraMours.Framework.Infrastructure.Contracts.Commons;
using TerraMours.Framework.Infrastructure.EFCore;
using TerraMours_Gpt.Domains.GptDomain.IServices;
using TerraMours_Gpt.Framework.Infrastructure.Contracts.Commons;
using TerraMours.Framework.Infrastructure.Redis;

namespace TerraMours_Gpt.Domains.GptDomain.Services
{
    /// <summary>
    /// 系统设置
    /// </summary>
    public class SettingsService : ISettingsService
    {
        private readonly FrameworkDbContext _dbContext;
        private readonly Serilog.ILogger _logger;
        private readonly IDistributedCacheHelper _helper;
        public SettingsService(FrameworkDbContext dbContext, Serilog.ILogger logger, IDistributedCacheHelper helper)
        {
            _dbContext = dbContext;
            _logger = logger;
            _helper = helper;
        }

        public async Task<ApiResponse<bool>> ChangeEmailSettings(Email email, long? userId)
        {
            var settings = await _dbContext.SysSettings.AsNoTracking().FirstOrDefaultAsync();
            if (settings == null)
            {
                return ApiResponse<bool>.Fail("未初始化数据");
            }
            settings.ChangeEmail(email,userId);
            _dbContext.ChangeTracker.Clear();
            _dbContext.SysSettings.Update(settings);
            await _dbContext.SaveChangesAsync();
            return ApiResponse<bool>.Success(true);
        }

        public async Task<ApiResponse<bool>> ChangeImagOptions(ImagOptions imagOptions, long? userId)
        {
            var settings = await _dbContext.GptOptions.FirstOrDefaultAsync();
            if (settings == null)
            {
                return ApiResponse<bool>.Fail("未初始化数据");
            }
            settings.ChangeImagOptions(imagOptions, userId);
            _dbContext.ChangeTracker.Clear();
            _dbContext.GptOptions.Update(settings);
            await _dbContext.SaveChangesAsync();
            return ApiResponse<bool>.Success(true);
        }

        public async Task<ApiResponse<bool>> ChangeOpenAIOptions(OpenAIOptions openAIOptions, long? userId)
        {
            var settings = await _dbContext.GptOptions.FirstOrDefaultAsync();
            if (settings == null)
            {
                return ApiResponse<bool>.Fail("未初始化数据");
            }
            settings.ChangeOpenAIOptions(openAIOptions, userId);
            _dbContext.ChangeTracker.Clear();
            _dbContext.GptOptions.Update(settings);
            await _dbContext.SaveChangesAsync();
            //删除key池缓存
            await _helper.RemoveAsync("GetKey");
            return ApiResponse<bool>.Success(true);
        }

        public async Task<ApiResponse<Email>> GetEmailSettings()
        {
            var settings =await _dbContext.SysSettings.FirstOrDefaultAsync();
            if (settings == null)
            {
                return ApiResponse<Email>.Fail("未初始化数据");
            }
            return ApiResponse<Email>.Success(settings.Email);
        }

        public async Task<ApiResponse<ImagOptions>> GetImagOptions()
        {
            var settings = await _dbContext.GptOptions.FirstOrDefaultAsync();
            if (settings == null)
            {
                return ApiResponse<ImagOptions>.Fail("未初始化数据");
            }
            return ApiResponse<ImagOptions>.Success(settings.ImagOptions);
        }

        public async Task<ApiResponse<OpenAIOptions>> GetOpenAIOptions()
        {
            var settings = await _dbContext.GptOptions.FirstOrDefaultAsync();
            if (settings == null)
            {
                return ApiResponse<OpenAIOptions>.Fail("未初始化数据");
            }
            return ApiResponse<OpenAIOptions>.Success(settings.OpenAIOptions);
        }
        public async Task<ApiResponse<AlipayOptions>> GetAlipayOptions() {
            var settings = await _dbContext.SysSettings.FirstOrDefaultAsync();
            if (settings == null) {
                return ApiResponse<AlipayOptions>.Fail("未初始化数据");
            }
            return ApiResponse<AlipayOptions>.Success(settings.Alipay);
        }

        public async Task<ApiResponse<bool>> ChangeAlipayOptions(AlipayOptions alipayOptions, long? userId) {
            var settings = await _dbContext.SysSettings.FirstOrDefaultAsync();
            if (settings == null) {
                return ApiResponse<bool>.Fail("未初始化数据");
            }
            settings.ChangeAlipayOptions(alipayOptions, userId);
            _dbContext.ChangeTracker.Clear();
            _dbContext.SysSettings.Update(settings);
            await _dbContext.SaveChangesAsync();
            return ApiResponse<bool>.Success(true);
        }

    }
}
