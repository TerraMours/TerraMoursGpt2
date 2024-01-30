using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Terramours_Gpt_Vector.Commons;
using Terramours_Gpt_Vector.IService;
using Terramours_Gpt_Vector.Req;
using Terramours_Gpt_Vector.Res.Vector;

namespace Terramours_Gpt_Vector.Controllers
{
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class IndexsController : ControllerBase
    {
        private readonly IIndexService _indexService;

        public IndexsController(IIndexService indexService)
        {
            _indexService = indexService;
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IResult> Delete(IndexDeleteReq req)
        {
            req = Common.GetHeader(req, Request);
            
            await _indexService.Delete(req);
            return Results.Ok();
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IResult> Query(IndexQueryReq req)
        {
            req = Common.GetHeader(req, Request);

            return Results.Ok(await _indexService.Query(req));
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IResult> Update(IndexUpdateReq req)
        {
            req = Common.GetHeader(req, Request);
            await _indexService.Update(req);
            return Results.Ok();
        }

        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IResult> Upsert(IndexUpsertReq req)
        {
            req = Common.GetHeader(req, Request);
            return Results.Ok(await _indexService.Upsert(req));
        }
        /// <summary>
        /// index名称集合
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IResult> IndexList(IndexQueryReq req)
        {
            req = Common.GetHeader(req, Request);
            return Results.Ok(await _indexService.IndexList(req));
        }
    }
}
