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
    public partial class BoardCutMng : Form
    {
        private delegate void CGCutImageEventArgsHandler(object sender, CGCutImageEventArgs cgciea);

        public event EventHandler<CGCutImageEventArgs> OnCGCutEvt;

        private BoardInfo bdInfo = null;
        private BoardInfoDetail infoDetail = new BoardInfoDetail();
        private PictureBox[] pbList = new PictureBox[18];
        private Panel[] pnList = new Panel[9];

        public BoardCutMng()
        {
            InitializeComponent();
        }

        public BoardCutMng(BoardInfo info, string _isPrimary)
            : this()
        {
            this.bdInfo = info;
            this.infoDetail.kindNum = info.kindNum;
            this.InitLang();
            this.init();

            if (_isPrimary == "1")
            {
                this.panel1.BackgroundImage = MewsBroad.Properties.Resources.bgTitle;
            }
            else
            {
                this.panel1.BackgroundImage = MewsBroad.Properties.Resources.bgTitleGreen;
            }
        }

        private void init()
        {
            this.pictureBox3.Tag = 3;
            this.pictureBox4.Tag = 4;
            this.pictureBox5.Tag = 5;
            this.pictureBox6.Tag = 6;
            this.pictureBox7.Tag = 7;
            this.pictureBox8.Tag = 8;
            this.pictureBox9.Tag = 9;
            this.pictureBox10.Tag = 10;
            this.pictureBox11.Tag = 11;

            this.panel3.Tag = 3;
            this.panel4.Tag = 4;
            this.panel5.Tag = 5;
            this.panel6.Tag = 6;
            this.panel7.Tag = 7;
            this.panel8.Tag = 8;
            this.panel9.Tag = 9;
            this.panel10.Tag = 10;
            this.panel11.Tag = 11;

            if (File.Exists(Util.file_CGCutImage + this.bdInfo.kindNum.ToString() + "-1.png"))
            {
                this.pictureBox3.Image = Image.FromFile(Util.file_CGCutImage + this.bdInfo.kindNum.ToString() + "-1.png");
            }
            if (File.Exists(Util.file_CGCutImage + this.bdInfo.kindNum.ToString() + "-2.png"))
            {
                this.pictureBox4.Image = Image.FromFile(Util.file_CGCutImage + this.bdInfo.kindNum.ToString() + "-2.png");
            }
            if (File.Exists(Util.file_CGCutImage + this.bdInfo.kindNum.ToString() + "-3.png"))
            {
                this.pictureBox5.Image = Image.FromFile(Util.file_CGCutImage + this.bdInfo.kindNum.ToString() + "-3.png");
            }
            if (File.Exists(Util.file_CGCutImage + this.bdInfo.kindNum.ToString() + "-4.png"))
            {
                this.pictureBox6.Image = Image.FromFile(Util.file_CGCutImage + this.bdInfo.kindNum.ToString() + "-4.png");
            }
            if (File.Exists(Util.file_CGCutImage + this.bdInfo.kindNum.ToString() + "-5.png"))
            {
                this.pictureBox7.Image = Image.FromFile(Util.file_CGCutImage + this.bdInfo.kindNum.ToString() + "-5.png");
            }
            if (File.Exists(Util.file_CGCutImage + this.bdInfo.kindNum.ToString() + "-6.png"))
            {
                this.pictureBox8.Image = Image.FromFile(Util.file_CGCutImage + this.bdInfo.kindNum.ToString() + "-6.png");
            }
            if (File.Exists(Util.file_CGCutImage + this.bdInfo.kindNum.ToString() + "-7.png"))
            {
                this.pictureBox9.Image = Image.FromFile(Util.file_CGCutImage + this.bdInfo.kindNum.ToString() + "-7.png");
            }
            if (File.Exists(Util.file_CGCutImage + this.bdInfo.kindNum.ToString() + "-8.png"))
            {
                this.pictureBox10.Image = Image.FromFile(Util.file_CGCutImage + this.bdInfo.kindNum.ToString() + "-8.png");
            }
            if (File.Exists(Util.file_CGCutImage + this.bdInfo.kindNum.ToString() + "-9.png"))
            {
                this.pictureBox11.Image = Image.FromFile(Util.file_CGCutImage + this.bdInfo.kindNum.ToString() + "-9.png");
            }

            if (this.bdInfo.dicText.Count > 0)
            {
                BoardInfoDetail tmp = this.bdInfo.dicText[this.bdInfo.kind];
                PictureBox pic = new PictureBox();

                for (int i = 3; i < 12; i++)
                {
                    if (tmp.cut[i - 3])
                    {
                        pic.Tag = i;
                        this.pictureBox3_Click(pic, new EventArgs());
                    }
                }
            }
        }

        private void pictureBox3_MouseEnter(object sender, EventArgs e)
        {
            object obj = (sender as PictureBox).Tag;

            switch((int)(obj))
            {
                case 3:
                    this.panel3.BackColor = Color.Lime;
                    break;
                case 4:
                    this.panel4.BackColor = Color.Lime;
                    break;
                case 5:
                    this.panel5.BackColor = Color.Lime;
                    break;
                case 6:
                    this.panel6.BackColor = Color.Lime;
                    break;
                case 7:
                    this.panel7.BackColor = Color.Lime;
                    break;
                case 8:
                    this.panel8.BackColor = Color.Lime;
                    break;
                case 9:
                    this.panel9.BackColor = Color.Lime;
                    break;
                case 10:
                    this.panel10.BackColor = Color.Lime;
                    break;
                case 11:
                    this.panel11.BackColor = Color.Lime;
                    break;
            }
        }

        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            this.panel3.BackColor = Color.White;
            this.panel4.BackColor = Color.White;
            this.panel5.BackColor = Color.White;
            this.panel6.BackColor = Color.White;
            this.panel7.BackColor = Color.White;
            this.panel8.BackColor = Color.White;
            this.panel9.BackColor = Color.White;
            this.panel10.BackColor = Color.White;
            this.panel11.BackColor = Color.White;
        }

        /// <summary>
        /// select
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            if (this.OnCGCutEvt != null)
            {
                this.OnCGCutEvt(this, new CGCutImageEventArgs(this.infoDetail));
            }

            this.Close();
        }

        /// <summary>
        /// clear
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 9; i++)
            {
                this.infoDetail.cut[i] = false;
            }

            if (this.OnCGCutEvt != null)
            {
                this.OnCGCutEvt(this, new CGCutImageEventArgs(this.infoDetail));
            }

            this.Close();
        }

        /// <summary>
        /// pictureBox Click, 체크 또는 해제
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            object obj = (sender as PictureBox).Tag;

            switch ((int)(obj))
            {
                case 3:
                    if (this.pictureBox3.Image == null)
                    {
                        return;
                    }

                    if (this.pictureBox13.Visible)
                    {
                        this.infoDetail.cut[0] = false;
                        this.panel13.BackColor = Color.White;
                        this.pictureBox13.Visible = false;
                    }
                    else
                    {
                        this.infoDetail.cut[0] = true;
                        this.panel13.BackColor = Color.Lime;
                        this.pictureBox13.Visible = true;
                    }
                    break;
                case 4:
                    if (this.pictureBox4.Image == null)
                    {
                        return;
                    }

                    if (this.pictureBox14.Visible)
                    {
                        this.infoDetail.cut[1] = false;
                        this.panel14.BackColor = Color.White;
                        this.pictureBox14.Visible = false;
                    }
                    else
                    {
                        this.infoDetail.cut[1] = true;
                        this.panel14.BackColor = Color.Lime;
                        this.pictureBox14.Visible = true;
                    }
                    break;
                case 5:
                    if (this.pictureBox5.Image == null)
                    {
                        return;
                    }

                    if (this.pictureBox15.Visible)
                    {
                        this.infoDetail.cut[2] = false;
                        this.panel15.BackColor = Color.White;
                        this.pictureBox15.Visible = false;
                    }
                    else
                    {
                        this.infoDetail.cut[2] = true;
                        this.panel15.BackColor = Color.Lime;
                        this.pictureBox15.Visible = true;
                    }
                    break;
                case 6:
                    if (this.pictureBox6.Image == null)
                    {
                        return;
                    }

                    if (this.pictureBox16.Visible)
                    {
                        this.infoDetail.cut[3] = false;
                        this.panel16.BackColor = Color.White;
                        this.pictureBox16.Visible = false;
                    }
                    else
                    {
                        this.infoDetail.cut[3] = true;
                        this.panel16.BackColor = Color.Lime;
                        this.pictureBox16.Visible = true;
                    }
                    break;
                case 7:
                    if (this.pictureBox7.Image == null)
                    {
                        return;
                    }

                    if (this.pictureBox17.Visible)
                    {
                        this.infoDetail.cut[4] = false;
                        this.panel17.BackColor = Color.White;
                        this.pictureBox17.Visible = false;
                    }
                    else
                    {
                        this.infoDetail.cut[4] = true;
                        this.panel17.BackColor = Color.Lime;
                        this.pictureBox17.Visible = true;
                    }
                    break;
                case 8:
                    if (this.pictureBox8.Image == null)
                    {
                        return;
                    }

                    if (this.pictureBox18.Visible)
                    {
                        this.infoDetail.cut[5] = false;
                        this.panel18.BackColor = Color.White;
                        this.pictureBox18.Visible = false;
                    }
                    else
                    {
                        this.infoDetail.cut[5] = true;
                        this.panel18.BackColor = Color.Lime;
                        this.pictureBox18.Visible = true;
                    }
                    break;
                case 9:
                    if (this.pictureBox9.Image == null)
                    {
                        return;
                    }

                    if (this.pictureBox19.Visible)
                    {
                        this.infoDetail.cut[6] = false;
                        this.panel19.BackColor = Color.White;
                        this.pictureBox19.Visible = false;
                    }
                    else
                    {
                        this.infoDetail.cut[6] = true;
                        this.panel19.BackColor = Color.Lime;
                        this.pictureBox19.Visible = true;
                    }
                    break;
                case 10:
                    if (this.pictureBox10.Image == null)
                    {
                        return;
                    }

                    if (this.pictureBox20.Visible)
                    {
                        this.infoDetail.cut[7] = false;
                        this.panel20.BackColor = Color.White;
                        this.pictureBox20.Visible = false;
                    }
                    else
                    {
                        this.infoDetail.cut[7] = true;
                        this.panel20.BackColor = Color.Lime;
                        this.pictureBox20.Visible = true;
                    }
                    break;
                case 11:
                    if (this.pictureBox11.Image == null)
                    {
                        return;
                    }

                    if (this.pictureBox21.Visible)
                    {
                        this.infoDetail.cut[8] = false;
                        this.panel21.BackColor = Color.White;
                        this.pictureBox21.Visible = false;
                    }
                    else
                    {
                        this.infoDetail.cut[8] = true;
                        this.panel21.BackColor = Color.Lime;
                        this.pictureBox21.Visible = true;
                    }
                    break;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBox1.Checked)
            {
                if (this.pictureBox3.Image != null)
                {
                    this.pictureBox13.Visible = false;
                }

                if (this.pictureBox4.Image != null)
                {
                    this.pictureBox14.Visible = false;
                }

                if (this.pictureBox5.Image != null)
                {
                    this.pictureBox15.Visible = false;
                }

                if (this.pictureBox6.Image != null)
                {
                    this.pictureBox16.Visible = false;
                }

                if (this.pictureBox7.Image != null)
                {
                    this.pictureBox17.Visible = false;
                }

                if (this.pictureBox8.Image != null)
                {
                    this.pictureBox18.Visible = false;
                }

                if (this.pictureBox9.Image != null)
                {
                    this.pictureBox19.Visible = false;
                }

                if (this.pictureBox10.Image != null)
                {
                    this.pictureBox20.Visible = false;
                }

                if (this.pictureBox11.Image != null)
                {
                    this.pictureBox21.Visible = false;
                }
            }
            else
            {
                if (this.pictureBox3.Image != null)
                {
                    this.pictureBox13.Visible = true;
                }

                if (this.pictureBox4.Image != null)
                {
                    this.pictureBox14.Visible = true;
                }

                if (this.pictureBox5.Image != null)
                {
                    this.pictureBox15.Visible = true;
                }

                if (this.pictureBox6.Image != null)
                {
                    this.pictureBox16.Visible = true;
                }

                if (this.pictureBox7.Image != null)
                {
                    this.pictureBox17.Visible = true;
                }

                if (this.pictureBox8.Image != null)
                {
                    this.pictureBox18.Visible = true;
                }

                if (this.pictureBox9.Image != null)
                {
                    this.pictureBox19.Visible = true;
                }

                if (this.pictureBox10.Image != null)
                {
                    this.pictureBox20.Visible = true;
                }

                if (this.pictureBox11.Image != null)
                {
                    this.pictureBox21.Visible = true;
                }
            }

            PictureBox pic = new PictureBox();

            for (int i = 3; i < 12; i++)
            {
                pic.Tag = i;
                this.pictureBox3_Click(pic, new EventArgs());
            }
        }

        private void InitLang()
        {
            btnClear.Text = LangPack.Getclear();
            btnClose.Text = LangPack.Getselect();
            checkBox1.Text = LangPack.GetAllSelect();

            if (LangPack.IsEng)
            {
                label6.Text = "CG Edit";
            }
            else
            {
                label6.Text = "Текст засварлах";
            }
        }
    }

    public class CGCutImageEventArgs : EventArgs
    {
        private BoardInfoDetail info;

        public BoardInfoDetail Info
        {
            get { return this.info; }
            set { this.info = value; }
        }

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="_state"></param>
        public CGCutImageEventArgs(BoardInfoDetail _info)
        {
            this.info = _info;
        }
    }
}