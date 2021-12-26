using HfutIe;
using HfutIe.offline;
using KEY_PART_INFOR;
using KEY_PART_INFOR.ClassStore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HfutIe
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool createNew;
            try
            {
                using (System.Threading.Mutex m = new System.Threading.Mutex(true, "Global\\" + Application.ProductName, out createNew))
                {
                    if (createNew)
                    {
                        //data.data.ConnStr = "data source=192.168.1.22/ORCL;database=JACMES;user id=JACMES;password=sa123";
                        //data.data.dataSource = "192.168.1.22";
                        XmlHelper.getSeriesPortConfig(ScannerHelper.path_SerialPort);
                        Application.EnableVisualStyles();
                        Application.SetCompatibleTextRenderingDefault(false);
                        //Application.Run(new LoginForm(ReadXML.GetWCCode(System.Windows.Forms.Application.StartupPath + @"\WCConfig\WC.xml")));
                        Application.Run(new LoginForm());
                        //Application.Run(new JAC_KEYPART_HCL());
                        Application.Run(new JAC_QPT_Hcl());
                        //string right = SystemLog.InOrOut;
                        //if (right == "3")
                        //{
                        //    Application.Run(new JAC_KEYPART());
                        //}
                        //else if (right == "4")
                        //{
                        //    Application.Run(new JAC_REPAIR());
                        //}
                        //else if (right == "5")
                        //{
                        //    Application.Run(new QualityGate());
                        //}
                        //else if (right == "7")
                        //{
                        //    Application.Run(new JAC_KEYPART_YZ());
                        //}
                        //else if (right == "8")
                        //{
                        //    Application.Run(new JAC_REPAIR_JJ());
                        //}
                        //else
                        //{
                        //    return;
                        //}
                    }
                    else
                    {
                        MessageBox.Show("只能运行一个程序实例!");
                    }
                }
            }
            catch (Exception ex)
            {
                ExLogHelper.Instance.WriteLog(ex, OperationType.Login);
                //throw ex;
                //MessageBox.Show("只能运行一个程序实例!");
            }
        }
    }
}
