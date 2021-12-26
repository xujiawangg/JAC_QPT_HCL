using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace HfutIe
{
    class ReadXML
    {
        private static string Com_code=null;
        public static string COM_code
        {
            get
            {
                if (Com_code == null)
                {
                    Com_code = GetOutPutCOMCodeByXML(System.Windows.Forms.Application.StartupPath + @"\OutPutConfig\OutPut.xml");
                }
                return Com_code;
            }
        }
        public static string GetOutPutCOMCodeByXML(string datasorucePath)
        {
            XmlDocument Doc = new XmlDocument();
            XDocument xmlDoc = XDocument.Load(datasorucePath);
            Doc.Load(datasorucePath);
            string COM_code = Doc.GetElementsByTagName("Com_code")[0].InnerText;
            return COM_code;
        }
        public static string GetWCCode(string datasorucePath)
        {
            XmlDocument Doc = new XmlDocument();
            XDocument xmlDoc = XDocument.Load(datasorucePath);
            Doc.Load(datasorucePath);
            string COM_code = Doc.GetElementsByTagName("wc_code")[0].InnerText;
            return COM_code;
        }
        public static string GetHCLCode(string datasorucePath)
        {
            XmlDocument Doc = new XmlDocument();
            XDocument xmlDoc = XDocument.Load(datasorucePath);
            Doc.Load(datasorucePath);
            string COM_code = Doc.GetElementsByTagName("HCL_code")[0].InnerText;
            return COM_code;
        }
        public static string GetTime(string datasorucePath)
        {
            XmlDocument Doc = new XmlDocument();
            XDocument xmlDoc = XDocument.Load(datasorucePath);
            Doc.Load(datasorucePath);
            string handleTime = Doc.GetElementsByTagName("handleTime")[0].InnerText;
            return handleTime;
        }
        public static string GetWCCode()
        {
            return ConfigurationManager.AppSettings["wc_code"];
        }
        public static string GetPrintName(string datasorucePath)
        {
            XmlDocument Doc = new XmlDocument();
            XDocument xmlDoc = XDocument.Load(datasorucePath);
            Doc.Load(datasorucePath);
            string PrintName = Doc.GetElementsByTagName("PrintName")[0].InnerText;
            return PrintName;
        }
    }
}
