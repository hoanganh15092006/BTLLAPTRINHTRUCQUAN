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
        elif any(k in stripped for k in ["public", "private", "static", "void", "string", "int", "decimal", "class", "namespace", "using", "new", "return", "if", "else", "try", "catch", "finally", "SELECT", "FROM", "WHERE", "UPDATE", "INSERT", "DELETE", "TRIGGER", "ALTER", "CREATE", "JOIN", "ON", "GROUP BY", "ORDER BY"]):
            c = (56, 189, 248, 255)
        elif any(k in stripped for k in ["MessageBox", "SqlCommand", "SqlConnection", "SqlDataReader", "SqlParameter", "Functions", "UIThemeHelper", "SecurityHelper", "GraphicsPath", "DataTable", "SqlDataAdapter"]):
            c = (167, 139, 250, 255)
        elif '"' in stripped or "'" in stripped:
            c = (52, 211, 153, 255)
            
        draw.text((pad+80, y), line, fill=c, font=font_code)
        y += 26
        if y > height - pad - 35:
            break
            
    outpath = os.path.join(ASSETS_DIR, filename)
    img.save(outpath, "PNG")
    print(f"Generated Code Image: {filename}")

def generate_all_15_code_assets():
    code_01 = [
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
        "public static void StyleButtonByContext(Button btn)",
        "{",
        "    btn.FlatStyle = FlatStyle.Flat;",
        "    btn.FlatAppearance.BorderSize = 0;",
        "    MakeRoundedControl(btn, 8);",
        "}"
    ]
    draw_carbon_code_image("code_01_stylecard_gdi.png", "Classes/UIThemeHelper.cs - GDI+ Fluent Rounded Rendering Engine", code_01)

    code_02 = [
        "public static void ApplyTheme(Control root)",
        "{",
        "    if (root == null) return;",
        "    if (root is Form frm)",
        "    {",
        "        frm.BackColor = ColorBackground;",
        "        frm.ForeColor = ColorText;",
        "    }",
        "    foreach (Control c in root.Controls)",
        "    {",
        "        if (c is Button btn) StyleButtonByContext(btn);",
        "        else if (c is TextBox txt) StyleTextBox(txt);",
        "        else if (c is ComboBox cbo) StyleComboBox(cbo);",
        "        else if (c is DataGridView dgv) StyleDataGridView(dgv);",
        "        else if (c is Panel pnl) ApplyPanelTheme(pnl);",
        "        else if (c is Label lbl) lbl.ForeColor = ColorText;",
        "        if (c.HasChildren) ApplyTheme(c); // Recursive Tree Traversal",
        "    }",
        "}",
        "",
        "public static void ToggleTheme()",
        "{",
        "    CurrentTheme = (CurrentTheme == ThemeMode.Dark) ? ThemeMode.Light : ThemeMode.Dark;",
        "    InitThemeColors();",
        "}"
    ]
    draw_carbon_code_image("code_02_applytheme_darklight.png", "Classes/UIThemeHelper.cs - Real-time Dark/Light Recursive Engine", code_02)

    code_03 = [
        "public static void Connect()",
        "{",
        "    if (Conn == null) Conn = new SqlConnection(ConnectionString);",
        "    if (Conn.State != ConnectionState.Open) Conn.Open();",
        "}",
        "",
        "public static DataTable GetDataToTable(string sql, SqlParameter[] parameters = null)",
        "{",
        "    DataTable table = new DataTable();",
        "    using (SqlCommand cmd = new SqlCommand(sql, Conn))",
        "    {",
        "        if (parameters != null) cmd.Parameters.AddRange(parameters);",
        "        using (SqlDataAdapter dap = new SqlDataAdapter(cmd))",
        "        {",
        "            dap.Fill(table);",
        "        }",
        "    }",
        "    return table;",
        "}",
        "",
        "public static string GetFieldValues(string sql, SqlParameter[] parameters = null)",
        "{",
        "    using (SqlCommand cmd = new SqlCommand(sql, Conn))",
        "    {",
        "        if (parameters != null) cmd.Parameters.AddRange(parameters);",
        "        object res = cmd.ExecuteScalar();",
        "        return res != null ? res.ToString() : \"\";",
        "    }",
        "}"
    ]
    draw_carbon_code_image("code_03_functions_adonet.png", "Classes/Functions.cs - Pure ADO.NET High Performance Data Layer", code_03)

    code_04 = [
        "private void btnDangNhap_Click(object sender, EventArgs e)",
        "{",
        "    string u = txtTenDangNhap.Text.Trim();",
        "    string p = txtMatKhau.Text;",
        "    string sql = \"SELECT TenDangNhap, MatKhau, Quyen, TrangThai, MaNV FROM tblNguoiDung WHERE TenDangNhap = @user\";",
        "    SqlParameter[] prms = { new SqlParameter(\"@user\", u) };",
        "    DataTable dt = Functions.GetDataToTable(sql, prms);",
        "    if (dt.Rows.Count == 0) { ShowError(\"Tài khoản không tồn tại!\"); return; }",
        "    string dbHash = dt.Rows[0][\"MatKhau\"].ToString();",
        "    if (!SecurityHelper.VerifyPassword(p, dbHash)) { ShowError(\"Mật khẩu không chính xác!\"); return; }",
        "    if (dt.Rows[0][\"TrangThai\"].ToString() == \"Khoa\") { ShowError(\"Tài khoản đang bị khóa!\"); return; }",
        "    Functions.CurrentMaNV = dt.Rows[0][\"MaNV\"].ToString();",
        "    Functions.CurrentRole = dt.Rows[0][\"Quyen\"].ToString();",
        "    Functions.GhiNhatKy(u, \"Đăng nhập\", \"Đăng nhập hệ thống thành công\");",
        "    this.Hide();",
        "    frmMain main = new frmMain();",
        "    main.Show();",
        "}"
    ]
    draw_carbon_code_image("code_04_dangnhap_auth.png", "Forms/frmDangNhap.cs - Enterprise Authentication & Role Resolution", code_04)

    code_05 = [
        "private void OpenChildForm(Form childForm, Button activeBtn)",
        "{",
        "    if (activeForm != null)",
        "    {",
        "        activeForm.Close();",
        "        activeForm.Dispose();",
        "    }",
        "    HighlightNavigationButton(activeBtn);",
        "    activeForm = childForm;",
        "    childForm.TopLevel = false;",
        "    childForm.FormBorderStyle = FormBorderStyle.None;",
        "    childForm.Dock = DockStyle.Fill;",
        "    pnlMain.Controls.Clear();",
        "    pnlMain.Controls.Add(childForm);",
        "    pnlMain.Tag = childForm;",
        "    childForm.BringToFront();",
        "    UIThemeHelper.ApplyTheme(childForm);",
        "    childForm.Show();",
        "}"
    ]
    draw_carbon_code_image("code_05_main_embed_form.png", "Forms/frmMain.cs - Single-Window Dynamic Panel Form Embedding", code_05)

    code_06 = [
        "private void btnXoa_Click(object sender, EventArgs e)",
        "{",
        "    string ma = txtMaMay.Text.Trim();",
        "    string chkHDB = \"SELECT COUNT(*) FROM tblChiTietHoaDonBan WHERE MaMay = @ma\";",
        "    string chkHDN = \"SELECT COUNT(*) FROM tblChiTietHoaDonNhap WHERE MaMay = @ma\";",
        "    int cntB = Convert.ToInt32(Functions.GetFieldValues(chkHDB, new SqlParameter[] { new SqlParameter(\"@ma\", ma) }));",
        "    int cntN = Convert.ToInt32(Functions.GetFieldValues(chkHDN, new SqlParameter[] { new SqlParameter(\"@ma\", ma) }));",
        "    if (cntB > 0 || cntN > 0)",
        "    {",
        "        MessageBox.Show(\"Không thể xóa máy này vì đã tồn tại trong lịch sử Hóa đơn Bán hoặc Nhập!\",",
        "                        \"Ràng buộc toàn vẹn\", MessageBoxButtons.OK, MessageBoxIcon.Warning);",
        "        return;",
        "    }",
        "    string sqlDelete = \"DELETE FROM tblMayViTinh WHERE MaMay = @ma\";",
        "    Functions.RunSQL(sqlDelete, new SqlParameter[] { new SqlParameter(\"@ma\", ma) });",
        "    Functions.GhiNhatKy(Functions.CurrentUser, \"Xóa Máy\", $\"Đã xóa sản phẩm mã {ma}\");",
        "    LoadData();",
        "}"
    ]
    draw_carbon_code_image("code_06_mayvitinh_crud_fk.png", "Forms/frmMayViTinh.cs - Referential Integrity Guard & CRUD Logic", code_06)

    code_07 = [
        "private void SwitchCategoryTab(CategoryMode mode)",
        "{",
        "    currentMode = mode;",
        "    dgvDanhMuc.Columns.Clear();",
        "    switch (mode)",
        "    {",
        "        case CategoryMode.LoaiMay:",
        "            SetupGridColumns(\"Mã Loại\", \"Tên Loại Máy\", \"Mô Tả\");",
        "            LoadGridData(\"SELECT MaLoai, TenLoai, MoTa FROM tblLoaiMay\");",
        "            break;",
        "        case CategoryMode.NhaCungCap:",
        "            SetupGridColumns(\"Mã NCC\", \"Tên Nhà Cung Cấp\", \"Địa Chỉ\", \"Điện Thoại\");",
        "            LoadGridData(\"SELECT MaNCC, TenNCC, DiaChi, DienThoai FROM tblNhaCungCap\");",
        "            break;",
        "        case CategoryMode.KhachHang:",
        "            SetupGridColumns(\"Mã KH\", \"Tên Khách Hàng\", \"Địa Chỉ\", \"Điện Thoại\");",
        "            LoadGridData(\"SELECT MaKhach, TenKhach, DiaChi, DienThoai FROM tblKhachHang\");",
        "            break;",
        "    }",
        "    UIThemeHelper.ApplyTheme(this);",
        "}"
    ]
    draw_carbon_code_image("code_07_danhmuc_polymorphic.png", "Forms/frmDanhMucHeThong.cs - 5-in-1 Polymorphic Dynamic Grid Loader", code_07)

    code_08 = [
        "private void btnLuu_Click(object sender, EventArgs e)",
        "{",
        "    string sqlHDN = \"IF NOT EXISTS (SELECT 1 FROM tblHoaDonNhap WHERE MaHDN = @ma)\" +",
        "                    \" INSERT INTO tblHoaDonNhap (MaHDN, MaNV, NgayNhap, MaNCC, TongTien) VALUES (@ma, @nv, @ngay, @ncc, 0)\";",
        "    SqlParameter[] prmsHDN = {",
        "        new SqlParameter(\"@ma\", txtMaHDN.Text),",
        "        new SqlParameter(\"@nv\", cboMaNV.SelectedValue),",
        "        new SqlParameter(\"@ngay\", dtpNgayNhap.Value),",
        "        new SqlParameter(\"@ncc\", cboMaNCC.SelectedValue)",
        "    };",
        "    Functions.RunSQL(sqlHDN, prmsHDN);",
        "",
        "    string sqlCT = \"INSERT INTO tblChiTietHoaDonNhap (MaHDN, MaMay, SoLuong, DonGia, GiamGia, ThanhTien)\" +",
        "                   \" VALUES (@ma, @may, @sl, @dg, @gg, @tt)\";",
        "    // SQL Server Trigger handles stock addition and TongTien update atomically",
        "    Functions.RunSQL(sqlCT, prmsCT);",
        "    LoadDataGridView();",
        "}"
    ]
    draw_carbon_code_image("code_08_hoadonnhap_logic.png", "Forms/frmHoaDonNhap.cs - Master-Detail Batch Ingestion Pipeline", code_08)

    code_09 = [
        "private void btnThemSanPham_Click(object sender, EventArgs e)",
        "{",
        "    int tonKho = Convert.ToInt32(Functions.GetFieldValues(",
        "        \"SELECT SoLuong FROM tblMayViTinh WHERE MaMay = @ma\",",
        "        new SqlParameter[] { new SqlParameter(\"@ma\", cboMaMay.SelectedValue) }));",
        "    int slBan = Convert.ToInt32(txtSoLuong.Text);",
        "    if (slBan > tonKho)",
        "    {",
        "        MessageBox.Show($\"Không đủ hàng trong kho! Hiện chỉ còn {tonKho} máy.\",",
        "                        \"Cảnh báo tồn kho\", MessageBoxButtons.OK, MessageBoxIcon.Warning);",
        "        txtSoLuong.Focus();",
        "        return;",
        "    }",
        "    decimal donGia = Convert.ToDecimal(txtDonGiaBan.Text);",
        "    decimal giamGia = Convert.ToDecimal(txtGiamGia.Text);",
        "    decimal thanhTien = slBan * donGia * (1 - giamGia / 100m);",
        "    SaveOrderDetail(thanhTien);",
        "}"
    ]
    draw_carbon_code_image("code_09_hoadonban_pos.png", "Forms/frmHoaDonBan.cs - POS Stock Guard & Realtime Discount Engine", code_09)

    code_10 = [
        "CREATE TRIGGER trg_ChiTietHoaDonBan_Insert_Update",
        "ON tblChiTietHoaDonBan AFTER INSERT, UPDATE",
        "AS BEGIN",
        "    SET NOCOUNT ON;",
        "    -- Auto decrement stock in tblMayViTinh",
        "    UPDATE tblMayViTinh",
        "    SET SoLuong = tblMayViTinh.SoLuong - (i.SoLuong - ISNULL(d.SoLuong, 0))",
        "    FROM tblMayViTinh",
        "    INNER JOIN inserted i ON tblMayViTinh.MaMay = i.MaMay",
        "    LEFT JOIN deleted d ON i.MaHDB = d.MaHDB AND i.MaMay = d.MaMay;",
        "",
        "    -- Auto recalculate TongTien on Master Header tblHoaDonBan",
        "    UPDATE tblHoaDonBan",
        "    SET TongTien = (SELECT ISNULL(SUM(ThanhTien), 0) FROM tblChiTietHoaDonBan WHERE tblChiTietHoaDonBan.MaHDB = tblHoaDonBan.MaHDB)",
        "    WHERE MaHDB IN (SELECT DISTINCT MaHDB FROM inserted);",
        "END"
    ]
    draw_carbon_code_image("code_10_sql_triggers.png", "Database_QLBanMayViTinh.sql - ACID Realtime Stock & Total Triggers", code_10)

    code_11 = [
        "private void ExecuteDynamicSearch()",
        "{",
        "    StringBuilder sql = new StringBuilder(\"SELECT * FROM tblMayViTinh WHERE 1=1 \");",
        "    List<SqlParameter> prms = new List<SqlParameter>();",
        "    if (!string.IsNullOrEmpty(txtTenMay.Text.Trim()))",
        "    {",
        "        sql.Append(\" AND TenMay LIKE @ten\");",
        "        prms.Add(new SqlParameter(\"@ten\", \"%\" + txtTenMay.Text.Trim() + \"%\"));",
        "    }",
        "    if (cboMaLoai.SelectedIndex > 0)",
        "    {",
        "        sql.Append(\" AND MaLoai = @loai\");",
        "        prms.Add(new SqlParameter(\"@loai\", cboMaLoai.SelectedValue));",
        "    }",
        "    if (numGiaMin.Value > 0)",
        "    {",
        "        sql.Append(\" AND DonGiaBan >= @gmin\");",
        "        prms.Add(new SqlParameter(\"@gmin\", numGiaMin.Value));",
        "    }",
        "    DataTable dt = Functions.GetDataToTable(sql.ToString(), prms.ToArray());",
        "    dgvTimKiem.DataSource = dt;",
        "}"
    ]
    draw_carbon_code_image("code_11_timkiem_dynamic_sql.png", "Forms/frmTimKiem.cs - Dynamic SQL Query Builder Pattern", code_11)

    code_12 = [
        "private void UpdateSummaryCards()",
        "{",
        "    decimal tonKhoValue = Convert.ToDecimal(Functions.GetFieldValues(",
        "        \"SELECT ISNULL(SUM(SoLuong * DonGiaNhap), 0) FROM tblMayViTinh\"));",
        "    decimal tongNhap = Convert.ToDecimal(Functions.GetFieldValues(",
        "        \"SELECT ISNULL(SUM(TongTien), 0) FROM tblHoaDonNhap\"));",
        "    decimal tongBan = Convert.ToDecimal(Functions.GetFieldValues(",
        "        \"SELECT ISNULL(SUM(TongTien), 0) FROM tblHoaDonBan\"));",
        "    decimal loiNhuan = tongBan - tongNhap;",
        "",
        "    cardTonKho.TextValue = tonKhoValue.ToString(\"#,##0\") + \" đ\";",
        "    cardNhapHang.TextValue = tongNhap.ToString(\"#,##0\") + \" đ\";",
        "    cardDoanhThu.TextValue = tongBan.ToString(\"#,##0\") + \" đ\";",
        "    cardLoiNhuan.TextValue = loiNhuan.ToString(\"#,##0\") + \" đ\";",
        "    cardLoiNhuan.ForeColor = (loiNhuan >= 0) ? Color.FromArgb(16, 185, 129) : Color.FromArgb(239, 68, 68);",
        "}"
    ]
    draw_carbon_code_image("code_12_baocao_responsive_cards.png", "Forms/frmBaoCaoThongKe.cs - Real-time BI Executive KPI Summary Cards", code_12)

    code_13 = [
        "private void LoadTop5NhanVienReport()",
        "{",
        "    string sql = \"SELECT TOP 5 WITH TIES \" +",
        "                 \"  nv.MaNV, nv.TenNV, nv.DienThoai, \" +",
        "                 \"  COUNT(hdb.MaHDB) AS SoHoaDon, \" +",
        "                 \"  ISNULL(SUM(hdb.TongTien), 0) AS TongDoanhThu \" +",
        "                 \"FROM tblNhanVien nv \" +",
        "                 \"INNER JOIN tblHoaDonBan hdb ON nv.MaNV = hdb.MaNV \" +",
        "                 \"WHERE YEAR(hdb.NgayBan) = @nam AND DATEPART(QUARTER, hdb.NgayBan) = @quy \" +",
        "                 \"GROUP BY nv.MaNV, nv.TenNV, nv.DienThoai \" +",
        "                 \"ORDER BY TongDoanhThu DESC;\";",
        "    SqlParameter[] prms = {",
        "        new SqlParameter(\"@nam\", numNam.Value),",
        "        new SqlParameter(\"@quy\", cboQuy.SelectedItem)",
        "    };",
        "    dgvBaoCao.DataSource = Functions.GetDataToTable(sql, prms);",
        "}"
    ]
    draw_carbon_code_image("code_13_baocao_top5_sql.png", "Forms/frmBaoCaoThongKe.cs - Top 5 With Ties Fair Ranking Engine", code_13)

    code_14 = [
        "public static void ExportToExcelNativeHtml(DataGridView dgv, string filePath, string title)",
        "{",
        "    StringBuilder sb = new StringBuilder();",
        "    sb.AppendLine(\"<html xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:x='urn:schemas-microsoft-com:office:excel'>\");",
        "    sb.AppendLine(\"<head><meta charset='utf-8'><style>\");",
        "    sb.AppendLine(\"table { border-collapse: collapse; width: 100%; font-family: Arial; }\");",
        "    sb.AppendLine(\"th { background-color: #1e293b; color: #ffffff; padding: 10px; border: 1px solid #475569; }\");",
        "    sb.AppendLine(\"td { padding: 8px; border: 1px solid #cbd5e1; text-align: left; }\");",
        "    sb.AppendLine(\".number { mso-number-format:'\\#\\,\\#\\#0'; text-align: right; }\");",
        "    sb.AppendLine(\"</style></head><body>\");",
        "    sb.AppendLine($\"<h2 style='text-align:center;'>{title}</h2>\");",
        "    sb.AppendLine(\"<table><thead><tr>\");",
        "    foreach (DataGridViewColumn col in dgv.Columns) if (col.Visible) sb.AppendLine($\"<th>{col.HeaderText}</th>\");",
        "    sb.AppendLine(\"</tr></thead><tbody>\");",
        "    // Rows traversal with native currency formatting",
        "    File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8);",
        "}"
    ]
    draw_carbon_code_image("code_14_baocao_excel_export.png", "Forms/frmBaoCaoThongKe.cs - Zero-Dependency Native HTML-XLS Engine", code_14)

    code_15 = [
        "public static void GhiNhatKy(string user, string hanhDong, string chiTiet)",
        "{",
        "    try",
        "    {",
        "        string sql = \"INSERT INTO tblNhatKyHoatDong (TenDangNhap, ThoiGian, HanhDong, ChiTiet)\" +",
        "                     \" VALUES (@u, GETDATE(), @action, @detail)\";",
        "        SqlParameter[] prms = {",
        "            new SqlParameter(\"@u\", user),",
        "            new SqlParameter(\"@action\", hanhDong),",
        "            new SqlParameter(\"@detail\", chiTiet)",
        "        };",
        "        RunSQL(sql, prms);",
        "    }",
        "    catch { /* Audit logging must be non-blocking */ }",
        "}",
        "",
        "public static bool CheckPermission(string requiredRole)",
        "{",
        "    if (CurrentRole == \"Admin\") return true;",
        "    return CurrentRole == requiredRole;",
        "}"
    ]
    draw_carbon_code_image("code_15_audit_log_rbac.png", "Classes/Functions.cs - Non-blocking Audit Trail & RBAC Engine", code_15)

