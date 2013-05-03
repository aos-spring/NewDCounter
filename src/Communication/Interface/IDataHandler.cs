using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DCounterCommunication
{
    /// <summary>
    /// 处理模块接口
    /// </summary>
    interface IDataHandler
    {
        /// <summary>
        /// 处理函数，获得计数结果
        /// </summary>
        /// <param name="buffer">传入参数：业务线</param>
        /// <returns>计数结果</returns>
        byte[] GetCounterResult(byte[] buffer);
    }
}
