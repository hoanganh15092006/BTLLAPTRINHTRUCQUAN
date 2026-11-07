# BÁO CÁO KỸ THUẬT & HƯỚNG DẪN HOẠT ĐỘNG: FORM HÓA ĐƠN BÁN HÀNG (frmHoaDonBan)

---

## 1. TỔNG QUAN VỀ FORM
- **Tên lớp (Class Name)**: `frmHoaDonBan`
- **Tập tin mã nguồn**:
  - Code-behind: [Forms/frmHoaDonBan.cs](file:///d:/HOCTRENLOP/LAPTRINHTRUCQUAN/BTLLAPTRINHTRUCQUAN/Forms/frmHoaDonBan.cs)
  - UI Designer: [Forms/frmHoaDonBan.Designer.cs](file:///d:/HOCTRENLOP/LAPTRINHTRUCQUAN/BTLLAPTRINHTRUCQUAN/Forms/frmHoaDonBan.Designer.cs)
- **Mục đích nghiệp vụ**: Phân hệ xử lý giao dịch bán hàng cốt lõi của doanh nghiệp. Thực hiện lập hóa đơn bán lẻ cho khách hàng, tự động kiểm tra số lượng tồn kho (chống xuất âm), tự động giảm trừ tồn kho qua Trigger CSDL, cảnh báo bán dưới giá vốn và hỗ trợ in hóa đơn xuất khẩu.
- **Phân quyền truy cập (RBAC)**: `Admin` và `BanHang` (Nhân viên thủ kho bị hạn chế tạo hóa đơn bán).

---

## 2. CÔNG NGHỆ & CƠ CHẾ KIỂM SOÁT TỒN KHO (CHỐNG XUẤT ÂM)
1. **Kiến trúc dữ liệu Master-Detail**:
   - Bảng Master: `tblHoaDonBan` (`SoHDB`, `MaNV`, `NgayBan`, `MaKhach`, `TongTien`).
   - Bảng Detail: `tblChiTietHDBan` (`SoHDB`, `MaMay`, `SoLuong`, `DonGia`, `GiamGia`, `ThanhTien`).
2. **Cơ chế chống xuất âm kho (Negative Stock Prevention)**:
   - Trước khi lưu bất kỳ dòng sản phẩm nào vào chi tiết hóa đơn bán, phần mềm thực hiện kiểm tra 2 lớp:
     - **Lớp 1 (Application Level)**: Đọc số lượng tồn kho khả dụng hiện tại từ `MayViTinh`. Nếu `SoLuongBan > TonKho`, chặn ngay lập tức và hiển thị cảnh báo: *"Chỉ còn lại {TonKho} sản phẩm trong kho. Không thể bán vượt quá số lượng tồn!"*.
     - **Lớp 2 (Database Trigger Level)**: Trigger `trg_ChiTietHDBan_Insert` kiểm tra tính hợp lệ số học và trừ tồn kho tự động:
       ```sql
       UPDATE MayViTinh SET SoLuong = SoLuong - inserted.SoLuong WHERE MaMay = inserted.MaMay
       ```
3. **Cơ chế hoàn kho tự động khi hủy đơn**:
   - Khi nhân viên nhấp đúp (Double-click) vào mặt hàng trên lưới để xóa hoặc hủy hóa đơn, Trigger `trg_ChiTietHDBan_Delete` tự động cộng bù lại số lượng tồn kho tương ứng:
     ```sql
     UPDATE MayViTinh SET SoLuong = SoLuong + deleted.SoLuong WHERE MaMay = deleted.MaMay
     ```
4. **Bố cục Responsive & Chống cắt chữ (Zero Truncation)**:
   - Toàn bộ nhãn hiển thị tại Detail Card (`lblMaMay`, `lblTenMay`, `lblTonKho`, `lblSoLuong`, `lblDonGia`, `lblThanhTien`) đều được cấu hình `AutoSize = true`.
   - Hàm `LayoutDetailCard()` tính toán tọa độ X tự động theo độ rộng thực tế của nhãn tiếng Việt có dấu, triệt tiêu hoàn toàn hiện tượng co giật hoặc cắt chữ.

---

## 3. CÁCH HOẠT ĐỘNG & QUY TRÌNH NGHIỆP VỤ (LIFECYCLE & WORKFLOW)

### 3.1. Sơ đồ quy trình bán hàng & kiểm soát kho
```mermaid
flowchart TD
    A([Nhấn nút [Thêm Hóa Đơn]]) --> B[Tạo mã tự động HDB_yyyyMMdd_HHmmss]
    B --> C[Hiển thị Ngày bán, chọn Nhân viên & Khách hàng]
    C --> D[/Chọn Mã máy vi tính cần bán/]
    D --> E[Hiển thị Tên máy, Giá bán niêm yết và Số lượng tồn kho khả dụng]
    E --> F[/Nhập Số lượng bán & Tỷ lệ giảm giá/]
    F --> G{Kiểm tra Số lượng bán > Tồn kho?}
    G -- Có (Vượt tồn) --> H[Báo lỗi: Kho chỉ còn {TonKho} máy, không thể bán!]
    G -- Hợp lệ --> I{Đơn giá bán < Đơn giá nhập?}
    I -- Bán lỗ --> J{Cảnh báo: Bán dưới giá nhập! Có chắc chắn muốn tiếp tục?}
    J -- Hủy --> K[Dừng thao tác]
    J -- Tiếp tục --> L[Tính Thành tiền = Số lượng * Giá bán - Giảm giá]
    I -- Không bán lỗ --> L
    L --> M{Nhấn nút [Lưu Hóa Đơn]}
    M --> N{Hóa đơn Master đã có chưa?}
    N -- Chưa có --> O[INSERT INTO tblHoaDonBan]
    N -- Đã có --> P[Bỏ qua bước Master]
    O --> Q[INSERT INTO tblChiTietHDBan]
    P --> Q
    Q --> R[SQL Server Trigger kích hoạt: Trừ kho MayViTinh & Cập nhật Tổng tiền]
    R --> S[Cập nhật lưới dgvChiTiet & Hiển thị tổng tiền chữ tiếng Việt]
    S --> T[Ghi Audit Log & Sẵn sàng In hóa đơn]
```

### 3.2. Chuyển đổi số tiền thành chữ tiếng Việt
- Hệ thống tích hợp hàm `Functions.ChuyenSoSangChu(tongTien)` chuyển đổi số tiền số học (ví dụ: `28,500,000`) thành chuỗi văn bản pháp lý chuyên nghiệp trên hóa đơn (*"Hai mươi tám triệu năm trăm nghìn đồng chẵn"*).

---

## 4. CHI TIẾT MÃ NGUỒN & PHƯƠNG THỨC CHÍNH

### 4.1. Bảng danh sách phương thức
| Phương thức | Phạm vi | Chức năng chi tiết |
|:---|:---|:---|
| `frmHoaDonBan_Load` | `private void` | Nạp danh mục nhân viên, khách hàng, sản phẩm; cấu hình theme và layout chi tiết |
| `LayoutDetailCard` | `private void` | Tự động căn chỉnh khoảng cách các ô nhập sản phẩm trên một hàng ngang không bị che khuất |
| `cboMaMay_SelectedIndexChanged`| `private void` | Lấy dữ liệu tên máy, đơn giá bán và số lượng tồn kho khả dụng từ CSDL nạp lên giao diện |
| `txtSoLuong_TextChanged` | `private void` | Kiểm tra tức thời số lượng bán so với tồn kho và tính lại thành tiền |
| `btnThemHD_Click` | `private void` | Sinh mã hóa đơn mới theo thời gian thực `HDB_...`, mở khóa controls |
| `btnLuuHD_Click` | `private void` | Thẩm định các trường, chèn Master (nếu chưa có) và chèn Detail vào CSDL |
| `dgvChiTiet_CellDoubleClick` | `private void` | Hủy dòng sản phẩm, gọi trigger SQL Server để hoàn trả tồn kho tự động |
| `btnInHD_Click` | `private void` | Kết xuất hóa đơn chi tiết ra file Excel chuyên nghiệp kèm đầy đủ thông tin đối tác |
| `btnHuyHD_Click` | `private void` | Xóa toàn bộ hóa đơn hiện tại và hoàn kho tất cả sản phẩm trong chi tiết |

### 4.2. Khối mã kiểm tra chống xuất âm kho trước khi lưu
```csharp
int slMua = int.Parse(txtSoLuong.Text);
int slTon = int.Parse(Functions.GetFieldValues("SELECT SoLuong FROM MayViTinh WHERE MaMay = N'" + cboMaMay.SelectedValue + "'"));

if (slMua > slTon)
{
    MessageBox.Show("Số lượng mặt hàng này chỉ còn " + slTon + " chiếc trong kho. Vui lòng nhập số lượng hợp lệ!", 
                    "Cảnh Báo Xuất Âm Kho", MessageBoxButtons.OK, MessageBoxIcon.Warning);
    txtSoLuong.Focus();
    return;
}
```

---

## 5. ẢNH CHỤP MINH CHỨNG GIAO DIỆN
- **Chế độ Sáng (Light Mode)**: `Screenshots/frmHoaDonBan_Light.png`
- **Chế độ Tối (Dark Mode)**: `Screenshots/frmHoaDonBan_Dark.png`
- **Giao diện khi chọn sản phẩm & Tính thành tiền**: `Screenshots/Verified/04_HoaDonBan_SelectProduct_Calculated.png`
- **Trạng thái Thêm Mới hóa đơn**: `Screenshots/Verified/04_HoaDonBan_ClickTaoMoi.png`

