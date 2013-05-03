using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading;

namespace DCounterCommunication
{
    public class NodeInfo
    {
        public string IP { get; set; }

        public string MachineName { get; set; }

        public int Port { get; set; }

        /// <summary>
        /// 是否可用
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// 是否是本机
        /// </summary>
        public bool IsLocalMachine { get; set; }

        /// <summary>
        /// 是否是主节点
        /// </summary>
        public bool IsPrimary { get; set; }

        /// <summary>
        /// 监听socket
        /// </summary>
        public Socket ServerSocket { get; set; }

        /// <summary>
        /// 连接socket
        /// </summary>
        public Socket ClientSocket { get; set; }


    }
}
