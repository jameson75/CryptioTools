using System;
using System.Net;
using Newtonsoft.Json;
using CipherPark.ExchangeTools.CoinbasePro.Models;

namespace CipherPark.ExchangeTools.CoinbasePro.Api
{
    public class CoinbaseApiException : ApplicationException
    {
        public CoinbaseApiException() : base() { }
        public CoinbaseApiException(string jsonErrorMessage, WebException exception) : base(ParseError(jsonErrorMessage), exception) { }
        private static string ParseError(string json)
        {
            if (string.IsNullOrEmpty(json))
                return null;
            else
                return JsonConvert.DeserializeObject<Error>(json).Message;
        }
    }
}
