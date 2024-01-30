using TerraMours.Domains.LoginDomain.Contracts.Common;
using TerraMours_Gpt.Domains.GptDomain.Contracts.Req;
using TerraMours_Gpt.Domains.GptDomain.Contracts.Res;
using TerraMours_Gpt.Domains.LoginDomain.Contracts.Common;

namespace TerraMours_Gpt.Domains.GptDomain.IServices {
    public interface IImageService {
        Task<ApiResponse<string?>> GenerateGraph(ImageReq req);

        Task<ApiResponse<bool>> ShareImage(long ImageRecordId,bool IsPublic, long? userId);
        Task<ApiResponse<bool>> DeleteImage(long ImageRecordId, long? userId);
        Task<ApiResponse<PagedRes<ImageRes>>> ShareImageList(PageReq page);

        Task<ApiResponse<PagedRes<ImageRes>>> MyImageList(PageReq page, long? userId);

        Task<ApiResponse<PagedRes<ImageRes>>> AllImageList(PageReq page, long? userId);
    }
}
