namespace Mews.Svr.Broad
{
    partial class BoardMngForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BoardMngForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnKindDel = new System.Windows.Forms.Button();
            this.btnKindEdit = new System.Windows.Forms.Button();
            this.btnKindAdd = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnView = new System.Windows.Forms.Button();
            this.btnDel = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.tabText = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.lvText = new System.Windows.Forms.ListView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.tabKind = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tvKind = new System.Windows.Forms.TreeView();
            this.imageList16 = new System.Windows.Forms.ImageList(this.components);
            this.btnClose = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            this.tabText.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabKind.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(794, 65);
            this.panel1.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(50, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 18);
            this.label1.TabIndex = 1;
            this.label1.Text = "CG Edit";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::MewsBroad.Properties.Resources.popIconCg;
            this.pictureBox1.Location = new System.Drawing.Point(12, 16);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(32, 32);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.btnKindDel);
            this.panel2.Controls.Add(this.btnKindEdit);
            this.panel2.Controls.Add(this.btnKindAdd);
            this.panel2.Controls.Add(this.btnAdd);
            this.panel2.Controls.Add(this.btnView);
            this.panel2.Controls.Add(this.btnDel);
            this.panel2.Controls.Add(this.btnEdit);
            this.panel2.Controls.Add(this.tabText);
            this.panel2.Controls.Add(this.tabKind);
            this.panel2.Location = new System.Drawing.Point(1, 67);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(793, 389);
            this.panel2.TabIndex = 5;
            // 
            // btnKindDel
            // 
            this.btnKindDel.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnKindDel.Location = new System.Drawing.Point(178, 339);
            this.btnKindDel.Name = "btnKindDel";
            this.btnKindDel.Size = new System.Drawing.Size(84, 38);
            this.btnKindDel.TabIndex = 64;
            this.btnKindDel.Text = "Delete";
            this.btnKindDel.UseVisualStyleBackColor = true;
            this.btnKindDel.Click += new System.EventHandler(this.btnKindDel_Click);
            // 
            // btnKindEdit
            // 
            this.btnKindEdit.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnKindEdit.Location = new System.Drawing.Point(93, 339);
            this.btnKindEdit.Name = "btnKindEdit";
            this.btnKindEdit.Size = new System.Drawing.Size(84, 38);
            this.btnKindEdit.TabIndex = 63;
            this.btnKindEdit.Text = "Edit";
            this.btnKindEdit.UseVisualStyleBackColor = true;
            this.btnKindEdit.Click += new System.EventHandler(this.btnKindEdit_Click);
            // 
            // btnKindAdd
            // 
            this.btnKindAdd.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnKindAdd.Location = new System.Drawing.Point(8, 339);
            this.btnKindAdd.Name = "btnKindAdd";
            this.btnKindAdd.Size = new System.Drawing.Size(84, 38);
            this.btnKindAdd.TabIndex = 62;
            this.btnKindAdd.Text = "Add";
            this.btnKindAdd.UseVisualStyleBackColor = true;
            this.btnKindAdd.Click += new System.EventHandler(this.btnKindAdd_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.Location = new System.Drawing.Point(451, 339);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(110, 38);
            this.btnAdd.TabIndex = 58;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnView
            // 
            this.btnView.BackColor = System.Drawing.Color.LightSteelBlue;
            this.btnView.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnView.Location = new System.Drawing.Point(340, 339);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(110, 38);
            this.btnView.TabIndex = 61;
            this.btnView.Text = "Preview";
            this.btnView.UseVisualStyleBackColor = false;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // btnDel
            // 
            this.btnDel.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDel.Location = new System.Drawing.Point(673, 339);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(110, 38);
            this.btnDel.TabIndex = 60;
            this.btnDel.Text = "Delete";
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEdit.Location = new System.Drawing.Point(562, 339);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(110, 38);
            this.btnEdit.TabIndex = 59;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // tabText
            // 
            this.tabText.Controls.Add(this.tabPage1);
            this.tabText.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabText.Location = new System.Drawing.Point(266, 9);
            this.tabText.Name = "tabText";
            this.tabText.SelectedIndex = 0;
            this.tabText.Size = new System.Drawing.Size(519, 326);
            this.tabText.TabIndex = 5;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.lvText);
            this.tabPage1.Location = new System.Drawing.Point(4, 23);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(511, 299);
            this.tabPage1.TabIndex = 1;
            this.tabPage1.Text = "Text List";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // lvText
            // 
            this.lvText.CheckBoxes = true;
            this.lvText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvText.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvText.ForeColor = System.Drawing.Color.Black;
            this.lvText.FullRowSelect = true;
            this.lvText.GridLines = true;
            this.lvText.Location = new System.Drawing.Point(3, 3);
            this.lvText.MultiSelect = false;
            this.lvText.Name = "lvText";
            this.lvText.Size = new System.Drawing.Size(505, 293);
            this.lvText.SmallImageList = this.imageList1;
            this.lvText.TabIndex = 52;
            this.lvText.UseCompatibleStateImageBehavior = false;
            this.lvText.View = System.Windows.Forms.View.Details;
            this.lvText.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvText_ItemCheck);
            this.lvText.SelectedIndexChanged += new System.EventHandler(this.lvText_SelectedIndexChanged);
            this.lvText.DoubleClick += new System.EventHandler(this.lvText_DoubleClick);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "listImgNo.png");
            this.imageList1.Images.SetKeyName(1, "listImg.png");
            // 
            // tabKind
            // 
            this.tabKind.Controls.Add(this.tabPage2);
            this.tabKind.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabKind.Location = new System.Drawing.Point(9, 9);
            this.tabKind.Name = "tabKind";
            this.tabKind.SelectedIndex = 0;
            this.tabKind.Size = new System.Drawing.Size(255, 326);
            this.tabKind.TabIndex = 4;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.tvKind);
            this.tabPage2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPage2.Location = new System.Drawing.Point(4, 23);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(247, 299);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Kind List";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tvKind
            // 
            this.tvKind.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvKind.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tvKind.FullRowSelect = true;
            this.tvKind.HideSelection = false;
            this.tvKind.ImageIndex = 0;
            this.tvKind.ImageList = this.imageList16;
            this.tvKind.Location = new System.Drawing.Point(3, 3);
            this.tvKind.Name = "tvKind";
            this.tvKind.SelectedImageIndex = 0;
            this.tvKind.Size = new System.Drawing.Size(241, 293);
            this.tvKind.TabIndex = 1;
            this.tvKind.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvKind_AfterSelect);
            // 
            // imageList16
            // 
            this.imageList16.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList16.ImageStream")));
            this.imageList16.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList16.Images.SetKeyName(0, "videotape.png");
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(558, 463);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(110, 41);
            this.btnClose.TabIndex = 252;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnClear
            // 
            this.btnClear.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.Location = new System.Drawing.Point(674, 463);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(110, 41);
            this.btnClear.TabIndex = 253;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // BoardMngForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(794, 510);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BoardMngForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.tabText.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabKind.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.TabControl tabText;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.ListView lvText;
        private System.Windows.Forms.TabControl tabKind;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TreeView tvKind;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.Button btnView;
        private System.Windows.Forms.Button btnKindDel;
        private System.Windows.Forms.Button btnKindEdit;
        private System.Windows.Forms.Button btnKindAdd;
        private System.Windows.Forms.ImageList imageList16;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ImageList imageList1;
    }
}