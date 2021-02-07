using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CipherPark.ExchangeTools.CoinbasePro.IntegrationTests
{
    public static class LinqExtensions
    {
        public static bool IsUniqueOn<TSource, TKey>(this IEnumerable<TSource> source,
                                                     Func<TSource, TKey> keySelector)
        {
            return source.GroupBy(keySelector).All(x => x.Count() <= 1);
        }
    }
}
