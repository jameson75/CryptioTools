using Newtonsoft.Json;
using CipherPark.CryptioTools.CoinbasePro.Common;
using CipherPark.CryptioTools.Utility;

namespace CipherPark.CryptioTools.CoinbasePro.Models
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