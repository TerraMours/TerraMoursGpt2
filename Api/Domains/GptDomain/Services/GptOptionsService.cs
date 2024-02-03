using Microsoft.EntityFrameworkCore;
using TerraMours.Framework.Infrastructure.EFCore;
using TerraMours_Gpt.Framework.Infrastructure.Contracts.Commons;
using TerraMours_Gpt_Api.Domains.GptDomain.IServices;

namespace TerraMours_Gpt_Api.Domains.GptDomain.Services
{
    public class GptOptionsService : IGptOptionsService
    {
        private readonly FrameworkDbContext _dbContext;

        public GptOptionsService(FrameworkDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public KeyOption[] GetKeyList()
        {
            var gptOptions = _dbContext.GptOptions
                .AsNoTracking()
                .OrderBy(m => m.GptOptionsId)
                .FirstOrDefault();

            if (gptOptions != null && gptOptions.OpenAIOptions?.OpenAI?.KeyList != null)
            {
                return gptOptions.OpenAIOptions.OpenAI.KeyList;
            }
            return null;
        }

    }
}
