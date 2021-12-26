using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace HfutIe
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label2.Top -= 4;
            if (label2.Top<0)
            {
                label2.Top = groupBox1.Height;
            }
        }

        private void About_Load(object sender, EventArgs e)
        {

        }
    }
}