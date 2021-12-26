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
using HfutIE.Entity;
using MsgBox;
using System.Threading;
using KEY_PART_INFOR.ClassStore;

namespace HfutIe
{
    class ScannerHelper
    {
        public static string path_SerialPort = Application.StartupPath + @"\Config1\SerialPortConfig.xml";//串口配置文件路径                                                                        
        public static SerialPortConfig seriesport;//串口设置
        public static string pro_line_name = "";
        public static string wc_code = "";
        static readonly object locker1 = new object();
        /// <summary>
        /// 打开串口
        /// </summary>
        /// <param name="ScannerSerialPort">当前程序中扫描枪串口控件名称</param>
        /// <returns></returns>
        public static bool OpenCom(SerialPort ScannerSerialPort)
        {
            try
            {
                //DataTable SCANconfig = ReadConfigfile.ReadScanfile(pro_line_name);//存放串口信息的表
                //DataTable dtcom = SCANconfig.Select("Wc_code='" + wc_code + "'").CopyToDataTable();
                //ScannerSerialPort.PortName = dtcom.Rows[0]["Com_code"].ToString();//为串口赋名 
                ScannerSerialPort.PortName = seriesport.PortName;
                ScannerSerialPort.BaudRate = seriesport.BaudRate;
                ScannerSerialPort.DataBits = seriesport.DataBits;
                ScannerSerialPort.StopBits = (StopBits)Enum.Parse(typeof(StopBits),seriesport.StopBits);
                ScannerSerialPort.Parity = (Parity)Enum.Parse(typeof(Parity),seriesport.Parity);
                ScannerSerialPort.BaudRate = 9600;//无线扫描枪
                //ScannerSerialPort.BaudRate = 115200;//有线扫描枪
                ScannerSerialPort.Open();
                return true;
            }
            catch (Exception e)
            {
                ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                //log.Fatal(wc_code + "工位 扫描枪串口连接失败，请检查硬件连接！" + e);
                log.Fatal("工位扫描枪串口连接失败，请检查硬件连接！" + e);
                //MessageBox.Show(pro_line_name + "线 " + wc_code + " 工位扫描枪串口连接失败，请检查硬件连接！" + e.Message);
                MyMsgBox.Show(wc_code + " 工位扫描枪串口连接失败，请检查硬件连接！", "系统提示", MyMsgBox.MyButtons.OK, MyMsgBox.MyIcon.Error,5);
                return false;
            }
        }

        public static bool CloseCom(SerialPort ScannerSerialPort)
        {
            try
            {
                if (ScannerSerialPort.IsOpen)
                {
                    ScannerSerialPort.Close();
                }
                if (ScannerSerialPort.IsOpen)
                {
                    //MessageBox.Show(pro_line_name + "线 " + wc_code + " 工位扫描枪串口关闭失败！");
                    MessageBox.Show("工位扫描枪串口关闭失败！");
                    return false;
                }
            }
            catch (Exception ex)
            {
                ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                log.Fatal("扫描枪串口关闭失败！" + ex.Message);
                MessageBox.Show("扫描枪串口关闭失败！" + ex.Message);
                return false;
            }
            return true;
        }


        /// <summary>
        /// 获取扫描的条码
        /// </summary>
        /// <param name="ScannerSerialPort">扫描枪的串口控件名</param>
        /// <returns></returns>
        public static object dataReceive(SerialPort ScannerSerialPort, out string BarCodeType, out bool IsOk)//获取扫描的条码
        {
            //int CR = 10;
            //int LF = 13;
            string ReceiveString = "";//读取到的字符串
            string BarCode = "";//扫描得到的条码
            BarCodeType = "";//条码类型
            IsOk = true;
            if (!ScannerSerialPort.IsOpen)
            {
                ScannerSerialPort.Open();
            }
            try
            {               
                while (true)
                {
                    byte[] buffer = new byte[ScannerSerialPort.BytesToRead];
                    ScannerSerialPort.Read(buffer, 0, buffer.Length);
                    for (int i = 0; i < buffer.Length; i++)
                    {
                        ReceiveString += (char)buffer[i];
                    }
                    if (buffer.Length == 0)
                    {
                        break;
                    }
                    Thread.Sleep(100);
                }
                if (ReceiveString != "")
                {
                    BarCode = ReceiveString.Trim('?');
                }
                if (BarCode != "")
                {
                    BarCodeType = DeBarcode.GetBarcodeType(ReceiveString);
                    
                }
                return BarCode;
            }
            catch (Exception ee)
            {
                IsOk = false;
                MessageBox.Show("条码格式有误！" + ee.ToString());
                return BarCode;
            }
        }

        /// <summary>
        /// 将字符串写入串口
        /// </summary>
        /// <param name="str">需要写入串口的字符串</param>
        /// <param name="serialport_name">需要写入串口名称</param>
        /// <returns></returns>
        public static bool WriteToSerialPort(string str, string serialport_name)
        {
            //str = "70609";
            str += "\r\n";
            SerialPort Com = new SerialPort();
            try
            {
                byte[] byteArr = System.Text.Encoding.Default.GetBytes(str);
                Com.ReadTimeout = 5000;
                Com.WriteTimeout = 5000;
                Com.PortName = serialport_name;
                Com.BaudRate = 9600;
                Com.StopBits = StopBits.One;
                Com.Parity = Parity.None;
                if (Com.IsOpen == false)
                {
                    Com.Open();
                }//
                Com.Write(byteArr, 0, byteArr.Length);
                Log.GetInstance.WriteLog("SerialOutPut", "向串口" + serialport_name + "写入数据成功。数据为：" + str);
                //Com.Write(str);//也可以写入字符串
                return true;
            }
            catch (Exception ex)
            {
                Log.GetInstance.WriteLog("SerialOutPut", "向串口" + serialport_name + "写入数据失败。原因是：" + ex.Message);
                return false;
            }
            finally
            {
                Com.Close();
            }
        }
    }
}
