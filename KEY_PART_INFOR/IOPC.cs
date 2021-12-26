using Opc.Da;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HfutIe
{
    public interface IOPC
    {
        /// <summary>
        /// DataChange函数，当OnDataChange时间出发的时候，会调用该函数。
        /// </summary>
        /// <param name="item"></param>
        void DataChange(ItemValueResult item);
    }
}
