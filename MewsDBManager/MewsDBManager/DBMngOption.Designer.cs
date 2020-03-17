namespace Mews.Svr.DBMng
{
    partial class DBMngOption
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.MainTC = new Adeng.Framework.Ctrl.TabControlEx();
            this.DBTP = new System.Windows.Forms.TabPage();
            this.Oracle8CB = new System.Windows.Forms.CheckBox();
            this.DbTestRstLB = new Adeng.Framework.Ctrl.LabelEx();
            this.DbTestBtn = new Adeng.Framework.Ctrl.ButtonEx();
            this.DbLB = new Adeng.Framework.Ctrl.LabelEx();
            this.DbPwTB = new Adeng.Framework.Ctrl.TextBoxEx();
            this.DbPwLB = new Adeng.Framework.Ctrl.LabelEx();
            this.DbIdTB = new Adeng.Framework.Ctrl.TextBoxEx();
            this.DbIdLB = new Adeng.Framework.Ctrl.LabelEx();
            this.DbSidTB = new Adeng.Framework.Ctrl.TextBoxEx();
            this.DbSidLB = new Adeng.Framework.Ctrl.LabelEx();
            this.DbIPTB = new Adeng.Framework.Ctrl.TextBoxEx();
            this.DbIPLB = new Adeng.Framework.Ctrl.LabelEx();
            this.TcpTP = new System.Windows.Forms.TabPage();
            this.Tcp1StateLB = new System.Windows.Forms.Label();
            this.TcpCloseBtn = new Adeng.Framework.Ctrl.ButtonEx();
            this.TcpConBtn = new Adeng.Framework.Ctrl.ButtonEx();
            this.TcpPortTB = new Adeng.Framework.Ctrl.TextBoxEx();
            this.TcpPortLB = new Adeng.Framework.Ctrl.LabelEx();
            this.TcpIpTB = new Adeng.Framework.Ctrl.TextBoxEx();
            this.TcpIpLB = new Adeng.Framework.Ctrl.LabelEx();
            this.TcpLB = new Adeng.Framework.Ctrl.LabelEx();
            this.UdpTP = new System.Windows.Forms.TabPage();
            this.UdpPortTB = new Adeng.Framework.Ctrl.TextBoxEx();
            this.UdpPortLB = new Adeng.Framework.Ctrl.LabelEx();
            this.UdpIpTB = new Adeng.Framework.Ctrl.TextBoxEx();
            this.UdpIpLB = new Adeng.Framework.Ctrl.LabelEx();
            this.UdpLB = new Adeng.Framework.Ctrl.LabelEx();
            this.LogTP = new System.Windows.Forms.TabPage();
            this.LogCB = new System.Windows.Forms.CheckBox();
            this.LogLB = new Adeng.Framework.Ctrl.LabelEx();
            this.SaveBtn = new Adeng.Framework.Ctrl.ButtonEx();
            this.CancleBtn = new Adeng.Framework.Ctrl.ButtonEx();
            this.OKBtn = new Adeng.Framework.Ctrl.ButtonEx();
            this.TopPN = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.MainPB = new System.Windows.Forms.PictureBox();
            this.MainTC.SuspendLayout();
            this.DBTP.SuspendLayout();
            this.TcpTP.SuspendLayout();
            this.UdpTP.SuspendLayout();
            this.LogTP.SuspendLayout();
            this.TopPN.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainPB)).BeginInit();
            this.SuspendLayout();
            // 
            // MainTC
            // 
            this.MainTC.BgColor = System.Drawing.Color.White;
            this.MainTC.Controls.Add(this.DBTP);
            this.MainTC.Controls.Add(this.TcpTP);
            this.MainTC.Controls.Add(this.UdpTP);
            this.MainTC.Controls.Add(this.LogTP);
            this.MainTC.FgColor = System.Drawing.Color.Black;
            this.MainTC.ItemSize = new System.Drawing.Size(40, 25);
            this.MainTC.Location = new System.Drawing.Point(12, 73);
            this.MainTC.Name = "MainTC";
            this.MainTC.SelectedIndex = 0;
            this.MainTC.Size = new System.Drawing.Size(409, 309);
            this.MainTC.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.MainTC.TabIndex = 0;
            // 
            // DBTP
            // 
            this.DBTP.Controls.Add(this.Oracle8CB);
            this.DBTP.Controls.Add(this.DbTestRstLB);
            this.DBTP.Controls.Add(this.DbTestBtn);
            this.DBTP.Controls.Add(this.DbLB);
            this.DBTP.Controls.Add(this.DbPwTB);
            this.DBTP.Controls.Add(this.DbPwLB);
            this.DBTP.Controls.Add(this.DbIdTB);
            this.DBTP.Controls.Add(this.DbIdLB);
            this.DBTP.Controls.Add(this.DbSidTB);
            this.DBTP.Controls.Add(this.DbSidLB);
            this.DBTP.Controls.Add(this.DbIPTB);
            this.DBTP.Controls.Add(this.DbIPLB);
            this.DBTP.Location = new System.Drawing.Point(4, 29);
            this.DBTP.Name = "DBTP";
            this.DBTP.Padding = new System.Windows.Forms.Padding(3);
            this.DBTP.Size = new System.Drawing.Size(401, 276);
            this.DBTP.TabIndex = 0;
            this.DBTP.Text = "DB";
            this.DBTP.UseVisualStyleBackColor = true;
            // 
            // Oracle8CB
            // 
            this.Oracle8CB.AutoSize = true;
            this.Oracle8CB.Location = new System.Drawing.Point(142, 187);
            this.Oracle8CB.Name = "Oracle8CB";
            this.Oracle8CB.Size = new System.Drawing.Size(188, 18);
            this.Oracle8CB.TabIndex = 35;
            this.Oracle8CB.Text = "Oracle8 Rease 8.0 ID USE";
            this.Oracle8CB.UseVisualStyleBackColor = true;
            this.Oracle8CB.CheckedChanged += new System.EventHandler(this.DbIPTB_TextChanged);
            // 
            // DbTestRstLB
            // 
            this.DbTestRstLB.AutoSize = true;
            this.DbTestRstLB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DbTestRstLB.Font = new System.Drawing.Font("굴림체", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.DbTestRstLB.ForeColor = System.Drawing.Color.Black;
            this.DbTestRstLB.Location = new System.Drawing.Point(261, 92);
            this.DbTestRstLB.Name = "DbTestRstLB";
            this.DbTestRstLB.Size = new System.Drawing.Size(0, 11);
            this.DbTestRstLB.TabIndex = 34;
            // 
            // DbTestBtn
            // 
            this.DbTestBtn.CrDisableEnd = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.DbTestBtn.CrDisableStart = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.DbTestBtn.CrEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(209)))), ((int)(((byte)(201)))));
            this.DbTestBtn.CrStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.DbTestBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DbTestBtn.Location = new System.Drawing.Point(275, 52);
            this.DbTestBtn.Name = "DbTestBtn";
            this.DbTestBtn.Size = new System.Drawing.Size(86, 27);
            this.DbTestBtn.TabIndex = 33;
            this.DbTestBtn.Text = "DB Test";
            this.DbTestBtn.Click += new System.EventHandler(this.DbTestBtn_Click);
            // 
            // DbLB
            // 
            this.DbLB.AutoSize = true;
            this.DbLB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DbLB.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.DbLB.ForeColor = System.Drawing.Color.Blue;
            this.DbLB.Location = new System.Drawing.Point(22, 237);
            this.DbLB.Name = "DbLB";
            this.DbLB.Size = new System.Drawing.Size(148, 15);
            this.DbLB.TabIndex = 32;
            this.DbLB.Text = "※ CC DB Connect Setting";
            // 
            // DbPwTB
            // 
            this.DbPwTB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DbPwTB.Location = new System.Drawing.Point(123, 147);
            this.DbPwTB.MaxLength = 15;
            this.DbPwTB.Name = "DbPwTB";
            this.DbPwTB.Size = new System.Drawing.Size(114, 22);
            this.DbPwTB.TabIndex = 31;
            this.DbPwTB.TextChanged += new System.EventHandler(this.DbIPTB_TextChanged);
            // 
            // DbPwLB
            // 
            this.DbPwLB.AutoSize = true;
            this.DbPwLB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DbPwLB.Location = new System.Drawing.Point(72, 154);
            this.DbPwLB.Name = "DbPwLB";
            this.DbPwLB.Size = new System.Drawing.Size(37, 14);
            this.DbPwLB.TabIndex = 30;
            this.DbPwLB.Text = "PW :";
            // 
            // DbIdTB
            // 
            this.DbIdTB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DbIdTB.Location = new System.Drawing.Point(123, 115);
            this.DbIdTB.MaxLength = 15;
            this.DbIdTB.Name = "DbIdTB";
            this.DbIdTB.Size = new System.Drawing.Size(114, 22);
            this.DbIdTB.TabIndex = 29;
            this.DbIdTB.TextChanged += new System.EventHandler(this.DbIPTB_TextChanged);
            // 
            // DbIdLB
            // 
            this.DbIdLB.AutoSize = true;
            this.DbIdLB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DbIdLB.Location = new System.Drawing.Point(80, 122);
            this.DbIdLB.Name = "DbIdLB";
            this.DbIdLB.Size = new System.Drawing.Size(30, 14);
            this.DbIdLB.TabIndex = 28;
            this.DbIdLB.Text = "ID :";
            // 
            // DbSidTB
            // 
            this.DbSidTB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DbSidTB.Location = new System.Drawing.Point(123, 85);
            this.DbSidTB.MaxLength = 10;
            this.DbSidTB.Name = "DbSidTB";
            this.DbSidTB.Size = new System.Drawing.Size(114, 22);
            this.DbSidTB.TabIndex = 27;
            this.DbSidTB.TextChanged += new System.EventHandler(this.DbIPTB_TextChanged);
            // 
            // DbSidLB
            // 
            this.DbSidLB.AutoSize = true;
            this.DbSidLB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DbSidLB.Location = new System.Drawing.Point(71, 92);
            this.DbSidLB.Name = "DbSidLB";
            this.DbSidLB.Size = new System.Drawing.Size(38, 14);
            this.DbSidLB.TabIndex = 26;
            this.DbSidLB.Text = "SID :";
            // 
            // DbIPTB
            // 
            this.DbIPTB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DbIPTB.Location = new System.Drawing.Point(123, 54);
            this.DbIPTB.MaxLength = 15;
            this.DbIPTB.Name = "DbIPTB";
            this.DbIPTB.Size = new System.Drawing.Size(114, 22);
            this.DbIPTB.TabIndex = 25;
            this.DbIPTB.TextChanged += new System.EventHandler(this.DbIPTB_TextChanged);
            this.DbIPTB.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TcpIpTB_KeyDown);
            // 
            // DbIPLB
            // 
            this.DbIPLB.AutoSize = true;
            this.DbIPLB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DbIPLB.Location = new System.Drawing.Point(80, 61);
            this.DbIPLB.Name = "DbIPLB";
            this.DbIPLB.Size = new System.Drawing.Size(29, 14);
            this.DbIPLB.TabIndex = 24;
            this.DbIPLB.Text = "IP :";
            // 
            // TcpTP
            // 
            this.TcpTP.Controls.Add(this.Tcp1StateLB);
            this.TcpTP.Controls.Add(this.TcpCloseBtn);
            this.TcpTP.Controls.Add(this.TcpConBtn);
            this.TcpTP.Controls.Add(this.TcpPortTB);
            this.TcpTP.Controls.Add(this.TcpPortLB);
            this.TcpTP.Controls.Add(this.TcpIpTB);
            this.TcpTP.Controls.Add(this.TcpIpLB);
            this.TcpTP.Controls.Add(this.TcpLB);
            this.TcpTP.Location = new System.Drawing.Point(4, 29);
            this.TcpTP.Name = "TcpTP";
            this.TcpTP.Padding = new System.Windows.Forms.Padding(3);
            this.TcpTP.Size = new System.Drawing.Size(401, 276);
            this.TcpTP.TabIndex = 1;
            this.TcpTP.Text = "TCP";
            this.TcpTP.UseVisualStyleBackColor = true;
            // 
            // Tcp1StateLB
            // 
            this.Tcp1StateLB.AutoSize = true;
            this.Tcp1StateLB.Location = new System.Drawing.Point(262, 154);
            this.Tcp1StateLB.Name = "Tcp1StateLB";
            this.Tcp1StateLB.Size = new System.Drawing.Size(0, 14);
            this.Tcp1StateLB.TabIndex = 31;
            // 
            // TcpCloseBtn
            // 
            this.TcpCloseBtn.CrDisableEnd = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.TcpCloseBtn.CrDisableStart = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.TcpCloseBtn.CrEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(209)))), ((int)(((byte)(201)))));
            this.TcpCloseBtn.CrStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.TcpCloseBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.TcpCloseBtn.Location = new System.Drawing.Point(273, 110);
            this.TcpCloseBtn.Name = "TcpCloseBtn";
            this.TcpCloseBtn.Size = new System.Drawing.Size(86, 27);
            this.TcpCloseBtn.TabIndex = 29;
            this.TcpCloseBtn.Text = "Close";
            this.TcpCloseBtn.Click += new System.EventHandler(this.TcpCloseBtn_Click);
            // 
            // TcpConBtn
            // 
            this.TcpConBtn.CrDisableEnd = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.TcpConBtn.CrDisableStart = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.TcpConBtn.CrEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(209)))), ((int)(((byte)(201)))));
            this.TcpConBtn.CrStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.TcpConBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.TcpConBtn.Location = new System.Drawing.Point(273, 76);
            this.TcpConBtn.Name = "TcpConBtn";
            this.TcpConBtn.Size = new System.Drawing.Size(86, 27);
            this.TcpConBtn.TabIndex = 28;
            this.TcpConBtn.Text = "Connect";
            this.TcpConBtn.Click += new System.EventHandler(this.TcpConBtn_Click);
            // 
            // TcpPortTB
            // 
            this.TcpPortTB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TcpPortTB.Location = new System.Drawing.Point(128, 111);
            this.TcpPortTB.MaxLength = 5;
            this.TcpPortTB.Name = "TcpPortTB";
            this.TcpPortTB.Size = new System.Drawing.Size(114, 22);
            this.TcpPortTB.TabIndex = 27;
            this.TcpPortTB.TextChanged += new System.EventHandler(this.TcpIpTB_TextChanged);
            this.TcpPortTB.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TcpPortTB_KeyDown);
            // 
            // TcpPortLB
            // 
            this.TcpPortLB.AutoSize = true;
            this.TcpPortLB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.TcpPortLB.Location = new System.Drawing.Point(65, 115);
            this.TcpPortLB.Name = "TcpPortLB";
            this.TcpPortLB.Size = new System.Drawing.Size(49, 14);
            this.TcpPortLB.TabIndex = 26;
            this.TcpPortLB.Text = "PORT :";
            // 
            // TcpIpTB
            // 
            this.TcpIpTB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TcpIpTB.Location = new System.Drawing.Point(128, 79);
            this.TcpIpTB.MaxLength = 15;
            this.TcpIpTB.Name = "TcpIpTB";
            this.TcpIpTB.Size = new System.Drawing.Size(114, 22);
            this.TcpIpTB.TabIndex = 25;
            this.TcpIpTB.TextChanged += new System.EventHandler(this.TcpIpTB_TextChanged);
            this.TcpIpTB.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TcpIpTB_KeyDown);
            // 
            // TcpIpLB
            // 
            this.TcpIpLB.AutoSize = true;
            this.TcpIpLB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.TcpIpLB.Location = new System.Drawing.Point(85, 83);
            this.TcpIpLB.Name = "TcpIpLB";
            this.TcpIpLB.Size = new System.Drawing.Size(29, 14);
            this.TcpIpLB.TabIndex = 24;
            this.TcpIpLB.Text = "IP :";
            // 
            // TcpLB
            // 
            this.TcpLB.AutoSize = true;
            this.TcpLB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.TcpLB.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.TcpLB.ForeColor = System.Drawing.Color.Blue;
            this.TcpLB.Location = new System.Drawing.Point(22, 237);
            this.TcpLB.Name = "TcpLB";
            this.TcpLB.Size = new System.Drawing.Size(236, 15);
            this.TcpLB.TabIndex = 23;
            this.TcpLB.Text = "※ CC Operation Management TCP Setting";
            // 
            // UdpTP
            // 
            this.UdpTP.Controls.Add(this.UdpPortTB);
            this.UdpTP.Controls.Add(this.UdpPortLB);
            this.UdpTP.Controls.Add(this.UdpIpTB);
            this.UdpTP.Controls.Add(this.UdpIpLB);
            this.UdpTP.Controls.Add(this.UdpLB);
            this.UdpTP.Location = new System.Drawing.Point(4, 29);
            this.UdpTP.Name = "UdpTP";
            this.UdpTP.Padding = new System.Windows.Forms.Padding(3);
            this.UdpTP.Size = new System.Drawing.Size(401, 276);
            this.UdpTP.TabIndex = 3;
            this.UdpTP.Text = "UDP";
            this.UdpTP.UseVisualStyleBackColor = true;
            // 
            // UdpPortTB
            // 
            this.UdpPortTB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.UdpPortTB.Location = new System.Drawing.Point(128, 111);
            this.UdpPortTB.MaxLength = 5;
            this.UdpPortTB.Name = "UdpPortTB";
            this.UdpPortTB.Size = new System.Drawing.Size(114, 22);
            this.UdpPortTB.TabIndex = 32;
            this.UdpPortTB.TextChanged += new System.EventHandler(this.UdpIpTB_TextChanged);
            this.UdpPortTB.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TcpPortTB_KeyDown);
            // 
            // UdpPortLB
            // 
            this.UdpPortLB.AutoSize = true;
            this.UdpPortLB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.UdpPortLB.Location = new System.Drawing.Point(65, 115);
            this.UdpPortLB.Name = "UdpPortLB";
            this.UdpPortLB.Size = new System.Drawing.Size(49, 14);
            this.UdpPortLB.TabIndex = 31;
            this.UdpPortLB.Text = "PORT :";
            // 
            // UdpIpTB
            // 
            this.UdpIpTB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.UdpIpTB.Location = new System.Drawing.Point(128, 79);
            this.UdpIpTB.MaxLength = 15;
            this.UdpIpTB.Name = "UdpIpTB";
            this.UdpIpTB.Size = new System.Drawing.Size(114, 22);
            this.UdpIpTB.TabIndex = 30;
            this.UdpIpTB.TextChanged += new System.EventHandler(this.UdpIpTB_TextChanged);
            this.UdpIpTB.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TcpIpTB_KeyDown);
            // 
            // UdpIpLB
            // 
            this.UdpIpLB.AutoSize = true;
            this.UdpIpLB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.UdpIpLB.Location = new System.Drawing.Point(85, 83);
            this.UdpIpLB.Name = "UdpIpLB";
            this.UdpIpLB.Size = new System.Drawing.Size(29, 14);
            this.UdpIpLB.TabIndex = 29;
            this.UdpIpLB.Text = "IP :";
            // 
            // UdpLB
            // 
            this.UdpLB.AutoSize = true;
            this.UdpLB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.UdpLB.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.UdpLB.ForeColor = System.Drawing.Color.Blue;
            this.UdpLB.Location = new System.Drawing.Point(22, 237);
            this.UdpLB.Name = "UdpLB";
            this.UdpLB.Size = new System.Drawing.Size(202, 15);
            this.UdpLB.TabIndex = 28;
            this.UdpLB.Text = "※ CC DB Management UDP Setting";
            // 
            // LogTP
            // 
            this.LogTP.Controls.Add(this.LogCB);
            this.LogTP.Controls.Add(this.LogLB);
            this.LogTP.Location = new System.Drawing.Point(4, 29);
            this.LogTP.Name = "LogTP";
            this.LogTP.Padding = new System.Windows.Forms.Padding(3);
            this.LogTP.Size = new System.Drawing.Size(401, 276);
            this.LogTP.TabIndex = 2;
            this.LogTP.Text = "LOG";
            this.LogTP.UseVisualStyleBackColor = true;
            // 
            // LogCB
            // 
            this.LogCB.AutoSize = true;
            this.LogCB.Location = new System.Drawing.Point(45, 64);
            this.LogCB.Name = "LogCB";
            this.LogCB.Size = new System.Drawing.Size(83, 18);
            this.LogCB.TabIndex = 27;
            this.LogCB.Text = "Log View";
            this.LogCB.UseVisualStyleBackColor = true;
            this.LogCB.CheckedChanged += new System.EventHandler(this.LogCB_CheckedChanged);
            // 
            // LogLB
            // 
            this.LogLB.AutoSize = true;
            this.LogLB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LogLB.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.LogLB.ForeColor = System.Drawing.Color.Blue;
            this.LogLB.Location = new System.Drawing.Point(22, 237);
            this.LogLB.Name = "LogLB";
            this.LogLB.Size = new System.Drawing.Size(320, 15);
            this.LogLB.TabIndex = 25;
            this.LogLB.Text = "※ The log will be on screen. Can slow down the program.";
            // 
            // SaveBtn
            // 
            this.SaveBtn.CrDisableEnd = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.SaveBtn.CrDisableStart = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.SaveBtn.CrEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(209)))), ((int)(((byte)(201)))));
            this.SaveBtn.CrStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.SaveBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.SaveBtn.Location = new System.Drawing.Point(335, 388);
            this.SaveBtn.Name = "SaveBtn";
            this.SaveBtn.Size = new System.Drawing.Size(86, 27);
            this.SaveBtn.TabIndex = 6;
            this.SaveBtn.Text = "Save";
            this.SaveBtn.Click += new System.EventHandler(this.SaveBtn_Click);
            // 
            // CancleBtn
            // 
            this.CancleBtn.CrDisableEnd = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.CancleBtn.CrDisableStart = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.CancleBtn.CrEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(209)))), ((int)(((byte)(201)))));
            this.CancleBtn.CrStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.CancleBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CancleBtn.Location = new System.Drawing.Point(242, 388);
            this.CancleBtn.Name = "CancleBtn";
            this.CancleBtn.Size = new System.Drawing.Size(86, 27);
            this.CancleBtn.TabIndex = 5;
            this.CancleBtn.Text = "Cancle";
            this.CancleBtn.Click += new System.EventHandler(this.CancleBtn_Click);
            // 
            // OKBtn
            // 
            this.OKBtn.CrDisableEnd = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.OKBtn.CrDisableStart = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.OKBtn.CrEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(209)))), ((int)(((byte)(201)))));
            this.OKBtn.CrStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.OKBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.OKBtn.Location = new System.Drawing.Point(150, 388);
            this.OKBtn.Name = "OKBtn";
            this.OKBtn.Size = new System.Drawing.Size(86, 27);
            this.OKBtn.TabIndex = 4;
            this.OKBtn.Text = "Ok";
            this.OKBtn.Click += new System.EventHandler(this.OKBtn_Click);
            // 
            // TopPN
            // 
            this.TopPN.BackColor = System.Drawing.Color.Transparent;
            this.TopPN.BackgroundImage = global::MewsDBManager.Properties.Resources.bgTitle;
            this.TopPN.Controls.Add(this.label1);
            this.TopPN.Controls.Add(this.MainPB);
            this.TopPN.Dock = System.Windows.Forms.DockStyle.Top;
            this.TopPN.Location = new System.Drawing.Point(0, 0);
            this.TopPN.Name = "TopPN";
            this.TopPN.Size = new System.Drawing.Size(432, 65);
            this.TopPN.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(51, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 18);
            this.label1.TabIndex = 2;
            this.label1.Text = "Option";
            // 
            // MainPB
            // 
            this.MainPB.BackColor = System.Drawing.Color.Transparent;
            this.MainPB.BackgroundImage = global::MewsDBManager.Properties.Resources.popIconSetting;
            this.MainPB.Location = new System.Drawing.Point(12, 16);
            this.MainPB.Name = "MainPB";
            this.MainPB.Size = new System.Drawing.Size(32, 32);
            this.MainPB.TabIndex = 0;
            this.MainPB.TabStop = false;
            // 
            // DBMngOption
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(432, 424);
            this.Controls.Add(this.TopPN);
            this.Controls.Add(this.SaveBtn);
            this.Controls.Add(this.CancleBtn);
            this.Controls.Add(this.OKBtn);
            this.Controls.Add(this.MainTC);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DBMngOption";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.MainTC.ResumeLayout(false);
            this.DBTP.ResumeLayout(false);
            this.DBTP.PerformLayout();
            this.TcpTP.ResumeLayout(false);
            this.TcpTP.PerformLayout();
            this.UdpTP.ResumeLayout(false);
            this.UdpTP.PerformLayout();
            this.LogTP.ResumeLayout(false);
            this.LogTP.PerformLayout();
            this.TopPN.ResumeLayout(false);
            this.TopPN.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainPB)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Adeng.Framework.Ctrl.TabControlEx MainTC;
        private System.Windows.Forms.TabPage DBTP;
        private System.Windows.Forms.TabPage TcpTP;
        private System.Windows.Forms.CheckBox Oracle8CB;
        private Adeng.Framework.Ctrl.LabelEx DbTestRstLB;
        private Adeng.Framework.Ctrl.ButtonEx DbTestBtn;
        private Adeng.Framework.Ctrl.LabelEx DbLB;
        private Adeng.Framework.Ctrl.TextBoxEx DbPwTB;
        private Adeng.Framework.Ctrl.LabelEx DbPwLB;
        private Adeng.Framework.Ctrl.TextBoxEx DbIdTB;
        private Adeng.Framework.Ctrl.LabelEx DbIdLB;
        private Adeng.Framework.Ctrl.TextBoxEx DbSidTB;
        private Adeng.Framework.Ctrl.LabelEx DbSidLB;
        private Adeng.Framework.Ctrl.TextBoxEx DbIPTB;
        private Adeng.Framework.Ctrl.LabelEx DbIPLB;
        private Adeng.Framework.Ctrl.ButtonEx TcpCloseBtn;
        private Adeng.Framework.Ctrl.ButtonEx TcpConBtn;
        private Adeng.Framework.Ctrl.TextBoxEx TcpPortTB;
        private Adeng.Framework.Ctrl.LabelEx TcpPortLB;
        private Adeng.Framework.Ctrl.TextBoxEx TcpIpTB;
        private Adeng.Framework.Ctrl.LabelEx TcpIpLB;
        private Adeng.Framework.Ctrl.LabelEx TcpLB;
        private System.Windows.Forms.TabPage LogTP;
        private System.Windows.Forms.CheckBox LogCB;
        private Adeng.Framework.Ctrl.LabelEx LogLB;
        private Adeng.Framework.Ctrl.ButtonEx SaveBtn;
        private Adeng.Framework.Ctrl.ButtonEx CancleBtn;
        private Adeng.Framework.Ctrl.ButtonEx OKBtn;
        private System.Windows.Forms.Panel TopPN;
        private System.Windows.Forms.PictureBox MainPB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label Tcp1StateLB;
        private System.Windows.Forms.TabPage UdpTP;
        private Adeng.Framework.Ctrl.TextBoxEx UdpPortTB;
        private Adeng.Framework.Ctrl.LabelEx UdpPortLB;
        private Adeng.Framework.Ctrl.TextBoxEx UdpIpTB;
        private Adeng.Framework.Ctrl.LabelEx UdpIpLB;
        private Adeng.Framework.Ctrl.LabelEx UdpLB;
    }
}