namespace Mews.Svr.Broad
{
    partial class OrderSelGrp
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

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OrderSelGrp));
            this.imageList16 = new System.Windows.Forms.ImageList(this.components);
            this.lbTerm = new System.Windows.Forms.Label();
            this.lvTerm = new System.Windows.Forms.ListView();
            this.lbDist = new System.Windows.Forms.Label();
            this.btnEnd = new Mews.Svr.Broad.GlassButton();
            this.lvDist = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // imageList16
            // 
            this.imageList16.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList16.ImageStream")));
            this.imageList16.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList16.Images.SetKeyName(0, "folder.ico");
            this.imageList16.Images.SetKeyName(1, "church.ico");
            this.imageList16.Images.SetKeyName(2, "school.ico");
            this.imageList16.Images.SetKeyName(3, "safe.ico");
            // 
            // lbTerm
            // 
            this.lbTerm.AutoSize = true;
            this.lbTerm.BackColor = System.Drawing.Color.Transparent;
            this.lbTerm.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTerm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(49)))), ((int)(((byte)(96)))));
            this.lbTerm.Location = new System.Drawing.Point(7, 251);
            this.lbTerm.Name = "lbTerm";
            this.lbTerm.Size = new System.Drawing.Size(114, 16);
            this.lbTerm.TabIndex = 20;
            this.lbTerm.Text = "▶ Term Select";
            // 
            // lvTerm
            // 
            this.lvTerm.CheckBoxes = true;
            this.lvTerm.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvTerm.FullRowSelect = true;
            this.lvTerm.GridLines = true;
            this.lvTerm.HideSelection = false;
            this.lvTerm.Location = new System.Drawing.Point(10, 280);
            this.lvTerm.Name = "lvTerm";
            this.lvTerm.Size = new System.Drawing.Size(291, 301);
            this.lvTerm.TabIndex = 22;
            this.lvTerm.UseCompatibleStateImageBehavior = false;
            this.lvTerm.View = System.Windows.Forms.View.Details;
            this.lvTerm.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvTerm_ItemCheck);
            this.lvTerm.SelectedIndexChanged += new System.EventHandler(this.lvTerm_SelectedIndexChanged);
            // 
            // lbDist
            // 
            this.lbDist.AutoSize = true;
            this.lbDist.BackColor = System.Drawing.Color.Transparent;
            this.lbDist.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDist.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(49)))), ((int)(((byte)(96)))));
            this.lbDist.Location = new System.Drawing.Point(9, 7);
            this.lbDist.Name = "lbDist";
            this.lbDist.Size = new System.Drawing.Size(105, 16);
            this.lbDist.TabIndex = 18;
            this.lbDist.Text = "▶ Dist Select";
            // 
            // btnEnd
            // 
            this.btnEnd.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnEnd.GlowColor = System.Drawing.Color.White;
            this.btnEnd.Location = new System.Drawing.Point(174, 588);
            this.btnEnd.Name = "btnEnd";
            this.btnEnd.Size = new System.Drawing.Size(125, 36);
            this.btnEnd.TabIndex = 23;
            this.btnEnd.Text = "SELECT";
            this.btnEnd.Click += new System.EventHandler(this.btnEnd_Click);
            // 
            // lvDist
            // 
            this.lvDist.CheckBoxes = true;
            this.lvDist.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvDist.FullRowSelect = true;
            this.lvDist.GridLines = true;
            this.lvDist.HideSelection = false;
            this.lvDist.Location = new System.Drawing.Point(10, 37);
            this.lvDist.Name = "lvDist";
            this.lvDist.Size = new System.Drawing.Size(291, 200);
            this.lvDist.TabIndex = 21;
            this.lvDist.UseCompatibleStateImageBehavior = false;
            this.lvDist.View = System.Windows.Forms.View.Details;
            this.lvDist.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvDist_ItemCheck);
            this.lvDist.SelectedIndexChanged += new System.EventHandler(this.lvDist_SelectedIndexChanged);
            // 
            // OrderSelGrp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Controls.Add(this.lvDist);
            this.Controls.Add(this.lvTerm);
            this.Controls.Add(this.btnEnd);
            this.Controls.Add(this.lbDist);
            this.Controls.Add(this.lbTerm);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "OrderSelGrp";
            this.Size = new System.Drawing.Size(313, 634);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ImageList imageList16;
        private System.Windows.Forms.Label lbTerm;
        private System.Windows.Forms.ListView lvTerm;
        private System.Windows.Forms.Label lbDist;
        private GlassButton btnEnd;
        private System.Windows.Forms.ListView lvDist;
    }
}
