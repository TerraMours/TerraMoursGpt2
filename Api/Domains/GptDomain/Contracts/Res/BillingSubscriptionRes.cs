using System.Text.Json.Serialization;

namespace TerraMours_Gpt.Domains.GptDomain.Contracts.Res
{
    public class BillingSubscriptionRes
    {
        [JsonPropertyName("object")]
        public string Object { get; set; }
        [JsonPropertyName("has_payment_method")]
        public bool HasPaymentMethod { get; set; }
        [JsonPropertyName("canceled")]
        public bool Canceled { get; set; }
        [JsonPropertyName("canceled_at")]
        public DateTime? CanceledAt { get; set; }
        [JsonPropertyName("delinquent")]
        public bool? Delinquent { get; set; }
        [JsonPropertyName("access_until")]
        public long AccessUntil { get; set; }
        [JsonPropertyName("soft_limit")]
        public int SoftLimit { get; set; }
        [JsonPropertyName("hard_limit")]
        public int HardLimit { get; set; }
        [JsonPropertyName("system_hard_limit")]
        public int SystemHardLimit { get; set; }
        [JsonPropertyName("soft_limit_usd")]
        public decimal SoftLimitUsd { get; set; }
        [JsonPropertyName("hard_limit_usd")]
        public decimal HardLimitUsd { get; set; }
        [JsonPropertyName("system_hard_limit_usd")]
        public decimal SystemHardLimitUsd { get; set; }
        [JsonPropertyName("plan")]
        public Plan Plan { get; set; }
        [JsonPropertyName("account_name")]
        public string AccountName { get; set; }
        [JsonPropertyName("po_number")]
        public string PoNumber { get; set; }
        [JsonPropertyName("billing_email")]
        public string BillingEmail { get; set; }
        [JsonPropertyName("tax_ids")]
        public string TaxIds { get; set; }
        //[JsonPropertyName("billing_address")]
        //public string BillingAddress { get; set; }
        [JsonPropertyName("business_address")]
        public string BusinessAddress { get; set; }
    }
    public class Plan
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }
        [JsonPropertyName("id")]
        public string Id { get; set; }
    }
}