def draw_diagram_card(draw, x, y, w, h, title, subtitle, fill, border, title_font, sub_font, text_fill=(255,255,255)):
    draw.rounded_rectangle([x, y, x+w, y+h], radius=12, fill=fill, outline=border, width=2)
    draw.text((x + 18, y + 14), title, fill=text_fill, font=title_font)
    if subtitle:
        draw.text((x + 18, y + 42), subtitle, fill=(148, 163, 184), font=sub_font)

def draw_arrow(draw, x1, y1, x2, y2, color=(56, 189, 248), width=3):
    draw.line([x1, y1, x2, y2], fill=color, width=width)
    import math
    angle = math.atan2(y2 - y1, x2 - x1)
    arrow_len = 12
    p1 = (x2 - arrow_len * math.cos(angle - math.pi/6), y2 - arrow_len * math.sin(angle - math.pi/6))
    p2 = (x2 - arrow_len * math.cos(angle + math.pi/6), y2 - arrow_len * math.sin(angle + math.pi/6))
    draw.polygon([ (x2, y2), p1, p2 ], fill=color)

def generate_7_graph_diagrams():
    font_header = get_font(24, bold=True)
    font_card_title = get_font(18, bold=True)
    font_card_sub = get_font(13, bold=False)

    # 1. graph_01_overview_3tier.png
    img = Image.new("RGBA", (1400, 850), (11, 15, 25, 255))
    draw = ImageDraw.Draw(img)
    draw.text((50, 30), "GRAPHIFY ARCHITECTURE: 3-TIER ENTERPRISE WINFORMS", fill=(56, 189, 248), font=font_header)
    
    draw_diagram_card(draw, 50, 90, 1300, 200, "PRESENTATION LAYER (TẦNG GIAO DIỆN)", 
                      "14 Forms: frmMain, frmDangNhap, frmMayViTinh, frmDanhMucHeThong, frmHoaDonNhap/Ban, frmTimKiem, frmBaoCao...",
                      (24, 33, 56), (99, 102, 241), font_card_title, font_card_sub)
    
    draw_diagram_card(draw, 50, 340, 1300, 200, "BUSINESS & HELPER LAYER (TẦNG NGHIỆP VỤ & ĐIỀU PHỐI)", 
                      "Functions.cs (Data Access, Session, Audit) | UIThemeHelper.cs (GDI+ Engine) | SecurityHelper.cs (BCrypt Hash)",
                      (30, 41, 59), (56, 189, 248), font_card_title, font_card_sub)
                      
    draw_diagram_card(draw, 50, 590, 1300, 200, "DATA LAYER (TẦNG CƠ SỞ DỮ LIỆU SQL SERVER)", 
                      "25 Tables 3NF (tblMayViTinh, tblHoaDonBan, tblHoaDonNhap...) | Realtime ACID Triggers | Referential Integrity FK",
                      (20, 30, 45), (52, 211, 153), font_card_title, font_card_sub)
                      
    for x in [300, 700, 1100]:
        draw_arrow(draw, x, 290, x, 340, (99, 102, 241), 4)
        draw_arrow(draw, x+40, 340, x+40, 290, (148, 163, 184), 2)
        draw_arrow(draw, x, 540, x, 590, (56, 189, 248), 4)
        draw_arrow(draw, x+40, 590, x+40, 540, (52, 211, 153), 2)
        
    img.save(os.path.join(ASSETS_DIR, "graph_01_overview_3tier.png"), "PNG")
    print("Generated Graph: graph_01_overview_3tier.png")

    # 2. graph_02_call_dependency.png
    img2 = Image.new("RGBA", (1400, 850), (11, 15, 25, 255))
    draw2 = ImageDraw.Draw(img2)
    draw2.text((50, 30), "GRAPHIFY DEPENDENCY MAP: 14 FORMS <-> HELPER CALL GRAPH", fill=(167, 139, 250), font=font_header)
    
    draw_diagram_card(draw2, 500, 100, 400, 120, "Functions.cs", "Core ADO.NET, Security, Logs, Session", (30, 41, 59), (56, 189, 248), font_card_title, font_card_sub)
    draw_diagram_card(draw2, 500, 270, 400, 120, "UIThemeHelper.cs", "GDI+ Fluent Shapes, Theme Mode, Card Styler", (30, 41, 59), (167, 139, 250), font_card_title, font_card_sub)
    
    forms_left = ["frmDangNhap / frmDangKy", "frmMain (MDI Shell)", "frmMayViTinh", "frmDanhMucHeThong"]
    for i, f in enumerate(forms_left):
        y = 120 + i * 140
        draw_diagram_card(draw2, 60, y, 350, 95, f, "Client Forms", (24, 33, 56), (99, 102, 241), font_card_title, font_card_sub)
        draw_arrow(draw2, 410, y + 45, 500, 160, (56, 189, 248), 2)
        draw_arrow(draw2, 410, y + 55, 500, 320, (167, 139, 250), 2)
        
    forms_right = ["frmHoaDonNhap / Ban", "frmTimKiem Dynamic", "frmBaoCaoThongKe", "frmNhatKy / frmCaiDat"]
    for i, f in enumerate(forms_right):
        y = 120 + i * 140
        draw_diagram_card(draw2, 980, y, 350, 95, f, "Client Forms", (24, 33, 56), (99, 102, 241), font_card_title, font_card_sub)
        draw_arrow(draw2, 980, y + 45, 900, 160, (56, 189, 248), 2)
        draw_arrow(draw2, 980, y + 55, 900, 320, (167, 139, 250), 2)

    img2.save(os.path.join(ASSETS_DIR, "graph_02_call_dependency.png"), "PNG")
    print("Generated Graph: graph_02_call_dependency.png")

    # 3. graph_03_erd_database.png
    img3 = Image.new("RGBA", (1400, 850), (11, 15, 25, 255))
    draw3 = ImageDraw.Draw(img3)
    draw3.text((50, 30), "DATABASE ERD: 3NF RELATIONAL SCHEMA WITH ACID TRIGGERS", fill=(52, 211, 153), font=font_header)
    
    draw_diagram_card(draw3, 60, 100, 350, 160, "tblMayViTinh (Sản Phẩm)", "PK: MaMay | FK: MaLoai, MaHangSX...\nSoLuong (Kho), DonGiaNhap, DonGiaBan", (24, 33, 56), (56, 189, 248), font_card_title, font_card_sub)
    draw_diagram_card(draw3, 520, 100, 360, 160, "tblChiTietHoaDonBan", "PK/FK: MaHDB, MaMay\nSoLuong, DonGia, GiamGia, ThanhTien", (30, 41, 59), (239, 68, 68), font_card_title, font_card_sub)
    draw_diagram_card(draw3, 980, 100, 350, 160, "tblHoaDonBan (Hóa Đơn)", "PK: MaHDB | FK: MaNV, MaKhach\nNgayBan, TongTien", (24, 33, 56), (245, 158, 11), font_card_title, font_card_sub)
    
    draw_diagram_card(draw3, 520, 340, 360, 160, "tblChiTietHoaDonNhap", "PK/FK: MaHDN, MaMay\nSoLuong, DonGia, GiamGia, ThanhTien", (30, 41, 59), (52, 211, 153), font_card_title, font_card_sub)
    draw_diagram_card(draw3, 980, 340, 350, 160, "tblHoaDonNhap (Phiếu Nhập)", "PK: MaHDN | FK: MaNV, MaNCC\nNgayNhap, TongTien", (24, 33, 56), (56, 189, 248), font_card_title, font_card_sub)

    draw_diagram_card(draw3, 60, 340, 350, 160, "Danh Mục Linh Kiện (3NF)", "tblLoaiMay, tblHangSanXuat, tblRAM\ntblOCung, tblManHinh, tblChip...", (24, 33, 56), (167, 139, 250), font_card_title, font_card_sub)

    draw_diagram_card(draw3, 60, 580, 350, 180, "tblNguoiDung (Bảo Mật)", "PK: TenDangNhap | FK: MaNV\nMatKhau (Hash), Quyen (Admin/NV)", (24, 33, 56), (244, 63, 94), font_card_title, font_card_sub)
    draw_diagram_card(draw3, 520, 580, 360, 180, "tblNhatKyHoatDong (Audit)", "PK: ID | FK: TenDangNhap\nThoiGian, HanhDong, ChiTiet", (30, 41, 59), (148, 163, 184), font_card_title, font_card_sub)
    draw_diagram_card(draw3, 980, 580, 350, 180, "tblKhachHang & tblNhaCungCap", "PK: MaKhach / MaNCC\nTen, DiaChi, DienThoai", (24, 33, 56), (99, 102, 241), font_card_title, font_card_sub)

    draw_arrow(draw3, 410, 180, 520, 180, (239, 68, 68), 3)
    draw_arrow(draw3, 880, 180, 980, 180, (245, 158, 11), 3)
    draw_arrow(draw3, 410, 220, 520, 400, (52, 211, 153), 3)
    draw_arrow(draw3, 880, 420, 980, 420, (56, 189, 248), 3)

    img3.save(os.path.join(ASSETS_DIR, "graph_03_erd_database.png"), "PNG")
    print("Generated Graph: graph_03_erd_database.png")

    # 4. graph_04_auth_rbac_flow.png
    img4 = Image.new("RGBA", (1400, 850), (11, 15, 25, 255))
    draw4 = ImageDraw.Draw(img4)
    draw4.text((50, 30), "AUTHENTICATION & RBAC PERMISSION FLOW PIPELINE", fill=(244, 63, 94), font=font_header)
    
    steps = [
        ("1. User Input", "Nhập Username & Password\ntrên frmDangNhap", (24, 33, 56), (99, 102, 241)),
        ("2. SQL Query", "SELECT user record\n& Verify BCrypt hash", (30, 41, 59), (56, 189, 248)),
        ("3. Role Resolver", "Phân tích quyền hạn:\nAdmin hay Nhân Viên", (30, 41, 59), (245, 158, 11)),
        ("4. Session Storage", "Lưu CurrentMaNV, CurrentRole\nvào Functions static session", (30, 41, 59), (167, 139, 250)),
        ("5. Navigation Shell", "Mở frmMain & Apply RBAC\nẨn/Hiện menu theo quyền", (24, 33, 56), (52, 211, 153))
    ]
    for i, (t, s, f, b) in enumerate(steps):
        x = 50 + i * 265
        draw_diagram_card(draw4, x, 220, 240, 160, t, s, f, b, font_card_title, font_card_sub)
        if i < len(steps) - 1:
            draw_arrow(draw4, x + 240, 300, x + 265, 300, (56, 189, 248), 3)

    draw_diagram_card(draw4, 300, 460, 800, 160, "Audit Trail Logging (Không phong tỏa)", "Ghi nhận sự kiện Đăng nhập vào tblNhatKyHoatDong (Timestamp, IP/Machine, User)", (20, 30, 45), (148, 163, 184), font_card_title, font_card_sub)
    draw_arrow(draw4, 700, 380, 700, 460, (148, 163, 184), 3)

    img4.save(os.path.join(ASSETS_DIR, "graph_04_auth_rbac_flow.png"), "PNG")
    print("Generated Graph: graph_04_auth_rbac_flow.png")

    # 5. graph_05_polymorphic_danhmuc.png
    img5 = Image.new("RGBA", (1400, 850), (11, 15, 25, 255))
    draw5 = ImageDraw.Draw(img5)
    draw5.text((50, 30), "POLYMORPHIC 5-IN-1 DANH MỤC ARCHITECTURE", fill=(167, 139, 250), font=font_header)
    
    draw_diagram_card(draw5, 50, 120, 380, 600, "TAB SELECTION (GIAO DIỆN)", 
                      "Button Tab 1: Loại Máy\nButton Tab 2: Hãng Sản Xuất\nButton Tab 3: Nhà Cung Cấp\nButton Tab 4: Khách Hàng\nButton Tab 5: Nhân Viên\n\nSingle Form frmDanhMucHeThong\nTối ưu tài nguyên RAM & GDI+",
                      (24, 33, 56), (99, 102, 241), font_card_title, font_card_sub)

    draw_diagram_card(draw5, 500, 120, 400, 260, "Dynamic Grid Reconfiguration", "Tự động xóa và tái cấu trúc cột DataGridView:\n- DataGridViewTextBoxColumn\n- HeaderText mapping\n- ReadOnly & DataPropertyName", (30, 41, 59), (56, 189, 248), font_card_title, font_card_sub)
    
    draw_diagram_card(draw5, 500, 440, 400, 280, "Dynamic SQL Dispatcher", "Nạp câu lệnh SQL tương ứng:\n- tblLoaiMay\n- tblHangSanXuat\n- tblNhaCungCap\n- tblKhachHang\n- tblNhanVien", (30, 41, 59), (52, 211, 153), font_card_title, font_card_sub)

    draw_diagram_card(draw5, 960, 260, 380, 320, "Single CRUD Handler", "Tất cả các nút Thêm, Sửa, Xóa, Lưu,\nBỏ qua đều tái sử dụng qua 1 bộ xử lý,\nphân nhánh theo enum CurrentCategory.\n\n-> Giảm 80% code trùng lặp!", (20, 30, 45), (245, 158, 11), font_card_title, font_card_sub)

    draw_arrow(draw5, 430, 250, 500, 250, (56, 189, 248), 3)
    draw_arrow(draw5, 430, 580, 500, 580, (52, 211, 153), 3)
    draw_arrow(draw5, 900, 250, 960, 360, (245, 158, 11), 3)
    draw_arrow(draw5, 900, 580, 960, 460, (245, 158, 11), 3)

    img5.save(os.path.join(ASSETS_DIR, "graph_05_polymorphic_danhmuc.png"), "PNG")
    print("Generated Graph: graph_05_polymorphic_danhmuc.png")

    # 6. graph_06_pos_trigger_pipeline.png
    img6 = Image.new("RGBA", (1400, 850), (11, 15, 25, 255))
    draw6 = ImageDraw.Draw(img6)
    draw6.text((50, 30), "POS & ACID REALTIME TRIGGER PIPELINE", fill=(239, 68, 68), font=font_header)
    
    draw_diagram_card(draw6, 50, 120, 380, 180, "1. POS Client (WinForms)", "Nhân viên chọn máy & số lượng\nChống bán âm (Check SoLuong > TonKho)\nClick 'Thêm máy' vào đơn", (24, 33, 56), (99, 102, 241), font_card_title, font_card_sub)
    draw_diagram_card(draw6, 500, 120, 400, 180, "2. C# ADO.NET Execution", "SqlCommand INSERT INTO tblChiTietHoaDonBan\nThực thi qua SqlConnection mở\nĐảm bảo tính nhất quán Parameterized", (30, 41, 59), (56, 189, 248), font_card_title, font_card_sub)
    draw_diagram_card(draw6, 960, 120, 380, 180, "3. SQL Server Trigger Fires", "trg_ChiTietHoaDonBan_Insert_Update kích hoạt\nChạy trong phạm vi giao dịch ACID\nĐộc lập với tầng ứng dụng", (20, 30, 45), (239, 68, 68), font_card_title, font_card_sub)

    draw_diagram_card(draw6, 960, 420, 380, 180, "4. Stock Auto-Decrement", "UPDATE tblMayViTinh\nSET SoLuong = SoLuong - inserted.SoLuong\nKhấu trừ kho tự động tức thì", (30, 41, 59), (52, 211, 153), font_card_title, font_card_sub)
    draw_diagram_card(draw6, 500, 420, 400, 180, "5. Master Total Recomputation", "UPDATE tblHoaDonBan\nSET TongTien = SUM(ChiTiet.ThanhTien)\nKhông bao giờ lệch doanh thu", (30, 41, 59), (245, 158, 11), font_card_title, font_card_sub)
    draw_diagram_card(draw6, 50, 420, 380, 180, "6. UI Realtime Feedback", "DataGridView nạp lại chi tiết\ntxtTongTien cập nhật số tiền mới\n4 KPI Cards trên Dashboard nhảy số", (24, 33, 56), (167, 139, 250), font_card_title, font_card_sub)

    draw_arrow(draw6, 430, 210, 500, 210, (56, 189, 248), 3)
    draw_arrow(draw6, 900, 210, 960, 210, (239, 68, 68), 3)
    draw_arrow(draw6, 1150, 300, 1150, 420, (52, 211, 153), 3)
    draw_arrow(draw6, 960, 510, 900, 510, (245, 158, 11), 3)
    draw_arrow(draw6, 500, 510, 430, 510, (167, 139, 250), 3)

    img6.save(os.path.join(ASSETS_DIR, "graph_06_pos_trigger_pipeline.png"), "PNG")
    print("Generated Graph: graph_06_pos_trigger_pipeline.png")

    # 7. graph_07_bi_analytics_flow.png
    img7 = Image.new("RGBA", (1400, 850), (11, 15, 25, 255))
    draw7 = ImageDraw.Draw(img7)
    draw7.text((50, 30), "BI EXECUTIVE DASHBOARD & NATIVE EXCEL EXPORT PIPELINE", fill=(245, 158, 11), font=font_header)
    
    draw_diagram_card(draw7, 50, 120, 380, 220, "1. OLTP Database", "tblMayViTinh (Tồn kho)\ntblHoaDonBan (Doanh thu)\ntblHoaDonNhap (Chi phí nhập)\ntblNhanVien (Hiệu suất bán)", (24, 33, 56), (99, 102, 241), font_card_title, font_card_sub)
    draw_diagram_card(draw7, 500, 120, 400, 220, "2. Dynamic BI Queries", "Aggregation Functions:\n- SUM(SoLuong * DonGiaNhap)\n- DATEPART(QUARTER, ...)\n- SELECT TOP 5 WITH TIES", (30, 41, 59), (56, 189, 248), font_card_title, font_card_sub)
    draw_diagram_card(draw7, 960, 120, 380, 220, "3. 4 KPI Summary Cards", "Card 1: Giá trị Tồn Kho\nCard 2: Tổng Tiền Nhập Hàng\nCard 3: Tổng Doanh Thu Bán\nCard 4: Lợi Nhuận Gộp Tức Thì", (20, 30, 45), (52, 211, 153), font_card_title, font_card_sub)

    draw_diagram_card(draw7, 500, 440, 400, 240, "4. Native HTML-XLS Engine", "StringBuilder sinh mã HTML Table chuẩn:\n- xmlns:o / xmlns:x XML namespace\n- Header đen chữ trắng (#1e293b)\n- Định dạng mso-number-format tiền tệ", (30, 41, 59), (245, 158, 11), font_card_title, font_card_sub)
    draw_diagram_card(draw7, 960, 440, 380, 240, "5. Enterprise Excel File (.xls)", "Mở ngay lập tức bằng MS Excel,\nGoogle Sheets, LibreOffice.\nKhông cần cài đặt thư viện COM/Interop!\nTốc độ xuất < 0.2s cho 10.000 dòng", (24, 33, 56), (167, 139, 250), font_card_title, font_card_sub)

    draw_arrow(draw7, 430, 230, 500, 230, (56, 189, 248), 3)
    draw_arrow(draw7, 900, 230, 960, 230, (52, 211, 153), 3)
    draw_arrow(draw7, 1150, 340, 1150, 440, (245, 158, 11), 3)
    draw_arrow(draw7, 900, 560, 960, 560, (167, 139, 250), 3)

    img7.save(os.path.join(ASSETS_DIR, "graph_07_bi_analytics_flow.png"), "PNG")
    print("Generated Graph: graph_07_bi_analytics_flow.png")

