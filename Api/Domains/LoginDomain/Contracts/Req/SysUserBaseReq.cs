using System.Text.Json.Serialization;

namespace TerraMours.Domains.LoginDomain.Contracts.Req
{
    public class SysUserBaseReq
    {
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
        public string RoleId { get; set; }
    }
}
