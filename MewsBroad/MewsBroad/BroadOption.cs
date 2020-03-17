using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using Adeng.Framework.Db;

namespace Mews.Svr.Broad
{
    public partial class BroadOption : Form
    {
        #region instance
        /// <summary>
        /// DB 셋팅 값 변경 시 발생하는 이벤트 핸들러
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="dsdea"></param>
        private delegate void DbSettingDataEvtArgsHandler(object sender, DbSettingDataEvtArgs dsdea);

        /// <summary>
        /// TCP 통신을 제어하는 이벤트 핸들러
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="usea"></param>
        private delegate void TcpSocketEventArgsHandler(object sender, TcpSocketEventArgs usea);

        /// <summary>
        /// TCP 셋팅 값 변경 시 발생하는 이벤트 핸들러
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="usea"></param>
        private delegate void TcpSettingEventArgsHandler(object sender, TcpSettingEventArgs usea);

        /// <summary>
        /// LOG 셋팅 값 변경 시 발생하는 이벤트 핸들러
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="lsea"></param>
        private delegate void LogSettingEventArgsHandler(object sender, LogSettingEventArgs lsea);

        /// <summary>
        /// 부저 셋팅 값 변경 시 발생하는 이벤트 핸들러
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="bea"></param>
        private delegate void BuzzerEventArgsHandler(object sender, BuzzerEventArgs bea);

        /// <summary>
        /// DB 셋팅 값 변경 시 발생하는 이벤트
        /// </summary>
        public event EventHandler<DbSettingDataEvtArgs> OnDbSettingDataEvt;

        /// <summary>
        /// TCP 통신 제어 이벤트 (운영대)
        /// </summary>
        public event EventHandler<TcpSocketEventArgs> OnTcpSocketEvt;

        /// <summary>
        /// TCP 통신 제어 이벤트 (경보대)
        /// </summary>
        public event EventHandler<TcpSocketEventArgs> OnTcpSocketEvt1;

        /// <summary>
        /// TCP 셋팅 값 변경 이벤트 (운영대)
        /// </summary>
        public event EventHandler<TcpSettingEventArgs> OnTcpSettingEvt;

        /// <summary>
        /// TCP 셋팅 값 변경 이벤트 (경보대)
        /// </summary>
        public event EventHandler<TcpSettingEventArgs> OnTcpSettingEvt1;

        /// <summary>
        /// LOG 셋팅 값 변경 이벤트
        /// </summary>
        public event EventHandler<LogSettingEventArgs> OnLogSettingEvt;

        /// <summary>
        /// 부저 셋팅 값 변경 이벤트
        /// </summary>
        public event EventHandler<BuzzerEventArgs> OnBuzzerEvt;
        #endregion

        #region val
        //DB
        private string dbIp = string.Empty;
        private string dbSid = string.Empty;
        private string dbId = string.Empty;
        private string dbPw = string.Empty;
        private bool db8i = false;
        private bool dbFLAG = false;

        //TCP (운영대)
        private string tcpIp = string.Empty;
        private string tcpPort = string.Empty;
        private bool tcpState = false;
        private bool tcpFLAG = false;

        //TCP1 (경보대)
        private string tcpIp1 = string.Empty;
        private string tcpPort1 = string.Empty;
        private bool tcpState1 = false;
        private bool tcpFLAG1 = false;

        //LOG
        private bool logFLAG = false;

        //buzzer
        private bool buzzerFlag = false;
        #endregion

        #region 접근
        /// <summary>
        /// DB IP Address
        /// </summary>
        public string DbIp
        {
            get { return this.dbIp; }
            set { this.dbIp = value; }
        }

        /// <summary>
        /// DB Service ID
        /// </summary>
        public string DbSid
        {
            get { return this.dbSid; }
            set { this.dbSid = value; }
        }

        /// <summary>
        /// DB ID
        /// </summary>
        public string DbId
        {
            get { return this.dbId; }
            set { this.dbId = value; }
        }

        /// <summary>
        /// DB PassWord
        /// </summary>
        public string DbPw
        {
            get { return this.dbPw; }
            set { this.dbPw = value; }
        }

        /// <summary>
        /// DB 연결 시 8i 사용 여부
        /// </summary>
        public bool Db8i
        {
            get { return this.db8i; }
            set { this.db8i = value; }
        }

        /// <summary>
        /// TCP IP
        /// </summary>
        public string TcpIp
        {
            get { return this.tcpIp; }
            set { this.tcpIp = value; }
        }

