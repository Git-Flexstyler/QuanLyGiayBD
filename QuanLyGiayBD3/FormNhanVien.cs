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
        private void btnLamMoi_Click_1(object sender, EventArgs e)
        {
            txtMaNV.Clear();
            txtTenNV.Clear();
            txtSDT.Clear();
            txtQueQuan.Clear();
            txtTaiKhoan.Clear();
            txtMatKhau.Clear();

            chkQuanTri.Checked = false;
            chkBanHang.Checked = false;
            chkKho.Checked = false;

            listViewNhanVien.SelectedItems.Clear();

            HienThiDanhSachNhanVien(); // Gọi lại dữ liệu ban đầu
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

                string quyen = item.SubItems[6].Text;
                string[] quyenArr = quyen.Split(';'); // chia chuỗi quyền

                // Reset các checkbox
                chkQuanTri.Checked = false;
                chkBanHang.Checked = false;
                chkKho.Checked = false;

                chkQuanTri.Checked = quyenArr.Contains("Admin");
                chkBanHang.Checked = quyenArr.Contains("BanHang");
                chkKho.Checked = quyenArr.Contains("QuanLyKho");

            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string tenNV = txtTenNV.Text.Trim();
            string sdt = txtSDT.Text.Trim();
            string queQuan = txtQueQuan.Text.Trim();
            string taiKhoan = txtTaiKhoan.Text.Trim();
            string matKhau = txtMatKhau.Text.Trim();

            List<string> danhSachQuyen = new List<string>();
            if (chkQuanTri.Checked) danhSachQuyen.Add("Admin");
            if (chkBanHang.Checked) danhSachQuyen.Add("BanHang");
            if (chkKho.Checked) danhSachQuyen.Add("QuanLyKho");

            string quyen = string.Join(";", danhSachQuyen);

            if (tenNV == "" || taiKhoan == "" || matKhau == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                return;
            }

            string connStr = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=QuanLyGiayTheThao;Integrated Security=True;TrustServerCertificate=True";
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                // 1. Kiểm tra tài khoản đã tồn tại chưa
                string checkQuery = "SELECT COUNT(*) FROM NhanVien WHERE TaiKhoan = @tk";
                SqlCommand checkCmd = new SqlCommand(checkQuery, conn);
                checkCmd.Parameters.AddWithValue("@tk", taiKhoan);

                int exist = (int)checkCmd.ExecuteScalar();
                if (exist > 0)
                {
                    MessageBox.Show("Tài khoản đã tồn tại. Vui lòng nhập tài khoản khác.");
                    return;
                }

                // 2. Thêm nhân viên nếu không trùng
                string insertQuery = @"
            INSERT INTO NhanVien (TenNV, SoDienThoai, QueQuan, Email, TaiKhoan, MatKhau, Quyen)
            VALUES (@TenNV, @SDT, @QueQuan, '', @TaiKhoan, @MatKhau, @Quyen);
            SELECT SCOPE_IDENTITY();";

                SqlCommand cmd = new SqlCommand(insertQuery, conn);
                cmd.Parameters.AddWithValue("@TenNV", tenNV);
                cmd.Parameters.AddWithValue("@SDT", sdt);
                cmd.Parameters.AddWithValue("@QueQuan", queQuan);
                cmd.Parameters.AddWithValue("@TaiKhoan", taiKhoan);
                cmd.Parameters.AddWithValue("@MatKhau", matKhau);
                cmd.Parameters.AddWithValue("@Quyen", quyen);

                int newMaNV = Convert.ToInt32(cmd.ExecuteScalar());

                // Thêm lên ListView (ví dụ)
                ListViewItem item = new ListViewItem(newMaNV.ToString());
                item.SubItems.Add(tenNV);
                item.SubItems.Add(sdt);
                item.SubItems.Add(queQuan);
                item.SubItems.Add(taiKhoan);
                item.SubItems.Add(matKhau);
                item.SubItems.Add(quyen);
                listViewNhanVien.Items.Add(item);
                item.Selected = true;

                MessageBox.Show("Thêm nhân viên thành công!");
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtMaNV.Text == "")
            {
                MessageBox.Show("Vui lòng chọn nhân viên để xóa!");
                return;
            }

            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa nhân viên này không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM NhanVien WHERE MaNV = @MaNV";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaNV", txtMaNV.Text);

                    try
                    {
                        conn.Open();
                        int rows = cmd.ExecuteNonQuery();
                        conn.Close();

                        if (rows > 0)
                        {
                            MessageBox.Show("Xóa nhân viên thành công!");
                            HienThiDanhSachNhanVien();
                            btnLamMoi.PerformClick(); // Gọi lại nút làm mới để xóa nội dung TextBox
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy nhân viên để xóa.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi xóa: " + ex.Message);
                    }
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (listViewNhanVien.SelectedItems.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một nhân viên để sửa.");
                return;
            }

            int maNV = int.Parse(txtMaNV.Text.Trim());
            string tenNV = txtTenNV.Text.Trim();
            string sdt = txtSDT.Text.Trim();
            string queQuan = txtQueQuan.Text.Trim();
            string taiKhoan = txtTaiKhoan.Text.Trim();
            string matKhau = txtMatKhau.Text.Trim();

            List<string> danhSachQuyen = new List<string>();
            if (chkQuanTri.Checked) danhSachQuyen.Add("Admin");
            if (chkBanHang.Checked) danhSachQuyen.Add("BanHang");
            if (chkKho.Checked) danhSachQuyen.Add("QuanLyKho");

            string quyen = string.Join(";", danhSachQuyen);

            string connStr = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=QuanLyGiayTheThao;Integrated Security=True;TrustServerCertificate=True";

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string sql = @"
            UPDATE NhanVien 
            SET TenNV = @TenNV, SoDienThoai = @SDT, QueQuan = @QueQuan, TaiKhoan = @TaiKhoan, MatKhau = @MatKhau, Quyen = @Quyen 
            WHERE MaNV = @MaNV";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@TenNV", tenNV);
                cmd.Parameters.AddWithValue("@SDT", sdt);
                cmd.Parameters.AddWithValue("@QueQuan", queQuan);
                cmd.Parameters.AddWithValue("@TaiKhoan", taiKhoan);
                cmd.Parameters.AddWithValue("@MatKhau", matKhau);
                cmd.Parameters.AddWithValue("@Quyen", quyen);
                cmd.Parameters.AddWithValue("@MaNV", maNV);

                try
                {
                    conn.Open();
                    int kq = cmd.ExecuteNonQuery();
                    if (kq > 0)
                    {
                        MessageBox.Show("Sửa thông tin nhân viên thành công.");
                        HienThiDanhSachNhanVien(); // Cập nhật lại listview
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy nhân viên để sửa.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string ten = txtTenNV.Text.Trim().ToLower();
            string sdt = txtSDT.Text.Trim();
            string que = txtQueQuan.Text.Trim().ToLower();
            string tk = txtTaiKhoan.Text.Trim().ToLower();

            List<ListViewItem> ketQua = new List<ListViewItem>();
            List<ListViewItem> khongKetQua = new List<ListViewItem>();

            foreach (ListViewItem item in listViewNhanVien.Items)
            {
                string tenNV = item.SubItems[1].Text.ToLower();
                string sdtNV = item.SubItems[2].Text;
                string queQuan = item.SubItems[3].Text.ToLower();
                string taiKhoan = item.SubItems[4].Text.ToLower();

                bool match = true;

                if (!string.IsNullOrEmpty(ten) && !tenNV.Contains(ten))
                    match = false;
                if (!string.IsNullOrEmpty(sdt) && !sdtNV.Contains(sdt))
                    match = false;
                if (!string.IsNullOrEmpty(que) && !queQuan.Contains(que))
                    match = false;
                if (!string.IsNullOrEmpty(tk) && !taiKhoan.Contains(tk))
                    match = false;

                if (match)
                {
                    item.BackColor = Color.LightYellow; // Tô màu cho dòng khớp
                    ketQua.Add((ListViewItem)item.Clone());
                }
                else
                {
                    item.BackColor = Color.White; // Reset màu cho dòng không khớp
                    khongKetQua.Add((ListViewItem)item.Clone());
                }
            }

            listViewNhanVien.Items.Clear();
            listViewNhanVien.Items.AddRange(ketQua.ToArray());
            listViewNhanVien.Items.AddRange(khongKetQua.ToArray());
        }
    }
}
