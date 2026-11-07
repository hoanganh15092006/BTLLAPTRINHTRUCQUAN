# NHẬT KÝ PHÁT TRIỂN & BÁO CÁO KIỂM THỬ TỰ ĐỘNG HỆ THỐNG (DEVLOG)
## HỆ THỐNG QUẢN LÝ BÁN MÁY VI TÍNH (QLBanMayViTinh) - MODERN .NET 4.8 WINFORMS ARCHITECTURE

---

## 1. SƠ ĐỒ KIẾN TRÚC & LUỒNG DỮ LIỆU (GRAPHIFY DEPENDENCY & CALL MAPPING)

Dự án áp dụng mô hình kiến trúc phân tầng 3-Tier Enterprise tối ưu cho Windows Forms .NET Framework 4.8 kết hợp ADO.NET thuần, bảo mật lớp ứng dụng AES-256/PBKDF2 và tính toàn vẹn dữ liệu tự động tại tầng cơ sở dữ liệu qua SQL Server Triggers.

### 1.1. Sơ Đồ Toàn Cảnh Kiến Trúc Hệ Thống (3-Tier Architecture)

```mermaid
graph TD
    subgraph UI_Layer ["Tầng Giao Diện (Presentation Layer - 14 Forms)"]
        F01["frmDangNhap"] --> F02["frmDangKy"]
        F01 --> F03["frmQuenMatKhau"]
        F01 --> F04["frmMain (MDI Master Container)"]
        
        F04 --> F05["frmMayViTinh"]
        F04 --> F06["frmDanhMucHeThong"]
        F04 --> F07["frmHoaDonNhap"]
        F04 --> F08["frmHoaDonBan"]
        F04 --> F09["frmTimKiem"]
        F04 --> F10["frmBaoCaoThongKe"]
        F04 --> F11["frmQuanLyNguoiDung"]
        F04 --> F12["frmHoSoCaNhan"]
        F04 --> F13["frmNhatKyHoatDong"]
        F04 --> F14["frmCaiDat"]
    end

    subgraph Theme_Engine ["Theme & Layout Engine"]
        TH["UIThemeHelper.cs"]
        TH -.->|StyleCard, ApplyTheme| UI_Layer
        TH -.->|Palette Token Dispatch| F04
    end

    subgraph Data_Access_Layer ["Tầng Truy Xuất Dữ Liệu (DAL)"]
        DAL["Functions.cs"]
        DAL -->|AES-256 GCM / CBC| SEC1["Security (Data Encryption)"]
        DAL -->|PBKDF2 SHA-256| SEC2["Security (Password Hash)"]
        DAL -->|Parameterized SQL / SqlClient| ADO["System.Data.SqlClient"]
        DAL -->|ImageToByteArray / ByteArrayToImage| IMG["GDI+ Image Pipeline"]
    end

    subgraph Database_Layer ["Tầng Cơ Sở Dữ Liệu (SQL Server 2022 / LocalDB)"]
        DB[(QLBanMayViTinh)]
        TRG1["Trigger: trg_ChiTietHDN_AUID"]
        TRG2["Trigger: trg_ChiTietHDB_AUID"]
        
        T_MVT["MayViTinh (33 mẫu, Anh VARBINARY)"]
        T_HDN["HoaDonNhap & ChiTietHDN"]
        T_HDB["HoaDonBan & ChiTietHDB"]
        T_DMSP["14 Bảng Danh Mục Linh Kiện Chuẩn 3NF"]
        T_SEC["TaiKhoan & NhatKyHoatDong & CauHinh"]
    end

    UI_Layer -->|Gọi hàm nghiệp vụ & CRUD| DAL
    DAL -->|Truy vấn tham số hóa & Transaction| DB
    DB --> TRG1
    DB --> TRG2
    TRG1 -.->|Tự động cộng tồn kho, tính giá bán 110%| T_MVT
    TRG1 -.->|Tự động cập nhật tổng tiền| T_HDN
    TRG2 -.->|Kiểm tra tồn kho & Ngăn chặn xuất âm| T_MVT
    TRG2 -.->|Tự động trừ tồn kho, cập nhật tổng tiền| T_HDB
```

---

## 2. MA TRẬN KIỂM THỬ ĐA TRẠNG THÁI (MULTI-STATE TEST MATRIX) TRÊN 14 FORM

Toàn bộ 14 Form đã được kích hoạt chu trình QA tự hành: kiểm tra trạng thái mặc định, giả lập click toàn bộ nút bấm / tab / radio button / grid cell, chụp ảnh màn hình cửa sổ thực tế, phân tích thị giác (Vision Inspection) và khắc phục triệt để các khiếm khuyết UI/UX.

