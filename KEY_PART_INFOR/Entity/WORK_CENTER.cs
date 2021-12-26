//=====================================================================================
// All Rights Reserved , Copyright @ HfutIE 2019
// Software Developers @ HfutIE 2019
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
    /// 工作中心基本信息表
    /// <author>
    ///		<name>she</name>
    ///		<date>2019.08.15 08:47</date>
    /// </author>
    /// </summary>
    [Description("工作中心基本信息表")]
    [PrimaryKey("WORK_CENTER_KEY")]
    public class WORK_CENTER : BaseEntity
    {
        #region 获取/设置 字段值
        /// <summary>
        /// production_line_key
        /// </summary>
        /// <returns></returns>
        [DisplayName("production_line_key")]
        public string PRODUCTION_LINE_KEY { get; set; }
        /// <summary>
        /// 工位主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("工位主键")]
        public string WORK_CENTER_KEY { get; set; }
        /// <summary>
        /// work_center_code
        /// </summary>
        /// <returns></returns>
        [DisplayName("work_center_code")]
        public string WORK_CENTER_CODE { get; set; }
        /// <summary>
        /// work_center_name
        /// </summary>
        /// <returns></returns>
        [DisplayName("work_center_name")]
        public string WORK_CENTER_NAME { get; set; }
        /// <summary>
        /// work_center_beat
        /// </summary>
        /// <returns></returns>
        [DisplayName("work_center_beat")]
        public int? WORK_CENTER_BEAT { get; set; }
        /// <summary>
        /// work_center_description
        /// </summary>
        /// <returns></returns>
        [DisplayName("work_center_description")]
        public string WORK_CENTER_DESCRIPTION { get; set; }
        /// <summary>
        /// work_center_type
        /// </summary>
        /// <returns></returns>
        [DisplayName("work_center_type")]
        public string WORK_CENTER_TYPE { get; set; }
        /// <summary>
        /// inst_list_key
        /// </summary>
        /// <returns></returns>
        [DisplayName("inst_list_key")]
        public string INST_LIST_KEY { get; set; }
        /// <summary>
        /// CreateDate
        /// </summary>
        /// <returns></returns>
        [DisplayName("CreateDate")]
        public DateTime? CREATEDATE { get; set; }
        /// <summary>
        /// CreateUserId
        /// </summary>
        /// <returns></returns>
        [DisplayName("CreateUserId")]
        public string CREATEUSERID { get; set; }
        /// <summary>
        /// CreateUserName
        /// </summary>
        /// <returns></returns>
        [DisplayName("CreateUserName")]
        public string CREATEUSERNAME { get; set; }
        /// <summary>
        /// ModifyDate
        /// </summary>
        /// <returns></returns>
        [DisplayName("ModifyDate")]
        public DateTime? MODIFYDATE { get; set; }
        /// <summary>
        /// ModifyUserId
        /// </summary>
        /// <returns></returns>
        [DisplayName("ModifyUserId")]
        public string MODIFYUSERID { get; set; }
        /// <summary>
        /// ModifyUserName
        /// </summary>
        /// <returns></returns>
        [DisplayName("ModifyUserName")]
        public string MODIFYUSERNAME { get; set; }
        /// <summary>
        /// reserve01
        /// </summary>
        /// <returns></returns>
        [DisplayName("reserve01")]
        public string RESERVE01 { get; set; }
        /// <summary>
        /// reserve02
        /// </summary>
        /// <returns></returns>
        [DisplayName("reserve02")]
        public string RESERVE02 { get; set; }
        /// <summary>
        /// reserve03
        /// </summary>
        /// <returns></returns>
        [DisplayName("reserve03")]
        public string RESERVE03 { get; set; }
        /// <summary>
        /// reserve04
        /// </summary>
        /// <returns></returns>
        [DisplayName("reserve04")]
        public string RESERVE04 { get; set; }
        /// <summary>
        /// reserve05
        /// </summary>
        /// <returns></returns>
        [DisplayName("reserve05")]
        public string RESERVE05 { get; set; }
        /// <summary>
        /// reserve06
        /// </summary>
        /// <returns></returns>
        [DisplayName("reserve06")]
        public string RESERVE06 { get; set; }
        /// <summary>
        /// reserve07
        /// </summary>
        /// <returns></returns>
        [DisplayName("reserve07")]
        public string RESERVE07 { get; set; }
        /// <summary>
        /// reserve08
        /// </summary>
        /// <returns></returns>
        [DisplayName("reserve08")]
        public string RESERVE08 { get; set; }
        /// <summary>
        /// reserve09
        /// </summary>
        /// <returns></returns>
        [DisplayName("reserve09")]
        public string RESERVE09 { get; set; }
        /// <summary>
        /// reserve10
        /// </summary>
        /// <returns></returns>
        [DisplayName("reserve10")]
        public string RESERVE10 { get; set; }
        /// <summary>
        /// DeleteMark
        /// </summary>
        /// <returns></returns>
        [DisplayName("DeleteMark")]
        public int? DELETEMARK { get; set; }
        /// <summary>
        /// 工作中心对应IP
        /// </summary>
        /// <returns></returns>
        [DisplayName("工作中心对应IP")]
        public string IP { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.WORK_CENTER_KEY = CommonHelper.GetGuid;
            this.CREATEDATE = DateTime.Now;
            //this.CREATEUSERID = ManageProvider.Provider.Current().UserId;
            //this.CREATEUSERNAME = ManageProvider.Provider.Current().UserName;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="KeyValue"></param>
        public override void Modify(string KeyValue)
        {
            this.WORK_CENTER_KEY = KeyValue;
            this.MODIFYDATE = DateTime.Now;
            //this.MODIFYUSERID = ManageProvider.Provider.Current().UserId;
            //this.MODIFYUSERNAME = ManageProvider.Provider.Current().UserName;
        }
        #endregion
    }
}