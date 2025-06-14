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
    public partial class HoaDon : Form
    {
        public HoaDon()
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
        public HoaDon(string maDonHang)
        {
            InitializeComponent();
            this.Size = new Size(520, 600);
            HienthiHoaDon(maDonHang);
            HienThiThongTinHoaDon(maDonHang);

        }
        private void HienthiHoaDon(string maDonHang)
        {
            try
            {
                Moketnoi();

                string query = @"
            SELECT ctdh.MaSP, sp.TenSP, ctdh.Size, ctdh.SoLuong, ctdh.DonGia
            FROM ChiTietDonHang ctdh
            JOIN SanPham sp ON ctdh.MaSP = sp.MaSP
            WHERE ctdh.MaDonHang = @MaDonHang";

                SqlCommand cmd = new SqlCommand(query, conec);
                cmd.Parameters.AddWithValue("@MaDonHang", maDonHang);

                SqlDataReader reader = cmd.ExecuteReader();

                listViewHoaDon.Items.Clear();

                while (reader.Read())
                {
                    ListViewItem item = new ListViewItem(reader["MaSP"].ToString());
                    item.SubItems.Add(reader["TenSP"].ToString());
                    item.SubItems.Add(reader["Size"].ToString());
                    item.SubItems.Add(reader["SoLuong"].ToString());
                    item.SubItems.Add(reader["DonGia"].ToString());

                    listViewHoaDon.Items.Add(item);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hiển thị chi tiết hóa đơn: " + ex.Message);
            }
            finally
            {
                DongKetNoi();
            }
        }
        private void HienThiThongTinHoaDon(string maDonHang)
        {
            try
            {
                Moketnoi();

                string query = @"
            SELECT dh.MaDonHang, dh.MaNV, kh.TenKH, kh.SoDienThoai, kh.DiaChi, dh.NgayBan, dh.TongTien
            FROM DonHang dh
            JOIN KhachHang kh ON dh.SoDienThoai = kh.SoDienThoai
            WHERE dh.MaDonHang = @MaDonHang";

                SqlCommand cmd = new SqlCommand(query, conec);
                cmd.Parameters.AddWithValue("@MaDonHang", maDonHang);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    txtMaDonHang.Text = reader["MaDonHang"].ToString();
                    txtMaNhanVien.Text = reader["MaNV"].ToString();
                    txtHoTen.Text = reader["TenKH"].ToString();
                    txtSoDienThoai.Text = reader["SoDienThoai"].ToString();
                    txtDiaChi.Text = reader["DiaChi"].ToString();
                    dtpNgayMua.Value = Convert.ToDateTime(reader["NgayBan"]);

                    // Xử lý tổng tiền, thuế, phải trả
                    double tongTien = Convert.ToDouble(reader["TongTien"]);
                    double thue = tongTien * 0.05;
                    double phaiTra = tongTien + thue;

                    txtTongTien.Text = tongTien.ToString("N0"); // format số đẹp
                    txtThue.Text = thue.ToString("N0");
                    txtPhaiTra.Text = phaiTra.ToString("N0");
                }
                else
                {
                    MessageBox.Show("Không tìm thấy hóa đơn.");
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi hiển thị thông tin hóa đơn: " + ex.Message);
            }
            finally
            {
                DongKetNoi();
            }
        }
    }
}
