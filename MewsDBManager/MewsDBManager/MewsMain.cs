using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using Adeng.Framework.Util;
using Adeng.Framework.Log;
using Adeng.Framework.Net;
using Mews.Svr.DBMng.Lib;
using Mews.Lib.Mewstcp;
using Mews.Lib.Mewsprt;
using Mews.Lib.MesLog;
using ADEng.Library.DMB_SD;

namespace Mews.Svr.DBMng
{
    public partial class MewsMain : Form
    {
        #region instance
        private TCPModClient clientSoc = null;
        private DBMngDataMng dataMng = null;
        private AdengUdpSocket udpSoc = null;
        private Queue<PrtBase> insertQ = null;
        private SimpleAsyncProc proc = new SimpleAsyncProc();
        private DBMngOption optionForm = null;
        private Thread procCountTD = null; //큐 처리 카운터 Thread
        #endregion

        #region val
        //private const string version1 = " Ver 1.0.0 (2012.11.21)"; //초기 버전
        private const string version1 = " Ver 1.9.0 (2014.06.23)"; //2차년도 사업 일부 적용
        private const int MainListBoxCount = 1000; //메인 리스트박스에 표출한 Items 수
        
        private readonly int getQueueCntTime = 500; //큐 카운트를 가져오는 시간
        private readonly string IniFileDirectoryPath = "C:\\Program Files\\adeng\\MewsDBMng";
        private readonly string IniFilePath = "C:\\Program Files\\adeng\\MewsDBMng\\Setting.ini";
        private readonly string setting = "SETTING";
        private readonly string DBIP = "DBIP";
        private readonly string DBSID = "DBSID";
        private readonly string DBID = "DBID";
        private readonly string DBPW = "DBPW";
        private readonly string Oracle8i = "Oracle8i";
        private readonly string TCPIP = "TCPIP";
        private readonly string TCPPort = "TCPPort";
        private readonly string LogFlag = "LogFLAG";
        private readonly string UDPIP = "UDPIP";
        private readonly string UDPPort = "UDPPort";

        private bool tcpState = false; //TCP 통신 상태 플래그
        private bool logState = false; //Log 표시여부 플래그
        private uint procCount = uint.MinValue; //큐 처리 카운트
        private string localTcpIp = string.Empty; //TCP 통신 IP
        private int localTcpPort = int.MinValue; //TCP 통신 PORT
        #endregion

        public MewsMain()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (AdengUtil.CheckAppOverlap("MewsDBManager"))
            {
                MessageBox.Show("The program is already running.", "EDWS DB Manager", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Dispose(true);
            }

            this.init();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;
            base.OnClosing(e);

            this.Hide();
            this.NotifyIcon.Visible = true;
        }

        /// <summary>
        /// OS 종료 메시지 받아 처리하기 위한..
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x11)
            {
                this.Dispose(true);
            }