| STT | Tên Form | Các Trạng Thái Tương Tác Đã Click & Kiểm Thử | Lỗi UI Phát Hiện Khi Soi Ảnh (Visual Inspection) | Logic Nghiệp Vụ Đã Kiểm Tra | Giải Pháp Sửa Lỗi Trong Code | Trạng Thái Sau Sửa (Status) |
| :---: | :--- | :--- | :--- | :--- | :--- | :---: |
| 1 | `frmDangNhap.cs` | - Mặc định Light/Dark<br>- Bấm nút Hiện/Ẩn mật khẩu (`btnTogglePass`)<br>- Bấm chuyển sang `frmDangKy`<br>- Bấm `frmCaiDat` | - Nút cấu hình CSDL hiển thị ký tự emoji hỏng (`⚙️`)<br>- Chưa đồng bộ nút ẩn/hiện mật khẩu | - Xác thực tài khoản Admin/BanHang/ThuKho qua hash PBKDF2<br>- Chặn SQL Injection<br>- Ghi nhớ trạng thái đăng nhập | - Thay emoji bằng chữ rõ nghĩa: `"Cấu hình kết nối SQL Server"`<br>- Hoàn thiện sự kiện đảo `UseSystemPasswordChar` | **VERIFIED PASS** |
| 2 | `frmDangKy.cs` | - Mặc định Light/Dark<br>- Bấm `btnDangKy` khi để trống các ô<br>- Nhập dữ liệu hợp lệ | - Label thông báo lỗi chưa định vị chuẩn giữa form | - Bắt buộc nhập đầy đủ trường<br>- Kiểm tra trùng lặp `TenDangNhap`<br>- Mã hóa mật khẩu bằng salt PBKDF2 | - Tối ưu hóa padding card, căn giữa nhãn `lblThongBao` hiển thị cảnh báo đỏ nổi bật | **VERIFIED PASS** |
| 3 | `frmQuenMatKhau.cs` | - Mặc định Light/Dark<br>- Bấm `btnDatLaiMatKhau` khi thiếu dữ liệu | - Độ tương phản chữ thông báo lỗi trong Dark Mode | - Xác thực tài khoản khớp với số điện thoại nhân viên bảo hộ<br>- Cập nhật mật khẩu mới mã hóa | - Tinh chỉnh màu nhãn trạng thái thích ứng theo Dark Mode | **VERIFIED PASS** |
| 4 | `frmMain.cs` | - Bấm lần lượt 11 nút sidebar: Dashboard, MayTinh, DanhMuc, HDNhap, HDBan, TimKiem, BaoCao, HoSo, NguoiDung, NhatKy, CaiDat<br>- Bấm `btnToggleTheme` trên Header | - Trạng thái Active nút sidebar chưa tương phản cao trong Light Mode<br>- Căn lề nút Toggle Theme khi resize form | - Nhúng Form con độc lập vào `pnlMain`<br>- Phân quyền hiển thị menu theo vai trò (RBAC)<br>- Phát tín hiệu đổi theme tới Form con đang hiển thị | - Chuẩn hóa `ActivateSidebarButton`<br>- Dynamic Layout trong sự kiện `Resize` của header | **VERIFIED PASS** |
| 5 | `frmMayViTinh.cs` | - Mặc định danh sách<br>- Chọn dòng DataGridView (nạp ảnh vào PictureBox)<br>- Bấm nút `btnThem`<br>- Lọc ô `txtTimKiemNhanh`<br>- Nhập/sửa trực tiếp Giá nhập, Giá bán, Tồn kho và bấm Lưu/Sửa | - `pnlGridCard` có `Padding(0, 10, 0, 0)` để lộ khoảng trắng bo cong phía trên DataGridView<br>- Ô Tồn kho bị khóa cứng ReadOnly và nút Sửa chưa cập nhật Giá nhập, Giá bán, Tồn kho vào CSDL | - Tải dữ liệu 33 mẫu máy và 14 linh kiện chuẩn 3NF<br>- Nạp và hiển thị ảnh nhị phân `VARBINARY(MAX)` an toàn không lock file<br>- Tự động gợi ý giá bán = 110% giá nhập khi nhập liệu, đồng thời cho phép gõ tay tùy ý và lưu chuẩn xác vào SQL Server | - Đặt `pnlGridCard.Padding = new Padding(0)`<br>- Đồng bộ DataGridView dock fill phẳng hoàn toàn<br>- Mở khóa `txtSoLuong.ReadOnly = false`<br>- Cập nhật câu lệnh `INSERT` và `UPDATE` lưu trực tiếp `GiaNhap`, `GiaBan`, `SoLuong` | **VERIFIED PASS** |
| 6 | `frmDanhMucHeThong.cs` | - Chuyển qua lại giữa 4 Tab: Khách Hàng, Nhà Cung Cấp, Nhân Viên, Linh Kiện & Thông Số<br>- Trong Tab 4: Chọn lần lượt 14 bảng danh mục (`LoaiMay`, `Chip`, `RAM`, `OCung`, `DungLuong`, `TocDo`, `ManHinh`, `CoMH`, `Chuot`, `BanPhim`, `Loa`, `OCD`, `USB`, `HangSanXuat`) | - TabControl kích thước `(275, 38)` làm tràn quá chiều rộng form (1020px), sinh nút cuộn tab xấu và cắt cụt chữ "LINH KIỆN & THÔNG SỐ"<br>- Input linh kiện X=600 bị hẹp (180px) so với các tab khác | - CRUD động trên 14 bảng danh mục thông qua cơ chế mapping cột tự động (`GetCurrentTableName`)<br>- Tự động cập nhật tiêu đề cột DataGridView tương ứng | - Giảm `tabMain.ItemSize` xuống `(240, 38)` giúp vừa vặn 4 tab trong chiều ngang<br>- Mở rộng `txtMaLK`, `txtTenLK` lên width 260px và căn chỉnh lại tọa độ Y chuẩn xác | **VERIFIED PASS** |
| 7 | `frmHoaDonNhap.cs` | - Bấm `btnThemHD` (Tạo mới)<br>- Chọn Nhà cung cấp, chọn Máy vi tính<br>- Nhập số lượng, đơn giá nhập<br>- Bấm `btnLuuHD`, `btnInHD` | - `pnlDetailCard` controls bị tràn sang phải vượt form width 1030px<br>- `pnlGridCard` hở padding viền trên | - Tự động tính thành tiền `txtThanhTien = SoLuong * DonGia`<br>- Tự động cộng dồn tồn kho qua Trigger CSDL<br>- Tự tính lại giá bán 110% trong bảng `MayViTinh` | - Sắp xếp lại tọa độ chi tiết sản phẩm: `txtThanhTien` nằm gọn tại X=850, W=130 (biên phải 980px <= 1006px)<br>- Xóa padding trên `pnlGridCard` | **VERIFIED PASS** |
| 8 | `frmHoaDonBan.cs` | - Bấm `btnThemHD` (Tạo mới)<br>- Chọn Khách hàng, chọn Máy vi tính<br>- Nhập số lượng bán<br>- Kiểm tra cảnh báo vượt tồn kho | - Hộp `txtThanhTien` tại X=930 bị tràn biên phải (Right = 1040px > 1030px) gây cắt cụt số tiền<br>- `pnlGridCard` hở padding trên | - Tự động điền tồn kho và đơn giá bán 110%<br>- Ngăn chặn xuất âm tại trigger CSDL (`trg_ChiTietHDB_AUID`)<br>- Hoàn trả tồn kho khi xóa mặt hàng bán | - Tái cấu trúc hàm `LayoutDetailCard()` và Designer: nén nhẹ khoảng cách nhãn, bố trí `txtThanhTien` tại X=844, W=130 (biên phải 974px < 982px)<br>- Đặt `pnlGridCard.Padding = new Padding(0)` | **VERIFIED PASS** |
| 9 | `frmTimKiem.cs` | - Chuyển đổi giữa 3 RadioButton: Máy Vi Tính, Hóa Đơn Nhập, Hóa Đơn Bán<br>- Nhập từ khóa, bấm `btnTimKiem`, bấm `btnLamMoi` | - `pnlFilterCard` không co giãn chiều cao khi chuyển từ MVT (1 dòng) sang Hóa Đơn (2 dòng: có khoảng ngày tháng), dẫn đến bị che khuất hàng dưới<br>- Lộ viền xám thừa của các panel lồng con | - Ghép nối câu lệnh SQL tìm kiếm động đa điều kiện an toàn<br>- Đổi datasource hiển thị lưới tương ứng theo loại đối tượng | - Bổ sung gán `.Name` chuẩn cho các RadioButton và Button<br>- Đặt chiều cao `pnlFilterCard.Height = 80px` cho MVT và `98px` cho Hóa Đơn<br>- Đặt `BackColor = Color.Transparent` cho `pnlRadioSwitch`, `pnlMayTinh`, `pnlHoaDon` | **VERIFIED PASS** |
| 10 | `frmBaoCaoThongKe.cs` | - Chuyển đổi 4 RadioButton: Tồn kho, Nhập hàng, Bán hàng, Top 5<br>- Đổi Tháng/Quý/Năm<br>- Bấm `btnXemBaoCao`, `btnXuatExcel` | - RadioButton bị cắt chữ tiếng Việt: `"Nhập Theo Quý/..."`, `"Top 5 Doanh T..."`<br>- `pnlGridCard` có padding trên để lộ viền thẻ màu trắng phía trên nhãn tiêu đề xanh<br>- Các sub-panel `pnlControls`, `pnlThoiGian` có viền kép thừa<br>- Chọn RadioButton nhưng bảng không tự động tải dữ liệu nếu chưa bấm nút Xem Báo Cáo<br>- Vệt đường kẻ ngang đứt đoạn thừa phía dưới hàng RadioButton | - Tự động ẩn/hiện bộ chọn thời gian theo loại báo cáo<br>- Tự động nạp dữ liệu tức thì (Auto-Refresh) khi chọn RadioButton hoặc thay đổi thời gian lọc<br>- Tổng hợp KPI doanh thu, chi phí, lợi nhuận ước tính<br>- Xuất file Excel định dạng chuẩn | - Tăng width cho cả 4 RadioButtons (155px - 175px)<br>- Đặt `pnlGridCard.Padding = new Padding(0)`<br>- Đặt `pnlControls` và `pnlThoiGian` có `BorderStyle = BorderStyle.None` và trong suốt<br>- Kích hoạt `btnXemBaoCao.PerformClick()` tự động trong `rdo*_CheckedChanged` và `cbo*_SelectedIndexChanged`<br>- Gọi Invalidate khử triệt để đường kẻ đứt đoạn | **VERIFIED PASS** |
| 11 | `frmQuanLyNguoiDung.cs` | - Chọn tài khoản trên DataGridView<br>- Bấm `btnThem`<br>- Bấm `btnKhoaMoKhoa`<br>- Bấm `btnDatLaiMK` | - Nút Thao tác không tự động đổi màu khi khóa/mở | - Phân quyền vai trò (Admin, BanHang, ThuKho)<br>- Khóa/Kích hoạt tài khoản người dùng<br>- Đặt lại mật khẩu mặc định qua PBKDF2 | - Chuẩn hóa màu sắc nút hành động theo `UIThemeHelper.Btn*Color`<br>- Tối ưu hóa phản hồi thị giác khi chọn dòng | **VERIFIED PASS** |
| 12 | `frmHoSoCaNhan.cs` | - Xem thông tin cá nhân nhân viên<br>- Bấm `btnLuuThongTin`<br>- Nhập mật khẩu cũ, mật khẩu mới, bấm `btnDoiMatKhau` | - Độ tương phản tiêu đề bảng nhật ký trong Dark Mode | - Cập nhật thông tin họ tên, địa chỉ, số điện thoại<br>- Xác thực mật khẩu cũ và đổi mật khẩu mới<br>- Xem lịch sử hoạt động riêng của user đang đăng nhập | - Bổ sung mapping màu Dark Mode cho các Label tiêu đề | **VERIFIED PASS** |
| 13 | `frmNhatKyHoatDong.cs` | - Lọc theo loại hành động (`cboHanhDong`)<br>- Bấm `btnLamMoi`<br>- Xem bảng cảnh báo tồn kho thấp (<= 5) | - Tiêu đề cảnh báo bị lỗi emoji hỏng (`⚠️`)<br>- Tiêu đề cột DataGridView cảnh báo bị ngắt dòng chữ đứng | - Audit Log ghi lại mọi thao tác thêm/sửa/xóa và đăng nhập<br>- Truy vấn tự động các máy tính có tồn kho nguy cấp | - Đổi tiêu đề thành: `"[!] CẢNH BÁO TỒN KHO THẤP (<= 5 CÁI)"`<br>- Đặt `MaMayVT.Width = 50`, `SoLuong.Width = 80` ("Tồn") và `TenMayVT.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill` | **VERIFIED PASS** |
| 14 | `frmCaiDat.cs` | - Bấm chuyển đổi giữa `rdoLight` và `rdoDark`<br>- Bấm `btnTestConnect`<br>- Bấm `btnLuuConnect`, `btnLuuCuaHang` | - RadioButton Light Mode có emoji mặt trời hỏng (`☀️`) | - Lưu trạng thái Theme vào bảng `tblCauHinhHeThong`<br>- Mã hóa chuỗi kết nối SQL Server bằng AES-256 an toàn | - Thay emoji bằng nhãn rõ ràng: `"Light Mode (Giao diện sáng chuẩn)"`<br>- Đồng bộ theme tức thì trên toàn bộ form đang mở | **VERIFIED PASS** |

