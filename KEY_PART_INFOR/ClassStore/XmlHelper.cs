using HfutIe;
using log4net;
using MsgBox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace KEY_PART_INFOR.ClassStore
{
    public static class XmlHelper
    {
        /// <summary>
        /// 获取本地串口配置信息
        /// </summary>
        /// <param name="xmlpath">配置文件路径</param>
        /// <returns></returns>
        public static void getSeriesPortConfig(string xmlpath)
        {
            try
            {
                XmlDocument xml_doc = new XmlDocument();
                XmlReaderSettings settings = new XmlReaderSettings();
                settings.IgnoreComments = true;//忽略文档里面的注释
                XmlReader reader = XmlReader.Create(xmlpath, settings);
                xml_doc.Load(reader);
                //读取串口配置信息
                SerialPortConfig series_port = new SerialPortConfig();
                XmlNode root_node = xml_doc.SelectSingleNode("data");
                XmlNodeList list_nodes = root_node.ChildNodes;
                foreach (XmlNode _nodes in list_nodes)
                {
                    switch (_nodes.Name)
                    {
                        case "PortName":
                            series_port.PortName = _nodes.InnerText;
                            break;
                        case "BaudRate":
                            series_port.BaudRate = int.Parse(_nodes.InnerText);
                            break;
                        case "DataBits":
                            series_port.DataBits = int.Parse(_nodes.InnerText);
                            break;
                        case "StopBits":
                            series_port.StopBits = _nodes.InnerText;
                            break;
                        case "Parity":
                            series_port.Parity = _nodes.InnerText;
                            break;
                    }
                }
                ScannerHelper.seriesport = series_port;
                reader.Close();
            }
            catch (Exception e)
            {
                //LocalTools.WriteErrorLog(ex, SystemInfo.path_ErrorLog);
                //Alert.ShowAlertControlErr(new Form(), "系统提示", "获取SeriesPortConfig配置文件失败：" + ex.Message, 2000);
                //throw;
                ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                //log.Fatal(wc_code + "工位 扫描枪串口连接失败，请检查硬件连接！" + e);
                log.Fatal("工位扫描枪串口连接失败，请检查硬件连接！" + e);
                //MessageBox.Show(pro_line_name + "线 " + wc_code + " 工位扫描枪串口连接失败，请检查硬件连接！" + e.Message);
                MyMsgBox.Show(" 工位扫描枪串口连接失败，请检查硬件连接！", "系统提示", MyMsgBox.MyButtons.OK, MyMsgBox.MyIcon.Error, 5);
            }
        }
    }
}
