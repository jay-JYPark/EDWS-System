using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.Data.OleDb;
using Adeng.Framework.Db;
using Adeng.Framework.Util;
using Mews.Lib.Mewsprt;

namespace Mews.Svr.DBMng.Lib
{
    public class DBMngDataMng
    {
        #region instance
        private AdengOleDb oleDb = null;
        private static DBMngDataMng instance = null;
        private static Mutex mutex = new Mutex();
        private bool isOpen = false;
        private List<TermData> termDataList = new List<TermData>();
        private List<ProvData> provDataList = new List<ProvData>();
        private List<DistData> distDataList = new List<DistData>();
        private delegate void TermStateTCPSendEvtArgsHandler(object sender, TermStateTCPSendEvtArgs tstcpsea);
        public event EventHandler<TermStateTCPSendEvtArgs> OnTermStateTCPSendEvt; //단말상태를 받은 후 수신확인을 전송하기 위한 이벤트
        #endregion

        #region val
        private readonly uint OraConnectCount = 201; //오라클 연결관리 카운트
        private uint OraQueryCount = 0; //오라클 쿼리작업 카운트
        private readonly string IniFilePath = "C:\\Program Files\\adeng\\MewsDBMng\\Setting.ini";
        private readonly string setting = "SETTING";
        private readonly string DBIP = "DBIP";
        private readonly string DBSID = "DBSID";
        private readonly string DBID = "DBID";
        private readonly string DBPW = "DBPW";
        private readonly string Oracle8i = "Oracle8i";
        private readonly int TEST = 0;
        private readonly int REAL = 1;
        private readonly int TRAIN = 2;
        private readonly int AlertTerm = 0;
        private readonly int BroadTerm = 1;
        #endregion

        #region 접근자
        public bool IsOpen
        {
            get { return this.isOpen; }
        }
        #endregion

        #region 생성자, 싱글톤 처리
        /// <summary>
        /// 싱글톤 처리
        /// </summary>
        /// <returns></returns>
        public static DBMngDataMng getInstance()
        {
            mutex.WaitOne();

            if (instance == null)
            {
                instance = new DBMngDataMng();
            }

            mutex.ReleaseMutex();
            return instance;
        }

        /// <summary>
        /// 생성자
        /// </summary>
        public DBMngDataMng()
        {
            this.oleDb = new AdengOleDb();
            this.oleDb.OracleProvider = "OraOLEDB.Oracle.1"; //64bit OracleProvider
        }
        #endregion

