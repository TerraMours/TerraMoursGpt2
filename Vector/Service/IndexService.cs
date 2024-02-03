using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Terramours_Gpt_Vector.Commons;
using Terramours_Gpt_Vector.Entities;
using Terramours_Gpt_Vector.IService;
using Terramours_Gpt_Vector.Req;
using Terramours_Gpt_Vector.Res;

namespace Terramours_Gpt_Vector.Service
{
    public class IndexService : IIndexService
    {
        private readonly EFCoreDbContext _dbContext;
        private readonly ILogger<VectorService> _logger;
        private readonly IMapper _mapper;

        public IndexService(EFCoreDbContext dbContext, ILogger<VectorService> logger, IMapper mapper)
        {
            _dbContext = dbContext;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task Delete(IndexDeleteReq req)
        {
            var index = await _dbContext.indexItems.FirstOrDefaultAsync(m => (m.IndexId == req.IndexId || m.Name==req.Name) && m.Key == req.Key );
            if (index == null)
            {
                throw new NullReferenceException("未别找到对应对象");
            }
            index.UpdateTime = DateTime.UtcNow;
            index.IsDeleted = true;
            _dbContext.indexItems.Update(index);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<string>> IndexList(IndexQueryReq req)
        {
            var items = await _dbContext.indexItems.Where(m => m.IsDeleted == false && (req.ThirdPartId == null || m.ThirdPartId == req.ThirdPartId) &&(req.Key==null || m.Key == req.Key))
                 .OrderBy(m => m.CreateTime)
                 .ToListAsync();
            List<string> indexQueryRes = new List<string>();
            foreach (var item in items)
            {
                indexQueryRes.Add(item.Name);
            }
            return indexQueryRes;
        }

        public async Task<List<IndexQueryRes>> Query(IndexQueryReq req)
        {
            var items = await _dbContext.indexItems.Where(m => m.IsDeleted == false && (req.ThirdPartId == null || m.ThirdPartId == req.ThirdPartId) && (req.Key == null || m.Key == req.Key))
                .OrderBy(m => m.CreateTime)
                .ToListAsync();
            List<IndexQueryRes> indexQueryRes = new List<IndexQueryRes>();
            foreach (var item in items)
            {
                IndexQueryRes queryRes = _mapper.Map<IndexQueryRes>(item);
                indexQueryRes.Add(queryRes);
            }
            return indexQueryRes;
        }

        public async Task Update(IndexUpdateReq req)
        {
            var index = await _dbContext.indexItems.FirstOrDefaultAsync(m => m.IndexId == req.IndexId && m.Key == req.Key);
            if (index == null)
            {
                throw new NullReferenceException("未别找到对应对象");
            }
            _mapper.Map(req, index);
            index.UpdateTime = DateTime.UtcNow;
            _dbContext.indexItems.Update(index);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IndexUpsertRes> Upsert(IndexUpsertReq req)
        {
            var index = _mapper.Map<IndexItem>(req);
            index.CreateTime = DateTime.UtcNow;
            index.IsDeleted = false;
            await _dbContext.indexItems.AddAsync(index);
            await _dbContext.SaveChangesAsync();
            var res = _mapper.Map<IndexUpsertRes>(index);
            return res;
        }
    }
}
