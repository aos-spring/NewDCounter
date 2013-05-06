using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DCounterCommunication;

namespace client
{
    class Program
    {
        static void Main(string[] args)
        {

            ServiceMain main = new ServiceMain(false);
            main.Start();
            Console.Read();
        }
    }
}
