using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Media;
using System.Threading;

namespace Mews.Svr.Broad
{
    public partial class StMsgMngForm : Form
    {
        private delegate void StoMsgChangedEventArgsHandler(object sender, StoMsgChangedEventArgs smcea);
        public event EventHandler<StoMsgChangedEventArgs> OnStoMsgChangedEvt;

        #region Fields
        private StMsgDataMng msgMng = null;
        private SoundPlayer sp = null;
        private Thread playTD = null;
        private string storedIsPrimary = string.Empty;
        #endregion

        public StMsgMngForm(string _isPrimary)
        {
            InitializeComponent();

            msgMng = StMsgDataMng.GetMsgMng();
            InitLang();
            InitCtrl();
            this.storedIsPrimary = _isPrimary;

            if (_isPrimary == "1")
            {
                this.panel1.BackgroundImage = MewsBroad.Properties.Resources.bgTitle;
            }
            else
            {
                this.panel1.BackgroundImage = MewsBroad.Properties.Resources.bgTitleGreen;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            #region Init Sort List
            new ListViewSortManager(
                lvMsg,
                new Type[] {
                    typeof(ListViewTextSort),
                    typeof(ListViewInt32Sort),
                    typeof(ListViewTextSort),
                    typeof(ListViewTextSort),
                    typeof(ListViewTextSort),
                },
                1,
                SortOrder.Ascending
            );
            #endregion
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            if (this.sp != null)
            {
                this.sp.Stop();
            }

            if (this.playTD != null)
            {
                this.playTD.Abort();
                this.playTD = null;
            }
        }

        private void InitCtrl()
        {
            #region List Header
            ColumnHeader h0 = new ColumnHeader();
            h0.Text = "";
            h0.Width = 30;
            this.lvMsg.Columns.Add(h0);

            ColumnHeader h1 = new ColumnHeader();
            h1.Text = LangPack.GetStoNum();
            h1.Width = 75;
            this.lvMsg.Columns.Add(h1);

            ColumnHeader h2 = new ColumnHeader();
            h2.Text = LangPack.GetName();
            h2.Width = 170;
            this.lvMsg.Columns.Add(h2);

            ColumnHeader h3 = new ColumnHeader();
            h3.Text = LangPack.GetTime();
            h3.Width = 100;
            this.lvMsg.Columns.Add(h3);

            ColumnHeader h5 = new ColumnHeader();
            h5.Text = LangPack.GetStoContext();
            h5.Width = 150;
            this.lvMsg.Columns.Add(h5);
            #endregion

            AddListViewItem();
        }

        private void AddListViewItem()
        {
            lvMsg.Items.Clear();

            foreach (KeyValuePair<string, MsgInfo> pair in msgMng.dicMsg)
            {
                MsgInfo msg = pair.Value;

                ListViewItem item = new ListViewItem();
                item.Text = "";
                item.Tag = msg;
                item.ImageIndex = 1;
                lvMsg.Items.Add(item);

                item.SubItems.Add(msg.msgNum);
                item.SubItems.Add(msg.msgName);
                item.SubItems.Add(msg.msgTime.ToString() + " sec");
                item.SubItems.Add(msg.msgText);
            }

            new ListViewSortManager(
                lvMsg,
                new Type[] {
                    typeof(ListViewTextSort),
                    typeof(ListViewInt32Sort),
                    typeof(ListViewTextSort),
                    typeof(ListViewTextSort),
                    typeof(ListViewTextSort),
                },
                1,
                SortOrder.Ascending
            );
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection selText = lvMsg.SelectedIndices;

            if (selText.Count <= 0)
            {
                MessageBox.Show(LangPack.GetMongolian("Please select message text to modify."), LangPack.GetMongolian(this.Name), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            StMsgEditForm form = new StMsgEditForm((lvMsg.Items[selText[0]].Tag as MsgInfo).msgNum, this.storedIsPrimary);
            form.OnStoMsgChangedEvt += new EventHandler<StoredMsgChangedEventArgs>(form_OnStoMsgChangedEvt);
            form.ShowDialog();
            form.OnStoMsgChangedEvt -= new EventHandler<StoredMsgChangedEventArgs>(form_OnStoMsgChangedEvt);

            AddListViewItem();
            this.SetMsgInfo();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            StMsgEditForm form = new StMsgEditForm("Add", this.storedIsPrimary);
            form.OnStoMsgChangedEvt += new EventHandler<StoredMsgChangedEventArgs>(form_OnStoMsgChangedEvt);
            form.ShowDialog();
            form.OnStoMsgChangedEvt -= new EventHandler<StoredMsgChangedEventArgs>(form_OnStoMsgChangedEvt);

            AddListViewItem();
            this.SetMsgInfo();
        }

        private void lvMsg_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection selText = lvMsg.SelectedItems;

            if (selText == null || selText.Count <= 0)
            {
                this.SetMsgInfo();
                return;
            }

            if (!(lvMsg.Items[selText[0].Index].Tag is MsgInfo))
            {
                this.SetMsgInfo();
                return;
            }

            MsgInfo msg = lvMsg.Items[selText[0].Index].Tag as MsgInfo;
            tbText.Text = msg.msgText;
            lbNum.Text = msg.msgNum;
            lbTime.Text = "[" + msg.msgTime.ToString() + " " + LangPack.GetSec() + "]";
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection selText = lvMsg.SelectedIndices;
            if (selText.Count <= 0)
            {
                MessageBox.Show(LangPack.GetMongolian("Please select message text to delete."), LangPack.GetMongolian(this.Name), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (DialogResult.No == MessageBox.Show(LangPack.GetMongolian("Want to delete?"), LangPack.GetMongolian(this.Name), MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                return;

            msgMng.dicMsg.Remove((lvMsg.Items[selText[0]].Tag as MsgInfo).msgNum);
            msgMng.SaveMsgNameData();

            AddListViewItem();
            this.SetMsgInfo();

            if (this.OnStoMsgChangedEvt != null)
            {
                this.OnStoMsgChangedEvt(this, new StoMsgChangedEventArgs());
            }
        }

        /// <summary>
        /// 하단의 정보를 선택한 리스트의 정보로 보여준다.
        /// </summary>
        private void SetMsgInfo()
        {
            if (this.lvMsg.SelectedItems.Count == 0)
            {
                tbText.Text = string.Empty;
                lbNum.Text = "0";
                lbTime.Text = "[0 " + LangPack.GetSec() + "]";
            }
            else
            {
                ListView.SelectedListViewItemCollection selText = lvMsg.SelectedItems;

                if (selText == null || selText.Count <= 0)
                    return;

                if (!(lvMsg.Items[selText[0].Index].Tag is MsgInfo))
                    return;

                MsgInfo msg = lvMsg.Items[selText[0].Index].Tag as MsgInfo;

                tbText.Text = msg.msgText;
                lbNum.Text = msg.msgNum;
                lbTime.Text = "[" + msg.msgTime.ToString() + " " + LangPack.GetSec() + "]";
            }
        }

        private void lvMsg_DoubleClick(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection selText = lvMsg.SelectedIndices;

            StMsgEditForm form = new StMsgEditForm((lvMsg.Items[selText[0]].Tag as MsgInfo).msgNum, this.storedIsPrimary);
            form.OnStoMsgChangedEvt += new EventHandler<StoredMsgChangedEventArgs>(form_OnStoMsgChangedEvt);
            form.ShowDialog();
            form.OnStoMsgChangedEvt -= new EventHandler<StoredMsgChangedEventArgs>(form_OnStoMsgChangedEvt);

            AddListViewItem();
            this.SetMsgInfo();
        }

        /// <summary>
        /// 저장메시지 추가 및 변경 시 발생하는 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void form_OnStoMsgChangedEvt(object sender, StoredMsgChangedEventArgs e)
        {
            if (this.OnStoMsgChangedEvt != null)
            {
                this.OnStoMsgChangedEvt(this, new StoMsgChangedEventArgs());
            }
        }

        /// <summary>
        /// 저장메시지 Play 버튼 클릭 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (this.button1.Text == LangPack.GetPlay())
            {
                try
                {
                    ListView.SelectedIndexCollection selText = lvMsg.SelectedIndices;

                    if (selText.Count <= 0)
                    {
                        MessageBox.Show(LangPack.GetMongolian("Please choose the stored message to play."), LangPack.GetMongolian("Sound Play"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (!(System.IO.File.Exists(Util.file_StoMsgPlay + (lvMsg.Items[selText[0]].Tag as MsgInfo).msgNum + ".wav")))
                    {
                        MessageBox.Show(LangPack.GetMongolian("Sorry! can not play sound because file don't exists."), LangPack.GetMongolian("Sound Play"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (this.sp == null)
                    {
                        this.sp = new SoundPlayer(Util.file_StoMsgPlay + (lvMsg.Items[selText[0]].Tag as MsgInfo).msgNum + ".wav");
                    }
                    else
                    {
                        this.sp.SoundLocation = Util.file_StoMsgPlay + (lvMsg.Items[selText[0]].Tag as MsgInfo).msgNum + ".wav";
                    }

                    this.sp.Play();
                    this.playTD = new Thread(new ParameterizedThreadStart(playMethod));
                    this.playTD.Start((lvMsg.Items[selText[0]].Tag as MsgInfo).msgTime);
                    this.button1.Text = LangPack.GetStop();
                }
                catch (Exception ex)
                {
                    this.button1.Text = LangPack.GetPlay();
                }
            }
            else
            {
                try
                {
                    this.sp.Stop();
                    this.button1.Text = LangPack.GetPlay();

                    if (this.playTD != null)
                    {
                        this.playTD.Abort();
                        this.playTD = null;
                    }
                }
                catch (Exception ex)
                {
                    this.sp = null;
                    this.button1.Text = LangPack.GetPlay();

                    if (this.playTD != null)
                    {
                        this.playTD.Abort();
                        this.playTD = null;
                    }
                }
            }
        }

        private void playMethod(object _time)
        {
            DateTime dt = DateTime.Now.AddSeconds((int)_time);

            while(true)
            {
                if (dt < DateTime.Now)
                {
                    MethodInvoker mi = delegate()
                    {
                        this.button1_Click(this, new EventArgs());
                    };

                    if (this.InvokeRequired)
                    {
                        this.Invoke(mi);
                        break;
                    }
                    else
                    {
                        mi();
                        break;
                    }
                }
            }
        }

        private void InitLang()
        {
            label1.Text = LangPack.GetStoredMsg();
            btnAdd.Text = LangPack.GetAdd();
            btnEdit.Text = LangPack.GetEdit();
            btnDel.Text = LangPack.GetDelete();
            btnClose.Text = LangPack.GetClose();
            button1.Text = LangPack.GetPlay();
            lbTime.Text = "[0 " + LangPack.GetSec() + "]";

            if (LangPack.IsEng)
            {
                lbNumText.Text = "▶ Stored Message Number :  ";
            }
            else
            {
                lbNumText.Text = "▶ Хадгалагдсан мессежийн дугаар :  ";
            }
        }
    }

    /// <summary>
    /// 저장메시지 추가 및 변경 시 발생하는 이벤트 아규먼트 클래스
    /// </summary>
    public class StoMsgChangedEventArgs : EventArgs
    {
        /// <summary>
        /// 생성자
        /// </summary>
        public StoMsgChangedEventArgs()
        {
        }
    }
}