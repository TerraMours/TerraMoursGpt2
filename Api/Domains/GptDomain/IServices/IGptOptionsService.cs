using TerraMours_Gpt.Framework.Infrastructure.Contracts.Commons;

namespace TerraMours_Gpt_Api.Domains.GptDomain.IServices
{
    public interface IGptOptionsService
    {
        KeyOption[] GetKeyList();
    }
}
