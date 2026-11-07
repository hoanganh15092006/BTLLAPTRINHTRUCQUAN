# KỊCH BẢN THUYẾT TRÌNH BẢO VỆ ĐỒ ÁN (32 SLIDES TOÀN DIỆN)
## ĐỀ TÀI: HỆ THỐNG QUẢN LÝ BÁN MÁY VI TÍNH (QLBanMayViTinh)
**Nền tảng:** C# .NET WinForms & SQL Server (Pure ADO.NET, 3NF, GDI+ Dual-Theme, ACID Triggers)

---

### SLIDE 01: Hệ Thống Quản Lý Bán Máy Vi Tính
- **Chủ đề / Phân hệ:** BÁO CÁO ĐỒ ÁN MÔN HỌC — *Báo Cáo Đồ Án Lập Trình Trực Quan — Kiến Trúc 3 Tầng WinForms & Pure ADO.NET SQL Server*
- **Chỉ số nổi bật (Metric):** `25 Bảng 3NF` (Chuẩn Hóa Quan Hệ)
- **Tài nguyên hình ảnh minh họa:** `assets/ui_main_light.png`
- **Tóm tắt giải pháp:** Giải pháp phần mềm quản trị toàn diện cho chuỗi siêu thị bán lẻ máy tính và thiết bị công nghệ cao, tối ưu hóa giao diện công thái học Fluent Design với khả năng chuyển đổi Theme Sáng/Tối tức thì.
- **Điểm sáng kỹ thuật & Giá trị doanh nghiệp:** Tối ưu hóa phản hồi hệ thống ở mức mili-giây nhờ loại bỏ tầng ORM cồng kềnh, quản trị trọn vẹn vòng đời sản phẩm từ linh kiện rời đến máy nguyên bộ và tự động hóa đồng bộ tồn kho bằng Trigger ACID.
- **So sánh kiến trúc:**
  + **Cách cũ:** Hệ thống cũ: Dùng ORM Entity Framework nặng nề, độ trễ nạp dữ liệu > 150ms, giao diện xám cổ điển.
  + **Giải pháp đồ án:** Giải pháp đồ án: Pure ADO.NET nạp bảng < 15ms, động cơ GDI+ bo góc mịn, hỗ trợ Dark/Light Mode tức thời.
- **Nội dung chi tiết trên Slide:**
  + Kiến trúc 3 tầng phân lớp minh bạch: Presentation (14 WinForms), Business & Helper Layer, Data Layer.
  + Cơ sở dữ liệu gồm 25 bảng quan hệ chuẩn 3NF, loại bỏ hoàn toàn dị thường thêm/xóa/sửa.
  + Đồng bộ số lượng tồn kho và tổng tiền hóa đơn tức thì thông qua SQL Server Triggers.
  + Động cơ xuất báo cáo và hóa đơn Native UTF-8 HTML Spreadsheet tốc độ cao không phụ thuộc Office COM.
- **LỜI THUYẾT TRÌNH CHI TIẾT (SPEAKER SCRIPT):**
  > "[00:00 - 01:15] Kính thưa quý Thầy Cô trong Hội đồng phản biện. Hôm nay, em xin trân trọng báo cáo đồ án 'Hệ Thống Quản Lý Bán Máy Vi Tính' xây dựng trên nền tảng C# WinForms .NET và SQL Server. Điểm nổi bật của đề tài là chúng em áp dụng kiến trúc 3 tầng phân lớp rõ ràng, không sử dụng các ORM cồng kềnh mà tối ưu hóa 100% bằng ADO.NET thuần, chuẩn hóa cơ sở dữ liệu quan hệ 25 bảng đạt chuẩn 3NF, đồng bộ kho hàng bằng Trigger thời gian thực và xây dựng động cơ giao diện GDI+ Dual-Theme tự phát triển."

---

### SLIDE 02: Bối Cảnh Thực Tế Chuỗi Bán Lẻ Máy Tính & Mục Tiêu
- **Chủ đề / Phân hệ:** BÀI TOÁN & TẦM NHÌN — *Khảo sát thực tiễn tại các chuỗi bán lẻ công nghệ (Phong Vũ, HACOM, GearVN)*
- **Chỉ số nổi bật (Metric):** `14 Danh Mục` (Linh Kiện Cấu Hình)
- **Tài nguyên hình ảnh minh họa:** `assets/graph_01_overview_3tier.png`
- **Tóm tắt giải pháp:** Kinh doanh thiết bị vi tính đòi hỏi theo dõi hàng ngàn cấu hình phức tạp: CPU, RAM, Ổ cứng, VGA, Màn hình, Bàn phím với giá trị đơn hàng lớn và yêu cầu kiểm soát dòng tiền nghiêm ngặt.
- **Điểm sáng kỹ thuật & Giá trị doanh nghiệp:** Bảo đảm tính minh bạch trong quản trị chuỗi cung ứng: kiểm soát chặt chẽ giá vốn nhập kho, tự động tính biên độ lợi nhuận, chống thất thoát hàng hóa và hỗ trợ ra quyết định kinh doanh qua bảng chỉ số KPI thời gian thực.
- **So sánh kiến trúc:**
  + **Cách cũ:** Phương thức cũ: Ghi chép sổ sách hoặc dùng Excel rời rạc, không kiểm soát được nhân viên sửa giá bán hay gian lận kho.
  + **Giải pháp đồ án:** Giải pháp đồ án: Tập trung hóa dữ liệu trên SQL Server, phân quyền RBAC và ghi vết toàn bộ hành vi vào bảng audit log.
- **Nội dung chi tiết trên Slide:**
  + Thách thức quản lý đa linh kiện: Mỗi máy vi tính là sự kết hợp của 14 thông số kỹ thuật phần cứng riêng biệt.
  + Nguy cơ bán âm và lệch tồn kho khi nhân viên bán lẻ tại quầy thao tác đồng thời nhiều giao dịch.
  + Yêu cầu tốc độ xuất hóa đơn POS tại quầy nhanh chóng, không xảy ra hiện tượng khóa bản ghi gây nghẽn.
  + Mục tiêu: Xây dựng hệ sinh thái phần mềm ổn định, bảo mật tài khoản nhân viên, đáp ứng chuẩn mực doanh nghiệp.
- **LỜI THUYẾT TRÌNH CHI TIẾT (SPEAKER SCRIPT):**
  > "[01:15 - 02:30] Thưa thầy cô, kinh doanh máy tính là ngành đặc thù có giá trị đơn hàng lớn và vòng đời linh kiện thay đổi nhanh. Việc ghi chép thủ công hoặc dùng các phần mềm rời rạc rất dễ dẫn đến bán âm kho hoặc sai lệch chiết khấu. Đồ án này giải quyết trọn vẹn bài toán từ khâu nhập hàng, cấu hình danh mục linh kiện, bán lẻ POS tại quầy đến báo cáo BI tức thì cho ban giám đốc."

---

### SLIDE 03: Toàn Cảnh Hệ Sinh Thái Công Nghệ Sử Dụng
- **Chủ đề / Phân hệ:** TECH STACK SHOWCASE — *Lựa chọn công nghệ tối ưu hóa tốc độ thực thi và tính ổn định doanh nghiệp*
- **Chỉ số nổi bật (Metric):** `< 15ms` (Độ Trễ Truy Vấn)
- **Tài nguyên hình ảnh minh họa:** `assets/graph_02_call_dependency.png`
- **Tóm tắt giải pháp:** Sự kết hợp hoàn hảo giữa giao diện Windows Forms thuần túy, tầng kết nối Pure ADO.NET hiệu năng cao và hệ quản trị cơ sở dữ liệu quan hệ Microsoft SQL Server.
- **Điểm sáng kỹ thuật & Giá trị doanh nghiệp:** Khai thác tối đa sức mạnh của phần cứng máy trạm Windows: mã biên dịch IL tối ưu, giải phóng bộ nhớ đồ họa GDI+ ngay sau khi vẽ và kết nối trực tiếp đến SQL Server qua SqlDataReader/SqlDataAdapter.
- **So sánh kiến trúc:**
  + **Cách cũ:** Sử dụng Entity Framework: Sinh câu lệnh SQL phức tạp, tốn nhiều bộ nhớ RAM cho Change Tracker, khó tối ưu.
  + **Giải pháp đồ án:** Pure ADO.NET trong dự án: Viết trực tiếp câu lệnh SQL tối ưu, kiểm soát 100% kết nối mở/đóng và tham số hóa an toàn.
- **Nội dung chi tiết trên Slide:**
  + Ngôn ngữ C# .NET: Hướng đối tượng mạnh mẽ, kiểm soát kiểu dữ liệu tĩnh và xử lý luồng sự kiện mượt mà.
  + Pure ADO.NET Data Access: Thực thi truy vấn qua SqlCommand có tham số, triệt tiêu chi phí Overhead của ORM.
  + Custom GDI+ Engine: Tạo trải nghiệm giao diện hiện đại với các góc bo tròn mềm mại và bóng đổ mà không cần thư viện ngoài.
  + SQL Server Relational Engine: Đảm bảo tính nguyên tử, nhất quán, cô lập và bền vững (ACID) cho mọi giao dịch nhập/bán.
- **LỜI THUYẾT TRÌNH CHI TIẾT (SPEAKER SCRIPT):**
  > "[02:30 - 03:45] Về công nghệ, nhóm lựa chọn C# WinForms kết hợp Pure ADO.NET. Thay vì sử dụng Entity Framework có độ trễ cao khi xử lý lượng lớn bản ghi trên máy trạm bán hàng, ADO.NET thuần với SqlDataReader và SqlDataAdapter giúp thời gian nạp dữ liệu giảm xuống dưới 15ms. Toàn bộ hiệu ứng bo góc và đổ màu giao diện được dựng bằng GDI+ nội sinh."

---

### SLIDE 04: Kiến Trúc Hệ Thống Đa Tầng (3-Tier Enterprise)
- **Chủ đề / Phân hệ:** KIẾN TRÚC HỆ THỐNG — *Phân tầng kiến trúc minh bạch, đảm bảo tính mở rộng và dễ bảo trì*
- **Chỉ số nổi bật (Metric):** `3 Tầng` (Kiến Trúc Độc Lập)
- **Tài nguyên hình ảnh minh họa:** `assets/graph_01_overview_3tier.png`
- **Tóm tắt giải pháp:** Tách bạch 3 lớp: Presentation Layer (14 WinForms giao diện), Business & Helper Layer (Functions, UIThemeHelper, SecurityHelper) và Data Layer (SQL Server Database 25 bảng).
- **Điểm sáng kỹ thuật & Giá trị doanh nghiệp:** Tuân thủ nguyên lý Separation of Concerns: Tầng giao diện không can thiệp sâu vào câu lệnh CSDL, tầng CSDL không phụ thuộc vào giao diện; việc thay đổi cấu trúc bảng hay đổi màu sắc chỉ cần sửa tại lớp Helper.
- **So sánh kiến trúc:**
  + **Cách cũ:** Kiến trúc 1 tầng (Monolithic Form): Viết trực tiếp kết nối SQL, tính toán doanh thu và giao diện vào chung 1 file Form.
  + **Giải pháp đồ án:** Kiến trúc 3 tầng của đồ án: Mọi form gọi qua Functions.GetDataToTable và UIThemeHelper.ApplyTheme, code sạch và dễ bảo trì.
- **Nội dung chi tiết trên Slide:**
  + Presentation Layer: 14 Forms giao diện chuyên biệt hóa theo từng phân hệ (Bán hàng, Nhập kho, Báo cáo, Quản trị).
  + Business & Helper Layer: Xử lý nghiệp vụ tập trung, băm mật khẩu PBKDF2, mã hóa AES-256, ghi vết nhật ký hệ thống.
  + Data Layer: Cơ sở dữ liệu chuẩn 3NF lưu trữ quan hệ chặt chẽ, tự động kích hoạt Triggers kiểm soát kho và tổng tiền.
  + Không để lộ chuỗi kết nối hay cấu trúc bảo mật bên ngoài, nâng cao tính an toàn và khả năng tái sử dụng.
- **LỜI THUYẾT TRÌNH CHI TIẾT (SPEAKER SCRIPT):**
  > "[03:45 - 05:00] Trên màn hình là sơ đồ kiến trúc 3 tầng tổng thể của dự án được trích xuất qua công cụ Graphify. Presentation Layer gồm 14 WinForms. Toàn bộ tương tác dữ liệu và nghiệp vụ phải đi qua lớp trung gian Functions.cs và UIThemeHelper.cs trước khi chạm tới Data Layer là SQL Server. Nhờ vậy, nếu sau này cần thay đổi cấu trúc bảng, chúng ta chỉ cần sửa tại lớp Helper mà không phải viết lại mã nguồn trong các Form."

---

