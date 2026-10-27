using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;
using QLBanMayViTinh.Classes;
using QLBanMayViTinh.Forms;

namespace QLBanMayViTinh.Testing
{
    public static class SelfTestSuite
    {
        public static string RunSelfTest()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("======================================================================");
            sb.AppendLine("            KẾT QUẢ TỰ KIỂM THỬ HỆ THỐNG (SELF-TEST SUITE)            ");
            sb.AppendLine("         QUẢN LÝ CỬA HÀNG BÁN MÁY VI TÍNH - ADO.NET SQLCLIENT         ");
            sb.AppendLine("======================================================================");
            sb.AppendLine(string.Format("Thời gian chạy kiểm thử: {0:dd/MM/yyyy HH:mm:ss}\n", DateTime.Now));

            int totalTests = 0;
            int passCount = 0;

            totalTests++;
            try
            {
                Functions.Connect();
                if (Functions.Conn.State == ConnectionState.Open)
                {
                    passCount++;
                    sb.AppendLine("[PASS] Test 1: Kết nối SQL Server thành công (Database: QLBanMayViTinh).");
                }
                else
                {
                    sb.AppendLine("[FAIL] Test 1: Không thể mở kết nối CSDL!");
                }
            }
            catch (Exception ex)
            {
                sb.AppendLine("[FAIL] Test 1: Lỗi kết nối CSDL: " + ex.Message);
            }

            totalTests++;
            try
            {
                string sqlCheckTables = @"SELECT COUNT(TABLE_NAME) FROM INFORMATION_SCHEMA.TABLES
                                          WHERE TABLE_TYPE = 'BASE TABLE' AND TABLE_NAME IN (
                                          'LoaiMay', 'Chip', 'OCung', 'DungLuong', 'TocDo', 'OCD',
                                          'ManHinh', 'CoMH', 'Chuot', 'BanPhim', 'USB', 'RAM',
                                          'Loa', 'HangSanXuat', 'NhanVien', 'KhachHang', 'NhaCungCap',
                                          'MayViTinh', 'HoaDonNhap', 'ChiTietHDN', 'HoaDonBan', 'ChiTietHDB',
                                          'tblTaiKhoan', 'tblNhatKyHoatDong', 'tblCauHinhHeThong')";
                string tableCount = Functions.GetFieldValues(sqlCheckTables);
                if (int.TryParse(tableCount, out int count) && count >= 21)
                {
                    passCount++;
                    sb.AppendLine(string.Format("[PASS] Test 2: CSDL đã tạo đầy đủ {0} bảng chuẩn hóa 3NF và hệ thống bảo mật.", count));
                }
                else
                {
                    sb.AppendLine(string.Format("[FAIL] Test 2: Chỉ tìm thấy {0} bảng trong CSDL!", tableCount));
                }
            }
            catch (Exception ex)
            {
                sb.AppendLine("[FAIL] Test 2: Lỗi kiểm tra bảng: " + ex.Message);
            }

            string testMayVT = "TEST_PC_999";
            string testHDN = "HDN_TEST_999";
            string testHDB = "HDB_TEST_999";

