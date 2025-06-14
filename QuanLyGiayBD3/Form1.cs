﻿using System;
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
            string user = txtTaiKhoan.Text.Trim();
            string pass = txtMatKhau.Text.Trim();

            string connStr = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=QuanLyGiayTheThao;Integrated Security=True;TrustServerCertificate=True";
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string query = @"
            SELECT COUNT(*) 
            FROM NhanVien 
            WHERE TaiKhoan = @user AND MatKhau = @pass";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@user", user);
                cmd.Parameters.AddWithValue("@pass", pass);

                int count = (int)cmd.ExecuteScalar();
                if (count == 1)
                {
                    MessageBox.Show("Đăng nhập thành công!");

                    // Lấy quyền để truyền sang FormMain (nếu cần)
                    string getRole = "SELECT Quyen FROM NhanVien WHERE TaiKhoan = @user";
                    SqlCommand roleCmd = new SqlCommand(getRole, conn);
                    roleCmd.Parameters.AddWithValue("@user", user);
                    string role = (string)roleCmd.ExecuteScalar();

                    FormMain f = new FormMain(user, role); // Truyền thêm quyền nếu FormMain hỗ trợ
                    this.Hide();
                    f.ShowDialog();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Sai tài khoản hoặc mật khẩu!");
                }
            }
        }

    }
}
