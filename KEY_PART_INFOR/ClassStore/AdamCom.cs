#region 程序集 Advantech.Adam, Version=8.2.12.0, Culture=neutral, PublicKeyToken=c24039c75946be9c
// E:\#1#纳威司达项目\1#项目程序\质量门\JAC_fAxle\bin\Debug\Advantech.Adam.dll
#endregion

using Advantech.Common;

namespace Advantech.Adam
{
    public class AdamCom : ComPort
    {
        public AdamCom(int i_i32Port);

        public bool Checksum { get; set; }
        public ErrorCode LastError { get; }

        public Adam2000 Adam2000(int i_iID);
        public bool AdamTransaction(string i_szSend, out string o_szRecv);
        public Alarm Alarm(int i_iAddr);
        public AnalogInput AnalogInput(int i_iAddr);
        public AnalogOutput AnalogOutput(int i_iAddr);
        public Configuration Configuration(int i_iAddr);
        public Counter Counter(int i_iAddr);
        public DigitalInput DigitalInput(int i_iAddr);
        public DigitalOutput DigitalOutput(int i_iAddr);
        public bool GetDspFwVersion(int i_iAddr, ref string o_szDspFwVer);
        public bool GetPlatformFwVersion(int i_iAddr, ref string o_szFwVer);
        public Modbus Modbus(int i_iAddr);
        public PID Pid(int i_iAddr);
        protected void AppendChecksum(ref string io_szData);
        protected uint ChecksumNumber(string i_szData);
        protected bool VfyTrimChecksum(ref string io_szData);
    }
}