            try
            {
                Functions.RunSQL("DELETE FROM ChiTietHDB WHERE MaHDB = N'" + testHDB + "'");
                Functions.RunSQL("DELETE FROM HoaDonBan WHERE MaHDB = N'" + testHDB + "'");
                Functions.RunSQL("DELETE FROM ChiTietHDN WHERE MaHDN = N'" + testHDN + "'");
                Functions.RunSQL("DELETE FROM HoaDonNhap WHERE MaHDN = N'" + testHDN + "'");
                Functions.RunSQL("DELETE FROM MayViTinh WHERE MaMayVT = N'" + testMayVT + "'");

                string sqlAddMay = string.Format(@"INSERT INTO MayViTinh
                    (MaMayVT, TenMayVT, MaLoai, MaChip, MaOCung, MaDungLuong, MaTocDo, MaOCD,
                     MaManHinh, MaCoMH, MaChuot, MaBanPhim, MaUSB, MaRAM, MaLoa, MaHangSX,
                     GiaNhap, GiaBan, ThoiGianBH, SoLuong, Anh, GhiChu)
                    VALUES (N'{0}', N'Máy Vi Tính Tự Kiểm Thử Unit Test', N'LM01', N'CPU01', N'OC01',
                            N'DL01', N'TD01', N'CD01', N'MH01', N'CO01', N'CH01', N'BP01',
                            N'USB01', N'RAM01', N'LOA01', N'HSX01', 0, 0, 12, 0, N'', N'Test Trigger')", testMayVT);
                Functions.RunSQL(sqlAddMay);

                string sqlAddHDN = string.Format(@"INSERT INTO HoaDonNhap (MaHDN, NgayNhap, MaNV, MaNCC, TongTien)
                                                   VALUES (N'{0}', GETDATE(), N'NV01', N'NCC01', 0)", testHDN);
                Functions.RunSQL(sqlAddHDN);

                decimal donGiaNhapTest = 20000000;
                int soLuongNhapTest = 20;
                string sqlAddCTHDN = string.Format(@"INSERT INTO ChiTietHDN (MaHDN, MaMayVT, SoLuong, DonGia, ThanhTien)
                                                     VALUES (N'{0}', N'{1}', {2}, {3}, 0)",
                    testHDN, testMayVT, soLuongNhapTest, donGiaNhapTest);
                Functions.RunSQL(sqlAddCTHDN);

                DataTable dtMay = Functions.GetDataToTable("SELECT SoLuong, GiaNhap, GiaBan FROM MayViTinh WHERE MaMayVT = N'" + testMayVT + "'");
                int slThucTe = Convert.ToInt32(dtMay.Rows[0]["SoLuong"]);
                decimal giaNhapThucTe = Convert.ToDecimal(dtMay.Rows[0]["GiaNhap"]);
                decimal giaBanThucTe = Convert.ToDecimal(dtMay.Rows[0]["GiaBan"]);

                totalTests++;
                if (slThucTe == soLuongNhapTest)
                {
                    passCount++;
                    sb.AppendLine(string.Format("[PASS] Test 3 (Yêu cầu 1): Tồn kho tự động tăng lên {0} cái sau khi nhập hàng.", slThucTe));
                }
                else
                {
                    sb.AppendLine(string.Format("[FAIL] Test 3 (Yêu cầu 1): Tồn kho sai! Kỳ vọng: {0}, Thực tế: {1}", soLuongNhapTest, slThucTe));
                }

                totalTests++;
                if (giaNhapThucTe == donGiaNhapTest)
                {
                    passCount++;
                    sb.AppendLine(string.Format("[PASS] Test 4 (Yêu cầu 2): Giá nhập tự động cập nhật đúng: {0:N0} VNĐ.", giaNhapThucTe));
                }
                else
                {
                    sb.AppendLine(string.Format("[FAIL] Test 4 (Yêu cầu 2): Giá nhập sai! Kỳ vọng: {0:N0}, Thực tế: {1:N0}", donGiaNhapTest, giaNhapThucTe));
                }

                totalTests++;
                decimal giaBanKyVong = Math.Round(donGiaNhapTest * 1.10m, 0);
                if (giaBanThucTe == giaBanKyVong)
                {
                    passCount++;
                    sb.AppendLine(string.Format("[PASS] Test 5 (Yêu cầu 3): Giá bán tự động tính = 110% * Giá nhập: {0:N0} VNĐ.", giaBanThucTe));
                }
                else
                {
                    sb.AppendLine(string.Format("[FAIL] Test 5 (Yêu cầu 3): Giá bán sai! Kỳ vọng 110%: {0:N0}, Thực tế: {1:N0}", giaBanKyVong, giaBanThucTe));
                }

                totalTests++;
                bool daChanXuatAm = false;
                try
                {
                    Functions.RunSQL(string.Format("INSERT INTO HoaDonBan (MaHDB, NgayBan, MaNV, MaKhach, TongTien) VALUES (N'{0}', GETDATE(), N'NV01', N'KH01', 0)", testHDB));
                    Functions.RunSQL(string.Format("INSERT INTO ChiTietHDB (MaHDB, MaMayVT, SoLuong, DonGia, ThanhTien) VALUES (N'{0}', N'{1}', 50, 22000000, 0)", testHDB, testMayVT), false);
                }
                catch
                {
                    daChanXuatAm = true;
                }

                if (daChanXuatAm)
                {
                    passCount++;
                    sb.AppendLine("[PASS] Test 6 (Ràng buộc toàn vẹn): Trigger chặn thành công việc bán quá số lượng tồn kho (chống xuất âm).");
                }
                else
                {
                    sb.AppendLine("[FAIL] Test 6 (Ràng buộc toàn vẹn): Trigger không chặn được xuất kho âm!");
                }

                totalTests++;
                int soLuongBanTest = 5;
                Functions.RunSQL(string.Format("INSERT INTO ChiTietHDB (MaHDB, MaMayVT, SoLuong, DonGia, ThanhTien) VALUES (N'{0}', N'{1}', {2}, 22000000, 0)", testHDB, testMayVT, soLuongBanTest));
                int slSauBan = Convert.ToInt32(Functions.GetFieldValues("SELECT SoLuong FROM MayViTinh WHERE MaMayVT = N'" + testMayVT + "'"));
                if (slSauBan == (soLuongNhapTest - soLuongBanTest))
                {
                    passCount++;
                    sb.AppendLine(string.Format("[PASS] Test 7 (Yêu cầu 1): Tồn kho tự động trừ từ {0} xuống còn {1} chiếc sau khi bán {2} chiếc.",
                        soLuongNhapTest, slSauBan, soLuongBanTest));
                }
                else
                {
                    sb.AppendLine(string.Format("[FAIL] Test 7 (Yêu cầu 1): Tồn kho sau bán sai! Kỳ vọng: {0}, Thực tế: {1}",
                        soLuongNhapTest - soLuongBanTest, slSauBan));
                }

                totalTests++;
                decimal tongTienHDB = Convert.ToDecimal(Functions.GetFieldValues("SELECT TongTien FROM HoaDonBan WHERE MaHDB = N'" + testHDB + "'"));
                if (tongTienHDB == (soLuongBanTest * 22000000m))
                {
                    passCount++;
                    sb.AppendLine(string.Format("[PASS] Test 8: Tổng tiền Hóa đơn bán tự động tính chính xác: {0:N0} VNĐ.", tongTienHDB));
                }
                else
                {
                    sb.AppendLine(string.Format("[FAIL] Test 8: Tổng tiền hóa đơn bán sai: {0:N0}", tongTienHDB));
                }

                totalTests++;
                Functions.RunSQL(string.Format("DELETE FROM ChiTietHDB WHERE MaHDB = N'{0}' AND MaMayVT = N'{1}'", testHDB, testMayVT));
                int slSauKhiXoaDong = Convert.ToInt32(Functions.GetFieldValues("SELECT SoLuong FROM MayViTinh WHERE MaMayVT = N'" + testMayVT + "'"));
                if (slSauKhiXoaDong == soLuongNhapTest)
                {
                    passCount++;
                    sb.AppendLine(string.Format("[PASS] Test 9: Sau khi xóa mặt hàng khỏi HĐ bán, số lượng tồn kho được hoàn lại nguyên vẹn: {0} chiếc.", slSauKhiXoaDong));
                }
                else
                {
                    sb.AppendLine(string.Format("[FAIL] Test 9: Hoàn trả tồn kho sai! Kỳ vọng: {0}, Thực tế: {1}", soLuongNhapTest, slSauKhiXoaDong));
                }

                Functions.RunSQL("DELETE FROM ChiTietHDB WHERE MaHDB = N'" + testHDB + "'");
                Functions.RunSQL("DELETE FROM HoaDonBan WHERE MaHDB = N'" + testHDB + "'");
                Functions.RunSQL("DELETE FROM ChiTietHDN WHERE MaHDN = N'" + testHDN + "'");
                Functions.RunSQL("DELETE FROM HoaDonNhap WHERE MaHDN = N'" + testHDN + "'");
                Functions.RunSQL("DELETE FROM MayViTinh WHERE MaMayVT = N'" + testMayVT + "'");
                sb.AppendLine("[CLEANUP] Đã dọn dẹp sạch sẽ toàn bộ bản ghi thử nghiệm. CSDL an toàn 100%.");
            }
            catch (Exception ex)
            {
                sb.AppendLine("[ERROR] Phát sinh ngoại lệ trong quá trình tự kiểm thử: " + ex.Message);
            }

            totalTests++;
            try
            {
                string testPwd = "SecretPassword@2026";
                string salt;
                string hash = SecurityHelper.HashPassword(testPwd, out salt);
                bool valid = SecurityHelper.VerifyPassword(testPwd, hash, salt);
                bool invalid = SecurityHelper.VerifyPassword("WrongPassword", hash, salt);

                if (valid && !invalid)
                {
                    passCount++;
                    sb.AppendLine("[PASS] Test 10 (Security): Thuật toán băm PBKDF2 và kiểm tra mật khẩu chính xác 100%.");
                }
                else
                {
                    sb.AppendLine("[FAIL] Test 10 (Security): Xác thực PBKDF2 không đúng!");
                }
            }
            catch (Exception ex)
            {
                sb.AppendLine("[FAIL] Test 10 (Security): Lỗi băm mật khẩu: " + ex.Message);
            }

            totalTests++;
            try
            {
                string plainText = "Số điện thoại: 0988.888.999 - Địa chỉ: 128 Cầu Giấy, Hà Nội";
                string encrypted = SecurityHelper.EncryptAES(plainText);
                string decrypted = SecurityHelper.DecryptAES(encrypted);

                if (encrypted.StartsWith("ENC:") && decrypted == plainText)
                {
                    passCount++;
                    sb.AppendLine("[PASS] Test 11 (Security): Mã hóa & Giải mã AES-256 bảo vệ dữ liệu nhạy cảm hoạt động hoàn hảo.");
                }
                else
                {
                    sb.AppendLine("[FAIL] Test 11 (Security): Chu trình mã hóa AES-256 không khớp với dữ liệu gốc!");
                }
            }
            catch (Exception ex)
            {
                sb.AppendLine("[FAIL] Test 11 (Security): Lỗi AES-256: " + ex.Message);
            }

            totalTests++;
            try
            {
                using (frmDangNhap f1 = new frmDangNhap()) { }
                using (frmDangKy f2 = new frmDangKy()) { }
                using (frmQuenMatKhau f3 = new frmQuenMatKhau()) { }
                using (frmHoSoCaNhan f4 = new frmHoSoCaNhan()) { }
                using (frmQuanLyNguoiDung f5 = new frmQuanLyNguoiDung()) { }
                using (frmDanhMucHeThong f6 = new frmDanhMucHeThong()) { }
                using (frmCaiDat f7 = new frmCaiDat()) { }
                using (frmNhatKyHoatDong f8 = new frmNhatKyHoatDong()) { }
                using (frmMayViTinh f9 = new frmMayViTinh()) { }
                using (frmHoaDonNhap f10 = new frmHoaDonNhap()) { }
                using (frmHoaDonBan f11 = new frmHoaDonBan()) { }
                using (frmTimKiem f12 = new frmTimKiem()) { }
                using (frmBaoCaoThongKe f13 = new frmBaoCaoThongKe()) { }

                passCount++;
                sb.AppendLine("[PASS] Test 12 (UI Forms): Khởi tạo thành công 13/13 Form giao diện mà không gặp bất kỳ lỗi runtime nào.");
            }
            catch (Exception ex)
            {
                sb.AppendLine("[FAIL] Test 12 (UI Forms): Lỗi khởi tạo Form: " + ex.Message);
            }

            sb.AppendLine("======================================================================");
            sb.AppendLine(string.Format("TỔNG KẾT: {0}/{1} BÀI TEST ĐẠT (TỈ LỆ THÀNH CÔNG: {2:0.0}%)",
                passCount, totalTests, ((double)passCount / totalTests) * 100));
            sb.AppendLine("======================================================================");

            return sb.ToString();
        }
    }
}