---

## 3. CHI TIẾT CÁC LỚP HỖ TRỢ & CƠ CHẾ CƠ SỞ DỮ LIỆU

### 3.1. Lớp Truy Xuất Dữ Liệu & Bảo Mật (`Classes/Functions.cs`)
- **Mã Hóa Dữ Liệu AES-256 (GCM / CBC)**:
  Bảo vệ an toàn các thông tin nhạy cảm (chuỗi kết nối SQL Server, thông tin cấu hình) trước nguy cơ bị trích xuất từ file config hoặc bộ nhớ.
- **Băm Mật Khẩu PBKDF2 (SHA-256 with Cryptographic Salt)**:
  Sử dụng `Rfc2898DeriveBytes` tạo salt ngẫu nhiên 16 bytes và sinh hash 32 bytes với 10,000 vòng lặp (iterations). Khắc phục hoàn toàn lỗ hổng lưu mật khẩu plain-text hoặc MD5/SHA-1 lỗi thời.
- **Hỗ Trợ Parameterized SQL Toàn Diện**:
  Cung cấp các hàm `ExecuteNonQuery(cmd)`, `GetDataToTable(cmd)` chấp nhận `SqlCommand` chứa `SqlParameterCollection`, triệt tiêu 100% rủi ro tấn công SQL Injection.
