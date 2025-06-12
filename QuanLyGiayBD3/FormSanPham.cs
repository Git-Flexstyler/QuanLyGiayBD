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
using System.Runtime.CompilerServices;
using System.IO;

namespace QuanLyGiayBD3
{

    public partial class FormSanPham : Form
    {
        public string connectionString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=QuanLyGiayTheThao;Integrated Security=True;TrustServerCertificate=True";

        public FormSanPham()
        {
            InitializeComponent();
        }

        private void FormSanPham_Load(object sender, EventArgs e)
        {
            lvSanPham.View = View.Details;
            lvSanPham.FullRowSelect = true;
            lvSanPham.GridLines = true;

            lvSanPham.Columns.Add("Mã SP", 70);
            lvSanPham.Columns.Add("Tên SP", 150);
            lvSanPham.Columns.Add("Thương hiệu", 100);
            lvSanPham.Columns.Add("Giá gốc", 100);
            lvSanPham.Columns.Add("Giá KM", 100);
            lvSanPham.Columns.Add("Số lượng", 80);
            lvSanPham.Columns.Add("Mã Kho", 80);
            lvSanPham.Columns.Add("Mô tả", 200);
            lvSanPham.Columns.Add("Hình ảnh", 100); // thêm dòng này
            txtTimKiem.Text = "Tìm kiếm";
            txtTimKiem.ForeColor = Color.Gray;

            LoadKhoToComboBox();
            LoadDanhSachSanPham();
        }
        private void LoadDanhSachSanPham()
        {
            lvSanPham.Items.Clear(); // Xóa dữ liệu cũ nếu có

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT MaSP, TenSP, ThuongHieu, GiaGoc, GiaKM, SoLuong, MaKho, MoTa, HinhAnh FROM SanPham";
                SqlCommand cmd = new SqlCommand(query, conn);

                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        ListViewItem item = new ListViewItem(reader["MaSP"].ToString());
                        item.SubItems.Add(reader["TenSP"].ToString());
                        item.SubItems.Add(reader["ThuongHieu"].ToString());
                        item.SubItems.Add(Convert.ToDouble(reader["GiaGoc"]).ToString("N0") + "đ");
                        item.SubItems.Add(Convert.ToDouble(reader["GiaKM"]).ToString("N0") + "đ");
                        item.SubItems.Add(reader["SoLuong"].ToString());
                        item.SubItems.Add(reader["MaKho"].ToString());
                        item.SubItems.Add(reader["MoTa"].ToString());
                        item.SubItems.Add(reader["HinhAnh"].ToString());

                        lvSanPham.Items.Add(item);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi load sản phẩm: " + ex.Message);
                }
            }
        }
        private void LoadKhoToComboBox()
        {
            cboMaKho.Items.Clear();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT MaKho FROM KhoHang";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    cboMaKho.Items.Add(reader["MaKho"].ToString());
                }
            }
        }
        private void lvSanPham_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvSanPham.SelectedItems.Count > 0)
            {
                ListViewItem item = lvSanPham.SelectedItems[0];

                txtMaSP.Text = item.SubItems[0].Text;
                txtTenSP.Text = item.SubItems[1].Text;
                txtThuongHieu.Text = item.SubItems[2].Text;

                // Những trường số để thẳng không có "đ"
                txtGiaGoc.Text = item.SubItems[3].Text.Replace("đ", "").Replace(",", "").Trim();
                txtGiaKM.Text = item.SubItems[4].Text.Replace("đ", "").Replace(",", "").Trim();

                txtSoLuong.Text = item.SubItems[5].Text;
                cboMaKho.Text = item.SubItems[6].Text;
                txtMoTa.Text = item.SubItems[7].Text;

                string tenAnh = item.SubItems[8].Text;
                string duongDan = Application.StartupPath + "\\Images\\" + tenAnh;

                if (System.IO.File.Exists(duongDan))
                {
                    try
                    {
                        // Đảm bảo giải phóng ảnh cũ nếu có
                        if (picHinhAnh.Image != null)
                        {
                            picHinhAnh.Image.Dispose();
                            picHinhAnh.Image = null;
                        }

                        // Load ảnh bằng MemoryStream để tránh lock file
                        using (FileStream fs = new FileStream(duongDan, FileMode.Open, FileAccess.Read))
                        {
                            using (MemoryStream ms = new MemoryStream())
                            {
                                fs.CopyTo(ms);
                                picHinhAnh.Image = Image.FromStream(ms);
                                ms.Position = 0; // đặt lại con trỏ
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi tải ảnh: " + ex.Message);
                        picHinhAnh.Image = null;
                    }
                }
                else
                {
                    picHinhAnh.Image = null;
                    MessageBox.Show("Không tìm thấy ảnh: " + duongDan);
                }

            }
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenSP.Text) ||
                string.IsNullOrWhiteSpace(txtThuongHieu.Text) ||
                string.IsNullOrWhiteSpace(txtGiaGoc.Text) ||
                string.IsNullOrWhiteSpace(txtGiaKM.Text) ||
                string.IsNullOrWhiteSpace(txtSoLuong.Text) ||
                string.IsNullOrWhiteSpace(cboMaKho.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.");
                return;
            }

            string[] imageInfo = picHinhAnh.Tag as string[];
            string fileName = imageInfo?[1];
            string originalPath = imageInfo?[0];

            if (!string.IsNullOrEmpty(fileName) && !string.IsNullOrEmpty(originalPath))
            {
                string destinationPath = Path.Combine(Application.StartupPath, "Images", fileName);

                try
                {
                    // Nếu file chưa tồn tại thì copy từ ảnh gốc
                    if (!File.Exists(destinationPath))
                    {
                        File.Copy(originalPath, destinationPath, true);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi sao chép ảnh: " + ex.Message);
                    return;
                }
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO SanPham (TenSP, ThuongHieu, GiaGoc, GiaKM, SoLuong, MaKho, MoTa, HinhAnh) " +
                               "VALUES (@TenSP, @ThuongHieu, @GiaGoc, @GiaKM, @SoLuong, @MaKho, @MoTa, @HinhAnh); " +
                               "SELECT SCOPE_IDENTITY();";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@TenSP", txtTenSP.Text);
                cmd.Parameters.AddWithValue("@ThuongHieu", txtThuongHieu.Text);
                cmd.Parameters.AddWithValue("@GiaGoc", decimal.Parse(txtGiaGoc.Text));
                cmd.Parameters.AddWithValue("@GiaKM", decimal.Parse(txtGiaKM.Text));
                cmd.Parameters.AddWithValue("@SoLuong", int.Parse(txtSoLuong.Text));
                cmd.Parameters.AddWithValue("@MaKho", cboMaKho.Text);
                cmd.Parameters.AddWithValue("@MoTa", txtMoTa.Text);
                cmd.Parameters.AddWithValue("@HinhAnh", fileName ?? (object)DBNull.Value);

                try
                {
                    conn.Open();
                    object result = cmd.ExecuteScalar();
                    int newMaSP = Convert.ToInt32(result);

                    MessageBox.Show("Thêm sản phẩm thành công! Mã SP: " + newMaSP);
                    LoadDanhSachSanPham();
                    ResetInputFields();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi thêm sản phẩm: " + ex.Message);
                }
            }
        }
        private void ResetInputFields()
        {
            txtMaSP.Clear();
            txtTenSP.Clear();
            txtThuongHieu.Clear();
            txtGiaGoc.Clear();
            txtGiaKM.Clear();
            txtSoLuong.Clear();
            cboMaKho.SelectedIndex = -1;
            txtMoTa.Clear();
            picHinhAnh.Image = null;
            picHinhAnh.Tag = null; // <<< thêm dòng này
        }
        private void btnChonAnh_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string filePath = ofd.FileName;
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(filePath);
                string destinationPath = Path.Combine(Application.StartupPath, "Images", fileName);

                try
                {
                    // Load ảnh từ file vào MemoryStream để tránh lock file gốc
                    using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {
                            fs.CopyTo(ms);
                            ms.Position = 0;
                            picHinhAnh.Image = Image.FromStream(ms);
                        }
                    }

                    // Ghi nhớ đường dẫn file gốc để copy file (thay vì Save từ Image)
                    picHinhAnh.Tag = new string[] { filePath, fileName }; // dùng mảng lưu cả đường dẫn và tên
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi chọn ảnh: " + ex.Message);
                }
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            // Xóa hết các textbox, combobox và PictureBox về trạng thái mặc định
            txtMaSP.Clear();
            txtTenSP.Clear();
            txtThuongHieu.Clear();
            txtGiaGoc.Clear();
            txtGiaKM.Clear();
            txtSoLuong.Clear();
            txtMoTa.Clear();
            cboMaKho.SelectedIndex = -1; // bỏ chọn trong ComboBox
            picHinhAnh.Image = null;

            // Nếu bạn có ListView, có thể bỏ chọn item
            lvSanPham.SelectedItems.Clear();
            txtTimKiem.Text = "Tìm kiếm";
            txtTimKiem.ForeColor = Color.Gray;

            LoadDanhSachSanPham(); // Hàm này bạn gọi lại để load lại toàn bộ danh sách
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaSP.Text))
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần sửa.");
                return;
            }

            // Làm sạch và parse
            string maSPText = txtMaSP.Text.Trim();
            if (!int.TryParse(maSPText, out int maSP))
            {
                MessageBox.Show("Mã sản phẩm không hợp lệ.");
                return;
            }

            if (!decimal.TryParse(txtGiaGoc.Text.Replace(",", ""), out decimal giaGoc) ||
                !decimal.TryParse(txtGiaKM.Text.Replace(",", ""), out decimal giaKM) ||
                !int.TryParse(txtSoLuong.Text, out int soLuong))
            {
                MessageBox.Show("Giá hoặc số lượng không hợp lệ.");
                return;
            }

            string[] imageInfo = picHinhAnh.Tag as string[];
            string fileName = imageInfo?[1];
            string originalPath = imageInfo?[0];

            if (!string.IsNullOrEmpty(fileName) && !string.IsNullOrEmpty(originalPath))
            {
                string destinationPath = Path.Combine(Application.StartupPath, "Images", fileName);
                try
                {
                    if (!File.Exists(destinationPath))
                    {
                        File.Copy(originalPath, destinationPath, true);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi sao chép ảnh: " + ex.Message);
                    return;
                }
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"UPDATE SanPham SET 
                        TenSP = @TenSP,
                        ThuongHieu = @ThuongHieu,
                        GiaGoc = @GiaGoc,
                        GiaKM = @GiaKM,
                        SoLuong = @SoLuong,
                        MaKho = @MaKho,
                        MoTa = @MoTa,
                        HinhAnh = @HinhAnh
                        WHERE MaSP = @MaSP";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaSP", maSP);
                cmd.Parameters.AddWithValue("@TenSP", txtTenSP.Text.Trim());
                cmd.Parameters.AddWithValue("@ThuongHieu", txtThuongHieu.Text.Trim());
                cmd.Parameters.AddWithValue("@GiaGoc", giaGoc);
                cmd.Parameters.AddWithValue("@GiaKM", giaKM);
                cmd.Parameters.AddWithValue("@SoLuong", soLuong);
                cmd.Parameters.AddWithValue("@MaKho", cboMaKho.Text.Trim());
                cmd.Parameters.AddWithValue("@MoTa", txtMoTa.Text.Trim());
                cmd.Parameters.AddWithValue("@HinhAnh", fileName ?? (object)DBNull.Value);

                try
                {
                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();
                    if (rows > 0)
                    {
                        MessageBox.Show("Cập nhật sản phẩm thành công.");
                        LoadDanhSachSanPham();
                        ResetInputFields();
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy sản phẩm cần sửa.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi sửa sản phẩm: " + ex.Message);
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaSP.Text))
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần xóa.");
                return;
            }

            if (!int.TryParse(txtMaSP.Text, out int maSP))
            {
                MessageBox.Show("Mã sản phẩm không hợp lệ.");
                return;
            }

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa sản phẩm này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM SanPham WHERE MaSP = @MaSP";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaSP", maSP);

                    try
                    {
                        conn.Open();
                        int rows = cmd.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            MessageBox.Show("Xóa sản phẩm thành công.");
                            LoadDanhSachSanPham();
                            ResetInputFields();
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy sản phẩm cần xóa.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi xóa sản phẩm: " + ex.Message);
                    }
                }
            }
        }

        private void txtTimKiem_Enter(object sender, EventArgs e)
        {
            if (txtTimKiem.Text == "Tìm kiếm" && txtTimKiem.ForeColor == Color.Gray)
            {
                txtTimKiem.Text = "";
                txtTimKiem.ForeColor = Color.Black;
            }
        }

        private void txtTimKiem_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTimKiem.Text))
            {
                txtTimKiem.Text = "Tìm kiếm";
                txtTimKiem.ForeColor = Color.Gray;
            }
        }
        private void ThucHienTimKiem()
        {
            string keyword = txtTimKiem.Text.Trim().ToLower();

            if (string.IsNullOrWhiteSpace(keyword) || keyword == "tìm kiếm")
            {
                return;
            }

            // Tạo 2 danh sách: khớp và không khớp
            List<ListViewItem> danhSachKhop = new List<ListViewItem>();
            List<ListViewItem> danhSachKhongKhop = new List<ListViewItem>();

            foreach (ListViewItem item in lvSanPham.Items)
            {
                string tenSP = item.SubItems[1].Text.ToLower();

                if (tenSP.Contains(keyword))
                {
                    item.BackColor = Color.LightGreen;
                    danhSachKhop.Add((ListViewItem)item.Clone());
                }
                else
                {
                    item.BackColor = Color.White;
                    danhSachKhongKhop.Add((ListViewItem)item.Clone());
                }
            }

            // Cập nhật lại ListView
            lvSanPham.Items.Clear();
            lvSanPham.Items.AddRange(danhSachKhop.ToArray());
            lvSanPham.Items.AddRange(danhSachKhongKhop.ToArray());
        }

        private void picSearch_Click(object sender, EventArgs e)
        {
            ThucHienTimKiem();
        }

        private void txtTimKiem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ThucHienTimKiem();
                e.SuppressKeyPress = true; // Không kêu tiếng beep
            }
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            if (txtTimKiem.Text != "Tìm kiếm" && txtTimKiem.ForeColor == Color.Gray)
            {
                txtTimKiem.ForeColor = Color.Black;
            }
        }

        private void btnXem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaSP.Text))
            {
                MessageBox.Show("Vui lòng chọn sản phẩm trước!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maSP = txtMaSP.Text;
            string tenSP = txtTenSP.Text;

            FormChiTietSanPham fChiTiet = new FormChiTietSanPham(maSP, tenSP);
            fChiTiet.ShowDialog(); // dùng ShowDialog để modal, không cho thao tác bên ngoài
        }
    }
}
