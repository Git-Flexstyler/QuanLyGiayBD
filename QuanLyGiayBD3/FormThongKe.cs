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
        private string connString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=QuanLyGiayTheThao;Integrated Security=True;TrustServerCertificate=True";
        public FormThongKe()
        {
            InitializeComponent();
        }

        private void LoadTopSanPham(DateTime ngayBatDau, DateTime ngayKetThuc)
        {
            string query = "WITH RankedProducts AS (" +
                           "    SELECT sp.TenSP, SUM(ctdh.SoLuong) AS SoLuongBan, SUM(ctdh.SoLuong * ctdh.DonGia) AS DoanhThu, " +
                           "           ROW_NUMBER() OVER (ORDER BY SUM(ctdh.SoLuong) DESC) AS STT " +
                           "    FROM SanPham sp INNER JOIN ChiTietDonHang ctdh ON sp.MaSP = ctdh.MaSP " +
                           "    INNER JOIN DonHang dh ON ctdh.MaDonHang = dh.MaDonHang " +
                           "    WHERE dh.NgayBan BETWEEN @NgayBatDau AND @NgayKetThuc " +
                           "    GROUP BY sp.TenSP " +
                           ") " +
                           "SELECT STT, TenSP, SoLuongBan, DoanhThu FROM RankedProducts ORDER BY STT";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@NgayBatDau", ngayBatDau);
                da.SelectCommand.Parameters.AddWithValue("@NgayKetThuc", ngayKetThuc);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridViewTopSanPham.DataSource = dt;

                // Định dạng màu cho top 3
                dataGridViewTopSanPham.CellFormatting += (sender, e) =>
                {
                    if (e.ColumnIndex == dataGridViewTopSanPham.Columns["STT"].Index && e.RowIndex >= 0)
                    {
                        int stt = Convert.ToInt32(dataGridViewTopSanPham.Rows[e.RowIndex].Cells["STT"].Value);
                        if (stt == 1)
                            e.CellStyle.BackColor = Color.Gold; // Vàng cho vị trí 1
                        else if (stt == 2)
                            e.CellStyle.BackColor = Color.Silver; // Bạc cho vị trí 2
                        else if (stt == 3)
                            e.CellStyle.BackColor = Color.SaddleBrown; // Đồng cho vị trí 3
                    }
                };
            }
        }
        private void LoadDoanhThuTheoThang()
        {
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
        private void LoadNhapKhoTheoThuongHieu()
        {
            string query = "SELECT sp.ThuongHieu, SUM(ctn.SoLuongNhap) AS TongSoLuongNhap " +
                           "FROM SanPham sp INNER JOIN ChiTietNhap ctn ON sp.MaSP = ctn.MaSP " +
                           "GROUP BY sp.ThuongHieu";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                chartNhapKho.Series.Clear();
                chartNhapKho.ChartAreas.Clear();

                ChartArea chartArea = new ChartArea("NhapKhoArea");
                chartNhapKho.ChartAreas.Add(chartArea);

                Series series = new Series("NhapKho")
                {
                    ChartType = SeriesChartType.Pie,
                    IsValueShownAsLabel = true,
                    LabelFormat = "{0:N0} đôi"
                };
                chartNhapKho.Series.Add(series);

                foreach (DataRow row in dt.Rows)
                {
                    string thuongHieu = row["ThuongHieu"].ToString();
                    int soLuong = Convert.ToInt32(row["TongSoLuongNhap"]);
                    series.Points.AddXY(thuongHieu, soLuong);
                }

                chartNhapKho.Titles.Add("Số lượng nhập kho theo thương hiệu");
            }
        }
        private void LoadKhachHangVIP()
        {
            string query = "SELECT ROW_NUMBER() OVER (ORDER BY TongTien DESC) AS STT, MaKH, TenKH, TongTien, SoDienThoai " +
                           "FROM KhachHang WHERE LoaiKH = @LoaiKH";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@LoaiKH", "VIP");
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridViewKhachHangVIP.DataSource = dt;

                // Định dạng màu cho top 3 (dựa trên STT)
                dataGridViewKhachHangVIP.CellFormatting += (sender, e) =>
                {
                    if (e.ColumnIndex == dataGridViewKhachHangVIP.Columns["STT"].Index && e.RowIndex >= 0)
                    {
                        int stt = Convert.ToInt32(dataGridViewKhachHangVIP.Rows[e.RowIndex].Cells["STT"].Value);
                        if (stt == 1)
                            e.CellStyle.BackColor = Color.Gold; // Vàng cho vị trí 1
                        else if (stt == 2)
                            e.CellStyle.BackColor = Color.Silver; // Bạc cho vị trí 2
                        else if (stt == 3)
                            e.CellStyle.BackColor = Color.SaddleBrown; // Đồng cho vị trí 3
                    }
                };
            }
        }

        private void FormThongKe_Load(object sender, EventArgs e)
        {
            dateTimePickerBatDau.Value = new DateTime(2025, 1, 1);
            dateTimePickerKetThuc.Value = DateTime.Now;
            LoadData();
        }
        private void LoadData()
        {
            DateTime ngayBatDau = dateTimePickerBatDau.Value;
            DateTime ngayKetThuc = dateTimePickerKetThuc.Value;
            LoadTopSanPham(ngayBatDau, ngayKetThuc);
            LoadDoanhThuTheoThang();
            LoadNhapKhoTheoThuongHieu();
            LoadKhachHangVIP();
        }
    }
}
