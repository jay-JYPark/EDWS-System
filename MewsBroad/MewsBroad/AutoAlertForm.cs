using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using Mews.Lib.Mewsprt;

namespace Mews.Svr.Broad
{
    public partial class AutoAlertForm : Form
    {
        #region Fields
        private delegate void CountEndEventArgsHandler(object sender, CountEndEventArgs ceea);
        private delegate void CloseEventArgsHandler(object sender, CloseEventArgs cea);
        public event EventHandler<CountEndEventArgs> OnCountEndEvt;
        public event EventHandler<CloseEventArgs> OnCloseEvt;
        public event EventHandler<CountEndEventArgs> OnCancleEvt;
        private Thread timeTD = null;
        private int tmpTime = 0;
        private PrtCmd24 p24 = null;
        #endregion

        public AutoAlertForm(string _isPrimary)
        {
            InitializeComponent();
            InitLang();

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

            if (this.OnCloseEvt != null)
            {
                this.OnCloseEvt(this, new CloseEventArgs());
            }
        }

        /// <summary>
        /// 즉시 방송 버튼 클릭 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnImd_Click(object sender, EventArgs e)
        {
            if (this.OnCountEndEvt != null)
            {
                this.OnCountEndEvt(this, new CountEndEventArgs(this.p24));
            }

            try
            {
                if (this.timeTD != null)
                {
                    this.timeTD.Abort();
                    this.timeTD = null;
                }
            }
            catch (Exception ex)
            {
            }

            this.btnImd.Enabled = false;
            this.btnImd.Visible = false;
            this.btnCancel.Text = LangPack.GetClear();
        }

        /// <summary>
        /// 취소 버튼 클릭 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (this.timeTD != null)
            {
                this.timeTD.Abort();
                this.timeTD = null;
            }

            string tmpStr = LangPack.GetMongolian("Want to cancel the auto alert?");

