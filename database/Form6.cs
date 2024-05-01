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
    public partial class frm6 : Form
    {
        public frm6()
        {
            InitializeComponent();
        }

        private void Form6_Load(object sender, EventArgs e)
        {

            string connectionString = ConfigurationManager.ConnectionStrings["MyDBConnectionString"].ConnectionString;
            // Khởi tạo kết nối
            SqlConnection connection = new SqlConnection(connectionString);

            // Mở kết nối
            connection.Open();

            // Truy vấn dữ liệu từ bảng trong CSDL
            string query = "SELECT * FROM nhapxuat";
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

        private void bt_nhap_Click(object sender, EventArgs e)
        {
            // Lấy thông tin từ các ô nhập liệu
            string idhoadon = idphieu.Text.Trim();
            string idhang = idhanghoa.Text.Trim();
            string soluongg = sl.Text.Trim();
            string ngaynhaphang = ngaynhap.Text.Trim();
            string kieudonhang = kieudon.Text.Trim();

            // Kiểm tra xem các ô nhập liệu có rỗng không
            if (string.IsNullOrEmpty(idhoadon) || string.IsNullOrEmpty(idhang) || string.IsNullOrEmpty(soluongg) || string.IsNullOrEmpty(ngaynhaphang) || string.IsNullOrEmpty(kieudonhang))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra xem số lượng nhập vào có phải là số không
            if (!int.TryParse(soluongg, out int soluong))
            {
                MessageBox.Show("Số lượng nhập vào không hợp lệ. Vui lòng nhập số nguyên dương.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Kiểm tra xem id phiếu có trùng không
            string connectionString = ConfigurationManager.ConnectionStrings["MyDBConnectionString"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);
            string checkDuplicateQuery = "SELECT COUNT(*) FROM nhapxuat WHERE idhoadon = @idhoadon";
            SqlCommand checkDuplicateCommand = new SqlCommand(checkDuplicateQuery, connection);
            checkDuplicateCommand.Parameters.AddWithValue("@idhoadon", idhoadon);

            try
            {
                connection.Open();
                int duplicateCount = (int)checkDuplicateCommand.ExecuteScalar();
                if (duplicateCount > 0)
                {
                    MessageBox.Show("ID phiếu đã tồn tại. Vui lòng chọn một ID phiếu khác.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }

            // Tạo câu lệnh SQL INSERT cho bảng nhập xuất
            string insertQuery = "INSERT INTO nhapxuat (idhoadon, idhanghoa, ngaynhapxuat, kieudon, soluong) VALUES (@idhoadon, @idhang, @ngaynhap, @kieudon, @soluong)";

            // Tạo câu lệnh SQL UPDATE để cập nhật số lượng hàng hóa trong bảng hanghoa
            string updateQuantityQuery = "UPDATE qlhanghoa SET sl = sl + @soluong WHERE id = @idhang";

            try
            {
                connection.Open();

                // Cập nhật số lượng hàng hóa trong bảng hanghoa
                SqlCommand updateQuantityCommand = new SqlCommand(updateQuantityQuery, connection);
                updateQuantityCommand.Parameters.AddWithValue("@idhang", idhang);
                updateQuantityCommand.Parameters.AddWithValue("@soluong", soluong);
                updateQuantityCommand.ExecuteNonQuery();

                // Thêm mới phiếu nhập hàng vào bảng nhập xuất
                SqlCommand insertCommand = new SqlCommand(insertQuery, connection);
                insertCommand.Parameters.AddWithValue("@idhoadon", idhoadon);
                insertCommand.Parameters.AddWithValue("@idhang", idhang);
                insertCommand.Parameters.AddWithValue("@ngaynhap", ngaynhaphang);
                insertCommand.Parameters.AddWithValue("@kieudon", kieudonhang);
                insertCommand.Parameters.AddWithValue("@soluong", soluong);
                insertCommand.ExecuteNonQuery();

                MessageBox.Show("Nhập hàng thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Tải lại dữ liệu từ cơ sở dữ liệu để cập nhật lên DataGridView (nếu cần)
                string reloadQuery = "SELECT * FROM nhapxuat";
                SqlDataAdapter adapter = new SqlDataAdapter(reloadQuery, connection);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void idphieu_TextChanged(object sender, EventArgs e)
        {

        }

        private void bt_them_Click(object sender, EventArgs e)
        {

        }

        private void bt_xoa_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem có hàng nào được chọn trong DataGridView không
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Lấy ID hàng hóa từ hàng được chọn
                string idhanghoa = dataGridView1.SelectedRows[0].Cells["id"].Value.ToString();

                // Hiển thị MessageBox xác nhận xóa
                DialogResult result = MessageBox.Show($"Bạn có chắc chắn muốn xóa hàng hóa có ID {idhanghoa}?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Kết nối cơ sở dữ liệu
                    string connectionString = ConfigurationManager.ConnectionStrings["MyDBConnectionString"].ConnectionString;
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        try
                        {
                            connection.Open();

                            // Tạo câu lệnh SQL để xóa hàng hóa
                            string deleteQuery = "DELETE FROM qlhanghoa WHERE id = @idhanghoa";
                            SqlCommand deleteCommand = new SqlCommand(deleteQuery, connection);
                            deleteCommand.Parameters.AddWithValue("@idhanghoa", idhanghoa);

                            int rowsAffected = deleteCommand.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show($"Đã xóa hàng hóa có ID {idhanghoa} thành công.");
                                // Refresh DataGridView sau khi xóa
                                string refreshQuery = "SELECT * FROM qlhanghoa";
                                SqlDataAdapter adapter = new SqlDataAdapter(refreshQuery, connection);
                                DataTable dataTable = new DataTable();
                                adapter.Fill(dataTable);
                                dataGridView1.DataSource = dataTable;
                            }
                            else
                            {
                                MessageBox.Show("Xóa hàng hóa thất bại.");
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một hàng hóa trong bảng để xóa.");
            }
        }

        private void bt_xuat_Click(object sender, EventArgs e)
        {

        }
    }
}
