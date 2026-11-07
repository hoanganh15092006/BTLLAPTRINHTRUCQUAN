# 💻 HỆ THỐNG QUẢN LÝ CỬA HÀNG BÁN MÁY VI TÍNH (QLBanMayViTinh)

[![Build Status](https://img.shields.io/badge/Build-Passing-brightgreen.svg)]()
[![E2E Tests](https://img.shields.io/badge/Tests-25%2F25%20Passed%20(100%25)-success.svg)]()
[![Code Quality](https://img.shields.io/badge/Zero--Comment%20Rule-100%25%20Clean-blue.svg)]()
[![Framework](https://img.shields.io/badge/.NET%20Framework-4.8-purple.svg)]()
[![Database](https://img.shields.io/badge/SQL%20Server-3NF%20Normalized-red.svg)]()
[![Security](https://img.shields.io/badge/Security-PBKDF2%20%2B%20AES--256-orange.svg)]()

> **Học phần:** Lập Trình Trực Quan (Windows Programming / BTL C# WinForms)  
> **Ngôn ngữ & Nền tảng:** C# (.NET Framework 4.8) — Windows Forms (WinForms Modern UI)  
> **Kiến trúc dữ liệu:** ADO.NET thuần túy (`System.Data.SqlClient`) — Hiệu năng tối đa, không phụ thuộc ORM  
> **Cơ sở dữ liệu:** Microsoft SQL Server (25 bảng chuẩn hóa 3NF, Trigger kiểm soát kho, Transaction)  
> **Bảo mật:** Mã hóa mật khẩu chuẩn PBKDF2 + Salt 128-bit, mã hóa dữ liệu nhạy cảm AES-256-CBC  

---

## 📑 MỤC LỤC
1. [Giới Thiệu Tổng Quan](#-1-giới-thiệu-tổng-quan)
2. [Kiến Trúc & Công Nghệ Lập Trình](#-2-kiến-trúc--công-nghệ-lập-trình)
3. [Chi Tiết 14 Form Nghiệp Vụ Trong Hệ Thống](#-3-chi-tiết-14-form-nghiệp-vụ-trong-hệ-thống)
   - [Form 1: frmDangNhap (Đăng Nhập)](#form-1-frmdangnhap---đăng-nhập-hệ-thống-bảo-mật)
   - [Form 2: frmDangKy (Đăng Ký)](#form-2-frmdangky---đăng-ký-tài-khoản-mới)
   - [Form 3: frmQuenMatKhau (Quên Mật Khẩu)](#form-3-frmquenmatkhau---khôi-phục-mật-khẩu)
   - [Form 4: frmMain (Dashboard Trung Tâm)](#form-4-frmmain---dashboard-trung-tâm--điều-hướng-đa-phân-hệ)
   - [Form 5: frmMayViTinh (Quản Lý Máy Vi Tính)](#form-5-frmmayvitinh---quản-lý-sản-phẩm-máy-vi-tính--14-cấu-hình-3nf)
   - [Form 6: frmDanhMucHeThong (Danh Mục Hệ Thống)](#form-6-frmdanhmuchethong---quản-lý-danh-mục-kh-ncc-nv--14-linh-kiện)
   - [Form 7: frmHoaDonNhap (Hóa Đơn Nhập)](#form-7-frmhoadonnhap---quản-lý-nhập-hàng-master-detail)
   - [Form 8: frmHoaDonBan (Hóa Đơn Bán & POS)](#form-8-frmhoadonban---quản-lý-bán-hàng--thu-ngân-pos-chống-xuất-âm)
   - [Form 9: frmTimKiem (Tra Cứu & Tìm Kiếm)](#form-9-frmtimkiem---tra-cứu--tìm-kiếm-đa-tiêu-chí-thông-minh)
   - [Form 10: frmBaoCaoThongKe (Báo Cáo Thống Kê)](#form-10-frmbaocaothongke---báo-cáo-thống-kê-doanh-thu-tài-chính-chuyên-sâu)
   - [Form 11: frmQuanLyNguoiDung (Phân Quyền Người Dùng)](#form-11-frmquanlynguoidung---quản-lý-tài-khoản--phân-quyền-rbac)
   - [Form 12: frmHoSoCaNhan (Hồ Sơ Cá Nhân)](#form-12-frmhosocanhan---hồ-sơ-người-dùng--nhật-ký-cá-nhân)
   - [Form 13: frmNhatKyHoatDong (Nhật Ký Kiểm Toán)](#form-13-frmnhatkyhoatdong---nhật-ký-hoạt-động-audit-trail)
   - [Form 14: frmCaiDat (Cài Đặt Hệ Thống)](#form-14-frmcaidat---cài-đặt-hệ-thống--chuỗi-kết-nối-mã-hóa)
4. [Thiết Kế Cơ Sở Dữ Liệu 3NF (25 Bảng & Triggers)](#-4-thiết-kế-cơ-sở-dữ-liệu-chuẩn-hóa-3nf-25-bảng)
5. [Cơ Chế Bảo Mật & Kiểm Soát Lỗi Nhập Liệu](#-5-cơ-chế-bảo-mật--kiểm-soát-lỗi-nhập-liệu)
6. [Quy Chuẩn Kiểm Thử Tự Động (E2E Test Suite)](#-6-quy-chuẩn-kiểm-thử-tự-động-e2e-test-suite)
7. [Hướng Dẫn Cài Đặt & Chạy Ứng Dụng](#-7-hướng-dẫn-cài-đặt--chạy-ứng-dụng)

---

## 📌 1. Giới Thiệu Tổng Quan

Hệ thống **Quản Lý Cửa Hàng Bán Máy Vi Tính (QLBanMayViTinh)** là giải pháp phần mềm quản lý toàn diện trên nền tảng Windows Desktop, được thiết kế chuyên biệt cho các chuỗi bán lẻ máy tính, trung tâm phân phối thiết bị tin học và linh kiện công nghệ.

Ứng dụng đáp ứng đầy đủ chu trình nghiệp vụ khép kín:
- **Quản lý danh mục phần cứng đa chiều (Chuẩn 3NF):** Chi tiết từ Vi xử lý (CPU), Bộ nhớ RAM, Ổ đĩa cứng, Dung lượng, Tốc độ Bus, Ổ đĩa quang, Màn hình, Kích cỡ, Chuột, Bàn phím, Cổng USB, Loa tích hợp đến Hãng sản xuất.
- **Vận hành nhập kho & bán hàng tự động:** Tự động tính giá bán = 110% giá nhập, tự động tính tổng tiền bằng chữ tiếng Việt, tự động đồng bộ tồn kho qua Triggers SQL Server.
- **Bảo vệ tài chính & kho vận:** Ngăn chặn tuyệt đối tình trạng bán hàng vượt quá số lượng tồn kho (chống xuất âm) bằng giao dịch an toàn (Transaction & Rollback).
- **Phân tích kinh doanh & báo cáo tài chính:** Theo dõi doanh số bán, chi phí nhập theo Quý/Năm, định giá tài sản tồn kho và vinh danh Top 5 nhân viên kinh doanh xuất sắc nhất.
- **Bảo mật doanh nghiệp cấp độ cao:** Mật khẩu băm PBKDF2 chống tấn công Rainbow Table / Brute-force; thông tin liên lạc nhạy cảm của khách hàng, đối tác và chuỗi kết nối máy chủ đều được mã hóa AES-256.

---

## 🛠️ 2. Kiến Trúc & Công Nghệ Lập Trình

```
┌──────────────────────────────────────────────────────────────────────────┐
│                   PRESENTATION LAYER (14 WINFORMS)                       │
│  frmMain ◄── [frmDangNhap, frmDangKy, frmQuenMatKhau]                    │
│    ├── Core POS: frmMayViTinh, frmDanhMucHeThong, frmHoaDonNhap, HDBan   │
│    ├── Analytics: frmTimKiem, frmBaoCaoThongKe                           │
│    └── Admin: frmQuanLyNguoiDung, frmHoSoCaNhan, frmNhatKy, frmCaiDat    │
├──────────────────────────────────────────────────────────────────────────┤
│                   BUSINESS LOGIC & ENGINE HELPERS                        │
│  UIThemeHelper.cs  │ Theme Engine (Dark Mode / Light Mode, Rounded Cards) │
│  SecurityHelper.cs │ PBKDF2 Password Hashing & AES-256-CBC Encryption     │
│  Functions.cs      │ Data Access, Validation, Key Gen, Vietnamese Currency│
├──────────────────────────────────────────────────────────────────────────┤
│                   DATABASE LAYER (SQL SERVER 2019/2022)                  │
│  25 Tables (3NF)   │ Triggers (Auto Inventory, Price 110%, Stock Guard)  │
│  Stored Procedures │ Transactions (ACID Guaranteed)                       │
└──────────────────────────────────────────────────────────────────────────┘
```

| Thành Phần | Công Nghệ Sử Dụng | Đặc Điểm Kỹ Thuật Nổi Bật |
| :--- | :--- | :--- |
| **Giao Diện (GUI)** | Windows Forms (.NET 4.8) | Modern Flat UI, Palette màu cao cấp, hỗ trợ chuyển đổi Dark Mode / Light Mode mượt mà không chớp nháy (DoubleBuffered Reflection). |
| **Truy Xuất CSDL** | ADO.NET thuần túy | Sử dụng `SqlConnection`, `SqlCommand`, `SqlParameter`, `SqlDataAdapter`, `DataTable` độc lập, an toàn 100% trước SQL Injection. |
| **Mã Hóa Dữ Liệu** | AES-256 (CBC + PKCS7) | Bảo vệ số điện thoại, địa chỉ nhân viên, khách hàng và chuỗi kết nối SQL Server (lưu trữ tiền tố `ENC:` an toàn). |
| **Bảo Mật Xác Thực** | PBKDF2 (Rfc2898DeriveBytes) | 10,000 vòng lặp kết hợp khóa muối Salt ngẫu nhiên 128-bit cho mỗi tài khoản, chống bẻ khóa từ điển. |
| **Cơ Chế Bố Cục Động**| Responsive Card Layout | Tự tính toán vị trí, kích thước control qua mã lệnh, tự động bật `AutoSize = true` cho nhãn chữ, cam kết không bị mất chữ trên mọi màn hình. |
| **Tự Động Hóa QA** | E2E Test Runner tích hợp | Bộ 25 bài kiểm thử tự động hộp đục (Opaque-box) kiểm chứng 100% các tính năng, ràng buộc biên và kịch bản thực tế. |

---

## 🖥️ 3. Chi Tiết 14 Form Nghiệp Vụ Trong Hệ Thống

Dự án sở hữu **14 Form** được thiết kế nhất quán theo chuẩn giao diện phẳng hiện đại, phân quyền truy cập thông minh và kiểm soát lỗi toàn diện:

### Form 1: `frmDangNhap` - Đăng Nhập Hệ Thống Bảo Mật
- **Mục đích:** Xác thực danh tính người dùng trước khi cấp quyền truy cập vào phần mềm.
- **Tính năng nổi bật:**
  - Kiểm tra tài khoản bằng thuật toán băm PBKDF2 và khóa muối Salt từ bảng `tblTaiKhoan`.
  - Kiểm tra trạng thái tài khoản (Chặn ngay nếu tài khoản đang ở trạng thái *"Bị khóa"*).
  - Tùy chọn *"Ghi nhớ tài khoản"* thông qua cấu hình hệ thống.
  - Nút xem/ẩn mật khẩu bằng icon trực quan.
  - Điều hướng nhanh sang Form Đăng ký tài khoản và Form Khôi phục mật khẩu.
  - Ghi nhật ký tự động vào bảng `tblNhatKyHoatDong` khi đăng nhập thành công hoặc thất bại.

### Form 2: `frmDangKy` - Đăng Ký Tài Khoản Mới
- **Mục đích:** Cho phép nhân viên tạo tài khoản mới để tham gia vận hành hệ thống.
- **Tính năng nổi bật:**
  - Kiểm tra độ dài mật khẩu (tối thiểu 6 ký tự) và so khớp mật khẩu xác nhận.
  - Tự động sinh muối ngẫu nhiên (Salt 16 bytes) và băm mật khẩu chuẩn PBKDF2.
  - Kiểm tra trùng lặp tên đăng nhập trong CSDL trước khi tạo.
  - Gắn liên kết tài khoản với danh sách Mã nhân viên chưa có tài khoản trong bảng `NhanVien`.
  - Phân quyền mặc định an toàn theo vai trò quy định.

### Form 3: `frmQuenMatKhau` - Khôi Phục Mật Khẩu
- **Mục đích:** Giúp người dùng lấy lại quyền truy cập khi quên mật khẩu đăng nhập.
- **Tính năng nổi bật:**
  - Quy trình xác minh bảo mật nhiều lớp: Xác thực Tên đăng nhập và Mã nhân viên tương ứng.
  - Bắt buộc kiểm tra mật khẩu mới phải khớp với xác nhận mật khẩu.
  - Tự động tạo lại khóa muối Salt mới và băm mật khẩu mới ghi đè vào bảng `tblTaiKhoan`.
  - Ghi vết hành động khôi phục vào nhật ký kiểm toán hệ thống.

### Form 4: `frmMain` - Dashboard Trung Tâm & Điều Hướng Đa Phân Hệ
- **Mục đích:** Khung làm việc chính của ứng dụng chứa Sidebar điều hướng, thanh Header trạng thái và khu vực hiển thị các Form con.
- **Tính năng nổi bật:**
  - Sidebar điều hướng dạng cây menu thông minh: Trang Chủ, Sản Phẩm, Danh Mục, Hóa Đơn Nhập, Hóa Đơn Bán, Tra Cứu, Báo Cáo Thống Kê, Phân Quyền, Hồ Sơ, Nhật Ký, Cài Đặt.
  - Nhúng mượt mà 10 Form nghiệp vụ con vào trong `pnlMain` mà không mở cửa sổ riêng rẽ, loại bỏ tình trạng nhấp nháy màn hình.
  - Hiển thị thông tin người dùng đang làm việc (Tên đăng nhập, Họ tên, Vai trò: Admin / Quản lý / Bán hàng / Kho).
  - Thanh trạng thái hiển thị thời gian thực và trạng thái kết nối SQL Server.
  - Phân quyền động: Tự động ẩn các nút chức năng quản trị (Phân quyền, Nhật ký, Cài đặt) đối với nhân viên bán hàng thường.

### Form 5: `frmMayViTinh` - Quản Lý Sản Phẩm Máy Vi Tính & 14 Cấu Hình 3NF
- **Mục đích:** Quản lý toàn bộ danh mục sản phẩm máy vi tính kèm 14 thông số cấu hình kỹ thuật chuẩn 3NF.
- **Tính năng nổi bật:**
  - Nạp động 14 ComboBox linh kiện từ các bảng danh mục: Loại máy, Chip, RAM, Ổ cứng, Dung lượng, Tốc độ Bus, Ổ đĩa quang, Màn hình, Cỡ màn hình, Chuột, Bàn phím, Cổng USB, Loa, Hãng sản xuất.
  - Xem trước và lưu trữ ảnh sản phẩm dạng nhị phân `VARBINARY(MAX)` trực tiếp trong CSDL (sử dụng MemoryStream chống khóa file ảnh trên ổ cứng).
  - **Cho phép sửa trực tiếp Giá Nhập, Giá Bán, Số Lượng Tồn Kho** ngay trên form và lưu vĩnh viễn vào CSDL.
  - Tự động gợi ý Giá Bán = 110% Giá Nhập khi người dùng thay đổi giá nhập hàng.
  - Cảnh báo an toàn nếu người dùng thiết lập giá bán thấp hơn giá nhập hoặc nhập giá trị âm.
  - Gắn bộ lọc phím `Functions.OnlyInteger` chặn nhập ký tự chữ vào các ô giá và số lượng.

### Form 6: `frmDanhMucHeThong` - Quản Lý Danh Mục KH, NCC, NV & 14 Linh Kiện
- **Mục đích:** Quản lý tập trung toàn bộ các đối tượng danh mục nền tảng của doanh nghiệp.
- **Gồm 4 Tab chức năng chuyên biệt:**
  1. **Tab Khách Hàng:** Quản lý Mã, Tên, Địa chỉ, Số điện thoại.
  2. **Tab Nhà Cung Cấp:** Quản lý Mã, Tên NCC, Địa chỉ, Số điện thoại.
  3. **Tab Nhân Viên:** Quản lý Mã NV, Họ tên, Ngày sinh, Giới tính, Địa chỉ, Số điện thoại.
  4. **Tab Thông Số Linh Kiện:** Quản lý linh hoạt dữ liệu của 14 bảng cấu hình phần cứng (Loại máy, Hãng SX, Chip, RAM, Ổ cứng...).
- **Cơ chế an toàn & bảo mật:**
  - Số điện thoại và địa chỉ được tự động mã hóa AES-256 trước khi lưu vào CSDL.
  - Kiểm tra hợp lệ số điện thoại bằng Regex chuẩn Việt Nam (`^0[0-9]{9,10}$`).
  - Kiểm tra ràng buộc độ tuổi nhân viên (từ 15 tuổi trở lên, ngày sinh không vượt quá ngày hiện tại).
  - Toàn bộ thao tác thêm, sửa, xóa đều được bọc trong khối `try-catch` ngăn chặn triệt để lỗi ngoại lệ CSDL.

### Form 7: `frmHoaDonNhap` - Quản Lý Nhập Hàng (Master-Detail)
- **Mục đích:** Lập phiếu và quản lý hóa đơn nhập máy vi tính từ nhà cung cấp vào kho.
- **Tính năng nổi bật:**
  - Mô hình **Master-Detail**: Khối thông tin phiếu (Mã HDN, Ngày nhập, Nhân viên, Nhà cung cấp) và khối chi tiết mặt hàng nhập (Chọn máy, Tên máy, Số lượng, Đơn giá nhập, Thành tiền).
  - Tự động đồng bộ hóa kho và giá: Khi lưu chi tiết hóa đơn, Trigger SQL Server tự động **tăng số lượng tồn kho** và **cập nhật đơn giá nhập mới nhất** cho sản phẩm tương ứng trong bảng `MayViTinh`.
  - Tự động cộng dồn Tổng tiền hóa đơn và chuyển đổi số tiền thành chữ tiếng Việt chuẩn kế toán.
  - Hỗ trợ xóa dòng mặt hàng với tính năng hoàn trả tồn kho tự động.
  - Bố cục responsive co dãn động (`LayoutDetailCard`), nhãn chữ hiển thị 100% đầy đủ không bị che khuất.

### Form 8: `frmHoaDonBan` - Quản Lý Bán Hàng & Thu Ngân (POS Chống Xuất Âm)
- **Mục đích:** Giao diện bán hàng lẻ trực tiếp cho khách hàng, lập hóa đơn và quản lý thu ngân.
- **Tính năng nổi bật:**
  - Khi chọn sản phẩm, tự động nạp Đơn giá bán chuẩn (đã được tính 110%) và hiển thị Số lượng tồn kho hiện tại theo thời gian thực.
  - **Cơ chế chống xuất âm kép (Client + Server):**
    - *Phía Client:* Kiểm tra ngay trên giao diện nếu số lượng bán lớn hơn tồn kho sẽ hiển thị cảnh báo cảnh giác và chặn lưu.
    - *Phía Database:* Trigger `trg_ChiTietHDB_AUID` thực thi giao dịch ACID, nếu phát hiện tồn kho bị âm sẽ tự động `RAISERROR` và `ROLLBACK TRANSACTION`.
  - Khi lưu đơn hàng thành công, tồn kho sản phẩm tự động giảm tương ứng.
  - Xóa sản phẩm khỏi hóa đơn: Tồn kho tự động được hoàn trả nguyên vẹn về kho hàng.
  - Hỗ trợ xuất và in hóa đơn bán hàng cho khách.
  - Bố cục co dãn động `LayoutDetailCard` với `AutoSize = true` cho tất cả nhãn chữ.

### Form 9: `frmTimKiem` - Tra Cứu & Tìm Kiếm Đa Tiêu Chí Thông Minh
- **Mục đích:** Cho phép nhân viên và ban quản lý tra cứu tức thì thông tin máy tính hoặc hóa đơn giao dịch.
- **Tính năng nổi bật:**
  - **Tab 1 - Tìm Kiếm Máy Vi Tính (Yêu cầu 4):** Lọc linh hoạt theo Mã máy, Tên máy, Loại máy, Hãng sản xuất.
  - **Tab 2 - Tìm Kiếm Hóa Đơn Nhập & Bán (Yêu cầu 5):** Lọc theo Mã hóa đơn, Mặt hàng, Nhân viên lập, Nhà cung cấp / Khách hàng, kết hợp lọc theo khoảng thời gian từ ngày - đến ngày.
  - Sử dụng thuật toán xây dựng câu lệnh Dynamic SQL `WHERE 1=1` thông minh, tự động ghép nối các tiêu chí tìm kiếm người dùng chỉ định.
  - Bố cục thích ứng `LayoutDynamicControls`: Tự động căn chỉnh nhãn *"Nhà Cung Cấp:"* và *"Khách Hàng:"* không bao giờ bị cắt chữ hay đè lên ô ComboBox.
  - Các nút Radio Switch góc trên hiển thị trọn vẹn 100% nội dung.

### Form 10: `frmBaoCaoThongKe` - Báo Cáo Thống Kê Doanh Thu Tài Chính Chuyên Sâu
- **Mục đích:** Cung cấp bức tranh toàn cảnh về tình hình tài chính, hiệu quả kinh doanh và tồn kho của cửa hàng.
- **Đáp ứng trọn vẹn 4 yêu cầu báo cáo cốt lõi:**
  - **Yêu Cầu 8:** Thống kê sản phẩm tồn kho phân nhóm theo từng loại máy vi tính, tính tổng giá trị hàng tồn định giá theo giá nhập kho.
  - **Yêu Cầu 7:** Báo cáo chi tiết và tổng hợp doanh thu nhập hàng theo Tháng / Quý (Quý 1 - 4) / Năm.
  - **Yêu Cầu 6:** Báo cáo chi tiết và tổng hợp doanh thu bán hàng theo Tháng / Quý / Năm.
  - **Yêu Cầu 9:** Báo cáo xếp hạng **Top 5 Nhân viên có doanh thu bán hàng cao nhất** kèm tổng doanh số đóng góp.
- **Tính năng cao cấp:**
  - 4 Thẻ KPI Header thời gian thực: *Tổng Giá Trị Tồn Kho*, *Tổng Chi Phí Nhập*, *Tổng Doanh Thu Bán*, *Ước Tính Lợi Nhuận*.
  - **Cơ chế Auto-Refresh:** Tự động tải lại báo cáo ngay khi người dùng bấm chuyển đổi RadioButton hoặc chọn lại Tháng, Quý, Năm mà không cần bấm nút tìm kiếm thủ công.
  - Tính năng **Xuất Dữ Liệu Ra File Excel (`ExportToExcel`)** chuyên nghiệp, tự động căn lề và định dạng tiền tệ.

### Form 11: `frmQuanLyNguoiDung` - Quản Lý Tài Khoản & Phân Quyền (RBAC)
- **Mục đích:** Cung cấp công cụ cho Quản trị viên (Admin) quản lý người dùng và phân quyền truy cập hệ thống.
- **Tính năng nổi bật:**
  - Quản lý danh sách tài khoản hệ thống từ bảng `tblTaiKhoan`.
  - Phân quyền theo 4 vai trò cụ thể:
    - `Admin`: Toàn quyền trên mọi phân hệ và chức năng hệ thống.
    - `QuanLy`: Quản lý kho, hàng hóa, xem báo cáo thống kê, không chỉnh sửa tài khoản admin.
    - `BanHang`: Sử dụng POS bán hàng, tra cứu sản phẩm, khách hàng, hồ sơ cá nhân.
    - `Kho`: Quản lý linh kiện, máy vi tính và lập phiếu nhập kho.
  - Tính năng **Khóa / Mở Khóa tài khoản** tức thì (Chặn tài khoản bị vô hiệu hóa đăng nhập vào hệ thống).
  - Tự động ngăn chặn Admin tự khóa tài khoản của chính mình.
  - Tạo mới tài khoản với mật khẩu khởi tạo mã hóa PBKDF2 an toàn.

### Form 12: `frmHoSoCaNhan` - Hồ Sơ Người Dùng & Nhật Ký Cá Nhân
- **Mục đích:** Nơi nhân viên quản lý thông tin tài khoản của mình và theo dõi các hoạt động đã thực hiện.
- **Tính năng nổi bật:**
  - Xem và chỉnh sửa thông tin cá nhân: Họ tên, Giới tính, Địa chỉ, Số điện thoại (dữ liệu được tự động giải mã AES-256 hiển thị và mã hóa lại khi lưu).
  - Đổi mật khẩu cá nhân: Kiểm tra mật khẩu cũ bằng hàm xác thực `VerifyPassword`, băm mật khẩu mới bằng `HashPassword`.
  - Hiển thị lưới lịch sử hoạt động gần nhất của chính người dùng đăng nhập từ bảng `tblNhatKyHoatDong`.

### Form 13: `frmNhatKyHoatDong` - Nhật Ký Hoạt Động (Audit Trail)
- **Mục đích:** Ghi nhận và giám sát toàn bộ các hành vi của người dùng trong hệ thống phục vụ công tác kiểm toán, bảo mật.
- **Tính năng nổi bật:**
  - Lưu vết tự động: Tên đăng nhập, Loại hành động (Đăng nhập, Thêm máy, Sửa giá, Lập hóa đơn, Đổi mật khẩu...), Chi tiết thao tác và Thời gian thực thi chính xác đến từng giây.
  - Bộ lọc tìm kiếm thông minh: Tìm theo Người thực hiện, khoảng thời gian từ ngày - đến ngày.
  - Tính năng phân trang hoặc làm mới danh mục theo thời gian thực.
  - Dữ liệu được bảo vệ chỉ đọc (Read-Only), không cho phép nhân viên sửa đổi nhật ký.

### Form 14: `frmCaiDat` - Cài Đặt Hệ Thống & Chuỗi Kết Nối Mã Hóa
- **Mục đích:** Cấu hình các tham số môi trường và giao diện ứng dụng.
- **Tính năng nổi bật:**
  - **Cấu hình chuỗi kết nối SQL Server:** Cho phép đổi máy chủ CSDL, kiểm tra kết nối thử (`Test Connection`) và lưu chuỗi kết nối mã hóa AES-256 an toàn vào bảng `tblCauHinhHeThong`.
  - **Cài đặt thông tin cửa hàng:** Tên cửa hàng, Địa chỉ, Hotline, Lời chào chân trang hóa đơn.
  - **Tùy chọn giao diện (Theme Switcher):** Chuyển đổi giữa chế độ **Light Mode (Sáng)** và **Dark Mode (Tối)**, tự động áp dụng giao diện mới trên toàn bộ hệ thống và lưu vĩnh viễn cấu hình vào CSDL.

---

## 🗄️ 4. Thiết Kế Cơ Sở Dữ Liệu Chuẩn Hóa 3NF (25 Bảng)

Cơ sở dữ liệu `QLBanMayViTinh` được thiết kế tối ưu hóa theo mô hình thực thể - mối quan hệ (ERD), đạt tiêu chuẩn **chuẩn hóa bậc 3 (3NF)**:

```
                            ┌────────────────┐
                            │    LoaiMay     │
                            ├────────────────┤
                            │  HangSanXuat   │
                            ├────────────────┤
                            │ Chip, RAM, USB │
                            │ OCung, DungLg  │
                            │ TocDo, OCD...  │
                            └───────┬────────┘
                                    │ (14 bảng danh mục cấu hình)
                                    ▼
┌──────────────┐             ┌──────────────┐             ┌──────────────┐
│  NhaCungCap  │────────────>│  MayViTinh   │<────────────│  KhachHang   │
└──────┬───────┘             └──────┬───────┘             └──────┬───────┘
       │                            │                            │
       ▼                            │                            ▼
┌──────────────┐             ┌──────┴───────┐             ┌──────────────┐
│  HoaDonNhap  │             │   NhanVien   │             │  HoaDonBan   │
└──────┬───────┘             └──────┬───────┘             └──────┬───────┘
       │                            │ (Liên kết tài khoản)       │
       ▼                            ▼                            ▼
┌──────────────┐             ┌──────────────┐             ┌──────────────┐
│  ChiTietHDN  │──[Trigger]─>│ tblTaiKhoan  │             │  ChiTietHDB  │──[Trigger Chặn âm]
└──────────────┘             └──────┬───────┘             └──────────────┘
                                    │
                                    ▼
                             ┌──────────────┐
                             │tblNhatKyHD   │
                             └──────────────┘
```

### Danh Sách 25 Bảng Dữ Liệu Chuẩn Hóa:
1. **14 Bảng Danh Mục Linh Kiện Cấu Hình Máy Vi Tính:**  
   `LoaiMay`, `HangSanXuat`, `Chip`, `RAM`, `OCung`, `DungLuong`, `TocDo`, `ManHinh`, `CoMH`, `Chuot`, `BanPhim`, `USB`, `Loa`, `OCD`.
2. **3 Bảng Danh Mục Nghiệp Vụ Cơ Bản:**  
   `KhachHang`, `NhaCungCap`, `NhanVien` (Cột `DienThoai NVARCHAR(255)` và `DiaChi NVARCHAR(500)` hỗ trợ chuỗi mã hóa AES).
3. **5 Bảng Nghiệp Vụ & Giao Dịch Kho - Bán Hàng:**  
   `MayViTinh`, `HoaDonNhap`, `ChiTietHDN`, `HoaDonBan`, `ChiTietHDB`.
4. **3 Bảng Quản Trị Hệ Thống & Bảo Mật:**  
   `tblTaiKhoan` (Xác thực người dùng), `tblNhatKyHoatDong` (Audit log), `tblCauHinhHeThong` (Lưu chuỗi kết nối mã hóa & Theme).

### Các Trigger SQL Server Tự Động Hóa Nghiệp Vụ:
- **`trg_ChiTietHDN_AUID`**: Tự động tăng tồn kho sản phẩm, cập nhật đơn giá nhập mới nhất vào bảng `MayViTinh` và tự động tính `GiaBan = GiaNhap * 1.10`.
- **`trg_ChiTietHDB_AUID`**: Tự động kiểm tra số lượng bán với tồn kho; chặn đứng giao dịch xuất âm bằng `RAISERROR` và `ROLLBACK TRANSACTION`; tự động trừ kho khi bán và hoàn trả tồn kho khi xóa mặt hàng.

---

## 🔒 5. Cơ Chế Bảo Mật & Kiểm Soát Lỗi Nhập Liệu

### 1. Mã Hóa Mật Khẩu (PBKDF2)
- Sử dụng thuật toán `Rfc2898DeriveBytes` với số vòng lặp băm `10,000 Iterations`.
- Mỗi tài khoản được cấp phát một muối ngẫu nhiên (Salt) độ dài 16 bytes bằng bộ sinh số ngẫu nhiên an toàn mã hóa `RNGCryptoServiceProvider`.

### 2. Mã Hóa Dữ Liệu Nhạy Cảm (AES-256)
- Số điện thoại, địa chỉ của nhân viên, đối tác và chuỗi kết nối máy chủ SQL Server được mã hóa bằng chuẩn `AES-256` chế độ `CBC`, đệm `PKCS7`.
- Chuỗi lưu trữ có tiền tố `ENC:`, tự động giải mã minh bạch khi hiển thị lên giao diện.

### 3. Kiểm Soát Bàn Phím & Định Dạng (Input Guards)
- **Lọc bàn phím (`KeyPress`):** Chặn người dùng gõ chữ vào tất cả các ô nhập số điện thoại, giá tiền, số lượng, thời gian bảo hành bằng hàm `Functions.OnlyInteger`.
- **Biểu thức chính quy (Regex):** Số điện thoại bắt buộc phải có từ 10 đến 11 chữ số và bắt đầu bằng số 0 (`^0[0-9]{9,10}$`).
- **Kiểm soát tính hợp lệ của giá và tồn kho:** Chặn số âm; cảnh báo xác nhận nếu giá bán đang thiết lập thấp hơn giá nhập hàng.

### 4. Bọc Kín Khối Bắt Lỗi An Toàn (Graceful Exception Handling)
- 100% các thao tác thực thi câu lệnh SQL trên toàn bộ 14 Form đều được bọc trong khối `try { ... } catch (Exception ex) { ... }`, ngăn chặn hoàn toàn hiện tượng văng lỗi Unhandled Exception ra hệ điều hành.

---

## 🧪 6. Quy Chuẩn Kiểm Thử Tự Động (E2E Test Suite)

Dự án tích hợp bộ công cụ tự động kiểm thử `E2ETestRunner.exe` chạy độc lập qua kịch bản PowerShell `run_e2e_tests.ps1`, kiểm tra liên hoàn **25 kịch bản thử nghiệm** đạt tỷ lệ thành công tuyệt đối:

```text
======================================================================
            E2E OPAQUE-BOX AUTOMATED TEST SUITE RUNNER                
        QLBanMayViTinh Modernized .NET 4.8 Store Management           
======================================================================
[PASS] T1.01   | Database Tables Count (25 3NF Tables)         | Found 25 normalized tables in schema.
[PASS] T1.02   | Computer Models Volume (>= 32 Records)        | Verified 33 computer models present.
[PASS] T1.03   | Computer Categories Diversity (4 Segments)    | Verified all 4 segments (LM01-LM04).
[PASS] T1.04   | Image Column Data Type (VARBINARY)            | Verified MayViTinh.Anh is VARBINARY(MAX).
[PASS] T1.05   | Image Binary Magic Bytes Verification         | Verified valid image magic headers.
[PASS] T1.06   | Safe Binary Image Conversion in DAL           | ByteArrayToImage round-trip verified.
[PASS] T1.07   | Parameterized SQL Support in DAL              | Parameterized query verified.
[PASS] T1.08   | VS Designer Compatibility (13 Forms Purity)   | All forms instantiated without error.
[PASS] T1.09   | Theme Engine Color Palettes & Contrast        | Verified tokens for Dark/Light modes.
[PASS] T1.10   | Streamlined Login Form Layout (R4)            | Login controls structured cleanly.
[PASS] T1.11   | Security PBKDF2 Password Hashing              | PBKDF2 hashing & verification verified.
[PASS] T1.12   | Security AES-256 Data Encryption              | AES-256 encrypt & decrypt round-trip verified.
[PASS] T2.01   | Null and Empty Image Handling                 | Null/empty byte arrays handled safely.
[PASS] T2.02   | Malformed Corrupted Image Payload Handling    | Corrupted byte payload handled gracefully.
[PASS] T2.03   | Zero and Extreme Financial Price Boundaries   | 0 VND and 999,999,999 VND boundaries verified.
[PASS] T2.04   | Negative Stock Trigger Prevention             | Trigger blocked negative inventory.
[PASS] T2.05   | Rapid Theme Toggle Stress (50 Iterations)     | 50 consecutive theme cycles with 0 GDI errors.
[PASS] T2.06   | Authentication Invalid Credentials & SQLi Guard| Rejected invalid and adversarial inputs.
[PASS] T3.01   | Binary Image Insert, Search, and DAL Decode   | Inserted synthetic image and verified integrity.
[PASS] T3.02   | Live Theme Dispatch Across Multiple Forms     | Multiple child forms themed concurrently.
[PASS] T3.03   | Theme Mode Persistence Round-Trip in Database | Theme state persisted and retrieved cleanly.
[PASS] T3.04   | Wholesale Pricing Trigger Recalculation (110%)| Verified 110% pricing trigger.
[PASS] T4.01   | Complete Store POS & Inventory Lifecycle      | 15 imported, 6 sold, 9 remaining verified.
[PASS] T4.02   | Inventory Restoration on Invoice Item Delete  | Inventory restored upon removing sale item.
[PASS] T4.03   | Zero Comments Compliance Audit (R5)           | Scanned 40 C# and 1 SQL files. 100% clean.
======================================================================
OVERALL STATUS: SUCCESS | 25/25 PASSED (100.0%)
======================================================================
```

### Luật Tối Thượng (Zero-Comment Rule)
Toàn bộ mã nguồn C# và các file Designer trong dự án tuân thủ nghiêm ngặt **Quy chuẩn Zero-Comment**: Tuyệt đối không chứa bất kỳ dòng comment thừa thãi nào (`//`, `/* */`, `///`), giữ cho mã nguồn thuần khiết, chuyên nghiệp và tự tài liệu hóa (Self-documenting code).

---

## 🚀 7. Hướng Dẫn Cài Đặt & Chạy Ứng Dụng

### Yêu Cầu Cấu Hình:
- **Hệ điều hành:** Windows 10 hoặc Windows 11 (64-bit khuyến nghị).
- **Môi trường phát triển:** Microsoft Visual Studio 2019 / 2022 (.NET desktop development).
- **Hệ quản trị CSDL:** Microsoft SQL Server (LocalDB, SQL Express, hoặc SQL Server Enterprise/Standard).

### Các Bước Thực Hiện:

#### Bước 1: Khởi Tạo Cơ Sở Dữ Liệu
1. Mở **SQL Server Management Studio (SSMS)** hoặc **Azure Data Studio**.
2. Kết nối vào máy chủ SQL Server của bạn (ví dụ: `.\SQLEXPRESS01`, `.\SQLEXPRESS`, hoặc `.`).
3. Mở file [Database_QLBanMayViTinh.sql](Database_QLBanMayViTinh.sql).
4. Nhấn **F5** (hoặc nút **Execute**) để khởi tạo Database `QLBanMayViTinh`, 25 bảng chuẩn hóa 3NF, Triggers tự động và bộ dữ liệu mẫu ban đầu.

#### Bước 2: Mở Dự Án Bằng Visual Studio
1. Nhấp đúp vào file [QLBanMayViTinh.sln](QLBanMayViTinh.sln) để mở toàn bộ Solution.
2. Kiểm tra chuỗi kết nối trong file [App.config](App.config) (Ứng dụng đã có hàm tự động dò tìm máy chủ SQL nội bộ).

#### Bước 3: Biên Dịch & Chạy Chương Trình
1. Nhấn **Ctrl + Shift + B** để Build Solution.
2. Nhấn **F5** để khởi chạy ứng dụng.
3. Tài khoản đăng nhập mặc định:
   - **Tên đăng nhập:** `admin`
   - **Mật khẩu:** `admin123`

#### Bước 4: Chạy Bộ Kiểm Thử Tự Động (E2E Test)
Mở PowerShell tại thư mục dự án và thực thi lệnh:
```powershell
.\run_e2e_tests.ps1
```
Hệ thống sẽ tự động biên dịch và kiểm tra toàn diện 25 bài test, xuất kết quả chi tiết ra màn hình console và ghi vào file `TEST_REPORT.txt`.

---
*Tài liệu kỹ thuật được xây dựng đồng bộ cho Đồ Án Bài Tập Lớn Học Phần Lập Trình Trực Quan (QLBanMayViTinh).*
