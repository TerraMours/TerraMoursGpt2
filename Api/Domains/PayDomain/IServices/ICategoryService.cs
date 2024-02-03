using TerraMours.Domains.LoginDomain.Contracts.Common;
using TerraMours_Gpt.Domains.PayDomain.Contracts.Req;
using TerraMours_Gpt.Domains.PayDomain.Contracts.Res;

namespace TerraMours_Gpt.Domains.PayDomain.IServices
{
    public interface ICategoryService
    {
        Task<ApiResponse<List<CategoryRes>>> GetAllCategoryList();
        Task<ApiResponse<bool>> AddCategory(CategoryReq categoryReq);

        Task<ApiResponse<bool>> DeleteCategory(long id);

        Task<ApiResponse<bool>> UpdateCategory(CategoryReq categoryReq);

        Task<ApiResponse<CategoryRes>> GetCategoryById(long id);


    }
}
