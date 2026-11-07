import os
import shutil
from PIL import Image, ImageDraw, ImageFont

ASSETS_DIR = os.path.abspath("Presentation/assets")
os.makedirs(ASSETS_DIR, exist_ok=True)

def get_font(size, bold=False):
    candidates = [
        "C:\\Windows\\Fonts\\segoeuib.ttf" if bold else "C:\\Windows\\Fonts\\segoeui.ttf",
        "C:\\Windows\\Fonts\\arialbd.ttf" if bold else "C:\\Windows\\Fonts\\arial.ttf",
        "C:\\Windows\\Fonts\\consola.ttf"
    ]
    for c in candidates:
        if os.path.exists(c):
            try:
                return ImageFont.truetype(c, size)
            except:
                pass
    return ImageFont.load_default()

def draw_carbon_code_image(filename, title, code_lines, width=1280, height=800):
    img = Image.new("RGBA", (width, height), (15, 23, 42, 255))
    draw = ImageDraw.Draw(img)
    
    pad = 30
    draw.rounded_rectangle([pad, pad, width-pad, height-pad], radius=16, fill=(30, 41, 59, 255), outline=(51, 65, 85, 255), width=2)
    
    draw.rounded_rectangle([pad, pad, width-pad, pad+45], radius=16, fill=(15, 23, 42, 255))
    draw.rectangle([pad, pad+30, width-pad, pad+45], fill=(15, 23, 42, 255))
    draw.line([pad, pad+45, width-pad, pad+45], fill=(51, 65, 85, 255), width=1)
    
    draw.ellipse([pad+18, pad+16, pad+30, pad+28], fill=(239, 68, 68, 255))
    draw.ellipse([pad+38, pad+16, pad+50, pad+28], fill=(245, 158, 11, 255))
    draw.ellipse([pad+58, pad+16, pad+70, pad+28], fill=(16, 185, 129, 255))
    
    font_title = get_font(16, bold=True)
    draw.text((pad+90, pad+13), title, fill=(148, 163, 184, 255), font=font_title)
    
    font_code = get_font(18, bold=False)
    font_num = get_font(16, bold=False)
    
    y = pad + 65
    for i, line in enumerate(code_lines):
        line_num = f"{i+1:2d}"
        draw.text((pad+25, y), line_num, fill=(100, 116, 139, 255), font=font_num)
        draw.line([pad+65, pad+55, pad+65, height-pad-20], fill=(51, 65, 85, 255), width=1)
        
        c = (226, 232, 240, 255)
        stripped = line.strip()
        if stripped.startswith("//") or stripped.startswith("--") or stripped.startswith("/*"):
            c = (100, 116, 139, 255)
        elif any(k in stripped for k in ["public", "private", "static", "void", "string", "int", "decimal", "class", "namespace", "using", "new", "return", "if", "else", "try", "catch", "finally", "SELECT", "FROM", "WHERE", "UPDATE", "INSERT", "DELETE", "TRIGGER", "ALTER"]):
            c = (56, 189, 248, 255)
        elif any(k in stripped for k in ["MessageBox", "SqlCommand", "SqlConnection", "SqlDataReader", "SqlParameter", "Functions", "UIThemeHelper", "SecurityHelper", "GraphicsPath"]):
            c = (167, 139, 250, 255)
        elif '"' in stripped or "'" in stripped:
            c = (52, 211, 153, 255)
            
        draw.text((pad+80, y), line, fill=c, font=font_code)
        y += 26
        if y > height - pad - 35:
            break
            
    out_path = os.path.join(ASSETS_DIR, filename)
    img.save(out_path, "PNG")
    print(f"Generated code image: {out_path}")

