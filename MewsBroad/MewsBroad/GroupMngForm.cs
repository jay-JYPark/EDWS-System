using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Mews.Ctrl.Mewsge;
using Mews.Lib.MewsDataMng;

namespace Mews.Svr.Broad
{
    public partial class GroupMngForm : Form
    {
        #region Fields
        private string newGrpName = string.Empty;
        private GroupMng grpMng = null;
        private MDataMng dbMng = null;
        public string isPri = string.Empty;
        #endregion

        #region Attributes
        public string NewGrpName
        {
            set { newGrpName = value; }
            get { return newGrpName; }
        }
        #endregion

        public GroupMngForm()
        {
            InitializeComponent();

            InitLang();
            InitCtrl();
        }

        public GroupMngForm(string _isPrimary)
        {
            InitializeComponent();

            InitLang();
            InitCtrl();
            this.isPri = _isPrimary;

            if (_isPrimary == "1")
            {
                this.panel1.BackgroundImage = MewsBroad.Properties.Resources.bgTitle;
            }
            else
            {
                this.panel1.BackgroundImage = MewsBroad.Properties.Resources.bgTitleGreen;
            }
        }

        private void InitCtrl()
        {
            // Base Data Info
            ///////////////////////////////////////////////////////////////////////////
            dbMng = MDataMng.GetInstance();
            DisplayBaseTree();

            // Group Info
            ///////////////////////////////////////////////////////////////////////////
            grpMng = GroupMng.GetGrpMng();
            grpMng.Init(dbMng);
            DisplayGroupTree();
        }

        private void DisplayBaseTree()
        {
            tvAll.Nodes.Clear();
            TreeNode node = new TreeNode();
            TreeNode porvNode = AddBaseItem(node, null);

            foreach (KeyValuePair<string, ProvData> pairProv in dbMng.DicProv)
            {
                if (pairProv.Value.dicDistData.Count == 0)
                    continue;

                TreeNode distNode = AddBaseItem(porvNode, pairProv.Value);

                if (pairProv.Value.dicDistData.Count != 0)
                {
                    foreach (KeyValuePair<string, DistData> pairDist in dbMng.DicDist)
                    {
                        if (pairProv.Value.Code != pairDist.Value.ProvCode)
                            continue;

                        TreeNode termNode = AddBaseItem(distNode, pairDist.Value);

                        foreach (KeyValuePair<string, TermData> pairTerm in dbMng.DicOnlyBroadOrderTerm)
                        {
                            if (pairDist.Value.Code != pairTerm.Value.DistCode)
                                continue;

                            AddBaseItem(termNode, pairTerm.Value);
                        }
                    }
                }
            }

            tvAll.ExpandAll();
        }

        private void DisplayGroupTree()
        {
            tvGrp.Nodes.Clear();

            if (grpMng.dicGrp.Count != 0)
            {
                foreach (KeyValuePair<string, Group> pair in grpMng.dicGrp)
                {
                    TreeNode gpNode = AddGroupItem(pair.Value);

                    foreach (KeyValuePair<string, ProvData> pairProv in pair.Value.dicProv)
                    {
                        AddGroupItem(gpNode, pairProv.Value);
                    }

                    foreach (KeyValuePair<string, DistData> pairDist in pair.Value.dicDist)
                    {
                        AddGroupItem(gpNode, pairDist.Value);
                    }

                    foreach (KeyValuePair<string, TermData> pairTerm in pair.Value.dicTerm)
                    {
                        AddGroupItem(gpNode, pairTerm.Value);
                    }
                }
            }

            int rootCnt = tvGrp.Nodes.Count;
            for (int i = 0; i < rootCnt; i++)
            {
                tvGrp.Nodes[i].Expand();
            }
        }

        private TreeNode AddGroupItem(Group gp)
        {
            TreeNode node = new TreeNode();
            node.Name = gp.Key;
            node.Text = gp.Name;
            node.Tag = gp;
            node.ImageIndex = 0;
            node.SelectedImageIndex = 0;
            node.NodeFont = new Font(tvGrp.Font, FontStyle.Bold);
            node.ForeColor = Color.Blue;
                        
            tvGrp.Nodes.Add(node);
            return node;
        }

