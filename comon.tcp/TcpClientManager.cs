using System;
using System.IO;
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

            Stream clientServerStreamRef = SendToServer(tcpclient, inputBytesFromEncoding);

            ReadServerAck(clientServerStreamRef);

            return tcpclient;
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

        private static Stream SendToServer(TcpClient tcpclient, byte[] inputBytesFromEncoding)
        {
            Stream clientStreamRef = tcpclient.GetStream();
            Console.WriteLine("Transmitting.....");

            clientStreamRef.Write(inputBytesFromEncoding, 0, inputBytesFromEncoding.Length);

            return clientStreamRef;
        }

        private static void ReadServerAck(Stream clientStreamRef)
        {
            byte[] ackBytesFromServer = new byte[100];
            int k = clientStreamRef.Read(ackBytesFromServer, 0, 100);

            for (int i = 0; i < k; i++)
                Console.Write(Convert.ToChar(ackBytesFromServer[i]));
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
