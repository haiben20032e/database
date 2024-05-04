using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Net.NetworkInformation;
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

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT p.idphieunhap, p.ngaynhap, p.idnhanvien, p.thue, c.idhanghoa, c.soluongnhap, c.gianhap " +
                               "FROM pnhap p " +
                               "INNER JOIN ctpnhap c ON p.idphieunhap = c.idphieunhap";

                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                dataGridView1.DataSource = dataTable;
            }

        }
        private void bt_thoat_Click(object sender, EventArgs e)
        {
            // Đóng form
            this.Close();
        }

        private void bt_nhap_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem các trường nhập liệu có rỗng không
            if (string.IsNullOrWhiteSpace(idphieu.Text) ||
                string.IsNullOrWhiteSpace(ngaynhap.Text) ||
                string.IsNullOrWhiteSpace(nv.Text) ||
                string.IsNullOrWhiteSpace(idhanghoa.Text) ||
                string.IsNullOrWhiteSpace(dongia.Text) ||
                string.IsNullOrWhiteSpace(sl.Text) ||
                string.IsNullOrWhiteSpace(thue.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.");
                return;
            }

            // Lấy dữ liệu từ các trường nhập
            string idphieuu = idphieu.Text.Trim();
            string ngaynhaphanghoa = ngaynhap.Text.Trim();
            string manhanvien = nv.Text.Trim();
            string idhanghoaa = idhanghoa.Text.Trim();
            string gia = dongia.Text.Trim();
            string soluongnhap = sl.Text.Trim();
            string thuee = thue.Text.Trim();

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
                    // Thêm dữ liệu vào bảng pnhap
                    string insertPhieuNhapQuery = "INSERT INTO pnhap (idphieunhap, ngaynhap, idnhanvien, thue) VALUES (@idphieu, @ngaynhap, @manv, @thue)";
                    SqlCommand insertPhieuNhapCommand = new SqlCommand(insertPhieuNhapQuery, connection, transaction);
                    insertPhieuNhapCommand.Parameters.AddWithValue("@idphieu", idphieuu);
                    insertPhieuNhapCommand.Parameters.AddWithValue("@ngaynhap", ngaynhaphanghoa);
                    insertPhieuNhapCommand.Parameters.AddWithValue("@manv", manhanvien);
                    insertPhieuNhapCommand.Parameters.AddWithValue("@thue", thuee);
                    insertPhieuNhapCommand.ExecuteNonQuery();

                    // Thêm dữ liệu vào bảng ctpnhap
                    string insertChiTietPhieuNhapQuery = "INSERT INTO ctpnhap (idphieunhap, idhanghoa, soluongnhap, gianhap) VALUES (@idphieu, @idhanghoa, @soluongnhap, @gia)";
                    SqlCommand insertChiTietPhieuNhapCommand = new SqlCommand(insertChiTietPhieuNhapQuery, connection, transaction);
                    insertChiTietPhieuNhapCommand.Parameters.AddWithValue("@idphieu", idphieuu);
                    insertChiTietPhieuNhapCommand.Parameters.AddWithValue("@idhanghoa", idhanghoaa);
                    insertChiTietPhieuNhapCommand.Parameters.AddWithValue("@soluongnhap", soluongnhap);
                    insertChiTietPhieuNhapCommand.Parameters.AddWithValue("@gia", gia);
                    insertChiTietPhieuNhapCommand.ExecuteNonQuery();

                    // Commit transaction nếu mọi thứ thành công
                    transaction.Commit();
                    MessageBox.Show("Nhập hàng thành công!");
                    Form6_Load(sender, e);
                }
                catch (Exception ex)
                {
                    // Rollback transaction nếu có lỗi xảy ra
                    transaction.Rollback();
                    MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
                }
            }
        }



        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void bt_xoa_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem người dùng đã chọn dòng dữ liệu trong DataGridView chưa
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Lấy ID phiếu nhập từ dòng được chọn
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                string idphieuu = selectedRow.Cells["idphieunhap"].Value.ToString();

                // Hiển thị hộp thoại xác nhận
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa phiếu nhập này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

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
                            // Xóa dữ liệu trong bảng ctpnhap
                            string deleteChiTietPhieuNhapQuery = "DELETE FROM ctpnhap WHERE idphieunhap = @idphieu";
                            SqlCommand deleteChiTietPhieuNhapCommand = new SqlCommand(deleteChiTietPhieuNhapQuery, connection, transaction);
                            deleteChiTietPhieuNhapCommand.Parameters.AddWithValue("@idphieu", idphieuu);
                            deleteChiTietPhieuNhapCommand.ExecuteNonQuery();

                            // Xóa dữ liệu trong bảng pnhap
                            string deletePhieuNhapQuery = "DELETE FROM pnhap WHERE idphieunhap = @idphieu";
                            SqlCommand deletePhieuNhapCommand = new SqlCommand(deletePhieuNhapQuery, connection, transaction);
                            deletePhieuNhapCommand.Parameters.AddWithValue("@idphieu", idphieuu);
                            deletePhieuNhapCommand.ExecuteNonQuery();

                            // Commit transaction nếu mọi thứ thành công
                            transaction.Commit();
                            MessageBox.Show("Xóa phiếu nhập thành công!");

                            // Refresh DataGridView
                            Form6_Load(sender, e);
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

        private void bt_them_Click(object sender, EventArgs e)
        {  // Kiểm tra xem các trường nhập liệu có rỗng không
            if (string.IsNullOrWhiteSpace(idphieu.Text) ||
                string.IsNullOrWhiteSpace(ngaynhap.Text) ||
                string.IsNullOrWhiteSpace(nv.Text) ||
                string.IsNullOrWhiteSpace(idhanghoa.Text) ||
                string.IsNullOrWhiteSpace(dongia.Text) ||
                string.IsNullOrWhiteSpace(sl.Text) ||
                string.IsNullOrWhiteSpace(thue.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.");
                return;
            }

            // Lấy dữ liệu từ các trường nhập
            string idphieuu = idphieu.Text.Trim();
            string ngaynhaphanghoa = ngaynhap.Text.Trim();
            string manhanvien = nv.Text.Trim();
            string idhanghoaa = idhanghoa.Text.Trim();
            string gia = dongia.Text.Trim();
            string soluongnhap = sl.Text.Trim();
            string thuee = thue.Text.Trim();

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
                    // Thêm dữ liệu vào bảng pnhap
                    string insertPhieuNhapQuery = "INSERT INTO pnhap (idphieunhap, ngaynhap, idnhanvien, thue) VALUES (@idphieu, @ngaynhap, @manv, @thue)";
                    SqlCommand insertPhieuNhapCommand = new SqlCommand(insertPhieuNhapQuery, connection, transaction);
                    insertPhieuNhapCommand.Parameters.AddWithValue("@idphieu", idphieuu);
                    insertPhieuNhapCommand.Parameters.AddWithValue("@ngaynhap", ngaynhaphanghoa);
                    insertPhieuNhapCommand.Parameters.AddWithValue("@manv", manhanvien);
                    insertPhieuNhapCommand.Parameters.AddWithValue("@thue", thuee);
                    insertPhieuNhapCommand.ExecuteNonQuery();

                    // Thêm dữ liệu vào bảng ctpnhap
                    string insertChiTietPhieuNhapQuery = "INSERT INTO ctpnhap (idphieunhap, idhanghoa, soluongnhap, gianhap) VALUES (@idphieu, @idhanghoa, @soluongnhap, @gia)";
                    SqlCommand insertChiTietPhieuNhapCommand = new SqlCommand(insertChiTietPhieuNhapQuery, connection, transaction);
                    insertChiTietPhieuNhapCommand.Parameters.AddWithValue("@idphieu", idphieuu);
                    insertChiTietPhieuNhapCommand.Parameters.AddWithValue("@idhanghoa", idhanghoaa);
                    insertChiTietPhieuNhapCommand.Parameters.AddWithValue("@soluongnhap", soluongnhap);
                    insertChiTietPhieuNhapCommand.Parameters.AddWithValue("@gia", gia);
                    insertChiTietPhieuNhapCommand.ExecuteNonQuery();

                    // Commit transaction nếu mọi thứ thành công
                    transaction.Commit();
                    MessageBox.Show("Nhập hàng thành công!");
                    Form6_Load(sender, e);
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
            // Kiểm tra xem idphieu được nhập hay chưa
            if (string.IsNullOrWhiteSpace(idphieu.Text))
            {
                MessageBox.Show("Vui lòng nhập ID phiếu.");
                return;
            }

            string idphieuu = idphieu.Text.Trim();
            bool hasUpdatePnhap = false;
            bool hasUpdateCtpnhap = false;
            string updatePnhapQuery = "UPDATE pnhap SET ";
            string updateCtpnhapQuery = "UPDATE ctpnhap SET ";

            // Kiểm tra và cập nhật thông tin cần thiết cho bảng pnhap
            if (!string.IsNullOrWhiteSpace(ngaynhap.Text))
            {
                updatePnhapQuery += "ngaynhap = @ngaynhap, ";
                hasUpdatePnhap = true;
            }

            if (!string.IsNullOrWhiteSpace(nv.Text))
            {
                updatePnhapQuery += "idnhanvien = @manv, ";
                hasUpdatePnhap = true;
            }

            if (!string.IsNullOrWhiteSpace(thue.Text))
            {
                updatePnhapQuery += "thue = @thue, ";
                hasUpdatePnhap = true;
            }

            // Kiểm tra và cập nhật thông tin cần thiết cho bảng ctpnhap
            if (!string.IsNullOrWhiteSpace(idhanghoa.Text))
            {
                updateCtpnhapQuery += "idhanghoa = @idhanghoa, ";
                hasUpdateCtpnhap = true;
            }

            if (!string.IsNullOrWhiteSpace(sl.Text))
            {
                updateCtpnhapQuery += "soluongnhap = @soluongnhap, ";
                hasUpdateCtpnhap = true;
            }

            if (!string.IsNullOrWhiteSpace(dongia.Text))
            {
                updateCtpnhapQuery += "gianhap = @gia, ";
                hasUpdateCtpnhap = true;
            }

            // Xóa dấu phẩy và khoảng trắng thừa
            updatePnhapQuery = updatePnhapQuery.TrimEnd(',', ' ');
            updateCtpnhapQuery = updateCtpnhapQuery.TrimEnd(',', ' ');

            // Kiểm tra xem có cần cập nhật không
            if (!hasUpdatePnhap && !hasUpdateCtpnhap)
            {
                MessageBox.Show("Không có thông tin nào được cập nhật.");
                return;
            }

            updatePnhapQuery += " WHERE idphieunhap = @idphieu";
            updateCtpnhapQuery += " WHERE idphieunhap = @idphieu";

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
                    // Cập nhật dữ liệu trong bảng pnhap
                    if (hasUpdatePnhap)
                    {
                        SqlCommand updatePhieuNhapCommand = new SqlCommand(updatePnhapQuery, connection, transaction);
                        updatePhieuNhapCommand.Parameters.AddWithValue("@idphieu", idphieuu);
                        if (!string.IsNullOrWhiteSpace(ngaynhap.Text))
                            updatePhieuNhapCommand.Parameters.AddWithValue("@ngaynhap", ngaynhap.Text.Trim());
                        if (!string.IsNullOrWhiteSpace(nv.Text))
                            updatePhieuNhapCommand.Parameters.AddWithValue("@manv", nv.Text.Trim());
                        if (!string.IsNullOrWhiteSpace(thue.Text))
                            updatePhieuNhapCommand.Parameters.AddWithValue("@thue", thue.Text.Trim());
                        updatePhieuNhapCommand.ExecuteNonQuery();
                    }

                    // Cập nhật dữ liệu trong bảng ctpnhap
                    if (hasUpdateCtpnhap)
                    {
                        SqlCommand updateChiTietPhieuNhapCommand = new SqlCommand(updateCtpnhapQuery, connection, transaction);
                        updateChiTietPhieuNhapCommand.Parameters.AddWithValue("@idphieu", idphieuu);
                        if (!string.IsNullOrWhiteSpace(idhanghoa.Text))
                            updateChiTietPhieuNhapCommand.Parameters.AddWithValue("@idhanghoa", idhanghoa.Text.Trim());
                        if (!string.IsNullOrWhiteSpace(sl.Text))
                            updateChiTietPhieuNhapCommand.Parameters.AddWithValue("@soluongnhap", sl.Text.Trim());
                        if (!string.IsNullOrWhiteSpace(dongia.Text))
                            updateChiTietPhieuNhapCommand.Parameters.AddWithValue("@gia", dongia.Text.Trim());
                        updateChiTietPhieuNhapCommand.ExecuteNonQuery();
                    }

                    // Commit transaction nếu mọi thứ thành công
                    transaction.Commit();
                    MessageBox.Show("Cập nhật dữ liệu thành công!");

                    // Refresh DataGridView
                    Form6_Load(sender, e);
                }
                catch (Exception ex)
                {
                    // Rollback transaction nếu có lỗi xảy ra
                    transaction.Rollback();
                    MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
                }
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
    }
}
