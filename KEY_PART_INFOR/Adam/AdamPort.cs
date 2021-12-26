using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HfutIe
{
    public class AdamPort
    {
        public enum PortState
        {
            /// <summary>
            /// 关闭
            /// </summary>
            Off,
            /// <summary>
            /// 点亮
            /// </summary>
            On
        }
        private Func<int, bool, bool> action;
        private PortState state = PortState.Off;
        /// <summary>
        /// 端口号
        /// </summary>
        public int PortNo { get; set; }

        /// <summary>
        /// 端口状态
        /// </summary>
        public PortState State
        {
            get { return state; }
        }

        /// <summary>
        /// 设置端口状态
        /// </summary>
        /// <param name="value">端口状态输入值</param>
        /// <returns>true：设置成功；false：设置失败</returns>
        public bool SetState(PortState value)
        {
            bool result;
            if (value == PortState.On)
            {
                result = action(PortNo, true);
            }
            else
            {
                result = action(PortNo, false);
            }
            if(result)
                state = value;
            return result;
        }

        public AdamPort(Func<int, bool, bool> action, int port)
        {
            this.action = action;
            PortNo = port;
        }
    }
}
