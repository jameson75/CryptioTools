using CipherPark.ExchangeTools.Utility;

namespace CipherPark.ExchangeTools.Kraken.Models
{
    public class OrderBookRequest
    {
        [UrlParameter("pair")]
        public string Pair { get; set; }
        [UrlParameter("count")]
        public int? Count { get; set; }
    }
}
