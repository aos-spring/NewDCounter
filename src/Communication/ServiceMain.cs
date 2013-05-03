using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DCounterCommunication;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace DcounterCommunication
{
    public class ServiceMain
    {
        private NodeInfo nodeInfo;
        private List<NodeInfo> allNodeInfo = new List<NodeInfo>();
        public ServiceMain(bool client)
        {
            if (client)
            {
                allNodeInfo.Add(new NodeInfo { IP = "127.0.0.1", Port = 9999, IsPrimary = true, IsLocalMachine = true });
                allNodeInfo.Add(new NodeInfo { IP = "127.0.0.1", Port = 9999, IsPrimary = false, IsLocalMachine = false });
            }
            else
            {
                allNodeInfo.Add(new NodeInfo { IP = "127.0.0.1", Port = 9999, IsPrimary = true, IsLocalMachine = false });
                allNodeInfo.Add(new NodeInfo { IP = "127.0.0.1", Port = 9999, IsPrimary = false, IsLocalMachine = true });
            }

            Init();

        }

        private void Init()
        {
            nodeInfo = allNodeInfo.Find(a => a.IsLocalMachine);
        }
        public void Start()
        {

            if (nodeInfo.IsPrimary)
            {
                nodeInfo.ServerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                nodeInfo.ServerSocket.Bind(new IPEndPoint(IPAddress.Any, nodeInfo.Port));
                nodeInfo.ServerSocket.Listen(1000);

                while (true)
                {
                    Socket socket = nodeInfo.ServerSocket.Accept();
                    Thread work = new Thread(GetCounter);
                    work.Start(socket);
                }

            }
            else
            {
                nodeInfo.ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                var temp = allNodeInfo.Find(a => a.IsPrimary);
                nodeInfo.ClientSocket.Connect(temp.IP, temp.Port);
                CcrQueue.SetSocket(nodeInfo.ClientSocket);
                ConnectService con = new ConnectService(100, 8192);
                con.Init();
                con.Start(new IPEndPoint(IPAddress.Any, 8888));


            }

        }

        private void GetCounter(object sock)
        {
            Socket socket = (Socket)sock;
            byte[] lenBuffer = new byte[4];
            int len;
            while (socket.Receive(lenBuffer, lenBuffer.Length, SocketFlags.None) > 0)
            {
                len = BitConverter.ToInt32(lenBuffer, 0);
                byte[] value = new byte[len];
                socket.Receive(value, len, SocketFlags.None);
                string counterStr = Encoding.ASCII.GetString(value);
                var result = counterStr.Split(',').Select(a => new ArraySegment<byte>
                    (BitConverter.GetBytes(Core.GetCounter(a)), 0, 8)).ToList();

                socket.Send(result);

            }
        }
    }
}