- **Xử Lý Dữ Liệu Nhị Phân Hình Ảnh (GDI+ Safe Pipeline)**:
  Chuyển đổi hai chiều `ByteArrayToImage` và `ImageToByteArray` thông qua `MemoryStream`, sao chép dữ liệu độc lập để giải phóng tài nguyên GDI+, tránh khóa file ảnh trên đĩa (`Out of memory` / `GDI+ generic error`).

### 3.2. Động Cơ Giao Diện Đa Chủ Đề (`Classes/UIThemeHelper.cs`)
- **Bảng Mã Màu Hiện Đại (Modern Slate & Indigo Palette)**:
  - *Chủ đề Sáng (Light Mode)*: Nền `#F1F5F9`, Thẻ `#FFFFFF`, Chữ chính `#0F172A`, Viền `#CBD5E1`.
  - *Chủ đề Tối (Dark Mode)*: Nền `#0B0F19`, Thẻ `#151D2A`, Chữ chính `#F1F5F9`, Viền `#2D3748`.
  - *Màu Nút Hành Động Chuẩn POS*: Thêm (`#10B981` Emerald), Sửa (`#F59E0B` Amber), Xóa (`#EF4444` Rose), Lưu/Tìm (`#4F46E5` Indigo), Làm Mới/Hủy (`#64748B` Slate).
