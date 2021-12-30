using System;

namespace CipherPark.CryptioTools.Utility
{
    public static class DateTimeExtensions
    {
        public static long ToUnixSeconds(this DateTime dateTime)
        {
            return UnixTimestampConverter.ToUnixSeconds(dateTime);
        }

        public static long ToUnixMilliseconds(this DateTime dateTime)
        {
            return UnixTimestampConverter.ToUnixMilliseconds(dateTime);
        }
    }
}
