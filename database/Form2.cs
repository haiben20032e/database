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
    public partial class frm2 : Form
    {
        private string connectionString;
        private SqlConnection sqlcon;

        public frm2()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void frm2_Load(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyDBConnectionString"].ConnectionString;
            // Khởi tạo kết nối
            SqlConnection connection = new SqlConnection(connectionString);

            // Mở kết nối
            connection.Open();

            // Truy vấn dữ liệu từ bảng trong CSDL
            string query = "SELECT * FROM khachhang1";
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

        private void nu_CheckedChanged(object sender, EventArgs e)
        {

        }

        

        private void bt_xoa_Click_1(object sender, EventArgs e)
        {
            // Kiểm tra xem có hàng nào được chọn trong DataGridView không
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Lấy ID khách hàng từ hàng được chọn
                string idkhachhang = dataGridView1.SelectedRows[0].Cells["id"].Value.ToString();

                // Hiển thị MessageBox xác nhận xóa
                DialogResult result = MessageBox.Show($"Bạn có chắc chắn muốn xóa khách hàng có ID {idkhachhang}?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

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

                    // Tạo câu lệnh SQL để xóa khách hàng
                    string deleteQuery = "DELETE FROM khachhang1 WHERE id = @idkhachhang";
                    SqlCommand deleteCommand = new SqlCommand(deleteQuery, sqlcon);
                    deleteCommand.Parameters.AddWithValue("@idkhachhang", idkhachhang);

                    int rowsAffected = deleteCommand.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show($"Đã xóa khách hàng có ID {idkhachhang} thành công.");
                        // Refresh DataGridView sau khi xóa
                        string refreshQuery = "SELECT * FROM khachhang1";
                        SqlCommand refreshCommand = new SqlCommand(refreshQuery, sqlcon);
                        SqlDataAdapter adapter = new SqlDataAdapter(refreshCommand);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        dataGridView1.DataSource = dataTable;
                    }
                    else
                    {
                        MessageBox.Show("Xóa khách hàng thất bại.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một khách hàng trong bảng để xóa");
            }
        }
        private void bt_update_Click_1(object sender, EventArgs e)
        {
            // Kiểm tra xem đã nhập ID khách hàng chưa
            if (string.IsNullOrEmpty(txt_id.Text))
            {
                MessageBox.Show("Vui lòng nhập ID khách hàng để cập nhật.");
                return;
            }
            if (sqlcon == null)
            {
                // Khởi tạo kết nối
                connectionString = ConfigurationManager.ConnectionStrings["MyDBConnectionString"].ConnectionString;
                sqlcon = new SqlConnection(connectionString);
            }

            // Kiểm tra trạng thái kết nối, mở nếu cần
            if (sqlcon.State != ConnectionState.Open)
            {
                sqlcon.Open();
            }

            // Tiếp tục với các thao tác SqlCommand ở đây

            string idkhachhang = txt_id.Text;

            // Tạo câu lệnh SQL SELECT để kiểm tra xem ID khách hàng tồn tại hay không
            string queryCheckExist = "SELECT COUNT(*) FROM khachhang1 WHERE id = @idkhachhang";
            SqlCommand cmdCheckExist = new SqlCommand(queryCheckExist, sqlcon);
            cmdCheckExist.Parameters.AddWithValue("@idkhachhang", idkhachhang);
            int count = (int)cmdCheckExist.ExecuteScalar();

            // Nếu ID khách hàng không tồn tại, báo lỗi và thoát khỏi phương thức
            if (count == 0)
            {
                MessageBox.Show("ID khách hàng không tồn tại trong cơ sở dữ liệu.");
                return;
            }

            // Tạo câu lệnh SQL UPDATE và cập nhật dữ liệu trong bảng khachhang1
            string query = "UPDATE khachhang1 SET ";
            bool hasUpdate = false;

            if (!string.IsNullOrEmpty(txt_ten.Text))
            {
                query += "tenkh = @hoten, ";
                hasUpdate = true;
            }
            if (!string.IsNullOrEmpty(txt_ngaysinh.Text))
            {
                query += "ngaysinh = @ngaysinh, ";
                hasUpdate = true;
            }
            if (!string.IsNullOrEmpty(txt_sdt.Text))
            {
                query += "sdt = @phone, ";
                hasUpdate = true;
            }
            if (!string.IsNullOrEmpty(txt_diachi.Text))
            {
                query += "diachi = @diachi, ";
                hasUpdate = true;
            }
            if (nam.Checked == true || nu.Checked == true)
            {
                query += "gioitinh = @gioitinh, ";
                hasUpdate = true;
            }

            // Kiểm tra xem có dữ liệu để cập nhật không
            if (hasUpdate)
            {
                // Loại bỏ dấu phẩy cuối cùng và thêm điều kiện WHERE vào câu lệnh UPDATE
                query = query.TrimEnd(' ', ',') + " WHERE id = @idkhachhang";

                SqlCommand sqlcmd = new SqlCommand(query, sqlcon);
                sqlcmd.Parameters.AddWithValue("@idkhachhang", idkhachhang);
                if (!string.IsNullOrEmpty(txt_ten.Text))
                    sqlcmd.Parameters.AddWithValue("@hoten", txt_ten.Text);
                if (!string.IsNullOrEmpty(txt_ngaysinh.Text))
                    sqlcmd.Parameters.AddWithValue("@ngaysinh", txt_ngaysinh.Text);
                if (!string.IsNullOrEmpty(txt_sdt.Text))
                    sqlcmd.Parameters.AddWithValue("@phone", txt_sdt.Text);
                if (!string.IsNullOrEmpty(txt_diachi.Text))
                    sqlcmd.Parameters.AddWithValue("@diachi", txt_diachi.Text);
                if (nam.Checked == true)
                    sqlcmd.Parameters.AddWithValue("@gioitinh", 1);
                else if (nu.Checked == true)
                    sqlcmd.Parameters.AddWithValue("@gioitinh", 0);

                int rowsAffected = sqlcmd.ExecuteNonQuery();

                // Hiển thị thông báo kết quả cập nhật khách hàng
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Cập nhật thông tin khách hàng thành công!");
                    // Refresh DataGridView
                    string refreshQuery = "SELECT * FROM khachhang1";
                    SqlCommand refreshCommand = new SqlCommand(refreshQuery, sqlcon);
                    SqlDataAdapter adapter = new SqlDataAdapter(refreshCommand);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dataGridView1.DataSource = dataTable;
                }
                else
                {
                    MessageBox.Show("Cập nhật thông tin khách hàng thất bại!");
                }
            }
            else
            {
                MessageBox.Show("Không có dữ liệu để cập nhật.");
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            
            {
                this.Close();
            }
        }

        private void bt_them_Click(object sender, EventArgs e)
        {
            string idkhachhang = txt_id.Text.Trim();
            string hoten = txt_ten.Text.Trim();
            string ngaysinh = txt_ngaysinh.Text.Trim();
            string phone = txt_sdt.Text.Trim();
            string diachi = txt_diachi.Text.Trim();

            // kieermtra xem số nhập vào phải số nguyên không

            int id;
            if (!int.TryParse(idkhachhang, out id))
            {
                MessageBox.Show("ID khách hàng phải là số nguyên.");
                return;
            }

            int sdt;
            if (!int.TryParse(phone, out sdt))
            {
                MessageBox.Show("Số điện thoại phải là số nguyên.");
                return;
            }
            string gioitinh = "";

            if (nam.Checked == true)
            {
                gioitinh = "nam";

            }
            else if (nu.Checked == true)
            {
                gioitinh = "nữ";
            }
            else
            {
                MessageBox.Show("Vui lòng chọn giới tính của khách hàng.");
                return;
            }
            // Kiểm tra kết nối và mở trạng thái của kết nối
            if (sqlcon == null)
            {
                // Khởi tạo kết nối
                connectionString = ConfigurationManager.ConnectionStrings["MyDBConnectionString"].ConnectionString;
                sqlcon = new SqlConnection(connectionString);
            }

            // Kiểm tra trạng thái kết nối, mở nếu cần
            if (sqlcon.State != ConnectionState.Open)
            {
                sqlcon.Open();
            }

            // Kiểm tra ID khách hàng trùng lặp
            string checkQuery = "SELECT COUNT(*) FROM khachhang1 WHERE id = @idkhachhang";
            SqlCommand checkCommand = new SqlCommand(checkQuery, sqlcon);
            checkCommand.Parameters.AddWithValue("@idkhachhang", idkhachhang);
            int existingCount = (int)checkCommand.ExecuteScalar();

            if (existingCount > 0)
            {
                MessageBox.Show("ID khách hàng đã tồn tại trong cơ sở dữ liệu.");
                return; // Không thực hiện thêm vào cơ sở dữ liệu nếu ID đã tồn tại
            }

            // Thêm mới khách hàng vào cơ sở dữ liệu
            string insertQuery = "INSERT INTO khachhang1 (id,tenkh,ngaysinh,sdt, diachi, gioitinh) " +
                                 "VALUES (@idkhachhang, @hoten, @ngaysinh, @phone, @diachi, @gioitinh)";
            SqlCommand insertCommand = new SqlCommand(insertQuery, sqlcon);
            insertCommand.Parameters.AddWithValue("@idkhachhang", idkhachhang);
            insertCommand.Parameters.AddWithValue("@hoten", hoten);
            insertCommand.Parameters.AddWithValue("@ngaysinh", ngaysinh);
            insertCommand.Parameters.AddWithValue("@phone", phone);
            insertCommand.Parameters.AddWithValue("@diachi", diachi);
            insertCommand.Parameters.AddWithValue("@gioitinh", gioitinh);

            int rowsAffected = insertCommand.ExecuteNonQuery();

            if (rowsAffected > 0)
            {
                MessageBox.Show("Thêm khách hàng thành công.");
                // Xóa dữ liệu đã nhập từ các ô giao diện
                txt_id.Text = "";
                txt_ten.Text = "";
                txt_ngaysinh.Text = "";
                txt_sdt.Text = "";
                txt_diachi.Text = "";
                nam.Checked = false;
                nu.Checked = false;
                // Cập nhật lại DataGridView sau khi thêm thành công
                string refreshQuery = "SELECT * FROM khachhang1";
                SqlCommand refreshCommand = new SqlCommand(refreshQuery, sqlcon);
                SqlDataAdapter adapter = new SqlDataAdapter(refreshCommand);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
            }
            else
            {
                MessageBox.Show("Thêm khách hàng thất bại.");
            }
        }
    }
}
