using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyGiayBD3
{
    public partial class FormChiTietSanPham : Form
    {
        public string connectionString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=QuanLyGiayTheThao;Integrated Security=True;TrustServerCertificate=True";

        private string maSP;
        private string tenSP;
        public FormChiTietSanPham(string maSP, string tenSP)
        {
            InitializeComponent();
            this.maSP = maSP;
            this.tenSP = tenSP;
        }

        private void FormChiTietSanPham_Load(object sender, EventArgs e)
        {
            lblMaSP.Text = "Mã sản phẩm: " + maSP;
            lblTenSP.Text = "Tên sản phẩm: " + tenSP;

            // Lấy thêm thông tin từ bảng SanPham
            string query = "SELECT GiaGoc, GiaKM, HinhAnh, MoTa FROM SanPham WHERE MaSP = @MaSP";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@MaSP", maSP);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    lblGiaGoc.Text =  Convert.ToDecimal(reader["GiaGoc"]).ToString("#,##0");

                    lblGiaKM.Text =  Convert.ToDecimal(reader["GiaKM"]).ToString("#,##0");

                    txtMoTa.Text = reader["MoTa"].ToString();

                    string tenAnh = reader["HinhAnh"].ToString();

                    string path = Path.Combine(Application.StartupPath, "Images", tenAnh);
                    if (File.Exists(path))
                    {
                        pictureBoxSanPham.Image = Image.FromFile(path);
                        pictureBoxSanPham.SizeMode = PictureBoxSizeMode.Zoom;
                    }
                    else
                    {
                        pictureBoxSanPham.Image = null;
                    }
                }
                conn.Close();
            }
            // Sau khi hoàn tất, tiếp tục tải Size:
            LoadChiTietSize();
        }
        private void LoadThongTinSanPham()
        {
            string query = "SELECT GiaGoc, GiaKM, HinhAnh, MoTa FROM SanPham WHERE MaSP = @MaSP";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@MaSP", maSP);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    lblGiaGoc.Text = "Giá gốc: " + Convert.ToDouble(reader["GiaGoc"]).ToString("N0") + " VNĐ";
                    lblGiaKM.Text = "Giá khuyến mãi: " + Convert.ToDouble(reader["GiaKM"]).ToString("N0") + " VNĐ";
                    txtMoTa.Text = reader["MoTa"].ToString();

                    string imagePath = reader["HinhAnh"].ToString();

                    if (!string.IsNullOrEmpty(imagePath) && System.IO.File.Exists(imagePath))
                    {
                        pictureBoxSanPham.Image = Image.FromFile(imagePath);
                        pictureBoxSanPham.SizeMode = PictureBoxSizeMode.Zoom;
                    }
                }
            }
        }
        private void LoadChiTietSize()
        {
            string query = "SELECT Size, SoLuong FROM ChiTietSanPham WHERE MaSP = @MaSP";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@MaSP", maSP);

                DataTable dt = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                adapter.Fill(dt);
                dgvChiTietSize.DataSource = dt;
            }

            dgvChiTietSize.Columns[0].HeaderText = "Size";
            dgvChiTietSize.Columns[1].HeaderText = "Số lượng";
            dgvChiTietSize.ReadOnly = true;
            dgvChiTietSize.AllowUserToAddRows = false;
            dgvChiTietSize.AllowUserToDeleteRows = false;
        }

    }
}
