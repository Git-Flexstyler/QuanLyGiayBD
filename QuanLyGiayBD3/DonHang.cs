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
    public partial class DonHang : Form
    {
        public DonHang()
        {
            InitializeComponent();
            CauHinhListView();
            HienthiDonHang();
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

        private void DonHang_Load(object sender, EventArgs e)
        {
            //InitializeComponent();
        }
        private void CauHinhListView()
        {
            listViewDonHang.View = View.Details;
            listViewDonHang.FullRowSelect = true;
            listViewDonHang.GridLines = true;

            listViewDonHang.Columns.Clear();
            listViewDonHang.Columns.Add("Mã đơn hàng", 120);
            listViewDonHang.Columns.Add("Ngày bán", 120);
            listViewDonHang.Columns.Add("Số điện thoại", 120);
            listViewDonHang.Columns.Add("Mã nhân viên", 120);
            listViewDonHang.Columns.Add("Tổng tiền", 120);
        }
        private void HienthiDonHang()
        {
            try
            {
                Moketnoi();

                string query = "SELECT * FROM DonHang ";
                SqlCommand cmd = new SqlCommand(query, conec);
                SqlDataReader reader = cmd.ExecuteReader();

                listViewDonHang.Items.Clear();

                while (reader.Read())
                {
                    ListViewItem item = new ListViewItem(reader["MaDonHang"].ToString());
                    item.SubItems.Add(Convert.ToDateTime(reader["NgayBan"]).ToString("dd/MM/yyyy"));
                    item.SubItems.Add(reader["SoDienThoai"].ToString());
                    item.SubItems.Add(reader["MaNV"].ToString());
                    item.SubItems.Add(reader["TongTien"].ToString());

                    listViewDonHang.Items.Add(item);
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

        private void listViewDonHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewDonHang.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = listViewDonHang.SelectedItems[0];

                txtMaDonHang.Text = selectedItem.SubItems[0].Text;
                dtpNgayBan.Value = DateTime.ParseExact(selectedItem.SubItems[1].Text, "dd/MM/yyyy", null);
                txtSoDienThoai.Text = selectedItem.SubItems[2].Text;
                txtMaNhanVien.Text = selectedItem.SubItems[3].Text;
                txtTongTien.Text = selectedItem.SubItems[4].Text;
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtMaDonHang.Text = null;
            dtpNgayBan.Text = null;
            txtSoDienThoai.Text = null;
            txtMaNhanVien.Text = null;
            txtTongTien.Text = null;
            HienthiDonHang();
        }

        private void btnHoaDon_Click(object sender, EventArgs e)
        {
            string maDonHang = txtMaDonHang.Text.Trim();

            if (string.IsNullOrEmpty(maDonHang))
            {
                MessageBox.Show("Vui lòng chọn hoặc nhập Mã đơn hàng.");
                return;
            }

            HoaDon formhoadon = new HoaDon(maDonHang);
            formhoadon.ShowDialog();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                Moketnoi();
                string query = "INSERT INTO DonHang ( NgayBan, SoDienThoai, MaNV, TongTien) VALUES ( @NgayBan, @SoDienThoai, @MaNV, @TongTien)";
                SqlCommand cmd = new SqlCommand(query, conec);

                cmd.Parameters.AddWithValue("@NgayBan", dtpNgayBan.Value);
                cmd.Parameters.AddWithValue("@SoDienThoai", txtSoDienThoai.Text);
                cmd.Parameters.AddWithValue("@MaNV", txtMaNhanVien.Text);
                cmd.Parameters.AddWithValue("@TongTien", txtTongTien.Text);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Thêm thành công!");
                HienthiDonHang();
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
            string maDonHang = txtMaDonHang.Text.Trim();

            if (string.IsNullOrEmpty(maDonHang))
            {
                MessageBox.Show("Vui lòng chọn một đơn hàng để sửa.");
                return;
            }

            try
            {
                Moketnoi();

                // Lấy thông tin cũ từ cơ sở dữ liệu
                string querySelect = "SELECT NgayBan, SoDienThoai, MaNV, TongTien FROM DonHang WHERE MaDonHang = @MaDonHang";
                SqlCommand cmdSelect = new SqlCommand(querySelect, conec);
                cmdSelect.Parameters.AddWithValue("@MaDonHang", maDonHang);
                SqlDataReader reader = cmdSelect.ExecuteReader();

                if (!reader.Read())
                {
                    reader.Close();
                    MessageBox.Show("Không tìm thấy đơn hàng cần sửa.");
                    return;
                }

                // Lấy dữ liệu từ DB
                DateTime ngayBanOld = Convert.ToDateTime(reader["NgayBan"]);
                string soDienThoaiOld = reader["SoDienThoai"].ToString();
                string maNVOld = reader["MaNV"].ToString();
                string tongTienOld = reader["TongTien"].ToString();
                reader.Close();

                // Lấy dữ liệu mới từ form
                DateTime ngayBanNew = dtpNgayBan.Value.Date;
                string soDienThoaiNew = txtSoDienThoai.Text.Trim();
                string maNVNew = txtMaNhanVien.Text.Trim();
                string tongTienNew = txtTongTien.Text.Trim();

                // So sánh
                if (ngayBanOld.Date == ngayBanNew &&
                    soDienThoaiOld == soDienThoaiNew &&
                    maNVOld == maNVNew &&
                    tongTienOld == tongTienNew)
                {
                    MessageBox.Show("Thông tin không thay đổi.");
                    return;
                }

                // Nếu có thay đổi thì tiến hành cập nhật
                string queryUpdate = "UPDATE DonHang SET NgayBan = @NgayBan,SoDienThoai = @SoDienThoai, MaNV = @MaNV, TongTien = @TongTien WHERE MaDonHang = @MaDonHang";
                SqlCommand cmdUpdate = new SqlCommand(queryUpdate, conec);
                cmdUpdate.Parameters.AddWithValue("@NgayBan", ngayBanNew);
                cmdUpdate.Parameters.AddWithValue("@MaKH", soDienThoaiNew);
                cmdUpdate.Parameters.AddWithValue("@MaNV", maNVNew);
                cmdUpdate.Parameters.AddWithValue("@TongTien", tongTienNew);
                cmdUpdate.Parameters.AddWithValue("@MaDonHang", maDonHang);

                int rowsAffected = cmdUpdate.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Sửa thông tin đơn hàng thành công!");
                    HienthiDonHang(); // Refresh lại dữ liệu
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
            if (string.IsNullOrWhiteSpace(txtMaDonHang.Text))
            {
                MessageBox.Show("Vui lòng nhập Mã Đơn Hàng cần xóa.");
                return;
            }

            DialogResult result = MessageBox.Show("Bạn có chắc muốn xóa?", "Xác nhận xóa", MessageBoxButtons.YesNo);
            if (result == DialogResult.No) return;

            try
            {
                Moketnoi();
                string query = "DELETE FROM DonHang WHERE MaDonHang = @MaDonHang";
                SqlCommand cmd = new SqlCommand(query, conec);
                cmd.Parameters.AddWithValue("@MaDonHang", txtMaDonHang.Text);

                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Xóa thành công!");
                    HienthiDonHang(); // refresh lại danh sách
                }
                else
                {
                    MessageBox.Show("Không tìm thấy Mã Đơn Hàng cần xóa.");
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

                // Truy vấn tất cả đơn hàng
                string query = "SELECT MaDonHang, NgayBan, SoDienThoai, MaNV, TongTien FROM DonHang";
                SqlCommand cmd = new SqlCommand(query, conec);
                SqlDataReader reader = cmd.ExecuteReader();

                listViewDonHang.Items.Clear();

                string maDonHangSearch = txtMaDonHang.Text.Trim().ToLower();
                string soDienThoaiSearch = txtSoDienThoai.Text.Trim().ToLower();
                string maNVSearch = txtMaNhanVien.Text.Trim().ToLower();
                string tongTienSearch = txtTongTien.Text.Trim();
                bool coNgayBan = dtpNgayBan.Checked;
                DateTime ngaySearch = dtpNgayBan.Value.Date;

                while (reader.Read())
                {
                    string maDonHang = reader["MaDonHang"].ToString();
                    string soDienThoai = reader["SoDienThoai"].ToString();
                    string maNV = reader["MaNV"].ToString();
                    string tongTien = reader["TongTien"].ToString();
                    DateTime ngayBan = Convert.ToDateTime(reader["NgayBan"]);

                    bool isMatch = true;

                    // Kiểm tra từng điều kiện gần đúng
                    if (!string.IsNullOrEmpty(maDonHangSearch) && !maDonHang.ToLower().Contains(maDonHangSearch))
                        isMatch = false;
                    if (!string.IsNullOrEmpty(soDienThoaiSearch) && !soDienThoai.ToLower().Contains(soDienThoaiSearch))
                        isMatch = false;
                    if (!string.IsNullOrEmpty(maNVSearch) && !maNV.ToLower().Contains(maNVSearch))
                        isMatch = false;
                    if (!string.IsNullOrEmpty(tongTienSearch) && !tongTien.Contains(tongTienSearch))
                        isMatch = false;
                    if (coNgayBan && (ngayBan.Date < ngaySearch || ngayBan.Date >= ngaySearch.AddDays(1)))
                        isMatch = false;

                    // Tạo item
                    ListViewItem item = new ListViewItem(maDonHang);
                    item.SubItems.Add(ngayBan.ToString("dd/MM/yyyy"));
                    item.SubItems.Add(soDienThoai);
                    item.SubItems.Add(maNV);
                    item.SubItems.Add(tongTien);

                    // Nếu khớp thì đưa lên đầu và bôi xanh
                    if (isMatch)
                    {
                        item.BackColor = Color.LightGreen;
                        listViewDonHang.Items.Insert(0, item);
                    }
                    else
                    {
                        listViewDonHang.Items.Add(item);
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

        private void btnChiTietDonHang_Click(object sender, EventArgs e)
        {
            string maDonHang = txtMaDonHang.Text.Trim();

            if (string.IsNullOrEmpty(maDonHang))
            {
                MessageBox.Show("Vui lòng chọn hoặc nhập Mã đơn hàng.");
                return;
            }

            ChiTietDonHang formChiTiet = new ChiTietDonHang(maDonHang);
            formChiTiet.ShowDialog();
        }
        private void txtMaDonHang_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtSoDienThoai.Focus();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }
        private void txtMaKhachHang_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtMaNhanVien.Focus();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }
        private void txtMaNhanVien_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtTongTien.Focus();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }
        private void txtTongTien_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtMaDonHang.Focus();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }
    }
}
