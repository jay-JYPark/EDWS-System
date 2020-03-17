using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Mews.Lib.Mewsprt;
using Mews.Lib.MewsDataMng;
using Mews.Ctrl.Mewsge;
using Mews.Ctrl.Mewsmap;
using Mews.Lib.Mewstcp;
using Mews.Lib.MewsCtrlPrt;

namespace Mews.Svr.Broad
{
    public partial class OrderMode2 : UserControl
    {
        #region instance
        private delegate void VhfSatDataEventArgsHandler(object sender, VhfSatDataEventArgs vsdea);
        private delegate void SatAudioControlEventArgsHandler(object sender, SatAudioControlEventArgs sacea);

        public event EventHandler<VhfSatDataEventArgs> OnVhfDataResvEvt;
        public event EventHandler<VhfSatDataEventArgs> OnSatDataRecvEvt;
        public event EventHandler<VhfSatDataEventArgs> OnControlPNSendEvt;
        public event EventHandler<SatAudioControlEventArgs> OnSatAudioControlEvt;
        #endregion

        #region Fields
        private List<GlassButton> lstBtnMode = new List<GlassButton>();
        private List<GlassButton> lstBtnMedia = new List<GlassButton>();
        private List<GlassButton> lstBtnBaseGrp = new List<GlassButton>();
        private List<GlassButton> lstBtnBaseType = new List<GlassButton>();
        private List<Panel> lstBasePnl = new List<Panel>();
        
        private byte curMode = 0;
        private byte curStep = 0;
        private byte curMsgNum = 0;
        private byte curSirenNum = 0;
        private byte curKind = 0;
        private byte curDest = 0;
        private byte curMedia = 0;
        private byte curLiveType = 0;
        private byte curReptCnt = 0;
        private BoardInfoDetail curBoardInfo = null;
        private Group curGrp = null;
        private List<string> curDistLstIP = null;
        private List<string> curTermLstIP = null;
        private BroadMain mainForm = null;
        private string boardText = string.Empty;
        private bool bKeyOrder = false;
        private bool bLampRed = false;
        private bool bLampGreen = false;
        private bool bLampYellow = false;
        private bool bLampBuzzer = false;
        private bool bOperErr = false;
        private bool bOrderIng = false;
        private bool bTVAudio = false;
        private Timer endTimer = null;
        #endregion

        public byte CurMsgNum
        {
            get { return this.curMsgNum; }
            set { this.curMsgNum = value; }
        }

        public bool BTVAudio
        {
            get { return this.bTVAudio; }
            set { this.bTVAudio = value; }
        }

        public byte CurMedia
        {
            get { return this.curMedia; }
            set { this.curMedia = value; }
        }

        public byte CurDest
        {
            get { return this.curDest; }
            set { this.curDest = value; }
        }

        public byte CurReptCnt
        {
            get { return this.curReptCnt; }
            set { this.curReptCnt = value; }
        }

        public byte CurKind
        {
            get { return this.curKind; }
            set { this.curKind = value; }
        }

        public byte CurMode
        {
            get { return this.curMode; }
            set { this.curMode = value; }
        }

        public List<string> CurDistLstIP
        {
            get { return this.curDistLstIP; }
            set { this.curDistLstIP = value; }
        }

        public List<string> CurTermLstIP
        {
            get { return this.curTermLstIP; }
            set { this.curTermLstIP = value; }
        }

        public Group CurGrp
        {
            get { return this.curGrp; }
            set { this.curGrp = value; }
        }

        public string MicSet
        {
            get { return btnMicSet.Tag.ToString(); }
        }

        public BoardInfoDetail CurBoardInfo
        {
            get { return this.curBoardInfo; }
            set { this.curBoardInfo = value; }
        }

        public string BoardText
        {
            get { return this.boardText; }
            set { this.boardText = value; }
        }

        public OrderMode2()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 초기화#
        /// </summary>
        /// <param name="mainForm"></param>
        public void InitCtrl(BroadMain mainForm)
        {
            this.mainForm = mainForm;

            #region List Setting
            lstBtnMode.Add(btnModeReal);
            lstBtnMode.Add(btnModeTest);
            lstBtnMode.Add(btnModeDrill);
            lstBtnMedia.Add(btnMediaAll);
            lstBtnMedia.Add(btnMediaVhf);
            lstBtnMedia.Add(btnMediaSate);
            lstBtnBaseGrp.Add(btnBaseAll);
            lstBtnBaseGrp.Add(btnBaseGrp);
            lstBtnBaseGrp.Add(btnBaseIndi);
            lstBtnBaseType.Add(btnBaseReady);
            lstBtnBaseType.Add(btnBaseLive);
            lstBtnBaseType.Add(btnBaseMsg);
            lstBtnBaseType.Add(btnBaseStop);
            
            lstBasePnl.Add(pnlMode);
            lstBasePnl.Add(pnlMedia);
            lstBasePnl.Add(pnlBaseGrp);
            lstBasePnl.Add(pnlBaseType);
            lstBasePnl.Add(pnlConfirm);
            lstBasePnl.Add(pnlMic);
            #endregion

            foreach (GlassButton btn in lstBtnMode)
            {
                btn.Tag = "N";
            }

            foreach (GlassButton btn in lstBtnMedia)
            {
                btn.Tag = "N";
            }

            foreach (GlassButton btn in lstBtnBaseGrp)
            {
                btn.Tag = "N";
            }

            foreach (GlassButton btn in lstBtnBaseType)
            {
                btn.Tag = "N";
            }

            btnPre.Tag = "N";
            btnCancel.Tag = "N";
            btnConfirm.Tag = "N";

            this.curStep = (byte)Util.emPnl.mode;
            SetNextStep(this.curStep);
            btnMicSet.Tag = "start";
            this.glassButton1_Click(this, new EventArgs());
        }

        #region Btn Event Mode
        /// <summary>
        /// 실제 버튼 클릭 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnModeReal_Click(object sender, EventArgs e)
        {
            if (!bKeyOrder)
            {
                LoginForm form = new LoginForm(this.mainForm.isPrimary);
                if (DialogResult.Cancel == form.ShowDialog())
                    return;
            }

            this.curMode = (byte)Util.emMode.real;
            SetStepMode(sender as GlassButton);
        }

        /// <summary>
        /// 시험 버튼 클릭 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnModeTest_Click(object sender, EventArgs e)
        {
            this.curMode = (byte)Util.emMode.test;
            SetStepMode(sender as GlassButton);
        }

        /// <summary>
        /// 훈련 버튼 클릭 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnModeDrill_Click(object sender, EventArgs e)
        {
            if (!bKeyOrder)
            {
                LoginForm form = new LoginForm(this.mainForm.isPrimary);
                if (DialogResult.Cancel == form.ShowDialog())
                    return;
            }

            this.curMode = (byte)Util.emMode.drill;
            SetStepMode(sender as GlassButton);
        }

        private void SetStepMode(GlassButton btn)
        {
            foreach (GlassButton gtn in lstBtnMode)
            {
                SetBtnColor(gtn, (byte)Util.emMode.none);
            }

            this.curStep = (byte)Util.emPnl.media;
            SetStepAttr(btn);
        }
        #endregion

        #region Btn Event Media
        /// <summary>
        /// 발령매체 ALL 버튼 클릭 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMediaAll_Click(object sender, EventArgs e)
        {
            this.curMedia = (byte)Util.emMedia.all;
            SetStepMedia(sender as GlassButton);

            if (OnSatAudioControlEvt != null)
            {
                this.OnSatAudioControlEvt(this, new SatAudioControlEventArgs(1));
            }
        }

        /// <summary>
        /// VHF 버튼 클릭 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMediaVhf_Click(object sender, EventArgs e)
        {
            this.curMedia = (byte)Util.emMedia.vhf;
            SetStepMedia(sender as GlassButton);

            if (OnSatAudioControlEvt != null)
            {
                this.OnSatAudioControlEvt(this, new SatAudioControlEventArgs(2));
            }
        }

        /// <summary>
        /// SAT 버튼 클릭 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMediaSate_Click(object sender, EventArgs e)
        {
            this.curMedia = (byte)Util.emMedia.sat;
            SetStepMedia(sender as GlassButton);

            if (OnSatAudioControlEvt != null)
            {
                this.OnSatAudioControlEvt(this, new SatAudioControlEventArgs(1));
            }
        }

        private void SetStepMedia(GlassButton btn)
        {
            foreach (GlassButton gtn in lstBtnMedia)
            {
                SetBtnColor(gtn, (byte)Util.emMode.none);
            }

            this.curStep = (byte)Util.emPnl.grp;
            SetStepAttr(btn);
        }
        #endregion

        #region Btn Event Group
        /// <summary>
        /// 발령 대상 ALL 버튼 클릭 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBaseAll_Click(object sender, EventArgs e)
        {
            this.curDest = (byte)Util.emDest.all;
            mainForm.HidePopGrpPnl();
            SetStepGrp(sender as GlassButton);
            btnBaseGrp.Text = LangPack.GetGroup();
            this.curGrp = null;
            this.curDistLstIP = null;
            this.curTermLstIP = null;
        }

