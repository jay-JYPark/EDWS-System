using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Adeng.Framework.Io;

namespace Mews.Svr.Broad
{
    public partial class StMsgEditForm : Form
    {
        private delegate void StoredMsgChangedEventArgsHandler(object sender, StoredMsgChangedEventArgs smcea);
        public event EventHandler<StoredMsgChangedEventArgs> OnStoMsgChangedEvt;

        #region Fields
        private StMsgDataMng msgMng = null;
        private string msgNum = string.Empty;
        private bool bAdd = false;
        #endregion

        public StMsgEditForm()
        {
            InitializeComponent();
        }

        public StMsgEditForm(string msgNum, string _isPrimary)
            : this()
        {
            InitLang();

            this.msgMng = StMsgDataMng.GetMsgMng();
            this.msgNum = msgNum;

            if (this.msgNum == "Add")
            {
                bAdd = true;
                numNo.Select();
                numNo_ValueChanged(this, new EventArgs());
            }
            else
            {
                ViewData(msgNum);
                bAdd = false;
                tbName.Select();
            }

            if (_isPrimary == "1")
            {
                this.panel1.BackgroundImage = MewsBroad.Properties.Resources.bgTitle;
            }
            else
            {
                this.panel1.BackgroundImage = MewsBroad.Properties.Resources.bgTitleGreen;
            }
        }

        private void ViewData(string Num)
        {
            if (!msgMng.dicMsg.ContainsKey(Num))
            {
                ResetData();
                return;
            }

            bAdd = true;
            numNo.Value = Convert.ToDecimal(Num);

            tbName.Text = msgMng.dicMsg[Num].msgName;
            tbText.Text = msgMng.dicMsg[Num].msgText;
            numSec.Value = Convert.ToDecimal(msgMng.dicMsg[Num].msgTime);
        }

        private void ResetData()
        {
            tbName.Text = "";
            tbText.Text = "";
            numSec.Value = 1;
        }

        private void btnSve_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbName.Text))
            {
                MessageBox.Show(LangPack.GetMongolian("Please enter the name."), LangPack.GetMongolian(this.Text), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbName.Focus();
                return;
            }

            if (string.IsNullOrEmpty(tbText.Text))
            {
                MessageBox.Show(LangPack.GetMongolian("Please enter message Text."), LangPack.GetMongolian(this.Text), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbText.Focus();
                return;
            }

            string stNum = numNo.Value.ToString();
            if (msgMng.dicMsg.ContainsKey(stNum))
            {
                if (bAdd)
                {
                    if (DialogResult.No == MessageBox.Show(LangPack.GetMongolian("Message already exists. Want to overwrite it?"), LangPack.GetMongolian(this.Text), MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                        return;
                }

                msgMng.dicMsg[stNum].msgName = tbName.Text;
                msgMng.dicMsg[stNum].msgText = tbText.Text;
                msgMng.dicMsg[stNum].msgTime = Convert.ToInt32(numSec.Value);

                MessageBox.Show(LangPack.GetMongolian("Modified."), LangPack.GetMongolian(this.Text), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MsgInfo msg = new MsgInfo();
                msg.msgNum = stNum;
                msg.msgName = tbName.Text;
                msg.msgText = tbText.Text;
                msg.msgTime = Convert.ToInt32(numSec.Value);
                msgMng.dicMsg.Add(msg.msgNum, msg);

                MessageBox.Show(LangPack.GetMongolian("Stored."), LangPack.GetMongolian(this.Text), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            //저장메시지 추가 및 수정했음..이 밑에서 이벤트로 전송
            if (this.OnStoMsgChangedEvt != null)
            {
                this.OnStoMsgChangedEvt(this, new StoredMsgChangedEventArgs());
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            msgMng.SaveMsgNameData();
        }

        private void StMsgEditForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.Close();
        }

        private void numNo_ValueChanged(object sender, EventArgs e)
        {
            ViewData(numNo.Value.ToString());
        }

        private void InitLang()
        {
            label2.Text = LangPack.GetName();
            label1.Text = LangPack.GetNumber();
            label3.Text = LangPack.GetText();
            label4.Text = LangPack.GetTime();
            label6.Text = LangPack.GetSec();
            btnSve.Text = LangPack.GetSave();
            btnClose.Text = LangPack.GetClose();

            if (LangPack.IsEng)
            {
                label8.Text = "Stored Message Edit";
            }
            else
            {
                label8.Text = "Хадглагдсан мессеж засварлах";
            }
        }
    }

    /// <summary>
    /// 저장메시지 추가 및 변경 시 발생하는 이벤트 아규먼트 클래스
    /// </summary>
    public class StoredMsgChangedEventArgs : EventArgs
    {
        /// <summary>
        /// 생성자
        /// </summary>
        public StoredMsgChangedEventArgs()
        {
        }
    }
}