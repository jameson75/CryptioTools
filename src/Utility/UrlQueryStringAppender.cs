using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CipherPark.CryptioTools.Utility
{
    public static class UrlQueryStringAppender
    {
        public static string Append(string baseUrl, object queryData)
        {
            StringBuilder sb = new StringBuilder(baseUrl);
            if (queryData != null)
            {
                string queryString = UrlQueryStringSerializer.SerializeObject(queryData);
                if (!string.IsNullOrEmpty(queryString))
                {
                    if (!baseUrl.EndsWith('?'))
                        sb.Append('?');
                    sb.Append(queryString);
                }
            }
            return sb.ToString();
        }
    }
}
