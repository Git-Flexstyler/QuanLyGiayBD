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
            LoadChiTietSize();
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
