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
    /// �ײ�ҳ����Ϣ
    /// </summary>
    public class CS_PageInfo : BaseEntity
    {
        /// <summary>
        /// ϵͳͼ��
        /// </summary>
        public byte[] SystemPicture { get; set; }
        /// <summary>
        /// ʵ����ͼ��
        /// </summary>
        public byte[] LaboratoryPicture { get; set; }
        /// <summary>
        /// Ԥ��1
        /// </summary>
        public byte[] Reserve1 { get; set; }
        /// <summary>
        /// Ԥ��2
        /// </summary>
        public byte[] Reserve2 { get; set; }
    }
}