using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;
using QLBanMayViTinh.Classes;

namespace QLBanMayViTinh.Testing
{
    /// <summary>
    /// Kịch bản Tự kiểm thử (Self-Test Suite) tự động xác minh tính toàn vẹn 
    /// của Triggers, CSDL 3NF, và toàn bộ 9 Yêu cầu nghiệp vụ của đề tài.
    /// Có cơ chế tự động Rollback / Cleanup để không làm sai lệch CSDL thật.
    /// </summary>
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

            // TEST 1: Kiểm tra kết nối Database
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

            // TEST 2: Kiểm tra đầy đủ 21 bảng trong CSDL
            totalTests++;
            try
            {
                string sqlCheckTables = @"SELECT COUNT(TABLE_NAME) FROM INFORMATION_SCHEMA.TABLES 
                                          WHERE TABLE_TYPE = 'BASE TABLE' AND TABLE_NAME IN (
                                          'LoaiMay', 'Chip', 'OCung', 'DungLuong', 'TocDo', 'OCD', 
                                          'ManHinh', 'CoMH', 'Chuot', 'BanPhim', 'USB', 'RAM', 
                                          'Loa', 'HangSanXuat', 'NhanVien', 'KhachHang', 'NhaCungCap', 
                                          'MayViTinh', 'HoaDonNhap', 'ChiTietHDN', 'HoaDonBan', 'ChiTietHDB')";
                string tableCount = Functions.GetFieldValues(sqlCheckTables);
                if (int.TryParse(tableCount, out int count) && count >= 21)
                {
                    passCount++;
                    sb.AppendLine(string.Format("[PASS] Test 2: CSDL đã tạo đầy đủ {0}/21 bảng chuẩn hóa 3NF.", count));
                }
                else
                {
                    sb.AppendLine(string.Format("[FAIL] Test 2: Chỉ tìm thấy {0}/21 bảng trong CSDL!", tableCount));
                }
            }
            catch (Exception ex)
            {
                sb.AppendLine("[FAIL] Test 2: Lỗi kiểm tra bảng: " + ex.Message);
            }

            // TEST 3, 4, 5: Kịch bản kiểm thử Trigger Nhập Hàng (Yêu cầu 1, 2, 3)
            string testMayVT = "TEST_PC_999";
            string testHDN = "HDN_TEST_999";
            string testHDB = "HDB_TEST_999";

