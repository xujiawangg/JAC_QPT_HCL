using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KEY_PART_INFOR.ClassStore
{
    class SerialPortConfig
    {
        /// <summary>
        /// 端口号：COM1
        /// </summary>
        public string PortName { get; set; }
        /// <summary>
        /// 波特率：300,600,1200,2400,4800,9600,19200,38400,43000,56000,57600,115200
        /// </summary>
        public int BaudRate { get; set; }
        /// <summary>
        /// 数据位：6,7,8
        /// </summary>
        public int DataBits { get; set; }
        /// <summary>
        /// 停止位：One,Two,OnePointFive
        /// </summary>
        public string StopBits { get; set; }
        /// <summary>
        /// 校验位：None,Odd,Even,Mark,Space
        /// </summary>
        public string Parity { get; set; }
    }
}
