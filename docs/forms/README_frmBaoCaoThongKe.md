# BÁO CÁO KỸ THUẬT & HƯỚNG DẪN HOẠT ĐỘNG: FORM BÁO CÁO THỐNG KÊ DOANH THU (frmBaoCaoThongKe)

---

## 1. TỔNG QUAN VỀ FORM
- **Tên lớp (Class Name)**: `frmBaoCaoThongKe`
- **Tập tin mã nguồn**:
  - Code-behind: [Forms/frmBaoCaoThongKe.cs](file:///d:/HOCTRENLOP/LAPTRINHTRUCQUAN/BTLLAPTRINHTRUCQUAN/Forms/frmBaoCaoThongKe.cs)
  - UI Designer: [Forms/frmBaoCaoThongKe.Designer.cs](file:///d:/HOCTRENLOP/LAPTRINHTRUCQUAN/BTLLAPTRINHTRUCQUAN/Forms/frmBaoCaoThongKe.Designer.cs)
- **Mục đích nghiệp vụ**: Cung cấp trung tâm phân tích kinh doanh (Business Intelligence) và giải quyết trọn vẹn 4 yêu cầu đồ án bắt buộc:
  - **Yêu cầu 6**: Thống kê danh sách máy vi tính tồn kho & định giá tài sản kho.
  - **Yêu cầu 7**: Báo cáo tổng chi phí nhập hàng theo Quý/Năm hoặc Tháng.
  - **Yêu cầu 8**: Báo cáo tổng doanh thu bán hàng theo Quý/Năm hoặc Tháng.
  - **Yêu cầu 9**: Báo cáo Top 5 nhân viên có doanh số bán hàng xuất sắc nhất.
  - Hỗ trợ xuất file Excel định dạng chuẩn OpenXML (.xlsx).
- **Phân quyền truy cập (RBAC)**: `Admin`, `ThuKho`, `BanHang` (được phân quyền dữ liệu phù hợp).

---

## 2. CÔNG NGHỆ, THIẾT KẾ THẺ KPI & XUẤT EXCEL
1. **Kiến trúc 4 Thẻ Summary KPI thời gian thực**:
   - `pnlCardTongTonKho`: Tổng giá trị tồn kho tính theo giá vốn nhập (`SUM(SoLuong * DonGiaNhap)`).
   - `pnlCardChiPhiNhap`: Tổng chi phí nhập hàng từ tất cả phiếu nhập trong kỳ.
   - `pnlCardDoanhThuBan`: Tổng doanh thu bán hàng thực tế từ hóa đơn bán lẻ.
   - `pnlCardUocTinhLoiNhuan`: Ước tính lợi nhuận gộp bằng công thức Doanh thu bán trừ Chi phí vốn.
2. **Kỹ thuật trích xuất Excel (EPPlus OpenXML)**:
   - Thao tác trực tiếp với định dạng Microsoft Excel `.xlsx` mà không phụ thuộc vào việc máy tính người dùng có cài đặt phần mềm Microsoft Office hay không (Zero Office COM Interop Dependency).
   - Tự động định dạng Header cột, căn giữa, kẻ viền ô, format số tiền có phân cách hàng nghìn (`#,##0 VNĐ`) và tính dòng Tổng cộng (Grand Total) chuyên nghiệp.
3. **Cơ chế thu phóng thanh lọc thời gian (Adaptive Dynamic Filter Panel)**:
   - Khi chọn *Sản Phẩm Tồn Kho*, thanh lọc thời gian tự động thu gọn (`Height = 0`) để tối đa hóa không gian hiển thị bảng.
   - Khi chọn *Nhập Theo Quý/Năm*, *Bán Theo Quý/Năm* hoặc *Top 5 Doanh Thu*, panel bộ lọc mở rộng hiển thị các lựa chọn: Lọc theo Tháng (1-12), Lọc theo Quý (1-4) hoặc Lọc theo Năm.

---

## 3. CÁCH HOẠT ĐỘNG & QUY TRÌNH NGHIỆP VỤ (LIFECYCLE & WORKFLOW)

### 3.1. Sơ đồ xử lý báo cáo & xuất Excel
```mermaid
flowchart TD
    A([Người dùng mở frmBaoCaoThongKe]) --> B[Tính toán số liệu 4 Thẻ KPI: Tồn kho, Chi phí, Doanh thu, Lợi nhuận]
    B --> C[/Chọn 1 trong 4 Chế độ báo cáo/]
    C --> D{Chế độ là gì?}
    D -- YC6: Tồn kho --> E[Ẩn bộ lọc thời gian & Tự động chạy truy vấn tồn kho]
    D -- YC7, YC8, YC9 --> F[Hiển thị thanh lọc thời gian & Chọn Tháng/Quý/Năm]
    F --> G{Nhấn nút [Xem Báo Cáo]}
    E --> H[Thực thi SQL Aggregate và nạp DataSource vào dgvBaoCao]
    G --> H
    H --> I[Hiển thị lưới dữ liệu & Cập nhật tiêu đề bảng]
    I --> J{Nhấn nút [Xuất Excel]?}
    J -- Không --> K([Kết thúc tra cứu])
    J -- Có --> L[Khởi tạo ExcelPackage EPPlus]
    L --> M[Ghi Header, Style màu sắc & Ghi từng dòng dữ liệu]
    M --> N[Tự động tính AutoFit độ rộng cột & Định dạng tiền tệ]
    N --> O[Mở SaveFileDialog & Lưu file .xlsx]
    O --> P[Ghi Audit Log & Mở file Excel cho người dùng]
```

---

## 4. CHI TIẾT MÃ NGUỒN & PHƯƠNG THỨC CHÍNH

### 4.1. Bảng danh sách phương thức
| Phương thức | Phạm vi | Chức năng chi tiết |
|:---|:---|:---|
| `frmBaoCaoThongKe_Load` | `private void` | Cấu hình theme, nạp giá trị Tháng/Quý/Năm mặc định, cập nhật 4 thẻ KPI |
| `CapNhatSummaryCards` | `private void` | Chạy 4 truy vấn SQL tính toán số liệu tổng giá trị kho, chi phí, doanh thu |
| `HienThiDieuKienLoc` | `private void` | Ẩn/hiện và điều chỉnh độ cao của panel bộ lọc tùy chế độ |
| `btnXemBaoCao_Click` | `private void` | Phân nhánh truy vấn tương ứng theo Yêu cầu 6, 7, 8, 9 và nạp lên lưới |
| `btnXuatExcel_Click` | `private void` | Kết xuất toàn bộ dữ liệu đang hiển thị trên lưới ra file Excel (.xlsx) |

### 4.2. Câu lệnh SQL Top 5 nhân viên có doanh thu cao nhất (Yêu cầu 9)
```csharp
string sql = @"SELECT TOP 5 
                nv.MaNV AS [Mã NV], 
                nv.TenNV AS [Tên Nhân Viên], 
                COUNT(DISTINCT hdb.SoHDB) AS [Số Hóa Đơn Lập], 
                SUM(hdb.TongTien) AS [Tổng Doanh Thu Đạt Được]
               FROM NhanVien nv
               INNER JOIN tblHoaDonBan hdb ON nv.MaNV = hdb.MaNV
               WHERE 1=1 " + sqlDieuKienThoiGian + @"
               GROUP BY nv.MaNV, nv.TenNV
               ORDER BY [Tổng Doanh Thu Đạt Được] DESC";
```

---

## 5. ẢNH CHỤP MINH CHỨNG GIAO DIỆN
- **Chế độ Sáng (Light Mode)**: `Screenshots/frmBaoCaoThongKe_Light.png`
- **Chế độ Tối (Dark Mode)**: `Screenshots/frmBaoCaoThongKe_Dark.png`
- **Báo cáo Top 5 Doanh Thu (Yêu Cầu 9)**: `Screenshots/Verified/01_BaoCao_rdoTop5_Light.png`
- **Báo cáo Doanh Thu Nhập Hàng (Yêu Cầu 7)**: `Screenshots/Verified/01_BaoCao_rdoNhapHang_Light.png`
- **Báo cáo Sản Phẩm Tồn Kho (Yêu Cầu 6)**: `Screenshots/Verified/01_BaoCao_rdoTonKho_Light.png`
