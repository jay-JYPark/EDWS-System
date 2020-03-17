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
    public partial class LoginForm : Form
    {
        #region Fields
        private string stPw = "EDWS";
        private bool bOk = false;
        #endregion

        public LoginForm(string _isPrimaryCG)
        {
            InitializeComponent();
            InitLang();

            if (_isPrimaryCG == "1")
            {
                this.panel1.BackgroundImage = MewsBroad.Properties.Resources.bgTitle;
            }
            else
            {
                this.panel1.BackgroundImage = MewsBroad.Properties.Resources.bgTitleGreen;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            OnLogin();
        }

        private void OnLogin()
        {
            if (tbPw.Text.ToUpper() == stPw)
            {
                bOk = true;
                this.Close();
            }
            else
            {
                MessageBox.Show(LangPack.GetMongolian("Password error."), LangPack.GetMongolian(this.Name), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            if(bOk)
                DialogResult = System.Windows.Forms.DialogResult.OK;
            else
                DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void tbPw_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                OnLogin();
        }

        private void LoginForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.Close();
        }

        private void InitLang()
        {
            if (LangPack.IsEng)
            {
                label8.Text = "Input Password";
                btnOk.Text = "OK";
                btnCancel.Text = "Cancel";
            }
            else
            {
                label8.Text = "Нууц дугаар оруулах";
                btnOk.Text = "Зөвшөөрөх";
                btnCancel.Text = "Цуцлах";
            }
        }
    }
}
