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
            TcpListener tcpListener = new TcpListener(IPAddress.Parse(myIpAddress), portToListen);
            Console.WriteLine("Starting listener...");
            tcpListener.Start();

            Socket s = tcpListener.AcceptSocket();
            Console.WriteLine("Connection accepted from " + s.RemoteEndPoint);

            byte[] b = new byte[100];
            int k = s.Receive(b);
            Console.WriteLine("Recieved...");

            for (int i = 0; i < k; i++)
                Console.Write(Convert.ToChar(b[i]));

            ASCIIEncoding asen = new ASCIIEncoding();
            s.Send(asen.GetBytes("This is acknowledgement from server... Thanks"));
            Console.WriteLine("Acknowledgement from server.");
            /* clean up */
            s.Close();

            return tcpListener;
        }
    }
}
