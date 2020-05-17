using comon.tcp2;
using System;

namespace tcpServer
{
    class Program
    {
        const string myIp = "127.0.0.1";
        const int portToListen = 8001;

        static void Main(string[] args)
        {
            try
            {
                var tcpListener = TcpServerManager.GetTcpListener(myIp, portToListen);
                tcpListener.Stop();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error..... " + e.StackTrace);
            }
        }
    }
}