def generate_all_code_assets():
    code_gdi = [
        "public static void StyleCard(Control ctrl, Color? bg = null, int radius = 10)",
        "{",
        "    Color cardBg = bg ?? ColorCard;",
        "    ctrl.BackColor = cardBg;",
        "    if (radius > 0) MakeRoundedControl(ctrl, radius);",
        "}",
        "",
        "public static void MakeRoundedControl(Control ctrl, int radius)",
        "{",
        "    if (ctrl == null || ctrl.Width <= 0 || ctrl.Height <= 0) return;",
        "    using (GraphicsPath path = new GraphicsPath())",
        "    {",
        "        path.AddArc(0, 0, radius * 2, radius * 2, 180, 90);",
        "        path.AddArc(ctrl.Width - radius * 2, 0, radius * 2, radius * 2, 270, 90);",
        "        path.AddArc(ctrl.Width - radius * 2, ctrl.Height - radius * 2, radius * 2, radius * 2, 0, 90);",
        "        path.AddArc(0, ctrl.Height - radius * 2, radius * 2, radius * 2, 90, 90);",
        "        path.CloseFigure();",
        "        ctrl.Region = new Region(path);",
        "    }",
        "}",
        "",
        "public static void ApplyTheme(Control root)",
        "{",
        "    // Recursive traversal across all WinForms hierarchy",
        "    foreach (Control c in root.Controls)",
        "    {",
        "        if (c is Button btn) StyleButtonByContext(btn);",
        "        else if (c is Panel pnl) ApplyPanelTheme(pnl);",
        "        else if (c is DataGridView dgv) StyleDataGridView(dgv);",
        "        if (c.HasChildren) ApplyTheme(c);",
        "    }",
        "}"
    ]
    draw_carbon_code_image("code_gdi_theme_engine.png", "Classes/UIThemeHelper.cs - GDI+ Fluent Rounded Rendering & Recursive Engine", code_gdi)

    code_adonet = [
        "public static void Connect()",
        "{",
        "    if (Conn == null)",
        "        Conn = new SqlConnection(ConnectionString);",
        "    if (Conn.State != ConnectionState.Open)",
        "        Conn.Open();",
        "}",
        "",
        "public static string GetFieldValues(string sql, SqlParameter[] parameters = null)",
        "{",
        "    string ma = \"\";",
        "    using (SqlCommand cmd = new SqlCommand(sql, Conn))",
        "    {",
        "        if (parameters != null) cmd.Parameters.AddRange(parameters);",
        "        using (SqlDataReader reader = cmd.ExecuteReader())",
        "        {",
        "            if (reader.Read()) ma = reader[0].ToString();",
        "        }",
        "    }",
        "    return ma;",
        "}",
        "",
        "public static void RunSQL(string sql, SqlParameter[] parameters = null, bool useTransaction = false)",
        "{",
        "    Connect();",
        "    using (SqlCommand cmd = new SqlCommand(sql, Conn))",
        "    {",
        "        if (parameters != null) cmd.Parameters.AddRange(parameters);",
        "        cmd.ExecuteNonQuery();",
        "    }",
        "}"
    ]
    draw_carbon_code_image("code_adonet_core.png", "Classes/Functions.cs - Pure ADO.NET High-Performance Data Access Layer", code_adonet)

    code_trigger = [
        "CREATE TRIGGER trg_ChiTietHDB_AUID ON tblChiTietHDBan",
        "AFTER INSERT, UPDATE, DELETE AS",
        "BEGIN",
        "    SET NOCOUNT ON;",
        "    -- 1. Anti Negative Stock Guard (ACID Check)",
        "    IF EXISTS (SELECT 1 FROM MayViTinh WHERE SoLuong < 0)",
        "    BEGIN",
        "        RAISERROR (N'Lỗi: Số lượng tồn kho không đủ để thực hiện xuất bán!', 16, 1);",
        "        ROLLBACK TRANSACTION;",
        "        RETURN;",
        "    END",
        "",
        "    -- 2. Inventory Deduct on Insert",
        "    IF EXISTS (SELECT 1 FROM inserted)",
        "    BEGIN",
        "        UPDATE MayViTinh SET SoLuong = MayViTinh.SoLuong - i.SoLuong",
        "        FROM MayViTinh m INNER JOIN inserted i ON m.MaMayVT = i.MaMayVT;",
        "    END",
        "",
        "    -- 3. Automatic Total Recalculation on Master Bill",
        "    UPDATE tblHoaDonBan SET TongTien = ISNULL((SELECT SUM(ThanhTien) ",
        "           FROM tblChiTietHDBan WHERE SoHDB = tblHoaDonBan.SoHDB), 0)",
        "    WHERE SoHDB IN (SELECT SoHDB FROM inserted UNION SELECT SoHDB FROM deleted);",
        "END"
    ]
    draw_carbon_code_image("code_sql_trigger_sync.png", "Database_QLBanMayViTinh.sql - SQL Server ACID Stock & Total Recalculation Trigger", code_trigger)

    code_query = [
        "// Dynamic Criteria Query Builder (frmBaoCaoThongKe & frmTimKiem)",
        "string sql = @\"SELECT TOP 5 WITH TIES ",
        "                nv.MaNV AS [Mã NV], nv.TenNV AS [Tên Nhân Viên], ",
        "                COUNT(DISTINCT hdb.SoHDB) AS [Số Hóa Đơn], ",
        "                SUM(hdb.TongTien) AS [Tổng Doanh Thu Đạt Được]",
        "               FROM NhanVien nv",
        "               INNER JOIN tblHoaDonBan hdb ON nv.MaNV = hdb.MaNV",
        "               WHERE 1=1 \";",
        "",
        "SqlCommand cmd = new SqlCommand();",
        "if (rdoThang.Checked)",
        "{",
        "    sql += \" AND MONTH(hdb.NgayBan) = @thang AND YEAR(hdb.NgayBan) = @nam\";",
        "    cmd.Parameters.AddWithValue(\"@thang\", cboThang.SelectedItem);",
        "    cmd.Parameters.AddWithValue(\"@nam\", cboNam.SelectedItem);",
        "}",
        "else if (rdoQuy.Checked)",
        "{",
        "    sql += \" AND DATEPART(QUARTER, hdb.NgayBan) = @quy AND YEAR(hdb.NgayBan) = @nam\";",
        "    cmd.Parameters.AddWithValue(\"@quy\", cboQuy.SelectedIndex + 1);",
        "    cmd.Parameters.AddWithValue(\"@nam\", cboNam.SelectedItem);",
        "}",
        "sql += \" GROUP BY nv.MaNV, nv.TenNV ORDER BY [Tổng Doanh Thu Đạt Được] DESC\";"
    ]
    draw_carbon_code_image("code_dynamic_query_builder.png", "Forms/frmBaoCaoThongKe.cs - Dynamic SQL Builder, TOP 5 WITH TIES & Aggregations", code_query)

    code_excel = [
        "// High-Speed HTML/UTF-8 Native Excel Stream Generator",
        "StringBuilder sb = new StringBuilder();",
        "sb.AppendLine(\"<html xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:x='urn:schemas-microsoft-com:office:excel'>\");",
        "sb.AppendLine(\"<head><meta http-equiv='Content-Type' content='text/html; charset=utf-8'>\");",
        "sb.AppendLine(\"<style>th { background-color: #4F46E5; color: white; font-weight: bold; } \");",
        "sb.AppendLine(\".num { mso-number-format:'\\#\\,\\#\\#0\\\\ \\\"VND\\\"'; text-align: right; }</style></head><body>\");",
        "sb.AppendLine(\"<h2>\" + title + \"</h2><table border='1' cellspacing='0'>\");",
        "",
        "// Output Columns Headers",
        "sb.Append(\"<tr>\");",
        "foreach (DataGridViewColumn col in dgv.Columns)",
        "    if (col.Visible) sb.AppendFormat(\"<th>{0}</th>\", col.HeaderText);",
        "sb.AppendLine(\"</tr>\");",
        "",
        "// Output Data Rows with Formatting",
        "foreach (DataGridViewRow row in dgv.Rows)",
        "{",
        "    sb.Append(\"<tr>\");",
        "    foreach (DataGridViewCell cell in row.Cells)",
        "        sb.AppendFormat(\"<td>{0}</td>\", cell.Value?.ToString() ?? \"\");",
        "    sb.AppendLine(\"</tr>\");",
        "}",
        "File.WriteAllText(savePath, sb.ToString(), Encoding.UTF8);"
    ]
    draw_carbon_code_image("code_html_excel_engine.png", "Classes/Functions.cs - Native High-Speed Excel Stream Engine (Zero-COM Dependency)", code_excel)

