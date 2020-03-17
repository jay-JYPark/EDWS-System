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
    public partial class SirenEditForm : Form
    {
        #region Fields
        private SirenDataMng sirenMng = null;
        #endregion

        public SirenEditForm()
        {
            InitializeComponent();

            sirenMng = SirenDataMng.GetSirenMng();
            Init();
        }

        private void Init()
        {
            #region Init Ctrl
            for(int i = 0; i<20; i++)
            {
                cbNumber.Items.Add((i + 1).ToString());
            }
            cbNumber.SelectedIndex = 0;
            #endregion
        }

        private void cbNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sirenNum = (cbNumber.SelectedIndex + 1).ToString();

            SirenInfo s = sirenMng.dicSiren[sirenNum];
            tbName.Text = s.SirenName;
            tbText.Text = s.SirenContext;
            numSec.Value = s.SirenTime;
        }

        private void btnSve_Click(object sender, EventArgs e)
        {
            string sirenNum = (cbNumber.SelectedIndex + 1).ToString();

            sirenMng.dicSiren[sirenNum].SirenName = tbName.Text;
            sirenMng.dicSiren[sirenNum].SirenContext = tbText.Text;
            sirenMng.dicSiren[sirenNum].SirenTime = Convert.ToInt32(numSec.Value);

            sirenMng.SaveSirenData();
            MessageBox.Show(LangPack.GetMongolian("Modified."), LangPack.GetMongolian(this.Text), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}