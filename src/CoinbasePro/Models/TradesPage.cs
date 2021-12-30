using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CipherPark.CryptioTools.CoinbasePro.Models
{
    public class TradesPage
    {
        /// <summary>
        /// 
        /// </summary>
        public Trade[] Trades { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Before { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string After { get; set; }
    }
}
