namespace QuanLyGiayBD3
{
    partial class FormSanPham
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
            this.groupBoxChucNang = new System.Windows.Forms.GroupBox();
            this.btnXemChiTiet = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnThem = new System.Windows.Forms.Button();
            this.txtSoLuong = new System.Windows.Forms.TextBox();
            this.lblSoLuong = new System.Windows.Forms.Label();
            this.lblAnh = new System.Windows.Forms.Label();
            this.cboMaKho = new System.Windows.Forms.ComboBox();
            this.lblMaKho = new System.Windows.Forms.Label();
            this.txtMoTa = new System.Windows.Forms.TextBox();
            this.lblMoTa = new System.Windows.Forms.Label();
            this.txtGiaKM = new System.Windows.Forms.TextBox();
            this.lblGiaKM = new System.Windows.Forms.Label();
            this.txtGiaGoc = new System.Windows.Forms.TextBox();
            this.lblGiaGoc = new System.Windows.Forms.Label();
            this.txtThuongHieu = new System.Windows.Forms.TextBox();
            this.lblThuongHieu = new System.Windows.Forms.Label();
            this.txtTenSP = new System.Windows.Forms.TextBox();
            this.lblTenSP = new System.Windows.Forms.Label();
            this.txtMaSP = new System.Windows.Forms.TextBox();
            this.groupBoxDanhSach = new System.Windows.Forms.GroupBox();
            this.lvSanPham = new System.Windows.Forms.ListView();
            this.lblMaSP = new System.Windows.Forms.Label();
            this.groupBoxThongTin = new System.Windows.Forms.GroupBox();
            this.picSearch = new System.Windows.Forms.PictureBox();
            this.txtTimKiem = new System.Windows.Forms.TextBox();
            this.btnChonAnh = new System.Windows.Forms.Button();
            this.picHinhAnh = new System.Windows.Forms.PictureBox();
            this.groupBoxChucNang.SuspendLayout();
            this.groupBoxDanhSach.SuspendLayout();
            this.groupBoxThongTin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSearch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picHinhAnh)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBoxChucNang
            // 
            this.groupBoxChucNang.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxChucNang.Controls.Add(this.btnXemChiTiet);
            this.groupBoxChucNang.Controls.Add(this.btnReset);
            this.groupBoxChucNang.Controls.Add(this.btnXoa);
            this.groupBoxChucNang.Controls.Add(this.btnSua);
            this.groupBoxChucNang.Controls.Add(this.btnThem);
            this.groupBoxChucNang.Font = new System.Drawing.Font("Segoe UI Semibold", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxChucNang.Location = new System.Drawing.Point(0, 324);
            this.groupBoxChucNang.Name = "groupBoxChucNang";
            this.groupBoxChucNang.Size = new System.Drawing.Size(1320, 92);
            this.groupBoxChucNang.TabIndex = 4;
            this.groupBoxChucNang.TabStop = false;
            this.groupBoxChucNang.Text = "Chức năng";
            // 
            // btnXemChiTiet
            // 
            this.btnXemChiTiet.Location = new System.Drawing.Point(1100, 28);
            this.btnXemChiTiet.Name = "btnXemChiTiet";
            this.btnXemChiTiet.Size = new System.Drawing.Size(125, 35);
            this.btnXemChiTiet.TabIndex = 5;
            this.btnXemChiTiet.Text = "Xem chi tiết";
            this.btnXemChiTiet.UseVisualStyleBackColor = true;
            this.btnXemChiTiet.Click += new System.EventHandler(this.btnXem_Click);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(850, 28);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(125, 35);
            this.btnReset.TabIndex = 4;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.Location = new System.Drawing.Point(600, 28);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(125, 35);
            this.btnXoa.TabIndex = 2;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = true;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btnSua
            // 
            this.btnSua.Location = new System.Drawing.Point(350, 28);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(125, 35);
            this.btnSua.TabIndex = 1;
            this.btnSua.Text = "Sửa";
            this.btnSua.UseVisualStyleBackColor = true;
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // btnThem
            // 
            this.btnThem.Location = new System.Drawing.Point(100, 28);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(125, 35);
            this.btnThem.TabIndex = 0;
            this.btnThem.Text = "Thêm";
            this.btnThem.UseVisualStyleBackColor = true;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // txtSoLuong
            // 
            this.txtSoLuong.Location = new System.Drawing.Point(519, 114);
            this.txtSoLuong.Name = "txtSoLuong";
            this.txtSoLuong.Size = new System.Drawing.Size(203, 29);
            this.txtSoLuong.TabIndex = 22;
            this.txtSoLuong.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblSoLuong
            // 
            this.lblSoLuong.AutoSize = true;
            this.lblSoLuong.Location = new System.Drawing.Point(431, 117);
            this.lblSoLuong.Name = "lblSoLuong";
            this.lblSoLuong.Size = new System.Drawing.Size(73, 21);
            this.lblSoLuong.TabIndex = 21;
            this.lblSoLuong.Text = "Số lượng";
            // 
            // lblAnh
            // 
            this.lblAnh.AutoSize = true;
            this.lblAnh.Location = new System.Drawing.Point(1167, 37);
            this.lblAnh.Name = "lblAnh";
            this.lblAnh.Size = new System.Drawing.Size(39, 21);
            this.lblAnh.TabIndex = 20;
            this.lblAnh.Text = "Ảnh";
            // 
            // cboMaKho
            // 
            this.cboMaKho.FormattingEnabled = true;
            this.cboMaKho.Location = new System.Drawing.Point(519, 163);
            this.cboMaKho.Name = "cboMaKho";
            this.cboMaKho.Size = new System.Drawing.Size(203, 29);
            this.cboMaKho.TabIndex = 13;
            // 
            // lblMaKho
            // 
            this.lblMaKho.AutoSize = true;
            this.lblMaKho.Location = new System.Drawing.Point(431, 166);
            this.lblMaKho.Name = "lblMaKho";
            this.lblMaKho.Size = new System.Drawing.Size(63, 21);
            this.lblMaKho.TabIndex = 12;
            this.lblMaKho.Text = "Mã kho";
            // 
            // txtMoTa
            // 
            this.txtMoTa.Location = new System.Drawing.Point(845, 37);
            this.txtMoTa.Multiline = true;
            this.txtMoTa.Name = "txtMoTa";
            this.txtMoTa.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMoTa.Size = new System.Drawing.Size(226, 158);
            this.txtMoTa.TabIndex = 11;
            // 
            // lblMoTa
            // 
            this.lblMoTa.AutoSize = true;
            this.lblMoTa.Location = new System.Drawing.Point(767, 34);
            this.lblMoTa.Name = "lblMoTa";
            this.lblMoTa.Size = new System.Drawing.Size(52, 21);
            this.lblMoTa.TabIndex = 10;
            this.lblMoTa.Text = "Mô tả";
            // 
            // txtGiaKM
            // 
            this.txtGiaKM.Location = new System.Drawing.Point(519, 67);
            this.txtGiaKM.Name = "txtGiaKM";
            this.txtGiaKM.Size = new System.Drawing.Size(203, 29);
            this.txtGiaKM.TabIndex = 9;
            this.txtGiaKM.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblGiaKM
            // 
            this.lblGiaKM.AutoSize = true;
            this.lblGiaKM.Location = new System.Drawing.Point(431, 73);
            this.lblGiaKM.Name = "lblGiaKM";
            this.lblGiaKM.Size = new System.Drawing.Size(63, 21);
            this.lblGiaKM.TabIndex = 8;
            this.lblGiaKM.Text = "Giá KM";
            this.lblGiaKM.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtGiaGoc
            // 
            this.txtGiaGoc.Location = new System.Drawing.Point(519, 26);
            this.txtGiaGoc.Name = "txtGiaGoc";
            this.txtGiaGoc.Size = new System.Drawing.Size(203, 29);
            this.txtGiaGoc.TabIndex = 7;
            this.txtGiaGoc.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblGiaGoc
            // 
            this.lblGiaGoc.AutoSize = true;
            this.lblGiaGoc.Location = new System.Drawing.Point(430, 29);
            this.lblGiaGoc.Name = "lblGiaGoc";
            this.lblGiaGoc.Size = new System.Drawing.Size(64, 21);
            this.lblGiaGoc.TabIndex = 6;
            this.lblGiaGoc.Text = "Giá gốc";
            // 
            // txtThuongHieu
            // 
            this.txtThuongHieu.Location = new System.Drawing.Point(126, 117);
            this.txtThuongHieu.Name = "txtThuongHieu";
            this.txtThuongHieu.Size = new System.Drawing.Size(270, 29);
            this.txtThuongHieu.TabIndex = 5;
            // 
            // lblThuongHieu
            // 
            this.lblThuongHieu.AutoSize = true;
            this.lblThuongHieu.Location = new System.Drawing.Point(20, 117);
            this.lblThuongHieu.Name = "lblThuongHieu";
            this.lblThuongHieu.Size = new System.Drawing.Size(100, 21);
            this.lblThuongHieu.TabIndex = 4;
            this.lblThuongHieu.Text = "Thương hiệu";
            // 
            // txtTenSP
            // 
            this.txtTenSP.Location = new System.Drawing.Point(126, 73);
            this.txtTenSP.Name = "txtTenSP";
            this.txtTenSP.Size = new System.Drawing.Size(270, 29);
            this.txtTenSP.TabIndex = 3;
            // 
            // lblTenSP
            // 
            this.lblTenSP.AutoSize = true;
            this.lblTenSP.Location = new System.Drawing.Point(20, 73);
            this.lblTenSP.Name = "lblTenSP";
            this.lblTenSP.Size = new System.Drawing.Size(58, 21);
            this.lblTenSP.TabIndex = 2;
            this.lblTenSP.Text = "Tên SP";
            // 
            // txtMaSP
            // 
            this.txtMaSP.Location = new System.Drawing.Point(126, 29);
            this.txtMaSP.Name = "txtMaSP";
            this.txtMaSP.Size = new System.Drawing.Size(270, 29);
            this.txtMaSP.TabIndex = 1;
            // 
            // groupBoxDanhSach
            // 
            this.groupBoxDanhSach.Controls.Add(this.lvSanPham);
            this.groupBoxDanhSach.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBoxDanhSach.Font = new System.Drawing.Font("Segoe UI Semibold", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxDanhSach.Location = new System.Drawing.Point(0, 422);
            this.groupBoxDanhSach.Name = "groupBoxDanhSach";
            this.groupBoxDanhSach.Size = new System.Drawing.Size(1320, 358);
            this.groupBoxDanhSach.TabIndex = 5;
            this.groupBoxDanhSach.TabStop = false;
            this.groupBoxDanhSach.Text = "Danh sách sản phẩm";
            // 
            // lvSanPham
            // 
            this.lvSanPham.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvSanPham.HideSelection = false;
            this.lvSanPham.Location = new System.Drawing.Point(3, 25);
            this.lvSanPham.Name = "lvSanPham";
            this.lvSanPham.Size = new System.Drawing.Size(1314, 330);
            this.lvSanPham.TabIndex = 0;
            this.lvSanPham.UseCompatibleStateImageBehavior = false;
            this.lvSanPham.SelectedIndexChanged += new System.EventHandler(this.lvSanPham_SelectedIndexChanged);
            // 
            // lblMaSP
            // 
            this.lblMaSP.AutoSize = true;
            this.lblMaSP.Location = new System.Drawing.Point(20, 29);
            this.lblMaSP.Name = "lblMaSP";
            this.lblMaSP.Size = new System.Drawing.Size(55, 21);
            this.lblMaSP.TabIndex = 0;
            this.lblMaSP.Text = "Mã SP";
            // 
            // groupBoxThongTin
            // 
            this.groupBoxThongTin.Controls.Add(this.picSearch);
            this.groupBoxThongTin.Controls.Add(this.txtTimKiem);
            this.groupBoxThongTin.Controls.Add(this.btnChonAnh);
            this.groupBoxThongTin.Controls.Add(this.txtSoLuong);
            this.groupBoxThongTin.Controls.Add(this.lblSoLuong);
            this.groupBoxThongTin.Controls.Add(this.lblAnh);
            this.groupBoxThongTin.Controls.Add(this.picHinhAnh);
            this.groupBoxThongTin.Controls.Add(this.cboMaKho);
            this.groupBoxThongTin.Controls.Add(this.lblMaKho);
            this.groupBoxThongTin.Controls.Add(this.txtMoTa);
            this.groupBoxThongTin.Controls.Add(this.lblMoTa);
            this.groupBoxThongTin.Controls.Add(this.txtGiaKM);
            this.groupBoxThongTin.Controls.Add(this.lblGiaKM);
            this.groupBoxThongTin.Controls.Add(this.txtGiaGoc);
            this.groupBoxThongTin.Controls.Add(this.lblGiaGoc);
            this.groupBoxThongTin.Controls.Add(this.txtThuongHieu);
            this.groupBoxThongTin.Controls.Add(this.lblThuongHieu);
            this.groupBoxThongTin.Controls.Add(this.txtTenSP);
            this.groupBoxThongTin.Controls.Add(this.lblTenSP);
            this.groupBoxThongTin.Controls.Add(this.txtMaSP);
            this.groupBoxThongTin.Controls.Add(this.lblMaSP);
            this.groupBoxThongTin.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxThongTin.Font = new System.Drawing.Font("Segoe UI Semibold", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxThongTin.Location = new System.Drawing.Point(0, 0);
            this.groupBoxThongTin.Name = "groupBoxThongTin";
            this.groupBoxThongTin.Size = new System.Drawing.Size(1320, 324);
            this.groupBoxThongTin.TabIndex = 3;
            this.groupBoxThongTin.TabStop = false;
            this.groupBoxThongTin.Text = "Thông tin sản phẩm";
            // 
            // picSearch
            // 
            this.picSearch.BackColor = System.Drawing.Color.White;
            this.picSearch.Image = global::QuanLyGiayBD3.Properties.Resources.kinhlup2;
            this.picSearch.Location = new System.Drawing.Point(366, 252);
            this.picSearch.Name = "picSearch";
            this.picSearch.Size = new System.Drawing.Size(30, 30);
            this.picSearch.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picSearch.TabIndex = 25;
            this.picSearch.TabStop = false;
            this.picSearch.Click += new System.EventHandler(this.picSearch_Click);
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.ForeColor = System.Drawing.Color.Gray;
            this.txtTimKiem.Location = new System.Drawing.Point(126, 252);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.Size = new System.Drawing.Size(270, 29);
            this.txtTimKiem.TabIndex = 24;
            this.txtTimKiem.Text = "Tim kiếm";
            this.txtTimKiem.TextChanged += new System.EventHandler(this.txtTimKiem_TextChanged);
            this.txtTimKiem.Enter += new System.EventHandler(this.txtTimKiem_Enter);
            this.txtTimKiem.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTimKiem_KeyDown);
            this.txtTimKiem.Leave += new System.EventHandler(this.txtTimKiem_Leave);
            // 
            // btnChonAnh
            // 
            this.btnChonAnh.Location = new System.Drawing.Point(1125, 252);
            this.btnChonAnh.Name = "btnChonAnh";
            this.btnChonAnh.Size = new System.Drawing.Size(120, 35);
            this.btnChonAnh.TabIndex = 23;
            this.btnChonAnh.Text = "Chọn ảnh";
            this.btnChonAnh.UseVisualStyleBackColor = true;
            this.btnChonAnh.Click += new System.EventHandler(this.btnChonAnh_Click);
            // 
            // picHinhAnh
            // 
            this.picHinhAnh.Location = new System.Drawing.Point(1100, 67);
            this.picHinhAnh.Name = "picHinhAnh";
            this.picHinhAnh.Size = new System.Drawing.Size(156, 160);
            this.picHinhAnh.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picHinhAnh.TabIndex = 19;
            this.picHinhAnh.TabStop = false;
            // 
            // FormSanPham
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1320, 780);
            this.Controls.Add(this.groupBoxChucNang);
            this.Controls.Add(this.groupBoxDanhSach);
            this.Controls.Add(this.groupBoxThongTin);
            this.Name = "FormSanPham";
            this.Text = "FormSanPham";
            this.Load += new System.EventHandler(this.FormSanPham_Load);
            this.groupBoxChucNang.ResumeLayout(false);
            this.groupBoxDanhSach.ResumeLayout(false);
            this.groupBoxThongTin.ResumeLayout(false);
            this.groupBoxThongTin.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSearch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picHinhAnh)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxChucNang;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.TextBox txtSoLuong;
        private System.Windows.Forms.Label lblSoLuong;
        private System.Windows.Forms.Label lblAnh;
        private System.Windows.Forms.PictureBox picHinhAnh;
        private System.Windows.Forms.ComboBox cboMaKho;
        private System.Windows.Forms.Label lblMaKho;
        private System.Windows.Forms.TextBox txtMoTa;
        private System.Windows.Forms.Label lblMoTa;
        private System.Windows.Forms.TextBox txtGiaKM;
        private System.Windows.Forms.Label lblGiaKM;
        private System.Windows.Forms.TextBox txtGiaGoc;
        private System.Windows.Forms.Label lblGiaGoc;
        private System.Windows.Forms.TextBox txtThuongHieu;
        private System.Windows.Forms.Label lblThuongHieu;
        private System.Windows.Forms.TextBox txtTenSP;
        private System.Windows.Forms.Label lblTenSP;
        private System.Windows.Forms.TextBox txtMaSP;
        private System.Windows.Forms.GroupBox groupBoxDanhSach;
        private System.Windows.Forms.Label lblMaSP;
        private System.Windows.Forms.GroupBox groupBoxThongTin;
        private System.Windows.Forms.ListView lvSanPham;
        private System.Windows.Forms.Button btnXemChiTiet;
        private System.Windows.Forms.Button btnChonAnh;
        private System.Windows.Forms.PictureBox picSearch;
        private System.Windows.Forms.TextBox txtTimKiem;
    }
}