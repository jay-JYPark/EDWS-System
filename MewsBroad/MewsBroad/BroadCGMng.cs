using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;

namespace Mews.Svr.Broad
{
    public class BroadCGMng
    {
        private static BroadCGMng cgMng = null;
        private static bool bInsFlag = false;
        public Dictionary<string, CGInfo> dicCG = new Dictionary<string, CGInfo>();

        public BroadCGMng()
        {
        }

        public static BroadCGMng GetCGMng()
        {
            if (!bInsFlag)
            {
                bInsFlag = true;
                cgMng = new BroadCGMng();
            }

            return cgMng;
        }

        public bool LoadBrdData()
        {
            dicCG.Clear();

            if (!File.Exists(Util.file_BrdData))
                return false;

            CGInfoXml cgXML = new CGInfoXml();

            using (Stream stream = new FileStream(Util.file_BrdData, FileMode.Open))
            {
                XmlSerializer ser = new XmlSerializer(typeof(CGInfo));
                cgXML = (CGInfoXml)ser.Deserialize(stream);
            }

            foreach (CGInfo cginfo in cgXML.lstCGInfo)
            {
                dicCG.Add(cginfo.name, cginfo);
            }

            return true;
        }

        public void SaveBrdData()
        {
            CGInfoXml CGXml = new CGInfoXml();

            foreach (KeyValuePair<string, CGInfo> pair in dicCG)
            {
                CGXml.lstCGInfo.Add(pair.Value);
            }

            using (Stream stream = new FileStream(Util.file_BrdData, FileMode.Create))
            {
                XmlSerializer ser = new XmlSerializer(typeof(CGInfoXml));
                ser.Serialize(stream, CGXml);
                stream.Close();
            }
        }
    }

    public class CGInfoXml
    {
        public List<CGInfo> lstCGInfo = new List<CGInfo>();

        public CGInfoXml()
        {
        }
    }

    public class CGInfo
    {
        public string name = string.Empty;
        public int repeat = 0;
        public string text = string.Empty;
        public string cut = string.Empty;

        public CGInfo()
        {
        }
    }
}