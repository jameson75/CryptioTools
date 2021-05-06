using System.Collections.Generic;

namespace CipherPark.ExchangeTools.Kraken.Models
{
    public class OrderBook
    {
        public object[][] Asks { get; set; }
        public object[][] Bids { get; set; }
    }

    public class OrderBookResponse : Response<OrderBookResult> { }

    public class OrderBookResult : Dictionary<string, OrderBook> { }
}
