using Newtonsoft.Json;
using CipherPark.CryptioTools.Utility;

namespace CipherPark.CryptioTools.CoinbasePro.Models
{
    public class ListOrderParams
    {
        /// <summary>
        /// [open, pending, active] Limit list of orders to these statuses. 
        /// Passing active returns orders of all statuses.
        /// </summary>
        [UrlParameter("status")]
        public string Status { get; set; }
        /// <summary>
        /// [optional] Only list orders for a specific product
        /// </summary>
        [UrlParameter("product_id")]
        public string ProductId { get; set; }
    }
}