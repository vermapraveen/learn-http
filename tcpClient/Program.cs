using comon.tcp2;
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
                Console.Write("Enter request : ");
                String inputString = Console.ReadLine();

                var tcpclient = TcpClientManager.Start(myIp, portToListen, "GET docOnServer.html");
                var response = Common.GetDataFromNetworkStream(tcpclient.GetStream());
                Console.WriteLine(response);

                tcpclient.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error..... " + e.StackTrace);
            }
        }


    }
}
