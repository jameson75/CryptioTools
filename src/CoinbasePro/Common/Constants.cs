using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CipherPark.CryptioTools.CoinbasePro.Common
{
    public static class HistoricRateGranularity
    {
        public const int OneMinute = 60;
        public const int FiveMinutes = 300;
        public const int FifteenMinutes = 900;
        public const int OneHour = 3600;
        public const int SixHours = 21600;
        public const int OneDay = 86400;
    }

    public static class WSDoneMessageReason
    {
        public const string Filled = "filled";
        public const string Canceled = "canceled";
    }

    public static class WSMessageTypes
    {
        public const string Heartbeat = "heartbeat";
        public const string Ticker = "ticker";
        public const string Level2Snapshot = "snapshot";
        public const string Level2Update = "l2update";
        public const string Received = "received";
        public const string Open = "open";
        public const string Done = "done";
        public const string Match = "match";
        public const string Change = "change";
        public const string Activate = "activate";
        public const string Status = "status";
    }

    public static class WSRequestTypes
    {
        public const string Subscribe = "subscribe";
        public const string Unsubscribe = "unsubscribe";
    }

    public static class WSChannelNames
    {
        public const string Heartbeat = "heartbeat";
        public const string Level2 = "level2";
        public const string Ticker = "ticker";
        public const string Full = "full";
        public const string User = "user";
        public const string Matches = "matches";
    }

    public static class StopTypes
    {
        public const string Loss = "loss";
        public const string Entry = "entry";
    }

    public static class OrderTypes
    {
        public const string Limit = "limit";
        public const string Market = "market";
    }

    public static class Sides
    {
        public const string Buy = "buy";
        public const string Sell = "sell";
    }  

    public static class SelfTradePreventionTypes
    {
        public const string dc = "dc";
        public const string co = "co";
        public const string cn = "cn";
        public const string cb = "cb";        
    }

    public static class TimeInForceTypes
    {
        public const string GTC = "GTC";
        public const string GTT = "GTT";
        public const string IOC = "IOC";
        public const string FOK = "FOK";
    }  

    public static class OrderStatusFilter
    {
        public const string open = "open";
        public const string pending = "pending";
        public const string active = "active";
        public const string all = "all";
    }
}
