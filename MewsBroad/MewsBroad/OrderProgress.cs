using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Mews.Svr.Broad
{
    public partial class OrderProgress : UserControl
    {
        #region Fields
        private System.Windows.Forms.Timer timer = null;
        #endregion

        public OrderProgress()
        {
            InitializeComponent();

            timer = new System.Windows.Forms.Timer();
            timer.Interval = 1000;
            timer.Tick += new EventHandler(timer_Tick);
            timer.Stop();
        }

        public void OrderStart(object info)
        {
            progressBar.Value = 0;
            lbTime.Text = "0 / 0 " + LangPack.GetSec();

            if (info is SirenInfo)
            {
                SirenInfo sInfo = info as SirenInfo;

                progressBar.Maximum = sInfo.SirenTime;
                lbKind.Text = "[SIREN] " + sInfo.SirenName;
            }
            else
            {
                MsgInfo mInfo = info as MsgInfo;

                progressBar.Maximum = (mInfo.msgTime * int.Parse(mInfo.msgReptCtn));
                lbKind.Text = LangPack.GetProgressMsg() + mInfo.msgName;
            }

            this.timer.Start();
        }

        public void OrderEnd()
        {
            this.timer.Stop();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (progressBar.Value >= progressBar.Maximum)
                {
                    lbTime.Text = progressBar.Maximum.ToString() + " / " + progressBar.Maximum.ToString() + " " + LangPack.GetSec();
                    this.timer.Stop();
                }
                else
                {
                    progressBar.Value++;
                    lbTime.Text = progressBar.Value.ToString() + " / " + progressBar.Maximum.ToString() + " " + LangPack.GetSec();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("OrderProgress.timer_Tick - " + ex.Message);
            }
        }
    }
}