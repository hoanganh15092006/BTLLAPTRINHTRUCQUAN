# Bộ Prompt Cho Antigravity — Nâng Cấp Toàn Diện Dự Án `QLBanMayViTinh` (C# WinForms)

Tài liệu này cung cấp **Master Prompt** được thiết kế chuyên biệt theo cơ chế hoạt động của **Antigravity** (AI Coding Agent có khả năng đọc/ghi file, chạy lệnh terminal/MSBuild và thao tác Git) dựa trên cấu trúc thực tế của kho lưu trữ [BTLLAPTRINHTRUCQUAN](https://github.com/hoanganh15092006/BTLLAPTRINHTRUCQUAN).

---

## Phần 1: Master Prompt (Copy toàn bộ khối dưới đây gửi cho Antigravity)

ROLE & CONTEXT:
Bạn là Senior C# .NET WinForms Architect & Security Engineer đang làm việc trực tiếp trên dự án `QLBanMayViTinh` (.NET Framework 4.7.2, ADO.NET, SQL Server) thuộc repository `https://github.com/hoanganh15092006/BTLLAPTRINHTRUCQUAN`.
Hiện tại dự án có:
- `Classes/`: `Functions.cs`, `UIThemeHelper.cs`
- `Forms/`: `frmMain.cs`, `frmMayViTinh.cs`, `frmHoaDonNhap.cs`, `frmHoaDonBan.cs`, `frmTimKiem.cs`, `frmBaoCaoThongKe.cs`
- `Testing/`: `SelfTestSuite.cs`
- `Database_QLBanMayViTinh.sql` (21 bảng chuẩn 3NF), `App.config`, `Program.cs`, `QLBanMayViTinh.csproj`, `QLBanMayViTinh.sln`.

Hãy thực thi toàn bộ 6 yêu cầu bắt buộc dưới đây theo đúng thứ tự từ Bước 1 đến Bước 6.

====================================================================
LUẬT TỐI THƯỢNG (ABSOLUTE ZERO-COMMENT LAW — VI PHẠM SẼ BỊ TỪ CHỐI TOÀN BỘ):
====================================================================
1. TUYỆT ĐỐI KHÔNG ĐƯỢC VIẾT BẤT KỲ COMMENT NÀO trong toàn bộ dự án (bao gồm cả file tạo mới và file chỉnh sửa).
2. Cấm hoàn toàn `//`, `/* ... */`, `///` (XML documentation) trong mọi file `.cs`.
3. Cấm hoàn toàn `--` và `/* ... */` trong file `.sql`.
4. Cấm hoàn toàn `<!-- ... -->` trong `.csproj`, `App.config`.
5. XÓA SẠCH toàn bộ các comment đang tồn tại sẵn trong các file cũ của dự án (`Functions.cs`, `UIThemeHelper.cs`, `Program.cs`, các Form, `SelfTestSuite.cs`, `Database_QLBanMayViTinh.sql`, `App.config`). Mã nguồn phải sạch comment 100%.

====================================================================
YÊU CẦU 1: BẢO ĐẢM TƯƠNG THÍCH TUYỆT ĐỐI VỚI VISUAL STUDIO WINFORMS DESIGNER
====================================================================
Để mọi Form mở được chế độ **View Designer (Shift+F7)** trên Visual Studio mà không gặp lỗi trắng màn hình hoặc crash Designer:
1. Mỗi Form bắt buộc tách thành 3 file chuẩn: `frmTenForm.cs`, `frmTenForm.Designer.cs`, và `frmTenForm.resx`, đồng thời khai báo đầy đủ thẻ `<Compile Include="...">` kèm `<DependentUpon>frmTenForm.cs</DependentUpon>` và `<SubType>Form</SubType>` trong `QLBanMayViTinh.csproj`.
2. Trong mọi file `.cs` của Form, class `public partial class frm... : Form` PHẢI là class đầu tiên nằm trong file. Không bao giờ đặt class phụ, enum hay struct đứng trước class Form.
3. Phương thức `InitializeComponent()` trong `.Designer.cs` chỉ được chứa câu lệnh khởi tạo control chuẩn của WinForms, gán thuộc tính cơ bản và đăng ký sự kiện (`+=`). Tuyệt đối không gọi `Functions.Connect()`, không truy vấn SQL, không đọc file hay gọi hàm tĩnh ngoài luồng trong `InitializeComponent()` hoặc trong Constructor trước khi kiểm tra `this.DesignMode` hoặc `LicenseManager.UsageMode == LicenseUsageMode.Designtime`.
4. Mọi logic nạp dữ liệu, áp dụng `UIThemeHelper` động, giải mã hay kiểm tra quyền chỉ được gọi trong sự kiện `Form_Load` và bọc sau điều kiện:
   `if (this.DesignMode || System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime) return;`
5. Sử dụng 100% control chuẩn của `System.Windows.Forms` kết hợp custom render thông qua `UIThemeHelper.cs` (không cài thêm thư viện UI bên thứ 3 chưa có trong `packages.config` để tránh lỗi thiếu DLL khi mở Designer).

====================================================================
YÊU CẦU 2: LÀM LẠI GIAO DIỆN ĐẸP, HIỆN ĐẠI THEO PHONG CÁCH WEB SAAS DASHBOARD
====================================================================
Nâng cấp `Classes/UIThemeHelper.cs` và thiết kế lại toàn bộ các Form hiện có (`frmMain`, `frmMayViTinh`, `frmHoaDonNhap`, `frmHoaDonBan`, `frmTimKiem`, `frmBaoCaoThongKe`) cùng các Form mới theo phong cách Modern SaaS Dashboard (lấy cảm hứng từ Linear / Tailwind Slate & Indigo):
1. Bảng màu chuẩn (hỗ trợ cả Light Mode & Dark Mode qua `UIThemeHelper`):
   - Sidebar Navigation: `#0F172A` (Slate 900), nút đang chọn (Active Indicator): `#4F46E5` (Indigo 600), chữ Sidebar: `#F8FAFC`.
   - Nền vùng làm việc chính (Main Content): `#F1F5F9` (Light) / `#1E293B` (Dark).
   - Nền Card / Panel chứa biểu mẫu: `#FFFFFF` (Light) / `#334155` (Dark) có viền mảnh `#E2E8F0`.
   - Màu nút hành động theo ngữ nghĩa: Primary/Thêm/Lưu (`#4F46E5`), Success/Hoàn tất (`#10B981`), Warning/Sửa (`#F59E0B`), Danger/Xóa (`#EF4444`), Neutral/Làm mới (`#64748B`).
2. Typography & Bố cục:
   - Font chữ toàn hệ thống: `Segoe UI` (Tiêu đề trang 16pt Bold, Tiêu đề nhóm 11pt SemiBold, Nội dung và ô nhập 10pt Regular).
   - `frmMain`: Thiết kế bố cục chuẩn Web App gồm:
     + Left Sidebar cố định bên trái (rộng 240px) chứa Logo, danh sách menu điều hướng phân nhóm rõ ràng (Tổng quan, Danh mục, Giao dịch, Tra cứu & Báo cáo, Hệ thống) và nút Đăng xuất ở đáy.
     + Top Header Bar (cao 60px) hiển thị tiêu đề phân hệ đang mở, ô tìm kiếm nhanh, trạng thái kết nối CSDL, badge vai trò + tên tài khoản đang đăng nhập, nút chuyển nhanh Light/Dark theme và nút Thông báo.
     + Vùng Workspace ở giữa (`pnlContent`) nhúng các Form con (`TopLevel = false`, `FormBorderStyle = FormBorderStyle.None`, `Dock = DockStyle.Fill`) kèm 4 thẻ KPI Summary Cards ở màn hình Dashboard trang chủ (Tổng doanh thu, Tổng đơn bán, Số mặt hàng máy tính, Cảnh báo sắp hết hàng).
3. Chuẩn hóa Controls trên tất cả các Form:
   - `DataGridView`: `BorderStyle = None`, `CellBorderStyle = SingleHorizontal`, `EnableHeadersVisualStyles = false`, Header nền `#1E293B` chữ trắng In đậm cao 38px, `RowTemplate.Height = 34`, màu dòng chẵn/lẻ xen kẽ (`AlternatingRowsDefaultCellStyle`), `SelectionMode = FullRowSelect`, `AutoSizeColumnsMode = Fill`.
   - `Button`: `FlatStyle = Flat`, `FlatAppearance.BorderSize = 0`, chiều cao chuẩn 36px, hiệu ứng đổi màu mượt khi Hover.

====================================================================
YÊU CẦU 3: BỔ SUNG ĐẦY ĐỦ CÁC FORM MÀ MỘT HỆ THỐNG WEB HIỆN ĐẠI CẦN CÓ
====================================================================
Tạo mới đầy đủ các Form sau trong thư mục `Forms/` (kèm `.Designer.cs` và `.resx`) và tích hợp vào `frmMain` cũng như luồng khởi chạy trong `Program.cs`:
1. `frmDangNhap` (Đăng nhập):
   - Giao diện chia 2 cột hiện đại (cột trái: Banner thương hiệu hệ thống Quản Lý Bán Máy Vi Tính; cột phải: Ô nhập Tên đăng nhập, Mật khẩu có nút Ẩn/Hiện mật khẩu, Checkbox "Ghi nhớ đăng nhập", liên kết mở `frmQuenMatKhau`, nút Đăng nhập và nút Cấu hình kết nối CSDL).
2. `frmDangKy` & `frmQuenMatKhau` (Tạo tài khoản & Khôi phục mật khẩu):
   - Cho phép tạo tài khoản nhân viên mới (hoặc khôi phục mật khẩu qua mã xác thực/thông tin nhân viên) với kiểm tra độ mạnh mật khẩu và mã hóa mật khẩu trước khi lưu.
3. `frmHoSoCaNhan` (Hồ sơ cá nhân & Đổi mật khẩu - User Profile):
   - Hiển thị và cập nhật thông tin nhân viên đang đăng nhập (`tblNhanVien`), đổi mật khẩu (yêu cầu xác thực mật khẩu hiện tại), xem lịch sử phiên đăng nhập.
4. `frmQuanLyNguoiDung` (Quản trị tài khoản & Phân quyền RBAC - Admin Panel):
   - Quản lý danh sách tài khoản, phân quyền vai trò (`Admin`, `BanHang`, `ThuKho`), khóa/mở khóa tài khoản, cấp lại mật khẩu. Tự động ẩn/khóa các chức năng nhạy cảm trên `frmMain` nếu tài khoản đăng nhập không phải `Admin`.
5. `frmDanhMucHeThong` (Quản lý Danh mục dùng chung):
   - Giao diện TabControl hiện đại cho phép quản lý trực tiếp các bảng danh mục trong CSDL mà trước đây chưa có Form riêng: Khách hàng (`tblKhachHang`), Nhà cung cấp (`tblNhaCungCap`), Nhân viên (`tblNhanVien`), Hãng sản xuất & Linh kiện (`tblHangSanXuat`, `tblLoaiMay`, `tblChip`, `tblRAM`, `tblOChung`).
6. `frmCaiDat` (Settings - Cài đặt hệ thống):
   - Cho phép tùy chỉnh giao diện (Light / Dark Mode, cỡ chữ), cấu hình chuỗi kết nối SQL Server (kiểm tra kết nối trực tiếp và lưu chuỗi kết nối đã mã hóa), cấu hình thông tin cửa hàng in trên hóa đơn, và chức năng Sao lưu / Xuất dữ liệu.
7. `frmNhatKyHoatDong` (Audit Log & Thông báo hệ thống):
   - Hiển thị lịch sử thao tác (Ai đăng nhập, ai thêm/sửa/xóa máy tính hoặc lập hóa đơn lúc nào) và danh sách cảnh báo tồn kho thấp.

====================================================================
YÊU CẦU 4: MÃ HÓA DỮ LIỆU TOÀN DIỆN (DATA ENCRYPTION & SECURITY)
====================================================================
Tạo lớp `Classes/SecurityHelper.cs` và cập nhật `Database_QLBanMayViTinh.sql` + `Classes/Functions.cs` để thực hiện mã hóa thực sự:
1. Mã hóa Mật khẩu (Password Hashing):
   - Sử dụng `PBKDF2` (`Rfc2898DeriveBytes` với `SHA256` và Salt ngẫu nhiên) hoặc `SHA-256` kèm Salt riêng cho từng tài khoản. Tuyệt đối không lưu mật khẩu dạng văn bản thuần (plaintext) trong CSDL.
2. Mã hóa Dữ liệu nhạy cảm & Cấu hình bằng `AES-256` (`System.Security.Cryptography.Aes`):
   - Mã hóa `AES-256` (CBC mode, PKCS7 padding) đối với các thông tin nhạy cảm của Khách hàng / Nhân viên / Nhà cung cấp (như Số điện thoại, Địa chỉ, Email hoặc Chuỗi kết nối lưu trong cấu hình) trước khi ghi xuống CSDL/file cấu hình, và tự động giải mã minh bạch khi hiển thị lên giao diện hoặc tìm kiếm.
   - Cung cấp hàm hỗ trợ tương thích ngược: nếu dữ liệu mẫu cũ trong CSDL chưa được mã hóa AES, hệ thống vẫn đọc được mà không văng ngoại lệ `FormatException` hay `CryptographicException`, đồng thời tự động chuyển sang bản mã hóa khi cập nhật.
3. Cập nhật `Database_QLBanMayViTinh.sql`:
   - Bổ sung bảng `tblTaiKhoan` (chứa `TenDangNhap`, `MatKhauHash`, `Salt`, `MaNV`, `VaiTro`, `TrangThai`, `NgayTao`), bảng `tblNhatKyHoatDong` và bảng `tblCauHinhHeThong` vào script SQL, kèm tài khoản Admin mặc định đã được băm mật khẩu chuẩn (ví dụ: `admin` / `Admin@123`).

====================================================================
YÊU CẦU 5: KIỂM THỬ BIÊN DỊCH & ĐẢM BẢO CHẠY ĐƯỢC NGAY (BUILD & SELF-TEST)
====================================================================
1. Cập nhật đầy đủ `QLBanMayViTinh.csproj` để bao gồm mọi file `.cs`, `.Designer.cs`, `.resx` mới.
2. Cập nhật `Testing/SelfTestSuite.cs` để kiểm thử thêm:
   - Quá trình mã hóa/giải mã AES-256 và băm/xác thực mật khẩu trong `SecurityHelper`.
   - Khả năng khởi tạo tất cả các Form mới và cũ mà không phát sinh lỗi runtime.
3. Chạy lệnh build (`msbuild QLBanMayViTinh.sln /p:Configuration=Debug` hoặc công cụ build tương đương trên máy) để kiểm tra và sửa triệt để 100% lỗi biên dịch (0 Errors).
4. Chạy quét kiểm tra lần cuối bằng regex hoặc script trên toàn bộ thư mục dự án để chắc chắn KHÔNG CÒN BẤT KỲ COMMENT NÀO trong mọi file `.cs`, `.sql`, `.config`, `.csproj`.

====================================================================
YÊU CẦU 6: TỰ ĐỘNG COMMIT & PUSH LÊN GITHUB
====================================================================
Sau khi dự án đã build thành công (0 lỗi) và vượt qua bài kiểm tra 0 comment:
1. Kiểm tra trạng thái Git bằng `git status` và `git remote -v`.
2. Thêm toàn bộ thay đổi: `git add -A`.
3. Tạo commit với thông điệp rõ ràng (ví dụ: `git commit -m "feat: Modernize UI, add auth/settings/admin forms, and implement AES-256 encryption"`).
4. Tự động đẩy thẳng lên nhánh chính của repository: `git push origin main`.

---

## Phần 2: Bảng Tham Khảo Thiết Kế Giao Diện & Kiến Trúc Form Cho Dự Án

Để bạn dễ hình dung hoặc yêu cầu Antigravity tinh chỉnh sâu hơn từng màn hình, dưới đây là bảng ánh xạ từ các trang chuẩn của một **Web App quản lý bán hàng** sang các **Form WinForms** trong dự án `QLBanMayViTinh`:

| Phân hệ trên Web App | Tên Form trong `QLBanMayViTinh` | Các thành phần UI & Tính năng chính |
| :--- | :--- | :--- |
| **Login / Auth Page** | `frmDangNhap.cs`, `frmDangKy.cs`, `frmQuenMatKhau.cs` | Split-screen card, ẩn/hiện mật khẩu, Remember Me (mã hóa token cục bộ), cấu hình nhanh SQL Server khi đổi máy. |
| **App Shell & Dashboard** | `frmMain.cs` | Sidebar cố định bên trái, Header hiển thị User/Role/Theme Toggle, 4 thẻ KPI tổng quan và bảng cảnh báo tồn kho thấp. |
| **Product Catalog** | `frmMayViTinh.cs` | Bộ lọc nhanh trên đầu, lưới sản phẩm kèm ảnh preview, form chi tiết cấu hình 14 bảng linh kiện dạng Card gọn gàng. |
| **Master Data Management** | `frmDanhMucHeThong.cs` | Quản lý tập trung Khách hàng, Nhà cung cấp, Nhân viên, Hãng sản xuất, Chip, RAM, Ổ cứng trên các Tab. |
| **POS / Sales & Purchase** | `frmHoaDonBan.cs`, `frmHoaDonNhap.cs` | Giao diện 2 cột (Trái: Giỏ hàng & chọn linh kiện/máy tính; Phải: Thông tin khách/NCC, tổng tiền, chiết khấu, xuất hóa đơn). |
| **Advanced Search** | `frmTimKiem.cs` | Bộ lọc đa điều kiện (khoảng giá, hãng, cấu hình Chip/RAM/VGA, ngày lập hóa đơn) kèm xuất kết quả. |
| **Analytics & Reports** | `frmBaoCaoThongKe.cs` | Thống kê doanh thu theo tháng/quý, lợi nhuận nhập-bán, top máy bán chạy và hàng tồn kho lâu ngày. |
| **User Profile & RBAC** | `frmHoSoCaNhan.cs`, `frmQuanLyNguoiDung.cs` | Đổi mật khẩu an toàn, cập nhật thông tin cá nhân, quản lý tài khoản và phân quyền `Admin` / `BanHang` / `ThuKho`. |
| **System Settings & Logs** | `frmCaiDat.cs`, `frmNhatKyHoatDong.cs` | Chuyển đổi Light/Dark Mode, cấu hình chuỗi kết nối mã hóa AES-256, xem lịch sử thao tác hệ thống (Audit Logs). |

---

## Phần 3: Prompt Phụ (Dùng Khi Cần Nhắc Lại Luật Xóa Comment Hoặc Sửa Lỗi Designer)

Nếu trong quá trình chạy, Antigravity vô tình sót comment hoặc làm lỗi View Designer của một Form nào đó, bạn có thể gửi ngay prompt ngắn sau:

```text
KIỂM TRA & SỬA LỖI NGHIÊM NGẶT:
1. Quét toàn bộ các file .cs, .Designer.cs, .sql, .config, .csproj trong workspace và XÓA SẠCH 100% mọi dòng comment (//, /* */, ///, --, <!-- -->). Không được để sót dù chỉ 1 ký tự comment.
2. Kiểm tra lại toàn bộ các file trong thư mục Forms/: đảm bảo mỗi Form có đủ 3 file (.cs, .Designer.cs, .resx), class Form là class đầu tiên trong file .cs, và không có bất kỳ lời gọi CSDL hay hàm ngoài nào chạy trong InitializeComponent() hoặc Constructor khi ở chế độ DesignMode.
3. Build lại solution bằng MSBuild để xác nhận 0 Errors, sau đó git add -A, git commit và git push origin main.
```
