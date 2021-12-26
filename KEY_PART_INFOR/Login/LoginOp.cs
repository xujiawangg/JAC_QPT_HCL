using HfutIe.DataAccess.Common;
using HfutIE.DataAccess;
using HfutIE.Entity;
using HfutIE.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace HfutIe
{
    class LoginOp
    {
        public LoginInfor logininfor = new LoginInfor();
        public int CheckPermission(string Account, string PassWord/*, string StationKey*/)
        {
            try
            {
                //logininfor.stationley = StationKey;
                logininfor.Account = Account;
                logininfor.InPassWord = PassWord;
                if (!logininfor.IsExistence())
                {
                    return 0;//不存在此用户
                }
                if (!logininfor.IsPassWordCorrect())
                {
                    return 1;//用户密码不正确                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           
                }
                //if (!logininfor.HasPermission())
                //{
                //    return 2;//用户没有此工位访问权限
                //}
                //if (logininfor.IsRepair(StationKey))
                //{
                //    return 4;
                //}
                //if (logininfor.IsQualityGate(StationKey))
                //{
                //    return 5;
                //}
                //if (logininfor.IsHZ_Line(StationKey))
                //{
                //    return 7;
                //}
                //if (logininfor.IsJJLine(StationKey))
                //{
                //    return 8;
                //}
                return 3;//有访问权限
            }
            catch (Exception ex)
            {
                return 6;
            }

        }
    }
    class LoginInfor
    {
        #region 仓储
        RepositoryFactory<BASE_USER> Base_UserRepositoryFactory = new RepositoryFactory<BASE_USER>();//用户基本信息表
        RepositoryFactory<QUALITY_GATE> QUALITY_GATERepository = new RepositoryFactory<QUALITY_GATE>();
        RepositoryFactory<STATION> STATIONRepository = new RepositoryFactory<STATION>();
        RepositoryFactory<WORK_CENTER> WorkCenterRepositoryFactory = new RepositoryFactory<WORK_CENTER>();//工作中心信息

        RepositoryFactory<A_BS_StaffInfo> a_BS_StaffInfoRepositoryFactory = new RepositoryFactory<A_BS_StaffInfo>();//人员基本表 JAC_QPT
        #endregion

        /// <summary>
        /// 用户key
        /// </summary>
        public string UserKey { set; get; }
        /// <summary>
        /// 用户名称
        /// </summary>
        public string Account { set; get; }
        /// <summary>
        /// 输入的密码
        /// </summary>
        public string InPassWord { set; get; }
        /// <summary>
        /// 数据库存储密码
        /// </summary>
        public string StoragePassWord { set; get; }
        /// <summary>
        /// 用户的密钥
        /// </summary>
        public string Secretkey { set; get; }
        /// <summary>
        /// 当前工位信息
        /// </summary>
        //public string stationley { set; get; }
        public bool IsExistence()
        {
            A_BS_StaffInfo userinfo =new A_BS_StaffInfo() ;
            if (ServerCommunicationState == true)//在线状态登录检验
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                userinfo = a_BS_StaffInfoRepositoryFactory.Repository().FindList().Where(s => s.StaffAccount == this.Account).FirstOrDefault();
                sw.Stop();
                long a = sw.ElapsedMilliseconds;
            }
            else//离线状态登录检验
            {
                userinfo = a_BS_StaffInfoRepositoryFactory.Repository(DatabaseType.SQLite).FindList().Where(s => s.StaffAccount == this.Account).FirstOrDefault();
            }
            if (userinfo != null)
            {
                if (userinfo.ID == null)
                {
                    return false;
                }
                else
                {
                    //先为登陆信息赋值
                    this.UserKey = userinfo.ID.ToString();
                    this.Secretkey = userinfo.Secretkey.ToString();
                    this.StoragePassWord = userinfo.AccountPassword.ToString();
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 密码是否正确
        /// </summary>
        /// <returns></returns>
        public bool IsPassWordCorrect()
        {
            string Enpassword = Md5Helper.MD5_1(this.InPassWord);
            string EnPassword = Md5Helper.MD5(DESEncrypt.Encrypt(Enpassword.ToLower(), this.Secretkey).ToLower(), 32).ToLower();
            if (EnPassword == StoragePassWord)
            {
                return true;
            }
            return false;
        }
        public bool HasPermission()//专用于安全件采集程序，其中权限校验的Base_Module表中，code为“keypart”
        {
            //根据用户权限
            string sql = "select a.Category from  Base_ModulePermission a ,Base_Module b  where a.ModuleId=b.ModuleId and b.code= 'keypart' and a.ObjectId='" + this.UserKey + "' and a.Category='5'";
            DataTable dt = DbHelperSQL.OpenTable(sql);
            if (dt.Rows.Count > 0)
            {
                return true;
            }
            //根据角色权限
            string sql2 = "select b.Category from Base_ObjectUserRelation a,Base_ModulePermission b,Base_Module c where a.ObjectId=b.ObjectId  and b.ModuleId=c.ModuleId  and  a.UserId='" + this.UserKey + "' and b.Category='2'  and c.Code='keypart'";
            DataTable dt2 = DbHelperSQL.OpenTable(sql2);
            if (dt2.Rows.Count > 0)
            {
                return true;
            }
            return false;
        }

        public LoginInfor()
        {
            ShowServerConnectionState();
        }
        #region 显示服务器通讯信号
        private bool ServerCommunicationState = false;//服务器通讯状态

        private readonly string ServerIP = DBhelperOracle.dataSource;//服务器的IP地址

        private void ShowServerConnectionState()
        {
            try
            {
                Ping pingSender = new Ping();
                PingOptions options = new PingOptions();
                options.DontFragment = true;
                string data = "127.0.0.1";
                byte[] buffer = Encoding.ASCII.GetBytes(data);
                int timeout = 120;
                //测试网络连接：目标计算机为192.168.1.1(可以换成你所需要的目标地址） 
                //如果网络连接成功，PING就应该有返回；否则，网络连接有问题 
                PingReply reply = pingSender.Send(ServerIP, timeout, buffer, options);
                if (reply.Status == IPStatus.Success)
                {

                    ServerCommunicationState = true;
                }
                else
                {
                    ServerCommunicationState = false;
                }
            }
            catch (Exception ex)
            {
                ServerCommunicationState = false;
            }
        }
        #endregion

        /// <summary>
        /// 判断该工位是否配置了质量门
        /// </summary>
        /// <param name="wc_key"></param>
        /// <returns></returns>
        public bool IsQualityGate(string wc_key)
        {
            bool isquality = false;
            WORK_CENTER wcentity = WorkCenterRepositoryFactory.Repository().FindEntity("Work_Center_Code", wc_key);
            List<STATION> list = STATIONRepository.Repository().FindList($" and WORK_CENTER_KEY='{wcentity.WORK_CENTER_KEY}'");
            foreach(var item in list)
            { 
                List<QUALITY_GATE> QG = new List<QUALITY_GATE>();
                QG = QUALITY_GATERepository.Repository().FindList().Where(s => s.CFG_STATION_KEY == item.STATION_KEY).ToList();
                if (QG.Count > 0)
                {
                    isquality = true;
                    break;
                } 
            }
            return isquality;
        }
        /// <summary>
        /// 判断该工位是否为返修工位
        /// </summary>
        /// <param name="wc_key"></param>
        /// <returns></returns>
        public bool IsRepair(string wc_key)
        {
            bool isrepair = false;
            WORK_CENTER wcentity = WorkCenterRepositoryFactory.Repository().FindEntity("Work_Center_Code", wc_key);
            STATION entity = STATIONRepository.Repository().FindList().Where(s => s.WORK_CENTER_KEY == wcentity.WORK_CENTER_KEY&&s.IS_MAIN=="是").FirstOrDefault();
            if (entity!=null&& entity.STATION_TYPE.Contains("装配返修"))
            {
                isrepair =true;
            }
            return isrepair;
        }
        /// <summary>
        /// 判断该工位是否为后装安全件工位
        /// </summary>
        /// <param name="wc_key"></param>
        /// <returns></returns>
        public bool IsHZ_Line(string wc_key)
        {
            bool isHZ = false;
            WORK_CENTER wcentity = WorkCenterRepositoryFactory.Repository().FindEntity("Work_Center_Code", wc_key);
            STATION entity = STATIONRepository.Repository().FindList($" and WORK_CENTER_KEY='{wcentity.WORK_CENTER_KEY}' and IS_MAIN='是'").FirstOrDefault();
            if (entity.STATION_CODE.Contains("L"))
            {
                isHZ = true;
            }
            return isHZ;
        }
        /// <summary>
        /// 判断该工位是否为机加返修工位
        /// </summary>
        /// <param name="wc_key"></param>
        /// <returns></returns>
        public bool IsJJLine(string wc_key)
        {
            bool isrepair = false;
            WORK_CENTER wcentity = WorkCenterRepositoryFactory.Repository().FindEntity("Work_Center_Code", wc_key);
            STATION entity = STATIONRepository.Repository().FindList($" and WORK_CENTER_KEY='{wcentity.WORK_CENTER_KEY}' and IS_MAIN='是'").FirstOrDefault();
            if (entity != null && entity.STATION_TYPE.Contains("机加返修"))
            {
                isrepair = true;
            }
            return isrepair;
        }
    }
}
