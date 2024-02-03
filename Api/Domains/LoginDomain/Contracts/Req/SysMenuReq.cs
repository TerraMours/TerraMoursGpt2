using System.Text.Json.Serialization;

namespace TerraMours.Domains.LoginDomain.Contracts.Req
{
    public class SysMenuReq: SysMenuBaseReq
    {
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

        /// <summary>
        /// 排序
        /// </summary>
        public int? OrderNo { get; set; }
    }
}
