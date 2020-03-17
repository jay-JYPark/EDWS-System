namespace Mews.Svr.Broad
{
    partial class BroadMain
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다.
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BroadMain));
            this.MainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitXToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editEToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mapIconToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.normalNToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modifyDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupGToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.storedMessageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cGCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autoAlertAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.languageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.englishEToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mongolianMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionOToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpHToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.infomationIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainStatusStrip = new System.Windows.Forms.StatusStrip();
            this.ToolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.ToolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.ToolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.MainTopPN = new System.Windows.Forms.Panel();
            this.primaryPB = new System.Windows.Forms.PictureBox();
            this.MainTopLB2 = new System.Windows.Forms.Label();
            this.MainTopLB1 = new System.Windows.Forms.Label();
            this.lbOnAir = new System.Windows.Forms.Label();
            this.MainTopLB = new System.Windows.Forms.Label();
            this.MainPB = new System.Windows.Forms.PictureBox();
            this.MapPanel = new Mews.Ctrl.Mewsmap.MewsPanel();
            this.MainMap = new Mews.Ctrl.Mewsmap.MewsMap();
            this.MapContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.normalModeNToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modifyModeDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mewsSyncFailUC = new Mews.Ctrl.MewsSettingSync.MewsSyncFailUC();
            this.mewsSettingSyncUC = new Mews.Ctrl.MewsSettingSync.MewsSettingSyncUC();
            this.orderSelLiveSiren = new Mews.Svr.Broad.OrderSelLiveSiren();
            this.orderSelLive1 = new Mews.Svr.Broad.OrderSelLive();
            this.orderProgress = new Mews.Svr.Broad.OrderProgress();
            this.orderSelSiren = new Mews.Svr.Broad.OrderSelSiren();
            this.orderSelMsg = new Mews.Svr.Broad.OrderSelMsg();
            this.orderSelGrp = new Mews.Svr.Broad.OrderSelGrp();
            this.MainCtrPN = new System.Windows.Forms.Panel();
            this.orderMode2 = new Mews.Svr.Broad.OrderMode2();
            this.MainMenuStrip.SuspendLayout();
            this.MainStatusStrip.SuspendLayout();
            this.MainTopPN.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.primaryPB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MainPB)).BeginInit();
            this.MapPanel.SuspendLayout();
            this.MainMap.SuspendLayout();
            this.MapContextMenuStrip.SuspendLayout();
            this.MainCtrPN.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainMenuStrip
            // 
            this.MainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileFToolStripMenuItem,
            this.editEToolStripMenuItem,
            this.viewVToolStripMenuItem,
            this.helpHToolStripMenuItem});
            this.MainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.MainMenuStrip.Name = "MainMenuStrip";
            this.MainMenuStrip.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.MainMenuStrip.Size = new System.Drawing.Size(1272, 24);
            this.MainMenuStrip.TabIndex = 0;
            this.MainMenuStrip.Text = "menuStrip1";
            // 
            // fileFToolStripMenuItem
            // 
            this.fileFToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitXToolStripMenuItem});
            this.fileFToolStripMenuItem.Name = "fileFToolStripMenuItem";
            this.fileFToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.fileFToolStripMenuItem.Text = "File(&F)";
            // 
            // exitXToolStripMenuItem
            // 
            this.exitXToolStripMenuItem.Name = "exitXToolStripMenuItem";
            this.exitXToolStripMenuItem.Size = new System.Drawing.Size(115, 22);
            this.exitXToolStripMenuItem.Text = "Exit(&X)";
            this.exitXToolStripMenuItem.Click += new System.EventHandler(this.exitXToolStripMenuItem_Click);
            // 
            // editEToolStripMenuItem
            // 
            this.editEToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mapIconToolStripMenuItem,
            this.groupGToolStripMenuItem,
            this.storedMessageToolStripMenuItem,
            this.cGCToolStripMenuItem});
            this.editEToolStripMenuItem.Name = "editEToolStripMenuItem";
            this.editEToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.editEToolStripMenuItem.Text = "Edit(&E)";
            // 
            // mapIconToolStripMenuItem
            // 
            this.mapIconToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.normalNToolStripMenuItem,
            this.modifyDToolStripMenuItem});
            this.mapIconToolStripMenuItem.Name = "mapIconToolStripMenuItem";
            this.mapIconToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.mapIconToolStripMenuItem.Text = "Map Icon(&M)";
            // 
            // normalNToolStripMenuItem
            // 
            this.normalNToolStripMenuItem.Name = "normalNToolStripMenuItem";
            this.normalNToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.normalNToolStripMenuItem.Text = "Operation Mode(&O)";
            this.normalNToolStripMenuItem.Click += new System.EventHandler(this.normalNToolStripMenuItem_Click);
            // 
            // modifyDToolStripMenuItem
            // 
            this.modifyDToolStripMenuItem.Name = "modifyDToolStripMenuItem";
            this.modifyDToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.modifyDToolStripMenuItem.Text = "Edit Mode(&E)";
            this.modifyDToolStripMenuItem.Click += new System.EventHandler(this.modifyDToolStripMenuItem_Click);
            // 
            // groupGToolStripMenuItem
            // 
            this.groupGToolStripMenuItem.Name = "groupGToolStripMenuItem";
            this.groupGToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.groupGToolStripMenuItem.Text = "Grouping(&G)";
            this.groupGToolStripMenuItem.Click += new System.EventHandler(this.groupGToolStripMenuItem_Click);
            // 
            // storedMessageToolStripMenuItem
            // 
            this.storedMessageToolStripMenuItem.Name = "storedMessageToolStripMenuItem";
            this.storedMessageToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.storedMessageToolStripMenuItem.Text = "Stored Message(&S)";
            this.storedMessageToolStripMenuItem.Click += new System.EventHandler(this.storedMessageToolStripMenuItem_Click);
            // 
            // cGCToolStripMenuItem
            // 
            this.cGCToolStripMenuItem.Name = "cGCToolStripMenuItem";
            this.cGCToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.cGCToolStripMenuItem.Text = "CG(&C)";
            this.cGCToolStripMenuItem.Click += new System.EventHandler(this.cGCToolStripMenuItem_Click);
            // 
            // viewVToolStripMenuItem
            // 
            this.viewVToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.autoAlertAToolStripMenuItem,
            this.toolStripSeparator1,
            this.languageToolStripMenuItem,
            this.optionOToolStripMenuItem});
            this.viewVToolStripMenuItem.Name = "viewVToolStripMenuItem";
            this.viewVToolStripMenuItem.Size = new System.Drawing.Size(73, 20);
            this.viewVToolStripMenuItem.Text = "Setting(&S)";
            // 
            // autoAlertAToolStripMenuItem
            // 
            this.autoAlertAToolStripMenuItem.Name = "autoAlertAToolStripMenuItem";
            this.autoAlertAToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.autoAlertAToolStripMenuItem.Text = "Auto Alert(&A)";
            this.autoAlertAToolStripMenuItem.Click += new System.EventHandler(this.autoAlertAToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(146, 6);
            // 
            // languageToolStripMenuItem
            // 
            this.languageToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.englishEToolStripMenuItem,
            this.mongolianMToolStripMenuItem});
            this.languageToolStripMenuItem.Name = "languageToolStripMenuItem";
            this.languageToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.languageToolStripMenuItem.Text = "Language(&L)";
            // 
            // englishEToolStripMenuItem
            // 
            this.englishEToolStripMenuItem.Name = "englishEToolStripMenuItem";
            this.englishEToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.englishEToolStripMenuItem.Text = "English(&E)";
            this.englishEToolStripMenuItem.Click += new System.EventHandler(this.englishEToolStripMenuItem_Click);
            // 
            // mongolianMToolStripMenuItem
            // 
            this.mongolianMToolStripMenuItem.Name = "mongolianMToolStripMenuItem";
            this.mongolianMToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.mongolianMToolStripMenuItem.Text = "Mongolian(&M)";
            this.mongolianMToolStripMenuItem.Click += new System.EventHandler(this.mongolianMToolStripMenuItem_Click);
            // 
            // optionOToolStripMenuItem
            // 
            this.optionOToolStripMenuItem.Name = "optionOToolStripMenuItem";
            this.optionOToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.optionOToolStripMenuItem.Text = "Option(&O)";
            this.optionOToolStripMenuItem.Click += new System.EventHandler(this.optionOToolStripMenuItem_Click);
            // 
            // helpHToolStripMenuItem
            // 
            this.helpHToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.infomationIToolStripMenuItem});
            this.helpHToolStripMenuItem.Name = "helpHToolStripMenuItem";
            this.helpHToolStripMenuItem.Size = new System.Drawing.Size(67, 20);
            this.helpHToolStripMenuItem.Text = "About(&A)";
            // 
            // infomationIToolStripMenuItem
            // 
            this.infomationIToolStripMenuItem.Name = "infomationIToolStripMenuItem";
            this.infomationIToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.infomationIToolStripMenuItem.Text = "Program Infomation(I)";
            this.infomationIToolStripMenuItem.Click += new System.EventHandler(this.infomationIToolStripMenuItem_Click);
            // 
            // MainStatusStrip
            // 
            this.MainStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripStatusLabel3,
            this.ToolStripStatusLabel1,
            this.ToolStripStatusLabel2});
            this.MainStatusStrip.Location = new System.Drawing.Point(0, 961);
            this.MainStatusStrip.Name = "MainStatusStrip";
            this.MainStatusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            this.MainStatusStrip.Size = new System.Drawing.Size(1272, 29);
            this.MainStatusStrip.TabIndex = 1;
            // 
            // ToolStripStatusLabel3
            // 
            this.ToolStripStatusLabel3.Name = "ToolStripStatusLabel3";
            this.ToolStripStatusLabel3.Size = new System.Drawing.Size(0, 24);
            // 
            // ToolStripStatusLabel1
            // 
            this.ToolStripStatusLabel1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.ToolStripStatusLabel1.Image = ((System.Drawing.Image)(resources.GetObject("ToolStripStatusLabel1.Image")));
            this.ToolStripStatusLabel1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ToolStripStatusLabel1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1";
            this.ToolStripStatusLabel1.Size = new System.Drawing.Size(1137, 24);
            this.ToolStripStatusLabel1.Spring = true;
            this.ToolStripStatusLabel1.Text = "Operation Management      ";
            this.ToolStripStatusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ToolStripStatusLabel2
            // 
            this.ToolStripStatusLabel2.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.ToolStripStatusLabel2.Image = ((System.Drawing.Image)(resources.GetObject("ToolStripStatusLabel2.Image")));
            this.ToolStripStatusLabel2.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ToolStripStatusLabel2.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.ToolStripStatusLabel2.Name = "ToolStripStatusLabel2";
            this.ToolStripStatusLabel2.Size = new System.Drawing.Size(118, 24);
            this.ToolStripStatusLabel2.Text = "Control Panel";
            this.ToolStripStatusLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // MainTopPN
            // 
            this.MainTopPN.BackgroundImage = global::MewsBroad.Properties.Resources.bgTitle;
            this.MainTopPN.Controls.Add(this.lbOnAir);
            this.MainTopPN.Controls.Add(this.primaryPB);
            this.MainTopPN.Controls.Add(this.MainTopLB2);
            this.MainTopPN.Controls.Add(this.MainTopLB1);
            this.MainTopPN.Controls.Add(this.MainTopLB);
            this.MainTopPN.Controls.Add(this.MainPB);
            this.MainTopPN.Dock = System.Windows.Forms.DockStyle.Top;
            this.MainTopPN.Location = new System.Drawing.Point(0, 24);
            this.MainTopPN.Name = "MainTopPN";
            this.MainTopPN.Size = new System.Drawing.Size(1272, 65);
            this.MainTopPN.TabIndex = 2;
            // 
            // primaryPB
            // 
            this.primaryPB.BackColor = System.Drawing.Color.Transparent;
            this.primaryPB.BackgroundImage = global::MewsBroad.Properties.Resources.ucPrimary;
            this.primaryPB.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.primaryPB.Location = new System.Drawing.Point(1045, 10);
            this.primaryPB.Name = "primaryPB";
            this.primaryPB.Size = new System.Drawing.Size(135, 45);
            this.primaryPB.TabIndex = 8;
            this.primaryPB.TabStop = false;
            // 
            // MainTopLB2
            // 
            this.MainTopLB2.AutoSize = true;
            this.MainTopLB2.BackColor = System.Drawing.Color.Transparent;
            this.MainTopLB2.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainTopLB2.ForeColor = System.Drawing.Color.Red;
            this.MainTopLB2.Location = new System.Drawing.Point(300, 8);
            this.MainTopLB2.Name = "MainTopLB2";
            this.MainTopLB2.Size = new System.Drawing.Size(178, 23);
            this.MainTopLB2.TabIndex = 7;
            this.MainTopLB2.Text = "[Primary Mode]";
            this.MainTopLB2.Visible = false;
            // 
            // MainTopLB1
            // 
            this.MainTopLB1.AutoSize = true;
            this.MainTopLB1.BackColor = System.Drawing.Color.Transparent;
            this.MainTopLB1.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainTopLB1.ForeColor = System.Drawing.Color.Yellow;
            this.MainTopLB1.Location = new System.Drawing.Point(37, 12);
            this.MainTopLB1.Name = "MainTopLB1";
            this.MainTopLB1.Size = new System.Drawing.Size(137, 18);
            this.MainTopLB1.TabIndex = 5;
            this.MainTopLB1.Text = "Control Center";
            // 
            // lbOnAir
            // 
            this.lbOnAir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbOnAir.BackColor = System.Drawing.Color.CornflowerBlue;
            this.lbOnAir.Font = new System.Drawing.Font("Verdana", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbOnAir.ForeColor = System.Drawing.Color.DarkSlateBlue;
            this.lbOnAir.Location = new System.Drawing.Point(717, 14);
            this.lbOnAir.Name = "lbOnAir";
            this.lbOnAir.Size = new System.Drawing.Size(463, 36);
            this.lbOnAir.TabIndex = 4;
            this.lbOnAir.Text = "ON-AIR";
            this.lbOnAir.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbOnAir.Visible = false;
            // 
            // MainTopLB
            // 
            this.MainTopLB.BackColor = System.Drawing.Color.Transparent;
            this.MainTopLB.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainTopLB.ForeColor = System.Drawing.Color.White;
            this.MainTopLB.Image = global::MewsBroad.Properties.Resources.topBroadcasting;
            this.MainTopLB.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.MainTopLB.Location = new System.Drawing.Point(0, 12);
            this.MainTopLB.Name = "MainTopLB";
            this.MainTopLB.Size = new System.Drawing.Size(423, 37);
            this.MainTopLB.TabIndex = 1;
            this.MainTopLB.Text = "EDWS Broadcasting Voice Dispatch";
            this.MainTopLB.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // MainPB
            // 
            this.MainPB.BackColor = System.Drawing.Color.Transparent;
            this.MainPB.BackgroundImage = global::MewsBroad.Properties.Resources.imgTitleLogoVer2;
            this.MainPB.Dock = System.Windows.Forms.DockStyle.Right;
            this.MainPB.Location = new System.Drawing.Point(1212, 0);
            this.MainPB.Name = "MainPB";
            this.MainPB.Size = new System.Drawing.Size(60, 65);
            this.MainPB.TabIndex = 0;
            this.MainPB.TabStop = false;
            // 
            // MapPanel
            // 
            this.MapPanel.BackColor = System.Drawing.Color.Transparent;
            this.MapPanel.Controls.Add(this.MainMap);
            this.MapPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MapPanel.Location = new System.Drawing.Point(320, 89);
            this.MapPanel.Name = "MapPanel";
            this.MapPanel.Size = new System.Drawing.Size(952, 872);
            this.MapPanel.TabIndex = 3;
            // 
            // MainMap
            // 
            this.MainMap.ContextMenuStrip = this.MapContextMenuStrip;
            this.MainMap.Controls.Add(this.mewsSyncFailUC);
            this.MainMap.Controls.Add(this.mewsSettingSyncUC);
            this.MainMap.Controls.Add(this.orderSelLiveSiren);
            this.MainMap.Controls.Add(this.orderSelLive1);
            this.MainMap.Controls.Add(this.orderProgress);
            this.MainMap.Controls.Add(this.orderSelSiren);
            this.MainMap.Controls.Add(this.orderSelMsg);
            this.MainMap.Controls.Add(this.orderSelGrp);
            this.MainMap.CurItem = null;
            this.MainMap.FHeight = 0F;
            this.MainMap.FWidth = 0F;
            this.MainMap.IsLangEng = false;
            this.MainMap.Location = new System.Drawing.Point(3, 3);
            this.MainMap.MapMode = Mews.Ctrl.Mewsmap.enMapMode.RunningMode;
            this.MainMap.Name = "MainMap";
            this.MainMap.Size = new System.Drawing.Size(952, 872);
            this.MainMap.TabIndex = 0;
            this.MainMap.UserKind = Mews.Ctrl.Mewsmap.enMapUser.userAlarmControl;
            // 
            // MapContextMenuStrip
            // 
            this.MapContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.normalModeNToolStripMenuItem,
            this.modifyModeDToolStripMenuItem});
            this.MapContextMenuStrip.Name = "MapContextMenuStrip";
            this.MapContextMenuStrip.Size = new System.Drawing.Size(185, 48);
            // 
            // normalModeNToolStripMenuItem
            // 
            this.normalModeNToolStripMenuItem.Name = "normalModeNToolStripMenuItem";
            this.normalModeNToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.normalModeNToolStripMenuItem.Text = "Operation Mode(&P)";
            this.normalModeNToolStripMenuItem.Click += new System.EventHandler(this.normalModeNToolStripMenuItem_Click);
            // 
            // modifyModeDToolStripMenuItem
            // 
            this.modifyModeDToolStripMenuItem.Name = "modifyModeDToolStripMenuItem";
            this.modifyModeDToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.modifyModeDToolStripMenuItem.Text = "Edit Mode(&D)";
            this.modifyModeDToolStripMenuItem.Click += new System.EventHandler(this.modifyModeDToolStripMenuItem_Click);
            // 
            // mewsSyncFailUC
            // 
            this.mewsSyncFailUC.BackColor = System.Drawing.Color.White;
            this.mewsSyncFailUC.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mewsSyncFailUC.Location = new System.Drawing.Point(530, 16);
            this.mewsSyncFailUC.Name = "mewsSyncFailUC";
            this.mewsSyncFailUC.Size = new System.Drawing.Size(410, 40);
            this.mewsSyncFailUC.TabIndex = 7;
            this.mewsSyncFailUC.Visible = false;
            // 
            // mewsSettingSyncUC
            // 
            this.mewsSettingSyncUC.BackColor = System.Drawing.Color.White;
            this.mewsSettingSyncUC.DiscountTime = ((short)(300));
            this.mewsSettingSyncUC.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mewsSettingSyncUC.Location = new System.Drawing.Point(251, 278);
            this.mewsSettingSyncUC.Name = "mewsSettingSyncUC";
            this.mewsSettingSyncUC.ReceiveTimeCC = ((short)(300));
            this.mewsSettingSyncUC.ReceiveTimeMCC = ((short)(300));
            this.mewsSettingSyncUC.Size = new System.Drawing.Size(450, 320);
            this.mewsSettingSyncUC.TabIndex = 6;
            this.mewsSettingSyncUC.Visible = false;
            // 
            // orderSelLiveSiren
            // 
            this.orderSelLiveSiren.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("orderSelLiveSiren.BackgroundImage")));
            this.orderSelLiveSiren.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.orderSelLiveSiren.Location = new System.Drawing.Point(288, 656);
            this.orderSelLiveSiren.Name = "orderSelLiveSiren";
            this.orderSelLiveSiren.Size = new System.Drawing.Size(183, 189);
            this.orderSelLiveSiren.TabIndex = 5;
            // 
            // orderSelLive1
            // 
            this.orderSelLive1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("orderSelLive1.BackgroundImage")));
            this.orderSelLive1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.orderSelLive1.Location = new System.Drawing.Point(477, 656);
            this.orderSelLive1.Name = "orderSelLive1";
            this.orderSelLive1.Size = new System.Drawing.Size(183, 189);
            this.orderSelLive1.TabIndex = 4;
            // 
            // orderProgress
            // 
            this.orderProgress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.orderProgress.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("orderProgress.BackgroundImage")));
            this.orderProgress.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.orderProgress.Location = new System.Drawing.Point(3, 774);
            this.orderProgress.Name = "orderProgress";
            this.orderProgress.Size = new System.Drawing.Size(468, 99);
            this.orderProgress.TabIndex = 3;
            // 
            // orderSelSiren
            // 
            this.orderSelSiren.BackColor = System.Drawing.Color.Transparent;
            this.orderSelSiren.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("orderSelSiren.BackgroundImage")));
            this.orderSelSiren.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.orderSelSiren.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.orderSelSiren.Location = new System.Drawing.Point(655, 16);
            this.orderSelSiren.Name = "orderSelSiren";
            this.orderSelSiren.Size = new System.Drawing.Size(238, 791);
            this.orderSelSiren.TabIndex = 2;
            // 
            // orderSelMsg
            // 
            this.orderSelMsg.BackColor = System.Drawing.Color.Transparent;
            this.orderSelMsg.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("orderSelMsg.BackgroundImage")));
            this.orderSelMsg.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.orderSelMsg.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.orderSelMsg.Location = new System.Drawing.Point(336, 16);
            this.orderSelMsg.Name = "orderSelMsg";
            this.orderSelMsg.Size = new System.Drawing.Size(313, 634);
            this.orderSelMsg.TabIndex = 1;
            // 
            // orderSelGrp
            // 
            this.orderSelGrp.BackColor = System.Drawing.Color.Transparent;
            this.orderSelGrp.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("orderSelGrp.BackgroundImage")));
            this.orderSelGrp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.orderSelGrp.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.orderSelGrp.Location = new System.Drawing.Point(17, 16);
            this.orderSelGrp.Name = "orderSelGrp";
            this.orderSelGrp.Size = new System.Drawing.Size(313, 634);
            this.orderSelGrp.TabIndex = 0;
            // 
            // MainCtrPN
            // 
            this.MainCtrPN.BackColor = System.Drawing.Color.Transparent;
            this.MainCtrPN.Controls.Add(this.orderMode2);
            this.MainCtrPN.Dock = System.Windows.Forms.DockStyle.Left;
            this.MainCtrPN.Location = new System.Drawing.Point(0, 89);
            this.MainCtrPN.Name = "MainCtrPN";
            this.MainCtrPN.Size = new System.Drawing.Size(320, 872);
            this.MainCtrPN.TabIndex = 4;
            // 
            // orderMode2
            // 
            this.orderMode2.BackColor = System.Drawing.Color.White;
            this.orderMode2.BoardText = "";
            this.orderMode2.BTVAudio = false;
            this.orderMode2.CurBoardInfo = null;
            this.orderMode2.CurDest = ((byte)(0));
            this.orderMode2.CurDistLstIP = null;
            this.orderMode2.CurGrp = null;
            this.orderMode2.CurKind = ((byte)(0));
            this.orderMode2.CurMedia = ((byte)(0));
            this.orderMode2.CurMode = ((byte)(0));
            this.orderMode2.CurMsgNum = ((byte)(0));
            this.orderMode2.CurReptCnt = ((byte)(0));
            this.orderMode2.CurTermLstIP = null;
            this.orderMode2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.orderMode2.Location = new System.Drawing.Point(0, 0);
            this.orderMode2.Name = "orderMode2";
            this.orderMode2.Size = new System.Drawing.Size(320, 876);
            this.orderMode2.TabIndex = 0;
            // 
            // BroadMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1272, 990);
            this.Controls.Add(this.MapPanel);
            this.Controls.Add(this.MainCtrPN);
            this.Controls.Add(this.MainTopPN);
            this.Controls.Add(this.MainStatusStrip);
            this.Controls.Add(this.MainMenuStrip);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(877, 592);
            this.Name = "BroadMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EDWS Broadcasting Voice Dispatch";
            this.MainMenuStrip.ResumeLayout(false);
            this.MainMenuStrip.PerformLayout();
            this.MainStatusStrip.ResumeLayout(false);
            this.MainStatusStrip.PerformLayout();
            this.MainTopPN.ResumeLayout(false);
            this.MainTopPN.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.primaryPB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MainPB)).EndInit();
            this.MapPanel.ResumeLayout(false);
            this.MainMap.ResumeLayout(false);
            this.MapContextMenuStrip.ResumeLayout(false);
            this.MainCtrPN.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip MainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileFToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitXToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewVToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionOToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpHToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem infomationIToolStripMenuItem;
        private System.Windows.Forms.StatusStrip MainStatusStrip;
        private System.Windows.Forms.Panel MainTopPN;
        private System.Windows.Forms.PictureBox MainPB;
        private System.Windows.Forms.Label MainTopLB;
        private System.Windows.Forms.ToolStripStatusLabel ToolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel ToolStripStatusLabel2;
        private Ctrl.Mewsmap.MewsPanel MapPanel;
        private Ctrl.Mewsmap.MewsMap MainMap;
        private System.Windows.Forms.ToolStripMenuItem editEToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mapIconToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem normalNToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem modifyDToolStripMenuItem;
        private System.Windows.Forms.Panel MainCtrPN;
        private System.Windows.Forms.ToolStripMenuItem groupGToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem storedMessageToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip MapContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem normalModeNToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem modifyModeDToolStripMenuItem;
        private OrderProgress orderProgress;
        private OrderSelSiren orderSelSiren;
        private OrderSelMsg orderSelMsg;
        private OrderSelGrp orderSelGrp;
        private OrderMode2 orderMode2;
        private System.Windows.Forms.Label lbOnAir;
        private System.Windows.Forms.ToolStripMenuItem cGCToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel ToolStripStatusLabel3;
        private OrderSelLive orderSelLive1;
        private OrderSelLiveSiren orderSelLiveSiren;
        private System.Windows.Forms.ToolStripMenuItem autoAlertAToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem languageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem englishEToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mongolianMToolStripMenuItem;
        private System.Windows.Forms.Label MainTopLB2;
        private System.Windows.Forms.Label MainTopLB1;
        private Ctrl.MewsSettingSync.MewsSyncFailUC mewsSyncFailUC;
        private Ctrl.MewsSettingSync.MewsSettingSyncUC mewsSettingSyncUC;
        private System.Windows.Forms.PictureBox primaryPB;
    }
}

