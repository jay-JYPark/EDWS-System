//#define test
#define real

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using Adeng.Framework.Util;
using Adeng.Framework.Log;
using Mews.Lib.MewsDataMng;
using Mews.Lib.Mewstcp;
using Mews.Lib.Mewsprt;
using Mews.Ctrl.Mewsmap;
using Mews.Ctrl.Mewsge;
using Mews.Lib.MesLog;
using Mews.Ctrl.MewsSettingSync;
using ADEng.Library.DMB_SD;
using Adeng.Framework.Io;

namespace Mews.Svr.Broad
{
    public partial class BroadMain : Form
    {
        #region instance
        private MDataMng initDataMng = null;
        private BroadOption optionForm = null;
        private TCPModClient clientSoc = null; //운영대 client
        private ClientMng clientSoc1 = null; //경보대 client
        private GroupMng grpMng = null; //그룹 Manager
        private StMsgDataMng msgMng = null; //저장메시지 Manager
        //private SirenDataMng sirenMng = null; //사이렌 Manager
        private BoardDataMng cgMng = null;
        private Timer timerNow = null;
        private BoardInfoDetail autoBoardCGInfo = new BoardInfoDetail();

        protected static int maxRemainBuffProv = 4096;
        protected static byte[] remainBuffProv = new byte[4096];
        protected static int remainCountProv;
        #endregion

        #region val
        //private const string version1 = " 1.0.0 (2013.06.24)"; //초기 버전
        //private const string version1 = " 1.1.0 (2013.10.30)"; //자동 방송일 때 CG 포함하는 기능 추가
        //private const string version1 = " 1.2.0 (2013.11.04)"; //MAP에 단말 정상/이상 정보 표시 기능 추가
        //private const string version1 = " 1.9.0 (2014.05.12)"; //EDWS 2차년도 사업 일부분 적용, primary/secondary에 따라 UI기본 컬러 변경 적용
        
#if test
        private const string version1 = " 1.9.1 (2014.06.23)_T"; //EDWS 2차년도 사업 일부분 적용, 기타 UI 및 데이터 송수신에 대한 프로세스 일부 적용
#endif

#if real
        private const string version1 = " 1.9.1 (2014.06.23)_R"; //EDWS 2차년도 사업 일부분 적용, 기타 UI 및 데이터 송수신에 대한 프로세스 일부 적용
#endif

        private readonly string IniFileDirectoryPath = "C:\\Program Files\\adeng\\MewsBroad";
        private readonly string IniFilePath = "C:\\Program Files\\adeng\\MewsBroad\\Setting.ini";
        private readonly string mapImagePath = Application.StartupPath + "\\Images\\layer.png";
        private readonly string setting = "SETTING";
        private readonly string DBIP = "DBIP";
        private readonly string DBSID = "DBSID";
        private readonly string DBID = "DBID";
        private readonly string DBPW = "DBPW";
        private readonly string Oracle8i = "Oracle8i";
        private readonly string TCPIP = "TCPIP";
        private readonly string TCPPort = "TCPPort";
        private readonly string TCPIP1 = "TCPIP1";
        private readonly string TCPPort1 = "TCPPort1";
        private readonly string LogFlag = "LogFLAG";
        private readonly string Language = "Language";
        private readonly string Primary = "Primary";

        private List<TermData> broadTermList = new List<TermData>(); //방송단말 리스트
        private List<TermStateInfo> termStateList = new List<TermStateInfo>(); //터미널 정상/이상에 대한 리스트
        private bool tcpState = false; //TCP 통신 상태 플래그, 운영대
        private string localTcpIp = string.Empty; //TCP 통신 IP, 운영대
        private int localTcpPort = int.MinValue; //TCP 통신 PORT, 운영대
        private bool tcpState1 = false; //TCP1 통신 상태 플래그, 경보대
        private string localTcpIp1 = string.Empty; //TCP1 통신 IP, 경보대
        private int localTcpPort1 = int.MinValue; //TCP1 통신 PORT, 경보대
        private bool logState = false; //Log 표시여부 플래그
        private int initState = 0; //기초데이터 로드 상태
        private bool exitFlag = false;
        private Int32 timeTick = 0;
        private string Lan = string.Empty;
        public string isPrimary = "2"; //1=Primary, 2=Secondary. 초기 값은 Secondary로 설정함.
        #endregion

