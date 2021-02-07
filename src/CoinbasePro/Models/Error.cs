using Newtonsoft.Json;

namespace CipherPark.ExchangeTools.CoinbasePro.Models
{
    public class Error
    {
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}