        #region oracle 관련
        /// <summary>
        /// 오라클에 접속한다.
        /// </summary>
        /// <param name="_ip"></param>
        /// <param name="_sid"></param>
        /// <param name="_id"></param>
        /// <param name="_pw"></param>
        /// <param name="_8i">
        /// 오라클 8i 프로바이더 사용 여부
        /// </param>
        /// <returns>
        /// 접속 결과를 반환한다.
        /// </returns>
        public bool OracleConnect(string _ip, string _sid, string _id, string _pw, bool _8i)
        {
            lock (this)
            {
                try
                {
                    if (this.oleDb != null)
                    {
                        this.oleDb.Close();
                        this.oleDb = null;
                    }
                }
                catch (Exception ex)
                {
                }

                try
                {
                    this.oleDb = new AdengOleDb();
                    this.oleDb.OracleProvider = "OraOLEDB.Oracle.1"; //64bit OracleProvider

                    if (!_8i)
                    {
                        oleDb.OpenOracle(_ip, _sid, _id, _pw);
                    }
                    else
                    {
                        oleDb.OpenOracle(_ip, _sid, _id, _pw, true);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(string.Format("DBMngDataMng.OracleConnect : {0}", ex.Message));
                }
            }

            if (this.oleDb == null)
            {
                return false;
            }
            else
            {
                return this.oleDb.IsOpen;
            }
        }

        /// <summary>
        /// 오라클의 접속 상태를 반환한다.
        /// </summary>
        /// <returns>
        /// true - 정상
        /// false - 이상
        /// </returns>
        public bool GetOracleState()
        {
            try
            {
                bool rst = false;

                if (this.oleDb.IsOpen)
                {
                    rst = true;
                }

                return rst;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 오라클의 연결을 해제한다.
        /// </summary>
        public void OracleClose()
        {
            try
            {
                if (this.oleDb != null)
                {
                    this.oleDb.Close();
                    this.oleDb = null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 오라클 연결을 관리한다.
        /// </summary>
        /// <param name="i"></param>
        public void OracleConMng(uint i)
        {
            if (((i + 1) % this.OraConnectCount) == 0)
            {
                if (this.oleDb != null)
                {
                    this.oleDb.Close();
                    this.oleDb = null;
                }

                string ip = AdengUtil.INIReadValueString(this.setting, this.DBIP, this.IniFilePath);
                string sid = AdengUtil.INIReadValueString(this.setting, this.DBSID, this.IniFilePath);
                string id = AdengUtil.INIReadValueString(this.setting, this.DBID, this.IniFilePath);
                string psw = AdengUtil.INIReadValueString(this.setting, this.DBPW, this.IniFilePath);
                bool support8 = (AdengUtil.INIReadValueString(this.setting, this.Oracle8i, this.IniFilePath) == "T") ? true : false;

                this.oleDb = new AdengOleDb();
                this.oleDb.OracleProvider = "OraOLEDB.Oracle.1"; //64bit OracleProvider
                this.OracleConnect(ip, sid, id, psw, support8);
                this.OraQueryCount = uint.MinValue;
            }
        }
        #endregion

        #region Data Insert (TC 0 ~ 17까지)
        #region TC 0 (경보 발령)
        public void SetDB_TC0(PrtCmd00 _tc0)
        {
            OleDbDataReader reader = null;
            int cnt = 0;
            int division = 0;

            if (((_tc0.Addr & 0x00ffffff) == 0x00ffffff) || ((_tc0.Addr & 0x0000ffff) == 0x0000ffff)) //전체 발령, 개별프로토콜 변경되서 적용됨
            {
                try
                {
                    //같은 발령이 DB에 있는지 확인
                    if (_tc0.Mode == REAL) //실제
                    {
                        this.oleDb.QueryData("SELECT COUNT(*) FROM REALALARMORDERHIST WHERE ORDERTIME = TO_DATE(" +
                            this.GetDateType(_tc0.TimeDT) + ", 'yyyymmddhh24miss') AND SOURCE = " + _tc0.Source + " AND ORDERIP = '" + _tc0.AddrStr + "'", out reader);

                        while (reader.Read())
                        {
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                cnt = int.Parse(reader[i].ToString());
                            }
                        }

                        if (reader != null)
                        {
                            reader.Dispose();
                        }
                    }
                    else if (_tc0.Mode == TEST) //시험
                    {
                        this.oleDb.QueryData("SELECT COUNT(*) FROM TESTALARMORDERHIST WHERE ORDERTIME = TO_DATE(" +
                            this.GetDateType(_tc0.TimeDT) + ", 'yyyymmddhh24miss') AND SOURCE = " + _tc0.Source + " AND ORDERIP = '" + _tc0.AddrStr + "'", out reader);

                        while (reader.Read())
                        {
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                cnt = int.Parse(reader[i].ToString());
                            }
                        }

                        if (reader != null)
                        {
                            reader.Dispose();
                        }
                    }
                    else if (_tc0.Mode == TRAIN) //훈련
                    {
                        this.oleDb.QueryData("SELECT COUNT(*) FROM TRAINALARMORDERHIST WHERE ORDERTIME = TO_DATE(" +
                            this.GetDateType(_tc0.TimeDT) + ", 'yyyymmddhh24miss') AND SOURCE = " + _tc0.Source + " AND ORDERIP = '" + _tc0.AddrStr + "'", out reader);

                        while (reader.Read())
                        {
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                cnt = int.Parse(reader[i].ToString());
                            }
                        }

                        if (reader != null)
                        {
                            reader.Dispose();
                        }
                    }

                    if (cnt > 0)
                    {
                        return;
                    }

                    reader = null;
                }
                catch (Exception ex)
                {
                    throw new Exception("같은 발령이 DB에 있는지 확인 - " + ex.Message);
                }

                try
                {
                    if ((_tc0.Addr & 0x00ffffff) == 0x00ffffff) //전체 발령
                    {
                        division = 1;
                    }
                    else if ((_tc0.Addr & 0x0000ffff) == 0x0000ffff) //시도개념의 발령 (ex. 울란바타르 전체 발령)
                    {
                        division = 2;
                    }
                    else if ((_tc0.Addr & 0x000000ff) == 0x000000ff) //시군개념의 발령 (ex. 바가노오르구 전체 발령)
                    {
                        division = 3;
                    }
                    else //개별 발령
                    {
                        division = 4;
                    }

                    //DB Insert 작업 시작
                    if (_tc0.Mode == REAL) //실제
                    {
                        this.oleDb.QueryData(
                            string.Format("INSERT INTO REALALARMORDERHIST VALUES(TO_DATE({0}, 'yyyymmddhh24miss'), {1}, {2}, {3}, null, {4}, {6}, '{5}')",
                            this.GetDateType(_tc0.TimeDT), _tc0.Media, _tc0.Kind, _tc0.Source, division, _tc0.AddrStr, _tc0.MsgNum));
                    }
                    else if (_tc0.Mode == TEST) //시험
                    {
                        this.oleDb.QueryData(
                            string.Format("INSERT INTO TESTALARMORDERHIST VALUES(TO_DATE({0}, 'yyyymmddhh24miss'), {1}, {2}, {3}, null, {4}, {6}, '{5}')",
                            this.GetDateType(_tc0.TimeDT), _tc0.Media, _tc0.Kind, _tc0.Source, division, _tc0.AddrStr, _tc0.MsgNum));
                    }
                    else if (_tc0.Mode == TRAIN) //훈련
                    {
                        this.oleDb.QueryData(
                            string.Format("INSERT INTO TRAINALARMORDERHIST VALUES(TO_DATE({0}, 'yyyymmddhh24miss'), {1}, {2}, {3}, null, {4}, {6}, '{5}')",
                            this.GetDateType(_tc0.TimeDT), _tc0.Media, _tc0.Kind, _tc0.Source, division, _tc0.AddrStr, _tc0.MsgNum));
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("DB Insert 작업 시작 - " + ex.Message);
                }

                try
                {
                    //결과 테이블 Insert 작업 시작
                    string tmpMode = string.Empty;

                    if (_tc0.Mode == REAL) //실제
                    {
                        tmpMode = "REALALARMRESULTHIST";
                    }
                    else if (_tc0.Mode == TEST) //시험
                    {
                        tmpMode = "TESTALARMRESULTHIST";
                    }
                    else if (_tc0.Mode == TRAIN) //훈련
                    {
                        tmpMode = "TRAINALARMRESULTHIST";
                    }

                    if (division == 1) //전체 발령
                    {
                        for (int i = 0; i < this.termDataList.Count; i++)
                        {
                            if (this.termDataList[i].TermKind == AlertTerm && this.termDataList[i].UseFlag == 1)
                            {
                                this.oleDb.QueryData(
                                    string.Format("INSERT INTO {0} VALUES(TO_DATE({1}, 'yyyymmddhh24miss'), {2}, {3}, {4}, null, {5}, null, '{6}')",
                                    tmpMode, this.GetDateType(_tc0.TimeDT), _tc0.Media, _tc0.Kind, _tc0.Source, this.termDataList[i].TermCode, _tc0.AddrStr));
                            }
                        }
                    }
                    else if (division == 2) //시도개념의 발령 (ex. 울란바타르 전체 발령)
                    {
                        int tmpProvCode = 0;

                        for (int i = 0; i < this.provDataList.Count; i++)
                        {
                            if ((AdengUtil.MakeStringToUintIp(this.provDataList[i].ProvIP)) == _tc0.Addr)
                            {
                                tmpProvCode = this.provDataList[i].ProvCode;
                                break;
                            }
                        }

                        for (int i = 0; i < this.termDataList.Count; i++)
                        {
                            if (this.termDataList[i].ProvCode == tmpProvCode)
                            {
                                if (this.termDataList[i].TermKind == AlertTerm && this.termDataList[i].UseFlag == 1)
                                {
                                    this.oleDb.QueryData(
                                        string.Format("INSERT INTO {0} VALUES(TO_DATE({1}, 'yyyymmddhh24miss'), {2}, {3}, {4}, null, {5}, null, '{6}')",
                                        tmpMode, this.GetDateType(_tc0.TimeDT), _tc0.Media, _tc0.Kind, _tc0.Source, this.termDataList[i].TermCode, _tc0.AddrStr));
                                }
                            }
                        }
                    }
                    else if (division == 3) //시군개념의 발령 (ex. 바가노오르구 전체 발령)
                    {
                        int tmpDistCode = 0;

                        for (int i = 0; i < this.distDataList.Count; i++)
                        {
                            if ((AdengUtil.MakeStringToUintIp(this.distDataList[i].DistIP)) == _tc0.Addr)
                            {
                                tmpDistCode = this.distDataList[i].DistCode;
                                break;
                            }
                        }

                        for (int i = 0; i < this.termDataList.Count; i++)
                        {
                            if (this.termDataList[i].DistCode == tmpDistCode)
                            {
                                if (this.termDataList[i].TermKind == AlertTerm && this.termDataList[i].UseFlag == 1)
                                {
                                    this.oleDb.QueryData(
                                        string.Format("INSERT INTO {0} VALUES(TO_DATE({1}, 'yyyymmddhh24miss'), {2}, {3}, {4}, null, {5}, null, '{6}')",
                                        tmpMode, this.GetDateType(_tc0.TimeDT), _tc0.Media, _tc0.Kind, _tc0.Source, this.termDataList[i].TermCode, _tc0.AddrStr));
                                }
                            }
                        }
                    }
                    else if (division == 4) //개별 발령
                    {
                        int tmpTermCode = this.GetTermCode(_tc0.AddrStr);

                        this.oleDb.QueryData(
                            string.Format("INSERT INTO {0} VALUES(TO_DATE({1}, 'yyyymmddhh24miss'), {2}, {3}, {4}, null, {5}, null, '{6}')",
                            tmpMode, this.GetDateType(_tc0.TimeDT), _tc0.Media, _tc0.Kind, _tc0.Source, tmpTermCode, _tc0.AddrStr));
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("결과 테이블 Insert 작업 시작 - " + ex.Message);
                }
            }
            else //개별 프로토콜 변경 된거 적용함.
            {
                for (int p = 0; p < _tc0.LstTermStr.Count; p++)
                {
                    try
                    {
                        //같은 발령이 DB에 있는지 확인
                        if (_tc0.Mode == REAL) //실제
                        {
                            this.oleDb.QueryData("SELECT COUNT(*) FROM REALALARMORDERHIST WHERE ORDERTIME = TO_DATE(" +
                                this.GetDateType(_tc0.TimeDT) + ", 'yyyymmddhh24miss') AND SOURCE = " + _tc0.Source + " AND ORDERIP = '" + _tc0.LstTermStr[p] + "'", out reader);

                            while (reader.Read())
                            {
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    cnt = int.Parse(reader[i].ToString());
                                }
                            }

                            if (reader != null)
                            {
                                reader.Dispose();
                            }
                        }
                        else if (_tc0.Mode == TEST) //시험
                        {
                            this.oleDb.QueryData("SELECT COUNT(*) FROM TESTALARMORDERHIST WHERE ORDERTIME = TO_DATE(" +
                                this.GetDateType(_tc0.TimeDT) + ", 'yyyymmddhh24miss') AND SOURCE = " + _tc0.Source + " AND ORDERIP = '" + _tc0.LstTermStr[p] + "'", out reader);

                            while (reader.Read())
                            {
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    cnt = int.Parse(reader[i].ToString());
                                }
                            }

                            if (reader != null)
                            {
                                reader.Dispose();
                            }
                        }
                        else if (_tc0.Mode == TRAIN) //훈련
                        {
                            this.oleDb.QueryData("SELECT COUNT(*) FROM TRAINALARMORDERHIST WHERE ORDERTIME = TO_DATE(" +
                                this.GetDateType(_tc0.TimeDT) + ", 'yyyymmddhh24miss') AND SOURCE = " + _tc0.Source + " AND ORDERIP = '" + _tc0.LstTermStr[p] + "'", out reader);

                            while (reader.Read())
                            {
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    cnt = int.Parse(reader[i].ToString());
                                }
                            }

                            if (reader != null)
                            {
                                reader.Dispose();
                            }
                        }

                        if (cnt > 0)
                        {
                            return;
                        }

                        reader = null;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("같은 발령이 DB에 있는지 확인 - " + ex.Message);
                    }

                    try
                    {
                        if ((_tc0.LstTerm[p] & 0x00ffffff) == 0x00ffffff) //전체 발령
                        {
                            division = 1;
                        }
                        else if ((_tc0.LstTerm[p] & 0x0000ffff) == 0x0000ffff) //시도개념의 발령 (ex. 울란바타르 전체 발령)
                        {
                            division = 2;
                        }
                        else if ((_tc0.LstTerm[p] & 0x000000ff) == 0x000000ff) //시군개념의 발령 (ex. 바가노오르구 전체 발령)
                        {
                            division = 3;
                        }
                        else //개별 발령
                        {
                            division = 4;
                        }

                        //DB Insert 작업 시작
                        if (_tc0.Mode == REAL) //실제
                        {
                            this.oleDb.QueryData(
                                string.Format("INSERT INTO REALALARMORDERHIST VALUES(TO_DATE({0}, 'yyyymmddhh24miss'), {1}, {2}, {3}, null, {4}, {6}, '{5}')",
                                this.GetDateType(_tc0.TimeDT), _tc0.Media, _tc0.Kind, _tc0.Source, division, _tc0.LstTermStr[p], _tc0.MsgNum));
                        }
                        else if (_tc0.Mode == TEST) //시험
                        {
                            this.oleDb.QueryData(
                                string.Format("INSERT INTO TESTALARMORDERHIST VALUES(TO_DATE({0}, 'yyyymmddhh24miss'), {1}, {2}, {3}, null, {4}, {6}, '{5}')",
                                this.GetDateType(_tc0.TimeDT), _tc0.Media, _tc0.Kind, _tc0.Source, division, _tc0.LstTermStr[p], _tc0.MsgNum));
                        }
                        else if (_tc0.Mode == TRAIN) //훈련
                        {
                            this.oleDb.QueryData(
                                string.Format("INSERT INTO TRAINALARMORDERHIST VALUES(TO_DATE({0}, 'yyyymmddhh24miss'), {1}, {2}, {3}, null, {4}, {6}, '{5}')",
                                this.GetDateType(_tc0.TimeDT), _tc0.Media, _tc0.Kind, _tc0.Source, division, _tc0.LstTermStr[p], _tc0.MsgNum));
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("DB Insert 작업 시작 - " + ex.Message);
                    }

                    try
                    {
                        //결과 테이블 Insert 작업 시작
                        string tmpMode = string.Empty;

                        if (_tc0.Mode == REAL) //실제
                        {
                            tmpMode = "REALALARMRESULTHIST";
                        }
                        else if (_tc0.Mode == TEST) //시험
                        {
                            tmpMode = "TESTALARMRESULTHIST";
                        }
                        else if (_tc0.Mode == TRAIN) //훈련
                        {
                            tmpMode = "TRAINALARMRESULTHIST";
                        }

                        if (division == 1) //전체 발령
                        {
                            for (int i = 0; i < this.termDataList.Count; i++)
                            {
                                if (this.termDataList[i].TermKind == AlertTerm && this.termDataList[i].UseFlag == 1)
                                {
                                    this.oleDb.QueryData(
                                        string.Format("INSERT INTO {0} VALUES(TO_DATE({1}, 'yyyymmddhh24miss'), {2}, {3}, {4}, null, {5}, null, '{6}')",
                                        tmpMode, this.GetDateType(_tc0.TimeDT), _tc0.Media, _tc0.Kind, _tc0.Source, this.termDataList[i].TermCode, _tc0.LstTermStr[p]));
                                }
                            }
                        }
                        else if (division == 2) //시도개념의 발령 (ex. 울란바타르 전체 발령)
                        {
                            int tmpProvCode = 0;

                            for (int i = 0; i < this.provDataList.Count; i++)
                            {
                                if ((AdengUtil.MakeStringToUintIp(this.provDataList[i].ProvIP)) == _tc0.LstTerm[p])
                                {
                                    tmpProvCode = this.provDataList[i].ProvCode;
                                    break;
                                }
                            }

                            for (int i = 0; i < this.termDataList.Count; i++)
                            {
                                if (this.termDataList[i].ProvCode == tmpProvCode)
                                {
                                    if (this.termDataList[i].TermKind == AlertTerm && this.termDataList[i].UseFlag == 1)
                                    {
                                        this.oleDb.QueryData(
                                            string.Format("INSERT INTO {0} VALUES(TO_DATE({1}, 'yyyymmddhh24miss'), {2}, {3}, {4}, null, {5}, null, '{6}')",
                                            tmpMode, this.GetDateType(_tc0.TimeDT), _tc0.Media, _tc0.Kind, _tc0.Source, this.termDataList[i].TermCode, _tc0.LstTermStr[p]));
                                    }
                                }
                            }
                        }
                        else if (division == 3) //시군개념의 발령 (ex. 바가노오르구 전체 발령)
                        {
                            int tmpDistCode = 0;

                            for (int i = 0; i < this.distDataList.Count; i++)
                            {
                                if ((AdengUtil.MakeStringToUintIp(this.distDataList[i].DistIP)) == _tc0.LstTerm[p])
                                {
                                    tmpDistCode = this.distDataList[i].DistCode;
                                    break;
                                }
                            }

                            for (int i = 0; i < this.termDataList.Count; i++)
                            {
                                if (this.termDataList[i].DistCode == tmpDistCode)
                                {
                                    if (this.termDataList[i].TermKind == AlertTerm && this.termDataList[i].UseFlag == 1)
                                    {
                                        this.oleDb.QueryData(
                                            string.Format("INSERT INTO {0} VALUES(TO_DATE({1}, 'yyyymmddhh24miss'), {2}, {3}, {4}, null, {5}, null, '{6}')",
                                            tmpMode, this.GetDateType(_tc0.TimeDT), _tc0.Media, _tc0.Kind, _tc0.Source, this.termDataList[i].TermCode, _tc0.LstTermStr[p]));
                                    }
                                }
                            }
                        }
                        else if (division == 4) //개별 발령
                        {
                            int tmpTermCode = this.GetTermCode(_tc0.LstTermStr[p]);

                            this.oleDb.QueryData(
                                string.Format("INSERT INTO {0} VALUES(TO_DATE({1}, 'yyyymmddhh24miss'), {2}, {3}, {4}, null, {5}, null, '{6}')",
                                tmpMode, this.GetDateType(_tc0.TimeDT), _tc0.Media, _tc0.Kind, _tc0.Source, tmpTermCode, _tc0.LstTermStr[p]));
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("결과 테이블 Insert 작업 시작 - " + ex.Message);
                    }
                }
            }

            //오라클 연결관리
            this.OracleConMng(this.OraQueryCount);
            this.OraQueryCount++;
        }
        #endregion

        #region TC 1 (경보 발령 결과)
        public void SetDB_TC1(PrtCmd01 _tc1)
        {
            OleDbDataReader reader = null;
            string tmpMode = string.Empty;
            int cnt = 0;

            if (_tc1.Mode == REAL) //실제
            {
                tmpMode = "REALALARMRESULTHIST";
            }
            else if (_tc1.Mode == TEST) //시험
            {
                tmpMode = "TESTALARMRESULTHIST";
            }
            else if (_tc1.Mode == TRAIN) //훈련
            {
                tmpMode = "TRAINALARMRESULTHIST";
            }

            try
            {
                //결과 테이블에 있는지 확인
                this.oleDb.QueryData("SELECT COUNT(*) FROM " + tmpMode + " WHERE ORDERTIME =  TO_DATE(" +
                    this.GetDateType(_tc1.TimeDT) + ", 'yyyymmddhh24miss') AND SOURCE = " + _tc1.Source + " AND TERMCODE = " + this.GetTermCode(_tc1.AddrStr), out reader);

                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        cnt = int.Parse(reader[i].ToString());
                    }
                }

                if (reader != null)
                {
                    reader.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("결과 테이블에 있는지 확인 - " + ex.Message);
            }

            //있다면 업데이트, 없으면 Pass
            if (cnt > 0)
            {
                try
                {
                    this.oleDb.QueryData(
                        string.Format(
                        "UPDATE {0} SET RESULTTIME = TO_DATE({1}, 'yyyymmddhh24miss'), SPEAKERRESULT = {2} WHERE ORDERTIME = TO_DATE({3}, 'yyyymmddhh24miss') AND SOURCE = {4} AND TERMCODE = {5}",
                        tmpMode, this.GetDateType(DateTime.Now), _tc1.Result, this.GetDateType(_tc1.TimeDT), _tc1.Source, this.GetTermCode(_tc1.AddrStr)));
                }
                catch (Exception ex)
                {
                    throw new Exception("있다면 업데이트, 없으면 Pass - " + ex.Message);
                }
            }

            //오라클 연결관리
            this.OracleConMng(this.OraQueryCount);
            this.OraQueryCount++;
        }
        #endregion

        #region TC 2 (단말에 보고할 정보가 있는지 요구, DB 저장 안함)
        public void SetDB_TC2(PrtCmd02 _tc2)
        {
            //오라클 연결관리
            this.OracleConMng(this.OraQueryCount);
            this.OraQueryCount++;
        }
        #endregion

        #region TC 3 (단말 제어)
        public void SetDB_TC3(PrtCmd03 _tc3)
        {
            //프로토콜과 DB의 code값이 1씩 차이가 나기 때문에 controlItem의 code값은 프로토콜에 +1을 해서 저장한다.

            //setvalue 값 설정
            string tmpValue = string.Empty;

            if (_tc3.Kind == 2) //앰프 사용 변경
            {
                tmpValue = _tc3.SetAmpUse.ToString();
            }
            else if (_tc3.Kind == 3) //앰프 레벨 변경
            {
                tmpValue = _tc3.SetOuputLvl.ToString();
            }
            else if (_tc3.Kind == 4) //현재시각 변경
            {
                tmpValue = this.GetDateType(_tc3.SetTimeDT);
            }
            else if (_tc3.Kind == 5) //리셋
            {
                tmpValue = _tc3.SetReset.ToString();
            }
            else if (_tc3.Kind == 6) //아날로그 상하한 설정
            {
                tmpValue = string.Format("{0}/{1}/{2}/{3}/{4}/{5}/{6}/{7}",
                    _tc3.SetTempHigh, _tc3.SetTempLow, _tc3.SetAcHigh, _tc3.SetAcLow, _tc3.SetDcHigh, _tc3.SetDcLow, _tc3.SetHumiHigh, _tc3.SetHumiLow);
            }

            try
            {
                DateTime tmpDT = DateTime.Now;

                if ((_tc3.Addr & 0x00ffffff) == 0x00ffffff) //전체 발령
                {
                    for (int i = 0; i < this.termDataList.Count; i++)
                    {
                        this.oleDb.QueryData(
                            string.Format("INSERT INTO TERMCONTROLHIST VALUES({0}, TO_DATE({1}, 'yyyymmddhh24miss'), {2}, {3})",
                            _tc3.Kind + 1, this.GetDateType(tmpDT), this.termDataList[i].TermCode, tmpValue));
                    }
                }
                else if ((_tc3.Addr & 0x0000ffff) == 0x0000ffff) //시도개념의 발령 (ex. 울란바타르 전체 발령)
                {
                    int tmpProvCode = 0;

                    for (int i = 0; i < this.provDataList.Count; i++)
                    {
                        if ((AdengUtil.MakeStringToUintIp(this.provDataList[i].ProvIP)) == (_tc3.Addr & 0x0000ffff))
                        {
                            tmpProvCode = this.provDataList[i].ProvCode;
                            break;
                        }
                    }

                    for (int i = 0; i < this.termDataList.Count; i++)
                    {
                        if (this.termDataList[i].ProvCode == tmpProvCode)
                        {
                            this.oleDb.QueryData(
                                string.Format("INSERT INTO TERMCONTROLHIST VALUES({0}, TO_DATE({1}, 'yyyymmddhh24miss'), {2}, {3})",
                                _tc3.Kind + 1, this.GetDateType(tmpDT), this.termDataList[i].TermCode, tmpValue));
                        }
                    }
                }
                else if ((_tc3.Addr & 0x000000ff) == 0x000000ff) //시군개념의 발령 (ex. 바가노오르구 전체 발령)
                {
                    int tmpDistCode = 0;

                    for (int i = 0; i < this.distDataList.Count; i++)
                    {
                        if ((AdengUtil.MakeStringToUintIp(this.distDataList[i].DistIP)) == (_tc3.Addr & 0x000000ff))
                        {
                            tmpDistCode = this.distDataList[i].DistCode;
                            break;
                        }
                    }

                    for (int i = 0; i < this.termDataList.Count; i++)
                    {
                        if (this.termDataList[i].DistCode == tmpDistCode)
                        {
                            this.oleDb.QueryData(
                                string.Format("INSERT INTO TERMCONTROLHIST VALUES({0}, TO_DATE({1}, 'yyyymmddhh24miss'), {2}, {3})",
                                _tc3.Kind + 1, this.GetDateType(tmpDT), this.termDataList[i].TermCode, tmpValue));
                        }
                    }
                }
                else //개별 발령
                {
                    int tmpTermCode = this.GetTermCode(_tc3.AddrStr);

                    this.oleDb.QueryData(
                        string.Format("INSERT INTO TERMCONTROLHIST VALUES({0}, TO_DATE({1}, 'yyyymmddhh24miss'), {2}, {3})",
                        _tc3.Kind + 1, this.GetDateType(tmpDT), tmpTermCode, tmpValue));
                }
            }
            catch (Exception ex)
            {
                throw new Exception("SetDB_TC3 - " + ex.Message);
            }
            //오라클 연결관리
            this.OracleConMng(this.OraQueryCount);
            this.OraQueryCount++;
        }
        #endregion

        #region TC 4 (단말이 올리는 상태, DB 저장 안함)
        public void SetDB_TC4(PrtCmd04 _tc4)
        {
            //오라클 연결관리
            this.OracleConMng(this.OraQueryCount);
            this.OraQueryCount++;
        }
        #endregion

        #region TC 5 (방송 발령)
        public void SetDB_TC5(PrtCmd05 _tc5)
        {
            OleDbDataReader reader = null;
            int cnt = 0;
            int division = 0;

            if (((_tc5.Addr & 0x00ffffff) == 0x00ffffff) || ((_tc5.Addr & 0x0000ffff) == 0x0000ffff)) //전체 발령, 개별프로토콜 변경되서 적용됨
            {
                try
                {
                    //같은 발령이 DB에 있는지 확인
                    if (_tc5.Mode == REAL) //실제
                    {
                        this.oleDb.QueryData("SELECT COUNT(*) FROM REALBROADORDERHIST WHERE ORDERTIME = TO_DATE(" +
                            this.GetDateType(_tc5.TimeDT) + ", 'yyyymmddhh24miss') AND SOURCE = " + _tc5.Source + " AND ORDERIP = '" + _tc5.AddrStr + "'", out reader);

                        while (reader.Read())
                        {
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                cnt = int.Parse(reader[i].ToString());
                            }
                        }

                        if (reader != null)
                        {
                            reader.Dispose();
                        }
                    }
                    else if (_tc5.Mode == TEST) //시험
                    {
                        this.oleDb.QueryData("SELECT COUNT(*) FROM TESTBROADORDERHIST WHERE ORDERTIME = TO_DATE(" +
                            this.GetDateType(_tc5.TimeDT) + ", 'yyyymmddhh24miss') AND SOURCE = " + _tc5.Source + " AND ORDERIP = '" + _tc5.AddrStr + "'", out reader);

                        while (reader.Read())
                        {
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                cnt = int.Parse(reader[i].ToString());
                            }
                        }

                        if (reader != null)
                        {
                            reader.Dispose();
                        }
                    }
                    else if (_tc5.Mode == TRAIN) //훈련
                    {
                        this.oleDb.QueryData("SELECT COUNT(*) FROM TRAINBROADORDERHIST WHERE ORDERTIME = TO_DATE(" +
                            this.GetDateType(_tc5.TimeDT) + ", 'yyyymmddhh24miss') AND SOURCE = " + _tc5.Source + " AND ORDERIP = '" + _tc5.AddrStr + "'", out reader);

                        while (reader.Read())
                        {
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                cnt = int.Parse(reader[i].ToString());
                            }
                        }

                        if (reader != null)
                        {
                            reader.Dispose();
                        }
                    }

                    if (cnt > 0)
                    {
                        return;
                    }

                    reader = null;
                }
                catch (Exception ex)
                {
                    throw new Exception("같은 방송 발령이 DB에 있는지 확인 - " + ex.Message);
                }

                try
                {
                    if ((_tc5.Addr & 0x00ffffff) == 0x00ffffff) //전체 발령
                    {
                        division = 1;
                    }
                    else if ((_tc5.Addr & 0x0000ffff) == 0x0000ffff) //시도개념의 발령 (ex. 울란바타르 전체 발령)
                    {
                        division = 2;
                    }
                    else if ((_tc5.Addr & 0x000000ff) == 0x000000ff) //시군개념의 발령 (ex. 바가노오르구 전체 발령)
                    {
                        division = 3;
                    }
                    else //개별 발령
                    {
                        division = 4;
                    }

                    //DB Insert 작업 시작
                    if (_tc5.Mode == REAL) //실제
                    {
                        this.oleDb.QueryData(
                            string.Format("INSERT INTO REALBROADORDERHIST VALUES(TO_DATE({0}, 'yyyymmddhh24miss'), {1}, {2}, {3}, {4}, {6}, '{5}')",
                            this.GetDateType(_tc5.TimeDT), _tc5.Media, _tc5.Kind, _tc5.Source, division, _tc5.AddrStr, _tc5.MsgNum));
                    }
                    else if (_tc5.Mode == TEST) //시험
                    {
                        this.oleDb.QueryData(
                            string.Format("INSERT INTO TESTBROADORDERHIST VALUES(TO_DATE({0}, 'yyyymmddhh24miss'), {1}, {2}, {3}, {4}, {6}, '{5}')",
                            this.GetDateType(_tc5.TimeDT), _tc5.Media, _tc5.Kind, _tc5.Source, division, _tc5.AddrStr, _tc5.MsgNum));
                    }
                    else if (_tc5.Mode == TRAIN) //훈련
                    {
                        this.oleDb.QueryData(
                            string.Format("INSERT INTO TRAINBROADORDERHIST VALUES(TO_DATE({0}, 'yyyymmddhh24miss'), {1}, {2}, {3}, {4}, {6}, '{5}')",
                            this.GetDateType(_tc5.TimeDT), _tc5.Media, _tc5.Kind, _tc5.Source, division, _tc5.AddrStr, _tc5.MsgNum));
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("방송 발령 DB Insert 작업 시작 - " + ex.Message);
                }

                try
                {
                    //결과 테이블 Insert 작업 시작
                    string tmpMode = string.Empty;

                    if (_tc5.Mode == REAL) //실제
                    {
                        tmpMode = "REALBROADRESULTHIST";
                    }
                    else if (_tc5.Mode == TEST) //시험
                    {
                        tmpMode = "TESTBROADRESULTHIST";
                    }
                    else if (_tc5.Mode == TRAIN) //훈련
                    {
                        tmpMode = "TRAINBROADRESULTHIST";
                    }

                    if (division == 1) //전체 발령
                    {
                        for (int i = 0; i < this.termDataList.Count; i++)
                        {
                            if (this.termDataList[i].TermKind == BroadTerm && this.termDataList[i].UseFlag == 1)
                            {
                                this.oleDb.QueryData(
                                    string.Format("INSERT INTO {0} VALUES(TO_DATE({1}, 'yyyymmddhh24miss'), {2}, {3}, {4}, null, {5}, null, '{6}')",
                                    tmpMode, this.GetDateType(_tc5.TimeDT), _tc5.Media, _tc5.Kind, _tc5.Source, this.termDataList[i].TermCode, _tc5.AddrStr));
                            }
                        }
                    }
                    else if (division == 2) //시도개념의 발령 (ex. 울란바타르 전체 발령)
                    {
                        int tmpProvCode = 0;

                        for (int i = 0; i < this.provDataList.Count; i++)
                        {
                            if ((AdengUtil.MakeStringToUintIp(this.provDataList[i].ProvIP)) == _tc5.Addr)
                            {
                                tmpProvCode = this.provDataList[i].ProvCode;
                                break;
                            }
                        }

                        for (int i = 0; i < this.termDataList.Count; i++)
                        {
                            if (this.termDataList[i].ProvCode == tmpProvCode)
                            {
                                if (this.termDataList[i].TermKind == BroadTerm && this.termDataList[i].UseFlag == 1)
                                {
                                    this.oleDb.QueryData(
                                        string.Format("INSERT INTO {0} VALUES(TO_DATE({1}, 'yyyymmddhh24miss'), {2}, {3}, {4}, null, {5}, null, '{6}')",
                                        tmpMode, this.GetDateType(_tc5.TimeDT), _tc5.Media, _tc5.Kind, _tc5.Source, this.termDataList[i].TermCode, _tc5.AddrStr));
                                }
                            }
                        }
                    }
                    else if (division == 3) //시군개념의 발령 (ex. 바가노오르구 전체 발령)
                    {
                        int tmpDistCode = 0;

                        for (int i = 0; i < this.distDataList.Count; i++)
                        {
                            if ((AdengUtil.MakeStringToUintIp(this.distDataList[i].DistIP)) == _tc5.Addr)
                            {
                                tmpDistCode = this.distDataList[i].DistCode;
                                break;
                            }
                        }

                        for (int i = 0; i < this.termDataList.Count; i++)
                        {
                            if (this.termDataList[i].DistCode == tmpDistCode)
                            {
                                if (this.termDataList[i].TermKind == BroadTerm && this.termDataList[i].UseFlag == 1)
                                {
                                    this.oleDb.QueryData(
                                        string.Format("INSERT INTO {0} VALUES(TO_DATE({1}, 'yyyymmddhh24miss'), {2}, {3}, {4}, null, {5}, null, '{6}')",
                                        tmpMode, this.GetDateType(_tc5.TimeDT), _tc5.Media, _tc5.Kind, _tc5.Source, this.termDataList[i].TermCode, _tc5.AddrStr));
                                }
                            }
                        }
                    }
                    else if (division == 4) //개별 발령
                    {
                        int tmpTermCode = this.GetTermCode(_tc5.AddrStr);

                        this.oleDb.QueryData(
                            string.Format("INSERT INTO {0} VALUES(TO_DATE({1}, 'yyyymmddhh24miss'), {2}, {3}, {4}, null, {5}, null, '{6}')",
                            tmpMode, this.GetDateType(_tc5.TimeDT), _tc5.Media, _tc5.Kind, _tc5.Source, tmpTermCode, _tc5.AddrStr));
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("방송발령 결과 테이블 Insert 작업 시작 - " + ex.Message);
                }
            }
            else //개별 프로토콜 변경된거 적용함.
            {
                for (int p = 0; p < _tc5.LstTermStr.Count; p++)
                {
                    try
                    {
                        //같은 발령이 DB에 있는지 확인
                        if (_tc5.Mode == REAL) //실제
                        {
                            this.oleDb.QueryData("SELECT COUNT(*) FROM REALBROADORDERHIST WHERE ORDERTIME = TO_DATE(" +
                                this.GetDateType(_tc5.TimeDT) + ", 'yyyymmddhh24miss') AND SOURCE = " + _tc5.Source + " AND ORDERIP = '" + _tc5.LstTermStr[p] + "'", out reader);

                            while (reader.Read())
                            {
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    cnt = int.Parse(reader[i].ToString());
                                }
                            }

                            if (reader != null)
                            {
                                reader.Dispose();
                            }
                        }
                        else if (_tc5.Mode == TEST) //시험
                        {
                            this.oleDb.QueryData("SELECT COUNT(*) FROM TESTBROADORDERHIST WHERE ORDERTIME = TO_DATE(" +
                                this.GetDateType(_tc5.TimeDT) + ", 'yyyymmddhh24miss') AND SOURCE = " + _tc5.Source + " AND ORDERIP = '" + _tc5.LstTermStr[p] + "'", out reader);

                            while (reader.Read())
                            {
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    cnt = int.Parse(reader[i].ToString());
                                }
                            }

                            if (reader != null)
                            {
                                reader.Dispose();
                            }
                        }
                        else if (_tc5.Mode == TRAIN) //훈련
                        {
                            this.oleDb.QueryData("SELECT COUNT(*) FROM TRAINBROADORDERHIST WHERE ORDERTIME = TO_DATE(" +
                                this.GetDateType(_tc5.TimeDT) + ", 'yyyymmddhh24miss') AND SOURCE = " + _tc5.Source + " AND ORDERIP = '" + _tc5.LstTermStr[p] + "'", out reader);

                            while (reader.Read())
                            {
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    cnt = int.Parse(reader[i].ToString());
                                }
                            }

                            if (reader != null)
                            {
                                reader.Dispose();
                            }
                        }

                        if (cnt > 0)
                        {
                            return;
                        }

                        reader = null;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("같은 방송 발령이 DB에 있는지 확인 - " + ex.Message);
                    }

                    try
                    {
                        if ((_tc5.LstTerm[p] & 0x00ffffff) == 0x00ffffff) //전체 발령
                        {
                            division = 1;
                        }
                        else if ((_tc5.LstTerm[p] & 0x0000ffff) == 0x0000ffff) //시도개념의 발령 (ex. 울란바타르 전체 발령)
                        {
                            division = 2;
                        }
                        else if ((_tc5.LstTerm[p] & 0x000000ff) == 0x000000ff) //시군개념의 발령 (ex. 바가노오르구 전체 발령)
                        {
                            division = 3;
                        }
                        else //개별 발령
                        {
                            division = 4;
                        }

                        //DB Insert 작업 시작
                        if (_tc5.Mode == REAL) //실제
                        {
                            this.oleDb.QueryData(
                                string.Format("INSERT INTO REALBROADORDERHIST VALUES(TO_DATE({0}, 'yyyymmddhh24miss'), {1}, {2}, {3}, {4}, {6}, '{5}')",
                                this.GetDateType(_tc5.TimeDT), _tc5.Media, _tc5.Kind, _tc5.Source, division, _tc5.LstTermStr[p], _tc5.MsgNum));
                        }
                        else if (_tc5.Mode == TEST) //시험
                        {
                            this.oleDb.QueryData(
                                string.Format("INSERT INTO TESTBROADORDERHIST VALUES(TO_DATE({0}, 'yyyymmddhh24miss'), {1}, {2}, {3}, {4}, {6}, '{5}')",
                                this.GetDateType(_tc5.TimeDT), _tc5.Media, _tc5.Kind, _tc5.Source, division, _tc5.LstTermStr[p], _tc5.MsgNum));
                        }
                        else if (_tc5.Mode == TRAIN) //훈련
                        {
                            this.oleDb.QueryData(
                                string.Format("INSERT INTO TRAINBROADORDERHIST VALUES(TO_DATE({0}, 'yyyymmddhh24miss'), {1}, {2}, {3}, {4}, {6}, '{5}')",
                                this.GetDateType(_tc5.TimeDT), _tc5.Media, _tc5.Kind, _tc5.Source, division, _tc5.LstTermStr[p], _tc5.MsgNum));
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("방송 발령 DB Insert 작업 시작 - " + ex.Message);
                    }

                    try
                    {
                        //결과 테이블 Insert 작업 시작
                        string tmpMode = string.Empty;

                        if (_tc5.Mode == REAL) //실제
                        {
                            tmpMode = "REALBROADRESULTHIST";
                        }
                        else if (_tc5.Mode == TEST) //시험
                        {
                            tmpMode = "TESTBROADRESULTHIST";
                        }
                        else if (_tc5.Mode == TRAIN) //훈련
                        {
                            tmpMode = "TRAINBROADRESULTHIST";
                        }

                        if (division == 1) //전체 발령
                        {
                            for (int i = 0; i < this.termDataList.Count; i++)
                            {
                                if (this.termDataList[i].TermKind == BroadTerm && this.termDataList[i].UseFlag == 1)
                                {
                                    this.oleDb.QueryData(
                                        string.Format("INSERT INTO {0} VALUES(TO_DATE({1}, 'yyyymmddhh24miss'), {2}, {3}, {4}, null, {5}, null, '{6}')",
                                        tmpMode, this.GetDateType(_tc5.TimeDT), _tc5.Media, _tc5.Kind, _tc5.Source, this.termDataList[i].TermCode, _tc5.LstTermStr[p]));
                                }
                            }
                        }
                        else if (division == 2) //시도개념의 발령 (ex. 울란바타르 전체 발령)
                        {
                            int tmpProvCode = 0;

                            for (int i = 0; i < this.provDataList.Count; i++)
                            {
                                if ((AdengUtil.MakeStringToUintIp(this.provDataList[i].ProvIP)) == _tc5.LstTerm[p])
                                {
                                    tmpProvCode = this.provDataList[i].ProvCode;
                                    break;
                                }
                            }

                            for (int i = 0; i < this.termDataList.Count; i++)
                            {
                                if (this.termDataList[i].ProvCode == tmpProvCode)
                                {
                                    if (this.termDataList[i].TermKind == BroadTerm && this.termDataList[i].UseFlag == 1)
                                    {
                                        this.oleDb.QueryData(
                                            string.Format("INSERT INTO {0} VALUES(TO_DATE({1}, 'yyyymmddhh24miss'), {2}, {3}, {4}, null, {5}, null, '{6}')",
                                            tmpMode, this.GetDateType(_tc5.TimeDT), _tc5.Media, _tc5.Kind, _tc5.Source, this.termDataList[i].TermCode, _tc5.LstTermStr[p]));
                                    }
                                }
                            }
                        }
                        else if (division == 3) //시군개념의 발령 (ex. 바가노오르구 전체 발령)
                        {
                            int tmpDistCode = 0;

                            for (int i = 0; i < this.distDataList.Count; i++)
                            {
                                if ((AdengUtil.MakeStringToUintIp(this.distDataList[i].DistIP)) == _tc5.LstTerm[p])
                                {
                                    tmpDistCode = this.distDataList[i].DistCode;
                                    break;
                                }
                            }

                            for (int i = 0; i < this.termDataList.Count; i++)
                            {
                                if (this.termDataList[i].DistCode == tmpDistCode)
                                {
                                    if (this.termDataList[i].TermKind == BroadTerm && this.termDataList[i].UseFlag == 1)
                                    {
                                        this.oleDb.QueryData(
                                            string.Format("INSERT INTO {0} VALUES(TO_DATE({1}, 'yyyymmddhh24miss'), {2}, {3}, {4}, null, {5}, null, '{6}')",
                                            tmpMode, this.GetDateType(_tc5.TimeDT), _tc5.Media, _tc5.Kind, _tc5.Source, this.termDataList[i].TermCode, _tc5.LstTermStr[p]));
                                    }
                                }
                            }
                        }
                        else if (division == 4) //개별 발령
                        {
                            int tmpTermCode = this.GetTermCode(_tc5.LstTermStr[p]);

                            this.oleDb.QueryData(
                                string.Format("INSERT INTO {0} VALUES(TO_DATE({1}, 'yyyymmddhh24miss'), {2}, {3}, {4}, null, {5}, null, '{6}')",
                                tmpMode, this.GetDateType(_tc5.TimeDT), _tc5.Media, _tc5.Kind, _tc5.Source, tmpTermCode, _tc5.LstTermStr[p]));
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("방송발령 결과 테이블 Insert 작업 시작 - " + ex.Message);
                    }
                }
            }

            //오라클 연결관리
            this.OracleConMng(this.OraQueryCount);
            this.OraQueryCount++;
        }
        #endregion

        #region TC 6 (방송 발령 결과)
        public void SetDB_TC6(PrtCmd06 _tc6)
        {
            OleDbDataReader reader = null;
            string tmpMode = string.Empty;
            int cnt = 0;

            if (_tc6.Mode == REAL) //실제
            {
                tmpMode = "REALBROADRESULTHIST";
            }
            else if (_tc6.Mode == TEST) //시험
            {
                tmpMode = "TESTBROADRESULTHIST";
            }
            else if (_tc6.Mode == TRAIN) //훈련
            {
                tmpMode = "TRAINBROADRESULTHIST";
            }

            try
            {
                //결과 테이블에 있는지 확인
                this.oleDb.QueryData("SELECT COUNT(*) FROM " + tmpMode + " WHERE ORDERTIME =  TO_DATE(" +
                    this.GetDateType(_tc6.TimeDT) + ", 'yyyymmddhh24miss') AND SOURCE = " + _tc6.Source + " AND TERMCODE = " + this.GetTermCode(_tc6.AddrStr), out reader);

                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        cnt = int.Parse(reader[i].ToString());
                    }
                }

                if (reader != null)
                {
                    reader.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("방송발령 결과 테이블에 있는지 확인 - " + ex.Message);
            }

            //있다면 업데이트, 없으면 Pass
            if (cnt > 0)
            {
                try
                {
                    this.oleDb.QueryData(
                        string.Format(
                        "UPDATE {0} SET RESULTTIME = TO_DATE({1}, 'yyyymmddhh24miss'), SPEAKERRESULT = {2} WHERE ORDERTIME = TO_DATE({3}, 'yyyymmddhh24miss') AND SOURCE = {4} AND TERMCODE = {5}",
                        tmpMode, this.GetDateType(DateTime.Now), _tc6.Result, this.GetDateType(_tc6.TimeDT), _tc6.Source, this.GetTermCode(_tc6.AddrStr)));
                }
                catch (Exception ex)
                {
                    throw new Exception("방송발령 결과 있다면 업데이트, 없으면 Pass - " + ex.Message);
                }
            }

            //오라클 연결관리
            this.OracleConMng(this.OraQueryCount);
            this.OraQueryCount++;
        }
        #endregion

