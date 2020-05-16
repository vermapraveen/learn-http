using System;
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

            SendAck(socket);

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

            for (int i = 0; i < lengthOfClientData; i++)
                Console.Write(Convert.ToChar(clientData[i]));
        }

        private static void SendAck(Socket socket)
        {
            ASCIIEncoding encoding = new ASCIIEncoding();
            socket.Send(encoding.GetBytes("This is acknowledgement from server... Thanks"));
            Console.WriteLine("Acknowledgement from server.");
        }
    }
}
