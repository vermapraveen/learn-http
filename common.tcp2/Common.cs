using System.IO;
using System.Net.Sockets;
using System.Text;

namespace comon.tcp2
{
    public static class Common
    {
        public static string GetDataFromNetworkStream(NetworkStream networkStream)
        {
            StringBuilder networkData = new StringBuilder();

            if (networkStream.CanRead)
            {
                byte[] readBufferSize = new byte[1024];
                int numberOfBytesRead = 0;

                do
                {
                    numberOfBytesRead = networkStream.Read(readBufferSize, 0, readBufferSize.Length);
                    networkData.AppendFormat("{0}", Encoding.ASCII.GetString(readBufferSize, 0, numberOfBytesRead));
                }
                while (networkStream.DataAvailable);

                return networkData.ToString();
            }
            else
            {
                throw new IOException("Sorry. You cannot read from this NetworkStream.");
            }
        }
    }
}
