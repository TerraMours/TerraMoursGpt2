using System.Runtime.Serialization;
using YamlDotNet.Core.Tokens;

namespace TerraMours_Gpt.Domains.GptDomain.Contracts.Enum
{
    public enum ModelTypeEnum
    {
        [EnumMember(Value = "ChatGpt")]
        ChatGpt,
        [EnumMember(Value = "Azure")]
        Azure
    }
}
