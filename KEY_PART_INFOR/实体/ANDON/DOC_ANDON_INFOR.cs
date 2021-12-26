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
    /// DOC_ANDON_INFOR
    /// <author>
    ///		<name>she</name>
    ///		<date>2017.05.04 09:53</date>
    /// </author>
    /// </summary>
    [Description("DOC_ANDON_INFOR")]
    public class DOC_ANDON_INFOR : BaseEntity
    {
        #region ��ȡ/���� �ֶ�ֵ
        /// <summary>
        /// andon_infor_key
        /// </summary>
        /// <returns></returns>
        [DisplayName("andon_infor_key")]
        public string andon_infor_key { get; set; }
        /// <summary>
        /// site_key
        /// </summary>
        /// <returns></returns>
        [DisplayName("site_key")]
        public string site_key { get; set; }
        /// <summary>
        /// site_code
        /// </summary>
        /// <returns></returns>
        [DisplayName("site_code")]
        public string site_code { get; set; }
        /// <summary>
        /// site_name
        /// </summary>
        /// <returns></returns>
        [DisplayName("site_name")]
        public string site_name { get; set; }
        /// <summary>
        /// area_key
        /// </summary>
        /// <returns></returns>
        [DisplayName("area_key")]
        public string area_key { get; set; }
        /// <summary>
        /// area_code
        /// </summary>
        /// <returns></returns>
        [DisplayName("area_code")]
        public string area_code { get; set; }
        /// <summary>
        /// area_name
        /// </summary>
        /// <returns></returns>
        [DisplayName("area_name")]
        public string area_name { get; set; }
        /// <summary>
        /// ws_key
        /// </summary>
        /// <returns></returns>
        [DisplayName("ws_key")]
        public string ws_key { get; set; }
        /// <summary>
        /// ws_code
        /// </summary>
        /// <returns></returns>
        [DisplayName("ws_code")]
        public string ws_code { get; set; }
        /// <summary>
        /// ws_name
        /// </summary>
        /// <returns></returns>
        [DisplayName("ws_name")]
        public string ws_name { get; set; }
        /// <summary>
        /// p_line_key
        /// </summary>
        /// <returns></returns>
        [DisplayName("p_line_key")]
        public string p_line_key { get; set; }
        /// <summary>
        /// p_line_code
        /// </summary>
        /// <returns></returns>
        [DisplayName("p_line_code")]
        public string p_line_code { get; set; }
        /// <summary>
        /// p_line_name
        /// </summary>
        /// <returns></returns>
        [DisplayName("p_line_name")]
        public string p_line_name { get; set; }
        /// <summary>
        /// wc_key
        /// </summary>
        /// <returns></returns>
        [DisplayName("wc_key")]
        public string wc_key { get; set; }
        /// <summary>
        /// wc_code
        /// </summary>
        /// <returns></returns>
        [DisplayName("wc_code")]
        public string wc_code { get; set; }
        /// <summary>
        /// wc_name
        /// </summary>
        /// <returns></returns>
        [DisplayName("wc_name")]
        public string wc_name { get; set; }
        /// <summary>
        /// equip_key
        /// </summary>
        /// <returns></returns>
        [DisplayName("equip_key")]
        public string equip_key { get; set; }
        /// <summary>
        /// equip_code
        /// </summary>
        /// <returns></returns>
        [DisplayName("equip_code")]
        public string equip_code { get; set; }
        /// <summary>
        /// equip_name
        /// </summary>
        /// <returns></returns>
        [DisplayName("equip_name")]
        public string equip_name { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        [DisplayName("����")]
        public string andon_type_key { get; set; }
        /// <summary>
        /// andon���ͱ��
        /// </summary>
        /// <returns></returns>
        [DisplayName("andon���ͱ��")]
        public string andon_type_code { get; set; }
        /// <summary>
        /// adon��������
        /// </summary>
        /// <returns></returns>
        [DisplayName("adon��������")]
        public string andon_type_name { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        [DisplayName("����")]
        public string andon_info_key { get; set; }
        /// <summary>
        /// adon_info_code
        /// </summary>
        /// <returns></returns>
        [DisplayName("adon_info_code")]
        public string andon_info_code { get; set; }
        /// <summary>
        /// adon_info_name
        /// </summary>
        /// <returns></returns>
        [DisplayName("adon_info_name")]
        public string andon_info_name { get; set; }
        /// <summary>
        /// remark
        /// </summary>
        /// <returns></returns>
        [DisplayName("remark")]
        public string remark { get; set; }
        /// <summary>
        /// push_people
        /// </summary>
        /// <returns></returns>
        [DisplayName("push_people")]
        public string push_people { get; set; }
        /// <summary>
        /// rank
        /// </summary>
        /// <returns></returns>
        [DisplayName("rank")]
        public string rank { get; set; }
        /// <summary>
        /// start_time
        /// </summary>
        /// <returns></returns>
        [DisplayName("start_time")]
        public DateTime? start_time { get; set; }
        /// <summary>
        /// end_time
        /// </summary>
        /// <returns></returns>
        [DisplayName("end_time")]
        public DateTime? end_time { get; set; }
        /// <summary>
        /// continued_time
        /// </summary>
        /// <returns></returns>
        [DisplayName("continued_time")]
        public int? continued_time { get; set; }
        /// <summary>
        /// feed_staff_key
        /// </summary>
        /// <returns></returns>
        [DisplayName("feed_staff_key")]
        public string feed_staff_key { get; set; }
        /// <summary>
        /// feed_staff_code
        /// </summary>
        /// <returns></returns>
        [DisplayName("feed_staff_code")]
        public string feed_staff_code { get; set; }
        /// <summary>
        /// feed_staff_name
        /// </summary>
        /// <returns></returns>
        [DisplayName("feed_staff_name")]
        public string feed_staff_name { get; set; }
        /// <summary>
        /// feed_time
        /// </summary>
        /// <returns></returns>
        [DisplayName("feed_time")]
        public DateTime? feed_time { get; set; }
        /// <summary>
        /// feed_continued_time
        /// </summary>
        /// <returns></returns>
        [DisplayName("feed_continued_time")]
        public int? feed_continued_time { get; set; }
        /// <summary>
        /// reserve1
        /// </summary>
        /// <returns></returns>
        [DisplayName("reserve1")]
        public string reserve1 { get; set; }
        /// <summary>
        /// reserve2
        /// </summary>
        /// <returns></returns>
        [DisplayName("reserve2")]
        public string reserve2 { get; set; }
        /// <summary>
        /// reserve3
        /// </summary>
        /// <returns></returns>
        [DisplayName("reserve3")]
        public string reserve3 { get; set; }
        /// <summary>
        /// reserve4
        /// </summary>
        /// <returns></returns>
        [DisplayName("reserve4")]
        public string reserve4 { get; set; }
        /// <summary>
        /// reserve5
        /// </summary>
        /// <returns></returns>
        [DisplayName("reserve5")]
        public string reserve5 { get; set; }
        /// <summary>
        /// reserve6
        /// </summary>
        /// <returns></returns>
        [DisplayName("reserve6")]
        public string reserve6 { get; set; }
        /// <summary>
        /// reserve7
        /// </summary>
        /// <returns></returns>
        [DisplayName("reserve7")]
        public string reserve7 { get; set; }
        /// <summary>
        /// reserve8
        /// </summary>
        /// <returns></returns>
        [DisplayName("reserve8")]
        public string reserve8 { get; set; }
        /// <summary>
        /// reserve9
        /// </summary>
        /// <returns></returns>
        [DisplayName("reserve9")]
        public string reserve9 { get; set; }
        /// <summary>
        /// reserve10
        /// </summary>
        /// <returns></returns>
        [DisplayName("reserve10")]
        public string reserve10 { get; set; }
        /// <summary>
        /// CreateDate
        /// </summary>
        /// <returns></returns>
        [DisplayName("CreateDate")]
        public DateTime? CreateDate { get; set; }
        /// <summary>
        /// CreateUserId
        /// </summary>
        /// <returns></returns>
        [DisplayName("CreateUserId")]
        public string CreateUserId { get; set; }
        /// <summary>
        /// CreateUserName
        /// </summary>
        /// <returns></returns>
        [DisplayName("CreateUserName")]
        public string CreateUserName { get; set; }
        /// <summary>
        /// ModifyDate
        /// </summary>
        /// <returns></returns>
        [DisplayName("ModifyDate")]
        public DateTime? ModifyDate { get; set; }
        /// <summary>
        /// ModifyUserId
        /// </summary>
        /// <returns></returns>
        [DisplayName("ModifyUserId")]
        public string ModifyUserId { get; set; }
        /// <summary>
        /// ModifyUserName
        /// </summary>
        /// <returns></returns>
        [DisplayName("ModifyUserName")]
        public string ModifyUserName { get; set; }
        #endregion

        #region ��չ����
        /// <summary>
        /// ��������
        /// </summary>
        public override void Create()
        {
            this.andon_infor_key = CommonHelper.GetGuid;
            this.CreateDate = DateTime.Now;
        }
        /// <summary>
        /// �༭����
        /// </summary>
        /// <param name="KeyValue"></param>
        public override void Modify(string KeyValue)
        {
            this.andon_infor_key = KeyValue;
            this.ModifyDate = DateTime.Now;
        }
        #endregion
    }
}