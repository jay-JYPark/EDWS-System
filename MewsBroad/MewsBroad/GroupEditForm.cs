using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Mews.Ctrl.Mewsge;

namespace Mews.Svr.Broad
{
    public partial class GroupEditForm : Form
    {
        private delegate void CGChangeEventArgsHandler(object sender, CGChangeEventArgs smcea);
        public event EventHandler<CGChangeEventArgs> OnCGChangeBoxEvt;

        #region Fields
        private GroupMngForm grpForm = null;
        private BoardMngForm brdForm = null;
        private string oldGrpName = string.Empty;
        private bool bGrp = false;
        #endregion

        #region Attributes
        public string OldGrpName
        {
            set { oldGrpName = value; }
            get { return oldGrpName; }
        }
        #endregion

        public GroupEditForm()
        {
            InitializeComponent();
        }

        public GroupEditForm(Form form)
            : this()
        {
            InitLang();

            if (form is GroupMngForm)
            {
                this.grpForm = form as GroupMngForm;
                this.bGrp = true;

                if (!string.IsNullOrEmpty(this.grpForm.NewGrpName))
                    tbName.Text = this.grpForm.NewGrpName;
                else
                    tbName.Text = LangPack.GetNewGroup();

                if (grpForm.isPri == "1")
                {
                    this.panel1.BackgroundImage = MewsBroad.Properties.Resources.bgTitle;
                }
                else
                {
                    this.panel1.BackgroundImage = MewsBroad.Properties.Resources.bgTitleGreen;
                }

                //this.Text = "Group Name Edit";
            }
            else if (form is BoardMngForm)
            {
                this.tbName.Width = 180;
                this.tbName.Height = 22;
                this.numericUpDown1.Visible = true;
                this.label2.Text = LangPack.GetKindNum();
                this.label2.Visible = true;
                this.brdForm = form as BoardMngForm;
                this.bGrp = false;

                if (!string.IsNullOrEmpty(this.brdForm.BrdNewName))
                {
                    tbName.Text = this.brdForm.BrdNewName;
                    this.numericUpDown1.Value = this.brdForm.BrdKindNum;
                }
                else
                {
                    tbName.Text = LangPack.GetNewName();
                }

                if (brdForm.isPrimaryCG == "1")
                {
                    this.panel1.BackgroundImage = MewsBroad.Properties.Resources.bgTitle;
                }
                else
                {
                    this.panel1.BackgroundImage = MewsBroad.Properties.Resources.bgTitleGreen;
                }

                //this.Text = "CG Edit";
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbName.Text))
            {
                MessageBox.Show(LangPack.GetMongolian("Please write a name."), LangPack.GetMongolian(this.Name), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbName.Focus();
                return;
            }

            GroupMng grpMng = GroupMng.GetGrpMng();

            foreach (KeyValuePair<string, Group> pair in grpMng.dicGrp)
            {
                if (pair.Value.Name.Trim() == tbName.Text.Trim())
                {
                    MessageBox.Show(LangPack.GetMongolian("The same name already exists. Please enter check again after."), LangPack.GetMongolian(this.Name), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    tbName.Focus();
                    return;
                }
            }

            if (this.bGrp)
            {
                this.grpForm.NewGrpName = tbName.Text + "   ";
            }
            else
            {
                if (this.brdForm.IsExistKey(string.Format("({0})_{1}", this.numericUpDown1.Value, this.tbName.Text)))
                {
                    MessageBox.Show(LangPack.GetMongolian("The title already exists. Please enter after checking again."), LangPack.GetMongolian(this.Name), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    tbName.Focus();
                    return;
                }

                this.brdForm.BrdNewName = tbName.Text;
                this.brdForm.BrdKindNum = (int)this.numericUpDown1.Value;

                if (this.OnCGChangeBoxEvt != null)
                {
                    this.OnCGChangeBoxEvt(this, new CGChangeEventArgs());
                }
            }

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void InitLang()
        {
            if (LangPack.IsEng)
            {
                label1.Text = "New Name";
                btnOk.Text = "OK";
                btnCancel.Text = "Cancel";
            }
            else
            {
                label1.Text = "Шинэ нэр";
                btnOk.Text = "Зөвшөөрөх";
                btnCancel.Text = "Цуцлах";
            }
        }
    }

    /// <summary>
    /// CG 추가 및 변경 시 발생하는 이벤트 아규먼트 클래스
    /// </summary>
    public class CGChangeBoxEventArgs : EventArgs
    {
        /// <summary>
        /// 생성자
        /// </summary>
        public CGChangeBoxEventArgs()
        {
        }
    }
}