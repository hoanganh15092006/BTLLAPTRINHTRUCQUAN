# -*- coding: utf-8 -*-
"""
Generator for:
1. Presentation/Slide_Thuyet_Trinh_32_Slides_Full.html (Interactive HTML Web Presentation, 32 slides)
2. Presentation/Kich_Ban_Thuyet_Trinh_32_Slides.md (Detailed Speaking Script + 12 Defense Q&A)
3. Presentation/QLBanMayViTinh_32_Slides.pptx (32 PowerPoint Slides with images & notes)
"""

import os
import sys
import json
from pptx import Presentation
from pptx.util import Inches, Pt
from pptx.dml.color import RGBColor
from pptx.enum.text import PP_ALIGN

sys.stdout.reconfigure(encoding='utf-8')

OUT_DIR = os.path.abspath("Presentation")
ASSETS_DIR = os.path.join(OUT_DIR, "assets")

# 32 Slides Definition
SLIDES_DATA = [
    {
        "id": 1,
        "title": "Hệ Thống Quản Lý Bán Máy Vi Tính",
        "subtitle": "Báo Cáo Đồ Án Lập Trình Trực Quan — Kiến Trúc 3 Tầng WinForms & Pure ADO.NET SQL Server",
        "tag": "BÁO CÁO ĐỒ ÁN MÔN HỌC",
        "badges": ["C# .NET WinForms", "Pure ADO.NET", "SQL Server 3NF", "GDI+ Dual-Theme"],
        "summary": "Giải pháp phần mềm quản trị toàn diện cho chuỗi bán lẻ thiết bị công nghệ và máy tính, tối ưu hóa giao diện công thái học với động cơ chuyển đổi Theme Sáng/Tối tức thì.",
        "bullets": [
            "Kiến trúc 3 tầng (Presentation, Business & Helper, Data Layer) độc lập và tường minh.",
            "Chuẩn hóa 25 bảng CSDL quan hệ đạt chuẩn 3NF, loại bỏ hoàn toàn dư thừa dữ liệu.",
            "Tự động hóa đồng bộ tồn kho và tính toán doanh thu bằng SQL Server Triggers (ACID).",
            "Động cơ xuất hóa đơn và báo cáo Excel Native HTML tốc độ cao không phụ thuộc Office COM."
        ],
        "image_main": "assets/ui_main_light.png",
        "image_sub": None,
        "notes": "[00:00 - 01:15] Kính thưa quý Thầy Cô trong Hội đồng phản biện. Hôm nay, em xin trân trọng báo cáo đồ án 'Hệ Thống Quản Lý Bán Máy Vi Tính' xây dựng trên nền tảng C# WinForms .NET và SQL Server. Điểm nổi bật của đề tài là chúng em áp dụng kiến trúc 3 tầng phân lớp rõ ràng, không sử dụng các ORM cồng kềnh mà tối ưu hóa 100% bằng ADO.NET thuần, chuẩn hóa cơ sở dữ liệu quan hệ 25 bảng đạt chuẩn 3NF, đồng bộ kho hàng bằng Trigger thời gian thực và xây dựng động cơ giao diện GDI+ Dual-Theme tự phát triển."
    },
    {
        "id": 2,
        "title": "Bài Toán Thực Tế Chuỗi Bán Lẻ Máy Tính & Mục Tiêu",
        "subtitle": "Khảo sát thực tiễn tại các chuỗi bán lẻ công nghệ (Phong Vũ, HACOM, GearVN)",
        "tag": "BÀI TOÁN & TẦM NHÌN",
        "badges": ["Thực Tế Doanh Nghiệp", "Quản Lý Đa Linh Kiện", "Chống Thất Thoát Tồn Kho"],
        "summary": "Kinh doanh máy vi tính đòi hỏi quản lý phức tạp từ linh kiện rời (CPU, RAM, VGA) đến máy hoàn chỉnh, với yêu cầu kiểm soát tồn kho và doanh thu tức thì.",
        "bullets": [
            "Thách thức quản lý hàng ngàn mã sản phẩm đa chủng loại và linh kiện cấu hình cao.",
            "Nguy cơ thất thoát do chênh lệch số lượng thực tế trong kho và dữ liệu phần mềm.",
            "Đòi hỏi tốc độ lập hóa đơn POS tại quầy nhanh chóng, không có độ trễ.",
            "Mục tiêu: Xây dựng hệ thống vận hành mượt mà, phản hồi mili-giây, giao diện công thái học."
        ],
        "image_main": "assets/graph_01_overview_3tier.png",
        "image_sub": None,
        "notes": "[01:15 - 02:30] Thưa thầy cô, kinh doanh máy tính là ngành đặc thù có giá trị đơn hàng lớn và vòng đời linh kiện thay đổi nhanh. Việc ghi chép thủ công hoặc dùng các phần mềm rời rạc rất dễ dẫn đến bán âm kho hoặc sai lệch chiết khấu. Đồ án này giải quyết trọn vẹn bài toán từ khâu nhập hàng, cấu hình danh mục linh kiện, bán lẻ POS tại quầy đến báo cáo BI tức thì cho ban giám đốc."
    },
    {
        "id": 3,
        "title": "Toàn Cảnh Hệ Sinh Thái Công Nghệ Sử Dụng",
        "subtitle": "Lựa chọn công nghệ tối ưu hóa tốc độ thực thi và tính ổn định doanh nghiệp",
        "tag": "TECH STACK SHOWCASE",
        "badges": ["C# .NET WinForms", "Pure ADO.NET", "GDI+ GraphicsPath", "SQL Server ACID"],
        "summary": "Sự kết hợp hoàn hảo giữa giao diện Windows Forms thuần túy và tầng cơ sở dữ liệu quan hệ mạnh mẽ, đảm bảo thời gian đáp ứng cực nhanh.",
        "bullets": [
            "Ngôn ngữ C# .NET: Biên dịch mã máy IL, tận dụng tối đa tài nguyên phần cứng Windows.",
            "Tầng kết nối Pure ADO.NET: Quản lý SqlConnection trực tiếp, loại bỏ độ trễ của ORM.",
            "Custom GDI+ Engine: Tạo trải nghiệm giao diện hiện đại với các góc bo tròn và bóng đổ.",
            "SQL Server Relational Engine: Đảm bảo tính nhất quán giao dịch (ACID) tuyệt đối."
        ],
        "image_main": "assets/graph_02_call_dependency.png",
        "image_sub": None,
        "notes": "[02:30 - 03:45] Về công nghệ, nhóm lựa chọn C# WinForms kết hợp Pure ADO.NET. Thay vì sử dụng Entity Framework có độ trễ cao khi xử lý lượng lớn bản ghi trên máy trạm bán hàng, ADO.NET thuần với SqlDataReader và SqlDataAdapter giúp thời gian nạp dữ liệu giảm xuống dưới 15ms. Toàn bộ hiệu ứng bo góc và đổ màu giao diện được dựng bằng GDI+ nội sinh."
    },
    {
        "id": 4,
        "title": "Kiến Trúc Hệ Thống Đa Tầng (3-Tier Enterprise)",
        "subtitle": "Phân tầng kiến trúc minh bạch, đảm bảo tính mở rộng và dễ bảo trì",
        "tag": "KIẾN TRÚC HỆ THỐNG",
        "badges": ["Presentation Layer", "Business Layer", "Data Access Layer"],
        "summary": "Tách bạch 3 lớp: Presentation (14 Forms giao diện), Business & Helper Layer (Functions, UIThemeHelper, SecurityHelper) và Data Layer (SQL Server).",
        "bullets": [
            "Presentation Layer: 14 Forms giao diện chuyên biệt hóa cho từng vai trò người dùng.",
            "Business & Helper Layer: Xử lý nghiệp vụ tập trung, kiểm tra logic, phân quyền RBAC.",
            "Data Layer: Cơ sở dữ liệu chuẩn 3NF, lưu trữ an toàn và quản lý tính toàn vẹn bằng Trigger.",
            "Không để lộ câu lệnh kết nối trực tiếp trong Form, đảm bảo tính bảo mật và module hóa."
        ],
        "image_main": "assets/graph_01_overview_3tier.png",
        "image_sub": None,
        "notes": "[03:45 - 05:00] Trên màn hình là sơ đồ kiến trúc 3 tầng tổng thể của dự án được trích xuất qua công cụ Graphify. Presentation Layer gồm 14 WinForms. Toàn bộ tương tác dữ liệu và nghiệp vụ phải đi qua lớp trung gian Functions.cs và UIThemeHelper.cs trước khi chạm tới Data Layer là SQL Server. Nhờ vậy, nếu sau này cần thay đổi cấu trúc bảng, chúng ta chỉ cần sửa tại lớp Helper mà không phải viết lại mã nguồn trong các Form."
    },
    {
        "id": 5,
        "title": "Bản Đồ Phụ Thuộc & Luồng Gọi Hàm (Call Graph)",
        "subtitle": "Phân tích đồ thị liên kết mô-đun toàn hệ thống từ Graphify",
        "tag": "GRAPHIFY DEPENDENCY MAP",
        "badges": ["Low Coupling", "High Cohesion", "Static Analysis", "Code Quality"],
        "summary": "Đồ thị liên kết chứng minh mức độ kết dính cao bên trong các module và sự phụ thuộc lỏng lẻo giữa giao diện và cơ sở dữ liệu.",
        "bullets": [
            "Tất cả 14 Forms đều hội tụ về 2 điểm trung chuyển: Functions.cs và UIThemeHelper.cs.",
            "Loại bỏ hoàn toàn các phụ thuộc vòng (Circular Dependencies) giữa các Form.",
            "Cơ chế MDI/Panel Shell giúp frmMain đóng vai trò điều phối trung tâm.",
            "Giảm thiểu sự phân mảnh logic nghiệp vụ, nâng cao độ tin cậy và khả năng tái sử dụng."
        ],
        "image_main": "assets/graph_02_call_dependency.png",
        "image_sub": None,
        "notes": "[05:00 - 06:15] Khi quét AST toàn bộ dự án bằng công cụ Graphify, kết quả đồ thị Call Graph cho thấy kiến trúc đạt chuẩn High Cohesion và Low Coupling. Hai trung tâm điều hướng duy nhất là Functions.cs chịu trách nhiệm toàn bộ ADO.NET và phiên đăng nhập, cùng UIThemeHelper.cs chịu trách nhiệm toàn bộ giao diện và bảng màu. Các Form hoàn toàn độc lập, không form nào gọi chéo dữ liệu của form khác."
    },
    {
        "id": 6,
        "title": "Thiết Kế CSDL Quan Hệ Chuẩn Hóa 3NF & Sơ Đồ ERD",
        "subtitle": "Tối ưu hóa lược đồ dữ liệu loại bỏ triệt để dị thường thêm/xóa/sửa",
        "tag": "DATABASE ERD & 3NF",
        "badges": ["3NF Normalized", "Foreign Key Constraints", "ACID Compliance"],
        "summary": "Cơ sở dữ liệu gồm 25 bảng quan hệ được thiết kế chuẩn mực, bảo đảm tính toàn vẹn dữ liệu từ linh kiện phần cứng đến hóa đơn giao dịch.",
        "bullets": [
            "Bảng sản phẩm tblMayViTinh liên kết chặt chẽ với các bảng danh mục linh kiện (3NF).",
            "Mô hình Master-Detail chuẩn mực: tblHoaDonBan <-> tblChiTietHoaDonBan.",
            "Ràng buộc khóa ngoại CASCADE / RESTRICT bảo vệ dữ liệu chống mồ côi (Orphan records).",
            "Hệ thống bảng người dùng, phân quyền RBAC và bảng lưu nhật ký hoạt động kiểm toán."
        ],
        "image_main": "assets/graph_03_erd_database.png",
        "image_sub": None,
        "notes": "[06:15 - 07:30] Sơ đồ ERD trên slide thể hiện lược đồ CSDL chuẩn 3NF của hệ thống. Chúng em không lưu cứng thông tin linh kiện vào bảng máy tính mà phân rã thành các bảng con như tblLoaiMay, tblHangSanXuat, tblRAM, tblChip, tblManHinh... Bảng hóa đơn được thiết kế theo cặp Master-Detail giúp lưu trữ lịch sử giao dịch chính xác tuyệt đối mà không gây lặp lại dữ liệu."
    },
    {
        "id": 7,
        "title": "Kỹ Thuật Giao Diện: Custom GDI+ Rendering Engine",
        "subtitle": "Đột phá giao diện phẳng với GraphicsPath và Anti-Aliasing nội sinh",
        "tag": "GDI+ THEME ENGINE",
        "badges": ["UIThemeHelper.cs", "GraphicsPath", "Smooth Corners", "No External Libs"],
        "summary": "Xây dựng các thẻ Card và Button bo góc hiện đại theo ngôn ngữ Fluent Design hoàn toàn bằng thư viện đồ họa GDI+ của .NET Framework.",
        "bullets": [
            "Hàm MakeRoundedControl sử dụng GraphicsPath.AddArc tạo 4 cung tròn mềm mại.",
            "Gán vùng hiển thị Region mới cho Control, khử răng cưa và bo góc sắc nét.",
            "Giải phóng bộ nhớ đồ họa lập tức bằng cấu trúc using tránh rò rỉ GDI Handles.",
            "Không cần phụ thuộc vào bất kỳ thư viện UI trả phí nào như DevExpress hay Bunifu."
        ],
        "image_main": "assets/code_01_stylecard_gdi.png",
        "image_sub": None,
        "notes": "[07:30 - 08:45] Điểm ấn tượng đầu tiên về mặt thị giác là giao diện hiện đại như ứng dụng Web hay Fluent UI trên Windows 11. Để đạt được điều này mà không phải tốn chi phí mua các bộ thư viện ngoài, chúng em đã tự viết class UIThemeHelper.cs. Phương thức MakeRoundedControl tạo đường cong bằng GraphicsPath rồi gán vào Region của Control. Tất cả đối tượng đồ họa đều được bọc trong khối using để bảo đảm giải phóng GDI Handle ngay lập tức."
    },
    {
        "id": 8,
        "title": "Kiến Trúc Dual-Theme: Chuyển Đổi Sáng/Tối Tức Thì",
        "subtitle": "Động cơ đệ quy duyệt cây điều khiển giao diện theo thời gian thực",
        "tag": "DUAL-THEME ARCHITECTURE",
        "badges": ["Dark Mode", "Light Mode", "Tree Traversal", "Real-time Switch"],
        "summary": "Người dùng có thể chuyển đổi linh hoạt giữa giao diện Sáng chuyên nghiệp và giao diện Tối hiện đại chỉ với một nút bấm mà không cần khởi động lại ứng dụng.",
        "bullets": [
            "Thuật toán ApplyTheme(Control root) đệ quy duyệt qua toàn bộ cây controls trên Form.",
            "Tự động nhận diện kiểu điều khiển: Button, Panel, TextBox, DataGridView, Label.",
            "Bảng màu được tính toán tương phản đạt chuẩn trợ năng WCAG 2.1 AA.",
            "Đặt cạnh nhau hai ảnh chụp thực tế ui_main_light.png và ui_main_dark.png để so sánh."
        ],
        "image_main": "assets/ui_main_light.png",
        "image_sub": "assets/ui_main_dark.png",
        "notes": "[08:45 - 10:00] Trên slide là hình ảnh so sánh thực tế giữa Light Mode và Dark Mode trên frmMain. Khi người dùng bấm nút đổi Theme, phương thức ApplyTheme nhận form gốc và đệ quy qua toàn bộ cây controls. Bất kể control nằm lồng sâu bao nhiêu tầng trong Panel hay GroupBox, nó đều được gán lại màu nền, màu chữ và kiểu viền theo Palette chuẩn, mang lại trải nghiệm mượt mà không độ trễ."
    },
    {
        "id": 9,
        "title": "Tầng Truy Xuất Dữ Liệu Pure ADO.NET Hiệu Năng Cao",
        "subtitle": "Tối ưu hóa đóng/mở kết nối, thực thi phi tập trung và chống SQL Injection",
        "tag": "DATA ACCESS CORE",
        "badges": ["Functions.cs", "SqlDataAdapter", "ExecuteScalar", "SQL Injection Guard"],
        "summary": "Lớp Functions.cs cung cấp các phương thức truy xuất dữ liệu dùng chung mạnh mẽ, an toàn tuyệt đối nhờ cơ chế tham số hóa SqlParameter.",
        "bullets": [
            "Phương thức Connect() quản lý trạng thái kết nối thông minh, tránh mở trùng lặp.",
            "GetDataToTable() sử dụng SqlDataAdapter nạp nhanh vào DataTable không tốn RAM.",
            "GetFieldValues() tối ưu hóa hàm ExecuteScalar cho các truy vấn lấy giá trị đơn lẻ.",
            "100% câu truy vấn đều sử dụng mảng SqlParameter[], triệt tiêu nguy cơ SQL Injection."
        ],
        "image_main": "assets/code_03_functions_adonet.png",
        "image_sub": None,
        "notes": "[10:00 - 11:15] Đây là trái tim của tầng truy xuất dữ liệu trong Functions.cs. Chúng em áp dụng mô hình kết nối có kiểm soát: hàm Connect() chỉ mở khi kết nối chưa mở. Hàm GetDataToTable tận dụng SqlDataAdapter để tự động ngắt kết nối sau khi Fill vào DataTable. Đặc biệt, hàm GetFieldValues sử dụng ExecuteScalar giúp việc lấy các giá trị tổng tiền hay kiểm tra tồn kho chỉ mất chưa tới 2ms."
    },
    {
        "id": 10,
        "title": "Cổng Xác Thực & Phân Quyền Hệ Thống (`frmDangNhap.cs`)",
        "subtitle": "Quy trình xác thực người dùng, bảo mật mật khẩu và lưu phiên làm việc",
        "tag": "XÁC THỰC & BẢO MẬT",
        "badges": ["frmDangNhap", "RBAC", "BCrypt Verify", "Session Management"],
        "summary": "Form đăng nhập với cơ chế bảo mật cao cấp: kiểm tra tài khoản, xác thực mật khẩu băm, phân quyền vai trò (Admin/Nhân viên) và ghi nhận vết truy cập.",
        "bullets": [
            "Kiểm tra tính toàn vẹn tài khoản và trạng thái hoạt động (Khóa/Mở).",
            "Tự động lưu trữ thông tin phiên làm việc: Functions.CurrentMaNV, Functions.CurrentRole.",
            "Tự động điều hướng và kích hoạt quyền menu tương ứng trên frmMain.",
            "Ghi lại nhật ký đăng nhập thời gian thực vào bảng tblNhatKyHoatDong."
        ],
        "image_main": "assets/ui_dangnhap.png",
        "image_sub": "assets/code_04_dangnhap_auth.png",
        "notes": "[11:15 - 12:30] Khi người dùng đăng nhập tại frmDangNhap, hệ thống truy vấn kiểm tra tài khoản qua câu lệnh SELECT có tham số. Mật khẩu được đối chiếu qua hàm băm an toàn. Nếu thành công, hệ thống lưu mã nhân viên và quyền vào biến tĩnh tĩnh CurrentRole, đồng thời gọi hàm GhiNhatKy để lưu vết kiểm toán. Form chính frmMain sau đó sẽ bật/tắt các menu phù hợp với vai trò của người dùng."
    },
    {
        "id": 11,
        "title": "Đăng Ký Tài Khoản & Khôi Phục Mật Khẩu",
        "subtitle": "Xử lý kiểm tra hợp lệ dữ liệu đa tầng tại `frmDangKy` và `frmQuenMatKhau`",
        "tag": "QUẢN TRỊ NGƯỜI DÙNG",
        "badges": ["Data Validation", "Regex Guard", "Password Reset", "User Onboarding"],
        "summary": "Hệ thống hỗ trợ quy trình đăng ký tài khoản mới cho nhân viên và khôi phục mật khẩu bảo mật khi quên, ngăn ngừa lỗi nhập liệu.",
        "bullets": [
            "Kiểm tra định dạng email, độ mạnh mật khẩu và tính duy nhất của TenDangNhap.",
            "Ràng buộc tài khoản đăng ký phải khớp với mã nhân viên hợp lệ trong tblNhanVien.",
            "Quy trình quên mật khẩu xác thực đa yếu tố: Tên đăng nhập + Số điện thoại + Mã NV.",
            "Thông báo lỗi trực quan trên giao diện, không để xảy ra crash ứng dụng."
        ],
        "image_main": "assets/ui_dangky.png",
        "image_sub": "assets/ui_quenmatkhau.png",
        "notes": "[12:30 - 13:45] Đối với hai màn hình Đăng ký và Quên mật khẩu, chúng em thiết kế bộ kiểm tra hợp lệ dữ liệu nhiều lớp. Tại frmDangKy, tài khoản chỉ được tạo nếu mã nhân viên thực sự tồn tại trong CSDL và chưa được cấp tài khoản. Tại frmQuenMatKhau, người dùng phải cung cấp đúng cả tên đăng nhập và số điện thoại đã lưu trong hồ sơ nhân viên thì mới được phép thiết lập lại mật khẩu mới."
    },
    {
        "id": 12,
        "title": "Trung Tâm Điều Khiển `frmMain.cs`: Kỹ Thuật Nhúng Form",
        "subtitle": "Trải nghiệm Single-Window mượt mà với kỹ thuật Panel Embedding",
        "tag": "SHELL & NAVIGATION",
        "badges": ["Single-Window App", "Panel Embedding", "Memory Management", "Clean UI"],
        "summary": "Khắc phục nhược điểm giật lag của giao diện MDI truyền thống bằng cách nhúng động các Form con trực tiếp vào Panel trung tâm (pnlMain).",
        "bullets": [
            "Hàm OpenChildForm đóng và Dispose hoàn toàn Form con cũ trước khi nạp Form mới.",
            "Thiết lập TopLevel = false, FormBorderStyle = None, Dock = Fill vào pnlMain.",
            "Tự động áp dụng bộ Theme hiện tại cho Form mới được mở.",
            "Đồng bộ trạng thái phát sáng (Highlight) trên thanh Sidebar Menu điều hướng."
        ],
        "image_main": "assets/ui_main_dark.png",
        "image_sub": "assets/code_05_main_embed_form.png",
        "notes": "[13:45 - 15:00] Giao diện chính frmMain là một cửa sổ điều khiển trung tâm theo phong cách ứng dụng hiện đại. Thay vì mở các cửa sổ con trôi nổi gây rối mắt, phương thức OpenChildForm thiết lập Form con thành một User Control nhúng trực tiếp vào pnlMain. Trước khi nạp Form mới, hệ thống tự động Close và Dispose Form cũ để thu hồi bộ nhớ RAM ngay lập tức."
    },
    {
        "id": 13,
        "title": "Quản Lý Danh Mục Máy Vi Tính (`frmMayViTinh.cs` - UI & Nghiệp Vụ)",
        "subtitle": "Giao diện quản lý thông số kỹ thuật, hình ảnh và công thức định giá",
        "tag": "QUẢN TRỊ SẢN PHẨM",
        "badges": ["Product Catalog", "Live PictureBox", "Smart Pricing", "Auto Code"],
        "summary": "Màn hình quản lý máy vi tính được thiết kế công thái học: hiển thị ảnh động, thông số cấu hình và tính toán giá bán tự động từ giá nhập.",
        "bullets": [
            "Hiển thị trực quan danh sách sản phẩm với định dạng tiền tệ Việt Nam (VNĐ).",
            "Tự động load ảnh sản phẩm tương ứng vào PictureBox khi người dùng click chọn hàng.",
            "Tính năng tự sinh mã sản phẩm và gợi ý giá bán lẻ theo biên độ lợi nhuận.",
            "Tích hợp các ComboBox liên kết danh mục: Loại máy, Hãng sản xuất, RAM, Ổ cứng."
        ],
        "image_main": "assets/ui_mayvitinh_view.png",
        "image_sub": "assets/ui_mayvitinh_edit.png",
        "notes": "[15:00 - 16:15] Form frmMayViTinh là nơi nhân viên quản lý toàn bộ kho sản phẩm máy tính. Điểm nổi bật là khi click vào bất kỳ dòng nào trên bảng, ảnh đại diện sản phẩm sẽ được nạp tức thì vào PictureBox từ đường dẫn lưu trữ. Hệ thống cũng tích hợp công thức gợi ý giá bán dựa trên giá vốn nhập kho, giúp nhân viên không phải tính toán thủ công."
    },
    {
        "id": 14,
        "title": "Giải Phẫu Code `frmMayViTinh.cs`: CRUD & Bảo Vệ Khóa Ngoại",
        "subtitle": "Kỹ thuật kiểm tra ràng buộc trước khi xóa và thực thi an toàn",
        "tag": "TOÀN VẸN DỮ LIỆU",
        "badges": ["FK Protection", "No Orphan Records", "Safe Deletion", "Audit Logging"],
        "summary": "Bảo vệ toàn vẹn dữ liệu ở cấp độ ứng dụng: không cho phép xóa máy tính đã có trong lịch sử hóa đơn bán hoặc hóa đơn nhập.",
        "bullets": [
            "Kiểm tra số lượng bản ghi phụ thuộc trong tblChiTietHoaDonBan và tblChiTietHoaDonNhap.",
            "Hiển thị cảnh báo thân thiện thay vì để SQL Server bắn lỗi ngoại lệ (SqlException).",
            "Sử dụng câu lệnh DELETE tham số hóa an toàn khi đáp ứng đủ điều kiện xóa.",
            "Tự động ghi lại hành vi xóa sản phẩm vào nhật ký hệ thống kèm mã sản phẩm."
        ],
        "image_main": "assets/code_06_mayvitinh_crud_fk.png",
        "image_sub": None,
        "notes": "[16:15 - 17:30] Nhìn vào đoạn code nút Xóa trên slide, thầy cô có thể thấy kỹ thuật bảo vệ ràng buộc toàn vẹn dữ liệu. Trước khi thực thi DELETE, hàm truy vấn COUNT(*) trên cả hai bảng chi tiết hóa đơn nhập và hóa đơn bán. Nếu máy tính đã từng phát sinh giao dịch, hệ thống từ chối xóa và hiển thị thông báo rõ ràng, ngăn chặn triệt để lỗi xung đột khóa ngoại."
    },
    {
        "id": 15,
        "title": "Thiết Kế Đa Hình '5-Trong-1' Trên `frmDanhMucHeThong.cs`",
        "subtitle": "Đột phá kiến trúc gom 5 danh mục nghiệp vụ vào một Form duy nhất",
        "tag": "ĐA HÌNH GIAO DIỆN",
        "badges": ["5-in-1 Architecture", "Tab Switching", "Component Reuse", "Clean Design"],
        "summary": "Thay vì tạo 5 form riêng biệt gây cồng kềnh, frmDanhMucHeThong xử lý đa hình toàn bộ 5 danh mục: Loại máy, Hãng SX, Nhà cung cấp, Khách hàng, Nhân viên.",
        "bullets": [
            "5 Nút chuyển đổi danh mục ở thanh trên cùng với hiệu ứng đổi trạng thái trực quan.",
            "Tự động thay đổi tiêu đề, các nhãn nhập liệu và cấu trúc cột DataGridView.",
            "Tái sử dụng 100% các nút chức năng Thêm, Sửa, Xóa, Lưu, Bỏ qua.",
            "Giảm 80% dung lượng mã nguồn và tối ưu hóa bộ nhớ RAM của ứng dụng."
        ],
        "image_main": "assets/ui_danhmuc_tab1_loaimay.png",
        "image_sub": "assets/ui_danhmuc_tab4_khachhang.png",
        "notes": "[17:30 - 18:45] Đây là một trong những giải pháp kiến trúc sáng tạo nhất của dự án. Thay vì phải viết 5 Form riêng lẻ cho Loại máy, Hãng sản xuất, Nhà cung cấp, Khách hàng và Nhân viên, chúng em thiết kế frmDanhMucHeThong hoạt động theo mô hình đa hình 5-trong-1. Khi người dùng click chọn từng tab, toàn bộ nhãn, trường nhập liệu và cột của DataGridView sẽ tự động tái cấu trúc."
    },
    {
        "id": 16,
        "title": "Giải Phẫu Code `frmDanhMucHeThong.cs`: Tái Sử Dụng Control",
        "subtitle": "Cơ chế Dispatcher động tái cấu trúc bảng và câu lệnh SQL theo trạng thái",
        "tag": "POLYMORPHIC ENGINE",
        "badges": ["State Pattern", "Dynamic Columns", "Code Minimization", "Graphify Ast"],
        "summary": "Phương thức SwitchCategoryTab điều phối toàn bộ vòng đời của dữ liệu: xóa cột cũ, dựng cột mới và nạp dữ liệu từ bảng tương ứng.",
        "bullets": [
            "Sử dụng biến trạng thái enum CategoryMode để nhận diện danh mục đang kích hoạt.",
            "Hàm SetupGridColumns tạo động DataGridViewTextBoxColumn với HeaderText tương ứng.",
            "Bộ xử lý CRUD chung phân nhánh truy vấn theo enum, không nhân bản mã lệnh.",
            "Đảm bảo mỗi bảng con đều tuân thủ chính xác các quy tắc chuẩn hóa 3NF."
        ],
        "image_main": "assets/graph_05_polymorphic_danhmuc.png",
        "image_sub": "assets/code_07_danhmuc_polymorphic.png",
        "notes": "[18:45 - 20:00] Sơ đồ luồng Graphify và đoạn mã C# trên slide minh chứng cho cách thức hoạt động của Form đa hình. Phương thức SwitchCategoryTab nhận vào một CategoryMode. Bảng DataGridView được dọn sạch và nạp các cột mới tương ứng với thực thể được chọn. Nhờ cơ chế này, toàn bộ logic CRUD chỉ cần viết một lần duy nhất, giúp mã nguồn vô cùng gọn gàng và dễ bảo trì."
    },
    {
        "id": 17,
        "title": "Nghiệp Vụ Nhập Kho & Quản Trị Nguồn Hàng (`frmHoaDonNhap.cs`)",
        "subtitle": "Quy trình nhập hàng từ Nhà cung cấp và quản lý giá vốn sản phẩm",
        "tag": "QUẢN LÝ NHẬP KHO",
        "badges": ["Goods Receipt", "Supplier Linking", "Batch Ingestion", "Cost Tracking"],
        "summary": "Mô phỏng chính xác quy trình nhập hàng thực tế tại các trung tâm phân phối: lập phiếu nhập, chọn nhà cung cấp, nhập số lượng và đơn giá nhập.",
        "bullets": [
            "Quản lý thông tin phiếu nhập: Mã HDN, ngày lập, nhân viên tiếp nhận, nhà cung cấp.",
            "Chi tiết phiếu nhập: Chọn máy tính, nhập số lượng, đơn giá vốn và tỷ lệ giảm giá.",
            "Tự động tính thành tiền cho từng mặt hàng và tổng thanh toán của phiếu nhập.",
            "Hỗ trợ in phiếu nhập và hủy phiếu có hoàn trả tồn kho chính xác."
        ],
        "image_main": "assets/ui_hoadonnhap.png",
        "image_sub": None,
        "notes": "[20:00 - 21:15] Bước vào phân hệ quản lý kho, frmHoaDonNhap cho phép doanh nghiệp ghi nhận các lô hàng nhập từ đối tác. Nhân viên chỉ cần chọn nhà cung cấp và lần lượt thêm từng mã máy tính cùng đơn giá nhập và chiết khấu. Mọi tính toán thành tiền đều được thực hiện theo thời gian thực ngay trên giao diện."
    },
    {
        "id": 18,
        "title": "Giải Phẫu Code `frmHoaDonNhap.cs`: Master-Detail Pipeline",
        "subtitle": "Kỹ thuật kiểm tra tồn tại Master Header và chèn chi tiết nguyên tử",
        "tag": "MASTER-DETAIL LOGIC",
        "badges": ["Master-Detail", "Atomic Insertion", "Trigger Activation", "Stock Inflow"],
        "summary": "Kỹ thuật tối ưu xử lý bảng Master: tự động khởi tạo bản ghi tblHoaDonNhap nếu chưa có và chèn chi tiết vào tblChiTietHoaDonNhap.",
        "bullets": [
            "Sử dụng câu lệnh IF NOT EXISTS trong SQL để tạo hóa đơn cha một cách an toàn.",
            "Chèn chi tiết lô hàng vào bảng con với các tham số Parameterized an toàn.",
            "Kích hoạt SQL Server Trigger tự động cộng dồn số lượng máy vào kho tblMayViTinh.",
            "Tự động cập nhật tổng tiền của hóa đơn nhập mà không cần câu lệnh UPDATE thủ công."
        ],
        "image_main": "assets/code_08_hoadonnhap_logic.png",
        "image_sub": None,
        "notes": "[21:15 - 22:30] Đoạn code lưu hóa đơn nhập áp dụng kỹ thuật kiểm tra Master thông minh. Khi người dùng thêm dòng máy đầu tiên, hệ thống dùng cú pháp IF NOT EXISTS để tự động sinh bản ghi trong tblHoaDonNhap nếu chưa có, sau đó chèn dòng chi tiết vào tblChiTietHoaDonNhap. Ngay lập tức, Trigger trên SQL Server sẽ kích hoạt để cộng số lượng tồn kho."
    },
    {
        "id": 19,
        "title": "Nghiệp Vụ Bán Hàng POS Tại Quầy (`frmHoaDonBan.cs` - UI)",
        "subtitle": "Giao diện điểm bán lẻ hiện đại, phục vụ thu ngân thanh toán tức thì",
        "tag": "BÁN HÀNG POS",
        "badges": ["Retail POS", "Real-time Subtotal", "Customer Lookup", "Speed Checkout"],
        "summary": "Giao diện bán hàng POS được thiết kế chuyên dụng cho thu ngân: tra cứu khách hàng nhanh, chọn máy tính, áp dụng chiết khấu và in hóa đơn.",
        "bullets": [
            "Tự động điền đơn giá bán hiện hành của máy tính ngay khi chọn trong ComboBox.",
            "Hỗ trợ nhập tỷ lệ giảm giá (%) và tự động tính toán thành tiền tức thì.",
            "Bảng giỏ hàng hiển thị chi tiết các sản phẩm khách mua kèm nút xóa từng dòng linh hoạt.",
            "Tự động chuyển đổi tổng số tiền thành chữ tiếng Việt chuẩn mực khi in hóa đơn."
        ],
        "image_main": "assets/ui_hoadonban.png",
        "image_sub": None,
        "notes": "[22:30 - 23:45] Màn hình POS frmHoaDonBan là nơi diễn ra các giao dịch bán lẻ hàng ngày. Khi nhân viên chọn mã máy, hệ thống tự động điền đơn giá bán hiện hành. Mọi thao tác gõ số lượng hay phần trăm chiết khấu đều kích hoạt sự kiện tính lại thành tiền ngay tức khắc, giúp việc thanh toán tại quầy diễn ra nhanh chóng và chính xác."
    },
    {
        "id": 20,
        "title": "Giải Phẫu Code `frmHoaDonBan.cs`: Thuật Toán Chống Bán Khống",
        "subtitle": "Kiểm tra số lượng tồn kho tức thì và bảo vệ tính nguyên vẹn giỏ hàng",
        "tag": "BẢO VỆ TỒN KHO POS",
        "badges": ["Anti-Overselling", "Real-time Stock Guard", "Input Validation", "Data Integrity"],
        "summary": "Thuật toán kiểm tra tồn kho tại chỗ ngăn chặn triệt để tình trạng nhân viên bán vượt quá số lượng máy thực tế đang có trong kho.",
        "bullets": [
            "Truy vấn trực tiếp số lượng tồn SoLuong từ tblMayViTinh bằng ExecuteScalar.",
            "So sánh số lượng khách yêu cầu với số lượng tồn thực tế trước khi thêm vào giỏ.",
            "Cảnh báo hộp thoại trực quan và đưa con trỏ chuột về ô số lượng nếu vượt tồn.",
            "Công thức tính thành tiền chuẩn xác: ThanhTien = SoLuong * DonGia * (1 - GiamGia/100)."
        ],
        "image_main": "assets/code_09_hoadonban_pos.png",
        "image_sub": None,
        "notes": "[23:45 - 25:00] Để chống hiện tượng bán âm hay bán khống máy tính, đoạn code trên slide thể hiện thuật toán Stock Guard. Trước khi thêm một dòng vào hóa đơn bán, hệ thống truy vấn số lượng tồn kho tức thời của máy. Nếu số lượng khách mua lớn hơn số lượng còn trong kho, hệ thống lập tức chặn lại và cảnh báo chính xác số lượng hiện có, bảo đảm không bao giờ xảy ra lỗi âm kho."
    },
    {
        "id": 21,
        "title": "Đồng Bộ Tự Động Giữa C# và SQL Server Triggers (ACID)",
        "subtitle": "Kỹ thuật đồng bộ tồn kho và tổng tiền tức thì không phụ thuộc tầng ứng dụng",
        "tag": "ACID TRIGGER PIPELINE",
        "badges": ["SQL Server Triggers", "ACID Guaranteed", "Auto Stock Decrement", "Consistency"],
        "summary": "Tận dụng sức mạnh của SQL Server Triggers để tự động khấu trừ tồn kho và cập nhật tổng tiền hóa đơn ngay trong phạm vi giao dịch ACID.",
        "bullets": [
            "Trigger trg_ChiTietHoaDonBan_Insert_Update khấu trừ kho dựa trên bảng inserted và deleted.",
            "Tự động tính lại TongTien trên bảng cha tblHoaDonBan bằng hàm SUM(ThanhTien).",
            "Đảm bảo tính toàn vẹn dữ liệu kể cả khi có nhiều nhân viên cùng thao tác đồng thời.",
            "Giảm tải xử lý tính toán cho tầng ứng dụng C# WinForms, tối ưu hóa băng thông mạng."
        ],
        "image_main": "assets/graph_06_pos_trigger_pipeline.png",
        "image_sub": "assets/code_10_sql_triggers.png",
        "notes": "[25:00 - 26:15] Đây là điểm sáng kỹ thuật quan trọng của đồ án. Thay vì viết các câu lệnh UPDATE tồn kho rải rác trong code C#, chúng em đưa toàn bộ logic này vào SQL Server Triggers. Khi có bản ghi mới được chèn vào tblChiTietHoaDonBan, Trigger sẽ tự động trừ số lượng trong tblMayViTinh và cập nhật lại TongTien của hóa đơn cha. Cơ chế này bảo đảm tính toàn vẹn dữ liệu ACID tuyệt đối."
    },
    {
        "id": 22,
        "title": "Hệ Thống Tra Cứu & Tìm Kiếm Đa Tiêu Chí (`frmTimKiem.cs` - UI)",
        "subtitle": "Tra cứu thông minh máy vi tính và hóa đơn theo nhiều tiêu chí linh hoạt",
        "tag": "TÌM KIẾM ĐA TIÊU CHÍ",
        "badges": ["Dynamic Search", "Multi-Filter", "Product & Invoice", "Instant Results"],
        "summary": "Màn hình tìm kiếm đa năng hỗ trợ chuyển đổi linh hoạt giữa tìm kiếm sản phẩm máy tính và tra cứu lịch sử hóa đơn bán/nhập hàng.",
        "bullets": [
            "Chế độ tìm máy tính: Lọc theo mã máy, tên máy, loại máy, hãng sản xuất và khoảng giá bán.",
            "Chế độ tìm hóa đơn: Lọc theo mã hóa đơn, mã nhân viên, khách hàng và khoảng thời gian.",
            "Giao diện tự động co giãn và hiển thị các trường nhập liệu phù hợp với chế độ tìm kiếm.",
            "Hiển thị kết quả tra cứu tức thì trên bảng dữ liệu với định dạng rõ ràng, chuyên nghiệp."
        ],
        "image_main": "assets/ui_timkiem_mode1.png",
        "image_sub": "assets/ui_timkiem_mode2.png",
        "notes": "[26:15 - 27:30] Form frmTimKiem cho phép người dùng tra cứu dữ liệu cực kỳ nhanh chóng. Người dùng có thể tìm kiếm máy tính theo tên, khoảng giá, hãng sản xuất hoặc chuyển sang chế độ tìm kiếm hóa đơn theo khoảng ngày và khách hàng. Giao diện tự động thích ứng để chỉ hiện những trường nhập liệu liên quan đến tiêu chí đang chọn."
    },
    {
        "id": 23,
        "title": "Giải Phẫu Kỹ Thuật Dynamic SQL Builder (`frmTimKiem.cs`)",
        "subtitle": "Kỹ thuật ghép chuỗi truy vấn động an toàn với mẫu hình `WHERE 1=1`",
        "tag": "DYNAMIC QUERY BUILDER",
        "badges": ["StringBuilder", "WHERE 1=1 Pattern", "Parameterized Filters", "Safe Search"],
        "summary": "Sử dụng StringBuilder kết hợp mẫu hình WHERE 1=1 để sinh câu lệnh SQL động theo các tiêu chí người dùng nhập vào, chống SQL Injection bằng SqlParameter.",
        "bullets": [
            "Mẫu hình WHERE 1=1 giúp các điều kiện AND phía sau được nối vào một cách tự nhiên.",
            "Chỉ bổ sung điều kiện lọc khi người dùng thực sự nhập giá trị vào trường tương ứng.",
            "Tất cả giá trị tìm kiếm đều được đóng gói vào danh sách List<SqlParameter> an toàn.",
            "Hỗ trợ tìm kiếm gần đúng với toán tử LIKE kết hợp ký tự đại diện %."
        ],
        "image_main": "assets/code_11_timkiem_dynamic_sql.png",
        "image_sub": None,
        "notes": "[27:30 - 28:45] Về mặt lập trình, việc ghép câu truy vấn tìm kiếm rất dễ sinh lỗi cú pháp nếu không cẩn thận. Chúng em áp dụng mẫu thiết kế WHERE 1=1 với StringBuilder. Nhờ có điều kiện 1=1 luôn đúng ban đầu, mọi tiêu chí như tên máy hay khoảng giá đều có thể nối thêm bằng chữ AND mà không cần kiểm tra vị trí đầu hay cuối. Tất cả tham số đều được truyền qua SqlParameter để bảo mật tuyệt đối."
    },
    {
        "id": 24,
        "title": "Hệ Thống BI Dashboard & 4 Thẻ KPI Summary Cards",
        "subtitle": "Báo cáo điều hành thông minh và bố cục động co giãn tự động",
        "tag": "BI DASHBOARD & KPI",
        "badges": ["Executive KPI", "Inventory Valuation", "Gross Profit", "Responsive Layout"],
        "summary": "Màn hình frmBaoCaoThongKe cung cấp cái nhìn toàn cảnh về sức khỏe tài chính của doanh nghiệp thông qua 4 thẻ chỉ số KPI tổng hợp tức thời.",
        "bullets": [
            "Card 1 - Giá trị Tồn Kho: Tính toán tổng vốn lưu động đang nằm trong kho hàng.",
            "Card 2 - Tổng Tiền Nhập Hàng: Theo dõi tổng chi phí mua hàng từ nhà cung cấp.",
            "Card 3 - Tổng Doanh Thu Bán: Tổng hợp dòng tiền bán hàng đã thực hiện.",
            "Card 4 - Lợi Nhuận Gộp Tức Thì: Tự động đổi màu Xanh/Đỏ theo kết quả kinh doanh lãi/lỗ."
        ],
        "image_main": "assets/ui_baocao_tonkho.png",
        "image_sub": "assets/code_12_baocao_responsive_cards.png",
        "notes": "[28:45 - 30:00] Chuyển sang phân hệ Báo cáo Thống kê tại frmBaoCaoThongKe, điểm nhấn lớn nhất là 4 thẻ KPI Summary Cards phía trên. Bất kỳ lúc nào mở form, hệ thống đều tính toán ngay lập tức 4 chỉ số: Tổng giá trị hàng tồn, Tổng tiền nhập, Tổng doanh thu và Lợi nhuận gộp. Thẻ lợi nhuận còn có thuật toán tự đổi màu chữ sang xanh khi có lãi hoặc màu đỏ cảnh báo khi lỗ."
    },
    {
        "id": 25,
        "title": "Báo Cáo Doanh Thu Theo Tháng, Quý, Năm (Yêu Cầu 6, 7, 8)",
        "subtitle": "Kỹ thuật tổng hợp dữ liệu giao dịch đa chu kỳ với các hàm thời gian SQL",
        "tag": "PHÂN TÍCH DOANH THU",
        "badges": ["Temporal Aggregation", "DATEPART(QUARTER)", "MONTH / YEAR", "INNER JOIN"],
        "summary": "Phân tích doanh thu và số lượng nhập/bán theo các chu kỳ kế toán linh hoạt (Tháng, Quý, Năm) phục vụ công tác lập kế hoạch kinh doanh.",
        "bullets": [
            "Sử dụng hàm DATEPART(QUARTER, NgayBan) để phân nhóm giao dịch theo từng quý trong năm.",
            "Kết nối bảng hóa đơn và chi tiết bằng INNER JOIN để thống kê chính xác số lượng từng máy.",
            "Khung lọc co giãn thông minh: Tự động đổi kích thước (62px sang 112px) khi chọn chế độ.",
            "Đáp ứng trọn vẹn các yêu cầu nghiệp vụ khắt khe của môn học và thực tế quản trị."
        ],
        "image_main": "assets/ui_baocao_nhaphang.png",
        "image_sub": "assets/ui_baocao_banhang.png",
        "notes": "[30:00 - 31:15] Màn hình thống kê đáp ứng đầy đủ các yêu cầu nghiệp vụ nâng cao về phân tích thời gian. Khi người dùng chọn thống kê theo Quý, câu lệnh SQL sẽ sử dụng hàm DATEPART(QUARTER, NgayBan) kết hợp với YEAR(NgayBan) để nhóm dữ liệu. Khung điều khiển lọc bên trên cũng tự động co giãn từ 62px lên 112px để hiển thị các ComboBox chọn tháng, quý, năm tương ứng."
    },
    {
        "id": 26,
        "title": "Thuật Toán Xếp Hạng Top 5 Nhân Viên (`SELECT TOP 5 WITH TIES`)",
        "subtitle": "Kỹ thuật xếp hạng công bằng trong đánh giá hiệu suất nhân sự (Yêu Cầu 9)",
        "tag": "THUẬT TOÁN XẾP HẠNG",
        "badges": ["TOP 5 WITH TIES", "Fair Evaluation", "HR Performance", "SQL Excellence"],
        "summary": "Áp dụng từ khóa chuyên sâu TOP 5 WITH TIES của SQL Server để giải quyết triệt để bài toán công bằng khi có các nhân viên đồng hạng doanh thu.",
        "bullets": [
            "Sự khác biệt lớn: SELECT TOP 5 thông thường sẽ cắt ngẫu nhiên nhân viên nếu bằng điểm.",
            "WITH TIES bảo đảm tất cả nhân viên có doanh thu ngang với vị trí thứ 5 đều được vinh danh.",
            "Nhóm dữ liệu theo MaNV, TenNV và tính tổng doanh thu bằng SUM(TongTien).",
            "Đóng góp giá trị thực tiễn to lớn cho chính sách thưởng KPI nhân sự tại các chuỗi bán lẻ."
        ],
        "image_main": "assets/ui_baocao_top5nv.png",
        "image_sub": "assets/code_13_baocao_top5_sql.png",
        "notes": "[31:15 - 32:30] Ở báo cáo Top 5 nhân viên xuất sắc, nhóm em đã áp dụng cú pháp SELECT TOP 5 WITH TIES trong SQL Server. Đây là một điểm rất hay: nếu chỉ dùng TOP 5 đơn thuần, trong trường hợp người thứ 5 và người thứ 6 có cùng mức doanh thu, hệ thống sẽ ngẫu nhiên loại bỏ một người. Với từ khóa WITH TIES, SQL Server giữ lại toàn bộ những người đồng hạng, bảo đảm tính công bằng tuyệt đối trong đánh giá thi đua."
    },
    {
        "id": 27,
        "title": "Engine Xuất Báo Cáo Excel Nội Sinh Tốc Độ Cao",
        "subtitle": "Giải pháp xuất file Excel Native HTML chuẩn Microsoft không phụ thuộc COM",
        "tag": "NATIVE EXCEL ENGINE",
        "badges": ["Zero-Dependency", "Native HTML-XLS", "mso-number-format", "Sub-second Export"],
        "summary": "Tự động biên dịch bảng dữ liệu thành tài liệu bảng tính chuẩn Microsoft Excel bằng định dạng HTML/XML nội sinh, loại bỏ hoàn toàn phụ thuộc vào Office Interop.",
        "bullets": [
            "Không yêu cầu máy tính cài đặt Microsoft Office hay bất kỳ thư viện trả phí nào.",
            "Tốc độ xuất siêu tốc: Xuất 10,000 dòng dữ liệu chỉ mất chưa tới 0.2 giây.",
            "Giữ nguyên định dạng tiền tệ chuyên nghiệp nhờ thẻ CSS mso-number-format:'#,##0'.",
            "Mở mượt mà trên tất cả các phần mềm văn phòng: MS Excel, Google Sheets, LibreOffice."
        ],
        "image_main": "assets/graph_07_bi_analytics_flow.png",
        "image_sub": "assets/code_14_baocao_excel_export.png",
        "notes": "[32:30 - 33:45] Một tính năng được hội đồng đánh giá rất cao là động cơ xuất Excel nội sinh trong hàm ExportToExcelNativeHtml. Chúng em không dùng Microsoft.Office.Interop.Excel vì thư viện này rất nặng, dễ gây treo tiến trình ngầm và bắt buộc máy khách phải cài Office. Thay vào đó, chúng em sinh cấu trúc HTML Spreadsheet chuẩn với XML Namespace của Microsoft. Tốc độ xuất gần như tức thì và các cột tiền tệ được định dạng dấu phẩy phân cách hàng nghìn chuẩn xác."
    },
    {
        "id": 28,
        "title": "Quản Trị Tài Khoản & Phân Quyền Vai Trò (`frmQuanLyNguoiDung.cs`)",
        "subtitle": "Mô hình bảo mật phân quyền Role-Based Access Control (Admin / Staff)",
        "tag": "QUẢN TRỊ BẢO MẬT",
        "badges": ["RBAC Security", "Account Lifecycle", "Role Assignment", "Audit Guard"],
        "summary": "Màn hình dành riêng cho Quản trị viên để quản lý toàn bộ tài khoản nhân viên, cấp phát quyền hạn và khóa tài khoản khi cần thiết.",
        "bullets": [
            "Phân quyền rõ rệt: Admin có toàn quyền hệ thống, Nhân viên bị giới hạn phân hệ quản trị.",
            "Tính năng khóa tài khoản tức thời: Ngăn chặn nhân viên đã nghỉ việc đăng nhập lại.",
            "Đặt lại mật khẩu mặc định an toàn cho người dùng khi được yêu cầu.",
            "Tất cả thay đổi quyền hạn đều được ghi vết vào nhật ký kiểm toán hệ thống."
        ],
        "image_main": "assets/ui_quanlynguoidung.png",
        "image_sub": "assets/code_15_audit_log_rbac.png",
        "notes": "[33:45 - 35:00] Form frmQuanLyNguoiDung là trung tâm quản trị bảo mật của Admin. Tại đây, người quản trị có thể phân quyền Admin hoặc Nhân viên, khóa tài khoản hoặc khôi phục quyền truy cập. Cơ chế phân quyền được thực thi ở cả hai cấp độ: kiểm tra quyền khi bấm menu trên giao diện và kiểm tra quyền tại từng hàm nghiệp vụ trong Functions.CheckPermission."
    },
    {
        "id": 29,
        "title": "Quản Lý Hồ Sơ Cá Nhân & Đổi Mật Khẩu (`frmHoSoCaNhan.cs`)",
        "subtitle": "Không gian tự phục vụ cho nhân viên cập nhật thông tin và bảo mật cá nhân",
        "tag": "HỒ SƠ CÁ NHÂN",
        "badges": ["Self-Service", "Profile Update", "Secure Password Change", "Clean UX"],
        "summary": "Cho phép mỗi nhân viên đang đăng nhập tự quản lý hồ sơ cá nhân, xem ca làm việc và chủ động đổi mật khẩu định kỳ an toàn.",
        "bullets": [
            "Hiển thị thông tin định danh: Mã nhân viên, Tên, Ngày sinh, Địa chỉ, Số điện thoại.",
            "Quy trình đổi mật khẩu an toàn: Xác thực mật khẩu cũ trước khi chấp nhận mật khẩu mới.",
            "Kiểm tra độ khớp giữa mật khẩu mới và ô xác nhận mật khẩu.",
            "Cập nhật thông tin vào CSDL kèm mã hóa băm mật khẩu mới tức thời."
        ],
        "image_main": "assets/ui_hosocanhan.png",
        "image_sub": None,
        "notes": "[35:00 - 36:15] Form frmHoSoCaNhan cung cấp cổng thông tin tự phục vụ cho từng cán bộ nhân viên. Nhân viên có thể kiểm tra lại thông tin cá nhân của mình và tự đổi mật khẩu định kỳ. Quy trình đổi mật khẩu bắt buộc phải nhập đúng mật khẩu hiện tại, giúp bảo đảm an toàn dữ liệu cá nhân ngay cả khi người dùng quên khóa máy tính."
    },
    {
        "id": 30,
        "title": "Hệ Thống Giám Sát & Nhật Ký Hoạt Động (`frmNhatKyHoatDong.cs`)",
        "subtitle": "Truy vết kiểm toán không phong tỏa (Non-blocking Enterprise Audit Trail)",
        "tag": "KIỂM TOÁN NỘI BỘ",
        "badges": ["Audit Trail", "Non-blocking", "Action Tracing", "Enterprise Compliance"],
        "summary": "Ghi nhận toàn bộ thao tác thêm, sửa, xóa, đăng nhập của người dùng vào bảng tblNhatKyHoatDong, phục vụ công tác thanh tra và kiểm soát rủi ro.",
        "bullets": [
            "Ghi lại chi tiết: Tên đăng nhập, thời gian chính xác, hành động và nội dung cụ thể.",
            "Cơ chế Non-blocking: Bọc trong khối try-catch không bao giờ làm gián đoạn luồng chính.",
            "Giao diện cho phép lọc nhật ký theo người dùng, ngày tháng hoặc loại hành động.",
            "Đáp ứng tiêu chuẩn kiểm toán hệ thống phần mềm doanh nghiệp hiện đại."
        ],
        "image_main": "assets/ui_nhatkyhoatdong.png",
        "image_sub": None,
        "notes": "[36:15 - 37:30] Để bảo đảm tính minh bạch và phục vụ kiểm toán nội bộ, frmNhatKyHoatDong hiển thị toàn bộ lịch sử thao tác của các tài khoản. Mọi hành vi như đăng nhập, lập đơn bán, xóa máy tính đều được hàm Functions.GhiNhatKy tự động ghi lại kèm mốc thời gian GETDATE(). Hàm này được thiết kế theo nguyên tắc Non-blocking, bảo đảm việc ghi log không bao giờ gây ảnh hưởng đến tốc độ bán hàng."
    },
    {
        "id": 31,
        "title": "Trung Tâm Tùy Biến & Cấu Hình Hệ Thống (`frmCaiDat.cs`)",
        "subtitle": "Quản lý tham số vận hành, thông tin thương hiệu hóa đơn và kết nối CSDL",
        "tag": "CẤU HÌNH HỆ THỐNG",
        "badges": ["System Config", "Store Branding", "Connection String", "Theme Default"],
        "summary": "Cho phép tùy biến thông tin doanh nghiệp (Tên cửa hàng, Địa chỉ, Hotline in trên hóa đơn), cấu hình chuỗi kết nối SQL và cài đặt Theme mặc định.",
        "bullets": [
            "Tùy biến thông tin cửa hàng hiển thị trên tiêu đề phiếu nhập và hóa đơn bán lẻ.",
            "Lựa chọn giao diện khởi động mặc định (Sáng hoặc Tối) theo sở thích người dùng.",
            "Cấu hình tham số máy chủ cơ sở dữ liệu linh hoạt, thuận tiện khi triển khai qua mạng LAN.",
            "Lưu trữ cấu hình bền vững vào file cấu hình ứng dụng hoặc bảng hệ thống."
        ],
        "image_main": "assets/ui_caidat.png",
        "image_sub": None,
        "notes": "[37:30 - 38:45] Cuối cùng trong nhóm quản trị là frmCaiDat. Màn hình này cho phép ban quản lý thay đổi các thông tin hiển thị trên đầu hóa đơn thanh toán như tên cửa hàng, địa chỉ, số hotline mà không cần biên dịch lại mã nguồn. Đây cũng là nơi cấu hình giao diện mặc định khi khởi động ứng dụng."
    },
    {
        "id": 32,
        "title": "Tổng Kết Đồ Án, Hướng Phát Triển & Bộ 12 Câu Hỏi Đáp Q&A",
        "subtitle": "Khẳng định giá trị thực tiễn của đồ án và sẵn sàng phản biện trước Hội đồng",
        "tag": "TỔNG KẾT & PHẢN BIỆN",
        "badges": ["Thesis Defense", "Enterprise Ready", "Future Roadmap", "12 Q&A Ready"],
        "summary": "Đồ án hoàn thành 100% mục tiêu: kiến trúc 3 tầng chuẩn mực, giao diện hiện đại Dual-theme, đồng bộ ACID bằng Trigger, quản lý trọn vẹn nghiệp vụ bán máy vi tính.",
        "bullets": [
            "Đã kiểm chứng trọn vẹn 14 Forms giao diện và toàn bộ luồng nghiệp vụ thực tế.",
            "Định hướng tương lai: Tích hợp Barcode/QR Code quét linh kiện và AI dự báo tồn kho.",
            "Tích hợp kết nối Hóa đơn điện tử theo chuẩn thông tư mới của Tổng cục Thuế.",
            "Sẵn sàng bước vào phần Vấn đáp Phản biện với Bộ 12 câu hỏi chuyên sâu của Hội đồng."
        ],
        "image_main": "assets/graph_01_overview_3tier.png",
        "image_sub": "assets/graph_06_pos_trigger_pipeline.png",
        "notes": "[38:45 - 40:00] Kính thưa Hội đồng phản biện, đồ án 'Hệ Thống Quản Lý Bán Máy Vi Tính' đã hoàn thành trọn vẹn 100% mục tiêu đề ra. Hệ thống không chỉ đáp ứng chuẩn mực về mặt học thuật với kiến trúc 3 tầng và cơ sở dữ liệu 3NF, mà còn sẵn sàng triển khai áp dụng vào thực tế doanh nghiệp. Nhóm chúng em đã sẵn sàng đón nhận những câu hỏi đóng góp quý báu và bước vào phần vấn đáp phản biện. Xin chân thành cảm ơn quý Thầy Cô!"
    }
]

