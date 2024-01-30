using Terramours_Gpt_Vector.Commons;
using Terramours_Gpt_Vector.Req;
using Terramours_Gpt_Vector.Res;
using Terramours_Gpt_Vector.Res.Vector;

namespace Terramours_Gpt_Vector.IService
{
    public interface IApiKeyService
    {
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        Task< ApiResponse<bool>> Delete(int id);
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        Task<ApiResponse<List<ApikeyQueryRes>>> Query(ApikeyQueryReq req);
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        Task<ApiResponse<bool>> Update(ApikeyUpdateReq req);

        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        Task<ApiResponse<ApikeyUpsertRes>> Upsert(ApikeyUpsertReq req);
    }
}
