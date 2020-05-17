using System;
using System.Net.Sockets;
using System.Text;

namespace comon.tcp
{
    public class TcpClientManager
    {
        public static TcpClient StartTcpClient(string serverIpAddress, int serverPort)
        {
            TcpClient tcpclient = CreateTcpConnection(serverIpAddress, serverPort);

            byte[] inputBytesFromEncoding = GenerateClientInput();

            SendRequestToServer(tcpclient.GetStream(), inputBytesFromEncoding);

            ReadResponseFromServer(tcpclient.GetStream());

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
            Console.WriteLine("Connecting.....");

            tcpclient.Connect(serverIpAddress, serverPort);
            // use the ipaddress as in the server program

            Console.WriteLine("Connected");
            return tcpclient;
        }

        private static void SendRequestToServer(NetworkStream clientStreamRef, byte[] inputBytesFromEncoding)
        {
            Console.WriteLine("Transmitting.....");

            clientStreamRef.Write(inputBytesFromEncoding, 0, inputBytesFromEncoding.Length);
        }

        private static byte[] GenerateClientInput()
        {
            Console.Write("Enter the string to be transmitted : ");

            String inputString = Console.ReadLine();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] inputBytesFromEncoding = encoding.GetBytes(inputString);
            return inputBytesFromEncoding;
        }
    }
}
