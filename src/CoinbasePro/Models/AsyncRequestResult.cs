using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CipherPark.ExchangeTools.CoinbasePro.Models
{
    public class AsyncRequestResult
    {
        public AsyncRequestResult(string content, string before, string after)
        {
            Content = content;
            Before = before;
            After = after;
        }
        public string Content { get; }
        public string Before { get; }
        public string After { get; }
    }
}
