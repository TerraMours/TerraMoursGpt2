using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Terramours_Gpt_Vector.Commons;
using Terramours_Gpt_Vector.IService;
using Terramours_Gpt_Vector.Req;

namespace Terramours_Gpt_Vector.Controllers
{
    /// <summary>
    /// apikey管理
    /// </summary>
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class ApiKeyController : ControllerBase
    {
        private readonly IApiKeyService _apiKeyService;

        public ApiKeyController(IApiKeyService apiKeyService)
        {
            _apiKeyService = apiKeyService;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IResult> Delete(ApikeyDeleteReq req)
        {
            req = Common.GetHeader(req, Request);
            var res= await _apiKeyService.Delete(req.KeyId);
            return Results.Ok(res);
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IResult> Query(ApikeyQueryReq req)
        {
            req = Common.GetHeader(req, Request);
            return Results.Ok(await _apiKeyService.Query(req));
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IResult> Update(ApikeyUpdateReq req)
        {
            req = Common.GetHeader(req, Request);
            var res = await _apiKeyService.Update(req);
            return Results.Ok(res);
        }

        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IResult> Upsert(ApikeyUpsertReq req)
        {
            req = Common.GetHeader(req, Request);
            return Results.Ok(await _apiKeyService.Upsert(req));
        }
    }
}
