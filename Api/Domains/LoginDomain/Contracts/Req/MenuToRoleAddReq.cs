using Newtonsoft.Json;

namespace TerraMours.Domains.LoginDomain.Contracts.Req
{
    public class MenuToRoleAddReq
    {
        /// <summary>
        /// 菜单id集合
        /// </summary>
        public List<long>? MenuIds { get; set; }

        /// <summary>
        /// 角色id
        /// </summary>
        public long? RoleId { get; set; }
    }
}
