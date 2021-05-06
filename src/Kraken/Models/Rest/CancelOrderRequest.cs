using CipherPark.ExchangeTools.Utility;

namespace CipherPark.ExchangeTools.Kraken.Models
{
    public class CancelOrderRequest
    {
        [UrlParameter("txid")]
        public string TransactionId { get; set; }
    }
}