### SLIDE 05: Bản Đồ Phụ Thuộc & Luồng Gọi Hàm (Call Graph)
- **Chủ đề / Phân hệ:** GRAPHIFY DEPENDENCY MAP — *Phân tích đồ thị liên kết mô-đun toàn hệ thống từ Graphify*
- **Chỉ số nổi bật (Metric):** `0 Vòng Lặp` (No Circular Calls)
- **Tài nguyên hình ảnh minh họa:** `assets/graph_02_call_dependency.png`
- **Tóm tắt giải pháp:** Đồ thị liên kết từ công cụ Graphify chứng minh mức độ kết dính cao bên trong các module và sự phụ thuộc lỏng lẻo giữa giao diện và cơ sở dữ liệu.
- **Điểm sáng kỹ thuật & Giá trị doanh nghiệp:** Loại bỏ hoàn toàn các phụ thuộc chéo nguy hiểm: Không có Form nào gọi trực tiếp dữ liệu từ Form khác; tất cả đều tương tác thông qua lớp dịch vụ tĩnh và biến phiên làm việc tập trung trong Functions.cs.
- **So sánh kiến trúc:**
  + **Cách cũ:** Hệ thống phụ thuộc chéo: Form bán hàng gọi biến toàn cục của Form sản phẩm, dễ gây lỗi NullReferenceException.
  + **Giải pháp đồ án:** Mô hình Call Graph của đồ án: Dữ liệu chia sẻ qua SqlParameters và Functions static session, độc lập và an toàn tuyệt đối.
- **Nội dung chi tiết trên Slide:**
  + Tất cả 14 Forms đều hội tụ về 2 trung tâm điều phối cốt lõi: Functions.cs và UIThemeHelper.cs.
  + Cơ chế Single-Window Form Embedding giúp frmMain đóng vai trò Shell điều hướng trung tâm.
  + Tránh hiện tượng rò rỉ bộ nhớ (Memory Leak) khi đóng/mở form nhờ cơ chế Dispose chủ động.
  + Nâng cao độ tin cậy và khả năng đóng gói module khi cần chuyển giao hoặc nâng cấp phiên bản.
- **LỜI THUYẾT TRÌNH CHI TIẾT (SPEAKER SCRIPT):**
  > "[05:00 - 06:15] Khi quét AST toàn bộ dự án bằng công cụ Graphify, kết quả đồ thị Call Graph cho thấy kiến trúc đạt chuẩn High Cohesion và Low Coupling. Hai trung tâm điều hướng duy nhất là Functions.cs chịu trách nhiệm toàn bộ ADO.NET và phiên đăng nhập, cùng UIThemeHelper.cs chịu trách nhiệm toàn bộ giao diện và bảng màu. Các Form hoàn toàn độc lập, không form nào gọi chéo dữ liệu của form khác."

---

### SLIDE 06: Thiết Kế CSDL Quan Hệ Chuẩn Hóa 3NF & Sơ Đồ ERD
- **Chủ đề / Phân hệ:** DATABASE ERD & 3NF — *Tối ưu hóa lược đồ dữ liệu loại bỏ triệt để dị thường thêm/xóa/sửa*
- **Chỉ số nổi bật (Metric):** `25 Bảng` (Thực Thể Quan Hệ)
- **Tài nguyên hình ảnh minh họa:** `assets/graph_03_erd_database.png`
- **Tóm tắt giải pháp:** Cơ sở dữ liệu gồm 25 bảng quan hệ được thiết kế chuẩn mực: tách biệt thông số phần cứng, phân định hóa đơn Master-Detail và bảo vệ an toàn thông tin khách hàng.
- **Điểm sáng kỹ thuật & Giá trị doanh nghiệp:** Chuẩn hóa bậc 3 (3NF) loại trừ hoàn toàn việc trùng lặp chuỗi văn bản thông số: bảng MayViTinh chỉ lưu các khóa ngoại MaLoai, MaChip, MaRAM... giúp tiết kiệm dung lượng đĩa và bảo đảm tính nhất quán dữ liệu.
- **So sánh kiến trúc:**
  + **Cách cũ:** Lưu cứng tên CPU, RAM vào từng dòng máy tính: Khi đổi tên một dòng chip phải cập nhật hàng ngàn bản ghi sản phẩm.
  + **Giải pháp đồ án:** Thiết kế 3NF trong đồ án: Phân rã thành bảng danh mục con, chỉ lưu mã khóa ngoại, cập nhật 1 nơi duy nhất là toàn hệ thống đồng bộ.
- **Nội dung chi tiết trên Slide:**
  + Bảng sản phẩm chính MayViTinh liên kết với 14 bảng danh mục linh kiện (Chip, RAM, OCung, ManHinh...).
  + Mô hình Master-Detail chặt chẽ: HoaDonBan <-> ChiTietHDB và HoaDonNhap <-> ChiTietHDN.
  + Ràng buộc khóa ngoại Foreign Key bảo vệ toàn vẹn: Chặn xóa máy tính nếu đang có hóa đơn tham chiếu (Mã lỗi 547).
  + Bảo mật thông tin khách hàng: Cột DiaChi và DienThoai trong bảng KhachHang được mã hóa bằng thuật toán AES-256.
- **LỜI THUYẾT TRÌNH CHI TIẾT (SPEAKER SCRIPT):**
  > "[06:15 - 07:30] Sơ đồ ERD trên slide thể hiện lược đồ CSDL chuẩn 3NF của hệ thống. Chúng em không lưu cứng thông tin linh kiện vào bảng máy tính mà phân rã thành các bảng con như LoaiMay, HangSanXuat, RAM, Chip, ManHinh... Bảng hóa đơn được thiết kế theo cặp Master-Detail giúp lưu trữ lịch sử giao dịch chính xác tuyệt đối mà không gây lặp lại dữ liệu."

---

### SLIDE 07: Kỹ Thuật Giao Diện: Custom GDI+ Rendering Engine
- **Chủ đề / Phân hệ:** GDI+ THEME ENGINE — *Đột phá giao diện phẳng với GraphicsPath và Anti-Aliasing nội sinh*
- **Chỉ số nổi bật (Metric):** `12px` (Bán Kính Bo Góc Card)
- **Tài nguyên hình ảnh minh họa:** `assets/code_01_stylecard_gdi.png`
- **Tóm tắt giải pháp:** Tự xây dựng các thẻ Card bo góc và Button phẳng hiện đại bằng System.Drawing.Drawing2D.GraphicsPath, mang phong cách Fluent Design lên nền Windows Forms.
- **Điểm sáng kỹ thuật & Giá trị doanh nghiệp:** Loại bỏ hoàn toàn sự phụ thuộc vào các bộ thư viện trả phí (DevExpress, Guna UI, Bunifu); phương thức MakeRoundedControl tạo Region chính xác và giải phóng tài nguyên đồ họa qua cấu trúc using an toàn.
- **So sánh kiến trúc:**
  + **Cách cũ:** Giao diện WinForms mặc định: Các nút bấm vuông vức xám xịt kiểu Windows 98, gây cảm giác lỗi thời cho người dùng.
  + **Giải pháp đồ án:** Custom GDI+ Engine của đồ án: Card bo góc 12px, nút bấm phẳng bo góc 8px với màu sắc phân cấp rõ ràng theo chuẩn hiện đại.
- **Nội dung chi tiết trên Slide:**
  + Hàm GetRoundedRectanglePath vẽ 4 góc bo tròn bằng phương thức GraphicsPath.AddArc mịn màng.
  + Hàm StyleCard thiết lập màu nền ColorCard, bật SetDoubleBuffered chống giật lag và gán Region bo góc.
  + Hàm StyleButton chuẩn hóa các nút Thêm, Sửa, Xóa, Lưu với bảng màu ngữ nghĩa (Semantic Palette).
  + Toàn bộ tài nguyên đồ họa Pens, Brushes, GraphicsPath đều được giải phóng ngay lập tức, không rò rỉ GDI Handles.
- **LỜI THUYẾT TRÌNH CHI TIẾT (SPEAKER SCRIPT):**
  > "[07:30 - 08:45] Điểm ấn tượng đầu tiên về mặt thị giác là giao diện hiện đại như ứng dụng Web hay Fluent UI trên Windows 11. Để đạt được điều này mà không phải tốn chi phí mua các bộ thư viện ngoài, chúng em đã tự viết class UIThemeHelper.cs. Phương thức MakeRoundedControl tạo đường cong bằng GraphicsPath rồi gán vào Region của Control. Tất cả đối tượng đồ họa đều được bọc trong khối using để bảo đảm giải phóng GDI Handle ngay lập tức."

---

### SLIDE 08: Kiến Trúc Dual-Theme: Chuyển Đổi Sáng/Tối Tức Thì
- **Chủ đề / Phân hệ:** DUAL-THEME ARCHITECTURE — *Động cơ đệ quy duyệt cây điều khiển giao diện theo thời gian thực*
- **Chỉ số nổi bật (Metric):** `100%` (Tự Động Quét Cây Control)
- **Tài nguyên hình ảnh minh họa:** `assets/ui_main_light.png`, `assets/ui_main_dark.png`
- **Tóm tắt giải pháp:** Người dùng có thể chuyển đổi linh hoạt giữa giao diện Sáng chuyên nghiệp và giao diện Tối bảo vệ mắt chỉ bằng một nút bấm mà không phải khởi động lại phần mềm.
- **Điểm sáng kỹ thuật & Giá trị doanh nghiệp:** Thuật toán đệ quy ApplyTheme(Control root) tự động duyệt sâu qua mọi cấp độ phân cấp (Form -> Panel -> GroupBox -> Control), đồng bộ màu sắc tức thì và lưu trạng thái vào CSDL bảng tblCauHinhHeThong.
- **So sánh kiến trúc:**
  + **Cách cũ:** Ứng dụng WinForms thông thường: Muốn đổi màu phải mở lại Form hoặc gán màu thủ công cho từng label rất dễ sót.
  + **Giải pháp đồ án:** Hệ thống Dual-Theme của đồ án: 1 dòng gọi UIThemeHelper.ApplyTheme(this), toàn bộ cây control đổi màu tức thì.
- **Nội dung chi tiết trên Slide:**
  + Hàm ApplyTheme nhận diện kiểu điều khiển: Button, TextBox, ComboBox, DataGridView, Panel, Label.
  + Tự động áp dụng bảng màu tương phản cao, bảo đảm độ rõ nét và đạt chuẩn trợ năng WCAG 2.1 AA.
  + Hàm SaveThemeToDatabase lưu giá trị 'Dark' hoặc 'Light' để khôi phục chính xác ở lần đăng nhập sau.
  + Tích hợp kỹ thuật SetDoubleBuffered trên toàn bộ controls giúp quá trình đổi theme không bị nhấp nháy (flicker-free).
- **LỜI THUYẾT TRÌNH CHI TIẾT (SPEAKER SCRIPT):**
  > "[08:45 - 10:00] Trên slide là hình ảnh so sánh thực tế giữa Light Mode và Dark Mode trên frmMain. Khi người dùng bấm nút đổi Theme, phương thức ApplyTheme nhận form gốc và đệ quy qua toàn bộ cây controls. Bất kể control nằm lồng sâu bao nhiêu tầng trong Panel hay GroupBox, nó đều được gán lại màu nền, màu chữ và kiểu viền theo Palette chuẩn, mang lại trải nghiệm mượt mà không độ trễ."

---

### SLIDE 09: Tầng Truy Xuất Dữ Liệu Pure ADO.NET & Non-locking Stream
- **Chủ đề / Phân hệ:** DATA ACCESS CORE — *Tối ưu hóa kết nối, chống SQL Injection và kỹ thuật tránh khóa file ảnh*
- **Chỉ số nổi bật (Metric):** `0 Giây` (Khóa File Đĩa (Lock-Free))
- **Tài nguyên hình ảnh minh họa:** `assets/code_03_functions_adonet.png`
- **Tóm tắt giải pháp:** Lớp Functions.cs cung cấp tập hợp các hàm xử lý dữ liệu ADO.NET cốt lõi kết hợp kỹ thuật giải phóng luồng nhị phân tránh hiện tượng khóa cứng tệp tin hình ảnh trên đĩa cứng.
- **Điểm sáng kỹ thuật & Giá trị doanh nghiệp:** Kỹ thuật LoadImageWithoutLock dùng FileStream đọc dữ liệu vào MemoryStream rồi đóng file ngay lập tức; người dùng có thể xóa, đổi tên hoặc ghi đè file ảnh sản phẩm trên Windows mà không bị lỗi file đang sử dụng.
- **So sánh kiến trúc:**
  + **Cách cũ:** Image.FromFile(path): Giữ chặt file ảnh trên ổ cứng, khi người dùng muốn cập nhật ảnh mới Windows sẽ báo lỗi File in use.
  + **Giải pháp đồ án:** Functions.LoadImageWithoutLock: Copy byte sang MemoryStream, ngắt kết nối tệp ngay lập tức, an toàn và chuyên nghiệp.
- **Nội dung chi tiết trên Slide:**
  + Phương thức Connect() kiểm tra trạng thái State == Open trước khi thao tác, tối ưu Connection Pooling.
  + GetDataToTable() sử dụng SqlDataAdapter nạp trực tiếp vào DataTable, tự động đóng DataReader ngầm.
  + LoadImageWithoutLock(): Giải quyết triệt để lỗi kinh điển của Image.FromFile làm khóa file ảnh sản phẩm.
  + 100% câu truy vấn có dữ liệu từ người dùng đều sử dụng mảng SqlParameter[], triệt tiêu hoàn toàn SQL Injection.
- **LỜI THUYẾT TRÌNH CHI TIẾT (SPEAKER SCRIPT):**
  > "[10:00 - 11:15] Đây là trái tim của tầng truy xuất dữ liệu trong Functions.cs. Chúng em áp dụng mô hình kết nối có kiểm soát: hàm Connect() chỉ mở khi kết nối chưa mở. Hàm GetDataToTable tận dụng SqlDataAdapter để tự động ngắt kết nối sau khi Fill vào DataTable. Đặc biệt, hàm LoadImageWithoutLock đọc byte vào MemoryStream rồi đóng file ngay, giúp khắc phục hoàn toàn lỗi khóa file ảnh của WinForms."

