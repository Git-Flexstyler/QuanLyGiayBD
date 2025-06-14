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
    public partial class ChiTietPhieuNhap : Form
    {
        public ChiTietPhieuNhap()
        {
            InitializeComponent();
        }
        public ChiTietPhieuNhap(string maPhieuNhap)
        {
            InitializeComponent();
            this.Size = new Size(1000, 550);
            txtMaPhieuNhap.Text = maPhieuNhap;
            CauHinhListView();
            HienthiChiTietPhieuNhap(maPhieuNhap);
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
            listViewChiTietPhieuNhap.View = View.Details;
            listViewChiTietPhieuNhap.FullRowSelect = true;
            listViewChiTietPhieuNhap.GridLines = true;

            listViewChiTietPhieuNhap.Columns.Clear();
            listViewChiTietPhieuNhap.Columns.Add("Mã phiếu nhập", 120);
            listViewChiTietPhieuNhap.Columns.Add("Mã sản phẩm", 120);
            listViewChiTietPhieuNhap.Columns.Add("Số lượng nhập", 120);
            listViewChiTietPhieuNhap.Columns.Add("Đơn giá", 120);
            listViewChiTietPhieuNhap.Columns.Add("Mã kho", 120);

        }
        private void HienthiChiTietPhieuNhap(string maPhieuNhap)
        {
            try
            {
                Moketnoi();

                string query = "SELECT * FROM ChiTietNhap where MaPhieuNhap = @MaPhieuNhap";
                SqlCommand cmd = new SqlCommand(query, conec);
                cmd.Parameters.AddWithValue("MaPhieuNhap", maPhieuNhap);
                SqlDataReader reader = cmd.ExecuteReader();

                listViewChiTietPhieuNhap.Items.Clear();

                while (reader.Read())
                {
                    ListViewItem item = new ListViewItem(reader["MaPhieuNhap"].ToString());
                    item.SubItems.Add(reader["MaSP"].ToString());
                    item.SubItems.Add(reader["SoLuongNhap"].ToString());
                    item.SubItems.Add(reader["DonGia"].ToString());
                    item.SubItems.Add(reader["MaKho"].ToString());


                    listViewChiTietPhieuNhap.Items.Add(item);
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

        private void listViewChiTietPhieuNhap_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewChiTietPhieuNhap.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = listViewChiTietPhieuNhap.SelectedItems[0];

                txtMaPhieuNhap.Text = selectedItem.SubItems[0].Text;
                txtMaSanPham.Text = selectedItem.SubItems[1].Text;
                txtSoLuongNhap.Text = selectedItem.SubItems[2].Text;
                txtDonGia.Text = selectedItem.SubItems[3].Text;
                txtMaKho.Text = selectedItem.SubItems[4].Text;
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtMaSanPham.Text = null;
            txtSoLuongNhap.Text = null;
            txtDonGia.Text = null;
            txtMaKho.Text = null;

            HienthiChiTietPhieuNhap(txtMaPhieuNhap.Text);
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

                int maPhieu = int.Parse(txtMaPhieuNhap.Text);
                int maSP = int.Parse(txtMaSanPham.Text);
                int soLuong = int.Parse(txtSoLuongNhap.Text);
                float donGia = float.Parse(txtDonGia.Text);
                int maKho = int.Parse(txtMaKho.Text);

                // 1. Kiểm tra chi tiết đã tồn tại chưa
                string checkQuery = "SELECT COUNT(*) FROM ChiTietNhap WHERE MaPhieuNhap = @MaPhieuNhap AND MaSP = @MaSP";
                SqlCommand checkCmd = new SqlCommand(checkQuery, conec);
                checkCmd.Parameters.AddWithValue("@MaPhieuNhap", maPhieu);
                checkCmd.Parameters.AddWithValue("@MaSP", maSP);

                int count = (int)checkCmd.ExecuteScalar();

                if (count > 0)
                {
                    // 2. Nếu tồn tại → UPDATE số lượng và đơn giá
                    string updateQuery = @"
                UPDATE ChiTietNhap
                SET SoLuongNhap = SoLuongNhap + @SoLuongNhap,
                    DonGia = DonGia + @DonGia
                WHERE MaPhieuNhap = @MaPhieuNhap AND MaSP = @MaSP";

                    SqlCommand updateCmd = new SqlCommand(updateQuery, conec);
                    updateCmd.Parameters.AddWithValue("@SoLuongNhap", soLuong);
                    updateCmd.Parameters.AddWithValue("@DonGia", donGia);
                    updateCmd.Parameters.AddWithValue("@MaPhieuNhap", maPhieu);
                    updateCmd.Parameters.AddWithValue("@MaSP", maSP);
                    updateCmd.ExecuteNonQuery();
                }
                else
                {
                    // 3. Nếu chưa tồn tại → INSERT mới
                    string insertQuery = @"
                INSERT INTO ChiTietNhap (MaPhieuNhap, MaSP, SoLuongNhap, DonGia, MaKho)
                VALUES (@MaPhieuNhap, @MaSP, @SoLuongNhap, @DonGia, @MaKho)";

                    SqlCommand insertCmd = new SqlCommand(insertQuery, conec);
                    insertCmd.Parameters.AddWithValue("@MaPhieuNhap", maPhieu);
                    insertCmd.Parameters.AddWithValue("@MaSP", maSP);
                    insertCmd.Parameters.AddWithValue("@SoLuongNhap", soLuong);
                    insertCmd.Parameters.AddWithValue("@DonGia", donGia);
                    insertCmd.Parameters.AddWithValue("@MaKho", maKho);
                    insertCmd.ExecuteNonQuery();
                }

                // 4. Cập nhật số lượng trong bảng SanPham
                string updateSPQuery = "UPDATE SanPham SET SoLuong = SoLuong + @SoLuongNhap WHERE MaSP = @MaSP";
                SqlCommand updateSPCmd = new SqlCommand(updateSPQuery, conec);
                updateSPCmd.Parameters.AddWithValue("@SoLuongNhap", soLuong);
                updateSPCmd.Parameters.AddWithValue("@MaSP", maSP);
                updateSPCmd.ExecuteNonQuery();

                MessageBox.Show("Cập nhật chi tiết phiếu nhập thành công!");
                HienthiChiTietPhieuNhap(txtMaPhieuNhap.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm chi tiết: " + ex.Message);
            }
            finally
            {
                DongKetNoi();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaPhieuNhap.Text) || string.IsNullOrWhiteSpace(txtMaSanPham.Text))
            {
                MessageBox.Show("Vui lòng chọn chi tiết phiếu nhập cần sửa.");
                return;
            }

            try
            {
                Moketnoi();

                int maPhieuNhap = int.Parse(txtMaPhieuNhap.Text.Trim());
                int maSP = int.Parse(txtMaSanPham.Text.Trim());

                // Lấy dữ liệu cũ
                string querySelect = "SELECT SoLuongNhap, DonGia, MaKho FROM ChiTietNhap WHERE MaPhieuNhap = @MaPhieuNhap AND MaSP = @MaSP";
                SqlCommand cmdSelect = new SqlCommand(querySelect, conec);
                cmdSelect.Parameters.AddWithValue("@MaPhieuNhap", maPhieuNhap);
                cmdSelect.Parameters.AddWithValue("@MaSP", maSP);

                SqlDataReader reader = cmdSelect.ExecuteReader();

                if (!reader.Read())
                {
                    reader.Close();
                    MessageBox.Show("Không tìm thấy chi tiết phiếu nhập cần sửa.");
                    return;
                }

                int soLuongCu = Convert.ToInt32(reader["SoLuongNhap"]);
                float donGiaCu = Convert.ToSingle(reader["DonGia"]);
                int maKhoCu = Convert.ToInt32(reader["MaKho"]);
                reader.Close();

                // Lấy dữ liệu mới từ form
                int soLuongMoi = int.Parse(txtSoLuongNhap.Text.Trim());
                float donGiaMoi = float.Parse(txtDonGia.Text.Trim());
                int maKhoMoi = int.Parse(txtMaKho.Text.Trim());

                // So sánh nếu không có gì thay đổi
                if (soLuongCu == soLuongMoi && donGiaCu == donGiaMoi && maKhoCu == maKhoMoi)
                {
                    MessageBox.Show("Thông tin không thay đổi.");
                    return;
                }

                // Cập nhật bảng ChiTietNhap
                string queryUpdate = @"
            UPDATE ChiTietNhap 
            SET SoLuongNhap = @SoLuongNhap, DonGia = @DonGia, MaKho = @MaKho
            WHERE MaPhieuNhap = @MaPhieuNhap AND MaSP = @MaSP";
                SqlCommand cmdUpdate = new SqlCommand(queryUpdate, conec);
                cmdUpdate.Parameters.AddWithValue("@SoLuongNhap", soLuongMoi);
                cmdUpdate.Parameters.AddWithValue("@DonGia", donGiaMoi);
                cmdUpdate.Parameters.AddWithValue("@MaKho", maKhoMoi);
                cmdUpdate.Parameters.AddWithValue("@MaPhieuNhap", maPhieuNhap);
                cmdUpdate.Parameters.AddWithValue("@MaSP", maSP);

                int rows = cmdUpdate.ExecuteNonQuery();

                if (rows > 0)
                {
                    // Cập nhật lại SoLuongTon trong bảng SanPham nếu số lượng thay đổi
                    int chenhLech = soLuongMoi - soLuongCu;

                    if (chenhLech != 0)
                    {
                        string querySP = "UPDATE SanPham SET SoLuong = SoLuong + @ChenhLech WHERE MaSP = @MaSP";
                        SqlCommand cmdSP = new SqlCommand(querySP, conec);
                        cmdSP.Parameters.AddWithValue("@ChenhLech", chenhLech);
                        cmdSP.Parameters.AddWithValue("@MaSP", maSP);
                        cmdSP.ExecuteNonQuery();
                    }

                    MessageBox.Show("Sửa chi tiết thành công!");
                    HienthiChiTietPhieuNhap(txtMaPhieuNhap.Text); // Làm mới list chi tiết
                }
                else
                {
                    MessageBox.Show("Không có bản ghi nào được sửa.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi sửa chi tiết: " + ex.Message);
            }
            finally
            {
                DongKetNoi();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaPhieuNhap.Text) || string.IsNullOrWhiteSpace(txtMaSanPham.Text))
            {
                MessageBox.Show("Vui lòng chọn chi tiết phiếu nhập cần xóa.");
                return;
            }

            DialogResult result = MessageBox.Show("Bạn có chắc muốn xóa chi tiết này?", "Xác nhận xóa", MessageBoxButtons.YesNo);
            if (result == DialogResult.No) return;

            try
            {
                Moketnoi();

                int maPhieuNhap = int.Parse(txtMaPhieuNhap.Text.Trim());
                int maSP = int.Parse(txtMaSanPham.Text.Trim());

                // 1. Lấy số lượng cũ trước khi xóa
                string querySelect = "SELECT SoLuongNhap FROM ChiTietNhap WHERE MaPhieuNhap = @MaPhieuNhap AND MaSP = @MaSP";
                SqlCommand cmdSelect = new SqlCommand(querySelect, conec);
                cmdSelect.Parameters.AddWithValue("@MaPhieuNhap", maPhieuNhap);
                cmdSelect.Parameters.AddWithValue("@MaSP", maSP);

                object resultSL = cmdSelect.ExecuteScalar();
                if (resultSL == null)
                {
                    MessageBox.Show("Không tìm thấy chi tiết cần xóa.");
                    return;
                }

                int soLuongCanTru = Convert.ToInt32(resultSL);

                // 2. Xóa dòng chi tiết
                string queryDelete = "DELETE FROM ChiTietNhap WHERE MaPhieuNhap = @MaPhieuNhap AND MaSP = @MaSP";
                SqlCommand cmdDelete = new SqlCommand(queryDelete, conec);
                cmdDelete.Parameters.AddWithValue("@MaPhieuNhap", maPhieuNhap);
                cmdDelete.Parameters.AddWithValue("@MaSP", maSP);

                int rows = cmdDelete.ExecuteNonQuery();

                if (rows > 0)
                {
                    // 3. Giảm số lượng trong bảng sản phẩm
                    string queryUpdateSP = "UPDATE SanPham SET SoLuong = SoLuong - @SLTru WHERE MaSP = @MaSP";
                    SqlCommand cmdUpdateSP = new SqlCommand(queryUpdateSP, conec);
                    cmdUpdateSP.Parameters.AddWithValue("@SLTru", soLuongCanTru);
                    cmdUpdateSP.Parameters.AddWithValue("@MaSP", maSP);
                    cmdUpdateSP.ExecuteNonQuery();

                    MessageBox.Show("Xóa chi tiết thành công!");
                    HienthiChiTietPhieuNhap(txtMaPhieuNhap.Text); // refresh danh sách chi tiết
                }
                else
                {
                    MessageBox.Show("Không có bản ghi nào bị xóa.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa chi tiết: " + ex.Message);
            }
            finally
            {
                DongKetNoi();
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                Moketnoi();

                string query = "SELECT MaPhieuNhap, MaSP, MaKho, SoLuongNhap, DonGia FROM ChiTietNhap where MaPhieuNhap = @MaPhieuNhap";
                SqlCommand cmd = new SqlCommand(query, conec);
                cmd.Parameters.AddWithValue("MaPhieuNhap", txtMaPhieuNhap.Text);
                SqlDataReader reader = cmd.ExecuteReader();

                listViewChiTietPhieuNhap.Items.Clear();

                string maPN_Search = txtMaPhieuNhap.Text.Trim().ToLower();
                string maSP_Search = txtMaSanPham.Text.Trim().ToLower();
                string maKho_Search = txtMaKho.Text.Trim().ToLower();
                string soLuong_Search = txtSoLuongNhap.Text.Trim();
                string donGia_Search = txtDonGia.Text.Trim();

                while (reader.Read())
                {
                    string maPN = reader["MaPhieuNhap"].ToString();
                    string maSP = reader["MaSP"].ToString();
                    string maKho = reader["MaKho"].ToString();
                    string soLuong = reader["SoLuongNhap"].ToString();
                    string donGia = reader["DonGia"].ToString();

                    bool isMatch = true;
                    if (!string.IsNullOrEmpty(maPN_Search) && !maPN.ToLower().Contains(maPN_Search))
                        isMatch = false;
                    if (!string.IsNullOrEmpty(maSP_Search) && !maSP.ToLower().Contains(maSP_Search))
                        isMatch = false;
                    if (!string.IsNullOrEmpty(maKho_Search) && !maKho.ToLower().Contains(maKho_Search))
                        isMatch = false;
                    if (!string.IsNullOrEmpty(soLuong_Search) && !soLuong.Contains(soLuong_Search))
                        isMatch = false;
                    if (!string.IsNullOrEmpty(donGia_Search) && !donGia.Contains(donGia_Search))
                        isMatch = false;

                    ListViewItem item = new ListViewItem(maPN);
                    item.SubItems.Add(maSP);
                    item.SubItems.Add(maKho);
                    item.SubItems.Add(soLuong);
                    item.SubItems.Add(donGia);

                    if (isMatch)
                    {
                        item.BackColor = Color.LightGreen;
                        listViewChiTietPhieuNhap.Items.Insert(0, item);
                    }
                    else
                    {
                        listViewChiTietPhieuNhap.Items.Add(item);
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
        private void txtMaPhieuNhap_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtMaSanPham.Focus();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void txtMaSP_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtSoLuongNhap.Focus();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void txtSoLuongNhap_KeyDown(object sender, KeyEventArgs e)
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
                txtMaKho.Focus();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }
        private void txtMaKhoKeyDown(object sender, KeyEventArgs e)
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
