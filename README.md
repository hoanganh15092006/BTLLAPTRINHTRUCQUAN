# 💻 HỆ THỐNG QUẢN LÝ CỬA HÀNG BÁN MÁY VI TÍNH

> **Đồ Án Bài Tập Lớn Học Phần:** Lập Trình Trực Quan (Windows Programming)  
> **Ngôn ngữ & Nền tảng:** C# (.NET Framework 4.8) — Windows Forms (WinForms)  
> **Công nghệ truy xuất dữ liệu:** ADO.NET thuần (`System.Data.SqlClient`)  
> **Cơ sở dữ liệu:** Microsoft SQL Server (Thiết kế chuẩn hóa 3NF, Trigger, Transaction)  
> **Kiến trúc phần mềm:** N-Tier Layered Architecture (Forms, Business Logic, Helpers)

---

## 📌 1. Giới Thiệu Tổng Quan

Hệ thống **Quản lý cửa hàng bán máy vi tính** là ứng dụng desktop hoàn chỉnh được xây dựng nhằm giải quyết bài toán quản lý kinh doanh thiết bị tin học, linh kiện và máy vi tính trọn gói. 

Ứng dụng đáp ứng toàn diện quy trình vận hành thực tế của một cửa hàng máy tính: từ quản lý danh mục linh kiện phần cứng chi tiết, quản lý nhập hàng từ nhà cung cấp, bán lẻ cho khách hàng, kiểm soát dòng tiền, cho đến các báo cáo thống kê doanh thu, tồn kho và hiệu suất làm việc của nhân viên.

---

## 🛠️ 2. Công Nghệ & Kỹ Thuật Lập Trình Sử Dụng

| Thành Phần | Công Nghệ / Kỹ Thuật | Mô Tả Chi Tiết |
| :--- | :--- | :--- |
| **Giao Diện (GUI)** | Windows Forms (WinForms) | Thiết kế theo phong cách **Modern Flat UI Dashboard** với bảng màu hiện đại (`#18181B` / `#F4F4F5`), bo góc tinh tế, chuyển form mượt mà trong Panel nội dung chính. |
| **Xử Lý Dữ Liệu** | ADO.NET (`System.Data.SqlClient`) | Lập trình hướng dữ liệu thuần túy với `SqlConnection`, `SqlCommand`, `SqlDataAdapter`, `DataTable` — **không phụ thuộc Entity Framework**, tối ưu hiệu năng và độ ổn định cao. |
| **Cơ Sở Dữ Liệu** | Microsoft SQL Server | Chuẩn hóa **3NF** toàn bộ 21 bảng, toàn vẹn dữ liệu qua Primary Key (PK), Foreign Key (FK) kèm ràng buộc `ON UPDATE CASCADE`. |
| **Tự Động Hóa CSDL** | SQL Server Triggers & Transaction | Tự động cập nhật kho, giá nhập và giá bán bằng Trigger `AFTER INSERT, UPDATE, DELETE`, kiểm soát giao dịch bằng `BEGIN TRAN / COMMIT / ROLLBACK` ngăn chặn hoàn toàn xuất kho âm. |
| **Chống Giật Lag Lưới** | DoubleBuffered Reflection | Tối ưu hóa `DataGridView` bằng Reflection kích hoạt bộ đệm kép `DoubleBuffered`, loại bỏ triệt để hiện tượng nhấp nháy khi cuộn danh sách hàng nghìn dòng. |
| **Kết Nối Thông Minh** | Auto-Discovery Connection String | Hàm kết nối thông minh tự động dò tìm các instance SQL Server (`.\SQLEXPRESS01`, `.\SQLEXPRESS`, `.`, `localhost`), giúp chạy ngay trên mọi máy tính mà không cần cấu hình thủ công. |
| **Xử Lý Ảnh Mượt Mà** | FileStream & MemoryStream | Đọc và nạp ảnh sản phẩm máy vi tính thông qua bộ nhớ đệm Stream, tuyệt đối **không khóa file ảnh** trên ổ cứng, cho phép người dùng thay đổi hoặc xóa ảnh tức thì. |
| **Tiện Ích Nghiệp Vụ** | Chuyển Số Thành Chữ Tiếng Việt | Thuật toán chuyển đổi số tiền hóa đơn thành chữ tiếng Việt chuẩn quy chuẩn kế toán (ví dụ: *Hai mươi hai triệu đồng chẵn*). |
| **Tự Động Kiểm Thử** | Self-Test Suite Tích Hợp | Bộ kiểm thử tự động 9 bài test tích hợp trực tiếp trên giao diện để kiểm tra tức thì tính toàn vẹn CSDL, Triggers và các ràng buộc nghiệp vụ. |

