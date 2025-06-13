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
    public partial class FormTrangChu : Form
    {
        public FormTrangChu()
        {
            InitializeComponent();
        }
        private void LoadTopSanPham()
        {
            string connString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=QuanLyGiayThethao;Integrated Security=True;TrustServerCertificate=True"; // Thay bằng chuỗi kết nối thực tế
            string query = @"SELECT TOP 5 sp.TenSP, sp.GiaGoc, sp.GiaKM, SUM(ctdh.SoLuong) AS TongSoLuong, sp.HinhAnh 
                             FROM SanPham sp 
                             INNER JOIN ChiTietDonHang ctdh ON sp.MaSP = ctdh.MaSP 
                             GROUP BY sp.TenSP, sp.GiaGoc, sp.GiaKM, sp.HinhAnh 
                             ORDER BY TongSoLuong DESC";

            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                flowLayoutPanel1.Controls.Clear(); // Xóa các ô cũ

                foreach (DataRow row in dt.Rows)
                {
                    ProductPanel panel = new ProductPanel();
                    panel.SetData(
                        row["TenSP"].ToString(),
                        string.Format("{0:N0} VNĐ", row["GiaGoc"]),
                        string.Format("{0:N0} VNĐ", row["GiaKM"]),
                        row["TongSoLuong"].ToString(),
                        row["HinhAnh"].ToString()
                    );
                    flowLayoutPanel1.Controls.Add(panel);
                }
            }
        }
        private void FormTrangChu_Load(object sender, EventArgs e)
        {
            LoadTopSanPham();
        }
    }
}
