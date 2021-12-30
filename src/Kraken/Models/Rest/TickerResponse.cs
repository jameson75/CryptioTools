using System.Collections.Generic;
using CipherPark.CryptioTools.Utility;

namespace CipherPark.CryptioTools.Kraken.Models
{
    public class TickerInfo
    {
        [UrlParameter("a")]
        public double[] Asks { get; set; }
        [UrlParameter("b")]
        public double[] Bids { get; set; }
        [UrlParameter("c")]
        public double[] LastTradesClosed { get; set; }
        [UrlParameter("v")]
        public double[] Volumes { get; set; }
        [UrlParameter("p")]
        public double[] VWAPs { get; set; }
        [UrlParameter("t")]
        public long[] NumberOfTrades { get; set; }
        [UrlParameter("l")]
        public double[] Lows { get; set; }
        [UrlParameter("h")]
        public double[] Highs { get; set; }
        [UrlParameter("o")]
        public double Open { get; set; }
    }

    public class TickerResponse : Response<TickerResult> { }

    public class TickerResult : Dictionary<string, TickerInfo> { }
}