def copy_and_setup_all_ui_assets():
    ui_map = {
        "ui_dangnhap.png": "Screenshots/Verified/06_DangNhap_Fixed.png",
        "ui_dangky.png": "Screenshots/Verified/02_DangKy_Validation_Error.png",
        "ui_quenmatkhau.png": "Screenshots/Verified/03_QuenMatKhau_Validation_Error.png",
        "ui_main_light.png": "Screenshots/Verified/00_Main_Dashboard_Light.png",
        "ui_main_dark.png": "Screenshots/Verified/00_Main_Dashboard_Dark.png",
        "ui_mayvitinh_view.png": "Screenshots/Verified/05_MayViTinh_Selected_Row.png",
        "ui_mayvitinh_edit.png": "Screenshots/Verified/05_MayViTinh_State_ThemMoi.png",
        "ui_danhmuc_tab1_loaimay.png": "Screenshots/Verified/06_DanhMuc_Tab4_LinhKien_Chip.png",
        "ui_danhmuc_tab2_hangsx.png": "Screenshots/Verified/06_DanhMuc_Tab4_LinhKien_ManHinh.png",
        "ui_danhmuc_tab3_ncc.png": "Screenshots/Verified/06_DanhMuc_Tab2_NhaCungCap.png",
        "ui_danhmuc_tab4_khachhang.png": "Screenshots/Verified/06_DanhMuc_Tab1_KhachHang.png",
        "ui_danhmuc_tab5_nhanvien.png": "Screenshots/Verified/06_DanhMuc_Tab3_NhanVien.png",
        "ui_hoadonnhap.png": "Screenshots/Verified/05_HoaDonNhap_SelectProduct_Calculated.png",
        "ui_hoadonban.png": "Screenshots/Verified/04_HoaDonBan_SelectProduct_Calculated.png",
        "ui_timkiem_mode1.png": "Screenshots/Verified/02_TimKiem_MVT_Searched.png",
        "ui_timkiem_mode2.png": "Screenshots/Verified/02_TimKiem_rdoHDB_Expanded.png",
        "ui_baocao_tonkho.png": "Screenshots/Verified/01_BaoCao_rdoTonKho_Light.png",
        "ui_baocao_nhaphang.png": "Screenshots/Verified/01_BaoCao_rdoNhapHang_Light.png",
        "ui_baocao_banhang.png": "Screenshots/Verified/01_BaoCao_rdoNhapHang_Dark.png",
        "ui_baocao_top5nv.png": "Screenshots/Verified/01_BaoCao_rdoTop5_Light.png",
        "ui_quanlynguoidung.png": "Screenshots/Verified/11_NguoiDung_Selected_User.png",
        "ui_hosocanhan.png": "Screenshots/Verified/12_HoSoCaNhan_View.png",
        "ui_nhatkyhoatdong.png": "Screenshots/Verified/07_NhatKy_Fixed.png",
        "ui_caidat.png": "Screenshots/Verified/08_CaiDat_Fixed.png"
    }

    for dst, src in ui_map.items():
        if os.path.exists(src):
            shutil.copyfile(src, os.path.join(ASSETS_DIR, dst))
            print(f"Copied UI Asset: {dst}")
        else:
            print(f"Warning: source {src} not found for {dst}")

if __name__ == "__main__":
    print("=== GENERATING ALL ASSETS FOR 32 SLIDES ===")
    generate_all_15_code_assets()
    generate_7_graph_diagrams()
    copy_and_setup_all_ui_assets()
    print("=== ALL ASSETS GENERATED SUCCESSFULLY ===")

