using Newtonsoft.Json;

namespace CipherPark.CryptioTools.CoinbasePro.Models
{
    public class Account
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("currency")]
        public string Currency { get; set; }
        [JsonProperty("balance")]
        public double Balance { get; set; }
        [JsonProperty("hold")]
        public double Hold { get; set; }
        [JsonProperty("available")]
        public double Available { get; set; }
        [JsonProperty("margin_enabled")]
        public bool MarginEnabled { get; set; }
        [JsonProperty("funded_amount")]
        public double FundedAmount { get; set; }
        [JsonProperty("default_amount")]
        public double DefaultAmount { get; set; }
        [JsonProperty("profile_id")]
        public string ProfileId { get; set; }
    }
}