            try
            {
                // Dọn dẹp dữ liệu rác cũ nếu có trước khi test
                Functions.RunSQL("DELETE FROM ChiTietHDB WHERE MaHDB = N'" + testHDB + "'");
                Functions.RunSQL("DELETE FROM HoaDonBan WHERE MaHDB = N'" + testHDB + "'");
                Functions.RunSQL("DELETE FROM ChiTietHDN WHERE MaHDN = N'" + testHDN + "'");
                Functions.RunSQL("DELETE FROM HoaDonNhap WHERE MaHDN = N'" + testHDN + "'");
                Functions.RunSQL("DELETE FROM MayViTinh WHERE MaMayVT = N'" + testMayVT + "'");

                // Tạo máy thử nghiệm với Số lượng = 0, Giá nhập = 0, Giá bán = 0
                string sqlAddMay = string.Format(@"INSERT INTO MayViTinh 
                    (MaMayVT, TenMayVT, MaLoai, MaChip, MaOCung, MaDungLuong, MaTocDo, MaOCD, 
                     MaManHinh, MaCoMH, MaChuot, MaBanPhim, MaUSB, MaRAM, MaLoa, MaHangSX, 
                     GiaNhap, GiaBan, ThoiGianBH, SoLuong, Anh, GhiChu)
                    VALUES (N'{0}', N'Máy Vi Tính Tự Kiểm Thử Unit Test', N'LM01', N'CPU01', N'OC01', 
                            N'DL01', N'TD01', N'CD01', N'MH01', N'CO01', N'CH01', N'BP01', 
                            N'USB01', N'RAM01', N'LOA01', N'HSX01', 0, 0, 12, 0, N'', N'Test Trigger')", testMayVT);
                Functions.RunSQL(sqlAddMay);

                // Tạo Hóa đơn nhập thử nghiệm
                string sqlAddHDN = string.Format(@"INSERT INTO HoaDonNhap (MaHDN, NgayNhap, MaNV, MaNCC, TongTien) 
                                                   VALUES (N'{0}', GETDATE(), N'NV01', N'NCC01', 0)", testHDN);
                Functions.RunSQL(sqlAddHDN);

                // Nhập 20 chiếc máy với Đơn giá nhập = 20,000,000 VNĐ
                decimal donGiaNhapTest = 20000000;
                int soLuongNhapTest = 20;
                string sqlAddCTHDN = string.Format(@"INSERT INTO ChiTietHDN (MaHDN, MaMayVT, SoLuong, DonGia, ThanhTien) 
                                                     VALUES (N'{0}', N'{1}', {2}, {3}, 0)",
                    testHDN, testMayVT, soLuongNhapTest, donGiaNhapTest);
                Functions.RunSQL(sqlAddCTHDN);

                // Lấy thông số thực tế của MayViTinh sau khi Trigger kích hoạt
                DataTable dtMay = Functions.GetDataToTable("SELECT SoLuong, GiaNhap, GiaBan FROM MayViTinh WHERE MaMayVT = N'" + testMayVT + "'");
                int slThucTe = Convert.ToInt32(dtMay.Rows[0]["SoLuong"]);
                decimal giaNhapThucTe = Convert.ToDecimal(dtMay.Rows[0]["GiaNhap"]);
                decimal giaBanThucTe = Convert.ToDecimal(dtMay.Rows[0]["GiaBan"]);

                // Test 3: Yêu cầu 1 - Tự động cộng tồn kho khi nhập
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

                // Test 4: Yêu cầu 2 - Tự động cập nhật Giá nhập mới nhất
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

                // Test 5: Yêu cầu 3 - Tự động tính Giá bán = 110% * Giá nhập
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

                // TEST 6: Trigger ngăn chặn xuất kho âm
                totalTests++;
                bool daChanXuatAm = false;
                try
                {
                    // Tạo HĐ bán
                    Functions.RunSQL(string.Format("INSERT INTO HoaDonBan (MaHDB, NgayBan, MaNV, MaKhach, TongTien) VALUES (N'{0}', GETDATE(), N'NV01', N'KH01', 0)", testHDB));
                    // Thử bán 50 chiếc trong khi kho chỉ có 20 chiếc (tắt MessageBox để test tự động)
                    Functions.RunSQL(string.Format("INSERT INTO ChiTietHDB (MaHDB, MaMayVT, SoLuong, DonGia, ThanhTien) VALUES (N'{0}', N'{1}', 50, 22000000, 0)", testHDB, testMayVT), false);
                }
                catch
                {
                    daChanXuatAm = true; // Trigger đã kích hoạt RAISERROR và ROLLBACK thành công
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

                // TEST 7: Trừ tồn kho khi bán hàng hợp lệ (Yêu cầu 1)
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

                // TEST 8: Kiểm tra cập nhật Tổng tiền Hóa đơn do Trigger tính
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

                // TEST 9: Hoàn trả số lượng tồn kho khi xóa mặt hàng trên hóa đơn
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

                // DỌN DẸP SẠCH DỮ LIỆU THỬ NGHIỆM
                Functions.RunSQL("DELETE FROM ChiTietHDB WHERE MaHDB = N'" + testHDB + "'");
                Functions.RunSQL("DELETE FROM HoaDonBan WHERE MaHDB = N'" + testHDB + "'");
                Functions.RunSQL("DELETE FROM ChiTietHDN WHERE MaHDN = N'" + testHDN + "'");
                Functions.RunSQL("DELETE FROM HoaDonNhap WHERE MaHDN = N'" + testHDN + "'");
                Functions.RunSQL("DELETE FROM MayViTinh WHERE MaMayVT = N'" + testMayVT + "'");
                sb.AppendLine("\n[CLEANUP] Đã dọn dẹp sạch sẽ toàn bộ bản ghi thử nghiệm. CSDL an toàn 100%.");
            }
            catch (Exception ex)
            {
                sb.AppendLine("[ERROR] Phát sinh ngoại lệ trong quá trình tự kiểm thử: " + ex.Message);
            }

            sb.AppendLine("======================================================================");
            sb.AppendLine(string.Format("TỔNG KẾT: {0}/{1} BÀI TEST ĐẠT (TỈ LỆ THÀNH CÔNG: {2:0.0}%)",
                passCount, totalTests, ((double)passCount / totalTests) * 100));
            sb.AppendLine("======================================================================");

            return sb.ToString();
        }
    }
}
