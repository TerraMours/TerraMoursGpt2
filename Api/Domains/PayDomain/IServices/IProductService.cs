using TerraMours.Domains.LoginDomain.Contracts.Common;
using TerraMours_Gpt.Domains.PayDomain.Contracts.Req;
using TerraMours_Gpt.Domains.PayDomain.Contracts.Res;

namespace TerraMours_Gpt.Domains.PayDomain.IServices
{
    public interface IProductService
    {
        Task<ApiResponse<bool>> AddProduct(ProductReq productReq);
        Task<ApiResponse<bool>> DeleteProduct(long id);
        Task<ApiResponse<bool>> UpdateProduct(ProductReq productReq);
        Task<ApiResponse<List<ProductRes>>> GetAllProductList();

        Task<ApiResponse<ProductRes>> GetProductById(long id);

        Task<ApiResponse<List<ProductRes>>> GetProductByCategoryId(long categoryId);

        Task<ApiResponse<string>> UploadProductImage(IFormFile file);
    }
}
