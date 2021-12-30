using Newtonsoft.Json;

namespace CipherPark.CryptioTools.Kraken.Models.Websockets
{
    public class WSRequest
    {
        [JsonProperty("event")]
        public string Event { get; set; }
        [JsonProperty("reqid")]
        public int? ReqId { get; set; }
    }
}