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
    public partial class OrderSelMsg : UserControl
    {
        #region Events
        public event InvokeValueTwo<byte, byte> evtMsgInfo;
        #endregion

        #region Fields
        private List<GlassButton> lstBtn = new List<GlassButton>();
        private byte curMode = 0;
        private StMsgDataMng msgMng = null;
        #endregion

        public OrderSelMsg()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            new ListViewSortManager(
                lvMsg,
                new Type[] {
                    typeof(ListViewTextSort),
                    typeof(ListViewInt32Sort),
                    typeof(ListViewTextSort),
                    typeof(ListViewTextSort),
                },
                1,
                SortOrder.Ascending
            );

            btnEnd.Text = LangPack.GetSelect();
            lbTitle.Text = LangPack.GetSelMsg();
            label1.Text = LangPack.GetReptCnt();
        }

        public void SetModeInfo(byte mode)
        {
            this.lvMsg.Items.Clear();
            this.curMode = mode;
            this.numRptCnt.Value = 1;

            foreach (KeyValuePair<string, MsgInfo> pair in msgMng.dicMsg)
            {
                MsgInfo msg = pair.Value as MsgInfo;

                ListViewItem item = new ListViewItem();
                item.Text = "";
                item.Name = msg.msgNum;
                item.Tag = msg;

                lvMsg.Items.Add(item);

                item.SubItems.Add(msg.msgNum);
                item.SubItems.Add(msg.msgName);
            }

            lvMsg.Sort();
        }

        public void Init()
        {
            //this.BackgroundImage = Util.GetBackgroundImage((byte)Util.emBackImage.grpActive);
            msgMng = StMsgDataMng.GetMsgMng();

            #region List Header
            ColumnHeader h0 = new ColumnHeader();
            h0.Text = "";
            h0.Width = 35;
            lvMsg.Columns.Add(h0);

            ColumnHeader h2 = new ColumnHeader();
            h2.Text = LangPack.GetMsgNum();
            h2.Width = 80;
            lvMsg.Columns.Add(h2);

            ColumnHeader h1 = new ColumnHeader();
            h1.Text = LangPack.GetMsgName();
            h1.Width = 160;
            lvMsg.Columns.Add(h1);
            #endregion
        }

        private void lvMsg_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            for (int i = 0; i < lvMsg.Items.Count; i++)
            {
                if (lvMsg.Items[i].Checked && (e.Index != i))
                    lvMsg.Items[i].Checked = false;
            }

            lvMsg.Items[e.Index].Selected = true;
        }

        private void lvMsg_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection selText = lvMsg.SelectedItems;

            if (selText == null || selText.Count <= 0)
                return;

            //for (int i = 0; i < lvMsg.Items.Count; i++)
            //{
            //    if (lvMsg.Items[i].Checked && !lvMsg.Items[i].Selected)
            //        lvMsg.Items[i].Checked = false;
            //}

            if (lvMsg.Items[selText[0].Index].Checked)
                lvMsg.Items[selText[0].Index].Checked = false;
            else
                lvMsg.Items[selText[0].Index].Checked = true;
        }

        private void btnEnd_Click(object sender, EventArgs e)
        {
            ListView.CheckedListViewItemCollection selText = lvMsg.CheckedItems;

            if (selText == null || selText.Count <= 0)
            {
                MessageBox.Show(LangPack.GetMongolian("Please select stored message."), LangPack.GetMongolian("Stored Message"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            MsgInfo msg = lvMsg.Items[selText[0].Index].Tag as MsgInfo;
            msg.msgReptCtn = numRptCnt.Value.ToString();

            if (evtMsgInfo != null)
                evtMsgInfo(Convert.ToByte(msg.msgNum), Convert.ToByte(numRptCnt.Value));
        }
    }
}