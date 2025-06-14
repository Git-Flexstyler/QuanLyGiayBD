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
    public partial class FormKhachHang : Form
    {
        string connectionString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=QuanLyGiayTheThao;Integrated Security=True;TrustServerCertificate=True";
        public FormKhachHang()
        {
            InitializeComponent();
        }

        private void FormKhachHang_Load(object sender, EventArgs e)
        {
            LoadLoaiKhachHang();
            HienThiDanhSachKhachHang();
        }
        private void LoadLoaiKhachHang()
        {
            cboLoaiKH.Items.Clear();
            cboLoaiKH.Items.Add("Thường");
            cboLoaiKH.Items.Add("VIP");
            cboLoaiKH.SelectedIndex = -1; // Gán mặc định sau khi đã có Items
        }

        void HienThiDanhSachKhachHang()
        {
            listViewKhachHang.Items.Clear();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM KhachHang";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ListViewItem item = new ListViewItem(reader["MaKH"].ToString());
                    item.SubItems.Add(reader["TenKH"].ToString());
                    item.SubItems.Add(reader["SoDienThoai"].ToString());
                    item.SubItems.Add(reader["DiaChi"].ToString());
                    item.SubItems.Add(reader["TongTien"].ToString());
                    item.SubItems.Add(reader["LoaiKH"].ToString());
                    listViewKhachHang.Items.Add(item);
                }
                reader.Close();
            }
        }

        private void LoadDanhSachKhachHang()
        {
            listViewKhachHang.Items.Clear();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM KhachHang ORDER BY MaKH";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ListViewItem item = new ListViewItem(reader["MaKH"].ToString());
                    item.SubItems.Add(reader["TenKH"].ToString());
                    item.SubItems.Add(reader["SoDienThoai"].ToString());
                    item.SubItems.Add(reader["DiaChi"].ToString());
                    item.SubItems.Add(reader["TongTien"].ToString());
                    item.SubItems.Add(reader["LoaiKH"].ToString());

                    item.BackColor = Color.White; // Đảm bảo không còn màu tìm kiếm
                    listViewKhachHang.Items.Add(item);
                }

                reader.Close();
                conn.Close();
            }
        }

        private void listViewKhachHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewKhachHang.SelectedItems.Count > 0)
            {
                ListViewItem item = listViewKhachHang.SelectedItems[0];
                txtMaKH.Text = item.SubItems[0].Text;
                txtTenKH.Text = item.SubItems[1].Text;
                txtSDT.Text = item.SubItems[2].Text;
                txtDiaChi.Text = item.SubItems[3].Text;
                txtTongTien.Text = item.SubItems[4].Text;
                cboLoaiKH.Text = item.SubItems[5].Text;
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtMaKH.Clear();
            txtTenKH.Clear();
            txtSDT.Clear();
            txtDiaChi.Clear();
            txtTongTien.Clear();
            cboLoaiKH.SelectedIndex = -1;
            LoadDanhSachKhachHang();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string ten = txtTenKH.Text.Trim();
            string sdt = txtSDT.Text.Trim();
            string diaChi = txtDiaChi.Text.Trim();
            string loaiKH = cboLoaiKH.Text;

            if (ten == "" || sdt == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"INSERT INTO KhachHang (TenKH, SoDienThoai, DiaChi, LoaiKH)
                                 VALUES (@TenKH, @SDT, @DiaChi, @LoaiKH);
                                 SELECT SCOPE_IDENTITY();";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@TenKH", ten);
                cmd.Parameters.AddWithValue("@SDT", sdt);
                cmd.Parameters.AddWithValue("@DiaChi", diaChi);
                cmd.Parameters.AddWithValue("@LoaiKH", loaiKH);

                conn.Open();
                int newID = Convert.ToInt32(cmd.ExecuteScalar());

                ListViewItem item = new ListViewItem(newID.ToString());
                item.SubItems.Add(ten);
                item.SubItems.Add(sdt);
                item.SubItems.Add(diaChi);
                item.SubItems.Add("0"); // Mặc định
                item.SubItems.Add(loaiKH);
                listViewKhachHang.Items.Add(item);

                MessageBox.Show("Thêm khách hàng thành công!");
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (txtMaKH.Text == "")
            {
                MessageBox.Show("Vui lòng chọn khách hàng để sửa.");
                return;
            }

            int maKH = int.Parse(txtMaKH.Text.Trim());
            string ten = txtTenKH.Text.Trim();
            string sdt = txtSDT.Text.Trim();
            string diaChi = txtDiaChi.Text.Trim();
            string loaiKH = cboLoaiKH.Text;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"UPDATE KhachHang 
                                 SET TenKH = @TenKH, SoDienThoai = @SDT, DiaChi = @DiaChi, LoaiKH = @LoaiKH 
                                 WHERE MaKH = @MaKH";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@TenKH", ten);
                cmd.Parameters.AddWithValue("@SDT", sdt);
                cmd.Parameters.AddWithValue("@DiaChi", diaChi);
                cmd.Parameters.AddWithValue("@LoaiKH", loaiKH);
                cmd.Parameters.AddWithValue("@MaKH", maKH);

                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    MessageBox.Show("Sửa thông tin khách hàng thành công!");
                    HienThiDanhSachKhachHang();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy khách hàng để sửa.");
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string ten = txtTenKH.Text.Trim();
            string sdt = txtSDT.Text.Trim();
            string diaChi = txtDiaChi.Text.Trim();
            string loaiKH = cboLoaiKH.Text;

            if (ten == "" || sdt == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"INSERT INTO KhachHang (TenKH, SoDienThoai, DiaChi, LoaiKH)
                                 VALUES (@TenKH, @SDT, @DiaChi, @LoaiKH);
                                 SELECT SCOPE_IDENTITY();";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@TenKH", ten);
                cmd.Parameters.AddWithValue("@SDT", sdt);
                cmd.Parameters.AddWithValue("@DiaChi", diaChi);
                cmd.Parameters.AddWithValue("@LoaiKH", loaiKH);

                conn.Open();
                int newID = Convert.ToInt32(cmd.ExecuteScalar());

                ListViewItem item = new ListViewItem(newID.ToString());
                item.SubItems.Add(ten);
                item.SubItems.Add(sdt);
                item.SubItems.Add(diaChi);
                item.SubItems.Add("0"); // Mặc định
                item.SubItems.Add(loaiKH);
                listViewKhachHang.Items.Add(item);

                MessageBox.Show("Thêm khách hàng thành công!");
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string tenKH = txtTenKH.Text.Trim().ToLower();
            string sdt = txtSDT.Text.Trim();
            string diaChi = txtDiaChi.Text.Trim().ToLower();
            string loaiKH = cboLoaiKH.SelectedItem != null ? cboLoaiKH.SelectedItem.ToString().ToLower() : "";

            List<ListViewItem> ketQuaTimKiem = new List<ListViewItem>();
            List<ListViewItem> khongKhop = new List<ListViewItem>();

            foreach (ListViewItem item in listViewKhachHang.Items)
            {
                string itemTen = item.SubItems[1].Text.ToLower();
                string itemSDT = item.SubItems[2].Text;
                string itemDiaChi = item.SubItems[3].Text.ToLower();
                string itemLoai = item.SubItems[5].Text.ToLower();

                bool match = true;

                if (!string.IsNullOrEmpty(tenKH) && !itemTen.Contains(tenKH))
                    match = false;
                if (!string.IsNullOrEmpty(sdt) && !itemSDT.Contains(sdt))
                    match = false;
                if (!string.IsNullOrEmpty(diaChi) && !itemDiaChi.Contains(diaChi))
                    match = false;
                if (!string.IsNullOrEmpty(loaiKH) && itemLoai != loaiKH)
                    match = false;

                if (match)
                {
                    item.BackColor = Color.LightBlue;  // tô màu khách hàng khớp
                    ketQuaTimKiem.Add((ListViewItem)item.Clone());
                }
                else
                {
                    item.BackColor = Color.White;
                    khongKhop.Add((ListViewItem)item.Clone());
                }
            }

            listViewKhachHang.Items.Clear();
            listViewKhachHang.Items.AddRange(ketQuaTimKiem.ToArray());
            listViewKhachHang.Items.AddRange(khongKhop.ToArray());
        }
    }
}