        /// <summary>
        /// 그룹 버튼 클릭 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBaseGrp_Click(object sender, EventArgs e)
        {
            this.mainForm.HidePopMsgPnl();
            this.curDest = (byte)Util.emDest.grp;

            foreach (GlassButton gtn in lstBtnBaseGrp)
            {
                SetBtnColor(gtn, (byte)Util.emMode.none);
            }

            mainForm.ViewPopGrpPnl(true);

            this.curDistLstIP = null;
            this.curTermLstIP = null;
        }

        /// <summary>
        /// 개별 버튼 클릭 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBaseIndi_Click(object sender, EventArgs e)
        {
            this.mainForm.HidePopMsgPnl();
            this.curDest = (byte)Util.emDest.ind;

            foreach (GlassButton gtn in lstBtnBaseGrp)
            {
                SetBtnColor(gtn, (byte)Util.emMode.none);
            }

            mainForm.ViewPopGrpPnl(false);
            btnBaseGrp.Text = LangPack.GetGroup();
            this.curGrp = null;
        }

        private void SetStepGrp(GlassButton btn)
        {
            foreach (GlassButton gtn in lstBtnBaseGrp)
            {
                SetBtnColor(gtn, (byte)Util.emMode.none);
            }

            this.curStep = (byte)Util.emPnl.type;
            SetStepAttr(btn);
        }
        #endregion

