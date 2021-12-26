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
    /// 用户信息表
    /// <author>
    ///		<name>she</name>
    ///		<date>2019.02.19 15:55</date>
    /// </author>
    /// </summary>
    [Description("用户信息表")]
    [PrimaryKey("USERID")]
    public class BASE_USER : BaseEntity
    {
        #region 获取/设置 字段值
        /// <summary>
        /// 用户主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("用户主键")]
        public string USERID { get; set; }
        /// <summary>
        /// 公司主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("公司主键")]
        public string COMPANYID { get; set; }
        /// <summary>
        /// 部门主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("部门主键")]
        public string DEPARTMENTID { get; set; }
        /// <summary>
        /// 内部用户
        /// </summary>
        /// <returns></returns>
        [DisplayName("内部用户")]
        public int? INNERUSER { get; set; }
        /// <summary>
        /// 用户编码
        /// </summary>
        /// <returns></returns>
        [DisplayName("用户编码")]
        public string CODE { get; set; }
        /// <summary>
        /// 登录账户
        /// </summary>
        /// <returns></returns>
        [DisplayName("登录账户")]
        public string ACCOUNT { get; set; }
        /// <summary>
        /// 登录密码
        /// </summary>
        /// <returns></returns>
        [DisplayName("登录密码")]
        public string PASSWORD { get; set; }
        /// <summary>
        /// 密码秘钥
        /// </summary>
        /// <returns></returns>
        [DisplayName("密码秘钥")]
        public string SECRETKEY { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        /// <returns></returns>
        [DisplayName("姓名")]
        public string REALNAME { get; set; }
        /// <summary>
        /// 姓名拼音
        /// </summary>
        /// <returns></returns>
        [DisplayName("姓名拼音")]
        public string SPELL { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        /// <returns></returns>
        [DisplayName("性别")]
        public string GENDER { get; set; }
        /// <summary>
        /// 出生日期
        /// </summary>
        /// <returns></returns>
        [DisplayName("出生日期")]
        public string BIRTHDAY { get; set; }
        /// <summary>
        /// 手机
        /// </summary>
        /// <returns></returns>
        [DisplayName("手机")]
        public string MOBILE { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        /// <returns></returns>
        [DisplayName("电话")]
        public string TELEPHONE { get; set; }
        /// <summary>
        /// QQ号码
        /// </summary>
        /// <returns></returns>
        [DisplayName("QQ号码")]
        public string OICQ { get; set; }
        /// <summary>
        /// 电子邮件
        /// </summary>
        /// <returns></returns>
        [DisplayName("电子邮件")]
        public string EMAIL { get; set; }
        /// <summary>
        /// 最后修改密码日期
        /// </summary>
        /// <returns></returns>
        [DisplayName("最后修改密码日期")]
        public string CHANGEPASSWORDDATE { get; set; }
        /// <summary>
        /// 单点登录标识
        /// </summary>
        /// <returns></returns>
        [DisplayName("单点登录标识")]
        public int? OPENID { get; set; }
        /// <summary>
        /// 登录次数
        /// </summary>
        /// <returns></returns>
        [DisplayName("登录次数")]
        public int? LOGONCOUNT { get; set; }
        /// <summary>
        /// 第一次访问时间
        /// </summary>
        /// <returns></returns>
        [DisplayName("第一次访问时间")]
        public string FIRSTVISIT { get; set; }
        /// <summary>
        /// 上一次访问时间
        /// </summary>
        /// <returns></returns>
        [DisplayName("上一次访问时间")]
        public string PREVIOUSVISIT { get; set; }
        /// <summary>
        /// 最后访问时间
        /// </summary>
        /// <returns></returns>
        [DisplayName("最后访问时间")]
        public string LASTVISIT { get; set; }
        /// <summary>
        /// 审核状态
        /// </summary>
        /// <returns></returns>
        [DisplayName("审核状态")]
        public string AUDITSTATUS { get; set; }
        /// <summary>
        /// 审核员主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("审核员主键")]
        public string AUDITUSERID { get; set; }
        /// <summary>
        /// 审核员
        /// </summary>
        /// <returns></returns>
        [DisplayName("审核员")]
        public string AUDITUSERNAME { get; set; }
        /// <summary>
        /// 审核时间
        /// </summary>
        /// <returns></returns>
        [DisplayName("审核时间")]
        public string AUDITDATETIME { get; set; }
        /// <summary>
        /// 是否在线
        /// </summary>
        /// <returns></returns>
        [DisplayName("是否在线")]
        public int? IS_ONLINE { get; set; } 
        /// <summary>
        /// 备注
        /// </summary>
        /// <returns></returns>
        [DisplayName("备注")]
        public string REMARK { get; set; }
        /// <summary>
        /// 有效
        /// </summary>
        /// <returns></returns>
        [DisplayName("有效")]
        public int? ENABLED { get; set; }
        /// <summary>
        /// 排序码
        /// </summary>
        /// <returns></returns>
        [DisplayName("排序码")]
        public int? SORTCODE { get; set; }
        /// <summary>
        /// 删除标记
        /// </summary>
        /// <returns></returns>
        [DisplayName("删除标记")]
        public int? DELETEMARK { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        /// <returns></returns>
        [DisplayName("创建时间")]
        public string CREATEDATE { get; set; }
        /// <summary>
        /// 创建用户主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("创建用户主键")]
        public string CREATEUSERID { get; set; }
        /// <summary>
        /// 创建用户
        /// </summary>
        /// <returns></returns>
        [DisplayName("创建用户")]
        public string CREATEUSERNAME { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        /// <returns></returns>
        [DisplayName("修改时间")]
        public string MODIFYDATE { get; set; }
        /// <summary>
        /// 修改用户主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("修改用户主键")]
        public string MODIFYUSERID { get; set; }
        /// <summary>
        /// 修改用户
        /// </summary>
        /// <returns></returns>
        [DisplayName("修改用户")]
        public string MODIFYUSERNAME { get; set; }
        /// <summary>
        /// WeChatID
        /// </summary>
        /// <returns></returns>
        [DisplayName("WeChatID")]
        public string WECHATID { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        //public override void Create()
        //{
        //    this.USERID = CommonHelper.GetGuid;
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
        //    this.USERID = KeyValue;
        //    this.MODIFYDATE = DateTime.Now;
        //    this.MODIFYUSERID = ManageProvider.Provider.Current().UserId;
        //    this.MODIFYUSERNAME = ManageProvider.Provider.Current().UserName;
        //}
        #endregion
    }
}