using Essensoft.Paylink.Alipay;
using TerraMours.Domains.LoginDomain.Contracts.Common;
using TerraMours.Framework.Infrastructure.Contracts.Commons;
using TerraMours_Gpt.Framework.Infrastructure.Contracts.Commons;

namespace TerraMours_Gpt.Domains.GptDomain.IServices
{
    public interface ISettingsService
    {
        /// <summary>
        /// 邮箱配置
        /// </summary>
        /// <returns></returns>
        public Task<ApiResponse<Email>> GetEmailSettings();

        /// <summary>
        /// 修改
        /// </summary>
        /// <returns></returns>
        public Task<ApiResponse<bool>> ChangeEmailSettings(Email email, long? userId);

        /// <summary>
        /// AI配置
        /// </summary>
        /// <returns></returns>
        public Task<ApiResponse<OpenAIOptions>> GetOpenAIOptions();

        /// <summary>
        /// 修改AI配置
        /// </summary>
        /// <returns></returns>
        public Task<ApiResponse<bool>> ChangeOpenAIOptions(OpenAIOptions openAIOptions, long? userId);

        /// <summary>
        /// 图片生成配置
        /// </summary>
        /// <returns></returns>
        public Task<ApiResponse<ImagOptions>> GetImagOptions();

        /// <summary>
        /// 修改图片生成配置
        /// </summary>
        /// <returns></returns>
        public Task<ApiResponse<bool>> ChangeImagOptions(ImagOptions imagOptions, long? userId);

        /// <summary>
        /// 支付配置
        /// </summary>
        /// <returns></returns>
        public Task<ApiResponse<AlipayOptions>> GetAlipayOptions();

        /// <summary>
        /// 支付配置
        /// </summary>
        /// <returns></returns>
        public Task<ApiResponse<bool>> ChangeAlipayOptions(AlipayOptions alipayOptions, long? userId);
    }
}
