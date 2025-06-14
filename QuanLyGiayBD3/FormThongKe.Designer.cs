namespace QuanLyGiayBD3
{
    partial class FormThongKe
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.groupBoxTopSP = new System.Windows.Forms.GroupBox();
            this.dataGridViewTopSanPham = new System.Windows.Forms.DataGridView();
            this.chartDoanhThu = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chartNhapKho = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dataGridViewKhachHangVIP = new System.Windows.Forms.DataGridView();
            this.dateTimePickerBatDau = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerKetThuc = new System.Windows.Forms.DateTimePicker();
            this.labelTuNgay = new System.Windows.Forms.Label();
            this.labelDenNgay = new System.Windows.Forms.Label();
            this.groupBoxTopSP.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTopSanPham)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartDoanhThu)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartNhapKho)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewKhachHangVIP)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBoxTopSP
            // 
            this.groupBoxTopSP.Controls.Add(this.labelDenNgay);
            this.groupBoxTopSP.Controls.Add(this.labelTuNgay);
            this.groupBoxTopSP.Controls.Add(this.dateTimePickerKetThuc);
            this.groupBoxTopSP.Controls.Add(this.dateTimePickerBatDau);
            this.groupBoxTopSP.Controls.Add(this.dataGridViewTopSanPham);
            this.groupBoxTopSP.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxTopSP.Location = new System.Drawing.Point(12, 22);
            this.groupBoxTopSP.Name = "groupBoxTopSP";
            this.groupBoxTopSP.Size = new System.Drawing.Size(1296, 628);
            this.groupBoxTopSP.TabIndex = 1;
            this.groupBoxTopSP.TabStop = false;
            this.groupBoxTopSP.Text = "Top sản phẩm bán chạy";
            // 
            // dataGridViewTopSanPham
            // 
            this.dataGridViewTopSanPham.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewTopSanPham.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewTopSanPham.Location = new System.Drawing.Point(39, 109);
            this.dataGridViewTopSanPham.Name = "dataGridViewTopSanPham";
            this.dataGridViewTopSanPham.RowHeadersWidth = 62;
            this.dataGridViewTopSanPham.RowTemplate.Height = 28;
            this.dataGridViewTopSanPham.Size = new System.Drawing.Size(1225, 489);
            this.dataGridViewTopSanPham.TabIndex = 0;
            // 
            // chartDoanhThu
            // 
            chartArea3.Name = "ChartArea1";
            this.chartDoanhThu.ChartAreas.Add(chartArea3);
            legend3.Name = "Legend1";
            this.chartDoanhThu.Legends.Add(legend3);
            this.chartDoanhThu.Location = new System.Drawing.Point(20, 45);
            this.chartDoanhThu.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chartDoanhThu.Name = "chartDoanhThu";
            series3.ChartArea = "ChartArea1";
            series3.Legend = "Legend1";
            series3.Name = "Series1";
            this.chartDoanhThu.Series.Add(series3);
            this.chartDoanhThu.Size = new System.Drawing.Size(1244, 426);
            this.chartDoanhThu.TabIndex = 2;
            this.chartDoanhThu.Text = "chart1";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chartDoanhThu);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 657);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Size = new System.Drawing.Size(1272, 489);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Biểu đồ doanh thu";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chartNhapKho);
            this.groupBox3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(12, 1168);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(652, 492);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Số lượng nhập kho theo nhà cung cấp";
            // 
            // chartNhapKho
            // 
            chartArea4.Name = "ChartArea1";
            this.chartNhapKho.ChartAreas.Add(chartArea4);
            legend4.Name = "Legend1";
            this.chartNhapKho.Legends.Add(legend4);
            this.chartNhapKho.Location = new System.Drawing.Point(39, 59);
            this.chartNhapKho.Name = "chartNhapKho";
            series4.ChartArea = "ChartArea1";
            series4.Legend = "Legend1";
            series4.Name = "Series1";
            this.chartNhapKho.Series.Add(series4);
            this.chartNhapKho.Size = new System.Drawing.Size(568, 374);
            this.chartNhapKho.TabIndex = 0;
            this.chartNhapKho.Text = "chart1";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dataGridViewKhachHangVIP);
            this.groupBox2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(710, 1168);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(574, 492);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Thống kê khách hàng thân thiết";
            // 
            // dataGridViewKhachHangVIP
            // 
            this.dataGridViewKhachHangVIP.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewKhachHangVIP.Location = new System.Drawing.Point(27, 59);
            this.dataGridViewKhachHangVIP.Name = "dataGridViewKhachHangVIP";
            this.dataGridViewKhachHangVIP.RowHeadersWidth = 62;
            this.dataGridViewKhachHangVIP.RowTemplate.Height = 28;
            this.dataGridViewKhachHangVIP.Size = new System.Drawing.Size(524, 374);
            this.dataGridViewKhachHangVIP.TabIndex = 0;
            // 
            // dateTimePickerBatDau
            // 
            this.dateTimePickerBatDau.Location = new System.Drawing.Point(196, 49);
            this.dateTimePickerBatDau.Name = "dateTimePickerBatDau";
            this.dateTimePickerBatDau.Size = new System.Drawing.Size(394, 39);
            this.dateTimePickerBatDau.TabIndex = 1;
            // 
            // dateTimePickerKetThuc
            // 
            this.dateTimePickerKetThuc.Location = new System.Drawing.Point(791, 50);
            this.dateTimePickerKetThuc.Name = "dateTimePickerKetThuc";
            this.dateTimePickerKetThuc.Size = new System.Drawing.Size(396, 39);
            this.dateTimePickerKetThuc.TabIndex = 2;
            // 
            // labelTuNgay
            // 
            this.labelTuNgay.AutoSize = true;
            this.labelTuNgay.Location = new System.Drawing.Point(68, 55);
            this.labelTuNgay.Name = "labelTuNgay";
            this.labelTuNgay.Size = new System.Drawing.Size(107, 32);
            this.labelTuNgay.TabIndex = 3;
            this.labelTuNgay.Text = "Từ ngày";
            // 
            // labelDenNgay
            // 
            this.labelDenNgay.AutoSize = true;
            this.labelDenNgay.Location = new System.Drawing.Point(639, 54);
            this.labelDenNgay.Name = "labelDenNgay";
            this.labelDenNgay.Size = new System.Drawing.Size(120, 32);
            this.labelDenNgay.TabIndex = 4;
            this.labelDenNgay.Text = "đến ngày";
            // 
            // FormThongKe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1346, 810);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBoxTopSP);
            this.Name = "FormThongKe";
            this.Text = "FormThongKe";
            this.Load += new System.EventHandler(this.FormThongKe_Load);
            this.groupBoxTopSP.ResumeLayout(false);
            this.groupBoxTopSP.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTopSanPham)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartDoanhThu)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartNhapKho)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewKhachHangVIP)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBoxTopSP;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartDoanhThu;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView dataGridViewTopSanPham;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartNhapKho;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dataGridViewKhachHangVIP;
        private System.Windows.Forms.Label labelDenNgay;
        private System.Windows.Forms.Label labelTuNgay;
        private System.Windows.Forms.DateTimePicker dateTimePickerKetThuc;
        private System.Windows.Forms.DateTimePicker dateTimePickerBatDau;
    }
}