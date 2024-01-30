using Terramours_Gpt_Vector.Req;
using Terramours_Gpt_Vector.Res.Vector;

namespace Terramours_Gpt_Vector.IService
{
    public interface IVectorService
    {
        Task Test();

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        Task Delete(VectorDeleteReq req);
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        Task<VectorQueryRes> Query(VectorQueryReq req);
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        Task Update(VectorUpdateReq req);

        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        Task<VectorUpsertRes> Upsert(VectorUpsertReq req);
    }
}
