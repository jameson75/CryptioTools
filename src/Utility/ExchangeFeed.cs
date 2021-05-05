using System;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using System.Net;

namespace CipherPark.ExchangeTools.Utility
{
    public abstract class ExchangeFeed
    {
        protected async Task OpenAsync(string wsUrl, string content, WebProxy webProxy = null)
        {
            WebSocket client = new ClientWebSocket();
            if (webProxy != null)
                ((ClientWebSocket)client).Options.Proxy = webProxy;
            await ((ClientWebSocket)client).ConnectAsync(new Uri(wsUrl), CancellationToken.None);       
            var payLoad = new ArraySegment<byte>(System.Text.Encoding.UTF8.GetBytes(content));
            await client.SendAsync(payLoad, WebSocketMessageType.Text, true, CancellationToken.None);
            new Thread(async () =>
            {
                while (true)
                {
                    byte[] responseBuffer = Array.Empty<byte>();
                    while (true)
                    {
                        var chunkBuffer = new ArraySegment<byte>(new byte[10]);
                        WebSocketReceiveResult result = await client.ReceiveAsync(chunkBuffer, CancellationToken.None);
                        if (result.Count > 0)
                        {
                            Array.Resize(ref responseBuffer, responseBuffer.Length + result.Count);
                            Array.Copy(chunkBuffer.Array, 0, responseBuffer, responseBuffer.Length - result.Count, result.Count);
                        }
                        if (result.EndOfMessage)
                            break;
                    }
                    string response = System.Text.Encoding.UTF8.GetString(responseBuffer);
                    HandleMessageReceived(response);
                }
            }).Start();
        }

        private void HandleMessageReceived(string response)
        {
            this.RawMessageRecieved?.Invoke(this, response);
            OnRawMessageReceived(response);
        }

        protected virtual void OnRawMessageReceived(string response) { }
        
        public event Action<object, string> RawMessageRecieved;        
    }
}
