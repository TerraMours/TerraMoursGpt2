using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Terramours_Gpt_Vector.Commons;
using Terramours_Gpt_Vector.IService;
using Terramours_Gpt_Vector.Req;
using Terramours_Gpt_Vector.Res.Vector;

namespace Terramours_Gpt_Vector.Controllers
{
    [Route("{index}/api/v1/[controller]/[action]")]
    [ApiController]
    public class VectorsController : ControllerBase
    {
        private readonly IVectorService _vectorService;

        public VectorsController(IVectorService vectorService)
        {
            _vectorService = vectorService;
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IResult> Delete(VectorDeleteReq req)
        {
            req = Common.GetHeader(req ,Request );
            var index = RouteData.Values["index"].ToString();
            req.Index = index;
            await _vectorService.Delete(req);
            return Results.Ok();
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IResult> Query(VectorQueryReq req)
        {
            req = Common.GetHeader(req, Request);
            var index = RouteData.Values["index"].ToString();
            req.Index = index;
            return Results.Ok(await _vectorService.Query(req));
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IResult> Update(VectorUpdateReq req)
        {
            req = Common.GetHeader(req, Request);
            var index = RouteData.Values["index"].ToString();
            req.Index = index;
            await _vectorService.Update(req);
            return Results.Ok();
        }

        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IResult> Upsert(VectorUpsertReq req)
        {
            req = Common.GetHeader(req, Request);
            var index = RouteData.Values["index"].ToString();
            req.Index = index;
            return Results.Ok(await _vectorService.Upsert(req));
        }
    }
}
