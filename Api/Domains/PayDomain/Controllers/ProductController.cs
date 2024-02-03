using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TerraMours_Gpt.Domains.PayDomain.Contracts.Req;
using TerraMours_Gpt.Domains.PayDomain.IServices;

namespace TerraMours_Gpt_Api.Domains.PayDomain.Controllers {
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase {
        private readonly IProductService _productService;

        public ProductController(IProductService productService) {
            _productService = productService;
        }
        /// <summary>
        /// 增加商品
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public async Task<IResult> AddProduct([FromBody] ProductReq req) {
            var res = await _productService.AddProduct(req);
            return Results.Ok(res);
        }

        /// <summary>
        /// 删除商品
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPut]
        public async Task<IResult> DeleteProduct(long id) {
            var res = await _productService.DeleteProduct(id);
            return Results.Ok(res);
        }

        /// <summary>
        /// 修改商品信息
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPut]
        public async Task<IResult> UpdateProduct([FromBody] ProductReq req) {
            var res = await _productService.UpdateProduct(req);
            return Results.Ok(res);
        }

        /// <summary>
        /// 查询所有商品信息
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public async Task<IResult> GetAllProductList() {
            var res = await _productService.GetAllProductList();
            return Results.Ok(res);
        }

        /// <summary>
        /// 根据id查询商品信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public async Task<IResult> GetProductById(long id) {
            var res = await _productService.GetProductById(id);
            return Results.Ok(res);
        }

        /// <summary>
        /// 根据分类id查询商品信息
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public async Task<IResult> GetProductByCategoryId(long categoryId) {
            var res = await _productService.GetProductByCategoryId(categoryId);
            return Results.Ok(res);
        }
        /// <summary>
        /// 上传图片，返回图片地址
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public async Task<IResult> UploadProductImage(IFormFile file) {
            var res = await _productService.UploadProductImage(file);
            return Results.Ok(res);
        }
    }
}
