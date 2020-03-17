namespace Mews.Svr.Broad
{
    partial class BoardEditForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BoardEditForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cutImageLV = new System.Windows.Forms.ListView();
            this.cutImageList = new System.Windows.Forms.ImageList(this.components);
            this.label7 = new System.Windows.Forms.Label();
            this.cutAddBtn = new System.Windows.Forms.Button();
            this.tbText = new System.Windows.Forms.TextBox();
            this.menuColor = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.foreColorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuForeRed = new System.Windows.Forms.ToolStripMenuItem();
            this.menuForeGreen = new System.Windows.Forms.ToolStripMenuItem();
            this.menuForeBlack = new System.Windows.Forms.ToolStripMenuItem();
            this.menuForeYellow = new System.Windows.Forms.ToolStripMenuItem();
            this.wHITEToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cbKind = new System.Windows.Forms.ComboBox();
            this.tbName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnView = new System.Windows.Forms.Button();
            this.lbByte = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbBlack = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            this.menuColor.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(590, 65);
            this.panel1.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(50, 23);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 18);
            this.label6.TabIndex = 3;
            this.label6.Text = "CG Edit";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::MewsBroad.Properties.Resources.popIconCg;
            this.pictureBox1.Location = new System.Drawing.Point(12, 16);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(32, 32);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.cutImageLV);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.cutAddBtn);
            this.panel2.Controls.Add(this.tbText);
            this.panel2.Controls.Add(this.cbKind);
            this.panel2.Controls.Add(this.tbName);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.btnView);
            this.panel2.Controls.Add(this.lbByte);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.btnSave);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Location = new System.Drawing.Point(1, 67);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(589, 461);
            this.panel2.TabIndex = 6;
            // 
            // cutImageLV
            // 
            this.cutImageLV.LargeImageList = this.cutImageList;
            this.cutImageLV.Location = new System.Drawing.Point(112, 261);
            this.cutImageLV.Name = "cutImageLV";
            this.cutImageLV.Size = new System.Drawing.Size(454, 132);
            this.cutImageLV.TabIndex = 290;
            this.cutImageLV.UseCompatibleStateImageBehavior = false;
            // 
            // cutImageList
            // 
            this.cutImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.cutImageList.ImageSize = new System.Drawing.Size(100, 70);
            this.cutImageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label7.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(16, 260);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(94, 135);
            this.label7.TabIndex = 289;
            this.label7.Text = "Cut";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cutAddBtn
            // 
            this.cutAddBtn.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cutAddBtn.Location = new System.Drawing.Point(340, 411);
            this.cutAddBtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cutAddBtn.Name = "cutAddBtn";
            this.cutAddBtn.Size = new System.Drawing.Size(110, 37);
            this.cutAddBtn.TabIndex = 288;
            this.cutAddBtn.Text = "Cut Add";
            this.cutAddBtn.UseVisualStyleBackColor = true;
            this.cutAddBtn.Click += new System.EventHandler(this.cutAddBtn_Click);
            // 
            // tbText
            // 
            this.tbText.ContextMenuStrip = this.menuColor;
            this.tbText.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbText.HideSelection = false;
            this.tbText.Location = new System.Drawing.Point(112, 82);
            this.tbText.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbText.MaxLength = 100000;
            this.tbText.Multiline = true;
            this.tbText.Name = "tbText";
            this.tbText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbText.Size = new System.Drawing.Size(454, 177);
            this.tbText.TabIndex = 287;
            this.tbText.TextChanged += new System.EventHandler(this.tbText_TextChanged);
            this.tbText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbText_KeyDown);
            this.tbText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbText_KeyPress);
            this.tbText.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbText_KeyUp);
            // 
            // menuColor
            // 
            this.menuColor.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.foreColorToolStripMenuItem});
            this.menuColor.Name = "menuColor";
            this.menuColor.Size = new System.Drawing.Size(153, 48);
            // 
            // foreColorToolStripMenuItem
            // 
            this.foreColorToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuForeRed,
            this.menuForeGreen,
            this.menuForeBlack,
            this.menuForeYellow,
            this.wHITEToolStripMenuItem});
            this.foreColorToolStripMenuItem.Name = "foreColorToolStripMenuItem";
            this.foreColorToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.foreColorToolStripMenuItem.Text = "ForeColor";
            // 
            // menuForeRed
            // 
            this.menuForeRed.Name = "menuForeRed";
            this.menuForeRed.Size = new System.Drawing.Size(125, 22);
            this.menuForeRed.Text = "RED";
            this.menuForeRed.Click += new System.EventHandler(this.menuForeColor_Click);
            // 
            // menuForeGreen
            // 
            this.menuForeGreen.Name = "menuForeGreen";
            this.menuForeGreen.Size = new System.Drawing.Size(125, 22);
            this.menuForeGreen.Text = "GREEN";
            this.menuForeGreen.Click += new System.EventHandler(this.menuForeColor_Click);
            // 
            // menuForeBlack
            // 
            this.menuForeBlack.Name = "menuForeBlack";
            this.menuForeBlack.Size = new System.Drawing.Size(125, 22);
            this.menuForeBlack.Text = "BLUE";
            this.menuForeBlack.Click += new System.EventHandler(this.menuForeColor_Click);
            // 
            // menuForeYellow
            // 
            this.menuForeYellow.Name = "menuForeYellow";
            this.menuForeYellow.Size = new System.Drawing.Size(125, 22);
            this.menuForeYellow.Text = "YELLOW";
            this.menuForeYellow.Click += new System.EventHandler(this.menuForeColor_Click);
            // 
            // wHITEToolStripMenuItem
            // 
            this.wHITEToolStripMenuItem.Name = "wHITEToolStripMenuItem";
            this.wHITEToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.wHITEToolStripMenuItem.Text = "WHITE";
            this.wHITEToolStripMenuItem.Click += new System.EventHandler(this.menuForeColor_Click);
            // 
            // cbKind
            // 
            this.cbKind.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbKind.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbKind.ForeColor = System.Drawing.Color.Blue;
            this.cbKind.FormattingEnabled = true;
            this.cbKind.Location = new System.Drawing.Point(112, 19);
            this.cbKind.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbKind.Name = "cbKind";
            this.cbKind.Size = new System.Drawing.Size(454, 26);
            this.cbKind.TabIndex = 286;
            // 
            // tbName
            // 
            this.tbName.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbName.ForeColor = System.Drawing.Color.Blue;
            this.tbName.ImeMode = System.Windows.Forms.ImeMode.Hangul;
            this.tbName.Location = new System.Drawing.Point(112, 50);
            this.tbName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(454, 26);
            this.tbName.TabIndex = 285;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label5.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(16, 49);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(94, 31);
            this.label5.TabIndex = 284;
            this.label5.Text = "Name";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnView
            // 
            this.btnView.BackColor = System.Drawing.Color.LightSteelBlue;
            this.btnView.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnView.Location = new System.Drawing.Point(224, 411);
            this.btnView.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(110, 37);
            this.btnView.TabIndex = 283;
            this.btnView.Text = "Text Preview";
            this.btnView.UseVisualStyleBackColor = false;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // lbByte
            // 
            this.lbByte.AutoSize = true;
            this.lbByte.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbByte.ForeColor = System.Drawing.Color.Blue;
            this.lbByte.Location = new System.Drawing.Point(129, 418);
            this.lbByte.Name = "lbByte";
            this.lbByte.Size = new System.Drawing.Size(61, 16);
            this.lbByte.TabIndex = 282;
            this.lbByte.Text = "0 / 900";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Blue;
            this.label4.Location = new System.Drawing.Point(23, 418);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 16);
            this.label4.TabIndex = 281;
            this.label4.Text = "Byte count : ";
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(456, 411);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(110, 37);
            this.btnSave.TabIndex = 280;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(16, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 178);
            this.label2.TabIndex = 264;
            this.label2.Text = "Text";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(16, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 31);
            this.label1.TabIndex = 261;
            this.label1.Text = "Kind";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbBlack
            // 
            this.cbBlack.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBlack.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbBlack.ForeColor = System.Drawing.Color.Blue;
            this.cbBlack.FormattingEnabled = true;
            this.cbBlack.Location = new System.Drawing.Point(102, 557);
            this.cbBlack.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbBlack.Name = "cbBlack";
            this.cbBlack.Size = new System.Drawing.Size(454, 26);
            this.cbBlack.TabIndex = 279;
            this.cbBlack.Visible = false;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label3.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(6, 555);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 31);
            this.label3.TabIndex = 265;
            this.label3.Text = "Black";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label3.Visible = false;
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(468, 550);
            this.btnClose.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(110, 41);
            this.btnClose.TabIndex = 252;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // BoardEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(590, 603);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.cbBlack);
            this.Controls.Add(this.label3);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BoardEditForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.menuColor.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbBlack;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ContextMenuStrip menuColor;
        private System.Windows.Forms.ToolStripMenuItem foreColorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuForeRed;
        private System.Windows.Forms.ToolStripMenuItem menuForeGreen;
        private System.Windows.Forms.ToolStripMenuItem menuForeYellow;
        private System.Windows.Forms.ToolStripMenuItem menuForeBlack;
        private System.Windows.Forms.Label lbByte;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnView;
        private System.Windows.Forms.ComboBox cbKind;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbText;
        private System.Windows.Forms.ToolStripMenuItem wHITEToolStripMenuItem;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button cutAddBtn;
        private System.Windows.Forms.ImageList cutImageList;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ListView cutImageLV;
    }
}