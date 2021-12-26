//=====================================================================================
// All Rights Reserved , Copyright @ HfutIE 2018
// Software Developers @ HfutIE 2018
//=====================================================================================

using HfutIe;
using HfutIE.Utilities;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HfutIE.Entity
{
    /// <summary>
    /// 控制地址基本表
    /// <author>
    ///		<name>she</name>
    ///		<date>2018.12.12 16:11</date>
    /// </author>
    /// </summary>
    [Description("控制地址基本表")]
    [PrimaryKey("CONTROL_ADDRESS_KEY")]
    public class CONTROL_ADDRESS : BaseEntity
    {
        #region 获取/设置 字段值
        /// <summary>
        /// 地址ID
        /// </summary>
        /// <returns></returns>
        [DisplayName("地址ID")]
        public string CONTROL_ADDRESS_KEY { get; set; }
        /// <summary>
        /// 是否配置
        /// </summary>
        /// <returns></returns>
        [DisplayName("是否配置")]
        public int? HAS_CONFIGED { get; set; }
        /// <summary>
        /// 被配主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("被配主键")]
        public string POSITION_KEY { get; set; }
        /// <summary>
        /// 地址类型代码
        /// </summary>
        /// <returns></returns>
        [DisplayName("地址类型代码")]
        public string ADDRESS_CODE { get; set; }
        /// <summary>
        /// 地址名称
        /// </summary>
        /// <returns></returns>
        [DisplayName("地址名称")]
        public string ADDRESS_NAME { get; set; }
        /// <summary>
        /// 地址路径
        /// </summary>
        /// <returns></returns>
        [DisplayName("地址路径")]
        public string ADDRESS_PATH { get; set; }
        /// <summary>
        /// 地址数据类型
        /// </summary>
        /// <returns></returns>
        [DisplayName("地址数据类型")]
        public string ADDRESS_DATA_TYPE { get; set; }
        /// <summary>
        /// 地址类型
        /// </summary>
        /// <returns></returns>
        [DisplayName("地址类型")]
        public string ADDRESS_TYPE { get; set; }
        /// <summary>
        /// 地址描述
        /// </summary>
        /// <returns></returns>
        [DisplayName("地址描述")]
        public string ADDRESS_DESCRIPTION { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        /// <returns></returns>
        [DisplayName("备注")]
        public string REMARKS { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        /// <returns></returns>
        [DisplayName("创建时间")]
        public DateTime? CREATEDATE { get; set; }
        /// <summary>
        /// 创建人key
        /// </summary>
        /// <returns></returns>
        [DisplayName("创建人key")]
        public string CREATEUSERID { get; set; }
        /// <summary>
        /// 创建人姓名
        /// </summary>
        /// <returns></returns>
        [DisplayName("创建人姓名")]
        public string CREATEUSERNAME { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        /// <returns></returns>
        [DisplayName("修改时间")]
        public DateTime? MODIFYDATE { get; set; }
        /// <summary>
        /// 修改人key
        /// </summary>
        /// <returns></returns>
        [DisplayName("修改人key")]
        public string MODIFYUSERID { get; set; }
        /// <summary>
        /// 修改人姓名
        /// </summary>
        /// <returns></returns>
        [DisplayName("修改人姓名")]
        public string MODIFYUSERNAME { get; set; }
        /// <summary>
        /// 删除状态
        /// </summary>
        /// <returns></returns>
        [DisplayName("删除状态")]
        public int? DELETEMARK { get; set; }
        /// <summary>
        /// 预留01
        /// </summary>
        /// <returns></returns>
        [DisplayName("预留01")]
        public string RESERVE01 { get; set; }
        /// <summary>
        /// 预留02
        /// </summary>
        /// <returns></returns>
        [DisplayName("预留02")]
        public string RESERVE02 { get; set; }
        /// <summary>
        /// 预留03
        /// </summary>
        /// <returns></returns>
        [DisplayName("预留03")]
        public string RESERVE03 { get; set; }
        /// <summary>
        /// 预留04
        /// </summary>
        /// <returns></returns>
        [DisplayName("预留04")]
        public string RESERVE04 { get; set; }
        /// <summary>
        /// 预留05
        /// </summary>
        /// <returns></returns>
        [DisplayName("预留05")]
        public string RESERVE05 { get; set; }
        /// <summary>
        /// 预留06
        /// </summary>
        /// <returns></returns>
        [DisplayName("预留06")]
        public string RESERVE06 { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.CONTROL_ADDRESS_KEY = CommonHelper.GetGuid;
            this.CREATEDATE = ServerTime.Now;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="KeyValue"></param>
        //public override void Modify(string KeyValue)
        //{
        //    this.CONTROL_ADDRESS_KEY = KeyValue;
        //    this.MODIFYDATE = DateTime.Now;
        //    this.MODIFYUSERID = ManageProvider.Provider.Current().UserId;
        //    this.MODIFYUSERNAME = ManageProvider.Provider.Current().UserName;
        //}
        #endregion
    }
}