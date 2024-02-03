using System.Text.Json.Serialization;

namespace TerraMours.Domains.LoginDomain.Contracts.Req
{
    /// <summary>
    /// 用户列表数据
    /// </summary>
    public class SysUserDetailRes : SysUserRes
    {
        public SysUserDetailRes()
        {
        }
        /// <summary>
        /// 邮箱 可以登录使用
        /// </summary>
        public string? UserEmail { get; set; }
        /// <summary>
        /// 用户手机号 国内需要验证11号位数 别的国家需要单独到时候加逻辑
        /// </summary>
        public string? UserPhoneNum { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public string? Gender { get; set; }
        /// <summary>
        /// 是否能登陆 账号被锁
        /// </summary>
        public bool EnableLogin { get; set; }

        /// <summary>
        /// 头像url地址 
        /// </summary>
        public string? HeadImageUrl { get; set; }

        #region 扩展字段
        /// <summary>
        /// 会员等级
        /// </summary>
        public int? VipLevel { get; set; }

        /// <summary>
        /// 会员过期时间
        /// </summary>
        public DateTime? VipExpireTime { get; set; }

        /// <summary>
        /// 图像剩余次数
        /// </summary>
        public int? ImageCount { get; set; }

        /// <summary>
        /// token余额价格
        /// </summary>
        public decimal? Balance { get; set; }
        #endregion
    }
}
