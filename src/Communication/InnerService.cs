using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;

namespace DcounterCommunication
{
    public class InnerService
    {
        Socket socket;
        Socket clientSocket;

        public InnerService()
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IPv4);
            socket.Listen(100);
            socket.Bind(new IPEndPoint(IPAddress.Any, 9090));
        }

        public void StartAccept()
        {
            clientSocket = socket.Accept();
        }
        public string Recv()
        {
            byte[] dataLen = new byte[4];

            clientSocket.Receive(dataLen, 4, SocketFlags.None);
            int len = BitConverter.ToInt32(dataLen, 0);
            byte[] buffer = new byte[len];
            clientSocket.Receive(buffer, len, SocketFlags.None);
            return Encoding.ASCII.GetString(buffer);
        }

        public void Send(byte[] buffer)
        {
            clientSocket.Send(buffer);

        }
    }
}
