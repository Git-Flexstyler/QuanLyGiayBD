using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyGiayBD3
{
    public partial class FormNhanVien : Form
    {
        public string connectionString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=QuanLyGiayTheThao;Integrated Security=True;TrustServerCertificate=True";

        public FormNhanVien()
        {
            InitializeComponent();
        }

        private void FormNhanVien_Load(object sender, EventArgs e)
        {
            HienThiDanhSachNhanVien();
        }

        void HienThiDanhSachNhanVien()
        {
            listViewNhanVien.Items.Clear();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT MaNV, TenNV, SoDienThoai, QueQuan, TaiKhoan, MatKhau, Quyen FROM NhanVien";

                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ListViewItem item = new ListViewItem(reader["MaNV"].ToString());
                    item.SubItems.Add(reader["TenNV"].ToString());
                    item.SubItems.Add(reader["SoDienThoai"].ToString());
                    item.SubItems.Add(reader["QueQuan"].ToString());
                    item.SubItems.Add(reader["TaiKhoan"].ToString());
                    item.SubItems.Add(reader["MatKhau"].ToString());
                    item.SubItems.Add(reader["Quyen"].ToString());

                    listViewNhanVien.Items.Add(item);
                }

                reader.Close();
                conn.Close();
            }
        }

        private void listViewNhanVien_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewNhanVien.SelectedItems.Count > 0)
            {
                ListViewItem item = listViewNhanVien.SelectedItems[0];

                // Lấy dữ liệu từ ListView
                txtMaNV.Text = item.SubItems[0].Text;
                txtTenNV.Text = item.SubItems[1].Text;
                txtSDT.Text = item.SubItems[2].Text;
                txtQueQuan.Text = item.SubItems[3].Text;
                txtTaiKhoan.Text = item.SubItems[4].Text;
                txtMatKhau.Text = item.SubItems[5].Text;

                string quyen = item.SubItems[6].Text; // cột quyền

                // Reset các checkbox
                chkQuanTri.Checked = false;
                chkBanHang.Checked = false;
                chkKho.Checked = false;

                // Tích vào checkbox tương ứng với quyền
                if (quyen == "Admin")
                    chkQuanTri.Checked = true;
                else if (quyen == "BanHang")
                    chkBanHang.Checked = true;
                else if (quyen == "Kho")
                    chkKho.Checked = true;
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            // Xóa nội dung TextBox
            txtMaNV.Text = "";
            txtTenNV.Text = "";
            txtSDT.Text = "";
            txtQueQuan.Text = "";
            txtTaiKhoan.Text = "";
            txtMatKhau.Text = "";

            // Bỏ tích các quyền
            chkQuanTri.Checked = false;
            chkBanHang.Checked = false;
            chkKho.Checked = false;

            // Bỏ chọn dòng trong ListView
            listViewNhanVien.SelectedItems.Clear();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"INSERT INTO NhanVien (TenNV, SoDienThoai, QueQuan, Email, TaiKhoan, MatKhau, Quyen)
                         VALUES (@TenNV, @SDT, @QueQuan, @Email, @TaiKhoan, @MatKhau, @Quyen);
                         SELECT SCOPE_IDENTITY();"; // Lấy MaNV vừa được thêm

                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@TenNV", txtTenNV.Text);
                cmd.Parameters.AddWithValue("@SDT", txtSDT.Text);
                cmd.Parameters.AddWithValue("@QueQuan", txtQueQuan.Text);
                cmd.Parameters.AddWithValue("@Email", ""); // Nếu bạn chưa có txtEmail
                cmd.Parameters.AddWithValue("@TaiKhoan", txtTaiKhoan.Text);
                cmd.Parameters.AddWithValue("@MatKhau", txtMatKhau.Text);

                // Gộp quyền từ checkbox
                string quyen = "";
                if (chkQuanTri.Checked) quyen += "Admin,";
                if (chkBanHang.Checked) quyen += "NhanVien,";
                if (chkKho.Checked) quyen += "QuanLy,";
                quyen = quyen.TrimEnd(',');
                cmd.Parameters.AddWithValue("@Quyen", quyen);

                conn.Open();
                object result = cmd.ExecuteScalar(); // Lấy MaNV vừa thêm
                conn.Close();

                if (result != null)
                {
                    int newMaNV = Convert.ToInt32(result);
                    MessageBox.Show("Thêm nhân viên thành công!");

                    // Hiển thị lại danh sách nhân viên
                    HienThiDanhSachNhanVien();

                    // Chọn dòng mới thêm trong ListView
                    foreach (ListViewItem item in listViewNhanVien.Items)
                    {
                        if (item.SubItems[0].Text == newMaNV.ToString())
                        {
                            item.Selected = true;
                            item.EnsureVisible(); // Cuộn đến dòng được chọn
                            listViewNhanVien.Select(); // Focus vào ListView

                            // Gọi sự kiện chọn để hiển thị lên các textbox
                            listViewNhanVien_SelectedIndexChanged(null, null);
                            break;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Thêm nhân viên thất bại!");
                }
            }
        }
    }
}