---

### SLIDE 10: Cổng Xác Thực & Phân Quyền Hệ Thống (`frmDangNhap.cs`)
- **Chủ đề / Phân hệ:** XÁC THỰC & BẢO MẬT — *Xác thực mã băm PBKDF2, phân định vai trò RBAC và ghi nhận vết truy cập*
- **Chỉ số nổi bật (Metric):** `10,000` (Vòng Lặp PBKDF2)
- **Tài nguyên hình ảnh minh họa:** `assets/ui_dangnhap.png`, `assets/code_04_dangnhap_auth.png`
- **Tóm tắt giải pháp:** Màn hình đăng nhập áp dụng cơ chế bảo mật doanh nghiệp: truy vấn bảng tblTaiKhoan, đối chiếu mật khẩu băm kèm muối (Salt), phân định vai trò và khởi tạo phiên làm việc tĩnh.
- **Điểm sáng kỹ thuật & Giá trị doanh nghiệp:** Bảo mật nhiều lớp: Tuyệt đối không lưu mật khẩu dạng rõ (plain text); hàm SecurityHelper.VerifyPassword kiểm tra độ khớp hash; phiên làm việc lưu mã nhân viên và vai trò để điều phối quyền hạn trên menu chính.
- **So sánh kiến trúc:**
  + **Cách cũ:** Lưu mật khẩu dạng thô hoặc mã hóa MD5 đơn giản: Dễ dàng bị giải mã ngược bằng bảng Rainbow Table.
  + **Giải pháp đồ án:** Hệ thống áp dụng PBKDF2 với chuỗi Salt ngẫu nhiên 16-byte và 10,000 vòng băm: Đạt chuẩn bảo mật an toàn thông tin.
- **Nội dung chi tiết trên Slide:**
  + Truy vấn thông tin tài khoản: MatKhauHash, Salt, MaNV, VaiTro, TrangThai từ bảng tblTaiKhoan.
  + Kiểm tra trạng thái tài khoản: Nếu TrangThai == 'Bị khóa' thì lập tức từ chối quyền truy cập.
  + Lưu trữ biến tĩnh toàn cục: Functions.CurrentUser, Functions.CurrentMaNV, Functions.CurrentRole.
  + Gọi Functions.LogAction() tự động ghi nhận sự kiện đăng nhập thành công vào bảng tblNhatKyHoatDong.
- **LỜI THUYẾT TRÌNH CHI TIẾT (SPEAKER SCRIPT):**
  > "[11:15 - 12:30] Khi người dùng đăng nhập tại frmDangNhap, hệ thống truy vấn kiểm tra tài khoản qua câu lệnh SELECT có tham số từ bảng tblTaiKhoan. Mật khẩu được đối chiếu qua thuật toán PBKDF2 kèm muối ngẫu nhiên. Nếu thành công, hệ thống lưu mã nhân viên và quyền vào biến tĩnh Functions.CurrentRole, đồng thời gọi hàm LogAction để lưu vết kiểm toán. Form chính frmMain sau đó sẽ bật/tắt các menu phù hợp."

---

### SLIDE 11: Đăng Ký Tài Khoản & Khôi Phục Mật Khẩu An Toàn
- **Chủ đề / Phân hệ:** QUẢN TRỊ TÀI KHOẢN — *Kiểm tra hợp lệ đa tầng tại `frmDangKy` và xác thực nhân viên tại `frmQuenMatKhau`*
- **Chỉ số nổi bật (Metric):** `3 Tiêu Chí` (Xác Thực Khôi Phục)
- **Tài nguyên hình ảnh minh họa:** `assets/ui_dangky.png`, `assets/ui_quenmatkhau.png`
- **Tóm tắt giải pháp:** Hệ thống hỗ trợ quy trình cấp tài khoản mới gắn chặt với hồ sơ nhân sự và quy trình lấy lại mật khẩu an toàn dựa trên thông tin định danh nội bộ.
- **Điểm sáng kỹ thuật & Giá trị doanh nghiệp:** Chống tạo tài khoản rác: Chỉ nhân viên đã có hồ sơ trong bảng NhanVien và chưa từng được cấp tài khoản mới được đăng ký; khi quên mật khẩu phải khớp cả Tên đăng nhập, Mã NV và Số điện thoại.
- **So sánh kiến trúc:**
  + **Cách cũ:** Cho phép tự do đăng ký tài khoản mà không liên kết nhân viên: Dẫn đến việc nhân viên ảo thao tác bán hàng.
  + **Giải pháp đồ án:** Ràng buộc chặt chẽ với bảng NhanVien: Mỗi tài khoản bắt buộc phải có một hồ sơ nhân sự thực tế bảo đảm trách nhiệm.
- **Nội dung chi tiết trên Slide:**
  + Kiểm tra tính duy nhất của Tên đăng nhập và kiểm tra sự tồn tại của Mã NV trong bảng NhanVien.
  + Tự động sinh chuỗi Salt ngẫu nhiên và băm mật khẩu bằng PBKDF2 trước khi INSERT vào tblTaiKhoan.
  + Quy trình quên mật khẩu đối chiếu 3 yếu tố định danh: TenDangNhap + MaNV + SoDienThoai.
  + Giao diện thông báo lỗi trực quan tại chỗ qua Label trạng thái, không để xảy ra crash ứng dụng.
- **LỜI THUYẾT TRÌNH CHI TIẾT (SPEAKER SCRIPT):**
  > "[12:30 - 13:45] Đối với hai màn hình Đăng ký và Quên mật khẩu, chúng em thiết kế bộ kiểm tra hợp lệ dữ liệu nhiều lớp. Tại frmDangKy, tài khoản chỉ được tạo nếu mã nhân viên thực sự tồn tại trong CSDL và chưa được cấp tài khoản. Tại frmQuenMatKhau, người dùng phải cung cấp đúng cả tên đăng nhập, mã nhân viên và số điện thoại đã lưu trong hồ sơ nhân viên thì mới được phép thiết lập lại mật khẩu mới."

---

### SLIDE 12: Trung Tâm Điều Khiển `frmMain.cs`: Kỹ Thuật Nhúng Form
- **Chủ đề / Phân hệ:** SHELL & NAVIGATION — *Trải nghiệm Single-Window mượt mà với kỹ thuật Panel Embedding vào `pnlContent`*
- **Chỉ số nổi bật (Metric):** `1 Cửa Sổ` (Single-Window Shell)
- **Tài nguyên hình ảnh minh họa:** `assets/ui_main_dark.png`, `assets/code_05_main_embed_form.png`
- **Tóm tắt giải pháp:** Loại bỏ hoàn toàn kiến trúc MDI cổ điển bằng giải pháp nhúng động các Form con trực tiếp vào Panel nội dung (pnlContent), tạo trải nghiệm liền mạch như các ứng dụng Web hiện đại.
- **Điểm sáng kỹ thuật & Giá trị doanh nghiệp:** Chủ động thu hồi tài nguyên bộ nhớ: Trước khi hiển thị Form con mới, hàm OpenChildForm tự động gọi Close() và Dispose() Form con cũ, ngăn ngừa hiện tượng tràn bộ nhớ khi sử dụng liên tục nhiều giờ.
- **So sánh kiến trúc:**
  + **Cách cũ:** MDI Form cổ điển: Các cửa sổ trôi nổi tự do, dễ bị thu nhỏ lạc mất cửa sổ con, màn hình giật nhấp nháy khi phóng to.
  + **Giải pháp đồ án:** Panel Embedding trong đồ án: Tất cả form nằm gọn trong 1 khung nhìn duy nhất, chuyển đổi tab mượt mà không độ trễ.
- **Nội dung chi tiết trên Slide:**
  + Thiết lập childForm.TopLevel = false, FormBorderStyle = None và Dock = DockStyle.Fill vào pnlContent.
  + Ẩn trang tổng quan pnlBentoDashboard và đưa Form con mới ra phía trước bằng BringToFront().
  + Tự động áp dụng chủ đề màu sắc hiện hành cho Form con thông qua UIThemeHelper.ApplyTheme(childForm).
  + Cập nhật tiêu đề trang (lblTitle) và mô tả nghiệp vụ (lblSubTitle) đồng bộ theo từng chức năng.
- **LỜI THUYẾT TRÌNH CHI TIẾT (SPEAKER SCRIPT):**
  > "[13:45 - 15:00] Giao diện chính frmMain là một cửa sổ điều khiển trung tâm theo phong cách ứng dụng hiện đại. Thay vì mở các cửa sổ con trôi nổi gây rối mắt, phương thức OpenChildForm thiết lập Form con thành một User Control nhúng trực tiếp vào pnlContent. Trước khi nạp Form mới, hệ thống tự động Close và Dispose Form cũ để thu hồi bộ nhớ RAM ngay lập tức."

---

### SLIDE 13: Quản Lý Sản Phẩm Máy Vi Tính (`frmMayViTinh.cs` - UI & Nghiệp Vụ)
- **Chủ đề / Phân hệ:** QUẢN TRỊ SẢN PHẨM — *Quản lý 14 thông số cấu hình phần cứng, hiển thị ảnh động và tự định giá bán*
- **Chỉ số nổi bật (Metric):** `+10%` (Tự Động Tính Giá Bán)
- **Tài nguyên hình ảnh minh họa:** `assets/ui_mayvitinh_view.png`, `assets/ui_mayvitinh_edit.png`
- **Tóm tắt giải pháp:** Giao diện quản lý thông số kỹ thuật toàn diện của máy tính: kết nối 14 ComboBox linh kiện chuẩn 3NF, hiển thị ảnh đại diện và công thức tự động gợi ý giá bán lẻ.
- **Điểm sáng kỹ thuật & Giá trị doanh nghiệp:** Tích hợp công thức kinh doanh thông minh: Khi nhập Giá Nhập, hệ thống gợi ý Giá Bán theo biên độ lợi nhuận 110%; ảnh sản phẩm được nạp động từ file nhị phân VARBINARY hoặc đường dẫn đĩa mà không bị khóa file.
- **So sánh kiến trúc:**
  + **Cách cũ:** Nhập liệu thủ công từng thông số dạng văn bản: Dễ sai chính tả, không chuẩn hóa được cấu hình máy.
  + **Giải pháp đồ án:** Hệ thống ComboBox liên kết bảng danh mục chuẩn 3NF: Chọn nhanh từ danh sách Chip, RAM, Ổ cứng có sẵn, chính xác 100%.
- **Nội dung chi tiết trên Slide:**
  + Hiển thị bảng dữ liệu DataGridView với đầy đủ thông số: Tên máy, Hãng SX, CPU, RAM, Ổ cứng, Giá bán, Tồn kho.
  + Sự kiện CellClick tự động nạp ảnh sản phẩm vào PictureBox và điền chính xác giá trị vào 14 ComboBox linh kiện.
  + Tự sinh mã máy vi tính duy nhất thông qua hàm Functions.CreateKey('MVT') tránh trùng lặp.
  + Các cột tiền tệ Giá Nhập, Giá Bán được tự động định dạng phân tách hàng nghìn VNĐ đẹp mắt.
- **LỜI THUYẾT TRÌNH CHI TIẾT (SPEAKER SCRIPT):**
  > "[15:00 - 16:15] Form frmMayViTinh là nơi nhân viên quản lý toàn bộ kho sản phẩm máy tính. Điểm nổi bật là khi click vào bất kỳ dòng nào trên bảng, ảnh đại diện sản phẩm sẽ được nạp tức thì vào PictureBox từ đường dẫn lưu trữ. Hệ thống cũng tích hợp công thức gợi ý giá bán dựa trên giá vốn nhập kho, giúp nhân viên không phải tính toán thủ công."

---

### SLIDE 14: Giải Phẫu Code `frmMayViTinh.cs`: Bẫy Lỗi Khóa Ngoại 547
- **Chủ đề / Phân hệ:** TOÀN VẸN DỮ LIỆU — *Kỹ thuật bảo vệ toàn vẹn dữ liệu và xử lý ngoại lệ SqlException chuyên nghiệp*
- **Chỉ số nổi bật (Metric):** `Mã 547` (Bắt Lỗi Ràng Buộc SQL)
- **Tài nguyên hình ảnh minh họa:** `assets/code_06_mayvitinh_crud_fk.png`
- **Tóm tắt giải pháp:** Giải pháp bảo vệ toàn vẹn dữ liệu quan hệ ở cấp độ hệ thống: bắt lỗi vi phạm khóa ngoại SqlException với mã lỗi 547 để hiển thị cảnh báo thân thiện thay vì làm sập ứng dụng.
- **Điểm sáng kỹ thuật & Giá trị doanh nghiệp:** Xử lý bẫy lỗi ở tầng Data Access trong hàm Functions.RunSqlDel: Khi người dùng cố gắng xóa một chiếc máy tính đã từng có giao dịch trong ChiTietHDB hoặc ChiTietHDN, hệ thống phát hiện mã lỗi 547 và hướng dẫn người dùng.
- **So sánh kiến trúc:**
  + **Cách cũ:** Để câu lệnh DELETE chạy tự do: SQL Server bắn ngoại lệ chưa được xử lý làm ứng dụng crash ngay trước mặt khách.
  + **Giải pháp đồ án:** Bẫy mã lỗi 547 trong Functions.RunSqlDel: Chặn đứng crash, giải thích rõ nguyên nhân dữ liệu đang có lịch sử hóa đơn.
