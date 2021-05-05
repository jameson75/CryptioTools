using CipherPark.ExchangeTools.Utility;

namespace CipherPark.ExchangeTools.Kraken.Models
{
    public class TickerRequest
    {
        [UrlParameter("pair")]
        public string Pair { get; set; }
    }
}
