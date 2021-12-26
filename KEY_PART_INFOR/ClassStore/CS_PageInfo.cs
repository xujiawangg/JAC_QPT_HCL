//=====================================================================================
// All Rights Reserved , Copyright @ HfutIE 2017
// Software Developers @ HfutIE 2017
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
    /// 底层页面信息
    /// </summary>
    public class CS_PageInfo : BaseEntity
    {
        /// <summary>
        /// 系统图标
        /// </summary>
        public byte[] SystemPicture { get; set; }
        /// <summary>
        /// 实验室图标
        /// </summary>
        public byte[] LaboratoryPicture { get; set; }
        /// <summary>
        /// 预留1
        /// </summary>
        public byte[] Reserve1 { get; set; }
        /// <summary>
        /// 预留2
        /// </summary>
        public byte[] Reserve2 { get; set; }
    }
}