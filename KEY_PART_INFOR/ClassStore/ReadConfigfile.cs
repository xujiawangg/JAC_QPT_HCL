using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.Collections;
using System.Xml;
using System.Xml.Linq;
using log4net;
using System.IO.Ports;

namespace HfutIe
{
    class ReadConfigfile
    {
        /// <summary>
        /// 读Scan配置文件
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public static DataTable ReadScanfile( string assembly)
        {
            string configname="";
            switch (assembly) 
            { 
                case "轻卡线":
                    configname = "QK_Scan.xml";
                    break;
                case "重卡线":
                    configname = "ZK_Scan.xml";
                    break;
                default:
                    configname = "Scan.xml";
                    break;                   
            }
            string datasorucePath = Application.StartupPath + @"\Config\"+configname;
            DataTable dt = new DataTable();
            dt.Columns.Add("Pro_line_name");
            dt.Columns.Add("Wc_code");
            dt.Columns.Add("Com_code");           
            try
            {
                if (System.IO.File.Exists(datasorucePath))
                {
                    XmlDocument Doc = new XmlDocument();
                    XDocument xmlDoc = XDocument.Load(datasorucePath);
                    Doc.Load(datasorucePath);
                    int i = 0;
                    foreach (XElement element in xmlDoc.Element("Scan_config").Elements())
                    {
                        DataRow dr = dt.NewRow();
                        dr["Pro_line_name"] = Doc.GetElementsByTagName("Pro_line_name")[i].InnerText;
                        dr["Wc_code"] = Doc.GetElementsByTagName("Wc_code")[i].InnerText;
                        dr["Com_code"] = Doc.GetElementsByTagName("Com_code")[i].InnerText;
                        dt.Rows.Add(dr);
                        i++;
                    }
                }
                else
                {
                    MessageBox.Show("配置文件丢失！\n本地监控终端参数配置文件丢失，请重新配置！", "配置文件丢失", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                log.Fatal("读取扫描枪配置参数出错，配置文件格式已损坏！", ex);
                MessageBox.Show(ex.Message);

            }
            return dt;
        }
        /// <summary>
        /// 读Scan配置文件
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public static DataTable ReadScanfile_RFID()
        {
            string configname = "Rfid_Scan.xml";
            string datasorucePath = Application.StartupPath + @"\Config\" + configname;
            DataTable dt = new DataTable();
            dt.Columns.Add("Pro_line_name");
            dt.Columns.Add("Wc_code");
            dt.Columns.Add("Com_code");
            try
            {
                if (System.IO.File.Exists(datasorucePath))
                {
                    XmlDocument Doc = new XmlDocument();
                    XDocument xmlDoc = XDocument.Load(datasorucePath);
                    Doc.Load(datasorucePath);
                    int i = 0;
                    foreach (XElement element in xmlDoc.Element("Scan_config").Elements())
                    {
                        DataRow dr = dt.NewRow();
                        dr["Pro_line_name"] = Doc.GetElementsByTagName("Pro_line_name")[i].InnerText;
                        dr["Wc_code"] = Doc.GetElementsByTagName("Wc_code")[i].InnerText;
                        dr["Com_code"] = Doc.GetElementsByTagName("Com_code")[i].InnerText;
                        dt.Rows.Add(dr);
                        i++;
                    }
                }
                else
                {
                    MessageBox.Show("配置文件丢失！\n本地监控终端参数配置文件丢失，请重新配置！", "配置文件丢失", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                log.Fatal("读取扫描枪配置参数出错，配置文件格式已损坏！", ex);
                MessageBox.Show(ex.Message);

            }
            return dt;
        }
    }
}