        #region TC 7 (자막 문안 전송)
        public void SetDB_TC7(PrtCmd07 _tc7)
        {
            if (((_tc7.Addr & 0x00ffffff) == 0x00ffffff) || ((_tc7.Addr & 0x0000ffff) == 0x0000ffff)) //전체 발령, 개별프로토콜 변경되서 적용됨
            {
                try
                {
                    //DB Insert 작업 시작
                    this.oleDb.QueryData(
                        string.Format("INSERT INTO TVCAPTIONMENTHIST VALUES(TO_DATE({0}, 'yyyymmddhh24miss'), {1}, {2}, {3}, {4}, '{5}', '{6}')",
                        this.GetDateType(_tc7.TimeDT), _tc7.Source, _tc7.Mode, _tc7.Kind, _tc7.RepeatNum, _tc7.Text, _tc7.AddrStr));
                }
                catch (Exception ex)
                {
                    throw new Exception("SetDB_TC7.DB Insert 작업 시작 - " + ex.Message);
                }
            }
            else //개별 프로토콜 변경된 것 반영함.
            {
                for (int i = 0; i < _tc7.LstTermStr.Count; i++)
                {
                    try
                    {
                        //DB Insert 작업 시작
                        this.oleDb.QueryData(
                            string.Format("INSERT INTO TVCAPTIONMENTHIST VALUES(TO_DATE({0}, 'yyyymmddhh24miss'), {1}, {2}, {3}, {4}, '{5}', '{6}')",
                            this.GetDateType(_tc7.TimeDT), _tc7.Source, _tc7.Mode, _tc7.Kind, _tc7.RepeatNum, _tc7.Text, _tc7.LstTermStr[i]));
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("SetDB_TC7.DB Insert 작업 시작 - " + ex.Message);
                    }
                }
            }

            //오라클 연결관리
            this.OracleConMng(this.OraQueryCount);
            this.OraQueryCount++;
        }
        #endregion

