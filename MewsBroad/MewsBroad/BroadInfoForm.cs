using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Mews.Svr.Broad
{
    public partial class BroadInfoForm : Form
    {
        public BroadInfoForm()
        {
            InitializeComponent();
        }

        public BroadInfoForm(string _hist, string _isPrimary)
        {
            InitializeComponent();

            this.label2.Text = _hist;
            
            if (_isPrimary == "1")
            {
                this.panel1.BackgroundImage = MewsBroad.Properties.Resources.bgInfoVer10;
            }
            else
            {
                this.panel1.BackgroundImage = MewsBroad.Properties.Resources.bgInfoVer10_green;
            }
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