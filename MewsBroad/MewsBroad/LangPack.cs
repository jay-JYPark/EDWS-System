using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mews.Svr.Broad
{
    public class LangPack
    {
        #region Fields
        public static bool IsEng = false;
        #endregion

        public LangPack()
        {
        }

        public static string GetProgName()
        {
            return LangPack.IsEng ? "EDWS Broadcasting Voice Dispatch" : "ГХГААС Өргөн нэвтрүүлгийн албан ёсны дуут мэдэгдэл";
        }

        #region Menu
        public static string GetMenuFile()
        {
            return LangPack.IsEng ? "File(&F)" : "Файл";
        }

        public static string GetMenuExit()
        {
            return LangPack.IsEng ? "Exit(&X)" : "Гарах";
        }

        public static string GetMenuEdit()
        {
            return LangPack.IsEng ? "Edit(&E)" : "Засварлах";
        }

        public static string GetMenuMapIcon()
        {
            return LangPack.IsEng ? "Map Icon(&M)" : "Газрын зургийн таних тэмдэг";
        }

        public static string GetMenuGrouping()
        {
            return LangPack.IsEng ? "Grouping(&G)" : "Бүлэглэх";
        }

        public static string GetMenuStrMng()
        {
            return LangPack.IsEng ? "Stored Message(&S)" : "Хадгалагдсан мэдээлэл";
        }

        public static string GetStoredMsg()
        {
            return LangPack.IsEng ? "Stored Message" : "Хадгалагдсан мэдээлэл";
        }

        public static string GetMenuCG()
        {
            return LangPack.IsEng ? "CG(&C)" : "Урсдаг зарын бичвэр";
        }

        public static string GetMenuOperMode()
        {
            return LangPack.IsEng ? "Operation Mode(&O)" : "Aшиглалтын горим";
        }

        public static string GetMenuEditMode()
        {
            return LangPack.IsEng ? "Edit Mode(&E)" : "Засварлах горим";
        }

        public static string GetMenuSetting()
        {
            return LangPack.IsEng ? "Setting(&S)" : "Тохиргоо";
        }

        public static string GetMenuAutoAlert()
        {
            return LangPack.IsEng ? "Auto Alert(&A)" : "Автомат түгшүүр";
        }

        public static string GetMenuOption()
        {
            return LangPack.IsEng ? "Option(&O)" : "Тохируулга";
        }

        public static string GetMenuAbout()
        {
            return LangPack.IsEng ? "About(&A)" : "Мэдээлэл";
        }

        public static string GetMenuInfo()
        {
            return LangPack.IsEng ? "Program Information(&I)" : "Програмын мэдээлэл";
        }

        public static string GetMenuSiren()
        {
            return LangPack.IsEng ? "Siren" : "Дуут дохиолол";
        }

        public static string GetMenuAlertText()
        {
            return LangPack.IsEng ? "Alert Text" : "Түгшүүрийн дохионы эх";
        }
        #endregion

        public static string GetMode()
        {
            return LangPack.IsEng ? "▶ Mode" : "▶ Горим";
        }

        public static string GetReal()
        {
            return LangPack.IsEng ? "REAL" : "Бодит";
        }

        public static string GetDrill()
        {
            return LangPack.IsEng ? "DRILL" : "Сургуулилт";
        }

        public static string GetTest()
        {
            return LangPack.IsEng ? "TEST" : "Туршилт";
        }

        public static string GetMedia()
        {
            return LangPack.IsEng ? "▶ Media" : "▶ Мэдээллийн хэрэгсэл";
        }

        public static string GetAll()
        {
            return LangPack.IsEng ? "ALL" : "Бүгд";
        }

        public static string GetVhf()
        {
            return LangPack.IsEng ? "VHF" : "Радио\nхолбоо";
        }

        public static string GetSat()
        {
            return LangPack.IsEng ? "SAT" : "Сансрын холбоо";
        }

        public static string GetBase()
        {
            return LangPack.IsEng ? "BASE" : "Үндсэн";
        }

        public static string GetDest()
        {
            return LangPack.IsEng ? "▶ Destination" : "▶ Байршил";
        }

        public static string GetGroup()
        {
            return LangPack.IsEng ? "GROUP" : "Бүлэг";
        }

        public static string GetIndi()
        {
            return LangPack.IsEng ? "INDIVIDUAL" : "Терминал тус\nбүрээр";
        }

        public static string GetDist()
        {
            return LangPack.IsEng ? "DIST" : "Дүүрэг";
        }

        public static string GetAlertKind()
        {
            return LangPack.IsEng ? "▶ Alert Kind" : "▶ Дохионы төрөл";
        }

        public static string GetReady()
        {
            return LangPack.IsEng ? "READY" : "Сирень дохио";
        }

        public static string GetSiren()
        {
            return LangPack.IsEng ? "SIREN" : "Дуут дохиолол";
        }

        public static string GetSiren1()
        {
            return LangPack.IsEng ? "SIREN 1" : "Дуут дохиолол 1";
        }

        public static string GetSiren2()
        {
            return LangPack.IsEng ? "SIREN 2" : "Дуут дохиолол 2";
        }

        public static string GetLive()
        {
            return LangPack.IsEng ? "LIVE" : "Бодит яриа";
        }

        public static string GetStoMsg()
        {
            return LangPack.IsEng ? "Sto. MSG" : "Хадгалагдсан мессеж";
        }

        public static string GetClear()
        {
            return LangPack.IsEng ? "CLEAR" : "Арилгах";
        }

        public static string Getclear()
        {
            return LangPack.IsEng ? "Clear" : "Арилгах";
        }

        public static string GetTVControl()
        {
            return LangPack.IsEng ? "▶ TV Control" : "▶ ТВ-ийн хяналт";
        }

        public static string GetAudio()
        {
            return LangPack.IsEng ? "AUDIO" : "Аудио";
        }

        public static string GetCGDisplay()
        {
            return LangPack.IsEng ? "CG" : "   Урсдаг зар";
        }

        public static string GetConfirmTitle()
        {
            return LangPack.IsEng ? "▶ Confirm" : "▶ Баталгаажуулах";
        }

        public static string GetConfirm()
        {
            return LangPack.IsEng ? "CONFIRM" : "Баталгаа\nжуулах";
        }

        public static string GetLivecast()
        {
            return LangPack.IsEng ? "▶ LiveCast" : "▶ Бодит яриа нэвтрүүлэх";
        }

        public static string GetLiveStart()
        {
            return LangPack.IsEng ? "LIVECAST START" : "Бодит яриа эхлэх";
        }

        public static string GetLiveEnd()
        {
            return LangPack.IsEng ? "LIVECAST END" : "Бодит яриа дуусах";
        }

        public static string GetEtc()
        {
            return LangPack.IsEng ? "▶ Etc" : "▶ Бусад";
        }

        public static string GetSnEtc()
        {
            return LangPack.IsEng ? "Etc" : "Бусад";
        }

        public static string GetPreStep()
        {
            return LangPack.IsEng ? "PREVIOUS STEP" : "Өмнөх үйлдэл";
        }

        public static string GetCancel()
        {
            return LangPack.IsEng ? "CANCEL" : "Цуцлах";
        }

        public static string Getcancel()
        {
            return LangPack.IsEng ? "Cancel" : "Цуцлах";
        }

        public static string GetOperationMng()
        {
            return LangPack.IsEng ? "Operation Management       " : "Ашиглалтын удирдлага       ";
        }

        public static string GetControlPanel()
        {
            return LangPack.IsEng ? "Control Panel" : "Хяналтын самбар";
        }

        #region Livecast
        public static string GetSelLive()
        {
            return LangPack.IsEng ? "▶ Select Livecast" : "▶ Бодит яриа сонгох";
        }

        public static string GetLiveMic()
        {
            return LangPack.IsEng ? "MIC" : "Микрофон";
        }

        public static string GetLiveRecord()
        {
            return LangPack.IsEng ? "RECORD" : "Бичлэг";
        }

        public static string GetOnAir()
        {
            return LangPack.IsEng ? "ON-AIR" : "Нэвтрүүлэг явагдаж байна";
        }
        #endregion

        #region Stored Message
        public static string GetSelMsg()
        {
            return LangPack.IsEng ? "▶ Select Stored Message" : "▶ Хадгалагдсан мессеж сонгох";
        }

        public static string GetProgressMsg()
        {
            return LangPack.IsEng ? "[STORED MESSAGE] " : "[мессеж] ";
        }

        public static string GetMsgNum()
        {
            return LangPack.IsEng ? "Number" : "Мессежийн дугаар";
        }

        public static string GetMsgName()
        {
            return LangPack.IsEng ? "Name" : "Мессежийн нэр";
        }

        public static string GetStoNum()
        {
            return LangPack.IsEng ? "Num" : "Дугаар";
        }

        public static string GetStoContext()
        {
            return LangPack.IsEng ? "Context" : "Агууламж";
        }
        #endregion

        #region Siren
        public static string GetSelSiren()
        {
            return LangPack.IsEng ? "▶ Select Siren" : "▶ Сирень дохио сонгох";
        }

        public static string GetSirenNum()
        {
            return LangPack.IsEng ? "Number" : "Сирень дохио дугаар";
        }
        #endregion

        public static string GetSelect()
        {
            return LangPack.IsEng ? "SELECT" : "Сонгох";
        }

        public static string Getselect()
        {
            return LangPack.IsEng ? "Select" : "Сонгох";
        }

        public static string GetAllSelect()
        {
            return LangPack.IsEng ? "All Select" : "Бүгдийг сонгох";
        }

        public static string GetReptCnt()
        {
            return LangPack.IsEng ? "Repeat Count" : "Давталтын тоо";
        }

        public static string GetSelTerm()
        {
            return LangPack.IsEng ? "▶ Term Select" : "▶ Терминал сонгох";
        }

        public static string GetTermName()
        {
            return LangPack.IsEng ? "Term Name" : "Терминалын нэр";
        }

        public static string GetSelDist()
        {
            return LangPack.IsEng ? "▶ Dist Select" : "▶ Дүүрэг сонгох";
        }

        public static string GetDistName()
        {
            return LangPack.IsEng ? "Dist Name" : "Дүүргийн нэр";
        }

        public static string GetSelGroup()
        {
            return LangPack.IsEng ? "▶ Group Select" : "▶ Бүлэг сонголт";
        }

        public static string GetGroupName()
        {
            return LangPack.IsEng ? "Group Name" : "Бүлгийн нэр";
        }

        public static string GetNo()
        {
            return LangPack.IsEng ? "No." : "Дэс дугаар";
        }

        public static string GetCount()
        {
            return LangPack.IsEng ? "Count" : "Терминалын тоо";
        }

        public static string GetSec()
        {
            return LangPack.IsEng ? "Sec" : "Сек";
        }

        public static string GetMin()
        {
            return LangPack.IsEng ? "Min" : "мин";
        }

        public static string GetNumber()
        {
            return LangPack.IsEng ? "Number" : "Дэс дугаар";
        }

        public static string GetTime()
        {
            return LangPack.IsEng ? "Time" : "Хугацаа";
        }

        public static string GetHigh()
        {
            return LangPack.IsEng ? "High" : "Өндөр";
        }

        public static string GetLow()
        {
            return LangPack.IsEng ? "Low" : "Бага";
        }

        public static string GetAuto()
        {
            return LangPack.IsEng ? "Auto" : "Автомат";
        }

        public static string GetManual()
        {
            return LangPack.IsEng ? "Manual" : "Гар ажиллагаа";
        }

        public static string GetInterval()
        {
            return LangPack.IsEng ? "Interval" : "Хоорондын зай";
        }

        public static string GetNonuse()
        {
            return LangPack.IsEng ? "Nonuse" : "Ашиглахгүй";
        }

        public static string GetUse()
        {
            return LangPack.IsEng ? "Use" : "Ашиглах";
        }

        public static string GetUsed()
        {
            return LangPack.IsEng ? "Used" : "Ашиглагдсан";
        }

        public static string GetUnused()
        {
            return LangPack.IsEng ? "Unused" : "Ашиглагдаагүй";
        }

        public static string GetName()
        {
            return LangPack.IsEng ? "Name" : "Нэр";
        }

        public static string GetText()
        {
            return LangPack.IsEng ? "Text" : "Текст";
        }

        public static string GetUseCheck()
        {
            return LangPack.IsEng ? "Use Check" : "Хэрэглэх эсэх";
        }

        public static string GetConnSucc()
        {
            return LangPack.IsEng ? "[Connect Sueecss]" : "[Холболт амжилттай боллоо]";
        }

        public static string GetConnFail()
        {
            return LangPack.IsEng ? "[Connect Fail]" : "[Холболт амжилтгүй боллоо]";
        }

        public static string GetNewName()
        {
            return LangPack.IsEng ? "New Name" : "Шинэ нэр";
        }

        public static string GetNewGroup()
        {
            return LangPack.IsEng ? "New Group" : "Шинэ Бүлэг";
        }

        public static string GetItem()
        {
            return LangPack.IsEng ? "Item" : "Хэсэг";
        }

        public static string GetSirenType()
        {
            return LangPack.IsEng ? "Siren Type" : "Дуут дохионы төрөл";
        }

        public static string GetStoMsgType()
        {
            return LangPack.IsEng ? "Stored Message Type" : "Хадгалагдсан мессежний төрөл";
        }

        public static string GetAdd()
        {
            return LangPack.IsEng ? "Add" : "нэмэх";
        }

        public static string GetEdit()
        {
            return LangPack.IsEng ? "Edit" : "засварлах";
        }

        public static string GetDelete()
        {
            return LangPack.IsEng ? "Delete" : "устгах";
        }

        public static string GetClose()
        {
            return LangPack.IsEng ? "Close" : "Хаах";
        }

        public static string GetKind()
        {
            return LangPack.IsEng ? "Kind" : "төрөл";
        }

        public static string GetKindNum()
        {
            return LangPack.IsEng ? "Kind Num" : "Төрлийн дугаар";
        }

        public static string GetContext()
        {
            return LangPack.IsEng ? "Context" : "Агууламж";
        }

        public static string GetSave()
        {
            return LangPack.IsEng ? "Save" : "Хадгалах";
        }

        public static string GetPlay()
        {
            return LangPack.IsEng ? "Play" : "Тоглуулах";
        }

        public static string GetStop()
        {
            return LangPack.IsEng ? "Stop" : "Зогсоох";
        }

        public static string GetPreview()
        {
            return LangPack.IsEng ? "Preview" : "урьдчилан харах";
        }

        public static string GetTrue()
        {
            return LangPack.IsEng ? "True" : "○";
        }

        public static string GetFalse()
        {
            return LangPack.IsEng ? "False" : "X";
        }

        public static string GetNone()
        {
            return LangPack.IsEng ? "none" : "-";
        }

        public static string GetCutAdd()
        {
            return LangPack.IsEng ? "Cut Add" : "Текстэд зураг нэмэх";
        }

        public static string GetCut()
        {
            return LangPack.IsEng ? "Cut" : "Текстэд орох зураг";
        }

        public static string GetLanguage()
        {
            return LangPack.IsEng ? "Language" : "Хэл сонгох";
        }

        public static string GetEnglish()
        {
            return LangPack.IsEng ? "English" : "Англи";
        }

        public static string GetMongolian()
        {
            return LangPack.IsEng ? "Mongolian" : "Монгол";
        }

        public static string GetOption()
        {
            return LangPack.IsEng ? "Option" : "Тохируулга";
        }

        public static string GetDB()
        {
            return LangPack.IsEng ? "DB" : "Өгөгдлийн сан";
        }

        public static string GetTCP1()
        {
            return LangPack.IsEng ? "TCP 1" : "Дамжуулал хянах протокол 1";
        }

        public static string GetTCP2()
        {
            return LangPack.IsEng ? "TCP 2" : "Дамжуулал хянах протокол 2";
        }

        public static string GetLog()
        {
            return LangPack.IsEng ? "Log" : "Нэвтрэх";
        }

        public static string GetBuzzer()
        {
            return LangPack.IsEng ? "Buzzer" : "Дохиолол";
        }

        public static string GetDBIP()
        {
            return LangPack.IsEng ? "IP" : "IP";
        }

        public static string GetDBSID()
        {
            return LangPack.IsEng ? "SID" : "SID";
        }

        public static string GetDBID()
        {
            return LangPack.IsEng ? "ID" : "Нэвтрэх хаяг";
        }

        public static string GetDBPW()
        {
            return LangPack.IsEng ? "PW" : "Нууц үг";
        }

        public static string GetDBTest()
        {
            return LangPack.IsEng ? "DB Test" : "Өгөгдлийн сан тест";
        }

        public static string Get8IDUSE()
        {
            return LangPack.IsEng ? "Oracle8 Release 8.0 ID USE" : "Oracle8 хувилбарын нэвтрэх хаяг ашиглах";
        }

        public static string GetDBConnSet()
        {
            return LangPack.IsEng ? "※ DB Connection Setting" : "※ Өгөгдлийн сангийн холболтын тохиргоо";
        }

        public static string GetTCPIP()
        {
            return LangPack.IsEng ? "IP" : "Интернет протокол";
        }

        public static string GetTCPPORT()
        {
            return LangPack.IsEng ? "PORT" : "Порт";
        }

        public static string GetTCPConnect()
        {
            return LangPack.IsEng ? "Connect" : "Холболт";
        }

        public static string GetTCPClose()
        {
            return LangPack.IsEng ? "Close" : "Хаах";
        }

        public static string GetTCPResult()
        {
            return LangPack.IsEng ? "Result" : "Үр дүн";
        }

        public static string GetTCPSuccess()
        {
            return LangPack.IsEng ? "Success" : "Амжилттай боллоо";
        }

        public static string GetTCPFail()
        {
            return LangPack.IsEng ? "Fail" : "Амжилтгүй боллоо";
        }

        public static string GetOperationManagementTCPSetting()
        {
            return LangPack.IsEng ? "※ Operation Management TCP Setting" : "※ Ашиглалтын удирдлага ТСР тохиргоо";
        }

        public static string GetControlPanelTCPSetting()
        {
            return LangPack.IsEng ? "※ Control Panel TCP Setting" : "※ Хяналтын самбарын ТСР тохиргоо";
        }

        public static string GetLogView()
        {
            return LangPack.IsEng ? "Log View" : "Нэвтэрсэнийг харуулах";
        }

        public static string GetTheLogwillscreen()
        {
            return LangPack.IsEng ? "※ The Log will be on screen." : "※ Нэвтэрсэн нь дэлгэц дээр харагдана.";
        }

        public static string GetCanslowprogram()
        {
            return LangPack.IsEng ? "Can slow down the program." : "Програм удааширч болзошгүй.";
        }

        public static string GetBroadCastBuzzerSet()
        {
            return LangPack.IsEng ? "※ BroadCast Buzzer Setting" : "※ Нэвтрүүлгийн дохиолол тохируулах";
        }

        public static string GetStoredMessageNumber()
        {
            return LangPack.IsEng ? "Stored Message Number" : "Хадгалагдсан мессежийн дугаар";
        }

        public static string GetMongolian(string stEn)
        {
            if (LangPack.IsEng)
                return stEn;

            string stTemp = string.Empty;

            switch (stEn)
            {
                case "Please select the items to modify.":
                    stTemp = "Засварлах хэсгийг сонгоно уу!";
                    break;

                case "Please select the items to delete.":
                    stTemp = "Устгах хэсгийг сонгоно уу!";
                    break;

                case "Please select signboard text to preview.":
                    stTemp = "Урьдчилан харах текст мэдээллийн самбарын бичвэрээ сонгоно уу!";
                    break;

                case "Please select one signboard text to preview.":
                    stTemp = "Урьдчилан харах текст мэдээллийн самбарын зөвхөн нэг бичвэр сонгоно уу!";
                    break;

                case "Please select CG text to preview.":
                    stTemp = "Урьдчилан харах текст мэдээллийн самбарын бичвэрээ сонгоно уу!";
                    break;

                case "Please select one CG text to preview.":
                    stTemp = "Урьдчилан харах текст мэдээллийн самбарын зөвхөн нэг бичвэр сонгоно уу!";
                    break;

                case "Please select signboard text to modify.":
                    stTemp = "Засварлах текст мэдээллийн самбарын бичвэрээ сонгоно уу!";
                    break;

                case "Please select signboard text to delete.":
                    stTemp = "Устгах текст мэдээллийн самбарын бичвэрээ сонгоно уу!";
                    break;

                case "Please choose the CG text to delete.":
                    stTemp = "Устгах текст мэдээллийн самбарын бичвэрээ сонгоно уу!";
                    break;

                case "Want to transfer command to clear signboard text?":
                    stTemp = "Текст мэдээллийн самбар устгах тушаалыг терминал руу дамжуулах уу?";
                    break;

                case "Transfer has been completed.":
                    stTemp = "Дамжуулалт амжилттай боллоо.";
                    break;

                case "Want to delete?":
                    stTemp = "Устгах уу?";
                    break;

                case "Deleted.":
                    stTemp = "Устгагдлаа.";
                    break;

                case "Deleted":
                    stTemp = "Устгагдлаа.";
                    break;

                case "Please enter the signboard title.":
                    stTemp = "Текст мэдээллийн самбарын нэрийг оруулна уу!";
                    break;

                case "Please enter the signboard text.":
                    stTemp = "Текст мэдээллийн самбарын бичвэрийг оруулна уу!";
                    break;

                case "Please enter the CG title.":
                    stTemp = "Текст мэдээллийн самбарын нэрийг оруулна уу!";
                    break;

                case "Please enter the CG text.":
                    stTemp = "Текст мэдээллийн самбарын бичвэрийг оруулна уу!";
                    break;

                case "Stored.":
                    stTemp = "Хадгалагдлаа.";
                    break;

                case "Stored":
                    stTemp = "Хадгалагдлаа.";
                    break;

                case "Please register kind first.":
                    stTemp = "Мэдээллийн төрлийг бүртгүүлнэ үү!";
                    break;

                case "The same title already exists. Please enter check again after.":
                    stTemp = "Ижил төстэй нэр хадгалагдсан байна. Нэрээ шалгаад дахин бүртгүүлнэ үү!";
                    break;

                case "Modified.":
                    stTemp = "Засвардлагдлаа.";
                    break;

                case "Modified":
                    stTemp = "Засвардлагдлаа.";
                    break;

                case "Text can not exceed the 900 byte.":
                    stTemp = "Бичвэр нь 900 битээс хэтрэх ёсгүй.";
                    break;

                case "Invalid format.":
                    stTemp = "Буруу формат байна.";
                    break;

                case "Please select terminal.":
                    stTemp = "Терминалыг сонгоно уу!";
                    break;

                case "Please select promotional text.":
                    stTemp = "Зар мэдээллийг сонгоно уу!";
                    break;

                case "Want to transfer to terminal?":
                    stTemp = "Терминал руу дамжуулах уу?";
                    break;

                case "Please select group to modify.":
                    stTemp = "Засварлах бүлгийг сонгоно уу!";
                    break;

                case "Please select the group to modify.":
                    stTemp = "Засварлах бүлгийг сонгоно уу!";
                    break;

                case "Failed.":
                    stTemp = "Амжилтгүй боллоо.";
                    break;

                case "Failed":
                    stTemp = "Амжилтгүй боллоо.";
                    break;

                case "Please select terminal/province/district. Want to overwite?":
                    stTemp = "Терминалын аймаг, дүүргийг сонгоно уу!";
                    break;

                case "Please select terminal/province/district.":
                    stTemp = "Терминалын аймаг, дүүргийг сонгоно уу!";
                    break;

                case "Please select group.":
                    stTemp = "Бүлгээ сонгоно уу!";
                    break;

                case "A group of up to sixteen members.":
                    stTemp = "Нэг бүлэгт 8 хүртэл гишүүн байна.";
                    break;

                case "Please enter the name.":
                    stTemp = "Нэр оруулна уу!";
                    break;

                case "Terminals can not be selected to issue more than sixteen.":
                    stTemp = "Команд буулгах зорилтот хэсэг нь 8-аас дээш сонгох боломжгүй.";
                    break;

                case "Please select terminal to issue.":
                    stTemp = "Терминалаа сонгоно уу!";
                    break;

                case "Please select scenario to modify.":
                    stTemp = "Засварлах сценариог сонгоно уу!";
                    break;

                case "Please select scenario to delete.":
                    stTemp = "Устгах сценариог сонгоно уу!";
                    break;

                case "Want to delete content of the scenario?":
                    stTemp = "Сценариогийн агууламжыг устгах уу?";
                    break;

                case "Scenario content has been deleted.":
                    stTemp = "Сценариогийн агууламж устгагдлаа.";
                    break;

                case "Please select a Alert Type.":
                    stTemp = "Түгшүүрийн төрлийг сонгоно уу!";
                    break;

                case "Can not add more than 30.":
                    stTemp = "30 -с илүү мэдээлэл оруулах боломжгүй.";
                    break;

                case "Please enter scenario name.":
                    stTemp = "Сценариогийн нэрийг оруулна уу!";
                    break;

                case "Please enter scenario steps.":
                    stTemp = "Сценарио үе шатыг үүсгэнэ үү!";
                    break;

                case "The same name already exists. Please enter check again after.":
                    stTemp = "Ижил төстэй нэр хадгалагдсан байна. Нэрээ шалгаад дахин бүртгүүлнэ үү!";
                    break;

                case "Want to cancel the scenario alert?":
                    stTemp = "Сценарио түгшүүр цуцлах уу?";
                    break;

                case "Please select message text to modify.":
                    stTemp = "Засварлах мессежээ сонгоно уу!";
                    break;

                case "Please select message text to delete.":
                    stTemp = "Устгах мессежээ сонгоно уу!";
                    break;

                case "Please choose the stored message to play.":
                    stTemp = "Тоглуулах хадгалагдсан мессежээ сонгоно уу!";
                    break;

                case "Sorry. Can not play sound because file don't exists.":
                    stTemp = "Уучлаарай! Сонгосон тоглуулах файл байхгүй байна.";
                    break;

                case "Please enter message Text.":
                    stTemp = "Мессежээ оруулна уу!";
                    break;

                case "Message already exists. Want to overwrite it?":
                    stTemp = "Ийм мессеж байна. Дарж хуулах уу?";
                    break;

                case "Please select scenario.":
                    stTemp = "Сценариогоо сонгоно уу!";
                    break;

                case "Want to cancel the auto alert?":
                    stTemp = "Автомат түгшүүрийг цуцлах уу?";
                    break;

                case "Password error.":
                    stTemp = "Нууц дугаар алдаатай байна.";
                    break;

                case "Password error":
                    stTemp = "Нууц дугаар алдаатай байна.";
                    break;

                case "The program is already running.":
                    stTemp = "Програм явагдаж байна.";
                    break;

                case "Data Load":
                    stTemp = "Өгөгдөл ачаалж байна.";
                    break;

                case "The basic configuration has been loaded from the local storage.\nAfter checking the DB connection settings, please restart.":
                    stTemp = "Үндсэн тохируулгыг дотоод санах байгууламжаас ачаалсан.\nӨгөгдлийн сангийн холболтыг шалгасны дараа дахин эхлүүлнэ үү!";
                    break;

                case "The basic configuration could not be loaded.\nAfter checking the DB connection settings, please restart.":
                    stTemp = "Дотоод өгөгдлийг ачааллаж чадсангүй.\nӨгөгдлийн сангийн холболтыг шалгасны дараа дахин эхлүүлнэ үү!";
                    break;

                case "Want to exit the program?":
                    stTemp = "Програмаас гарах уу?";
                    break;

                case "Terminal location is not accurate.":
                    stTemp = "Терминалын байршлын мэдээлэл тодорхой биш байна.";
                    break;

                case "Scenario is not registered.":
                    stTemp = "Бүртгэгдээгүй сценарио байна.";
                    break;

                case "Current alert state. \nCanceled by the terminal can not be canceled. \nReally sure you want to cancel?":
                    stTemp = "Одоогоор команд буулгасан статустай байна. \nЦуцласан ч гэсэн терминалаас устахгүй. \nГэсэн хэдий ч цуцлах уу?";
                    break;

                case "Please select one signboard text to modify.":
                    stTemp = "Өөрчлөлт оруулах нэг текст мэдээллийн самбарын бичвэрээ сонгоно уу!";
                    break;

                case "Please enter IP.":
                    stTemp = "IP-гаа оруулна уу.";
                    break;

                case "Please enter Port.":
                    stTemp = "Port-оо оруулна уу.";
                    break;

                case "Please enter DB IP.":
                    stTemp = "Өгөгдлийн IP-гаа оруулна уу.";
                    break;

                case "Please enter DB ID.":
                    stTemp = "Өгөгдөлд нэвтрэх нэрээ оруулна уу.";
                    break;

                case "Please enter DB password.":
                    stTemp = "Өгөгдлийн нууц дугаараа оруулна уу.";
                    break;

                case "Please enter DB SID.":
                    stTemp = "Өгөгдлийн SID-г оруулна уу";
                    break;

                case "Warning":
                    stTemp = "Түгшүүр";
                    break;

                case "Please select stored message.":
                    stTemp = "Хадгалсан мессежээ сонгоно уу.";
                    break;

                case "Please choose the siren to play.":
                    stTemp = "Тоглуулах дуут дохиоллоо сонгоно уу.";
                    break;

                case "Please enter the title.":
                    stTemp = "Гарчиг оруулна уу";
                    break;

                case "Please enter this message text.":
                    stTemp = "Энэ мессеж бичвэрийг оруулна уу.";
                    break;

                case "The basic data changed. please restart":
                    stTemp = "The basic data changed. please restart";
                    break;

                case "Setting data send fail.":
                    stTemp = "Setting data send fail.";
                    break;
            }

            return stTemp;
        }
    }
}