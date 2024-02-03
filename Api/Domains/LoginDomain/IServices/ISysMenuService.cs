using TerraMours.Domains.LoginDomain.Contracts.Common;
using TerraMours.Domains.LoginDomain.Contracts.Req;
using TerraMours.Domains.LoginDomain.Contracts.Res;
using TerraMours.Framework.Infrastructure.Contracts.SystemModels;

namespace TerraMours.Domains.LoginDomain.IServices
{
    public interface ISysMenuService
    {
        Task<ApiResponse<List<SysMenuRes>>> GetAllMenuList();
        Task<ApiResponse<bool>> DelMenu(SysMenuBaseReq req);
        Task<ApiResponse<bool>> UpdateMenu(SysMenuReq req);
        Task<ApiResponse<bool>> AddMenu(SysMenuReq req);
        Task<ApiResponse<List<SysMenuRes>>> GetMenuTree(long? roleId);
        Task<ApiResponse<bool>> AddMenuToRole(MenuToRoleAddReq req);
        Task<ApiResponse<List<long>>> GetRoleMenusIds(MenuToRoleAddReq req);
        Task<ApiResponse<SysMenuRouteRes>> GetUserRoutes(long? roleId);
        Task<ApiResponse<List<KeyValueRes>>>GetMenuSelect(SysMenuBaseReq req);
    }
}
