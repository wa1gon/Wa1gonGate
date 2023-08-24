using System.Net;
using System.Net.Sockets;

namespace HamDotNetToolkit
{
    internal class TcpClientWrapper : ITcpClientWrapper
    {
        private TcpClient? client = null;
        public int ReceiveTimeout { get => client!.ReceiveTimeout; set => client!.ReceiveTimeout = value; }
        public int ReceiveBufferSize { get => client!.ReceiveBufferSize; set => client!.ReceiveBufferSize = value; }

        public bool NoDelay { get => client!.NoDelay; set => client!.NoDelay = value; }

        public LingerOption? LingerState { get => client!.LingerState; set => client!.LingerState = value; }

        public bool ExclusiveAddressUse { get => client!.ExclusiveAddressUse; set => client!.ExclusiveAddressUse = value; }

        public bool Connected { get => client!.Connected; }

        public Socket Client { get => client!.Client; set => client!.Client = value; }
        public int Available { get => client!.Available; }

        public int SendBufferSize { get => client!.SendBufferSize; set => client!.SendBufferSize = value; }

        public int SendTimeout { get => client!.SendTimeout; set => client!.SendTimeout = value; }

        public TcpClientWrapper()
        {
            this.client = new TcpClient();
        }
        public TcpClientWrapper(TcpClient client)
        {
            this.client = client;
        }
        public TcpClientWrapper(AddressFamily family)
        {
            client = new TcpClient(family); 
        }
        public TcpClientWrapper(IPEndPoint endPoint)
        {
            client = new TcpClient(endPoint);
        }
        public TcpClientWrapper(string hostName, int port)
        {
            client = new TcpClient(hostName,port);
        }
        public IAsyncResult BeginConnect(IPAddress address, int port, AsyncCallback? requestCallback, object? state)
        {
            return client!.BeginConnect(address, port, requestCallback, state);
        }

        public IAsyncResult BeginConnect(IPAddress[] addresses, int port, AsyncCallback? requestCallback, object? state)
        {
            return client!.BeginConnect(addresses, port, requestCallback, state);
        }

        public IAsyncResult BeginConnect(string host, int port, AsyncCallback? requestCallback, object? state)
        {
            return client!.BeginConnect(host, port, requestCallback, state);
        }

        public void Close()
        {
            client!.Close();
        }

        public void Connect(IPAddress address, int port)
        {
            client!.Connect(address, port);
        }

        public void Connect(IPAddress[] ipAddresses, int port)
        {
            client!.Connect(ipAddresses, port);
        }

        public void Connect(IPEndPoint remoteEP)
        {
            client!.Connect(remoteEP);
        }

        public void Connect(string hostname, int port)
        {
            client!.Connect(hostname, port);
        }

        public Task ConnectAsync(IPAddress[] addresses, int port)
        {
            return client!.ConnectAsync(addresses, port);
        }


        public ValueTask ConnectAsync(IPAddress[] addresses, int port, CancellationToken cancellationToken)
        {
            return ConnectAsync(addresses, port, cancellationToken);
        }

        public Task ConnectAsync(IPAddress address, int port)
        {
            return client!.ConnectAsync(address, port);
        }

        public ValueTask ConnectAsync(string host, int port, CancellationToken cancellationToken)
        {
            return client!.ConnectAsync(host, port, cancellationToken);
        }

        public Task ConnectAsync(string host, int port)
        {
            return client!.ConnectAsync(host, port);
        }

        public ValueTask ConnectAsync(IPAddress address, int port, CancellationToken cancellationToken)
        {
            return client!.ConnectAsync(address.ToString(), port, cancellationToken);
        }
        public Task ConnectAsync(IPEndPoint remoteEP)
        {
            return client!.ConnectAsync(remoteEP);
        }
        public ValueTask ConnectAsync(IPEndPoint remoteEP, CancellationToken cancellationToken)
        {
            return client!.ConnectAsync(remoteEP, cancellationToken);
        }

        public void Dispose()
        {
            client!.Dispose();
        }

        public void EndConnect(IAsyncResult asyncResult)
        {
            EndConnect(asyncResult);
        }

        public NetworkStream GetStream()
        {
            return client!.GetStream();
        }

        protected virtual void Dispose(bool disposing)
        {
            client!.Dispose();
        }
    }
}
