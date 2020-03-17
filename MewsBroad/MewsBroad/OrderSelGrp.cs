using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Mews.Lib.MewsDataMng;
using Mews.Ctrl.Mewsge;

namespace Mews.Svr.Broad
{
    public partial class OrderSelGrp : UserControl
    {
        #region Events
        public event InvokeValueOne<Group> evtGroupInfo;
        public event InvokeValueTwo<List<string>, List<string>> evtIndInfo;
        #endregion

        #region Fields
        private bool bGroup = false;
        private bool bSnGrp = false;
        private MDataMng dbMng = null;
        private BroadMain mainForm = null;
        private GroupMng grpMng = null;
        #endregion

        public OrderSelGrp()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            #region Init Sort List
            new ListViewSortManager(
                lvDist,
                new Type[] {
                    typeof(ListViewTextSort),
                    typeof(ListViewTextSort),
                    typeof(ListViewTextSort),
                },
                1,
                SortOrder.Ascending
            );

            new ListViewSortManager(
                lvTerm,
                new Type[] {
                    typeof(ListViewTextSort),
                    typeof(ListViewTextSort),
                    typeof(ListViewTextSort),
                },
                1,
                SortOrder.Ascending
            );
            #endregion
        }

        public void Init(BroadMain mainForm)
        {
            this.mainForm = mainForm;
            dbMng = this.mainForm.GetDbMng();
            grpMng = this.mainForm.GetGrpMng();
            btnEnd.Text = LangPack.GetSelect();

            #region Term LIst Header
            ColumnHeader h0 = new ColumnHeader();
            h0.Text = "";
            h0.Width = 30;
            this.lvTerm.Columns.Add(h0);

            ColumnHeader h1 = new ColumnHeader();
            h1.Text = LangPack.GetTermName();
            h1.Width = 155;
            this.lvTerm.Columns.Add(h1);

            ColumnHeader h2 = new ColumnHeader();
            h2.Text = LangPack.GetDistName();
            h2.Width = 160;
            this.lvTerm.Columns.Add(h2);
            #endregion
        }

        public void SetGroupList(bool bGrp)
        {
            btnEnd.Enabled = true;
            this.bSnGrp = false;
            this.bGroup = bGrp;
            SetCtrlInit();
        }

        private void SetCtrlInit()
        {
            lvDist.Clear();
            lvDist.CheckBoxes = true;
            lvTerm.Items.Clear();
            lvTerm.CheckBoxes = true;
            btnEnd.Text = LangPack.GetSelect();

            if (this.bGroup)
            {
                this.BackgroundImage = Util.GetBackgroundImage((byte)Util.emBackImage.grpActive);
                lbDist.Text = LangPack.GetSelGroup();

                ColumnHeader h0 = new ColumnHeader();
                h0.Text = "";
                h0.Width = 0;
                this.lvDist.Columns.Add(h0);

                ColumnHeader h1 = new ColumnHeader();
                h1.Text = LangPack.GetNo();
                h1.Width = 40;
                this.lvDist.Columns.Add(h1);

                ColumnHeader h2 = new ColumnHeader();
                h2.Text = LangPack.GetGroupName();
                h2.Width = 150;
                this.lvDist.Columns.Add(h2);

                ColumnHeader h3 = new ColumnHeader();
                h3.Text = LangPack.GetCount();
                h3.Width = 60;
                this.lvDist.Columns.Add(h3);

                lvDist.Size = new System.Drawing.Size(291, 583);
                lvTerm.Visible = false;
                btnEnd.Visible = false;
                ViewGroupList();

                lvDist.Sort();
            }
            else
            {
                this.BackgroundImage = Util.GetBackgroundImage((byte)Util.emBackImage.grpActive);
                lbDist.Text = LangPack.GetSelDist();
                lbTerm.Text = LangPack.GetSelTerm();

                ColumnHeader h0 = new ColumnHeader();
                h0.Text = "";
                h0.Width = 30;
                this.lvDist.Columns.Add(h0);

                ColumnHeader h1 = new ColumnHeader();
                h1.Text = LangPack.GetNo();
                h1.Width = 40;
                this.lvDist.Columns.Add(h1);

                ColumnHeader h2 = new ColumnHeader();
                h2.Text = LangPack.GetDistName();
                h2.Width = 150;
                this.lvDist.Columns.Add(h2);

                ColumnHeader h3 = new ColumnHeader();
                h3.Text = LangPack.GetCount();
                h3.Width = 60;
                this.lvDist.Columns.Add(h3);

                if (this.bSnGrp)
                {
                    lvDist.Size = new System.Drawing.Size(291, 530);
                    lvTerm.Visible = false;
                    btnEnd.Visible = true;
                    ViewTermsList(true);
                }
                else
                {
                    this.BackgroundImage = Util.GetBackgroundImage((byte)Util.emBackImage.IndActive);

                    lvDist.Size = new System.Drawing.Size(291, 200);
                    lvTerm.Visible = true;
                    btnEnd.Visible = true;
                    ViewTermsList(false);

                    lvTerm.Columns[1].Text = LangPack.GetTermName();
                    lvTerm.Columns[2].Text = LangPack.GetDistName();

                    lvTerm.Sort();
                }
            }
        }

