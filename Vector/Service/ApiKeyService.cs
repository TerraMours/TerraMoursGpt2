using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Terramours_Gpt_Vector.Commons;
using Terramours_Gpt_Vector.Entities;
using Terramours_Gpt_Vector.IService;
using Terramours_Gpt_Vector.Req;
using Terramours_Gpt_Vector.Res;

namespace Terramours_Gpt_Vector.Service
{
    public class ApiKeyService : IApiKeyService
    {
        private readonly EFCoreDbContext _dbContext;
        private readonly ILogger<VectorService> _logger;
        private readonly IMapper _mapper;

        public ApiKeyService(EFCoreDbContext dbContext, ILogger<VectorService> logger, IMapper mapper)
        {
            _dbContext = dbContext;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<ApiResponse<bool>> Delete(int id)
        {
            var apiKey = await _dbContext.apiKeys.FirstOrDefaultAsync(m =>m.KeyId == id);
            if (apiKey == null)
            {
                return ApiResponse<bool>.Fail("未别找到对应对象");
            }
            apiKey.UpdateTime = DateTime.UtcNow;
            apiKey.IsDeleted = true;
            _dbContext.apiKeys.Update(apiKey);
            await _dbContext.SaveChangesAsync();
            return ApiResponse<bool>.Success(true);
        }

        public async Task<ApiResponse<List<ApikeyQueryRes>>> Query(ApikeyQueryReq req)
        {
            var items=await _dbContext.apiKeys.Where(m=>m.IsDeleted ==false && m.ThirdPartId== req.ThirdPartId)
                .OrderBy(m=>m.CreateTime)
                .ToListAsync();
            List<ApikeyQueryRes> apikeyQueryRes = new List<ApikeyQueryRes>();
            foreach (var item in items)
            {
                ApikeyQueryRes queryRes = _mapper.Map<ApikeyQueryRes>(item);
                apikeyQueryRes.Add(queryRes);
            }
            return ApiResponse < List < ApikeyQueryRes >> .Success(apikeyQueryRes);
        }

        public async Task<ApiResponse<bool>> Update(ApikeyUpdateReq req)
        {
            var apiKey = await _dbContext.apiKeys.FirstOrDefaultAsync(m => m.KeyId == req.KeyId);
            if (apiKey == null)
            {
                return ApiResponse<bool>.Fail("未别找到对应对象");
            }
             _mapper.Map(req, apiKey);
            apiKey.UpdateTime = DateTime.UtcNow;
            _dbContext.apiKeys.Update(apiKey);
            await _dbContext.SaveChangesAsync();
            return ApiResponse<bool>.Success(true);
        }

        public async Task<ApiResponse<ApikeyUpsertRes>> Upsert(ApikeyUpsertReq req)
        {
            var apikey=_mapper.Map<ApiKey>(req);
            apikey.Key = Guid.NewGuid().ToString();
            apikey.CreateTime = DateTime.UtcNow;
            apikey.IsDeleted = false;
            await _dbContext.apiKeys.AddAsync(apikey);
            await _dbContext.SaveChangesAsync();
            var res = _mapper.Map<ApikeyUpsertRes>(apikey);
            return ApiResponse < ApikeyUpsertRes > .Success(res);
        }
    }
}
