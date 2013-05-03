using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading;

namespace socketClietn
{
    class Program
    {
        static void Main(string[] args)
        {

            for (int i = 0; i < 2; i++)
            {
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socket.Connect("127.0.0.1", 8888);
                byte[] a1 = new byte[5];
                a1[0] = 4;
                Array.Copy(Encoding.ASCII.GetBytes("test"), 0, a1, 1, 4);

                socket.Send(a1);
                ThreadPool.QueueUserWorkItem(Print, socket);
            }

            Socket socket1 = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket1.Connect("127.0.0.1", 8888);
            byte[] a3 = new byte[6];
            a3[0] = 5;
            Array.Copy(Encoding.ASCII.GetBytes("testa"), 0, a3, 1, 5);
            socket1.Send(a3);
            ThreadPool.QueueUserWorkItem(Print, socket1);
            Console.Read();
        }
        static void Print(object sock)
        {
            Socket socket = sock as Socket;
            byte[] re = new byte[8];
            socket.Receive(re);
            Console.WriteLine(BitConverter.ToInt64(re, 0));

        }
    }
}