            base.WndProc(ref m);
        }

        #region Init
        /// <summary>
        /// 초기화
        /// </summary>
        private void init()
        {
            //디렉토리가 있는지 체크..
            if (!Directory.Exists(this.IniFileDirectoryPath))
            {
                Directory.CreateDirectory(IniFileDirectoryPath);
            }

            //파일이 있는지 체크..없으면 파일 생성
            if (!File.Exists(IniFilePath))
            {
                AdengUtil.INIWriteValueString(this.setting, this.DBIP, "58.181.17.58", IniFilePath);
                AdengUtil.INIWriteValueString(this.setting, this.DBSID, "orcl", IniFilePath);
                AdengUtil.INIWriteValueString(this.setting, this.DBID, "mews33", IniFilePath);
                AdengUtil.INIWriteValueString(this.setting, this.DBPW, "mews", IniFilePath);
                AdengUtil.INIWriteValueString(this.setting, this.Oracle8i, "F", IniFilePath);
                AdengUtil.INIWriteValueString(this.setting, this.TCPIP, "127.0.0.1", IniFilePath);
                AdengUtil.INIWriteValueString(this.setting, this.TCPPort, "9009", IniFilePath);
                AdengUtil.INIWriteValueString(this.setting, this.LogFlag, "F", IniFilePath);
                AdengUtil.INIWriteValueString(this.setting, this.UDPIP, "127.0.0.1", IniFilePath);
                AdengUtil.INIWriteValueString(this.setting, this.UDPPort, "7001", IniFilePath);
            }

            //tcp client socket 생성
            this.clientSoc = TCPModClient.getTCPModClient();
            this.clientSoc.evConnected += new TCPModClient.ConnectedHandler(clientSoc_evConnected);
            this.clientSoc.evConnectFailed += new TCPModClient.ConnectedFailedHandler(clientSoc_evConnectFailed);
            this.clientSoc.evDisconnected += new TCPModClient.DisconnectedHandler(clientSoc_evDisconnected);
            this.clientSoc.evReceived += new TCPModClient.ReceivedHandler(clientSoc_evReceived);
            this.clientSoc.evSendFailed += new TCPModClient.SendFailedHandler(clientSoc_evSendFailed);
            this.clientSoc.evTimeout += new TCPModClient.TimeOutHandler(clientSoc_evTimeout);

            //udp socket 생성
            try
            {
                this.udpSoc = new AdengUdpSocket(AdengUtil.INIReadValueString(this.setting, this.UDPIP, this.IniFilePath),
                    int.Parse(AdengUtil.INIReadValueString(this.setting, this.UDPPort, this.IniFilePath)));
                this.udpSoc.recvEvtHandler += new RecvEvtHandler(udpSoc_recvEvtHandler);
                this.SetLogListBox("UDP Success!");
            }
            catch (Exception ex)
            {
                this.SetLogListBox("UDP Fail!");
            }

            //data Manager 생성
            this.dataMng = DBMngDataMng.getInstance();
            this.dataMng.OnTermStateTCPSendEvt += new EventHandler<TermStateTCPSendEvtArgs>(dataMng_OnTermStateTCPSendEvt);

            //queue 생성
            this.insertQ = new Queue<PrtBase>();

            try
            {
                //로그 셋팅 값을 가져온다.
                this.logState = (AdengUtil.INIReadValueString(this.setting, this.LogFlag, IniFilePath) == "T" ? true : false);

                //TCP 연결
                this.localTcpIp = AdengUtil.INIReadValueString(this.setting, this.TCPIP, this.IniFilePath);
                this.localTcpPort = int.Parse(AdengUtil.INIReadValueString(this.setting, this.TCPPort, this.IniFilePath));
                this.clientSoc.ConnectTo(this.localTcpIp, this.localTcpPort);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("MewsMain.init - " + ex.Message);
            }

            //폴링 데이터 처리
            PrtBase p9 = PrtMng.GetPrtObject(9);
            p9.MakeData();
            byte[] buff = PrtMng.MakeFrame(p9);
            this.clientSoc.BtPoll = buff;

            //기초데이터 로드 (단말)
            if (this.TryConnect())
            {
                if (this.dataMng.InitDataLoad())
                {
                    this.SetLogListBox("Init Data Load Success!");
                }
                else
                {
                    this.SetLogListBox("Init Data Load Fail (DB Read Fail!)");
                }
            }
            else
            {
                this.SetLogListBox("Init Data Load Fail (DB Connect Fail!)");
            }

            //처리한 큐 카운트를 라벨에 셋팅한다.
            this.procCountTD = new Thread(new ThreadStart(this.SetDataProcCnt));
            this.procCountTD.IsBackground = true;
            this.procCountTD.Start();

            //큐처리
            this.proc.runAsyncProcess += new InvokeValue(proc_runAsyncProcess);
            this.proc.Start();

            //프로그램 제목 최신 버전 표시
            this.Text += version1;

            //시작 로그
            if (this.logState)
            {
                MTextLog.WriteTextLog(true, this.Text + " START");
            }
        }
        #endregion

        #region UI event
        /// <summary>
        /// 아이콘 더블 클릭 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NotifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            this.NotifyIcon.Visible = false;
        }

        /// <summary>
        /// 옵션 클릭 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void optionOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (this.optionForm = new DBMngOption())
            {
                this.optionForm.OnDbSettingDataEvt +=new EventHandler<DbSettingDataEvtArgs>(optionForm_OnDbSettingDataEvt);
                this.optionForm.OnTcpSocketEvt+=new EventHandler<TcpSocketEventArgs>(optionForm_OnTcpSocketEvt);
                this.optionForm.OnTcpSettingEvt+=new EventHandler<TcpSettingEventArgs>(optionForm_OnTcpSettingEvt);
                this.optionForm.OnLogSettingEvt+=new EventHandler<LogSettingEventArgs>(optionForm_OnLogSettingEvt);
                this.optionForm.OnUdpSettingEvt += new EventHandler<TcpSettingEventArgs>(optionForm_OnUdpSettingEvt);

                //DB
                this.optionForm.DbIp = AdengUtil.INIReadValueString(this.setting, this.DBIP, this.IniFilePath);
                this.optionForm.DbSid = AdengUtil.INIReadValueString(this.setting, this.DBSID, this.IniFilePath);
                this.optionForm.DbId = AdengUtil.INIReadValueString(this.setting, this.DBID, this.IniFilePath);
                this.optionForm.DbPw = AdengUtil.INIReadValueString(this.setting, this.DBPW, this.IniFilePath);
                this.optionForm.Db8i = (AdengUtil.INIReadValueString(this.setting, this.Oracle8i, this.IniFilePath) == "T" ? true : false);

                //TCP
                this.optionForm.TcpIp = AdengUtil.INIReadValueString(this.setting, this.TCPIP, this.IniFilePath);
                this.optionForm.TcpPort = AdengUtil.INIReadValueString(this.setting, this.TCPPort, this.IniFilePath);
                this.optionForm.TcpState = this.tcpState;

                //UDP
                this.optionForm.UdpIp = AdengUtil.INIReadValueString(this.setting, this.UDPIP, this.IniFilePath);
                this.optionForm.UdpPort = AdengUtil.INIReadValueString(this.setting, this.UDPPort, this.IniFilePath);

                //LOG
                this.optionForm.LogCheckBox = (AdengUtil.INIReadValueString(this.setting, this.LogFlag, this.IniFilePath) == "T" ? true : false);

                //ShowDialog
                this.optionForm.ShowDialog();

                this.optionForm.OnDbSettingDataEvt -= new EventHandler<DbSettingDataEvtArgs>(optionForm_OnDbSettingDataEvt);
                this.optionForm.OnTcpSocketEvt -= new EventHandler<TcpSocketEventArgs>(optionForm_OnTcpSocketEvt);
                this.optionForm.OnTcpSettingEvt -= new EventHandler<TcpSettingEventArgs>(optionForm_OnTcpSettingEvt);
                this.optionForm.OnLogSettingEvt -= new EventHandler<LogSettingEventArgs>(optionForm_OnLogSettingEvt);
                this.optionForm.OnUdpSettingEvt -= new EventHandler<TcpSettingEventArgs>(optionForm_OnUdpSettingEvt);
            }
        }

        /// <summary>
        /// 종료 메뉴 클릭 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitXToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// NotifyIcon Context 메뉴 중 Open 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CMSToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Show();
            this.NotifyIcon.Visible = false;
        }

        /// <summary>
        /// NotifyIcon Context 메뉴 중 Exit 이벤트 (프로그램 강제 종료)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CMSToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            this.clientSoc.UnInitTCPModClient(); //TCP 종료
            this.proc.Stop(); //큐 스레드 종료
            this.procCountTD.Abort(); //큐 카운트 종료

            //종료 로그
            if (this.logState)
            {
                MTextLog.WriteTextLog(true, this.Text + " END");
            }

            foreach (Process proc in Process.GetProcesses())
            {
                if (proc.ProcessName.ToUpper().StartsWith("MEWSDBMANAGER"))
                {
                    proc.Kill();
                }
            }
        }

        /// <summary>
        /// ListBox Context 메뉴 중 Clear 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clearCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.MainLB.Items.Clear();
        }
        #endregion

        #region event
        /// <summary>
        /// DB Management로부터(udp) 수신받는 리시브 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void udpSoc_recvEvtHandler(object sender, AdengRecvEvtArgs e)
        {
            if (e.Len > 0)
            {
                try
                {
                    PrtBase protoBase = PrtMng.ParseFrame(e.Buff);

                    switch (protoBase.Cmd)
                    {
                        case 30:
                            PrtCmd30 p30 = protoBase as PrtCmd30;

                            if (p30.CompleteState == 1) //CC변경 완료
                            {
                                this.SetLogListBox("Data receive from CC DB Management - Basic data changed!");
                                this.clientSoc.Send(e.Buff);
                                this.SetLogListBox("Data send to CC Opration Management - Basic data changed!");
                            }
                            break;

                        default:
                            break;
                    }
                }
                catch (Exception ex)
                {
                    this.SetLogListBox("UDP data receiver error - " + ex.Message);
                }
            }
        }

        /// <summary>
        /// 프로그램 정보 메뉴 클릭 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void infomationIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (BroadInfoForm form = new BroadInfoForm(this.Text))
            {
                form.ShowDialog();
            }
        }

        /// <summary>
        /// 단말 이상/정상 패킷을 받은 후 정상적으로 처리한 뒤 운영대로 보내는 수신확인 패킷
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataMng_OnTermStateTCPSendEvt(object sender, TermStateTCPSendEvtArgs e)
        {
            try
            {
                e.Cmd15.TermSts = 2; //수신 확인
                byte[] buff = e.Cmd15.MakeData();
                this.clientSoc.Send(buff);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("MewsMain.dataMng_OnTermStateTCPSendEvt - " + ex.Message);
            }
        }

        /// <summary>
        /// TCP 서버에서 발생시킨 Timeout 이벤트
        /// </summary>
        private void clientSoc_evTimeout()
        {
            this.tcpState = false;
            this.SetLogListBox(string.Format("TCP {0} / {1} Server Timeout!", this.localTcpIp, this.localTcpPort));
            this.SetToolLBImage(this.ToolStripStatusLabel3, MewsDBManager.Properties.Resources.IconListRed);
            this.SetOptionTcpBtn(false);
        }

        /// <summary>
        /// TCP 서버로 데이터 전송 실패 이벤트
        /// </summary>
        /// <param name="exc"></param>
        private void clientSoc_evSendFailed(Exception exc)
        {
            this.SetLogListBox(string.Format("TCP {0} / {1} Server Data Send Fail!", this.localTcpIp, this.localTcpPort));
        }

        /// <summary>
        /// 서버 소켓에서 전송한 데이터 이벤트
        /// </summary>
        /// <param name="buff"></param>
        private void clientSoc_evReceived(byte[] buff)
        {
            try
            {
                PrtBase pBase = PrtMng.ParseFrame(buff);

                if (pBase.Cmd == 9)
                {
                    return;
                }

                if (pBase.Cmd == 3)
                {
                    PrtCmd03 p3 = pBase as PrtCmd03;

                    if ((p3.Kind == (byte)PrtCmd03.emKind.SET_TIME) && (p3.Addr == 0xffffffff))
                    {
                        this.setLocalTime(p3.SetTimeDT);
                        return;
                    }
                }

                if (pBase != null)
                {
                    lock (this.insertQ)
                    {
                        this.insertQ.Enqueue(pBase);
                    }

                    this.proc.ManuEvent.Set();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("clientSoc_evReceived - " + ex.Message);
            }
        }

        /// <summary>
        /// 서버와 TCP 통신 연결 종료 이벤트
        /// </summary>
        /// <param name="exc"></param>
        private void clientSoc_evDisconnected(Exception exc)
        {
            this.tcpState = false;
            this.SetLogListBox(string.Format("TCP {0} / {1} Connect Close!", this.localTcpIp, this.localTcpPort));
            this.SetToolLBImage(this.ToolStripStatusLabel3, MewsDBManager.Properties.Resources.IconListRed);
            this.SetOptionTcpBtn(false);
        }

        /// <summary>
        /// 서버와 TCP 통신 연결 실패 이벤트
        /// </summary>
        /// <param name="exc"></param>
        private void clientSoc_evConnectFailed(Exception exc)
        {
            this.tcpState = false;
            this.SetLogListBox(string.Format("TCP {0} / {1} Connect Fail!", this.localTcpIp, this.localTcpPort));
            this.SetToolLBImage(this.ToolStripStatusLabel3, MewsDBManager.Properties.Resources.IconListRed);
            this.SetOptionTcpBtn(false);
        }

        /// <summary>
        /// 서버와 TCP 통신 연결 성공 이벤트
        /// </summary>
        private void clientSoc_evConnected()
        {
            this.tcpState = true;
            this.SetLogListBox(string.Format("TCP {0} / {1} Connect Success!", this.localTcpIp, this.localTcpPort));
            this.SetToolLBImage(this.ToolStripStatusLabel3, MewsDBManager.Properties.Resources.IconListGreen);
            this.SetOptionTcpBtn(true);
        }

        /// <summary>
        /// 환경 설정창에서 발생하는 Log 셋팅 값 변경 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void optionForm_OnLogSettingEvt(object sender, LogSettingEventArgs e)
        {
            if (e.State)
            {
                AdengUtil.INIWriteValueString(this.setting, this.LogFlag, "T", this.IniFilePath);
                this.logState = true;
            }
            else
            {
                AdengUtil.INIWriteValueString(this.setting, this.LogFlag, "F", this.IniFilePath);
                this.logState = false;
            }
        }

        /// <summary>
        /// 환경 설정창에서 발생하는 UDP 셋팅 값 변경 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void optionForm_OnUdpSettingEvt(object sender, TcpSettingEventArgs e)
        {
            AdengUtil.INIWriteValueString(this.setting, this.UDPIP, e.Ip, this.IniFilePath);
            AdengUtil.INIWriteValueString(this.setting, this.UDPPort, e.Port, this.IniFilePath);
            MessageBox.Show("UDP setting changed. Please program restart!", "EDWS DB Manager", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// 환경 설정창에서 발생하는 TCP 셋팅 값 변경 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void optionForm_OnTcpSettingEvt(object sender, TcpSettingEventArgs e)
        {
            AdengUtil.INIWriteValueString(this.setting, this.TCPIP, e.Ip, this.IniFilePath);
            AdengUtil.INIWriteValueString(this.setting, this.TCPPort, e.Port, this.IniFilePath);
            this.localTcpIp = e.Ip;
            this.localTcpPort = int.Parse(e.Port);
            
            //바뀐 설정값으로 재연결
            this.clientSoc.Disconnect();
            this.clientSoc.ConnectTo(this.localTcpIp, this.localTcpPort);
        }

        /// <summary>
        /// 환경 설정창에서 발생하는 TCP 통신 제어 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void optionForm_OnTcpSocketEvt(object sender, TcpSocketEventArgs e)
        {
            if (e.State) //통신연결
            {
                AdengUtil.INIWriteValueString(this.setting, this.TCPIP, e.Ip, this.IniFilePath);
                AdengUtil.INIWriteValueString(this.setting, this.TCPPort, e.Port, this.IniFilePath);
                this.localTcpIp = e.Ip;
                this.localTcpPort = int.Parse(e.Port);

                this.clientSoc.ConnectTo(this.localTcpIp, this.localTcpPort);
            }
            else //연결해제
            {
                this.clientSoc.Disconnect();

                this.tcpState = false;
                this.SetLogListBox(string.Format("TCP {0} / {1} Connect Close!", this.localTcpIp, this.localTcpPort));
                this.SetToolLBImage(this.ToolStripStatusLabel3, MewsDBManager.Properties.Resources.IconListRed);
                this.SetOptionTcpBtn(false);
            }
        }

        /// <summary>
        /// 환경 설정창에서 발생하는 DB 셋팅 값 변경 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void optionForm_OnDbSettingDataEvt(object sender, DbSettingDataEvtArgs e)
        {
            AdengUtil.INIWriteValueString(this.setting, this.DBIP, e.DbIp, this.IniFilePath);
            AdengUtil.INIWriteValueString(this.setting, this.DBSID, e.DbSid, this.IniFilePath);
            AdengUtil.INIWriteValueString(this.setting, this.DBID, e.DbId, this.IniFilePath);
            AdengUtil.INIWriteValueString(this.setting, this.DBPW, e.DbPw, this.IniFilePath);

            if (e.Oracle8)
            {
                AdengUtil.INIWriteValueString(this.setting, this.Oracle8i, "T", this.IniFilePath);
            }
            else
            {
                AdengUtil.INIWriteValueString(this.setting, this.Oracle8i, "F", this.IniFilePath);
            }
        }

        /// <summary>
        /// 큐 처리
        /// </summary>
        private void proc_runAsyncProcess()
        {
            while (this.proc.ContinueThread)
            {
                this.proc.ManuEvent.Reset();
                this.proc.ManuEvent.WaitOne(500);

                int remainCnt = 0;

                lock (this.insertQ)
                {
                    remainCnt = this.insertQ.Count;
                }

                if (remainCnt > 0)
                {
                    if (TryConnect() == false)
                    {
                        continue;
                    }
                }

                while (true)
                {
                    PrtBase tmp = null;

                    try
                    {
                        lock (this.insertQ)
                        {
                            if (this.insertQ.Count > 0)
                            {
                                tmp = this.insertQ.Dequeue();
                            }
                        }

                        if (tmp == null)
                        {
                            break;
                        }

                        this.SyncDataInsert(tmp);
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("proc_runAsyncProcess - " + ex.Message);
                        break;
                    }
                }

                this.dataMng.OracleClose();
            }
        }
        #endregion

        #region Method
        #region Data Insert
        /// <summary>
        /// Data Insert 처리
        /// </summary>
        /// <param name="_base"></param>
        private void SyncDataInsert(PrtBase _base)
        {
            PrtBase tmp = _base;

            try
            {
                switch (tmp.Cmd)
                {
                    case 0:
                        PrtCmd00 p0 = tmp as PrtCmd00;
                        this.dataMng.SetDB_TC0(p0);
                        this.procCount++;
                        break;
                    
                    case 1:
                        PrtCmd01 p1 = tmp as PrtCmd01;
                        this.dataMng.SetDB_TC1(p1);
                        this.procCount++;
                        break;
                    
                        /*
                    case 2:
                        PrtCmd02 p2 = tmp as PrtCmd02;
                        this.dataMng.SetDB_TC2(p2);
                        this.procCount++;
                        break;
                         */
                    
                    case 3:
                        PrtCmd03 p3 = tmp as PrtCmd03;
                        this.dataMng.SetDB_TC3(p3);
                        this.procCount++;
                        break;
                    
                        /*
                    case 4:
                        PrtCmd04 p4 = tmp as PrtCmd04;
                        this.dataMng.SetDB_TC4(p4);
                        this.procCount++;
                        break;
                         */
                    
                    case 5:
                        PrtCmd05 p5 = tmp as PrtCmd05;
                        this.dataMng.SetDB_TC5(p5);
                        this.procCount++;
                        break;
                    
                    case 6:
                        PrtCmd06 p6 = tmp as PrtCmd06;
                        this.dataMng.SetDB_TC6(p6);
                        this.procCount++;
                        break;
                    
                    case 7:
                        PrtCmd07 p7 = tmp as PrtCmd07;
                        this.dataMng.SetDB_TC7(p7);
                        this.procCount++;
                        break;
                    
                    case 8:
                        PrtCmd08 p8 = tmp as PrtCmd08;
                        this.dataMng.SetDB_TC8(p8);
                        this.procCount++;
                        break;
                    
                        /*
                    case 9:
                        PrtCmd09 p9 = tmp as PrtCmd09;
                        this.dataMng.SetDB_TC9(p9);
                        this.procCount++;
                        break;
                    
                    case 10:
                        PrtCmd10 p10 = tmp as PrtCmd10;
                        this.dataMng.SetDB_TC10(p10);
                        this.procCount++;
                        break;
                    
                    case 11:
                        PrtCmd11 p11 = tmp as PrtCmd11;
                        this.dataMng.SetDB_TC11(p11);
                        this.procCount++;
                        break;
                         */

                    case 12:
                        PrtCmd12 p12 = tmp as PrtCmd12;
                        this.dataMng.SetDB_TC12(p12);
                        this.procCount++;
                        break;

                        /*
                    case 13:
                        PrtCmd13 p13 = tmp as PrtCmd13;
                        this.dataMng.SetDB_TC13(p13);
                        this.procCount++;
                        break;

                    case 14:
                        PrtCmd14 p14 = tmp as PrtCmd14;
                        this.dataMng.SetDB_TC14(p14);
                        this.procCount++;
                        break;
                         */

                    case 15:
                        PrtCmd15 p15 = tmp as PrtCmd15;
                        this.dataMng.SetDB_TC15(p15);
                        this.procCount++;
                        break;

                        /*
                    case 16:
                        PrtCmd16 p16 = tmp as PrtCmd16;
                        this.dataMng.SetDB_TC16(p16);
                        this.procCount++;
                        break;

                    case 17:
                        PrtCmd17 p17 = tmp as PrtCmd17;
                        this.dataMng.SetDB_TC17(p17);
                        this.procCount++;
                        break;
                         */

                    case 21:
                        PrtCmd21 p21 = tmp as PrtCmd21;
                        this.dataMng.SetDB_TC21(p21);
                        this.procCount++;
                        break;
                    
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("SyncDataInsert - " + ex.Message);
                //this.SetLogListBox((object)ex.Message);
            }
        }
        #endregion

        /// <summary>
        /// 운영대에서 받은 시간정보를 OS에 셋팅한다.
        /// </summary>
        /// <param name="_dt"></param>
        private void setLocalTime(DateTime _dt)
        {
            MethodInvoker set = delegate()
            {
                SystemTime st = new SystemTime();
                bool rst = st.SetLocalTime(_dt);
                this.SetLogListBox(string.Format("Server time receive - {0}", (rst == true) ? "Success" : "Fail"));
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

        /// <summary>
        /// 옵션 TCP 통신 버튼을 활성/비활성 한다.
        /// </summary>
        /// <param name="_state"></param>
        private void SetOptionTcpBtn(bool _state)
        {
            MethodInvoker setBtn = delegate()
            {
                if (this.optionForm != null)
                {
                    if (_state)
                    {
                        this.optionForm.TcpConButton = false;
                        this.optionForm.TcpCloseButton = true;
                        this.optionForm.Tcp1LBColor = Color.Blue;
                        this.optionForm.Tcp1StsLB = string.Format("Result : {0}", "Connect");
                    }
                    else
                    {
                        this.optionForm.TcpConButton = true;
                        this.optionForm.TcpCloseButton = false;
                        this.optionForm.Tcp1LBColor = Color.Red;
                        this.optionForm.Tcp1StsLB = string.Format("Result  :  {0}", "Fail");
                    }
                }
            };

            if (this.InvokeRequired)
            {
                this.Invoke(setBtn);
            }
            else
            {
                setBtn();
            }
        }

        /// <summary>
        /// ToolStrip Image를 변경한다 (MethodInvoker 사용)
        /// </summary>
        /// <param name="_tsLB"></param>
        /// <param name="_img"></param>
        private void SetToolLBImage(ToolStripLabel _tsLB, Image _img)
        {
            MethodInvoker SetImage = delegate()
            {
                _tsLB.Image = _img;
            };

            if (this.InvokeRequired)
            {
                this.Invoke(SetImage);
            }
            else
            {
                SetImage();
            }
        }

        /// <summary>
        /// 윈도우 이벤트로그에 Information 이벤트를 기록한다.
        /// </summary>
        /// <param name="_msg"></param>
        private void SetEventLogInfo(string _msg)
        {
            EventLogMng.WriteLog("EDWS DB Manager", EventLogEntryType.Information, _msg);
        }

        /// <summary>
        /// 윈도우 이벤트로그에 Error 이벤트를 기록한다.
        /// </summary>
        /// <param name="_msg"></param>
        private void SetEventLogError(string _msg)
        {
            EventLogMng.WriteLog("EDWS DB Manager", EventLogEntryType.Error, _msg);
        }

        /// <summary>
        /// 메인 리스트박스에 로그를 기록한다.
        /// </summary>
        /// <param name="_obj"></param>
        private void SetLogListBox(object _obj)
        {
            MethodInvoker invoker = delegate()
            {
                if (this.MainLB.Items.Count > MainListBoxCount)
                {
                    this.MainLB.Items.Clear();
                }

                this.MainLB.Items.Insert(0, string.Format("{0} / {1}", _obj, DateTime.Now));
            };

            if (this.InvokeRequired)
            {
                this.Invoke(invoker);
            }
            else
            {
                invoker();
            }
        }

        /// <summary>
        /// 큐 데이터를 처리한 카운트를 라벨에 셋팅한다.
        /// </summary>
        private void SetDataProcCnt()
        {
            while (true)
            {
                CheckCount();
                Thread.Sleep(this.getQueueCntTime);
            }
        }

        /// <summary>
        /// 큐 데이터 카운트 처리 메소드
        /// </summary>
        private void CheckCount()
        {
            MethodInvoker SetTL = delegate()
            {
                if (this.procCount > uint.MaxValue - 100000)
                {
                    this.procCount = 0;
                }

                this.ToolStripStatusLabel1.Text = string.Format(" Process : {0}", this.procCount);
                this.ToolStripStatusLabel2.Text = string.Format(" Stand by : {0}", this.insertQ.Count);
            };

            if (this.InvokeRequired)
            {
                this.Invoke(SetTL);
            }
            else
            {
                SetTL();
            }
        }

        /// <summary>
        /// 재접속을 위한 메소드
        /// </summary>
        /// <returns></returns>
        private bool TryConnect()
        {
            try
            {
                string ip = AdengUtil.INIReadValueString(this.setting, this.DBIP, this.IniFilePath);
                string sid = AdengUtil.INIReadValueString(this.setting, this.DBSID, this.IniFilePath);
                string id = AdengUtil.INIReadValueString(this.setting, this.DBID, this.IniFilePath);
                string psw = AdengUtil.INIReadValueString(this.setting, this.DBPW, this.IniFilePath);
                bool support8 = (AdengUtil.INIReadValueString(this.setting, this.Oracle8i, this.IniFilePath) == "T") ? true : false;

                if (this.dataMng.OracleConnect(ip, sid, id, psw, support8) == false)
                {
                    throw new Exception(string.Format("{0}, {1}, {2}, {3}, {4}", ip, sid, id, psw, support8));
                }

                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("TryConnect - " + ex.Message);
            }

            return false;
        }
        #endregion

        #region 시험 메소드
        public void tempM()
        {
            PrtBase pb = new PrtBase();
            PrtCmd00 p01 = new PrtCmd00();
        }
        #endregion
    }
}