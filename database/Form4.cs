using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace database
{
    public partial class Hanghoa_frm4 : Form
    {
        public Hanghoa_frm4()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void Hanghoa_frm4_Load(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyDBConnectionString"].ConnectionString;
            // Khởi tạo kết nối
            SqlConnection connection = new SqlConnection(connectionString);

            // Mở kết nối
            connection.Open();

            // Truy vấn dữ liệu từ bảng trong CSDL
            string query = "SELECT * FROM qlhanghoa";
            SqlCommand command = new SqlCommand(query, connection);

            // Đọc dữ liệu vào DataTable
            DataTable dataTable = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(dataTable);

            // Gán DataTable làm nguồn dữ liệu cho DataGridView
            dataGridView1.DataSource = dataTable;

            // Đóng kết nối sau khi sử dụng xong
            connection.Close();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bt_them_Click(object sender, EventArgs e)
        {

        }
    }
}
