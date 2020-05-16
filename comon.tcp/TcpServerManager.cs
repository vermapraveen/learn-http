using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace comon.tcp
{
    public class TcpServerManager
    {
        public static TcpListener GetTcpListener(string myIpAddress, int portToListen)
        {
            TcpListener tcpListener = StartTcpListener(myIpAddress, portToListen);

            Socket socket = tcpListener.AcceptSocket();

            ReceiveClientData(socket);

            socket.Close();

            return tcpListener;
        }

        private static TcpListener StartTcpListener(string myIpAddress, int portToListen)
        {
            TcpListener tcpListener = new TcpListener(IPAddress.Parse(myIpAddress), portToListen);
            Console.WriteLine("Starting listener...");
            tcpListener.Start();
            return tcpListener;
        }

        private static void ReceiveClientData(Socket socket)
        {
            Console.WriteLine("Connection accepted from " + socket.RemoteEndPoint);

            byte[] clientData = new byte[100];
            int lengthOfClientData = socket.Receive(clientData);
            Console.WriteLine("Recieved...");

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < lengthOfClientData; i++)
                sb.Append(Convert.ToChar(clientData[i]));

            var clientDataText = sb.ToString();
            string[] inputStrings = clientDataText.Split(new char[] { ' ' });
            string documentToReturn = "";

            if (inputStrings[0] == "GET")
            {
                if (inputStrings.Length > 1)
                    documentToReturn = inputStrings[1];
            }

            ASCIIEncoding encoding = new ASCIIEncoding();

            if (!string.IsNullOrEmpty(documentToReturn))
            {
                if (File.Exists(documentToReturn))
                {
                    socket.Send(encoding.GetBytes("Found!<From Server>"));
                }
                else
                {
                    socket.Send(encoding.GetBytes("Not Found!<From Server>"));
                }
            }
            else
            {
                socket.Send(encoding.GetBytes("Invalid Request!<From Server>"));
            }

            Console.WriteLine("Acknowledgement sent.");

        }
    }
}
