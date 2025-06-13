namespace QuanLyGiayBD3
{
    partial class ProductPanel
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblSoLuong = new System.Windows.Forms.Label();
            this.lblGiaKM = new System.Windows.Forms.Label();
            this.lblGiaGoc = new System.Windows.Forms.Label();
            this.lblTen = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lblSoLuong);
            this.panel1.Controls.Add(this.lblGiaKM);
            this.panel1.Controls.Add(this.lblGiaGoc);
            this.panel1.Controls.Add(this.lblTen);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.MaximumSize = new System.Drawing.Size(244, 350);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(244, 350);
            this.panel1.TabIndex = 2;
            // 
            // lblSoLuong
            // 
            this.lblSoLuong.AutoSize = true;
            this.lblSoLuong.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSoLuong.Location = new System.Drawing.Point(27, 304);
            this.lblSoLuong.Name = "lblSoLuong";
            this.lblSoLuong.Size = new System.Drawing.Size(149, 32);
            this.lblSoLuong.TabIndex = 4;
            this.lblSoLuong.Text = "Số lượt bán: ";
            // 
            // lblGiaKM
            // 
            this.lblGiaKM.AutoSize = true;
            this.lblGiaKM.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGiaKM.ForeColor = System.Drawing.Color.Red;
            this.lblGiaKM.Location = new System.Drawing.Point(74, 267);
            this.lblGiaKM.Name = "lblGiaKM";
            this.lblGiaKM.Size = new System.Drawing.Size(76, 28);
            this.lblGiaKM.TabIndex = 3;
            this.lblGiaKM.Text = "Giá KM";
            // 
            // lblGiaGoc
            // 
            this.lblGiaGoc.AutoSize = true;
            this.lblGiaGoc.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Strikeout, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGiaGoc.ForeColor = System.Drawing.Color.Silver;
            this.lblGiaGoc.Location = new System.Drawing.Point(74, 239);
            this.lblGiaGoc.Name = "lblGiaGoc";
            this.lblGiaGoc.Size = new System.Drawing.Size(79, 28);
            this.lblGiaGoc.TabIndex = 2;
            this.lblGiaGoc.Text = "Giá gốc";
            // 
            // lblTen
            // 
            this.lblTen.AutoSize = true;
            this.lblTen.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTen.Location = new System.Drawing.Point(40, 178);
            this.lblTen.Name = "lblTen";
            this.lblTen.Size = new System.Drawing.Size(163, 32);
            this.lblTen.TabIndex = 1;
            this.lblTen.Text = "Tên sản phẩm";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(56, 15);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(120, 160);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // ProductPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "ProductPanel";
            this.Size = new System.Drawing.Size(244, 350);
            this.MouseEnter += new System.EventHandler(this.ProductPanel_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.ProductPanel_MouseLeave);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblSoLuong;
        private System.Windows.Forms.Label lblGiaKM;
        private System.Windows.Forms.Label lblGiaGoc;
        private System.Windows.Forms.Label lblTen;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}