        #region TC 8 (전광판 문안 전송)
        public void SetDB_TC8(PrtCmd08 _tc8)
        {
            if (((_tc8.Addr & 0x00ffffff) == 0x00ffffff) || ((_tc8.Addr & 0x0000ffff) == 0x0000ffff)) //전체 발령, 개별프로토콜 변경되서 적용됨
            {
                try
                {
                    if (_tc8.Mode != 3)
                    {
                        //DB Insert 작업 시작
                        this.oleDb.QueryData(
                            string.Format("INSERT INTO DISPLAYMENTHIST VALUES(TO_DATE({0}, 'yyyymmddhh24miss'), {1}, {2}, '{3}', '{4}')",
                            this.GetDateType(_tc8.TimeDT), _tc8.Mode, _tc8.ShowTime, _tc8.Text, _tc8.AddrStr));
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("SetDB_TC8.DB Insert 작업 시작 - " + ex.Message);
                }
            }
            else
            {
                for (int i = 0; i < _tc8.LstTermStr.Count; i++)
                {
                    try
                    {
                        if (_tc8.Mode != 3)
                        {
                            //DB Insert 작업 시작
                            this.oleDb.QueryData(
                                string.Format("INSERT INTO DISPLAYMENTHIST VALUES(TO_DATE({0}, 'yyyymmddhh24miss'), {1}, {2}, '{3}', '{4}')",
                                this.GetDateType(_tc8.TimeDT), _tc8.Mode, _tc8.ShowTime, _tc8.Text, _tc8.LstTermStr[i]));
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("SetDB_TC8.DB Insert 작업 시작 - " + ex.Message);
                    }
                }
            }

            //오라클 연결관리
            this.OracleConMng(this.OraQueryCount);
            this.OraQueryCount++;
        }
        #endregion

        #region TC 9 (서버시스템 간 폴링, DB 저장 안함)
        public void SetDB_TC9(PrtCmd09 _tc9)
        {
            //오라클 연결관리
            this.OracleConMng(this.OraQueryCount);
            this.OraQueryCount++;
        }
        #endregion

        #region TC 10 (시나리오 발령, 미사용)
        public void SetDB_TC10(PrtCmd10 _tc10)
        {
            //오라클 연결관리
            this.OracleConMng(this.OraQueryCount);
            this.OraQueryCount++;
        }
        #endregion

        #region TC 11 (시나리오 발령 결과, 미사용)
        public void SetDB_TC11(PrtCmd11 _tc11)
        {
            //오라클 연결관리
            this.OracleConMng(this.OraQueryCount);
            this.OraQueryCount++;
        }
        #endregion

        #region TC 12 (시나리오 발령)
        public void SetDB_TC12(PrtCmd12 _tc12)
        {
            OleDbDataReader reader = null;
            int cnt = 0;

            //시나리오 테이블에 추가
            try
            {
                //시나리오 테이블에 있는지 확인
                this.oleDb.QueryData("SELECT COUNT(*) FROM SCENARIOORDERHIST WHERE SCENARIOID = TO_DATE(" +
                    this.GetDateType(_tc12.StartTimeDT) + ", 'yyyymmddhh24miss')", out reader);

                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        cnt = int.Parse(reader[i].ToString());
                    }
                }

                if (reader != null)
                {
                    reader.Dispose();
                }

                if (cnt == 0) //시나리오 테이블에 없다면 insert
                {
                    //DB Insert 작업 시작
                    this.oleDb.QueryData(
                        string.Format("INSERT INTO SCENARIOORDERHIST VALUES(TO_DATE({0}, 'yyyymmddhh24miss'), '{1}')",
                        this.GetDateType(_tc12.StartTimeDT), _tc12.SnName));
                }
            }
            catch (Exception ex)
            {
                throw new Exception("SetDB_TC12.시나리오 테이블에 추가 - " + ex.Message);
            }

            string tmpMode = string.Empty;
            reader = null;
            cnt = 0;

            if (_tc12.Mode == REAL) //실제
            {
                tmpMode = "REALALARMORDERHIST";
            }
            else if (_tc12.Mode == TEST) //시험
            {
                tmpMode = "TESTALARMORDERHIST";
            }
            else if (_tc12.Mode == TRAIN) //훈련
            {
                tmpMode = "TRAINALARMORDERHIST";
            }

            //발령 테이블에 있는지 확인
            try
            {
                this.oleDb.QueryData("SELECT COUNT(*) FROM " + tmpMode + " WHERE ORDERTIME = TO_DATE(" +
                    this.GetDateType(_tc12.TimeDT) + ", 'yyyymmddhh24miss') AND SOURCE = " + _tc12.Source, out reader);

                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        cnt = int.Parse(reader[i].ToString());
                    }
                }

