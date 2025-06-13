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
using System.Windows.Forms.DataVisualization.Charting;

namespace QuanLyGiayBD3
{
    public partial class FormThongKe : Form
    {
        public FormThongKe()
        {
            InitializeComponent();
            LoadTopSanPham();
            LoadDoanhThuTheoThang();
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
        private void LoadDoanhThuTheoThang()
        {
            string connString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=QuanLyGiayThethao;Integrated Security=True;TrustServerCertificate=True";
            string query = @"SELECT MONTH(NgayBan) AS Thang, SUM(TongTien) AS TongDoanhThu
                             FROM DonHang
                             WHERE YEAR(NgayBan) = 2025
                             GROUP BY MONTH(NgayBan)
                             ORDER BY Thang";

            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                // Xóa dữ liệu cũ trong Chart
                chartDoanhThu.Series.Clear();
                chartDoanhThu.ChartAreas.Clear();

                // Tạo ChartArea
                ChartArea chartArea = new ChartArea("DoanhThuArea");
                chartDoanhThu.ChartAreas.Add(chartArea);

                // Tạo Series cho biểu đồ cột
                Series series = new Series("DoanhThu")
                {
                    ChartType = SeriesChartType.Column, // Loại biểu đồ cột
                    IsValueShownAsLabel = true, // Hiển thị giá trị trên cột
                    LabelFormat = "{0:N0} VNĐ" // Định dạng tiền tệ
                };
                chartDoanhThu.Series.Add(series);

                // Thêm dữ liệu vào Series
                foreach (DataRow row in dt.Rows)
                {
                    int thang = Convert.ToInt32(row["Thang"]);
                    decimal doanhThu = Convert.ToDecimal(row["TongDoanhThu"]);
                    series.Points.AddXY(thang, doanhThu);
                }

                // Tùy chỉnh giao diện
                chartDoanhThu.Titles.Add("Doanh thu theo tháng (2025)");
                chartArea.AxisX.Title = "Tháng";
                chartArea.AxisY.Title = "Doanh thu (VNĐ)";
                chartArea.AxisX.Interval = 1; // Mỗi tháng cách nhau 1 đơn vị
                chartArea.AxisY.LabelStyle.Format = "{0:N0} VNĐ"; // Định dạng trục Y
            }
        }
    }
}
