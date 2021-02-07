using System;
using System.Net;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CipherPark.ExchangeTools.CoinbasePro.Models;

namespace CipherPark.ExchangeTools.CoinbasePro.Api
{
    public class CoinbaseBufferedFeed : IDisposable
    {
        private readonly object _concurrencyRoot = new object();
        private readonly AutoResetEvent _dispatchSignal = new AutoResetEvent(false);
        private readonly ManualResetEvent _terminationSignal = new ManualResetEvent(false);
        private readonly Queue<WSChannelMessage> _messageQueue = new Queue<WSChannelMessage>();

        public CoinbaseFeed InternalFeed { get; } = null;

        public CoinbaseBufferedFeed(string endPoint, string key, string secret, string passPhrase, WebProxy proxy = null)
        {
            StartDispatcher();
            InternalFeed = new CoinbaseFeed(endPoint, key, secret, passPhrase, proxy);
            InternalFeed.MessageReceived += Stream_MessageReceived;
        }

        public async Task OpenAsync(string endPoint, string[] products)
        {
            await InternalFeed.OpenAsync(endPoint, products);
        }       

        public void Dispose()
        {
            //Ensure the dispatcher thread is signaled to terminate.
            _terminationSignal.Set();
            GC.SuppressFinalize(this);
        }

        public event Action<object, WSChannelMessage> MessageReceived;

        private void OnMessageReceived(WSChannelMessage message)
        {
            this.MessageReceived?.Invoke(this, message);
        }

        private void Stream_MessageReceived(object sender, WSChannelMessage message)
        {
            lock (_concurrencyRoot)
                _messageQueue.Enqueue(message);

            _dispatchSignal.Set();
        }

        private void StartDispatcher()
        {
            new Thread(() =>
            {
                bool exitThread = false;
                WaitHandle[] waitHandles = new WaitHandle[] { _dispatchSignal, _terminationSignal };
                while (!exitThread)
                {
                    int handleIndex = WaitHandle.WaitAny(waitHandles);
                    switch (handleIndex)
                    {
                        case 0:
                            Queue<WSChannelMessage> auxQueue = null;
                            lock (_concurrencyRoot)
                            {
                                auxQueue = new Queue<WSChannelMessage>(_messageQueue);
                                _messageQueue.Clear();
                            }
                            while (auxQueue.Count > 0)
                                OnMessageReceived(auxQueue.Dequeue());
                            break;
                        case 1:
                            exitThread = true;
                            break;
                        default:
                            exitThread = true;
                            break;
                    }
                }
            }).Start();
        }       
    }
}
