using System;

namespace CipherPark.CryptioTools.Utility
{
    public static class UnixTimestampConverter
    {
        private static readonly DateTime _UnixEpoc = new DateTime(1970, 1, 1);
        private static readonly long _UnixEpocInSeconds = (long)(_UnixEpoc - DateTime.MinValue).TotalSeconds;

        public static long ToUnixSeconds(DateTime dateTime)
        {
            return (long)(dateTime - _UnixEpoc).TotalSeconds;
        }

        public static long ToUnixMilliseconds(DateTime dateTime)
        {
            return (long)(dateTime - _UnixEpoc).TotalMilliseconds;
        }

        public static DateTime FromUnixSeconds(long date)
        {
            return new DateTime((date + _UnixEpocInSeconds) * TimeSpan.TicksPerSecond, DateTimeKind.Utc);
        }
    }
}