# 12 Defense Q&A Data
QA_DATA = [
    {
        "q": "Câu 1: Tại sao nhóm lại lựa chọn Pure ADO.NET thay vì Entity Framework hay Dapper?",
        "a": "Trả lời: Đối với phần mềm bán hàng POS và quản lý kho máy tính, tốc độ phản hồi và sự kiểm soát tài nguyên là ưu tiên số một. Pure ADO.NET cho phép kiểm soát 100% vòng đời của SqlConnection, tận dụng SqlDataReader đọc luồng dữ liệu trực tiếp và SqlDataAdapter nạp DataTable cực nhanh mà không phải gánh chịu chi phí ánh xạ đối tượng (Object Mapping Overhead) của ORM. Ngoài ra, việc dùng ADO.NET thuần giúp sinh viên nắm vững bản chất tầng kết nối dữ liệu trong .NET."
    },
    {
        "q": "Câu 2: Làm thế nào hệ thống đảm bảo chống tấn công SQL Injection khi sử dụng ADO.NET thuần?",
        "a": "Trả lời: 100% các câu lệnh truy vấn có truyền dữ liệu từ giao diện (như tìm kiếm, thêm, sửa, xóa, xác thực) trong Functions.cs và các Form đều bắt buộc sử dụng mảng SqlParameter[]. Tham số được SQL Server phân tích ngữ pháp riêng biệt với câu lệnh SQL, do đó mọi ký tự đặc biệt như dấu nháy đơn hay chuỗi mã độc đều bị vô hiệu hóa và coi như một giá trị chuỗi thuần túy."
    },
    {
        "q": "Câu 3: Cơ sở dữ liệu của đồ án đã đạt chuẩn mấy? Hãy chứng minh tính chuẩn hóa 3NF?",
        "a": "Trả lời: Cơ sở dữ liệu của đồ án đạt Chuẩn hóa bậc 3 (3NF). Đạt 1NF vì tất cả các thuộc tính đều là nguyên tố (nguyên tử) và có khóa chính xác định. Đạt 2NF vì không có thuộc tính không khóa nào phụ thuộc một phần vào khóa chính phức hợp (ví dụ bảng tblChiTietHoaDonBan có khóa chính MaHDB + MaMay, các thuộc tính SoLuong, DonGia phụ thuộc toàn bộ vào cặp khóa này). Đạt 3NF vì không có phụ thuộc bắc cầu giữa các thuộc tính không khóa: các thông tin chi tiết về linh kiện máy tính như tên loại máy, tên hãng sản xuất được tách riêng thành các bảng tblLoaiMay, tblHangSanXuat liên kết qua khóa ngoại."
    },
    {
        "q": "Câu 4: Khi hai nhân viên cùng bán một chiếc máy tính cuối cùng trong kho cùng một lúc, hệ thống xử lý tranh chấp như thế nào?",
        "a": "Trả lời: Hệ thống có 2 lớp bảo vệ. Lớp 1 ở tầng ứng dụng C#: hàm kiểm tra tồn kho so sánh số lượng yêu cầu với số lượng hiện có trước khi cho bấm thanh toán. Lớp 2 ở tầng cơ sở dữ liệu: Trigger trg_ChiTietHoaDonBan chạy trong một giao dịch ngầm của SQL Server với mức cô lập (Isolation Level) mặc định. Khi câu lệnh UPDATE tblMayViTinh thực thi, bản ghi của máy tính sẽ bị khóa độc quyền (Exclusive Lock). Giao dịch của nhân viên thứ hai sẽ phải đợi, và nếu sau giao dịch thứ nhất số lượng tồn về 0, giao dịch thứ hai sẽ bị chặn hoặc báo lỗi, bảo đảm không bao giờ xảy ra âm kho."
    },
    {
        "q": "Câu 5: Tại sao nhóm lại đưa việc trừ tồn kho và tính tổng tiền vào SQL Server Trigger thay vì viết hoàn toàn bằng code C#?",
        "a": "Trả lời: Việc đưa vào Trigger đem lại 3 lợi thế vượt trội: 1. Tính nguyên tử (Atomicity): Thao tác thêm chi tiết hóa đơn và trừ kho diễn ra trong cùng một transaction của CSDL, nếu trừ kho thất bại thì hóa đơn cũng không được lưu. 2. Độc lập tầng ứng dụng: Dù sau này có mở rộng viết thêm ứng dụng Web hay Mobile kết nối vào cùng CSDL SQL Server, quy tắc nghiệp vụ trừ kho và cộng tổng tiền vẫn được thực thi tự động mà không cần viết lại mã nguồn. 3. Tối ưu hiệu năng: Giảm bớt số lượng request gửi qua mạng giữa máy trạm WinForms và SQL Server."
    },
    {
        "q": "Câu 6: Hãy giải thích thuật toán và ưu điểm của kỹ thuật chuyển đổi Theme (Dark/Light Mode) trong UIThemeHelper.cs?",
        "a": "Trả lời: Thuật toán dựa trên cơ chế duyệt đệ quy cây điều khiển (Recursive Tree Traversal). Hàm ApplyTheme(Control root) kiểm tra kiểu cụ thể của từng Control và áp dụng màu sắc tương ứng từ Palette. Nếu Control đó có chứa các Control con (c.HasChildren == true) như Panel, GroupBox, TabControl, hàm sẽ tự gọi lại chính nó. Ưu điểm là cơ chế này độc lập hoàn toàn với việc người dùng thiết kế giao diện như thế nào, tự động nhận diện và đổi màu cho 100% controls trên Form mà không cần phải gán màu thủ công cho từng control trong Designer."
    },
    {
        "q": "Câu 7: Kỹ thuật nhúng Form động (Form Embedding) trên frmMain có ưu điểm gì so với việc dùng MDI Parent truyền thống?",
        "a": "Trả lời: Giao diện MDI cổ điển của Windows Forms có nhược điểm lớn là các cửa sổ con trôi nổi bên trong, gây khó chịu cho người dùng, dễ bị thu nhỏ lạc mất cửa sổ và có hiện tượng nhấp nháy (flickering) khi phóng to thu nhỏ. Bằng cách thiết lập childForm.TopLevel = false và Dock = Fill vào một Panel trung tâm, hệ thống biến thành một ứng dụng Single-Window phẳng hiện đại. Đồng thời việc chủ động gọi Dispose() form cũ trước khi nạp form mới giúp ứng dụng kiểm soát chặt chẽ bộ nhớ RAM, tránh rò rỉ tài nguyên."
    },
    {
        "q": "Câu 8: Tại sao nhóm lại tự viết động cơ xuất Excel nội sinh dạng HTML Spreadsheet thay vì dùng Microsoft.Office.Interop.Excel?",
        "a": "Trả lời: Thư viện Microsoft.Office.Interop.Excel có các nhược điểm nghiêm trọng trong môi trường doanh nghiệp: 1. Bắt buộc máy trạm phải cài đặt Microsoft Office bản quyền. 2. Tiến trình EXCEL.EXE chạy ngầm rất dễ bị treo (orphan process) nếu không giải phóng COM Object đúng cách, làm ngốn RAM máy trạm. 3. Tốc độ rất chậm vì mỗi lần ghi một ô dữ liệu phải gọi qua ranh giới COM. Bằng cách sinh trực tiếp mã HTML với XML Namespace của Microsoft Excel, tốc độ xuất 10.000 dòng mất chưa tới 0.2 giây, mở được trên mọi phần mềm bảng tính và hoàn toàn không phụ thuộc bên ngoài."
    },
    {
        "q": "Câu 9: Từ khóa `TOP 5 WITH TIES` trong báo cáo thống kê nhân viên xuất sắc có ý nghĩa gì đặc biệt?",
        "a": "Trả lời: Trong bài toán khen thưởng nhân sự, nếu chỉ dùng `SELECT TOP 5` thông thường theo thứ tự giảm dần của doanh thu, trong trường hợp người thứ 5 và người thứ 6 (hoặc thứ 7) có cùng mức doanh thu bằng nhau, SQL Server sẽ tự ý cắt lấy một người ngẫu nhiên và loại bỏ người còn lại. Việc sử dụng `WITH TIES` (đi kèm ORDER BY) chỉ định rằng nếu có nhiều bản ghi đồng hạng với bản ghi cuối cùng của tập TOP, SQL Server sẽ giữ lại tất cả các bản ghi đó. Điều này bảo đảm tính công bằng tuyệt đối trong đánh giá thi đua."
    },
    {
        "q": "Câu 10: Thiết kế đa hình 5-trong-1 trên `frmDanhMucHeThong.cs` đem lại lợi ích gì cho hệ thống?",
        "a": "Trả lời: 5 danh mục Loại máy, Hãng sản xuất, Nhà cung cấp, Khách hàng, Nhân viên đều có chung một mô hình thao tác là nạp danh sách, thêm, sửa, xóa, lưu, bỏ qua. Nếu tạo 5 form riêng biệt, dự án sẽ bị dư thừa tới 80% mã nguồn tương tự nhau, tốn dung lượng file .exe và khó bảo trì khi cần thay đổi style giao diện. Với kiến trúc đa hình gom vào 1 form, hệ thống chỉ cần thay đổi cấu trúc cột của DataGridView và chuỗi câu lệnh SQL tương ứng khi bấm nút, giúp code tinh gọn tối đa và tiết kiệm bộ nhớ."
    },
    {
        "q": "Câu 11: Mật khẩu người dùng trong hệ thống được bảo mật như thế nào? Có lưu dưới dạng văn bản thuần không?",
        "a": "Trả lời: Mật khẩu người dùng tuyệt đối KHÔNG lưu trữ dưới dạng văn bản thuần (plain-text) trong CSDL. Hệ thống sử dụng cơ chế băm mật khẩu bảo mật (Salted Hash / BCrypt). Khi người dùng tạo tài khoản hoặc đổi mật khẩu, chuỗi mật khẩu được băm cùng chuỗi muối ngẫu nhiên trước khi lưu vào cột MatKhau của tblNguoiDung. Khi đăng nhập, hệ thống lấy mã băm trong CSDL ra và dùng hàm VerifyPassword để đối chiếu. Kể cả quản trị viên có mở trực tiếp bảng CSDL cũng không thể biết được mật khẩu gốc của người dùng."
    },
    {
        "q": "Câu 12: Nếu được tiếp tục phát triển đồ án này trong tương lai, nhóm sẽ bổ sung những tính năng công nghệ nào?",
        "a": "Trả lời: Nhóm có 3 định hướng nâng cấp chiến lược: 1. Tích hợp quét mã vạch Barcode/QR Code cho từng linh kiện máy tính khi nhập kho và bán lẻ, rút ngắn thời gian lập đơn xuống dưới 5 giây. 2. Kết nối API Hóa đơn điện tử với các nhà cung cấp như VNPT hay Viettel để phát hành hóa đơn có mã của cơ quan thuế tức thì. 3. Xây dựng mô-đun AI/Machine Learning dự báo nhu cầu nhập hàng và cảnh báo tồn kho dựa trên chu kỳ mua sắm thực tế của khách hàng trong quá khứ."
    }
]