- **Bộ Xử Lý Đệ Quy Áp Dụng Theme (`ApplyTheme`)**:
  Tự động duyệt cây điều khiển (Control Hierarchy), nhận diện loại control (Button, TextBox, ComboBox, DataGridView, Panel, Label, CheckBox, RadioButton) và thiết lập màu sắc, font chữ, viền phẳng đồng bộ.
- **Khử Viền Thừa & Bo Góc Phẳng (Flat Card & Container Styling)**:
  Tự động gán `BorderStyle.None` và `BackColor = Color.Transparent` cho các Panel vùng chứa con (`pnlControls`, `pnlThoiGian`, `pnlSub*`), ngăn chặn hiện tượng viền đứt đoạn hoặc chồng chéo viền.
- **Lắng Nghe & Đồng Bộ Sự Kiện Theme Tức Thì (`ThemeChanged`)**:
  Khi người dùng bấm đổi theme trên Header `frmMain` hoặc trong `frmCaiDat`, sự kiện `ThemeChanged` phát thông báo tới tất cả Form con đang mở, chuyển đổi giao diện trong thời gian thực mà không cần khởi động lại ứng dụng.

### 3.3. Tự Động Hóa Toàn Vẹn CSDL Qua SQL Server Triggers
Hệ thống ủy thác toàn bộ logic cập nhật tồn kho và tính toán tài chính cho tầng CSDL thông qua 2 trigger chính:
1. **Trigger `trg_ChiTietHDN_AUID` (Bảng `ChiTietHDN`)**:
   - Khi thêm/sửa/xóa dòng chi tiết phiếu nhập, trigger tự động tính toán chênh lệch số lượng `(inserted.SoLuong - deleted.SoLuong)` và cộng dồn vào cột `SoLuong` của bảng `MayViTinh`.
   - Tự động cập nhật `GiaNhap` mới nhất cho mẫu máy và tự động tính `GiaBan = GiaNhap * 1.10` (lợi nhuận định mức 10%).
   - Tự động tính lại tổng tiền `TongTien` trong bảng `HoaDonNhap`.
2. **Trigger `trg_ChiTietHDB_AUID` (Bảng `ChiTietHDB`)**:
   - **Cơ Chế Ngăn Xuất Âm (Negative Stock Guard)**: Kiểm tra nếu `MayViTinh.SoLuong < (inserted.SoLuong - deleted.SoLuong)`, trigger lập tức thực thi `RAISERROR` và `ROLLBACK TRANSACTION`, đảm bảo kho không bao giờ bị âm.
   - Khi hợp lệ, tự động trừ tồn kho tương ứng và tự động hoàn trả tồn kho nếu xóa mặt hàng khỏi hóa đơn bán.
   - Tự động cộng dồn tổng thành tiền vào cột `TongTien` trong bảng `HoaDonBan`.