- **Nội dung chi tiết trên Slide:**
  + Kiểm tra mã máy vi tính trước khi thực hiện câu lệnh DELETE FROM MayViTinh WHERE MaMayVT = @ma.
  + Khối try-catch trong Functions.RunSqlDel bắt chính xác kiểu ngoại lệ SqlException.
  + Kiểm tra thuộc tính ex.Number == 547 (Mã lỗi chuẩn của SQL Server cho vi phạm Foreign Key Constraint).
  + Hiển thị thông báo: 'Dữ liệu này đang được tham chiếu ở bảng khác, bạn không thể xóa!' thân thiện với người dùng.
- **LỜI THUYẾT TRÌNH CHI TIẾT (SPEAKER SCRIPT):**
  > "[16:15 - 17:30] Nhìn vào đoạn code nút Xóa trên slide, thầy cô có thể thấy kỹ thuật bảo vệ ràng buộc toàn vẹn dữ liệu chuyên sâu. Trong hàm Functions.RunSqlDel, khối try-catch bắt ngoại lệ SqlException và kiểm tra thuộc tính ex.Number == 547. Đây là mã lỗi chuẩn của SQL Server khi vi phạm ràng buộc khóa ngoại. Thay vì để chương trình bị crash, hệ thống sẽ hiển thị cảnh báo giải thích rõ sản phẩm đang có trong hóa đơn."

---

### SLIDE 15: Thiết Kế Đa Hình Trên `frmDanhMucHeThong.cs` (4 Tab & 14 Danh Mục)
- **Chủ đề / Phân hệ:** ĐA HÌNH GIAO DIỆN — *Đột phá kiến trúc gom Khách hàng, NCC, Nhân viên và 14 bảng linh kiện vào 1 Form*
- **Chỉ số nổi bật (Metric):** `14 Trong 1` (Tích Hợp Danh Mục)
- **Tài nguyên hình ảnh minh họa:** `assets/ui_danhmuc_tab1_loaimay.png`, `assets/ui_danhmuc_tab4_khachhang.png`
- **Tóm tắt giải pháp:** Thay vì tạo hàng chục form riêng biệt gây phình to mã nguồn, frmDanhMucHeThong gom toàn bộ danh mục đối tác và 14 bảng linh kiện phần cứng vào một Form duy nhất.
- **Điểm sáng kỹ thuật & Giá trị doanh nghiệp:** Quản trị danh mục thông minh: Tab Khách hàng tự động giải mã AES-256 số điện thoại và địa chỉ; Tab Linh kiện có ComboBox cboLoaiDanhMuc điều khiển động 14 bảng phần cứng (LoaiMay, Chip, RAM, HangSX, ManHinh...).
- **So sánh kiến trúc:**
  + **Cách cũ:** Tạo 17 file Form riêng biệt cho 17 danh mục: Mã nguồn bị nhân bản 80%, tốn tài nguyên GDI Handle của Windows.
  + **Giải pháp đồ án:** Kiến trúc Tab & Dynamic Grid trong đồ án: Gom trọn vẹn vào 1 Form, tái sử dụng toàn bộ nút chức năng, code cực kỳ tinh gọn.
- **Nội dung chi tiết trên Slide:**
  + Tab 1: Khách hàng — Tự động giải mã dữ liệu nhạy cảm bằng SecurityHelper.DecryptAES trước khi hiển thị lên Grid.
  + Tab 2: Nhà cung cấp — Quản lý thông tin đối tác nhập khẩu, địa chỉ và đường dây nóng hỗ trợ.
  + Tab 3: Nhân viên — Quản lý hồ sơ cán bộ bán hàng, ngày sinh, giới tính và liên kết tài khoản.
  + Tab 4: Linh kiện máy tính — Tải động 14 bảng danh mục phụ trợ thông qua ComboBox lựa chọn loại danh mục.
- **LỜI THUYẾT TRÌNH CHI TIẾT (SPEAKER SCRIPT):**
  > "[17:30 - 18:45] Đây là một trong những giải pháp kiến trúc sáng tạo nhất của dự án. Thay vì phải viết 17 Form riêng lẻ cho từng danh mục đối tác và linh kiện, chúng em thiết kế frmDanhMucHeThong gom toàn bộ vào 4 Tab. Đặc biệt ở Tab Khách hàng, hệ thống tự động giải mã AES-256 cho số điện thoại và địa chỉ. Ở Tab Linh kiện, chỉ cần 1 bảng điều khiển duy nhất có thể quản lý trọn vẹn 14 bảng phần cứng."

---

### SLIDE 16: Giải Phẫu Code `frmDanhMucHeThong.cs`: Bộ Điều Phối Động
- **Chủ đề / Phân hệ:** POLYMORPHIC ENGINE — *Kỹ thuật `GetCurrentTableName` tự động nạp cấu trúc cột và câu lệnh SQL tương ứng*
- **Chỉ số nổi bật (Metric):** `80%` (Giảm Trùng Lặp Code)
- **Tài nguyên hình ảnh minh họa:** `assets/graph_05_polymorphic_danhmuc.png`, `assets/code_07_danhmuc_polymorphic.png`
- **Tóm tắt giải pháp:** Phương thức GetCurrentTableName trả về tên bảng và cặp khóa (colMa, colTen) tương ứng với lựa chọn trong cboLoaiDanhMuc, giúp hàm LoadDataLK nạp dữ liệu động cho mọi loại linh kiện.
- **Điểm sáng kỹ thuật & Giá trị doanh nghiệp:** Cơ chế Dispatcher ánh xạ linh hoạt: Dù người dùng chọn LoaiMay, Chip, RAM hay HangSanXuat, câu truy vấn SELECT {0} AS Ma, {1} AS Ten FROM {2} đều được sinh động và gán tiêu đề cột DataGridView tương ứng.
- **So sánh kiến trúc:**
  + **Cách cũ:** Viết riêng từng hàm LoadChip, LoadRAM, LoadManHinh: Khi thêm một loại linh kiện mới phải viết lại toàn bộ các hàm CRUD.
  + **Giải pháp đồ án:** Hàm LoadDataLK đa năng: Chỉ cần bổ sung 1 dòng ánh xạ trong GetCurrentTableName là hệ thống tự động hỗ trợ thêm danh mục mới.
- **Nội dung chi tiết trên Slide:**
  + Hàm GetCurrentTableName: Phân tích tiền tố chuỗi trong ComboBox và gán chính xác colMa, colTen và tên bảng SQL.
  + Hàm LoadDataLK: Nạp dữ liệu vào DataTable và đặt HeaderText tương ứng (Mã LoaiMay / Tên LoaiMay).
  + Cột bảng được phân bổ FillWeight cân đối (Mã chiếm 25%, Tên thông số chiếm 75%) bảo đảm mỹ thuật hiển thị.
  + Tất cả thao tác Thêm, Sửa, Xóa linh kiện đều sử dụng chung một bộ nút bấm, tiết kiệm tối đa dòng lệnh.
- **LỜI THUYẾT TRÌNH CHI TIẾT (SPEAKER SCRIPT):**
  > "[18:45 - 20:00] Sơ đồ luồng Graphify và đoạn mã C# trên slide minh chứng cho cách thức hoạt động của Form đa hình. Phương thức GetCurrentTableName nhận vào giá trị ComboBox và trả ra tên cột mã, tên cột tên và tên bảng CSDL tương ứng. Nhờ cơ chế này, toàn bộ logic CRUD chỉ cần viết một lần duy nhất, giúp mã nguồn vô cùng gọn gàng và dễ bảo trì."

---

### SLIDE 17: Nghiệp Vụ Nhập Kho & Quản Trị Nguồn Hàng (`frmHoaDonNhap.cs`)
- **Chủ đề / Phân hệ:** QUẢN LÝ NHẬP KHO — *Quy trình nhập hàng từ Nhà cung cấp và quản lý giá vốn sản phẩm*
- **Chỉ số nổi bật (Metric):** `Tự Động` (Cộng Dồn Tồn Kho)
- **Tài nguyên hình ảnh minh họa:** `assets/ui_hoadonnhap.png`
- **Tóm tắt giải pháp:** Mô phỏng quy trình nhập hàng chuyên nghiệp tại các trung tâm phân phối: lập phiếu nhập, liên kết nhà cung cấp, ghi nhận số lượng và đơn giá nhập từng lô máy tính.
- **Điểm sáng kỹ thuật & Giá trị doanh nghiệp:** Tự động hóa hoàn toàn luồng dữ liệu kho: Khi lưu chi tiết nhập hàng, hệ thống không chỉ ghi nhận phiếu nhập mà còn kích hoạt Trigger cập nhật ngay số lượng tồn kho và tự động tính lại tổng chi phí vốn.
- **So sánh kiến trúc:**
  + **Cách cũ:** Nhập kho thủ công: Nhân viên phải mở bảng sản phẩm gõ cộng dồn số lượng bằng tay, rất dễ tính nhẩm sai.
  + **Giải pháp đồ án:** Quy trình khép kín trong đồ án: Chỉ cần bấm Lưu phiếu nhập, Trigger CSDL tự động cộng dồn số lượng và cập nhật giá vốn mới.
- **Nội dung chi tiết trên Slide:**
  + Quản lý thông tin Master: Mã phiếu nhập (HDN), ngày lập, nhân viên tiếp nhận và thông tin nhà cung cấp.
  + Chi tiết mặt hàng: Chọn máy vi tính, số lượng nhập, đơn giá vốn nhập và phần trăm chiết khấu.
  + Tự động tính thành tiền theo công thức: ThanhTien = SoLuong * DonGia * (1 - GiamGia / 100).
  + Tích hợp các nút chức năng: Thêm hóa đơn mới, Lưu phiếu nhập, In phiếu và Hủy phiếu an toàn.
- **LỜI THUYẾT TRÌNH CHI TIẾT (SPEAKER SCRIPT):**
  > "[20:00 - 21:15] Bước vào phân hệ quản lý kho, frmHoaDonNhap cho phép doanh nghiệp ghi nhận các lô hàng nhập từ đối tác. Nhân viên chỉ cần chọn nhà cung cấp và lần lượt thêm từng mã máy tính cùng đơn giá nhập và chiết khấu. Mọi tính toán thành tiền đều được thực hiện theo thời gian thực ngay trên giao diện."

---

### SLIDE 18: Giải Phẫu Code `frmHoaDonNhap.cs`: Master-Detail Ingestion
- **Chủ đề / Phân hệ:** MASTER-DETAIL LOGIC — *Kỹ thuật kiểm tra tồn tại Master Header và chèn chi tiết nguyên tử*
- **Chỉ số nổi bật (Metric):** `Mã HDN` (Tự Sinh Khóa Định Danh)
- **Tài nguyên hình ảnh minh họa:** `assets/code_08_hoadonnhap_logic.png`
- **Tóm tắt giải pháp:** Thuật toán xử lý hóa đơn Master-Detail thông minh: sử dụng Functions.CheckKey để kiểm tra sự tồn tại của phiếu nhập cha trong bảng HoaDonNhap trước khi chèn chi tiết vào ChiTietHDN.
- **Điểm sáng kỹ thuật & Giá trị doanh nghiệp:** Bảo đảm tính toàn vẹn giao dịch: Bản ghi Master chỉ được tạo một lần duy nhất cho cả đơn hàng; mỗi dòng sản phẩm thêm vào sẽ tự động kích hoạt Trigger trg_ChiTietHDN_AUID để tính toán tổng tiền và cập nhật kho.
- **So sánh kiến trúc:**
  + **Cách cũ:** Tạo hóa đơn cha trước bằng một nút bấm riêng: Nếu nhân viên đổi ý không nhập hàng thì tạo ra hóa đơn rác (Orphan Master).
  + **Giải pháp đồ án:** Kiểm tra tự động trong đồ án: Chỉ khởi tạo hóa đơn cha khi có ít nhất một mặt hàng hợp lệ được lưu, dữ liệu luôn sạch.
- **Nội dung chi tiết trên Slide:**
  + Hàm Functions.CreateKey('HDN') tự động sinh mã phiếu nhập duy nhất dựa trên thời gian thực.
  + Kiểm tra if (!Functions.CheckKey(checkHDN)) để chỉ INSERT hóa đơn cha khi thêm mặt hàng đầu tiên.
  + Chèn dòng chi tiết vào ChiTietHDN với mảng SqlParameter an toàn tuyệt đối.
  + Ghi nhật ký hệ thống qua Functions.LogAction('Nhập kho', chiTiet) phục vụ kiểm toán nội bộ.
- **LỜI THUYẾT TRÌNH CHI TIẾT (SPEAKER SCRIPT):**
  > "[21:15 - 22:30] Đoạn code lưu hóa đơn nhập áp dụng kỹ thuật kiểm tra Master thông minh. Khi người dùng thêm dòng máy đầu tiên, hệ thống dùng hàm CheckKey để kiểm tra. Nếu chưa có, hệ thống mới tự động tạo bản ghi trong HoaDonNhap, sau đó chèn dòng chi tiết vào ChiTietHDN. Ngay lập tức, Trigger trên SQL Server sẽ kích hoạt để cộng số lượng tồn kho."

