# BÁO CÁO KỸ THUẬT & HƯỚNG DẪN HOẠT ĐỘNG: FORM HÓA ĐƠN NHẬP KHO (frmHoaDonNhap)

---

## 1. TỔNG QUAN VỀ FORM
- **Tên lớp (Class Name)**: `frmHoaDonNhap`
- **Tập tin mã nguồn**:
  - Code-behind: [Forms/frmHoaDonNhap.cs](file:///d:/HOCTRENLOP/LAPTRINHTRUCQUAN/BTLLAPTRINHTRUCQUAN/Forms/frmHoaDonNhap.cs)
  - UI Designer: [Forms/frmHoaDonNhap.Designer.cs](file:///d:/HOCTRENLOP/LAPTRINHTRUCQUAN/BTLLAPTRINHTRUCQUAN/Forms/frmHoaDonNhap.Designer.cs)
- **Mục đích nghiệp vụ**: Quản lý nhập hàng từ Nhà cung cấp theo mô hình Master-Detail (Hóa đơn - Chi tiết hóa đơn). Đảm bảo quản lý giá vốn chính xác, tự động tăng số lượng tồn kho của máy vi tính thông qua cơ chế Database Trigger và in/xuất phiếu nhập hàng.
- **Phân quyền truy cập (RBAC)**: `Admin` và `ThuKho` (Nhân viên bán hàng bị hạn chế truy cập phân hệ này).

---

## 2. CÔNG NGHỆ & CƠ CHẾ DATABASE TRIGGER ĐỒNG BỘ KHO
1. **Kiến trúc Master-Detail trong SQL Server**:
   - Bảng Master: `tblHoaDonNhap` (`SoHDN`, `MaNV`, `NgayNhap`, `MaNCC`, `TongTien`).
   - Bảng Detail: `tblChiTietHDNhap` (`SoHDN`, `MaMay`, `SoLuong`, `DonGia`, `GiamGia`, `ThanhTien`).
2. **Cơ chế Trigger tự động tăng tồn kho & cập nhật giá vốn**:
   - Khi thêm dòng chi tiết nhập hàng, Trigger `trg_ChiTietHDNhap_Insert` trên SQL Server tự động:
     - Tăng số lượng tồn: `UPDATE MayViTinh SET SoLuong = SoLuong + inserted.SoLuong WHERE MaMay = inserted.MaMay`.
     - Cập nhật giá nhập mới nhất: `UPDATE MayViTinh SET DonGiaNhap = inserted.DonGia WHERE MaMay = inserted.MaMay`.
     - Tính tổng tiền hóa đơn: `UPDATE tblHoaDonNhap SET TongTien = (SELECT SUM(ThanhTien) FROM tblChiTietHDNhap WHERE SoHDN = ...)`.
   - Khi xóa dòng chi tiết (Double-click dòng hàng), Trigger `trg_ChiTietHDNhap_Delete` tự động trừ lùi số lượng tồn kho tương ứng và cập nhật lại tổng tiền.
3. **Bố cục Responsive & Chống cắt chữ (Zero Truncation)**:
   - Các nhãn và ô nhập trong khu vực Detail Card sử dụng `AutoSize = true` và hàm `LayoutDetailCard()` tính toán tọa độ luồng động (`curX = lbl.Right + 5; curX = control.Right + 12;`), loại bỏ hoàn toàn hiện tượng co giật hoặc cắt chữ khi đổi độ phân giải.

---

## 3. CÁCH HOẠT ĐỘNG & QUY TRÌNH NGHIỆP VỤ (LIFECYCLE & WORKFLOW)

### 3.1. Sơ đồ quy trình lập phiếu nhập hàng
```mermaid
flowchart TD
    A([Người dùng nhấn [Thêm Hóa Đơn]]) --> B[Tạo mã phiếu tự động HDN_yyyyMMdd_HHmmss]
    B --> C[Khóa mã phiếu, gán ngày hiện tại, mở chọn NCC & NV]
    C --> D[/Chọn Mặt hàng cần nhập, nhập Số lượng & Đơn giá nhập/]
    D --> E{Kiểm tra Số lượng > 0 & Đơn giá >= 0?}
    E -- Không hợp lệ --> F[Báo lỗi: Số lượng phải > 0 và đơn giá không âm]
    E -- Hợp lệ --> G[Tự động tính Thành tiền = Số lượng * Đơn giá - Giảm giá]
    G --> H{Nhấn nút [Lưu Hóa Đơn]}
    H --> I{Phiếu nhập đã tồn tại trong tblHoaDonNhap?}
    I -- Chưa --> J[INSERT INTO tblHoaDonNhap Master]
    I -- Đã có --> K[Bỏ qua bước tạo Master]
    J --> L{Mặt hàng này đã có trong chi tiết phiếu chưa?}
    K --> L
    L -- Đã có --> M[Báo lỗi: Mặt hàng này đã có trong hóa đơn, vui lòng xóa dòng cũ để sửa lại!]
    L -- Chưa có --> N[INSERT INTO tblChiTietHDNhap]
    N --> O[SQL Server Trigger kích hoạt: Tăng tồn kho & Cập nhật tổng tiền]
    O --> P[Nạp lại bảng dgvChiTiet & Hiển thị tổng tiền mới]
    P --> Q[Ghi Audit Log & Thông báo thành công]
```

### 3.2. Chức năng hủy hàng trực tiếp trên lưới (Double-click Delete)
- Khi người dùng nhấp đúp (Double-click) vào bất kỳ dòng nào trong bảng `dgvChiTiet`:
  1. Hệ thống hiển thị hộp thoại xác nhận: *"Bạn có chắc chắn muốn xóa mặt hàng này khỏi hóa đơn nhập? Số lượng tồn kho sẽ được hoàn trả lùi tự động."*
  2. Nếu xác nhận Có: Thực thi lệnh `DELETE FROM tblChiTietHDNhap WHERE SoHDN = @shdn AND MaMay = @mm`.
  3. Trigger CSDL tự động trừ lùi số lượng trong kho của `MayViTinh`.
  4. Nạp lại lưới và cập nhật tổng tiền master.

---

## 4. CHI TIẾT MÃ NGUỒN & PHƯƠNG THỨC CHÍNH

### 4.1. Bảng danh sách phương thức
| Phương thức | Phạm vi | Chức năng chi tiết |
|:---|:---|:---|
| `frmHoaDonNhap_Load` | `private void` | Nạp dữ liệu các ComboBox Nhân viên, NCC, Máy tính; cấu hình theme và layout |
| `LayoutDetailCard` | `private void` | Căn chỉnh tọa độ dòng chọn sản phẩm nhập hàng tự co giãn theo độ dài font chữ |
| `btnThemHD_Click` | `private void` | Sinh mã hóa đơn mới theo thời gian thực `HDN_...`, xóa trắng form, chuyển trạng thái cho phép nhập |
| `cboMaMay_SelectedIndexChanged`| `private void` | Khi chọn máy vi tính, tự động điền Tên máy và giá nhập tham chiếu hiện tại |
| `txtSoLuong_TextChanged` | `private void` | Tính toán tức thời `ThanhTienLine` khi số lượng thay đổi |
| `txtDonGia_TextChanged` | `private void` | Tính toán tức thời `ThanhTienLine` khi đơn giá nhập thay đổi |
| `btnLuuHD_Click` | `private void` | Thẩm định các trường bắt buộc, chèn Master (nếu chưa có) và chèn Detail vào CSDL |
| `dgvChiTiet_CellDoubleClick` | `private void` | Xóa mặt hàng khỏi phiếu nhập và kích hoạt trigger hoàn trả tồn kho |
| `btnInHD_Click` | `private void` | Xuất chi tiết hóa đơn nhập ra file Excel hoặc phiếu in báo cáo |
| `cboTimHDN_DropDown` | `private void` | Nạp danh sách các mã hóa đơn nhập sẵn có để người dùng tra cứu lại |

### 4.2. Khối mã chèn dữ liệu giao dịch nhập kho
```csharp
if (!Functions.CheckKey("SELECT SoHDN FROM tblHoaDonNhap WHERE SoHDN = N'" + maHDN + "'"))
{
    string sqlMaster = @"INSERT INTO tblHoaDonNhap(SoHDN, NgayNhap, MaNV, MaNCC, TongTien) 
                         VALUES(@sohdn, @ngay, @manv, @mancc, @tongtien)";
    using (SqlCommand cmdM = new SqlCommand(sqlMaster, Functions.Conn))
    {
        cmdM.Parameters.AddWithValue("@sohdn", maHDN);
        cmdM.Parameters.AddWithValue("@ngay", dtpNgayNhap.Value);
        cmdM.Parameters.AddWithValue("@manv", cboMaNV.SelectedValue);
        cmdM.Parameters.AddWithValue("@mancc", cboMaNCC.SelectedValue);
        cmdM.Parameters.AddWithValue("@tongtien", 0);
        cmdM.ExecuteNonQuery();
    }
}

string sqlDetail = @"INSERT INTO tblChiTietHDNhap(SoHDN, MaMay, SoLuong, DonGia, GiamGia, ThanhTien) 
                     VALUES(@sohdn, @mamay, @sl, @dg, @gg, @tt)";
using (SqlCommand cmdD = new SqlCommand(sqlDetail, Functions.Conn))
{
    cmdD.Parameters.AddWithValue("@sohdn", maHDN);
    cmdD.Parameters.AddWithValue("@mamay", cboMaMay.SelectedValue);
    cmdD.Parameters.AddWithValue("@sl", soLuong);
    cmdD.Parameters.AddWithValue("@dg", donGia);
    cmdD.Parameters.AddWithValue("@gg", giamGia);
    cmdD.Parameters.AddWithValue("@tt", thanhTien);
    cmdD.ExecuteNonQuery();
}
```

---

## 5. ẢNH CHỤP MINH CHỨNG GIAO DIỆN
- **Chế độ Sáng (Light Mode)**: `Screenshots/frmHoaDonNhap_Light.png`
- **Chế độ Tối (Dark Mode)**: `Screenshots/frmHoaDonNhap_Dark.png`
- **Trạng thái Tạo mới hóa đơn**: `Screenshots/Verified/05_HoaDonNhap_ClickTaoMoi.png`
- **Chọn sản phẩm & Tính thành tiền**: `Screenshots/Verified/05_HoaDonNhap_SelectProduct_Calculated.png`

