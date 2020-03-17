using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Mews.Svr.Broad
{
    public partial class BoardEditForm : Form
    {
        private delegate void CGChangeEventArgsHandler(object sender, CGChangeEventArgs smcea);
        public event EventHandler<CGChangeEventArgs> OnCGChangeEvt;

        #region Fields
        private BoardDataMng brdMng = null;
        private bool bAdd = false;
        private BoardInfoDetail infoD = null;
        private BoardInfoDetail cutInfoD = null;
        private int lvMaxLen = 900;
        private bool bCtrlV = false;
        private string oldName = string.Empty;
        private bool bOpenBrackets = false;
        private bool bCloseBrackets = false;
        private bool shiftFlag = false;
        public string primary = string.Empty;
        #endregion

        public BoardEditForm()
        {
            InitializeComponent();
        }

        public BoardEditForm(BoardInfoDetail info, string _isPrimary)
            : this()
        {
            InitLang();

            if (info == null)
            {
                bAdd = true;
            }
            else
            {
                this.infoD = info;
                cutInfoD = info;
            }

            Init();
            this.primary = _isPrimary;

            if (_isPrimary == "1")
            {
                this.panel1.BackgroundImage = MewsBroad.Properties.Resources.bgTitle;
            }
            else
            {
                this.panel1.BackgroundImage = MewsBroad.Properties.Resources.bgTitleGreen;
            }
        }

        private void Init()
        {
            brdMng = BoardDataMng.GetBrdMng();

            if (brdMng.dicBrd.Count == 0)
            {
                MessageBox.Show(LangPack.GetMongolian("Please register kind first."), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            cbBlack.Items.Add("Unused");
            cbBlack.Items.Add("Used");

            foreach (KeyValuePair<string, BoardInfo> pair in brdMng.dicBrd)
            {
                cbKind.Items.Add(pair.Key);
            }

            this.cutImageList.Images.Clear();
            this.cutImageLV.Items.Clear();

            if (this.infoD != null)
            {
                if (File.Exists(Util.file_CGCutImage + this.infoD.kindNum.ToString() + "-1.png"))
                {
                    Image img = Image.FromFile(Util.file_CGCutImage + this.infoD.kindNum.ToString() + "-1.png");
                    this.cutImageList.Images.Add(img);
                }
                if (File.Exists(Util.file_CGCutImage + this.infoD.kindNum.ToString() + "-2.png"))
                {
                    Image img = Image.FromFile(Util.file_CGCutImage + this.infoD.kindNum.ToString() + "-2.png");
                    this.cutImageList.Images.Add(img);
                }
                if (File.Exists(Util.file_CGCutImage + this.infoD.kindNum.ToString() + "-3.png"))
                {
                    Image img = Image.FromFile(Util.file_CGCutImage + this.infoD.kindNum.ToString() + "-3.png");
                    this.cutImageList.Images.Add(img);
                }
                if (File.Exists(Util.file_CGCutImage + this.infoD.kindNum.ToString() + "-4.png"))
                {
                    Image img = Image.FromFile(Util.file_CGCutImage + this.infoD.kindNum.ToString() + "-4.png");
                    this.cutImageList.Images.Add(img);
                }
                if (File.Exists(Util.file_CGCutImage + this.infoD.kindNum.ToString() + "-5.png"))
                {
                    Image img = Image.FromFile(Util.file_CGCutImage + this.infoD.kindNum.ToString() + "-5.png");
                    this.cutImageList.Images.Add(img);
                }
                if (File.Exists(Util.file_CGCutImage + this.infoD.kindNum.ToString() + "-6.png"))
                {
                    Image img = Image.FromFile(Util.file_CGCutImage + this.infoD.kindNum.ToString() + "-6.png");
                    this.cutImageList.Images.Add(img);
                }
                if (File.Exists(Util.file_CGCutImage + this.infoD.kindNum.ToString() + "-7.png"))
                {
                    Image img = Image.FromFile(Util.file_CGCutImage + this.infoD.kindNum.ToString() + "-7.png");
                    this.cutImageList.Images.Add(img);
                }
                if (File.Exists(Util.file_CGCutImage + this.infoD.kindNum.ToString() + "-8.png"))
                {
                    Image img = Image.FromFile(Util.file_CGCutImage + this.infoD.kindNum.ToString() + "-8.png");
                    this.cutImageList.Images.Add(img);
                }
                if (File.Exists(Util.file_CGCutImage + this.infoD.kindNum.ToString() + "-9.png"))
                {
                    Image img = Image.FromFile(Util.file_CGCutImage + this.infoD.kindNum.ToString() + "-9.png");
                    this.cutImageList.Images.Add(img);
                }
            }

            if (bAdd)
            {
                cbKind.SelectedIndex = 0;
                cbBlack.SelectedIndex = 0;
            }
            else
            {
                oldName = this.infoD.name;
                cbKind.Text = this.infoD.kind;
                tbName.Text = this.infoD.name;
                cbKind.Enabled = false;

                tbText.Text = this.infoD.text;
                tbText_TextChanged(null, null);
                cbBlack.SelectedIndex = (this.infoD.isBlack ? 1 : 0);

                for (int i = 0; i < 9; i++)
                {
                    if (this.infoD.cut[i])
                    {
                        ListViewItem lvi = new ListViewItem();
                        lvi.ImageIndex = i;
                        lvi.Text = (i + 1).ToString();
                        this.cutImageLV.Items.Add(lvi);
                    }
                }
            }
        }

        private void tbText_TextChanged(object sender, EventArgs e)
        {
            int len = tbText.Text.Length;
            if (len > 0)
            {
                int cnt = Encoding.UTF8.GetByteCount(tbText.Text);
                lbByte.Text = string.Format("{0} / {1}", cnt.ToString(), lvMaxLen.ToString());
            }
            else
            {
                lbByte.Text = "0 / " + lvMaxLen.ToString();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbName.Text))
            {
                MessageBox.Show(LangPack.GetMongolian("Please enter the CG title."), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbName.Focus();
                return;
            }

            if (string.IsNullOrEmpty(tbText.Text))
            {
                MessageBox.Show(LangPack.GetMongolian("Please enter the CG text."), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbText.Focus();
                return;
            }

            if (bAdd)
            {
                BoardInfo info = brdMng.dicBrd[cbKind.Text];
                if (info.dicText != null && info.dicText.ContainsKey(tbName.Text))
                {
                    MessageBox.Show(LangPack.GetMongolian("The same title already exists. Please enter check again after."), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    tbName.Focus();
                    return;
                }

                BoardInfoDetail dinfo = new BoardInfoDetail();
                dinfo.name = tbName.Text;
                dinfo.text = tbText.Text;
                dinfo.kindNum = info.kindNum;
                dinfo.kind = cbKind.Text;
                dinfo.isBlack = (cbBlack.SelectedIndex == 0 ? false : true);

                if (this.cutInfoD != null)
                {
                    for (int i = 0; i < 9; i++)
                    {
                        dinfo.cut[i] = this.cutInfoD.cut[i];
                    }
                }

                brdMng.dicBrd[cbKind.Text].dicText.Add(tbName.Text, dinfo);

                MessageBox.Show(LangPack.GetMongolian("Stored."), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                ResetCtrl();
            }
            else
            {
                // Name(Key) 수정
                if (this.oldName != tbName.Text)
                {
                    // 기존 Name(Key) 삭제
                    brdMng.dicBrd[cbKind.Text].dicText.Remove(this.oldName);

                    // 새로운 Name(Key) 등록
                    BoardInfoDetail dinfo = new BoardInfoDetail();
                    dinfo.name = tbName.Text;
                    dinfo.text = tbText.Text;
                    dinfo.kind = cbKind.Text;
                    dinfo.kindNum = infoD.kindNum;
                    dinfo.isBlack = (cbBlack.SelectedIndex == 0 ? false : true);

                    for (int i = 0; i < 9; i++)
                    {
                        dinfo.cut[i] = this.cutInfoD.cut[i];
                    }

                    brdMng.dicBrd[cbKind.Text].dicText.Add(tbName.Text, dinfo);
                }
                else
                {
                    brdMng.dicBrd[cbKind.Text].dicText[tbName.Text].text = tbText.Text;
                    brdMng.dicBrd[cbKind.Text].dicText[tbName.Text].isBlack = (cbBlack.SelectedIndex == 0 ? false : true);

                    for (int i = 0; i < 9; i++)
                    {
                        brdMng.dicBrd[cbKind.Text].dicText[tbName.Text].cut[i] = this.cutInfoD.cut[i];
                    }
                }

                brdMng.SaveBrdData();
                MessageBox.Show(LangPack.GetMongolian("Modified."), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Close();
            }

            if (this.OnCGChangeEvt != null)
            {
                this.OnCGChangeEvt(this, new CGChangeEventArgs());
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            brdMng.SaveBrdData();

            this.Close();
        }

        private void ResetCtrl()
        {
            tbName.Text = "";
            tbText.Text = "";
            cbBlack.SelectedIndex = 0;
        }

        private void menuForeColor_Click(object sender, EventArgs e)
        {
            int index = tbText.SelectionStart;

            string stTemp = "{" + (sender as ToolStripMenuItem).Text.Substring(0, 1) + "}";

            tbText.Text = tbText.Text.Insert(index, stTemp);
            tbText_TextChanged(null, null);

            tbText.SelectionStart = index + stTemp.Length;
        }

        private void menuBackColor_Click(object sender, EventArgs e)
        {
            int index = tbText.SelectionStart;

            string stTemp = "{" + (sender as ToolStripMenuItem).Text.ToLower() + "}";

            tbText.Text = tbText.Text.Insert(index, stTemp);
            tbText_TextChanged(null, null);

            tbText.SelectionStart = index + stTemp.Length;
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            BoardViewForm form = new BoardViewForm(tbText.Text, cbBlack.SelectedIndex == 0 ? false : true, this.primary);
            form.ShowDialog();
        }

        private void tbText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (bCtrlV)
                e.Handled = true;

            if (e.KeyChar == '\r')
            {
                e.Handled = true;
            }

            if (bOpenBrackets)
            {
                e.KeyChar = '[';
                bOpenBrackets = false;
            }
            else if (bCloseBrackets)
            {
                e.KeyChar = ']';
                bCloseBrackets = false;
            }

            TextBox tb = sender as TextBox;

            int cnt = Encoding.UTF8.GetByteCount(tb.Text);

            if (cnt >= this.lvMaxLen)
            {
                if (e.KeyChar != (char)Keys.Delete && e.KeyChar != (char)Keys.Back)
                    e.Handled = true;
            }
        }

        private void tbText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.V)
                bCtrlV = true;
            else
                bCtrlV = false;

            this.shiftFlag = e.Shift;

            if ((this.shiftFlag == true) && (e.KeyValue == 219))
                bOpenBrackets = true;
            else if ((this.shiftFlag == true) && (e.KeyValue == 221))
                bCloseBrackets = true;
        }

        private void tbText_KeyUp(object sender, KeyEventArgs e)
        {
            this.shiftFlag = e.Shift;
        }

        /// <summary>
        /// cut add 버튼 클릭 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cutAddBtn_Click(object sender, EventArgs e)
        {
            BoardInfo info = brdMng.dicBrd[cbKind.Text];
            BoardInfo tmp = new BoardInfo();

            if (cutInfoD == null)
            {
                this.cutInfoD = new BoardInfoDetail();
            }

            cutInfoD.kindNum = info.kindNum;
            cutInfoD.kind = cbKind.Text;

            tmp.kind = cbKind.Text;
            tmp.kindNum = info.kindNum;
            tmp.dicText.Add(cutInfoD.kind, cutInfoD);

            using (BoardCutMng form = new BoardCutMng(tmp, this.primary))
            {
                form.OnCGCutEvt += new EventHandler<CGCutImageEventArgs>(form_OnCGCutEvt);
                form.ShowDialog();
                form.OnCGCutEvt -= new EventHandler<CGCutImageEventArgs>(form_OnCGCutEvt);
            }
        }

        /// <summary>
        /// CG CUT 선택 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void form_OnCGCutEvt(object sender, CGCutImageEventArgs e)
        {
            MethodInvoker set = delegate()
            {
                bool cutFlag = false;
                bool cutClear = false;
                string[] tmp = this.tbText.Text.Split('{');
                string cutTmp = string.Empty;
                this.cutInfoD = e.Info;

                this.cutImageList.Images.Clear();
                this.cutImageLV.Items.Clear();

                if (File.Exists(Util.file_CGCutImage + e.Info.kindNum.ToString() + "-1.png"))
                {
                    Image img = Image.FromFile(Util.file_CGCutImage + e.Info.kindNum.ToString() + "-1.png");
                    this.cutImageList.Images.Add(img);
                }
                if (File.Exists(Util.file_CGCutImage + e.Info.kindNum.ToString() + "-2.png"))
                {
                    Image img = Image.FromFile(Util.file_CGCutImage + e.Info.kindNum.ToString() + "-2.png");
                    this.cutImageList.Images.Add(img);
                }
                if (File.Exists(Util.file_CGCutImage + e.Info.kindNum.ToString() + "-3.png"))
                {
                    Image img = Image.FromFile(Util.file_CGCutImage + e.Info.kindNum.ToString() + "-3.png");
                    this.cutImageList.Images.Add(img);
                }
                if (File.Exists(Util.file_CGCutImage + e.Info.kindNum.ToString() + "-4.png"))
                {
                    Image img = Image.FromFile(Util.file_CGCutImage + e.Info.kindNum.ToString() + "-4.png");
                    this.cutImageList.Images.Add(img);
                }
                if (File.Exists(Util.file_CGCutImage + e.Info.kindNum.ToString() + "-5.png"))
                {
                    Image img = Image.FromFile(Util.file_CGCutImage + e.Info.kindNum.ToString() + "-5.png");
                    this.cutImageList.Images.Add(img);
                }
                if (File.Exists(Util.file_CGCutImage + e.Info.kindNum.ToString() + "-6.png"))
                {
                    Image img = Image.FromFile(Util.file_CGCutImage + e.Info.kindNum.ToString() + "-6.png");
                    this.cutImageList.Images.Add(img);
                }
                if (File.Exists(Util.file_CGCutImage + e.Info.kindNum.ToString() + "-7.png"))
                {
                    Image img = Image.FromFile(Util.file_CGCutImage + e.Info.kindNum.ToString() + "-7.png");
                    this.cutImageList.Images.Add(img);
                }
                if (File.Exists(Util.file_CGCutImage + e.Info.kindNum.ToString() + "-8.png"))
                {
                    Image img = Image.FromFile(Util.file_CGCutImage + e.Info.kindNum.ToString() + "-8.png");
                    this.cutImageList.Images.Add(img);
                }
                if (File.Exists(Util.file_CGCutImage + e.Info.kindNum.ToString() + "-9.png"))
                {
                    Image img = Image.FromFile(Util.file_CGCutImage + e.Info.kindNum.ToString() + "-9.png");
                    this.cutImageList.Images.Add(img);
                }

                for (int i = 0; i < 9; i++)
                {
                    if (e.Info.cut[i])
                    {
                        cutTmp = cutTmp + (i + 1).ToString();

                        ListViewItem lvi = new ListViewItem();
                        lvi.ImageIndex = i;
                        lvi.Text = (i + 1).ToString();
                        this.cutImageLV.Items.Add(lvi);

                        if (i != 8)
                        {
                            cutTmp = cutTmp + ",";
                        }
                    }
                }

                if (cutTmp != string.Empty)
                {
                    if (cutTmp[cutTmp.Length - 1] == ',')
                    {
                        cutTmp = cutTmp.Substring(0, cutTmp.Length - 1);
                    }
                }

                if (cutTmp == string.Empty)
                {
                    cutClear = true;
                }

                string cutPrt = "{CUT" + cutTmp + "}";

                try
                {
                    if (tmp[1].Substring(0, 3) == "CUT")
                    {
                        cutFlag = true;
                    }
                }
                catch (Exception ex)
                {
                    cutFlag = false;
                }

                if (!cutFlag) //기존에 cut 선택이 없으면..
                {
                    if (!cutClear) //컷 해제가 아니면
                    {
                        this.tbText.Text = cutPrt + this.tbText.Text;
                    }
                }
                else
                {
                    int endPoint = this.tbText.Text.IndexOf('}');
                    this.tbText.Text = this.tbText.Text.Substring(endPoint + 1, this.tbText.Text.Length - (endPoint + 1));

                    if (!cutClear)
                    {
                        this.tbText.Text = cutPrt + this.tbText.Text;
                    }
                }
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

        private void InitLang()
        {
            btnClose.Text = LangPack.GetClose();
            btnSave.Text = LangPack.GetSave();
            btnView.Text = LangPack.GetPreview();
            cutAddBtn.Text = LangPack.GetCutAdd();
            label1.Text = LangPack.GetKind();
            label5.Text = LangPack.GetName();
            label2.Text = LangPack.GetText();
            label7.Text = LangPack.GetCut();

            if (LangPack.IsEng)
            {
                label6.Text = "CG Edit";
                label4.Text = "Byte count : ";
            }
            else
            {
                label6.Text = "Текст засварлах";
                label4.Text = "Бит : ";
            }
        }
    }

    /// <summary>
    /// CG 추가 및 변경 시 발생하는 이벤트 아규먼트 클래스
    /// </summary>
    public class CGChangeEventArgs : EventArgs
    {
        /// <summary>
        /// 생성자
        /// </summary>
        public CGChangeEventArgs()
        {
        }
    }
}