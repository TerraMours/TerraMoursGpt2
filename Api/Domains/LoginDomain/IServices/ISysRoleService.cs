using TerraMours.Domains.LoginDomain.Contracts.Common;
using TerraMours.Domains.LoginDomain.Contracts.Req;
using TerraMours.Domains.LoginDomain.Contracts.Res;

namespace TerraMours.Domains.LoginDomain.IServices
{
    public interface ISysRoleService
    {
        Task<ApiResponse<List<SysRoleRes>>> GetAllRoleList();
        Task<ApiResponse<bool>> DelRole(SysRoleBaseReq req);
        Task<ApiResponse<bool>> UpdateRole(SysRoleBaseReq req);
        Task<ApiResponse<bool>> AddRole(SysRoleBaseReq req);
        Task<ApiResponse<List<KeyValueRes>>> GetRoleSelect();
    }
}
