using Newtonsoft.Json;

namespace CipherPark.ExchangeTools.Kraken.Models.Websockets
{
    public class WSSystemStatusMessage : WSMessage
    {
        [JsonProperty("connectionID")]
        public string ConnectionID { get; set; }
        public string Status { get; set; }
        public string Version { get; set; }
    }
}
