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
    public partial class BoardMngForm : Form
    {
        #region Event
        private delegate void CGChangedEventArgsHandler(object sender, CGChangedEventArgs smcea);
        public event EventHandler<CGChangedEventArgs> OnCGChangedEvt;
        public event InvokeValueOne<BoardInfoDetail> evtBoardText;
        #endregion

        #region Fields
        private BoardDataMng brdMng = null;
        private bool bOrder = false;
        private string brdNewName = string.Empty;
        private int brdKindNum = 0;
        private BoardInfoDetail brdInfo = null;
        public string isPrimaryCG = string.Empty;
        #endregion

        #region Attributes
        public string BrdNewName
        {
            set { brdNewName = value; }
            get { return brdNewName; }
        }

        public int BrdKindNum
        {
            get { return this.brdKindNum; }
            set { this.brdKindNum = value; }
        }
        #endregion

        public BoardMngForm()
        {
            InitializeComponent();
        }

        public BoardMngForm(BoardInfoDetail info, bool bOrder, string _isPrimary)
            : this()
        {
            InitLang();

            this.brdInfo = info;
            this.bOrder = bOrder;

            brdMng = BoardDataMng.GetBrdMng();
            InitCtrl();
            this.isPrimaryCG = _isPrimary;

            if (this.isPrimaryCG == "1")
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
                lvText,
                new Type[] {
                    typeof(ListViewTextSort),
                    typeof(ListViewTextSort),
                    typeof(ListViewTextSort),
                    typeof(ListViewTextSort),
                },
                1,
                SortOrder.Ascending
            );
            #endregion
        }

        private void InitCtrl()
        {
            if (this.bOrder)
            {
                btnClear.Visible = true;
                btnClose.Text = LangPack.Getselect();
                btnView.Visible = false;
                btnDel.Visible = false;
                btnAdd.Visible = false;
                btnEdit.Visible = false;
                btnKindAdd.Visible = false;
                btnKindDel.Visible = false;
                btnKindEdit.Visible = false;

                tabKind.Size = new Size(tabKind.Size.Width, tabKind.Size.Height + 44);
                tabText.Size = new Size(tabText.Size.Width, tabText.Size.Height + 44);

                lvText.CheckBoxes = false;
            }
            else
            {
                btnClear.Visible = false;
                btnClose.Text = LangPack.GetClose();
                btnClose.Location = new Point(674, 463);
            }

            #region Init List Header
            ColumnHeader h0 = new ColumnHeader();
            h0.Text = "";
            h0.Width = 50;
            this.lvText.Columns.Add(h0);

            //ColumnHeader h3 = new ColumnHeader();
            //h3.Text = "";
            //h3.Width = 30;
            //this.lvText.Columns.Add(h3);

            ColumnHeader h1 = new ColumnHeader();
            h1.Text = LangPack.GetName();
            h1.Width = 100;
            this.lvText.Columns.Add(h1);

            ColumnHeader h2 = new ColumnHeader();
            h2.Text = LangPack.GetText();
            h2.Width = 300;
            this.lvText.Columns.Add(h2);

            //ColumnHeader h3 = new ColumnHeader();
            //h3.Text = "Blink";
            //h3.Width = 50;
            //this.lvText.Columns.Add(h3);
            #endregion

            AddTreeData();

            if (this.bOrder && this.brdInfo != null)
            {
                foreach (TreeNode node in tvKind.Nodes)
                {
                    if (node.Text == this.brdInfo.kind)
                    {
                        tvKind.SelectedNode = node;
                        break;
                    }
                }
            }
        }

        private void AddTreeData()
        {
            tvKind.Nodes.Clear();

            foreach (KeyValuePair<string, BoardInfo> pair in brdMng.dicBrd)
            {
                TreeNode node = new TreeNode();
                node.Text = pair.Value.kind;
                node.Name = pair.Value.kind;
                node.Tag = pair.Value;
                node.ImageIndex = 0;
                node.SelectedImageIndex = 0;
                tvKind.Nodes.Add(node);
            }

            //tvKind.Sort();
            tvKind.ExpandAll();

            new ListViewSortManager(
                lvText,
                new Type[] {
                    typeof(ListViewTextSort),
                    typeof(ListViewTextSort),
                    typeof(ListViewTextSort),
                    typeof(ListViewTextSort),
                },
                1,
                SortOrder.Ascending
            );
        }

        #region Event Button
        private void btnAdd_Click(object sender, EventArgs e)
        {
            BoardEditForm form = new BoardEditForm(null, isPrimaryCG);
            form.OnCGChangeEvt += new EventHandler<CGChangeEventArgs>(form_OnCGChangeEvt);
            form.ShowDialog();
            form.OnCGChangeEvt -= new EventHandler<CGChangeEventArgs>(form_OnCGChangeEvt);

            lvText.Items.Clear();
            tvKind.SelectedNode = null;
        }

        /// <summary>
        /// CG 추가 및 수정 시 팝업에서 발생시키는 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void form_OnCGChangeEvt(object sender, CGChangeEventArgs e)
        {
            if (this.OnCGChangedEvt != null)
            {
                this.OnCGChangedEvt(this, new CGChangedEventArgs());
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            TreeNode selNode = tvKind.SelectedNode;

            ListView.CheckedIndexCollection selText = lvText.CheckedIndices;

            if (selText == null || selText.Count == 0)
            {
                MessageBox.Show(LangPack.GetMongolian("Please select the items to modify."), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (selText.Count != 1)
            {
                MessageBox.Show(LangPack.GetMongolian("Please choose one CG text to modify."), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            BoardInfoDetail info = lvText.Items[selText[0]].Tag as BoardInfoDetail;
            BoardEditForm form = new BoardEditForm(info, isPrimaryCG);
            form.OnCGChangeEvt += new EventHandler<CGChangeEventArgs>(form_OnCGChangeEvt);
            form.ShowDialog();
            form.OnCGChangeEvt -= new EventHandler<CGChangeEventArgs>(form_OnCGChangeEvt);

            tvKind.SelectedNode = null;
            tvKind.SelectedNode = selNode;
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            TreeNode selNode = tvKind.SelectedNode;
            
            ListView.CheckedIndexCollection selText = lvText.CheckedIndices;

            if (selText == null || selText.Count == 0)
            {
                MessageBox.Show(LangPack.GetMongolian("Please choose the CG text to delete."), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (DialogResult.No == MessageBox.Show(LangPack.GetMongolian("Want to delete?"), this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2))
                return;

            for (int i = 0; i < selText.Count; i++)
            {
                BoardInfoDetail info = lvText.Items[selText[i]].Tag as BoardInfoDetail;
                brdMng.dicBrd[info.kind].dicText.Remove(info.name);
                brdMng.SaveBrdData();
            }

            MessageBox.Show(LangPack.GetMongolian("Deleted."), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (this.OnCGChangedEvt != null)
            {
                this.OnCGChangedEvt(this, new CGChangedEventArgs());
            }

            tvKind.SelectedNode = null;
            tvKind.SelectedNode = selNode;
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            ListView.CheckedIndexCollection selText = lvText.CheckedIndices;

            if (selText == null || selText.Count == 0)
            {
                MessageBox.Show(LangPack.GetMongolian("Please select CG text to preview."), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (selText.Count != 1)
            {
                MessageBox.Show(LangPack.GetMongolian("Please select one CG text to preview."), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            BoardInfoDetail info = lvText.Items[selText[0]].Tag as BoardInfoDetail;

            BoardViewForm form = new BoardViewForm(info.text, info.isBlack, this.isPrimaryCG);
            form.ShowDialog();

            lvText.Items[selText[0]].Checked = false;
        }
        #endregion

        private void tvKind_AfterSelect(object sender, TreeViewEventArgs e)
        {
            lvText.Items.Clear();

            BoardInfo info = e.Node.Tag as BoardInfo;

            foreach (KeyValuePair<string, BoardInfoDetail> pair in info.dicText)
            {
                AddListViewItem(pair.Value);
            }

            if (this.bOrder && this.brdInfo != null)
            {
                foreach (ListViewItem item in lvText.Items)
                {
                    if ((item.Tag as BoardInfoDetail) == this.brdInfo)
                    {
                        item.ForeColor = Color.Blue;
                        item.Font = new Font(lvText.Font, FontStyle.Bold);
                        break;
                    }
                }
            }
        }

        private void AddListViewItem(BoardInfoDetail infoD)
        {
            bool imageFlag = false;

            for (int i = 0; i < infoD.cut.Length; i++)
            {
                if (infoD.cut[i])
                {
                    imageFlag = true;
                    break;
                }
            }

            ListViewItem item = new ListViewItem();

            if (imageFlag)
            {
                item.ImageIndex = 1;
            }
            else
            {
                item.ImageIndex = 0;
            }
            
            item.Text = "";
            item.Tag = infoD;
            lvText.Items.Add(item);

            item.SubItems.Add(infoD.name);
            item.SubItems.Add(infoD.text);
            //item.SubItems.Add(infoD.isBlack ? "Yes" : "No");
        }

        private void lvText_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection selText = lvText.SelectedItems;

            if (selText == null || selText.Count <= 0)
                return;

            if (bOrder)
            {
                BoardInfoDetail info = lvText.Items[selText[0].Index].Tag as BoardInfoDetail;

                if (evtBoardText != null)
                    evtBoardText(info);

                //this.Close();
            }

            if (lvText.Items[selText[0].Index].Checked)
                lvText.Items[selText[0].Index].Checked = false;
            else
                lvText.Items[selText[0].Index].Checked = true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lvText_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            ListView.CheckedIndexCollection selText = lvText.CheckedIndices;

            if (selText == null || selText.Count <= 0)
                return;

            if (bOrder)
            {
                BoardInfoDetail info = lvText.Items[selText[0]].Tag as BoardInfoDetail;

                if (evtBoardText != null)
                    evtBoardText(info);

                //this.Close();
            }
        }

        private void btnKindAdd_Click(object sender, EventArgs e)
        {
            this.brdNewName = string.Empty;

            GroupEditForm form = new GroupEditForm(this);
            form.OnCGChangeBoxEvt += new EventHandler<CGChangeEventArgs>(form_OnCGChangeBoxEvt);
            
            if (DialogResult.OK != form.ShowDialog())
            {
                form.OnCGChangeBoxEvt -= new EventHandler<CGChangeEventArgs>(form_OnCGChangeBoxEvt);
                return;
            }
            
            BoardInfo brd = new BoardInfo();
            brd.kind = string.Format("({0})_{1}", this.brdKindNum, this.brdNewName);
            brd.kindNum = this.brdKindNum; //kind number
            brd.dicText = new Dictionary<string, BoardInfoDetail>();
            brdMng.dicBrd.Add(string.Format("({0})_{1}", this.brdKindNum, this.brdNewName), brd);

            brdMng.SaveBrdData();
            AddTreeData();
            form.OnCGChangeBoxEvt -= new EventHandler<CGChangeEventArgs>(form_OnCGChangeBoxEvt);
        }

        /// <summary>
        /// CG의 KIND 추가, 수정 창에서 주는 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void form_OnCGChangeBoxEvt(object sender, CGChangeEventArgs e)
        {
            if (this.OnCGChangedEvt != null)
            {
                this.OnCGChangedEvt(this, new CGChangedEventArgs());
            }
        }

        private void btnKindDel_Click(object sender, EventArgs e)
        {
            TreeNode node = tvKind.SelectedNode;
            if (node == null)
            {
                MessageBox.Show(LangPack.GetMongolian("Please select the items to delete."), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (DialogResult.No == MessageBox.Show(LangPack.GetMongolian("Want to delete?"), this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2))
                return;

            if (brdMng.dicBrd.ContainsKey(node.Name))
                brdMng.dicBrd.Remove(node.Name);

            MessageBox.Show(LangPack.GetMongolian("Deleted."), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (this.OnCGChangedEvt != null)
            {
                this.OnCGChangedEvt(this, new CGChangedEventArgs());
            }

            this.brdMng.SaveBrdData();
            AddTreeData();
            lvText.Items.Clear();
        }

        private void btnKindEdit_Click(object sender, EventArgs e)
        {
            TreeNode node = tvKind.SelectedNode;
            
            if (node == null)
            {
                MessageBox.Show(LangPack.GetMongolian("Please select the items to modify."), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string tmpName = string.Empty;
            string[] tmp = node.Text.Split('_');
            tmpName = node.Text.Replace(tmp[0] + "_", "");
            tmp[0] = tmp[0].Trim('(');
            tmp[0] = tmp[0].Trim(')');
            
            this.brdNewName = tmpName;
            this.brdKindNum = int.Parse(tmp[0]);

            GroupEditForm form = new GroupEditForm(this);
            form.OnCGChangeBoxEvt += new EventHandler<CGChangeEventArgs>(form_OnCGChangeBoxEvt);
            
            if (DialogResult.OK != form.ShowDialog())
            {
                form.OnCGChangeBoxEvt -= new EventHandler<CGChangeEventArgs>(form_OnCGChangeBoxEvt);
                return;
            }

            BoardInfo brd = new BoardInfo();
            brd.kind = string.Format("({0})_{1}", this.brdKindNum, this.brdNewName);
            brd.kindNum = this.brdKindNum;

            Dictionary<string, BoardInfoDetail> dicNewBrd = new Dictionary<string, BoardInfoDetail>();
            
            foreach (KeyValuePair<string, BoardInfoDetail> pair in brdMng.dicBrd[node.Text].dicText)
            {
                BoardInfoDetail newBrd = new BoardInfoDetail();
                newBrd.kind = brd.kind;
                newBrd.kindNum = brd.kindNum;
                newBrd.name = pair.Value.name;
                newBrd.text = pair.Value.text;
                dicNewBrd.Add(newBrd.name, newBrd);
            }
            
            brd.dicText = dicNewBrd;
            brdMng.dicBrd.Add(string.Format("({0})_{1}", this.brdKindNum, this.brdNewName), brd);
            brdMng.dicBrd.Remove(node.Text);

            brdMng.SaveBrdData();
            AddTreeData();
            form.OnCGChangeBoxEvt -= new EventHandler<CGChangeEventArgs>(form_OnCGChangeBoxEvt);
        }

        public bool IsExistKey(string key)
        {
            if (brdMng.dicBrd.ContainsKey(key))
                return true;
            else
                return false;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (!bOrder)
                return;

            if (evtBoardText != null)
                evtBoardText(null);

            this.Close();
        }

        private void lvText_DoubleClick(object sender, EventArgs e)
        {
            TreeNode selNode = tvKind.SelectedNode;
            ListView.SelectedIndexCollection selText = lvText.SelectedIndices;

            BoardInfoDetail info = lvText.Items[selText[0]].Tag as BoardInfoDetail;
            BoardEditForm form = new BoardEditForm(info, isPrimaryCG);
            form.OnCGChangeEvt += new EventHandler<CGChangeEventArgs>(form_OnCGChangeEvt);
            form.ShowDialog();
            form.OnCGChangeEvt -= new EventHandler<CGChangeEventArgs>(form_OnCGChangeEvt);

            tvKind.SelectedNode = null;
            tvKind.SelectedNode = selNode;
        }

        private void InitLang()
        {
            btnView.Text = LangPack.GetPreview();
            btnAdd.Text = LangPack.GetAdd();
            btnEdit.Text = LangPack.GetEdit();
            btnDel.Text = LangPack.GetDelete();
            btnKindAdd.Text = LangPack.GetAdd();
            btnKindEdit.Text = LangPack.GetEdit();
            btnKindDel.Text = LangPack.GetDelete();
            btnClose.Text = LangPack.GetClose();
            btnClear.Text = LangPack.GetClear();

            if (LangPack.IsEng)
            {
                label1.Text = "CG Edit";
                tabPage2.Text = "Kind List";
                tabPage1.Text = "Text List";
            }
            else
            {
                label1.Text = "Текст засварлах";
                tabPage2.Text = "Мэдээллийн төрөл";
                tabPage1.Text = "Текстийн жагсаалт";
            }
        }
    }

    /// <summary>
    /// CG 추가 및 변경 시 발생하는 이벤트 아규먼트 클래스
    /// </summary>
    public class CGChangedEventArgs : EventArgs
    {
        /// <summary>
        /// 생성자
        /// </summary>
        public CGChangedEventArgs()
        {
        }
    }
}