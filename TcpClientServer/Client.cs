using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TcpClientServer
{
    public class Client
    {
        readonly IPAddress clientIP;
        readonly IPEndPoint clientEP;
        private int Port { get; set; }

        public Client(string ip, int port)
        {
            clientIP = IPAddress.Parse(ip);
            clientEP = new IPEndPoint(clientIP, Port);
            Port = port;
        }

        public bool SendData(string message)
        {
            Socket clientSock = new Socket(
                AddressFamily.InterNetwork,
                SocketType.Stream,
                ProtocolType.Tcp);

            try
            {
                clientSock.Connect(clientEP);

                clientSock.Send(Encoding.UTF8.GetBytes(message));

                clientSock.Shutdown(SocketShutdown.Both);
                clientSock.Close();

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