        private DistData GetDistData(string ip)
        {
            foreach (KeyValuePair<string, DistData> pair in MDataMng.GetInstance().DicDist)
            {
                if (pair.Value.DistIp == ip)
                {
                    return pair.Value;
                }
            }

            return null;
        }

        private TermData GetTermIp(string _ip)
        {
            foreach (KeyValuePair<string, TermData> pair in MDataMng.GetInstance().DicOnlyBroadOrderTerm)
            {
                if (pair.Value.TermIp == _ip)
                {
                    return pair.Value;
                }
            }

            return null;
        }

        public void setSelInd(List<string> _dist, List<string> _term)
        {
            if (_dist == null && _term == null)
                return;

            for (int i = 0; i < _dist.Count; i++)
            {
                DistData tmpDist = this.GetDistData(_dist[i]);

                for (int j = 0; j < this.lvDist.Items.Count; j++)
                {
                    if (this.lvDist.Items[j].Tag == tmpDist)
                    {
                        this.lvDist.Items[j].Checked = true;
                        this.lvDist.Items[j].Font = new Font(this.lvDist.Font, FontStyle.Bold);
                        this.lvDist.Items[j].ForeColor = Color.Blue;
                    }
                }
            }

            for (int i = 0; i < _term.Count; i++)
            {
                TermData tmpTerm = this.GetTermIp(_term[i]);

                for (int j = 0; j < this.lvTerm.Items.Count; j++)
                {
                    if (this.lvTerm.Items[j].Tag == tmpTerm)
                    {
                        this.lvTerm.Items[j].Checked = true;
                        this.lvTerm.Items[j].Font = new Font(this.lvDist.Font, FontStyle.Bold);
                        this.lvTerm.Items[j].ForeColor = Color.Blue;
                    }
                }
            }
        }