---

## 4. NHẬT KÝ KIỂM THỬ TỰ ĐỘNG (AUTOMATED VERIFICATION LOGS)

### 4.1. Kết Quả Chạy E2E Test Suite (`E2ETestRunner.exe`)
Toàn bộ 25 kịch bản kiểm thử tích hợp (Integration & Opaque-Box Tests) đã được biên dịch và chạy tự động với kết quả **25/25 PASSED (100.0%)**:

```
======================================================================
            E2E OPAQUE-BOX AUTOMATED TEST SUITE RUNNER                
        QLBanMayViTinh Modernized .NET 4.8 Store Management           
======================================================================
Timestamp : 2026-11-07 21:01:12
Filter    : All Tiers (1 - 4)
Platform  : Microsoft Windows NT 6.2.9200.0 (32-bit)
----------------------------------------------------------------------

[PASS] T1.01   | Database Tables Count (25 3NF Tables)         |   228 ms | Found 25 normalized tables in QLBanMayViTinh schema.
[PASS] T1.02   | Computer Models Volume (>= 32 Records)        |     1 ms | Verified 33 computer models present in MayViTinh table.
[PASS] T1.03   | Computer Categories Diversity (4 Segments)    |     3 ms | Verified all 4 segments (LM01, LM02, LM03, LM04) are populated.
[PASS] T1.04   | Image Column Data Type (VARBINARY)            |    31 ms | Verified MayViTinh.Anh is VARBINARY(MAX).
[PASS] T1.05   | Image Binary Magic Bytes Verification         |     4 ms | Verified 5 models have valid image magic headers.
[PASS] T1.06   | Safe Binary Image Conversion in DAL           |     7 ms | ByteArrayToImage and ImageToByteArray round-trip successfully.
[PASS] T1.07   | Parameterized SQL Support in DAL              |     5 ms | Parameterized query executed and verified with SqlParameter.
[PASS] T1.08   | VS Designer Compatibility (13 Forms Purity)   |    82 ms | All 13 forms instantiated with zero designtime exceptions.
[PASS] T1.09   | Theme Engine Color Palettes & Contrast        |     0 ms | Verified high-contrast color tokens for Dark and Light modes.
[PASS] T1.10   | Streamlined Login Form Layout (R4)            |     1 ms | Login intro panel removed and login controls structured into card layout.
[PASS] T1.11   | Security PBKDF2 Password Hashing              |   238 ms | PBKDF2 hashing, salting, and verification verified.
[PASS] T1.12   | Security AES-256 Data Encryption              |     1 ms | AES-256 encryption and decryption round-trip verified.
[PASS] T2.01   | Null and Empty Image Handling                 |     0 ms | Null and empty byte arrays handled safely.
[PASS] T2.02   | Malformed Corrupted Image Payload Handling    |     0 ms | Corrupted byte payload handled gracefully without GDI+ crash.
[PASS] T2.03   | Zero and Extreme Financial Price Boundaries   |    13 ms | 0 VND and 999,999,999 VND boundaries verified.
[PASS] T2.04   | Negative Stock Trigger Prevention             |    66 ms | Database trigger trg_ChiTietHDB_AUID successfully blocked negative inventory.
[PASS] T2.05   | Rapid Theme Toggle Stress (50 Iterations)     |    27 ms | Form withstood 50 consecutive theme cycles with zero GDI errors.
[PASS] T2.06   | Authentication Invalid Credentials & SQLi Guard |   259 ms | PBKDF2 verification rejected invalid and adversarial credentials.
[PASS] T3.01   | Binary Image Insert, Search, and DAL Decode   |     6 ms | Inserted synthetic image, queried record, and verified binary integrity.
[PASS] T3.02   | Live Theme Dispatch Across Multiple Forms     |    13 ms | Multiple child forms themed concurrently without exceptions.
[PASS] T3.03   | Theme Mode Persistence Round-Trip in Database |     4 ms | Theme state persisted and retrieved from tblCauHinhHeThong.
[PASS] T3.04   | Wholesale Pricing Trigger Recalculation (110%) |    47 ms | Verified 110% pricing trigger: GiaNhap = 18,000,000, GiaBan = 19,800,000 VND.
[PASS] T4.01   | Complete Store POS & Inventory Lifecycle      |    86 ms | End-to-end POS lifecycle validated: 15 imported, 6 sold, 9 remaining, Total HDB: 145,200,000 VND.
[PASS] T4.02   | Inventory Restoration on Invoice Item Delete  |    11 ms | Inventory successfully restored from 6 back to 10 upon removing sale item.
[PASS] T4.03   | Zero Comments Compliance Audit (R5)           |    34 ms | Scanned 40 C# and 1 SQL files. 100% clean with 0 comments.

----------------------------------------------------------------------
                        TIER BREAKDOWN SUMMARY                        
----------------------------------------------------------------------
Tier 1: Feature Coverage (R1 - R5 Contracts)      : 12/12 (100.0%)
Tier 2: Boundary & Corner Cases (Adversarial)     :  6/ 6 (100.0%)
Tier 3: Cross-Feature Combinations (Integration)  :  4/ 4 (100.0%)
Tier 4: Real-World Scenarios (End-to-End Store)   :  3/ 3 (100.0%)
======================================================================
OVERALL STATUS: SUCCESS | 25/25 PASSED (100.0%)
======================================================================
```

