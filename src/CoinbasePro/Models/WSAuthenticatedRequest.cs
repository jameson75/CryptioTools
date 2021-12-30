using Newtonsoft.Json;

namespace CipherPark.CryptioTools.CoinbasePro.Models
{
    public class WSAuthenticatedRequest : WSRequest
    {
        [JsonProperty(PropertyName = "signature", NullValueHandling = NullValueHandling.Ignore)]
        public string Signature { get; set; }
        [JsonProperty(PropertyName = "key", NullValueHandling = NullValueHandling.Ignore)]
        public string Key { get; set; }
        [JsonProperty(PropertyName = "passphrase", NullValueHandling = NullValueHandling.Ignore)]
        public string Passphrase { get; set; }
        [JsonProperty(PropertyName = "timestamp", NullValueHandling = NullValueHandling.Ignore)]
        public string Timestamp { get; set; }
    }
}