using System.Text.Json.Serialization;

namespace Terramours_Gpt_Vector.Req
{
    public record BaseReq
    {
        [JsonIgnore]
        public string? Key { get; set; }

        /// <summary>
        /// indexName
        /// </summary>
        [JsonIgnore]
        public string? Index { get; set; }

    }
}