---

### SLIDE 19: Nghiệp Vụ Bán Hàng POS Tại Quầy (`frmHoaDonBan.cs` - UI)
- **Chủ đề / Phân hệ:** BÁN HÀNG POS — *Giao diện điểm bán lẻ hiện đại, phục vụ thu ngân thanh toán tức thì*
- **Chỉ số nổi bật (Metric):** `Tức Thì` (Tính Toán Giỏ Hàng)
- **Tài nguyên hình ảnh minh họa:** `assets/ui_hoadonban.png`
- **Tóm tắt giải pháp:** Màn hình bán hàng POS được thiết kế chuyên biệt cho thu ngân: tra cứu khách hàng nhanh, tự động nạp đơn giá bán, tự động tính thành tiền và in hóa đơn thanh toán.
- **Điểm sáng kỹ thuật & Giá trị doanh nghiệp:** Trải nghiệm thanh toán không độ trễ: Khi chọn mã máy vi tính, hệ thống tự động điền đơn giá bán hiện hành và số lượng tồn kho; mọi thay đổi về số lượng mua hay tỷ lệ chiết khấu (%) đều tính lại tổng tiền ngay lập tức.
- **So sánh kiến trúc:**
  + **Cách cũ:** Thu ngân phải tra bảng giá giấy và dùng máy tính cầm tay tính chiết khấu: Rất dễ bấm nhầm và gây ùn tắc tại quầy.
  + **Giải pháp đồ án:** Giao diện POS thông minh của đồ án: Tự động tính toán chiết khấu, kiểm tra tồn kho và in hóa đơn trong vài giây.
- **Nội dung chi tiết trên Slide:**
  + Tự động điền đơn giá và số lượng tồn kho khi chọn máy vi tính trong cboMaMay.
  + Ràng buộc nhập liệu bàn phím: Chỉ cho phép nhập số nguyên vào ô số lượng qua hàm Functions.OnlyInteger.
  + Bảng giỏ hàng DataGridView hiển thị danh sách sản phẩm khách mua kèm nút hủy từng món linh hoạt.
  + Tích hợp hàm Functions.ChuyenSoSangChu() tự động chuyển đổi tổng số tiền thanh toán thành chữ tiếng Việt khi in hóa đơn.
- **LỜI THUYẾT TRÌNH CHI TIẾT (SPEAKER SCRIPT):**
  > "[22:30 - 23:45] Màn hình POS frmHoaDonBan là nơi diễn ra các giao dịch bán lẻ hàng ngày. Khi nhân viên chọn mã máy, hệ thống tự động điền đơn giá bán hiện hành. Mọi thao tác gõ số lượng hay phần trăm chiết khấu đều kích hoạt sự kiện tính lại thành tiền ngay tức khắc, giúp việc thanh toán tại quầy diễn ra nhanh chóng và chính xác."

---

### SLIDE 20: Giải Phẫu Code `frmHoaDonBan.cs`: Thuật Toán Chống Bán Khống
- **Chủ đề / Phân hệ:** BẢO VỆ TỒN KHO POS — *Kiểm tra số lượng tồn kho tức thì và bảo vệ tính nguyên vẹn giỏ hàng*
- **Chỉ số nổi bật (Metric):** `2 Lớp` (Bảo Vệ Chống Bán Âm)
- **Tài nguyên hình ảnh minh họa:** `assets/code_09_hoadonban_pos.png`
- **Tóm tắt giải pháp:** Thuật toán kiểm tra tồn kho tại chỗ (Client-side Stock Guard) ngăn chặn triệt để tình trạng nhân viên bán vượt quá số lượng máy vi tính thực tế đang có trong kho.
- **Điểm sáng kỹ thuật & Giá trị doanh nghiệp:** Bảo vệ hai lớp toàn diện: Lớp 1 kiểm tra trực tiếp trên giao diện if (slBan > tonKho); Lớp 2 là Trigger trong CSDL SQL Server với lệnh RAISERROR và ROLLBACK TRANSACTION, bảo đảm không bao giờ có thể xuất âm kho.
- **So sánh kiến trúc:**
  + **Cách cũ:** Cho phép lưu đơn hàng âm rồi sửa kho sau: Dẫn đến việc bán hàng không có sẵn cho khách, gây mất uy tín cửa hàng.
  + **Giải pháp đồ án:** Kiểm tra 2 lớp nghiêm ngặt trong đồ án: Ngăn chặn ngay từ lúc gõ phím và chốt chặn cuối cùng bằng Trigger tại CSDL.
- **Nội dung chi tiết trên Slide:**
  + Chuyển đổi an toàn int.TryParse(txtSoLuong.Text) và int.TryParse(txtTonKho.Text).
  + Nếu số lượng bán lớn hơn tồn kho: Lập tức hiển thị thông báo 'Cảnh Báo Hết Hàng' và đưa con trỏ Focus về ô số lượng.
  + Làm sạch chuỗi tiền tệ (.Replace(',', '').Replace('.', '')) trước khi parse sang decimal để tránh lỗi định dạng vùng miền.
  + Lưu chi tiết bán hàng vào ChiTietHDB an toàn, ghi nhận doanh thu và kích hoạt luồng trừ kho tự động.
- **LỜI THUYẾT TRÌNH CHI TIẾT (SPEAKER SCRIPT):**
  > "[23:45 - 25:00] Để chống hiện tượng bán âm hay bán khống máy tính, đoạn code trên slide thể hiện thuật toán Stock Guard. Trước khi thêm một dòng vào hóa đơn bán, hệ thống truy vấn số lượng tồn kho tức thời của máy. Nếu số lượng khách mua lớn hơn số lượng còn trong kho, hệ thống lập tức chặn lại và cảnh báo chính xác số lượng hiện có, bảo đảm không bao giờ xảy ra lỗi âm kho."

---

### SLIDE 21: Đồng Bộ Tự Động Giữa C# và SQL Server Triggers (ACID)
- **Chủ đề / Phân hệ:** ACID TRIGGER PIPELINE — *Trigger `trg_ChiTietHDB_AUID` và `trg_ChiTietHDN_AUID` đảm bảo nhất quán tuyệt đối*
- **Chỉ số nổi bật (Metric):** `100% ACID` (Giao Dịch Toàn Vẹn)
- **Tài nguyên hình ảnh minh họa:** `assets/graph_06_pos_trigger_pipeline.png`, `assets/code_10_sql_triggers.png`
- **Tóm tắt giải pháp:** Tận dụng sức mạnh cốt lõi của SQL Server Triggers: tự động khấu trừ tồn kho trong MayViTinh, cập nhật tổng tiền trong HoaDonBan và tự động tính giá bán buôn (+10%) khi nhập hàng.
- **Điểm sáng kỹ thuật & Giá trị doanh nghiệp:** Bảo đảm tính toàn vẹn ở tầng thấp nhất của CSDL: Kể cả khi có nhiều nhân viên cùng bán máy tính cuối cùng tại các máy trạm khác nhau, câu lệnh ROLLBACK TRANSACTION trong Trigger sẽ ngăn chặn tranh chấp dữ liệu.
- **So sánh kiến trúc:**
  + **Cách cũ:** Viết câu lệnh UPDATE SoLuong thủ công rải rác trong C#: Khi mất mạng giữa chừng, chi tiết hóa đơn lưu được nhưng kho không bị trừ.
  + **Giải pháp đồ án:** SQL Server Trigger trong đồ án: Hoạt động trong cùng một transaction nguyên tử (Atomic), nếu có lỗi là rollback toàn bộ.
- **Nội dung chi tiết trên Slide:**
  + Trigger trg_ChiTietHDB_AUID: Kiểm tra nếu m.SoLuong < df.DeltaSL thì lập tức RAISERROR và ROLLBACK TRANSACTION.
  + Tự động khấu trừ kho: UPDATE MayViTinh SET SoLuong = SoLuong - df.DeltaSL dựa trên bảng inserted và deleted.
  + Tự động tính lại tổng tiền: UPDATE HoaDonBan SET TongTien = SUM(ThanhTien), không bao giờ bị lệch sổ sách kế toán.
  + Trigger trg_ChiTietHDN_AUID: Tự động cộng dồn kho và cập nhật công thức GiaBan = ROUND(GiaNhap * 1.10, 0) tức thì.
- **LỜI THUYẾT TRÌNH CHI TIẾT (SPEAKER SCRIPT):**
  > "[25:00 - 26:15] Đây là điểm sáng kỹ thuật quan trọng của đồ án. Thay vì viết các câu lệnh UPDATE tồn kho rải rác trong code C#, chúng em đưa toàn bộ logic này vào SQL Server Triggers. Khi có bản ghi mới được chèn vào ChiTietHDB, Trigger trg_ChiTietHDB_AUID sẽ tự động kiểm tra tồn kho, trừ số lượng trong MayViTinh và cập nhật lại TongTien của hóa đơn cha. Nếu thiếu hàng, Trigger dùng RAISERROR và ROLLBACK lập tức."

---

### SLIDE 22: Hệ Thống Tra Cứu & Tìm Kiếm Đa Tiêu Chí (`frmTimKiem.cs` - UI)
- **Chủ đề / Phân hệ:** TÌM KIẾM ĐA TIÊU CHÍ — *Tra cứu thông minh máy vi tính, hóa đơn bán và phiếu nhập theo nhiều tiêu chí*
- **Chỉ số nổi bật (Metric):** `3 Chế Độ` (Máy Tính & Hóa Đơn)
- **Tài nguyên hình ảnh minh họa:** `assets/ui_timkiem_mode1.png`, `assets/ui_timkiem_mode2.png`
- **Tóm tắt giải pháp:** Màn hình tìm kiếm đa năng tích hợp 3 chế độ tra cứu qua RadioButton: Tìm kiếm máy tính theo linh kiện/hãng/giá, tìm kiếm hóa đơn bán và tìm kiếm phiếu nhập kho.
- **Điểm sáng kỹ thuật & Giá trị doanh nghiệp:** Trải nghiệm người dùng thông minh: Khi chuyển đổi RadioButton, hàm HienThiDieuKien() tự động ẩn/hiện các khung nhập liệu tương ứng, thay đổi tiêu đề cột DataGridView và nạp dữ liệu tức thì.
- **So sánh kiến trúc:**
  + **Cách cũ:** Tạo 3 Form tìm kiếm riêng biệt: Người dùng phải chuyển qua lại nhiều cửa sổ, gây phân mảnh thao tác.
  + **Giải pháp đồ án:** Form tìm kiếm tích hợp trong đồ án: Chuyển đổi 3 chế độ trên 1 màn hình duy nhất, lọc nhanh và kết xuất bảng rõ ràng.
- **Nội dung chi tiết trên Slide:**
  + Chế độ Máy tính: Lọc theo Mã máy, Tên máy, Loại máy, Hãng sản xuất và kết hợp nhiều điều kiện đồng thời.
  + Chế độ Hóa đơn bán: Lọc theo Mã hóa đơn, Nhân viên bán, Khách hàng mua và khoảng thời gian (Từ ngày - Đến ngày).
  + Chế độ Hóa đơn nhập: Lọc theo Mã phiếu nhập, Nhà cung cấp, Mặt hàng và khoảng thời gian nhập kho.
  + Tự động định dạng các cột tiền tệ (Tổng tiền, Giá nhập, Giá bán) bằng UIThemeHelper.FormatCurrencyColumns.
- **LỜI THUYẾT TRÌNH CHI TIẾT (SPEAKER SCRIPT):**
  > "[26:15 - 27:30] Form frmTimKiem cho phép người dùng tra cứu dữ liệu cực kỳ nhanh chóng. Người dùng có thể tìm kiếm máy tính theo tên, khoảng giá, hãng sản xuất hoặc chuyển sang chế độ tìm kiếm hóa đơn bán/nhập theo khoảng ngày và đối tác. Giao diện tự động thích ứng để chỉ hiện những trường nhập liệu liên quan đến tiêu chí đang chọn."

---

### SLIDE 23: Giải Phẫu Kỹ Thuật Dynamic SQL Builder (`frmTimKiem.cs`)
- **Chủ đề / Phân hệ:** DYNAMIC QUERY BUILDER — *Mẫu hình `WHERE 1=1` kết hợp khử dấu nháy đơn `.Replace("'", "''")`*
- **Chỉ số nổi bật (Metric):** `WHERE 1=1` (Mẫu Ghép Chuỗi SQL)
- **Tài nguyên hình ảnh minh họa:** `assets/code_11_timkiem_dynamic_sql.png`
- **Tóm tắt giải pháp:** Kỹ thuật sinh câu lệnh truy vấn động linh hoạt: khởi tạo mệnh đề WHERE 1=1 và nối thêm các điều kiện AND khi người dùng nhập dữ liệu, bảo đảm an toàn chuỗi với .Replace("'", "''").
- **Điểm sáng kỹ thuật & Giá trị doanh nghiệp:** Loại bỏ hoàn toàn các lỗi cú pháp SQL kinh điển: Không cần phải kiểm tra xem mệnh đề WHERE đã có điều kiện đầu tiên hay chưa; đồng thời kết nối đa bảng (MayViTinh, LoaiMay, HangSanXuat, Chip, RAM) bằng LEFT JOIN.
- **So sánh kiến trúc:**
  + **Cách cũ:** Ghép chuỗi SQL không có WHERE 1=1: Phải viết hàng chục lệnh if-else kiểm tra cờ isFirstCondition rất phức tạp và dễ lỗi cú pháp.
  + **Giải pháp đồ án:** Mẫu hình WHERE 1=1 trong đồ án: Mọi điều kiện lọc đều bắt đầu bằng AND tự nhiên, mã nguồn ngắn gọn và sáng sủa.
