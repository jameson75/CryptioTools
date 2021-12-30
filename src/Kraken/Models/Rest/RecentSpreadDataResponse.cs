using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CipherPark.CryptioTools.Kraken.Models
{
    public class RecentSpreadDataResult
    {
        [JsonExtensionData(ReadData = true)]
        private Dictionary<string, JToken> _raw = null;
        public long Last { get; set; }
        public Dictionary<string, object[][]> Data
        {
            get { return _raw.Take(1).ToDictionary(k => k.Key, v => ((JToken)v.Value).ToObject<object[][]>()); }
        }
    }
    
    public class RecentSpreadDataResponse : Response<RecentSpreadDataResult> { }
}
