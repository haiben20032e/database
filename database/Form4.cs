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
        private string connectionString;
        private SqlConnection sqlcon;
        public Hanghoa_frm4()
        {
            InitializeComponent();
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
            string idsanpham = txt_id.Text.Trim();
            string hang = txt_hang.Text.Trim();
            string sanpham = txt_tensp.Text.Trim();
            string soluong = txt_sl.Text.Trim();
            string baohanh = txt_bh.Text.Trim();
            string mota = txt_mt.Text.Trim();

            // Check if input fields are empty
            if (string.IsNullOrEmpty(idsanpham) || string.IsNullOrEmpty(hang) || string.IsNullOrEmpty(sanpham) ||
                string.IsNullOrEmpty(soluong) || string.IsNullOrEmpty(baohanh) || string.IsNullOrEmpty(mota))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin.");
                return;
            }

            int id;
            if (!int.TryParse(idsanpham, out id))
            {
                MessageBox.Show("ID hàng hóa phải là số nguyên.");
                return;
            }

            int quantity;
            if (!int.TryParse(soluong, out quantity))
            {
                MessageBox.Show("Số lượng nhập vào phải là số nguyên.");
                return;
            }

            // Initialize connection if not already done
            if (sqlcon == null)
            {
                connectionString = ConfigurationManager.ConnectionStrings["MyDBConnectionString"].ConnectionString;
                sqlcon = new SqlConnection(connectionString);
            }

            // Open connection if not already open
            if (sqlcon.State != ConnectionState.Open)
            {
                sqlcon.Open();
            }

            // Check for duplicate product ID
            string checkQuery = "SELECT COUNT(*) FROM qlhanghoa WHERE id = @idsanpham";
            SqlCommand checkCommand = new SqlCommand(checkQuery, sqlcon);
            checkCommand.Parameters.AddWithValue("@idsanpham", idsanpham);
            int existingCount = (int)checkCommand.ExecuteScalar();

            if (existingCount > 0)
            {
                MessageBox.Show("ID hàng hóa đã tồn tại trong cơ sở dữ liệu."); 
                return; // Don't proceed if ID already exists
            }

            // Add new product to the database
            string insertQuery = "INSERT INTO qlhanghoa (id, hang, tensp, sl, bh, mt) " +
                                 "VALUES (@idsanpham, @hang, @sanpham, @soluong, @baohanh, @mota)";
            SqlCommand insertCommand = new SqlCommand(insertQuery, sqlcon);
            insertCommand.Parameters.AddWithValue("@idsanpham", idsanpham);
            insertCommand.Parameters.AddWithValue("@hang", hang);
            insertCommand.Parameters.AddWithValue("@sanpham", sanpham);
            insertCommand.Parameters.AddWithValue("@soluong", soluong);
            insertCommand.Parameters.AddWithValue("@baohanh", baohanh);
            insertCommand.Parameters.AddWithValue("@mota", mota);

            int rowsAffected = insertCommand.ExecuteNonQuery();

            if (rowsAffected > 0)
            {
                MessageBox.Show("Thêm sản phẩm thành công.");
                // Clear input fields after successful addition
                txt_id.Text = "";
                txt_hang.Text = "";
                txt_tensp.Text = "";
                txt_sl.Text = "";
                txt_bh.Text = "";
                txt_mt.Text = "";

                // Update DataGridView after successful addition
                string refreshQuery = "SELECT * FROM qlhanghoa";
                SqlCommand refreshCommand = new SqlCommand(refreshQuery, sqlcon);
                SqlDataAdapter adapter = new SqlDataAdapter(refreshCommand);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
            }
            else
            {
                MessageBox.Show("Thêm sản phẩm thất bại.");
            }
        }


        private void bt_xoa_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem có hàng nào được chọn trong DataGridView không
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Lấy ID sản phẩm từ hàng được chọn
                string idsanpham = dataGridView1.SelectedRows[0].Cells["id"].Value.ToString();

                // Hiển thị MessageBox xác nhận xóa
                DialogResult result = MessageBox.Show($"Bạn có chắc chắn muốn xóa sản phẩm có ID {idsanpham}?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Kết nối cơ sở dữ liệu
                    if (sqlcon == null)
                    {
                        connectionString = ConfigurationManager.ConnectionStrings["MyDBConnectionString"].ConnectionString;
                        sqlcon = new SqlConnection(connectionString);
                    }

                    if (sqlcon.State != ConnectionState.Open)
                    {
                        sqlcon.Open();
                    }

                    // Tạo câu lệnh SQL để xóa sản phẩm
                    string deleteQuery = "DELETE FROM qlhanghoa WHERE id = @idsanpham";
                    SqlCommand deleteCommand = new SqlCommand(deleteQuery, sqlcon);
                    deleteCommand.Parameters.AddWithValue("@idsanpham", idsanpham);

                    int rowsAffected = deleteCommand.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show($"Đã xóa sản phẩm có ID {idsanpham} thành công.");
                        // Refresh DataGridView sau khi xóa
                        string refreshQuery = "SELECT * FROM qlhanghoa";
                        SqlCommand refreshCommand = new SqlCommand(refreshQuery, sqlcon);
                        SqlDataAdapter adapter = new SqlDataAdapter(refreshCommand);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        dataGridView1.DataSource = dataTable;
                    }
                    else
                    {
                        MessageBox.Show("Xóa sản phẩm thất bại.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một sản phẩm trong bảng để xóa.");
            }
        }

        private void bt_sua_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem đã nhập ID sản phẩm chưa
            if (string.IsNullOrEmpty(txt_id.Text))
            {
                MessageBox.Show("Vui lòng nhập ID sản phẩm để cập nhật.");
                return;
            }

            // Kiểm tra xem ID sản phẩm nhập vào có phải là số không
            int id;
            if (!int.TryParse(txt_id.Text, out id))
            {
                MessageBox.Show("ID sản phẩm phải là một số nguyên.");
                return;
            }

            // Kiểm tra xem đã nhập số lượng sản phẩm chưa và có phải là số không
            

            // Khởi tạo kết nối nếu cần
            if (sqlcon == null)
            {
                connectionString = ConfigurationManager.ConnectionStrings["MyDBConnectionString"].ConnectionString;
                sqlcon = new SqlConnection(connectionString);
            }

            // Mở kết nối nếu chưa mở
            if (sqlcon.State != ConnectionState.Open)
            {
                sqlcon.Open();
            }

            string idsanpham = txt_id.Text;

            // Kiểm tra sự tồn tại của ID sản phẩm trong cơ sở dữ liệu
            string queryCheckExist = "SELECT COUNT(*) FROM qlhanghoa WHERE id = @idsanpham";
            SqlCommand cmdCheckExist = new SqlCommand(queryCheckExist, sqlcon);
            cmdCheckExist.Parameters.AddWithValue("@idsanpham", idsanpham);
            int count = (int)cmdCheckExist.ExecuteScalar();

            // Nếu ID sản phẩm không tồn tại, hiển thị thông báo lỗi và thoát
            if (count == 0)
            {
                MessageBox.Show("ID sản phẩm không tồn tại trong cơ sở dữ liệu.");
                return;
            }

            // Tạo câu lệnh SQL UPDATE và cập nhật dữ liệu trong bảng sanpham
            string query = "UPDATE qlhanghoa SET ";
            bool hasUpdate = false;

            // Kiểm tra và cập nhật thông tin cần thiết
            if (!string.IsNullOrEmpty(txt_hang.Text))
            {
                query += "hang = @hang, ";
                hasUpdate = true;
            }
            if (!string.IsNullOrEmpty(txt_tensp.Text))
            {
                query += "tensp = @tensp, ";
                hasUpdate = true;
            }
            
           
            if (!string.IsNullOrEmpty(txt_sl.Text))
            {
                query += "sl = @soluong, ";
                hasUpdate = true;
            }
            if (!string.IsNullOrEmpty(txt_bh.Text))
            {
                query += "bh = @baohanh, ";
                hasUpdate = true;
            }
            if (!string.IsNullOrEmpty(txt_mt.Text))
            {
                query += "mt = @mota, ";
                hasUpdate = true;
            }

            // Kiểm tra xem có dữ liệu để cập nhật không
            if (hasUpdate)
            {
                // Loại bỏ dấu phẩy cuối cùng và thêm điều kiện WHERE vào câu lệnh UPDATE
                query = query.TrimEnd(' ', ',') + " WHERE id = @idsanpham";

                SqlCommand sqlcmd = new SqlCommand(query, sqlcon);
                sqlcmd.Parameters.AddWithValue("@idsanpham", idsanpham);
                if (!string.IsNullOrEmpty(txt_hang.Text))
                    sqlcmd.Parameters.AddWithValue("@hang", txt_hang.Text);
                if (!string.IsNullOrEmpty(txt_tensp.Text))
                    sqlcmd.Parameters.AddWithValue("@tensp", txt_tensp.Text);
                if (!string.IsNullOrEmpty(txt_sl.Text))
                    sqlcmd.Parameters.AddWithValue("@soluong", txt_sl.Text);
                if (!string.IsNullOrEmpty(txt_bh.Text))
                    sqlcmd.Parameters.AddWithValue("@baohanh", txt_bh.Text);
                if (!string.IsNullOrEmpty(txt_mt.Text))
                    sqlcmd.Parameters.AddWithValue("@mota", txt_mt.Text);

                int rowsAffected = sqlcmd.ExecuteNonQuery();
                
                // Hiển thị thông báo kết quả cập nhật sản phẩm
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Cập nhật thông tin sản phẩm thành công!");
                    // Clear input fields after successful addition
                    
                    txt_hang.Text = "";
                    txt_tensp.Text = "";
                    txt_sl.Text = "";
                    txt_bh.Text = "";
                    txt_mt.Text = "";

                    // Update DataGridView after successful addition
                    string refreshQuery = "SELECT * FROM qlhanghoa";
                    SqlCommand refreshCommand = new SqlCommand(refreshQuery, sqlcon);
                    SqlDataAdapter adapter = new SqlDataAdapter(refreshCommand);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dataGridView1.DataSource = dataTable;
                  
                }
                else
                {
                    MessageBox.Show("Cập nhật thông tin sản phẩm thất bại!");
                }
            }
            else
            {
                MessageBox.Show("Không có dữ liệu để cập nhật.");
            }
        }

        private void bt_tim_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem đã nhập ID sản phẩm cần tìm kiếm chưa
            if (string.IsNullOrEmpty(txt_id.Text))
            {
                MessageBox.Show("Vui lòng nhập ID sản phẩm cần tìm kiếm.");
                return;
            }

            // Kiểm tra xem ID sản phẩm nhập vào có phải là số không
            int id;
            if (!int.TryParse(txt_id.Text, out id))
            {
                MessageBox.Show("ID sản phẩm phải là một số nguyên.");
                return;
            }

            // Khởi tạo kết nối nếu cần
            if (sqlcon == null)
            {
                connectionString = ConfigurationManager.ConnectionStrings["MyDBConnectionString"].ConnectionString;
                sqlcon = new SqlConnection(connectionString);
            }

            // Mở kết nối nếu chưa mở
            if (sqlcon.State != ConnectionState.Open)
            {
                sqlcon.Open();
            }

            // Tạo câu lệnh SQL để tìm kiếm dữ liệu
            string searchQuery = "SELECT * FROM qlhanghoa WHERE id = @id";
            SqlCommand searchCommand = new SqlCommand(searchQuery, sqlcon);
            searchCommand.Parameters.AddWithValue("@id", id);

            // Thực thi câu lệnh SQL và đổ dữ liệu vào DataTable
            SqlDataAdapter adapter = new SqlDataAdapter(searchCommand);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            // Kiểm tra xem có dữ liệu được tìm thấy không
            if (dataTable.Rows.Count > 0)
            {
                // Hiển thị dữ liệu tìm kiếm lên đầu của DataGridView
                dataGridView1.DataSource = dataTable;
            }
            else
            {
                MessageBox.Show("Không tìm thấy sản phẩm với ID đã nhập.");
            }
        }

        private void bt_thoat_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Bạn có muốn thoát ứng dụng không?", "thông báo", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}
