using Newtonsoft.Json;

namespace TerraMours.Domains.LoginDomain.Contracts.Req
{
    public class SysMenuBaseReq
    {
        /// <summary>
        /// 菜单id
        /// </summary>
        public int? MenuId { get; set; }

        /// <summary>
        /// 角色id
        /// </summary>
        public long? RoleId { get; set; }
    }
}
