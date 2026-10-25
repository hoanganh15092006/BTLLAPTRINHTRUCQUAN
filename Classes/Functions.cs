using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace QLBanMayViTinh.Classes
{
    /// <summary>
    /// Class tiện ích dùng chung xử lý ADO.NET thuần (System.Data.SqlClient),
    /// chuỗi kết nối SQL Server, các hàm truy vấn, điền ComboBox, sinh mã tự động,
    /// chuyển số thành chữ tiếng Việt và xử lý ảnh mượt mà không khóa file.
    /// </summary>
    public static class Functions
    {
        // Chuỗi kết nối SQL Server. Tự động nhận diện instance đang chạy
        public static string ConnString = @"Data Source=.\SQLEXPRESS01;Initial Catalog=QLBanMayViTinh;Integrated Security=True";
        public static SqlConnection Conn = null;

        /// <summary>
        /// Mở kết nối tới CSDL SQL Server với cơ chế tự động dò tìm Instance
        /// </summary>
        public static void Connect()
        {
            if (Conn != null && Conn.State == ConnectionState.Open)
                return;

            string[] candidateSources = new string[]
            {
                @".\SQLEXPRESS01",
                @".\SQLEXPRESS",
                @".",
                @"localhost",
                @"(local)"
            };

            string dbName = "QLBanMayViTinh";
            Exception lastException = null;

            foreach (string src in candidateSources)
            {
                try
                {
                    string testStr = string.Format("Data Source={0};Initial Catalog={1};Integrated Security=True;Connect Timeout=3", src, dbName);
                    SqlConnection testConn = new SqlConnection(testStr);
                    testConn.Open();

                    Conn = testConn;
                    ConnString = testStr;
                    return;
                }
                catch (Exception ex)
                {
                    lastException = ex;
                }
            }

            MessageBox.Show("Không thể kết nối đến CSDL SQL Server '" + dbName + "'!\n" +
                            "Hệ thống đã thử các instance (.\\SQLEXPRESS01, .\\SQLEXPRESS, ., localhost) nhưng không thành công.\n\n" +
                            "Vui lòng đảm bảo SQL Server đã bật và CSDL QLBanMayViTinh đã được tạo.\n" +
                            "Chi tiết lỗi: " + (lastException != null ? lastException.Message : ""),
                            "Lỗi Kết Nối CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Đóng kết nối CSDL
        /// </summary>
        public static void Disconnect()
        {
            if (Conn != null && Conn.State == ConnectionState.Open)
            {
                Conn.Close();
                Conn.Dispose();
                Conn = null;
            }
        }

        /// <summary>
        /// Lấy dữ liệu từ câu lệnh SQL SELECT trả về đối tượng DataTable
        /// </summary>
        public static DataTable GetDataToTable(string sql)
        {
            DataTable table = new DataTable();
            try
            {
                Connect();
                using (SqlCommand cmd = new SqlCommand(sql, Conn))
                {
                    using (SqlDataAdapter dap = new SqlDataAdapter(cmd))
                    {
                        dap.Fill(table);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thực thi truy vấn lấy dữ liệu:\n" + ex.Message + "\nSQL: " + sql,
                                "Lỗi Truy Vấn", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return table;
        }

        /// <summary>
        /// Thực thi câu lệnh SQL INSERT, UPDATE hoặc DELETE
        /// </summary>
        public static void RunSQL(string sql, bool showMessageBox = true)
        {
            try
            {
                Connect();
                using (SqlCommand cmd = new SqlCommand(sql, Conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                if (showMessageBox)
                {
                    MessageBox.Show("Lỗi thực thi câu lệnh SQL:\n" + ex.Message + "\nSQL: " + sql,
                                    "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                throw;
            }
        }

        /// <summary>
        /// Thực thi câu lệnh SQL xóa dữ liệu có kiểm tra ràng buộc khóa ngoại
        /// </summary>
        public static void RunSqlDel(string sql)
        {
            try
            {
                Connect();
                using (SqlCommand cmd = new SqlCommand(sql, Conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 547) // Foreign key constraint violation
                {
                    MessageBox.Show("Dữ liệu này đang được tham chiếu ở bảng khác, bạn không thể xóa!",
                                    "Cảnh Báo Ràng Buộc", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("Lỗi xóa dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Kiểm tra sự tồn tại của khóa chính (Return true nếu đã tồn tại)
        /// </summary>
        public static bool CheckKey(string sql)
        {
            bool exists = false;
            try
            {
                Connect();
                using (SqlCommand cmd = new SqlCommand(sql, Conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                            exists = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kiểm tra khóa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return exists;
        }

        /// <summary>
        /// Lấy giá trị của 1 ô (cột đầu tiên của dòng đầu tiên) từ câu lệnh SELECT
        /// </summary>
        public static string GetFieldValues(string sql)
        {
            string ma = "";
            try
            {
                Connect();
                using (SqlCommand cmd = new SqlCommand(sql, Conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ma = reader.GetValue(0).ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lấy giá trị trường: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return ma;
        }

        /// <summary>
        /// Đổ dữ liệu vào ComboBox từ câu lệnh SQL
        /// </summary>
        public static void FillCombo(string sql, ComboBox cbo, string ma, string ten)
        {
            try
            {
                DataTable table = GetDataToTable(sql);
                cbo.DataSource = table;
                cbo.ValueMember = ma;     // Trường lưu trữ giá trị mã
                cbo.DisplayMember = ten;  // Trường hiển thị tên cho người dùng
                cbo.SelectedIndex = -1;   // Mặc định không chọn dòng nào
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi nạp ComboBox: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Tự sinh mã khóa chính theo tiền tố kết hợp ngày giờ: TIÊN_TỐ_ddMMyyyy_HHmmss
        /// Ví dụ: HDN_25102026_140510
        /// </summary>
        public static string CreateKey(string prefix)
        {
            string key = prefix + "_" + DateTime.Now.ToString("ddMMyyyy_HHmmss");
            return key;
        }

        /// <summary>
        /// Đọc ảnh từ file một cách an toàn (tránh lỗi khóa file GDI+ trong C#)
        /// </summary>
        public static Image LoadImageWithoutLock(string filePath)
        {
            if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
                return null;

            try
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        fs.CopyTo(ms);
                        ms.Position = 0;
                        return Image.FromStream(ms);
                    }
                }
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Chỉ cho phép nhập số nguyên trong ô TextBox (sự kiện KeyPress)
        /// </summary>
        public static void OnlyInteger(KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Chỉ cho phép nhập số thực/tiền tệ (cho phép nhập dấu chấm hoặc phẩy thập phân)
        /// </summary>
        public static void OnlyDecimal(KeyPressEventArgs e, string currentText)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.') && (e.KeyChar != ','))
            {
                e.Handled = true;
            }
            if ((e.KeyChar == '.' || e.KeyChar == ',') && (currentText.IndexOf('.') > -1 || currentText.IndexOf(',') > -1))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Chuyển đổi số tiền thành chữ tiếng Việt chuẩn (dùng cho in hóa đơn)
        /// </summary>
        public static string ChuyenSoSangChu(double gSo)
        {
            if (gSo == 0) return "Không đồng";
            string[] chuSo = new string[10] { "không", "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín" };
            string[] tien = new string[6] { "", " nghìn", " triệu", " tỷ", " nghìn tỷ", " triệu tỷ" };
            int i, donvi, chuc, tram;
            string s = " ", tach = "";
            double so = Math.Abs(gSo);
            string strSo = so.ToString("0");
            int len = strSo.Length;
            int n = (len + 2) / 3;

            for (i = 0; i < n; i++)
            {
                tach = "";
                int pos = len - 3 * (i + 1);
                int l = 3;
                if (pos < 0)
                {
                    l = 3 + pos;
                    pos = 0;
                }
                string sub = strSo.Substring(pos, l).PadLeft(3, '0');
                tram = int.Parse(sub.Substring(0, 1));
                chuc = int.Parse(sub.Substring(1, 1));
                donvi = int.Parse(sub.Substring(2, 1));

                if (tram == 0 && chuc == 0 && donvi == 0)
                {
                    if (s == " ") s = "";
                    continue;
                }

                if (tram != 0 || i < n - 1)
                {
                    tach += " " + chuSo[tram] + " trăm";
                }

                if (chuc == 0 && donvi != 0)
                {
                    if (tram != 0 || i < n - 1) tach += " linh " + chuSo[donvi];
                    else tach += " " + chuSo[donvi];
                }
                else if (chuc == 1)
                {
                    tach += " mười";
                    if (donvi == 1) tach += " một";
                    else if (donvi == 5) tach += " lăm";
                    else if (donvi != 0) tach += " " + chuSo[donvi];
                }
                else if (chuc > 1)
                {
                    tach += " " + chuSo[chuc] + " mươi";
                    if (donvi == 1) tach += " mốt";
                    else if (donvi == 5) tach += " lăm";
                    else if (donvi != 0) tach += " " + chuSo[donvi];
                }

                s = tach + tien[i] + s;
            }

            s = s.Trim();
            if (s.Length > 0)
            {
                s = s.Substring(0, 1).ToUpper() + s.Substring(1) + " đồng chẵn.";
            }
            if (gSo < 0) s = "Âm " + s;
            return s;
        }
    }
}
