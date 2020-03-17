using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;

namespace Mews.Svr.Broad
{
    public class StMsgDataMng
    {
        #region Fields
        public Dictionary<string, MsgInfo> dicMsg = new Dictionary<string, MsgInfo>();
        private static StMsgDataMng msgMng = null;
        private static bool bInsFlag = false;
        #endregion

        public StMsgDataMng()
        {
        }

        public static StMsgDataMng GetMsgMng()
        {
            if (!bInsFlag)
            {
                bInsFlag = true;
                msgMng = new StMsgDataMng();
            }

            return msgMng;
        }

        public void LoadMsgNameData()
        {
            dicMsg.Clear();

            if (!File.Exists(Util.file_MsgData))
                return;

            MsgXmlInfo msgXml = new MsgXmlInfo();

            using (Stream stream = new FileStream(Util.file_MsgData, FileMode.Open))
            {
                XmlSerializer ser = new XmlSerializer(typeof(MsgXmlInfo));
                msgXml = (MsgXmlInfo)ser.Deserialize(stream);
            }

            foreach (MsgInfo msg in msgXml.lstMsg)
            {
                dicMsg.Add(msg.msgNum, msg);
            }
        }

        public void SaveMsgNameData()
        {
            MsgXmlInfo msgXml = new MsgXmlInfo();

            foreach (KeyValuePair<string, MsgInfo> pair in dicMsg)
            {
                msgXml.lstMsg.Add(pair.Value);
            }

            using (Stream stream = new FileStream(Util.file_MsgData, FileMode.Create))
            {
                XmlSerializer ser = new XmlSerializer(typeof(MsgXmlInfo));
                ser.Serialize(stream, msgXml);
                stream.Close();
            }
        }
    }

    public class MsgXmlInfo
    {
        public List<MsgInfo> lstMsg = new List<MsgInfo>();

        public MsgXmlInfo()
        {
        }
    }

    public class MsgInfo
    {
        public string msgNum = string.Empty;
        public string msgName = string.Empty;
        public string msgText = string.Empty;
        public string msgReptCtn = "1";
        public int msgTime = 0;     // sec

        public MsgInfo()
        {
        }
    }
}
