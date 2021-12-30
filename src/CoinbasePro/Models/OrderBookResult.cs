namespace CipherPark.CryptioTools.CoinbasePro.Models
{
    public class OrderBookResult
    {
        public long Sequence { get; set; }
        public object[][] Bids { get; set; }
        public object[][] Asks { get; set; }
    }

    #region Extension
    public static class OrderBookResultExtensions
    {
        public static double GetBidPrice(this OrderBookResult result, int index)
        {
            return System.Convert.ToDouble(result.Bids[index][0]);
        }

        public static double GetBidSize(this OrderBookResult result, int index)
        {
            return System.Convert.ToDouble(result.Bids[index][1]);
        }

        public static string GetBidOrderId(this OrderBookResult result, int index)
        {
            return System.Convert.ToString(result.Bids[index][2]);
        }

        public static double GetAskPrice(this OrderBookResult result, int index)
        {
            return System.Convert.ToDouble(result.Asks[index][0]);
        }

        public static double GetAskSize(this OrderBookResult result, int index)
        {
            return System.Convert.ToDouble(result.Asks[index][1]);
        }

        public static string GetAskOrderId(this OrderBookResult result, int index)
        {
            return System.Convert.ToString(result.Asks[index][2]);
        }
    }
    #endregion
}
    