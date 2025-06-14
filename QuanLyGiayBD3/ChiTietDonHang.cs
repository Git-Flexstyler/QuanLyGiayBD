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
    public partial class ChiTietDonHang : Form
    {
        public ChiTietDonHang()
        {
            InitializeComponent();
        }
        SqlConnection conec = null;
        public string connectionString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=QuanLyGiayTheThao;Integrated Security=True;TrustServerCertificate=True";
        private void Moketnoi()
        {
            if (conec == null)
            {
                conec = new SqlConnection(connectionString);
            }
            if (conec.State == ConnectionState.Closed)
            {
                conec.Open();
            }
        }
        private void DongKetNoi()
        {
            if (conec != null && conec.State == ConnectionState.Open)
            {
                conec.Close();
            }
        }
        private void TestKetNoi()
        {
            try
            {
                Moketnoi();
                MessageBox.Show("Kết nối thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối: " + ex.Message);
            }
        }
        public ChiTietDonHang(string maDonHang)
        {
            InitializeComponent();
            txtMaDonHang.Text = maDonHang;
            CauHinhListView();
            HienthiDonHang(maDonHang);
        }
        private void CauHinhListView()
        {
            listViewChiTietDonHang.View = View.Details;
            listViewChiTietDonHang.FullRowSelect = true;
            listViewChiTietDonHang.GridLines = true;

            listViewChiTietDonHang.Columns.Clear();
            listViewChiTietDonHang.Columns.Add("Mã đơn hàng", 120);
            listViewChiTietDonHang.Columns.Add("Mã sản phẩm", 120);
            listViewChiTietDonHang.Columns.Add("Size", 120);
            listViewChiTietDonHang.Columns.Add("Số lượng", 120);
            listViewChiTietDonHang.Columns.Add("Đơn giá", 120);
        }
        private void HienthiDonHang(string maDonHang)
        {
            try
            {
                Moketnoi();

                string query = "SELECT * FROM ChiTietDonHang where MaDonHang = @MaDonHang";
                SqlCommand cmd = new SqlCommand(query, conec);
                cmd.Parameters.AddWithValue("@MaDonHang", txtMaDonHang.Text);
                SqlDataReader reader = cmd.ExecuteReader();

                listViewChiTietDonHang.Items.Clear();

                while (reader.Read())
                {
                    ListViewItem item = new ListViewItem(reader["MaDonHang"].ToString());
                    item.SubItems.Add(reader["MaSP"].ToString());
                    item.SubItems.Add(reader["Size"].ToString());
                    item.SubItems.Add(reader["SoLuong"].ToString());
                    item.SubItems.Add(reader["DonGia"].ToString());

                    listViewChiTietDonHang.Items.Add(item);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hiển thị đơn hàng: " + ex.Message);
            }
            finally
            {
                DongKetNoi();
            }
        }

        private void listViewChiTietDonHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewChiTietDonHang.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = listViewChiTietDonHang.SelectedItems[0];

                txtMaDonHang.Text = selectedItem.SubItems[0].Text;
                txtMaSanPham.Text = selectedItem.SubItems[1].Text;
                txtSize.Text = selectedItem.SubItems[2].Text;
                txtSoLuong.Text = selectedItem.SubItems[3].Text;
                txtDonGia.Text = selectedItem.SubItems[4].Text;
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtMaSanPham.Text = null;
            txtSize.Text = null;
            txtSoLuong.Text = null;
            txtDonGia.Text = null;
            HienthiDonHang(txtMaDonHang.Text);
        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                Moketnoi();
                string query = "INSERT INTO ChiTietDonHang ( MaDonHang,MaSP, Size, SoLuong, DonGia) VALUES ( @MaDonHang,@MaSP, @Size, @SoLuong, @DonGia)";
                SqlCommand cmd = new SqlCommand(query, conec);

                cmd.Parameters.AddWithValue("@MaDonHang", txtMaDonHang.Text);
                cmd.Parameters.AddWithValue("@MaSP", txtMaSanPham.Text);
                cmd.Parameters.AddWithValue("@Size", txtSize.Text);
                cmd.Parameters.AddWithValue("@SoLuong", txtSoLuong.Text);
                cmd.Parameters.AddWithValue("@DonGia", txtDonGia.Text);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Thêm thành công!");
                HienthiDonHang(txtMaDonHang.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thêm: " + ex.Message);
            }
            finally
            {
                DongKetNoi();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string maSanPham = txtMaSanPham.Text.Trim();

            if (string.IsNullOrEmpty(maSanPham))
            {
                MessageBox.Show("Vui lòng chọn một sản phẩm để sửa.");
                return;
            }

            try
            {
                Moketnoi();

                // Lấy thông tin cũ từ cơ sở dữ liệu
                string querySelect = "SELECT MaSP, Size, SoLuong, DonGia FROM ChiTietDonHang WHERE MaDonHang = @MaDonHang and MaSP = @MaSp";
                SqlCommand cmdSelect = new SqlCommand(querySelect, conec);
                cmdSelect.Parameters.AddWithValue("MaDonHang", txtMaDonHang.Text);
                cmdSelect.Parameters.AddWithValue("@MaSP", txtMaSanPham.Text);
                SqlDataReader reader = cmdSelect.ExecuteReader();

                if (!reader.Read())
                {
                    reader.Close();
                    MessageBox.Show("Không tìm thấy sản phẩm cần sửa.");
                    return;
                }

                // Lấy dữ liệu từ DB
                string SizeOld = reader["Size"].ToString();
                string SoLuongOld = reader["SoLuong"].ToString();
                string DonGiaOld = reader["DonGia"].ToString();
                reader.Close();

                // Lấy dữ liệu mới từ form
                string SizeNew = txtSize.Text.Trim();
                string SoLuongNew = txtSoLuong.Text.Trim();
                string DonGiaNew = txtDonGia.Text.Trim();

                // So sánh
                if (SizeOld == SizeNew &&
                    SoLuongOld == SoLuongNew &&
                    DonGiaOld == DonGiaNew)
                {
                    MessageBox.Show("Thông tin không thay đổi.");
                    return;
                }

                // Nếu có thay đổi thì tiến hành cập nhật
                string queryUpdate = "UPDATE ChiTietDonHang SET Size = @Size,SoLuong = @SoLuong, DonGia = @DonGia WHERE MaDonHang = @MaDonHang and MaSP = @MaSP";
                SqlCommand cmdUpdate = new SqlCommand(queryUpdate, conec);
                cmdUpdate.Parameters.AddWithValue("@Size", SizeNew);
                cmdUpdate.Parameters.AddWithValue("@SoLuong", SoLuongNew);
                cmdUpdate.Parameters.AddWithValue("@DonGia", DonGiaNew);
                cmdUpdate.Parameters.AddWithValue("@MaDonHang", txtMaDonHang.Text);
                cmdUpdate.Parameters.AddWithValue("@MaSP", txtMaSanPham.Text);

                int rowsAffected = cmdUpdate.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Sửa thông tin đơn hàng thành công!");
                    HienthiDonHang(txtMaDonHang.Text); // Refresh lại dữ liệu
                }
                else
                {
                    MessageBox.Show("Không có bản ghi nào được cập nhật.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi sửa: " + ex.Message);
            }
            finally
            {
                DongKetNoi();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaSanPham.Text))
            {
                MessageBox.Show("Vui lòng nhập Mã sản phẩm cần xóa.");
                return;
            }

            DialogResult result = MessageBox.Show("Bạn có chắc muốn xóa?", "Xác nhận xóa", MessageBoxButtons.YesNo);
            if (result == DialogResult.No) return;

            try
            {
                Moketnoi();
                string query = "DELETE FROM ChiTietDonHang WHERE MaDonHang = @MaDonHang and MaSP = @MaSP";
                SqlCommand cmd = new SqlCommand(query, conec);
                cmd.Parameters.AddWithValue("@MaDonHang", txtMaDonHang.Text);
                cmd.Parameters.AddWithValue("@MaSP", txtMaSanPham.Text);

                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Xóa thành công!");
                    HienthiDonHang(txtMaDonHang.Text); // refresh lại danh sách
                }
                else
                {
                    MessageBox.Show("Không tìm thấy sản phẩm cần xóa.");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xóa: " + ex.Message);
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                Moketnoi();

                string query = "SELECT MaDonHang, MaSP, Size, SoLuong, DonGia FROM ChiTietDonHang where MaDonHang = @MaDonHang";
                SqlCommand cmd = new SqlCommand(query, conec);
                cmd.Parameters.AddWithValue("@MaDonHang", txtMaDonHang.Text);
                SqlDataReader reader = cmd.ExecuteReader();

                listViewChiTietDonHang.Items.Clear();

                string maDH_Search = txtMaDonHang.Text.Trim().ToLower();
                string maSP_Search = txtMaSanPham.Text.Trim().ToLower();
                string size_Search = txtSize.Text.Trim().ToLower();
                string soLuong_Search = txtSoLuong.Text.Trim();
                string donGia_Search = txtDonGia.Text.Trim();

                while (reader.Read())
                {
                    string maDH = reader["MaDonHang"].ToString();
                    string maSP = reader["MaSP"].ToString();
                    string size = reader["Size"].ToString();
                    string soLuong = reader["SoLuong"].ToString();
                    string donGia = reader["DonGia"].ToString();

                    bool isMatch = true;
                    if (!string.IsNullOrEmpty(maDH_Search) && !maDH.ToLower().Contains(maDH_Search))
                        isMatch = false;
                    if (!string.IsNullOrEmpty(maSP_Search) && !maSP.ToLower().Contains(maSP_Search))
                        isMatch = false;
                    if (!string.IsNullOrEmpty(size_Search) && !size.ToLower().Contains(size_Search))
                        isMatch = false;
                    if (!string.IsNullOrEmpty(soLuong_Search) && !soLuong.Contains(soLuong_Search))
                        isMatch = false;
                    if (!string.IsNullOrEmpty(donGia_Search) && !donGia.Contains(donGia_Search))
                        isMatch = false;

                    ListViewItem item = new ListViewItem(maDH);
                    item.SubItems.Add(maSP);
                    item.SubItems.Add(size);
                    item.SubItems.Add(soLuong);
                    item.SubItems.Add(donGia);

                    if (isMatch)
                    {
                        item.BackColor = Color.LightGreen;
                        listViewChiTietDonHang.Items.Insert(0, item);
                    }
                    else
                    {
                        listViewChiTietDonHang.Items.Add(item);
                    }
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tìm kiếm: " + ex.Message);
            }
            finally
            {
                DongKetNoi();
            }
        }
        private void txtMaSanPham_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtSize.Focus();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }
        private void txtSize_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtSoLuong.Focus();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }
        private void txtSoLuong_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtDonGia.Focus();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }
        private void txtDonGia_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtMaSanPham.Focus();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }
    }
}
