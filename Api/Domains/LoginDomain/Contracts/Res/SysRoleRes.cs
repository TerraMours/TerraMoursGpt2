namespace TerraMours.Domains.LoginDomain.Contracts.Res
{
    public class SysRoleRes
    {
        public SysRoleRes()
        {
        }
        /// <summary>
        /// 主键 角色id 自增
        /// </summary>
        public long RoleId { get; set; }
        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// 是否管理员
        /// </summary>
        public bool? IsAdmin { get; set; }

        /// <summary>
        /// 新建用户默认角色
        /// </summary>
        public bool? IsNewUser { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateDate { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? ModifyDate { get; set; }
    }
}