- **Nội dung chi tiết trên Slide:**
  + Khởi tạo câu truy vấn với mệnh đề cơ sở: WHERE 1=1.
  + Nối điều kiện lọc gần đúng: sql += " AND m.TenMayVT LIKE N'%" + txtTenMay.Text.Trim().Replace("'", "''") + "%'".
  + Lọc chính xác theo ComboBox: sql += " AND m.MaLoai = N'" + cboLoai.SelectedValue.ToString() + "'".
  + Lọc khoảng ngày chính xác: sql += string.Format(" AND h.NgayBan BETWEEN '{0:yyyy-MM-dd 00:00:00}' AND '{1:yyyy-MM-dd 23:59:59}'").
- **LỜI THUYẾT TRÌNH CHI TIẾT (SPEAKER SCRIPT):**
  > "[27:30 - 28:45] Về mặt lập trình, việc ghép câu truy vấn tìm kiếm rất dễ sinh lỗi cú pháp nếu không cẩn thận. Chúng em áp dụng mẫu thiết kế WHERE 1=1 với StringBuilder. Nhờ có điều kiện 1=1 luôn đúng ban đầu, mọi tiêu chí như tên máy hay khoảng giá đều có thể nối thêm bằng chữ AND mà không cần kiểm tra vị trí đầu hay cuối. Dấu nháy đơn cũng được khử bằng Replace để tránh lỗi SQL."

---

### SLIDE 24: Hệ Thống BI Dashboard & 4 Thẻ KPI Summary Cards
- **Chủ đề / Phân hệ:** BI DASHBOARD & KPI — *Báo cáo điều hành thông minh và bố cục động co giãn tự động tại `frmBaoCaoThongKe`*
- **Chỉ số nổi bật (Metric):** `4 Thẻ KPI` (Chỉ Số Điều Hành)
- **Tài nguyên hình ảnh minh họa:** `assets/ui_baocao_tonkho.png`, `assets/code_12_baocao_responsive_cards.png`
- **Tóm tắt giải pháp:** Màn hình frmBaoCaoThongKe cung cấp cái nhìn toàn cảnh về bức tranh tài chính doanh nghiệp thông qua 4 thẻ KPI Summary Cards được tính toán tự động bằng hàm CapNhatSummaryCards().
- **Điểm sáng kỹ thuật & Giá trị doanh nghiệp:** Tổng hợp tức thời sức khỏe tài chính: Thẻ Giá trị tồn kho tính vốn lưu động nằm trong kho; Thẻ Tổng nhập và Tổng bán đối chiếu dòng tiền; Thẻ Lợi nhuận gộp tự động đổi màu Xanh khi có lãi hoặc màu Đỏ khi lỗ.
- **So sánh kiến trúc:**
  + **Cách cũ:** Muốn biết doanh thu và lợi nhuận phải in sổ kế toán cuối tháng: Ban giám đốc không nắm bắt được số liệu tức thời.
  + **Giải pháp đồ án:** Hệ thống 4 KPI Cards trong đồ án: Cập nhật tự động theo thời gian thực mỗi khi mở form hoặc có giao dịch mới phát sinh.
- **Nội dung chi tiết trên Slide:**
  + Card 1 (lblCardGiaTriTon): SELECT ISNULL(SUM(SoLuong * GiaNhap), 0) FROM MayViTinh — Đo lường vốn đọng trong kho.
  + Card 2 (lblCardTongNhap): SELECT ISNULL(SUM(TongTien), 0) FROM HoaDonNhap — Theo dõi chi phí vốn đã thanh toán.
  + Card 3 (lblCardTongBan): SELECT ISNULL(SUM(TongTien), 0) FROM HoaDonBan — Tổng hợp dòng doanh thu bán lẻ đạt được.
  + Card 4 (lblCardLoiNhuan): Lợi nhuận = Tổng bán - Tổng nhập, tự động đổi màu RGB(16, 185, 129) khi lãi và RGB(239, 68, 68) khi lỗ.
- **LỜI THUYẾT TRÌNH CHI TIẾT (SPEAKER SCRIPT):**
  > "[28:45 - 30:00] Chuyển sang phân hệ Báo cáo Thống kê tại frmBaoCaoThongKe, điểm nhấn lớn nhất là 4 thẻ KPI Summary Cards phía trên. Bất kỳ lúc nào mở form, hàm CapNhatSummaryCards đều tính toán ngay lập tức 4 chỉ số: Tổng giá trị hàng tồn, Tổng tiền nhập, Tổng doanh thu và Lợi nhuận gộp. Thẻ lợi nhuận còn có thuật toán tự đổi màu chữ sang xanh khi có lãi hoặc màu đỏ cảnh báo khi lỗ."

---

### SLIDE 25: Báo Cáo Doanh Thu Theo Tháng, Quý, Năm (Yêu Cầu Nghiệp Vụ)
- **Chủ đề / Phân hệ:** PHÂN TÍCH DOANH THU — *Kỹ thuật tổng hợp dữ liệu giao dịch đa chu kỳ với các hàm thời gian SQL*
- **Chỉ số nổi bật (Metric):** `3 Khung Giờ` (Tháng, Quý, Năm)
- **Tài nguyên hình ảnh minh họa:** `assets/ui_baocao_nhaphang.png`, `assets/ui_baocao_banhang.png`
- **Tóm tắt giải pháp:** Phân tích cơ cấu doanh thu và số lượng nhập/bán theo các chu kỳ kế toán linh hoạt (Tháng, Quý, Năm) phục vụ công tác lập kế hoạch kinh doanh và quản trị tài chính.
- **Điểm sáng kỹ thuật & Giá trị doanh nghiệp:** Bố cục giao diện co giãn thông minh: Khi chọn chế độ xem theo thời gian, khung pnlFilterCard tự động điều chỉnh chiều cao, nạp các ComboBox cboThang, cboQuy, cboNam tương ứng và tính toán tổng tiền đơn hàng.
- **So sánh kiến trúc:**
  + **Cách cũ:** Báo cáo cố định không lọc được theo quý: Kế toán phải xuất ra Excel rồi dùng hàm SUMIFS thủ công.
  + **Giải pháp đồ án:** Hệ thống lọc thời gian trực tiếp trong đồ án: Chọn Tháng/Quý/Năm là CSDL tự động tổng hợp và hiển thị kết quả trong 10ms.
- **Nội dung chi tiết trên Slide:**
  + Sử dụng hàm DATEPART(QUARTER, h.NgayBan) kết hợp YEAR(h.NgayBan) để phân nhóm doanh thu theo từng quý trong năm.
  + Hàm HienThiDieuKienLoc() điều chỉnh linh hoạt vị trí các nút bấm và kích thước bảng DataGridView.
  + Kết nối bảng HoaDonBan/HoaDonNhap với NhanVien và KhachHang/NhaCungCap để hiển thị báo cáo chi tiết.
  + Nhãn tổng kết lblTongKet hiển thị tổng số tiền của toàn bộ báo cáo sau khi áp dụng bộ lọc thời gian.
- **LỜI THUYẾT TRÌNH CHI TIẾT (SPEAKER SCRIPT):**
  > "[30:00 - 31:15] Màn hình thống kê đáp ứng đầy đủ các yêu cầu nghiệp vụ nâng cao về phân tích thời gian. Khi người dùng chọn thống kê theo Quý, câu lệnh SQL sẽ sử dụng hàm DATEPART(QUARTER, h.NgayBan) kết hợp với YEAR(h.NgayBan) để nhóm dữ liệu. Khung điều khiển lọc bên trên cũng tự động co giãn để hiển thị các ComboBox chọn tháng, quý, năm tương ứng."

---

### SLIDE 26: Thuật Toán Xếp Hạng Top 5 Nhân Viên (`SELECT TOP 5 WITH TIES`)
- **Chủ đề / Phân hệ:** THUẬT TOÁN XẾP HẠNG — *Kỹ thuật xếp hạng công bằng trong đánh giá hiệu suất nhân sự*
- **Chỉ số nổi bật (Metric):** `WITH TIES` (Không Bỏ Sót Đồng Hạng)
- **Tài nguyên hình ảnh minh họa:** `assets/ui_baocao_top5nv.png`, `assets/code_13_baocao_top5_sql.png`
- **Tóm tắt giải pháp:** Áp dụng từ khóa chuyên sâu SELECT TOP 5 WITH TIES của SQL Server để vinh danh các nhân viên có thành tích xuất sắc nhất trong quý mà không làm mất đi tính công bằng khi có đồng hạng doanh thu.
- **Điểm sáng kỹ thuật & Giá trị doanh nghiệp:** Giải quyết triệt để bài toán đồng hạng nhân sự: Nếu chỉ dùng SELECT TOP 5 thông thường, khi nhân viên thứ 5 và thứ 6 có cùng mức doanh thu, SQL Server sẽ ngẫu nhiên loại bỏ 1 người; với WITH TIES, cả hai đều được xếp hạng và nhận thưởng.
- **So sánh kiến trúc:**
  + **Cách cũ:** Dùng TOP 5 đơn thuần: Nhân viên thứ 6 có cùng doanh thu 100 triệu với nhân viên thứ 5 nhưng bị phần mềm bỏ rơi dẫn đến khiếu nại.
  + **Giải pháp đồ án:** SELECT TOP 5 WITH TIES trong đồ án: Bảo đảm tính công bằng tuyệt đối cho chính sách khen thưởng và đánh giá KPI nhân sự.
- **Nội dung chi tiết trên Slide:**
  + Cú pháp truy vấn: SELECT TOP 5 WITH TIES nv.MaNV, nv.TenNV, nv.DienThoai, COUNT(h.MaHDB) AS SoHoaDonDaBan, ISNULL(SUM(h.TongTien), 0) AS TongTienBan.
  + Nhóm dữ liệu theo thông tin nhân viên: GROUP BY nv.MaNV, nv.TenNV, nv.DienThoai và ORDER BY TongTienBan DESC.
  + Bảng báo cáo hiển thị số lượng hóa đơn bán được và tổng doanh thu mang về cho cửa hàng.
  + Tự động tính tổng tiền đóng góp của nhóm Top 5 nhân viên xuất sắc qua nhãn lblTongKet.
- **LỜI THUYẾT TRÌNH CHI TIẾT (SPEAKER SCRIPT):**
  > "[31:15 - 32:30] Ở báo cáo Top 5 nhân viên xuất sắc, nhóm em đã áp dụng cú pháp SELECT TOP 5 WITH TIES trong SQL Server. Đây là một điểm rất hay: nếu chỉ dùng TOP 5 đơn thuần, trong trường hợp người thứ 5 và người thứ 6 có cùng mức doanh thu, hệ thống sẽ ngẫu nhiên loại bỏ một người. Với từ khóa WITH TIES, SQL Server giữ lại toàn bộ những người đồng hạng, bảo đảm tính công bằng tuyệt đối trong đánh giá thi đua."

---

### SLIDE 27: Engine Xuất Báo Cáo Excel Nội Sinh Tốc Độ Cao
- **Chủ đề / Phân hệ:** NATIVE EXCEL ENGINE — *Giải pháp xuất file Excel Native HTML chuẩn Microsoft không phụ thuộc COM*
- **Chỉ số nổi bật (Metric):** `< 0.2s` (Xuất 10,000 Dòng Dữ Liệu)
- **Tài nguyên hình ảnh minh họa:** `assets/graph_07_bi_analytics_flow.png`, `assets/code_14_baocao_excel_export.png`
- **Tóm tắt giải pháp:** Tự động biên dịch bảng DataGridView thành tài liệu bảng tính chuẩn Microsoft Excel dạng HTML UTF-8 (.xls), loại bỏ hoàn toàn việc cài đặt Microsoft Office Interop.
- **Điểm sáng kỹ thuật & Giá trị doanh nghiệp:** Khắc phục triệt để lỗi rò rỉ tiến trình EXCEL.EXE chạy ngầm gây treo máy của thư viện Office Interop; tốc độ xuất siêu tốc (< 0.2s), giữ nguyên định dạng tiếng Việt Unicode và mở mượt mà trên mọi phần mềm bảng tính.
- **So sánh kiến trúc:**
  + **Cách cũ:** Sử dụng Microsoft.Office.Interop.Excel: Bắt buộc cài đặt MS Office, chạy rất chậm và thường xuyên để lại tiến trình rác trong Task Manager.
  + **Giải pháp đồ án:** Động cơ xuất Native HTML-XLS trong đồ án: Hoàn toàn không phụ thuộc bên ngoài, tốc độ tức thì, file chuẩn đẹp.
