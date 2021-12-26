using Advantech.Adam;
using Advantech.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace HfutIe
{
    public class Adam4055Util
    {
        private int m_iCom, m_iAddr;//, m_iCount;//, m_iDITotal, m_iDOTotal;
        private bool m_bStart;//是否在连接板卡
        private Adam4000Config m_adamConfig;
        //private Adam4000Type m_Adam4000Type;
        protected AdamCom adamCom;
        private System.Timers.Timer timer;
        /// <summary>
        /// 轰鸣器提示，轰鸣器在true 打开；false 关闭
        /// </summary>
        private bool buzzerFlag;
        private bool isAlarm;
        /// <summary>
        /// 是否根据次数报警
        /// </summary>
        private bool alarmByNum;
        /// <summary>
        /// 报警闪烁次数
        /// </summary>
        private int alarmNum;
        /// <summary>
        /// COM口
        /// </summary>
        public int COM
        {
            get { return m_iCom; }
            set { m_iCom = value; }
        }
        /// <summary>
        /// 报警灯端口
        /// </summary>
        public AdamPort Red, Green, Yellow, Buzzer;



        public bool IsAlarm { get { return isAlarm; } }

        /// <summary>
        /// 初始化ADAM-4055板卡工具类
        /// </summary>
        /// <param name="COM">使用的COM口数字，如使用COM1，则参数为1</param>
        /// <param name="redPort">报警灯 红灯的输入端口</param>
        /// <param name="greenPort">报警灯 绿灯的输入端口</param>
        /// <param name="yellowPort">报警灯 黄灯的输入端口</param>
        /// <param name="buzzerPort">报警灯 轰鸣器的输入端口</param>
        public Adam4055Util(int COM, int redPort, int greenPort, int yellowPort, int buzzerPort)
        {
            m_iCom = COM;     //使用COMx（x：1，2，3……）
            m_iAddr = 1;    // 使用地址为1的板卡（同一COM口可能连接多个板卡，地址依次为1，2，3……）
            //m_iCount = 0;   // the counting start from 0
            m_bStart = false;

            Red = new AdamPort(setDOValue, redPort);
            Green = new AdamPort(setDOValue, greenPort);
            Yellow = new AdamPort(setDOValue, yellowPort);
            Buzzer = new AdamPort(setDOValue, buzzerPort);

            buzzerFlag = true;
            isAlarm = false;
            alarmByNum = true;
            timer = new System.Timers.Timer();
            timer.Elapsed += Timer_Tick;
            timer.Interval = 800;

            //m_Adam4000Type = Adam4000Type.Adam4055;
            //m_iDITotal = DigitalInput.GetChannelTotal(m_Adam4000Type);
            //m_iDOTotal = DigitalOutput.GetChannelTotal(m_Adam4000Type);

            //InitAdam4055();
            adamCom = new AdamCom(m_iCom);
            adamCom.Checksum = false; // disbale checksum
        }

        /// <summary>
        /// 与板卡建立连接
        /// </summary>
        /// <returns>连接状态</returns>
        public virtual bool Connect()
        {
            lock (this)
            {
                if (!m_bStart)
                {
                    if (adamCom.OpenComPort())
                    {
                        // 设置COM口的状态, 9600,N,8,1
                        adamCom.SetComPortState(Baudrate.Baud_9600, Databits.Eight, Parity.None, Stopbits.One);
                        // set COM port timeout
                        adamCom.SetComPortTimeout(500, 1000, 0, 1000, 0);
                        //m_iCount = 0; // reset the reading counter
                                      // get module config
                        if (adamCom.Configuration(m_iAddr).GetModuleConfig(out m_adamConfig))
                        {
                            m_bStart = true;
                        }
                        else
                        {
                            if (adamCom.IsOpen)
                                adamCom.CloseComPort();
                            m_bStart = false;
                        }
                    }
                    else
                    {
                        m_bStart = false;
                    }
                }
                return m_bStart;
            }
        }

        /// <summary>
        /// 与板卡断开连接
        /// </summary>
        public virtual void Disconnect()
        {
            if (!m_bStart)
                return;
            else
            {
                adamCom.CloseComPort();
                m_bStart = false;
            }
        }

        /// <summary>
        /// 设置DO端口号的灯 亮/灭
        /// </summary>
        /// <param name="DONo">DO的端口号</param>
        /// <param name="DOvalue">true：灯亮 false：等灭</param>
        /// <returns></returns>
        private bool setDOValue(int DONo, bool DOvalue)
        {
            if (!m_bStart)
                if (!Connect())
                {
                    return false;
                }
            if (m_bStart)
                return adamCom.DigitalOutput(m_iAddr).SetValue(DONo, DOvalue);
            else
                return false;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (buzzerFlag)//打开灯、轰鸣器
            {
                Buzzer.SetState(AdamPort.PortState.On);
                Red.SetState(AdamPort.PortState.On);
            }
            else//关闭灯、轰鸣器
            {
                Buzzer.SetState(AdamPort.PortState.Off);
                Red.SetState(AdamPort.PortState.Off);
                if (alarmByNum)
                {
                    alarmNum--;
                    if (alarmNum <= 0)
                    {
                        timer.Stop();
                        isAlarm = false;
                    }
                }
            }
            buzzerFlag = !buzzerFlag;
        }

        /// <summary>
        /// 开始报警
        /// </summary>
        public virtual void AlarmStart()
        {
            alarmByNum = false;
            AlarmOn();
        }

        /// <summary>
        /// 关闭报警
        /// </summary>
        public virtual void AlarmEnd()
        {
            AlarmOff();
        }

        /// <summary>
        /// 根据给定次数闪烁报警
        /// </summary>
        /// <param name="count">闪烁次数</param>
        public virtual void AlarmByNum(int count)
        {
            alarmNum = count;
            alarmByNum = true;
            AlarmOn();

        }

        private void AlarmOn()
        {
            buzzerFlag = true;
            isAlarm = true;

            Red.SetState(AdamPort.PortState.On);
            Buzzer.SetState(AdamPort.PortState.On);
            buzzerFlag = !buzzerFlag;
            timer.Start();
        }

        private void AlarmOff()
        {
            if (isAlarm)
            {
                timer.Stop();
                Red.SetState(AdamPort.PortState.Off);
                Buzzer.SetState(AdamPort.PortState.Off);
                isAlarm = false;
            }
        }
    }
}
