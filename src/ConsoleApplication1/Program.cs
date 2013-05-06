using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Ccr.Core;
using System.Net.Sockets;
using System.Threading;
 
using DCounterCommunication;

namespace ConsoleApplication1
{
    public static class CcrQueue
    {
        static int millionSecond = 500;
        static Dispatcher dispatcher = new Dispatcher(0, "default");

        static DispatcherQueue taskQueue = new DispatcherQueue("queue", dispatcher);

        public static Port<int> port = new Port<int>();
        static Port<int> time = new Port<int>();

        static JoinSinglePortReceiver recever;

        static CcrQueue()
        {
            recever = Arbiter.MultipleItemReceive<int>(true, port, 10, SocketHandler);

            Arbiter.Activate(taskQueue, recever);
            Default();
        }
        static public void AddSocket(int SocketKey)
        {
            port.Post(SocketKey);
        }

        static void Default()
        {

            ThreadPool.QueueUserWorkItem(b =>
              {
                  while (true)
                  {
                      var temp = port.TestForMultipleElements(port.ItemCount);
                      if (temp != null)
                          temp.ToList().ForEach(a => { if (a != null)Console.WriteLine("testvalue = " + a.Item); });

                      Thread.Sleep(millionSecond);
                  }
              });

        }

        static void SocketHandler(params int[] t)
        {


            t.ToList().ForEach(a => { Console.WriteLine("last is {0}", a); });


        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            ServiceMain main = new ServiceMain(true);
            main.Start();
            Console.Read();
        }
    }
}
