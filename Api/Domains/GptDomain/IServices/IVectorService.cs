using AllInAI.Sharp.API.Dto;
using AllInAI.Sharp.API.Req;
using AllInAI.Sharp.API.Res.Vector;
using TerraMours.Domains.LoginDomain.Contracts.Common;
using TerraMours_Gpt_Api.Domains.GptDomain.Contracts.Req;
using TerraMours_Gpt_Api.Domains.GptDomain.Contracts.Res;

namespace TerraMours_Gpt_Api.Domains.GptDomain.IServices {
    public interface IVectorService {
        Task<ApiResponse<List<VectorQueryRes>>> GetList(int knowledgeId);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        Task<ApiResponse<bool>> Delete(int knowledgeId, VectorDeleteReq req);
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        Task<ApiResponse<VectorQueryRes>> Query(VectorQueryReq req, int knowledgeId);
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        Task<ApiResponse<bool>> Update(VectorUpdateReq req, int knowledgeId);

        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        Task<ApiResponse<VectorUpsertRes>> Upsert(VectorUpsertReq req, int knowledgeId);


        Task<ApiResponse<List<string>>> ListIndexes(int knowledgeId);

        Task<ApiResponse<bool>> CreateIndex(string name, int knowledgeId);
        Task<ApiResponse<bool>> DeleteIndex(string name, int knowledgeId);
        /// <summary>
        /// 查询知识库空间和统计
        /// </summary>
        /// <returns></returns>

        Task<ApiResponse<IndexStats>> DescribeIndexStats(int knowledgeId);

        /// <summary>
        /// Embadding后插入
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        Task<ApiResponse<VectorUpsertRes>> EmbaddingUpsert(VectorUpsertReq req, int knowledgeId, long? userId);

        /// <summary>
        /// Embadding后查询
        /// </summary>
        /// <param name="req"></param>
        /// <param name="knowledgeId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<ApiResponse<VectorQueryRes>> EmbaddingQuery(VectorQueryReq req, int knowledgeId, long? userId);
        
    }
}
