using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyGiayBD3
{
    public partial class ProductPanel : UserControl
    {
        public ProductPanel()
        {
            InitializeComponent();
        }
        public void SetData(string ten, string giaGoc, string giaKM, string soLuong, string hinhAnh)
        {
            lblTen.Text = ten ?? "Tên sản phẩm";
            lblGiaGoc.Text = giaGoc ?? "Giá gốc";
            lblGiaKM.Text = giaKM ?? "Giá KM";
            lblSoLuong.Text = "Số lượng bán: " + (soLuong ?? "0");

            // Đặt màu đỏ cho giá KM
            lblGiaKM.ForeColor = Color.Red;

            // Xử lý tải ảnh (ghép đường dẫn giống FormSanPham)
            string fullPath = System.IO.Path.Combine(Application.StartupPath, "Images", hinhAnh);
            if (!string.IsNullOrEmpty(hinhAnh) && System.IO.File.Exists(fullPath))
            {
                try
                {
                    using (System.IO.FileStream fs = new System.IO.FileStream(fullPath, System.IO.FileMode.Open, System.IO.FileAccess.Read))
                    {
                        using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                        {
                            fs.CopyTo(ms);
                            ms.Position = 0;
                            pictureBox1.Image = Image.FromStream(ms);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi tải ảnh: " + ex.Message);
                    pictureBox1.Image = null;
                }
            }
            else
            {
                pictureBox1.Image = null; // Nếu không có ảnh, để trống
                MessageBox.Show("Không tìm thấy ảnh tại: " + fullPath);
            }
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void ProductPanel_MouseEnter(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(224, 247, 250);
        }

        private void ProductPanel_MouseLeave(object sender, EventArgs e)
        {
            this.BackColor = Color.WhiteSmoke;
        }
    }
}

