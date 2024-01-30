using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TerraMours_Gpt.Domains.LoginDomain.Contracts.Req;
using TerraMours_Gpt.Domains.LoginDomain.IServices;

namespace TerraMours_Gpt_Api.Domains.LoginDomain.Controllers {
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class AnalysisController : ControllerBase {
        private readonly IAnalysisService _analysisService;

        public AnalysisController(IAnalysisService analysisService) {
            _analysisService = analysisService;
        }
        /// <summary>
        /// 统计数量
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public async Task<IResult> TotalAnalysis([FromBody] AnalysisBaseReq req) {
            var res = await _analysisService.TotalAnalysis(req);
            return Results.Ok(res);
        }

        /// <summary>
        /// 单个统计数量（图表）
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<IResult> AnalysisList([FromBody] AnalysisListReq req) {
            var res = await _analysisService.AnalysisList(req);
            return Results.Ok(res);
        }

        /// <summary>
        /// 所有统计数量（图表）
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public async Task<IResult> AllAnalysisList([FromBody] AnalysisBaseReq req) {
            var res = await _analysisService.AllAnalysisList(req);
            return Results.Ok(res);
        }

        /// <summary>
        /// 单个统计数量（图表）
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<IResult> PieAnalysisList([FromBody] AnalysisListReq req) {
            var res = await _analysisService.PieAnalysisList(req);
            return Results.Ok(res);
        }
        /// <summary>
        /// 销售统计
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<IResult> SaleAnalysis([FromBody] AnalysisListReq req) {
            var res = await _analysisService.SaleAnalysis(req);
            return Results.Ok(res);
        }
    }
}
