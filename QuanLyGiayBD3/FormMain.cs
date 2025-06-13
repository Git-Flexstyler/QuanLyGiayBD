using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyGiayBD3
{
    public partial class FormMain : Form
    {
        private Form currentFormChild;
        private string tenTaiKhoan;
        private string quyenNguoiDung;

        public FormMain(string taiKhoan, string quyen)
        {
            InitializeComponent();
            tenTaiKhoan = taiKhoan;
            quyenNguoiDung = quyen;

            lblAdmin.Text = $"Hello, {tenTaiKhoan} ({quyenNguoiDung})";

            PhanQuyenNguoiDung();
        }

        private void PhanQuyenNguoiDung()
        {
            // Giả sử chỉ Admin mới có quyền xem Nhân viên và Thống kê
            if (quyenNguoiDung != "Admin")
            {
                btnNhanVien.Enabled = false;
            }
        }

        private void OpenChildForm(Form childForm)
        {
            // Đóng và dispose form cũ nếu có
            if (currentFormChild != null)
            {
                currentFormChild.Close();
                currentFormChild.Dispose(); // Giải phóng hoàn toàn
            }

            currentFormChild = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;

            panelContainer.Controls.Clear(); // Xóa toàn bộ controls cũ
            panelContainer.Controls.Add(childForm); // Thêm form mới
            panelContainer.Tag = childForm;

            childForm.BringToFront();
            childForm.Show();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            OpenChildForm(new FormTrangChu()); // Mở form mặc định
            lblNgay.Text = DateTime.Now.ToString("dddd, dd/MM/yyyy"); // gán ngày hiện tại
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormLogin login = new FormLogin();
            login.ShowDialog();
            this.Close();
        }

        private void btnSanPham_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormSanPham());
        }

        private void btnKhachHang_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormKhachHang());
        }

        private void btnNhanVien_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormNhanVien());
        }
        private void btnKhoHang_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormKhoHang());
        }

        private void btnNhapHang_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormNhapHang());
        }
        private void btnTrangChu_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormTrangChu());
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormThongKe());
        }


        // Thêm các button khác tương tự
    }
}