                if (reader != null)
                {
                    reader.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("SetDB_TC12.발령 테이블에 있는지 확인 - " + ex.Message);
            }

            //있다면 업데이트, 없으면 Pass
            if (cnt > 0)
            {
                try
                {
                    this.oleDb.QueryData(
                        string.Format(
                        "UPDATE {0} SET SCENARIOID = TO_DATE({1}, 'yyyymmddhh24miss') WHERE ORDERTIME = TO_DATE({2}, 'yyyymmddhh24miss') AND SOURCE = {3}",
                        tmpMode, this.GetDateType(_tc12.StartTimeDT), this.GetDateType(_tc12.TimeDT), _tc12.Source));
                }
                catch (Exception ex)
                {
                    throw new Exception("SetDB_TC12.있다면 업데이트, 없으면 Pass - " + ex.Message);
                }
            }

            //오라클 연결관리
            this.OracleConMng(this.OraQueryCount);
            this.OraQueryCount++;
        }
        #endregion

        #region TC 13 (자동방송 이벤트)
        public void SetDB_TC13(PrtCmd13 _tc13)
        {
            //오라클 연결관리
            this.OracleConMng(this.OraQueryCount);
            this.OraQueryCount++;
        }
        #endregion

        #region TC 14 (자동방송 발령, 1차년도에서는 사용하지 않음)
        public void SetDB_TC14(PrtCmd14 _tc14)
        {
            //오라클 연결관리
            this.OracleConMng(this.OraQueryCount);
            this.OraQueryCount++;
        }
        #endregion