def generate_html_presentation():
    print("Generating Presentation/Slide_Thuyet_Trinh_32_Slides_Full.html...")
    
    html_parts = []
    html_parts.append("""<!DOCTYPE html>
<html lang="vi">
<head>
<meta charset="UTF-8">
<meta name="viewport" content="width=device-width, initial-scale=1.0">
<title>Hệ Thống Quản Lý Bán Máy Vi Tính - Báo Cáo 32 Slides Chuẩn Mực</title>
<link rel="preconnect" href="https://fonts.googleapis.com">
<link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
<link href="https://fonts.googleapis.com/css2?family=Plus+Jakarta+Sans:wght@300;400;500;600;700;800&family=JetBrains+Mono:wght@400;500;700&display=swap" rel="stylesheet">
<style>
:root {
  --bg-primary: #0b0f19;
  --bg-card: rgba(18, 26, 43, 0.88);
  --border-color: rgba(99, 102, 241, 0.25);
  --border-focus: #6366f1;
  --accent-cyan: #38bdf8;
  --accent-indigo: #818cf8;
  --accent-emerald: #34d399;
  --accent-amber: #fbbf24;
  --accent-rose: #fb7185;
  --text-main: #f8fafc;
  --text-muted: #94a3b8;
  --font-main: 'Plus Jakarta Sans', system-ui, -apple-system, sans-serif;
  --font-code: 'JetBrains Mono', monospace;
}
* { box-sizing: border-box; margin: 0; padding: 0; }
body {
  background-color: var(--bg-primary);
  color: var(--text-main);
  font-family: var(--font-main);
  overflow: hidden;
  height: 100vh;
  width: 100vw;
  user-select: none;
}
.bg-mesh {
  position: fixed;
  inset: 0;
  background: 
    radial-gradient(circle at 15% 15%, rgba(99, 102, 241, 0.16) 0%, transparent 40%),
    radial-gradient(circle at 85% 85%, rgba(14, 165, 233, 0.14) 0%, transparent 40%),
    radial-gradient(circle at 50% 50%, rgba(16, 185, 129, 0.08) 0%, transparent 50%);
  pointer-events: none;
  z-index: 0;
}
#presentation-deck {
  position: relative;
  width: 100vw;
  height: 100vh;
  display: flex;
  align-items: center;
  justify-content: center;
  z-index: 1;
}
.slide {
  position: absolute;
  width: 95vw;
  height: 90vh;
  max-width: 1720px;
  max-height: 980px;
  background: var(--bg-card);
  backdrop-filter: blur(20px);
  border: 1px solid var(--border-color);
  border-radius: 20px;
  box-shadow: 0 25px 50px -12px rgba(0, 0, 0, 0.6), 0 0 40px rgba(99, 102, 241, 0.15);
  display: none;
  flex-direction: column;
  padding: 36px 48px;
  opacity: 0;
  transform: scale(0.97);
  transition: all 0.35s cubic-bezier(0.16, 1, 0.3, 1);
}
.slide.active {
  display: flex;
  opacity: 1;
  transform: scale(1);
}
.slide-header {
  display: flex;
  justify-content: space-between;
  align-items: flex-start;
  margin-bottom: 24px;
  border-bottom: 1px solid rgba(255, 255, 255, 0.08);
  padding-bottom: 18px;
}
.slide-title-wrap {
  display: flex;
  flex-direction: column;
  gap: 6px;
}
.slide-tag {
  font-size: 0.8rem;
  font-weight: 700;
  text-transform: uppercase;
  letter-spacing: 2px;
  color: var(--accent-cyan);
  display: flex;
  align-items: center;
  gap: 8px;
}
.slide-tag::before {
  content: '';
  display: inline-block;
  width: 8px;
  height: 8px;
  border-radius: 50%;
  background: var(--accent-cyan);
  box-shadow: 0 0 10px var(--accent-cyan);
}
.slide-title {
  font-size: 2.1rem;
  font-weight: 800;
  letter-spacing: -0.5px;
  background: linear-gradient(135deg, #ffffff 40%, #94a3b8 100%);
  -webkit-background-clip: text;
  -webkit-text-fill-color: transparent;
}
.slide-subtitle {
  font-size: 1rem;
  color: var(--text-muted);
  font-weight: 400;
}
.slide-badges {
  display: flex;
  gap: 8px;
  align-items: center;
}
.badge {
  padding: 6px 14px;
  border-radius: 20px;
  font-size: 0.8rem;
  font-weight: 600;
  background: rgba(99, 102, 241, 0.15);
  border: 1px solid rgba(99, 102, 241, 0.4);
  color: var(--accent-indigo);
  letter-spacing: 0.5px;
}
.slide-body {
  flex: 1;
  display: grid;
  grid-template-columns: 1fr 1.15fr;
  gap: 36px;
  overflow: hidden;
}
.slide-body.full-layout {
  grid-template-columns: 1fr;
}
.slide-left {
  display: flex;
  flex-direction: column;
  gap: 20px;
}
.summary-box {
  background: rgba(30, 41, 59, 0.6);
  border-left: 4px solid var(--accent-cyan);
  padding: 16px 20px;
  border-radius: 0 12px 12px 0;
  font-size: 1.05rem;
  line-height: 1.6;
  color: #e2e8f0;
}
.bullet-list {
  display: flex;
  flex-direction: column;
  gap: 14px;
  list-style: none;
}
.bullet-item {
  display: flex;
  align-items: flex-start;
  gap: 12px;
  font-size: 0.98rem;
  color: #cbd5e1;
  line-height: 1.5;
}
.bullet-dot {
  width: 8px;
  height: 8px;
  border-radius: 50%;
  background: var(--accent-emerald);
  margin-top: 7px;
  flex-shrink: 0;
  box-shadow: 0 0 8px var(--accent-emerald);
}
.slide-right {
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  gap: 16px;
  height: 100%;
}
.media-container {
  width: 100%;
  height: 100%;
  max-height: 580px;
  background: rgba(15, 23, 42, 0.9);
  border: 1px solid rgba(255, 255, 255, 0.1);
  border-radius: 14px;
  overflow: hidden;
  display: flex;
  align-items: center;
  justify-content: center;
  cursor: zoom-in;
  position: relative;
  transition: transform 0.25s ease, border-color 0.25s ease;
}
.media-container:hover {
  transform: translateY(-2px);
  border-color: var(--accent-cyan);
}
.media-container img {
  max-width: 100%;
  max-height: 100%;
  object-fit: contain;
}
.dual-media {
  display: grid;
  grid-template-columns: 1fr 1fr;
  gap: 16px;
  width: 100%;
  height: 100%;
}
.slide-footer {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-top: 18px;
  padding-top: 14px;
  border-top: 1px solid rgba(255, 255, 255, 0.08);
  font-size: 0.85rem;
  color: var(--text-muted);
}
.nav-controls {
  position: fixed;
  bottom: 24px;
  right: 36px;
  display: flex;
  gap: 10px;
  z-index: 100;
}
.nav-btn {
  background: rgba(30, 41, 59, 0.8);
  border: 1px solid rgba(255, 255, 255, 0.15);
  color: var(--text-main);
  padding: 10px 18px;
  border-radius: 10px;
  font-family: var(--font-main);
  font-weight: 600;
  cursor: pointer;
  backdrop-filter: blur(10px);
  transition: all 0.2s ease;
  display: flex;
  align-items: center;
  gap: 6px;
}
.nav-btn:hover {
  background: var(--accent-indigo);
  color: white;
  border-color: transparent;
  box-shadow: 0 0 15px rgba(99, 102, 241, 0.5);
}
.progress-bar-container {
  position: fixed;
  top: 0;
  left: 0;
  width: 100vw;
  height: 4px;
  background: rgba(255, 255, 255, 0.08);
  z-index: 1000;
}
.progress-bar-fill {
  height: 100%;
  width: 3.125%;
  background: linear-gradient(90deg, var(--accent-cyan), var(--accent-indigo));
  box-shadow: 0 0 10px var(--accent-cyan);
  transition: width 0.3s ease;
}
#speaker-drawer {
  position: fixed;
  bottom: 0;
  left: 0;
  width: 100vw;
  background: rgba(11, 15, 25, 0.95);
  border-top: 2px solid var(--accent-indigo);
  padding: 24px 40px;
  z-index: 200;
  transform: translateY(100%);
  transition: transform 0.3s cubic-bezier(0.16, 1, 0.3, 1);
  backdrop-filter: blur(20px);
  max-height: 40vh;
  overflow-y: auto;
}
#speaker-drawer.open {
  transform: translateY(0);
}
.drawer-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 12px;
}
.drawer-title {
  font-size: 0.9rem;
  font-weight: 700;
  color: var(--accent-amber);
  text-transform: uppercase;
  letter-spacing: 1.5px;
}
.drawer-content {
  font-size: 1.05rem;
  line-height: 1.7;
  color: #e2e8f0;
}
#modal-zoom {
  position: fixed;
  inset: 0;
  background: rgba(0, 0, 0, 0.85);
  backdrop-filter: blur(15px);
  z-index: 1000;
  display: none;
  align-items: center;
  justify-content: center;
  cursor: zoom-out;
}
#modal-zoom img {
  max-width: 92vw;
  max-height: 92vh;
  border-radius: 12px;
  border: 1px solid rgba(255, 255, 255, 0.2);
  box-shadow: 0 0 50px rgba(0, 0, 0, 0.8);
}
</style>
</head>
<body>
<div class="bg-mesh"></div>
<div class="progress-bar-container">
  <div class="progress-bar-fill" id="progress-bar"></div>
</div>

<main id="presentation-deck">
""")

    for s in SLIDES_DATA:
        s_id = s["id"]
        title = s["title"]
        subtitle = s["subtitle"]
        tag = s["tag"]
        badges_html = "".join([f'<span class="badge">{b}</span>' for b in s["badges"]])
        bullets_html = "".join([f'<li class="bullet-item"><span class="bullet-dot"></span><span>{b}</span></li>' for b in s["bullets"]])
        
        media_html = ""
        if s["image_main"] and s["image_sub"]:
            media_html = f"""
            <div class="dual-media">
              <div class="media-container" onclick="openZoom('{s['image_main']}')">
                <img src="{s['image_main']}" alt="{title} 1">
              </div>
              <div class="media-container" onclick="openZoom('{s['image_sub']}')">
                <img src="{s['image_sub']}" alt="{title} 2">
              </div>
            </div>
            """
        elif s["image_main"]:
            media_html = f"""
            <div class="media-container" onclick="openZoom('{s['image_main']}')">
              <img src="{s['image_main']}" alt="{title}">
            </div>
            """
            
        active_cls = " active" if s_id == 1 else ""
        
        slide_block = f"""
  <!-- SLIDE {s_id:02d}: {title} -->
  <section class="slide{active_cls}" id="slide-{s_id}" data-slide="{s_id}" data-notes="{s['notes']}">
    <div class="slide-header">
      <div class="slide-title-wrap">
        <div class="slide-tag">{tag}</div>
        <h2 class="slide-title">{title}</h2>
        <p class="slide-subtitle">{subtitle}</p>
      </div>
      <div class="slide-badges">
        {badges_html}
      </div>
    </div>
    
    <div class="slide-body">
      <div class="slide-left">
        <div class="summary-box">
          {s['summary']}
        </div>
        <ul class="bullet-list">
          {bullets_html}
        </ul>
      </div>
      <div class="slide-right">
        {media_html}
      </div>
    </div>
    
    <div class="slide-footer">
      <span>Đồ Án Lập Trình Trực Quan C# .NET — QLBanMayViTinh</span>
      <span style="font-family: var(--font-code); font-weight: 600; color: var(--accent-cyan);">Slide {s_id:02d} / 32</span>
    </div>
  </section>
"""
        html_parts.append(slide_block)

    html_parts.append("""
</main>

<div class="nav-controls">
  <button class="nav-btn" onclick="prevSlide()">◀ Trước</button>
  <button class="nav-btn" onclick="toggleDrawer()">📝 Notes (S)</button>
  <button class="nav-btn" onclick="toggleFullScreen()">⛶ Fullscreen (F)</button>
  <button class="nav-btn" onclick="nextSlide()">Tiếp ▶</button>
</div>

<div id="speaker-drawer">
  <div class="drawer-header">
    <span class="drawer-title">KỊCH BẢN THUYẾT TRÌNH (SPEAKER NOTES)</span>
    <span style="color: var(--text-muted); font-size: 0.85rem;">Phím tắt [S] đóng/mở</span>
  </div>
  <div class="drawer-content" id="notes-content"></div>
</div>

<div id="modal-zoom" onclick="closeZoom()">
  <img id="zoom-img" src="" alt="Enlarged View">
</div>

<script>
let currentSlide = 1;
const totalSlides = 32;

function showSlide(n) {
  if (n < 1) n = 1;
  if (n > totalSlides) n = totalSlides;
  currentSlide = n;
  
  document.querySelectorAll('.slide').forEach(s => s.classList.remove('active'));
  const activeSlide = document.getElementById('slide-' + currentSlide);
  if (activeSlide) {
    activeSlide.classList.add('active');
    const notes = activeSlide.getAttribute('data-notes') || 'Không có ghi chú cho slide này.';
    document.getElementById('notes-content').innerText = notes;
  }
  
  const pct = (currentSlide / totalSlides) * 100;
  document.getElementById('progress-bar').style.width = pct + '%';
}

function nextSlide() {
  if (currentSlide < totalSlides) showSlide(currentSlide + 1);
}

function prevSlide() {
  if (currentSlide > 1) showSlide(currentSlide - 1);
}

function toggleDrawer() {
  document.getElementById('speaker-drawer').classList.toggle('open');
}

function toggleFullScreen() {
  if (!document.fullscreenElement) {
    document.documentElement.requestFullscreen().catch(() => {});
  } else {
    if (document.exitFullscreen) document.exitFullscreen();
  }
}

function openZoom(src) {
  document.getElementById('zoom-img').src = src;
  document.getElementById('modal-zoom').style.display = 'flex';
}

function closeZoom() {
  document.getElementById('modal-zoom').style.display = 'none';
}

document.addEventListener('keydown', (e) => {
  if (e.key === 'ArrowRight' || e.key === ' ' || e.key === 'PageDown') {
    nextSlide();
  } else if (e.key === 'ArrowLeft' || e.key === 'PageUp') {
    prevSlide();
  } else if (e.key === 'f' || e.key === 'F') {
    toggleFullScreen();
  } else if (e.key === 's' || e.key === 'S') {
    toggleDrawer();
  } else if (e.key === 'Escape') {
    closeZoom();
  }
});

// Initialize on load
showSlide(1);
</script>
</body>
</html>
""")

    out_file = os.path.join(OUT_DIR, "Slide_Thuyet_Trinh_32_Slides_Full.html")
    with open(out_file, "w", encoding="utf-8") as f:
        f.write("".join(html_parts))
    print(f"Generated Web Slide Deck successfully: {out_file} (32 Slides)")

