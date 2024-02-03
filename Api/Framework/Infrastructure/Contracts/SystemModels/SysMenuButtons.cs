namespace TerraMours.Framework.Infrastructure.Contracts.SystemModels
{
    /// <summary>
    /// 菜单按钮表
    /// </summary>
    public class SysMenuButtons : BaseEntity
    {
        /// <summary>
        /// 菜单按钮id 主键 自增
        /// </summary>
        public long MenuButtonId { get; set; }
        /// <summary>
        /// 菜单id
        /// </summary>
        public long MenuId { get; set; }
        /// <summary>
        /// 每个菜单页面对应的每一个按钮名字 例如 新增
        /// </summary>
        public string ButtonShowName { get; set; }
        /// <summary>
        /// 每个菜单页面对应的每一个按钮名字 例如 add
        /// </summary>
        public string ButtonEnName { get; set; }
        /// <summary>
        /// 角色菜单导航属性id外键
        /// </summary>
        public long RolesToMenuId { get; set; }
    }
}