        #region TC 15 (단말 이상/복구)
        public void SetDB_TC15(PrtCmd15 _tc15)
        {
            if (_tc15.TermSts == 1) //이상
            {
                //이상 패킷이 온 경우 복구가 되지 않은 같은 단말의 정보가 있는지 확인하고 없으면 insert, 있으면 버림
                OleDbDataReader reader = null;
                int cnt = 0;

                try
                {
                    this.oleDb.QueryData("SELECT COUNT(*) FROM TERMERRORHIST WHERE TERMCODE = " +
                        this.GetTermCode(_tc15.AddrStr) + " AND RECOVERTIME is null", out reader);

                    while (reader.Read())
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            cnt = int.Parse(reader[i].ToString());
                        }
                    }

                    if (reader != null)
                    {
                        reader.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("SetDB_TC15.이상정보가 온 상태에서 같은 단말의 정보가 있는지 확인 - " + ex.Message);
                }

                if (cnt > 0)
                {
                    return;
                }

                //같은 단말의 복구가 되지 않은 정보가 없으므로 데이터 insert
                try
                {
                    this.oleDb.QueryData(
                        string.Format("INSERT INTO TERMERRORHIST (TERMCODE, ERRORTIME) VALUES({0}, TO_DATE({1}, 'yyyymmddhh24miss'))",
                        this.GetTermCode(_tc15.AddrStr), this.GetDateType(_tc15.TimeDT)));

                    if (this.OnTermStateTCPSendEvt != null)
                    {
                        this.OnTermStateTCPSendEvt(this, new TermStateTCPSendEvtArgs(_tc15));
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("SetDB_TC15.이상정보가 온 상태에서 같은 단말의 복구가 되지 않은 정보가 없으므로 데이터 insert - " + ex.Message);
                }
            }
            else if (_tc15.TermSts == 0) //정상
            {
                //정상 패킷이 온 경우 해당 단말의 이상 정보가 있는지 확인하고 있으면 update, 없으면 버림
                OleDbDataReader reader = null;
                int cnt = 0;

                try
                {
                    this.oleDb.QueryData("SELECT COUNT(*) FROM TERMERRORHIST WHERE TERMCODE = " +
                        this.GetTermCode(_tc15.AddrStr) + " AND RECOVERTIME is null", out reader);

                    while (reader.Read())
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            cnt = int.Parse(reader[i].ToString());
                        }
                    }

                    if (reader != null)
                    {
                        reader.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("SetDB_TC15.정상정보가 온 상태에서 같은 단말의 정보가 있는지 확인 - " + ex.Message);
                }

                if (cnt == 0)
                {
                    return;
                }

                //같은 단말의 복구가 되지 않은 정보가 있으므로 데이터 update
                try
                {
                    this.oleDb.QueryData(
                        string.Format(
                        "UPDATE TERMERRORHIST SET RECOVERTIME = TO_DATE({0}, 'yyyymmddhh24miss') WHERE TERMCODE = {1} AND RECOVERTIME is null",
                        this.GetDateType(_tc15.TimeDT), this.GetTermCode(_tc15.AddrStr)));

                    if (this.OnTermStateTCPSendEvt != null)
                    {
                        this.OnTermStateTCPSendEvt(this, new TermStateTCPSendEvtArgs(_tc15));
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("SetDB_TC15.정상정보가 온 상태에서 같은 단말의 복구가 되지 않은 정보가 있으므로 데이터 insert - " + ex.Message);
                }
            }

            //오라클 연결관리
            this.OracleConMng(this.OraQueryCount);
            this.OraQueryCount++;
        }
        #endregion

