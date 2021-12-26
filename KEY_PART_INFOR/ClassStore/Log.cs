using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HfutIe
{
    /// <summary>
    /// 记录Windows Service执行过程的日志
    /// create by LY
    /// 2017-7-6 18:03:47
    /// </summary>
    class Log
    {
        private static Log log;
        public static Log GetInstance
        {
            get
            {
                if (log == null)
                {
                    log = new Log();
                }
                return log;
            }
        }
        /// <summary>
        /// 本地日志记录
        /// </summary>
        /// <param name="message">日志记录信息</param>
        public void WriteLog(string message)
        {
            FileStream fileStream = null;
            StreamWriter streamWriter = null;
            try
            {
                //string path = Path.Combine("C:\\Users\\LY\\Desktop\\", string.Format("{0:yyyyMMdd}", DateTime.Now), ".txt");//每天一条记录
                string ooo = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
                string path = Path.Combine(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "Log.txt");//每天一条记录，但存储在一个文件内
                using (fileStream = new FileStream(path, FileMode.Append, FileAccess.Write))
                {
                    using (streamWriter = new StreamWriter(fileStream))
                    {
                        string str = DateTime.Now + "：" + message;
                        streamWriter.WriteLine(str);
                        if (streamWriter != null)
                        {
                            streamWriter.Close();
                        }
                    }
                    if (fileStream != null)
                    {
                        fileStream.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                log.Fatal("写入日志错误！" + ex.Message);
                WriteLog("写入日志错误！" + ex.Message);
            }
        }
        /// <summary>
        /// 本地日志记录
        /// </summary>
        /// <param name="message">日志记录信息</param>
        public void WriteLog(string file_name, string message)
        {
            FileStream fileStream = null;
            StreamWriter streamWriter = null;
            try
            {
                string ooo = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
                string path = Path.Combine(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase, file_name + ".log");//每天一条记录，但存储在一个文件内
                using (fileStream = new FileStream(path, FileMode.Append, FileAccess.Write))
                {
                    using (streamWriter = new StreamWriter(fileStream))
                    {
                        string str = DateTime.Now + "：" + message;
                        streamWriter.WriteLine(str);
                        if (streamWriter != null)
                        {
                            streamWriter.Close();
                        }
                    }
                    if (fileStream != null)
                    {
                        fileStream.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                log.Fatal("写入日志错误！" + ex.Message);
                WriteLog("写入日志错误！" + ex.Message);
            }
        }
    }
}
