﻿namespace Mews.Svr.Broad
{
    partial class OrderSelLiveSiren
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
            this.btnMic = new Mews.Svr.Broad.GlassButton();
            this.btnRec = new Mews.Svr.Broad.GlassButton();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnMic
            // 
            this.btnMic.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMic.GlowColor = System.Drawing.Color.White;
            this.btnMic.Location = new System.Drawing.Point(18, 57);
            this.btnMic.Name = "btnMic";
            this.btnMic.Size = new System.Drawing.Size(144, 47);
            this.btnMic.TabIndex = 34;
            this.btnMic.Tag = "1";
            this.btnMic.Text = "SIREN 1";
            this.btnMic.Click += new System.EventHandler(this.btnMic_Click);
            // 
            // btnRec
            // 
            this.btnRec.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRec.GlowColor = System.Drawing.Color.White;
            this.btnRec.Location = new System.Drawing.Point(18, 107);
            this.btnRec.Name = "btnRec";
            this.btnRec.Size = new System.Drawing.Size(144, 47);
            this.btnRec.TabIndex = 35;
            this.btnRec.Tag = "2";
            this.btnRec.Text = "SIREN 2";
            this.btnRec.Click += new System.EventHandler(this.btnMic_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(49)))), ((int)(((byte)(96)))));
            this.label2.Location = new System.Drawing.Point(17, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(114, 16);
            this.label2.TabIndex = 33;
            this.label2.Text = "▶ Select Siren";
            // 
            // OrderSelLiveSiren
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::MewsBroad.Properties.Resources.bgLiveActive;
            this.Controls.Add(this.btnMic);
            this.Controls.Add(this.btnRec);
            this.Controls.Add(this.label2);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "OrderSelLiveSiren";
            this.Size = new System.Drawing.Size(183, 189);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Mews.Svr.Broad.GlassButton btnMic;
        private Mews.Svr.Broad.GlassButton btnRec;
        private System.Windows.Forms.Label label2;

    }
}
