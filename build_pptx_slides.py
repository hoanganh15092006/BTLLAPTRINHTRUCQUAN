import os
from pptx import Presentation
from pptx.util import Inches, Pt
from pptx.dml.color import RGBColor
from pptx.enum.text import PP_ALIGN

prs = Presentation()
prs.slide_width = Inches(13.333)
prs.slide_height = Inches(7.5)

COLOR_BG = RGBColor(15, 23, 42)
COLOR_CARD = RGBColor(30, 41, 59)
COLOR_TITLE = RGBColor(56, 189, 248)
COLOR_WHITE = RGBColor(248, 250, 252)
COLOR_MUTED = RGBColor(148, 163, 184)
COLOR_ACCENT = RGBColor(99, 102, 241)
COLOR_GREEN = RGBColor(52, 211, 153)
COLOR_YELLOW = RGBColor(251, 191, 36)

blank_slide_layout = prs.slide_layouts[6]

slides_data = [
    {
        "num": 1,
        "tag": "ĐỒ ÁN LẬP TRÌNH TRỰC QUAN • C# WINFORMS & SQL SERVER",
        "title": "HỆ THỐNG QUẢN LÝ BÁN MÁY VI TÍNH (QLBanMayViTinh)",
        "desc": "Giải pháp phần mềm ERP/POS chuyên nghiệp quản trị toàn diện chuỗi cung ứng máy tính, 14 linh kiện phần cứng chuẩn 3NF, bảo vệ giao dịch tài chính ACID và phân tích kinh doanh thời gian thực.",
        "points": [
            "• Nền tảng: .NET Framework 4.8 & C# WinForms",
            "• Data Access: Pure ADO.NET Engine (Zero ORM overhead)",
            "• Database: 25 Bảng 3NF + Automated ACID Triggers",
            "• UI/UX: Custom GDI+ Dual-Theme Engine (Light/Dark)",
            "• Bảo mật: Mật khẩu PBKDF2 10k vòng + Mã hóa AES-256",
            "• Đạt chuẩn QA: 25/25 bài test tự động E2E vượt qua 100%"
        ],
        "image": "Presentation/assets/ui_main_light.png",
        "notes": "[00:00 - 01:00] Kính thưa quý thầy cô trong Hội đồng phản biện. Hôm nay nhóm xin trân trọng báo cáo đồ án 'Hệ Thống Quản Lý Bán Máy Vi Tính' xây dựng trên nền tảng C# WinForms .NET và SQL Server. Điểm đặc biệt của đồ án là chúng em áp dụng kiến trúc 3 tầng chuẩn mực, không dùng ORM cồng kềnh mà tối ưu hoá 100% bằng ADO.NET thuần, chuẩn hóa 25 bảng CSDL 3NF, đồng bộ kho bằng Trigger và động cơ giao diện GDI+ Dual-Theme tự viết."
    },
    {
        "num": 2,
        "tag": "THÁCH THỨC THỰC TẾ & ĐỘT PHÁ KỸ THUẬT",
        "title": "BÀI TOÁN THỰC TẾ & TẦM NHÌN GIẢI PHÁP",
        "desc": "Giải quyết triệt để các tồn tại cố hữu của phần mềm quản lý bán lẻ truyền thống thông qua chuẩn hóa cơ sở dữ liệu và cơ chế bảo vệ giao dịch tự động.",
        "points": [
            "• Dị thường dữ liệu (Redundancy): Tránh nhập text tự do bằng 14 bảng cấu hình 3NF",
            "• Chống xuất âm kho (Negative Stock): Thu ngân không thể bán vượt quá tồn kho thực tế",
            "• Bảo vệ tài chính: Cảnh báo bán dưới giá vốn và tự động gợi ý giá bán 110%",
            "• Bảo mật dữ liệu cá nhân: Mã hóa AES-256 số điện thoại và địa chỉ khách hàng",
            "• Giao diện hiện đại: Bố cục Bento Grid phẳng, tự động thích ứng trên mọi màn hình"
        ],
        "image": "Presentation/assets/graph_workflow_pos_trigger.png",
        "notes": "[01:00 - 02:00] Trong thực tế tại các showroom máy tính như Thế Giới Di Động hay GearVN, vấn đề lớn nhất là quản lý hàng trăm mẫu máy tính với hàng ngàn linh kiện rời rạc: CPU, RAM, Ổ cứng... Nếu thiết kế CSDL lỏng lẻo sẽ gây dị thường dữ liệu (redundancy) và nghiêm trọng nhất là hiện tượng 'xuất âm kho' khi thu ngân bán nhanh hơn thủ kho nhập. Đồ án này giải quyết triệt để vấn đề đó bằng chuẩn hóa 3NF và khóa Trigger tầng CSDL."
    },
    {
        "num": 3,
        "tag": "TOÀN CẢNH NỀN TẢNG KỸ THUẬT",
        "title": "6 TRỤ CỘT CÔNG NGHỆ NỀN TẢNG (FULL TECH STACK)",
        "desc": "Kiến trúc hệ thống được xây dựng trên các công nghệ chuẩn mực, tối ưu hóa hiệu năng tối đa cho môi trường máy bàn Desktop.",
        "points": [
            "1. WinForms Modern GDI+: Custom GraphicsPath bo tròn, Responsive Layout",
            "2. Pure ADO.NET Engine: 100% Parameterized SQL, tốc độ < 5ms",
            "3. SQL Server 25 Bảng 3NF: Khóa ngoại FK chặt chẽ, Triggers kiểm soát kho",
            "4. Dual-Theme Engine: Chuyển đổi Dark/Light Mode mượt mà không nhấp nháy",
            "5. Enterprise Security: PBKDF2 Salt 128-bit, AES-256, Phân quyền RBAC 3 cấp",
            "6. Native Excel Streaming: Xuất file OpenXML/HTML-UTF8 không phụ thuộc MS Office"
        ],
        "image": "Presentation/assets/graph_system_architecture.png",
        "notes": "[02:00 - 03:00] Về công nghệ sử dụng, chúng em phân bổ thành 6 trụ cột cốt lõi. Chúng em kiên quyết chọn ADO.NET thuần túy thay vì Entity Framework để nắm quyền kiểm soát 100% câu lệnh T-SQL, loại bỏ độ trễ ORM, giúp các thao tác tính toán tổng tiền, trừ kho đạt tốc độ dưới 5 mili-giây. Kết hợp hệ thống Custom Rendering GDI+ tự viết cho phép bo góc mọi control trên màn hình."
    },
    {
        "num": 4,
        "tag": "PHÂN TÍCH ĐỒ THỊ TRI THỨC MÃ NGUỒN",
        "title": "KIẾN TRÚC ĐA TẦNG & ĐỒ THỊ PHỤ THUỘC (GRAPHIFY)",
        "desc": "Kết quả phân tích AST và Call Graph từ công cụ Graphify chứng minh tính độc lập và sạch sẽ của các tầng kiến trúc.",
        "points": [
            "• Quét toàn diện 527 Nodes và 1.668 Edges trên toàn bộ codebase",
            "• 14 Form nghiệp vụ tách biệt hoàn toàn với tầng dữ liệu",
            "• Mọi giao tiếp hội tụ qua 3 Helper Classes: Functions, UIThemeHelper, SecurityHelper",
            "• Cam kết Zero Circular Dependency (Không có bất kỳ phụ thuộc vòng nào)",
            "• Dễ dàng bảo trì, mở rộng và kiểm thử đơn vị độc lập"
        ],
        "image": "Presentation/assets/graph_dependency_map.png",
        "notes": "[03:00 - 04:00] Xin mời quý thầy cô quan sát đồ thị phụ thuộc Graphify trên màn hình. Chúng em đã dùng công cụ Graphify quét toàn bộ AST của dự án gồm 527 Nodes và 1.668 Edges. Kết quả chứng minh kiến trúc phần mềm hoàn toàn sạch sẽ: 14 Form nghiệp vụ tách biệt hoàn toàn với tầng dữ liệu, tất cả đều hội tụ qua 3 lớp Helper dùng chung: Functions (Truy xuất CSDL), UIThemeHelper (Giao diện) và SecurityHelper (Mật mã). Tuyệt đối không có phụ thuộc vòng (Zero Circular Dependency)."
    },
    {
        "num": 5,
        "tag": "ĐỘT PHÁ TRẢI NGHIỆM NGƯỜI DÙNG (UX/UI)",
        "title": "GIAO DIỆN HIỆN ĐẠI CUSTOM GDI+ & DUAL-THEME ENGINE",
        "desc": "Xóa bỏ định kiến WinForms cũ kỹ, kiến tạo giao diện phẳng Fluent Bento Grid với công nghệ GDI+ tự viết.",
        "points": [
            "• MakeRoundedControl: Dùng GraphicsPath.AddArc tạo góc bo 10-14px mềm mại",
            "• Thuật toán đệ quy ApplyTheme: Duyệt cây điều khiển đổi màu token toàn form",
            "• Reflection DoubleBuffered: Kích hoạt vẽ 2 lớp, triệt tiêu nhấp nháy 100%",
            "• Ghi nhớ Theme người dùng: Lưu cấu hình vĩnh viễn vào CSDL SQL Server",
            "• Thời gian chuyển theme cực nhanh: Chỉ mất dưới 15ms cho toàn màn hình"
        ],
        "image": "Presentation/assets/code_gdi_theme_engine.png",
        "notes": "[04:00 - 05:00] Điểm sáng lớn về mặt giao diện là hệ thống Custom Rendering GDI+. Thông thường sinh viên làm WinForms nút bấm rất vuông vức thô kệch. Tại đây chúng em dùng System.Drawing.Drawing2D.GraphicsPath để vẽ các góc bo tròn mềm mại cho thẻ Bento và nút bấm, sau đó cắt xén bằng Region. Khi bấm nút Dark/Light mode trên thanh Header, sự kiện OnThemeChanged kích hoạt hàm duyệt đệ quy ApplyTheme đổi màu toàn bộ form trong chưa đầy 15 phần nghìn giây."
    },
    {
        "num": 6,
        "tag": "TẦNG TRUY XUẤT DỮ LIỆU TỐC ĐỘ CAO",
        "title": "TẦNG DỮ LIỆU THUẦN ADO.NET & PARAMETERIZED QUERY",
        "desc": "Tối ưu hóa hiệu năng truy xuất cơ sở dữ liệu với ADO.NET thuần túy, an toàn tuyệt đối trước mọi nguy cơ SQL Injection.",
        "points": [
            "• GetFieldValues: Đọc nhanh giá trị vô hướng trong 1 dòng lệnh",
            "• RunSQL: Thực thi thao tác thêm, sửa, xóa với mảng SqlParameter an toàn",
            "• 100% Parameterized Query: Loại bỏ hoàn toàn việc nối chuỗi câu lệnh SQL",
            "• Khối lệnh using: Đảm bảo tự động giải phóng kết nối và tài nguyên bộ nhớ",
            "• Connection Pooling: Tận dụng cơ chế tái sử dụng kết nối của SQL Server"
        ],
        "image": "Presentation/assets/code_adonet_core.png",
        "notes": "[05:00 - 06:00] Ở tầng dữ liệu, toàn bộ giao tiếp CSDL được chuẩn hóa trong Functions.cs. Chúng em xây dựng các hàm tiện ích dùng chung như GetFieldValues đọc nhanh một giá trị vô hướng, RunSQL thực thi phi truy vấn an toàn và FillCombo nạp danh mục. Điểm cốt tử là mọi truy vấn đều truyền qua mảng SqlParameter, tuyệt đối không dùng phép nối chuỗi thô, triệt tiêu hoàn toàn nguy cơ SQL Injection."
    },
    {
        "num": 7,
        "tag": "BẢO MẬT XÁC THỰC & KIỂM SOÁT QUYỀN HẠN",
        "title": "HỆ THỐNG XÁC THỰC, BẢO MẬT PBKDF2 & PHÂN QUYỀN RBAC",
        "desc": "Mô hình an ninh thông tin đa lớp: Xác thực mật mã học, phân quyền vai trò động và đối soát số điện thoại hồ sơ.",
        "points": [
            "• frmDangNhap: Băm PBKDF2 xác thực, kiểm tra tài khoản bị khóa, nút toggle mật khẩu",
            "• frmDangKy: Sinh muối ngẫu nhiên 128-bit, tự động liên kết hồ sơ nhân viên",
            "• frmQuenMatKhau: Đối soát số điện thoại nhân sự để cấp lại mật khẩu an toàn",
            "• Phân quyền RBAC 3 cấp độ:",
            "  - Admin: Toàn quyền tất cả các phân hệ và cấu hình hệ thống",
            "  - Thủ Kho: Quản lý linh kiện, máy tính, hóa đơn nhập; ẩn hóa đơn bán",
            "  - Bán Hàng: Sử dụng POS bán lẻ; ẩn hóa đơn nhập, cài đặt, phân quyền"
        ],
        "image": "Presentation/assets/graph_navigation_state.png",
        "notes": "[06:00 - 07:00] Khâu bảo mật được thiết kế 3 lớp vững chắc. Mật khẩu người dùng được băm bằng thuật toán PBKDF2 với 10.000 vòng lặp kết hợp khóa muối Salt 128-bit độc nhất cho từng tài khoản. Kể cả quản trị viên mở CSDL ra cũng không thể giải mã được mật khẩu. Ngoài ra, tính năng Quên Mật Khẩu có cơ chế đối soát số điện thoại thực tế trong hồ sơ nhân sự, ngăn chặn người ngoài xâm nhập tài khoản nội bộ."
    },
    {
        "num": 8,
        "tag": "KHUNG ỨNG DỤNG ĐƠN CỬA SỔ (SDI)",
        "title": "TRUNG TÂM ĐIỀU KHIỂN frmMain & NHÚNG FORM CON ĐỘNG",
        "desc": "Kiến trúc nhúng form con động (Dynamic Form Embedding) vào Panel trung tâm, mang lại trải nghiệm mượt mà như ứng dụng Web SPA.",
        "points": [
            "• TopLevel = false & FormBorderStyle.None: Nhúng form con gọn gàng vào pnlContent",
            "• Tự động Dispose() form cũ: Giải phóng bộ nhớ RAM ngay khi chuyển phân hệ",
            "• 4 Thẻ Bento KPI: Tự động tổng hợp Doanh thu, Chi phí, Tồn kho, Lợi nhuận",
            "• Header thông minh: Đồng hồ số nhảy từng giây, trạng thái kết nối SQL Server",
            "• Menu Sidebar động: Tự động co giãn và đổi màu nút điều hướng đang kích hoạt"
        ],
        "image": "Presentation/assets/ui_main_dark.png",
        "notes": "[07:00 - 08:00] Màn hình frmMain đóng vai trò như một Bento Shell hiện đại. Thay vì mở cửa sổ MDI cổ lỗ sĩ hay mở form pop-up lộn xộn, chúng em áp dụng kỹ thuật Dynamic Form Embedding: đóng gói các Form con, bỏ viền FormBorderStyle.None, gán TopLevel = false và nhúng trực tiếp vào panel nội dung pnlContent. Người dùng cảm giác như đang sử dụng một ứng dụng Web SPA mượt mà, đồng thời 4 thẻ KPI Header cập nhật doanh thu và lợi nhuận tự động."
    },
    {
        "num": 9,
        "tag": "PHÂN HỆ QUẢN LÝ SẢN PHẨM CỐT LÕI",
        "title": "QUẢN LÝ MÁY VI TÍNH & 14 CẤU HÌNH PHẦN CỨNG 3NF",
        "desc": "Quản lý danh mục máy tính chuyên nghiệp, kết nối 14 thông số kỹ thuật chuẩn hóa 3NF và chỉnh sửa giá trực tiếp an toàn.",
        "points": [
            "• 14 Khóa ngoại chuẩn 3NF: Loại máy, Chip CPU, RAM, Ổ cứng, Màn hình, Hãng SX...",
            "• Lưu trữ ảnh nhị phân: Sử dụng MemoryStream tránh lỗi khóa file trên ổ đĩa",
            "• Cho phép sửa trực tiếp: Giá Nhập, Giá Bán, Tồn Kho lưu vĩnh viễn vào CSDL",
            "• Gợi ý giá bán thông minh: Tự động tính Giá Bán = 110% Giá Nhập khi nhập kho",
            "• Bộ lọc phím OnlyInteger: Ngăn chặn triệt để người dùng gõ ký tự chữ vào ô giá"
        ],
        "image": "Presentation/assets/ui_mayvitinh.png",
        "notes": "[08:00 - 09:00] Form frmMayViTinh là trái tim của hệ thống quản lý sản phẩm. Mỗi máy tính ở đây được chuẩn hóa 14 thông số linh kiện phần cứng độc lập. Chúng em tích hợp tính năng nạp ảnh nhị phân VARBINARY(MAX) qua MemoryStream để không khóa file ảnh trên ổ đĩa. Đặc biệt, người dùng có thể sửa trực tiếp Giá Nhập, Giá Bán và Tồn Kho lưu vào CSDL, có kiểm soát ràng buộc giá bán không được thấp hơn giá nhập và tồn kho không được âm."
    },
    {
        "num": 10,
        "tag": "KIẾN TRÚC TINH GỌN (POLYMORPHIC UI)",
        "title": "KIẾN TRÚC '4-TRONG-1' TRÊN frmDanhMucHeThong",
        "desc": "Thiết kế giao diện đa hình (Polymorphic UI) gộp 4 phân hệ danh mục nền tảng vào 1 Form duy nhất, giảm 80% mã lệnh trùng lặp.",
        "points": [
            "• Tab 1 - Khách Hàng: Quản lý đối tác mua lẻ, tự động mã hóa AES-256 SĐT & Địa chỉ",
            "• Tab 2 - Nhà Cung Cấp: Quản lý nguồn hàng nhập khẩu, kiểm tra trùng lặp",
            "• Tab 3 - Nhân Viên: Ràng buộc tuổi lao động >= 15 tuổi, chặn ngày sinh tương lai",
            "• Tab 4 - Linh Kiện Động: Tự động ánh xạ 14 bảng linh kiện CSDL chỉ với 1 DataGridView",
            "• Tối ưu mã nguồn: Hàm GetCurrentTableName phân giải bảng CSDL linh hoạt"
        ],
        "image": "Presentation/assets/ui_danhmuc_all_in_one.png",
        "notes": "[09:00 - 10:00] Tại form Danh Mục Hệ Thống, thay vì tạo 5 form rời rạc quản lý Khách Hàng, Nhà Cung Cấp, Nhân Viên, Linh Kiện, chúng em đã gom tất cả vào một Form duy nhất với kiến trúc 4 Tab thông minh. Đặc biệt ở Tab 4, chỉ với 1 bảng DataGridView và 1 bộ nút Thêm/Sửa/Xóa, hệ thống tự động ánh xạ vào 14 bảng linh kiện khác nhau tùy theo ComboBox người dùng chọn. Số điện thoại và địa chỉ đều được mã hóa AES-256 an toàn."
    },
    {
        "num": 11,
        "tag": "QUẢN TRỊ NHẬP HÀNG & GIÁ VỐN",
        "title": "NGHIỆP VỤ NHẬP KHO MASTER-DETAIL (frmHoaDonNhap)",
        "desc": "Lập phiếu nhập hàng chuyên nghiệp từ Nhà cung cấp, tự động tăng số lượng tồn kho và cập nhật giá vốn qua Database Trigger.",
        "points": [
            "• Mô hình Master-Detail: Bảng tblHoaDonNhap và chi tiết tblChiTietHDNhap",
            "• Trigger tự động tăng kho: Kích hoạt trg_ChiTietHDNhap cộng dồn tồn kho MayViTinh",
            "• Cập nhật giá vốn tức thì: Đơn giá nhập mới nhất được cập nhật tự động",
            "• Hoàn trả kho khi xóa dòng: Double-click dòng hàng để xóa và trừ lùi kho an toàn",
            "• Zero Truncation: Bố cục LayoutDetailCard tự co giãn, hiển thị đầy đủ nhãn chữ"
        ],
        "image": "Presentation/assets/ui_hoadonnhap.png",
        "notes": "[10:00 - 11:00] Tiếp theo là phân hệ Nhập Kho frmHoaDonNhap theo chuẩn Master-Detail. Khi thủ kho chọn sản phẩm và nhập số lượng cùng đơn giá nhập, hệ thống tự động tính thành tiền. Điểm cốt lõi là khi bấm Lưu, Trigger trên SQL Server tự động tăng số lượng tồn kho của máy tương ứng và cập nhật đơn giá nhập mới nhất cho sản phẩm. Nếu người dùng nhấp đúp để xóa sản phẩm khỏi phiếu, Trigger sẽ tự động trừ lùi hoàn trả kho."
    },
    {
        "num": 12,
        "tag": "THU NGÂN BÁN LẺ & CHỐNG THẤT THOÁT",
        "title": "BÁN HÀNG POS & CHỐNG XUẤT ÂM KHO (frmHoaDonBan)",
        "desc": "Phân hệ thu ngân bán lẻ trực quan, kiểm tra tồn kho khả dụng thời gian thực và bảo vệ kho hàng bằng cơ chế chống xuất âm kép.",
        "points": [
            "• Chống xuất âm 2 lớp: Client kiểm tra chặn trước, Server có Trigger ROLLBACK",
            "• Nạp giá chuẩn tự động: Tự nạp giá bán 110% và hiển thị tồn kho thực tế",
            "• Cảnh báo bán lỗ: Thông báo xác nhận nếu người dùng thiết lập giá bán < giá nhập",
            "• Đọc tiền thành chữ: Hàm ChuyenSoSangChu hỗ trợ in hóa đơn kế toán chuẩn xác",
            "• Hoàn kho khi hủy đơn: Trả lại nguyên vẹn số lượng máy tính khi hủy dòng sản phẩm"
        ],
        "image": "Presentation/assets/ui_hoadonban.png",
        "notes": "[11:00 - 12:00] Đây là giao diện quan trọng nhất đối với nhân viên bán hàng: frmHoaDonBan. Khi nhân viên chọn máy tính, hệ thống tự động điền đơn giá bán 110% và hiển thị số lượng tồn kho khả dụng hiện thời. Cơ chế chống xuất âm hoạt động 2 lớp: Client kiểm tra chặn ngay lập tức nếu số lượng bán lớn hơn tồn kho; Server dùng Trigger Rollback giao dịch nếu phát hiện tồn kho âm. Đồng thời có tính năng đọc số tiền thành chữ tiếng Việt chuẩn xác."
    },
    {
        "num": 13,
        "tag": "BẢO TOÀN GIAO DỊCH ACID TẦNG CSDL",
        "title": "SỨC MẠNH ĐỒNG BỘ C# & DATABASE TRIGGER SQL SERVER",
        "desc": "Tối ưu hóa kiến trúc bằng cách chuyển giao logic bảo toàn dữ liệu cho Database Trigger, đảm bảo tính nhất quán ACID tuyệt đối.",
        "points": [
            "• Trigger trg_ChiTietHDB_AUID: Tự động trừ tồn kho và tính lại tổng tiền Master",
            "• Khóa giao dịch an toàn: Nếu phát hiện tồn kho âm, tự động RAISERROR và ROLLBACK",
            "• Giảm tải cho ứng dụng C#: Loại bỏ hàng chục câu lệnh UPDATE thủ công",
            "• Bảo toàn 100% dữ liệu: Dù ứng dụng mất điện đột ngột, CSDL vẫn luôn nhất quán",
            "• Đáp ứng đồng thời cao: Hỗ trợ nhiều thu ngân lập hóa đơn cùng một thời điểm"
        ],
        "image": "Presentation/assets/code_sql_trigger_sync.png",
        "notes": "[12:00 - 13:00] Kính thưa Hội đồng, điểm mấu chốt tạo nên đẳng cấp kỹ thuật của đồ án là cơ chế đồng bộ tự động giữa C# và SQL Server Trigger. Thay vì viết hàng chục dòng lệnh UPDATE tồn kho thủ công trong C# dễ gây lỗi bất đồng bộ, chúng em giao toàn bộ trách nhiệm bảo toàn dữ liệu cho Database Trigger trg_ChiTietHDB_AUID. Nhờ đó, tính toàn vẹn ACID được bảo đảm 100%, dù ứng dụng bị tắt đột ngột thì kho hàng và tổng tiền vẫn khớp tuyệt đối."
    },
    {
        "num": 14,
        "tag": "TRA CỨU ĐA CHIỀU NHANH CHÓNG (YÊU CẦU 4, 5)",
        "title": "CÔNG CỤ TRUY VẤN & TÌM KIẾM ĐA NĂNG (frmTimKiem)",
        "desc": "Giải quyết toàn diện Yêu cầu 4 và 5 của đề tài với công cụ tìm kiếm đa chiều trên Máy Tính, Hóa Đơn Nhập và Hóa Đơn Bán.",
        "points": [
            "• 3 Chế độ tìm kiếm: Máy Vi Tính (YC4), Hóa Đơn Nhập (YC5), Hóa Đơn Bán (YC5)",
            "• Thuật toán Dynamic SQL: Khởi tạo WHERE 1=1, tự ghép nối tiêu chí người dùng nhập",
            "• Lọc linh hoạt: Theo mã, tên, đối tác, nhân viên và khoảng ngày Từ Ngày - Đến Ngày",
            "• LayoutDynamicControls: Tự động dịch chuyển vị trí các ô nhập liệu chống cắt chữ",
            "• Tốc độ tra cứu cực nhanh: Phản hồi dưới 10ms trên hàng ngàn bản ghi CSDL"
        ],
        "image": "Presentation/assets/ui_timkiem_dynamic.png",
        "notes": "[13:00 - 14:00] Form frmTimKiem giải quyết trọn vẹn Yêu cầu 4 và Yêu cầu 5 của đề tài. Chúng em tích hợp công cụ tìm kiếm đa chiều trên cả 3 thực thể: Máy Vi Tính, Hóa Đơn Nhập và Hóa Đơn Bán. Thuật toán Dynamic SQL Builder nối chuỗi WHERE 1=1 thông minh kết hợp với các tham số Parameter giúp người dùng có thể lọc theo bất kỳ tiêu chí nào: Mã, Tên, Ngày tháng, Khoảng giá hay Đối tác mà không làm phát sinh lỗi SQL Injection."
    },
    {
        "num": 15,
        "tag": "PHÂN TÍCH KINH DOANH & BÁO CÁO QUẢN TRỊ",
        "title": "BI DASHBOARD & BÁO CÁO TÀI CHÍNH (frmBaoCaoThongKe)",
        "desc": "Trung tâm phân tích kinh doanh thời gian thực, đáp ứng trọn vẹn 4 yêu cầu báo cáo thống kê cốt lõi của đề tài.",
        "points": [
            "• Yêu Cầu 6: Thống kê máy tính tồn kho & định giá tài sản kho theo giá nhập",
            "• Yêu Cầu 7: Báo cáo doanh thu nhập hàng theo Tháng / Quý (1-4) / Năm",
            "• Yêu Cầu 8: Báo cáo doanh thu bán hàng theo Tháng / Quý / Năm",
            "• Yêu Cầu 9: Báo cáo xếp hạng Top 5 nhân viên có doanh thu cao nhất",
            "• 4 Thẻ Summary Cards: Tổng Tồn Kho, Chi Phí Nhập, Doanh Thu, Ước Tính Lợi Nhuận",
            "• Dynamic Collapsing: Thanh lọc tự thu gọn 0px khi xem tồn kho để mở rộng bảng"
        ],
        "image": "Presentation/assets/ui_baocao_tonkho.png",
        "notes": "[14:00 - 15:00] Form Báo Cáo Thống Kê frmBaoCaoThongKe là trung tâm Business Intelligence giải quyết trọn vẹn 4 yêu cầu 6, 7, 8, 9 của đồ án. Điểm ấn tượng nhất là 4 thẻ KPI Summary Cards trên đầu form hiển thị: Tổng Giá Trị Tồn Kho, Tổng Chi Phí Nhập, Tổng Doanh Thu Bán và Ước Tính Lợi Nhuận. Thanh lọc thời gian có tính năng Dynamic Collapsing: tự thu gọn 0px khi xem tồn kho và mở rộng khi lọc Quý/Năm."
    },
    {
        "num": 16,
        "tag": "THUẬT TOÁN XẾP HẠNG & ENGINE XUẤT BÁO CÁO",
        "title": "THUẬT TOÁN TOP 5 WITH TIES & ENGINE XUẤT EXCEL",
        "desc": "Vận dụng kỹ thuật truy vấn nâng cao TOP 5 WITH TIES và engine kết xuất báo cáo Excel định dạng OpenXML/HTML tốc độ cao.",
        "points": [
            "• TOP 5 WITH TIES: Xử lý triệt để bài toán đồng hạng doanh thu của nhân viên",
            "• Phân tích thời gian chuẩn xác: Sử dụng DATEPART(QUARTER, NgayBan) và MONTH()",
            "• Native Excel Engine: Sinh luồng HTML-UTF8 với thẻ mở rộng mso-number-format",
            "• Định dạng tiền tệ chuyên nghiệp: Tự động format #,#0 VND và kẻ viền bảng biểu",
            "• Độc lập hoàn toàn: Người dùng không cần cài đặt phần mềm Microsoft Office"
        ],
        "image": "Presentation/assets/code_dynamic_query_builder.png",
        "notes": "[15:00 - 16:00] Để xếp hạng nhân viên xuất sắc nhất một cách công bằng theo đúng chuẩn kế toán, chúng em sử dụng mệnh đề TOP 5 WITH TIES trong SQL Server kết hợp ORDER BY DESC, đảm bảo nếu nhân viên thứ 5 và thứ 6 có cùng doanh số thì cả hai đều được vinh danh. Bên cạnh đó, tính năng Xuất Excel được viết bằng luồng StringBuilder HTML-UTF8 tốc độ cao, không cần phụ thuộc thư viện Excel COM nặng nề của Microsoft Office."
    },
    {
        "num": 17,
        "tag": "QUẢN TRỊ HỆ THỐNG & CẤU HÌNH MÔI TRƯỜNG",
        "title": "QUẢN LÝ NGƯỜI DÙNG, HỒ SƠ & CÀI ĐẶT CSDL",
        "desc": "Bộ ba phân hệ quản trị hạ tầng: Phân quyền tài khoản người dùng, hồ sơ cá nhân và cấu hình kết nối CSDL linh hoạt.",
        "points": [
            "• frmQuanLyNguoiDung: Phân quyền vai trò, khóa tài khoản, chống tự khóa Admin",
            "• frmHoSoCaNhan: Cập nhật thông tin liên hệ, đổi mật khẩu 3 bước bảo mật",
            "• frmCaiDat: Kiểm tra chuỗi kết nối SQL Server chủ động (Test Connection trong 5s)",
            "• Cấu hình Key-Value: Lưu tên cửa hàng, hotline, địa chỉ in chân trang hóa đơn",
            "• Sao lưu & Phục hồi: Hỗ trợ tạo file backup .bak và restore dữ liệu nhanh chóng"
        ],
        "image": "Presentation/assets/ui_caidat.png",
        "notes": "[16:00 - 17:00] Phân hệ quản trị hệ thống gồm 3 form: Quản Lý Người Dùng, Hồ Sơ Cá Nhân và Cài Đặt. Tại form Quản lý người dùng, Admin có thể tạo tài khoản, đổi vai trò phân quyền và bật/tắt khóa tài khoản với cơ chế bảo vệ chống tự khóa chính mình. Form Cài Đặt cho phép kiểm tra chuỗi kết nối SQL Server chủ động và cấu hình thông tin cửa hàng để in lên hóa đơn."
    },
    {
        "num": 18,
        "tag": "KIỂM TOÁN AN NINH & TRUY VẾT HOẠT ĐỘNG",
        "title": "'HỘP ĐEN' KIỂM TOÁN NỘI BỘ (frmNhatKyHoatDong)",
        "desc": "Hệ thống ghi vết kiểm toán (Audit Trail) giám sát 100% biến động dữ liệu và trung tâm cảnh báo sản phẩm sắp hết hàng.",
        "points": [
            "• Lưu vết tự động: Người dùng, Hành động, Chi tiết và Thời gian chính xác từng giây",
            "• Low Stock Alert: Quét và hiển thị các mẫu máy có tồn kho <= 5 chiếc",
            "• Bộ lọc đa chiều: Lọc theo thời gian, người thực hiện hoặc loại nghiệp vụ",
            "• Chống chối bỏ (Non-repudiation): Dữ liệu nhật ký được bảo vệ chỉ đọc (Read-only)",
            "• Phát hiện gian lận: Ngăn chặn hành vi tự ý sửa giá bán hoặc biển thủ thu ngân"
        ],
        "image": "Presentation/assets/ui_nhatkyhoatdong.png",
        "notes": "[17:00 - 18:00] Để đảm bảo tính an ninh và minh bạch trong chuỗi bán lẻ, chúng em xây dựng 'Hộp đen kiểm toán' frmNhatKyHoatDong. Mỗi khi người dùng đăng nhập, thêm máy, sửa giá, lập hóa đơn, hàm Functions.LogAction tự động ghi nhận thời gian chính xác tới từng giây. Đặc biệt trên form còn tích hợp bảng cảnh báo các sản phẩm có số lượng tồn kho dưới ngưỡng an toàn (nhỏ hơn 5 chiếc) giúp quản lý lên kế hoạch nhập hàng kịp thời."
    },
    {
        "num": 19,
        "tag": "QUY CHUẨN KỸ THUẬT & ĐẢM BẢO CHẤT LƯỢNG",
        "title": "BỘ TEST SUITE TỰ ĐỘNG & QUY TẮC ZERO-COMMENT",
        "desc": "Quy trình kiểm thử hồi quy tự động 4 tầng (Opaque-Box E2E Tests) đạt tỷ lệ 25/25 bài kiểm tra thành công tuyệt đối.",
        "points": [
            "• Tier 1 - Feature Coverage (12/12): Kiểm tra 25 bảng 3NF, VARBINARY ảnh, 14 Form",
            "• Tier 2 - Boundary Cases (6/6): Kiểm tra giá trị biên 0 VNĐ, 999 tỷ VNĐ, chặn xuất âm",
            "• Tier 3 - Integration (4/4): Đồng bộ theme liên form, tự tính giá bán 110%",
            "• Tier 4 - Real-World POS (3/3): Chu trình trọn vẹn Nhập kho -> Bán hàng -> Hoàn tồn",
            "• Quy tắc Zero-Comment: 100% sạch sẽ trên 40 file C# và script SQL chuẩn quốc tế"
        ],
        "image": "Presentation/assets/code_html_excel_engine.png",
        "notes": "[18:00 - 19:00] Về quy trình đảm bảo chất lượng, đồ án không kiểm thử thủ công qua loa mà được trang bị bộ kiểm thử tự động Opaque-Box E2E Test Suite gồm 25 bài kiểm tra chia làm 4 tầng: từ kiểm tra số lượng bảng 3NF, tính toàn vẹn ảnh nhị phân, stress test giao diện 50 chu kỳ theme liên tục, cho đến kiểm tra bảo vệ xuất âm kho. Kết quả kiểm thử đạt 25/25 bài thành công (100%), tuyệt đối không có bất kỳ dòng comment thừa nào trong mã nguồn."
    },
    {
        "num": 20,
        "tag": "TỔNG KẾT ĐỒ ÁN & SẴN SÀNG PHẢN BIỆN",
        "title": "TỔNG KẾT GIÁ TRỊ ĐỒ ÁN & HỎI ĐÁP HỘI ĐỒNG (Q&A)",
        "desc": "Đồ án khẳng định năng lực xây dựng ứng dụng Windows Desktop ở đẳng cấp phần mềm doanh nghiệp, sẵn sàng đón nhận câu hỏi từ Hội đồng.",
        "points": [
            "1. Kiến trúc chuẩn mực: 14 Forms nhúng SDI, Pure ADO.NET, 25 Bảng 3NF",
            "2. Giao dịch tin cậy: Database Triggers bảo toàn kho hàng và doanh thu tuyệt đối",
            "3. Bảo mật tiên tiến: Băm PBKDF2 chống bẻ khóa và mã hóa AES-256 dữ liệu khách",
            "4. UI/UX đột phá: Custom GDI+ Fluent Bento, Dual-Theme chuyển đổi tức thì",
            "5. Hướng phát triển: Nâng cấp RESTful Web API, thanh toán VietQR động và e-Invoice"
        ],
        "image": "Presentation/assets/graph_system_architecture.png",
        "notes": "[19:00 - 20:00] Để kết luận, đồ án QLBanMayViTinh không chỉ hoàn thành xuất sắc các yêu cầu của môn học Lập Trình Trực Quan mà còn vượt qua giới hạn của một bài tập lớn thông thường với kiến trúc 3 tầng chuẩn mực, đồ họa GDI+ hiện đại và bảo mật cấp doanh nghiệp. Nhóm chúng em đã sẵn sàng lắng nghe câu hỏi và giải trình trước quý thầy cô trong Hội đồng phản biện. Xin chân thành cảm ơn!"
    }
]

