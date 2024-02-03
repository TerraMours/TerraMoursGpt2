using TerraMours.Domains.LoginDomain.Contracts.Common;
using TerraMours.Domains.LoginDomain.Contracts.Req;
using TerraMours.Domains.LoginDomain.Contracts.Res;
using TerraMours_Gpt.Domains.LoginDomain.Contracts.Req;
using TerraMours_Gpt.Domains.LoginDomain.Contracts.Res;

namespace TerraMours_Gpt.Domains.LoginDomain.IServices
{
    public interface IAnalysisService
    {
        /// <summary>
        /// 数量统计
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        Task<ApiResponse<List<TotalAnalysisRes>>> TotalAnalysis(AnalysisBaseReq req);

        /// <summary>
        /// 数量图表
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        Task<ApiResponse<List<TotalAnalysisRes>>> AnalysisList(AnalysisListReq req);

        /// <summary>
        /// 所有统计图表
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        Task<ApiResponse<List<AnalysisListRes>>> AllAnalysisList(AnalysisBaseReq req);

        /// <summary>
        /// 饼状图统计
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        Task<ApiResponse<List<TotalAnalysisRes>>> PieAnalysisList(AnalysisListReq req);

        /// <summary>
        /// 销售统计
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        Task<ApiResponse<List<TotalAnalysisRes>>> SaleAnalysis(AnalysisBaseReq req);
    }
}
