using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Ccr.Core;
using System.Net.Sockets;
using System.Threading;

namespace DcounterCommunication
{


    public static class CcrQueue
    {
        static int sleepTime = 1000;

        static Dispatcher dispatcher = new Dispatcher(0, "default");

        static DispatcherQueue taskQueue = new DispatcherQueue("queue", dispatcher);

        static Port<SocketAsyncEventArgs> port = new Port<SocketAsyncEventArgs>();

        static JoinSinglePortReceiver recever;

        static Thread sleep = new Thread(new ThreadStart(Default));

        static Socket sendSocket = null;

        static public void SetSocket(Socket socket)
        {
            sendSocket = socket;
        }

        static CcrQueue()
        {
            recever = Arbiter.MultipleItemReceive(true, port, 3, SocketHandler);

            sleep.Start();
            Arbiter.Activate(taskQueue, recever);
        }
        static public void AddSocket(SocketAsyncEventArgs SocketKey)
        {
            port.Post(SocketKey);
        }
        static void Default()
        {
            while (false)
            {
                var temp = port.TestForMultipleElements(port.ItemCount);
                if (temp != null)
                    temp.ToList().ForEach(a => { if (a != null)Console.WriteLine("testvalue = " + a.Item); });

                Thread.Sleep(sleepTime);
            }


        }
        static void SocketHandler(params SocketAsyncEventArgs[] t)
        {
            int dataLen = sizeof(Int64)  ;
            List<ArraySegment<byte>> temp = new List<ArraySegment<byte>>();

            int sendLen = t.Sum(a => a.BytesTransferred) - 1;
            temp.Add(new ArraySegment<byte>(BitConverter.GetBytes(sendLen), 0, 4));
            for (int i = 0; i < t.Length; i++)
            {
                temp.Add(new ArraySegment<byte>(t[i].Buffer, t[i].Offset + 1, t[i].BytesTransferred - 1));
                if (i != t.Length - 1)
                {
                    temp.Add(new ArraySegment<byte>(Encoding.ASCII.GetBytes(","), 0, 1));
                }


            }
            sendSocket.Send(temp);
            byte[] buffer = new byte[t.Length * dataLen];
            sendSocket.Receive(buffer);

            for (int i = 0; i < t.Length; i++)
            {
                Array.Copy(buffer, i * dataLen, t[i].Buffer, 0, dataLen);
                t[i].SetBuffer(0, dataLen);
                ((AsyncUserToken)t[i].UserToken).Socket.SendAsync(t[i]);
            }



        }
    }
}
