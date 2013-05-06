using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.IO;


namespace DCounterCommunication
{
    /// <summary>
    /// 初始化配置，全局唯一
    /// </summary>
    class DCounterConfig
    {
        public static DCounterConfig Instance = new DCounterConfig();
        /// <summary>
        /// 外部通信端口
        /// </summary>
        public int OutServerPort { get { return _outServerPort; } }

        private static int _outServerPort;

        /// <summary>
        /// 内部通讯机器列表
        /// </summary>
        public List<NodeInfo> Machines { get { return _machines; } }

        private static List<NodeInfo> _machines;


        private DCounterConfig() { }

        static DCounterConfig()
        {

            Initial();
        }

        private static void Initial()
        {
            string path = "./DCounter.config";
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            using (StreamReader sr = new StreamReader(fs))
            {
                XDocument xdoc = XDocument.Load(sr);
                var temp = xdoc.Element("configuration").Element("dcounter");
                _outServerPort = int.Parse(temp.Element("serverPort").Value);
                int innerPort = int.Parse(temp.Element("machines").Attribute("port").Value);
                var machines = temp.Element("machines").Elements("name");
                _machines = new List<NodeInfo>();
                string localIP = "";
                foreach (var item in machines)
                {
                    _machines.Add(new NodeInfo { IP = item.Value, Port = innerPort, Status = false, IsPrimary = false, IsLocalMachine = item.Value == localIP });
                }

            }

            ///read config
        }
    }
}
