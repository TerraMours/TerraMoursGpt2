using Terramours_Gpt_Vector.Req;
using Terramours_Gpt_Vector.Res;
using Terramours_Gpt_Vector.Res.Vector;

namespace Terramours_Gpt_Vector.IService
{
    public interface IIndexService
    {
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        Task Delete(IndexDeleteReq req);
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        Task<List<IndexQueryRes>> Query(IndexQueryReq req);
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        Task Update(IndexUpdateReq req);

        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        Task<IndexUpsertRes> Upsert(IndexUpsertReq req);

        /// <summary>
        /// index名称集合
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        Task<List<string>> IndexList(IndexQueryReq req);

    }
}
