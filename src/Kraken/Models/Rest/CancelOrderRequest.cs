using CipherPark.CryptioTools.Utility;

namespace CipherPark.CryptioTools.Kraken.Models
{
    public class CancelOrderRequest
    {
        [UrlParameter("txid")]
        public string TransactionId { get; set; }
    }
}
