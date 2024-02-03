using System.Text.Json.Serialization;

namespace TerraMours_Gpt.Domains.GptDomain.Contracts.Res
{
    public class ErrorRes
    {
        [JsonPropertyName("error")]
        public ErrorDto Error { get; set; }
    }

    public class ErrorDto
    {
        [JsonPropertyName("code")]
        public string Code { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonPropertyName("param")]
        public string Param { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("details")]
        public List<ErrorDetail> Details { get; set; }
    }

    public class ErrorDetail
    {
        [JsonPropertyName("field")]
        public string Field { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }
    }
}