        public void setSelGroup(Group _grp)
        {
            if (_grp == null)
                return;

            for (int i = 0; i < this.lvDist.Items.Count; i++)
            {
                if (this.lvDist.Items[i].Tag == _grp)
                {
                    this.lvDist.ItemCheck -= new System.Windows.Forms.ItemCheckEventHandler(this.lvDist_ItemCheck);
                    this.lvDist.Items[i].Checked = true;
                    this.lvDist.Items[i].Font = new Font(this.lvDist.Font, FontStyle.Bold);
                    this.lvDist.Items[i].ForeColor = Color.Blue;
                    this.lvDist.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvDist_ItemCheck);
                }
            }
        }

        private void ViewGroupList()
        {
            ListViewItem item = null;
            int index = 0;

            foreach (KeyValuePair<string, Group> pair in grpMng.dicGrp)
            {
                Group gp = pair.Value as Group;

                item = new ListViewItem();
                item.Text = "";
                item.Tag = gp;
                lvDist.Items.Add(item);

                item.SubItems.Add((++index).ToString());
                item.SubItems.Add(gp.Name);
                item.SubItems.Add((gp.dicProv.Count + gp.dicDist.Count + gp.dicTerm.Count).ToString());
            }
        }

        private void ViewTermsList(bool bSn)
        {
            ListViewItem item = null;

            foreach (KeyValuePair<string, DistData> pairDist in dbMng.DicDist)
            {
                DistData dist = pairDist.Value as DistData;

                item = new ListViewItem();
                item.Text = "";
                item.Tag = dist;
                lvDist.Items.Add(item);

                // Dist 의 경우 No 는 code 값으로 삽입 (조작반 배열과 맞추기 위해)
                item.SubItems.Add(dist.Code.ToString());
                item.SubItems.Add(dist.Name);
                item.SubItems.Add(dist.dicBroadTermData.Count.ToString());
            }

            this.lvDist.Sort();

            if (!bSn)
            {
                foreach (KeyValuePair<string, TermData> pairTerm in dbMng.DicOnlyBroadOrderTerm)
                {
                    TermData term = pairTerm.Value as TermData;
                    DistData dist = dbMng.GetDistData(term.DistCode);

                    item = new ListViewItem();
                    item.Text = "";
                    item.Tag = term;
                    lvTerm.Items.Add(item);
                    item.SubItems.Add(term.Name);
                    item.SubItems.Add(dist.Name);
                }
            }
        }

        private void lvDist_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.Index < 0)
                return;

            ListView.CheckedIndexCollection selDist = lvDist.CheckedIndices;
            ListView.CheckedIndexCollection selTerm = lvTerm.CheckedIndices;

            if (e.NewValue == CheckState.Checked && (selDist.Count + selTerm.Count) >= 17)
            {
                MessageBox.Show(LangPack.GetMongolian("Terminals can not be selected to issue more than sixteen."), LangPack.GetMongolian(this.Name), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.NewValue = CheckState.Unchecked;
                return;
            }

            ListViewItem item = lvDist.Items[e.Index];

            if (bGroup)
            {
                Group grp = item.Tag as Group;

                if (evtGroupInfo != null)
                    evtGroupInfo(grp);

                item.ForeColor = Color.Blue;
                item.Font = new System.Drawing.Font(lvDist.Font, FontStyle.Bold);
                lvDist.CheckBoxes = false;
            }
            else
            {
                DistData dist = item.Tag as DistData;
                List<ListViewItem> lstIndex = new List<ListViewItem>();

                if (e.NewValue == CheckState.Checked)
                {
                    item.ForeColor = Color.Blue;
                    item.Font = new Font(lvDist.Font, FontStyle.Bold);

                    foreach (ListViewItem lv in lvTerm.Items)
                    {
                        TermData term = lv.Tag as TermData;
                        if (term.DistCode == dist.Code)
                            lstIndex.Add(lv);
                    }

                    foreach (ListViewItem index in lstIndex)
                    {
                        lvTerm.Items.Remove(index);
                    }
                }
                else
                {
                    item.ForeColor = Color.Black;
                    item.Font = new Font(lvDist.Font, FontStyle.Regular);

                    ListViewItem newItem = null;

                    foreach(KeyValuePair<string, TermData> pair in dist.dicBroadTermData)
                    {
                        TermData term = pair.Value as TermData;

                        newItem = new ListViewItem();
                        newItem.Text = "";
                        newItem.Tag = term;
                        lvTerm.Items.Add(newItem);
                        newItem.SubItems.Add(term.Name);
                        newItem.SubItems.Add(dist.Name);
                    }

                    lvTerm.Sort();
                }
            }
        }

        private void lvTerm_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.Index < 0)
                return;

            ListView.CheckedIndexCollection selDist = lvDist.CheckedIndices;
            ListView.CheckedIndexCollection selTerm = lvTerm.CheckedIndices;

            if (e.NewValue == CheckState.Checked && (selDist.Count + selTerm.Count) >= 17)
            {
                MessageBox.Show(LangPack.GetMongolian("Terminals can not be selected to issue more than sixteen."), LangPack.GetMongolian(this.Name), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.NewValue = CheckState.Unchecked;
                return;
            }

            ListViewItem item = lvTerm.Items[e.Index];

            if (e.NewValue == CheckState.Checked)
            {
                item.ForeColor = Color.Blue;
                item.Font = new Font(lvTerm.Font, FontStyle.Bold);
            }
            else
            {
                item.ForeColor = Color.Black;
                item.Font = new Font(lvTerm.Font, FontStyle.Regular);
            }
        }

        public void btnEnd_Click(object sender, EventArgs e)
        {
            ListView.CheckedIndexCollection selDist = lvDist.CheckedIndices;
            ListView.CheckedIndexCollection selTerm = lvTerm.CheckedIndices;

            if ((selDist == null && selTerm == null) || ((selDist.Count + selTerm.Count) == 0))
            {
                MessageBox.Show(LangPack.GetMongolian("Please select terminal(s) to issue."), LangPack.GetMongolian(this.Name), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if ((selDist.Count + selTerm.Count) > 16)
            {
                MessageBox.Show(LangPack.GetMongolian("Terminals can not be selected to issue more than sixteen."), LangPack.GetMongolian(this.Name), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            List<string> lstDistIP = new List<string>();
            List<string> lstTermIP = new List<string>();

            for (int i = 0; i < selDist.Count; i++)
            {
                lstDistIP.Add((lvDist.Items[selDist[i]].Tag as DistData).DistIp);
            }

            for (int i = 0; i < selTerm.Count; i++)
            {
                lstTermIP.Add((lvTerm.Items[selTerm[i]].Tag as TermData).TermIp);
            }

            if (evtIndInfo != null)
                evtIndInfo(lstDistIP, lstTermIP);

            this.lvDist.CheckBoxes = false;
            this.lvTerm.CheckBoxes = false;

            if (bSnGrp)
                this.BackgroundImage = Util.GetBackgroundImage((byte)Util.emBackImage.grpNone);
            else
                this.BackgroundImage = Util.GetBackgroundImage((byte)Util.emBackImage.IndNone);

            btnEnd.Enabled = false;
        }

        private void lvDist_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection selText = lvDist.SelectedItems;

            if (selText == null || selText.Count <= 0)
                return;

            if (lvDist.Items[selText[0].Index].Checked)
                lvDist.Items[selText[0].Index].Checked = false;
            else
                lvDist.Items[selText[0].Index].Checked = true;
        }

        private void lvTerm_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection selText = lvTerm.SelectedItems;

            if (selText == null || selText.Count <= 0)
                return;

            if (lvTerm.Items[selText[0].Index].Checked)
                lvTerm.Items[selText[0].Index].Checked = false;
            else
                lvTerm.Items[selText[0].Index].Checked = true;
        }
    }
}
