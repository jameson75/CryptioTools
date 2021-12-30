using CipherPark.CryptioTools.Utility;

namespace CipherPark.CryptioTools.Kraken.Models
{
    public class StandardOrderRequest
    {
        [UrlParameter("pair")]
        public string Pair { get; set; }
        [UrlParameter("type")]
        public string Type { get; set; }
        [UrlParameter("ordertype")]
        public string OrderType { get; set; }
        [UrlParameter("price")]
        public double? Price { get; set; }
        [UrlParameter("price2")]
        public double? Price2 { get; set; }
        [UrlParameter("volume")]
        public double Volume { get; set; }
        [UrlParameter("leverage")]
        public double? Leverage { get; set; }
        [UrlParameter("oflags")]
        public string OrderFlags { get; set; }
        [UrlParameter("starttm")]
        public long? StartTime { get; set; }
        [UrlParameter("expiretm")]
        public long? ExpirationTime { get; set; }
        [UrlParameter("userref")]
        public int? UserReferenceId { get; set; }
        [UrlParameter("validate")]
        public string validate { get; set; }
        [UrlParameter("close")]
        public object[] close { get; set; }
    }
}
