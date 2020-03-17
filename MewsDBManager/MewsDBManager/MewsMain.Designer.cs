namespace Mews.Svr.DBMng
{
    partial class MewsMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MewsMain));
            this.MenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitXToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionOToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpHToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.infomationIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.StatusStrip = new System.Windows.Forms.StatusStrip();
            this.ToolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.ToolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.ToolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.MainLB = new System.Windows.Forms.ListBox();
            this.ListBoxCMS = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.clearCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TopPN = new System.Windows.Forms.Panel();
            this.MainImgLB = new System.Windows.Forms.Label();
            this.MainPB = new System.Windows.Forms.PictureBox();
            this.NotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.NotifyIconCMS = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.CMSToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.CMSToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.CMSToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuStrip.SuspendLayout();
            this.StatusStrip.SuspendLayout();
            this.ListBoxCMS.SuspendLayout();
            this.TopPN.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainPB)).BeginInit();
            this.NotifyIconCMS.SuspendLayout();
            this.SuspendLayout();
            // 
            // MenuStrip
            // 
            this.MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileFToolStripMenuItem,
            this.viewVToolStripMenuItem,
            this.helpHToolStripMenuItem});
            this.MenuStrip.Location = new System.Drawing.Point(0, 0);
            this.MenuStrip.Name = "MenuStrip";
            this.MenuStrip.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.MenuStrip.Size = new System.Drawing.Size(869, 24);
            this.MenuStrip.TabIndex = 0;
            this.MenuStrip.Text = "menuStrip1";
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
            // viewVToolStripMenuItem
            // 
            this.viewVToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionOToolStripMenuItem});
            this.viewVToolStripMenuItem.Name = "viewVToolStripMenuItem";
            this.viewVToolStripMenuItem.Size = new System.Drawing.Size(63, 20);
            this.viewVToolStripMenuItem.Text = "View(&V)";
            // 
            // optionOToolStripMenuItem
            // 
            this.optionOToolStripMenuItem.Name = "optionOToolStripMenuItem";
            this.optionOToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.optionOToolStripMenuItem.Text = "Option(&O)";
            this.optionOToolStripMenuItem.Click += new System.EventHandler(this.optionOToolStripMenuItem_Click);
            // 
            // helpHToolStripMenuItem
            // 
            this.helpHToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.infomationIToolStripMenuItem});
            this.helpHToolStripMenuItem.Name = "helpHToolStripMenuItem";
            this.helpHToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.helpHToolStripMenuItem.Text = "Help(&H)";
            // 
            // infomationIToolStripMenuItem
            // 
            this.infomationIToolStripMenuItem.Name = "infomationIToolStripMenuItem";
            this.infomationIToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.infomationIToolStripMenuItem.Text = "Infomation(&I)";
            this.infomationIToolStripMenuItem.Click += new System.EventHandler(this.infomationIToolStripMenuItem_Click);
            // 
            // StatusStrip
            // 
            this.StatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripStatusLabel1,
            this.ToolStripStatusLabel2,
            this.ToolStripStatusLabel3});
            this.StatusStrip.Location = new System.Drawing.Point(0, 529);
            this.StatusStrip.Name = "StatusStrip";
            this.StatusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            this.StatusStrip.Size = new System.Drawing.Size(869, 29);
            this.StatusStrip.TabIndex = 1;
            this.StatusStrip.Text = "statusStrip1";
            // 
            // ToolStripStatusLabel1
            // 
            this.ToolStripStatusLabel1.AutoSize = false;
            this.ToolStripStatusLabel1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1";
            this.ToolStripStatusLabel1.Size = new System.Drawing.Size(174, 24);
            this.ToolStripStatusLabel1.Text = " Process :                    ";
            this.ToolStripStatusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ToolStripStatusLabel2
            // 
            this.ToolStripStatusLabel2.AutoSize = false;
            this.ToolStripStatusLabel2.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.ToolStripStatusLabel2.Name = "ToolStripStatusLabel2";
            this.ToolStripStatusLabel2.Size = new System.Drawing.Size(178, 24);
            this.ToolStripStatusLabel2.Text = " Stand by :                    ";
            this.ToolStripStatusLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ToolStripStatusLabel3
            // 
            this.ToolStripStatusLabel3.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.ToolStripStatusLabel3.Image = ((System.Drawing.Image)(resources.GetObject("ToolStripStatusLabel3.Image")));
            this.ToolStripStatusLabel3.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ToolStripStatusLabel3.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.ToolStripStatusLabel3.Name = "ToolStripStatusLabel3";
            this.ToolStripStatusLabel3.Size = new System.Drawing.Size(500, 24);
            this.ToolStripStatusLabel3.Spring = true;
            this.ToolStripStatusLabel3.Text = "CC Operation Management";
            this.ToolStripStatusLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ToolStripStatusLabel3.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            // 
            // MainLB
            // 
            this.MainLB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MainLB.ContextMenuStrip = this.ListBoxCMS;
            this.MainLB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainLB.Font = new System.Drawing.Font("굴림", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.MainLB.FormattingEnabled = true;
            this.MainLB.HorizontalScrollbar = true;
            this.MainLB.ItemHeight = 15;
            this.MainLB.Location = new System.Drawing.Point(0, 89);
            this.MainLB.Name = "MainLB";
            this.MainLB.Size = new System.Drawing.Size(869, 440);
            this.MainLB.TabIndex = 3;
            // 
            // ListBoxCMS
            // 
            this.ListBoxCMS.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearCToolStripMenuItem});
            this.ListBoxCMS.Name = "ListBoxCMS";
            this.ListBoxCMS.Size = new System.Drawing.Size(126, 26);
            // 
            // clearCToolStripMenuItem
            // 
            this.clearCToolStripMenuItem.Name = "clearCToolStripMenuItem";
            this.clearCToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.clearCToolStripMenuItem.Text = "Clear(&C)";
            this.clearCToolStripMenuItem.Click += new System.EventHandler(this.clearCToolStripMenuItem_Click);
            // 
            // TopPN
            // 
            this.TopPN.BackColor = System.Drawing.Color.Transparent;
            this.TopPN.BackgroundImage = global::MewsDBManager.Properties.Resources.bgTitle;
            this.TopPN.Controls.Add(this.MainImgLB);
            this.TopPN.Controls.Add(this.MainPB);
            this.TopPN.Dock = System.Windows.Forms.DockStyle.Top;
            this.TopPN.Location = new System.Drawing.Point(0, 24);
            this.TopPN.Name = "TopPN";
            this.TopPN.Size = new System.Drawing.Size(869, 65);
            this.TopPN.TabIndex = 2;
            // 
            // MainImgLB
            // 
            this.MainImgLB.Dock = System.Windows.Forms.DockStyle.Left;
            this.MainImgLB.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainImgLB.ForeColor = System.Drawing.Color.White;
            this.MainImgLB.Image = global::MewsDBManager.Properties.Resources.topDbms;
            this.MainImgLB.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.MainImgLB.Location = new System.Drawing.Point(0, 0);
            this.MainImgLB.Name = "MainImgLB";
            this.MainImgLB.Size = new System.Drawing.Size(368, 65);
            this.MainImgLB.TabIndex = 1;
            this.MainImgLB.Text = "EDWS DB Manager (Control Center)";
            this.MainImgLB.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // MainPB
            // 
            this.MainPB.BackColor = System.Drawing.Color.Transparent;
            this.MainPB.BackgroundImage = global::MewsDBManager.Properties.Resources.imgTitleLogo;
            this.MainPB.Dock = System.Windows.Forms.DockStyle.Right;
            this.MainPB.Location = new System.Drawing.Point(743, 0);
            this.MainPB.Name = "MainPB";
            this.MainPB.Size = new System.Drawing.Size(126, 65);
            this.MainPB.TabIndex = 0;
            this.MainPB.TabStop = false;
            // 
            // NotifyIcon
            // 
            this.NotifyIcon.ContextMenuStrip = this.NotifyIconCMS;
            this.NotifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("NotifyIcon.Icon")));
            this.NotifyIcon.Text = "EDWS DB Manager (Control Center)";
            this.NotifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.NotifyIcon_MouseDoubleClick);
            // 
            // NotifyIconCMS
            // 
            this.NotifyIconCMS.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CMSToolStripMenuItem1,
            this.CMSToolStripSeparator1,
            this.CMSToolStripMenuItem2});
            this.NotifyIconCMS.Name = "NotifyIconCMS";
            this.NotifyIconCMS.Size = new System.Drawing.Size(126, 54);
            // 
            // CMSToolStripMenuItem1
            // 
            this.CMSToolStripMenuItem1.Name = "CMSToolStripMenuItem1";
            this.CMSToolStripMenuItem1.Size = new System.Drawing.Size(125, 22);
            this.CMSToolStripMenuItem1.Text = "Open(&O)";
            this.CMSToolStripMenuItem1.Click += new System.EventHandler(this.CMSToolStripMenuItem1_Click);
            // 
            // CMSToolStripSeparator1
            // 
            this.CMSToolStripSeparator1.Name = "CMSToolStripSeparator1";
            this.CMSToolStripSeparator1.Size = new System.Drawing.Size(122, 6);
            // 
            // CMSToolStripMenuItem2
            // 
            this.CMSToolStripMenuItem2.Name = "CMSToolStripMenuItem2";
            this.CMSToolStripMenuItem2.Size = new System.Drawing.Size(125, 22);
            this.CMSToolStripMenuItem2.Text = "Exit(&X)";
            this.CMSToolStripMenuItem2.Click += new System.EventHandler(this.CMSToolStripMenuItem2_Click);
            // 
            // MewsMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(869, 558);
            this.Controls.Add(this.MainLB);
            this.Controls.Add(this.TopPN);
            this.Controls.Add(this.StatusStrip);
            this.Controls.Add(this.MenuStrip);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.MenuStrip;
            this.MinimumSize = new System.Drawing.Size(438, 293);
            this.Name = "MewsMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EDWS DB Manager (Control Center)";
            this.MenuStrip.ResumeLayout(false);
            this.MenuStrip.PerformLayout();
            this.StatusStrip.ResumeLayout(false);
            this.StatusStrip.PerformLayout();
            this.ListBoxCMS.ResumeLayout(false);
            this.TopPN.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MainPB)).EndInit();
            this.NotifyIconCMS.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip MenuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileFToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitXToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewVToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionOToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpHToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem infomationIToolStripMenuItem;
        private System.Windows.Forms.StatusStrip StatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel ToolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel ToolStripStatusLabel2;
        private System.Windows.Forms.ListBox MainLB;
        private System.Windows.Forms.Panel TopPN;
        private System.Windows.Forms.NotifyIcon NotifyIcon;
        private System.Windows.Forms.ContextMenuStrip NotifyIconCMS;
        private System.Windows.Forms.ToolStripSeparator CMSToolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem CMSToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem CMSToolStripMenuItem2;
        private System.Windows.Forms.ContextMenuStrip ListBoxCMS;
        private System.Windows.Forms.ToolStripMenuItem clearCToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel ToolStripStatusLabel3;
        private System.Windows.Forms.PictureBox MainPB;
        private System.Windows.Forms.Label MainImgLB;
    }
}

