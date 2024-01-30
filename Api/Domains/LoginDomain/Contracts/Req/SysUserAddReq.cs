namespace TerraMours.Domains.LoginDomain.Contracts.Req
{
    /// <summary>
    /// 新增用户参数类
    /// </summary>
    public class SysUserAddReq
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string? UserName { get; set; }
        /// <summary>
        /// 角色
        /// </summary>
        public long? RoleId { get; set; }
        /// <summary>
        /// 邮箱 可以登录使用
        /// </summary>
        public string UserEmail { get; set; }
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
        public bool? EnableLogin { get; set; }
    }
}
