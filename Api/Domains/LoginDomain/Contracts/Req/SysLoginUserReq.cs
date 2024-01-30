namespace TerraMours.Domains.LoginDomain.Contracts.Req
{
    public class SysLoginUserReq
    {
        /// <summary>
        /// 登录账号 UserEmail 或者 UserPhoneNum 因为可以使用邮箱或者手机号登录。
        /// </summary>
        public string UserAccount { get; set; }

        /// <summary>
        /// 密码（是否加密）UserTrueName
        /// </summary>
        public string UserPassword { get; set; }
    }
}
