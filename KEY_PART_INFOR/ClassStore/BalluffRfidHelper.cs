using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using System;
using HfutIe;
using System.Collections.Generic;

namespace HFUTIEMES
{
    public abstract class BalluffRfidHelper
    {
        private static readonly object locker = new object();

        /// <summary>
        /// 错误日志
        /// </summary>
        public static string LastErrorInfo { get; private set; }

        /// <summary>
        /// 等待间隔（默认为1秒）
        /// </summary>
        private static int waitInterval = 1000;

        public static string wc_code = "";

        #region 读取数据
        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="startPosition">起始位</param>
        /// <param name="length">读取长度</param>
        /// <param name="serialPort">串口对象</param>
        /// <param name="repeatTimesIfFail">失败尝试次数</param>
        /// <returns></returns>
        public static string ReadRfid(int startPosition, int length, SerialPort serialPort, int repeatTimesIfFail = 3)
        {
            if (startPosition > 9999)
            {
                throw new ArgumentOutOfRangeException("startPosition");
            }
            if (length > 9999)
            {
                throw new ArgumentOutOfRangeException("length");
            }
            if (repeatTimesIfFail < 0)
            {
                throw new ArgumentOutOfRangeException("repeatTimesIfFail");
            }

            string result = "";
            lock (locker)
            {
                LastErrorInfo = "Read\r\n";
                //向RFID传输“读取数据命令”："L" + 四位数起始位置 + 四位数数据长度 + "10" + '\13'
                string readCommand = "L" + startPosition.ToString("0000") + length.ToString("0000") + "10" + (char)13;
                //默认编码：UTF-8
                Encoding encoding = Encoding.UTF8;
                try
                {
                    for (int i = 0; i < repeatTimesIfFail; ++i)
                    {
                        byte[] commandBytes = encoding.GetBytes(readCommand);
                        serialPort.Write(commandBytes, 0, commandBytes.Length);
                        //等待串口回复
                        System.Threading.Thread.Sleep(waitInterval);
                        byte[] commandReplyBytes = new byte[serialPort.BytesToRead];
                        serialPort.Read(commandReplyBytes, 0, commandReplyBytes.Length);//获取串口可读取的数据，数据类型是ASCII码

                        byte[] correctReplyBytes = { 6, 48, 13 };//交互正确
                        if (commandReplyBytes.Length == 3 && commandReplyBytes[0] == 6 && commandReplyBytes[1] == 48 && commandReplyBytes[2] == 13)
                        {
                            string startReadCommand = ((char)2).ToString() + ((char)13).ToString();
                            byte[] startReadCommandBytes = encoding.GetBytes(startReadCommand);
                            serialPort.Write(startReadCommandBytes, 0, startReadCommandBytes.Length);
                            //等待串口回复
                            System.Threading.Thread.Sleep(waitInterval);
                            //开始读取数据
                            byte[] replyDataBytes = new byte[serialPort.BytesToRead];
                            serialPort.Read(replyDataBytes, 0, replyDataBytes.Length);//获取串口可读取的数据，数据类型是ASCII码

                            //对replyDataBytes进行校验
                            if (replyDataBytes.Length >= 1)
                            {
                                if (replyDataBytes[0] != 21)//能够正确读取数据
                                {
                                    result = "";
                                    for (int j = 1; j < replyDataBytes.Length ; j++)
                                    {
                                        if (replyDataBytes[j] == 13)
                                            continue;
                                        result += (char)replyDataBytes[j];
                                    }
                                    return result;//一旦读取数据成功，则返回数据
                                }
                                else//不能正确读取数据，需要抛出异常代码
                                {
                                    for (int j = 1; j < replyDataBytes.Length - 1; j++)
                                    {
                                        if (replyDataBytes[j] == 21) continue;
                                        if (replyDataBytes[j] == 13) continue;
                                        LastErrorInfo += $"{i + 1}：{ (char)replyDataBytes[j]}；";
                                    }
                                }
                            }
                        }
                        else
                        {
                            LastErrorInfo += $"{i + 1}/{repeatTimesIfFail}：读取RFID失败：读取命令被拒绝或无响应；";
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return null;
        }

        public static int ReadRfidInteger(int startPosition, int length, SerialPort serialPort, int repeatTimesIfFail = 3)
        {
            return int.Parse(ReadRfid(startPosition, length, serialPort, repeatTimesIfFail));
        }
        #endregion

        #region 写入数据
        /// <summary>
        /// 写入数据
        /// </summary>
        /// <param name="startPosition">起始位</param>
        /// <param name="length">写入数据长度</param>
        /// <param name="data">数据内容</param>
        /// <param name="serialPort">串口对象</param>
        /// <param name="repeatTimesIfFail">失败重试次数</param>
        /// <returns></returns>
        public static bool WriteRfid(int startPosition, int length, string data, SerialPort serialPort, int repeatTimesIfFail = 3)
        {
            if (startPosition > 9999)
            {
                throw new ArgumentOutOfRangeException("startPosition");
            }
            if (length > 9999)
            {
                throw new ArgumentOutOfRangeException("length");
            }
            if (repeatTimesIfFail < 0)
            {
                throw new ArgumentOutOfRangeException("repeatTimesIfFail");
            }
            //默认编码：UTF-8
            Encoding encoding = Encoding.UTF8;
            if (encoding.GetBytes(data).Length > length)
            {
                throw new ArgumentOutOfRangeException("data以UTF8编码后的长度超出length范围，请重试。");
            }
            lock (locker)
            {
                LastErrorInfo = "Write\r\n";
                bool result = false;
                //向RFID传输“写入数据命令”："P" + 四位数起始位置 + 四位数数据长度 + "11" + '\13'
                string writeCommand = "P" + startPosition.ToString("0000") + length.ToString("0000") + "11" + (char)13;
                try
                {
                    byte[] commandBytes = encoding.GetBytes(writeCommand);
                    //开始读取命令：'\2' +                       '\13'
                    string writeDataCommand = ((char)2).ToString() + data + ((char)13).ToString();
                    byte[] writeDataCommandBytes = encoding.GetBytes(writeDataCommand);
                    for (int i = 0; i < repeatTimesIfFail; ++i)
                    {
                        serialPort.Write(commandBytes, 0, commandBytes.Length);
                        //等待串口回复
                        System.Threading.Thread.Sleep(waitInterval);
                        byte[] commandReplyBytes = new byte[serialPort.BytesToRead];
                        serialPort.Read(commandReplyBytes, 0, commandReplyBytes.Length);//获取串口可读取的数据，数据类型是ASCII码
                        byte[] correctReplyBytes = { 6, 48, 13 };//交互正确
                        Log.GetInstance.WriteLog("外圈;" + i + "次：/n" + string.Join("，", commandReplyBytes));
                        if (commandReplyBytes.Length == 3 && commandReplyBytes[0] == 6 && commandReplyBytes[1] == 48 && commandReplyBytes[2] == 13)
                        {
                            for (; i < repeatTimesIfFail; ++i)
                            {
                                serialPort.Write(writeDataCommandBytes, 0, writeDataCommandBytes.Length);
                                //等待串口回复
                                System.Threading.Thread.Sleep(waitInterval);
                                //开始读取数据
                                byte[] writeSuccessBytes = new byte[serialPort.BytesToRead];
                                serialPort.Read(writeSuccessBytes, 0, writeSuccessBytes.Length);//获取串口可读取的数据，数据类型是ASCII码
                                #region 不同的判断规则 待优化CZQ:20190612
                                bool isOk = writeSuccessBytes.Length == 3 && writeSuccessBytes[0] == 6 && writeSuccessBytes[1] == 48 && writeSuccessBytes[2] == 13;
                                if (length > 720) isOk = writeSuccessBytes.Length >= 3 && writeSuccessBytes[0] == 6 && writeSuccessBytes[1] == 48 && (writeSuccessBytes[2] == 13||writeSuccessBytes[2] == 21);
                                #endregion
                                Log.GetInstance.WriteLog("内圈;"+i+"次：/n"+string.Join("，",writeSuccessBytes));
                                if (isOk)
                                {
                                    return true;
                                }
                                else if (writeSuccessBytes.Length >= 1 && writeSuccessBytes[0] == 21)
                                {
                                    for (int k = 1; k < writeSuccessBytes.Length - 1; k++)
                                    {
                                        if (writeSuccessBytes[k] == 21) continue;
                                        if (writeSuccessBytes[k] == 13) continue;
                                        LastErrorInfo += $"{i + 1}/{repeatTimesIfFail}：{(char)writeSuccessBytes[k] }；\r\n";
                                    }
                                }
                            }
                        }
                        else
                        {
                            LastErrorInfo += $"{i + 1}/{repeatTimesIfFail}：写入RFID失败：写入命令被拒绝或无响应；\r\n";
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return result;
            }
        }
        #endregion

        #region 优化后方法 20200505CZQ

        #region 读byte数组
        /// <summary>
        /// 读byte数组
        /// </summary>
        /// <param name="startPosition">起始位</param>
        /// <param name="length">读取长度</param>
        /// <param name="serialPort">串口对象</param>
        /// <param name="repeatTimesIfFail">失败尝试次数</param>
        /// <returns></returns>
        public static byte[] ReadByteArray(int startPosition, int length, SerialPort serialPort, int repeatTimesIfFail = 3)
        {
            string tagString = ReadRfid(startPosition, length, serialPort, repeatTimesIfFail = 3);
            Encoding encoding = Encoding.UTF8;
            byte[] newByteArray = encoding.GetBytes(tagString);//将字符串转换成byte数组
            return newByteArray;
        }
        #endregion

        #region 写带int值的byte数组
        /// <summary>
        /// 写带int值的byte数组（将byte数组，将其中部分替换成int值）20200505CZQ
        /// </summary>
        /// <param name="startPosition">起始位</param>
        /// <param name="length">字符串长度</param>
        /// <param name="data">字符串</param>
        /// <param name="intDic">整型字典<位置，int值></param>
        /// <param name="serialPort">串口对象</param>
        /// <param name="repeatTimesIfFail">失败重试次数</param>
        /// <returns></returns>
        public static bool WriteRfidIntByte(int startPosition, int length, byte[] data, Dictionary<int, int> intDic, SerialPort serialPort, int repeatTimesIfFail = 3)
        {
            #region 在字符串数组中插入int值
            if (intDic != null && intDic.Count > 0)
            {//若该参数不为空，则数组进行处理
                foreach (var dic in intDic)
                {
                    if (dic.Key >= 0 && dic.Key < data.Length)
                    {//若在字符范围内，将对应位置byte更改成对应的int值
                        data[dic.Key] = (byte)dic.Value;
                    }
                    else
                        LastErrorInfo += $"\n方法WriteRfidIntString中参数intDic对象{dic}的主键超出字符串长度范围";
                }
            }
            #endregion
            return WriteRfidByteArray(startPosition, length, data, serialPort, repeatTimesIfFail);
        }
        #endregion

        #region 写入Byte数组
        /// <summary>
        /// 写入Byte数组信息 
        /// </summary>
        /// <author>chuzhuqiu</author>
        /// <param name="startPosition">起始位</param>
        /// <param name="length">长度</param>
        /// <param name="data">byte数组</param>
        /// <param name="serialPort">串口对象</param>
        /// <param name="repeatTimesIfFail">失败重试次数</param>
        /// <returns></returns>
        public static bool WriteRfidByteArray(int startPosition, int length, byte[] data, SerialPort serialPort, int repeatTimesIfFail = 3)
        {
            if (startPosition > 9999)
            {
                throw new ArgumentOutOfRangeException("startPosition");
            }
            if (length > 9999)
            {
                throw new ArgumentOutOfRangeException("length");
            }
            if (repeatTimesIfFail < 0)
            {
                throw new ArgumentOutOfRangeException("repeatTimesIfFail");
            }
            //默认编码：UTF-8
            Encoding encoding = Encoding.UTF8;
            if (data.Length > length)
            {
                throw new ArgumentOutOfRangeException("data以UTF8编码后的长度超出length范围，请重试。");
            }
            lock (locker)
            {
                LastErrorInfo = "Write\r\n";
                bool result = false;
                //向RFID传输“写入数据命令”："P" + 四位数起始位置 + 四位数数据长度 + "11" + '\13'
                string writeCommand = "P" + startPosition.ToString("0000") + length.ToString("0000") + "11" + (char)13;
                try
                {
                    byte[] commandBytes = encoding.GetBytes(writeCommand);
                    //开始读取命令：'\2' +                       '\13'
                    //string writeDataCommand = ((char)2).ToString() + data + ((char)13).ToString();
                    #region 准备写入的byte信息 20200505修改CZQ
                    List<byte> bylist = new List<byte>();
                    bylist.Add((byte)2);
                    bylist.AddRange(data);
                    bylist.Add((byte)13);
                    byte[] writeDataCommandBytes = bylist.ToArray();
                    #endregion
                    for (int i = 0; i < repeatTimesIfFail; ++i)
                    {
                        serialPort.Write(commandBytes, 0, commandBytes.Length);
                        //等待串口回复
                        System.Threading.Thread.Sleep(waitInterval);
                        byte[] commandReplyBytes = new byte[serialPort.BytesToRead];
                        serialPort.Read(commandReplyBytes, 0, commandReplyBytes.Length);//获取串口可读取的数据，数据类型是ASCII码
                        byte[] correctReplyBytes = { 6, 48, 13 };//交互正确
                        Log.GetInstance.WriteLog("外圈;" + i + "次：/n" + string.Join("，", commandReplyBytes));
                        if (commandReplyBytes.Length == 3 && commandReplyBytes[0] == 6 && commandReplyBytes[1] == 48 && commandReplyBytes[2] == 13)
                        {
                            for (; i < repeatTimesIfFail; ++i)
                            {
                                serialPort.Write(writeDataCommandBytes, 0, writeDataCommandBytes.Length);
                                //等待串口回复
                                System.Threading.Thread.Sleep(waitInterval);
                                //开始读取数据
                                byte[] writeSuccessBytes = new byte[serialPort.BytesToRead];
                                serialPort.Read(writeSuccessBytes, 0, writeSuccessBytes.Length);//获取串口可读取的数据，数据类型是ASCII码
                                #region 不同的判断规则 待优化CZQ:20190612
                                bool isOk = writeSuccessBytes.Length == 3 && writeSuccessBytes[0] == 6 && writeSuccessBytes[1] == 48 && writeSuccessBytes[2] == 13;
                                if (length > 720) isOk = writeSuccessBytes.Length >= 3 && writeSuccessBytes[0] == 6 && writeSuccessBytes[1] == 48 && (writeSuccessBytes[2] == 13 || writeSuccessBytes[2] == 21);
                                #endregion
                                Log.GetInstance.WriteLog("内圈;" + i + "次：/n" + string.Join("，", writeSuccessBytes));
                                if (isOk)
                                {
                                    return true;
                                }
                                else if (writeSuccessBytes.Length >= 1 && writeSuccessBytes[0] == 21)
                                {
                                    for (int k = 1; k < writeSuccessBytes.Length - 1; k++)
                                    {
                                        if (writeSuccessBytes[k] == 21) continue;
                                        if (writeSuccessBytes[k] == 13) continue;
                                        LastErrorInfo += $"{i + 1}/{repeatTimesIfFail}：{(char)writeSuccessBytes[k] }；\r\n";
                                    }
                                }
                            }
                        }
                        else
                        {
                            LastErrorInfo += $"{i + 1}/{repeatTimesIfFail}：写入RFID失败：写入命令被拒绝或无响应；\r\n";
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return result;
            }
        }
            #endregion

        #region 写带int值的字符串
            /// <summary>
            /// 写带int值的字符串（将字符转换成ASCLL值的byte数组，将其中部分替换成int值）20200505CZQ
            /// </summary>
            /// <param name="startPosition">起始位</param>
            /// <param name="length">字符串长度</param>
            /// <param name="data">字符串</param>
            /// <param name="intDic">整型字典<位置，int值></param>
            /// <param name="serialPort">串口对象</param>
            /// <param name="repeatTimesIfFail">失败重试次数</param>
            /// <returns></returns>
        public static bool WriteRfidIntString(int startPosition, int length, string data, Dictionary<int, int> intDic, SerialPort serialPort, int repeatTimesIfFail = 3)
        {
            Encoding encoding = Encoding.UTF8;
            byte[] oldByteArray = encoding.GetBytes(data);//将字符串转换成byte数组
            #region 在字符串数组中插入int值
            if (intDic != null && intDic.Count > 0)
            {//若该参数不为空，则数组进行处理
                foreach (var dic in intDic)
                {
                    if (dic.Key >= 0 && dic.Key < data.Length)
                    {//若在字符范围内，将对应位置byte更改成对应的int值
                        oldByteArray[dic.Key] = (byte)dic.Value;
                    }
                    else
                        LastErrorInfo += $"\n方法WriteRfidIntString中参数intDic对象{dic}的主键超出字符串长度范围";
                }
            }
            #endregion
            return WriteRfidByteArray(startPosition, length, oldByteArray, serialPort, repeatTimesIfFail);
        }
        #endregion

        #region 写入字符串
        /// <summary>
        /// 写入字符串（将字符转换成ASCLL值的byte数组）20200505CZQ
        /// </summary>
        /// <param name="startPosition">起始位</param>
        /// <param name="length">字符串长度</param>
        /// <param name="data">字符串</param>
        /// <param name="serialPort">串口对象</param>
        /// <param name="repeatTimesIfFail">失败重试次数</param>
        /// <returns></returns>
        public static bool WriteRfidString(int startPosition, int length, string data, SerialPort serialPort, int repeatTimesIfFail = 3)
        {
            Encoding encoding = Encoding.UTF8;
            return WriteRfidByteArray(startPosition, length, encoding.GetBytes(data), serialPort, repeatTimesIfFail);
        }
        #endregion

        #region 写入整数型数值
        /// <summary>
        /// 写入整数型数值（原值，非ASCALL值）20200505CZQ
        /// </summary>
        /// <param name="startPosition"></param>
        /// <param name="data"></param>
        /// <param name="serialPort"></param>
        /// <param name="repeatTimesIfFail"></param>
        /// <returns></returns>
        public static bool WriteRfidInteger(int startPosition, int data, SerialPort serialPort, int repeatTimesIfFail = 3)
        {
            return WriteRfidByteArray(startPosition, 1, new byte[] { (byte)data }, serialPort, repeatTimesIfFail);
        }
        #endregion

        #region 解析数字
        public int stringToInt(char charValue)
        {
            return charValue;
        }
        #endregion

        #endregion
        /// <summary>
        /// 打开串口
        /// </summary>
        /// <param name="ScannerSerialPort">当前程序中扫描枪串口控件名称</param>
        /// <returns></returns>
        public static bool OpenRfidCom(SerialPort ScannerSerialPort)
        {
            try
            {
                if (!ScannerSerialPort.IsOpen)
                {
                    DataTable Rfidconfig = ReadConfigfile.ReadScanfile_RFID();//存放串口信息的表
                    //string Timeoutset = Rfidconfig.Rows[0]["Timeoutset"].ToString().Trim();
                    DataTable dtcom = Rfidconfig.Select("Wc_code='" + wc_code + "'").CopyToDataTable();
                    string PortName = dtcom.Rows[0]["Com_code"].ToString().Trim();
                    //string baudRate = Rfidconfig.Rows[0]["BaudRate"].ToString().Trim();
                    string baudRate = "9600";
                    ScannerSerialPort.PortName = PortName;
                    ScannerSerialPort.BaudRate = int.Parse(baudRate.ToString().Trim());
                    ScannerSerialPort.Parity = Parity.None;
                    ScannerSerialPort.Open();
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        /// <summary>
        /// 关闭RfidCom口
        /// </summary>
        public static bool CloseRfidCom(SerialPort ScannerSerialPort)
        {
            try
            {
                if (ScannerSerialPort.IsOpen)
                {
                    ScannerSerialPort.Close();
                }
                if (ScannerSerialPort.IsOpen)
                {
                    return false;
                }
            }
            catch (Exception ex)
            {

                return false;
            }
            return true;
        }
    }
}
