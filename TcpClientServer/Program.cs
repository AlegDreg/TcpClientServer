namespace TcpClientServer
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            StartServer();

            StartClient();
        }

        private static void StartClient()
        {
            Client client = new Client("255.255.255.255" /* - Ip example*/ , 33367);

            if (client.SendData("Hello world!"))
                Console.WriteLine("The message has been sent") ;
        }

        private static void StartServer()
        {
            Server server = new Server(1024, 33367);
            server.Start();
            server.OnNewMessage += Server_OnNewMessage;
        }

        private static void Server_OnNewMessage(string message)
        {
            Console.WriteLine("New message received: " + message);
        }
    }
       
}

