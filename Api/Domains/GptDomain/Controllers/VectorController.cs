using AllInAI.Sharp.API.Dto;
using AllInAI.Sharp.API.Req;
using AllInAI.Sharp.API.Res.Vector;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Ocsp;
using System.Security.Claims;
using System.Xml.Linq;
using TerraMours.Domains.LoginDomain.Contracts.Common;
using TerraMours_Gpt_Api.Domains.GptDomain.Contracts.Req;
using TerraMours_Gpt_Api.Domains.GptDomain.IServices;

namespace TerraMours_Gpt_Api.Domains.GptDomain.Controllers {
    [Route("{knowledgeId}/api/v1/[controller]/[action]")]
    [ApiController]
    public class VectorController : ControllerBase {
        private readonly IVectorService _vectorService;

        public VectorController(IVectorService vectorService)
        {
            _vectorService = vectorService;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public async Task<IResult> Delete(VectorDeleteReq req){
            var knowledgeId = int.Parse( RouteData.Values["knowledgeId"].ToString());
            var res = await _vectorService.Delete(knowledgeId,req);
            return Results.Ok(res);
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public async Task<IResult> Query(VectorQueryReq req)
        {
            var knowledgeId = int.Parse(RouteData.Values["knowledgeId"].ToString());
            var res = await _vectorService.Query(req,knowledgeId);
            return Results.Ok(res);
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public async Task<IResult> Update(VectorUpdateReq req)
        {
            var knowledgeId = int.Parse(RouteData.Values["knowledgeId"].ToString());
            var res = await _vectorService.Update(req, knowledgeId);
            return Results.Ok(res);
        }

        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public async Task<IResult> Upsert(VectorUpsertReq req)
        {
            var knowledgeId = int.Parse(RouteData.Values["knowledgeId"].ToString());
            var res = await _vectorService.Upsert(req, knowledgeId);
            return Results.Ok(res);
        }


        [Authorize]
        [HttpPost]
        public async Task<IResult> ListIndexes()
        {
            var knowledgeId = int.Parse(RouteData.Values["knowledgeId"].ToString());
            var res = await _vectorService.ListIndexes(knowledgeId);
            return Results.Ok(res);
        }

        [Authorize]
        [HttpPost]
        public async Task<IResult> CreateIndex(string name)
        {
            var knowledgeId = int.Parse(RouteData.Values["knowledgeId"].ToString());
            var res = await _vectorService.CreateIndex(name, knowledgeId);
            return Results.Ok(res);
        }
        [Authorize]
        [HttpPost]
        public async Task<IResult> DeleteIndex(string name)
        {
            var knowledgeId = int.Parse(RouteData.Values["knowledgeId"].ToString());
            var res = await _vectorService.DeleteIndex(name, knowledgeId);
            return Results.Ok(res);
        }
        [Authorize]
        [HttpGet]
        public async Task<IResult> DescribeIndexStats(){
            var knowledgeId = int.Parse(RouteData.Values["knowledgeId"].ToString());
            var res = await _vectorService.DescribeIndexStats(knowledgeId);
            return Results.Ok(res);
        }
        /// <summary>
        /// Embadding后插入
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public async Task<IResult> EmbaddingUpsert(VectorUpsertReq req)
        {
            var knowledgeId = int.Parse(RouteData.Values["knowledgeId"].ToString());
            var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.UserData));

            var res = await _vectorService.EmbaddingUpsert(req, knowledgeId,userId);
            return Results.Ok(res);
        }
        /// <summary>
        /// Embadding后查询
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public async Task<IResult> EmbaddingQuery(VectorQueryReq req)
        {
            var knowledgeId = int.Parse(RouteData.Values["knowledgeId"].ToString());
            var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.UserData));
            var res = await _vectorService.EmbaddingQuery(req, knowledgeId, userId);
            return Results.Ok(res);
        }
    }
}
