using AutoMapper;
using TerraMours.Domains.LoginDomain.Contracts.Req;

namespace TerraMours.Framework.Infrastructure.Contracts.SystemModels
{
    /// <summary>
    /// 系统菜单表
    /// </summary>
    public class SysMenus : BaseEntity
    {
        /// <summary>
        /// 菜单id 主键 自增
        /// </summary>
        public long MenuId { get; set; }
        /// <summary>
        /// 菜单父级id 
        /// </summary>
        public long? ParentId { get; set; }
        /// <summary>
        /// 是否有子菜单 默认无
        /// </summary>
        public bool HasChildren { get; set; }
        /// <summary>
        /// 菜单名称 
        /// </summary>
        public string MenuName { get; set; }
        /// <summary>
        /// 菜单url地址 应该是sysurl + 菜单按钮 类似于相对路径的配置
        /// </summary>
        public string? MenuUrl { get; set; }
        /// <summary>
        /// 菜单图标  用不着 这个前端设置即可
        /// </summary>
        public string? Icon { get; set; }
        /// <summary>
        /// 菜单描述or说明
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? IconUrl { get; set; }

        /// <summary>
        /// 菜单id 外键 与主键相等
        /// </summary>
        //public long MenuForeignId { get; set; }

        /// <summary>
        /// 是否是首页 默认false
        /// </summary>
        public bool IsHome { get; set; }
        /// <summary>
        /// 是否是外链链接，默认是false，默认是内部系统地址
        /// </summary>
        public bool ExternalUrl { get; set; }
        /// <summary>
        /// 是否可见，默认true
        /// </summary>
        public bool IsShow { get; set; }

        public SysMenus()
        {
        }
        /// <summary>
        /// 新建角色 
        /// </summary>
        /// <param name="roleName"></param>
        public SysMenus(SysMenuReq req)
        {
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
        public SysMenus Delete()
        {
            this.Enable = false;
            return this;
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <returns></returns>
        public SysMenus Change(IMapper mapper, SysMenuReq req)
        {
            mapper.Map(req, this);
            return this;
        }
    }
}
