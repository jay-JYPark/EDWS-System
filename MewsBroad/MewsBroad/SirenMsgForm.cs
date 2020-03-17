using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Mews.Svr.Broad
{
    public partial class SirenMsgForm : Form
    {
        #region Fields
        private SirenDataMng sirenMng = null;
        #endregion

        public SirenMsgForm()
        {
            InitializeComponent();

            sirenMng = SirenDataMng.GetSirenMng();
            Init();
        }

        private void Init()
        {
            #region List Header
            ColumnHeader h0 = new ColumnHeader();
            h0.Text = "...";
            h0.Width = 10;
            this.lvSiren.Columns.Add(h0);

            ColumnHeader h1 = new ColumnHeader();
            h1.Text = "No";
            h1.Width = 50;
            this.lvSiren.Columns.Add(h1);

            ColumnHeader h2 = new ColumnHeader();
            h2.Text = "Name";
            h2.Width = 180;
            this.lvSiren.Columns.Add(h2);

            ColumnHeader h3 = new ColumnHeader();
            h3.Text = "Time";
            h3.Width = 150;
            this.lvSiren.Columns.Add(h3);

            ColumnHeader h4 = new ColumnHeader();
            h4.Text = "Context";
            h4.Width = 200;
            this.lvSiren.Columns.Add(h4);
            #endregion

            AddListViewItem();
        }

        private void AddListViewItem()
        {
            lvSiren.Items.Clear();

            foreach (KeyValuePair<string, SirenInfo> pair in sirenMng.dicSiren)
            {
                SirenInfo siren = pair.Value;

                ListViewItem item = new ListViewItem();
                item.Text = "";
                item.Tag = siren;
                lvSiren.Items.Add(item);

                item.SubItems.Add(siren.SirenNum);
                item.SubItems.Add(siren.SirenName);
                item.SubItems.Add(siren.SirenTime.ToString() + " sec");
                item.SubItems.Add(siren.SirenContext);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            SirenEditForm form = new SirenEditForm();
            form.ShowDialog();

            AddListViewItem();
        }
    }
}