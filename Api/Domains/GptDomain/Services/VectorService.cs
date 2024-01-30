using AllInAI.Sharp.API.Dto;
using AllInAI.Sharp.API.Req;
using AllInAI.Sharp.API.Res.Vector;
using AutoMapper;
using IdentityModel;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Ocsp;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Xml.Linq;
using TerraMours.Domains.LoginDomain.Contracts.Common;
using TerraMours.Framework.Infrastructure.EFCore;
using TerraMours_Gpt.Domains.GptDomain.IServices;
using TerraMours_Gpt.Framework.Infrastructure.Contracts.Commons;
using TerraMours_Gpt_Api.Domains.GptDomain.Contracts.Res;
using TerraMours_Gpt_Api.Domains.GptDomain.IServices;
using TerraMours_Gpt_Api.Framework.Infrastructure.Contracts.GptModels;

namespace TerraMours_Gpt_Api.Domains.GptDomain.Services {
    public class VectorService : IVectorService
    {
        private readonly FrameworkDbContext _dbContext;
        private readonly Serilog.ILogger _logger;
        private readonly IMapper _mapper;

        public VectorService(FrameworkDbContext dbContext, Serilog.ILogger logger, IMapper mapper)
        {
            _dbContext = dbContext;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<ApiResponse<bool>> CreateIndex(string name, int knowledgeId)
        {
            var know =await _dbContext.knowledgeItems.FirstOrDefaultAsync(m => m.KnowledgeId == knowledgeId);
            if (know == null)
                return ApiResponse<bool>.Fail("未找到对应知识库记录");
            var type = (AllInAI.Sharp.API.Enums.AITypeEnum)know.KnowledgeType;
            string baseUrl = type== AllInAI.Sharp.API.Enums.AITypeEnum.Pinecone? $"https://controller.{know.WorkSpace}.pinecone.io":know.BaseUrl;
            AuthOption authOption = new AuthOption() { Key = know.ApiKey, BaseUrl = baseUrl, AIType = type };
            AllInAI.Sharp.API.Service.VectorService vectorService = new AllInAI.Sharp.API.Service.VectorService(authOption);
            await vectorService.CreateIndex(name, 1536, Metric.Cosine);
            return ApiResponse<bool>.Success(true);
        }

        public async Task<ApiResponse<bool>> Delete(int knowledgeId, VectorDeleteReq req)
        {
            var know = await _dbContext.knowledgeItems.FirstOrDefaultAsync(m => m.KnowledgeId == knowledgeId);
            if (know == null)
                return ApiResponse<bool>.Fail("未找到对应知识库记录");
            var type = (AllInAI.Sharp.API.Enums.AITypeEnum)know.KnowledgeType;
            string baseUrl = type == AllInAI.Sharp.API.Enums.AITypeEnum.Pinecone ? $"https://{know.IndexName}.{know.WorkSpace}.pinecone.io" : $"{know.BaseUrl}/{know.IndexName}/";
            AuthOption authOption = new AuthOption() { Key = know.ApiKey, BaseUrl = baseUrl, AIType = type };
            AllInAI.Sharp.API.Service.VectorService vectorService = new AllInAI.Sharp.API.Service.VectorService(authOption);
            await vectorService.Delete(req);
            return ApiResponse<bool>.Success(true);
        }

        public async Task<ApiResponse<bool>> DeleteIndex(string name, int knowledgeId)
        {
            var know = await _dbContext.knowledgeItems.FirstOrDefaultAsync(m => m.KnowledgeId == knowledgeId);
            if (know == null)
                return ApiResponse<bool>.Fail("未找到对应知识库记录");
            var type = (AllInAI.Sharp.API.Enums.AITypeEnum)know.KnowledgeType;
            string baseUrl = type == AllInAI.Sharp.API.Enums.AITypeEnum.Pinecone ? $"https://controller.{know.WorkSpace}.pinecone.io" : know.BaseUrl;
            AuthOption authOption = new AuthOption() { Key = know.ApiKey, BaseUrl = baseUrl, AIType = type };
            AllInAI.Sharp.API.Service.VectorService vectorService = new AllInAI.Sharp.API.Service.VectorService(authOption);
            await vectorService.DeleteIndex(name);
            return ApiResponse<bool>.Success(true);
        }

        public async Task<ApiResponse<IndexStats>> DescribeIndexStats(int knowledgeId)
        {
            var know = await _dbContext.knowledgeItems.FirstOrDefaultAsync(m => m.KnowledgeId == knowledgeId);
            if (know == null)
                return ApiResponse<IndexStats>.Fail("未找到对应知识库记录");
            var type = (AllInAI.Sharp.API.Enums.AITypeEnum)know.KnowledgeType;
            string baseUrl = type == AllInAI.Sharp.API.Enums.AITypeEnum.Pinecone ? $"https://{know.IndexName}.{know.WorkSpace}.pinecone.io" : $"{know.BaseUrl}/{know.IndexName}/"; 
            AuthOption authOption = new AuthOption() { Key = know.ApiKey, BaseUrl = baseUrl, AIType = type };
            AllInAI.Sharp.API.Service.VectorService vectorService = new AllInAI.Sharp.API.Service.VectorService(authOption);
            var res= await vectorService.DescribeIndexStats();
            return ApiResponse<IndexStats>.Success(res);
        }


        public async Task<ApiResponse<VectorQueryRes>> EmbaddingQuery(VectorQueryReq req, int knowledgeId, long? userId)
        {
            var know = await _dbContext.knowledgeItems.FirstOrDefaultAsync(m => m.KnowledgeId == knowledgeId);
            if (know == null)
                return ApiResponse<VectorQueryRes>.Fail("未找到对应知识库记录");
            var type = (AllInAI.Sharp.API.Enums.AITypeEnum)know.KnowledgeType;
            string baseUrl = type == AllInAI.Sharp.API.Enums.AITypeEnum.Pinecone ? $"https://{know.IndexName}.{know.WorkSpace}.pinecone.io" : $"{know.BaseUrl}/{know.IndexName}/";
            AuthOption authOption = new AuthOption() { Key = know.ApiKey, BaseUrl = baseUrl, AIType = type };
            AllInAI.Sharp.API.Service.VectorService vectorService = new AllInAI.Sharp.API.Service.VectorService(authOption);

            var embeddingReq = new EmbeddingReq()
            {
                Input = JsonSerializer.Serialize(req.Filter, new JsonSerializerOptions()
                {
                    Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
                }),
                Model = "text-embedding-ada-002"
            };
            var embeddedItem =await Embedding(embeddingReq, userId);
            req.Vector = embeddedItem.Data.Data.FirstOrDefault().Embedding;
            req.Filter = null;
            if(string.IsNullOrEmpty(req.Namespace))
            {
                req.Namespace = know.NameSpace;
            }
            try
            {
                var res = await vectorService.Query(req);

                return ApiResponse<VectorQueryRes>.Success(res);
            }
            catch (Exception ex) {
                return ApiResponse<VectorQueryRes>.Fail(ex.ToString());
            }
        }

        public async Task<ApiResponse<VectorUpsertRes>> EmbaddingUpsert(VectorUpsertReq req, int knowledgeId, long? userId)
        {
            var know = await _dbContext.knowledgeItems.FirstOrDefaultAsync(m => m.KnowledgeId == knowledgeId);
            if (know == null)
                return ApiResponse<VectorUpsertRes>.Fail("未找到对应知识库记录");
            var type = (AllInAI.Sharp.API.Enums.AITypeEnum)know.KnowledgeType;
            string baseUrl = type == AllInAI.Sharp.API.Enums.AITypeEnum.Pinecone ? $"https://{know.IndexName}.{know.WorkSpace}.pinecone.io" : $"{know.BaseUrl}/{know.IndexName}/";
            AuthOption authOption = new AuthOption() { Key = know.ApiKey, BaseUrl = baseUrl, AIType = type };
            AllInAI.Sharp.API.Service.VectorService vectorService = new AllInAI.Sharp.API.Service.VectorService(authOption);
            foreach (var item in req.Vectors)
            {
                var embeddingReq = new EmbeddingReq() { Input = JsonSerializer.Serialize(item.Metadata, new JsonSerializerOptions()
                {
                    Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
                }),Model= "text-embedding-ada-002"
                };
                var embeddedItem = Embedding(embeddingReq,userId);

                item.Values = embeddedItem.Result.Data.Data.FirstOrDefault().Embedding;
            }
            var res = await vectorService.Upsert(req);
            return ApiResponse<VectorUpsertRes>.Success(res);
        }

        public Task<ApiResponse<List<VectorQueryRes>>> GetList(int knowledgeId)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponse<List<string>>> ListIndexes(int knowledgeId)
        {
            var know = await _dbContext.knowledgeItems.FirstOrDefaultAsync(m => m.KnowledgeId == knowledgeId);
            if (know == null)
                return ApiResponse<List<string>>.Fail("未找到对应知识库记录");
            var type = (AllInAI.Sharp.API.Enums.AITypeEnum)know.KnowledgeType;
            string baseUrl = type == AllInAI.Sharp.API.Enums.AITypeEnum.Pinecone ? $"https://controller.{know.WorkSpace}.pinecone.io" : know.BaseUrl;
            AuthOption authOption = new AuthOption() { Key = know.ApiKey, BaseUrl = baseUrl, AIType = type };
            AllInAI.Sharp.API.Service.VectorService vectorService = new AllInAI.Sharp.API.Service.VectorService(authOption);
            var res = await vectorService.ListIndexes();
            return ApiResponse<List<string>>.Success(res);
        }

        public async Task<ApiResponse<VectorQueryRes>> Query(VectorQueryReq req, int knowledgeId)
        {
            var know = await _dbContext.knowledgeItems.FirstOrDefaultAsync(m => m.KnowledgeId == knowledgeId);
            if (know == null)
                return ApiResponse<VectorQueryRes>.Fail("未找到对应知识库记录");
            var type = (AllInAI.Sharp.API.Enums.AITypeEnum)know.KnowledgeType;
            string baseUrl = type == AllInAI.Sharp.API.Enums.AITypeEnum.Pinecone ? $"https://{know.IndexName}.{know.WorkSpace}.pinecone.io" : $"{know.BaseUrl}/{know.IndexName}/";
            AuthOption authOption = new AuthOption() { Key = know.ApiKey, BaseUrl = baseUrl, AIType = type };
            AllInAI.Sharp.API.Service.VectorService vectorService = new AllInAI.Sharp.API.Service.VectorService(authOption);
            string json = JsonSerializer.Serialize(req, new JsonSerializerOptions()
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
            });
            var res = await vectorService.Query(req);
            return ApiResponse<VectorQueryRes>.Success(res);
        }

