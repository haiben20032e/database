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
    public partial class frm7 : Form
    {
        public frm7()
        {
            InitializeComponent();
        }

        private void bt_them_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem các trường nhập liệu có rỗng không
            if (string.IsNullOrWhiteSpace(idphieu.Text) ||
                string.IsNullOrWhiteSpace(ngayxuat.Text) ||
                string.IsNullOrWhiteSpace(nv.Text) ||
                string.IsNullOrWhiteSpace(idhanghoa.Text) ||
                string.IsNullOrWhiteSpace(dongia.Text) ||
                string.IsNullOrWhiteSpace(sl.Text) ||
                string.IsNullOrWhiteSpace(idkhachhang.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.");
                return;
            }

            // Lấy dữ liệu từ các trường nhập
            string idphieuu = idphieu.Text.Trim();
            string ngayxuathanghoa = ngayxuat.Text.Trim();
            string manhanvien = nv.Text.Trim();
            string idhanghoaa = idhanghoa.Text.Trim();
            string gia = dongia.Text.Trim();
            string soluongxuat = sl.Text.Trim();
            string idkhachhangg = idkhachhang.Text.Trim();

            string connectionString = ConfigurationManager.ConnectionStrings["MyDBConnectionString"].ConnectionString;

            // Khởi tạo kết nối
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Mở kết nối
                connection.Open();

                // Bắt đầu một giao dịch
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    // Thêm dữ liệu vào bảng phieuxuat
                    string insertPhieuXuatQuery = "INSERT INTO pxuat (idphieuxuat, ngayxuat, idnhanvien, idkhachhang) VALUES (@idphieu, @ngayxuat, @manv, @idkhachhang)";
                    SqlCommand insertPhieuXuatCommand = new SqlCommand(insertPhieuXuatQuery, connection, transaction);
                    insertPhieuXuatCommand.Parameters.AddWithValue("@idphieu", idphieuu);
                    insertPhieuXuatCommand.Parameters.AddWithValue("@ngayxuat", ngayxuathanghoa);
                    insertPhieuXuatCommand.Parameters.AddWithValue("@manv", manhanvien);
                    insertPhieuXuatCommand.Parameters.AddWithValue("@idkhachhang", idkhachhangg);
                    insertPhieuXuatCommand.ExecuteNonQuery();

                    // Thêm dữ liệu vào bảng ctphieuxuat
                    string insertChiTietPhieuXuatQuery = "INSERT INTO ctpxuat (idphieuxuat, idhanghoa, soluongxuat, giaxuat) VALUES (@idphieu, @idhanghoa, @soluongxuat, @gia)";
                    SqlCommand insertChiTietPhieuXuatCommand = new SqlCommand(insertChiTietPhieuXuatQuery, connection, transaction);
                    insertChiTietPhieuXuatCommand.Parameters.AddWithValue("@idphieu", idphieuu);
                    insertChiTietPhieuXuatCommand.Parameters.AddWithValue("@idhanghoa", idhanghoaa);
                    insertChiTietPhieuXuatCommand.Parameters.AddWithValue("@soluongxuat", soluongxuat);
                    insertChiTietPhieuXuatCommand.Parameters.AddWithValue("@gia", gia);
                    insertChiTietPhieuXuatCommand.ExecuteNonQuery();

                    // Commit transaction nếu mọi thứ thành công
                    transaction.Commit();
                    MessageBox.Show("Xuất hàng thành công!");

                    // Refresh DataGridView
                    Form7_Load(sender, e);
                }
                catch (Exception ex)
                {
                    // Rollback transaction nếu có lỗi xảy ra
                    transaction.Rollback();
                    MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
                }
            }
        }

        private void Form7_Load(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyDBConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = @"SELECT p.idphieuxuat, p.ngayxuat, p.idnhanvien, p.idkhachhang, c.idhanghoa, c.soluongxuat, c.giaxuat
                         FROM pxuat p
                         INNER JOIN ctpxuat c ON p.idphieuxuat = c.idphieuxuat";

                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                dataGridView1.DataSource = dataTable;
            }
        }

        private void bt_xuat_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem các trường nhập liệu có rỗng không
            if (string.IsNullOrWhiteSpace(idphieu.Text) ||
                string.IsNullOrWhiteSpace(ngayxuat.Text) ||
                string.IsNullOrWhiteSpace(nv.Text) ||
                string.IsNullOrWhiteSpace(idhanghoa.Text) ||
                string.IsNullOrWhiteSpace(dongia.Text) ||
                string.IsNullOrWhiteSpace(sl.Text) ||
                string.IsNullOrWhiteSpace(idkhachhang.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.");
                return;
            }

            // Lấy dữ liệu từ các trường nhập
            string idphieuu = idphieu.Text.Trim();
            string ngayxuathanghoa = ngayxuat.Text.Trim();
            string manhanvien = nv.Text.Trim();
            string idhanghoaa = idhanghoa.Text.Trim();
            string gia = dongia.Text.Trim();
            string soluongxuat = sl.Text.Trim();
            string idkhachhangg = idkhachhang.Text.Trim();

            string connectionString = ConfigurationManager.ConnectionStrings["MyDBConnectionString"].ConnectionString;

            // Khởi tạo kết nối
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Mở kết nối
                connection.Open();

                // Bắt đầu một giao dịch
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    // Thêm dữ liệu vào bảng phieuxuat
                    string insertPhieuXuatQuery = "INSERT INTO pxuat (idphieuxuat, ngayxuat, idnhanvien, idkhachhang) VALUES (@idphieu, @ngayxuat, @manv, @idkhachhang)";
                    SqlCommand insertPhieuXuatCommand = new SqlCommand(insertPhieuXuatQuery, connection, transaction);
                    insertPhieuXuatCommand.Parameters.AddWithValue("@idphieu", idphieuu);
                    insertPhieuXuatCommand.Parameters.AddWithValue("@ngayxuat", ngayxuathanghoa);
                    insertPhieuXuatCommand.Parameters.AddWithValue("@manv", manhanvien);
                    insertPhieuXuatCommand.Parameters.AddWithValue("@idkhachhang", idkhachhangg);
                    insertPhieuXuatCommand.ExecuteNonQuery();

                    // Thêm dữ liệu vào bảng ctphieuxuat
                    string insertChiTietPhieuXuatQuery = "INSERT INTO ctpxuat (idphieuxuat, idhanghoa, soluongxuat, giaxuat) VALUES (@idphieu, @idhanghoa, @soluongxuat, @gia)";
                    SqlCommand insertChiTietPhieuXuatCommand = new SqlCommand(insertChiTietPhieuXuatQuery, connection, transaction);
                    insertChiTietPhieuXuatCommand.Parameters.AddWithValue("@idphieu", idphieuu);
                    insertChiTietPhieuXuatCommand.Parameters.AddWithValue("@idhanghoa", idhanghoaa);
                    insertChiTietPhieuXuatCommand.Parameters.AddWithValue("@soluongxuat", soluongxuat);
                    insertChiTietPhieuXuatCommand.Parameters.AddWithValue("@gia", gia);
                    insertChiTietPhieuXuatCommand.ExecuteNonQuery();

                    // Commit transaction nếu mọi thứ thành công
                    transaction.Commit();
                    MessageBox.Show("Xuất hàng thành công!");

                    // Refresh DataGridView
                    Form7_Load(sender, e);
                }
                catch (Exception ex)
                {
                    // Rollback transaction nếu có lỗi xảy ra
                    transaction.Rollback();
                    MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
                }
            }
        }

        private void bt_sua_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem idphieuxuat được nhập hay chưa
            if (string.IsNullOrWhiteSpace(idphieu.Text))
            {
                MessageBox.Show("Vui lòng nhập ID phiếu xuất.");
                return;
            }

            string idphieuxuatt = idphieu.Text.Trim();
            bool hasUpdatePxuat = false;
            bool hasUpdateCtpxuat = false;
            string updatePxuatQuery = "UPDATE pxuat SET ";
            string updateCtpxuatQuery = "UPDATE ctpxuat SET ";

            // Kiểm tra và cập nhật thông tin cần thiết cho bảng pxuat
            if (!string.IsNullOrWhiteSpace(ngayxuat.Text))
            {
                updatePxuatQuery += "ngayxuat = @ngayxuat, ";
                hasUpdatePxuat = true;
            }

            if (!string.IsNullOrWhiteSpace(nv.Text))
            {
                updatePxuatQuery += "idnhanvien = @manv, ";
                hasUpdatePxuat = true;
            }

            if (!string.IsNullOrWhiteSpace(idkhachhang.Text))
            {
                updatePxuatQuery += "idkhachhang = @idkhachhang, ";
                hasUpdatePxuat = true;
            }

            // Kiểm tra và cập nhật thông tin cần thiết cho bảng ctpxuat
            if (!string.IsNullOrWhiteSpace(idhanghoa.Text))
            {
                updateCtpxuatQuery += "idhanghoa = @idhanghoa, ";
                hasUpdateCtpxuat = true;
            }

            if (!string.IsNullOrWhiteSpace(sl.Text))
            {
                updateCtpxuatQuery += "soluongxuat = @soluongxuat, ";
                hasUpdateCtpxuat = true;
            }

            if (!string.IsNullOrWhiteSpace(dongia.Text))
            {
                updateCtpxuatQuery += "giaxuat = @giaxuat, ";
                hasUpdateCtpxuat = true;
            }

            // Xóa dấu phẩy và khoảng trắng thừa
            updatePxuatQuery = updatePxuatQuery.TrimEnd(',', ' ');
            updateCtpxuatQuery = updateCtpxuatQuery.TrimEnd(',', ' ');

            // Kiểm tra xem có cần cập nhật không
            if (!hasUpdatePxuat && !hasUpdateCtpxuat)
            {
                MessageBox.Show("Không có thông tin nào được cập nhật.");
                return;
            }

            updatePxuatQuery += " WHERE idphieuxuat = @idphieuxuat";
            updateCtpxuatQuery += " WHERE idphieuxuat = @idphieuxuat";

            string connectionString = ConfigurationManager.ConnectionStrings["MyDBConnectionString"].ConnectionString;

            // Khởi tạo kết nối
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Mở kết nối
                connection.Open();

                // Bắt đầu một giao dịch
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    // Cập nhật dữ liệu trong bảng pxuat
                    if (hasUpdatePxuat)
                    {
                        SqlCommand updatePhieuXuatCommand = new SqlCommand(updatePxuatQuery, connection, transaction);
                        updatePhieuXuatCommand.Parameters.AddWithValue("@idphieuxuat", idphieuxuatt);
                        if (!string.IsNullOrWhiteSpace(ngayxuat.Text))
                            updatePhieuXuatCommand.Parameters.AddWithValue("@ngayxuat", ngayxuat.Text.Trim());
                        if (!string.IsNullOrWhiteSpace(nv.Text))
                            updatePhieuXuatCommand.Parameters.AddWithValue("@manv", nv.Text.Trim());
                        if (!string.IsNullOrWhiteSpace(idkhachhang.Text))
                            updatePhieuXuatCommand.Parameters.AddWithValue("@idkhachhang", idkhachhang.Text.Trim());
                        updatePhieuXuatCommand.ExecuteNonQuery();
                    }

                    // Cập nhật dữ liệu trong bảng ctpxuat
                    if (hasUpdateCtpxuat)
                    {
                        SqlCommand updateChiTietPhieuXuatCommand = new SqlCommand(updateCtpxuatQuery, connection, transaction);
                        updateChiTietPhieuXuatCommand.Parameters.AddWithValue("@idphieuxuat", idphieuxuatt);
                        if (!string.IsNullOrWhiteSpace(idhanghoa.Text))
                            updateChiTietPhieuXuatCommand.Parameters.AddWithValue("@idhanghoa", idhanghoa.Text.Trim());
                        if (!string.IsNullOrWhiteSpace(sl.Text))
                            updateChiTietPhieuXuatCommand.Parameters.AddWithValue("@soluongxuat", sl.Text.Trim());
                        if (!string.IsNullOrWhiteSpace(dongia.Text))
                            updateChiTietPhieuXuatCommand.Parameters.AddWithValue("@giaxuat", dongia.Text.Trim());
                        updateChiTietPhieuXuatCommand.ExecuteNonQuery();
                    }

                    // Commit transaction nếu mọi thứ thành công
                    transaction.Commit();
                    MessageBox.Show("Cập nhật dữ liệu thành công!");

                    // Refresh DataGridView
                    Form7_Load(sender, e);
                }
                catch (Exception ex)
                {
                    // Rollback transaction nếu có lỗi xảy ra
                    transaction.Rollback();
                    MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
                }
            }
        }

        private void bt_xoa_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem người dùng đã chọn dòng dữ liệu trong DataGridView chưa
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Lấy ID phiếu xuất từ dòng được chọn
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                string idphieuu = selectedRow.Cells["idphieuxuat"].Value.ToString();

                // Hiển thị hộp thoại xác nhận
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa phiếu xuất này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    string connectionString = ConfigurationManager.ConnectionStrings["MyDBConnectionString"].ConnectionString;

                    // Khởi tạo kết nối
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        // Mở kết nối
                        connection.Open();

                        // Bắt đầu một giao dịch
                        SqlTransaction transaction = connection.BeginTransaction();

                        try
                        {
                            // Xóa dữ liệu trong bảng ctpxuat
                            string deleteChiTietPhieuXuatQuery = "DELETE FROM ctpxuat WHERE idphieuxuat = @idphieu";
                            SqlCommand deleteChiTietPhieuXuatCommand = new SqlCommand(deleteChiTietPhieuXuatQuery, connection, transaction);
                            deleteChiTietPhieuXuatCommand.Parameters.AddWithValue("@idphieu", idphieuu);
                            deleteChiTietPhieuXuatCommand.ExecuteNonQuery();

                            // Xóa dữ liệu trong bảng pxuat
                            string deletePhieuXuatQuery = "DELETE FROM pxuat WHERE idphieuxuat = @idphieu";
                            SqlCommand deletePhieuXuatCommand = new SqlCommand(deletePhieuXuatQuery, connection, transaction);
                            deletePhieuXuatCommand.Parameters.AddWithValue("@idphieu", idphieuu);
                            deletePhieuXuatCommand.ExecuteNonQuery();

                            // Commit transaction nếu mọi thứ thành công
                            transaction.Commit();
                            MessageBox.Show("Xóa phiếu xuất thành công!");

                            // Refresh DataGridView
                            Form7_Load(sender, e);
                        }
                        catch (Exception ex)
                        {
                            // Rollback transaction nếu có lỗi xảy ra
                            transaction.Rollback();
                            MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một dòng dữ liệu để xóa.");
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
    }
}
