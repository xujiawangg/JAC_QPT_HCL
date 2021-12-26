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
    /// Bom版本信息
    /// <author>
    ///		<name>she</name>
    ///		<date>2019.02.28 16:27</date>
    /// </author>
    /// </summary>
    [Description("Bom版本信息")]
    [PrimaryKey("VERSION_KEY")]
    public class BOM_VERSION : BaseEntity
    {
        #region 获取/设置 字段值
        /// <summary>
        /// 版本ID
        /// </summary>
        /// <returns></returns>
        [DisplayName("版本ID")]
        public string VERSION_KEY { get; set; }
        /// <summary>
        /// 版本编号
        /// </summary>
        /// <returns></returns>
        [DisplayName("版本编号")]
        public string VERSION_CODE { get; set; }
        /// <summary>
        /// 版本名称
        /// </summary>
        /// <returns></returns>
        [DisplayName("版本名称")]
        public string VERSION_NAME { get; set; }
        /// <summary>
        /// 物料主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("物料主键")]
        public string PART_KEY { get; set; }
        /// <summary>
        /// 是否默认版本
        /// </summary>
        /// <returns></returns>
        [DisplayName("是否默认版本")]
        public string IS_DEFAULT { get; set; }
        /// <summary>
        /// 是否提交
        /// </summary>
        /// <returns></returns>
        [DisplayName("是否提交")]
        public string IS_SUBMIT { get; set; }
        /// <summary>
        /// 生效时间
        /// </summary>
        /// <returns></returns>
        [DisplayName("生效时间")]
        public DateTime? EFFECTIVE_TIME { get; set; }
        /// <summary>
        /// 失效时间
        /// </summary>
        /// <returns></returns>
        [DisplayName("失效时间")]
        public DateTime? FAILURE_TIME { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        /// <returns></returns>
        [DisplayName("描述")]
        public string REMARKS { get; set; }
        /// <summary>
        /// 预留字段1
        /// </summary>
        /// <returns></returns>
        [DisplayName("预留字段1")]
        public string RESERVE1 { get; set; }
        /// <summary>
        /// 预留字段2
        /// </summary>
        /// <returns></returns>
        [DisplayName("预留字段2")]
        public string RESERVE2 { get; set; }
        /// <summary>
        /// 预留字段3
        /// </summary>
        /// <returns></returns>
        [DisplayName("预留字段3")]
        public string RESERVE3 { get; set; }
        /// <summary>
        /// 预留字段4
        /// </summary>
        /// <returns></returns>
        [DisplayName("预留字段4")]
        public string RESERVE4 { get; set; }
        /// <summary>
        /// 预留字段5
        /// </summary>
        /// <returns></returns>
        [DisplayName("预留字段5")]
        public string RESERVE5 { get; set; }
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
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        //public override void Create()
        //{
        //    this.VERSION_KEY = CommonHelper.GetGuid;
        //    this.CREATEDATE = DateTime.Now;
        //    this.CREATEUSERID = ManageProvider.Provider.Current().UserId;
        //    this.CREATEUSERNAME = ManageProvider.Provider.Current().UserName;
        //}
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="KeyValue"></param>
        //public override void Modify(string KeyValue)
        //{
        //    this.VERSION_KEY = KeyValue;
        //    this.MODIFYDATE = DateTime.Now;
        //    this.MODIFYUSERID = ManageProvider.Provider.Current().UserId;
        //    this.MODIFYUSERNAME = ManageProvider.Provider.Current().UserName;
        //}
        #endregion
    }
}