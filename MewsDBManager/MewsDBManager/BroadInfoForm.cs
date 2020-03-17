using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Mews.Svr.DBMng
{
    public partial class BroadInfoForm : Form
    {
        public BroadInfoForm()
        {
            InitializeComponent();
        }

        public BroadInfoForm(string _hist)
        {
            InitializeComponent();

            this.label2.Text = _hist;
        }

        private void BroadInfoForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}