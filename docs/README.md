# HỆ THỐNG BÁO CÁO KỸ THUẬT CHI TIẾT 14 FORMS HỆ THỐNG ERP QLBanMayViTinh

Chào mừng đến với thư mục tài liệu kỹ thuật chi tiết của đồ án **Hệ Thống Quản Lý Bán Máy Vi Tính (QLBanMayViTinh)**.

Tại đây cung cấp đầy đủ 14 bản báo cáo độc lập cho từng Form trong hệ thống, trình bày chuyên sâu về:
1. **Cách hoạt động & Luồng xử lý (Workflow & Lifecycle)** có sơ đồ Mermaid (Flowchart, Sequence).
2. **Công nghệ & Kỹ thuật áp dụng** (WinForms Modern GDI+, ADO.NET, Parameterized Query, Trigger, AES-256, BCrypt, EPPlus...).
3. **Cấu trúc mã nguồn chi tiết** (Danh sách hàm, tham số, giải thích từng khối code nghiệp vụ, câu lệnh SQL, cơ chế xử lý ngoại lệ).
4. **Giao diện & Ảnh chụp minh chứng** (Light Mode / Dark Mode / Trạng thái lỗi).

---

## DANH MỤC 14 BÁO CÁO KỸ THUẬT THEO TỪNG FORM

| STT | Tên Form | File Tài Liệu | Mục Đích Nghiệp Vụ & Điểm Nổi Bật |
|:---:|:---|:---|:---|
| 01 | **frmDangNhap** | [README_frmDangNhap.md](file:///d:/HOCTRENLOP/LAPTRINHTRUCQUAN/BTLLAPTRINHTRUCQUAN/docs/forms/README_frmDangNhap.md) | Xác thực danh tính, băm mật khẩu SHA-256 + Salt, toggle ẩn/hiện mật khẩu, chống Brute-Force, nạp theme. |
| 02 | **frmDangKy** | [README_frmDangKy.md](file:///d:/HOCTRENLOP/LAPTRINHTRUCQUAN/BTLLAPTRINHTRUCQUAN/docs/forms/README_frmDangKy.md) | Đăng ký tài khoản nhân viên mới, sinh Salt ngẫu nhiên, kiểm tra trùng lặp và liên kết hồ sơ nhân sự. |
| 03 | **frmQuenMatKhau** | [README_frmQuenMatKhau.md](file:///d:/HOCTRENLOP/LAPTRINHTRUCQUAN/BTLLAPTRINHTRUCQUAN/docs/forms/README_frmQuenMatKhau.md) | Khôi phục mật khẩu tự động qua số điện thoại nhân viên đăng ký, chống dò quét tài khoản. |
| 04 | **frmMain** | [README_frmMain.md](file:///d:/HOCTRENLOP/LAPTRINHTRUCQUAN/BTLLAPTRINHTRUCQUAN/docs/forms/README_frmMain.md) | Dashboard Bento Grid hiển thị KPI thời gian thực, điều hướng Form con SDI, phân quyền RBAC 3 cấp độ. |
| 05 | **frmMayViTinh** | [README_frmMayViTinh.md](file:///d:/HOCTRENLOP/LAPTRINHTRUCQUAN/BTLLAPTRINHTRUCQUAN/docs/forms/README_frmMayViTinh.md) | Quản lý 14 linh kiện chuẩn 3NF, cho phép sửa trực tiếp Giá Nhập, Giá Bán, Tồn Kho, kiểm tra giá bán $\ge$ giá nhập. |
| 06 | **frmHoaDonNhap** | [README_frmHoaDonNhap.md](file:///d:/HOCTRENLOP/LAPTRINHTRUCQUAN/BTLLAPTRINHTRUCQUAN/docs/forms/README_frmHoaDonNhap.md) | Lập phiếu nhập kho Master-Detail, Database Trigger tự động tăng tồn kho & cập nhật giá vốn, xuất Excel. |
| 07 | **frmHoaDonBan** | [README_frmHoaDonBan.md](file:///d:/HOCTRENLOP/LAPTRINHTRUCQUAN/BTLLAPTRINHTRUCQUAN/docs/forms/README_frmHoaDonBan.md) | Lập hóa đơn bán lẻ Master-Detail, cơ chế chống xuất âm kho (`SoLuong <= TonKho`), tự trừ kho, in hóa đơn. |
| 08 | **frmDanhMucHeThong** | [README_frmDanhMucHeThong.md](file:///d:/HOCTRENLOP/LAPTRINHTRUCQUAN/BTLLAPTRINHTRUCQUAN/docs/forms/README_frmDanhMucHeThong.md) | Quản trị tập trung 4 Tabs (Khách hàng, NCC, Nhân viên, 14 Linh kiện 3NF), mã hóa AES-256 SĐT & Địa chỉ. |
| 09 | **frmTimKiem** | [README_frmTimKiem.md](file:///d:/HOCTRENLOP/LAPTRINHTRUCQUAN/BTLLAPTRINHTRUCQUAN/docs/forms/README_frmTimKiem.md) | Tra cứu đa tiêu chí (Yêu cầu 4) trên Máy Vi Tính, Hóa Đơn Nhập, Hóa Đơn Bán; layout động chống tràn chữ. |
| 10 | **frmBaoCaoThongKe** | [README_frmBaoCaoThongKe.md](file:///d:/HOCTRENLOP/LAPTRINHTRUCQUAN/BTLLAPTRINHTRUCQUAN/docs/forms/README_frmBaoCaoThongKe.md) | Trung tâm BI giải quyết Yêu cầu 6, 7, 8, 9; 4 thẻ KPI Realtime, xuất báo cáo Excel định dạng OpenXML. |
| 11 | **frmQuanLyNguoiDung**| [README_frmQuanLyNguoiDung.md](file:///d:/HOCTRENLOP/LAPTRINHTRUCQUAN/BTLLAPTRINHTRUCQUAN/docs/forms/README_frmQuanLyNguoiDung.md) | Quản trị tài khoản hệ thống, phân quyền RBAC, khóa/mở khóa tài khoản, cơ chế chống tự khóa Admin. |
| 12 | **frmNhatKyHoatDong** | [README_frmNhatKyHoatDong.md](file:///d:/HOCTRENLOP/LAPTRINHTRUCQUAN/BTLLAPTRINHTRUCQUAN/docs/forms/README_frmNhatKyHoatDong.md) | Hệ thống Audit Trail truy vết lịch sử thao tác toàn diện, tích hợp cảnh báo sản phẩm sắp hết hàng. |
| 13 | **frmHoSoCaNhan** | [README_frmHoSoCaNhan.md](file:///d:/HOCTRENLOP/LAPTRINHTRUCQUAN/BTLLAPTRINHTRUCQUAN/docs/forms/README_frmHoSoCaNhan.md) | Quản lý thông tin tài khoản đang đăng nhập, đổi mật khẩu 3 bước bảo mật, tra cứu lịch sử lập hóa đơn. |
| 14 | **frmCaiDat** | [README_frmCaiDat.md](file:///d:/HOCTRENLOP/LAPTRINHTRUCQUAN/BTLLAPTRINHTRUCQUAN/docs/forms/README_frmCaiDat.md) | Cấu hình kết nối SQL Server chủ động, lưu trữ Key-Value thông tin cửa hàng, Sao lưu/Phục hồi CSDL .bak. |

