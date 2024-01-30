namespace TerraMours.Framework.Infrastructure.Contracts.SystemModels
{
    public class SysRole : BaseEntity
    {
        /// <summary>
        /// 主键 角色id 自增
        /// </summary>
        public long RoleId { get; set; }
        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get; set; }
        /// <summary>
        /// 父级ID
        /// </summary>
        public long? ParentId { get; set; }
        /// <summary>
        /// 是否有子角色
        /// </summary>
        public bool HasChildren { get; set; }
        /// <summary>
        /// 部门ID 方便后续扩展
        /// </summary>
        public long? DeptId { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        public string? DeptName { get; set; }

        /// <summary>
        /// 是否管理员
        /// </summary>
        public bool? IsAdmin { get; set; }

        /// <summary>
        /// 新建用户默认角色
        /// </summary>
        public bool? IsNewUser { get; set; }

        /// <summary>
        /// 一个角色对应的多个权限（菜单）外键是roleid
        /// </summary>
        public List<SysRolesToMenu>? RolesToMenus { get; set; }

        public SysRole()
        {

        }
        /// <summary>
        /// 新建角色 todo：添加部门字段
        /// </summary>
        /// <param name="roleName"></param>
        public SysRole(string roleName,bool? isAdmin, bool? isNewUser)
        {
            RoleName = roleName;
            IsAdmin = isAdmin;
            IsNewUser = isNewUser;
            HasChildren = false;
            //EntityBase
            Version = 1;
            Enable = true;
            CreateDate = DateTime.Now;
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        public SysRole Delete()
        {
            this.Enable=false;
            return this;
        }
        /// <summary>
        /// 修改角色名称
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public SysRole ChangeName(string roleName)
        {
            this.RoleName=roleName;
            return this;
        }
    }
}
