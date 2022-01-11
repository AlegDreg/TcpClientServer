namespace TcpClientServer
{
    internal class Ip
    {
        public static string GetCurrentIp()
        {
            return new System.Net.WebClient().DownloadString("https://api.ipify.org");
        }
    }
}
