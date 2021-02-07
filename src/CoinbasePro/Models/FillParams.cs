using Newtonsoft.Json;
using CipherPark.ExchangeTools.CoinbasePro.Common;
using CipherPark.ExchangeTools.Utility;

namespace CipherPark.ExchangeTools.CoinbasePro.Models
{
    public class FillParams
    {
        /// <summary>
        /// Limit list of fills to this order id.
        /// </summary>
        [UrlParameter("order_id")]
        public string OrderId { get; set; }
        /// <summary>
        /// Limit list of fills to this product id.
        /// </summary>
        [UrlParameter("product_id")]
        public string ProductId { get; set; }
    }
}