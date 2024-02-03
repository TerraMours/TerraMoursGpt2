using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TerraMours.Domains.LoginDomain.Contracts.Common;
using TerraMours.Framework.Infrastructure.EFCore;
using TerraMours.Framework.Infrastructure.Redis;
using TerraMours_Gpt.Domains.PayDomain.Contracts.Req;
using TerraMours_Gpt.Domains.PayDomain.Contracts.Res;
using TerraMours_Gpt.Domains.PayDomain.IServices;
using Category = TerraMours_Gpt.Framework.Infrastructure.Contracts.ProductModels.Category;

namespace TerraMours_Gpt.Domains.PayDomain.Services
{
    /// <summary>
    /// 商品分类增删改查接口
    /// </summary>
    public class CategoryService : ICategoryService
    {
        private readonly FrameworkDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IDistributedCacheHelper _helper;
        public CategoryService(FrameworkDbContext dbContext, IMapper mapper, IDistributedCacheHelper helper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _helper = helper;
        }

        /// <summary>
        /// 新增分类信息
        /// </summary>
        /// <param name="categoryReq"></param>
        /// <returns></returns>
        public async Task<ApiResponse<bool>> AddCategory(CategoryReq categoryReq)
        {
            var category = new Category(categoryReq.Name, categoryReq.Description);
            _mapper.Map(categoryReq, category);
            if (category == null)
            {
                return ApiResponse<bool>.Fail("添加分类失败");
            }
            await _dbContext.Categorys.AddAsync(category);
            var res = await _dbContext.SaveChangesAsync();
            //删除过期的缓存
            await _helper.RemoveAsync("GetAllCategoryList");
            //把新的信息添加到redis中
            var categoryList = await _helper.GetOrCreateAsync("GetAllCategoryList", async options => { return await _dbContext.Categorys.ToListAsync(); });
            return ApiResponse<bool>.Success(true);

        }

        /// <summary>
        /// 删除分类（逻辑删）
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ApiResponse<bool>> DeleteCategory(long id)
        {
            var category = await _dbContext.Categorys.FirstOrDefaultAsync(x => x.Id == id);
            if (category == null)
            {
                return ApiResponse<bool>.Fail("该分类不存在");
            }
            category.DeleteCategory();
            _dbContext.ChangeTracker.Clear();
            _dbContext.Categorys.Update(category);
            await _dbContext.SaveChangesAsync();
            await _helper.RemoveAsync("GetAllCategoryList");
            return ApiResponse<bool>.Success(true);
        }

        /// <summary>
        /// 查询所有的分类信息
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResponse<List<CategoryRes>>> GetAllCategoryList()
        {
            var categoryList = await _helper.GetOrCreateAsync("GetAllCategoryList", async options => { return await _dbContext.Categorys.ToListAsync(); });
            var categoryResList = _mapper.Map<List<CategoryRes>>(categoryList);
            return ApiResponse<List<CategoryRes>>.Success(categoryResList);
        }

        /// <summary>
        /// 根据id查询分类信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<ApiResponse<CategoryRes>> GetCategoryById(long id)
        {
            var categoryList = await _helper.GetOrCreateAsync("GetAllCategoryList", async options => { return await _dbContext.Categorys.ToListAsync(); }) ?? throw new Exception("该分类不存在");
            var category = categoryList.Find(x => x.Id == id);
            var res = _mapper.Map<CategoryRes>(category);
            return ApiResponse<CategoryRes>.Success(res);
        }

        /// <summary>
        /// 修改分类信息
        /// </summary>
        /// <param name="categoryReq"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ApiResponse<bool>> UpdateCategory(CategoryReq categoryReq)
        {
            var category = await _dbContext.Categorys.FirstOrDefaultAsync(x => x.Id == categoryReq.Id) ?? throw new Exception("该分类不存在");
            //_mapper.Map(categoryReq, category);
            category.UpdateCategory(categoryReq.Name, categoryReq.Description);
            _dbContext.ChangeTracker.Clear();
            _dbContext.Categorys.Update(category);
            var res = await _dbContext.SaveChangesAsync();
            //删除过期的缓存
            await _helper.RemoveAsync("GetAllCategoryList");
            return ApiResponse<bool>.Success(true);
        }
    }
}
