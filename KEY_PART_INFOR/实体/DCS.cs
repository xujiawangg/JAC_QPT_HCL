
using System;
using System.ComponentModel;
using System.Text;

namespace HfutIe.Entity
{
    /// <summary>
    /// DCS
    /// <author>
    ///		<name>she</name>
    ///		<date>2017.02.24 13:49</date>
    /// </author>
    /// </summary>
    public class DCS :BaseEntity
    {
        #region 获取/设置 字段值
        /// <summary>
        /// dcs_key
        /// </summary>
        /// <returns></returns>
        [DisplayName("dcs_key")]
        public string dcs_key { get; set; }
        /// <summary>
        /// DCS名称
        /// </summary>
        /// <returns></returns>
        public string dcs_name { get; set; }
        /// <summary>
        /// DCS描述
        /// </summary>
        /// <returns></returns>
        [DisplayName("DCS描述")]
        public string dcs_description { get; set; }
        /// <summary>
        /// DCS类型
        /// </summary>
        /// <returns></returns>
        [DisplayName("DCS类型")]
        public string dcs_type { get; set; }
        /// <summary>
        /// 预留01
        /// </summary>
        /// <returns></returns>
        [DisplayName("预留03")]
        public string reserve03 { get; set; }
        /// <summary>
        /// 预留02
        /// </summary>
        /// <returns></returns>
        [DisplayName("预留04")]
        public string reserve04 { get; set; }
        /// <summary>
        /// dcs_code
        /// </summary>
        /// <returns></returns>
        [DisplayName("dcs_code")]
        public string dcs_code { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        /// <returns></returns>
        [DisplayName("创建时间")]
        public DateTime? CreateDate { get; set; }
        /// <summary>
        /// 创建者ID
        /// </summary>
        /// <returns></returns>
        [DisplayName("创建者ID")]
        public string CreateUserId { get; set; }
        /// <summary>
        /// 创建者姓名
        /// </summary>
        /// <returns></returns>
        [DisplayName("创建者姓名")]
        public string CreateUserName { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        /// <returns></returns>
        [DisplayName("修改时间")]
        public DateTime? ModifyDate { get; set; }
        /// <summary>
        /// 修改者ID
        /// </summary>
        /// <returns></returns>
        [DisplayName("修改者ID")]
        public string ModifyUserId { get; set; }
        /// <summary>
        /// 修改者姓名
        /// </summary>
        /// <returns></returns>
        [DisplayName("修改者姓名")]
        public string ModifyUserName { get; set; }
        #endregion
        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.dcs_key = CommonHelper.GetGuid;
            this.CreateDate = DateTime.Now;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="KeyValue"></param>
        public override void Modify(string KeyValue)
        {
            this.dcs_key = KeyValue;
            this.ModifyDate = DateTime.Now;
        }
        #endregion
    }
}