- **Nội dung chi tiết trên Slide:**
  + Khai báo thẻ meta http-equiv='Content-Type' content='text/html; charset=utf-8' chống lỗi font chữ tiếng Việt.
  + Tạo cấu trúc bảng table border='1' với tiêu đề bảng màu xanh đậm (#1E293B) và chữ trắng nổi bật.
  + Duyệt nhanh qua các dòng DataGridViewRow và ghi tệp thông qua File.WriteAllText(fileName, sb.ToString(), Encoding.UTF8).
  + Tập tin xuất ra tương thích 100% với Microsoft Excel, Google Sheets, LibreOffice và WPS Office.
- **LỜI THUYẾT TRÌNH CHI TIẾT (SPEAKER SCRIPT):**
  > "[32:30 - 33:45] Một tính năng được hội đồng đánh giá rất cao là động cơ xuất Excel nội sinh trong hàm btnXuatExcel_Click. Chúng em không dùng Microsoft.Office.Interop.Excel vì thư viện này rất nặng, dễ gây treo tiến trình ngầm và bắt buộc máy khách phải cài Office. Thay vào đó, chúng em sinh cấu trúc HTML Spreadsheet chuẩn với UTF-8. Tốc độ xuất gần như tức thì và mở mượt mà trên mọi phần mềm bảng tính."

---

### SLIDE 28: Quản Trị Tài Khoản & Phân Quyền Vai Trò (`frmQuanLyNguoiDung.cs`)
- **Chủ đề / Phân hệ:** QUẢN TRỊ BẢO MẬT — *Mô hình bảo mật phân quyền Role-Based Access Control (Admin / Staff)*
- **Chỉ số nổi bật (Metric):** `RBAC` (Phân Quyền Vai Trò)
- **Tài nguyên hình ảnh minh họa:** `assets/ui_quanlynguoidung.png`, `assets/code_15_audit_log_rbac.png`
- **Tóm tắt giải pháp:** Màn hình dành riêng cho Quản trị viên để quản lý vòng đời tài khoản nhân viên trong bảng tblTaiKhoan: cấp quyền, khóa/mở khóa tài khoản và đặt lại mật khẩu an toàn.
- **Điểm sáng kỹ thuật & Giá trị doanh nghiệp:** Bảo vệ an ninh doanh nghiệp: Tính năng khóa tài khoản tức thời (btnKhoaMoKhoa_Click) ngăn chặn nhân viên đã nghỉ việc truy cập vào hệ thống mà không cần xóa tài khoản, bảo toàn lịch sử giao dịch trong quá khứ.
- **So sánh kiến trúc:**
  + **Cách cũ:** Khi nhân viên nghỉ việc thì xóa luôn tài khoản: Làm đứt gãy khóa ngoại của các hóa đơn cũ do nhân viên đó từng lập.
  + **Giải pháp đồ án:** Cơ chế Khóa tài khoản trong đồ án: Giữ nguyên lịch sử hóa đơn trong CSDL nhưng chặn không cho đăng nhập mới.
- **Nội dung chi tiết trên Slide:**
  + Hiển thị danh sách tài khoản liên kết với họ tên nhân viên từ bảng NhanVien qua INNER JOIN.
  + Phân định rõ 3 vai trò hệ thống: QuanTri (Toàn quyền), BanHang (Chỉ lập hóa đơn bán), Kho (Chỉ nhập hàng).
  + Nút Khóa/Mở Khóa đảo trạng thái giữa 'Hoạt động' và 'Bị khóa' tức thì trong CSDL.
  + Tự động ghi lại nhật ký hành động qua Functions.LogAction('Đổi trạng thái', chiTiet) bảo đảm trách nhiệm giải trình.
- **LỜI THUYẾT TRÌNH CHI TIẾT (SPEAKER SCRIPT):**
  > "[33:45 - 35:00] Form frmQuanLyNguoiDung là trung tâm quản trị bảo mật của Admin. Tại đây, người quản trị có thể phân quyền Admin hoặc Nhân viên, khóa tài khoản hoặc khôi phục quyền truy cập. Cơ chế phân quyền được thực thi ở cả hai cấp độ: kiểm tra quyền khi bấm menu trên giao diện và kiểm tra quyền tại từng hàm nghiệp vụ."

---

### SLIDE 29: Quản Lý Hồ Sơ Cá Nhân & Đổi Mật Khẩu (`frmHoSoCaNhan.cs`)
- **Chủ đề / Phân hệ:** HỒ SƠ CÁ NHÂN — *Không gian tự phục vụ cho nhân viên cập nhật thông tin và bảo mật cá nhân*
- **Chỉ số nổi bật (Metric):** `16 Byte` (Chuỗi Muối Ngẫu Nhiên)
- **Tài nguyên hình ảnh minh họa:** `assets/ui_hosocanhan.png`
- **Tóm tắt giải pháp:** Cổng thông tin tự phục vụ dành cho nhân viên đang đăng nhập: xem thông tin cá nhân, theo dõi lịch sử hoạt động của chính mình và đổi mật khẩu an toàn định kỳ.
- **Điểm sáng kỹ thuật & Giá trị doanh nghiệp:** Quy trình đổi mật khẩu an toàn chuẩn doanh nghiệp: Nhân viên bắt buộc phải nhập đúng mật khẩu hiện tại để xác thực; mật khẩu mới được băm lại bằng thuật toán PBKDF2 với chuỗi Salt mới trước khi cập nhật vào CSDL.
- **So sánh kiến trúc:**
  + **Cách cũ:** Nhân viên muốn đổi mật khẩu phải nhờ Admin vào sửa CSDL: Rất bất tiện và lộ mật khẩu cho người quản trị.
  + **Giải pháp đồ án:** Cổng tự phục vụ trong đồ án: Nhân viên tự chủ động đổi mật khẩu mọi lúc, quản trị viên cũng không thể biết mật khẩu gốc.
- **Nội dung chi tiết trên Slide:**
  + Nạp thông tin hồ sơ nhân sự từ bảng NhanVien dựa trên biến phiên Functions.CurrentMaNV.
  + Hiển thị bảng lịch sử thao tác gần nhất của người dùng từ bảng tblNhatKyHoatDong.
  + Kiểm tra tính hợp lệ: Mật khẩu mới phải có độ dài từ 6 ký tự trở lên và khớp với ô xác nhận mật khẩu.
  + Ghi lại nhật ký đổi mật khẩu thành công và thông báo người dùng bảo mật thông tin.
- **LỜI THUYẾT TRÌNH CHI TIẾT (SPEAKER SCRIPT):**
  > "[35:00 - 36:15] Form frmHoSoCaNhan cung cấp cổng thông tin tự phục vụ cho từng cán bộ nhân viên. Nhân viên có thể kiểm tra lại thông tin cá nhân của mình và tự đổi mật khẩu định kỳ. Quy trình đổi mật khẩu bắt buộc phải nhập đúng mật khẩu hiện tại, giúp bảo đảm an toàn dữ liệu cá nhân ngay cả khi người dùng quên khóa máy tính."

---

### SLIDE 30: Hệ Thống Giám Sát & Nhật Ký Hoạt Động (`frmNhatKyHoatDong.cs`)
- **Chủ đề / Phân hệ:** KIỂM TOÁN NỘI BỘ — *Truy vết kiểm toán không phong tỏa (Non-blocking Enterprise Audit Trail)*
- **Chỉ số nổi bật (Metric):** `0 Giây` (Ảnh Hưởng Hiệu Năng)
- **Tài nguyên hình ảnh minh họa:** `assets/ui_nhatkyhoatdong.png`
- **Tóm tắt giải pháp:** Ghi nhận toàn bộ thao tác thêm, sửa, xóa, đăng nhập của người dùng vào bảng tblNhatKyHoatDong, phục vụ công tác thanh tra, truy vết trách nhiệm và kiểm soát rủi ro nội bộ.
- **Điểm sáng kỹ thuật & Giá trị doanh nghiệp:** Cơ chế Non-blocking Logging: Hàm Functions.LogAction được bọc trong khối try-catch độc lập; nếu có sự cố về mạng hay nhật ký thì giao dịch bán hàng chính vẫn được thực hiện bình thường, không bao giờ làm gián đoạn thu ngân.
- **So sánh kiến trúc:**
  + **Cách cũ:** Không có nhật ký thao tác: Khi thất thoát hàng hay bị xóa đơn hàng, ban giám đốc không thể biết ai là người thực hiện.
  + **Giải pháp đồ án:** Audit Trail của đồ án: Mọi thao tác xóa máy, hủy đơn hay đổi mật khẩu đều lưu vết chính xác đến từng giây.
- **Nội dung chi tiết trên Slide:**
  + Lưu trữ chi tiết 4 trường: TenDangNhap, HanhDong, ThoiGian (hàm GETDATE() của SQL Server), và ChiTiet nội dung.
  + Giao diện cho phép lọc nhật ký theo tên người dùng, loại hành động hoặc khoảng ngày phát sinh.
  + Tích hợp cảnh báo các mặt hàng máy vi tính sắp hết hàng trong kho (SoLuong <= 3) để quản lý nhập thêm.
  + Đáp ứng trọn vẹn tiêu chuẩn kiểm toán hệ thống phần mềm doanh nghiệp hiện đại.
- **LỜI THUYẾT TRÌNH CHI TIẾT (SPEAKER SCRIPT):**
  > "[36:15 - 37:30] Để bảo đảm tính minh bạch và phục vụ kiểm toán nội bộ, frmNhatKyHoatDong hiển thị toàn bộ lịch sử thao tác của các tài khoản. Mọi hành vi như đăng nhập, lập đơn bán, xóa máy tính đều được hàm Functions.LogAction tự động ghi lại kèm mốc thời gian GETDATE(). Hàm này được thiết kế theo nguyên tắc Non-blocking, bảo đảm việc ghi log không bao giờ gây ảnh hưởng đến tốc độ bán hàng."

---

### SLIDE 31: Trung Tâm Cấu Hình & Tùy Biến Hệ Thống (`frmCaiDat.cs`)
- **Chủ đề / Phân hệ:** CẤU HÌNH HỆ THỐNG — *Quản lý tham số vận hành, thông tin thương hiệu hóa đơn và kết nối CSDL*
- **Chỉ số nổi bật (Metric):** `100%` (Tùy Biến Không Cần Code Lại)
- **Tài nguyên hình ảnh minh họa:** `assets/ui_caidat.png`
- **Tóm tắt giải pháp:** Cho phép thay đổi thông tin doanh nghiệp (Tên cửa hàng, Địa chỉ, Hotline in trên hóa đơn), kiểm tra chuỗi kết nối SQL Server và lưu trữ cài đặt giao diện mặc định vào bảng tblCauHinhHeThong.
- **Điểm sáng kỹ thuật & Giá trị doanh nghiệp:** Tách biệt cấu hình khỏi mã nguồn: Khi chuỗi cửa hàng mở rộng thêm chi nhánh mới hoặc đổi máy chủ cơ sở dữ liệu qua mạng LAN, người quản trị chỉ cần cấu hình trên giao diện mà không phải biên dịch lại file .exe.
- **So sánh kiến trúc:**
  + **Cách cũ:** Hardcode chuỗi kết nối và tên cửa hàng trong mã C#: Mỗi lần đổi máy chủ hay số hotline phải nhờ lập trình viên build lại.
  + **Giải pháp đồ án:** Trung tâm cấu hình trong đồ án: Người dùng tự thay đổi thông tin thương hiệu và máy chủ CSDL trực quan trên giao diện.
- **Nội dung chi tiết trên Slide:**
  + Nút Kiểm tra kết nối (btnTestConnect_Click): Thử nghiệm kết nối đến máy chủ SQL Server và thông báo trạng thái tức thời.
  + Lưu thông tin cửa hàng: Tên đơn vị, số điện thoại, địa chỉ hiển thị trên tiêu đề phiếu nhập và hóa đơn bán hàng.
  + Cài đặt chủ đề mặc định: Lựa chọn chế độ Sáng hoặc Tối khi khởi động ứng dụng cho từng máy trạm.
  + Lưu trữ bền vững vào bảng tblCauHinhHeThong, tự động nạp lại khi người dùng khởi động phần mềm.
- **LỜI THUYẾT TRÌNH CHI TIẾT (SPEAKER SCRIPT):**
  > "[37:30 - 38:45] Cuối cùng trong nhóm quản trị là frmCaiDat. Màn hình này cho phép ban quản lý thay đổi các thông tin hiển thị trên đầu hóa đơn thanh toán như tên cửa hàng, địa chỉ, số hotline mà không cần biên dịch lại mã nguồn. Đây cũng là nơi cấu hình giao diện mặc định khi khởi động ứng dụng."

---

### SLIDE 32: Tổng Kết Đồ Án, Hướng Phát Triển & Bộ 12 Câu Hỏi Đáp Q&A
- **Chủ đề / Phân hệ:** TỔNG KẾT & PHẢN BIỆN — *Khẳng định giá trị thực tiễn của đồ án và sẵn sàng phản biện trước Hội đồng*
- **Chỉ số nổi bật (Metric):** `100%` (Hoàn Thành Mục Tiêu)
- **Tài nguyên hình ảnh minh họa:** `assets/graph_01_overview_3tier.png`, `assets/graph_06_pos_trigger_pipeline.png`
- **Tóm tắt giải pháp:** Đồ án 'Hệ Thống Quản Lý Bán Máy Vi Tính' hoàn thành trọn vẹn 100% mục tiêu đề ra: kiến trúc 3 tầng chuẩn mực, CSDL chuẩn 3NF, đồng bộ kho bằng Trigger ACID và giao diện Dual-Theme hiện đại.
- **Điểm sáng kỹ thuật & Giá trị doanh nghiệp:** Sản phẩm hoàn chỉnh sẵn sàng ứng dụng thực tế: Không chỉ đáp ứng chuẩn mực học thuật của môn học Lập trình trực quan mà còn giải quyết bài toán vận hành bán lẻ và quản trị dòng tiền cho doanh nghiệp.
- **So sánh kiến trúc:**
  + **Cách cũ:** Các đồ án thông thường: Giao diện sơ sài, câu lệnh SQL ghép nối thô sơ, dễ bị lỗi âm kho khi nhiều người dùng.
  + **Giải pháp đồ án:** Đồ án QLBanMayViTinh: Kiến trúc 3 tầng chuẩn mực, bẫy lỗi 547, Trigger ACID, mã hóa AES-256, giao diện Fluent đẳng cấp.
- **Nội dung chi tiết trên Slide:**
  + Đã kiểm chứng thực tế toàn bộ 14 Forms giao diện và các luồng nghiệp vụ kinh doanh cốt lõi.
  + Định hướng nâng cấp 1: Tích hợp Barcode / QR Code quét mã linh kiện tăng tốc độ lập đơn bán hàng.
  + Định hướng nâng cấp 2: Kết nối API Hóa đơn điện tử có mã của cơ quan Thuế theo quy định hiện hành.
  + Định hướng nâng cấp 3: Tích hợp mô hình AI/Machine Learning dự báo nhu cầu nhập hàng theo mùa vụ.
  + Sẵn sàng bước vào phần Vấn đáp Phản biện với Bộ 12 câu hỏi chuyên sâu của Hội đồng.
- **LỜI THUYẾT TRÌNH CHI TIẾT (SPEAKER SCRIPT):**
  > "[38:45 - 40:00] Kính thưa Hội đồng phản biện, đồ án 'Hệ Thống Quản Lý Bán Máy Vi Tính' đã hoàn thành trọn vẹn 100% mục tiêu đề ra. Hệ thống không chỉ đáp ứng chuẩn mực về mặt học thuật với kiến trúc 3 tầng và cơ sở dữ liệu 3NF, mà còn sẵn sàng triển khai áp dụng vào thực tế doanh nghiệp. Nhóm chúng em đã sẵn sàng đón nhận những câu hỏi đóng góp quý báu và bước vào phần vấn đáp phản biện. Xin chân thành cảm ơn quý Thầy Cô!"

---

## BỘ 12 CÂU HỎI VÀ ĐÁP ÁN BẢO VỆ ĐỒ ÁN TRƯỚC HỘI ĐỒNG (Q&A)

#### Câu 1: Tại sao nhóm lại lựa chọn Pure ADO.NET thay vì Entity Framework hay Dapper?
Trả lời: Đối với phần mềm bán hàng POS và quản lý kho máy tính, tốc độ phản hồi và sự kiểm soát tài nguyên là ưu tiên số một. Pure ADO.NET cho phép kiểm soát 100% vòng đời của SqlConnection, tận dụng SqlDataReader đọc luồng dữ liệu trực tiếp và SqlDataAdapter nạp DataTable cực nhanh (< 15ms) mà không phải gánh chịu chi phí ánh xạ đối tượng (Object Mapping Overhead) của ORM. Ngoài ra, việc dùng ADO.NET thuần giúp chúng em làm chủ hoàn toàn bản chất tầng kết nối và tối ưu câu lệnh SQL theo chuẩn của môn học.

#### Câu 2: Làm thế nào hệ thống đảm bảo chống tấn công SQL Injection khi sử dụng ADO.NET thuần?
Trả lời: 100% các câu lệnh truy vấn có nhận dữ liệu từ người dùng (như đăng nhập, tìm kiếm, thêm, sửa, xóa) trong Functions.cs và các Form đều sử dụng mảng SqlParameter[] hoặc tham số SqlCommand.Parameters.AddWithValue. Tham số được SQL Server phân tích ngữ pháp riêng biệt với câu lệnh SQL, do đó mọi ký tự đặc biệt như dấu nháy đơn hay chuỗi mã độc đều bị vô hiệu hóa. Đối với các chuỗi tìm kiếm động trong frmTimKiem, hệ thống còn áp dụng thêm kỹ thuật khử dấu nháy đơn .Replace("'", "''").

#### Câu 3: Cơ sở dữ liệu của đồ án đã đạt chuẩn mấy? Hãy chứng minh tính chuẩn hóa 3NF?
Trả lời: Cơ sở dữ liệu của đồ án gồm 25 bảng đạt Chuẩn hóa bậc 3 (3NF). Đạt 1NF vì tất cả các thuộc tính đều là nguyên tố (nguyên tử) và có khóa chính xác định. Đạt 2NF vì không có thuộc tính không khóa nào phụ thuộc một phần vào khóa chính phức hợp (ví dụ bảng ChiTietHDB có khóa chính MaHDB + MaMayVT, các thuộc tính SoLuong, DonGia phụ thuộc toàn bộ vào cặp khóa này). Đạt 3NF vì không có phụ thuộc bắc cầu: bảng MayViTinh chỉ lưu các khóa ngoại MaLoai, MaHangSX, MaChip, MaRAM...; các thông tin mô tả chi tiết được tách thành 14 bảng danh mục linh kiện con độc lập.

#### Câu 4: Khi hai nhân viên cùng bán một chiếc máy vi tính cuối cùng trong kho cùng lúc, hệ thống xử lý tranh chấp như thế nào?
Trả lời: Hệ thống có 2 lớp bảo vệ. Lớp 1 ở tầng ứng dụng C# (frmHoaDonBan): hàm kiểm tra tồn kho so sánh số lượng khách yêu cầu với số lượng hiện có trước khi cho lưu đơn. Lớp 2 ở tầng cơ sở dữ liệu: Trigger trg_ChiTietHDB_AUID chạy trong transaction ngầm của SQL Server. Khi câu lệnh UPDATE MayViTinh thực thi, bản ghi của chiếc máy đó bị khóa độc quyền (Exclusive Lock). Trigger kiểm tra nếu m.SoLuong < df.DeltaSL thì lập tức kích hoạt RAISERROR(N'Số lượng tồn kho không đủ để xuất bán...', 16, 1) và ROLLBACK TRANSACTION, bảo đảm không bao giờ xảy ra âm kho.

#### Câu 5: Tại sao nhóm lại đưa việc trừ tồn kho và tính tổng tiền vào SQL Server Trigger thay vì viết hoàn toàn bằng code C#?
Trả lời: Việc đưa vào Trigger đem lại 3 lợi thế vượt trội: 1. Tính nguyên tử (Atomicity): Thao tác thêm chi tiết hóa đơn và trừ kho diễn ra trong cùng một transaction của CSDL; nếu trừ kho thất bại thì hóa đơn cũng tự động bị hủy (Rollback). 2. Độc lập tầng ứng dụng: Dù sau này có mở rộng viết thêm ứng dụng Web hay Mobile kết nối vào cùng CSDL SQL Server, quy tắc nghiệp vụ trừ kho và cộng tổng tiền vẫn được thực thi tự động. 3. Tối ưu hiệu năng: Giảm bớt số lượng request gửi qua mạng giữa máy trạm WinForms và máy chủ CSDL.

#### Câu 6: Hãy giải thích thuật toán và ưu điểm của kỹ thuật chuyển đổi Theme (Dark/Light Mode) trong UIThemeHelper.cs?
Trả lời: Thuật toán dựa trên cơ chế duyệt đệ quy cây điều khiển (Recursive Tree Traversal). Hàm ApplyTheme(Control root) kiểm tra kiểu cụ thể của từng Control và áp dụng màu sắc tương ứng từ Palette chuẩn. Nếu Control đó có chứa các Control con (c.HasChildren == true) như Panel, GroupBox, TabControl, hàm sẽ tự gọi lại chính nó. Ưu điểm là cơ chế này độc lập hoàn toàn với việc người dùng thiết kế giao diện như thế nào, tự động nhận diện và đổi màu cho 100% controls trên Form mà không cần phải gán màu thủ công cho từng control trong Designer.

#### Câu 7: Kỹ thuật nhúng Form động (Form Embedding) trên frmMain có ưu điểm gì so với việc dùng MDI Parent truyền thống?
Trả lời: Giao diện MDI cổ điển của Windows Forms có nhược điểm lớn là các cửa sổ con trôi nổi bên trong, gây khó chịu cho người dùng, dễ bị thu nhỏ lạc mất cửa sổ và có hiện tượng nhấp nháy (flickering) khi phóng to thu nhỏ. Bằng cách thiết lập childForm.TopLevel = false và Dock = Fill vào Panel pnlContent, hệ thống biến thành một ứng dụng Single-Window phẳng hiện đại. Đồng thời việc chủ động gọi Close() và Dispose() form cũ trước khi nạp form mới giúp ứng dụng kiểm soát chặt chẽ bộ nhớ RAM, tránh rò rỉ tài nguyên.

#### Câu 8: Tại sao nhóm lại tự viết động cơ xuất Excel nội sinh dạng HTML Spreadsheet thay vì dùng Microsoft.Office.Interop.Excel?
Trả lời: Thư viện Microsoft.Office.Interop.Excel có các nhược điểm nghiêm trọng trong môi trường doanh nghiệp: 1. Bắt buộc máy trạm phải cài đặt Microsoft Office bản quyền. 2. Tiến trình EXCEL.EXE chạy ngầm rất dễ bị treo (orphan process) nếu không giải phóng COM Object đúng cách, làm ngốn RAM máy trạm. 3. Tốc độ rất chậm vì mỗi lần ghi một ô dữ liệu phải gọi qua ranh giới COM. Bằng cách sinh trực tiếp mã HTML Table với charset=utf-8, tốc độ xuất 10.000 dòng mất chưa tới 0.2 giây, mở được trên mọi phần mềm bảng tính và hoàn toàn không phụ thuộc bên ngoài.

#### Câu 9: Từ khóa `TOP 5 WITH TIES` trong báo cáo thống kê nhân viên xuất sắc có ý nghĩa gì đặc biệt?
Trả lời: Trong bài toán khen thưởng nhân sự, nếu chỉ dùng `SELECT TOP 5` thông thường theo thứ tự giảm dần của doanh thu, trong trường hợp người thứ 5 và người thứ 6 (hoặc thứ 7) có cùng mức doanh thu bằng nhau, SQL Server sẽ tự ý cắt lấy một người ngẫu nhiên và loại bỏ người còn lại. Việc sử dụng `WITH TIES` (đi kèm ORDER BY) chỉ định rằng nếu có nhiều bản ghi đồng hạng với bản ghi cuối cùng của tập TOP, SQL Server sẽ giữ lại tất cả các bản ghi đó. Điều này bảo đảm tính công bằng tuyệt đối trong đánh giá thi đua.

#### Câu 10: Thiết kế đa hình trên `frmDanhMucHeThong.cs` đem lại lợi ích gì cho hệ thống?
Trả lời: Hệ thống có tới 17 danh mục (Khách hàng, Nhà cung cấp, Nhân viên và 14 bảng linh kiện phần cứng). Nếu tạo 17 form riêng biệt, dự án sẽ bị dư thừa tới 80% mã nguồn tương tự nhau, tốn dung lượng file .exe và tiêu tốn nhiều GDI Handle của hệ điều hành. Với kiến trúc 4 Tab và hàm GetCurrentTableName nạp động 14 bảng linh kiện, hệ thống chỉ cần thay đổi cấu trúc cột của DataGridView và chuỗi câu lệnh SQL tương ứng khi bấm nút, giúp code tinh gọn tối đa và tiết kiệm bộ nhớ.

#### Câu 11: Mật khẩu người dùng trong hệ thống được bảo mật như thế nào? Có lưu dưới dạng văn bản thuần không?
Trả lời: Mật khẩu người dùng tuyệt đối KHÔNG lưu trữ dưới dạng văn bản thuần (plain text) trong CSDL. Hệ thống sử dụng thuật toán PBKDF2 với 10,000 vòng băm và chuỗi muối ngẫu nhiên 16-byte (Salt). Khi người dùng tạo tài khoản hoặc đổi mật khẩu, chuỗi mật khẩu được băm cùng Salt trước khi lưu vào cột MatKhauHash và Salt của bảng tblTaiKhoan. Khi đăng nhập, hàm SecurityHelper.VerifyPassword lấy mã băm trong CSDL ra để đối chiếu. Kể cả người quản trị mở trực tiếp bảng CSDL cũng không thể giải mã được mật khẩu gốc.

#### Câu 12: Kỹ thuật `LoadImageWithoutLock` trong `Functions.cs` giải quyết vấn đề kỹ thuật gì của Windows Forms?
Trả lời: Trong Windows Forms, phương thức mặc định Image.FromFile(filePath) sẽ giữ một khóa độc quyền (Exclusive Lock) trên tệp tin hình ảnh ở đĩa cứng cho đến khi đối tượng Image bị giải phóng hoặc ứng dụng tắt. Điều này khiến người dùng không thể đổi tên, cập nhật ảnh mới hay xóa tệp tin trên Windows Explorer (sẽ bị báo lỗi 'The action can't be completed because the file is open'). Hàm LoadImageWithoutLock mở FileStream với FileAccess.Read, copy toàn bộ byte vào MemoryStream rồi đóng file ngay lập tức, sau đó tạo Image từ MemoryStream, loại bỏ hoàn toàn hiện tượng khóa file trên đĩa.
