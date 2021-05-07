namespace CipherPark.ExchangeTools.Kraken.Models
{
    public class WebSocketsTokenResult
    {
        public string Token { get; set; }        
        public int Expires { get; set; }        
    }

    public class WebSocketsTokenResponse : Response<WebSocketsTokenResult> { }
}
