using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using DevExpress.XtraGrid.Views.Grid;
using System.Data;

namespace HfutIe
{
    public static class Extension
    {
        #region Label

        public static void SetText(this Label lbl, string text)
        {
            lbl.Invoke(new MethodInvoker(() => { lbl.Text = text; }));
        }
        public static void SetText(DevExpress.XtraEditors.LabelControl Lable, string text)
        {
            Lable.Invoke(new MethodInvoker(() => { Lable.Text = text; }));
        }
        public static void SetText(this Label lbl, string text, Color backColor)
        {
            lbl.Invoke(new MethodInvoker(() => { lbl.Text = text; lbl.BackColor = backColor; }));
        }

        public static void SetText(this Label lbl, Color backColor)
        {
            lbl.Invoke(new MethodInvoker(() => {lbl.BackColor = backColor; }));
        }
        public static void SetText(this Label lbl, BorderStyle bs)
        {
            lbl.Invoke(new MethodInvoker(() => {lbl.BorderStyle  = bs; }));
        }

        #endregion

        #region TextBox

        public static void SetText(this TextBox tb, string text)
        {

            tb.Invoke(new MethodInvoker(() => { tb.Text = text; }));
        }

        public static void AppendText(this TextBox tb, string text)
        {
            tb.Invoke(new MethodInvoker(() => { tb.Text += text; }));
        }
        public static void SetFocus(this TextBox tb)
        {
            tb.Invoke(new MethodInvoker(() => { tb.Focus (); }));
        }

        #endregion

        #region PictureBox
        public static void SetPicture(this PictureBox pb, string fileroute)
        {
            pb.Invoke(new MethodInvoker(() => { pb.ImageLocation = fileroute; }));
        }
        public static void SetPicture(DevExpress.XtraEditors.PictureEdit pb, string path)
        {
            pb.Invoke(new MethodInvoker(() => { pb.Image = Image.FromFile(path); }));
        }
        #endregion

        #region DataGridView

        public static void AddRow(this DataGridView grid, params object[] row)
        {
            grid.Invoke(new MethodInvoker(() => { grid.Rows.Add(row); }));
        }

        public static void InsertRow(this DataGridView grid, int index, params object[] row)
        {
            grid.Invoke(new MethodInvoker(() => { grid.Rows.Insert(index, row); }));
        }
        public static void SetRowColor(this DataGridView grid, int i, Color backcolor)
        {
            grid.Invoke(new MethodInvoker(() => { grid.Rows[i].DefaultCellStyle.ForeColor = backcolor; }));
        }
        public static void ClearSelectRow(this DataGridView grid)
        {
            grid.Invoke(new MethodInvoker(() => { grid.ClearSelection(); }));
        }
        public static void ClearRows(this DataGridView grid)
        {
            grid.Invoke(new MethodInvoker(() => { grid.Rows.Clear(); }));
        }

        #endregion
        public static void SetDataSource(DevExpress.XtraGrid.GridControl grid, DataTable dt)
        {
            grid.Invoke(new MethodInvoker(() => { grid.DataSource = dt; }));
        }
        #region FlowLayoutPanel

        public static void Enable(this FlowLayoutPanel panel)
        {
            panel.Invoke(new MethodInvoker(() => { panel.Enabled = true; }));
        }

        public static void Disable(this FlowLayoutPanel panel)
        {
            panel.Invoke(new MethodInvoker(() => { panel.Enabled = false; }));
        }

        #endregion

        #region PulseButton
        public static void Set_ColorBottom(this PulseButton.PulseButton pb, Color color)
        {
            pb.Invoke(new MethodInvoker(() => { pb.ButtonColorBottom = color; }));

        }
        public static void Set_ColorTop(this PulseButton.PulseButton pb, Color color)
        {
            pb.Invoke(new MethodInvoker(() => { pb.ButtonColorTop = color; }));

        }
        public static void Set_ForeColor(this PulseButton.PulseButton pb, Color color)
        {
            pb.Invoke(new MethodInvoker(() => { pb.ForeColor = color; }));

        }
        public static void Set_PulseColor(this PulseButton.PulseButton pb, Color color)
        {
            pb.Invoke(new MethodInvoker(() => { pb.PulseColor = color; }));

        }
        #endregion
    }
}
