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
            string user = txtTaiKhoan.Text;
            string pass = txtMatKhau.Text;

            string connStr = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=QuanLyGiayTheThao;Integrated Security=True;TrustServerCertificate=True";
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM TaiKhoan WHERE TaiKhoan = @user AND MatKhau = @pass";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@user", user);
                cmd.Parameters.AddWithValue("@pass", pass);

                int count = (int)cmd.ExecuteScalar();
                if (count == 1)
                {
                    FormMain f = new FormMain(user);
                    MessageBox.Show("Đăng nhập thành công!");
                    this.Hide();     // Ẩn FormLogin
                    f.ShowDialog();  // Mở FormMain (modal)
                    this.Close();    // Đóng FormLogin sau khi FormMain 
                }
                else
                {
                    MessageBox.Show("Sai tài khoản hoặc mật khẩu!");
                }

            }
        }
    }
}
