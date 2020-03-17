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
    public partial class BoardViewForm : Form
    {
        #region Fields
        private string text = string.Empty;
        private System.Windows.Forms.Timer timer = null;
        private string brdText = string.Empty;
        //private bool bBlink = false;
        #endregion

        public BoardViewForm()
        {
            InitializeComponent();
            InitLang();
        }

        public BoardViewForm(string text, bool bBlink, string _isPrimary)
            : this()
        {
            this.text = text;
            //this.bBlink = bBlink;
            pnlHide.Visible = false;

            timer = new Timer();
            timer.Interval = 500;
            timer.Tick += new EventHandler(timer_Tick);
            timer.Stop();

            if (_isPrimary == "1")
            {
                this.panel1.BackgroundImage = MewsBroad.Properties.Resources.bgTitle;
            }
            else
            {
                this.panel1.BackgroundImage = MewsBroad.Properties.Resources.bgTitleGreen;
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            this.timer.Stop();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (pnlHide.Visible)
                    pnlHide.Visible = false;
                else
                    pnlHide.Visible = true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("BoardViewForm.timer_Tick - " + ex.Message);
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            ViewText();
        }

        private void ViewText()
        {
            tbText.Text = "";;

            string subStr = this.text + "{END}";
            SearchColor(subStr);
        }

        private void SearchColor(string str)
        {
            bool bSuccess = true;

            try
            {
                int loop = 0;

                Color color = Color.White;
                bool bFore = true;

                while (true)
                {
                    loop++;
                    if (loop > 100)
                    {
                        bSuccess = false;
                        break;
                    }

                    int indexS = str.IndexOf('{');
                    int indexE = str.IndexOf('}');

                    if (indexS == -1)
                    {
                        bSuccess = false;
                        break;
                    }

                    AddData(str.Substring(0, indexS), color, bFore);

                    string stColor = str.Substring(indexS, indexE - indexS + 1);
                    if (stColor == "{END}")
                    {
                        bSuccess = true;
                        break;
                    }

                    // ForeColor
                    bFore = true;
                    color = GetForeColor(stColor);
                    str = str.Substring(indexE + 1);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(LangPack.GetMongolian("Format is invalid."), this.Name, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                bSuccess = false;
            }

            if (!bSuccess)
                this.Close();

            brdText = tbText.Text;

            //if(this.bBlink)
            //    timer.Start();
        }

        private void AddData(string text, Color color, bool bFore)
        {
            tbText.Focus();
            tbText.SelectionStart = tbText.TextLength;

            if (bFore)
                tbText.SelectionColor = color;
            else
                tbText.SelectionBackColor = color;

            tbText.AppendText(text);
            tbText.Focus();

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private Color GetForeColor(string stColor)
        {
            if (stColor == "{R}")
                return Color.Red;
            else if (stColor == "{G}")
                return Color.Green;
            else if (stColor == "{Y}")
                return Color.Yellow;
            else if (stColor == "{B}")
                return Color.Blue;
            else if (stColor == "{W}")
                return Color.White;
            else
                return Color.White;
        }

        private Color GetBackColor(string stColor)
        {
            if (stColor == "{r}")
                return Color.Red;
            else if (stColor == "{g}")
                return Color.Green;
            else if (stColor == "{y}")
                return Color.Yellow;
            else if (stColor == "{b}")
                return Color.Blue;
            else if (stColor == "{w}")
                return Color.White;
            else
                return Color.White;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            this.timer.Start();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            this.pnlHide.Visible = false;
            this.timer.Stop();
        }

        private void InitLang()
        {
            btnClose.Text = LangPack.GetClose();

            if (LangPack.IsEng)
            {
                label6.Text = "CG Preview";
            }
            else
            {
                label6.Text = "Текст урьдчилан харах";
            }
        }
    }
}