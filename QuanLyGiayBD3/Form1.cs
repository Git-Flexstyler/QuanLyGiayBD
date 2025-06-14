using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace QuanLyGiayBD3
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();

            // Ẩn nhóm đổi mật khẩu từ đầu
            lblTaiKhoanDMK.Visible = false;
            txtTaiKhoanDMK.Visible = false;
            lblMatKhauCu.Visible = false;
            txtMatKhauCu.Visible = false;
            lblMatKhauMoi.Visible = false;
            txtMatKhauMoi.Visible = false;
            lblXacNhan.Visible = false;
            txtXacNhan.Visible = false;
            btnDoiMatKhau.Visible = false;
            btnHuy.Visible = false;

            // Gán sự kiện phím
            txtTaiKhoan.KeyDown += TxtTaiKhoan_KeyDown;
            txtMatKhau.KeyDown += TxtMatKhau_KeyDown;

    }

        private void TxtTaiKhoan_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // Ngăn tiếng "beep"
                txtMatKhau.Focus();        // Di chuyển tới ô mật khẩu
            }
        }

        private void TxtMatKhau_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnLogin.PerformClick(); // Gọi sự kiện đăng nhập
            }
        }

        private void chkHienMatKhau_CheckedChanged(object sender, EventArgs e)
        {
            txtMatKhau.UseSystemPasswordChar = !chkHienMatKhau.Checked;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string user = txtTaiKhoan.Text.Trim();
            string pass = txtMatKhau.Text.Trim();

            string connStr = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=QuanLyGiayTheThao;Integrated Security=True;TrustServerCertificate=True";

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                // Kiểm tra tài khoản + mật khẩu
                string query = "SELECT COUNT(*) FROM NhanVien WHERE TaiKhoan = @user AND MatKhau = @pass";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@user", user);
                    cmd.Parameters.AddWithValue("@pass", pass);

                    int count = (int)cmd.ExecuteScalar();

                    if (count == 1)
                    {
                        // Lấy quyền của nhân viên
                        string getRole = "SELECT Quyen FROM NhanVien WHERE TaiKhoan = @user";
                        using (SqlCommand roleCmd = new SqlCommand(getRole, conn))
                        {
                            roleCmd.Parameters.AddWithValue("@user", user);
                            string role = roleCmd.ExecuteScalar()?.ToString();

                            MessageBox.Show("Đăng nhập thành công!");

                            FormMain f = new FormMain(user, role); // Truyền user và role
                            this.Hide();
                            f.ShowDialog();
                            this.Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Sai tài khoản hoặc mật khẩu!");
                    }
                }
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Ẩn nhóm đăng nhập
            lblTaiKhoan.Visible = false;
            txtTaiKhoan.Visible = false;
            lblMatKhau.Visible = false;
            txtMatKhau.Visible = false;
            btnLogin.Visible = false;
            chkHienMatKhau.Visible = false;

            // Hiện nhóm đổi mật khẩu
            lblTaiKhoanDMK.Visible = true;
            txtTaiKhoanDMK.Visible = true;
            lblMatKhauCu.Visible = true;
            txtMatKhauCu.Visible = true;
            lblMatKhauMoi.Visible = true;
            txtMatKhauMoi.Visible = true;
            lblXacNhan.Visible = true;
            txtXacNhan.Visible = true;
            btnDoiMatKhau.Visible = true;
            btnHuy.Visible = true;
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            // Hiện nhóm đăng nhập
            lblTaiKhoan.Visible = true;
            txtTaiKhoan.Visible = true;
            lblMatKhau.Visible = true;
            txtMatKhau.Visible = true;
            btnLogin.Visible = true;
            chkHienMatKhau.Visible = true;

            // Ẩn nhóm đổi mật khẩu
            lblTaiKhoanDMK.Visible = false;
            txtTaiKhoanDMK.Visible = false;
            lblMatKhauCu.Visible = false;
            txtMatKhauCu.Visible = false;
            lblMatKhauMoi.Visible = false;
            txtMatKhauMoi.Visible = false;
            lblXacNhan.Visible = false;
            txtXacNhan.Visible = false;
            btnDoiMatKhau.Visible = false;
            btnHuy.Visible = false;

            // Xóa dữ liệu các textbox
            txtTaiKhoanDMK.Clear();
            txtMatKhauCu.Clear();
            txtMatKhauMoi.Clear();
            txtXacNhan.Clear();

            txtTaiKhoan.Clear();
            txtMatKhau.Clear();
            chkHienMatKhau.Checked = false;

        }

        private void btnDoiMatKhau_Click(object sender, EventArgs e)
        {
            string taiKhoan = txtTaiKhoanDMK.Text.Trim();
            string matKhauCu = txtMatKhauCu.Text.Trim();
            string matKhauMoi = txtMatKhauMoi.Text.Trim();
            string xacNhan = txtXacNhan.Text.Trim();

            if (taiKhoan == "" || matKhauCu == "" || matKhauMoi == "" || xacNhan == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.");
                return;
            }

            if (matKhauMoi != xacNhan)
            {
                MessageBox.Show("Mật khẩu xác nhận không khớp.");
                return;
            }

            string connStr = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=QuanLyGiayTheThao;Integrated Security=True;TrustServerCertificate=True";
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                // Kiểm tra tài khoản và mật khẩu cũ
                string checkQuery = "SELECT COUNT(*) FROM NhanVien WHERE TaiKhoan = @tk AND MatKhau = @mk";
                SqlCommand checkCmd = new SqlCommand(checkQuery, conn);
                checkCmd.Parameters.AddWithValue("@tk", taiKhoan);
                checkCmd.Parameters.AddWithValue("@mk", matKhauCu);

                int count = (int)checkCmd.ExecuteScalar();
                if (count == 0)
                {
                    MessageBox.Show("Sai tài khoản hoặc mật khẩu cũ.");
                    return;
                }

                // Cập nhật mật khẩu mới
                string updateQuery = "UPDATE NhanVien SET MatKhau = @mkmoi WHERE TaiKhoan = @tk";
                SqlCommand updateCmd = new SqlCommand(updateQuery, conn);
                updateCmd.Parameters.AddWithValue("@mkmoi", matKhauMoi);
                updateCmd.Parameters.AddWithValue("@tk", taiKhoan);

                int rows = updateCmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    MessageBox.Show("Đổi mật khẩu thành công!");
                    btnHuy_Click(null, null); // Quay lại giao diện đăng nhập
                }
                else
                {
                    MessageBox.Show("Đổi mật khẩu thất bại.");
                }
            }
        }

        private void linkLabelDangKy_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Ẩn phần đăng nhập
            txtTaiKhoan.Visible = false;
            txtMatKhau.Visible = false;
            btnLogin.Visible = false;
            chkHienMatKhau.Visible = false;
            lblTaiKhoan.Visible = false;
            lblMatKhau.Visible = false;
            linkLabel1.Visible = false;
            linkLabelDangKy.Visible = false;

            // Hiện phần đăng ký
            txtTenNV_DK.Visible = true;
            txtTaiKhoan_DK.Visible = true;
            txtMatKhau_DK.Visible = true;
            txtXacNhan_DK.Visible = true;
            btnDangKy.Visible = true;
            btnHuyDK.Visible = true;
            lblTenNV_DK.Visible = true;
            lblTaiKhoan_DK.Visible = true;
            lblMatKhau_DK.Visible = true;
            lblXacNhan_DK.Visible = true;

            // Xóa dữ liệu (nếu có)
            txtTenNV_DK.Clear();
            txtTaiKhoan_DK.Clear();
            txtMatKhau_DK.Clear();
            txtXacNhan_DK.Clear();
        }

        private void btnHuyDK_Click(object sender, EventArgs e)
        {
            // Ẩn phần đăng ký
            txtTenNV_DK.Visible = false;
            txtTaiKhoan_DK.Visible = false;
            txtMatKhau_DK.Visible = false;
            txtXacNhan_DK.Visible = false;
            btnDangKy.Visible = false;
            btnHuyDK.Visible = false;
            lblTenNV_DK.Visible = false;
            lblTaiKhoan_DK.Visible = false;
            lblMatKhau_DK.Visible = false;
            lblXacNhan_DK.Visible = false;

            // Hiện phần đăng nhập
            txtTaiKhoan.Visible = true;
            txtMatKhau.Visible = true;
            btnLogin.Visible = true;
            chkHienMatKhau.Visible = true;
            lblTaiKhoan.Visible = true;
            lblMatKhau.Visible = true;
            linkLabel1.Visible = true;
            linkLabelDangKy.Visible = true;

            // Xóa dữ liệu
            txtTaiKhoan.Clear();
            txtMatKhau.Clear();
        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            string tenNV = txtTenNV_DK.Text.Trim();
            string taiKhoan = txtTaiKhoan_DK.Text.Trim();
            string matKhau = txtMatKhau_DK.Text.Trim();
            string xacNhan = txtXacNhan_DK.Text.Trim();

            if (matKhau != xacNhan)
            {
                MessageBox.Show("Mật khẩu xác nhận không khớp!");
                return;
            }

            string connStr = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=QuanLyGiayTheThao;Integrated Security=True;TrustServerCertificate=True";
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                // Kiểm tra tài khoản đã tồn tại chưa
                string checkQuery = "SELECT COUNT(*) FROM NhanVien WHERE TaiKhoan = @tk";
                SqlCommand checkCmd = new SqlCommand(checkQuery, conn);
                checkCmd.Parameters.AddWithValue("@tk", taiKhoan);
                int count = (int)checkCmd.ExecuteScalar();

                if (count > 0)
                {
                    MessageBox.Show("Tài khoản đã tồn tại!");
                    return;
                }

                // Thêm tài khoản mới (gán quyền mặc định nếu cần)
                string insertQuery = "INSERT INTO NhanVien (TenNV, TaiKhoan, MatKhau) VALUES (@ten, @tk, @mk)";
                SqlCommand insertCmd = new SqlCommand(insertQuery, conn);
                insertCmd.Parameters.AddWithValue("@ten", tenNV);
                insertCmd.Parameters.AddWithValue("@tk", taiKhoan);
                insertCmd.Parameters.AddWithValue("@mk", matKhau);
                insertCmd.ExecuteNonQuery();

                MessageBox.Show("Đăng ký thành công!");

                // Quay lại giao diện đăng nhập
                btnHuyDK_Click(null, null);
            }
        }
    }
}
