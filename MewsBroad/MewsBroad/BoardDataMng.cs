using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;

namespace Mews.Svr.Broad
{
    public class BoardDataMng
    {
        #region Fields
        private static BoardDataMng brdMng = null;
        private static bool bInsFlag = false;
        public Dictionary<string, BoardInfo> dicBrd = new Dictionary<string, BoardInfo>();
        #endregion
        
        public BoardDataMng()
        {
        }

        public static BoardDataMng GetBrdMng()
        {
            if (!bInsFlag)
            {
                bInsFlag = true;
                brdMng = new BoardDataMng();
            }

            return brdMng;
        }

        public bool LoadBrdData()
        {
            dicBrd.Clear();           

            if (!File.Exists(Util.file_BrdData))
                return false;

            BoardXmlInfo brdXml = new BoardXmlInfo();

            using (Stream stream = new FileStream(Util.file_BrdData, FileMode.Open))
            {
                XmlSerializer ser = new XmlSerializer(typeof(BoardXmlInfo));
                brdXml = (BoardXmlInfo)ser.Deserialize(stream);
            }

            foreach (BoardInfoEx brd in brdXml.lstXml)
            {
                BoardInfo info = new BoardInfo();
                info.kind = brd.kind;
                info.kindNum = brd.kindNum;

                foreach(BoardInfoDetail brdt in brd.lstBrdInof)
                {
                    info.dicText.Add(brdt.name, brdt);
                }

                dicBrd.Add(info.kind, info);
            }

            return true;
        }

        public void SaveBrdData()
        {
            BoardXmlInfo brdXml = new BoardXmlInfo();

            foreach (KeyValuePair<string, BoardInfo> pair in dicBrd)
            {
                BoardInfoEx infoEx = new BoardInfoEx();
                infoEx.kind = pair.Key;
                infoEx.kindNum = pair.Value.kindNum;

                foreach (KeyValuePair<string, BoardInfoDetail> pairD in pair.Value.dicText)
                {
                    infoEx.lstBrdInof.Add(pairD.Value);
                }

                brdXml.lstXml.Add(infoEx);
            }

            using (Stream stream = new FileStream(Util.file_BrdData, FileMode.Create))
            {
                XmlSerializer ser = new XmlSerializer(typeof(BoardXmlInfo));
                ser.Serialize(stream, brdXml);
                stream.Close();
            }
        }
    }

    public class BoardXmlInfo
    {
        public List<BoardInfoEx> lstXml = new List<BoardInfoEx>();

        public BoardXmlInfo()
        {
        }
    }

    public class BoardInfo
    {
        public string kind;
        public int kindNum;
        public Dictionary<string, BoardInfoDetail> dicText = new Dictionary<string, BoardInfoDetail>();

        public BoardInfo()
        {
        }
    }

    public class BoardInfoDetail
    {
        public string kind = string.Empty;
        public int kindNum = 0;
        public string name = string.Empty;
        public string text = string.Empty;
        public bool[] cut = new bool[9];
        public bool isBlack = false;

        public BoardInfoDetail()
        {
        }
    }

    public class BoardInfoEx
    {
        public string kind;
        public int kindNum;
        public List<BoardInfoDetail> lstBrdInof = new List<BoardInfoDetail>();
        
        public BoardInfoEx()
        {
        }
    }
}