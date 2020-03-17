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
using Mews.Svr.DBMng.Lib;

namespace Mews.Svr.DBMng
{
    public partial class DBMngOption : Form
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
        /// DB 셋팅 값 변경 시 발생하는 이벤트
        /// </summary>
        public event EventHandler<DbSettingDataEvtArgs> OnDbSettingDataEvt;

        /// <summary>
        /// TCP 통신 제어 이벤트
        /// </summary>
        public event EventHandler<TcpSocketEventArgs> OnTcpSocketEvt;

        /// <summary>
        /// TCP 셋팅 값 변경 이벤트
        /// </summary>
        public event EventHandler<TcpSettingEventArgs> OnTcpSettingEvt;

        /// <summary>
        /// LOG 셋팅 값 변경 이벤트
        /// </summary>
        public event EventHandler<LogSettingEventArgs> OnLogSettingEvt;

        /// <summary>
        /// UDP 셋팅 값 변경 이벤트
        /// </summary>
        public event EventHandler<TcpSettingEventArgs> OnUdpSettingEvt;

        /// <summary>
        /// DB Data Manager
        /// </summary>
        private DBMngDataMng dataMng = null;
        #endregion

        #region val
        //DB
        private string dbIp = string.Empty;
        private string dbSid = string.Empty;
        private string dbId = string.Empty;
        private string dbPw = string.Empty;
        private bool db8i = false;
        private bool dbFLAG = false;

        //TCP
        private string tcpIp = string.Empty;
        private string tcpPort = string.Empty;
        private bool tcpState = false;
        private bool tcpFLAG = false;

        //UDP
        private string udpIp = string.Empty;
        private string udpPort = string.Empty;
        private bool udpFLAG = false;

        //LOG
        private bool logFLAG = false;
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
        /// UDP IP
        /// </summary>
        public string UdpIp
        {
            get { return this.udpIp; }
            set { this.udpIp = value; }
        }

        /// <summary>
        /// UDP PORT
        /// </summary>
        public string UdpPort
        {
            get { return this.udpPort; }
            set { this.udpPort = value; }
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
        /// TCP1 상태 라벨의 글자색 셋팅
        /// </summary>
        public Color Tcp1LBColor
        {
            set { this.Tcp1StateLB.ForeColor = value; }
        }
        #endregion

        public DBMngOption()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.Init();
        }

        #region Init
        private void Init()
        {
            this.dataMng = DBMngDataMng.getInstance();

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
                this.Tcp1StateLB.Text = string.Format("Result : {0}", "Connect");
            }
            else
            {
                this.TcpConBtn.Enabled = true;
                this.TcpCloseBtn.Enabled = false;
                this.Tcp1StateLB.ForeColor = Color.Red;
                this.Tcp1StateLB.Text = string.Format("Result  :  {0}", "Fail");
            }

            //UDP 탭
            this.UdpIpTB.Text = this.udpIp;
            this.UdpPortTB.Text = this.udpPort;

            this.SaveBtn.Enabled = false;
            this.dbFLAG = false;
            this.tcpFLAG = false;
            this.udpFLAG = false;
            this.logFLAG = false;
        }
        #endregion

        #region Method
        /// <summary>
        /// TCP 항목 검사
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// UDP 항목 검사
        /// </summary>
        /// <returns></returns>
        private bool udpValidator()
        {
            if (this.UdpIpTB.Text == string.Empty)
            {
                return false;
            }

            if (this.UdpPortTB.Text == string.Empty)
            {
                return false;
            }

            return true;
        }

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
                    MessageBox.Show("Please check DB items.", "DB", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    MessageBox.Show("Please check TCP items.", "TCP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (this.OnTcpSettingEvt != null)
                {
                    this.OnTcpSettingEvt(this, new TcpSettingEventArgs(this.TcpIpTB.Text, this.TcpPortTB.Text));
                }
            }

            //UDP
            if (this.udpFLAG)
            {
                if (!this.udpValidator())
                {
                    MessageBox.Show("Please check UDP items.", "UDP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (this.OnUdpSettingEvt != null)
                {
                    this.OnUdpSettingEvt(this, new TcpSettingEventArgs(this.UdpIpTB.Text, this.UdpPortTB.Text));
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

            this.SaveBtn.Enabled = false;
            this.dbFLAG = false;
            this.tcpFLAG = false;
            this.udpFLAG = false;
            this.logFLAG = false;
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
                MessageBox.Show("Please check DB items.", "DB Test", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    this.DbTestRstLB.Text = string.Format("Result : {0}", "Success");
                }
                else
                {
                    this.DbTestRstLB.ForeColor = Color.Red;
                    this.DbTestRstLB.Text = string.Format("Result  :  {0}", "Fail");
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
                MessageBox.Show("Please check TCP items.", "TCP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (this.OnTcpSocketEvt != null)
            {
                this.OnTcpSocketEvt(this, new TcpSocketEventArgs(true, this.TcpIpTB.Text, this.TcpPortTB.Text));
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
        /// UDP 항목 변경 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UdpIpTB_TextChanged(object sender, EventArgs e)
        {
            this.udpFLAG = true;
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
}