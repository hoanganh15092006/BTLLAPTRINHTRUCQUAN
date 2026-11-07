# BÁO CÁO KỸ THUẬT & HƯỚNG DẪN HOẠT ĐỘNG: FORM QUẢN LÝ MÁY VI TÍNH (frmMayViTinh)

---

## 1. TỔNG QUAN VỀ FORM
- **Tên lớp (Class Name)**: `frmMayViTinh`
- **Tập tin mã nguồn**:
  - Code-behind: [Forms/frmMayViTinh.cs](file:///d:/HOCTRENLOP/LAPTRINHTRUCQUAN/BTLLAPTRINHTRUCQUAN/Forms/frmMayViTinh.cs)
  - UI Designer: [Forms/frmMayViTinh.Designer.cs](file:///d:/HOCTRENLOP/LAPTRINHTRUCQUAN/BTLLAPTRINHTRUCQUAN/Forms/frmMayViTinh.Designer.cs)
- **Mục đích nghiệp vụ**: Phân hệ cốt lõi quản lý danh mục máy tính và 14 cấu hình linh kiện phần cứng theo chuẩn hóa cơ sở dữ liệu 3NF. Hỗ trợ thao tác Thêm, Sửa, Xóa, Tìm kiếm nhanh, Quản lý hình ảnh sản phẩm và trực tiếp hiệu chỉnh Giá nhập, Giá bán, Tồn kho lưu vào CSDL kèm cơ chế kiểm soát lỗi nghiêm ngặt.
- **Phân quyền truy cập (RBAC)**: `Admin`, `ThuKho`, `BanHang` (BanHang chỉ có quyền xem và tra cứu).

---

## 2. CÔNG NGHỆ & CẤU TRÚC 14 LINH KIỆN PHẦN CỨNG (CHUẨN 3NF)
Mỗi máy vi tính trong hệ thống được cấu thành từ 14 bảng quan hệ khóa ngoại (Foreign Keys) độc lập:
1. `tblLoai`: Loại máy (Laptop văn phòng, PC Gaming, Workstation, All-in-One...)
2. `tblHangSX`: Hãng sản xuất (DELL, HP, ASUS, Lenovo, Apple, MSI...)
3. `tblChip`: Bộ vi xử lý CPU (Intel Core i5, Core i7, AMD Ryzen 5, Ryzen 7...)
4. `tblRAM`: Bộ nhớ trong RAM (8GB DDR4, 16GB DDR5, 32GB...)
5. `tblOCung`: Loại ổ đĩa cứng (SSD NVMe M.2, HDD 3.5 inch, SSD SATA...)
6. `tblDungLuong`: Dung lượng lưu trữ (256 GB, 512 GB, 1024 GB - 1TB...)
7. `tblTocDo`: Tốc độ bus (3200 MHz, 5000 MB/s, 7000 MB/s...)
8. `tblManHinh`: Loại màn hình hiển thị (IPS FHD 144Hz, OLED 4K UHD...)
9. `tblCoMH`: Kích cỡ màn hình (14.0 inch, 15.6 inch, 24.0 inch, 27.0 inch...)
10. `tblChuot`: Chuột đi kèm (Chuột quang văn phòng, Chuột Gaming không dây...)
11. `tblBanPhim`: Bàn phím tích hợp hoặc đi kèm (Phím cơ RGB, Phím chiclet...)
12. `tblUSB`: Cổng USB kết nối (USB 3.0, Type-C Thunderbolt...)
13. `tblOCD`: Ổ đĩa quang (Ổ DVD-RW gắn ngoài, Không trang bị...)
14. `tblLoa`: Hệ thống âm thanh (Loa stereo, Waves MaxxAudio...)

---

## 3. CÁCH HOẠT ĐỘNG & QUY TRÌNH NGHIỆP VỤ (LIFECYCLE & WORKFLOW)

### 3.1. Sơ đồ luồng lưu trữ & kiểm soát lỗi (CRUD Workflow)
```mermaid
flowchart TD
    A([Người dùng chọn Thêm Mới hoặc Sửa]) --> B[Mở khóa các ô nhập liệu & ComboBox linh kiện]
    B --> C[/Nhập Tên máy, Giá nhập, Giá bán, Tồn kho, Bảo hành, Chọn 14 linh kiện/]
    C --> D{Nhấn nút [Lưu]}
    D --> E{Kiểm tra Tên máy có rỗng?}
    E -- Rỗng --> F[Báo lỗi: Vui lòng nhập Tên máy vi tính!]
    E -- Có tên --> G{Kiểm tra Giá nhập & Giá bán?}
    G -- Giá < 0 --> H[Báo lỗi: Giá không được là số âm!]
    G -- Hợp lệ --> I{Kiểm tra Tồn kho?}
    I -- Tồn kho < 0 --> J[Báo lỗi: Số lượng tồn kho không được âm!]
    I -- Tồn kho >= 0 --> K{Giá bán < Giá nhập?}
    K -- Có --> L{Hỏi xác nhận: Giá bán thấp hơn giá nhập có muốn tiếp tục?}
    L -- Hủy --> M[Dừng thao tác]
    L -- Đồng ý --> N[Tiến hành ghi CSDL]
    K -- Không --> N
    N --> O{Trạng thái là Thêm Mới hay Sửa?}
    O -- Thêm mới --> P[Sinh mã máy tự động MVTxx & Chạy INSERT INTO MayViTinh]
    O -- Sửa --> Q[Chạy UPDATE MayViTinh SET ... WHERE MaMay=@m]
    P --> R[Cập nhật lưới DataGridView & Ghi Audit Log]
    Q --> R
    R --> S[Khóa các ô nhập liệu & Nạp lại KPI thẻ Bento]
```

### 3.2. Cơ chế kiểm soát lỗi nhập liệu (Input Validation Mechanism)
- **Lọc ký tự số nguyên (`KeyPress`)**:
  - Ô `txtSoLuong`, `txtBaoHanh` chỉ cho phép gõ số và phím điều khiển `Backspace` (`e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);`).
- **Định dạng tiền tệ tự động (`TextChanged`)**:
  - Ô `txtDonGiaNhap` và `txtDonGiaBan` tự động format dạng số phân tách hàng nghìn (`15,000,000`) giúp người dùng quan sát trực quan không bị nhầm lẫn chữ số 0.
- **Bảo toàn CSDL**: Mọi thao tác đều nằm trong khối `try-catch` đảm bảo khi có sự cố mạng hoặc khóa ngoại CSDL, chương trình hiển thị thông báo lỗi rõ ràng và không bị crash.

---

## 4. CHI TIẾT MÃ NGUỒN & PHƯƠNG THỨC CHÍNH

### 4.1. Bảng danh sách phương thức
| Phương thức | Phạm vi | Chức năng chi tiết |
|:---|:---|:---|
| `frmMayViTinh_Load` | `private void` | Cấu hình styling, nạp dữ liệu 14 ComboBox linh kiện, nạp danh sách máy vào DataGridView |
| `LoadComboBoxes` | `private void` | Nạp đồng thời dữ liệu cho 14 ComboBox từ 14 bảng danh mục linh kiện 3NF |
| `LoadDataGridView` | `private void` | Thực hiện câu truy vấn `SELECT` kết hợp `INNER JOIN` 14 bảng lấy tên chi tiết linh kiện |
| `dgvMayViTinh_CellClick`| `private void` | Khi người dùng chọn 1 dòng trên bảng, bind toàn bộ thông tin linh kiện, ảnh và giá lên form |
| `btnThem_Click` | `private void` | Xóa trắng form, sinh mã máy mới tự động dạng `MVTxx`, chuyển trạng thái `btnLuu` |
| `btnLuu_Click` | `private void` | Kiểm tra tính hợp lệ dữ liệu, thực thi câu lệnh SQL INSERT hoặc UPDATE |
| `btnSua_Click` | `private void` | Mở khóa toàn bộ các ô nhập giá nhập, giá bán, tồn kho và combo cấu hình để chỉnh sửa |
| `btnXoa_Click` | `private void` | Kiểm tra ràng buộc khóa ngoại (đã xuất hiện trong Hóa Đơn Nhập/Bán chưa), xóa máy an toàn |
| `btnChonAnh_Click` | `private void` | Mở hộp thoại `OpenFileDialog` để người dùng chọn ảnh đại diện sản phẩm (`.jpg`, `.png`) |
| `txtTimKiemNhanh_TextChanged`| `private void` | Lọc tức thời danh sách máy vi tính theo Tên hoặc Mã mà không cần load lại form |

### 4.2. Câu lệnh SQL cập nhật trực tiếp Giá nhập, Giá bán, Tồn kho
```csharp
string sql = @"UPDATE MayViTinh SET 
                TenMay = @ten, MaLoai = @loai, MaHangSX = @hang, 
                MaChip = @chip, MaRAM = @ram, MaOCung = @ocung, 
                MaDungLuong = @dungluong, MaTocDo = @tocdo, MaManHinh = @manhinh, 
                MaCoMH = @comh, MaChuot = @chuot, MaBanPhim = @banphim, 
                MaUSB = @usb, MaOCD = @ocd, MaLoa = @loa, 
                DonGiaNhap = @gianhap, DonGiaBan = @giaban, SoLuong = @soluong, 
                ThoiGianBaoHanh = @baohanh, Anh = @anh 
               WHERE MaMay = @mamay";

using (SqlCommand cmd = new SqlCommand(sql, Functions.Conn))
{
    cmd.Parameters.AddWithValue("@mamay", txtMaMay.Text.Trim());
    cmd.Parameters.AddWithValue("@ten", txtTenMay.Text.Trim());
    cmd.Parameters.AddWithValue("@gianhap", giaNhap);
    cmd.Parameters.AddWithValue("@giaban", giaBan);
    cmd.Parameters.AddWithValue("@soluong", soLuong);
    cmd.Parameters.AddWithValue("@baohanh", baoHanh);
    cmd.Parameters.AddWithValue("@anh", (object)duongDanAnh ?? DBNull.Value);
    // ... Gán 14 tham số khóa ngoại linh kiện ...
    cmd.ExecuteNonQuery();
}
```

---

## 5. ẢNH CHỤP MINH CHỨNG GIAO DIỆN
- **Chế độ Sáng (Light Mode)**: `Screenshots/frmMayViTinh_Light.png`
- **Chế độ Tối (Dark Mode)**: `Screenshots/frmMayViTinh_Dark.png`
- **Giao diện khi chọn dòng máy tính**: `Screenshots/Verified/05_MayViTinh_Selected_Row.png`
- **Trạng thái Thêm Mới**: `Screenshots/Verified/05_MayViTinh_State_ThemMoi.png`