        public async Task<ApiResponse<bool>> Update(VectorUpdateReq req, int knowledgeId)
        {
            var know = await _dbContext.knowledgeItems.FirstOrDefaultAsync(m => m.KnowledgeId == knowledgeId);
            if (know == null)
                return ApiResponse<bool>.Fail("未找到对应知识库记录");
            var type = (AllInAI.Sharp.API.Enums.AITypeEnum)know.KnowledgeType;
            string baseUrl = type == AllInAI.Sharp.API.Enums.AITypeEnum.Pinecone ? $"https://{know.IndexName}.{know.WorkSpace}.pinecone.io" : $"{know.BaseUrl}/{know.IndexName}/";
            AuthOption authOption = new AuthOption() { Key = know.ApiKey, BaseUrl = baseUrl, AIType = type };
            AllInAI.Sharp.API.Service.VectorService vectorService = new AllInAI.Sharp.API.Service.VectorService(authOption);
            await vectorService.Update(req);
            return ApiResponse<bool>.Success(true);
        }

        public async Task<ApiResponse<VectorUpsertRes>> Upsert(VectorUpsertReq req, int knowledgeId)
        {
            var know = await _dbContext.knowledgeItems.FirstOrDefaultAsync(m => m.KnowledgeId == knowledgeId);
            if (know == null)
                return ApiResponse<VectorUpsertRes>.Fail("未找到对应知识库记录");
            var type = (AllInAI.Sharp.API.Enums.AITypeEnum)know.KnowledgeType;
            string baseUrl = type == AllInAI.Sharp.API.Enums.AITypeEnum.Pinecone ? $"https://{know.IndexName}.{know.WorkSpace}.pinecone.io" : $"{know.BaseUrl}/{know.IndexName}/";
            AuthOption authOption = new AuthOption() { Key = know.ApiKey, BaseUrl = baseUrl, AIType = type };
            AllInAI.Sharp.API.Service.VectorService vectorService = new AllInAI.Sharp.API.Service.VectorService(authOption);
            var res = await vectorService.Upsert(req);
            return ApiResponse<VectorUpsertRes>.Success(res);
        }

