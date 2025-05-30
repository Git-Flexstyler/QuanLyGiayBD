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
        private Form currentFormChild; // Form con đang mở

        public FormMain()
        {
            InitializeComponent();
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

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormThongKe());
        }


        // Thêm các button khác tương tự
    }
}
