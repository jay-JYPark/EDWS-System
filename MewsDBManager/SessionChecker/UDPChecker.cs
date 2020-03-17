using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Mews.Svr.DBMng.Lib
{
    public class UDPChecker
    {
        /// <summary>
        /// 체크 중 일정시간 마다 이벤트를 발생시키기 위한 이벤트 핸들러
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="tscea"></param>
        private delegate void UdpSessionChkEventArgsHandler(object sender, UdpSessionChkEventArgs tscea);

        /// <summary>
        /// 일정시간 마다 알려주기 위한 이벤트
        /// </summary>
        public event EventHandler<UdpSessionChkEventArgs> OnUdpSessionChkEvt;

        private Thread CheckTD = null;
        private bool Gen = true;
        private bool Flag = false;

        /// <summary>
        /// 체크 여부 판단 멤버
        /// </summary>
        public bool GEN
        {
            set { this.Gen = value; }
            get { return this.Gen; }
        }

        /// <summary>
        /// 전체 동작 여부 판단 멤버
        /// </summary>
        public bool FLAG
        {
            set { this.Flag = value; }
            get { return this.Flag; }
        }

        public UDPChecker()
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
        }

        /// <summary>
        /// 체크 메소드
        /// </summary>
        private void CheckStartTD()
        {
            this.Flag = true;

            while (this.Gen)
            {
                Thread.Sleep(10000);

                if (this.Gen)
                {
                    if (this.OnUdpSessionChkEvt != null)
                    {
                        this.OnUdpSessionChkEvt(this, new UdpSessionChkEventArgs());
                    }
                }
            }

            this.Flag = false;
        }
    }

    /// <summary>
    /// 체크 중 일정 시간 마다 이벤트를 발생시키기 위해 사용하는 이벤트 아규먼트 클래스
    /// </summary>
    public class UdpSessionChkEventArgs : EventArgs
    {
        public UdpSessionChkEventArgs()
        {
        }
    }
}