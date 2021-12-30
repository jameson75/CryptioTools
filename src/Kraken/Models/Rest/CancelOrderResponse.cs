namespace CipherPark.CryptioTools.Kraken.Models
{
    public class CancelOrderResult
    {
        public int Count { get; set; }
        public object Pending { get; set; }
    }
    
    public class CancelOrderResponse : Response<CancelOrderResult> { }
}
