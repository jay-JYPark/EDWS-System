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
    public partial class AutoEditForm : Form
    {
        StMsgDataMng stoMsg = null;

        public AutoEditForm(string _isPrimary)
        {
            InitializeComponent();

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
            InitLang();
            this.stoMsg = StMsgDataMng.GetMsgMng();

            foreach (KeyValuePair<string, MsgInfo> pair in stoMsg.dicMsg)
            {
                MsgInfo msg = pair.Value;

                comboBox1.Items.Add("(" + msg.msgNum + ")" + msg.msgName);
                comboBox2.Items.Add("(" + msg.msgNum + ")" + msg.msgName);
            }

            this.numericUpDown1.Value = (decimal)Util.autoInfo.intensity;

            if (Util.autoInfo.highAuto)
            {
                this.radioButton1.Checked = true;
                RadioButton rb = new RadioButton();
                rb.Tag = "ha";
                rb.Checked = true;
                this.radioButton1_CheckedChanged((object)rb, new EventArgs());
            }
            else
            {
                this.radioButton2.Checked = true;
                RadioButton rb = new RadioButton();
                rb.Tag = "hm";
                rb.Checked = true;
                this.radioButton1_CheckedChanged((object)rb, new EventArgs());
            }

            if (Util.autoInfo.lowAuto)
            {
                this.radioButton3.Checked = true;
                RadioButton rb = new RadioButton();
                rb.Tag = "la";
                rb.Checked = true;
                this.radioButton3_CheckedChanged((object)rb, new EventArgs());
            }
            else
            {
                this.radioButton4.Checked = true;
                RadioButton rb = new RadioButton();
                rb.Tag = "lm";
                rb.Checked = true;
                this.radioButton3_CheckedChanged((object)rb, new EventArgs());
            }

            if (Util.autoInfo.highMsg != string.Empty)
            {
                this.comboBox1.SelectedItem = Util.autoInfo.highMsg;
            }

            if (Util.autoInfo.lowMsg != string.Empty)
            {
                this.comboBox2.SelectedItem = Util.autoInfo.lowMsg;
            }

            this.numericUpDown2.Value = Util.autoInfo.highTime;
            this.numericUpDown3.Value = Util.autoInfo.lowTime;
            this.checkBox1.Checked = Util.autoInfo.highCBSUse;
            this.checkBox2.Checked = Util.autoInfo.lowCBSUse;
        }

        /// <summary>
        /// save 버튼 클릭 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (this.radioButton1.Checked)
            {
                Util.autoInfo.highAuto = true;
            }
            else if (this.radioButton2.Checked)
            {
                Util.autoInfo.highAuto = false;
            }

            if (this.radioButton3.Checked)
            {
                Util.autoInfo.lowAuto = true;
            }
            else if (this.radioButton4.Checked)
            {
                Util.autoInfo.lowAuto = false;
            }

            Util.autoInfo.highCBSUse = checkBox1.Checked;
            Util.autoInfo.lowCBSUse = checkBox2.Checked;
            Util.autoInfo.highTime = (int)this.numericUpDown2.Value;
            Util.autoInfo.lowTime = (int)this.numericUpDown3.Value;
            Util.autoInfo.intensity = (float)this.numericUpDown1.Value;
            Util.autoInfo.highMsg = this.comboBox1.SelectedItem.ToString();
            Util.autoInfo.lowMsg = this.comboBox2.SelectedItem.ToString();
            Util.SaveAutoinfo();
        }

        /// <summary>
        /// close 버튼 클릭 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// high 라디오 버튼
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as RadioButton).Tag.ToString() == "ha")
            {
                if (this.radioButton1.Checked)
                {
                    this.numericUpDown2.Enabled = false;
                }
            }
            else
            {
                if (this.radioButton2.Checked)
                {
                    this.numericUpDown2.Enabled = true;
                }
            }
        }

        /// <summary>
        /// low 라디오 버튼
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as RadioButton).Tag.ToString() == "la")
            {
                if (this.radioButton3.Checked)
                {
                    this.numericUpDown3.Enabled = false;
                }
            }
            else
            {
                if (this.radioButton4.Checked)
                {
                    this.numericUpDown3.Enabled = true;
                }
            }
        }

        private void InitLang()
        {
            if (LangPack.IsEng)
            {
                label8.Text = "Auto Alert Edit";
                groupBox1.Text = "Intensity";
                groupBox2.Text = "High Intensity";
                groupBox6.Text = "Low Intensity";
                groupBox3.Text = "Alert";
                groupBox9.Text = "Alert";
                groupBox4.Text = "CBS";
                groupBox8.Text = "CBS";
                radioButton1.Text = "Auto";
                radioButton3.Text = "Auto";
                radioButton2.Text = "Manual";
                radioButton4.Text = "Manual";
                label1.Text = "sec";
                label2.Text = "sec";
                checkBox1.Text = "Send to CBS";
                checkBox2.Text = "Send to CBS";
                btnSave.Text = "Save";
                btnClose.Text = "Close";
                groupBox5.Text = "Stored Message";
                groupBox7.Text = "Stored Message";
            }
            else
            {
                label8.Text = "Автомат түгшүүр засварлах";
                groupBox1.Text = "Газар хөдлөлтийн хүчдэл";
                groupBox2.Text = "Өндөр хүчдэл";
                groupBox6.Text = "Бага хүчдэл";
                groupBox3.Text = "Түгшүүр";
                groupBox9.Text = "Түгшүүр";
                groupBox4.Text = "Масс мессеж систем";
                groupBox8.Text = "Масс мессеж систем";
                radioButton1.Text = "Автомат";
                radioButton3.Text = "Автомат";
                radioButton2.Text = "Гар ажиллагаа";
                radioButton4.Text = "Гар ажиллагаа";
                label1.Text = "Сек";
                label2.Text = "Сек";
                checkBox1.Text = "Масс мессеж систем рүү илгээх";
                checkBox2.Text = "Масс мессеж систем рүү илгээх";
                btnSave.Text = "Хадгалах";
                btnClose.Text = "Хаах";
                groupBox5.Text = "Хадгалагдсан мэдээлэл";
                groupBox7.Text = "Хадгалагдсан мэдээлэл";
            }
        }
    }
}