﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace database
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            tk.KeyDown += Tk_KeyDown;
            pass.KeyDown += Pass_KeyDown;
        }

        private void Tk_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                pass.Focus(); // Chuyển focus sang ô mật khẩu khi nhấn Enter trên ô tên đăng nhập
            }
        }

        private void Pass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btdn_Click(sender, e); // Gọi sự kiện Click của nút đăng nhập khi nhấn Enter trên ô mật khẩu
            }
        }

        private void btdn_Click(object sender, EventArgs e)
        {
            string tendangnhap = tk.Text;
            string matkhau = pass.Text;
            string connectionString = ConfigurationManager.ConnectionStrings["MyDBConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Tạo câu truy vấn SQL
                    string query = "SELECT COUNT(*) FROM ngdung WHERE tendangnhap = @tendangnhap AND pass = @pass";

                    // Tạo đối tượng SqlCommand
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Thêm tham số vào câu truy vấn
                        command.Parameters.AddWithValue("@tendangnhap", tendangnhap);
                        command.Parameters.AddWithValue("@pass", matkhau);

                        // Thực thi câu truy vấn và trả về số lượng bản ghi thỏa mãn điều kiện
                        int count = (int)command.ExecuteScalar();

                        if (count > 0)
                        {
                            MessageBox.Show("Đăng nhập thành công!");
                            frm3 frm = new frm3();
                            frm.ShowDialog();
                        }
                        else
                        {
                            MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu!");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi kết nối: " + ex.Message);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Bạn có muốn thoát ứng dụng không?", "thông báo", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            // Không có xử lý
        }

        private void tk_TextChanged(object sender, EventArgs e)
        {
            // Không có xử lý
        }

        private void pass_TextChanged(object sender, EventArgs e)
        {
            // Không có xử lý
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}