        private TreeNode AddGroupItem(TreeNode node, MewsDataBase data)
        {
            TreeNode subNode = new TreeNode();
            subNode.Name = data.Code.ToString();
            subNode.Text = data.Name;
            subNode.Tag = data;

            if (data is TermData)
            {
                TermData term = data as TermData;
                subNode.ImageIndex = 3;
                subNode.SelectedImageIndex = 3;
            }
            else if (data is DistData)
            {
                DistData dist = data as DistData;
                subNode.ImageIndex = 2;
                subNode.SelectedImageIndex = 2;

                //if (dbMng.DicDist.ContainsKey(dist.Code.ToString()))
                //{
                //    foreach (KeyValuePair<string, TermData> pairTerm in dbMng.DicDist[dist.Code.ToString()].dicTermData)
                //    {
                //        AddGroupItem(subNode, pairTerm.Value);
                //    }
                //}
            }
            else if (data is ProvData)
            {
                ProvData prov = data as ProvData;
                subNode.ImageIndex = 1;
                subNode.SelectedImageIndex = 1;

                //if (dbMng.DicProv.ContainsKey(prov.Code.ToString()))
                //{
                //    foreach (KeyValuePair<string, DistData> pairDist in dbMng.DicProv[prov.Code.ToString()].dicDistData)
                //    {
                //        AddGroupItem(subNode, pairDist.Value);
                //    }
                //}
            }

            node.Nodes.Add(subNode);

            return subNode;
        }