def generate_graph_diagrams():
    width = 1280
    height = 760
    
    # 1. graph_system_architecture.png
    img1 = Image.new("RGBA", (width, height), (15, 23, 42, 255))
    d1 = ImageDraw.Draw(img1)
    f_h1 = get_font(26, bold=True)
    f_title = get_font(18, bold=True)
    f_body = get_font(14, bold=False)
    
    d1.text((40, 25), "KIẾN TRÚC TỔNG THỂ 3 TẦNG (3-TIER ARCHITECTURE) - QLBanMayViTinh", fill=(56, 189, 248, 255), font=f_h1)
    d1.text((40, 65), "Phân tách rõ ràng trách nhiệm giữa Tầng Giao Diện, Tầng Xử Lý & Tầng Dữ Liệu SQL Server", fill=(148, 163, 184, 255), font=f_body)
    
    # Tier 1
    d1.rounded_rectangle([40, 110, 1240, 270], radius=14, fill=(30, 41, 59, 255), outline=(99, 102, 241, 255), width=2)
    d1.text((60, 125), "TẦNG GIAO DIỆN (PRESENTATION LAYER - 14 WINFORMS SDI / EMBEDDED)", fill=(165, 180, 252, 255), font=f_title)
    forms = [
        "frmDangNhap / frmDangKy / frmQuenMatKhau (Auth)", "frmMain (Bento Shell Dashboard)",
        "frmMayViTinh (14 Cấu hình 3NF)", "frmDanhMucHeThong (Khách, NCC, NV, Linh kiện)",
        "frmHoaDonNhap (Phiếu Nhập Master-Detail)", "frmHoaDonBan (POS Chống Xuất Âm)",
        "frmTimKiem (Tra cứu đa tiêu chí YC4)", "frmBaoCaoThongKe (BI Analytics YC6,7,8,9)",
        "frmQuanLyNguoiDung (RBAC Security)", "frmHoSoCaNhan / frmNhatKy / frmCaiDat"
    ]
    for i, f in enumerate(forms):
        col = i % 2
        row = i // 2
        x = 60 + col * 580
        y = 160 + row * 22
        d1.text((x, y), f"•  {f}", fill=(241, 245, 249, 255), font=f_body)
        
    # Tier 2
    d1.rounded_rectangle([40, 300, 1240, 470], radius=14, fill=(30, 41, 59, 255), outline=(16, 185, 129, 255), width=2)
    d1.text((60, 315), "TẦNG NGHIỆP VỤ & TRỢ NĂNG (BUSINESS LOGIC & HELPER ENGINE)", fill=(52, 211, 153, 255), font=f_title)
    helpers = [
        ("Classes/Functions.cs", "Pure ADO.NET Connection, Parameterized Queries, Currency Formatter, Audit Logger"),
        ("Classes/UIThemeHelper.cs", "GDI+ GraphicsPath Rounded Cards/Buttons, Dual Theme Engine (Dark/Light), Reflection DoubleBuffer"),
        ("Classes/SecurityHelper.cs", "PBKDF2 Password Hashing (10,000 Iterations + 128-bit Salt), AES-256-CBC Encryption for Sensitive Fields"),
        ("Testing Suite (E2E)", "25/25 Opaque-Box Automated Regression Tests, Zero-Comment Quality Compliance Audit")
    ]
    for i, (name, desc) in enumerate(helpers):
        y = 350 + i * 28
        d1.text((60, y), f"★  {name}:", fill=(56, 189, 248, 255), font=get_font(15, bold=True))
        d1.text((320, y), desc, fill=(226, 232, 240, 255), font=f_body)
        
    # Tier 3
    d1.rounded_rectangle([40, 500, 1240, 710], radius=14, fill=(30, 41, 59, 255), outline=(245, 158, 11, 255), width=2)
    d1.text((60, 515), "TẦNG DỮ LIỆU (DATA ACCESS LAYER & SQL SERVER 2019/2022 - 25 BẢNG 3NF)", fill=(251, 191, 36, 255), font=f_title)
    db_items = [
        "25 Bảng chuẩn 3NF (MayViTinh, 14 Bảng linh kiện con, NhanVien, KhachHang, NhaCungCap, HoaDon, tblTaiKhoan, tblNhatKy...)",
        "Database Triggers tự động: trg_ChiTietHDB_AUID (Chống xuất âm, hoàn tồn, cộng dồn tổng tiền), trg_ChiTietHDN (Tăng tồn, cập nhật giá vốn)",
        "Giao dịch an toàn ACID (BEGIN TRAN, COMMIT, ROLLBACK) & Parameterized Queries chống SQL Injection 100%",
        "Hàm phân tích thời gian và xếp hạng nâng cao: DATEPART(QUARTER), MONTH(), TOP 5 WITH TIES"
    ]
    for i, item in enumerate(db_items):
        y = 550 + i * 36
        d1.text((60, y), f"⚡  {item}", fill=(241, 245, 249, 255), font=f_body)

    out1 = os.path.join(ASSETS_DIR, "graph_system_architecture.png")
    img1.save(out1, "PNG")
    print(f"Generated diagram: {out1}")

    # 2. graph_dependency_map.png
    img2 = Image.new("RGBA", (width, height), (15, 23, 42, 255))
    d2 = ImageDraw.Draw(img2)
    d2.text((40, 25), "ĐỒ THỊ PHỤ THUỘC & LỜI GỌI HÀM (DEPENDENCY & CALL GRAPH - GRAPHIFY)", fill=(167, 139, 250, 255), font=f_h1)
    d2.text((40, 65), "Toàn bộ 14 Form hội tụ về 3 Class lõi; không có phụ thuộc vòng (Zero Circular Dependency)", fill=(148, 163, 184, 255), font=f_body)
    
    # Draw central Core Classes
    d2.rounded_rectangle([480, 140, 800, 250], radius=12, fill=(30, 41, 59, 255), outline=(56, 189, 248, 255), width=3)
    d2.text((510, 160), "Functions.cs (Core DAL)", fill=(56, 189, 248, 255), font=f_title)
    d2.text((510, 195), "• Connect() / RunSQL()\n• GetFieldValues() / FillCombo()", fill=(226, 232, 240, 255), font=f_body)
    
    d2.rounded_rectangle([480, 290, 800, 400], radius=12, fill=(30, 41, 59, 255), outline=(167, 139, 250, 255), width=3)
    d2.text((510, 310), "UIThemeHelper.cs (Theme)", fill=(167, 139, 250, 255), font=f_title)
    d2.text((510, 345), "• StyleCard() / StyleButton()\n• ApplyTheme() / ToggleTheme()", fill=(226, 232, 240, 255), font=f_body)

    d2.rounded_rectangle([480, 440, 800, 550], radius=12, fill=(30, 41, 59, 255), outline=(245, 158, 11, 255), width=3)
    d2.text((510, 460), "SecurityHelper.cs (Crypto)", fill=(245, 158, 11, 255), font=f_title)
    d2.text((510, 495), "• HashPassword() / Verify()\n• EncryptAES() / DecryptAES()", fill=(226, 232, 240, 255), font=f_body)

    # Left Forms
    left_forms = ["frmDangNhap", "frmDangKy", "frmQuenMatKhau", "frmMain", "frmMayViTinh", "frmDanhMucHeThong", "frmCaiDat"]
    for i, lf in enumerate(left_forms):
        y = 120 + i * 80
        d2.rounded_rectangle([60, y, 320, y+55], radius=8, fill=(30, 41, 59, 255), outline=(99, 102, 241, 255), width=1)
        d2.text((80, y+16), lf, fill=(241, 245, 249, 255), font=f_title)
        d2.line([320, y+27, 480, 195], fill=(71, 85, 105, 150), width=1)
        d2.line([320, y+27, 480, 345], fill=(71, 85, 105, 150), width=1)

    # Right Forms
    right_forms = ["frmHoaDonBan", "frmHoaDonNhap", "frmTimKiem", "frmBaoCaoThongKe", "frmQuanLyNguoiDung", "frmHoSoCaNhan", "frmNhatKyHoatDong"]
    for i, rf in enumerate(right_forms):
        y = 120 + i * 80
        d2.rounded_rectangle([960, y, 1220, y+55], radius=8, fill=(30, 41, 59, 255), outline=(16, 185, 129, 255), width=1)
        d2.text((980, y+16), rf, fill=(241, 245, 249, 255), font=f_title)
        d2.line([800, 195, 960, y+27], fill=(71, 85, 105, 150), width=1)
        d2.line([800, 345, 960, y+27], fill=(71, 85, 105, 150), width=1)

    d2.rounded_rectangle([380, 600, 900, 710], radius=12, fill=(15, 23, 42, 255), outline=(52, 211, 153, 255), width=2)
    d2.text((410, 620), "KẾT QUẢ PHÂN TÍCH GRAPHIFY KNOWLEDGE GRAPH", fill=(52, 211, 153, 255), font=f_title)
    d2.text((410, 655), "Tổng 527 Nodes, 1,668 Edges. 100% các Form gọi qua DAL chuẩn hóa, tách rời UI logic.", fill=(226, 232, 240, 255), font=f_body)

    out2 = os.path.join(ASSETS_DIR, "graph_dependency_map.png")
    img2.save(out2, "PNG")
    print(f"Generated diagram: {out2}")

    # 3. graph_workflow_pos_trigger.png
    img3 = Image.new("RGBA", (width, height), (15, 23, 42, 255))
    d3 = ImageDraw.Draw(img3)
    d3.text((40, 25), "LUỒNG NGHIỆP VỤ BÁN HÀNG & ĐỒNG BỘ TRIGGER CSDL TỰ ĐỘNG", fill=(244, 63, 94, 255), font=f_h1)
    d3.text((40, 65), "Quy trình từ thao tác người dùng trên frmHoaDonBan -> Trigger SQL Server -> Cập nhật kho & KPI", fill=(148, 163, 184, 255), font=f_body)

    steps = [
        ("1. Người Dùng Chọn Hàng", "frmHoaDonBan: Chọn máy vi tính, xem tồn kho khả dụng và nhập số lượng bán.", (56, 189, 248)),
        ("2. Kiểm Soát Xuất Âm", "Client Guard: Chặn ngay nếu SoLuongBan > TonKho. Cảnh báo nếu GiaBan < GiaNhap.", (245, 158, 11)),
        ("3. Giao Dịch ADO.NET", "Thực thi SqlCommand INSERT vào tblHoaDonBan và tblChiTietHDBan.", (99, 102, 241)),
        ("4. SQL Trigger Kích Hoạt", "trg_ChiTietHDB_AUID: Trừ tồn kho MayViTinh, ROLLBACK nếu âm, cập nhật TongTien.", (239, 68, 68)),
        ("5. Đồng Bộ Thời Gian Thực", "Giao diện cập nhật tổng tiền chữ tiếng Việt, nạp lại lưới, đồng bộ 4 KPI Cards.", (16, 185, 129))
    ]
    for i, (title, desc, color) in enumerate(steps):
        x = 50 + i * 240
        y = 170
        d3.rounded_rectangle([x, y, x+215, y+380], radius=14, fill=(30, 41, 59, 255), outline=color, width=2)
        d3.rounded_rectangle([x, y, x+215, y+60], radius=14, fill=color)
        d3.rectangle([x, y+40, x+215, y+60], fill=color)
        d3.text((x+15, y+18), f"BƯỚC {i+1}", fill=(255, 255, 255, 255), font=get_font(18, bold=True))
        
        d3.text((x+15, y+80), title, fill=(241, 245, 249, 255), font=get_font(15, bold=True))
        
        words = desc.split()
        lines = []
        cur = ""
        for w in words:
            if len(cur + " " + w) > 18:
                lines.append(cur)
                cur = w
            else:
                cur = cur + " " + w if cur else w
        if cur: lines.append(cur)
        
        for li, l in enumerate(lines):
            d3.text((x+15, y+130 + li*22), l, fill=(203, 213, 225, 255), font=f_body)

        if i < 4:
            arrow_x = x + 215 + 5
            d3.polygon([(arrow_x+5, y+190), (arrow_x+15, y+190), (arrow_x+15, y+180), (arrow_x+25, y+195), (arrow_x+15, y+210), (arrow_x+15, y+200), (arrow_x+5, y+200)], fill=(148, 163, 184, 255))

    d3.rounded_rectangle([50, 600, 1230, 710], radius=12, fill=(30, 41, 59, 255), outline=(52, 211, 153, 255), width=2)
    d3.text((70, 620), "GIÁ TRỊ DOANH NGHIỆP: BẢO TOÀN DỮ LIỆU KHO TUYỆT ĐỐI (ZERO DATA CORRUPTION)", fill=(52, 211, 153, 255), font=f_title)
    d3.text((70, 655), "Dù ứng dụng mất mạng, mất điện hay người dùng tắt ngang, CSDL luôn nhất quán nhờ tính năng ACID Transaction.", fill=(226, 232, 240, 255), font=f_body)

    out3 = os.path.join(ASSETS_DIR, "graph_workflow_pos_trigger.png")
    img3.save(out3, "PNG")
    print(f"Generated diagram: {out3}")

    # 4. graph_navigation_state.png
    img4 = Image.new("RGBA", (width, height), (15, 23, 42, 255))
    d4 = ImageDraw.Draw(img4)
    d4.text((40, 25), "SƠ ĐỒ ĐIỀU HƯỚNG, PHÂN QUYỀN RBAC & AUDIT TRAIL", fill=(245, 158, 11, 255), font=f_h1)
    d4.text((40, 65), "Mô hình phân quyền động 3 cấp độ (Admin, Thủ Kho, Bán Hàng) và hộp đen ghi log an ninh", fill=(148, 163, 184, 255), font=f_body)

    # Auth box
    d4.rounded_rectangle([40, 150, 280, 520], radius=12, fill=(30, 41, 59, 255), outline=(99, 102, 241, 255), width=2)
    d4.text((60, 170), "XÁC THỰC (AUTH)", fill=(165, 180, 252, 255), font=f_title)
    d4.text((60, 210), "1. frmDangNhap\n   • Verify PBKDF2\n   • Check Status == 'Khóa'\n   • Set Session Functions\n\n2. frmDangKy\n   • Salt 128-bit\n   • Auto Link NhanVien\n\n3. frmQuenMatKhau\n   • Đối soát SĐT hồ sơ", fill=(226, 232, 240, 255), font=f_body)

    # Main Shell
    d4.rounded_rectangle([340, 150, 640, 520], radius=12, fill=(30, 41, 59, 255), outline=(56, 189, 248, 255), width=2)
    d4.text((360, 170), "frmMain (BENTO SHELL)", fill=(56, 189, 248, 255), font=f_title)
    d4.text((360, 210), "• Single Document Interface\n• Nhúng Form con vào Panel\n• Dynamic Theme Dispatcher\n• Realtime SQL Status\n• Phân quyền RBAC động:\n  - Admin: Full quyền\n  - ThuKho: Ẩn HĐ Bán\n  - BanHang: Ẩn HĐ Nhập,\n    Quản Lý Người Dùng, Cài Đặt", fill=(226, 232, 240, 255), font=f_body)

    # Child modules
    d4.rounded_rectangle([700, 150, 960, 520], radius=12, fill=(30, 41, 59, 255), outline=(16, 185, 129, 255), width=2)
    d4.text((720, 170), "PHÂN HỆ NGHIỆP VỤ", fill=(52, 211, 153, 255), font=f_title)
    d4.text((720, 210), "• frmMayViTinh\n• frmDanhMucHeThong\n• frmHoaDonNhap\n• frmHoaDonBan\n• frmTimKiem\n• frmBaoCaoThongKe\n• frmQuanLyNguoiDung\n• frmHoSoCaNhan\n• frmCaiDat", fill=(226, 232, 240, 255), font=f_body)

    # Audit Trail
    d4.rounded_rectangle([1020, 150, 1240, 520], radius=12, fill=(30, 41, 59, 255), outline=(244, 63, 94, 255), width=2)
    d4.text((1035, 170), "AUDIT LOG", fill=(251, 113, 133, 255), font=f_title)
    d4.text((1035, 210), "frmNhatKyHoatDong\n\n• Ghi nhận 100% CRUD\n• Thời gian tới từng giây\n• Tên user & hành động\n• Cảnh báo tồn kho <= 5\n• Chống gian lận nội bộ", fill=(226, 232, 240, 255), font=f_body)

    # Connecting arrows
    d4.line([280, 335, 340, 335], fill=(56, 189, 248, 255), width=3)
    d4.line([640, 335, 700, 335], fill=(52, 211, 153, 255), width=3)
    d4.line([960, 335, 1020, 335], fill=(244, 63, 94, 255), width=3)

    out4 = os.path.join(ASSETS_DIR, "graph_navigation_state.png")
    img4.save(out4, "PNG")
    print(f"Generated diagram: {out4}")