        #region TC 16 (회선 감시 요청, DB 저장 안함)
        public void SetDB_TC16(PrtCmd16 _tc16)
        {
            //오라클 연결관리
            this.OracleConMng(this.OraQueryCount);
            this.OraQueryCount++;
        }
        #endregion

        #region TC 17 (회선 감시 결과, DB 저장 안함)
        public void SetDB_TC17(PrtCmd17 _tc17)
        {
            //오라클 연결관리
            this.OracleConMng(this.OraQueryCount);
            this.OraQueryCount++;
        }
        #endregion

        #region TC 21 (단말 로그인/로그아웃)
        public void SetDB_TC21(PrtCmd21 _tc21)
        {
            try
            {
                //DB Insert 작업 시작
                this.oleDb.QueryData(
                    string.Format("INSERT INTO TERMLOGINHIST VALUES(TO_DATE({0}, 'yyyymmddhh24miss'), '{1}', '{2}', {3})",
                    this.GetDateType(_tc21.TimeDT), _tc21.AddrStr, _tc21.UserID, (_tc21.IsLogin == true) ? 1 : 0));
            }
            catch (Exception ex)
            {
                throw new Exception("SetDB_TC21.DB Insert 작업 시작 - " + ex.Message);
            }

            //오라클 연결관리
            this.OracleConMng(this.OraQueryCount);
            this.OraQueryCount++;
        }
        #endregion
        #endregion

        #region method
        /// <summary>
        /// 단말의 IP를 받아 검색하여 코드를 반환한다.
        /// </summary>
        /// <param name="_termIP"></param>
        /// <returns></returns>
        public int GetTermCode(string _termIP)
        {
            int rst = 0;

            for (int i = 0; i < this.termDataList.Count; i++)
            {
                if (this.termDataList[i].TermIp == _termIP)
                {
                    rst = this.termDataList[i].TermCode;
                    break;
                }
            }

            return rst;
        }

