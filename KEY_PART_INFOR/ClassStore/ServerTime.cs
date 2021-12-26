using HfutIe;
using HfutIE.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HfutIe
{
    /// <summary>
    /// ClassName：ServerTime
    /// Describe：服务器时间（秒级误差）
    /// Author： LY
    /// CreateTime：2019年1月8日10:14:36
    /// LastModify：（无）
    /// LastModifyTime：（无）
    /// Copyright(C) :LY
    /// </summary>
    public class ServerTime
    {
        /// <summary>
        /// 计时器
        /// </summary>
        private static Stopwatch stopWatch;
        /// <summary>
        /// 服务器时间
        /// </summary>
        private static DateTime serverTime;
        /// <summary>
        /// 获取服务器时间的时刻，即在何时获取的服务器时间，此时间以计时器的耗时为依据
        /// </summary>
        private static double getServerTimeTime;

        /// <summary>
        /// 静态构造函数，初始化计时器，获取服务器时间，并计算“获取服务器时间”的时刻
        /// </summary>
        static ServerTime()
        {
            stopWatch = new Stopwatch();//计时器初始化
            stopWatch.Start();//计时器开始计时
            try
            {
                SetInitTime();//尝试获取服务器时间。
            }
            catch
            {
                //如果获取服务器时间失败
                Task.Run(new Action(TryGetServerTime));//另开线程，每5分钟尝试获取服务器时间
                serverTime = DateTime.Now;//临时将本机置为服务器时间，以供模块使用
                getServerTimeTime = 0;//获取时间设为0
            }
        }

        /// <summary>
        /// 获取服务器当前时间
        /// </summary>
        public static DateTime Now
        {
            get
            {
                return serverTime.AddMilliseconds(stopWatch.ElapsedMilliseconds - getServerTimeTime);//从服务器时间加上：“当前时刻”减去“获取服务器时间的时刻”之间的耗时，结果即为服务器的当前时间
            }
        }
        /// <summary>
        /// 获取服务器时间，并设置
        /// </summary>
        private static void SetInitTime()
        {
            long startGetServerTime = stopWatch.ElapsedMilliseconds;//“获取服务器时间”的开始时刻
            string UnHandleTime = GetServerTime();//获取服务器时间（原始格式）
            string rawServerTime = UnHandleTime.Substring(1, UnHandleTime.Length - 2);//处理从服务器获取的时间格式
            //string rawServerTime = DateTime.Now.ToString();
            long endGetServerTime = stopWatch.ElapsedMilliseconds;//“获取服务器时间”的结束时刻
            serverTime = DateTime.Parse(rawServerTime);//时间格式转化
            //long parseTime = stopWatch.ElapsedMilliseconds - endGetServerTime;//格式转换耗时
            getServerTimeTime = startGetServerTime + (endGetServerTime - startGetServerTime) / 2.0;//“获取服务器时间”的时刻，计算公式为：开始时刻+（结束时刻-开始时刻）/ 2。此公式基于假设：双向网络（客户端=>服务器；服务器=>客户端）传输耗时相等。
        }
        /// <summary>
        /// 在第一次获取时间失败的情况下，每5分钟尝试获取一次服务器时间
        /// </summary>
        private static void TryGetServerTime()
        {
            while (true)
            {
                try
                {
                    SetInitTime();//尝试获取服务器时间。
                    break;//如果获取到服务器时间，跳出
                }
                catch
                {
                    Thread.Sleep(5 * 60 * 1000);//每五分钟做一次同步，尝试获取最新的服务器时间
                }
            }
        }
        /// <summary>
        /// 此方法可去掉，改用WebApi从服务器获取时间即可，此处为替代WebApi方法的方法
        /// WebAPI服务端方法大体如下：
        public static string GetServerTime()
        {
            string a = "http://" + ServerDictionary.IPCode + "/API/WebApiTime/GetLocalTime";
            return WebApiHttp.HttpGet(a);//通过WebApi获取服务器时间
            //return WebApiHttp.HttpGet("http://192.168.1.22:1111/API/WebApiTime/GetLocalTime");//通过WebApi获取服务器时间

        }
        /// </summary>
        /// <returns></returns>
        //private static string GetServerTime()
        //{
        //    Random rd = new Random();//生成随机数
        //    return new DateTime(2019, 1, 8, rd.Next(0, 24), rd.Next(0, 60), rd.Next(0, 60)).ToString();
        //}
    }
}