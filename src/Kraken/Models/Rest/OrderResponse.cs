using Newtonsoft.Json;

namespace CipherPark.CryptioTools.Kraken.Models
{
    public class AddStandardOrderResult
    {
        [JsonProperty(PropertyName = "descr")]
        public OrderDescription Description { get; set; }
        [JsonProperty(PropertyName = "txid")]
        public string[] TransactionIds { get; set; }
    }

    public class OrderDescription
    {
        [JsonProperty(PropertyName = "order")]
        public string Order { get; set; }
        [JsonProperty(PropertyName = "close")]
        public string Close { get; set; }
    }   

    public class StandardOrderResponse : Response<AddStandardOrderResult> { }
}
