using CipherPark.CryptioTools.Utility;

namespace CipherPark.CryptioTools.Kraken.Models
{
    public class TradesHistoryRequest
    {
        [UrlParameter("type")]
        public string Type { get; set; }
        [UrlParameter("trades")]
        public bool? Trades { get; set; }
        [UrlParameter("start")]
        public long? Start { get; set; }
        [UrlParameter("end")]
        public long? End { get; set; }
        [UrlParameter("ofs")]
        public string ResultOffset { get; set; }
    }
}
