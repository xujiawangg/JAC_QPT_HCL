using System;
using System.Collections.Generic;
using System.Text;
using Advantech.Adam;
using System.Xml;

namespace HfutIe
{
    public class MyAdam4055Util : Adam4055Util
    {
        private static string adamConfigFilePath = System.Windows.Forms.Application.StartupPath +  @"\Config\AdamConfig.xml";
        private static int comPort = 1;
        private static int redPort = 1;
        private static int greenPort = 3;
        private static int yellowPort = 4;
        private static int buzzerPort = 2;

        /// <summary>
        /// ADAM4055Util的构造函数
        /// </summary>
        /// <param name="COM">报警灯的COM口</param>
        /// <param name="redPort">红色警示灯端口</param>
        /// <param name="greenPort">绿色警示灯端口</param>
        /// <param name="yellowPort">黄色警示灯端口</param>
        /// <param name="buzzerPort">轰鸣器端口</param>
        public MyAdam4055Util()
            : base(comPort, redPort, greenPort, yellowPort, buzzerPort)
        { }

        /// <summary>
        /// 与板卡建立连接
        /// 
        /// 
        /// 异常：写配置文件失败
        /// </summary>
        /// <returns></returns>
        public override bool Connect()
        {
            bool connSuccess = false;
            connSuccess = base.Connect();

            if (!connSuccess)
            {
                string[] listPortName = System.IO.Ports.SerialPort.GetPortNames();
                int adamCOMPort = 0;
                //遍历所有COM口，尝试建立板卡之间的通讯
                for (int i = 0; i < listPortName.Length; i++)
                {
                    if (int.TryParse(listPortName[i].ToUpper().Replace("COM", ""), out adamCOMPort))
                    {
                        COM = adamCOMPort;
                        adamCom = new AdamCom(COM);
                        adamCom.Checksum = false; // disbale checksum
                        connSuccess = base.Connect();
                    }
                    if (connSuccess)
                        break;
                }
                //如果与板卡建立通讯成功，就修改配置文件中的板卡对应的COM口
                if (connSuccess)
                {
                    try
                    {
                        XmlDocument xmlDoc = new XmlDocument();
                        xmlDoc.Load(adamConfigFilePath);

                        XmlNode rootNode = xmlDoc.SelectSingleNode("Config");
                        rootNode.SelectSingleNode("COM").InnerText = adamCOMPort.ToString();

                        xmlDoc.Save(adamConfigFilePath);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("修改ADAM配置文件失败！" + "\nError:" + ex.Message);
                    }
                }
            }
            return connSuccess;
        }

        /// <summary>
        /// 获取Adam配置信息
        /// 
        /// 异常：文件不存在；文件受损
        /// </summary>
        /// <param name="adamConfigPath">配置文件路径</param>
        /// <param name="adamCOMPort">COM口号</param>
        /// <param name="adamRedPort">红色灯端口号</param>
        /// <param name="adamGreenPort">绿色灯端口号</param>
        /// <param name="adamYellowPort">黄色灯端口号</param>
        /// <param name="adamBuzzerPort">轰鸣器端口号</param>
        public static void GetAdamConfig()
        {
            if (System.IO.File.Exists(adamConfigFilePath))
            {
                try
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(adamConfigFilePath);

                    XmlNode rootNode = xmlDoc.SelectSingleNode("Config");

                    comPort = int.Parse(rootNode.SelectSingleNode("COM").InnerText);
                    redPort = int.Parse(rootNode.SelectSingleNode("Red").InnerText);
                    greenPort = int.Parse(rootNode.SelectSingleNode("Green").InnerText);
                    yellowPort = int.Parse(rootNode.SelectSingleNode("Yellow").InnerText);
                    buzzerPort = int.Parse(rootNode.SelectSingleNode("Buzzer").InnerText);
                }
                catch (Exception ex)
                {
                    string message = "ADAM配置文件受损:" + adamConfigFilePath;
                    throw new Exception(message + "\nError:" + ex.Message);
                }
            }
            else
            {
                throw new Exception("系统找不到指定目录下的文件： " + adamConfigFilePath);
            }

        }
    }
}