        /// <summary>
        /// 기초데이터 로드
        /// </summary>
        public bool InitDataLoad()
        {
            OleDbDataReader reader = null;

            try
            {
                //단말 데이터 로드
                this.oleDb.QueryData("SELECT TERMCODE, TERMIP, USEFLAG, TERMNAME, PERSONNAME, PERSONPHONE, TERMKIND, PROVCODE, DISTCODE, ADDEQUIPFLAG, INSTALLDATE" +
                " FROM TERMINFO", out reader);

                while (reader.Read())
                {
                    TermData tData = new TermData();
                    int tmpNum;

                    //TERMCODE INT
                    if (int.TryParse(reader[0].ToString(), out tmpNum))
                    {
                        tData.TermCode = tmpNum;
                    }

                    //TERMIP STRING
                    if (reader[1].ToString() != string.Empty)
                    {
                        tData.TermIp = reader[1].ToString();
                    }

                    //USEFLAG INT
                    if (int.TryParse(reader[2].ToString(), out tmpNum))
                    {
                        tData.UseFlag = tmpNum;
                    }

                    //TERMNAME STRING
                    if (reader[3].ToString() != string.Empty)
                    {
                        tData.TermName = reader[3].ToString();
                    }

                    //PERSONNAME STRING
                    if (reader[4].ToString() != string.Empty)
                    {
                        tData.PersonName = reader[4].ToString();
                    }

                    //PERSONPHONE STRING
                    if (reader[5].ToString() != string.Empty)
                    {
                        tData.PersonPhone = reader[5].ToString();
                    }

                    //TERMKIND INT
                    if (int.TryParse(reader[6].ToString(), out tmpNum))
                    {
                        tData.TermKind = tmpNum;
                    }

                    //PROVCODE INT
                    if (int.TryParse(reader[7].ToString(), out tmpNum))
                    {
                        tData.ProvCode = tmpNum;
                    }

                    //DISTCODE INT
                    if (int.TryParse(reader[8].ToString(), out tmpNum))
                    {
                        tData.DistCode = tmpNum;
                    }

                    //ADDEQUIPFLAG INT
                    if (int.TryParse(reader[9].ToString(), out tmpNum))
                    {
                        tData.AddEquipFlag = tmpNum;
                    }

                    //INSTALLDATE DATETIME
                    if (reader[10].ToString() != string.Empty)
                    {
                        tData.InstallDate = (DateTime)reader[10];
                    }

                    this.termDataList.Add(tData);
                }

                reader = null;

                //시도 데이터 로드
                this.oleDb.QueryData("SELECT PROVCODE, PROVNAME, PROVIP FROM PROVINFO", out reader);

                while (reader.Read())
                {
                    ProvData pData = new ProvData();
                    int tmpNum;

                    //PROVCODE INT
                    if (int.TryParse(reader[0].ToString(), out tmpNum))
                    {
                        pData.ProvCode = tmpNum;
                    }

                    //PROVNAME STRING
                    if (reader[1].ToString() != string.Empty)
                    {
                        pData.ProvName = reader[1].ToString();
                    }

                    //PROVIP STRING
                    if (reader[2].ToString() != string.Empty)
                    {
                        pData.ProvIP = reader[2].ToString();
                    }

                    this.provDataList.Add(pData);
                }

                reader = null;

                //시군 데이터 로드
                this.oleDb.QueryData("SELECT DISTCODE, DISTNAME, PROVCODE, DISTIP, VSATFLAG FROM DISTINFO", out reader);

                while (reader.Read())
                {
                    DistData dData = new DistData();
                    int tmpNum;

                    //DISTCODE INT
                    if (int.TryParse(reader[0].ToString(), out tmpNum))
                    {
                        dData.DistCode = tmpNum;
                    }

                    //DISTNAME STRING
                    if (reader[1].ToString() != string.Empty)
                    {
                        dData.DistName = reader[1].ToString();
                    }

                    //PROVCODE INT
                    if (int.TryParse(reader[2].ToString(), out tmpNum))
                    {
                        dData.ProvCode = tmpNum;
                    }

                    //DISTIP STRING
                    if (reader[3].ToString() != string.Empty)
                    {
                        dData.DistIP = reader[3].ToString();
                    }

                    //VSATFLAG INT
                    if (int.TryParse(reader[4].ToString(), out tmpNum))
                    {
                        dData.VSatFlag = tmpNum;
                    }

                    this.distDataList.Add(dData);
                }

                reader = null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("DBMngDataMng.InitDataLoad - " + ex.Message);
                return false;
            }

            return true;
        }

        /// <summary>
        /// DateTime을 인자로 받아 ex 20110317154830 (14 byte) 형태로 반환한다.
        /// </summary>
        /// <param name="_dt"></param>
        /// <returns></returns>
        public string GetDateType(DateTime _dt)
        {
            StringBuilder rst = new StringBuilder();

            rst.Append(_dt.Year);

            if (_dt.Month < 10)
            {
                rst.Append(string.Format("0{0}", _dt.Month));
            }
            else
            {
                rst.Append(_dt.Month);
            }

            if (_dt.Day < 10)
            {
                rst.Append(string.Format("0{0}", _dt.Day));
            }
            else
            {
                rst.Append(_dt.Day);
            }

            if (_dt.Hour < 10)
            {
                rst.Append(string.Format("0{0}", _dt.Hour));
            }
            else
            {
                rst.Append(_dt.Hour);
            }

            if (_dt.Minute < 10)
            {
                rst.Append(string.Format("0{0}", _dt.Minute));
            }
            else
            {
                rst.Append(_dt.Minute);
            }

            if (_dt.Second < 10)
            {
                rst.Append(string.Format("0{0}", _dt.Second));
            }
            else
            {
                rst.Append(_dt.Second);
            }

            return rst.ToString();
        }

        /// <summary>
        /// uint를 인자로 받아 DateTime 형태로 반환한다.
        /// </summary>
        /// <param name="_dt"></param>
        /// <returns></returns>
        public DateTime GetDateType(uint _dt)
        {
            DateTime t0 = new DateTime(1970, 1, 1, 9, 0, 0);
            TimeSpan ts0 = new TimeSpan(t0.Ticks);
            TimeSpan ts1 = TimeSpan.FromSeconds(ts0.TotalSeconds + (double)_dt);
            DateTime t = new DateTime(ts1.Ticks);

            return t;
        }

        /// <summary>
        /// byte[]를 string으로 변환하여 반환한다.
        /// </summary>
        /// <param name="totalbyte"></param>
        /// <returns></returns>
        public string LogFileWrite(byte[] totalbyte)
        {
            try
            {
                string stmp = AdengUtil.Byte2HexString(totalbyte);
                return stmp;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("DBMngDataMng.LogFileWrite - " + ex.Message);
                return string.Empty;
            }
        }
        #endregion
    }

    #region class
    /// <summary>
    /// 단말 정보 테이블
    /// </summary>
    public class TermData
    {
        #region 멤버
        private int termCode = int.MinValue;
        private int termKind = int.MinValue;
        private int provCode = int.MinValue;
        private int distCode = int.MinValue;
        private int addEquipFlag = int.MinValue;
        private int useFlag = int.MinValue;
        private string termIp = string.Empty;
        private string termName = string.Empty;
        private string personName = string.Empty;
        private string personPhone = string.Empty;
        private DateTime installDate = DateTime.MinValue;
        #endregion

        #region 접근자
        public int TermCode
        {
            get { return termCode; }
            set { termCode = value; }
        }

        public int TermKind
        {
            get { return termKind; }
            set { termKind = value; }
        }

        public int ProvCode
        {
            get { return provCode; }
            set { provCode = value; }
        }

        public int DistCode
        {
            get { return distCode; }
            set { distCode = value; }
        }

        public int AddEquipFlag
        {
            get { return addEquipFlag; }
            set { addEquipFlag = value; }
        }

        public int UseFlag
        {
            get { return useFlag; }
            set { useFlag = value; }
        }

        public string TermIp
        {
            get { return termIp; }
            set { termIp = value; }
        }

        public string TermName
        {
            get { return termName; }
            set { termName = value; }
        }

        public string PersonName
        {
            get { return personName; }
            set { personName = value; }
        }

        public string PersonPhone
        {
            get { return personPhone; }
            set { personPhone = value; }
        }

        public DateTime InstallDate
        {
            get { return installDate; }
            set { installDate = value; }
        }
        #endregion
    }

    /// <summary>
    /// 시도 정보 테이블
    /// </summary>
    public class ProvData
    {
        #region 멤버
        private int provCode = int.MinValue;
        private string provName = string.Empty;
        private string provIp = string.Empty;
        #endregion

        #region 접근자
        public int ProvCode
        {
            get { return this.provCode; }
            set { this.provCode = value; }
        }

        public string ProvName
        {
            get { return this.provName; }
            set { this.provName = value; }
        }

        public string ProvIP
        {
            get { return this.provIp; }
            set { this.provIp = value; }
        }
        #endregion
    }

    /// <summary>
    /// 시군 정보 테이블
    /// </summary>
    public class DistData
    {
        #region 멤버
        private int distCode = int.MinValue;
        private string distName = string.Empty;
        private int provCode = int.MinValue;
        private string distIp = string.Empty;
        private int vsatFlag = int.MinValue;
        #endregion

        #region 접근자
        public int DistCode
        {
            get { return this.distCode; }
            set { this.distCode = value; }
        }

        public string DistName
        {
            get { return this.distName; }
            set { this.distName = value; }
        }

        public int ProvCode
        {
            get { return this.provCode; }
            set { this.provCode = value; }
        }

        public string DistIP
        {
            get { return this.distIp; }
            set { this.distIp = value; }
        }

        public int VSatFlag
        {
            get { return this.vsatFlag; }
            set { this.vsatFlag = value; }
        }
        #endregion
    }

    /// <summary>
    /// 단말 상태를 받은 후 수신확인을 TCP로 전송하기 위한 이벤트 아규먼트 클래스
    /// </summary>
    public class TermStateTCPSendEvtArgs : EventArgs
    {
        private PrtCmd15 prtCmd15 = null;

        public PrtCmd15 Cmd15
        {
            get { return this.prtCmd15; }
            set { this.prtCmd15 = value; }
        }

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="_prtCmd15"></param>
        public TermStateTCPSendEvtArgs(PrtCmd15 _prtCmd15)
        {
            this.prtCmd15 = _prtCmd15;
        }
    }
    #endregion
}