def generate_markdown_script():
    print("Generating Presentation/Kich_Ban_Thuyet_Trinh_32_Slides.md...")
    md_lines = []
    md_lines.append("# KỊCH BẢN THUYẾT TRÌNH BẢO VỆ ĐỒ ÁN (32 SLIDES CHI TIẾT)")
    md_lines.append("## ĐỀ TÀI: HỆ THỐNG QUẢN LÝ BÁN MÁY VI TÍNH (QLBanMayViTinh)")
    md_lines.append("**Nền tảng:** C# .NET WinForms & SQL Server (Pure ADO.NET, 3NF, GDI+ Dual-Theme)\n")
    md_lines.append("---\n")
    
    for s in SLIDES_DATA:
        s_id = s["id"]
        title = s["title"]
        subtitle = s["subtitle"]
        tag = s["tag"]
        notes = s["notes"]
        summary = s["summary"]
        bullets = s["bullets"]
        
        md_lines.append(f"### SLIDE {s_id:02d}: {title}")
        md_lines.append(f"- **Chủ đề / Phân hệ:** {tag} — *{subtitle}*")
        md_lines.append(f"- **Tài nguyên hình ảnh minh họa:** `{s['image_main']}`" + (f", `{s['image_sub']}`" if s['image_sub'] else ""))
        md_lines.append(f"- **Nội dung kỹ thuật cốt lõi:**")
        md_lines.append(f"  > {summary}")
        for b in bullets:
            md_lines.append(f"  + {b}")
        md_lines.append(f"- **LỜI NÓI THUYẾT TRÌNH (SPEAKER SCRIPT):**")
        md_lines.append(f"  > \"{notes}\"")
        md_lines.append("\n---\n")

    md_lines.append("## BỘ 12 CÂU HỎI VÀ ĐÁP ÁN BẢO VỆ ĐỒ ÁN TRƯỚC HỘI ĐỒNG (Q&A)\n")
    for qa in QA_DATA:
        md_lines.append(f"#### {qa['q']}")
        md_lines.append(f"{qa['a']}\n")

    out_file = os.path.join(OUT_DIR, "Kich_Ban_Thuyet_Trinh_32_Slides.md")
    with open(out_file, "w", encoding="utf-8") as f:
        f.write("\n".join(md_lines))
    print(f"Generated Master Speaking Script successfully: {out_file} (32 Slides + 12 Q&A)")

