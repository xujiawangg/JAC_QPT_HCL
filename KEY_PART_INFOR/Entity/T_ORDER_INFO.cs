//=====================================================================================
// All Rights Reserved , Copyright @ HfutIE 2017
// Software Developers @ HfutIE 2017
//=====================================================================================

using System;
using System.ComponentModel;
using System.Text;

namespace HfutIe
{
    /// <summary>
    /// AGVMessage
    /// <author>
    ///		<name>she</name>
    ///		<date>2017.12.09 12:47</date>
    /// </author>
    /// </summary>
    [Description("P_PLAN_M")]
    public class T_ORDER_INFO
    {
        #region 获取/设置 字段值
        /// <summary>
        /// WMS_ID
        /// </summary>
        /// <returns></returns>
        [DisplayName("WMS_ID")]
        public int? WMS_ID { get; set; }
        /// <summary>
        /// STEP_ID
        /// </summary>
        /// <returns></returns>
        [DisplayName("STEP_ID")]
        public int? STEP_ID { get; set; }
        /// <summary>
        /// PRIORITY
        /// </summary>
        /// <returns></returns>
        [DisplayName("PRIORITY")]
        public int? PRIORITY { get; set; }
        /// <summary>
        /// GETEMP_ID
        /// </summary>
        /// <returns></returns>
        [DisplayName("GETEMP_ID")]
        public int? GETEMP_ID { get; set; }
        /// <summary>
        /// PUTEMP_ID
        /// </summary>
        /// <returns></returns>
        [DisplayName("PUTEMP_ID")]
        public int? PUTEMP_ID { get; set; }
        /// <summary>
        /// GETFULL_ID
        /// </summary>
        /// <returns></returns>
        [DisplayName("GETFULL_ID")]
        public int? GETFULL_ID { get; set; }
        /// <summary>
        /// PUTFULL_ID
        /// </summary>
        /// <returns></returns>
        [DisplayName("PUTFULL_ID")]
        public int? PUTFULL_ID { get; set; }
        /// <summary>
        /// JOB_STATUS
        /// </summary>
        /// <returns></returns>
        [DisplayName("JOB_STATUS")]
        public int? JOB_STATUS { get; set; }
        #endregion
    }
}