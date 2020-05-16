using comon.tcp;
using System;

namespace tcpClient
{
    class Program
    {
        const string myIp = "127.0.0.1";
        const int portToListen = 8001;

        static void Main(string[] args)
        {
            try
            {
                var tcpclnt = TcpClientManager.GetTcpClient(myIp, portToListen, null);

                tcpclnt.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error..... " + e.StackTrace);
            }
        }


    }
}
