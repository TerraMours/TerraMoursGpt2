namespace TerraMours.Domains.LoginDomain.Contracts.Res
{
    /// <summary>
    /// 菜单配置返回参数类
    /// </summary>
    public class SysMenuRes
    {
        public SysMenuRes() { }
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
        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
        /// <summary>
        /// 排序号  猜测是会根据那个部门的id来排序 职位等级等 目前用不到
        /// </summary>
        public int? OrderNo { get; set; }
        /// <summary>
        /// 子节点
        /// </summary>
        public List<SysMenuRes>? Children { get; set; }
        /// <summary>
        /// 是否选中（树用）
        /// </summary>
        public bool? IsChecked { get; set; }
    }
    /// <summary>
    /// 前端路由配置类
    /// </summary>
    public class SysMenuRouteRes
    {
        /// <summary>
        /// 首页路由（第一次进系统时候展示页面路由）
        /// </summary>
        public string? Home { get; set; }
        /// <summary>
        /// 路由集合
        /// </summary>
        public List<RouteObject>? Routes { get; set; }
    }

    public class RouteObject
    {
        /// <summary>
        /// 路由名称(路由唯一标识) 
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 路由路径
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// 路由的组件
        /// </summary>
        public string Component { get; set; }
        /// <summary>
        /// 子路由
        /// </summary>
        public List<RouteObject>? Children { get; set; }
        /// <summary>
        /// 路由描述
        /// </summary>
        public Meta Meta { get; set; }
        /// <summary>
        /// 路由初始化
        /// </summary>
        /// <param name="path"></param>
        /// <param name="component"></param>
        /// <param name="meta"></param>
        public RouteObject(string path, string component, Meta meta)
        {
            Name = path.Substring(1).Replace("/","_");
            Path = path;
            Component = component;
            Meta = meta;
        }
    }

    /// <summary>
    /// 路由描述
    /// </summary>
    public class Meta
    {
        public Meta()
        {

        }
        /// <summary>
        /// 新建路由详情
        /// </summary>
        /// <param name="title"></param>
        /// <param name="icon"></param>
        /// <param name="href"></param>
        /// <param name="order"></param>
        /// <param name="singleLayout"></param>
        public Meta(string title, string? icon, int? order = null, string? href=null, string? singleLayout = null)
        {
            Title = title;
            Icon = icon;
            Href = href;
            Order = order;
            SingleLayout = singleLayout;
            //默认
            RequiresAuth =true;
        }

        /// <summary>
        /// 路由标题(可用来作document.title或者菜单的名称) 
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 需要登录权限
        /// </summary>
        public bool? RequiresAuth { get; set; }
        /// <summary>
        /// 菜单和面包屑对应的图标
        /// </summary>
        public string? Icon { get; set; }
        /// <summary>
        /// 使用本地svg作为的菜单和面包屑对应的图标(assets/svg-icon文件夹的的svg文件名)
        /// </summary>
        public string? LocalIcon { get; set; }
        /// <summary>
        /// 外链链接
        /// </summary>
        public string? Href { get; set; }
        /// <summary>
        /// 是否在菜单中隐藏(一些列表、表格的详情页面需要通过参数跳转，所以不能显示在菜单中)
        /// </summary>
        public bool? Hide { get; set; }
        /// <summary>
        /// 是否支持多个tab页签(默认一个，即相同name的路由会被替换)
        /// </summary>
        public bool? MultiTab { get; set; }
        /// <summary>
        /// 当前路由需要选中的菜单项(用于跳转至不在左侧菜单显示的路由且需要高亮某个菜单的情况)
        /// </summary>
        public string? ActiveMenu { get; set; }
        /// <summary>
        /// 作为单级路由的父级路由布局组件
        /// </summary>
        public string? SingleLayout { get; set; }

        public string? DynamicPath { get; set; }
        /// <summary>
        /// 路由顺序，可用于菜单的排序
        /// </summary>
        public int? Order { get; set; }
    }
}
