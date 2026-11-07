# BÁO CÁO KỸ THUẬT & HƯỚNG DẪN HOẠT ĐỘNG: FORM NHẬT KÝ HOẠT ĐỘNG & AUDIT TRAIL (frmNhatKyHoatDong)

---

## 1. TỔNG QUAN VỀ FORM
- **Tên lớp (Class Name)**: `frmNhatKyHoatDong`
- **Tập tin mã nguồn**:
  - Code-behind: [Forms/frmNhatKyHoatDong.cs](file:///d:/HOCTRENLOP/LAPTRINHTRUCQUAN/BTLLAPTRINHTRUCQUAN/Forms/frmNhatKyHoatDong.cs)
  - UI Designer: [Forms/frmNhatKyHoatDong.Designer.cs](file:///d:/HOCTRENLOP/LAPTRINHTRUCQUAN/BTLLAPTRINHTRUCQUAN/Forms/frmNhatKyHoatDong.Designer.cs)
- **Mục đích nghiệp vụ**: Phân hệ kiểm soát an ninh thông tin và truy vết hoạt động hệ thống (Audit Trail). Tự động ghi nhận và hiển thị lịch sử mọi thao tác quan trọng (Đăng nhập, Thêm/Sửa/Xóa máy tính, Lập hóa đơn nhập/bán, Thay đổi cấu hình) kèm thời gian chính xác tới từng giây, đồng thời tích hợp trung tâm cảnh báo sản phẩm sắp hết hàng.
- **Phân quyền truy cập (RBAC)**: `Admin`, `ThuKho`, `BanHang` (Chức năng xóa nhật ký chỉ dành riêng cho Admin).

---

## 2. CÔNG NGHỆ & CƠ CHẾ GHI NHẬT KÝ TỰ ĐỘNG
1. **Kiến trúc lưu trữ Audit Log `tblNhatKyHoatDong`**:
   - Cấu trúc bảng gồm: `Id` (Khóa chính tự tăng), `ThoiGian` (Thời gian thực `GETDATE()`), `NguoiDung` (Username thực hiện), `HanhDong` (Tên nghiệp vụ), `ChiTiet` (Nội dung mô tả sự kiện).
   - Hàm ghi log toàn cục `Functions.LogAction(hanhDong, chiTiet)`: Được gọi tự động tại mọi điểm giao dịch quan trọng trong toàn bộ hệ thống.
2. **Trung tâm cảnh báo tồn kho thấp (Low Stock Alert Panel)**:
   - Form tự động chạy truy vấn nền quét các sản phẩm trong bảng `MayViTinh` có `SoLuong <= 5`.
   - Hiển thị danh sách cảnh báo màu đỏ/cam nổi bật, giúp thủ kho và nhân viên bán hàng kịp thời lên kế hoạch nhập hàng bổ sung.
3. **Cơ chế lọc đa tiêu chí**:
   - Lọc theo khoảng ngày (`TuNgay` - `DenNgay`).
   - Lọc theo người dùng thực hiện (`txtNguoiDung`).
   - Lọc theo loại hành động (`cboHanhDong`).

---

## 3. CÁCH HOẠT ĐỘNG & QUY TRÌNH NGHIỆP VỤ (LIFECYCLE & WORKFLOW)

### 3.1. Sơ đồ xử lý truy vết hoạt động
```mermaid
flowchart TD
    A([Thao tác người dùng: Thêm/Sửa/Xóa/Đăng nhập]) --> B[Gọi hàm toàn cục Functions.LogAction]
    B --> C[INSERT INTO tblNhatKyHoatDong ThoiGian, NguoiDung, HanhDong, ChiTiet]
    C --> D[(Cơ sở dữ liệu SQL Server)]
    D -->|Khi mở frmNhatKyHoatDong| E[Nạp danh sách log mới nhất lên DataGridView]
    E --> F[Chạy truy vấn quét tồn kho: SELECT * FROM MayViTinh WHERE SoLuong <= 5]
    F --> G[Hiển thị danh sách cảnh báo lên thẻ Low Stock Alert]
    G --> H[/Người dùng lọc theo Ngày, Người Dùng, Hành Động/]
    H --> I[Chạy câu truy vấn lọc có điều kiện & Refresh lưới dữ liệu]
    I --> J{Admin nhấn [Xóa Nhật Ký]?}
    J -- Không --> K([Kết thúc tra cứu])
    J -- Có --> L{Xác nhận quyền Admin & Hỏi lần 2}
    L -- Đồng ý --> M[Thực thi DELETE log cũ hơn 30 ngày hoặc toàn bộ]
```

---

## 4. CHI TIẾT MÃ NGUỒN & PHƯƠNG THỨC CHÍNH

### 4.1. Bảng danh sách phương thức
| Phương thức | Phạm vi | Chức năng chi tiết |
|:---|:---|:---|
| `frmNhatKyHoatDong_Load` | `private void` | Cấu hình theme, đặt khoảng thời gian lọc mặc định (7 ngày gần nhất), nạp danh sách log và cảnh báo |
| `LoadLogs` | `private void` | Thực thi câu lệnh `SELECT TOP 500 ... ORDER BY ThoiGian DESC` nạp vào DataGridView |
| `LoadLowStockAlerts` | `private void` | Truy vấn các dòng máy tính có tồn kho dưới ngưỡng an toàn ($\le 5$) nạp vào danh sách cảnh báo |
| `btnLoc_Click` | `private void` | Xây dựng câu truy vấn lọc theo các tiêu chí người dùng thiết lập |
| `btnLamMoi_Click` | `private void` | Khôi phục bộ lọc mặc định và tải lại toàn bộ nhật ký mới nhất |
| `btnXoaNhatKy_Click` | `private void` | Kiểm tra quyền Admin tối cao và cho phép dọn dẹp các bản ghi nhật ký đã quá cũ |

### 4.2. Khối mã hàm ghi log toàn cục trong Functions.cs
```csharp
public static void LogAction(string hanhDong, string chiTiet)
{
    try
    {
        string sql = @"INSERT INTO tblNhatKyHoatDong(ThoiGian, NguoiDung, HanhDong, ChiTiet) 
                       VALUES(GETDATE(), @user, @action, @detail)";
        using (SqlCommand cmd = new SqlCommand(sql, Conn))
        {
            cmd.Parameters.AddWithValue("@user", !string.IsNullOrEmpty(CurrentUsername) ? CurrentUsername : "System");
            cmd.Parameters.AddWithValue("@action", hanhDong);
            cmd.Parameters.AddWithValue("@detail", chiTiet);
            cmd.ExecuteNonQuery();
        }
    }
    catch
    {
    }
}
```

---

## 5. ẢNH CHỤP MINH CHỨNG GIAO DIỆN
- **Chế độ Sáng (Light Mode)**: `Screenshots/frmNhatKyHoatDong_Light.png`
- **Chế độ Tối (Dark Mode)**: `Screenshots/frmNhatKyHoatDong_Dark.png`
- **Giao diện bảng nhật ký & cảnh báo tồn kho**: `Screenshots/Verified/07_NhatKy_Fixed.png`

