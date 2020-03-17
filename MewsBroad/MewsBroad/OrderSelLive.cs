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
    public partial class OrderSelLive : UserControl
    {
        #region Events
        public event InvokeValueOne<byte> evtLiveType;
        #endregion

        public OrderSelLive()
        {
            InitializeComponent();

            label2.Text = LangPack.GetSelLive();
            btnMic.Text = LangPack.GetLiveMic();
            btnRec.Text = LangPack.GetLiveRecord();
        }

        public void btnMic_Click(object sender, EventArgs e)
        {
            if (evtLiveType != null)
                evtLiveType(0);
        }

        public void btnRec_Click(object sender, EventArgs e)
        {
            if (evtLiveType != null)
                evtLiveType(1);
        }
    }
}
