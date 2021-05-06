
namespace CipherPark.ExchangeTools.Kraken.Models
{
    public abstract class Response<T>
    {
        public string[] Error { get; set; }
        public T Result { get; set; }
    }
}
