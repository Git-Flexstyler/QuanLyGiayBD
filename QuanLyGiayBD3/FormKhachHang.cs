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
    public partial class FormKhachHang : Form
    {
        string connectionString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=QuanLyGiayTheThao;Integrated Security=True;TrustServerCertificate=True";
        public FormKhachHang()
        {
            InitializeComponent();
        }

        private void FormKhachHang_Load(object sender, EventArgs e)
        {

        }

        void HienThiDanhSachKhachHang()
        {
            listViewKhachHang.Items.Clear();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM KhachHang";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ListViewItem item = new ListViewItem(reader["MaKH"].ToString());
                    item.SubItems.Add(reader["TenKH"].ToString());
                    item.SubItems.Add(reader["SoDienThoai"].ToString());
                    item.SubItems.Add(reader["DiaChi"].ToString());
                    item.SubItems.Add(reader["TongTien"].ToString());
                    item.SubItems.Add(reader["LoaiKH"].ToString());
                    listViewKhachHang.Items.Add(item);
                }
                reader.Close();
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
