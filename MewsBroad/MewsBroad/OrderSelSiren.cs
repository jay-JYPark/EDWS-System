using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Mews.Svr.Broad
{
    public partial class OrderSelSiren : UserControl
    {
        #region Events
        public event InvokeValueOne<byte> evtSirenNum;
        private SirenDataMng sirenMng = null;
        private List<GlassButton> lstBtn = new List<GlassButton>();
        #endregion

        public OrderSelSiren()
        {
            InitializeComponent();
        }

        public void Init()
        {
            #region Init Button List
            lstBtn.Add(btnS1);
            lstBtn.Add(btnS2);
            lstBtn.Add(btnS3);
            lstBtn.Add(btnS4);
            lstBtn.Add(btnS5);
            lstBtn.Add(btnS6);
            lstBtn.Add(btnS7);
            lstBtn.Add(btnS8);
            lstBtn.Add(btnS9);
            lstBtn.Add(btnS10);
            lstBtn.Add(btnS11);
            lstBtn.Add(btnS12);
            lstBtn.Add(btnS13);
            lstBtn.Add(btnS14);
            lstBtn.Add(btnS15);
            lstBtn.Add(btnS16);
            lstBtn.Add(btnS17);
            lstBtn.Add(btnS18);
            lstBtn.Add(btnS19);
            lstBtn.Add(btnS20);
            #endregion
        }

        public void SetButton()
        {
            sirenMng = SirenDataMng.GetSirenMng();

            foreach (KeyValuePair<string, SirenInfo> pair in sirenMng.dicSiren)
            {
                SirenInfo s = pair.Value;

                lstBtn[Convert.ToInt16(s.SirenNum) - 1].Text = s.SirenName;
                lstBtn[Convert.ToInt16(s.SirenNum) - 1].Tag = s.SirenNum;
            }
        }

        private void btnS_Click(object sender, EventArgs e)
        {
            if (evtSirenNum != null)
                evtSirenNum(Convert.ToByte((sender as GlassButton).Tag));
        }
    }
}