        public async Task<ApiResponse<AllInAI.Sharp.API.Res.EmbeddingRes>> Embedding(EmbeddingReq req, long? userId)
        {
            var openAiOptions = _dbContext.GptOptions.FirstOrDefault().OpenAIOptions;
            var keyOption = openAiOptions.OpenAI.KeyList.Where(m => m.IsEnable == true && m.ModelTypes.Contains("gpt-3.5-turbo")).FirstOrDefault();
            var authOption = new AuthOption()
            {
                Key = keyOption.Key,
                BaseUrl = keyOption.BaseUrl,
                AIType = AllInAI.Sharp.API.Enums.AITypeEnum.OpenAi
            };
            AllInAI.Sharp.API.Service.ChatService chatService =
            new AllInAI.Sharp.API.Service.ChatService(authOption);
            var res = await chatService.Embedding(req);
            EmbedRecord embedRecord = new EmbedRecord()
            {
                CreateID = userId,
                CreateDate = DateTime.Now,
                InputJson = JsonSerializer.Serialize(req.InputCalculated, new JsonSerializerOptions()
                {
                    Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
                }),
                ResultJson = JsonSerializer.Serialize(res.Data, new JsonSerializerOptions()
                {
                    Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
                }),
                TotalTokens = res.Usage.TotalTokens,
                PromptTokens = res.Usage.PromptTokens,
                CompletionTokens = res.Usage.CompletionTokens
            };
            _dbContext.Add(embedRecord);
            await _dbContext.SaveChangesAsync();
            return ApiResponse<AllInAI.Sharp.API.Res.EmbeddingRes>.Success(res);
        }
    }
}
