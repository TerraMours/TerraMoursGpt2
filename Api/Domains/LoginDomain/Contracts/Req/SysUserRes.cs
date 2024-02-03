using System.Text.Json.Serialization;

namespace TerraMours.Domains.LoginDomain.Contracts.Req {
    /// <summary>
    /// 用户信息
    /// </summary>
    public class SysUserRes {
        public SysUserRes(long userId, string userName, long? roleId) {
            this.UserId = userId;
            this.UserName = userName;
            this.RoleId = roleId;
        }

        public SysUserRes() {
        }

        /// <summary>
        /// 用户id
        /// </summary>
        public long UserId { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 角色
        /// </summary>
        public long? RoleId { get; set; }
    }
}