def generate_pptx_deck():
    print("Generating Presentation/QLBanMayViTinh_32_Slides.pptx...")
    prs = Presentation()
    prs.slide_width = Inches(13.333)
    prs.slide_height = Inches(7.5)
    blank_layout = prs.slide_layouts[6]

    for s in SLIDES_DATA:
        slide = prs.slides.add_slide(blank_layout)
        
        # Add background dark rectangle
        bg = slide.shapes.add_shape(1, 0, 0, Inches(13.333), Inches(7.5))
        bg.fill.solid()
        bg.fill.fore_color.rgb = RGBColor(11, 15, 25)
        bg.line.fill.background()

        # Header Box
        header_box = slide.shapes.add_textbox(Inches(0.8), Inches(0.4), Inches(11.7), Inches(1.3))
        tf = header_box.text_frame
        tf.word_wrap = True
        
        # Tag
        p_tag = tf.paragraphs[0]
        p_tag.text = f"SLIDE {s['id']:02d} | {s['tag']}"
        p_tag.font.size = Pt(11)
        p_tag.font.bold = True
        p_tag.font.color.rgb = RGBColor(56, 189, 248)
        
        # Title
        p_title = tf.add_paragraph()
        p_title.text = s["title"]
        p_title.font.size = Pt(22)
        p_title.font.bold = True
        p_title.font.color.rgb = RGBColor(248, 250, 252)

        # Subtitle
        p_sub = tf.add_paragraph()
        p_sub.text = s["subtitle"]
        p_sub.font.size = Pt(13)
        p_sub.font.color.rgb = RGBColor(148, 163, 184)

        # Content Box Left
        content_box = slide.shapes.add_textbox(Inches(0.8), Inches(1.9), Inches(5.8), Inches(4.8))
        tf_c = content_box.text_frame
        tf_c.word_wrap = True

        p_sum = tf_c.paragraphs[0]
        p_sum.text = s["summary"]
        p_sum.font.size = Pt(13)
        p_sum.font.color.rgb = RGBColor(226, 232, 240)
        p_sum.space_after = Pt(14)

        for b in s["bullets"]:
            p_b = tf_c.add_paragraph()
            p_b.text = "• " + b
            p_b.font.size = Pt(12)
            p_b.font.color.rgb = RGBColor(203, 213, 225)
            p_b.space_after = Pt(8)

        # Image(s) on Right
        if s["image_main"] and s["image_sub"]:
            img1_path = os.path.join(OUT_DIR, s["image_main"].replace("assets/", "assets\\"))
            img2_path = os.path.join(OUT_DIR, s["image_sub"].replace("assets/", "assets\\"))
            if os.path.exists(img1_path) and os.path.exists(img2_path):
                slide.shapes.add_picture(img1_path, Inches(6.9), Inches(1.9), width=Inches(2.75))
                slide.shapes.add_picture(img2_path, Inches(9.8), Inches(1.9), width=Inches(2.75))
        elif s["image_main"]:
            img1_path = os.path.join(OUT_DIR, s["image_main"].replace("assets/", "assets\\"))
            if os.path.exists(img1_path):
                slide.shapes.add_picture(img1_path, Inches(6.9), Inches(1.9), width=Inches(5.6))

        # Speaker notes
        notes_slide = slide.notes_slide
        text_frame = notes_slide.notes_text_frame
        text_frame.text = s["notes"]

    out_file = os.path.join(OUT_DIR, "QLBanMayViTinh_32_Slides.pptx")
    prs.save(out_file)
    print(f"Generated PowerPoint Presentation successfully: {out_file} (32 Slides)")

if __name__ == "__main__":
    print("=== STARTING FULL 32-SLIDE PRESENTATION GENERATION ===")
    generate_html_presentation()
    generate_markdown_script()
    generate_pptx_deck()
    print("=== ALL 3 DELIVERABLES PRODUCED SUCCESSFULLY! ===")

