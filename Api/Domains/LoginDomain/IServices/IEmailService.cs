using TerraMours.Domains.LoginDomain.Contracts.Common;
using TerraMours.Domains.LoginDomain.Contracts.Req;

namespace TerraMours.Domains.LoginDomain.IServices
{
    public interface IEmailService
    {
        Task<ApiResponse<bool>> CreateCheckCode(EmailReq req);
    }
}
