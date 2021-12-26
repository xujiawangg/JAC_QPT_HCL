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
    /// 线边库工位物料配置表
    /// <author>
    ///		<name>she</name>
    ///		<date>2018.12.24 10:03</date>
    /// </author>
    /// </summary>
    [Description("线边库工位物料配置表")]
    [PrimaryKey("MATERIAL_WC_PARTKEY")]
    public class MATERIAL_WC_PART : BaseEntity
    {
        #region 获取/设置 字段值
        /// <summary>
        /// 主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("主键")]
        public string MATERIAL_WC_PARTKEY { get; set; }
        /// <summary>
        /// 工位编号
        /// </summary>
        /// <returns></returns>
        [DisplayName("工位编号")]
        public string STATION_CODE { get; set; }
        /// <summary>
        /// 工位名称
        /// </summary>
        /// <returns></returns>
        [DisplayName("工位名称")]
        public string STATION_NAME { get; set; }
        /// <summary>
        /// 工位主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("工位主键")]
        public string STATION_KEY { get; set; }
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
        /// 安全库存
        /// </summary>
        /// <returns></returns>
        [DisplayName("安全库存")]
        public int? SAFETY_NUM { get; set; }
        /// <summary>
        /// 最大库存
        /// </summary>
        /// <returns></returns>
        [DisplayName("最大库存")]
        public int? MAX_NUM { get; set; }
        /// <summary>
        /// 配送单位
        /// </summary>
        /// <returns></returns>
        [DisplayName("配送单位")]
        public string DELIVERY_UNIT { get; set; }
        /// <summary>
        /// 配送单位数量
        /// </summary>
        /// <returns></returns>
        [DisplayName("配送单位数量")]
        public int? DELIVERY_UNIT_NUM { get; set; }
        /// <summary>
        /// 存储数量
        /// </summary>
        /// <returns></returns>
        [DisplayName("存储数量")]
        public int? STORAGE_NUM { get; set; }
        /// <summary>
        /// Delivery_Batch
        /// </summary>
        /// <returns></returns>
        [DisplayName("Delivery_Batch")]
        public int? DELIVERY_BATCH { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        /// <returns></returns>
        [DisplayName("备注")]
        public string REMARK { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        /// <returns></returns>
        [DisplayName("创建日期")]
        public DateTime? CREATEDATE { get; set; }
        /// <summary>
        /// 创建者主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("创建者主键")]
        public string CREATEUSERID { get; set; }
        /// <summary>
        /// 创建者姓名
        /// </summary>
        /// <returns></returns>
        [DisplayName("创建者姓名")]
        public string CREATEUSERNAME { get; set; }
        /// <summary>
        /// 修改日期
        /// </summary>
        /// <returns></returns>
        [DisplayName("修改日期")]
        public DateTime? MODIFYDATE { get; set; }
        /// <summary>
        /// 修改者主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("修改者主键")]
        public string MODIFYUSERID { get; set; }
        /// <summary>
        /// 修改者姓名
        /// </summary>
        /// <returns></returns>
        [DisplayName("修改者姓名")]
        public string MODIFYUSERNAME { get; set; }
        /// <summary>
        /// 预留字段01
        /// </summary>
        /// <returns></returns>
        [DisplayName("预留字段01")]
        public string RESERVE01 { get; set; }
        /// <summary>
        /// 预留字段02
        /// </summary>
        /// <returns></returns>
        [DisplayName("预留字段02")]
        public string RESERVE02 { get; set; }
        /// <summary>
        /// 预留字段03
        /// </summary>
        /// <returns></returns>
        [DisplayName("预留字段03")]
        public string RESERVE03 { get; set; }
        /// <summary>
        /// 预留字段04
        /// </summary>
        /// <returns></returns>
        [DisplayName("预留字段04")]
        public string RESERVE04 { get; set; }
        /// <summary>
        /// 预留字段05
        /// </summary>
        /// <returns></returns>
        [DisplayName("预留字段05")]
        public string RESERVE05 { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.MATERIAL_WC_PARTKEY = CommonHelper.GetGuid;
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
            this.MATERIAL_WC_PARTKEY = KeyValue;
            this.MODIFYDATE = ServerTime.Now;
            this.MODIFYUSERID = SystemLog.UserKey;
            this.MODIFYUSERNAME = SystemLog.UserName;
        }
        #endregion
    }
}