        /// <summary>
        /// TCP PORT
        /// </summary>
        public string TcpPort
        {
            get { return this.tcpPort; }
            set { this.tcpPort = value; }
        }

        /// <summary>
        /// TCP STATE
        /// </summary>
        public bool TcpState
        {
            get { return this.tcpState; }
            set { this.tcpState = value; }
        }

        /// <summary>
        /// TCP 통신 연결 버튼의 Enable 상태
        /// </summary>
        public bool TcpConButton
        {
            get { return this.TcpConBtn.Enabled; }
            set { this.TcpConBtn.Enabled = value; }
        }

        /// <summary>
        /// TCP 연결 해제 버튼의 Enable 상태
        /// </summary>
        public bool TcpCloseButton
        {
            get { return this.TcpCloseBtn.Enabled; }
            set { this.TcpCloseBtn.Enabled = value; }
        }

        /// <summary>
        /// TCP1 IP
        /// </summary>
        public string TcpIp1
        {
            get { return this.tcpIp1; }
            set { this.tcpIp1 = value; }
        }

        /// <summary>
        /// TCP1 PORT
        /// </summary>
        public string TcpPort1
        {
            get { return this.tcpPort1; }
            set { this.tcpPort1 = value; }
        }

        /// <summary>
        /// TCP1 STATE
        /// </summary>
        public bool TcpState1
        {
            get { return this.tcpState1; }
            set { this.tcpState1 = value; }
        }

        /// <summary>
        /// TCP1 통신 연결 버튼의 Enable 상태
        /// </summary>
        public bool TcpConButton1
        {
            get { return this.TcpConBtn1.Enabled; }
            set { this.TcpConBtn1.Enabled = value; }
        }

        /// <summary>
        /// TCP1 연결 해제 버튼의 Enable 상태
        /// </summary>
        public bool TcpCloseButton1
        {
            get { return this.TcpCloseBtn1.Enabled; }
            set { this.TcpCloseBtn1.Enabled = value; }
        }

        /// <summary>
        /// 로그 체크박스의 체크 상태
        /// </summary>
        public bool LogCheckBox
        {
            get { return this.LogCB.Checked; }
            set { this.LogCB.Checked = value; }
        }

        /// <summary>
        /// TCP1 상태 표시
        /// </summary>
        public string Tcp1StsLB
        {
            set { this.Tcp1StateLB.Text = value; }
        }

        /// <summary>
        /// TCP2 상태 표시
        /// </summary>
        public string Tcp2StsLB
        {
            set { this.Tcp2StateLB.Text = value; }
        }

        /// <summary>
        /// TCP1 상태 라벨의 글자색 셋팅
        /// </summary>
        public Color Tcp1LBColor
        {
            set { this.Tcp1StateLB.ForeColor = value; }
        }

        /// <summary>
        /// TCP2 상태 라벨의 글자색 셋팅
        /// </summary>
        public Color Tcp2LBColor
        {
            set { this.Tcp2StateLB.ForeColor = value; }
        }

        /// <summary>
        /// 실제 방송 부저
        /// </summary>
        public bool RealBuzzerCB
        {
            set { this.checkBox1.Checked = value; }
            get { return this.checkBox1.Checked; }
        }

        /// <summary>
        /// 훈련 방송 부저
        /// </summary>
        public bool DrillBuzzerCB
        {
            set { this.checkBox2.Checked = value; }
            get { return this.checkBox2.Checked; }
        }

        /// <summary>
        /// 시험 방송 부저
        /// </summary>
        public bool TestBuzzerCB
        {
            set { this.checkBox3.Checked = value; }
            get { return this.checkBox3.Checked; }
        }
        #endregion

