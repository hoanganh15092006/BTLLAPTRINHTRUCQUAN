# 🧠 Antigravity ⇄ Google Gemini Web Collaborative Intelligence Protocol

Tài liệu này quy định **Giao Thức Phối Hợp Tác Tử 2 Chiều (Bi-Directional Agent Collaboration)** giữa **Antigravity IDE** và **Google Gemini Web**. 

Bất kỳ AI Agent nào vận hành bên trong Google Antigravity IDE khi làm việc trên dự án này đều **BẮT BUỘC** tuân thủ tuyệt đối quy trình 4 giai đoạn dưới đây.

> 📖 **Cẩm nang hướng dẫn lệnh đầy đủ cho người dùng:**  
> Xem chi tiết tại [HUONG_DAN_SU_DUNG_LENH_GEMINI.md](file:///d:/ApoGeminiChoAnti/HUONG_DAN_SU_DUNG_LENH_GEMINI.md) với trọn bộ 12 cấu hình (4 dòng mô hình × 3 cấp độ tư duy: Thấp, Trung bình, Cao).

---

## 🎯 Phân Định Vai Trò (Architectural Roles)

1. **Google Gemini Web (`gemini.google.com`) — Master AI Architect & Reasoning Engine:**
   - Đóng vai trò là **Kiến trúc sư trưởng / Cố vấn cao cấp**.
   - Chịu trách nhiệm: Tư duy chiến lược, phân tích logic thuật toán phức tạp, thiết kế giải pháp tổng thể, tạo prompt chỉ dẫn hành động chi tiết và giải đáp khi gặp khó khăn.

2. **Antigravity IDE Agent — Senior Execution Engineer & Local Operator:**
   - Đóng vai trò là **Kỹ sư thực thi hiện trường**.
   - Chịu trách nhiệm: Trực tiếp thao tác trên workspace (đọc code `view_file`, sửa file `replace_file_content`, chạy lệnh terminal `run_command`, kiểm thử `npm test`), báo cáo tiến độ và tương tác với người dùng.

---

## 🖥️ Cơ Chế Tự Động Chuyển Cửa Sổ 2 Chiều (Bi-Directional Auto-Window Switcher)

Hệ thống tích hợp bộ điều phối chuyển cửa sổ native Win32 siêu tốc (<20ms) giữa Antigravity IDE và Google Chrome:
- **Khi gửi câu hỏi / prompt**: Tự động chuyển focus sang cửa sổ **Google Chrome** để người dùng có thể quan sát trực tiếp chuỗi tư duy suy luận và văn bản đang được streaming trên giao diện web.
- **Khi nhận được câu trả lời hoàn tất (hoặc lỗi)**: Tự động chuyển focus trở lại ngay cửa sổ **Antigravity IDE** để Antigravity tiếp tục thực thi mã nguồn và kiểm thử.
- Cấu hình qua `.env`: `AUTO_FOCUS_SWITCH=true` (mặc định bật).

---

## ⚡ Cơ Chế Chống Giật Lag & Tự Phục Hồi Cấu Hình (Anti-Lag & Auto Config Restore)

Khi lịch sử chat trên Gemini Web quá dài, giao diện DOM có thể bị gián đoạn hoặc ngốn bộ nhớ:
- Hệ thống hỗ trợ tính năng **Tự động mở chat mới an toàn (Safe New Chat)** thông qua thao tác DOM mà không tải lại trang.
- Tự động **khôi phục lại cấu hình Mô Hình và Cấp độ tư duy** ban đầu cho tab đó mà không cần can thiệp thủ công.
- Tích hợp **Web Locks API** (`navigator.locks.request`) và `autoDiscardable: false` để tab không bao giờ bị Chrome đưa vào chế độ ngủ đông (background freezing/throttling).

---

## 🔄 Quy Trình 4 Giai Đoạn Bắt Buộc (The 4-Stage Closed Loop)

### Giai Đoạn 1: Tiếp Nhận Yêu Cầu Của Người Dùng
* Khi người dùng đưa ra một yêu cầu mới, tính năng mới hoặc bài toán cần giải quyết:
  * Antigravity phân tích yêu cầu, thu thập thông tin hiện trạng (các file liên quan, cấu trúc hiện tại).
  * **TUYỆT ĐỐI KHÔNG** tự ý đoán mò hoặc thực hiện các thay đổi kiến trúc lớn mà chưa có sự định hướng từ Gemini.

---

### Giai Đoạn 2: Tham Vấn Gemini Web (Consultation First)
* Antigravity chủ động gọi công cụ **`gemini_web_consult`** (hoặc `gemini_web_ask`):
  * Cung cấp cho Gemini: Mục tiêu của người dùng + Hiện trạng dự án + Câu hỏi kiến trúc cần tư vấn.
  * Hỗ trợ tham số `model_preference` hoặc `tab_id` để gửi đến đúng mô hình và tab mong muốn.
  * Gemini Web sẽ phân tích và trả về:
    1. 🎯 **Định hướng chiến lược** & giải pháp tối ưu.
    2. 📋 **Bản kế hoạch hành động từng bước (Step-by-step Plan)** dành riêng cho Antigravity.
    3. 💻 **Mã nguồn mẫu hoặc prompt thực thi chi tiết**.
    4. 🔄 **Chỉ dẫn câu hỏi cần quay lại hỏi tiếp** nếu gặp vướng mắc.

---

### Giai Đoạn 3: Thực Thi & Vòng Lặp Phản Hồi (Execution & Closed-Loop Feedback)
* Antigravity nhận kế hoạch và chỉ dẫn từ Gemini, bắt tay vào thực thi từng bước:
  * Sửa code, tạo file, cấu hình (`write_to_file`, `replace_file_content`).
  * Chạy test hoặc kiểm tra chẩn đoán để xác minh tính đúng đắn (`run_command`).
* **VÒNG LẶP HỒI TIẾP (Feedback Loop):**
  * Nếu trong quá trình thực thi phát sinh lỗi (error, build fail, test fail, hoặc giao diện không như ý):
  * Antigravity **LẬP TỨC QUAY LẠI GỬI LỖI CHO GEMINI WEB QUA `gemini_web_auto_repair`**:
    > Gửi toàn bộ error log + file liên quan sang Argon 4 (Cao), tự động ghi đè bản vá với `auto_apply: true`, lặp lại tối đa 5 chu kỳ cho đến khi hoàn toàn hết lỗi!

---

### Giai Đoạn 4: 🛡️ CỔNG BẢO MẬT & QUYỀN RIÊNG TƯ (MANDATORY SECURITY GATE)

> [!CAUTION]
> **Antigravity BẮT BUỘC PHẢI DÙNG CÔNG CỤ `ask_question` ĐỂ HỎI Ý KIẾN VÀ ĐƯỢC NGƯỜI DÙNG ĐỒNG Ý TRƯỚC KHI THỰC HIỆN BẤT KỲ HÀNH ĐỘNG NÀO SAU ĐÂY:**

1. **Khóa Bí Mật & Thông Tin Xác Thực (Secrets & Credentials):**
   - Đọc, ghi đè, hiển thị hoặc sửa đổi các file nhạy cảm: `.env`, Private Key, Secret Token, API Keys, SSH Keys, Certificates.
2. **Dữ Liệu Riêng Tư & Cá Nhân (User Privacy):**
   - Truy cập vào dữ liệu người dùng cá nhân (thông tin tài khoản, danh bạ, email cá nhân) hoặc chia sẻ ra ngoài môi trường local máy tính.
3. **Thao Tác Phá Hủy & Rủi Ro Cao (Destructive Actions):**
   - Xóa file hoặc thư mục (`rm`, `rimraf`, `Remove-Item`), xóa bảng/dữ liệu cơ sở dữ liệu, chạy `git reset --hard`, `git push --force`.
4. **Hành Vi Truy Cập Mạng Bên Ngoài (External Network Egress):**
   - Gửi dữ liệu ra các domain/endpoint lạ nằm ngoài `127.0.0.1` và `gemini.google.com`.

**Quy tắc ứng xử:**  
Khi gặp bất kỳ tình huống nào thuộc 4 nhóm trên, Antigravity **PHẢI DỪNG LẠI NGAY LẬP TỨC**, giải thích lý do rõ ràng và dùng `ask_question` để người dùng chủ động lựa chọn đồng ý hoặc từ chối. Chỉ khi người dùng bấm xác nhận đồng ý thì mới được phép thực thi.

---

## ⚡ CÚ PHÁP CÂU LỆNH ĐIỀU KHIỂN BỘ 3 GEMINI (3-Tier Directives)

Khi người dùng ra lệnh bằng câu lệnh có tiền tố `(gemini)` hoặc `-(gemini)`, Antigravity **BẮT BUỘC** thực hiện 2 hành động:
1. **Liệt kê các Web Gemini hiện có**: Sử dụng `gemini_web_list_tabs` (hoặc kiểm tra status) để báo cáo các tab, tài khoản (`u/3`, `u/2`), mô hình và cấp độ tư duy hiện tại.
2. **Hệ Thống 4 Dòng Mô Hình & 3 Cấp Độ Tư Duy (Full 4x3 Matrix)**:

Trên giao diện `gemini.google.com`, mỗi mô hình đều có menu con **"Cấp độ tư duy"** gồm 3 nấc:
- **Thấp**: Nhanh chóng và hiệu quả (ưu tiên tốc độ phản hồi).
- **Trung bình**: Độ sâu cân bằng (giữa suy luận và thời gian).
- **Cao**: Cực kỳ kỹ lưỡng (suy luận từng bước, chuỗi tư duy dài nhất).

| Dòng Mô Hình | Cấp Độ Tư Duy | Cú Pháp Lệnh / Model Pref | Timeout Chuẩn | Mục Đích Sử Dụng |
| :--- | :--- | :--- | :--- | :--- |
| **🎯 4 Argon** *(Nhiệm vụ phức tạp)* | **Thấp** | `-(argon 4(thấp))` hoặc `argon_low` | **90s** (1.5 phút) | Việc khó, thiết kế kiến trúc, giải thuật, phản hồi nhanh |
| | **Trung bình** | `-(argon 4(trung bình))` hoặc `argon_medium` | **270s** (4.5 phút, 3x Thấp) | Việc khó, phân tích logic cân bằng, Master-Detail, Báo cáo |
| | **Cao** | `-(argon 4(cao))` hoặc `argon_high` | **1350s** (22.5 phút, 5x TB) | Việc cực khó, CSDL 3NF, Triggers, deadlock, concurrency, RCA bug phức tạp |
| **⚡ 3.8 Flash** *(Trợ giúp toàn diện)* | **Cao** | `-(flash 3.8(cao))` hoặc `flash_high` | **1350s** (22.5 phút, 5x TB) | Độ khó trung bình: viết code WinForms C#, unit tests, components, API |
| | **Trung bình** | `-(flash 3.8(trung bình))` hoặc `flash_medium` | **270s** (4.5 phút, 3x Thấp) | Hỗ trợ code thông thường, refactoring vừa phải, UIThemeHelper |
| | **Thấp** | `-(flash 3.8(thấp))` hoặc `flash_low` | **90s** (1.5 phút) | Việc dễ, siêu tốc độ: tra cứu cú pháp, format JSON/Regex/Markdown |
| **💎 3.1 Pro** *(Suy luận nâng cao)* | **Thấp / TB / Cao** | `-(pro 3.1(cao))` hoặc `pro_high` | **90s / 270s / 1350s** | Toán học, logic suy luận trừu tượng, thẩm định quy trình |
| **🚀 3.5 Flash-Lite** *(Trả lời nhanh nhất)* | **Thấp / TB / Cao** | `-(flash-lite 3.5(thấp))` hoặc `lite_low` | **90s / 270s / 1350s** | Tóm tắt cực nhanh, dịch thuật ngắn, tra từ điển |

> ⏱️ **Cơ Chế Dynamic Adaptive Timeout & Keep-Alive:**
> - Tuyệt đối không áp đặt một con số cứng 150s cho tất cả!
> - Việc khó trên **Argon 4 (Cao)** hoặc **Flash 3.8 (Cao)** tự động được cấp trần thời gian **1350 giây (22.5 phút)** để Gemini thoải mái suy nghĩ sâu và viết trọn vẹn khối mã nguồn lớn.
> - Chừng nào Gemini còn đang tạo nội dung (streaming / typing / stop button visible), watchdog **KHÔNG BAO GIỜ timeout ngắt ngang**!
>
> 🎯 **Chế Độ Hỏi Tập Trung 1 Vấn Đề (Focused Single-Task Execution):**
> - Antigravity giải quyết tuần tự từng vấn đề một cách dứt điểm và chất lượng cao nhất.
> - Antigravity được toàn quyền tự chọn mô hình phù hợp nhất theo độ khó của từng bài toán.

---

### 💡 2 CHẾ ĐỘ ĐIỀU PHỐI ĐA LUỒNG VƯỢT TRỘI (Multi-Model Orchestration Engine)

Hệ thống cung cấp **2 Lựa Chọn (Options)** giúp Antigravity mạnh mẽ và đa luồng hơn hẳn tính năng Teamwork Preview:

#### 🟢 Option 1: Tự Động Phân Rã & Điều Phối vào Bảng Ma Trận (`gemini_web_orchestrate` với `mode: 'auto_matrix'`)
- **Cách dùng**: Người dùng chỉ cần đưa ra mục tiêu tổng thể (ví dụ: *Làm trọn bộ Bài tập lớn C# WinForms 21 bảng, 9 yêu cầu*).
- **Cơ chế hoạt động**:
  - Tool tự động sinh **Bảng Ma Trận Nhiệm Vụ (Task Matrix Board)** gồm 5 giai đoạn tối ưu:
    1. **Argon 4 (Cao)**: CSDL chuẩn hóa 3NF, SQL Triggers AFTER INSERT/UPDATE/DELETE tự động hóa Yêu cầu 1-2-3, Transaction, Test Deadlock.
    2. **Argon 4 (Trung bình)**: Logic nghiệp vụ Master-Detail Hóa đơn Nhập/Bán, Báo cáo Doanh thu Tháng/Quý/Năm, Top 5 NV, Xuất Excel.
    3. **Argon 4 (Thấp)**: Class ADO.NET SqlClient `Functions.cs`, Tìm kiếm đa điều kiện dynamic SQL WHERE 1=1, Xử lý ảnh không lock file.
    4. **Flash 3.8 (Cao)**: Thiết kế Modern Flat UI Dashboard (#18181B / #F4F4F5), Lên bảng Controls, Code CRUD 14 danh mục & Main.
    5. **Flash 3.8 (Trung bình)**: `UIThemeHelper.cs` (DoubleBuffered, Alternating rows), Dữ liệu mẫu INSERT INTO 21 bảng, Validation.
  - Tool bắn **ĐỒNG THỜI ĐA LUỒNG (True Parallel)** đến tất cả các tab Gemini trên Chrome!
  - Thu thập toàn bộ câu trả lời, reasoning thoughts và code blocks vào một bảng tổng hợp hoàn chỉnh.

#### 🟡 Option 2: Người Dùng Tự Prompt Bằng Cú Pháp Phân Công (`gemini_web_batch_ask` hoặc `gemini_web_orchestrate` với `mode: 'manual_directives'`)
- **Cách dùng**: Người dùng tự viết prompt phân chia theo cú pháp:
```markdown
-(gemini)
-(argon 4(cao) - argon 4(trung) - argon 4(thấp) - flash 3.8(cao) - flash 3.8(trung bình))
-argon 4(cao) dùng để: [Phần việc cực khó, CSDL, Triggers, Test Suite]
-argon 4(trung) dùng để: [Phần việc khó, Master-Detail, Báo cáo Thống kê]
-argon 4(thấp) dùng để: [Class Functions.cs, Tìm kiếm đa điều kiện, Xử lý ảnh]
-flash 3.8(cao) dùng để: [Giao diện Modern Flat UI Dashboard, CRUD 14 danh mục]
-flash 3.8(trung bình) dùng để: [UIThemeHelper, Script dữ liệu mẫu 21 bảng, Hàm tiện ích]

câu hỏi của bạn:
[Nội dung bài toán chi tiết]
```
- **Cơ chế hoạt động**:
  - Antigravity gọi `gemini_web_orchestrate` hoặc `gemini_web_batch_ask` (hoặc gọi `gemini_web_ask` - hệ thống sẽ tự động bóc tách các khối `-argon...` và `-flash...` thành các subtasks).
  - Gửi song song đến từng tab tương ứng mà không bị gián đoạn hay nghẽn tuần tự.

---

### 🚀 NGUYÊN TẮC BẮT BUỘC THỰC THI (MANDATORY POST-CONSULTATION EXECUTION):
> [!IMPORTANT]
> **Antigravity TUYỆT ĐỐI KHÔNG ĐƯỢC CHỈ HỎI GEMINI RỒI BỎ ĐẤY!**
> Ngay sau khi nhận được Ma Trận Kết Quả và toàn bộ code blocks từ các mô hình Gemini Web:
> 1. Antigravity **BẮT BUỘC PHẢI BẮT TAY VÀO THỰC THI NGAY**:
>    - Tạo file CSDL `.sql` và ghi đầy đủ script 21 bảng, triggers, dữ liệu mẫu (`write_to_file`).
>    - Tạo/sửa các file code C# (`Functions.cs`, `UIThemeHelper.cs`, các Forms) (`replace_file_content`).
> 2. Kiểm tra lại tính tương thích và báo cáo tiến độ từng giai đoạn cho người dùng!

---

### 🩺 VÒNG LẶP TỰ ĐỘNG SỬA LỖI KHÉP KÍN (Autonomous Self-Healing Loop via `gemini_web_auto_repair`):
> [!TIP]
> **Khi chạy bất kỳ lệnh biên dịch, kiểm thử hoặc build gặp lỗi (exit code != 0):**
> 1. Antigravity **TUYỆT ĐỐI KHÔNG DỪNG LẠI HỎI NGƯỜI DÙNG**.
> 2. Antigravity tự động kích hoạt công cụ **`gemini_web_auto_repair`**:
>    - Gửi toàn bộ compiler/terminal error log + code file hiện tại sang **Argon 4 (Cao)**.
>    - Argon 4 phân tích Root Cause Analysis (RCA) và sinh bản vá code chính xác.
>    - Với `auto_apply: true`, công cụ tự động ghi đè bản vá vào đúng các file nguồn.
> 3. Antigravity chạy lại lệnh kiểm tra, lặp lại tối đa **5 chu kỳ** cho đến khi đạt kết quả hoàn hảo 100% với 0 lỗi!
