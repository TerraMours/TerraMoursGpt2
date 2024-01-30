using TerraMours.Framework.Infrastructure.Contracts.SystemModels;

namespace TerraMours_Gpt.Framework.Infrastructure.Contracts.GptModels
{
    /// <summary>
    /// 验证码（充值卡）、邀请码
    /// </summary>
    public class Verification:BaseEntity
    {
        public long VerificationId { get; set; }
        /// <summary>
        /// 验证码
        /// </summary>
        public string VerificationCode { get; set; }
        /// <summary>
        /// 验证码类型：0 充值卡 1 邀请码
        /// </summary>
        public int? CodeType { get; set; }

        /// <summary>
        /// 是否核销
        /// </summary>
        public bool? IsUsed { get; set; }

        /// <summary>
        /// 使用人/受邀人
        /// </summary>
        public string[]? UsedUserIds { get; set; } 
    }
}