            if (this.btnCancel.Text == LangPack.GetClear())
            {
                tmpStr = LangPack.GetMongolian("Want to cancel the auto alert?");

                if (MessageBox.Show(tmpStr, this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    if (this.OnCancleEvt != null)
                    {
                        this.OnCancleEvt(this, new CountEndEventArgs(this.p24));
                    }

                    this.Close();
                }
            }
            else
            {
                if (MessageBox.Show(tmpStr, this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    this.tmpTime = (int.Parse(this.label13.Text.ToString()) + 1);
                    this.timeTD = new Thread(timeTDMethod);
                    this.timeTD.IsBackground = true;
                    this.timeTD.Start();
                }
                else
                {
                    this.Close();
                }
            }
        }

        /// <summary>
        /// init 메소드
        /// </summary>
        /// <param name="_p24"></param>
        public void setInit(PrtCmd24 _p24)
        {
            this.p24 = _p24;
            this.label5.Text = (_p24.DisMode == 0) ? LangPack.GetTest() : (_p24.DisMode == 1) ? LangPack.GetReal() : LangPack.GetDrill();
            this.label6.Text = (_p24.DisValue * 0.01).ToString();

            if (Util.autoInfo.intensity < (_p24.DisValue * 0.01)) //high
            {
                this.label12.Text = (Util.autoInfo.highCBSUse == true) ? LangPack.GetUse() : LangPack.GetNonuse();
                this.label11.Text = Util.autoInfo.highMsg;

                if (Util.autoInfo.highAuto)
                {
                    this.label13.Text = "0";
                    tmpTime = 0;
                    this.label4.Text = LangPack.GetAuto();
                    this.btnCancel.Text = LangPack.GetClear();
                }
                else
                {
                    this.label13.Text = Util.autoInfo.highTime.ToString();
                    tmpTime = Util.autoInfo.highTime;
                    this.label4.Text = LangPack.GetManual();
                }
            }
            else //low
            {
                this.label12.Text = (Util.autoInfo.lowCBSUse == true) ? LangPack.GetUse() : LangPack.GetNonuse();
                this.label11.Text = Util.autoInfo.lowMsg;

                if (Util.autoInfo.lowAuto)
                {
                    this.label13.Text = "0";
                    tmpTime = 0;
                    this.label4.Text = LangPack.GetAuto();
                    this.btnCancel.Text = LangPack.GetClear();
                }
                else
                {
                    this.label13.Text = Util.autoInfo.lowTime.ToString();
                    tmpTime = Util.autoInfo.lowTime;
                    this.label4.Text = LangPack.GetManual();
                }
            }

            if (tmpTime != 0)
            {
                this.timeTD = new Thread(timeTDMethod);
                this.timeTD.IsBackground = true;
                this.timeTD.Start();
            }

            if (this.label4.Text == LangPack.GetAuto())
            {
                this.btnImd.Enabled = false;
                this.btnImd.Visible = false;
            }
            else
            {
                this.btnImd.Enabled = true;
            }
        }

        public void timeTDMethod()
        {
            while (true)
            {
                if (this.tmpTime > 0)
                {
                    MethodInvoker set = delegate()
                    {
                        this.label13.Text = (--this.tmpTime).ToString();
                    };

                    if (this.InvokeRequired)
                    {
                        this.Invoke(set);
                    }
                    else
                    {
                        set();
                    }
                }

                if (this.tmpTime == 0)
                {
                    MethodInvoker btnSet = delegate()
                    {
                        this.btnImd.Enabled = false;
                        this.btnImd.Visible = false;
                        this.btnCancel.Text = LangPack.GetClear();
                    };

                    if (this.InvokeRequired)
                    {
                        this.Invoke(btnSet);
                    }
                    else
                    {
                        btnSet();
                    }

                    if (this.OnCountEndEvt != null)
                    {
                        this.OnCountEndEvt(this, new CountEndEventArgs(this.p24));
                    }

                    try
                    {
                        this.timeTD.Abort();
                    }
                    catch (Exception ex)
                    {
                        this.timeTD = null;
                    }
                    break;
                }

                Thread.Sleep(1000);
            }
        }

        private void InitLang()
        {
            btnCancel.Text = LangPack.GetCancel();
            btnClose.Text = LangPack.GetClose();

            if (LangPack.IsEng)
            {
                label8.Text = "Auto Alert";
                groupBox1.Text = "Auto Alert Information";
                label2.Text = "Mode";
                label9.Text = "Auto Alert";
                label1.Text = "Intensity";
                label10.Text = "Send to CBS";
                label3.Text = "Stored Message";
                btnImd.Text = "Immediately Alert";
                lbSec.Text = "(sec)";
            }
            else
            {
                label8.Text = "Автомат түгшүүр";
                groupBox1.Text = "Автомат түгшүүрийн мэдээлэл";
                label2.Text = "Горим";
                label9.Text = "Түгшүүр";
                label1.Text = "хүчдэл";
                label10.Text = "Масс мессеж систем";
                label3.Text = "Хадгалагдсан мэдээлэл";
                btnImd.Text = "Яаралтай түгшүүр";
                lbSec.Text = "(Сек)";
            }
        }
    }

    /// <summary>
    /// 카운트 종료 시 발생하는 이벤트 아규먼트 클래스
    /// </summary>
    public class CountEndEventArgs : EventArgs
    {
        private PrtCmd24 p24;

        public PrtCmd24 P24
        {
            get { return this.p24; }
            set { this.p24 = value; }
        }

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="_state"></param>
        public CountEndEventArgs(PrtCmd24 _p24)
        {
            this.p24 = _p24;
        }
    }

    /// <summary>
    /// 종료 시 발생하는 이벤트 아규먼트 클래스
    /// </summary>
    public class CloseEventArgs : EventArgs
    {
        public CloseEventArgs()
        {
        }
    }
}