        public OrderSelGrp orderSelGroup
        {
            get { return this.orderSelGrp; }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            //언어 셋팅 값을 가져온다.
            this.Lan = AdengUtil.INIReadValueString(this.setting, this.Language, IniFilePath);

            if (this.Lan == string.Empty)
            {
                this.Lan = "M";
            }

            if (AdengUtil.CheckAppOverlap("MewsBroad"))
            {
                MessageBox.Show(LangPack.GetMongolian("The program is already running."), "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.exitFlag = true;
                this.Close();
                return;
            }

            this.init();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (!this.exitFlag)
            {
                if (MessageBox.Show(LangPack.GetMongolian("Want to exit the program?"), this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    e.Cancel = true;
                    return;
                }
            }

            base.OnClosing(e);

            if (this.exitFlag)
            {
                return;
            }

            if (this.MainMap.MapMode == enMapMode.EditMode)
            {
                this.MainMap.SaveItemPoses();
            }

            this.MainMap.UnInitMewsMap();
            this.clientSoc.UnInitTCPModClient();
            this.clientSoc1.UnInitTCPIndClient();
            this.timerNow.Stop();

            this.clientSoc.evConnected -= new TCPModClient.ConnectedHandler(clientSoc_evConnected);
            this.clientSoc.evConnectFailed -= new TCPModClient.ConnectedFailedHandler(clientSoc_evConnectFailed);
            this.clientSoc.evDisconnected -= new TCPModClient.DisconnectedHandler(clientSoc_evDisconnected);
            this.clientSoc.evReceived -= new TCPModClient.ReceivedHandler(clientSoc_evReceived);
            this.clientSoc.evSendFailed -= new TCPModClient.SendFailedHandler(clientSoc_evSendFailed);
            this.clientSoc.evTimeout -= new TCPModClient.TimeOutHandler(clientSoc_evTimeout);

            this.clientSoc1.evConnected -= new TCPIndClient.ConnectedHandler(clientSoc1_evConnected);
            this.clientSoc1.evConnectFailed -= new TCPIndClient.ConnectedFailedHandler(clientSoc1_evConnectFailed);
            this.clientSoc1.evDisconnected -= new TCPIndClient.DisconnectedHandler(clientSoc1_evDisconnected);
            this.clientSoc1.evReceived -= new TCPIndClient.ReceivedHandler(clientSoc1_evReceived);
            this.clientSoc1.evSendFailed -= new TCPIndClient.SendFailedHandler(clientSoc1_evSendFailed);
            this.clientSoc1.evTimeout -= new TCPIndClient.TimeOutHandler(clientSoc1_evTimeout);

            this.orderSelMsg.evtMsgInfo -= new InvokeValueTwo<byte, byte>(orderSelMsg_evtMsgNum);
            this.orderSelSiren.evtSirenNum -= new InvokeValueOne<byte>(orderSelSiren_evtSirenNum);
            this.orderSelGrp.evtGroupInfo -= new InvokeValueOne<Group>(orderSelGrp_evtGroupInfo);
            this.orderSelGrp.evtIndInfo -= new InvokeValueTwo<List<string>, List<string>>(orderSelGrp_evtIndInfo);
            this.orderSelLive1.evtLiveType -= new InvokeValueOne<byte>(orderSelLive1_evtLiveType);

            //종료 로그
            if (this.logState)
            {
                MTextLog.WriteTextLog(true, this.Text + " END");
            }
        }

        public BroadMain()
        {
            InitializeComponent();
        }

        #region Method
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
                AdengUtil.INIWriteValueString(this.setting, this.DBIP, "127.0.0.1", IniFilePath);
                AdengUtil.INIWriteValueString(this.setting, this.DBSID, "orcl", IniFilePath);
                AdengUtil.INIWriteValueString(this.setting, this.DBID, "mews33", IniFilePath);
                AdengUtil.INIWriteValueString(this.setting, this.DBPW, "mews", IniFilePath);
                AdengUtil.INIWriteValueString(this.setting, this.Oracle8i, "F", IniFilePath);
                AdengUtil.INIWriteValueString(this.setting, this.TCPIP, "127.0.0.1", IniFilePath);
                AdengUtil.INIWriteValueString(this.setting, this.TCPPort, "9001", IniFilePath);
                AdengUtil.INIWriteValueString(this.setting, this.TCPIP1, "127.0.0.1", IniFilePath);
                AdengUtil.INIWriteValueString(this.setting, this.TCPPort1, "9002", IniFilePath);
                AdengUtil.INIWriteValueString(this.setting, this.LogFlag, "F", IniFilePath);
                AdengUtil.INIWriteValueString(this.setting, this.Language, "M", IniFilePath);
                AdengUtil.INIWriteValueString(this.setting, this.Primary, "2", IniFilePath);
            }

            //기초데이터 로드
            this.initDataMng = MDataMng.GetInstance();

            //언어 설정
            if (this.Lan == "E")
            {
                LangPack.IsEng = true;
                this.englishEToolStripMenuItem.Enabled = false;
                this.mongolianMToolStripMenuItem.Enabled = true;
            }
            else if (this.Lan == "M")
            {
                LangPack.IsEng = false;
                this.englishEToolStripMenuItem.Enabled = true;
                this.mongolianMToolStripMenuItem.Enabled = false;
            }

            //1 - DB에서 로드
            //2 - XML에서 로드
            //3 - 로드 실패
            this.initState = this.initDataMng.Init(AdengUtil.INIReadValueString(this.setting, this.DBID, IniFilePath), AdengUtil.INIReadValueString(this.setting, this.DBPW, IniFilePath),
                AdengUtil.INIReadValueString(this.setting, this.DBIP, IniFilePath), AdengUtil.INIReadValueString(this.setting, this.DBSID, IniFilePath));

            if (this.initState == 2)
            {
                MessageBox.Show(LangPack.GetMongolian("The basic configuration has been loaded from the local storage.\nAfter checking the DB connection settings, please restart."), LangPack.GetMongolian("Data Load"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (this.initState == 3)
            {
                MessageBox.Show(LangPack.GetMongolian("The basic configuration could not be loaded.\nAfter checking the DB connection settings, please restart."), LangPack.GetMongolian("Data Load"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //자동방송에 대한 CG 관련
            if (!File.Exists(Util.file_Auto_CG_Data))
            {
                Util.autoCGInfo.highText = "highText";
                Util.autoCGInfo.lowText = "lowText";
                Util.autoCGInfo.highKindNum = 100;
                Util.autoCGInfo.lowKindNum = 100;
                Util.SaveAutoCGinfo();
            }

            Util.LoadOptioninfo(); //buzzer
            Util.LoadAutoinfo(); //auto alert
            Util.LoadAutoCGinfo(); //자동방송 CG

            //맵에 셋팅할 단말 정보 init
            if (this.initState != 3)
            {
                foreach (KeyValuePair<string, TermData> term in this.initDataMng.DicOnlyBroadOrderTerm)
                {
                    this.broadTermList.Add(term.Value);

                    TermStateInfo tmp = new TermStateInfo();
                    tmp.TermIP = term.Value.TermIp;
                    tmp.TermState = false;
                    this.termStateList.Add(tmp);
                }
            }

            //맵 모듈 셋팅
            this.MainMap.SetMapImageFile(this.mapImagePath);
            this.MainMap.FWidth = (float)this.MainMap.Width;
            this.MainMap.FHeight = (float)this.MainMap.Height;
            this.MainMap.InitMewsMap(this.broadTermList);
            this.MainMap.SaveItemPoses();
            this.MainMap.MapMode = enMapMode.RunningMode;
            this.normalNToolStripMenuItem.Enabled = false;
            this.normalModeNToolStripMenuItem.Enabled = false;

            //방송 조작반 Panel 초기화
            this.orderMode2.InitCtrl(this);
            this.orderMode2.OnSatDataRecvEvt += new EventHandler<VhfSatDataEventArgs>(orderMode2_OnSatDataRecvEvt);
            this.orderMode2.OnVhfDataResvEvt += new EventHandler<VhfSatDataEventArgs>(orderMode2_OnVhfDataResvEvt);
            this.orderMode2.OnControlPNSendEvt += new EventHandler<VhfSatDataEventArgs>(orderMode2_OnControlPNSendEvt);
            this.orderMode2.OnSatAudioControlEvt += new EventHandler<SatAudioControlEventArgs>(orderMode2_OnSatAudioControlEvt);

            //그룹 Manager 초기화
            this.grpMng = GroupMng.GetGrpMng();
            this.grpMng.Init(this.initDataMng);

            //저장메시지 Manager 초기화
            this.msgMng = StMsgDataMng.GetMsgMng();
            this.msgMng.LoadMsgNameData();

            //사이렌 Manager 초기화
            //this.sirenMng = SirenDataMng.GetSirenMng();
            //this.sirenMng.LoadSirenData();

            //CG 초기화
            this.cgMng = BoardDataMng.GetBrdMng();
            this.cgMng.LoadBrdData();

            //프로그래스바 초기화
            this.HideOrderProgress();

            //팝업's init
            this.orderSelMsg.Location = new Point(0, 100);
            this.orderSelMsg.Hide();
            this.orderSelMsg.Init();

            this.orderSelSiren.Location = new Point(0, 50);
            this.orderSelSiren.Hide();
            this.orderSelSiren.Init();

            this.orderSelGrp.Location = new Point(0, 100);
            this.orderSelGrp.Hide();
            this.orderSelGrp.Init(this);

            this.orderSelLive1.Location = new Point(0, 365);
            this.orderSelLive1.Hide();
            this.orderSelLive1.evtLiveType += new InvokeValueOne<byte>(orderSelLive1_evtLiveType);

            this.orderSelLiveSiren.Location = new Point(0, 365);
            this.orderSelLiveSiren.Hide();

            this.orderSelMsg.evtMsgInfo += new InvokeValueTwo<byte, byte>(orderSelMsg_evtMsgNum);
            this.orderSelSiren.evtSirenNum += new InvokeValueOne<byte>(orderSelSiren_evtSirenNum);
            this.orderSelGrp.evtGroupInfo += new InvokeValueOne<Group>(orderSelGrp_evtGroupInfo);
            this.orderSelGrp.evtIndInfo += new InvokeValueTwo<List<string>, List<string>>(orderSelGrp_evtIndInfo);

            //운영대 tcp client socket 생성
            this.clientSoc = TCPModClient.getTCPModClient();
            this.clientSoc.evConnected += new TCPModClient.ConnectedHandler(clientSoc_evConnected);
            this.clientSoc.evConnectFailed += new TCPModClient.ConnectedFailedHandler(clientSoc_evConnectFailed);
            this.clientSoc.evDisconnected += new TCPModClient.DisconnectedHandler(clientSoc_evDisconnected);
            this.clientSoc.evReceived += new TCPModClient.ReceivedHandler(clientSoc_evReceived);
            this.clientSoc.evSendFailed += new TCPModClient.SendFailedHandler(clientSoc_evSendFailed);
            this.clientSoc.evTimeout += new TCPModClient.TimeOutHandler(clientSoc_evTimeout);

            //경보대 tcp client socket 생성 -> PLC로 변경, 경보대는 후에 추가
            this.clientSoc1 = new ClientMng();
            this.clientSoc1.InitTCPIndClient();
            this.clientSoc1.evConnected += new TCPIndClient.ConnectedHandler(clientSoc1_evConnected);
            this.clientSoc1.evConnectFailed += new TCPIndClient.ConnectedFailedHandler(clientSoc1_evConnectFailed);
            this.clientSoc1.evDisconnected += new TCPIndClient.DisconnectedHandler(clientSoc1_evDisconnected);
            this.clientSoc1.evReceived += new TCPIndClient.ReceivedHandler(clientSoc1_evReceived);
            this.clientSoc1.evSendFailed += new TCPIndClient.SendFailedHandler(clientSoc1_evSendFailed);
            this.clientSoc1.evTimeout += new TCPIndClient.TimeOutHandler(clientSoc1_evTimeout);

            try
            {
                //로그 셋팅 값을 가져온다.
                this.logState = (AdengUtil.INIReadValueString(this.setting, this.LogFlag, IniFilePath) == "T" ? true : false);

                //운영대 TCP 연결
                this.localTcpIp = AdengUtil.INIReadValueString(this.setting, this.TCPIP, this.IniFilePath);
                this.localTcpPort = int.Parse(AdengUtil.INIReadValueString(this.setting, this.TCPPort, this.IniFilePath));
                this.clientSoc.ConnectTo(this.localTcpIp, this.localTcpPort);

                //경보대 TCP 연결
                this.localTcpIp1 = AdengUtil.INIReadValueString(this.setting, this.TCPIP1, this.IniFilePath);
                this.localTcpPort1 = int.Parse(AdengUtil.INIReadValueString(this.setting, this.TCPPort1, this.IniFilePath));
                this.clientSoc1.ConnectTo(this.localTcpIp1, this.localTcpPort1);

                //Primary, Secondary 설정
                this.isPrimary = AdengUtil.INIReadValueString(this.setting, this.Primary, this.IniFilePath);
                this.isPrimarySetting(byte.Parse(this.isPrimary));
            }
            catch (Exception ex)
            {
                Debug.WriteLine("BroadMain.init - " + ex.Message);

                if (this.logState)
                {
                    MTextLog.WriteTextLog(true, "BroadMain.init - " + ex.Message);
                }
            }

            //폴링 데이터 처리
            PrtBase p9 = PrtMng.GetPrtObject(9);
            p9.MakeData();
            byte[] buff = PrtMng.MakeFrame(p9);
            this.clientSoc.BtPoll = buff; //운영대 폴링 데이터
            //this.clientSoc1.BtPoll = buff; //경보대 폴링 데이터 -> PLC로 변경했으므로 폴링 데이터 제거
            this.clientSoc1.IsUsePolling = false;

            //프로그램 제목 최신 버전 표시, 하단의 시간 표시
            this.Text += version1;
            this.ToolStripStatusLabel3.Text = "[ " + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + " ]";

            this.timerNow = new Timer();
            this.timerNow.Interval = 200;
            this.timerNow.Tick += new EventHandler(timerNow_Tick);
            this.timerNow.Start();

            //풀스크린으로 로그
            this.WindowState = FormWindowState.Maximized;

            //시작 로그
            if (this.logState)
            {
                MTextLog.WriteTextLog(true, this.Text + " START");
            }

            this.initLang();

            //CC에서 사용하는 설정데이터 동기화 수락 이벤트
            this.mewsSettingSyncUC.OnDataSyncApplyEvtToCC += new EventHandler<DataSyncEventArgs>(mewsSettingSyncUC_OnDataSyncApplyEvtToCC);

            #region test 2014/04/11
            ////프로토콜 29 test
            //PrtBase pb = PrtMng.GetPrtObject(29);
            //PrtCmd29 p29 = pb as PrtCmd29;
            //p29.IsPrimary = 2; //p=1, s=2
            //byte[] tmpBuff = p29.MakeData();
            //tmpBuff = PrtMng.MakeFrame(pb);

            //PrtBase p = PrtMng.ParseFrame(tmpBuff);
            //PrtCmd29 recvP29 = p as PrtCmd29;
            //System.Diagnostics.Debug.WriteLine(recvP29.Cmd + " - " + recvP29.IsPrimary);


            ////프로토콜 30 test
            //pb = PrtMng.GetPrtObject(30);
            //PrtCmd30 p30 = pb as PrtCmd30;
            //p30.CompleteState = 2; //cc=1, mcc=2
            //tmpBuff = p30.MakeData();
            //tmpBuff = PrtMng.MakeFrame(pb);

            //p = PrtMng.ParseFrame(tmpBuff);
            //PrtCmd30 recvP30 = p as PrtCmd30;
            //System.Diagnostics.Debug.WriteLine(recvP30.Cmd + " - " + recvP30.CompleteState);


            ////프로토콜 31-1 test
            //pb = PrtMng.GetPrtObject(31);
            //PrtCmd31 p31 = pb as PrtCmd31;
            //p31.Kind = 1;
            //p31.FileName = "fileNameXML.xml";
            //p31.FileLength = (byte)p31.FileName.Length;
            //p31.FileData = Encoding.Default.GetBytes("내용이 뿌려지면 되는거다~몇자인던 잘 나와야지~이것저것~랄랄라~테스트용이지만 확실하게~끝이라고 적기전까지가 내용이다~자 1024로 잡아 놓은걸 테스트해보자~끝!");
            //tmpBuff = p31.MakeData();
            //tmpBuff = PrtMng.MakeFrame(pb);

            //p = PrtMng.ParseFrame(tmpBuff);
            //PrtCmd31 recvP31 = p as PrtCmd31;
            //System.Diagnostics.Debug.WriteLine(recvP31.Cmd + " - " + recvP31.Kind + " - " + recvP31.FileName + " - " + recvP31.FileLength.ToString() + " - " + Encoding.Default.GetString(recvP31.FileData));


            ////프로토콜 31-2 test
            //pb = PrtMng.GetPrtObject(31);
            //p31 = pb as PrtCmd31;
            //p31.Kind = 2;
            //p31.FileName = "fileNameXML.xml";
            //tmpBuff = p31.MakeData();
            //tmpBuff = PrtMng.MakeFrame(pb);

            //p = PrtMng.ParseFrame(tmpBuff);
            //recvP31 = p as PrtCmd31;
            //System.Diagnostics.Debug.WriteLine(recvP31.Cmd + " - " + recvP31.Kind + " - " + recvP31.FileName + " - " + recvP31.FileLength);


            ////프로토콜 31-3 test
            //pb = PrtMng.GetPrtObject(31);
            //p31 = pb as PrtCmd31;
            //p31.Kind = 3;
            //p31.FileName = "fileNameXML.xml";
            //tmpBuff = p31.MakeData();
            //tmpBuff = PrtMng.MakeFrame(pb);

            //p = PrtMng.ParseFrame(tmpBuff);
            //recvP31 = p as PrtCmd31;
            //System.Diagnostics.Debug.WriteLine(recvP31.Cmd + " - " + recvP31.Kind + " - " + recvP31.FileName + " - " + recvP31.FileLength);


            ////프로토콜 31-4 test
            //pb = PrtMng.GetPrtObject(31);
            //p31 = pb as PrtCmd31;
            //p31.Kind = 4;
            //p31.FileName = "fileNameXML.xml";
            //tmpBuff = p31.MakeData();
            //tmpBuff = PrtMng.MakeFrame(pb);

            //p = PrtMng.ParseFrame(tmpBuff);
            //recvP31 = p as PrtCmd31;
            //System.Diagnostics.Debug.WriteLine(recvP31.Cmd + " - " + recvP31.Kind + " - " + recvP31.FileName + " - " + recvP31.FileLength);
            #endregion
        }

        /// <summary>
        /// CC에서 사용하는 설정데이터 동기화 수락 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mewsSettingSyncUC_OnDataSyncApplyEvtToCC(object sender, DataSyncEventArgs e)
        {
            try
            {
                PrtBase pb = PrtMng.GetPrtObject(31);
                PrtCmd31 p31 = pb as PrtCmd31;

                p31.Kind = 1; //CC방송대는 변경 완료
                p31.FileLength = (byte)e.FileName.Length;
                p31.FileName = e.FileName;

                //보낼 xml파일 스트림 만들기
                StringWriter sw = new StringWriter();
                AdengXmlNode node = new AdengXmlNode();
                node.SaveXmlDocument(sw);
                node.SaveXmlDocument(System.Windows.Forms.Application.StartupPath + "\\xmldata\\" + e.FileName);
                byte[] xmlDataBuff = Encoding.Default.GetBytes(sw.ToString());
                p31.FileData = xmlDataBuff;
                pb.MakeData();
                byte[] buff = PrtMng.MakeFrame(pb);
                this.clientSoc.Send(buff);
            }
            catch (Exception ex)
            {
                MessageBox.Show(LangPack.GetMongolian("mewsSettingSyncUC_OnDataSyncApplyEvtToCC - " + ex.Message), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 언어 설정 메소드
        /// </summary>
        private void initLang()
        {
            if (LangPack.IsEng)
            {
                MainTopLB.Size = new Size(423, 65);
            }
            else
            {
                MainTopLB.Size = new Size(660, 65);
            }

            MainTopLB.Text = LangPack.GetProgName();
            this.Text = LangPack.GetProgName() + version1;
            this.fileFToolStripMenuItem.Text = LangPack.GetMenuFile();
            this.exitXToolStripMenuItem.Text = LangPack.GetMenuExit();
            this.editEToolStripMenuItem.Text = LangPack.GetMenuEdit();
            this.mapIconToolStripMenuItem.Text = LangPack.GetMenuMapIcon();
            this.normalNToolStripMenuItem.Text = LangPack.GetMenuOperMode();
            this.modifyDToolStripMenuItem.Text = LangPack.GetMenuEditMode();
            this.groupGToolStripMenuItem.Text = LangPack.GetMenuGrouping();
            this.storedMessageToolStripMenuItem.Text = LangPack.GetMenuStrMng();
            this.cGCToolStripMenuItem.Text = LangPack.GetMenuCG();
            this.viewVToolStripMenuItem.Text = LangPack.GetMenuSetting();
            this.autoAlertAToolStripMenuItem.Text = LangPack.GetMenuAutoAlert();
            this.optionOToolStripMenuItem.Text = LangPack.GetMenuOption();
            this.helpHToolStripMenuItem.Text = LangPack.GetMenuAbout();
            this.infomationIToolStripMenuItem.Text = LangPack.GetMenuInfo();
            this.ToolStripStatusLabel1.Text = LangPack.GetOperationMng();
            this.ToolStripStatusLabel2.Text = LangPack.GetControlPanel();
            this.normalModeNToolStripMenuItem.Text = LangPack.GetMenuOperMode();
            this.modifyModeDToolStripMenuItem.Text = LangPack.GetMenuEditMode();
            this.lbOnAir.Text = LangPack.GetOnAir();
            this.languageToolStripMenuItem.Text = LangPack.GetLanguage();
            this.englishEToolStripMenuItem.Text = LangPack.GetEnglish();
            this.mongolianMToolStripMenuItem.Text = LangPack.GetMongolian();
            this.orderMode2.InitLang();
        }

        /// <summary>
        /// 자동방송 팝업 종료 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void autoForm_OnCloseEvt(object sender, CloseEventArgs e)
        {
            
        }

        /// <summary>
        /// 방송 종류 선택 시 발생하는 이벤트
        /// </summary>
        /// <param name="valueT"></param>
        private void orderSelLive1_evtLiveType(byte liveType)
        {
            if (this.InvokeRequired)
                this.Invoke(new InvokeValueOne<byte>(SelLiveInfo));
            else
                SelLiveInfo(liveType);
        }

        private void SelLiveInfo(byte liveType)
        {
            orderMode2.GetLiveInfo(liveType);
        }

        public void selectLiveMic()
        {
            this.orderSelLive1.btnMic_Click(null, new EventArgs());
        }

        public void selectLiveRecord()
        {
            this.orderSelLive1.btnRec_Click(null, new EventArgs());
        }

        /// <summary>
        /// 방송 종류 팝업 표출 메소드
        /// </summary>
        public void ViewPopLivePnl()
        {
            this.orderSelLive1.Show();
        }

        /// <summary>
        /// 방송 종류 팝업 숨김 메소드
        /// </summary>
        public void HidePopLivePnl()
        {
            this.orderSelLive1.Hide();
        }

        /// <summary>
        /// 방송 중 사이렌 팝업 표출 메소드
        /// </summary>
        public void ViewPopLiveSirenPnl()
        {
            this.orderSelLiveSiren.setBtnColor(this.orderMode2.CurMode);
            this.orderSelLiveSiren.Show();
        }

        /// <summary>
        /// 방송 중 사이렌 팝업 숨김 이벤트
        /// </summary>
        public void HidePopLiveSirenPnl()
        {
            this.orderSelLiveSiren.init();
            this.orderSelLiveSiren.Hide();
        }

        /// <summary>
        /// ON-AIR 텍스트를 표출하는 메소드
        /// </summary>
        /// <param name="bReady"></param>
        public void ShowOnAir(bool bReady)
        {
            if (bReady)
            {
                lbOnAir.BackColor = Color.Yellow;
                lbOnAir.ForeColor = Color.Red;
                this.orderMode2.ShowOnAirCtrl(1);
            }
            else
            {
                lbOnAir.BackColor = Color.CornflowerBlue;
                lbOnAir.ForeColor = Color.DarkSlateBlue;
                this.orderMode2.ShowOnAirCtrl(0);
            }

            lbOnAir.Visible = true;
        }

        /// <summary>
        /// ON-AIR 텍스트에 이상 데이터 수신받음을 표시한다.
        /// </summary>
        public void FalseOnAir()
        {
            lbOnAir.BackColor = Color.Thistle;
            lbOnAir.ForeColor = Color.CornflowerBlue;
        }

        /// <summary>
        /// 기초데이터 인스턴스 리턴 메소드
        /// </summary>
        /// <returns></returns>
        public MDataMng GetDbMng()
        {
            return initDataMng;
        }

        /// <summary>
        /// 그룹관리 인스턴스 리턴 메소드
        /// </summary>
        /// <returns></returns>
        public GroupMng GetGrpMng()
        {
            return grpMng;
        }

        /// <summary>
        /// 옵션의 운영대 TCP 통신 버튼을 활성/비활성 한다.
        /// </summary>
        /// <param name="_state"></param>
        private void SetOptionTcp1Btn(bool _state)
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
                        this.optionForm.Tcp1StsLB = string.Format(LangPack.GetTCPResult() + " : {0}", LangPack.GetTCPConnect());
                    }
                    else
                    {
                        this.optionForm.TcpConButton = true;
                        this.optionForm.TcpCloseButton = false;
                        this.optionForm.Tcp1LBColor = Color.Red;
                        this.optionForm.Tcp1StsLB = string.Format(LangPack.GetTCPResult() + " : {0}", LangPack.GetTCPFail());
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
        /// 옵션의 경보대 TCP 통신 버튼을 활성/비활성 한다.
        /// </summary>
        /// <param name="_state"></param>
        private void SetOptionTcp2Btn(bool _state)
        {
            MethodInvoker setBtn = delegate()
            {
                if (this.optionForm != null)
                {
                    if (_state)
                    {
                        this.optionForm.TcpConButton1 = false;
                        this.optionForm.TcpCloseButton1 = true;
                        this.optionForm.Tcp2LBColor = Color.Blue;
                        this.optionForm.Tcp2StsLB = string.Format(LangPack.GetTCPResult() + " : {0}", LangPack.GetTCPConnect());
                    }
                    else
                    {
                        this.optionForm.TcpConButton1 = true;
                        this.optionForm.TcpCloseButton1 = false;
                        this.optionForm.Tcp2LBColor = Color.Red;
                        this.optionForm.Tcp2StsLB = string.Format(LangPack.GetTCPResult() + " : {0}", LangPack.GetTCPFail());
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
            EventLogMng.WriteLog("EDWS BroadSystem", EventLogEntryType.Information, _msg);
        }

        /// <summary>
        /// 윈도우 이벤트로그에 Error 이벤트를 기록한다.
        /// </summary>
        /// <param name="_msg"></param>
        private void SetEventLogError(string _msg)
        {
            EventLogMng.WriteLog("EDWS BroadSystem", EventLogEntryType.Error, _msg);
        }

        /// <summary>
        /// 조작반 panel에서 그룹/개별 클릭 이벤트
        /// </summary>
        /// <param name="bGroup"></param>
        public void ViewPopGrpPnl(bool bGroup)
        {
            //bool 인자로 그룹 또는 시군/개별단말이 나온다.
            this.orderSelGrp.SetGroupList(bGroup);
            this.orderSelGrp.Show();

            if (bGroup && (this.orderMode2.CurGrp != null))
            {
                this.orderSelGrp.setSelGroup(this.orderMode2.CurGrp);
            }

            if (!bGroup && (this.orderMode2.CurDistLstIP != null || this.orderMode2.CurTermLstIP != null))
            {
                this.orderSelGrp.setSelInd(this.orderMode2.CurDistLstIP, this.orderMode2.CurTermLstIP);
            }
        }

        public void selectGrp()
        {
            this.orderSelGrp.btnEnd_Click(null, new EventArgs());
        }

        /// <summary>
        /// 그룹 또는 시군/개별단말 유저컨트롤 숨기기
        /// </summary>
        public void HidePopGrpPnl()
        {
            this.orderSelGrp.Hide();
        }

        /// <summary>
        /// 저장메시지 유저컨트롤 보이기
        /// </summary>
        /// <param name="curMode"></param>
        public void ViewPopMsgPnl(byte curMode)
        {
            this.orderSelMsg.SetModeInfo(curMode);
            this.orderSelMsg.Show();
        }

        /// <summary>
        /// 저장메시지 유저컨트롤 숨기기
        /// </summary>
        public void HidePopMsgPnl()
        {
            this.orderSelMsg.Hide();
        }

        /// <summary>
        /// 사이렌 유저컨트롤 보이기
        /// </summary>
        public void ViewPopSirenPnl()
        {
            this.orderSelSiren.SetButton();
            this.orderSelSiren.Show();
        }

        /// <summary>
        /// 사이렌 유저컨트롤 숨기기
        /// </summary>
        public void HidePopSirenPnl()
        {
            this.orderSelSiren.Hide();
        }

        /// <summary>
        /// 저장메시지 유저컨트롤에서 선택 시 발생하는 이벤트 invoke 메소드
        /// </summary>
        /// <param name="msgNum"></param>
        private void SendMsgInfo(byte msgNum, byte reptCnt)
        {
            this.orderMode2.GetMsgNumInfo(msgNum, reptCnt);
            HidePopMsgPnl();
        }

        /// <summary>
        /// 사이렌 유저컨트롤에서 선택 시 발생하는 이벤트 invoke 메소드
        /// </summary>
        /// <param name="SirenNum"></param>
        private void SendSirenInfo(byte SirenNum)
        {
            this.orderMode2.GetSirenNumInfo(SirenNum);
            HidePopSirenPnl();
        }

        /// <summary>
        /// 그룹 유저컨트롤에서 체크박스 선택 시 발생하는 이벤트 invoke 메소드
        /// </summary>
        /// <param name="grp"></param>
        private void SendGrpInfo(Group grp)
        {
            this.orderMode2.GetGrpInfo(grp);
            HidePopGrpPnl();
        }

        /// <summary>
        /// 시군/개별 유저컨트롤에서 선택 시 발생하는 이벤트 invoke 메소드
        /// </summary>
        /// <param name="lstIP"></param>
        private void SendIndInfo(List<string> lstDistIP, List<string> lstTermIP)
        {
            this.orderMode2.GetIndInfo(lstDistIP, lstTermIP);
            HidePopGrpPnl();
        }

        /// <summary>
        /// ON-AIR 텍스트를 숨긴다.
        /// </summary>
        public void HIdeOnAir()
        {
            lbOnAir.Visible = false;
            this.orderMode2.ShowOnAirCtrl(0);
        }

        /// <summary>
        /// 프로그래스바 보이기
        /// </summary>
        /// <param name="info"></param>
        public void ShowOrderProgress(object info)
        {
            this.orderProgress.OrderStart(info);
            this.orderProgress.Visible = true;
        }

        /// <summary>
        /// 프로그래스바 숨기기
        /// </summary>
        public void HideOrderProgress()
        {
            this.orderProgress.OrderEnd();
            this.orderProgress.Visible = false;
        }

        /// <summary>
        /// 맵에 해당 지역 및 해당 단말을 표출하기 위한 메소드
        /// </summary>
        /// <param name="mwInfo"></param>
        public void SetMapItem(MapWarnInfo mwInfo)
        {
            this.MainMap.SetMapWarnInfo(mwInfo);
        }

        /// <summary>
        /// 맵에 터미널 이상/정상 정보를 표출하기 위한 메소드
        /// </summary>
        public void SetMapTermState()
        {
            for (int i = 0; i < this.termStateList.Count; i++)
            {
                if (this.termStateList[i].TermState == true)
                {
                    this.MainMap.SetMapTermStatusInfo(this.termStateList[i].TermIP, this.termStateList[i].TermState);
                }
            }
        }
        #endregion

        #region event
        /// <summary>
        /// 타이머 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerNow_Tick(object sender, EventArgs e)
        {
            try
            {
                //if (timeTick + 1000 < System.Environment.TickCount)
                //{
                //    ToolStripStatusLabel3.Text = "[ " + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + " ]";
                //    timeTick = System.Environment.TickCount;
                //}
                if (timeTick > 4) //timer 200을 바꾸면 이것도 바꿔야 됨.
                {
                    ToolStripStatusLabel3.Text = "[ " + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + " ]";
                    timeTick = 0;
                }

                timeTick++;
                this.MainMap.Invalidate();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("BroadMain.timerNow_Tick - " + ex.Message);

                if (this.logState)
                {
                    MTextLog.WriteTextLog(true, "BroadMain.timerNow_Tick - " + ex.Message);
                }
            }
        }

        /// <summary>
        /// 조작반 유저컨트롤에서 오는 VHF 데이터
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void orderMode2_OnVhfDataResvEvt(object sender, VhfSatDataEventArgs e)
        {
            MethodInvoker SetInvoker = delegate()
            {
                this.clientSoc.Send(e.Buff);
            };

            if (this.orderMode2.InvokeRequired)
            {
                this.Invoke(SetInvoker);
            }
            else
            {
                SetInvoker();
            }
        }

        /// <summary>
        /// 조작반 유저컨트롤에서 오는 SAT 데이터 (처음에는 SAT로 직접 보내는 프로세스였지만 운영대로 보내는 걸로 바뀜)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void orderMode2_OnSatDataRecvEvt(object sender, VhfSatDataEventArgs e)
        {
            MethodInvoker SetInvoker = delegate()
            {
                this.clientSoc.Send(e.Buff);
            };

            if (this.orderMode2.InvokeRequired)
            {
                this.Invoke(SetInvoker);
            }
            else
            {
                SetInvoker();
            }
        }

        /// <summary>
        /// 위성음성 채널 사용에 대한 이벤트 (운영대로 보내는 프로토콜)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void orderMode2_OnSatAudioControlEvt(object sender, SatAudioControlEventArgs e)
        {
            MethodInvoker SetInvoker = delegate()
            {
                PrtBase pb = PrtMng.GetPrtObject(27);
                PrtCmd27 p27 = pb as PrtCmd27;
                p27.ChaCtrl = e.IsOpen;
                byte[] cbsBuff = p27.MakeData();
                cbsBuff = PrtMng.MakeFrame(pb);
                
                this.clientSoc.Send(cbsBuff);
            };

            if (this.orderMode2.InvokeRequired)
            {
                this.Invoke(SetInvoker);
            }
            else
            {
                SetInvoker();
            }
        }

        /// <summary>
        /// UI 조작반에서 조작한 패킷을 PLC로 보내는 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void orderMode2_OnControlPNSendEvt(object sender, VhfSatDataEventArgs e)
        {
            MethodInvoker SetInvoker = delegate()
            {
                this.clientSoc1.Send(e.Buff);
            };

            if (this.InvokeRequired)
            {
                this.Invoke(SetInvoker);
            }
            else
            {
                SetInvoker();
            }
        }

        /// <summary>
        /// 시군/개별 유저컨트롤에서 선택 시 발생하는 이벤트
        /// </summary>
        /// <param name="valueT"></param>
        /// <param name="valueU"></param>
        private void orderSelGrp_evtIndInfo(List<string> lstDistIP, List<string> lstTermIp)
        {
            if (this.InvokeRequired)
                this.Invoke(new InvokeValueTwo<List<string>, List<string>>(SendIndInfo));
            else
                SendIndInfo(lstDistIP, lstTermIp);
        }

        /// <summary>
        /// 그룹 유저컨트롤에서 체크박스 선택 시 발생하는 이벤트
        /// </summary>
        /// <param name="valueT"></param>
        private void orderSelGrp_evtGroupInfo(Group grp)
        {
            if (this.InvokeRequired)
                this.Invoke(new InvokeValueOne<Group>(SendGrpInfo));
            else
                SendGrpInfo(grp);
        }

        /// <summary>
        /// 사이렌 유저컨트롤에서 선택 시 발생하는 이벤트
        /// </summary>
        /// <param name="valueT"></param>
        private void orderSelSiren_evtSirenNum(byte sirenNum)
        {
            if (this.InvokeRequired)
                this.Invoke(new InvokeValueOne<byte>(SendSirenInfo));
            else
                SendSirenInfo(sirenNum);
        }

        /// <summary>
        /// 저장메시지 유저컨트롤에서 선택 시 발생하는 이벤트
        /// </summary>
        /// <param name="valueT"></param>
        private void orderSelMsg_evtMsgNum(byte msgNum, byte reptCnt)
        {
            if (this.InvokeRequired)
                this.Invoke(new InvokeValueTwo<byte, byte>(SendMsgInfo));
            else
                SendMsgInfo(msgNum, reptCnt);
        }

        /// <summary>
        /// 메뉴의 지도 아이콘 운영모드 클릭 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void normalNToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.MainMap.IsLangEng = LangPack.IsEng;
            this.MainMap.MapMode = enMapMode.RunningMode;
            this.MainMap.SaveItemPoses();
            this.modifyDToolStripMenuItem.Enabled = true;
            this.normalNToolStripMenuItem.Enabled = false;
            this.modifyModeDToolStripMenuItem.Enabled = true;
            this.normalModeNToolStripMenuItem.Enabled = false;
        }

        /// <summary>
        /// 메뉴의 지도 아이콘 변경모드 클릭 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void modifyDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.MainMap.IsLangEng = LangPack.IsEng;
            this.MainMap.MapMode = enMapMode.EditMode;
            this.normalNToolStripMenuItem.Enabled = true;
            this.modifyDToolStripMenuItem.Enabled = false;
            this.normalModeNToolStripMenuItem.Enabled = true;
            this.modifyModeDToolStripMenuItem.Enabled = false;
        }

        /// <summary>
        /// 경보대 TCP 서버에서 발생시킨 Timeout 이벤트
        /// </summary>
        private void clientSoc1_evTimeout()
        {
            this.tcpState1 = false;
            this.SetToolLBImage(this.ToolStripStatusLabel2, MewsBroad.Properties.Resources.IconListRed);
            this.SetOptionTcp2Btn(false);
        }

        /// <summary>
        /// 경보대 TCP 서버로 데이터 전송 실패 이벤트
        /// </summary>
        /// <param name="exc"></param>
        private void clientSoc1_evSendFailed(Exception exc)
        {
            //로그를 남기는 부분이 있다면 사용..
        }

        /// <summary>
        /// 경보대 서버 소켓에서 전송한 데이터 이벤트
        /// </summary>
        /// <param name="buff"></param>
        private void clientSoc1_evReceived(byte[] buff)
        {
            MethodInvoker setBtn = delegate()
            {
                int size = buff.Length;

                try
                {
                    byte[] totBuff = null;
                    int packetLen = 0, totRemainLen = 0;

                    //lock (lockFlag)
                    {
                        totBuff = new byte[maxRemainBuffProv];
                        totBuff.Initialize();

                        // 현재까지 받은 패킷를 전부 작업버퍼에 복사한다.
                        Array.Copy(
                            remainBuffProv,
                            0,
                            totBuff,
                            0,
                            remainCountProv);

                        // 복사받은 패킷 다음부터 현재패킷을 합친다..
                        Array.Copy(
                            buff,
                            0,
                            totBuff,
                            remainCountProv,
                            size);

                        remainCountProv += size;

                        while (true)
                        {
                            packetLen = 0;

                            // 실제 패킷길이를 얻는다.
                            if (remainCountProv >= 4)
                            {
                                packetLen |= (int)(totBuff[totRemainLen + 3] << 8);
                                packetLen |= (int)(totBuff[totRemainLen + 2]);
                                packetLen += 4;

                                // 현재까지 받은 패킷길이가 실제 데이타길이보다 큰지 체크한다.
                                if (remainCountProv >= packetLen)
                                {
                                    byte[] tempBuff = new byte[packetLen];
                                    Array.Copy(
                                        totBuff,
                                        totRemainLen,
                                        tempBuff,
                                        0,
                                        packetLen);

                                    this.orderMode2.RecvCtrlData(tempBuff);

                                    totRemainLen += packetLen;
                                    remainCountProv -= packetLen;
                                    packetLen = 0;
                                }
                                else
                                {
                                    // 저장할 버퍼를 초기화시키고.. 
                                    remainBuffProv.Initialize();

                                    // 현재까지의 패킷길이를 확인한후..
                                    if (remainCountProv > 0)
                                    {
                                        // 버퍼에 빽업시킨다.
                                        Array.Copy(
                                            totBuff,
                                            totRemainLen,
                                            remainBuffProv,
                                            0,
                                            remainCountProv);
                                    }
                                    break;
                                }
                            }
                            else
                            {
                                // 저장할 버퍼를 초기화시키고.. 
                                remainBuffProv.Initialize();

                                // 현재까지의 패킷길이를 확인한후..
                                if (remainCountProv > 0)
                                {
                                    // 버퍼에 빽업시킨다.
                                    Array.Copy(
                                        totBuff,
                                        totRemainLen,
                                        remainBuffProv,
                                        0,
                                        remainCountProv);
                                }
                                break;
                            }
                        } // END WHILE  
                    }

                }
                catch (Exception ex)
                {
                    // 저장할 버퍼를 초기화시키고.. 
                    remainBuffProv.Initialize();
                    remainCountProv = 0;

                    //throw new Exception(err.Message + "(ParsePacketProv)");
                }
            };

            if (this.InvokeRequired)
            {
                Invoke(setBtn);
            }
            else
            {
                setBtn();
            }
        }

        /// <summary>
        /// 경보대 서버와 TCP 통신 연결 종료 이벤트
        /// </summary>
        /// <param name="exc"></param>
        private void clientSoc1_evDisconnected(Exception exc)
        {
            this.tcpState1 = false;
            this.SetToolLBImage(this.ToolStripStatusLabel2, MewsBroad.Properties.Resources.IconListRed);
            this.SetOptionTcp2Btn(false);
        }

        /// <summary>
        /// 경보대 서버와 TCP 통신 연결 실패 이벤트
        /// </summary>
        /// <param name="exc"></param>
        private void clientSoc1_evConnectFailed(Exception exc)
        {
            this.tcpState1 = false;
            this.SetToolLBImage(this.ToolStripStatusLabel2, MewsBroad.Properties.Resources.IconListRed);
            this.SetOptionTcp2Btn(false);
        }

        /// <summary>
        /// 경보대 서버와 TCP 통신 연결 성공 이벤트
        /// </summary>
        private void clientSoc1_evConnected()
        {
            this.tcpState1 = true;
            this.SetToolLBImage(this.ToolStripStatusLabel2, MewsBroad.Properties.Resources.IconListGreen);
            this.SetOptionTcp2Btn(true);

            MethodInvoker set = delegate()
            {
                this.orderMode2.OnSendDataToCtrlPnl();
                this.orderMode2.SetLampOff();
            };

            if (this.orderMode2.InvokeRequired)
            {
                this.Invoke(set);
            }
            else
            {
                set();
            }
        }

        /// <summary>
        /// 운영대 TCP 서버에서 발생시킨 Timeout 이벤트
        /// </summary>
        private void clientSoc_evTimeout()
        {
            this.tcpState = false;
            this.SetToolLBImage(this.ToolStripStatusLabel1, MewsBroad.Properties.Resources.IconListRed);
            this.SetOptionTcp1Btn(false);
        }

        /// <summary>
        /// 운영대 TCP 서버로 데이터 전송 실패 이벤트
        /// </summary>
        /// <param name="exc"></param>
        private void clientSoc_evSendFailed(Exception exc)
        {
            //로그를 남기는 부분이 있다면 사용..
        }

        /// <summary>
        /// 운영대 서버 소켓에서 전송한 데이터 이벤트
        /// </summary>
        /// <param name="buff"></param>
        private void clientSoc_evReceived(byte[] buff)
        {
            MethodInvoker setBtn = delegate()
            {
                try
                {
                    PrtBase p = PrtMng.ParseFrame(buff);

                    if (p == null)
                    {
                        Debug.WriteLine("BroadMain.clientSoc_evReceived - 수신한 데이터를 Parse 할수 없음");

                        if (this.logState)
                        {
                            MTextLog.WriteTextLog(true, "BroadMain.clientSoc_evReceived - 수신한 데이터를 Parse 할수 없음");
                        }

                        return;
                    }

                    switch (p.Cmd)
                    {
                        case 19:
                            if (this.orderMode2.CurKind == (byte)Util.emKind.liveBrd)
                            {
                                PrtCmd19 p19 = p as PrtCmd19;

                                if (p19.SetKind == 1) //시작, 종료에 대한 응답은 적용하는 곳 없음
                                {
                                    if (this.orderMode2.MicSet == "end")
                                    {
                                        if (p19.Rult == 0) //정상
                                        {
                                            ShowOnAir(true);
                                        }
                                        else //이상
                                        {
                                            //일단은 이상 표시는 하지 않음. VHF/SAT 둘 중 먼저 도착하면 열린거니까..
                                            //this.FalseOnAir();
                                        }
                                    }
                                }
                            }
                            break;

                        case 20:
                            if (this.orderMode2.MicSet == "end")
                            {
                                PrtCmd20 p20 = p as PrtCmd20;

                                if (p20.BroadKind == 2)
                                {
                                    this.orderMode2.SetLiveEnd();
                                }
                            }
                            break;

                        case 3:
                            PrtCmd03 p3 = p as PrtCmd03;

                            if (p3.Kind == (byte)PrtCmd03.emKind.SET_TIME)
                            {
                                SystemTime st = new SystemTime();
                                bool rst = st.SetLocalTime(p3.SetTimeDT);

                                if (this.logState)
                                {
                                    MTextLog.WriteTextLog(true, string.Format("Server time receive - {0}", (rst == true) ? "Success" : "Fail"));
                                }
                            }
                            break;

                        case 24: //EQC에서 들어오는 자동 방송 이벤트
                            this.orderMode2.SetAutoReset();
                            PrtCmd24 p24 = p as PrtCmd24;

#if test
                            p24.DisMode = 0; //지진방송 나가는 거 임시로 시험으로 고정함. JYP
#endif

                            if (Util.autoInfo.intensity < (p24.DisValue * 0.01)) //high
                            {
                                if (Util.autoInfo.highAuto) //auto
                                {
                                    this.orderMode2.CurMode = p24.DisMode;
                                    this.orderMode2.CurKind = (byte)Util.emKind.storedMsg;
                                    this.orderMode2.CurMedia = (byte)Util.emMedia.all;
                                    this.orderMode2.BTVAudio = true;
                                    this.orderMode2.CurReptCnt = 1;

                                    if (p24.DisAddrStr == "15.255.255.255" || p24.DisAddrStr == "15.8.255.255")
                                    {
                                        this.orderMode2.CurDest = (byte)Util.emDest.all;
                                    }
                                    else
                                    {
                                        this.orderMode2.CurDest = (byte)Util.emDest.ind;
                                        List<string> tmpLst = new List<string>();
                                        List<string> tmpTermLst = new List<string>();
                                        string[] tmpSplit = p24.DisAddrStr.Split('.');
                                        tmpSplit[3] = "255";
                                        tmpLst.Add(tmpSplit[0] + "." + tmpSplit[1] + "." + tmpSplit[2] + "." + tmpSplit[3]);
                                        this.orderMode2.CurDistLstIP = tmpLst;
                                        this.orderMode2.CurTermLstIP = tmpTermLst;
                                    }

                                    string[] tmpMsgNum = Util.autoInfo.highMsg.Split(')');
                                    tmpMsgNum[0] = tmpMsgNum[0].Replace("(", "");
                                    this.orderMode2.CurMsgNum = byte.Parse(tmpMsgNum[0]);

                                    //자동방송에 쓰이는 CG 셋팅
                                    this.autoBoardCGInfo.cut = new bool[9];
                                    this.autoBoardCGInfo.cut[0] = true;
                                    this.autoBoardCGInfo.isBlack = false;
                                    this.autoBoardCGInfo.kind = "AutoCG";
                                    this.autoBoardCGInfo.kindNum = Util.autoCGInfo.highKindNum;
                                    this.autoBoardCGInfo.name = "AutoCG";
                                    this.autoBoardCGInfo.text = Util.autoCGInfo.highText;
                                    this.orderMode2.BoardText = Util.autoCGInfo.highText;
                                    this.orderMode2.CurBoardInfo = this.autoBoardCGInfo;

                                    this.orderMode2.AutoMakeOrderPacket();

                                    if (Util.autoInfo.highCBSUse)
                                    {
                                        PrtBase pb = PrtMng.GetPrtObject(13);
                                        PrtCmd13 p13 = pb as PrtCmd13;
                                        p13.DisTime = p24.DisTime;
                                        p13.DisTimeDT = p24.DisTimeDT;
                                        p13.DisMode = p24.DisMode;
                                        p13.DisCode = 2;
                                        p13.DisStsInfo = 2;
                                        p13.DisFlag = 2;
                                        p13.DisAddrStr = p24.DisAddrStr;
                                        p13.Reserved1 = p24.DisValue;

                                        byte[] cbsBuff = p13.MakeData();
                                        cbsBuff = PrtMng.MakeFrame(pb);
                                        this.clientSoc.Send(cbsBuff);
                                    }
                                }
                            }
                            else //low
                            {
                                if (Util.autoInfo.lowAuto)
                                {
                                    this.orderMode2.CurMode = p24.DisMode;
                                    this.orderMode2.CurKind = (byte)Util.emKind.storedMsg;
                                    this.orderMode2.CurMedia = (byte)Util.emMedia.all;
                                    this.orderMode2.BTVAudio = true;
                                    this.orderMode2.CurReptCnt = 1;

                                    if (p24.DisAddrStr == "15.255.255.255" || p24.DisAddrStr == "15.8.255.255")
                                    {
                                        this.orderMode2.CurDest = (byte)Util.emDest.all;
                                    }
                                    else
                                    {
                                        this.orderMode2.CurDest = (byte)Util.emDest.ind;
                                        List<string> tmpLst = new List<string>();
                                        List<string> tmpTermLst = new List<string>();
                                        string[] tmpSplit = p24.DisAddrStr.Split('.');
                                        tmpSplit[3] = "255";
                                        tmpLst.Add(tmpSplit[0] + "." + tmpSplit[1] + "." + tmpSplit[2] + "." + tmpSplit[3]);
                                        this.orderMode2.CurDistLstIP = tmpLst;
                                        this.orderMode2.CurTermLstIP = tmpTermLst;
                                    }

                                    string[] tmpMsgNum = Util.autoInfo.lowMsg.Split(')');
                                    tmpMsgNum[0] = tmpMsgNum[0].Replace("(", "");
                                    this.orderMode2.CurMsgNum = byte.Parse(tmpMsgNum[0]);

                                    //자동방송에 쓰이는 CG 셋팅
                                    this.autoBoardCGInfo.cut = new bool[9];
                                    this.autoBoardCGInfo.cut[0] = true;
                                    this.autoBoardCGInfo.isBlack = false;
                                    this.autoBoardCGInfo.kind = "AutoCG";
                                    this.autoBoardCGInfo.kindNum = Util.autoCGInfo.lowKindNum;
                                    this.autoBoardCGInfo.name = "AutoCG";
                                    this.autoBoardCGInfo.text = Util.autoCGInfo.lowText;
                                    this.orderMode2.BoardText = Util.autoCGInfo.lowText;
                                    this.orderMode2.CurBoardInfo = this.autoBoardCGInfo;

                                    this.orderMode2.AutoMakeOrderPacket();

                                    if (Util.autoInfo.lowCBSUse)
                                    {
                                        PrtBase pb = PrtMng.GetPrtObject(13);
                                        PrtCmd13 p13 = pb as PrtCmd13;
                                        p13.DisTime = p24.DisTime;
                                        p13.DisTimeDT = p24.DisTimeDT;
                                        p13.DisMode = p24.DisMode;
                                        p13.DisCode = 2;
                                        p13.DisStsInfo = 2;
                                        p13.DisFlag = 2;
                                        p13.DisAddrStr = p24.DisAddrStr;
                                        p13.Reserved1 = p24.DisValue;

                                        byte[] cbsBuff = p13.MakeData();
                                        cbsBuff = PrtMng.MakeFrame(pb);
                                        this.clientSoc.Send(cbsBuff);
                                    }
                                }
                            }

                            //자동방송 폼 init
                            AutoAlertForm autoForm = new AutoAlertForm(this.isPrimary);
                            autoForm.OnCountEndEvt += new EventHandler<CountEndEventArgs>(autoForm_OnCountEndEvt);
                            autoForm.OnCancleEvt += new EventHandler<CountEndEventArgs>(autoForm_OnCancleEvt);
                            autoForm.setInit(p24);
                            autoForm.Show();
                            break;

                        case 28:
                            PrtCmd28 p28 = p as PrtCmd28;

                            for (int i = 0; i < this.termStateList.Count; i++)
                            {
                                if (this.termStateList[i].TermIP == p28.AddrStr)
                                {
                                    this.termStateList[i].TermState = (p28.TermSts == 1) ? false : true;
                                }
                            }

                            this.MainMap.SetMapTermStatusInfo(p28.AddrStr, (p28.TermSts == 1) ? false : true);
                            break;

                        case 29: //Primary, Secondary 설정 데이터
                            PrtCmd29 p29 = p as PrtCmd29;
                            AdengUtil.INIWriteValueString(this.setting, this.Primary, (p29.IsPrimary == 1) ? "1" : "2", IniFilePath);

                            MethodInvoker invoker = delegate()
                            {
                                this.isPrimarySetting(p29.IsPrimary);
                            };

                            if (this.InvokeRequired)
                            {
                                this.Invoke(invoker);
                            }
                            else
                            {
                                invoker();
                            }
                            break;

                        case 30: //기초데이터 변경
                            PrtCmd30 p30 = p as PrtCmd30;

                            if (p30.CompleteState == 1) //CC 변경 완료
                            {
                                MessageBox.Show(LangPack.GetMongolian("The basic data changed. please restart"), "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            break;

                        case 31: //설정데이터 변경
                            PrtCmd31 p31 = p as PrtCmd31;

                            if (p31.Kind == 2) //MCC 적용 완료
                            {
                                this.mewsSettingSyncUC.setReceiveApplyCC(p31.FileName);
                            }
                            else if (p31.Kind == 4) //MCC 동기화 요청
                            {
                                DataSyncEventArgs dsea = new DataSyncEventArgs(p31.FileName);
                                this.mewsSettingSyncUC_OnDataSyncApplyEvtToCC(this, dsea);
                            }
                            break;

                        case 5: // MCC방송대에서 발령 데이터 수신할 경우, Map에 발령 정보 표출
                            PrtCmd05 p5 = p as PrtCmd05;
                            ShowMapWaringInfo(p5);
                            break;

                        default:
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("BroadMain.clientSoc_evReceived - " + ex.Message);

                    if (this.logState)
                    {
                        MTextLog.WriteTextLog(true, "BroadMain.clientSoc_evReceived - " + ex.Message);
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

        private void ShowMapWaringInfo(PrtCmd05 p5)
        {
            MapWarnInfo mwInfo = new MapWarnInfo();
            mwInfo.Mode = p5.Mode;
            mwInfo.WarnTime = p5.TimeDT;
            mwInfo.WarnKind = p5.Kind;

            // 전체발령
            if (p5.LstTermStr[0] == Util.totalIP)
            {
                mwInfo.TargetKind = 0;
                mwInfo.lstTermIP = orderMode2.GetTermIpList(MDataMng.GetInstance().DicOnlyBroadOrderTerm);
                mwInfo.lstTargetIP = orderMode2.GetDistIpList(MDataMng.GetInstance().DicDist);
            }
            else
            {
                List<string> lstTermIpMap = new List<string>();
                List<string> lstDistIp = new List<string>();

                foreach (string orderIp in p5.LstTermStr)
                {
                    string[] arrIp = orderIp.Split('.');

                    // 시군발령
                    if (arrIp[3] == "255")
                    {
                        mwInfo.TargetKind = 1;

                        foreach (string termip in orderMode2.GetTermIpList(orderMode2.GetDistData(orderIp).dicBroadTermData))
                        {
                            lstTermIpMap.Add(termip);
                        }

                        lstDistIp.Add(orderIp);
                    }
                    // 개별발령
                    else
                    {
                        mwInfo.TargetKind = 2;
                        lstTermIpMap.Add(orderIp);
                    }
                }

                mwInfo.lstTermIP = lstTermIpMap;
                mwInfo.lstTargetIP = lstDistIp;
            }

            mwInfo.IsLangEnglish = LangPack.IsEng;
            this.SetMapItem(mwInfo);
        }

        /// <summary>
        /// 자동방송 캔슬 버튼 클릭 이벤트 (해제 처리)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void autoForm_OnCancleEvt(object sender, CountEndEventArgs e)
        {
            MethodInvoker set = delegate()
            {
                this.orderMode2.CurMode = e.P24.DisMode;
                this.orderMode2.CurKind = (byte)Util.emKind.stop;
                this.orderMode2.CurMedia = (byte)Util.emMedia.all;
                this.orderMode2.BTVAudio = true;
                this.orderMode2.CurDest = (byte)Util.emDest.all;
                this.orderMode2.AutoMakeOrderPacket();
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
        /// 자동방송 카운트 제로 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void autoForm_OnCountEndEvt(object sender, CountEndEventArgs e)
        {
            MethodInvoker set = delegate()
            {
                this.orderMode2.CurMode = e.P24.DisMode;
                this.orderMode2.CurKind = (byte)Util.emKind.storedMsg;
                this.orderMode2.CurMedia = (byte)Util.emMedia.all;
                this.orderMode2.BTVAudio = true;
                this.orderMode2.CurReptCnt = 1;
                this.orderMode2.CurDest = (byte)Util.emDest.all;

                if (Util.autoInfo.intensity < (e.P24.DisValue * 0.01)) //high
                {
                    string[] tmpMsgNum = Util.autoInfo.highMsg.Split(')');
                    tmpMsgNum[0] = tmpMsgNum[0].Replace("(", "");
                    this.orderMode2.CurMsgNum = byte.Parse(tmpMsgNum[0]);

                    //자동방송에 쓰이는 CG 셋팅
                    this.autoBoardCGInfo.cut = new bool[9];
                    this.autoBoardCGInfo.cut[0] = true;
                    this.autoBoardCGInfo.isBlack = false;
                    this.autoBoardCGInfo.kind = "AutoCG";
                    this.autoBoardCGInfo.kindNum = Util.autoCGInfo.highKindNum;
                    this.autoBoardCGInfo.name = "AutoCG";
                    this.autoBoardCGInfo.text = Util.autoCGInfo.highText;
                    this.orderMode2.BoardText = Util.autoCGInfo.highText;
                    this.orderMode2.CurBoardInfo = this.autoBoardCGInfo;
                    
                    this.orderMode2.AutoMakeOrderPacket();

                    if (Util.autoInfo.highCBSUse)
                    {
                        PrtBase pb = PrtMng.GetPrtObject(13);
                        PrtCmd13 p13 = pb as PrtCmd13;
                        p13.DisTime = e.P24.DisTime;
                        p13.DisTimeDT = e.P24.DisTimeDT;
                        p13.DisMode = e.P24.DisMode;
                        p13.DisCode = 2;
                        p13.DisStsInfo = 2;
                        p13.DisFlag = 2;
                        p13.DisAddrStr = e.P24.DisAddrStr;
                        p13.Reserved1 = e.P24.DisValue;

                        byte[] cbsBuff = p13.MakeData();
                        cbsBuff = PrtMng.MakeFrame(pb);
                        this.clientSoc.Send(cbsBuff);
                    }
                }
                else //low
                {
                    string[] tmpMsgNum = Util.autoInfo.lowMsg.Split(')');
                    tmpMsgNum[0] = tmpMsgNum[0].Replace("(", "");
                    this.orderMode2.CurMsgNum = byte.Parse(tmpMsgNum[0]);

                    //자동방송에 쓰이는 CG 셋팅
                    this.autoBoardCGInfo.cut = new bool[9];
                    this.autoBoardCGInfo.cut[0] = true;
                    this.autoBoardCGInfo.isBlack = false;
                    this.autoBoardCGInfo.kind = "AutoCG";
                    this.autoBoardCGInfo.kindNum = Util.autoCGInfo.lowKindNum;
                    this.autoBoardCGInfo.name = "AutoCG";
                    this.autoBoardCGInfo.text = Util.autoCGInfo.lowText;
                    this.orderMode2.BoardText = Util.autoCGInfo.lowText;
                    this.orderMode2.CurBoardInfo = this.autoBoardCGInfo;

                    this.orderMode2.AutoMakeOrderPacket();

                    if (Util.autoInfo.lowCBSUse)
                    {
                        PrtBase pb = PrtMng.GetPrtObject(13);
                        PrtCmd13 p13 = pb as PrtCmd13;
                        p13.DisTime = e.P24.DisTime;
                        p13.DisTimeDT = e.P24.DisTimeDT;
                        p13.DisMode = e.P24.DisMode;
                        p13.DisCode = 2;
                        p13.DisStsInfo = 2;
                        p13.DisFlag = 2;
                        p13.DisAddrStr = e.P24.DisAddrStr;
                        p13.Reserved1 = e.P24.DisValue;

                        byte[] cbsBuff = p13.MakeData();
                        cbsBuff = PrtMng.MakeFrame(pb);
                        this.clientSoc.Send(cbsBuff);
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

        /// <summary>
        /// 운영대 서버와 TCP 통신 연결 종료 이벤트
        /// </summary>
        /// <param name="exc"></param>
        private void clientSoc_evDisconnected(Exception exc)
        {
            this.tcpState = false;
            this.SetToolLBImage(this.ToolStripStatusLabel1, MewsBroad.Properties.Resources.IconListRed);
            this.SetOptionTcp1Btn(false);
        }

        /// <summary>
        /// 운영대 서버와 TCP 통신 연결 실패 이벤트
        /// </summary>
        /// <param name="exc"></param>
        private void clientSoc_evConnectFailed(Exception exc)
        {
            this.tcpState = false;
            this.SetToolLBImage(this.ToolStripStatusLabel1, MewsBroad.Properties.Resources.IconListRed);
            this.SetOptionTcp1Btn(false);
        }

        /// <summary>
        /// 운영대 서버와 TCP 통신 연결 성공 이벤트
        /// </summary>
        private void clientSoc_evConnected()
        {
            this.tcpState = true;
            this.SetToolLBImage(this.ToolStripStatusLabel1, MewsBroad.Properties.Resources.IconListGreen);
            this.SetOptionTcp1Btn(true);
        }

        /// <summary>
        /// 메뉴의 종료 클릭 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitXToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 메뉴의 옵션 클릭 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void optionOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (this.optionForm = new BroadOption(this.isPrimary))
            {
                this.optionForm.OnDbSettingDataEvt += new EventHandler<DbSettingDataEvtArgs>(optionForm_OnDbSettingDataEvt);
                this.optionForm.OnTcpSocketEvt += new EventHandler<TcpSocketEventArgs>(optionForm_OnTcpSocketEvt);
                this.optionForm.OnTcpSocketEvt1 += new EventHandler<TcpSocketEventArgs>(optionForm_OnTcpSocketEvt1);
                this.optionForm.OnTcpSettingEvt += new EventHandler<TcpSettingEventArgs>(optionForm_OnTcpSettingEvt);
                this.optionForm.OnTcpSettingEvt1 += new EventHandler<TcpSettingEventArgs>(optionForm_OnTcpSettingEvt1);
                this.optionForm.OnLogSettingEvt += new EventHandler<LogSettingEventArgs>(optionForm_OnLogSettingEvt);
                this.optionForm.OnBuzzerEvt += new EventHandler<BuzzerEventArgs>(optionForm_OnBuzzerEvt);

                //DB
                this.optionForm.DbIp = AdengUtil.INIReadValueString(this.setting, this.DBIP, this.IniFilePath);
                this.optionForm.DbSid = AdengUtil.INIReadValueString(this.setting, this.DBSID, this.IniFilePath);
                this.optionForm.DbId = AdengUtil.INIReadValueString(this.setting, this.DBID, this.IniFilePath);
                this.optionForm.DbPw = AdengUtil.INIReadValueString(this.setting, this.DBPW, this.IniFilePath);
                this.optionForm.Db8i = (AdengUtil.INIReadValueString(this.setting, this.Oracle8i, this.IniFilePath) == "T" ? true : false);

                //TCP (운영대)
                this.optionForm.TcpIp = AdengUtil.INIReadValueString(this.setting, this.TCPIP, this.IniFilePath);
                this.optionForm.TcpPort = AdengUtil.INIReadValueString(this.setting, this.TCPPort, this.IniFilePath);
                this.optionForm.TcpState = this.tcpState;

                //TCP1 (경보대)
                this.optionForm.TcpIp1 = AdengUtil.INIReadValueString(this.setting, this.TCPIP1, this.IniFilePath);
                this.optionForm.TcpPort1 = AdengUtil.INIReadValueString(this.setting, this.TCPPort1, this.IniFilePath);
                this.optionForm.TcpState1 = this.tcpState1;

                //LOG
                this.optionForm.LogCheckBox = (AdengUtil.INIReadValueString(this.setting, this.LogFlag, this.IniFilePath) == "T" ? true : false);

                //Buzzer
                this.optionForm.RealBuzzerCB = Util.optionInfo.bUseBuzzerReal;
                this.optionForm.DrillBuzzerCB = Util.optionInfo.bUseBuzzerDrill;
                this.optionForm.TestBuzzerCB = Util.optionInfo.bUseBuzzerTest;

                this.optionForm.ShowDialog();

                this.optionForm.OnDbSettingDataEvt -= new EventHandler<DbSettingDataEvtArgs>(optionForm_OnDbSettingDataEvt);
                this.optionForm.OnTcpSocketEvt -= new EventHandler<TcpSocketEventArgs>(optionForm_OnTcpSocketEvt);
                this.optionForm.OnTcpSocketEvt1 -= new EventHandler<TcpSocketEventArgs>(optionForm_OnTcpSocketEvt1);
                this.optionForm.OnTcpSettingEvt -= new EventHandler<TcpSettingEventArgs>(optionForm_OnTcpSettingEvt);
                this.optionForm.OnTcpSettingEvt1 -= new EventHandler<TcpSettingEventArgs>(optionForm_OnTcpSettingEvt1);
                this.optionForm.OnLogSettingEvt -= new EventHandler<LogSettingEventArgs>(optionForm_OnLogSettingEvt);
                this.optionForm.OnBuzzerEvt -= new EventHandler<BuzzerEventArgs>(optionForm_OnBuzzerEvt);
            }
        }

        /// <summary>
        /// 환경 설정창에서 발생하는 부저 셋팅 값 변경 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void optionForm_OnBuzzerEvt(object sender, BuzzerEventArgs e)
        {
            Util.optionInfo.bUseBuzzerReal = e.Real;
            Util.optionInfo.bUseBuzzerDrill = e.Drill;
            Util.optionInfo.bUseBuzzerTest = e.Test;
            Util.SaveOptionInfo();
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
        /// 환경 설정창에서 발생하는 TCP1 셋팅 값 변경 이벤트 (경보대)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void optionForm_OnTcpSettingEvt1(object sender, TcpSettingEventArgs e)
        {
            AdengUtil.INIWriteValueString(this.setting, this.TCPIP1, e.Ip, this.IniFilePath);
            AdengUtil.INIWriteValueString(this.setting, this.TCPPort1, e.Port, this.IniFilePath);
            this.localTcpIp1 = e.Ip;
            this.localTcpPort1 = int.Parse(e.Port);

            //바뀐 설정값으로 재연결
            this.clientSoc1.Disconnect();
            this.clientSoc1.ConnectTo(this.localTcpIp1, this.localTcpPort1);
        }

        /// <summary>
        /// 환경 설정창에서 발생하는 TCP 셋팅 값 변경 이벤트 (운영대)
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
        /// 환경 설정창에서 발생하는 TCP1 통신 제어 이벤트 (경보대)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void optionForm_OnTcpSocketEvt1(object sender, TcpSocketEventArgs e)
        {
            if (e.State) //통신연결
            {
                AdengUtil.INIWriteValueString(this.setting, this.TCPIP1, e.Ip, this.IniFilePath);
                AdengUtil.INIWriteValueString(this.setting, this.TCPPort1, e.Port, this.IniFilePath);
                this.localTcpIp1 = e.Ip;
                this.localTcpPort1 = int.Parse(e.Port);

                this.clientSoc1.ConnectTo(this.localTcpIp1, this.localTcpPort1);
            }
            else //연결해제
            {
                this.clientSoc1.Disconnect();
                this.tcpState1 = false;
                this.SetToolLBImage(this.ToolStripStatusLabel2, MewsBroad.Properties.Resources.IconListRed);
                this.SetOptionTcp2Btn(false);
            }
        }

        /// <summary>
        /// 환경 설정창에서 발생하는 TCP 통신 제어 이벤트 (운영대)
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
                this.SetToolLBImage(this.ToolStripStatusLabel1, MewsBroad.Properties.Resources.IconListRed);
                this.SetOptionTcp1Btn(false);
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
        /// 메뉴의 그룹 편집 클릭 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void groupGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (GroupMngForm groupMng = new GroupMngForm(this.isPrimary))
            {
                groupMng.ShowDialog();
            }
        }

        /// <summary>
        /// 메뉴의 저장메시지 편집 클릭 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void storedMessageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (StMsgMngForm stMsgMng = new StMsgMngForm(this.isPrimary))
            {
                stMsgMng.OnStoMsgChangedEvt += new EventHandler<StoMsgChangedEventArgs>(stMsgMng_OnStoMsgChangedEvt);
                stMsgMng.ShowDialog();
                stMsgMng.OnStoMsgChangedEvt -= new EventHandler<StoMsgChangedEventArgs>(stMsgMng_OnStoMsgChangedEvt);
            }
        }

        /// <summary>
        /// CG 추가/수정/삭제 시 발생하는 이벤트
        /// 설정데이터 변경 이벤트 시작점
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void form_OnCGChangedEvt(object sender, CGChangedEventArgs e)
        {
            //try
            //{
            //    PrtBase pb = PrtMng.GetPrtObject(31);
            //    PrtCmd31 p31 = pb as PrtCmd31;

            //    p31.Kind = 1; //CC방송대는 변경 완료
            //    p31.FileLength = 11;
            //    p31.FileName = "BrdData.xml";

            //    //보낼 xml파일 스트림 만들기
            //    StringWriter sw = new StringWriter();
            //    AdengXmlNode node = new AdengXmlNode();
            //    node.SaveXmlDocument(sw);
            //    node.SaveXmlDocument(System.Windows.Forms.Application.StartupPath + "\\xmldata\\BrdData.xml");
            //    byte[] xmlDataBuff = Encoding.Default.GetBytes(sw.ToString());
            //    p31.FileData = xmlDataBuff;
            //    pb.MakeData();
            //    byte[] buff = PrtMng.MakeFrame(pb);
            //    this.clientSoc.Send(buff);

            //    this.mewsSettingSyncUC.ReceiveTimeCC = (short)300;
            //    this.mewsSettingSyncUC.init(MewsSettingSyncUC.regionEnum.CC_VD, "BrdData.xml", this.mewsSyncFailUC, p31);
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(LangPack.GetMongolian("BrdData.xml Setting data send fail. - " + ex.Message), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        /// <summary>
        /// 저장메시지 추가/수정/삭제 시 발생하는 이벤트
        /// 설정데이터 변경 이벤트 시작점
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void stMsgMng_OnStoMsgChangedEvt(object sender, StoMsgChangedEventArgs e)
        {
            //try
            //{
            //    PrtBase pb = PrtMng.GetPrtObject(31);
            //    PrtCmd31 p31 = pb as PrtCmd31;

            //    p31.Kind = 1; //CC방송대는 변경 완료
            //    p31.FileLength = 15;
            //    p31.FileName = "MsgNameData.xml";

            //    //보낼 xml파일 스트림 만들기
            //    StringWriter sw = new StringWriter();
            //    AdengXmlNode node = new AdengXmlNode();
            //    node.SaveXmlDocument(sw);
            //    node.SaveXmlDocument(System.Windows.Forms.Application.StartupPath + "\\xmldata\\MsgNameData.xml");
            //    byte[] xmlDataBuff = Encoding.Default.GetBytes(sw.ToString());
            //    p31.FileData = xmlDataBuff;
            //    pb.MakeData();
            //    byte[] buff = PrtMng.MakeFrame(pb);
            //    this.clientSoc.Send(buff);

            //    this.mewsSettingSyncUC.ReceiveTimeCC = (short)300;
            //    this.mewsSettingSyncUC.init(MewsSettingSyncUC.regionEnum.CC_VD, "MsgNameData.xml", this.mewsSyncFailUC, p31);
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(LangPack.GetMongolian("MsgNameData.xml Setting data send fail. - " + ex.Message), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        /// <summary>
        /// 지도의 Context Menu 일반모드 클릭 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void normalModeNToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.normalNToolStripMenuItem_Click(sender, e);
        }

        /// <summary>
        /// 지도의 Context Menu 편집모드 클릭 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void modifyModeDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.modifyDToolStripMenuItem_Click(sender, e);
        }

        /// <summary>
        /// 메뉴의 사이렌 편집 클릭 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sirenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SirenMsgForm sirenMsgMng = new SirenMsgForm())
            {
                sirenMsgMng.ShowDialog();
            }
        }

        /// <summary>
        /// 메뉴의 CG 편집 클릭 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cGCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (BoardMngForm form = new BoardMngForm(null, false, this.isPrimary))
            {
                form.OnCGChangedEvt += new EventHandler<CGChangedEventArgs>(form_OnCGChangedEvt);
                form.ShowDialog();
                form.OnCGChangedEvt -= new EventHandler<CGChangedEventArgs>(form_OnCGChangedEvt);
            }
        }

        /// <summary>
        /// 메뉴의 프로그램 정보 클릭 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void infomationIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (BroadInfoForm info = new BroadInfoForm(this.Text, this.isPrimary))
            {
                info.ShowDialog();
            }
        }

        /// <summary>
        /// 메뉴의 자동방송 설정 클릭 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void autoAlertAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (AutoEditForm auto = new AutoEditForm(this.isPrimary))
            {
                auto.ShowDialog();
            }
        }
        #endregion

        /// <summary>
        /// 영어
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void englishEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AdengUtil.INIWriteValueString(this.setting, this.Language, "E", IniFilePath);
            LangPack.IsEng = true;
            this.englishEToolStripMenuItem.Enabled = false;
            this.mongolianMToolStripMenuItem.Enabled = true;
            this.initLang();
        }

        /// <summary>
        /// 몽골어
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mongolianMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AdengUtil.INIWriteValueString(this.setting, this.Language, "M", IniFilePath);
            LangPack.IsEng = false;
            this.englishEToolStripMenuItem.Enabled = true;
            this.mongolianMToolStripMenuItem.Enabled = false; ;
            this.initLang();
        }

        /// <summary>
        /// Primary, Secondary 설정에 따른 셋팅을 위한 메소드
        /// </summary>
        /// <param name="_isPrimary"></param>
        private void isPrimarySetting(byte _isPrimary)
        {
            if (_isPrimary == 1)
            {
                this.MainTopLB2.ForeColor = Color.Red;
                this.MainTopLB2.Text = "[Primary Mode]";
                this.orderMode2.Enabled = true;
                this.primaryPB.BackgroundImage = MewsBroad.Properties.Resources.ucPrimary;
                this.MainTopPN.BackgroundImage = MewsBroad.Properties.Resources.bgTitle;
            }
            else if (_isPrimary == 2)
            {
                this.MainTopLB2.ForeColor = Color.White;
                this.MainTopLB2.Text = "[Secondary Mode]";
                this.orderMode2.Enabled = false;
                this.primaryPB.BackgroundImage = MewsBroad.Properties.Resources.ucSecond;
                this.MainTopPN.BackgroundImage = MewsBroad.Properties.Resources.bgTitleGreen;
            }
        }
    }

    /// <summary>
    /// 사이렌/EBS 터미널의 상태를 MAP에 표출하기 위한 Class
    /// </summary>
    public class TermStateInfo
    {
        string termIP;
        bool termState;

        public string TermIP
        {
            get { return this.termIP; }
            set { this.termIP = value; }
        }

        public bool TermState
        {
            get { return this.termState; }
            set { this.termState = value; }
        }
    }
}