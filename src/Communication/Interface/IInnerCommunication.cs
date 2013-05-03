using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DCounterCommunication
{
    /// <summary>
    ///dcounter 机器内部通信
    /// </summary>
    interface IInnerCommunication
    {
        /// <summary>
        /// 节点列表
        /// </summary>
        List<NodeInfo> Nodes { get; set; }

        /// <summary>
        ///  本机服务开启,找到NodeInfos里面IsLocalMachine为true中的ServerSocket进行监听
        /// </summary>
        /// <param name="nodes">本机节点数据</param>
        /// <returns>正确返回true</returns>
        bool StartListen();

        /// <summary>
        /// 连接nodes islocalmachine为false的节点，更新机器状态池
        /// </summary>
        /// <param name="nodes">所有节点数据</param>
        /// <returns>成功返回NULL,失败返回所有失败节点，并重置状态</returns>
        List<NodeInfo> ConnetNodes();
    }
}
