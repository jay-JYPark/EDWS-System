using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;

namespace Mews.Svr.Broad
{
    public class SirenDataMng
    {
        #region Fields
        public Dictionary<string, SirenInfo> dicSiren = new Dictionary<string, SirenInfo>();
        private static bool bInsFlag = false;
        private static SirenDataMng sirenMng = null;
        #endregion

        public SirenDataMng()
        {
        }

        public static SirenDataMng GetSirenMng()
        {
            if (!bInsFlag)
            {
                bInsFlag = true;
                sirenMng = new SirenDataMng();
            }

            return sirenMng;
        }

        public void LoadSirenData()
        {
            dicSiren.Clear();

            if (!File.Exists(Util.file_SirenData))
            {
                // Default Data
                for (int i = 0; i < 20; i++)
                {
                    SirenInfo s = new SirenInfo();

                    s.SirenNum = (i + 1).ToString();
                    s.SirenName = "Siren " + (i + 1).ToString();
                    s.SirenTime = 30;
                    dicSiren.Add(s.SirenNum, s);
                }

                SaveSirenData();
                return;
            }

            SirenXmlInfo SirenXml = new SirenXmlInfo();

            using (Stream stream = new FileStream(Util.file_SirenData, FileMode.Open))
            {
                XmlSerializer ser = new XmlSerializer(typeof(SirenXmlInfo));
                SirenXml = (SirenXmlInfo)ser.Deserialize(stream);
            }

            foreach (SirenInfo siren in SirenXml.lstSiren)
            {
                dicSiren.Add(siren.SirenNum, siren);
            }
        }

        public void SaveSirenData()
        {
            SirenXmlInfo sirenXml = new SirenXmlInfo();

            foreach (KeyValuePair<string, SirenInfo> pair in dicSiren)
            {
                sirenXml.lstSiren.Add(pair.Value);
            }

            using (Stream stream = new FileStream(Util.file_SirenData, FileMode.Create))
            {
                XmlSerializer ser = new XmlSerializer(typeof(SirenXmlInfo));
                ser.Serialize(stream, sirenXml);
                stream.Close();
            }
        }
    }

    public class SirenXmlInfo
    {
        public List<SirenInfo> lstSiren = new List<SirenInfo>();

        public SirenXmlInfo()
        {
        }
    }

    public class SirenInfo
    {
        public string SirenNum = string.Empty;
        public string SirenName = string.Empty;
        public Int32 SirenTime = 0;
        public string SirenContext = string.Empty;

        public SirenInfo()
        {
        }
    }
}