using System.Net;
using System.Net.Sockets;

namespace HamDotNetToolkit
{
    internal interface ITcpClientWrapper
    {
        int Available { get; }
        Socket Client { get; set; }
        bool Connected { get; }
        bool ExclusiveAddressUse { get; set; }
        LingerOption? LingerState { get; set; }
        bool NoDelay { get; set; }
        int ReceiveBufferSize { get; set; }
        int ReceiveTimeout { get; set; }
        int SendBufferSize { get; set; }
        int SendTimeout { get; set; }

        IAsyncResult BeginConnect(IPAddress address, int port, AsyncCallback? requestCallback, object? state);
        IAsyncResult BeginConnect(IPAddress[] addresses, int port, AsyncCallback? requestCallback, object? state);
        IAsyncResult BeginConnect(string host, int port, AsyncCallback? requestCallback, object? state);
        void Close();
        void Connect(IPAddress address, int port);
        void Connect(IPAddress[] ipAddresses, int port);
        void Connect(IPEndPoint remoteEP);
        void Connect(string hostname, int port);
        Task ConnectAsync(IPAddress address, int port);
        ValueTask ConnectAsync(IPAddress address, int port, CancellationToken cancellationToken);
        Task ConnectAsync(IPAddress[] addresses, int port);
        ValueTask ConnectAsync(IPAddress[] addresses, int port, CancellationToken cancellationToken);
        Task ConnectAsync(IPEndPoint remoteEP);
        ValueTask ConnectAsync(IPEndPoint remoteEP, CancellationToken cancellationToken);
        Task ConnectAsync(string host, int port);
        ValueTask ConnectAsync(string host, int port, CancellationToken cancellationToken);
        void Dispose();
        void EndConnect(IAsyncResult asyncResult);
        NetworkStream GetStream();
    }
}