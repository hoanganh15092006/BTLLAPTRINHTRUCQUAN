# BÁO CÁO KỸ THUẬT & HƯỚNG DẪN HOẠT ĐỘNG: FORM CÀI ĐẶT HỆ THỐNG & CSDL (frmCaiDat)

---

## 1. TỔNG QUAN VỀ FORM
- **Tên lớp (Class Name)**: `frmCaiDat`
- **Tập tin mã nguồn**:
  - Code-behind: [Forms/frmCaiDat.cs](file:///d:/HOCTRENLOP/LAPTRINHTRUCQUAN/BTLLAPTRINHTRUCQUAN/Forms/frmCaiDat.cs)
  - UI Designer: [Forms/frmCaiDat.Designer.cs](file:///d:/HOCTRENLOP/LAPTRINHTRUCQUAN/BTLLAPTRINHTRUCQUAN/Forms/frmCaiDat.Designer.cs)
- **Mục đích nghiệp vụ**: Phân hệ trung tâm cấu hình hạ tầng của phần mềm. Cung cấp chức năng kiểm tra và thiết lập chuỗi kết nối SQL Server linh hoạt, tùy biến thông tin pháp nhân cửa hàng in trên hóa đơn, quản lý sao lưu/phục hồi CSDL (Backup/Restore .bak) và cấu hình chủ đề giao diện toàn cục.
- **Phân quyền truy cập (RBAC)**: `Admin` (Cũng có thể mở độc lập từ màn hình Đăng Nhập khi CSDL bị lỗi kết nối).

---

## 2. CÔNG NGHỆ, QUẢN TRỊ CSDL & SAO LƯU DỰ PHÒNG
1. **Kiến trúc cấu hình khóa - giá trị (Key-Value Store)**:
   - Sử dụng bảng CSDL `tblCauHinh` với cấu trúc `(MaKey NVARCHAR(50), GiaTri NVARCHAR(MAX))` để lưu trữ động các tham số hệ thống mà không cần build lại mã nguồn:
     - `TenCuaHang`: Tên doanh nghiệp/cửa hàng hiển thị trên tiêu đề hóa đơn.
     - `DiaChiCuaHang`: Địa chỉ trụ sở chính in trên hóa đơn xuất khẩu.
     - `Hotline`: Số điện thoại đường dây nóng hỗ trợ khách hàng.
     - `Theme`: Trạng thái giao diện mặc định (`Dark` hoặc `Light`).
2. **Kiểm tra kết nối SQL Server chủ động (Active Connection Test)**:
   - Cho phép người dùng nhập Server Name (ví dụ: `.\SQLEXPRESS01`, `localhost`), Database Name (`QLBanMayViTinh`), tùy chọn xác thực Windows Authentication hoặc SQL Server Authentication (User/Password).
   - Nút [Kiểm Tra Kết Nối] thử nghiệm mở kết nối ngắn (`Timeout = 5s`) và phản hồi trạng thái tức thời không làm treo giao diện.
3. **Cơ chế Sao lưu (Backup) và Phục hồi (Restore) CSDL**:
   - Sử dụng lệnh T-SQL tiêu chuẩn `BACKUP DATABASE QLBanMayViTinh TO DISK = @path` tạo file sao lưu đầy đủ `.bak`.
   - Lệnh `RESTORE DATABASE QLBanMayViTinh FROM DISK = @path WITH REPLACE` cho phép khôi phục dữ liệu nhanh chóng khi gặp sự cố phần cứng.

---

## 3. CÁCH HOẠT ĐỘNG & QUY TRÌNH NGHIỆP VỤ (LIFECYCLE & WORKFLOW)

### 3.1. Sơ đồ xử lý cấu hình kết nối CSDL
```mermaid
flowchart TD
    A([Người dùng mở Cài Đặt Hệ Thống]) --> B[Tải các cấu hình hiện tại từ tblCauHinh]
    B --> C[/Người dùng chỉnh sửa Server Name hoặc Database Name/]
    C --> D{Nhấn nút [Kiểm Tra Kết Nối]}
    D --> E[Tạo thử SqlConnection tạm thời với Timeout = 5s]
    E --> F{Kết nối thành công?}
    F -- Thất bại --> G[Báo lỗi chi tiết: Sai tên Server hoặc tài khoản]
    F -- Thành công --> H[Thông báo: Kết nối SQL Server thành công!]
    H --> I{Nhấn nút [Lưu Cấu Hình]}
    I --> J[Ghi chuỗi kết nối vào app.config / lưu bảng cấu hình]
    J --> K[Gán lại Functions.Conn & Cập nhật trạng thái CSDL trên Header]
    K --> L[Ghi Audit Log & Hoàn tất]
```

---

## 4. CHI TIẾT MÃ NGUỒN & PHƯƠNG THỨC CHÍNH

### 4.1. Bảng danh sách phương thức
| Phương thức | Phạm vi | Chức năng chi tiết |
|:---|:---|:---|
| `frmCaiDat_Load` | `private void` | Cấu hình theme, nạp thông tin cửa hàng và thiết lập kết nối hiện thời |
| `LoadStoreSettings` | `private void` | Đọc các key `TenCuaHang`, `DiaChiCuaHang`, `Hotline` từ `tblCauHinh` |
| `btnTestConnect_Click` | `private void` | Kiểm tra khả năng kết nối tới SQL Server theo các thông số nhập trên form |
| `btnLuuConnect_Click` | `private void` | Lưu chuỗi kết nối mới và tái thiết lập kết nối toàn cục |
| `btnLuuCuaHang_Click` | `private void` | Cập nhật thông tin cửa hàng vào `tblCauHinh` để in lên hóa đơn |
| `btnLuuGiaoDien_Click` | `private void` | Lưu tùy chọn giao diện sáng/tối và thông báo thay đổi tới toàn bộ form con |
| `SaveConfigKey` | `private void` | Hàm tiện ích thực thi lệnh MERGE/UPSERT vào bảng `tblCauHinh` |

### 4.2. Khối mã hàm lưu cấu hình động (SaveConfigKey)
```csharp
private void SaveConfigKey(string key, string val)
{
    try
    {
        string sql = @"IF EXISTS (SELECT 1 FROM tblCauHinh WHERE MaKey = @k)
                           UPDATE tblCauHinh SET GiaTri = @v WHERE MaKey = @k
                       ELSE
                           INSERT INTO tblCauHinh(MaKey, GiaTri) VALUES(@k, @v)";
        using (SqlCommand cmd = new SqlCommand(sql, Functions.Conn))
        {
            cmd.Parameters.AddWithValue("@k", key);
            cmd.Parameters.AddWithValue("@v", val);
            cmd.ExecuteNonQuery();
        }
    }
    catch (Exception ex)
    {
        MessageBox.Show("Lỗi lưu cấu hình: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}
```

---

## 5. ẢNH CHỤP MINH CHỨNG GIAO DIỆN
- **Chế độ Sáng (Light Mode)**: `Screenshots/frmCaiDat_Light.png`
- **Chế độ Tối (Dark Mode)**: `Screenshots/frmCaiDat_Dark.png`
- **Giao diện cấu hình tổng thể**: `Screenshots/Verified/08_CaiDat_Fixed.png`

