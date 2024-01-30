using TerraMours.Domains.LoginDomain.Contracts.Common;
using TerraMours.Domains.LoginDomain.Contracts.Req;
using TerraMours.Domains.LoginDomain.Contracts.Res;
using TerraMours_Gpt.Domains.LoginDomain.Contracts.Req;

namespace TerraMours.Domains.LoginDomain.IServices
{
    public interface ISysUserService
    {
        Task<ApiResponse<string>> Register(SysUserReq userReq);
        Task<ApiResponse<LoginRes>> Login(SysLoginUserReq userReq);
        Task<ApiResponse<List<SysUserDetailRes>>> GetAllUserList();
        Task<ApiResponse<bool>> DelUser(SysUserBaseReq userReq);
        Task<ApiResponse<bool>> UpdateUser(SysUserUpdateReq userReq,long userId);
        Task<ApiResponse<bool>> AddUser(SysUserAddReq userReq);
        Task<string> Logout(SysLoginUserReq userReq);
        Task<ApiResponse<SysUserDetailRes>> GetUserInfoById(long userId);
        Task<List<KeyValueRes>>GetUserNameList();
        
        Task<ApiResponse<string>> ChangePassword(SysUserReq userReq);
    }
}
