//=====================================================================================
// All Rights Reserved , Copyright @ HfutIE 2017
// Software Developers @ HfutIE 2017
//=====================================================================================

using System;
using System.ComponentModel;
using System.Text;

namespace HfutIe
{
    #region 获取/设置 字段值
    /// <summary>
    /// ADON_INFO
    /// <author>
    ///		<name>she</name>
    ///		<date>2017.03.21 20:35</date>
    /// </author>
    /// </summary>
    public class GUIDFILES : BaseEntity
    {
        /// <summary>
        /// 文档ID
        /// </summary>
        [DisplayName("文档ID")]
        public string document_key { get; set; }
        /// <summary>
        /// 生产线ID
        /// </summary>
        [DisplayName("生产线ID")]
        public string p_line_key { get; set; }
        /// <summary>
        /// 生产线编号
        /// </summary>
        [DisplayName("生产线编号")]
        public string p_line_code { get; set; }
        /// <summary>
        /// 生产线名称
        /// </summary>
        [DisplayName("生产线名称")]
        public string p_line_name { get; set; }
        /// <summary>
        /// 工位ID
        /// </summary>
        [DisplayName("工位ID")]
        public string wc_key { get; set; }
        /// <summary>
        /// 工位编号
        /// </summary>
        [DisplayName("工位编号")]
        public string wc_code { get; set; }
        /// <summary>
        /// 工位名称
        /// </summary>
        [DisplayName("工位名称")]
        public string wc_name { get; set; }
        /// <summary>
        /// 产品ID
        /// </summary>
        [DisplayName("产品ID")]
        public string product_key { get; set; }
        /// <summary>
        /// 产品编号
        /// </summary>
        [DisplayName("产品编号")]
        public string product_code { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        [DisplayName("产品名称")]
        public string product_name { get; set; }
        /// <summary>
        /// 文档编号
        /// </summary>
        [DisplayName("文档编号")]
        public string document_code { get; set; }

        /// <summary>
        /// 文档名称
        /// </summary>
        [DisplayName("文档名称")]
        public string document_name { get; set; }
        /// <summary>
        /// 文档类别
        /// </summary>
        [DisplayName("文档类别")]
        public string document_type { get; set; }
        /// <summary>
        /// 文档版本号
        /// </summary>
        [DisplayName("文档版本号")]
        public string document_edition { get; set; }
        /// <summary>
        /// 文档大小
        /// </summary>
        [DisplayName("文档大小")]
        public string document_size { get; set; }
        /// <summary>
        /// 文档内容
        /// </summary>
        [DisplayName("文档内容")]
        public byte[] document_file { get; set; }
        /// <summary>
        /// 文档内容
        /// </summary>
        [DisplayName("备注")]
        public byte[] remarks { get; set; }

        #endregion
    }
}