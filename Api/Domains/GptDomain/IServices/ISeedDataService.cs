using TerraMours.Domains.LoginDomain.Contracts.Common;

namespace TerraMours_Gpt.Domains.GptDomain.IServices
{
    public interface ISeedDataService
    {
        Task<ApiResponse<bool>> EnsureSeedData();
    }
}
