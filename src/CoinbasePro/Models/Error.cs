using Newtonsoft.Json;

namespace CipherPark.CryptioTools.CoinbasePro.Models
{
    public class Error
    {
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}