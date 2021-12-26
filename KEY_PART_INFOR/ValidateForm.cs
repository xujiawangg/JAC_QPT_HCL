using HfutIe.DataAccess.Common;
using HfutIE.Entity;
using HfutIE.Repository;
using MsgBox;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HfutIe.Online
{
    /// <summary>
    /// 操作验证窗口
    /// </summary>
    /// version:V1.0
    /// author:储著求
    /// date:20200104
    public partial class ValidateForm : Form
    {
        #region 变量
        private List<string> CharacterList;//验证角色列表
        private string UserName;//用户名
        private string UserPassWord;//密码
        #endregion

        #region 仓储
        public static Repository<BASE_USER> BASE_USER_Repository = new Repository<BASE_USER>();//用户
        #endregion

        #region 初始化
        /// <summary>
        /// 操作验证窗口
        /// </summary>
        /// <param name="moduleCode">底层模块编号（与【系统模块】中一致）</param>
        /// <param name="buttonCode">底层按钮编号（与【系统按钮】一致）</param>
        /// <param name="buttonName">窗口标题文字（默认为“操作权限验证”）</param>
        public ValidateForm(string moduleCode, string buttonCode,string buttonName="操作权限验证")
        {
            List<string> Character = getRolesByButton(buttonCode, moduleCode);
            #region 验证角色初始化
            //2.若不为空，则按非空集合初始化
            CharacterList = Character;
            #endregion
            InitializeComponent();
            title_lbl.Text = buttonName;
            #region 未配置角色提示
            //1.若数组为空，默认为班组长权限
            if (CharacterList == null || CharacterList.Count == 0)
            {
                tip_lbl.Text = "该按钮未配置可操作角色，请联系IT！";
            }
            #endregion
        }
        #endregion

        #region 取消操作
        private void Cancel()
        {
            this.Close();
        }

        /// <summary>
        /// 取消按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            Cancel();
        }

        /// <summary>
        /// 关闭按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Exit_pic_Box_Click(object sender, EventArgs e)
        {
            Cancel();
        }
        #endregion

        #region 确认操作
        /// <summary>
        /// 确认按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void confrim_btn_Click(object sender, EventArgs e)
        {
            Vaildate();
        }
        private void Vaildate()
        {
            try
            {
                #region 0.未配置角色提示
                //1.若数组为空，默认为班组长权限
                if (CharacterList == null || CharacterList.Count == 0)
                {
                    tip_lbl.Text = "该按钮未配置可操作角色，请联系IT！";
                    return;
                }
                #endregion
                #region 1.验证信息是否完整
                UserName = UserName_txted.Text.Trim();//用户名
                UserPassWord = Password_txted.Text.Trim();//密码
                if (UserName == "") { userTip_lbl.Text = "请输入用户名！"; return; }
                if (UserPassWord == "") { passwordTip_lbl.Text = "请输入密码！"; return; }
                #endregion
                #region 2.按用户名查找到该用户 
                string sqlString = $"SELECT * FROM BASE_USER WHERE ACCOUNT = '{UserName}'";
                BASE_USER cur_user = BASE_USER_Repository.FindEntityBySql(sqlString);
                #endregion
                #region 3.验证用户名,密码
                if (cur_user != null && cur_user.ACCOUNT != null && cur_user.ACCOUNT != "")
                {//存在此用户
                    #region 加密输入的密码
                    string Enpassword = Md5Helper.MD5_1(UserPassWord);
                    string EnPassword = Md5Helper.MD5(DESEncrypt.Encrypt(Enpassword.ToLower(), cur_user.SECRETKEY).ToLower(), 32).ToLower();
                    #endregion
                    if (cur_user.PASSWORD == EnPassword)
                    {//用户名，密码均正确
                        #region 4. 验证用户权限
                        string sqlRoleString = $"SELECT FULLNAME FROM  (SELECT * FROM BASE_OBJECTUSERRELATION O,BASE_ROLES R WHERE O.OBJECTID = R.ROLEID  AND O.USERID = '{cur_user.USERID}' AND O.CATEGORY = 2)";
                        DataTable dtRoles = BASE_USER_Repository.FindTableBySql(sqlRoleString);//查找出该用户的所有角色
                        if (dtRoles != null)
                        {
                            for (int i = 0; i < dtRoles.Rows.Count; i++)
                            {
                                string role = dtRoles.Rows[i][0].ToString();
                                if (CharacterList.Contains(role))
                                {
                                    tip_lbl.Text = "验证成功！正在执行操作。。。";
                                    #region 触发验证成功后续操作
                                    this.DialogResult = DialogResult.OK;//返回成功
                                    Cancel();//关闭窗口
                                    MyMsgBox.Show("验证成功！正在执行操作。。。", "信息提示", MyMsgBox.MyButtons.OK, MyMsgBox.MyIcon.Error, 3);
                                    #endregion
                                }
                            }
                            tip_lbl.Text = "权限不足！";
                            return;
                        }
                        else
                        {
                            tip_lbl.Text = "权限不足！";
                        }
                        #endregion
                    }
                    else
                    {
                        passwordTip_lbl.Text = "密码错误！";
                    }
                }
                else
                {
                    userTip_lbl.Text = "用户名错误！";
                }
                #endregion
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 清空提示
        private void UserName_txted_EditValueChanged(object sender, EventArgs e)
        {
            userTip_lbl.Text = "";
            tip_lbl.Text = "";
        }

        private void Password_txted_EditValueChanged(object sender, EventArgs e)
        {
            passwordTip_lbl.Text = "";
            tip_lbl.Text = "";
        }
        #endregion

        #region 根据按钮找到操作角色
        public List<string> getRolesByButton(string buttonCode,string ModelCode)
        {
            string SqlString = $"SELECT R.FULLNAME FROM BASE_BUTTON B, BASE_BUTTONPERMISSION P,BASE_ROLES R,BASE_MODULE M WHERE B.BUTTONID = P.MODULEBUTTONID AND R.ROLEID = P.OBJECTID AND P.MODULEID = M.MODULEID AND B.CODE = '{buttonCode}'  AND  M.FCODE = '{ModelCode}'";
            DataTable btn_dt = BASE_USER_Repository.FindTableBySql(SqlString);
            List<string> roleList = new List<string>();
            if (btn_dt != null && btn_dt.Rows.Count > 0)
            {
                for (int i = 0; i < btn_dt.Rows.Count; i++)
                {
                    roleList.Add(btn_dt.Rows[i][0].ToString());
                }
            }
            return roleList;
        }
        #endregion
    }
}
