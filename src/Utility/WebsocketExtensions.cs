using System.Net.WebSockets;

namespace CipherPark.ExchangeTools.Utility
{
    public static class WebSocketExtensions
    {
        public static void SetProxy(this WebSocket client, System.Net.WebProxy proxy)
        {
            if (client is ClientWebSocket)
                ((ClientWebSocket)client).Options.Proxy = proxy;
            else
                ((System.Net.WebSockets.Managed.ClientWebSocket)client).Options.Proxy = proxy;
        }
    }
}
