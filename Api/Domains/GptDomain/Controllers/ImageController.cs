using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TerraMours_Gpt.Domains.GptDomain.Contracts.Req;
using TerraMours_Gpt.Domains.GptDomain.IServices;
using TerraMours_Gpt.Domains.LoginDomain.Contracts.Common;
using TerraMours_Gpt.Framework.Infrastructure.Middlewares;

namespace TerraMours_Gpt_Api.Domains.GptDomain.Controllers {
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class ImageController : ControllerBase {
        private readonly IImageService _imageService;

        public ImageController( IImageService imageService) {
            _imageService = imageService;
        }
        /// <summary>
        /// 生成图片
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [Authorize]
        [KeyMiddlewareEnabled]
        [HttpPost]
        public async Task<IResult> GenerateGraph(ImageReq req) {
            if (HttpContext?.Items["key"] != null) {
                req.Key = HttpContext?.Items["key"]?.ToString();
            }
            if (HttpContext?.Items["baseUrl"] != null) {
                req.BaseUrl = HttpContext?.Items["baseUrl"]?.ToString();
            }
            if (HttpContext?.Items["baseType"] != null) {
                req.BaseType = int.Parse(HttpContext?.Items["baseType"]?.ToString());
            }
            req.UserId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.UserData));
            var res = await _imageService.GenerateGraph(req);
            return Results.Ok(res);
        }

        /// <summary>
        /// 公开图片
        /// </summary>
        /// <param name="ImageRecordId"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public async Task<IResult> ShareImage(long ImageRecordId, bool IsPublic) {
            var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.UserData));
            var res = await _imageService.ShareImage(ImageRecordId, IsPublic, userId);
            return Results.Ok(res);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ImageRecordId"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public async Task<IResult> DeleteImage(long ImageRecordId) {
            var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.UserData));
            var res = await _imageService.DeleteImage(ImageRecordId, userId);
            return Results.Ok(res);
        }

        /// <summary>
        /// 图片广场
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public async Task<IResult> ShareImageList(PageReq page) {
            var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.UserData));
            var res = await _imageService.ShareImageList(page);
            return Results.Ok(res);
        }

        /// <summary>
        /// 我的图片
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public async Task<IResult> MyImageList(PageReq page) {
            var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.UserData));
            var res = await _imageService.MyImageList(page, userId);
            return Results.Ok(res);
        }
        /// <summary>
        /// 全部图片
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public async Task<IResult> AllImageList(PageReq page) {
            var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.UserData));
            var res = await _imageService.AllImageList(page, userId);
            return Results.Ok(res);
        }
    }
}
