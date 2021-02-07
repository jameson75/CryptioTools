using System;
using System.Text;

namespace CipherPark.ExchangeTools.Utility
{
    public static class ExceptionExtensions
    {
        /// <summary>
        /// Concatenates the messages and stack trace of an exception hierarchy and
        /// returns the results.
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        public static string GetCompleteDetails(this Exception exception)
        {
            StringBuilder sb = new StringBuilder();
            Exception _ex = exception;
            while (_ex != null)
            {
                sb.AppendLine(_ex.Message);
                sb.AppendLine(_ex.StackTrace);
                _ex = _ex.InnerException;
            }
            return sb.ToString();
        }
    }
}
