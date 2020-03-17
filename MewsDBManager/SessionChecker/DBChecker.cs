using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Mews.Svr.DBMng.Lib
{
    public class DBChecker
    {
        /// <summary>
        /// 체크 중 일정시간 마다 연결체크 이벤트를 발생시키기 위한 이벤트 핸들러
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="tscea"></param>
        private delegate void DBConChkEventArgsHandler(object sender, DBConChkEventArgs dbccea);

        /// <summary>
        /// 체크 중 일정시간 마다 재접속 이벤트를 발생시키기 위한 이벤트 핸들러
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="dbrcea"></param>
        private delegate void DBReConnectEventArgsHandler(object sender, DBReConnectEventArgs dbrcea);

        /// <summary>
        /// 일정시간 마다 연결체크를 알려주기 위한 이벤트
        /// </summary>
        public event EventHandler<DBConChkEventArgs> OnDBConChkEvt;

        /// <summary>
        /// 일정시간 마다 재접속을 알려주기 위한 이벤트
        /// </summary>
        public event EventHandler<DBReConnectEventArgs> OnDBReConnectEvt;

        private Thread CheckTD = null;
        private Thread ReConTD = null;
        private const int StateCheckTime = 5000;
        private const int ReConnectTime = 30000;
        private bool ReConFlag = true;

        /// <summary>
        /// ReConnect Flag 접근자
        /// </summary>
        public bool ReConnectFLAG
        {
            get { return this.ReConFlag; }
            set { this.ReConFlag = value; }
        }

        public DBChecker()
        {
        }

        /// <summary>
        /// 체크 메소드 시작
        /// </summary>
        public void Start()
        {
            this.CheckTD = new Thread(new ThreadStart(this.CheckStartTD));
            this.CheckTD.IsBackground = true;
            this.CheckTD.Start();

            this.ReConTD = new Thread(new ThreadStart(this.ReConnectTD));
            this.ReConTD.IsBackground = true;
            this.ReConTD.Start();
        }

        /// <summary>
        /// 체크 메소드
        /// </summary>
        private void CheckStartTD()
        {
            while (true)
            {
                Thread.Sleep(StateCheckTime);

                if (this.OnDBConChkEvt != null)
                {
                    this.OnDBConChkEvt(this, new DBConChkEventArgs());
                }
            }
        }

        /// <summary>
        /// 재접속 메소드
        /// </summary>
        private void ReConnectTD()
        {
            while (ReConFlag)
            {
                Thread.Sleep(ReConnectTime);

                if (this.OnDBReConnectEvt != null)
                {
                    this.OnDBReConnectEvt(this, new DBReConnectEventArgs());
                }
            }
        }
    }

    /// <summary>
    /// 체크 중 일정 시간 마다 연결체크 이벤트를 발생시키기 위해 사용하는 이벤트 아규먼트 클래스
    /// </summary>
    public class DBConChkEventArgs : EventArgs
    {
        public DBConChkEventArgs()
        {
        }
    }

    /// <summary>
    /// 체크 중 일정 시간 마다 재접속 이벤트를 발생시키기 위해 사용하는 이벤트 아규먼트 클래스
    /// </summary>
    public class DBReConnectEventArgs : EventArgs
    {
        public DBReConnectEventArgs()
        {
        }
    }
}