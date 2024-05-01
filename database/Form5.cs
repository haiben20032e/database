using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace database
{
    public partial class frm5 : Form
    {

        private string connectionString;
        private SqlConnection sqlcon;
        private string gioitinh;

        public frm5()
        {
            InitializeComponent();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyDBConnectionString"].ConnectionString;
            // Khởi tạo kết nối
            SqlConnection connection = new SqlConnection(connectionString);

            // Mở kết nối
            connection.Open();

            // Truy vấn dữ liệu từ bảng trong CSDL
            string query = "SELECT * FROM nhanvien";
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

        private void bt_them_Click(object sender, EventArgs e)
        {
            string idnhanvien = txt_id.Text.Trim();
            string tennv = txt_tennv.Text.Trim();
            string namsinh = txt_namsinh.Text.Trim();
            string sodt = txt_sdt.Text.Trim();
            string chucvu = txt_chucvu.Text.Trim();
            string luong = txt_luong.Text.Trim();
            string diachi = txt_diachi.Text.Trim();
            int id;
            if (!int.TryParse(idnhanvien, out id))
            {
                MessageBox.Show("ID khách hàng phải là số nguyên.");
                return;
            }

            int sdt;
            if (!int.TryParse(sodt, out sdt))
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
                MessageBox.Show("Vui lòng chọn giới tính của nhân viên.");
                return;
            }

            // Check if input fields are empty
            if (string.IsNullOrEmpty(idnhanvien) || string.IsNullOrEmpty(tennv) || string.IsNullOrEmpty(namsinh) ||
                string.IsNullOrEmpty(sodt) || string.IsNullOrEmpty(chucvu) || string.IsNullOrEmpty(luong) ||
                string.IsNullOrEmpty(diachi))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin.");
                return;
            }

            int luongValue;
            if (!int.TryParse(luong, out luongValue))
            {
                MessageBox.Show("Lương phải là số nguyên.");
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

            // Check for duplicate employee ID
            string checkQuery = "SELECT COUNT(*) FROM nhanvien WHERE id = @idnhanvien";
            SqlCommand checkCommand = new SqlCommand(checkQuery, sqlcon);
            checkCommand.Parameters.AddWithValue("@idnhanvien", idnhanvien);
            int existingCount = (int)checkCommand.ExecuteScalar();

            if (existingCount > 0)
            {
                MessageBox.Show("ID nhân viên đã tồn tại trong cơ sở dữ liệu.");
                return; // Don't proceed if ID already exists
            }

            // Add new employee to the database
            string insertQuery = "INSERT INTO nhanvien (id, ten, namsinh, sdt, chucvu, luong, diachi, gioitinh) " +
                                 "VALUES (@idnhanvien, @tennv, @namsinh, @sodt, @chucvu, @luong, @diachi, @gioitinh)";
            SqlCommand insertCommand = new SqlCommand(insertQuery, sqlcon);
            insertCommand.Parameters.AddWithValue("@idnhanvien", idnhanvien);
            insertCommand.Parameters.AddWithValue("@tennv", tennv);
            insertCommand.Parameters.AddWithValue("@namsinh", namsinh);
            insertCommand.Parameters.AddWithValue("@sodt", sodt);
            insertCommand.Parameters.AddWithValue("@chucvu", chucvu);
            insertCommand.Parameters.AddWithValue("@luong", luongValue);
            insertCommand.Parameters.AddWithValue("@diachi", diachi);
            insertCommand.Parameters.AddWithValue("@gioitinh", gioitinh);

            int rowsAffected = insertCommand.ExecuteNonQuery();

            if (rowsAffected > 0)
            {
                MessageBox.Show("Thêm nhân viên thành công.");
                // Clear input fields after successful addition
                txt_id.Text = "";
                txt_tennv.Text = "";
                txt_namsinh.Text = "";
                txt_sdt.Text = "";
                txt_chucvu.Text = "";
                txt_luong.Text = "";
                txt_diachi.Text = "";

                // Update DataGridView after successful addition
                string refreshQuery = "SELECT * FROM nhanvien";
                SqlCommand refreshCommand = new SqlCommand(refreshQuery, sqlcon);
                SqlDataAdapter adapter = new SqlDataAdapter(refreshCommand);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
            }
            else
            {
                MessageBox.Show("Thêm nhân viên thất bại.");
            }
        }


        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void bt_sua_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem đã nhập ID nhân viên chưa
            if (string.IsNullOrEmpty(txt_id.Text))
            {
                MessageBox.Show("Vui lòng nhập ID nhân viên để cập nhật.");
                return;
            }

            // Kiểm tra xem ID nhân viên nhập vào có phải là số không
            int id;
            if (!int.TryParse(txt_id.Text, out id))
            {
                MessageBox.Show("ID nhân viên phải là một số nguyên.");
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

            string idnhanvien = txt_id.Text;

            // Kiểm tra sự tồn tại của ID nhân viên trong cơ sở dữ liệu
            string queryCheckExist = "SELECT COUNT(*) FROM nhanvien WHERE id = @idnhanvien";
            SqlCommand cmdCheckExist = new SqlCommand(queryCheckExist, sqlcon);
            cmdCheckExist.Parameters.AddWithValue("@idnhanvien", idnhanvien);
            int count = (int)cmdCheckExist.ExecuteScalar();

            // Nếu ID nhân viên không tồn tại, hiển thị thông báo lỗi và thoát
            if (count == 0)
            {
                MessageBox.Show("ID nhân viên không tồn tại trong cơ sở dữ liệu.");
                return;
            }

            // Tạo câu lệnh SQL UPDATE và cập nhật dữ liệu trong bảng nhanvien
            string query = "UPDATE nhanvien SET ";
            bool hasUpdate = false;

            // Kiểm tra và cập nhật thông tin cần thiết
            if (!string.IsNullOrEmpty(txt_tennv.Text))
            {
                query += "ten = @tennv, ";
                hasUpdate = true;
            }
            if (!string.IsNullOrEmpty(txt_namsinh.Text))
            {
                query += "namsinh = @namsinh, ";
                hasUpdate = true;
            }
            if (!string.IsNullOrEmpty(txt_sdt.Text))
            {
                query += "sdt = @sdt, ";
                hasUpdate = true;
            }
            if (!string.IsNullOrEmpty(txt_chucvu.Text))
            {
                query += "chucvu = @chucvu, ";
                hasUpdate = true;
            }
            if (!string.IsNullOrEmpty(txt_luong.Text))
            {
                query += "luong = @luong, ";
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
                query = query.TrimEnd(' ', ',') + " WHERE id = @idnhanvien";

                SqlCommand sqlcmd = new SqlCommand(query, sqlcon);
                sqlcmd.Parameters.AddWithValue("@idnhanvien", idnhanvien);
                if (!string.IsNullOrEmpty(txt_tennv.Text))
                    sqlcmd.Parameters.AddWithValue("@tennv", txt_tennv.Text);
                if (!string.IsNullOrEmpty(txt_namsinh.Text))
                    sqlcmd.Parameters.AddWithValue("@namsinh", txt_namsinh.Text);
                if (!string.IsNullOrEmpty(txt_sdt.Text))
                    sqlcmd.Parameters.AddWithValue("@sdt", txt_sdt.Text);
                if (!string.IsNullOrEmpty(txt_chucvu.Text))
                    sqlcmd.Parameters.AddWithValue("@chucvu", txt_chucvu.Text);
                if (!string.IsNullOrEmpty(txt_luong.Text))
                    sqlcmd.Parameters.AddWithValue("@luong", txt_luong.Text);
                if (!string.IsNullOrEmpty(txt_diachi.Text))
                    sqlcmd.Parameters.AddWithValue("@diachi", txt_diachi.Text);
                if (nam.Checked == true)
                    sqlcmd.Parameters.AddWithValue("@gioitinh", "nam");
                else if (nu.Checked == true)
                    sqlcmd.Parameters.AddWithValue("@gioitinh", "nữ");
                int rowsAffected = sqlcmd.ExecuteNonQuery();

                // Hiển thị thông báo kết quả cập nhật nhân viên
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Cập nhật thông tin nhân viên thành công!");
                    // Clear input fields after successful addition
                    txt_id.Text = "";
                    txt_tennv.Text = "";
                    txt_namsinh.Text = "";
                    txt_sdt.Text = "";
                    txt_chucvu.Text = "";
                    txt_luong.Text = "";
                    txt_diachi.Text = "";
                    nam.Checked = false;
                    nu.Checked = false;

                    // Update DataGridView after successful addition
                    string refreshQuery = "SELECT * FROM nhanvien";
                    SqlCommand refreshCommand = new SqlCommand(refreshQuery, sqlcon);
                    SqlDataAdapter adapter = new SqlDataAdapter(refreshCommand);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dataGridView1.DataSource = dataTable;
                }
                else
                {
                    MessageBox.Show("Cập nhật thông tin nhân viên thất bại!");
                }
            }
            else
            {
                MessageBox.Show("Không có dữ liệu để cập nhật.");
            }
        }


        private void bt_xoa_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem có hàng nào được chọn trong DataGridView không
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Lấy ID nhân viên từ hàng được chọn
                string idnhanvien = dataGridView1.SelectedRows[0].Cells["id"].Value.ToString();

                // Hiển thị MessageBox xác nhận xóa
                DialogResult result = MessageBox.Show($"Bạn có chắc chắn muốn xóa nhân viên có ID {idnhanvien}?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

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

                    // Tạo câu lệnh SQL để xóa nhân viên
                    string deleteQuery = "DELETE FROM nhanvien WHERE id = @idnhanvien";
                    SqlCommand deleteCommand = new SqlCommand(deleteQuery, sqlcon);
                    deleteCommand.Parameters.AddWithValue("@idnhanvien", idnhanvien);

                    int rowsAffected = deleteCommand.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show($"Đã xóa nhân viên có ID {idnhanvien} thành công.");
                        // Refresh DataGridView sau khi xóa
                        string refreshQuery = "SELECT * FROM nhanvien";
                        SqlCommand refreshCommand = new SqlCommand(refreshQuery, sqlcon);
                        SqlDataAdapter adapter = new SqlDataAdapter(refreshCommand);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        dataGridView1.DataSource = dataTable;
                    }
                    else
                    {
                        MessageBox.Show("Xóa nhân viên thất bại.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một nhân viên trong bảng để xóa.");
            }
        }

        private void bt_tim_Click(object sender, EventArgs e)
        {
            // Lấy từ khóa tìm kiếm từ textbox
            string keyword = txt_id.Text.Trim();

            // Kiểm tra xem từ khóa tìm kiếm có được nhập không
            if (string.IsNullOrEmpty(keyword))
            {
                MessageBox.Show("Vui lòng nhập mã nhân viên để tiến hành tìm kiếm ! .");
                return;
            }

            // Khởi tạo câu lệnh SQL tìm kiếm
            string searchQuery = "SELECT * FROM nhanvien WHERE id LIKE @keyword OR ten LIKE @keyword OR chucvu LIKE @keyword OR diachi LIKE @keyword";

            // Mở kết nối nếu cần
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

            // Thực thi câu lệnh SQL tìm kiếm
            SqlCommand searchCommand = new SqlCommand(searchQuery, sqlcon);
            searchCommand.Parameters.AddWithValue("@keyword", "%" + keyword + "%");
            SqlDataAdapter adapter = new SqlDataAdapter(searchCommand);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            // Kiểm tra kết quả tìm kiếm
            if (dataTable.Rows.Count > 0)
            {
                // Hiển thị kết quả tìm kiếm trong DataGridView
                dataGridView1.DataSource = dataTable;
            }
            else
            {
                MessageBox.Show("Không tìm thấy kết quả nào.");
            }
        }

        private void bt_in_Click(object sender, EventArgs e)
        {
            // Sử dụng PrintDocument để in nội dung DataGridView
            PrintDocument printDoc = new PrintDocument();
            printDoc.DocumentName = "Danh sách nhân viên";

            // Sử dụng PrintPreviewDialog để xem trước trước khi in
            PrintPreviewDialog previewDlg = new PrintPreviewDialog();
            previewDlg.Document = printDoc;

            // Tạo sự kiện in
            printDoc.PrintPage += (s, ev) =>
            {
                // Vẽ DataGridView
                Bitmap bm = new Bitmap(this.dataGridView1.Width, this.dataGridView1.Height);
                this.dataGridView1.DrawToBitmap(bm, new Rectangle(0, 0, this.dataGridView1.Width, this.dataGridView1.Height));
                ev.Graphics.DrawImage(bm, 0, 0);
            };

            // Hiển thị trước khi in
            previewDlg.ShowDialog();
        }

        private void bt_thoat_Click(object sender, EventArgs e)
        {
            // Đóng form
            this.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
