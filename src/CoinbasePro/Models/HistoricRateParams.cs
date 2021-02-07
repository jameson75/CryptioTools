using CipherPark.ExchangeTools.Utility;

namespace CipherPark.ExchangeTools.CoinbasePro.Models
{
    public class HistoricRateParams
    {
        [UrlParameter("granularity")]
        public long? Granularity { get; set; }
        [UrlParameter("start")]
        public long? Start { get; set; }
        [UrlParameter("end")]
        public long? End { get; set; }
    }
}