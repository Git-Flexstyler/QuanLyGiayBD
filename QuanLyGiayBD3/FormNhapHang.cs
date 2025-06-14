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
    public partial class FormNhapHang : Form
    {
        public FormNhapHang()
        {
            InitializeComponent();
            CauHinhListView();
            HienthiPhieuNhap();

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
        private void CauHinhListView()
        {
            listViewNhapHang.View = View.Details;
            listViewNhapHang.FullRowSelect = true;
            listViewNhapHang.GridLines = true;

            listViewNhapHang.Columns.Clear();
            listViewNhapHang.Columns.Add("Mã phiếu nhập", 120);
            listViewNhapHang.Columns.Add("Ngày nhập", 120);
            listViewNhapHang.Columns.Add("Người nhập", 120);
            listViewNhapHang.Columns.Add("Ghi chú", 120);

        }
        private void HienthiPhieuNhap()
        {
            try
            {
                Moketnoi();

                string query = "SELECT * FROM NhapHang ";
                SqlCommand cmd = new SqlCommand(query, conec);
                SqlDataReader reader = cmd.ExecuteReader();

                listViewNhapHang.Items.Clear();

                while (reader.Read())
                {
                    ListViewItem item = new ListViewItem(reader["MaPhieuNhap"].ToString());
                    item.SubItems.Add(Convert.ToDateTime(reader["NgayNhap"]).ToString("dd/MM/yyyy"));
                    item.SubItems.Add(reader["NguoiNhap"].ToString());
                    item.SubItems.Add(reader["GhiChu"].ToString());


                    listViewNhapHang.Items.Add(item);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hiển thị phiếu nhập: " + ex.Message);
            }
            finally
            {
                DongKetNoi();
            }
        }

        private void listViewNhapHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (listViewNhapHang.SelectedItems.Count > 0)
                {
                    ListViewItem selectedItem = listViewNhapHang.SelectedItems[0];

                    txtMaPhieuNhap.Text = selectedItem.SubItems[0].Text;
                    dtpNgayNhap.Value = DateTime.ParseExact(selectedItem.SubItems[1].Text, "dd/MM/yyyy", null);
                    txtNguoiNhap.Text = selectedItem.SubItems[2].Text;
                    txtGhiChu.Text = selectedItem.SubItems[3].Text;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi chọn phiếu nhập: " + ex.Message);
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtMaPhieuNhap.Text = null;
            dtpNgayNhap.Text = null;
            txtNguoiNhap.Text = null;
            txtGhiChu.Text = null;
            HienthiPhieuNhap();
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
                string query = "INSERT INTO NhapHang ( NgayNhap, NguoiNhap, GhiChu) VALUES ( @NgayNhap, @NguoiNhap, @GhiChu)";
                SqlCommand cmd = new SqlCommand(query, conec);

                cmd.Parameters.AddWithValue("@NgayBan", dtpNgayNhap.Value);
                cmd.Parameters.AddWithValue("@MaKH", txtNguoiNhap.Text);
                cmd.Parameters.AddWithValue("@MaNV", txtGhiChu.Text);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Thêm thành công!");
                HienthiPhieuNhap();
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
            string maPhieuNhap = txtMaPhieuNhap.Text.Trim();

            if (string.IsNullOrEmpty(maPhieuNhap))
            {
                MessageBox.Show("Vui lòng chọn một phiếu nhập để sửa.");
                return;
            }

            try
            {
                Moketnoi();

                // Lấy thông tin cũ từ cơ sở dữ liệu
                string querySelect = "SELECT NgayNhap, NguoiNhap, GhiChu FROM NhapHang WHERE MaPhieuNhap = @MaPhieuNhap";
                SqlCommand cmdSelect = new SqlCommand(querySelect, conec);
                cmdSelect.Parameters.AddWithValue("@MaDonHang", maPhieuNhap);
                SqlDataReader reader = cmdSelect.ExecuteReader();

                if (!reader.Read())
                {
                    reader.Close();
                    MessageBox.Show("Không tìm thấy phiếu nhập cần sửa.");
                    return;
                }

                // Lấy dữ liệu từ DB
                DateTime ngayNhapOld = Convert.ToDateTime(reader["NgayNhap"]);
                string nguoiNhapOld = reader["NguoiNhap"].ToString();
                string ghiChuOld = reader["GhiChu"].ToString();
                reader.Close();

                // Lấy dữ liệu mới từ form
                DateTime ngayNhapNew = dtpNgayNhap.Value.Date;
                string nguoiNhapNew = txtNguoiNhap.Text.Trim();
                string ghiChuNew = txtGhiChu.Text.Trim();


                // So sánh
                if (ngayNhapOld.Date == ngayNhapNew &&
                    nguoiNhapOld == nguoiNhapNew &&
                    ghiChuOld == ghiChuNew)
                {
                    MessageBox.Show("Thông tin không thay đổi.");
                    return;
                }

                // Nếu có thay đổi thì tiến hành cập nhật
                string queryUpdate = "UPDATE NhapHang SET NgayNhap = @NgayNhap,NguoiNhap = @NguoiNhap, GhiChu = @GhiChu WHERE MaPhieuNhap = @MaPhieuNhap";
                SqlCommand cmdUpdate = new SqlCommand(queryUpdate, conec);
                cmdUpdate.Parameters.AddWithValue("@NgayBan", ngayNhapNew);
                cmdUpdate.Parameters.AddWithValue("@MaKH", nguoiNhapNew);
                cmdUpdate.Parameters.AddWithValue("@MaNV", ghiChuNew);
                cmdUpdate.Parameters.AddWithValue("@MaDonHang", maPhieuNhap);

                int rowsAffected = cmdUpdate.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Sửa thông tin phiếu nhập thành công!");
                    HienthiPhieuNhap(); // Refresh lại dữ liệu
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
            if (string.IsNullOrWhiteSpace(txtMaPhieuNhap.Text))
            {
                MessageBox.Show("Vui lòng nhập Mã phiếu nhập cần xóa.");
                return;
            }

            DialogResult result = MessageBox.Show("Bạn có chắc muốn xóa?", "Xác nhận xóa", MessageBoxButtons.YesNo);
            if (result == DialogResult.No) return;

            try
            {
                Moketnoi();
                string query = "DELETE FROM NhapHang WHERE MaPhieuNhap = @MaPhieuNhap";
                SqlCommand cmd = new SqlCommand(query, conec);
                cmd.Parameters.AddWithValue("@MaDonHang", txtMaPhieuNhap.Text);

                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Xóa thành công!");
                    HienthiPhieuNhap(); // refresh lại danh sách
                }
                else
                {
                    MessageBox.Show("Không tìm thấy Mã phiếu nhập cần xóa.");
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

                string query = "SELECT MaPhieuNhap, NgayNhap, NguoiNhap, GhiChu FROM NhapHang";
                SqlCommand cmd = new SqlCommand(query, conec);
                SqlDataReader reader = cmd.ExecuteReader();

                listViewNhapHang.Items.Clear();

                string maPN_Search = txtMaPhieuNhap.Text.Trim().ToLower();
                string nguoiNhap_Search = txtNguoiNhap.Text.Trim().ToLower();
                string ghiChu_Search = txtGhiChu.Text.Trim().ToLower();
                bool coNgay = dtpNgayNhap.Checked;
                DateTime ngaySearch = dtpNgayNhap.Value.Date;

                while (reader.Read())
                {
                    string maPN = reader["MaPhieuNhap"].ToString();
                    string nguoiNhap = reader["NguoiNhap"].ToString();
                    string ghiChu = reader["GhiChu"].ToString();
                    DateTime ngayNhap = Convert.ToDateTime(reader["NgayNhap"]);

                    bool isMatch = true;
                    if (!string.IsNullOrEmpty(maPN_Search) && !maPN.ToLower().Contains(maPN_Search))
                        isMatch = false;
                    if (!string.IsNullOrEmpty(nguoiNhap_Search) && !nguoiNhap.ToLower().Contains(nguoiNhap_Search))
                        isMatch = false;
                    if (!string.IsNullOrEmpty(ghiChu_Search) && !ghiChu.ToLower().Contains(ghiChu_Search))
                        isMatch = false;
                    if (coNgay && (ngayNhap.Date < ngaySearch || ngayNhap.Date >= ngaySearch.AddDays(1)))
                        isMatch = false;

                    ListViewItem item = new ListViewItem(maPN);
                    item.SubItems.Add(ngayNhap.ToString("dd/MM/yyyy"));
                    item.SubItems.Add(nguoiNhap);
                    item.SubItems.Add(ghiChu);

                    if (isMatch)
                    {
                        item.BackColor = Color.LightGreen;
                        listViewNhapHang.Items.Insert(0, item);
                    }
                    else
                    {
                        listViewNhapHang.Items.Add(item);
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

        private void btnChiTietPhieuNhap_Click(object sender, EventArgs e)
        {
            string maPhieuNhap = txtMaPhieuNhap.Text.Trim();

            if (string.IsNullOrEmpty(maPhieuNhap))
            {
                MessageBox.Show("Vui lòng chọn hoặc nhập Mã phiếu nhập.");
                return;
            }

            ChiTietPhieuNhap formChiTiet = new ChiTietPhieuNhap(maPhieuNhap);
            formChiTiet.ShowDialog();
        }
        private void txtMaPhieuNhap_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtNguoiNhap.Focus();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }
        private void txtNguoiNhap_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtGhiChu.Focus();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }
        private void txtGhiChu_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtMaPhieuNhap.Focus();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }
    }
}