---

## 🗄️ 3. Thiết Kế Cơ Sở Dữ Liệu Chuẩn Hóa 3NF (21 Bảng)

Cơ sở dữ liệu được thiết kế tối ưu hóa theo mô hình quan hệ, gồm **21 bảng** phân định rõ ràng các chức năng:

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
└──────┬───────┘             └──────────────┘             └──────┬───────┘
       │                                                         │
       ▼                                                         ▼
┌──────────────┐                                          ┌──────────────┐
│  ChiTietHDN  │──[Trigger Cập nhật kho, giá nhập/bán]──>│  ChiTietHDB  │──[Trigger Trừ kho, chặn âm]
└──────────────┘                                          └──────────────┘
```

### Chi Tiết Phân Nhóm 21 Bảng:
1. **14 Bảng Danh Mục Thuộc Tính & Cấu Hình Máy Tính:**
   - `LoaiMay` (Mã loại, Tên loại máy)
   - `Chip` (Mã chip, Tên loại CPU)
   - `RAM` (Mã RAM, Dung lượng RAM)
   - `OCung` (Mã ổ cứng, Loại ổ: SSD / HDD / NVMe)
   - `DungLuong` (Mã dung lượng ổ cứng: 256GB, 512GB, 1TB...)
   - `TocDo` (Mã tốc độ, Tốc độ xung nhịp)
   - `OCD` (Mã ổ đĩa quang, Tên loại ổ CD/DVD)
   - `ManHinh` (Mã màn hình, Tên loại tấm nền: IPS, OLED, VA...)
   - `CoMH` (Mã cỡ màn hình: 14 inch, 15.6 inch, 27 inch...)
   - `Chuot` (Mã chuột đi kèm)
   - `BanPhim` (Mã bàn phím đi kèm)
   - `USB` (Mã cổng USB hỗ trợ: USB 3.2, Type-C, Thunderbolt...)
   - `Loa` (Mã loa tích hợp)
   - `HangSanXuat` (Mã hãng sản xuất: Dell, Asus, HP, Lenovo, Apple, Acer...)

2. **3 Bảng Danh Mục Nghiệp Vụ:**
   - `NhanVien` (Mã NV, Tên NV, Giới tính, Ngày sinh, Điện thoại, Địa chỉ, Mã công việc)
   - `KhachHang` (Mã khách, Tên khách hàng, Địa chỉ, Điện thoại)
   - `NhaCungCap` (Mã NCC, Tên NCC, Địa chỉ, Điện thoại)

3. **4 Bảng Giao Dịch & Chi Tiết:**
   - `MayViTinh` (Bảng trung tâm lưu trữ thông tin sản phẩm, cấu hình liên kết, số lượng tồn kho, giá nhập, giá bán, thời gian bảo hành, file ảnh, ghi chú)
   - `HoaDonNhap` (Mã HDN, Ngày nhập, Mã NV lập, Mã NCC, Tổng tiền)
   - `ChiTietHDN` (Mã HDN, Mã máy vi tính, Số lượng nhập, Đơn giá nhập, Giảm giá %, Thành tiền)
   - `HoaDonBan` (Mã HDB, Ngày bán, Mã NV bán, Mã khách hàng, Tổng tiền)
   - `ChiTietHDB` (Mã HDB, Mã máy vi tính, Số lượng bán, Đơn giá bán, Giảm giá %, Thành tiền)

---

## 🎯 4. Giải Quyết Trọn Vẹn 9 Yêu Cầu Nghiệp Vụ Của Đề Tài

| STT | Tên Nghiệp Vụ | Cơ Chế Kỹ Thuật Hiện Thực |
| :---: | :--- | :--- |
| **Yêu Cầu 1** | **Cập nhật số lượng tồn kho tự động** | Dùng **SQL Trigger** `AFTER INSERT, UPDATE, DELETE` trên cả 2 bảng `ChiTietHDN` (tự động cộng tồn kho) và `ChiTietHDB` (tự động trừ tồn kho khi bán; tự động hoàn lại tồn kho khi xóa mặt hàng). |
| **Yêu Cầu 2** | **Cập nhật Đơn giá nhập mới nhất** | Trigger trên bảng `ChiTietHDN` tự động lấy đơn giá của lần nhập gần nhất và ghi đè vào cột `GiaNhap` trong bảng `MayViTinh`. |
| **Yêu Cầu 3** | **Tự động tính Đơn giá bán = 110% * Đơn giá nhập** | Trigger tự động tính toán: `GiaBan = ROUND(GiaNhap * 1.10, 0)` ngay khi nhập hàng và cập nhật vào bảng `MayViTinh`. |
| **Chống Xuất Âm** | **Ràng buộc kiểm tra tồn kho** | Trigger trên `ChiTietHDB` so sánh số lượng bán với tồn kho thực tế. Nếu `SoLuongBan > SoLuongTon`, trigger tự động ném ngoại lệ `RAISERROR` và `ROLLBACK TRANSACTION` để hủy giao dịch. |
| **Yêu Cầu 4** | **Tìm kiếm máy vi tính theo đa tiêu chí** | Form tìm kiếm đa năng: kết hợp tìm kiếm theo Mã loại, Dung lượng ổ cứng, Tốc độ, RAM, Cỡ màn hình, Hãng SX... xây dựng câu lệnh Dynamic SQL `WHERE 1=1` linh hoạt. |
| **Yêu Cầu 5** | **Tìm kiếm Hóa đơn bán đa điều kiện** | Lọc hóa đơn bán hàng theo Mã HDB, Mã nhân viên, Mã khách hàng, Tháng/Năm lập hóa đơn, và khoảng Tổng tiền. |
| **Yêu Cầu 6** | **Báo cáo máy vi tính đã nhập theo Quý / Năm** | Truy vấn tổng hợp dữ liệu `HoaDonNhap` và `ChiTietHDN` theo `DATEPART(QUARTER, ...)` và Năm, tính tổng số lượng nhập và tổng tiền nhập. |
| **Yêu Cầu 7** | **Báo cáo máy vi tính đã bán theo Quý / Năm** | Truy vấn tổng hợp dữ liệu `HoaDonBan` và `ChiTietHDB` theo `DATEPART(QUARTER, ...)` và Năm, tính tổng doanh thu bán hàng. |
| **Yêu Cầu 8** | **Báo cáo máy vi tính còn tồn kho theo từng Loại** | Thống kê số lượng máy tính còn tồn kho trong bảng `MayViTinh` phân nhóm theo từng `LoaiMay` tính đến thời điểm hiện tại. |
| **Yêu Cầu 9** | **Báo cáo Top 5 nhân viên có doanh thu cao nhất** | Sử dụng câu lệnh `SELECT TOP 5 WITH TIES` kết hợp hàm `SUM(TongTien)` phân nhóm theo nhân viên trong tháng/năm được chọn. |

---

## 🖥️ 5. Các Chức Năng Chính Trên Ứng Dụng

### 1. Dashboard Điều Hướng Chính (`frmMain`)
- Sidebar điều hướng với hiệu ứng Hover hiện đại.
- Hiển thị linh hoạt các form con bên trong Panel chính mà không gây nhấp nháy cửa sổ.
- Tích hợp nút **"Chạy Tự Kiểm Thử (Auto-Test)"** nổi bật cho phép kiểm tra toàn hệ thống trong 1 giây.

### 2. Quản Lý Danh Mục Máy Vi Tính (`frmMayViTinh`)
- Nạp động 14 ComboBox linh kiện phần cứng từ CSDL.
- Thực hiện đầy đủ các thao tác Thêm, Sửa, Xóa, Lưu, Hủy, Đóng.
- Xem trước ảnh đại diện của từng máy vi tính khi chọn dòng trên lưới dữ liệu.
- Tải ảnh từ máy tính thông qua hộp thoại `OpenFileDialog` với định dạng JPG, PNG, BMP mượt mà.

### 3. Quản Lý Hóa Đơn Nhập Hàng (`frmHoaDonNhap`)
- Mô hình **Master-Detail**: Phần trên quản lý thông tin chung (Số HĐ, Ngày nhập, Nhân viên, Nhà cung cấp), phần dưới quản lý chi tiết các mặt hàng.
- Khi chọn mã máy tính, tự động điền tên máy.
- Tự động tính Thành tiền = `Số lượng * Đơn giá * (1 - Giảm giá / 100)`.
- Tự động cộng dồn Tổng tiền hóa đơn và chuyển số tiền thành chữ tiếng Việt.
- Nhấp đúp chuột (`CellDoubleClick`) vào một dòng mặt hàng để xóa và hoàn trả số lượng tức thì.

### 4. Quản Lý Hóa Đơn Bán Hàng (`frmHoaDonBan`)
- Quản lý thông tin khách hàng, nhân viên bán hàng, ngày lập hóa đơn.
- Khi chọn mã máy tính, tự động hiển thị đơn giá bán chuẩn (đã được tính 110%).
- Kiểm tra tồn kho ngay trên giao diện trước khi thêm hàng; nếu vượt quá số lượng sẽ cảnh báo ngay cho thu ngân.
- Hỗ trợ xuất và in hóa đơn chi tiết.

### 5. Tra Cứu & Tìm Kiếm Đa Điều Kiện (`frmTimKiem`)
- Hỗ trợ 2 Tab tìm kiếm chuyên biệt:
  - **Tìm kiếm Máy Vi Tính**: Tìm theo Loại máy, RAM, Ổ cứng, Cỡ màn hình, Hãng SX.
  - **Tìm kiếm Hóa Đơn Bán**: Tìm theo Mã HĐ, Mã nhân viên, Mã khách, Tháng, Năm, Tổng tiền.
- Báo cáo số lượng bản ghi thỏa mãn điều kiện tìm kiếm.

### 6. Báo Cáo & Thống Kê Doanh Thu (`frmBaoCaoThongKe`)
- **Báo cáo nhập hàng**: Lọc theo Quý (1, 2, 3, 4) và Năm.
- **Báo cáo bán hàng**: Lọc theo Quý và Năm kèm tổng doanh thu đạt được.
- **Báo cáo hàng tồn kho**: Thống kê danh sách máy còn tồn kho theo từng loại máy.
- **Top 5 Nhân viên xuất sắc**: Xếp hạng 5 nhân viên có doanh thu cao nhất theo tháng/năm.

### 7. Module Tự Kiểm Thử Hệ Thống (`SelfTestSuite`)
- Tự động chạy chuỗi 9 bài test liên hoàn:
  - Kiểm tra kết nối SQL Server.
  - Kiểm tra sự tồn tại của đầy đủ 21 bảng.
  - Thử nghiệm nhập hàng ➔ kiểm tra tồn kho tăng.
  - Kiểm tra giá nhập tự động cập nhật.
  - Kiểm tra giá bán tự động tính = 110% giá nhập.
  - Thử nghiệm bán vượt số lượng ➔ kiểm tra Trigger chặn xuất âm thành công.
  - Thử nghiệm bán hàng hợp lệ ➔ kiểm tra tồn kho giảm đúng.
  - Kiểm tra tính tổng tiền hóa đơn bán.
  - Thử nghiệm xóa mặt hàng khỏi hóa đơn ➔ kiểm tra tồn kho được hoàn trả nguyên vẹn.
  - Tự động dọn dẹp sạch sẽ dữ liệu thử nghiệm (Cleanup & Rollback).

---

## 🚀 6. Hướng Dẫn Cài Đặt & Chạy Ứng Dụng

### Yêu Cầu Môi Trường:
- **Hệ điều hành:** Windows 10 / Windows 11.
- **IDE:** Microsoft Visual Studio 2019 hoặc Visual Studio 2022 (đã cài đặt gói `.NET desktop development`).
- **CSDL:** Microsoft SQL Server (bản Enterprise, Standard hoặc SQL Server Express).

### Các Bước Thực Hiện:

#### Bước 1: Khởi Tạo Cơ Sở Dữ Liệu
1. Mở công cụ **SQL Server Management Studio (SSMS)** hoặc **Azure Data Studio**.
2. Kết nối vào SQL Server trên máy của bạn (ví dụ: `.\SQLEXPRESS` hoặc `.\SQLEXPRESS01` hoặc `.`).
3. Mở file [Database_QLBanMayViTinh.sql](Database_QLBanMayViTinh.sql).
4. Nhấn **Execute** (hoặc phím `F5`) để tạo Database `QLBanMayViTinh`, 21 bảng, toàn bộ Triggers và dữ liệu mẫu khởi tạo.

#### Bước 2: Mở Dự Án Bằng Visual Studio
1. Nhấp đúp chuột vào file [QLBanMayViTinh.sln](QLBanMayViTinh.sln) để mở toàn bộ dự án trong Visual Studio.
2. (Tùy chọn) Nếu SQL Server của bạn có cấu hình instance đặc thù, bạn có thể kiểm tra chuỗi kết nối tại file [App.config](App.config):
   ```xml
   <connectionStrings>
       <add name="ConnStr" 
            connectionString="Data Source=.\SQLEXPRESS01;Initial Catalog=QLBanMayViTinh;Integrated Security=True" 
            providerName="System.Data.SqlClient" />
   </connectionStrings>
   ```
   *(Lưu ý: Ứng dụng đã tích hợp sẵn cơ chế Auto-Discovery nên sẽ tự động thử kết nối các instance khả dụng trên máy).*

#### Bước 3: Biên Dịch & Chạy Chương Trình
1. Nhấn tổ hợp phím **`Ctrl + Shift + B`** (hoặc vào menu `Build` ➔ `Build Solution`).
2. Nhấn phím **`F5`** (hoặc nút **Start** màu xanh) để khởi chạy ứng dụng.
3. Khi giao diện chính mở lên, nhấn vào nút màu tím **"Chạy Tự Kiểm Thử (Auto-Test)"** trên thanh menu để kiểm tra kết nối CSDL và các tính năng nghiệp vụ.

---

## 📂 7. Cấu Trúc Thư Mục Dự Án

```text
BTLLAPTRINHTRUCQUAN/
│
├── QLBanMayViTinh.sln                 # File Solution chính mở bằng Visual Studio
├── QLBanMayViTinh.csproj              # File cấu hình Project C# WinForms .NET 4.8
├── Program.cs                         # Điểm vào chương trình (Main Entry Point)
├── App.config                         # File cấu hình hệ thống & Chuỗi kết nối CSDL
├── Database_QLBanMayViTinh.sql        # Kịch bản SQL tạo Database, 21 bảng, Triggers, Dữ liệu mẫu
├── README.md                          # Tài liệu hướng dẫn & Báo cáo kỹ thuật dự án
├── .gitignore                         # Danh sách loại trừ file build/cache khi commit git
│
├── Classes/                           # Thư mục chứa các lớp xử lý nghiệp vụ dùng chung
│   ├── Functions.cs                   # ADO.NET SqlClient thuần, Auto-Discovery CSDL, tiện ích đọc số, xử lý ảnh
│   └── UIThemeHelper.cs               # Bảng màu Flat Design, hàm bo góc và DoubleBuffered chống giật lưới
│
├── Forms/                             # Thư mục chứa toàn bộ giao diện người dùng (Windows Forms)
│   ├── frmMain.cs                     # Form Dashboard chính điều hướng các chức năng
│   ├── frmMain.Designer.cs
│   ├── frmMayViTinh.cs                 # Form Quản lý danh mục 14 thành phần máy vi tính
│   ├── frmMayViTinh.Designer.cs
│   ├── frmHoaDonNhap.cs               # Form Quản lý hóa đơn nhập hàng Master-Detail
│   ├── frmHoaDonNhap.Designer.cs
│   ├── frmHoaDonBan.cs                # Form Quản lý hóa đơn bán hàng Master-Detail
│   ├── frmHoaDonBan.Designer.cs
│   ├── frmTimKiem.cs                  # Form Tìm kiếm đa điều kiện máy tính & hóa đơn
│   ├── frmTimKiem.Designer.cs
│   ├── frmBaoCaoThongKe.cs            # Form Thống kê doanh thu, tồn kho, Top 5 nhân viên
│   └── frmBaoCaoThongKe.Designer.cs
│
├── Testing/                           # Thư mục kiểm thử tự động
│   └── SelfTestSuite.cs               # Bộ kịch bản 9 bài test tự động xác minh Triggers & CSDL
│
└── Images/                            # Thư mục lưu trữ hình ảnh minh họa sản phẩm máy vi tính
```

---

## ⚖️ Giấy Phép & Bản Quyền
Dự án được phát triển phục vụ mục đích học tập và nghiên cứu trong học phần **Lập trình trực quan**. Toàn bộ mã nguồn và cơ sở dữ liệu mở cho mục đích tham khảo học thuật.
