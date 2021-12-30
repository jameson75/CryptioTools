using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CipherPark.CryptioTools.CoinbasePro.Api
{
    internal static class CoinbaseRequestSignature
    {
        internal static string Generate(string timeStamp, string requestPath, string body, string secret, string method)
        {
            //*********************************************************
            //See: https://docs.gdax.com/?javascript#creating-a-request
            //     Section: "Signing a Message".
            //**********************************************************                      

            // create the prehash string by concatenating required parts
            var what = timeStamp + method + requestPath + body;

            // decode the base64 secret
            var key = Convert.FromBase64String(secret);

            // create a sha256 hmac with the secret
            var hmac = new System.Security.Cryptography.HMACSHA256(key);

            // sign the require message with the hmac
            // and finally base64 encode the result
            return Convert.ToBase64String(hmac.ComputeHash(System.Text.ASCIIEncoding.UTF8.GetBytes(what)));
        }
    }   
}
