using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Media;
using System.Threading;
using System.IO;
using Adeng.Framework.Util;
using System.Diagnostics;

namespace Mews.Svr.Broad
{
    public partial class OrderSelLiveSiren : UserControl
    {
        private delegate void LiveSirenEventArgsHandler(object sender, LiveSirenEventArgs lsea);
        public event EventHandler<LiveSirenEventArgs> OnSirenEvt;
        private readonly string IniFilePath = "C:\\Program Files\\adeng\\MewsBroad\\Setting.ini";
        private readonly string setting = "SETTING";
        private byte mode = 0;
        private SoundPlayer sp = null;
        private Thread playTD = null;
        private string[] msgTime = new string[] { "303", "130" };

        public OrderSelLiveSiren()
        {
            InitializeComponent();

            label2.Text = LangPack.GetSelSiren();
            btnMic.Text = LangPack.GetSiren1();
            btnRec.Text = LangPack.GetSiren2();

            try
            {
                if (AdengUtil.INIReadValueString(this.setting, "siren1", this.IniFilePath) != string.Empty)
                {
                    msgTime[0] = AdengUtil.INIReadValueString(this.setting, "siren1", this.IniFilePath);
                }

                if (AdengUtil.INIReadValueString(this.setting, "siren2", this.IniFilePath) != string.Empty)
                {
                    msgTime[1] = AdengUtil.INIReadValueString(this.setting, "siren2", this.IniFilePath);
                }

                if (!(System.IO.File.Exists(Util.file_Siren + "siren_1.wav")))
                {
                    this.LameMp3ToWav(Util.file_Siren + "siren_1.mp3", Util.file_Siren + "siren_1.wav");
                }

                if (!(System.IO.File.Exists(Util.file_Siren + "siren_2.wav")))
                {
                    this.LameMp3ToWav(Util.file_Siren + "siren_2.mp3", Util.file_Siren + "siren_2.wav");
                }
            }
            catch (Exception ex)
            {
                AdengUtil.INIWriteValueString(this.setting, "siren1", msgTime[0], IniFilePath);
                AdengUtil.INIWriteValueString(this.setting, "siren2", msgTime[1], IniFilePath);
            }
        }

        public void setBtnColor(byte _kind)
        {
            mode = _kind;
            this.btnMic.BackColor = Color.Black;
            this.btnRec.BackColor = Color.Black;

            if (AdengUtil.INIReadValueString(this.setting, "siren1", this.IniFilePath) != string.Empty)
            {
                msgTime[0] = AdengUtil.INIReadValueString(this.setting, "siren1", this.IniFilePath);
            }

            if (AdengUtil.INIReadValueString(this.setting, "siren2", this.IniFilePath) != string.Empty)
            {
                msgTime[1] = AdengUtil.INIReadValueString(this.setting, "siren2", this.IniFilePath);
            }
        }

        public void init()
        {
            this.btnMic.BackColor = Color.Black;
            this.btnRec.BackColor = Color.Black;

            if (this.sp != null)
            {
                this.sp.Stop();
                this.sp = null;
            }

            if (this.playTD != null)
            {
                this.playTD.Abort();
                this.playTD = null;
            }
        }

        private void btnMic_Click(object sender, EventArgs e)
        {
            if (this.sp != null)
            {
                this.sp.Stop();
                this.sp = null;
            }

            if (this.playTD != null)
            {
                this.playTD.Abort();
                this.playTD = null;
            }

            if ((sender as GlassButton).Tag.ToString() == "1")
            {
                if (this.btnMic.BackColor == Color.Black) //정지
                {
                    this.btnMic.BackColor = Util.lstColor[mode];
                    this.btnRec.BackColor = Color.Black;
                    this.playSiren(1);
                }
                else //시작
                {
                    this.btnMic.BackColor = Color.Black;
                }
            }
            else if ((sender as GlassButton).Tag.ToString() == "2")
            {
                if (this.btnRec.BackColor == Color.Black) //정지
                {
                    this.btnRec.BackColor = Util.lstColor[mode];
                    this.btnMic.BackColor = Color.Black;
                    this.playSiren(2);
                }
                else //시작
                {
                    this.btnRec.BackColor = Color.Black;
                }
            }
        }

        private void playSiren(byte _num)
        {
            if (!(System.IO.File.Exists(Util.file_Siren + "siren_" + _num.ToString() + ".wav")))
            {
                MessageBox.Show(LangPack.GetMongolian("Sorry! can not play sound because file don't exists."), LangPack.GetMongolian("Siren"), MessageBoxButtons.OK, MessageBoxIcon.Error);

                if (_num == 1)
                {
                    btnMic.BackColor = Color.Black;
                }
                else
                {
                    btnRec.BackColor = Color.Black;
                }

                return;
            }

            if (this.sp == null)
            {
                this.sp = new SoundPlayer(Util.file_Siren + "siren_" + _num.ToString() + ".wav");
            }
            else
            {
                this.sp.SoundLocation = Util.file_Siren + "siren_" + _num.ToString() + ".wav";
            }

            this.sp.Play();
            this.playTD = new Thread(new ParameterizedThreadStart(playMethod));
            this.playTD.Start(this.msgTime[_num - 1]);
        }

        private void playMethod(object _time)
        {
            int min = int.Parse((string)_time) / 60;
            int sec = (int.Parse((string)_time) - (60 * min));

            DateTime dt = DateTime.Now;

            if (min != 0)
            {
                dt = DateTime.Now.AddMinutes((double)min);
            }

            if (sec != 0)
            {
                dt = dt.AddSeconds((double)sec);
            }

            while (true)
            {
                if (dt < DateTime.Now)
                {
                    MethodInvoker mi = delegate()
                    {
                        this.init();
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

                Thread.Sleep(20);
            }
        }

        private void LameMp3ToWav(string mp3File, string outwavFile)
        {
            ProcessStartInfo psi = new ProcessStartInfo();
            psi.FileName = Util.file_Siren + "lame.exe";
            psi.Arguments = mp3File + " " + outwavFile;
            psi.WindowStyle = ProcessWindowStyle.Hidden;
            Process p = Process.Start(psi);
            p.WaitForExit();
        }
    }

    /// <summary>
    /// 이벤트 아규먼트 클래스
    /// </summary>
    public class LiveSirenEventArgs : EventArgs
    {
        private byte num = 0;

        public byte Num
        {
            get { return this.num; }
            set { this.num = value; }
        }

        public LiveSirenEventArgs(byte _num)
        {
            this.num = _num;
        }
    }
}