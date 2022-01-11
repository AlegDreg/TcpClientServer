using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TcpClientServer
{
    internal class Server
    {
        private int Port { get; set; }
        private string Ip { get; set; }

        /// <summary>
        /// Maximum length of the received message
        /// </summary>
        private int ByteLenght { get; set; }

        public delegate void NewMessage(string message);
        public event NewMessage OnNewMessage;

        public Server(int bytes, int port, string? ip = null)
        {
            ByteLenght = bytes;

            Port = port;

            Ip = ip == null ? TcpClientServer.Ip.GetCurrentIp() : ip;
        }

        public void Start()
        {
            Socket serverSock = new Socket(
                AddressFamily.InterNetwork,
                SocketType.Stream,
                ProtocolType.Tcp);

            IPEndPoint serverEP = new IPEndPoint(IPAddress.Any, Port);

            SocketPermission perm = new SocketPermission(NetworkAccess.Accept, TransportType.Tcp, Ip, Port);
            perm.Assert();

            serverSock.Bind(serverEP);
            serverSock.Listen(10);

            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    Socket connection = serverSock.Accept();

                    Byte[] serverBuffer = new Byte[ByteLenght];

                    int bytes = connection.Receive(
                        serverBuffer,
                        serverBuffer.Length,
                        0);

                    string message = Encoding.UTF8.GetString(
                        serverBuffer,
                        0,
                        bytes);

                    connection.Close();

                    CheckMessage(message);
                }
            }).Start();
        }

        private void CheckMessage(string message)
        {
            OnNewMessage?.Invoke(message);
        }
    }
}
