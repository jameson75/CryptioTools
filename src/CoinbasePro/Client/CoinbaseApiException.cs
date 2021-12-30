using System;
using System.Net;
using Newtonsoft.Json;
using CipherPark.CryptioTools.CoinbasePro.Models;

namespace CipherPark.CryptioTools.CoinbasePro.Api
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
