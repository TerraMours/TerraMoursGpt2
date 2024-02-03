using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Ocsp;
using System.Security.Claims;
using TerraMours_Gpt_Api.Domains.GptDomain.Contracts.Req;
using TerraMours_Gpt_Api.Domains.GptDomain.Contracts.Res;
using TerraMours_Gpt_Api.Domains.GptDomain.IServices;

namespace TerraMours_Gpt_Api.Domains.GptDomain.Controllers
{
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class KnowledgeController : ControllerBase
    {
        private readonly IKnowledgeService _knowledgeService;

        public KnowledgeController(IKnowledgeService knowledgeService)
        {
            _knowledgeService = knowledgeService;
        }
        [Authorize]
        [HttpGet]
        public async Task<IResult> GetList()
        {
            var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.UserData));
            var res = await _knowledgeService.GetList(userId);
            return Results.Ok(res);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public async Task<IResult> Delete(int id)
        {
            var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.UserData));
            var res = await _knowledgeService.Delete(id,userId);
            return Results.Ok(res);
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public async Task<IResult> Query(int id)
        {
            var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.UserData));
            var res = await _knowledgeService.Query(id,userId);
            return Results.Ok(res);
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public async Task<IResult> Update(KnowledgeUpdateReq req)
        {
            var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.UserData));
            var res = await _knowledgeService.Update(req, userId);
            return Results.Ok(res);
        }

        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public async Task<IResult> Upsert(KnowledgeReq req)
        {
            var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.UserData));
            var res = await _knowledgeService.Upsert(req, userId);
            return Results.Ok(res);
        }

        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public async Task<IResult> DescribeIndexStats(KnowledgeReq req)
        {
            var res = await _knowledgeService.DescribeIndexStats(req);
            return Results.Ok(res);
        }
    }
}
