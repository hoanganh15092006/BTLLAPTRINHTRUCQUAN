using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace QLBanMayViTinh.Classes
{
    public static class Functions
    {
        public static string ConnString = @"Data Source=.\SQLEXPRESS01;Initial Catalog=QLBanMayViTinh;Integrated Security=True";
        public static SqlConnection Conn = null;

        public static string CurrentUsername = "admin";
        public static string CurrentFullName = "Quản Trị Viên";
        public static string CurrentRole = "Admin";
        public static string CurrentMaNV = "NV01";

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
                    EnsureSystemTables();
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

        public static void EnsureSystemTables()
        {
            if (Conn == null || Conn.State != ConnectionState.Open)
                return;

            try
            {
                string sqlCheckTables = @"
                IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'tblTaiKhoan')
                BEGIN
                    CREATE TABLE tblTaiKhoan (
                        TenDangNhap NVARCHAR(50) NOT NULL PRIMARY KEY,
                        MatKhauHash NVARCHAR(255) NOT NULL,
                        Salt NVARCHAR(100) NOT NULL,
                        MaNV NVARCHAR(20) NULL,
                        VaiTro NVARCHAR(50) NOT NULL DEFAULT 'BanHang',
                        TrangThai NVARCHAR(50) NOT NULL DEFAULT N'Hoạt động',
                        NgayTao DATETIME NOT NULL DEFAULT GETDATE()
                    );
                END;

                IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'tblNhatKyHoatDong')
                BEGIN
                    CREATE TABLE tblNhatKyHoatDong (
                        Id INT IDENTITY(1,1) PRIMARY KEY,
                        TenDangNhap NVARCHAR(50) NULL,
                        HanhDong NVARCHAR(100) NOT NULL,
                        ThoiGian DATETIME NOT NULL DEFAULT GETDATE(),
                        ChiTiet NVARCHAR(500) NULL
                    );
                END;

                IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'tblCauHinhHeThong')
                BEGIN
                    CREATE TABLE tblCauHinhHeThong (
                        KeyName NVARCHAR(100) NOT NULL PRIMARY KEY,
                        KeyValue NVARCHAR(1000) NULL
                    );
                END;";

                using (SqlCommand cmd = new SqlCommand(sqlCheckTables, Conn))
                {
                    cmd.ExecuteNonQuery();
                }

                string countUsers = "SELECT COUNT(*) FROM tblTaiKhoan";
                int count = 0;
                using (SqlCommand cmd = new SqlCommand(countUsers, Conn))
                {
                    object obj = cmd.ExecuteScalar();
                    if (obj != null) int.TryParse(obj.ToString(), out count);
                }

                if (count == 0)
                {
                    string salt;
                    string hash = SecurityHelper.HashPassword("Admin@123", out salt);
                    string insertAdmin = string.Format(
                        "INSERT INTO tblTaiKhoan (TenDangNhap, MatKhauHash, Salt, MaNV, VaiTro, TrangThai, NgayTao) " +
                        "VALUES (N'admin', N'{0}', N'{1}', N'NV01', 'Admin', N'Hoạt động', GETDATE())",
                        hash, salt);

                    using (SqlCommand cmd = new SqlCommand(insertAdmin, Conn))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    string staffSalt;
                    string staffHash = SecurityHelper.HashPassword("Staff@123", out staffSalt);
                    string insertStaff = string.Format(
                        "INSERT INTO tblTaiKhoan (TenDangNhap, MatKhauHash, Salt, MaNV, VaiTro, TrangThai, NgayTao) " +
                        "VALUES (N'banhang', N'{0}', N'{1}', N'NV02', 'BanHang', N'Hoạt động', GETDATE()), " +
                        "(N'thukho', N'{0}', N'{1}', N'NV03', 'ThuKho', N'Hoạt động', GETDATE())",
                        staffHash, staffSalt);

                    using (SqlCommand cmd = new SqlCommand(insertStaff, Conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }

                EnsureCleanVietnameseData();
            }
            catch
            {
            }
        }

        public static void EnsureCleanVietnameseData()
        {
            try
            {
                if (Conn == null || Conn.State != ConnectionState.Open) return;

                string checkSql = "SELECT COUNT(*) FROM LoaiMay WHERE TenLoai LIKE '%Ã%' OR TenLoai LIKE '%Ä%'";
                int count = 0;
                using (SqlCommand cmd = new SqlCommand(checkSql, Conn))
                {
                    object obj = cmd.ExecuteScalar();
                    if (obj != null) int.TryParse(obj.ToString(), out count);
                }

                if (count > 0)
                {
                    string repairSql = @"
                    UPDATE LoaiMay SET TenLoai = N'Máy tính xách tay (Laptop)' WHERE MaLoai = 'LM01';
                    UPDATE LoaiMay SET TenLoai = N'Máy tính để bàn Gaming (PC Gaming)' WHERE MaLoai = 'LM02';
                    UPDATE LoaiMay SET TenLoai = N'Máy tính để bàn Văn phòng (All-in-One)' WHERE MaLoai = 'LM03';
                    UPDATE LoaiMay SET TenLoai = N'Máy trạm đồ họa chuyên nghiệp (Workstation)' WHERE MaLoai = 'LM04';
                    UPDATE Chip SET TenChip = N'Intel Core i5 13400F (10 Cores, up to 4.6 GHz)' WHERE MaChip = 'CPU01';
                    UPDATE Chip SET TenChip = N'Intel Core i7 13700K (16 Cores, up to 5.4 GHz)' WHERE MaChip = 'CPU02';
                    UPDATE Chip SET TenChip = N'AMD Ryzen 5 7600X (6 Cores, up to 5.3 GHz)' WHERE MaChip = 'CPU03';
                    UPDATE Chip SET TenChip = N'AMD Ryzen 7 7800X3D (8 Cores, 3D V-Cache)' WHERE MaChip = 'CPU04';
                    UPDATE OCung SET TenOCung = N'SSD M.2 NVMe PCIe Gen4 x4' WHERE MaOCung = 'OC01';
                    UPDATE OCung SET TenOCung = N'SSD 2.5 inch SATA III' WHERE MaOCung = 'OC02';
                    UPDATE OCung SET TenOCung = N'HDD 3.5 inch Western Digital Blue' WHERE MaOCung = 'OC03';
                    UPDATE DungLuong SET TenDungLuong = N'512 GB' WHERE MaDungLuong = 'DL01';
                    UPDATE DungLuong SET TenDungLuong = N'1024 GB (1 TB)' WHERE MaDungLuong = 'DL02';
                    UPDATE DungLuong SET TenDungLuong = N'2048 GB (2 TB)' WHERE MaDungLuong = 'DL03';
                    UPDATE TocDo SET TenTocDo = N'3500 MB/s (PCIe 3.0)' WHERE MaTocDo = 'TD01';
                    UPDATE TocDo SET TenTocDo = N'7000 MB/s (PCIe 4.0 Siêu tốc)' WHERE MaTocDo = 'TD02';
                    UPDATE TocDo SET TenTocDo = N'7200 RPM (HDD Standard)' WHERE MaTocDo = 'TD03';
                    UPDATE OCD SET TenOCD = N'Không trang bị ổ CD' WHERE MaOCD = 'CD01';
                    UPDATE OCD SET TenOCD = N'Ổ đĩa DVD-RW ghi đọc gắn ngoài' WHERE MaOCD = 'CD02';
                    UPDATE ManHinh SET TenManHinh = N'Màn hình IPS Full HD 144Hz' WHERE MaManHinh = 'MH01';
                    UPDATE ManHinh SET TenManHinh = N'Màn hình IPS 2K QHD 165Hz Chống chói' WHERE MaManHinh = 'MH02';
                    UPDATE ManHinh SET TenManHinh = N'Màn hình OLED 4K UHD 120Hz Chuẩn màu' WHERE MaManHinh = 'MH03';
                    UPDATE CoMH SET TenCoMH = N'14.0 inch' WHERE MaCoMH = 'CO01';
                    UPDATE CoMH SET TenCoMH = N'15.6 inch' WHERE MaCoMH = 'CO02';
                    UPDATE CoMH SET TenCoMH = N'24.0 inch (Desktop)' WHERE MaCoMH = 'CO03';
                    UPDATE CoMH SET TenCoMH = N'27.0 inch (Desktop)' WHERE MaCoMH = 'CO04';
                    UPDATE Chuot SET TenChuot = N'Chuột không dây Logitech Silent M331' WHERE MaChuot = 'CH01';
                    UPDATE Chuot SET TenChuot = N'Chuột Gaming Razer DeathAdder Essential' WHERE MaChuot = 'CH02';
                    UPDATE Chuot SET TenChuot = N'Chuột quang văn phòng có dây Genius' WHERE MaChuot = 'CH03';
                    UPDATE BanPhim SET TenBanPhim = N'Bàn phím cơ DareU EK87 Red Switch RGB' WHERE MaBanPhim = 'BP01';
                    UPDATE BanPhim SET TenBanPhim = N'Bàn phím cơ không dây Asus ROG Azoth' WHERE MaBanPhim = 'BP02';
                    UPDATE BanPhim SET TenBanPhim = N'Bàn phím cao su văn phòng Logitech K120' WHERE MaBanPhim = 'BP03';
                    UPDATE USB SET TenUSB = N'Kingston DataTraveler Exodia USB 3.2', DungLuong = N'64 GB' WHERE MaUSB = 'USB01';
                    UPDATE USB SET TenUSB = N'SanDisk Ultra Flair USB 3.0 Vỏ kim loại', DungLuong = N'128 GB' WHERE MaUSB = 'USB02';
                    UPDATE USB SET TenUSB = N'Samsung Type-C Flash Drive', DungLuong = N'256 GB' WHERE MaUSB = 'USB03';
                    UPDATE RAM SET TenRAM = N'16GB (2x8GB) DDR4 3200MHz Kingston Fury' WHERE MaRAM = 'RAM01';
                    UPDATE RAM SET TenRAM = N'32GB (2x16GB) DDR5 6000MHz Corsair Vengeance' WHERE MaRAM = 'RAM02';
                    UPDATE RAM SET TenRAM = N'64GB (2x32GB) DDR5 6400MHz G.Skill Trident Z5' WHERE MaRAM = 'RAM03';
                    UPDATE Loa SET TenLoa = N'Loa tích hợp sẵn Stereo Waves MaxxAudio' WHERE MaLoa = 'LOA01';
                    UPDATE Loa SET TenLoa = N'Loa để bàn Microlab M-108 2.1 Siêu trầm' WHERE MaLoa = 'LOA02';
                    UPDATE Loa SET TenLoa = N'Loa Soundbar Gaming Edifier G2000 RGB' WHERE MaLoa = 'LOA03';
                    UPDATE HangSanXuat SET TenHangSX = N'ASUS ROG / TUF Gaming' WHERE MaHangSX = 'HSX01';
                    UPDATE HangSanXuat SET TenHangSX = N'DELL Technologies' WHERE MaHangSX = 'HSX02';
                    UPDATE HangSanXuat SET TenHangSX = N'MSI Gaming Series' WHERE MaHangSX = 'HSX03';
                    UPDATE HangSanXuat SET TenHangSX = N'HP (Hewlett-Packard)' WHERE MaHangSX = 'HSX04';
                    UPDATE NhanVien SET TenNV = N'Nguyễn Hoàng Long', GioiTinh = N'Nam', DiaChi = N'128 Cầu Giấy, Hà Nội' WHERE MaNV = 'NV01';
                    UPDATE NhanVien SET TenNV = N'Trần Thị Mai Phương', GioiTinh = N'Nữ', DiaChi = N'45 Giải Phóng, Hai Bà Trưng, Hà Nội' WHERE MaNV = 'NV02';
                    UPDATE NhanVien SET TenNV = N'Lê Minh Tuấn', GioiTinh = N'Nam', DiaChi = N'78 Nguyễn Trãi, Thanh Xuân, Hà Nội' WHERE MaNV = 'NV03';
                    UPDATE NhanVien SET TenNV = N'Phạm Thu Hà', GioiTinh = N'Nữ', DiaChi = N'12 Kim Mã, Ba Đình, Hà Nội' WHERE MaNV = 'NV04';
                    UPDATE NhanVien SET TenNV = N'Đỗ Quang Huy', GioiTinh = N'Nam', DiaChi = N'90 Hoàng Mai, Hà Nội' WHERE MaNV = 'NV05';
                    UPDATE KhachHang SET TenKhach = N'Công ty CP Công Nghệ NextTech', DiaChi = N'Tầng 5 Keangnam Landmark, Nam Từ Liêm' WHERE MaKhach = 'KH01';
                    UPDATE KhachHang SET TenKhach = N'Vũ Đình Khang', DiaChi = N'15 Hoàng Quốc Việt, Cầu Giấy, Hà Nội' WHERE MaKhach = 'KH02';
                    UPDATE KhachHang SET TenKhach = N'Hoàng Yến Nhi', DiaChi = N'22 Tây Sơn, Đống Đa, Hà Nội' WHERE MaKhach = 'KH03';
                    UPDATE KhachHang SET TenKhach = N'Trần Đức Anh', DiaChi = N'36 Lê Văn Lương, Cầu Giấy, Hà Nội' WHERE MaKhach = 'KH04';
                    UPDATE NhaCungCap SET TenNCC = N'Tổng Kho Máy Tính Viễn Sơn', DiaChi = N'Số 175 Phố Vọng, Hai Bà Trưng, Hà Nội' WHERE MaNCC = 'NCC01';
                    UPDATE NhaCungCap SET TenNCC = N'Công ty Phân Phối Synnex FPT', DiaChi = N'Tòa nhà FPT Cầu Giấy, Hà Nội' WHERE MaNCC = 'NCC02';
                    UPDATE NhaCungCap SET TenNCC = N'Công ty TNHH Phân Phối Dầu Khí PSD', DiaChi = N'Tầng 6 Petrovietnam Tower, Hà Nội' WHERE MaNCC = 'NCC03';
                    UPDATE MayViTinh SET TenMayVT = N'Laptop ASUS ROG Strix G16', GhiChu = N'Laptop Gaming cao cấp 2024' WHERE MaMayVT = 'MVT01';
                    UPDATE MayViTinh SET TenMayVT = N'Laptop Dell XPS 15 9530', GhiChu = N'Dòng doanh nhân mỏng nhẹ sang trọng' WHERE MaMayVT = 'MVT02';
                    UPDATE MayViTinh SET TenMayVT = N'PC Gaming MSI Dragon Core', GhiChu = N'PC Gaming hiệu năng cao giá tốt' WHERE MaMayVT = 'MVT03';
                    UPDATE MayViTinh SET TenMayVT = N'Máy trạm HP Z4 Workstation', GhiChu = N'Chuyên render kiến trúc, 3D max' WHERE MaMayVT = 'MVT04';";

                    using (SqlCommand cmd = new SqlCommand(repairSql, Conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch
            {
            }
        }

        public static void LogAction(string action, string detail)
        {
            try
            {
                Connect();
                if (Conn == null || Conn.State != ConnectionState.Open) return;

                string user = string.IsNullOrEmpty(CurrentUsername) ? "system" : CurrentUsername;
                string sql = "INSERT INTO tblNhatKyHoatDong (TenDangNhap, HanhDong, ThoiGian, ChiTiet) VALUES (@u, @a, GETDATE(), @d)";
                using (SqlCommand cmd = new SqlCommand(sql, Conn))
                {
                    cmd.Parameters.AddWithValue("@u", user);
                    cmd.Parameters.AddWithValue("@a", action ?? "");
                    cmd.Parameters.AddWithValue("@d", detail ?? "");
                    cmd.ExecuteNonQuery();
                }
            }
            catch
            {
            }
        }

        public static void Disconnect()
        {
            if (Conn != null && Conn.State == ConnectionState.Open)
            {
                Conn.Close();
                Conn.Dispose();
                Conn = null;
            }
        }

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
                if (ex.Number == 547)
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

        public static void FillCombo(string sql, ComboBox cbo, string ma, string ten)
        {
            try
            {
                DataTable table = GetDataToTable(sql);
                cbo.DataSource = table;
                cbo.ValueMember = ma;
                cbo.DisplayMember = ten;
                cbo.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi nạp ComboBox: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static string CreateKey(string prefix)
        {
            string key = prefix + "_" + DateTime.Now.ToString("ddMMyyyy_HHmmss");
            return key;
        }

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

        public static void OnlyInteger(KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

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
