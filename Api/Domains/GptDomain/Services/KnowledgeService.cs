using AllInAI.Sharp.API.Dto;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TerraMours.Domains.LoginDomain.Contracts.Common;
using TerraMours.Framework.Infrastructure.EFCore;
using TerraMours_Gpt_Api.Domains.GptDomain.Contracts.Req;
using TerraMours_Gpt_Api.Domains.GptDomain.Contracts.Res;
using TerraMours_Gpt_Api.Domains.GptDomain.IServices;
using TerraMours_Gpt_Api.Framework.Infrastructure.Contracts.GptModels;

namespace TerraMours_Gpt_Api.Domains.GptDomain.Services
{
    public class KnowledgeService : IKnowledgeService
    {
        private readonly FrameworkDbContext _dbContext;
        private readonly Serilog.ILogger _logger;
        private readonly IMapper _mapper;

        public KnowledgeService(FrameworkDbContext dbContext, Serilog.ILogger logger, IMapper mapper)
        {
            _dbContext = dbContext;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<ApiResponse<bool>> Delete(int id, long userId)
        {
            var res=await _dbContext.knowledgeItems.FirstOrDefaultAsync(m => m.KnowledgeId == id);
            if(res == null)
            {
                return ApiResponse<bool>.Fail("未初始化数据");
            }
            res.Delete(userId);
            _dbContext.knowledgeItems.Update(res);
             await _dbContext.SaveChangesAsync();
            return ApiResponse<bool>.Success(true);
        }

        public async Task<ApiResponse<IndexStats>> DescribeIndexStats(KnowledgeReq know)
        {
            var type = (AllInAI.Sharp.API.Enums.AITypeEnum)know.KnowledgeType;
            string baseUrl = type == AllInAI.Sharp.API.Enums.AITypeEnum.Pinecone ? $"https://{know.IndexName}.{know.WorkSpace}.pinecone.io" : $"{know.BaseUrl}/{know.IndexName}/";
            AuthOption authOption = new AuthOption() { Key = know.ApiKey, BaseUrl = baseUrl, AIType = type };
            AllInAI.Sharp.API.Service.VectorService vectorService = new AllInAI.Sharp.API.Service.VectorService(authOption);
            var res = await vectorService.DescribeIndexStats();
            return ApiResponse<IndexStats>.Success(res);
        }

        public async Task<ApiResponse<List<KnowledgeRes>>> GetList(long userId)
        {
            var list =await _dbContext.knowledgeItems.Where(m => m.CreateID == userId || m.IsCommon == true).ToListAsync();
            var res= _mapper.Map<List<KnowledgeRes>>(list);
            return ApiResponse<List<KnowledgeRes>>.Success(res);
        }

        public async Task<ApiResponse<KnowledgeRes>> Query(int id, long userId)
        {
            var result = await _dbContext.knowledgeItems.FirstOrDefaultAsync(m => m.KnowledgeId == id);
            var res = _mapper.Map<KnowledgeRes>(result);
            return ApiResponse<KnowledgeRes>.Success(res);
        }

        public async Task<ApiResponse<bool>> Update(KnowledgeUpdateReq req, long userId)
        {
            var res = await _dbContext.knowledgeItems.FirstOrDefaultAsync(m => m.KnowledgeId == req.KnowledgeId);
            _mapper.Map(req,res);
            res.ModifyDate = DateTime.Now;
            res.ModifyID = userId;
            _dbContext.knowledgeItems.Update(res);
            await _dbContext.SaveChangesAsync();
            return ApiResponse<bool>.Success(true);
        }

        public async Task<ApiResponse<KnowledgeRes>> Upsert(KnowledgeReq req, long userId)
        {
            var res = _mapper.Map<KnowledgeItem>(req);
            res.CreateDate = DateTime.Now;
            res.Enable = true;
            res.CreateID = userId;
            await _dbContext.knowledgeItems.AddAsync(res);
            await _dbContext.SaveChangesAsync();
            var result= _mapper.Map<KnowledgeRes>(res);
            return ApiResponse < KnowledgeRes >.Success(result);
        }
    }
}