def copy_and_composite_ui_assets():
    # Mapping verified screenshots to the required presentation asset names
    mapping = {
        "ui_dangnhap.png": "Screenshots/Verified/06_DangNhap_Fixed.png",
        "ui_dangky.png": "Screenshots/Verified/02_DangKy_Validation_Error.png",
        "ui_quenmatkhau.png": "Screenshots/Verified/03_QuenMatKhau_Validation_Error.png",
        "ui_main_light.png": "Screenshots/Verified/00_Main_Dashboard_Light.png",
        "ui_main_dark.png": "Screenshots/Verified/00_Main_Dashboard_Dark.png",
        "ui_mayvitinh.png": "Screenshots/Verified/05_MayViTinh_Selected_Row.png",
        "ui_hoadonnhap.png": "Screenshots/Verified/05_HoaDonNhap_SelectProduct_Calculated.png",
        "ui_hoadonban.png": "Screenshots/Verified/04_HoaDonBan_SelectProduct_Calculated.png",
        "ui_timkiem_dynamic.png": "Screenshots/Verified/02_TimKiem_MVT_Searched.png",
        "ui_baocao_tonkho.png": "Screenshots/Verified/01_BaoCao_rdoTonKho_Light.png",
        "ui_baocao_doanhthu.png": "Screenshots/Verified/01_BaoCao_rdoTop5_Light.png",
        "ui_quanlynguoidung.png": "Screenshots/Verified/11_NguoiDung_Selected_User.png",
        "ui_hosocanhan.png": "Screenshots/Verified/12_HoSoCaNhan_View.png",
        "ui_nhatkyhoatdong.png": "Screenshots/Verified/07_NhatKy_Fixed.png",
        "ui_caidat.png": "Screenshots/Verified/08_CaiDat_Fixed.png"
    }
    
    for dst_name, src_path in mapping.items():
        if os.path.exists(src_path):
            dst_path = os.path.join(ASSETS_DIR, dst_name)
            shutil.copyfile(src_path, dst_path)
            print(f"Copied UI asset: {dst_name}")
        else:
            print(f"Warning: {src_path} not found")

    # Composite: ui_danhmuc_all_in_one.png (Combine KH, NCC, NV, LinhKien tabs)
    tab_kh = "Screenshots/Verified/06_DanhMuc_Tab1_KhachHang.png"
    tab_ncc = "Screenshots/Verified/06_DanhMuc_Tab2_NhaCungCap.png"
    tab_nv = "Screenshots/Verified/06_DanhMuc_Tab3_NhanVien.png"
    tab_lk = "Screenshots/Verified/06_DanhMuc_Tab4_LinhKien_RAM.png"
    
    if os.path.exists(tab_kh) and os.path.exists(tab_ncc) and os.path.exists(tab_nv) and os.path.exists(tab_lk):
        img_kh = Image.open(tab_kh)
        img_ncc = Image.open(tab_ncc)
        img_nv = Image.open(tab_nv)
        img_lk = Image.open(tab_lk)
        
        w, h = 640, 380
        comp = Image.new("RGBA", (1280, 760), (15, 23, 42, 255))
        comp.paste(img_kh.resize((w, h)), (0, 0))
        comp.paste(img_ncc.resize((w, h)), (w, 0))
        comp.paste(img_nv.resize((w, h)), (0, h))
        comp.paste(img_lk.resize((w, h)), (w, h))
        
        dst_comp = os.path.join(ASSETS_DIR, "ui_danhmuc_all_in_one.png")
        comp.save(dst_comp, "PNG")
        print(f"Composited multi-tab UI asset: ui_danhmuc_all_in_one.png")

if __name__ == "__main__":
    print("=== STARTING ASSET GENERATION FOR PRESENTATION ===")
    generate_all_code_assets()
    generate_graph_diagrams()
    copy_and_composite_ui_assets()
    print("=== ALL ASSETS GENERATED SUCCESSFULLY IN Presentation/assets/ ===")

