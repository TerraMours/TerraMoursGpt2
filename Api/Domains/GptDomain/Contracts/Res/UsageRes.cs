using System.Text.Json.Serialization;

namespace TerraMours_Gpt.Domains.GptDomain.Contracts.Res
{
    public class UsageRes
    {
        [JsonPropertyName("object")]
        public string Object { get; set; }

        [JsonPropertyName("daily_costs")]
        public List<DailyCost> DailyCosts { get; set; }

        [JsonPropertyName("total_usage")]
        public decimal TotalUsage { get; set; }
    }
    public class LineItem
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("cost")]
        public decimal Cost { get; set; }
    }
    public class DailyCost
    {
        [JsonPropertyName("timestamp")]
        public double Timestamp { get; set; }

        [JsonPropertyName("line_items")]
        public List<LineItem> LineItems { get; set; }
    }
}
