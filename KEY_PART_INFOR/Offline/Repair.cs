using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HfutIe.Offline
{
    //1.手动自动按键操作逻辑
    //2.二级关联界面
    public partial class Repair : Form
    {
        #region 0 BasicVariables
        //Basic
        public string wc_code_C = "OPA050";
        public string pro_line_name_C = "轻卡线";
        string tempCode = "17300001J000023323B5020XZ-W004";
        private Point pi;
        JAC_FrontAxleEntities ctx = new JAC_FrontAxleEntities();
        //DataSource
        P_ASSEMBLE_PRODUCT_STATE paps;
        //DataBase
        CHECK_INFOR ci;
        CHECK_INFOR_Q_DETAIL ciqd;
        CHECK_INFOR_SCREW_DETAIL cisd;
        CHECK_INFOR_S_P_DETAIL cispd;
        //DOC_PRODUCT_FAULT_INFOR dpfi;
        //DOC_PRODUCT_MT_INFOR dpmi;
        PRODUCT_FAULT_ITEM pfi;
        PRODUCT_MAINTAIN_ITEM pmi;
        DOC_ASSEMBLE_PRODUCT_STATE dsps;
        //DataBaseList
        List<P_ASSEMBLE_PRODUCT_STATE> papsL;
        List<CHECK_INFOR> ciL;
        List<CHECK_INFOR_Q_DETAIL> ciqdL;
        List<CHECK_INFOR_SCREW_DETAIL> cisdL;
        List<CHECK_INFOR_S_P_DETAIL> cispdL;
        List<DOC_PRODUCT_FAULT_INFOR> dpfiL;
        List<DOC_PRODUCT_MT_INFOR> dpmiL;
        List<PRODUCT_FAULT_ITEM> pfiL;
        List<PRODUCT_MAINTAIN_ITEM> pmiL;
        #endregion
        string a = Guid.NewGuid().ToString().ToLower();//varchar主键

        #region 1 FormConstruct
        public Repair()
        {
            InitializeComponent();
            ScannerHelper.pro_line_name = pro_line_name_C;
            ScannerHelper.wc_code = wc_code_C;
            ScannerHelper.OpenCom(scanport);
        }
        private void Repair_Load(object sender, EventArgs e)
        {
            using (var ctx = new JAC_FrontAxleEntities())
            {
                pfiL = ctx.PRODUCT_FAULT_ITEM.ToList();
                pmiL = ctx.PRODUCT_MAINTAIN_ITEM.ToList();
            }
            //SetLabel(lblArrive, true);
            LoadTree(treeViewFault);
            LoadTree(treeViewMaintain);
            dgvFaultDetail.AlternatingRowsDefaultCellStyle.BackColor = Color.Gray;
            dgvMainDetail.AlternatingRowsDefaultCellStyle.BackColor = Color.Gray;
            dgvFaultDetail.Columns["detail_describe"].ReadOnly = false;
            dgvFaultDetail.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;

        }
        private void Repair_Load()
        {
            //登录人员显示
            staffcode_txt.Text = SystemLog.UserCode;//系统登录员工编号
            staffname_txt.Text = SystemLog.UserName;//系统登录员工姓名
            DateTime dt = DateTime.Now;
            string week = dt.DayOfWeek.ToString();
            SetLableText(showweek_lbl, week);
            switch (week)
            {
                case "Monday":
                    week = "星期一";
                    break;
                case "Tuesday":
                    week = "星期二";
                    break;
                case "Wednesday":
                    week = "星期三";
                    break;
                case "Thursday":
                    week = "星期四";
                    break;
                case "Friday":
                    week = "星期五";
                    break;
                case "Saturday":
                    week = "星期六";
                    break;
                case "Sunday":
                    week = "星期日";
                    break;
                default:
                    break;

            }
            SetLableText(showweek_lbl, week);
            productborncode_txt.Clear();
            plancode_txt.Clear();
            productbatchno_txt.Clear();
            productcode_txt.Clear();
            dgvFaultDetail.Rows.Clear();
            dgvMainDetail.Rows.Clear();
            treeViewFault.Nodes.Clear();
            treeViewMaintain.Nodes.Clear();
            using (var ctx = new JAC_FrontAxleEntities())
            {
                pfiL = ctx.PRODUCT_FAULT_ITEM.ToList();
                pmiL = ctx.PRODUCT_MAINTAIN_ITEM.ToList();
            }
            //SetLabel(lblArrive, true);
            LoadTree(treeViewFault);
            LoadTree(treeViewMaintain);
            dgvFaultDetail.AlternatingRowsDefaultCellStyle.BackColor = Color.Gray;
            dgvMainDetail.AlternatingRowsDefaultCellStyle.BackColor = Color.Gray;

        }
        #endregion

        #region 2 Controls
        private void OKbtn_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("是否确定返修？", "前桥MES返修工位", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if(dr==DialogResult.OK)
            {
                Boolean result= CheckCode();
                
                if (result == true)
                {
                    Boolean isFirst = IsFirst();
                    if (isFirst == true)
                    {
                        if ((dgvFaultDetail.Rows.Count == 0) || (dgvMainDetail.Rows.Count == 0))
                        {
                            MessageBox.Show("请维护故障信息后，重新返修！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        else
                        {
                            SaveData();//保存界面数据
                                       //ClearForm();//清空界面控件
                            Repair_Load();
                            MessageBox.Show("该产品已成功返修！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("该产品已下线，请重新输入！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("该产品出生证无效，请重新输入！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            else
            {
                return;
            }
        }
        private void sizeBtn_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void closeBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void productborncode_txt_TextChanged(object sender, EventArgs e)
        {
            if (inputmodel_btn.Text=="自动")
            {
                return;
            }
            else
            {
                using (var ctx = new JAC_FrontAxleEntities())
                {
                    paps = ctx.P_ASSEMBLE_PRODUCT_STATE.Where(s => s.product_born_code == productborncode_txt.Text).FirstOrDefault();
                }
                if (paps == null)
                {
                    MessageBox.Show("当前产品出生证无关联信息，请重新输入！", "提醒", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    SetText(plancode_txt, paps.mes_plan_code);
                    SetText(productbatchno_txt, paps.product_batch_no);
                    SetText(productcode_txt, paps.product_code);
                }
            }
        }
        private void treeViewFault_MouseDown(object sender, MouseEventArgs e)
        {
            pi = new Point(e.X, e.Y);
        }
        private void treeViewMaintain_MouseDown(object sender, MouseEventArgs e)
        {
            pi = new Point(e.X, e.Y);
        }
        private void treeViewFault_DoubleClick(object sender, EventArgs e)
        {
            TreeNode node = this.treeViewFault.GetNodeAt(pi);
            if (pi.X < node.Bounds.Left || pi.X > node.Bounds.Right)
            {
                //label5.Text = "1";     //不触发事件   
                return;
            }
            else
            {
                //var A = productborncode_txt.Text;
                var paps = ctx.P_ASSEMBLE_PRODUCT_STATE.Where(s=>s.product_born_code == productborncode_txt.Text).FirstOrDefault();
                if (paps == null)
                {
                    return;
                }
                else
                { if (node.BackColor == Color.Gray)
                    {
                        MessageBox.Show("该项已选择。请勿重复点选！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    else
                    {
                        var nodeset = pfiL.Where(a => a.product_fault_item_code == node.Name).FirstOrDefault();
                        if (nodeset != null)
                        {
                            TreeToDgv(nodeset, dgvFaultDetail); //1:fault;2:maintain 
                            node.BackColor = Color.LightGray;
                            return;
                        }
                        else
                        {
                            //label5.Text = "222";     //不触发事件
                            return;
                        }
                    }
                }
            }
        }
        private void treeViewMaintain_DoubleClick(object sender, EventArgs e)
        {
            TreeNode node = this.treeViewMaintain.GetNodeAt(pi);
            if (pi.X < node.Bounds.Left || pi.X > node.Bounds.Right)
            {
                return;
            }
            else
            {
                //var A = productborncode_txt.Text;
                var paps = ctx.P_ASSEMBLE_PRODUCT_STATE.Where(s => s.product_born_code == productborncode_txt.Text).FirstOrDefault();
                if (paps == null)
                {
                    return;
                }
                else
                {
                    if (node.BackColor == Color.Gray)
                    {
                        MessageBox.Show("该项已选择。请勿重复点选！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    else
                    {
                        var nodeset = pmiL.Where(a => a.product_maintain_item_code == node.Name).FirstOrDefault();
                        if (nodeset != null)
                        {
                            TreeToDgv(nodeset, dgvMainDetail); //1:fault;2:maintain 
                            node.BackColor = Color.LightGray;
                            return;
                        }
                        else
                        {
                            return;
                        }
                    }
                }
            }
        }
        private void dgvFaultDetail_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowindex = e.RowIndex;
            PRODUCT_FAULT_ITEM pfi = new PRODUCT_FAULT_ITEM();
            using (var ctx = new JAC_FrontAxleEntities())
            {
                string a = dgvFaultDetail.Rows[rowindex].Cells["detail_product_fault_item_code"].Value.ToString();
                pfi = ctx.PRODUCT_FAULT_ITEM.Where(s => s.product_fault_item_code == a).ToList().FirstOrDefault();
            }
            FindNode(treeViewFault, pfi);
            dgvFaultDetail.Rows.RemoveAt(rowindex);

            MessageBox.Show("该项已删除！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void dgvMainDetail_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowindex = e.RowIndex;
            PRODUCT_MAINTAIN_ITEM pmi = new PRODUCT_MAINTAIN_ITEM();
            using (var ctx = new JAC_FrontAxleEntities())
            {
                string a = dgvMainDetail.Rows[rowindex].Cells["detail_product_MAINTAIN_item_code"].Value.ToString();
                pmi = ctx.PRODUCT_MAINTAIN_ITEM.Where(s => s.product_maintain_item_code == a).ToList().FirstOrDefault();
            }
            FindNode(treeViewMaintain, pmi);
            dgvMainDetail.Rows.RemoveAt(rowindex);
            MessageBox.Show("该项已删除！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion

        #region 3 Function_Void
        private void scanport_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            string barcodetype = "";
            bool IsOk = false;

            object obj = ScannerHelper.dataReceive(scanport, out barcodetype, out IsOk);
            //string barcode = obj.
            string barcode = obj.ToString().Trim();
            SetText(productborncode_txt, barcode);

            if (barcodetype == "产品出生证条形码" && IsOk)
            {
                ShowProductInfo();
                //Boolean result = IsFirst();
                //if (result == true)
                //{
                //    ShowProductInfo();
                //}
                //else
                //{
                //    MessageBox.Show("该产品已下线", "提醒", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //}
            }
            else
            {
                SetText(productborncode_txt, "产品出生证错误，请重新扫描！");
                //MessageBox.Show("非产品出生证信息", "提醒", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }


        } 
        /// <summary>
                 /// 使用委托为Label赋值
                 /// </summary>
                 /// <param name="pb">Label控件名</param>
                 /// <param name="st">要赋值的字符串</param>
        private delegate void SetLableTextDelegate(Label pb, string st);
        private void SetLableText(Label lbl, string st)
        {
            if (lbl.InvokeRequired) Invoke(new SetLableTextDelegate(SetLableText), lbl, st);
            else lbl.Text = st;
        }
        /// <summary>
        /// 使用委托为TextBox赋值
        /// </summary>
        /// <param name="txt">TextBox控件名</param>
        /// <param name="st">要赋值的字符串</param>
        private delegate void SetTextDelegate(TextBox txt, string st);
        private  Boolean  CheckCode()
        {
            Boolean result;
            using (var ctx = new JAC_FrontAxleEntities())
            {
                paps = ctx.P_ASSEMBLE_PRODUCT_STATE.Where(s => s.product_born_code == productborncode_txt.Text).FirstOrDefault();
            }
            if (paps == null)
            {
                return result = false;
            }
            else
            {
                return result = true;
            }
        }
        private Boolean IsFirst()
        {
            Boolean result;
            using (var ctx = new JAC_FrontAxleEntities())
            {
                paps = ctx.P_ASSEMBLE_PRODUCT_STATE.Where(s => s.product_born_code == productborncode_txt.Text).FirstOrDefault();
            }
            if (paps.assemble_offline_time == null)
            {
                return result = true;
            }
            else
            {
                return result = false;
            }
        }
        private void SetText(TextBox txt, string st)
        {
            if (txt.InvokeRequired) Invoke(new SetTextDelegate(SetText), txt, st);
            else txt.Text = st;
        }
        private void LoadDataDgv(List<PRODUCT_MAINTAIN_ITEM> ld, DataGridView dgv)
        {
            for (int row = 0; row < ld.Count; row++)
            {
                dgv.Rows.Add();
                dgv.Rows[row].Cells["m_num"].Value = row + 1;
                dgv.Rows[row].Cells["product_maintain_item_code"].Value = ld[row].product_maintain_item_code;
                dgv.Rows[row].Cells["product_maintain_item_name"].Value = ld[row].product_maintain_item_name;
                dgv.Rows[row].Cells["product_maintain_type_code"].Value = ld[row].product_maintain_type_code;
                dgv.Rows[row].Cells["product_maintain_type_name"].Value = ld[row].product_maintain_type_name;
                dgv.Rows[row].Cells["m_describe"].Value = ld[row].discribe;
            }
        }
        private void LoadDataDgv(List<PRODUCT_FAULT_ITEM> ld, DataGridView dgv)
        {
            for (int row = 0; row < ld.Count; row++)
            {
                dgv.Rows.Add();
                dgv.Rows[row].Cells["num"].Value = row + 1;
                dgv.Rows[row].Cells["product_fault_item_code"].Value = ld[row].product_fault_item_code;
                dgv.Rows[row].Cells["product_fault_item_name"].Value = ld[row].product_fault_item_name;
                dgv.Rows[row].Cells["product_fault_type_code"].Value = ld[row].product_fault_type_code;
                dgv.Rows[row].Cells["product_fault_type_name"].Value = ld[row].product_fault_type_name;
                dgv.Rows[row].Cells["describe"].Value = ld[row].discribe;
            }
        }
        private void FindNode(TreeView tv, PRODUCT_FAULT_ITEM pfi)
        {
            foreach (TreeNode tn in tv.Nodes)
            {
                foreach (TreeNode item in tn.Nodes)
                {
                    if (item.Name == pfi.product_fault_item_code)
                    {
                        item.BackColor = Color.FromArgb(213, 231, 243);
                        return;
                    }
                }
            }
        }
        private void FindNode(TreeView tv, PRODUCT_MAINTAIN_ITEM pmi)
        {
            foreach (TreeNode tn in tv.Nodes)
            {
                foreach (TreeNode item in tn.Nodes)
                {
                    if (item.Name == pmi.product_maintain_item_code)
                    {
                        item.BackColor = Color.FromArgb(213, 231, 243);
                        return;
                    }
                }
            }
        }
        private void SetLabel(Label lbl, Boolean result)
        {
            if (result == true)
            {
                lbl.BackColor = Color.LightGreen;
            }
            else
            {
                lbl.BackColor = Color.DarkGray;
            }
        }
        private void LoadTree(TreeView tv)
        {
            //string treeinfo = string.Empty;
            TreeNode node;
            if (tv.Name.Contains("Fault"))
            {
                //treeinfo = "Fault";
                var treedetail = pfiL;
                if (pfiL.Count > 0)
                {
                    for(int row = 0; row < pfiL.Count; row++)
                    {
                        node = new TreeNode();
                        node.Text = pfiL[row].product_fault_type_name;
                        node.Name = pfiL[row].product_fault_type_code;
                        tv.Nodes.Add(node);
                        SetSubNode(pfiL, node);
                    }
                }
                else
                {
                    MessageBox.Show("系统无故障基础信息！");
                }
            }
            else if (tv.Name.Contains("Maintain"))
            {
               // treeinfo = "Maintain";
                var treedetail = pmiL;
                if (pmiL.Count > 0)
                {
                    for (int row = 0; row < pmiL.Count; row++)
                    {
                        node = new TreeNode();
                        node.Text = pmiL[row].product_maintain_type_name;
                        node.Name = pmiL[row].product_maintain_type_code;
                        tv.Nodes.Add(node);
                        SetSubNode(pmiL, node);
                    }
                }
                else
                {
                    MessageBox.Show("系统无排故基础信息！");
                }
            }
            else
            {
                MessageBox.Show("树加载配置有误！");
                return;
            }
        }
        private void SaveData() //将故障及排故详细信息记录至数据库
        {
            
            //获取dgv对应的记录插入新表
            JAC_FrontAxleEntities ctx = new JAC_FrontAxleEntities();
            var paps = ctx.P_ASSEMBLE_PRODUCT_STATE.Where(s => s.product_born_code == productborncode_txt.Text).FirstOrDefault();
            paps.assemble_offline_time = DateTime.Now;
            ctx.Entry<P_ASSEMBLE_PRODUCT_STATE>(paps).State = EntityState.Modified;
            ctx.SaveChanges();
        }
        private void ShowProductInfo()
        {
            using (var ctx = new JAC_FrontAxleEntities())
            {
                paps = ctx.P_ASSEMBLE_PRODUCT_STATE.Where(s => s.product_born_code == productborncode_txt.Text).FirstOrDefault();
            }
            if (paps == null)
            {
                MessageBox.Show("当前产品出生证无关联信息，请重新输入！", "提醒", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                SetText(plancode_txt, paps.mes_plan_code);
                SetText(productbatchno_txt, paps.product_batch_no);
                SetText(productcode_txt, paps.product_code);
            }
        }
        private void ClearForm()
        {
            productborncode_txt.Clear();
            plancode_txt.Clear();
            productbatchno_txt.Clear();
            productcode_txt.Clear();
            dgvFaultDetail.Rows.Clear();
            dgvMainDetail.Rows.Clear();
        }
        private void SetSubNode(List<PRODUCT_FAULT_ITEM> pfiL,TreeNode node)
        {
            TreeNode newnode;
            var nodeset = pfiL.Where(a => a.product_fault_type_code == node.Name).ToList();
            if (nodeset.Count > 0)
            {
                for(int row =0;row< nodeset.Count; row++)
                {
                    newnode = new TreeNode();
                    newnode.Text = pfiL[row].product_fault_item_name;
                    newnode.Name = pfiL[row].product_fault_item_code;
                    node.Nodes.Add(newnode);
                }
            }
            else
            {
                return;
            }
        }
        private void SetSubNode(List<PRODUCT_MAINTAIN_ITEM> pmiL, TreeNode node)
        {
            TreeNode newnode;
            var nodeset = pmiL.Where(a => a.product_maintain_type_code == node.Name).ToList();
            if (nodeset.Count > 0)
            {
                for (int row = 0; row < nodeset.Count; row++)
                {
                    newnode = new TreeNode();
                    newnode.Text = pmiL[row].product_maintain_item_name;
                    newnode.Name = pmiL[row].product_maintain_item_code;
                    node.Nodes.Add(newnode);
                }
            }
            else
            {
                return;
            }
        }
        private void TreeToDgv(PRODUCT_FAULT_ITEM ld, DataGridView dgv)
        {
            dgv.Rows.Add();
            dgv.Rows[dgv.Rows.Count - 1].Cells["detail_m"].Value = dgv.Rows.Count ;
            dgv.Rows[dgv.Rows.Count - 1].Cells["detail_product_fault_item_code"].Value = ld.product_fault_item_code;
            dgv.Rows[dgv.Rows.Count - 1].Cells["detail_product_fault_item_name"].Value = ld.product_fault_item_name;
            dgv.Rows[dgv.Rows.Count - 1].Cells["detail_product_fault_type_code"].Value = ld.product_fault_type_code;
            dgv.Rows[dgv.Rows.Count - 1].Cells["detail_product_fault_type_name"].Value = ld.product_fault_type_name;
            //dgv.Rows[dgv.Rows.Count - 1].Cells["detail_describe"].Value = ld.discribe;
         }
        private void TreeToDgv(PRODUCT_MAINTAIN_ITEM ld,DataGridView dgv)
        {
            dgv.Rows.Add();
            dgv.Rows[dgv.Rows.Count - 1].Cells["detail_m_num"].Value = dgv.Rows.Count;
            dgv.Rows[dgv.Rows.Count - 1].Cells["detail_product_maintain_item_code"].Value = ld.product_maintain_item_code;
            dgv.Rows[dgv.Rows.Count - 1].Cells["detail_product_maintain_item_name"].Value = ld.product_maintain_item_name;
            dgv.Rows[dgv.Rows.Count - 1].Cells["detail_product_maintain_type_code"].Value = ld.product_maintain_type_code;
            dgv.Rows[dgv.Rows.Count - 1].Cells["detail_product_maintain_type_name"].Value = ld.product_maintain_type_name;
            //dgv.Rows[dgv.Rows.Count - 1].Cells["detail_m_describe"].Value = ld.discribe;
        }

        private void inputmodel_btn_Click(object sender, EventArgs e)
        {
            if(productborncode_txt.ReadOnly == true)
            {
                //ClearForm();
                Repair_Load();
                productborncode_txt.ReadOnly = false;
                productborncode_txt.Focus();
                confirm_btn.Enabled = true;
                ScannerHelper.CloseCom(scanport);
                inputmodel_btn.Text = "自动输入";
            }
            else
            {
                //ClearForm();
                Repair_Load();
                productborncode_txt.ReadOnly = true;
                productborncode_txt.Text="请扫描产品条码";
                confirm_btn.Enabled = false;
                ScannerHelper.OpenCom(scanport);
                inputmodel_btn.Text = "手动输入";
            }
        }

        private void confirm_btn_Click(object sender, EventArgs e)
        {
            using (var ctx = new JAC_FrontAxleEntities())
            {
                paps = ctx.P_ASSEMBLE_PRODUCT_STATE.Where(s => s.product_born_code == productborncode_txt.Text).FirstOrDefault();
            }
            if (paps == null)
            {
                MessageBox.Show("当前产品出生证无关联信息，请重新输入！", "提醒", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                if (paps.assemble_offline_time == null) {
                SetText(plancode_txt, paps.mes_plan_code);
                SetText(productbatchno_txt, paps.product_batch_no);
                SetText(productcode_txt, paps.product_code);
                }
                else
                {
                    MessageBox.Show("该产品已下线，请重新输入！", "提醒", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
        }
        #endregion

        private void dgvFaultDetail_MouseClick(object sender, MouseEventArgs e)
        {
           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://192.168.1.3:8095/Home/AccordionIndex?ModuleId=d19e5476-9c37-4f5c-96bd-fe74cc8a62d1&Url=EquipmentModule/EQUIP_FAULT_ITEM/Index&FullName=故障库管理&Png=lightbulb_off.png");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://192.168.1.3:8095/Home/AccordionIndex?ModuleId=3ec8ea54-a85b-4351-b6d5-dbf47dce8633&Url=EquipmentModule/EQUIP_MAINTAIN_ITEM/Index&FullName=排故库管理&Png=lighthouse.png");
        }

        private void recordtime_tmr_Tick(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            string time = dt.Year.ToString().Trim() + "-" + dt.Month.ToString("00").Trim() + "-" + dt.Day.ToString("00").Trim() + "  " + dt.Hour.ToString("00").Trim() + ":" + dt.Minute.ToString("00").Trim() + ":" + dt.Second.ToString("00").Trim();
            SetLableText(showtime_lbl, time);
        }
    }
}


