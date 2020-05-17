using System;
using System.Net.Sockets;
using System.Text;

namespace comon.tcp2
{
    public class TcpClientManager
    {
        public static TcpClient Start(string serverIpAddress, int serverPort, string request)
        {
            TcpClient tcpclient = CreateTcpConnection(serverIpAddress, serverPort);

            byte[] inputBytesFromEncoding = GenerateClientInput(request);

            SendRequestToServer(tcpclient.GetStream(), inputBytesFromEncoding);

            return tcpclient;
        }

        private static void ReadResponseFromServer(NetworkStream clientServerStreamRef)
        {
            var response = Common.GetDataFromNetworkStream(clientServerStreamRef);
            Console.WriteLine(response);
        }

        private static TcpClient CreateTcpConnection(string serverIpAddress, int serverPort)
        {
            TcpClient tcpclient = new TcpClient();
            tcpclient.Connect(serverIpAddress, serverPort);
            return tcpclient;
        }

        private static void SendRequestToServer(NetworkStream clientStreamRef, byte[] inputBytesFromEncoding)
        {
            clientStreamRef.Write(inputBytesFromEncoding, 0, inputBytesFromEncoding.Length);
        }

        private static byte[] GenerateClientInput(string request)
        {
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] inputBytesFromEncoding = encoding.GetBytes(request);
            return inputBytesFromEncoding;
        }
    }
}