        #region Btn Event Type
        /// <summary>
        /// 예비 버튼 클릭 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBaseReady_Click(object sender, EventArgs e)
        {
            bool selFlag = false;

            foreach (GlassButton gtn in lstBtnBaseGrp)
            {
                if (gtn.BackColor != Color.Black)
                {
                    selFlag = true;
                }
            }

            if (!selFlag)
            {
                MessageBox.Show(LangPack.GetMongolian("Please select type first."), LangPack.GetMongolian("Type Select"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            this.curKind = (byte)Util.emKind.ready;
            SetStepType(sender as GlassButton);
            this.btnBaseMsg.Text = LangPack.GetStoMsg();
            this.btnBaseLive.Text = LangPack.GetLive();
            this.btnBdSend.Enabled = true;
            this.glassButton1.Enabled = true;
            this.mainForm.HidePopLivePnl();
        }

        #region 미사용
        /// <summary>
        /// 사이렌 버튼 클릭 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBaseSiren_Click(object sender, EventArgs e)
        {
            this.curKind = (byte)Util.emKind.siren1;

            foreach (GlassButton gtn in lstBtnBaseType)
            {
                SetBtnColor(gtn, (byte)Util.emMode.none);
            }

            this.mainForm.HidePopMsgPnl();
            mainForm.ViewPopSirenPnl();
        }
        #endregion

        /// <summary>
        /// 방송 버튼 클릭 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBaseLive_Click(object sender, EventArgs e)
        {
            bool selFlag = false;

            foreach (GlassButton gtn in lstBtnBaseGrp)
            {
                if (gtn.BackColor != Color.Black)
                {
                    selFlag = true;
                }
            }

            if (!selFlag)
            {
                MessageBox.Show(LangPack.GetMongolian("Please select type first."), LangPack.GetMongolian("Type Select"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            mainForm.HidePopGrpPnl();
            mainForm.HidePopMsgPnl();
            mainForm.HidePopSirenPnl();
            mainForm.ViewPopLivePnl();

            //this.curKind = (byte)Util.emKind.liveBrd;
            //SetStepType(sender as GlassButton);
            this.btnBaseMsg.Text = LangPack.GetStoMsg();
            //this.btnBdSend.Enabled = true;
        }

        /// <summary>
        /// 저장메시지 버튼 클릭 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBaseMsg_Click(object sender, EventArgs e)
        {
            bool selFlag = false;

            foreach (GlassButton gtn in lstBtnBaseGrp)
            {
                if (gtn.BackColor != Color.Black)
                {
                    selFlag = true;
                }
            }

            if (!selFlag)
            {
                MessageBox.Show(LangPack.GetMongolian("Please select type first."), LangPack.GetMongolian("Type Select"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            this.curKind = (byte)Util.emKind.storedMsg;
            
            foreach (GlassButton gtn in lstBtnBaseType)
            {
                SetBtnColor(gtn, (byte)Util.emMode.none);
            }

            this.mainForm.HidePopSirenPnl();
            this.mainForm.HidePopLivePnl();
            this.mainForm.ViewPopMsgPnl(this.curMode);
            this.btnBaseLive.Text = LangPack.GetLive();
            this.btnBdSend.Enabled = true;
            this.glassButton1.Enabled = true;
        }

        /// <summary>
        /// 해제 버튼 클릭 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStop_Click(object sender, EventArgs e)
        {
            bool selFlag = false;

            foreach (GlassButton gtn in lstBtnBaseGrp)
            {
                if (gtn.BackColor != Color.Black)
                {
                    selFlag = true;
                }
            }

            if (!selFlag)
            {
                MessageBox.Show(LangPack.GetMongolian("Please select type first."), LangPack.GetMongolian("Type Select"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            this.curKind = (byte)Util.emKind.stop;
            SetStepType(sender as GlassButton);
            this.btnBaseMsg.Text = LangPack.GetStoMsg();
            this.btnBaseLive.Text = LangPack.GetLive();
            this.mainForm.HidePopLivePnl();
            this.GetBoardText(null);
            this.btnBdSend.Enabled = false;
            this.glassButton1.ImageIndex = 1;
            this.glassButton1.Enabled = false;
            this.bTVAudio = true;
        }

        private void SetStepType(GlassButton btn)
        {
            btnMicSet.BackColor = Color.Black;

            foreach (GlassButton gtn in lstBtnBaseType)
            {
                SetBtnColor(gtn, (byte)Util.emMode.none);
            }

            this.curStep = (byte)Util.emPnl.confirm;
            SetStepAttr(btn);
        }
        #endregion

        #region Btn Event Confirm
        /// <summary>
        /// 컨펌 버튼 클릭 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            bool selFlag = false;

            foreach (GlassButton gtn in lstBtnBaseType)
            {
                if (gtn.BackColor != Color.Black)
                {
                    selFlag = true;
                }
            }

            if (!selFlag)
            {
                MessageBox.Show(LangPack.GetMongolian("Please select command kind first."), LangPack.GetMongolian("Command Kind Select"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            bOrderIng = true;

            this.mainForm.HidePopGrpPnl();
            this.mainForm.HidePopMsgPnl();
            this.mainForm.HidePopSirenPnl();

            if (this.curKind == (byte)Util.emKind.liveBrd)
            {
                btnMicSet.BackColor = Color.FromArgb(255, 128, 0);

                this.curStep = (byte)Util.emPnl.mic;
                SetNextStep(this.curStep);

                //방송시작 버튼까지 자동으로 클릭
                this.btnMicSet_Click(this, new EventArgs());
                this.mainForm.ViewPopLiveSirenPnl();
            }

            MakeOrderPacket();

            if (this.curKind == (byte)Util.emKind.liveBrd)
            {
            }
            else if (btnBaseStop.BackColor != Color.Black)
            {
                this.curDistLstIP = null;
                this.curTermLstIP = null;
                
                SetFirstStep();

                if (this.bKeyOrder && this.curKind == (byte)Util.emKind.stop)
                {
                    if (this.curMode == (byte)Util.emMode.real)
                        btnModeReal_Click(btnModeReal, null);
                    else if (this.curMode == (byte)Util.emMode.drill)
                        btnModeDrill_Click(btnModeDrill, null);
                }

                this.curGrp = null;

                if (OnSatAudioControlEvt != null)
                {
                    this.OnSatAudioControlEvt(this, new SatAudioControlEventArgs(2));
                }

                this.mainForm.SetMapTermState();
            }
            else
            {
                this.curStep = (byte)Util.emPnl.type;
                SetNextStep(this.curStep);
            }

            if (this.curBoardInfo == null)
            {
                this.toolTip.RemoveAll();
                return;
            }
        }
        #endregion

        #region Btn Event Etc
        /// <summary>
        /// 전단계 버튼 클릭 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPre_Click(object sender, EventArgs e)
        {
            if (this.curStep == (byte)Util.emPnl.mic)
            {
                this.curStep--;
                btnMicSet.BackColor = Color.Black;
            }

            if (!SetNextStep(this.curStep - 1))
                return;

            if (this.curStep == (byte)Util.emPnl.confirm)
            {
                btnBaseMsg.Text = LangPack.GetStoMsg();
                this.btnBaseLive.Text = LangPack.GetLive();
            }

            if (this.curStep == (byte)Util.emPnl.type)
            {
                this.btnBaseLive.Text = LangPack.GetLive();
            }

            if (this.curStep == (byte)Util.emPnl.grp)
            {
                btnBaseGrp.Text = LangPack.GetGroup();
                this.curDistLstIP = null;
                this.curTermLstIP = null;
                this.curGrp = null;
            }

            ResetCurrenBtn();
            this.curStep--;
        }

        /// <summary>
        /// 선택된 단계 전체 취소 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (this.bOrderIng)
            {
                if (DialogResult.No == MessageBox.Show(LangPack.GetMongolian("Current alert state. \nCanceled by the terminal can not be canceled. \nReally sure you want to cancel?"), LangPack.GetMongolian("Waring"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2))
                    return;
            }

            SetFirstStep();

            this.mainForm.HidePopMsgPnl();
            this.mainForm.HidePopGrpPnl();
            this.mainForm.HidePopSirenPnl();
            this.mainForm.HidePopLivePnl();
            btnMicSet.BackColor = Color.Black;

            btnBdSendNo_Click(null, null);
            this.glassButton1.ImageIndex = 1;
            this.glassButton1.Enabled = false;
            this.bTVAudio = true;
            this.GetBoardText(null);
            this.toolTip.RemoveAll();
            this.toolTip1.RemoveAll();
            this.curGrp = null;
            this.curDistLstIP = null;
            this.curTermLstIP = null;
            OnSendDataToCtrlPnl();
        }

        /// <summary>
        /// 첫 단계로 가기 위한 메소드
        /// </summary>
        private void SetFirstStep()
        {
            bOrderIng = false;
            this.bLampRed = false;
            this.bLampGreen = false;
            this.bLampYellow = false;
            this.bLampBuzzer = false;

            this.mainForm.HIdeOnAir();

            boardText = string.Empty;
            btnBaseMsg.Text = LangPack.GetStoMsg();
            btnBaseGrp.Text = LangPack.GetGroup();
            this.btnBaseLive.Text = LangPack.GetLive();

            this.curStep = (byte)Util.emPnl.mode;
            SetNextStep(this.curStep);
            btnMicSet.BackColor = Color.Black;

            ResetModeBtn();
            ResetMediaBtn();
            ResetGrpBtn();
            ResetTypeBtn();

            btnBdSendNo_Click(null, null);
            this.glassButton1.ImageIndex = 1;
            this.glassButton1.Enabled = false;
            this.bTVAudio = true;
            OnSendDataToCtrlPnl();
        }

        /// <summary>
        /// 자동 방송 들어오는 경우 초기화하기 위한 메소드
        /// </summary>
        public void SetAutoReset()
        {
            SetFirstStep();

            this.mainForm.HidePopMsgPnl();
            this.mainForm.HidePopGrpPnl();
            this.mainForm.HidePopSirenPnl();
            this.mainForm.HidePopLivePnl();
            btnMicSet.BackColor = Color.Black;

            btnBdSendNo_Click(null, null);
            this.glassButton1.ImageIndex = 1;
            this.glassButton1.Enabled = false;
            this.bTVAudio = true;
            this.GetBoardText(null);
            this.toolTip.RemoveAll();
            this.toolTip1.RemoveAll();
            this.curGrp = null;
            this.curDistLstIP = null;
            this.curTermLstIP = null;
            
            OnSendDataToCtrlPnl();
        }
        #endregion

        #region Btn Event CG 관련
        /// <summary>
        /// CG 보내지 않음 버튼 클릭 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBdSendNo_Click(object sender, EventArgs e)
        {
            btnBdSendNo.ImageIndex = 1;
            btnBdSend.ImageIndex = 0;
        }

        /// <summary>
        /// CG 보냄 버튼 클릭 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBdSend_Click(object sender, EventArgs e)
        {
            using (BoardMngForm form = new BoardMngForm(this.curBoardInfo, true, this.mainForm.isPrimary))
            {
                form.evtBoardText += new InvokeValueOne<BoardInfoDetail>(form_evtBoardText);
                form.ShowDialog();
                form.evtBoardText -= new InvokeValueOne<BoardInfoDetail>(form_evtBoardText);
            }
        }

        /// <summary>
        /// AUDIO 절체 판단 여부
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void glassButton1_Click(object sender, EventArgs e)
        {
            if (this.glassButton1.ImageIndex == 0)
            {
                this.glassButton1.ImageIndex = 1;
                this.bTVAudio = true;
            }
            else
            {
                this.glassButton1.ImageIndex = 0;
                this.bTVAudio = false;
            }
        }

        private void form_evtBoardText(BoardInfoDetail info)
        {
            if (this.InvokeRequired)
                this.Invoke(new InvokeValueOne<BoardInfoDetail>(GetBoardText));
            else
                GetBoardText(info);
        }
        #endregion

        #region Btn Event Mic Start/End
        /// <summary>
        /// 방송 시작/종료 버튼 클릭 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMicSet_Click(object sender, EventArgs e)
        {
            string btnTag = btnMicSet.Tag as string;

            if (btnTag.CompareTo("start") == 0)
            {
                // SAT
                if (this.curMedia != (byte)Util.emMedia.vhf)
                {
                    PrtBase pb = PrtMng.GetPrtObject(18);
                    PrtCmd18 p18 = pb as PrtCmd18;

                    p18.SetKind = (byte)Util.emBroad.start;
                    p18.MediaKind = (byte)Util.emMedia.sat;
                    pb.MakeData();
                    byte[] buff = PrtMng.MakeFrame(pb);

                    if (this.OnSatDataRecvEvt != null)
                    {
                        this.OnSatDataRecvEvt(this, new VhfSatDataEventArgs(buff));
                    }
                }

                // VHF
                if (this.curMedia != (byte)Util.emMedia.sat)
                {
                    PrtBase pb = PrtMng.GetPrtObject(18);
                    PrtCmd18 p18 = pb as PrtCmd18;

                    p18.SetKind = (byte)Util.emBroad.start;
                    p18.MediaKind = (byte)Util.emMedia.vhf;
                    pb.MakeData();
                    byte[] buff = PrtMng.MakeFrame(pb);

                    if (this.OnVhfDataResvEvt != null)
                    {
                        this.OnVhfDataResvEvt(this, new VhfSatDataEventArgs(buff));
                    }
                }

                this.SetOnAir();
                mainForm.ShowOnAir(false);
            }
            else
            {
                if (btnMicSet.Text == LangPack.GetLiveEnd())
                {
                    // SAT
                    if (this.curMedia != (byte)Util.emMedia.vhf)
                    {
                        PrtBase pb = PrtMng.GetPrtObject(18);
                        PrtCmd18 p18 = pb as PrtCmd18;

                        p18.SetKind = (byte)Util.emBroad.end;
                        p18.MediaKind = (byte)Util.emMedia.sat;
                        pb.MakeData();
                        byte[] buff = PrtMng.MakeFrame(pb);

                        if (this.OnSatDataRecvEvt != null)
                        {
                            this.OnSatDataRecvEvt(this, new VhfSatDataEventArgs(buff));
                        }
                    }

                    // VHF
                    if (this.curMedia != (byte)Util.emMedia.sat)
                    {
                        PrtBase pb = PrtMng.GetPrtObject(18);
                        PrtCmd18 p18 = pb as PrtCmd18;

                        p18.SetKind = (byte)Util.emBroad.end;
                        p18.MediaKind = (byte)Util.emMedia.vhf;
                        pb.MakeData();
                        byte[] buff = PrtMng.MakeFrame(pb);

                        if (this.OnVhfDataResvEvt != null)
                        {
                            this.OnVhfDataResvEvt(this, new VhfSatDataEventArgs(buff));
                        }
                    }

                    this.mainForm.HIdeOnAir();
                    this.mainForm.HidePopLiveSirenPnl();
                    btnMicSet.Text = LangPack.GetLiveEnd() + " 3" + LangPack.GetSec();

                    this.endTimer = new Timer();
                    this.endTimer.Interval = 1000;
                    this.endTimer.Tick += new EventHandler(endTimer_Tick);
                    this.endTimer.Start();
                }
            }
        }

        /// <summary>
        /// 방송종료 타이머 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void endTimer_Tick(object sender, EventArgs e)
        {
            if (btnMicSet.Text == LangPack.GetLiveEnd())
            {
                btnMicSet.Text = LangPack.GetLiveEnd() + " 3" + LangPack.GetSec();
            }
            else if (btnMicSet.Text == LangPack.GetLiveEnd() + " 3" + LangPack.GetSec())
            {
                btnMicSet.Text = LangPack.GetLiveEnd() + " 2" + LangPack.GetSec();
            }
            else if (btnMicSet.Text == LangPack.GetLiveEnd() + " 2" + LangPack.GetSec())
            {
                btnMicSet.Text = LangPack.GetLiveEnd() + " 1" + LangPack.GetSec();
            }
            else if (btnMicSet.Text == LangPack.GetLiveEnd() + " 1" + LangPack.GetSec())
            {
                btnMicSet.Text = LangPack.GetLiveEnd() + " 0" + LangPack.GetSec();
                this.endTimer.Stop();
                this.endTimer.Tick -= new EventHandler(endTimer_Tick);
                this.endTimer = null;
            }

            if (btnMicSet.Text == LangPack.GetLiveEnd() + " 0" + LangPack.GetSec())
            {
                btnMicSet.Tag = "start";
                btnMicSet.Text = LangPack.GetLiveStart();

                pnlEtc.Enabled = true;
                btnMicSet.BackColor = Color.Black;

                this.curStep = (byte)Util.emPnl.type;
                SetNextStep(this.curStep);
                OnSendDataToCtrlPnl();
            }
        }
        #endregion

        #region Btn Attributes
        private void GetBoardText(BoardInfoDetail info)
        {
            if (info == null)
            {
                this.curBoardInfo = null;
                btnBdSend.ImageIndex = 0;
                btnBdSend.Text = LangPack.GetCGDisplay();
                this.toolTip.RemoveAll();
            }
            else
            {
                this.curBoardInfo = info;
                boardText = info.text;
                btnBdSend.ImageIndex = 1;
                btnBdSend.Text = "    " + info.name;
            }
        }

        /// <summary>
        /// 각 단계로 이동하기 위한 메소드
        /// </summary>
        /// <param name="btn"></param>
        private void SetStepAttr(GlassButton btn)
        {
            SetBtnColor(btn, this.curMode);

            foreach (GlassButton gb in lstBtnMedia)
            {
                gb.GlowColor = Util.lstColor[this.curMode];
            }

            foreach (GlassButton gb in lstBtnBaseGrp)
            {
                gb.GlowColor = Util.lstColor[this.curMode];
            }

            foreach (GlassButton gb in lstBtnBaseType)
            {
                gb.GlowColor = Util.lstColor[this.curMode];
            }

            SetNextStep(this.curStep);

            OnSendDataToCtrlPnl();
        }

        /// <summary>
        /// 다음 단계로 이동하기 위한 메소드
        /// </summary>
        /// <param name="curPnl"></param>
        /// <returns></returns>
        private bool SetNextStep(int curPnl)
        {
            if (curPnl < 0)
                return false;

            this.mainForm.HidePopGrpPnl();
            this.mainForm.HidePopMsgPnl();
            this.mainForm.HidePopSirenPnl();
            this.mainForm.HidePopLivePnl();

            List<Panel> lstPnl = null;
            lstPnl = lstBasePnl;

            foreach (Panel pn in lstBasePnl)
            {
                pn.Enabled = false;
                pn.BackColor = Util.lstColor[(int)Util.emStep.none];
            }

            if (curPnl > 0 && this.curStep != (byte)Util.emPnl.mic)
                lstPnl[curPnl - 1].Enabled = true;

            pnlBoard.Enabled = false;
            pnlBoard.BackColor = Util.lstColor[(int)Util.emStep.none];

            if (this.curStep == (byte)Util.emPnl.grp)
            {
                lstBasePnl[curPnl].Enabled = true;
                lstBasePnl[curPnl].BackColor = Util.lstColor[(int)Util.emStep.me];
            }
            else if (this.curStep == (byte)Util.emPnl.mic)
            {
                lstBasePnl[curPnl].Enabled = true;
                lstBasePnl[curPnl].BackColor = Util.lstColor[(int)Util.emStep.me];

                lstBasePnl[curPnl - 2].Enabled = true;
                lstBasePnl[curPnl-2].BackColor = Util.lstColor[(int)Util.emStep.me];
            }
            else
            {
                if (curPnl == (byte)Util.emPnl.confirm)
                {
                    pnlBoard.Enabled = true;
                    pnlBoard.BackColor = Util.lstColor[(int)Util.emStep.me];
                }

                lstPnl[curPnl].Enabled = true;
                lstPnl[curPnl].BackColor = Util.lstColor[(int)Util.emStep.me];
            }

            return true;
        }

        /// <summary>
        /// 버튼에 color를 셋팅하는 메소드
        /// </summary>
        /// <param name="btn"></param>
        /// <param name="mode"></param>
        private void SetBtnColor(GlassButton btn, byte mode)
        {
            if (mode == (byte)Util.emMode.real)
            {
                btn.Tag = "P";
                btn.BackColor = Util.lstColor[(int)Util.emMode.real];
                btn.ShineColor = Color.White; // Util.lstColor[(int)Util.emMode.real];
            }
            else if (mode == (byte)Util.emMode.test)
            {
                btn.Tag = "P";
                btn.BackColor = Util.lstColor[(int)Util.emMode.test];
                btn.ShineColor = Color.White; // Util.lstColor[(int)Util.emMode.test];
            }
            else if (mode == (byte)Util.emMode.drill)
            {
                btn.Tag = "P";
                btn.BackColor = Util.lstColor[(int)Util.emMode.drill];
                btn.ShineColor = Color.White; // Util.lstColor[(int)Util.emMode.drill];
            }
            else if (mode == (byte)Util.emMode.none)
            {
                btn.Tag = "N";
                btn.BackColor = Util.lstColor[(int)Util.emMode.none];
                btn.ShineColor = Color.White; // Util.lstColor[(int)Util.emMode.none];
            }
        }
        #endregion

        #region Reset Button
        /// <summary>
        /// 단계에 맞게 버튼을 리셋한다.
        /// </summary>
        private void ResetCurrenBtn()
        {
            if (curStep == (byte)Util.emPnl.mode)
                ResetModeBtn();
            else if (curStep == (byte)Util.emPnl.media)
                ResetMediaBtn();
            else if (curStep == (byte)Util.emPnl.grp)
            {
                ResetGrpBtn();
                this.mainForm.HidePopGrpPnl();
                this.mainForm.HidePopMsgPnl();
                this.mainForm.HidePopSirenPnl();
                this.mainForm.HidePopLivePnl();
            }
            else if (curStep == (byte)Util.emPnl.type)
            {
                ResetTypeBtn();
                this.mainForm.HidePopMsgPnl();
                this.mainForm.HidePopSirenPnl();
                this.mainForm.HidePopLivePnl();
            }

            this.OnSendDataToCtrlPnl();
        }

        public void SetLiveEnd()
        {
            this.btnMicSet_Click(this, new EventArgs());
        }

        private void ResetModeBtn()
        {
            foreach (GlassButton gtn in lstBtnMode)
            {
                SetBtnColor(gtn, (byte)Util.emMode.none);
            }
        }

        private void ResetMediaBtn()
        {
            foreach (GlassButton gtn in lstBtnMedia)
            {
                SetBtnColor(gtn, (byte)Util.emMode.none);
            }
        }

        private void ResetGrpBtn()
        {
            foreach (GlassButton gtn in lstBtnBaseGrp)
            {
                SetBtnColor(gtn, (byte)Util.emMode.none);
            }
        }

        private void ResetTypeBtn()
        {
            foreach (GlassButton gtn in lstBtnBaseType)
            {
                SetBtnColor(gtn, (byte)Util.emMode.none);
            }
        }
        #endregion

        #region Event Process
        private void tvGrp_AfterCheck(object sender, TreeViewEventArgs e)
        {
            foreach (TreeNode node in e.Node.Nodes)
            {
                node.Checked = e.Node.Checked;
            }
        }
        #endregion

        #region Make Packeet
        public void AutoMakeOrderPacket()
        {
            MakeOrderPacket();
        }

        /// <summary>
        /// 패킷 생성
        /// </summary>
        private void MakeOrderPacket()
        {
            MapWarnInfo mwInfo = new MapWarnInfo();
            mwInfo.Mode = this.curMode;
            mwInfo.WarnTime = DateTime.Now;
            mwInfo.WarnKind = this.curKind;
            mwInfo.IsLangEnglish = LangPack.IsEng;

            PrtBase pb = PrtMng.GetPrtObject(5);
            PrtCmd05 p5 = pb as PrtCmd05;
            p5.TimeDT = DateTime.Now;
            p5.Mode = this.curMode;
            p5.Source = (byte)Util.emSource.cc;
            p5.Media = this.curMedia;
            p5.Kind = this.curKind;

            if (this.curBoardInfo != null)
            {
                p5.IsCgUse = true;//CG 있음
            }
            else
            {
                p5.IsCgUse = false;//CG 없음
            }

            if (this.bTVAudio) //TV AUDIO 사용함
            {
                p5.IsAudio = true;
            }
            else
            {
                p5.IsAudio = false;
            }

            if (this.curKind > 3)
            {
                p5.MsgNum = 0;
            }
            else
            {
                p5.MsgNum = this.curMsgNum;
            }

            if (this.curKind == 0)
            {
                this.mainForm.ShowOrderProgress(SirenDataMng.GetSirenMng().dicSiren[this.curSirenNum.ToString()]);
            }
            else if (this.curKind == 3)
            {
                this.mainForm.ShowOrderProgress(StMsgDataMng.GetMsgMng().dicMsg[this.curMsgNum.ToString()]);
                p5.MsgReptCnt = this.curReptCnt;
            }
            else if (this.curKind == 4)
            {
                p5.MsgNum = this.curLiveType;
                this.mainForm.HideOrderProgress();
            }
            else
            {
                this.mainForm.HideOrderProgress();
            }

            // 전체발령
            if (this.curDest == (byte)Util.emDest.all)
            {
                p5.AddrStr = Util.totalIP;
                List<string> tmp = new List<string>();
                tmp.Add(Util.totalIP);
                p5.LstTermStr = tmp;

                if (this.curBoardInfo != null && (byte)this.curBoardInfo.kindNum == 255)
                {
                    try
                    {
                        p5.CGNum = byte.Parse(this.btnBdSend.Text.ToString());
                    }
                    catch (Exception ex)
                    {
                        p5.CGNum = 1;
                        MessageBox.Show("CG title don't number. CG title must have number.");
                    }
                }
                else
                {
                    p5.CGNum = 0;
                }

                byte[] buff = p5.MakeData();
                buff = PrtMng.MakeFrame(pb);
                SendData(buff);

                if (this.curBoardInfo != null && (byte)this.curBoardInfo.kindNum != 255)
                {
                    this.MakePacketSingboard(p5.TimeDT, p5.AddrStr);
                }

                mwInfo.TargetKind = 0;
                mwInfo.lstTermIP = GetTermIpList(MDataMng.GetInstance().DicOnlyBroadOrderTerm);
                mwInfo.lstTargetIP = GetDistIpList(MDataMng.GetInstance().DicDist);
            }
            // 그룹발령
            else if (this.curDest == (byte)Util.emDest.grp)
            {
                mwInfo.TargetKind = 2;
                List<string> lstTermIp = new List<string>();
                List<string> lstProto = new List<string>();

                foreach (KeyValuePair<string, ProvData> pair in this.curGrp.dicProv)
                {
                    //p5.AddrStr = pair.Value.ProvIp;
                    //byte[] buff = p5.MakeData();
                    //buff = PrtMng.MakeFrame(pb);
                    //SendData(buff);
                    //this.MakePacketSingboard(p5.TimeDT, p5.AddrStr);
                    
                    lstProto.Add(pair.Value.ProvIp);
                }
                foreach (KeyValuePair<string, DistData> pair in this.curGrp.dicDist)
                {
                    foreach (KeyValuePair<string, TermData> pairTerm in MDataMng.GetInstance().GetDistData(pair.Value.Code).dicTermData)
                    {
                        if (pairTerm.Value.TermKind == 1) //1은 방송단말, 0은 경보단말
                            lstTermIp.Add(pairTerm.Value.TermIp);
                    }

                    //p5.AddrStr = pair.Value.DistIp;
                    //byte[] buff = p5.MakeData();
                    //buff = PrtMng.MakeFrame(pb);
                    //SendData(buff);
                    //this.MakePacketSingboard(p5.TimeDT, p5.AddrStr);

                    lstProto.Add(pair.Value.DistIp);
                }
                foreach (KeyValuePair<string, TermData> pair in this.curGrp.dicTerm)
                {
                    lstTermIp.Add(pair.Value.TermIp);
                    lstProto.Add(pair.Value.TermIp);

                    //p5.AddrStr = pair.Value.TermIp;
                    //byte[] buff = p5.MakeData();
                    //buff = PrtMng.MakeFrame(pb);
                    //SendData(buff);
                    //this.MakePacketSingboard(p5.TimeDT, p5.AddrStr);
                }

                p5.AddrStr = "0.0.0.0";
                p5.LstTermStr = lstProto;

                if (this.curBoardInfo != null && (byte)this.curBoardInfo.kindNum == 255)
                {
                    try
                    {
                        p5.CGNum = byte.Parse(this.btnBdSend.Text.ToString());
                    }
                    catch (Exception ex)
                    {
                        p5.CGNum = 1;
                        MessageBox.Show("CG title don't number. CG title must have number.");
                    }
                }
                else
                {
                    p5.CGNum = 0;
                }

                byte[] buff = p5.MakeData();
                buff = PrtMng.MakeFrame(pb);
                SendData(buff);

                if (this.curBoardInfo != null && (byte)this.curBoardInfo.kindNum != 255)
                {
                    this.MakePacketSingboard(p5.TimeDT, p5.LstTermStr);
                }

                mwInfo.lstTermIP = lstTermIp;
                mwInfo.lstTargetIP = GetDistIpList(curGrp.dicDist);
            }
            // 시군 및 개별발령
            else if (this.curDest == (byte)Util.emDest.ind)
            {
                mwInfo.lstTargetIP.Clear();
                mwInfo.lstTermIP.Clear();

                List<string> lstTermIp = new List<string>();
                List<string> lstProto = new List<string>();

                foreach (string ip in this.curDistLstIP)
                {
                    mwInfo.TargetKind = 1;

                    foreach (string termip in GetTermIpList(GetDistData(ip).dicBroadTermData))
                    {
                        lstTermIp.Add(termip);
                    }

                    //p5.AddrStr = ip;
                    //byte[] buff = p5.MakeData();
                    //buff = PrtMng.MakeFrame(pb);
                    //SendData(buff);
                    //this.MakePacketSingboard(p5.TimeDT, p5.AddrStr);

                    lstProto.Add(ip);
                }

                foreach (string ip in this.curTermLstIP)
                {
                    mwInfo.TargetKind = 2;
                    lstTermIp.Add(ip);

                    //p5.AddrStr = ip;
                    //byte[] buff = p5.MakeData();
                    //buff = PrtMng.MakeFrame(pb);
                    //SendData(buff);
                    //this.MakePacketSingboard(p5.TimeDT, p5.AddrStr);

                    lstProto.Add(ip);
                }

                p5.AddrStr = "0.0.0.0";
                p5.LstTermStr = lstProto;

                if (this.curBoardInfo != null && (byte)this.curBoardInfo.kindNum == 255)
                {
                    try
                    {
                        p5.CGNum = byte.Parse(this.btnBdSend.Text.ToString());
                    }
                    catch (Exception ex)
                    {
                        p5.CGNum = 1;
                        MessageBox.Show("CG title don't number. CG title must have number.");
                    }
                }
                else
                {
                    p5.CGNum = 0;
                }

                byte[] buff = p5.MakeData();
                buff = PrtMng.MakeFrame(pb);
                SendData(buff);

                if (this.curBoardInfo != null && (byte)this.curBoardInfo.kindNum != 255)
                {
                    this.MakePacketSingboard(p5.TimeDT, p5.LstTermStr);
                }

                mwInfo.lstTermIP = lstTermIp;
                mwInfo.lstTargetIP = curDistLstIP;
            }

            //발령 종료 패킷 전송
            PrtBase exitPB = PrtMng.GetPrtObject(23);
            PrtCmd23 p23 = exitPB as PrtCmd23;
            p23.Kind = 1;
            byte[] exitBuff = p23.MakeData();
            exitBuff = PrtMng.MakeFrame(exitPB);
            SendData(exitBuff);

            //맵 모듈에 해당 지역 및 단말 셋팅
            this.mainForm.SetMapItem(mwInfo);

            //CG Button Init
            this.curBoardInfo = null;
            btnBdSend.Text = LangPack.GetCGDisplay();
            btnBdSend.ImageIndex = 0;
            
            //this.glassButton1.ImageIndex = 1;
            //this.bTVAudio = true;

            if (this.curKind == (byte)Util.emKind.stop)
                SetLampOff();
            else
                SetLampOn();
        }

        /// <summary>
        /// CG로 보내기 위한 패킷 생성
        /// </summary>
        /// <param name="time"></param>
        /// <param name="ip"></param>
        private void MakePacketSingboard(DateTime time, string ip)
        {
            if (this.curBoardInfo == null)
                return;

            PrtBase pb = PrtMng.GetPrtObject(7);
            PrtCmd07 p7 = pb as PrtCmd07;

            p7.AddrStr = ip;
            List<string> tmp = new List<string>();
            tmp.Add(Util.totalIP);
            p7.LstTermStr = tmp;
            p7.TimeDT = time;
            p7.Mode = this.curMode;
            p7.Media = this.curMedia;

            //개별이거나 방송인 경우에는 매체를 위성으로 고정
            if (this.curDest == 1 || this.curDest == 2) //개별, 그룹
            {
                p7.Media = (byte)Util.emMedia.sat;
            }

            if (this.curKind == 4) //방송
            {
                p7.Media = (byte)Util.emMedia.sat;
            }

            p7.Source = (byte)Util.emSource.cc;
            p7.Kind = this.curKind;
            p7.RepeatNum = 1; //일단 1로 고정, 반복횟수 의미 없으므로 1로 고정
            p7.TextCode = (byte)this.curBoardInfo.kindNum;
            p7.Text = this.boardText;

            pb.MakeData();
            byte[] buff = PrtMng.MakeFrame(pb);
            SendData(buff);
        }

        /// <summary>
        /// CG로 보내기 위한 패킷 생성 (프로토콜 변경된 것)
        /// </summary>
        /// <param name="time"></param>
        /// <param name="Ip"></param>
        private void MakePacketSingboard(DateTime time, List<string> Ip)
        {
            if (this.curBoardInfo == null)
                return;

            PrtBase pb = PrtMng.GetPrtObject(7);
            PrtCmd07 p7 = pb as PrtCmd07;

            p7.AddrStr = "0.0.0.0";
            p7.LstTermStr = Ip;
            p7.TimeDT = time;
            p7.Mode = this.curMode;
            p7.Media = this.curMedia;

            //개별이거나 방송인 경우에는 매체를 위성으로 고정
            //if (this.curDest == 1 || this.curDest == 2) //개별, 그룹
            //{
            //    p7.Media = (byte)Util.emMedia.sat;
            //}

            if (this.curKind == 4) //방송
            {
                p7.Media = (byte)Util.emMedia.sat;
            }

            p7.Source = (byte)Util.emSource.cc;
            p7.Kind = this.curKind;
            p7.RepeatNum = 1; //일단 1로 고정, 반복횟수 의미 없으므로 1로 고정
            p7.TextCode = (byte)this.curBoardInfo.kindNum;
            p7.Text = this.boardText;

            pb.MakeData();
            byte[] buff = PrtMng.MakeFrame(pb);
            SendData(buff);
        }
        #endregion

        #region Send Data
        /// <summary>
        /// 패킷을 운영대로 전송하기 위한 메소드 (이벤트 발생시킴)
        /// </summary>
        /// <param name="buff"></param>
        private void SendData(byte[] buff)
        {
            if (this.OnVhfDataResvEvt != null)
            {
                this.OnVhfDataResvEvt(this, new VhfSatDataEventArgs(buff));
            }
        }
        #endregion

        #region Util Func
        public void InitLang()
        {
            label1.Text = LangPack.GetMode();
            btnModeReal.Text = LangPack.GetReal();
            btnModeDrill.Text = LangPack.GetDrill();
            btnModeTest.Text = LangPack.GetTest();
            label2.Text = LangPack.GetMedia();
            btnMediaAll.Text = LangPack.GetAll();
            btnMediaVhf.Text = LangPack.GetVhf();
            btnMediaSate.Text = LangPack.GetSat();
            tabPage1.Text = LangPack.GetBase();
            label4.Text = LangPack.GetDest();
            btnBaseAll.Text = LangPack.GetAll();
            btnBaseGrp.Text = LangPack.GetGroup();
            btnBaseIndi.Text = LangPack.GetIndi();
            label3.Text = LangPack.GetAlertKind();
            btnBaseReady.Text = LangPack.GetReady();
            btnBaseLive.Text = LangPack.GetLive();
            btnBaseMsg.Text = LangPack.GetStoMsg();
            btnBaseStop.Text = LangPack.GetClear();
            label9.Text = LangPack.GetTVControl();
            glassButton1.Text = LangPack.GetAudio();
            btnBdSend.Text = LangPack.GetCGDisplay();
            label10.Text = LangPack.GetConfirmTitle();
            btnConfirm.Text = LangPack.GetConfirm();
            label5.Text = LangPack.GetLivecast();
            btnMicSet.Text = LangPack.GetLiveStart();
            label8.Text = LangPack.GetEtc();
            btnPre.Text = LangPack.GetPreStep();
            btnCancel.Text = LangPack.GetCancel();
        }

        public void GetLiveInfo(byte liveType)
        {
            this.curLiveType = liveType;

            this.curKind = (byte)Util.emKind.liveBrd;

            if (liveType == 0) //MIC
            {
                this.btnBaseLive.Text = LangPack.GetLive() + "\n" + LangPack.GetLiveMic();
            }
            else //녹음방송
            {
                this.btnBaseLive.Text = LangPack.GetLive() + "\n" + LangPack.GetLiveRecord();
            }

            SetStepType(btnBaseLive);
            this.btnBdSend.Enabled = true;
            this.glassButton1.Enabled = true;
        }

        /// <summary>
        /// 저장메시지 유저컨트롤에서 선택 시 발생하는 이벤트를 적용하기 위한 메소드
        /// </summary>
        /// <param name="msgNum"></param>
        public void GetMsgNumInfo(byte msgNum, byte reptCnt)
        {
            this.curMsgNum = msgNum;
            this.curReptCnt = reptCnt;

            StMsgDataMng msgMng = StMsgDataMng.GetMsgMng();
            btnBaseMsg.Text = string.Format(LangPack.GetStoMsg() + " {0}\n{1}", msgMng.dicMsg[msgNum.ToString()].msgNum, msgMng.dicMsg[msgNum.ToString()].msgName);
            SetStepType(btnBaseMsg);
        }

        /// <summary>
        /// 사이렌 유저컨트롤에서 선택 시 발생하는 이벤트를 적용하기 위한 메소드
        /// </summary>
        /// <param name="SirenNum"></param>
        public void GetSirenNumInfo(byte SirenNum)
        {
            this.curSirenNum = SirenNum;
        }

        /// <summary>
        /// 그룹 유저컨트롤에서 받은 이벤트를 UI에 적용하는 메소드
        /// </summary>
        /// <param name="grp"></param>
        public void GetGrpInfo(Group grp)
        {
            this.curGrp = grp;
            btnBaseGrp.Text = grp.Name;
            SetStepGrp(btnBaseGrp);
        }

        /// <summary>
        /// 방송 종류 선택 후 버튼에 종류 셋팅하는 이벤트
        /// </summary>
        /// <param name="_kind"></param>
        public void SetLivekind(string _kind)
        {

        }

        /// <summary>
        /// 시군/개별 유저컨트롤에서 선택 시 발생하는 이벤트 후 UI에 적용하는 메소드
        /// </summary>
        /// <param name="lstDIstIP"></param>
        /// <param name="lstTermIP"></param>
        public void GetIndInfo(List<string> lstDIstIP, List<string> lstTermIP)
        {
            this.curDistLstIP = lstDIstIP;
            this.curTermLstIP = lstTermIP;
            SetStepGrp(btnBaseIndi);
        }

        /// <summary>
        /// 방송단말 리스트를 검색하여 반환한다.
        /// </summary>
        /// <param name="dicTerm"></param>
        /// <returns></returns>
        public List<string> GetTermIpList(Dictionary<string, TermData> dicTerm)
        {
            List<string> lstIp = new List<string>();

            foreach (KeyValuePair<string, TermData> pair in dicTerm)
            {
                if (pair.Value.TermKind == 1) //방송단말
                    lstIp.Add(pair.Value.TermIp);
            }

            return lstIp;
        }

        public List<string> GetDistIpList(Dictionary<string, DistData> dicDist)
        {
            List<string> lstIp = new List<string>();

            foreach (KeyValuePair<string, DistData> pair in dicDist)
            {
                lstIp.Add(pair.Value.DistIp);
            }

            return lstIp;
        }

        public DistData GetDistData(string ip)
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
        #endregion

        public void RecvCtrlData(byte[] buff)
        {
            if (buff == null)
                return;

            PrtCtrlBase p = PrtCtrlMng.ParseFrame(buff);
            
            if (p == null)
                return;

            switch (p.Code)
            {
                case 13:
                    PrtCtrlCmd13 p13 = p as PrtCtrlCmd13;

                    if (p13.IsNullData && !bKeyOrder)
                        return;

                    if (p13.EtLampTest == 1)
                    {
                        OnLampTest();
                        return;
                    }

                    if (p13.EtBuzzer == 1)
                    {
                        this.bLampBuzzer = false;
                        OnLampControl();
                        return;
                    }

                    if (p13.KeyReal == 1 && !bKeyOrder)
                    {
                        if (bOperErr)
                        {
                            bOperErr = false;
                            OnSendDataToCtrlPnl();
                        }

                        bKeyOrder = true;
                        SetButtonAttr(btnModeDrill, 1);
                    }
                    else if (p13.KeyReal != 1 && bKeyOrder)
                    {
                        bKeyOrder = false;
                        SetFirstStep();
                        return;
                    }

                    if (p13.EtCancel == 1)
                    {
                        bOperErr = false;
                        OnSendDataToCtrlPnl();
                        return;
                    }

                    SetButtonAttr(btnModeReal, p13.MoReal);
                    SetButtonAttr(btnModeDrill, p13.MoDrill);
                    SetButtonAttr(btnModeTest, p13.MoTest);

                    SetButtonAttr(btnMediaSate, p13.MeSat);
                    SetButtonAttr(btnMediaVhf, p13.MeVhf);

                    SetButtonAttr(btnBaseAll, p13.ArAll);
                    SetButtonAttr(btnBaseIndi, (byte)(p13.ArGu1 + p13.ArGu2 + p13.ArGu3 + p13.ArGu4 + p13.ArGu5 + p13.ArGu6 + p13.ArGu7 + p13.ArGu8 + p13.ArGu9));
                    UpdateSelList(1, p13.ArGu1);
                    UpdateSelList(2, p13.ArGu2);
                    UpdateSelList(3, p13.ArGu3);
                    UpdateSelList(4, p13.ArGu4);
                    UpdateSelList(5, p13.ArGu5);
                    UpdateSelList(6, p13.ArGu6);
                    UpdateSelList(7, p13.ArGu7);
                    UpdateSelList(8, p13.ArGu8);
                    UpdateSelList(9, p13.ArGu9);

                    //UpdateSelSiren(1, p13.TySiren1); 
                    SetButtonAttr(btnBaseReady, p13.TySiren1);

                    //UpdateSelSiren(2, p13.TySiren2); 버튼 미사용
                    //UpdateSelSiren(3, p13.TySiren3); 버튼 미사용
                    
                    //UpdateSelSiren(4, p13.TySiren4);
                    if (p13.TySiren4 == 1)
                    {
                        this.curLiveType = 0;
                    }
                    else if (p13.TyBroad == 1)
                    {
                        this.curLiveType = 1;
                    }

                    SetButtonAttr(btnBaseLive, p13.TySiren4);
                    SetButtonAttr(btnBaseLive, p13.TyBroad);

                    //UpdateSelSiren(5, p13.TySiren5);
                    SetButtonAttr(btnMicSet, p13.TySiren5);

                    //UpdateSelSiren(6, p13.TySiren6);
                    SetButtonAttr(btnMicSet, p13.TySiren6);

                    //SetButtonAttr(btnBaseLive, p13.TyBroad); 버튼 미사용
                    SetButtonAttr(btnBaseStop, p13.TyClear);
                    //SetButtonAttr(btnMicSet, p13.BrStart); 버튼 미사용
                    //SetButtonAttr(btnMicSet, p13.BrStop); 버튼 미사용
                    SetButtonAttr(btnCancel, p13.EtCancel);
                    SetButtonAttr(btnConfirm, p13.Confirm);

                    break;
            }
        }

        private void UpdateSelList(byte code, byte val)
        {
            if (val == 0)
                return;

            if (!btnBaseIndi.Enabled)
                return;

            bool bRemove = false;

            if (curDistLstIP == null)
            {
                curDistLstIP = new List<string>();
            }

            if (curTermLstIP == null)
            {
                curTermLstIP = new List<string>();
            }

            if (code == 1)
            {
                if (!curDistLstIP.Contains("15.8.2.255"))
                {
                    curDistLstIP.Add("15.8.2.255");
                    this.mainForm.orderSelGroup.setSelInd(curDistLstIP, curTermLstIP);
                }
            }
            else if (code == 2)
            {
                if (!curDistLstIP.Contains("15.8.3.255"))
                {
                    curDistLstIP.Add("15.8.3.255");
                    this.mainForm.orderSelGroup.setSelInd(curDistLstIP, curTermLstIP);
                }
            }
            else if (code == 3)
            {
                if (!curDistLstIP.Contains("15.8.4.255"))
                {
                    curDistLstIP.Add("15.8.4.255");
                    this.mainForm.orderSelGroup.setSelInd(curDistLstIP, curTermLstIP);
                }
            }
            else if (code == 4)
            {
                if (!curDistLstIP.Contains("15.8.5.255"))
                {
                    curDistLstIP.Add("15.8.5.255");
                    this.mainForm.orderSelGroup.setSelInd(curDistLstIP, curTermLstIP);
                }
            }
            else if (code == 5)
            {
                if (!curDistLstIP.Contains("15.8.6.255"))
                {
                    curDistLstIP.Add("15.8.6.255");
                    this.mainForm.orderSelGroup.setSelInd(curDistLstIP, curTermLstIP);
                }
            }
            else if (code == 6)
            {
                if (!curDistLstIP.Contains("15.8.7.255"))
                {
                    curDistLstIP.Add("15.8.7.255");
                    this.mainForm.orderSelGroup.setSelInd(curDistLstIP, curTermLstIP);
                }
            }
            else if (code == 7)
            {
                if (!curDistLstIP.Contains("15.8.8.255"))
                {
                    curDistLstIP.Add("15.8.8.255");
                    this.mainForm.orderSelGroup.setSelInd(curDistLstIP, curTermLstIP);
                }
            }
            else if (code == 8)
            {
                if (!curDistLstIP.Contains("15.8.9.255"))
                {
                    curDistLstIP.Add("15.8.9.255");
                    this.mainForm.orderSelGroup.setSelInd(curDistLstIP, curTermLstIP);
                }
            }
            else if (code == 9)
            {
                if (!curDistLstIP.Contains("15.8.10.255"))
                {
                    curDistLstIP.Add("15.8.10.255");
                    this.mainForm.orderSelGroup.setSelInd(curDistLstIP, curTermLstIP);
                }
            }

            OnSendDataToCtrlPnl();

            //for (int i = 0; i < lstSelDist.Count(); i++)
            //{
            //    if (lstSelDist[i] == code.ToString())
            //    {
            //        bRemove = true;
            //        lstSelDist.RemoveAt(i);
            //        break;
            //    }
            //}

            //if (!bRemove)
            //    lstSelDist.Add(code.ToString());

            //mainForm.RecvSelDist(lstSelDist);
        }

        private void UpdateSelSiren(byte num, byte val)
        {
            if (val == 0)
                return;

            //if (this.lstSelDist.Count != 0)
            //    this.mainForm.SetAutoEnd();

            //if (!btnBaseSiren.Enabled)
            //{
            //    this.bOperErr = true;
            //    OnSendDataToCtrlPnl();
            //    return;
            //}

            this.curKind = (byte)Util.emKind.siren1;

            GetSirenNumInfo(num);
        }

        private void SetButtonAttr(GlassButton btn, byte val)
        {
            if (val == 0)
                return;

            if (this.bOperErr)
                return;

            if (this.curStep == (byte)Util.emPnl.grp && this.curDest == (byte)Util.emDest.ind)
            {
                if (btn == btnBaseReady || btn == btnBaseLive || btn == btnBaseStop)
                {
                    if (this.curDistLstIP != null && this.curDistLstIP.Count != 0)
                    {
                        this.mainForm.selectGrp();
                    }
                }
            }

            if (!btn.Enabled)
            {
                bOperErr = true;
                OnSendDataToCtrlPnl();
                return;
            }

            if (btn == btnModeReal)
            {
                if (bKeyOrder)
                    btnModeReal_Click(btn, null);
                else
                {
                    bOperErr = true;
                    OnSendDataToCtrlPnl();
                }
            }
            else if (btn == btnModeTest)
            {
                if (bKeyOrder)
                {
                    bOperErr = true;
                    OnSendDataToCtrlPnl();
                }
                else
                    btnModeTest_Click(btn, null);
            }
            else if (btn == btnModeDrill)
            {
                if (bKeyOrder)
                    btnModeDrill_Click(btn, null);
                else
                {
                    bOperErr = true;
                    OnSendDataToCtrlPnl();
                }
            }

            else if (btn == btnMediaSate)
            {
                if (this.curMedia == (byte)Util.emMedia.vhf)
                    btnMediaAll_Click(btnMediaAll, null);
                else
                    btnMediaSate_Click(btn, null);
            }
            else if (btn == btnMediaVhf)
            {
                if (this.curMedia == (byte)Util.emMedia.sat)
                    btnMediaAll_Click(btnMediaAll, null);
                else
                    btnMediaVhf_Click(btn, null);
            }

            else if (btn == btnBaseAll)
            {
                //bSelDistFst = true;
                //this.lstSelDist.Clear();
                //this.lstSelDistRecv.Clear();
                btnBaseAll_Click(btn, null);
            }
            else if (btn == btnBaseIndi)
                btnBaseIndi_Click(btn, null);
            else if (btn == btnBaseReady)
                btnBaseReady_Click(btn, null);
            else if (btn == btnBaseLive)
            {
                if (this.curLiveType == 0) //MIC
                {
                    btnBaseLive_Click(btn, null);
                    this.mainForm.selectLiveMic();
                }
                else
                {
                    btnBaseLive_Click(btn, null);
                    this.mainForm.selectLiveRecord();
                }
            }
            else if (btn == btnMicSet)
                btnMicSet_Click(btn, null);
            else if (btn == btnBaseStop)
                btnStop_Click(btn, null);
            else if (btn == btnConfirm)
                btnConfirm_Click(btn, null);
        }

        public void SetLampOff()
        {
            this.bLampRed = false;
            this.bLampGreen = false;
            this.bLampYellow = false;
            this.bLampBuzzer = false;

            OnLampControl();
        }

        private void SetLampOn()
        {
            if (this.curMode == (byte)Util.emMode.real)
            {
                this.bLampRed = true;

                if (Util.optionInfo.bUseBuzzerReal)
                    this.bLampBuzzer = true;
            }
            else if (this.curMode == (byte)Util.emMode.drill)
            {
                this.bLampYellow = true;

                if (Util.optionInfo.bUseBuzzerDrill)
                    this.bLampBuzzer = true;
            }
            else if (this.curMode == (byte)Util.emMode.test)
            {
                this.bLampGreen = true;

                if (Util.optionInfo.bUseBuzzerTest)
                    this.bLampBuzzer = true;
            }

            OnLampControl();
        }

        private void OnLampControl()
        {
            PrtCtrlBase p = PrtCtrlMng.GetPrtCtrlObject(17);
            PrtCtrlCmd17 p17 = p as PrtCtrlCmd17;

            p17.LampRed = (byte)(this.bLampRed ? 1 : 0);
            p17.LampGreen = (byte)(this.bLampGreen ? 1 : 0);
            p17.LampYellow = (byte)(this.bLampYellow ? 1 : 0);
            p17.LampBuzzer = (byte)(this.bLampBuzzer ? 1 : 0);

            p.MakeData();
            byte[] buff = PrtCtrlMng.MakeFrame(p);

            if (this.OnControlPNSendEvt != null)
            {
                this.OnControlPNSendEvt(this, new VhfSatDataEventArgs(buff));
            }
        }

        /// <summary>
        /// 조작반의 ON-AIR 제어
        /// </summary>
        public void ShowOnAirCtrl(byte _onAir)
        {
            PrtCtrlBase p = PrtCtrlMng.GetPrtCtrlObject(17);
            PrtCtrlCmd17 p17 = p as PrtCtrlCmd17;

            p17.LampRed = (byte)(this.bLampRed ? 1 : 0);
            p17.LampGreen = (byte)(this.bLampGreen ? 1 : 0);
            p17.LampYellow = (byte)(this.bLampYellow ? 1 : 0);
            p17.LampBuzzer = (byte)(this.bLampBuzzer ? 1 : 0);
            p17.LampRed2 = _onAir;

            p.MakeData();
            byte[] buff = PrtCtrlMng.MakeFrame(p);

            if (this.OnControlPNSendEvt != null)
            {
                this.OnControlPNSendEvt(this, new VhfSatDataEventArgs(buff));
            }
        }

        private void SendToLampTest(PrtCtrlBase p)
        {
            p.MakeData();
            byte[] buff = PrtCtrlMng.MakeFrame(p);

            if (this.OnControlPNSendEvt != null)
            {
                this.OnControlPNSendEvt(this, new VhfSatDataEventArgs(buff));
            }
        }

        private void OnLampTest()
        {
            PrtCtrlBase p = PrtCtrlMng.GetPrtCtrlObject(15);
            PrtCtrlCmd15 p15 = p as PrtCtrlCmd15;

            // ON
            //////////////////////////////
            p15.MoReal = 1;
            p15.ArAll = 1;
            p15.ArGu5 = 1;
            p15.TySiren1 = 1;
            p15.TySiren2 = 1;
            p15.TySiren3 = 1;
            SendToLampTest(p);

            p15.MoDrill = 1;
            p15.ArGu1 = 1;
            p15.ArGu6 = 1;
            p15.TySiren4 = 1;
            p15.TySiren5 = 1;
            p15.TySiren6 = 1;
            SendToLampTest(p);

            p15.MoTest = 1;
            p15.ArGu2 = 1;
            p15.ArGu7 = 1;
            p15.TyBroad = 1;
            p15.BrStart = 1;
            p15.BrStop = 1;
            SendToLampTest(p);

            p15.MeSat = 1;
            p15.ArGu3 = 1;
            p15.ArGu8 = 1;
            p15.Reserved = 1;
            p15.EtLampTest = 1;
            p15.EtBuzzer = 1;
            SendToLampTest(p);

            p15.MeVhf = 1;
            p15.ArGu4 = 1;
            p15.ArGu9 = 1;
            p15.TyClear = 1;
            p15.EtCancel = 1;
            p15.Confirm = 1;
            SendToLampTest(p);

            // OFF
            //////////////////////////////
            p15.MoReal = 0;
            p15.ArAll = 0;
            p15.ArGu5 = 0;
            p15.TySiren1 = 0;
            p15.TySiren2 = 0;
            p15.TySiren3 = 0;
            SendToLampTest(p);

            p15.MoDrill = 0;
            p15.ArGu1 = 0;
            p15.ArGu6 = 0;
            p15.TySiren4 = 0;
            p15.TySiren5 = 0;
            p15.TySiren6 = 0;
            SendToLampTest(p);

            p15.MoTest = 0;
            p15.ArGu2 = 0;
            p15.ArGu7 = 0;
            p15.TyBroad = 0;
            p15.BrStart = 0;
            p15.BrStop = 0;
            SendToLampTest(p);

            p15.MeSat = 0;
            p15.ArGu3 = 0;
            p15.ArGu8 = 0;
            p15.Reserved = 0;
            p15.EtLampTest = 0;
            p15.EtBuzzer = 0;
            SendToLampTest(p);

            p15.MeVhf = 0;
            p15.ArGu4 = 0;
            p15.ArGu9 = 0;
            p15.TyClear = 0;
            p15.EtCancel = 0;
            p15.Confirm = 0;
            SendToLampTest(p);

            OnSendDataToCtrlPnl();
        }

        public void OnSendDataToCtrlPnl()
        {
            PrtCtrlBase p = PrtCtrlMng.GetPrtCtrlObject(15);
            PrtCtrlCmd15 p15 = p as PrtCtrlCmd15;

            p15.MoReal = GetButtonAttr(btnModeReal);
            p15.MoDrill = GetButtonAttr(btnModeDrill);
            p15.MoTest = GetButtonAttr(btnModeTest);

            if ((btnMediaAll.Tag as string) == "P")
            {
                p15.MeSat = 1;
                p15.MeVhf = 1;
            }
            else
            {
                p15.MeSat = GetButtonAttr(btnMediaSate);
                p15.MeVhf = GetButtonAttr(btnMediaVhf);
            }

            p15.ArAll = GetButtonAttr(btnBaseAll);

            if (curDistLstIP != null)
            {
                foreach (string ip in this.curDistLstIP)
                {
                    if (ip == "15.8.2.255")
                        p15.ArGu1 = 1;
                    if (ip == "15.8.3.255")
                        p15.ArGu2 = 1;
                    if (ip == "15.8.4.255")
                        p15.ArGu3 = 1;
                    if (ip == "15.8.5.255")
                        p15.ArGu4 = 1;
                    if (ip == "15.8.6.255")
                        p15.ArGu5 = 1;
                    if (ip == "15.8.7.255")
                        p15.ArGu6 = 1;
                    if (ip == "15.8.8.255")
                        p15.ArGu7 = 1;
                    if (ip == "15.8.9.255")
                        p15.ArGu8 = 1;
                    if (ip == "15.8.10.255")
                        p15.ArGu9 = 1;
                }
            }

            if (p15.ArAll == 1 || curDistLstIP == null)
            {
                p15.ArGu1 = 0;
                p15.ArGu2 = 0;
                p15.ArGu3 = 0;
                p15.ArGu4 = 0;
                p15.ArGu5 = 0;
                p15.ArGu6 = 0;
                p15.ArGu7 = 0;
                p15.ArGu8 = 0;
                p15.ArGu9 = 0;
            }

            p15.TySiren1 = GetButtonAttr(btnBaseReady);

            if (this.curLiveType == 0)
            {
                p15.TySiren4 = GetButtonAttr(btnBaseLive);
            }
            else
            {
                p15.TyBroad = GetButtonAttr(btnBaseLive);
            }

            p15.TySiren5 = GetButtonAttr(btnMicSet);
            p15.TyClear = GetButtonAttr(btnBaseStop);
            p15.EtCancel = GetButtonAttr(btnCancel);
            p15.Confirm = GetButtonAttr(btnConfirm);
            p15.EtCancel = (byte)(this.bOperErr ? 2 : 0);

            p.MakeData();
            byte[] buff = PrtCtrlMng.MakeFrame(p);

            if (this.OnControlPNSendEvt != null)
            {
                this.OnControlPNSendEvt(this, new VhfSatDataEventArgs(buff));
            }
        }

        public void SetOnAir()
        {
            btnMicSet.Tag = "end";
            btnMicSet.Text = LangPack.GetLiveEnd();

            pnlEtc.Enabled = false;
            pnlBaseType.Enabled = false;
            pnlBaseType.BackColor = Util.lstColor[(int)Util.emStep.none];
            btnMicSet.BackColor = Color.Red;
            OnSendDataToCtrlPnl();
        }

        private byte GetButtonAttr(GlassButton btn)
        {
            string btnTag = btn.Tag as string;

            if (btnTag == "N" || btnTag == "start")
                return 0;
            else if (btnTag == "P" || btnTag == "end")
                return 1;
            else
                return 2;
        }

        /// <summary>
        /// CG 버튼 마우스 올림 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBdSend_MouseHover(object sender, EventArgs e)
        {
            if (this.curBoardInfo == null)
            {
                this.toolTip.RemoveAll();
                return;
            }

            if (this.btnBdSend.Text == LangPack.GetCGDisplay())
            {
                this.toolTip.RemoveAll();
                return;
            }

            this.toolTip.ToolTipTitle = string.Format("[ {0} ]", this.curBoardInfo.name);
            this.toolTip.SetToolTip(btnBdSend, this.curBoardInfo.text);
        }

        /// <summary>
        /// 저장메시지 마우스 올림 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBaseMsg_MouseHover(object sender, EventArgs e)
        {
            if (this.btnBaseMsg.Text == LangPack.GetStoMsg())
            {
                this.toolTip1.RemoveAll();
                return;
            }

            StMsgDataMng msgMng = StMsgDataMng.GetMsgMng();
            MsgInfo tmpMsgInfo = msgMng.dicMsg[this.curMsgNum.ToString()];

            this.toolTip1.ToolTipTitle = string.Format("[{0}]", tmpMsgInfo.msgName);
            //this.toolTip1.SetToolTip(this.btnBaseMsg, string.Format("Stored Message Number : {0}\nTime : {1} sec\nRepeat Count : {2}", tmpMsgInfo.msgNum, tmpMsgInfo.msgTime, this.curReptCnt.ToString()));
            this.toolTip1.SetToolTip(this.btnBaseMsg, LangPack.GetStoredMessageNumber() + " : " + tmpMsgInfo.msgNum + "\n" + LangPack.GetTime() + " : " + tmpMsgInfo.msgTime + " " + LangPack.GetSec() + "\n" + LangPack.GetReptCnt() + " : " + this.curReptCnt.ToString());
        }
    }

    /// <summary>
    /// VHF/SAT Data를 전송하기 위한 이벤트 아규먼트 클래스
    /// </summary>
    public class VhfSatDataEventArgs : EventArgs
    {
        private byte[] buff;

        public byte[] Buff
        {
            get { return this.buff; }
            set { this.buff = value; }
        }

        public VhfSatDataEventArgs(byte[] _buff)
        {
            this.buff = _buff;
        }
    }

    /// <summary>
    /// 위성음성채널 사용 이벤트 아규먼트 클래스
    /// </summary>
    public class SatAudioControlEventArgs : EventArgs
    {
        private byte isOpen;

        public byte IsOpen
        {
            get { return this.isOpen; }
            set { this.isOpen = value; }
        }

        public SatAudioControlEventArgs(byte _isOpen)
        {
            this.isOpen = _isOpen;
        }
    }
}