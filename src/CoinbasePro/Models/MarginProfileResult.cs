using System;
using Newtonsoft.Json;

namespace CipherPark.CryptioTools.CoinbasePro.Models
{
    public class MarginProfileResult
    {
        [JsonProperty("profile_id")]
        public string ProfileId { get; set; }

        [JsonProperty("margin_initial_equity")]
        public double MarginInitialEquity { get; set; }

        [JsonProperty("margin_warning_equity")]
        public double MarginWarningEquity { get; set; }

        [JsonProperty("margin_call_equity")]
        public double MarginCallEquity { get; set; }

        [JsonProperty("equity_percentage")]
        public double EquityPercentage { get; set; }

        [JsonProperty("selling_power")]
        public double SellingPower { get; set; }

        [JsonProperty("buying_power")]
        public double BuyingPower { get; set; }

        [JsonProperty("borrow_power")]
        public double BorrowPower { get; set; }

        [JsonProperty("interest_rate")]
        public double InterestRate { get; set; }

        [JsonProperty("interest_paid")]
        public double InterestPaid { get; set; }

        [JsonProperty("collateral_currencies")]
        public string[] CollateralCurrencies { get; set; }

        [JsonProperty("collateral_hold_value")]
        public string CollateralHoldValue { get; set; }

        [JsonProperty("last_liquidation_at")]
        public DateTime  LastLiquidationAt { get; set; }

        [JsonProperty("available_borrow_limits")]
        public BorrowLimits AvailableBorrowLimits { get; set; }  
        
        [JsonProperty("borrow_limit")]
        public double BorrowLimit { get; set; }

        [JsonProperty("top_up_amounts")]
        public TopUpAmounts TopUpAmounts { get; set; }
}

    public class BorrowLimits
    {
        [JsonProperty("marginable_limit")]
        public double MarginableLImit { get; set; }

        [JsonProperty("nonmarginable_limit")]
        public double NonMarginableLimit { get; set; }    
    }

    public class TopUpAmounts
    {
        [JsonProperty("borrowable_usd")]
        public double BorrowableUSD { get; set; }

        [JsonProperty("non_borrowable_usd")]
        public double NonBorrowableUSD { get; set; }
}
}
