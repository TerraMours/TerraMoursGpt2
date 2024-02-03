using System.Text.Json.Serialization;

namespace TerraMours.Domains.LoginDomain.Contracts.Req
{
    public class SysUserReq
    {
        /// <summary>
        /// 登录账号 UserEmail 或者 UserPhoneNum 因为可以使用邮箱或者手机号登录。
        /// </summary>
        public string UserAccount { get; set; }

        /// <summary>
        /// 密码（是否加密）UserTrueName
        /// </summary>
        public string UserPassword { get; set; }
        /// <summary>
        /// 注册时使用，保证两次密码一样
        /// </summary>
        public string RepeatPassword { get; set; }
        /// <summary>
        /// 校验码
        /// </summary>
        public string CheckCode { get; set; }
    }
}
