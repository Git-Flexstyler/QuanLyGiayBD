namespace QuanLyGiayBD3
{
    partial class FormTrangChu
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
            this.panelAnhBia = new System.Windows.Forms.Panel();
            this.groupBoxInfo = new System.Windows.Forms.GroupBox();
            this.groupBoxTopSP = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBoxAnhBia = new System.Windows.Forms.PictureBox();
            this.panelAnhBia.SuspendLayout();
            this.groupBoxInfo.SuspendLayout();
            this.groupBoxTopSP.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAnhBia)).BeginInit();
            this.SuspendLayout();
            // 
            // panelAnhBia
            // 
            this.panelAnhBia.Controls.Add(this.pictureBoxAnhBia);
            this.panelAnhBia.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelAnhBia.Location = new System.Drawing.Point(0, 0);
            this.panelAnhBia.Name = "panelAnhBia";
            this.panelAnhBia.Size = new System.Drawing.Size(1299, 488);
            this.panelAnhBia.TabIndex = 0;
            // 
            // groupBoxInfo
            // 
            this.groupBoxInfo.Controls.Add(this.pictureBox1);
            this.groupBoxInfo.Location = new System.Drawing.Point(3, 1224);
            this.groupBoxInfo.Name = "groupBoxInfo";
            this.groupBoxInfo.Size = new System.Drawing.Size(1296, 483);
            this.groupBoxInfo.TabIndex = 1;
            this.groupBoxInfo.TabStop = false;
            // 
            // groupBoxTopSP
            // 
            this.groupBoxTopSP.Controls.Add(this.flowLayoutPanel1);
            this.groupBoxTopSP.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxTopSP.Location = new System.Drawing.Point(3, 512);
            this.groupBoxTopSP.Name = "groupBoxTopSP";
            this.groupBoxTopSP.Size = new System.Drawing.Size(1296, 706);
            this.groupBoxTopSP.TabIndex = 2;
            this.groupBoxTopSP.TabStop = false;
            this.groupBoxTopSP.Text = "Top sản phẩm bán chạy";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(20, 35);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1252, 640);
            this.flowLayoutPanel1.TabIndex = 0;
            this.flowLayoutPanel1.WrapContents = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Image = global::QuanLyGiayBD3.Properties.Resources.anhbia2;
            this.pictureBox1.Location = new System.Drawing.Point(3, 22);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1290, 458);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBoxAnhBia
            // 
            this.pictureBoxAnhBia.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxAnhBia.Image = global::QuanLyGiayBD3.Properties.Resources.anhbia;
            this.pictureBoxAnhBia.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxAnhBia.Name = "pictureBoxAnhBia";
            this.pictureBoxAnhBia.Size = new System.Drawing.Size(1299, 488);
            this.pictureBoxAnhBia.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxAnhBia.TabIndex = 0;
            this.pictureBoxAnhBia.TabStop = false;
            // 
            // FormTrangChu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1320, 780);
            this.Controls.Add(this.groupBoxTopSP);
            this.Controls.Add(this.groupBoxInfo);
            this.Controls.Add(this.panelAnhBia);
            this.Name = "FormTrangChu";
            this.Text = "FormTrangChu";
            this.Load += new System.EventHandler(this.FormTrangChu_Load);
            this.panelAnhBia.ResumeLayout(false);
            this.groupBoxInfo.ResumeLayout(false);
            this.groupBoxTopSP.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAnhBia)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelAnhBia;
        private System.Windows.Forms.PictureBox pictureBoxAnhBia;
        private System.Windows.Forms.GroupBox groupBoxInfo;
        private System.Windows.Forms.GroupBox groupBoxTopSP;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}