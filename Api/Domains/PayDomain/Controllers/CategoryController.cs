using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TerraMours_Gpt.Domains.PayDomain.Contracts.Req;
using TerraMours_Gpt.Domains.PayDomain.IServices;

namespace TerraMours_Gpt_Api.Domains.PayDomain.Controllers {
    /// <summary>
    /// 分类接口
    /// </summary>
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class CategoryController : ControllerBase {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService) {
            _categoryService = categoryService;
        }
        /// <summary>
        /// 查询所有分类信息
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public async Task<IResult> GetAllCategoryList() {
            var res = await _categoryService.GetAllCategoryList();
            return Results.Ok(res);
        }

        /// <summary>
        /// 新增分类信息
        /// </summary>
        /// <param name="categoryReq"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public async Task<IResult> AddCategory([FromBody] CategoryReq categoryReq) {
            var res = await _categoryService.AddCategory(categoryReq);
            return Results.Ok(res);
        }

        /// <summary>
        /// 删除分类信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public async Task<IResult> DeleteCategory(long id) {
            var res = await _categoryService.DeleteCategory(id);
            return Results.Ok(res);
        }

        /// <summary>
        /// 更新分类信息 
        /// </summary>
        /// <param name="categoryReq"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public async Task<IResult> UpdateCategory([FromBody] CategoryReq categoryReq) {
            var res = await _categoryService.UpdateCategory(categoryReq);
            return Results.Ok(res);
        }

        /// <summary>
        /// 根据id查询分类信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public async Task<IResult> GetCategoryById(long id) {
            var res = await _categoryService.GetCategoryById(id);
            return Results.Ok(res);
        }
    }
}