        private TreeNode AddBaseItem(TreeNode parentNode, MewsDataBase data)
        {
            TreeNode node = new TreeNode();

            if (data == null)
            {
                node.Text = "ALL";
                node.Name = "ALL";
                tvAll.Nodes.Add(node);

                return node;
            }

            node.Name = data.Code.ToString();
            node.Text = data.Name;
            node.Tag = data;

            if (data is ProvData)
            {
                node.ImageIndex = 1;
                node.SelectedImageIndex = 1;
            }
            else if (data is DistData)
            {
                node.ImageIndex = 2;
                node.SelectedImageIndex = 2;
            }
            else if (data is TermData)
            {
                node.ImageIndex = 3;
                node.SelectedImageIndex = 3;
            }

            parentNode.Nodes.Add(node);
            return node;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            GroupEditForm form = new GroupEditForm(this);
            if (DialogResult.OK != form.ShowDialog())
                return;

            // Create new group
            Group gp = new Group();
            gp.Key = DateTime.Now.ToBinary().ToString();
            gp.Name = this.newGrpName;
            AddGroupItem(gp);

            this.newGrpName = string.Empty;

            grpMng.UpdateGrp(gp.Key, gp);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            TreeNode node = tvGrp.SelectedNode;
            if (node == null || !(node.Tag is Group))
            {
                MessageBox.Show(LangPack.GetMongolian("Please select the group to modify."), LangPack.GetMongolian(this.Name), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            this.newGrpName = node.Text;
            GroupEditForm form = new GroupEditForm(this);
            if (DialogResult.OK != form.ShowDialog())
                return;

            node.Text = this.newGrpName;

            Group gp = node.Tag as Group;
            gp.Name = this.newGrpName;
            grpMng.UpdateGrp(gp.Key, gp);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            bool bRult = false;

            TreeNode node = tvGrp.SelectedNode;
            TreeNode prtNode = node;
            if (node == null)
            {
                MessageBox.Show(LangPack.GetMongolian("Please select the items to delete."), LangPack.GetMongolian(this.Name), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (DialogResult.No == MessageBox.Show(LangPack.GetMongolian("Want to delete?"), LangPack.GetMongolian(this.Name), MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2))
                return;

            if (node.Tag is Group)
            {
                Group gp = node.Tag as Group;
                bRult = grpMng.DeleteGrp(gp.Key);
            }
            else
            {
                int iCnt = 0;
                string grpKey = string.Empty;

                while (true)
                {
                    // 무한 Loop 방지
                    iCnt++;
                    if (iCnt > 100)
                        return;

                    prtNode = prtNode.Parent;

                    if (prtNode.Tag is Group)
                    {
                        grpKey = (prtNode.Tag as Group).Key;
                        break;
                    }
                }

                if (node.Tag is ProvData)
                {
                    ProvData prov = node.Tag as ProvData;
                    bRult = grpMng.DeleteGrpProv(grpKey, prov);
                }
                else if (node.Tag is DistData)
                {
                    DistData dist = node.Tag as DistData;
                    bRult = grpMng.DeleteGrpDist(grpKey, dist);
                }
                else if (node.Tag is TermData)
                {
                    TermData term = node.Tag as TermData;
                    bRult = grpMng.DeleteGrpTerm(grpKey, term);
                }
            }

            tvGrp.Nodes.Remove(node);

            if(bRult)
                MessageBox.Show(LangPack.GetMongolian("Deleted."), LangPack.GetMongolian(this.Name), MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show(LangPack.GetMongolian("Failed."), LangPack.GetMongolian(this.Name), MessageBoxButtons.OK, MessageBoxIcon.Error);

            DisplayGroupTree();
        }

        private void btnMove_Click(object sender, EventArgs e)
        {
            Dictionary<string, TermData> dicTermTemp = new Dictionary<string, TermData>();
            Dictionary<string, DistData> dicDistTemp = new Dictionary<string, DistData>();
            Dictionary<string, ProvData> dicProvTemp = new Dictionary<string, ProvData>();

            foreach (TreeNode nodeProv in tvAll.Nodes[0].Nodes)
            {
                // Add Prov All
                if (nodeProv.Checked)
                {
                    ProvData prov = nodeProv.Tag as ProvData;
                    dicProvTemp.Add(prov.Code.ToString(), prov);
                }
                else
                {
                    foreach (TreeNode nodeDist in nodeProv.Nodes)
                    {
                        //  Add Dist All
                        if (nodeDist.Checked)
                        {
                            DistData dist = nodeDist.Tag as DistData;
                            dicDistTemp.Add(dist.Code.ToString(), dist);
                        }
                        else
                        {
                            foreach (TreeNode nodeTerm in nodeDist.Nodes)
                            {
                                // Add Term One
                                if (nodeTerm.Checked)
                                {
                                    TermData term = nodeTerm.Tag as TermData;
                                    dicTermTemp.Add(term.Code.ToString(), term);
                                }
                            }
                        }
                    }
                }
            }

            if (dicTermTemp.Count == 0 && dicDistTemp.Count == 0 && dicProvTemp.Count == 0)
            {
                MessageBox.Show(LangPack.GetMongolian("Please select terminal/province/district."), LangPack.GetMongolian(this.Name), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            TreeNode grpNode = tvGrp.SelectedNode;
            if (grpNode == null || !(grpNode.Tag is Group))
            {
                MessageBox.Show(LangPack.GetMongolian("Please select group."), LangPack.GetMongolian(this.Name), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (grpNode.Nodes.Count >= 17)
            {
                MessageBox.Show(LangPack.GetMongolian("A group of up to sixteen members."), LangPack.GetMongolian(this.Name), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if ((grpNode.Nodes.Count + dicProvTemp.Count + dicDistTemp.Count + dicTermTemp.Count) > 16)
            {
                MessageBox.Show(LangPack.GetMongolian("A group of up to sixteen members."), LangPack.GetMongolian(this.Name), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Group gp = grpNode.Tag as Group;
            grpMng.UpdateGrpProv(gp.Key, dicProvTemp);
            grpMng.UpdateGrpDist(gp.Key, dicDistTemp);
            grpMng.UpdateGrpTerm(gp.Key, dicTermTemp);

            DisplayGroupTree();

            tvAll.Nodes[0].Checked = false;
        }

        private void tvGrp_AfterCheck(object sender, TreeViewEventArgs e)
        {
            foreach (TreeNode node in e.Node.Nodes)
            {
                node.Checked = e.Node.Checked;
            }
        }

        private void tvAll_AfterCheck(object sender, TreeViewEventArgs e)
        {
            foreach (TreeNode node in e.Node.Nodes)
            {
                node.Checked = e.Node.Checked;
            }
        }

        private void InitLang()
        {
            btnAdd.Text = LangPack.GetAdd();
            btnEdit.Text = LangPack.GetEdit();
            btnDel.Text = LangPack.GetDelete();
            btnClose.Text = LangPack.GetClose();

            if (LangPack.IsEng)
            {
                label1.Text = "Group Edit";
            }
            else
            {
                label1.Text = "Бүлэг засварлах";
            }
        }
    }
}