for s in slides_data:
    slide = prs.slides.add_slide(blank_slide_layout)
    
    # Background shape
    bg_shape = slide.shapes.add_shape(1, 0, 0, prs.slide_width, prs.slide_height)
    bg_shape.fill.solid()
    bg_shape.fill.fore_color.rgb = COLOR_BG
    bg_shape.line.fill.background()
    
    # Header Card
    header_box = slide.shapes.add_textbox(Inches(0.6), Inches(0.4), Inches(12.133), Inches(1.1))
    tf_h = header_box.text_frame
    tf_h.word_wrap = True
    
    p_tag = tf_h.paragraphs[0]
    p_tag.text = s["tag"]
    p_tag.font.size = Pt(11)
    p_tag.font.bold = True
    p_tag.font.color.rgb = COLOR_TITLE
    p_tag.font.name = "Segoe UI"
    
    p_title = tf_h.add_paragraph()
    p_title.text = s["title"]
    p_title.font.size = Pt(22)
    p_title.font.bold = True
    p_title.font.color.rgb = COLOR_WHITE
    p_title.font.name = "Segoe UI"

    # Left Column (Content Text Card)
    left_card = slide.shapes.add_shape(1, Inches(0.6), Inches(1.65), Inches(5.8), Inches(5.35))
    left_card.fill.solid()
    left_card.fill.fore_color.rgb = COLOR_CARD
    left_card.line.color.rgb = RGBColor(51, 65, 85)
    left_card.line.width = Pt(1)

    tb_left = slide.shapes.add_textbox(Inches(0.8), Inches(1.8), Inches(5.4), Inches(5.0))
    tf_l = tb_left.text_frame
    tf_l.word_wrap = True
    
    p_desc = tf_l.paragraphs[0]
    p_desc.text = s["desc"]
    p_desc.font.size = Pt(13)
    p_desc.font.italic = True
    p_desc.font.color.rgb = COLOR_MUTED
    p_desc.font.name = "Segoe UI"
    
    for pt in s["points"]:
        p_pt = tf_l.add_paragraph()
        p_pt.text = pt
        p_pt.font.size = Pt(12)
        p_pt.font.color.rgb = COLOR_WHITE
        p_pt.font.name = "Segoe UI"

    # Right Column (Image Showcase Card)
    img_path = s["image"]
    if os.path.exists(img_path):
        slide.shapes.add_picture(img_path, Inches(6.7), Inches(1.65), width=Inches(6.0))
    
    # Speaker Notes
    notes_slide = slide.notes_slide
    text_frame = notes_slide.notes_text_frame
    text_frame.text = s["notes"]

out_pptx = os.path.abspath("Presentation/QLBanMayViTinh_Full_Presentation.pptx")
prs.save(out_pptx)
print(f"Generated PowerPoint Presentation: {out_pptx}")

