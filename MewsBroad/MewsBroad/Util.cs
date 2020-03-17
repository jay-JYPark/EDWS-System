using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Mews.Svr.Broad
{
    #region Delegate
    public delegate void InvokeVoid();
    public delegate void InvokeValueOne<T>(T valueT);
    public delegate void InvokeValueTwo<T, U>(T valueT, U valueU);
    #endregion

    public class Util
    {
        #region Enum
        public enum emPnl : byte
        {
            mode = 0,
            media = 1,
            grp = 2,
            type = 3,
            confirm = 4,
            mic = 5
        }
        public enum emMode : byte
        {
            test = 0,
            real = 1,
            drill = 2,
            none = 3
        }
        public enum emStep : byte
        {
            none = 4,
            me = 5
        }

        public enum emBackImage
        {
            grpActive = 1,
            grpNone = 2,
            IndActive = 3,
            IndNone = 4
        }

        public enum emKind : byte
        {
            siren1 = 0,
            siren2 = 1,
            siren3 = 2,
            storedMsg = 3,
            liveBrd = 4,
            stop = 5,
            brdEnd = 6,
            ready = 7
        }

        public enum emDest : byte
        {
            all = 0,
            grp = 1,
            ind = 2
        }

        public enum emMedia : byte
        {
            all = 0,
            vhf = 1,
            sat = 2
        }

        public enum emBroad : byte
        {
            start = 1,
            end = 2
        }

        public enum emSource : byte
        {
            cc = 0,
            mcc = 1,
            term = 2,
            aimac = 3
        }
        #endregion

        #region Fields
        public static List<Color> lstColor = new List<Color>();
        public static string totalIP = "15.255.255.255";
        public static TimeData timeData = null;
        public static string fileDir = System.Windows.Forms.Application.StartupPath;
        private static string timeFile = "\\xmldata\\TimeData.xml";
        public static string file_MsgData = string.Empty;
        public static string file_CGData = string.Empty;
        public static string file_SirenData = string.Empty;
        public static string file_BrdData = string.Empty;
        public static string file_CGCutImage = string.Empty;
        public static string file_OptionData = string.Empty;
        public static string file_StoMsgPlay = string.Empty;
        public static string file_Siren = string.Empty;
        public static string file_Auto = string.Empty;
        public static string file_Auto_CG_Data = string.Empty;
        public static OptionInfo optionInfo = null;
        public static AutoInfo autoInfo = null;
        public static AutoCGInfo autoCGInfo = new AutoCGInfo();
        #endregion

        public Util()
        {
        }

        static Util()
        {
            file_MsgData = System.Windows.Forms.Application.StartupPath + "\\xmldata\\MsgNameData.xml";
            file_CGData = System.Windows.Forms.Application.StartupPath + "\\xmldata\\CGData.xml";
            file_SirenData = System.Windows.Forms.Application.StartupPath + "\\xmldata\\SirenData.xml";
            file_BrdData = System.Windows.Forms.Application.StartupPath + "\\xmldata\\BrdData.xml";
            file_CGCutImage = System.Windows.Forms.Application.StartupPath + "\\Images\\CG\\";
            file_OptionData = System.Windows.Forms.Application.StartupPath + "\\xmldata\\OptionData.xml";
            file_StoMsgPlay = System.Windows.Forms.Application.StartupPath + "\\StoredMsg\\";
            file_Siren = System.Windows.Forms.Application.StartupPath + "\\Siren\\";
            file_Auto = System.Windows.Forms.Application.StartupPath + "\\xmldata\\AutoData.xml";
            file_Auto_CG_Data = System.Windows.Forms.Application.StartupPath + "\\xmldata\\AutoCGData.xml";

            lstColor.Add(Color.DarkGreen);
            lstColor.Add(Color.DarkRed);
            lstColor.Add(Color.DarkOrange);
            lstColor.Add(Color.Black);
            lstColor.Add(Color.FromArgb(255, 252, 236));    // 기본 생상
            //lstColor.Add(Color.FromArgb(202, 235, 246));    // Step 색상
            lstColor.Add(Color.LightBlue);    // Step 색상

            LoadTimeData();
        }

        public static void LoadOptioninfo()
        {
            try
            {
                optionInfo = new OptionInfo();

                if (!File.Exists(Util.file_OptionData))
                    return;

                using (Stream stream = new FileStream(Util.file_OptionData, FileMode.Open))
                {
                    XmlSerializer ser = new XmlSerializer(typeof(OptionInfo));
                    optionInfo = (OptionInfo)ser.Deserialize(stream);
                }
            }
            catch (Exception ex)
            {
                File.Decrypt(Util.file_OptionData);
                SaveOptionInfo();

                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        public static void SaveOptionInfo()
        {
            using (Stream stream = new FileStream(Util.file_OptionData, FileMode.Create))
            {
                XmlSerializer ser = new XmlSerializer(typeof(OptionInfo));
                ser.Serialize(stream, optionInfo);
                stream.Close();
            }
        }

        public static void LoadAutoinfo()
        {
            try
            {
                autoInfo = new AutoInfo();

                if (!File.Exists(Util.file_Auto))
                    return;

                using (Stream stream = new FileStream(Util.file_Auto, FileMode.Open))
                {
                    XmlSerializer ser = new XmlSerializer(typeof(AutoInfo));
                    autoInfo = (AutoInfo)ser.Deserialize(stream);
                }
            }
            catch (Exception ex)
            {
                File.Decrypt(Util.file_Auto);
                SaveAutoinfo();
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        public static void SaveAutoinfo()
        {
            using (Stream stream = new FileStream(Util.file_Auto, FileMode.Create))
            {
                XmlSerializer ser = new XmlSerializer(typeof(AutoInfo));
                ser.Serialize(stream, autoInfo);
                stream.Close();
            }
        }

        public static void LoadAutoCGinfo()
        {
            try
            {
                autoCGInfo = new AutoCGInfo();

                if (!File.Exists(Util.file_Auto_CG_Data))
                    return;

                using (Stream stream = new FileStream(Util.file_Auto_CG_Data, FileMode.Open))
                {
                    XmlSerializer ser = new XmlSerializer(typeof(AutoCGInfo));
                    autoCGInfo = (AutoCGInfo)ser.Deserialize(stream);
                }
            }
            catch (Exception ex)
            {
                File.Decrypt(Util.file_Auto_CG_Data);
                SaveAutoCGinfo();
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        public static void SaveAutoCGinfo()
        {
            using (Stream stream = new FileStream(Util.file_Auto_CG_Data, FileMode.Create))
            {
                XmlSerializer ser = new XmlSerializer(typeof(AutoCGInfo));
                ser.Serialize(stream, autoCGInfo);
                stream.Close();
            }
        }

        public static Image GetBackgroundImage(byte kind)
        {
            Image img = null;

            switch (kind)
            {
                case (byte)emBackImage.grpActive:
                    img = Image.FromFile(fileDir + "\\Images\\bgGroupActive.png");
                    break;

                case (byte)emBackImage.grpNone:
                    img = Image.FromFile(fileDir + "\\Images\\bgGroup.png");
                    break;

                case (byte)emBackImage.IndActive:
                    img = Image.FromFile(fileDir + "\\Images\\bgIndividualActive.png");
                    break;

                case (byte)emBackImage.IndNone:
                    img = Image.FromFile(fileDir + "\\Images\\bgIndividual.png");
                    break;
            }

            return img;
        }

        // Siren & Message Time
        private static void LoadTimeData()
        {
            timeData = new TimeData();

            if (!File.Exists(fileDir + timeData))
            {
                timeData.lstSiren.Add("10");
                timeData.lstSiren.Add("20");
                timeData.lstSiren.Add("30");

                timeData.lstMsg.Add("51");
                timeData.lstMsg.Add("52");
                timeData.lstMsg.Add("53");
                timeData.lstMsg.Add("54");
                timeData.lstMsg.Add("55");
                timeData.lstMsg.Add("56");
                timeData.lstMsg.Add("57");
                timeData.lstMsg.Add("58");
                timeData.lstMsg.Add("59");
                timeData.lstMsg.Add("60");
                timeData.lstMsg.Add("61");
                timeData.lstMsg.Add("62");
                timeData.lstMsg.Add("63");
                timeData.lstMsg.Add("64");
                timeData.lstMsg.Add("65");
                timeData.lstMsg.Add("66");
                timeData.lstMsg.Add("67");
                timeData.lstMsg.Add("68");
                timeData.lstMsg.Add("69");
                timeData.lstMsg.Add("70");

                SaveTimeData();
            }
            else
            {
                using (Stream stream = new FileStream(fileDir + timeFile, FileMode.Open))
                {
                    XmlSerializer ser = new XmlSerializer(typeof(TimeData));
                    timeData = (TimeData)ser.Deserialize(stream);
                }
            }
        }

        public static void SaveTimeData()
        {
            using (Stream stream = new FileStream(fileDir + timeFile, FileMode.Create))
            {
                XmlSerializer ser = new XmlSerializer(typeof(TimeData));
                ser.Serialize(stream, timeData);
                stream.Close();
            }
        }
    }

    public class TimeData
    {
        public List<string> lstSiren = new List<string>();
        public List<string> lstMsg = new List<string>();

        public TimeData()
        {
        }
    }

    public class MovePanelInfo
    {
        public int minVal = 0;
        public int maxVal = 0;
        public int yVal = 0;
        public int interval = 0;
        public bool bShow = false;
        public UserControl ucPanl = null;

        public MovePanelInfo()
        {
        }
    }

    public class OptionInfo
    {
        public bool bUseBuzzerReal = false;
        public bool bUseBuzzerDrill = false;
        public bool bUseBuzzerTest = false;
    }

    public class AutoInfo
    {
        public float intensity = 0;
        public bool highAuto = true;
        public bool lowAuto = true;
        public int highTime = 0;
        public int lowTime = 0;
        public string highMsg = string.Empty;
        public string lowMsg = string.Empty;
        public bool highCBSUse = false;
        public bool lowCBSUse = false;
    }

    public class AutoCGInfo
    {
        public string highText = string.Empty;
        public string lowText = string.Empty;
        public int highKindNum = 0;
        public int lowKindNum = 0;
    }
}
