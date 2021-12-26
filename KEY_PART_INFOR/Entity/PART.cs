//=====================================================================================
// All Rights Reserved , Copyright @ HfutIE 2018
// Software Developers @ HfutIE 2018
//=====================================================================================

using HfutIe;using HfutIE.Utilities;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HfutIE.Entity
{
    /// <summary>
    /// 零部件基本信息
    /// <author>
    ///		<name>she</name>
    ///		<date>2018.12.24 15:49</date>
    /// </author>
    /// </summary>
    [Description("零部件基本信息")]
    [PrimaryKey("PART_KEY")]
    public class PART : BaseEntity
    {
        #region 获取/设置 字段值
        /// <summary>
        /// 物料主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("物料主键")]
        public string PART_KEY { get; set; }
        /// <summary>
        /// 物料编号
        /// </summary>
        /// <returns></returns>
        [DisplayName("物料编号")]
        public string PART_CODE { get; set; }
        /// <summary>
        /// 物料名称
        /// </summary>
        /// <returns></returns>
        [DisplayName("物料名称")]
        public string PART_NAME { get; set; }
        /// <summary>
        /// 制造类型
        /// </summary>
        /// <returns></returns>
        [DisplayName("制造类型")]
        public string PART_TYPE { get; set; }
        /// <summary>
        /// 是否产品
        /// </summary>
        /// <returns></returns>
        [DisplayName("是否产品")]
        public string IS_PRODUCT { get; set; }
        /// <summary>
        /// 物料类型
        /// </summary>
        /// <returns></returns>
        [DisplayName("物料类型")]
        public string PART_VARIETY { get; set; }
        /// <summary>
        /// 物料模型号
        /// </summary>
        /// <returns></returns>
        [DisplayName("物料模型号")]
        public string PART_MODEL_NO { get; set; }
        /// <summary>
        /// 所属平台
        /// </summary>
        /// <returns></returns>
        [DisplayName("所属平台")]
        public string PART_PLATFORM { get; set; }
        /// <summary>
        /// 物料图号
        /// </summary>
        /// <returns></returns>
        [DisplayName("物料图号")]
        public string PART_DRAW_NO { get; set; }
        /// <summary>
        /// 物料结构号
        /// </summary>
        /// <returns></returns>
        [DisplayName("物料结构号")]
        public string PART_STRUCT_NO { get; set; }
        /// <summary>
        /// 物料简易码
        /// </summary>
        /// <returns></returns>
        [DisplayName("物料简易码")]
        public string PART_ABB { get; set; }
        /// <summary>
        /// 物料单位
        /// </summary>
        /// <returns></returns>
        [DisplayName("物料单位")]
        public string PART_UNIT { get; set; }
        /// <summary>
        /// 采集时间
        /// </summary>
        /// <returns></returns>
        [DisplayName("采集时间")]
        public DateTime? SYNCHRO_TIME { get; set; }
        /// <summary>
        /// ERP物料主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("ERP物料主键")]
        public string ERP_PART_KEY { get; set; }
        /// <summary>
        /// ERP物料编号
        /// </summary>
        /// <returns></returns>
        [DisplayName("ERP物料编号")]
        public string ERP_PART_CODE { get; set; }
        /// <summary>
        /// 接收时间
        /// </summary>
        /// <returns></returns>
        [DisplayName("接收时间")]
        public DateTime? REVISE_TIME { get; set; }
        /// <summary>
        /// 是否配置
        /// </summary>
        /// <returns></returns>
        [DisplayName("是否配置")]
        public string HAS_CONFIGED { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        /// <returns></returns>
        [DisplayName("备注")]
        public string REMARKS { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        /// <returns></returns>
        [DisplayName("创建日期")]
        public DateTime? CREATEDATE { get; set; }
        /// <summary>
        /// 创建人id
        /// </summary>
        /// <returns></returns>
        [DisplayName("创建人id")]
        public string CREATEUSERID { get; set; }
        /// <summary>
        /// 创建人姓名
        /// </summary>
        /// <returns></returns>
        [DisplayName("创建人姓名")]
        public string CREATEUSERNAME { get; set; }
        /// <summary>
        /// 修改日期
        /// </summary>
        /// <returns></returns>
        [DisplayName("修改日期")]
        public DateTime? MODIFYDATE { get; set; }
        /// <summary>
        /// 修改人id
        /// </summary>
        /// <returns></returns>
        [DisplayName("修改人id")]
        public string MODIFYUSERID { get; set; }
        /// <summary>
        /// 修改人姓名
        /// </summary>
        /// <returns></returns>
        [DisplayName("修改人姓名")]
        public string MODIFYUSERNAME { get; set; }
        /// <summary>
        /// 保留
        /// </summary>
        /// <returns></returns>
        [DisplayName("保留")]
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
        /// 删除标记
        /// </summary>
        /// <returns></returns>
        [DisplayName("删除标记")]
        public int? DELETEMARK { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.PART_KEY = CommonHelper.GetGuid;
            this.CREATEDATE = ServerTime.Now;
            this.CREATEUSERID = SystemLog.UserKey;
            this.CREATEUSERNAME = SystemLog.UserName;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="KeyValue"></param>
        public override void Modify(string KeyValue)
        {
            this.PART_KEY = KeyValue;
            this.MODIFYDATE = ServerTime.Now;
            this.MODIFYUSERID = SystemLog.UserKey;
            this.MODIFYUSERNAME = SystemLog.UserName;
        }
        #endregion
    }
}