        public BroadOption(string _isPrimary)
        {
            InitializeComponent();

            if (_isPrimary == "1")
            {
                this.TopPN.BackgroundImage = MewsBroad.Properties.Resources.bgTitle;
            }
            else
            {
                this.TopPN.BackgroundImage = MewsBroad.Properties.Resources.bgTitleGreen;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.initLang();
            this.Init();
        }

        #region Init
        private void Init()
        {
            //DB 탭
            this.DbIPTB.Text = this.dbIp;
            this.DbSidTB.Text = this.dbSid;
            this.DbIdTB.Text = this.dbId;
            this.DbPwTB.Text = this.dbPw;
            this.Oracle8CB.Checked = this.db8i;

            //TCP 탭
            this.TcpIpTB.Text = this.tcpIp;
            this.TcpPortTB.Text = this.tcpPort;

            if (this.tcpState)
            {
                this.TcpCloseBtn.Enabled = true;
                this.TcpConBtn.Enabled = false;
                this.Tcp1StateLB.ForeColor = Color.Blue;
                this.Tcp1StateLB.Text = string.Format(LangPack.GetTCPResult() + " : {0}", LangPack.GetTCPConnect());
            }
            else
            {
                this.TcpConBtn.Enabled = true;
                this.TcpCloseBtn.Enabled = false;
                this.Tcp1StateLB.ForeColor = Color.Red;
                this.Tcp1StateLB.Text = string.Format(LangPack.GetTCPResult() + " : {0}", LangPack.GetTCPFail());
            }

            //TCP1 탭
            this.TcpIpTB1.Text = this.tcpIp1;
            this.TcpPortTB1.Text = this.tcpPort1;

            if (this.tcpState1)
            {
                this.TcpCloseBtn1.Enabled = true;
                this.TcpConBtn1.Enabled = false;
                this.Tcp2StateLB.ForeColor = Color.Blue;
                this.Tcp2StateLB.Text = string.Format(LangPack.GetTCPResult() + " : {0}", LangPack.GetTCPConnect());
            }
            else
            {
                this.TcpConBtn1.Enabled = true;
                this.TcpCloseBtn1.Enabled = false;
                this.Tcp2StateLB.ForeColor = Color.Red;
                this.Tcp2StateLB.Text = string.Format(LangPack.GetTCPResult() + " : {0}", LangPack.GetTCPFail());
            }

            this.SaveBtn.Enabled = false;
            this.dbFLAG = false;
            this.tcpFLAG = false;
            this.tcpFLAG1 = false;
            this.logFLAG = false;
            this.buzzerFlag = false;
        }
        #endregion

        #region Method
        /// <summary>
        /// 설정 값 저장 메소드
        /// </summary>
        private void save()
        {
            //DB
            if (this.dbFLAG)
            {
                if (!this.dbValidator())
                {
                    MessageBox.Show(LangPack.GetMongolian("Please check DB items."), LangPack.GetMongolian("DB"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (this.OnDbSettingDataEvt != null)
                {
                    this.OnDbSettingDataEvt(this, new DbSettingDataEvtArgs(
                        this.DbIPTB.Text, this.DbSidTB.Text,
                        this.DbIdTB.Text, this.DbPwTB.Text, this.Oracle8CB.Checked));
                }
            }

            //TCP
            if (this.tcpFLAG)
            {
                if (!this.tcp1Validator())
                {
                    MessageBox.Show(LangPack.GetMongolian("Please check TCP1 items."), LangPack.GetMongolian("TCP1"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
             
                if (this.OnTcpSettingEvt != null)
                {
                    this.OnTcpSettingEvt(this, new TcpSettingEventArgs(this.TcpIpTB.Text, this.TcpPortTB.Text));
                }
            }

            //TCP1
            if (this.tcpFLAG1)
            {
                if (!this.tcp2Validator())
                {
                    MessageBox.Show(LangPack.GetMongolian("Please check TCP2 items."), LangPack.GetMongolian("TCP2"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (this.OnTcpSettingEvt1 != null)
                {
                    this.OnTcpSettingEvt1(this, new TcpSettingEventArgs(this.TcpIpTB1.Text, this.TcpPortTB1.Text));
                }
            }

            //LOG
            if (this.logFLAG)
            {
                if (this.OnLogSettingEvt != null)
                {
                    this.OnLogSettingEvt(this, new LogSettingEventArgs(this.LogCB.Checked));
                }
            }

            if (this.buzzerFlag)
            {
                if (this.OnBuzzerEvt != null)
                {
                    this.OnBuzzerEvt(this, new BuzzerEventArgs(this.checkBox1.Checked, this.checkBox2.Checked, this.checkBox3.Checked));
                }
            }

            this.SaveBtn.Enabled = false;
            this.dbFLAG = false;
            this.tcpFLAG = false;
            this.tcpFLAG1 = false;
            this.logFLAG = false;
            this.buzzerFlag = false;
        }

        /// <summary>
        /// DB 테스트 전의 유효성 검사
        /// </summary>
        /// <returns></returns>
        private bool dbValidator()
        {
            if (this.DbIPTB.Text == string.Empty)
            {
                return false;
            }

            if (this.DbSidTB.Text == string.Empty)
            {
                return false;
            }

            if (this.DbIdTB.Text == string.Empty)
            {
                return false;
            }

            if (this.DbPwTB.Text == string.Empty)
            {
                return false;
            }

            return true;
        }

        private bool tcp1Validator()
        {
            if (this.TcpIpTB.Text == string.Empty)
            {
                return false;
            }

            if (this.TcpPortTB.Text == string.Empty)
            {
                return false;
            }

            return true;
        }

        private bool tcp2Validator()
        {
            if (this.TcpIpTB1.Text == string.Empty)
            {
                return false;
            }

            if (this.TcpPortTB1.Text == string.Empty)
            {
                return false;
            }

            return true;
        }
        #endregion

        #region event
        /// <summary>
        /// 확인 버튼 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OKBtn_Click(object sender, EventArgs e)
        {
            this.save();
            this.Close();
        }

        /// <summary>
        /// 취소 버튼 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancleBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 적용 버튼 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            this.save();
        }

        /// <summary>
        /// DB 테스트 버튼 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DbTestBtn_Click(object sender, EventArgs e)
        {
            if (!this.dbValidator())
            {
                MessageBox.Show(LangPack.GetMongolian("Please check DB items."), LangPack.GetMongolian("DB Test"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            AdengOleDb db = null;

            try
            {
                db = new AdengOleDb();
                db.OracleProvider = "OraOLEDB.Oracle.1"; //64bit OracleProvider
                db.OpenOracle(this.DbIPTB.Text, this.DbSidTB.Text, this.DbIdTB.Text, this.DbPwTB.Text, this.Oracle8CB.Checked);

                if (db.IsOpen)
                {
                    this.DbTestRstLB.ForeColor = Color.Blue;
                    this.DbTestRstLB.Text = string.Format(LangPack.GetTCPResult() + " : {0}", LangPack.GetTCPSuccess());
                }
                else
                {
                    this.DbTestRstLB.ForeColor = Color.Red;
                    this.DbTestRstLB.Text = string.Format(LangPack.GetTCPResult() + " : {0}", LangPack.GetTCPFail());
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("DBMngOption.DbTestBtn_Click - " + ex.Message);
            }
            finally
            {
                if (db != null)
                {
                    db.Close();
                    db = null;
                }
            }
        }

        /// <summary>
        /// TCP 통신연결 버튼 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TcpConBtn_Click(object sender, EventArgs e)
        {
            if (this.TcpIpTB.Text == string.Empty || this.TcpPortTB.Text == string.Empty)
            {
                MessageBox.Show(LangPack.GetMongolian("Please check TCP1 items."), LangPack.GetMongolian("TCP1 Connect"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (this.OnTcpSocketEvt != null)
            {
                this.OnTcpSocketEvt(this, new TcpSocketEventArgs(true, this.TcpIpTB.Text, this.TcpPortTB.Text));
            }
        }

        /// <summary>
        /// TCP1 통신연결 버튼 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TcpConBtn1_Click(object sender, EventArgs e)
        {
            if (this.TcpIpTB1.Text == string.Empty || this.TcpPortTB1.Text == string.Empty)
            {
                MessageBox.Show(LangPack.GetMongolian("Please check TCP2 items."), LangPack.GetMongolian("TCP2 Connect"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (this.OnTcpSocketEvt1 != null)
            {
                this.OnTcpSocketEvt1(this, new TcpSocketEventArgs(true, this.TcpIpTB1.Text, this.TcpPortTB1.Text));
            }
        }

        /// <summary>
        /// TCP 연결해제 버튼 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TcpCloseBtn_Click(object sender, EventArgs e)
        {
            if (this.OnTcpSocketEvt != null)
            {
                this.OnTcpSocketEvt(this, new TcpSocketEventArgs(false, string.Empty, string.Empty));
            }
        }

        /// <summary>
        /// TCP1 연결해제 버튼 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TcpCloseBtn1_Click(object sender, EventArgs e)
        {
            if (this.OnTcpSocketEvt1 != null)
            {
                this.OnTcpSocketEvt1(this, new TcpSocketEventArgs(false, string.Empty, string.Empty));
            }
        }

        /// <summary>
        /// 부저 체크박스 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            this.buzzerFlag = true;
            this.SaveBtn.Enabled = true;
        }

        /// <summary>
        /// 로그 체크박스 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LogCB_CheckedChanged(object sender, EventArgs e)
        {
            this.logFLAG = true;
            this.SaveBtn.Enabled = true;
        }

        /// <summary>
        /// DB 항목 변경 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DbIPTB_TextChanged(object sender, EventArgs e)
        {
            this.dbFLAG = true;
            this.SaveBtn.Enabled = true;
        }

        /// <summary>
        /// TCP 항목 변경 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TcpIpTB_TextChanged(object sender, EventArgs e)
        {
            this.tcpFLAG = true;
            this.SaveBtn.Enabled = true;
        }

        /// <summary>
        /// TCP1 항목 변경 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TcpIpTB1_TextChanged(object sender, EventArgs e)
        {
            this.tcpFLAG1 = true;
            this.SaveBtn.Enabled = true;
        }

        /// <summary>
        /// IP 텍스트박스에 숫자(점 포함)만 입력가능하게 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TcpIpTB_KeyDown(object sender, KeyEventArgs e)
        {
            // 8 - 백스페이스
            // 46 - DEL
            // 37 - 좌 화살표
            // 38 - 위 화살표
            // 39 - 우 화살표
            // 40 - 아래 화살표
            // 16 - 쉬프트
            // 190 - '.'
            // 110 - '.'
            // (48 ~ 57) - 숫자 0 ~ 9 
            // (96 ~ 105) - 숫자 0 ~ 9
            // 35 - End
            // 36 - Home
            if (e.KeyValue == 8 || e.KeyValue == 46 || e.KeyValue == 37 || e.KeyValue == 38
                || e.KeyValue == 39 || e.KeyValue == 40 || e.KeyValue == 16 || e.KeyValue == 190 || e.KeyValue == 110
                || (e.KeyValue > 47 && e.KeyValue < 58)
                || (e.KeyValue > 95 && e.KeyValue < 106)
                || e.KeyValue == 35 || e.KeyValue == 36)
            {
                e.SuppressKeyPress = false;
            }
            else
            {
                e.SuppressKeyPress = true;
            }
        }

        /// <summary>
        /// 포트 텍스트박스에 숫자만 가능하게 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TcpPortTB_KeyDown(object sender, KeyEventArgs e)
        {
            // 8 - 백스페이스
            // 46 - DEL
            // 37 - 좌 화살표
            // 38 - 위 화살표
            // 39 - 우 화살표
            // 40 - 아래 화살표
            // 16 - 쉬프트
            // (48 ~ 57) - 숫자 0 ~ 9 
            // (96 ~ 105) - 숫자 0 ~ 9
            // 35 - End
            // 36 - Home
            if (e.KeyValue == 8 || e.KeyValue == 46 || e.KeyValue == 16
                || e.KeyValue == 37 || e.KeyValue == 38 || e.KeyValue == 39 || e.KeyValue == 40
                || (e.KeyValue > 47 && e.KeyValue < 58)
                || (e.KeyValue > 95 && e.KeyValue < 106)
                || e.KeyValue == 35 || e.KeyValue == 36)
            {
                e.SuppressKeyPress = false;
            }
            else
            {
                e.SuppressKeyPress = true;
            }
        }

        private void initLang()
        {
            this.label1.Text = LangPack.GetOption();
            this.DBTP.Text = LangPack.GetDB();
            this.TcpTP.Text = LangPack.GetTCP1();
            this.TcpTP1.Text = LangPack.GetTCP2();
            this.LogTP.Text = LangPack.GetLog();
            this.tabPage1.Text = LangPack.GetBuzzer();
            this.OKBtn.Text = LangPack.GetClose();
            this.CancleBtn.Text = LangPack.Getcancel();
            this.SaveBtn.Text = LangPack.GetSave();

            this.DbIPLB.Text = LangPack.GetDBIP();
            this.DbSidLB.Text = LangPack.GetDBSID();
            this.DbIdLB.Text = LangPack.GetDBID();
            this.DbPwLB.Text = LangPack.GetDBPW();
            this.DbTestBtn.Text = LangPack.GetDBTest();
            this.Oracle8CB.Text = LangPack.Get8IDUSE();
            this.DbLB.Text = LangPack.GetDBConnSet();

            this.TcpIpLB.Text = LangPack.GetTCPIP();
            this.TcpPortLB.Text = LangPack.GetTCPPORT();
            this.TcpConBtn.Text = LangPack.GetTCPConnect();
            this.TcpCloseBtn.Text = LangPack.GetTCPClose();
            this.TcpLB.Text = LangPack.GetOperationManagementTCPSetting();

            this.TcpIpLB1.Text = LangPack.GetTCPIP();
            this.TcpPortLB1.Text = LangPack.GetTCPPORT();
            this.TcpConBtn1.Text = LangPack.GetTCPConnect();
            this.TcpCloseBtn1.Text = LangPack.GetTCPClose();
            this.TcpLB1.Text = LangPack.GetControlPanelTCPSetting();

            this.LogCB.Text = LangPack.GetLogView();
            this.LogLB.Text = LangPack.GetTheLogwillscreen() + "\n" + LangPack.GetCanslowprogram();

            this.labelEx1.Text = LangPack.GetBroadCastBuzzerSet();
            this.checkBox1.Text = LangPack.GetReal();
            this.checkBox2.Text = LangPack.GetDrill();
            this.checkBox3.Text = LangPack.GetTest();
        }
        #endregion
    }

    /// <summary>
    /// DB의 셋팅 값 변경이 있을 때 발생하는 이벤트 아규먼트 클래스
    /// </summary>
    public class DbSettingDataEvtArgs : EventArgs
    {
        private string dbIp = string.Empty;
        private string dbSid = string.Empty;
        private string dbId = string.Empty;
        private string dbPw = string.Empty;
        private bool oracle8 = false;

        public string DbIp
        {
            get { return this.dbIp; }
            set { this.dbIp = value; }
        }

        public string DbSid
        {
            get { return this.dbSid; }
            set { this.dbSid = value; }
        }

        public string DbId
        {
            get { return this.dbId; }
            set { this.dbId = value; }
        }

        public string DbPw
        {
            get { return this.dbPw; }
            set { this.dbPw = value; }
        }

        public bool Oracle8
        {
            get { return this.oracle8; }
            set { this.oracle8 = value; }
        }

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="_dbIp">DB IP</param>
        /// <param name="_dbSid">DB Service ID</param>
        /// <param name="_dbId">DB ID</param>
        /// <param name="_dbPw">DB Password</param>
        /// <param name="_oracle8">Oracle 8i 버전 사용 유무</param>
        public DbSettingDataEvtArgs(
            string _dbIp,
            string _dbSid,
            string _dbId,
            string _dbPw,
            bool _oracle8)
        {
            this.dbIp = _dbIp;
            this.dbSid = _dbSid;
            this.dbId = _dbId;
            this.dbPw = _dbPw;
            this.oracle8 = _oracle8;
        }
    }

    /// <summary>
    /// TCP 통신 제어 이벤트를 발생시킬 때 사용하는 이벤트 아규먼트 클래스
    /// </summary>
    public class TcpSocketEventArgs : EventArgs
    {
        private bool state = false;
        private string ip = string.Empty;
        private string port = string.Empty;

        public bool State
        {
            get { return this.state; }
            set { this.state = value; }
        }

        public string Ip
        {
            get { return this.ip; }
            set { this.ip = value; }
        }

        public string Port
        {
            get { return this.port; }
            set { this.port = value; }
        }

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="_state"></param>
        public TcpSocketEventArgs(bool _state, string _ip, string _port)
        {
            this.state = _state;
            this.ip = _ip;
            this.port = _port;
        }
    }

    /// <summary>
    /// TCP의 셋팅 값 변경이 있을 때 발생하는 이벤트 아규먼트 클래스
    /// </summary>
    public class TcpSettingEventArgs : EventArgs
    {
        private string ip = string.Empty;
        private string port = string.Empty;

        public string Ip
        {
            get { return this.ip; }
            set { this.ip = value; }
        }

        public string Port
        {
            get { return this.port; }
            set { this.port = value; }
        }

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="_state"></param>
        public TcpSettingEventArgs(string _ip, string _port)
        {
            this.ip = _ip;
            this.port = _port;
        }
    }

    /// <summary>
    /// 로그 표시 셋팅 값 변경이 있을 때 발생하는 이벤트 아규먼트 클래스
    /// </summary>
    public class LogSettingEventArgs : EventArgs
    {
        private bool state = false;

        public bool State
        {
            get { return this.state; }
            set { this.state = value; }
        }

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="_state"></param>
        public LogSettingEventArgs(bool _state)
        {
            this.state = _state;
        }
    }

    public class BuzzerEventArgs : EventArgs
    {
        private bool real = false;
        private bool drill = false;
        private bool test = false;

        public bool Real
        {
            get { return this.real; }
            set { this.real = value; }
        }

        public bool Drill
        {
            get { return this.drill; }
            set { this.drill = value; }
        }

        public bool Test
        {
            get { return this.test; }
            set { this.test = value; }
        }

        public BuzzerEventArgs(bool _real, bool _drill, bool _test)
        {
            this.real = _real;
            this.drill = _drill;
            this.test = _test;
        }
    }
}