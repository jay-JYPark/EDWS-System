namespace Mews.Svr.Broad
{
    partial class OrderSelMsg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OrderSelMsg));
            this.lbTitle = new System.Windows.Forms.Label();
            this.lvMsg = new System.Windows.Forms.ListView();
            this.btnEnd = new Mews.Svr.Broad.GlassButton();
            this.label1 = new System.Windows.Forms.Label();
            this.numRptCnt = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numRptCnt)).BeginInit();
            this.SuspendLayout();
            // 
            // lbTitle
            // 
            this.lbTitle.AutoSize = true;
            this.lbTitle.BackColor = System.Drawing.Color.Transparent;
            this.lbTitle.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(49)))), ((int)(((byte)(96)))));
            this.lbTitle.Location = new System.Drawing.Point(15, 9);
            this.lbTitle.Name = "lbTitle";
            this.lbTitle.Size = new System.Drawing.Size(197, 16);
            this.lbTitle.TabIndex = 28;
            this.lbTitle.Text = "▶ Select Stored Message ";
            // 
            // lvMsg
            // 
            this.lvMsg.CheckBoxes = true;
            this.lvMsg.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvMsg.FullRowSelect = true;
            this.lvMsg.GridLines = true;
            this.lvMsg.HideSelection = false;
            this.lvMsg.Location = new System.Drawing.Point(10, 37);
            this.lvMsg.MultiSelect = false;
            this.lvMsg.Name = "lvMsg";
            this.lvMsg.Size = new System.Drawing.Size(291, 508);
            this.lvMsg.TabIndex = 29;
            this.lvMsg.UseCompatibleStateImageBehavior = false;
            this.lvMsg.View = System.Windows.Forms.View.Details;
            this.lvMsg.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvMsg_ItemCheck);
            this.lvMsg.SelectedIndexChanged += new System.EventHandler(this.lvMsg_SelectedIndexChanged);
            // 
            // btnEnd
            // 
            this.btnEnd.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEnd.GlowColor = System.Drawing.Color.White;
            this.btnEnd.Location = new System.Drawing.Point(176, 589);
            this.btnEnd.Name = "btnEnd";
            this.btnEnd.Size = new System.Drawing.Size(125, 36);
            this.btnEnd.TabIndex = 30;
            this.btnEnd.Text = "SELECT";
            this.btnEnd.Click += new System.EventHandler(this.btnEnd_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(11, 559);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 16);
            this.label1.TabIndex = 34;
            this.label1.Text = "Repeat Count :";
            // 
            // numRptCnt
            // 
            this.numRptCnt.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numRptCnt.Location = new System.Drawing.Point(141, 554);
            this.numRptCnt.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numRptCnt.Name = "numRptCnt";
            this.numRptCnt.Size = new System.Drawing.Size(160, 26);
            this.numRptCnt.TabIndex = 33;
            this.numRptCnt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numRptCnt.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // OrderSelMsg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numRptCnt);
            this.Controls.Add(this.btnEnd);
            this.Controls.Add(this.lvMsg);
            this.Controls.Add(this.lbTitle);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "OrderSelMsg";
            this.Size = new System.Drawing.Size(313, 634);
            ((System.ComponentModel.ISupportInitialize)(this.numRptCnt)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbTitle;
        private System.Windows.Forms.ListView lvMsg;
        private GlassButton btnEnd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numRptCnt;
    }
}