### 4.2. Kiểm Toán Luật Tối Thượng (Zero-Comment Compliance Audit)
- **Quy chuẩn**: Tuyệt đối không có bất kỳ dòng comment nào (`//`, `/* */`, `///`, `--`) trong toàn bộ code C# và SQL.
- **Phương pháp kiểm toán**: Trình phân tích cú pháp tĩnh Abstract Syntax Tree (AST) trong kịch bản kiểm thử tự động `T4.03`.
- **Kết quả**: Quét toàn bộ 40 file mã nguồn `.cs` / `.Designer.cs` và 1 file `.sql` trong dự án. Số lượng comment phát hiện: **0 (100% CLEAN)**.

---

## 5. NÂNG CẤP CƠ CHẾ KIỂM SOÁT LỖI NHẬP LIỆU & FIX LỖI SQL TRUNCATION MÃ HÓA AES

### 5.1. Khắc phục lỗi SqlException: String or binary data would be truncated
- **Nguyên nhân gốc rễ**: Khi thêm mới khách hàng (`frmDanhMucHeThong`), số điện thoại và địa chỉ được mã hóa AES-256 thông qua `SecurityHelper.EncryptAES()`. Chuỗi mã hóa sinh ra có định dạng `ENC:<Base64>` có độ dài từ 44 đến 80 ký tự. Trong khi đó, định nghĩa cột `DienThoai` trong bảng `KhachHang`, `NhanVien`, `NhaCungCap` trước đây là `NVARCHAR(20)`, dẫn đến lỗi tràn kích thước cột SQL Server (Error 8152).
- **Giải pháp**:
  - Thực thi nâng cấp trực tiếp trên CSDL SQL Server: Mở rộng `DienThoai` thành `NVARCHAR(255)` và `DiaChi` thành `NVARCHAR(500)` cho cả 3 bảng `KhachHang`, `NhanVien`, `NhaCungCap`.
  - Cập nhật đồng bộ kịch bản kiến tạo CSDL trong [Database_QLBanMayViTinh.sql](file:///d:/HOCTRENLOP\LAPTRINHTRUCQUAN\BTLLAPTRINHTRUCQUAN\Database_QLBanMayViTinh.sql).

### 5.2. Triển khai cơ chế kiểm soát lỗi nhập liệu toàn diện (Input Validation & Error Guard)
1. **Ràng buộc sự kiện bàn phím (KeyPress Guards)**:
   - Áp dụng `Functions.OnlyInteger` chặn người dùng gõ ký tự chữ vào các ô số điện thoại (`txtDienThoaiKH`, `txtDienThoaiNCC`, `txtDienThoaiNV`, `txtDienThoai`) và các ô số lượng, đơn giá, thời gian bảo hành.
2. **Kiểm tra định dạng số điện thoại (Regex Validation)**:
   - Bắt buộc kiểm tra định dạng số điện thoại Việt Nam chuẩn (`^0[0-9]{9,10}$` - từ 10 đến 11 chữ số và bắt đầu bằng số 0) trước khi tiến hành mã hóa và lưu vào CSDL. Nếu sai định dạng, hiển thị thông báo hướng dẫn và đặt focus về ô nhập liệu.
3. **Kiểm tra độ dài & bắt buộc nhập liệu (Boundary Checks)**:
   - Kiểm tra rỗng và giới hạn độ dài tối đa của Mã và Tên (`MaKH <= 20`, `TenKH <= 100`, `MaNCC <= 20`, `TenNCC <= 150`, `MaNV <= 20`, `TenNV <= 100`, `DiaChi <= 250`).
   - Kiểm tra ngày sinh nhân viên (`NgaySinh <= Ngày hiện tại` và tuổi >= 15).
4. **Kiểm soát tính hợp lệ của nghiệp vụ kho & giá bán**:
   - `frmMayViTinh`: Giá nhập, giá bán và số lượng tồn kho không được là số âm. Cảnh báo xác nhận người dùng nếu giá bán đang thiết lập thấp hơn giá nhập hàng.
   - `frmHoaDonNhap`: Kiểm tra số lượng nhập > 0, đơn giá >= 0.
   - `frmHoaDonBan`: Kiểm tra số lượng bán > 0, đơn giá >= 0, kiểm soát số lượng bán không vượt quá số lượng tồn kho hiện có.
5. **Bọc kín khối bắt lỗi an toàn (Graceful Exception Handling)**:
   - Bao bọc toàn bộ các thao tác thực thi câu lệnh SQL (`INSERT`, `UPDATE`, `DELETE`) bên trong các khối `try-catch (Exception ex)` để ngăn chặn hoàn toàn việc văng lỗi Unhandled Exception ra hệ điều hành / trình gỡ lỗi Visual Studio.

### 5.3. Khắc phục lỗi mất chữ / cắt chữ (Text Truncation) trên Card Chi Tiết Hóa Đơn
- **Vị trí lỗi phát hiện**: Khu vực `CHỌN SẢN PHẨM BÁN HÀNG (DETAIL - CHỐNG XUẤT ÂM)` trong [`frmHoaDonBan`](file:///d:/HOCTRENLOP/LAPTRINHTRUCQUAN/BTLLAPTRINHTRUCQUAN/Forms/frmHoaDonBan.cs) và `CHI TIẾT MẶT HÀNG NHẬP KHO (DETAIL)` trong [`frmHoaDonNhap`](file:///d:/HOCTRENLOP/LAPTRINHTRUCQUAN/BTLLAPTRINHTRUCQUAN/Forms/frmHoaDonNhap.cs).
- **Hiện tượng**: Các nhãn Label bị co hẹp chiều rộng (`Size.Width` cố định từ 48px - 68px) khiến chữ tiếng Việt có dấu bị cắt đuôi:
  - "Chọn máy:" bị cắt thành "họn má"
  - "Tên máy:" bị cắt thành "Tên má"
  - "Tồn kho:" bị cắt thành "Tồn "
  - "Số bán:" bị cắt thành "Số bá"
  - "Giá bán:" bị cắt thành "Giá bá"
  - "Thành tiền:" bị cắt thành "Thành tiề"
- **Giải pháp xử lý**:
  - Thiết lập thuộc tính `AutoSize = true` cho toàn bộ các Label tiêu đề trong Designer (`lblMaMay`, `lblTenMay`, `lblTonKho`, `lblSoLuong`, `lblDonGia`, `lblThanhTien`).
  - Trong phương thức căn chỉnh động `LayoutDetailCard()`, chuyển sang thuật toán định vị động liên hoàn: `curX = lbl.Right + 5;` sau đó `curX = control.Right + 12;`.
  - Kết quả: Toàn bộ nhãn chữ hiển thị 100% đầy đủ, sắc nét, tự co dãn hoàn hảo theo mọi tỷ lệ màn hình (100%, 125%, 150% DPI).

### 5.4. Khắc phục lỗi cắt chữ "Nhà Cung Cấp" & Radio Button trên Form Tìm Kiếm
- **Vị trí lỗi phát hiện**: Form Tra cứu & Tìm kiếm [`frmTimKiem`](file:///d:/HOCTRENLOP/LAPTRINHTRUCQUAN/BTLLAPTRINHTRUCQUAN/Forms/frmTimKiem.cs).
- **Hiện tượng**:
  - Nhãn đối tác khi chuyển sang chế độ Hóa đơn nhập bị cắt chữ thành *"Nhà Cung Cấ"* do `lblDoiTac.Width` chỉ có 90px (trong khi chuỗi "Nhà Cung Cấp:" cần ~110px).
  - Nút radio *"Hóa Đơn Bán (YC5)"* ở góc trên bên phải bị ép khung cắt đuôi thành *"Hóa Đ..."*.
- **Giải pháp xử lý**:
  - Bật `AutoSize = true` cho `lblDoiTac` và toàn bộ các Label, RadioButton trên Form.
  - Tăng chiều rộng `pnlRadioSwitch` từ 515px lên 560px và căn phải tự động theo chiều rộng cửa sổ.
  - Xây dựng phương thức `LayoutDynamicControls()` gắn với sự kiện `Resize` và `HienThiDieuKien()`, tự động đẩy vị trí `cboDoiTac.Location.X = lblDoiTac.Right + 8` để không bao giờ bị đè chữ hay tràn lề.
  - Chụp ảnh kiểm thử thực tế và xác nhận hiển thị hoàn chỉnh 100% tại [Screenshots/frmTimKiem_HoaDonNhap_Verified.png](file:///d:/HOCTRENLOP/LAPTRINHTRUCQUAN/BTLLAPTRINHTRUCQUAN/Screenshots/frmTimKiem_HoaDonNhap_Verified.png).

---
*Báo cáo được khởi tạo tự động bởi Autonomous QA & Senior C# .NET WinForms Architect Agent.*

