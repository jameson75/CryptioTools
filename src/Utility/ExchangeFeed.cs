using System;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using System.Net;

namespace CipherPark.CryptioTools.Utility
{
    public abstract class ExchangeFeed : IDisposable
    {
        private ClientWebSocket client;
        
        protected bool IsOpen
        {
            get { return client?.State == WebSocketState.Open; }
        }

        protected async Task SendAsync(string content)
        {
            if (client == null)
                throw new InvalidOperationException("Client not created.");    
            var payLoad = new ArraySegment<byte>(System.Text.Encoding.UTF8.GetBytes(content));
            await client.SendAsync(payLoad, WebSocketMessageType.Text, true, CancellationToken.None);
        }        

        protected async Task OpenAsync(string wsUrl, string content, WebProxy webProxy = null)
        {
            client = new ClientWebSocket();
            if (webProxy != null)
                client.Options.Proxy = webProxy;
            await client.ConnectAsync(new Uri(wsUrl), CancellationToken.None);         
            if (content != null)
                await SendAsync(content);
            new Thread(async () =>
            {
                while (client.State == WebSocketState.Open)                
                {
                    byte[] responseBuffer = Array.Empty<byte>();                    
                    while (true)
                    {
                        var chunkBuffer = new ArraySegment<byte>(new byte[10]);
                        WebSocketReceiveResult result = await client.ReceiveAsync(chunkBuffer, CancellationToken.None);
                        if (result.MessageType == WebSocketMessageType.Close)
                        {
                            break;
                        }
                        else
                        {
                            if (result.Count > 0)
                            {
                                Array.Resize(ref responseBuffer, responseBuffer.Length + result.Count);
                                Array.Copy(chunkBuffer.Array, 0, responseBuffer, responseBuffer.Length - result.Count, result.Count);
                            }
                            if (result.EndOfMessage && responseBuffer.Length > 0)
                            {
                                string response = System.Text.Encoding.UTF8.GetString(responseBuffer);
                                OnEndOfMessage(response);
                                break;
                            }
                        }                       
                    }                  
                }

                client.Dispose();
                client = null;

            }).Start();
        }

        private void OnEndOfMessage(string response)
        {
            this.RawMessageRecieved?.Invoke(this, response);
            OnRawMessageReceived(response);
        }

        protected virtual void OnRawMessageReceived(string response) { }

        public virtual void Dispose()
        {
            if (client != null)
            {
                if (client.State == WebSocketState.Open)
                {
                    client.CloseOutputAsync(WebSocketCloseStatus.NormalClosure, null, CancellationToken.None).GetAwaiter().GetResult();
                }
                else
                {
                    client.Dispose();
                    client = null;
                }
            }
        }

        public event Action<object, string> RawMessageRecieved;        
    }
}
