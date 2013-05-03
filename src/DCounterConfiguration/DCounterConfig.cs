using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DCounterConfiguration
{
    /// <summary>
    /// 初始化配置，全局唯一
    /// </summary>
    class DCounterConfig
    {
        static DCounterConfig Instance = new DCounterConfig();
        /// <summary>
        /// 外部通信端口
        /// </summary>
        public int OutServerPort { get; set; }
        /// <summary>
        /// 内部通信端口
        /// </summary>
        public int InServerPort { get; set; }

        /// <summary>
        /// 内部通讯机器列表
        /// </summary>
        public List<string> Machines { get; set; }


        private DCounterConfig() { }

        static DCounterConfig()
        {
            Initial();
        }

        private static void Initial()
        {
            ///read config
        }
    }
}
