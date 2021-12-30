using Newtonsoft.Json;

namespace CipherPark.CryptioTools.CoinbasePro.Models
{
    public class MarginStatusResult
    {
        [JsonProperty("tier")]
        public int Tier { get; set; }

        [JsonProperty("enabled")]
        public bool Enabled { get; set; }

        [JsonProperty("eligible")]
        public bool Eligible { get; set; }
    }
}
