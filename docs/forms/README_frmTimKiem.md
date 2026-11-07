# BÁO CÁO KỸ THUẬT & HƯỚNG DẪN HOẠT ĐỘNG: FORM TÌM KIẾM ĐA NĂNG (frmTimKiem)

---

## 1. TỔNG QUAN VỀ FORM
- **Tên lớp (Class Name)**: `frmTimKiem`
- **Tập tin mã nguồn**:
  - Code-behind: [Forms/frmTimKiem.cs](file:///d:/HOCTRENLOP/LAPTRINHTRUCQUAN/BTLLAPTRINHTRUCQUAN/Forms/frmTimKiem.cs)
  - UI Designer: [Forms/frmTimKiem.Designer.cs](file:///d:/HOCTRENLOP/LAPTRINHTRUCQUAN/BTLLAPTRINHTRUCQUAN/Forms/frmTimKiem.Designer.cs)
- **Mục đích nghiệp vụ**: Phân hệ tra cứu và truy vấn đa chiều theo yêu cầu đồ án (Yêu cầu 4). Cho phép tìm kiếm linh hoạt trên 3 thực thể lớn của hệ thống: Máy Vi Tính, Hóa Đơn Nhập Kho và Hóa Đơn Bán Hàng theo các tiêu chí kết hợp (Mã, Tên, Nhân viên, Đối tác, Khoảng thời gian, Khoảng giá).
- **Phân quyền truy cập (RBAC)**: Tất cả các vai trò (`Admin`, `ThuKho`, `BanHang`).

---

## 2. CÔNG NGHỆ & THIẾT KẾ GIAO DIỆN CHỐNG CẮT CHỮ
1. **Kiến trúc chuyển đổi tiêu chí động (Dynamic Criteria Switcher)**:
   - Panel chuyển đổi Radio Button (`pnlRadioSwitch`) có kích thước rộng $560\text{ px}$, chứa 3 tùy chọn:
     - `rdoMayViTinh`: Tìm kiếm máy vi tính theo cấu hình & tầm giá.
     - `rdoHoaDonNhap`: Tìm kiếm phiếu nhập hàng theo ngày & Nhà Cung Cấp.
     - `rdoHoaDonBan`: Tìm kiếm hóa đơn bán lẻ theo ngày & Khách Hàng.
   - Nhãn đối tác `lblDoiTac` và các điều khiển phụ trợ được bật `AutoSize = true` và tự động hoán đổi tiêu đề: *"Nhà Cung Cấp:"* khi chọn Hóa đơn nhập và *"Khách Hàng:"* khi chọn Hóa đơn bán.
2. **Hàm bố cục động `LayoutDynamicControls()`**:
   - Tự động đo đạc độ rộng thực tế của nhãn tiếng Việt và định vị các ComboBox `cboDoiTac`, `cboMatHang`, `cboNhanVien`, loại bỏ hoàn toàn lỗi tràn chữ hay cắt cụt chữ.
3. **Kỹ thuật sinh câu truy vấn SQL động (Dynamic SQL Builder)**:
   - Hệ thống xây dựng mệnh đề `WHERE` linh hoạt dựa trên các ô có nhập liệu thực tế (`WHERE 1=1 AND ...`), kết hợp tham số hóa `@param` an toàn tuyệt đối trước tấn công SQL Injection.

---

## 3. CÁCH HOẠT ĐỘNG & QUY TRÌNH NGHIỆP VỤ (LIFECYCLE & WORKFLOW)

### 3.1. Sơ đồ xử lý tìm kiếm đa tiêu chí
```mermaid
flowchart TD
    A([Người dùng chọn chế độ tìm kiếm]) --> B{Loại đối tượng?}
    B -- Máy Vi Tính --> C[Ẩn bộ lọc ngày tháng & Hiện bộ lọc Cấu hình, Giá bán]
    B -- Hóa Đơn Nhập --> D[Hiện lọc từ ngày - đến ngày, Đối tác: Nhà Cung Cấp]
    B -- Hóa Đơn Bán --> E[Hiện lọc từ ngày - đến ngày, Đối tác: Khách Hàng]
    C --> F[/Nhập từ khóa hoặc chọn tiêu chí/]
    D --> F
    E --> F
    F --> G{Nhấn nút [Bắt Đầu Tìm]}
    G --> H[Khởi tạo câu lệnh SQL gốc với WHERE 1=1]
    H --> I[Kiểm tra từng tiêu chí: Thêm AND Col = @Val vào câu truy vấn]
    I --> J[Thực thi qua SqlDataAdapter & Nạp vào DataTable]
    J --> K{Số bản ghi tìm thấy?}
    K -- 0 bản ghi --> L[Hiển thị thông báo: Không tìm thấy kết quả nào phù hợp]
    K -- > 0 bản ghi --> M[Gán DataSource cho dgvKetQua & Cập nhật thanh trạng thái]
```

---

## 4. CHI TIẾT MÃ NGUỒN & PHƯƠNG THỨC CHÍNH

### 4.1. Bảng danh sách phương thức
| Phương thức | Phạm vi | Chức năng chi tiết |
|:---|:---|:---|
| `frmTimKiem_Load` | `private void` | Cấu hình theme, đặt mặc định tìm kiếm Máy Vi Tính, nạp danh mục tìm kiếm |
| `HienThiDieuKien` | `private void` | Ẩn/hiện các ô nhập liệu tùy theo chế độ tìm kiếm đang được chọn |
| `LayoutDynamicControls`| `private void` | Căn chỉnh tọa độ ngang của các label và combobox theo kích thước font chữ |
| `btnTimKiem_Click` | `private void` | Phân nhánh xử lý tìm kiếm theo 3 chế độ, xây dựng câu SQL động và nạp lên lưới |
| `btnLamMoi_Click` | `private void` | Xóa trắng toàn bộ các tiêu chí tìm kiếm và nạp lại kết quả mặc định |

### 4.2. Khối mã tìm kiếm Hóa đơn bán động
```csharp
string sql = @"SELECT hdb.SoHDB AS [Mã HĐ], hdb.NgayBan AS [Ngày Bán], 
                      nv.TenNV AS [Nhân Viên Lập], kh.TenKhach AS [Khách Hàng], 
                      hdb.TongTien AS [Tổng Tiền]
               FROM tblHoaDonBan hdb
               LEFT JOIN NhanVien nv ON hdb.MaNV = nv.MaNV
               LEFT JOIN KhachHang kh ON hdb.MaKhach = kh.MaKhach
               WHERE 1=1";

SqlCommand cmd = new SqlCommand();
if (!string.IsNullOrEmpty(txtMaHD.Text.Trim()))
{
    sql += " AND hdb.SoHDB LIKE @mahd";
    cmd.Parameters.AddWithValue("@mahd", "%" + txtMaHD.Text.Trim() + "%");
}
if (chkNgay.Checked)
{
    sql += " AND hdb.NgayBan BETWEEN @tungay AND @denngay";
    cmd.Parameters.AddWithValue("@tungay", dtpTuNgay.Value.Date);
    cmd.Parameters.AddWithValue("@denngay", dtpDenNgay.Value.Date.AddDays(1).AddSeconds(-1));
}
if (cboDoiTac.SelectedIndex > 0)
{
    sql += " AND hdb.MaKhach = @makh";
    cmd.Parameters.AddWithValue("@makh", cboDoiTac.SelectedValue);
}
// ... Thực thi và nạp lên DataGridView ...
```

---

## 5. ẢNH CHỤP MINH CHỨNG GIAO DIỆN
- **Chế độ Sáng (Light Mode)**: `Screenshots/frmTimKiem_Light.png`
- **Chế độ Tối (Dark Mode)**: `Screenshots/frmTimKiem_Dark.png`
- **Tìm kiếm Hóa Đơn Nhập (Hiện đủ chữ Nhà Cung Cấp)**: `Screenshots/frmTimKiem_HoaDonNhap_Verified.png`
- **Kết quả tìm kiếm Máy Vi Tính**: `Screenshots/Verified/02_TimKiem_MVT_Searched.png`

