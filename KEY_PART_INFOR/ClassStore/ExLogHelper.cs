//=====================================================================================
// All Rights Reserved , Copyright @ HfutIE 2014
// Software Developers @ HfutIE 2014
//=====================================================================================


using log4net;
using System;
using System.ComponentModel;


namespace HfutIe
{
    /// <summary>
    /// 系统日志表
    /// <author>
    ///		<name>she</name>
    ///		<date>2017.06.23 22:43</date>
    /// </author>
    /// </summary>
    class ExLogHelper
    {
        #region 静态实例化
        private static ExLogHelper item;
        public static ExLogHelper Instance
        {
            get
            {
                if (item == null)
                {
                    item = new ExLogHelper();
                }
                return item;
            }
        }
        #endregion
        private Base_SysLog SysLog = new Base_SysLog();
        #region 写入操作日志
        /// <summary>
        /// 写入作业日志（新增操作）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="OperationType">操作类型</param>
        /// <param name="Status">状态</param>
        /// <param name="Remark">操作说明</param>
        /// <returns></returns>
        public void WriteLog(Exception ex, OperationType OperationType)
        { 
            try
            {
                SysLog.SysLogId = CommonHelper.GetGuid;
                SysLog.ObjectId = "System";
                SysLog.LogType = CommonHelper.GetString((int)OperationType);
                SysLog.IPAddress = SystemLog.IP;
                SysLog.IPAddressName = SystemLog.PCname;
                SysLog.CompanyId = "";
                SysLog.DepartmentId = "";
                SysLog.CreateUserId = SystemLog.UserKey;
                SysLog.CreateUserName = SystemLog.UserName;
                SysLog.ModuleId ="84575917-032d-472e-951d-9cac54de7de1";
                SysLog.Remark = ex.Message;
                SysLog.Status = "-1";
                new DataBaseOpByEntity().Insert(SysLog);
            }
            catch (Exception exp)
            {
                ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                log.Fatal("底层异常日志记录失败!", exp);
            }
             
        }
 
        #endregion
    }
    /// <summary>
    /// 操作类型
    /// </summary>
    public enum OperationType 
    {
        /// <summary>
        /// 登陆
        /// </summary>  
        Login = 0,
        /// <summary>
        ///  扫码
        /// </summary>   
        Scan = 9,
        /// <summary>
        ///  运行
        /// </summary>
        Run = 10,
        /// <summary>
        /// 安全退出
        /// </summary>
        Exit = 8,
    }
    class Base_SysLog : BaseEntity
    {
        #region 获取/设置 字段值
        /// <summary>
        /// 日志主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("日志主键")]
        public string SysLogId { get; set; }
        /// <summary>
        /// 对象主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("对象主键")]
        public string ObjectId { get; set; }
        /// <summary>
        /// 日志类型
        /// </summary>
        /// <returns></returns>
        [DisplayName("日志类型")]
        public string LogType { get; set; }
        /// <summary>
        /// 操作IP
        /// </summary>
        /// <returns></returns>
        [DisplayName("操作IP")]
        public string IPAddress { get; set; }
        /// <summary>
        /// IP地址所在地址
        /// </summary>
        /// <returns></returns>
        [DisplayName("IP地址所在地址")]
        public string IPAddressName { get; set; }
        /// <summary>
        /// 公司主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("公司主键")]
        public string CompanyId { get; set; }
        /// <summary>
        /// 部门主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("部门主键")]
        public string DepartmentId { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        /// <returns></returns>
        [DisplayName("创建时间")]
        public DateTime? CreateDate { get; set; }
        /// <summary>
        /// 创建用户主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("创建用户主键")]
        public string CreateUserId { get; set; }
        /// <summary>
        /// 创建用户
        /// </summary>
        /// <returns></returns>
        [DisplayName("创建用户")]
        public string CreateUserName { get; set; }
        /// <summary>
        /// 模块主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("模块主键")]
        public string ModuleId { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        /// <returns></returns>
        [DisplayName("描述")]
        public string Remark { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        /// <returns></returns>
        [DisplayName("状态")]
        public string Status { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.SysLogId = CommonHelper.GetGuid;
            this.CreateDate = ServerTime.Now;

        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="KeyValue"></param>
        public override void Modify(string KeyValue)
        {
            this.SysLogId = KeyValue;
        }
        #endregion
    }
}