using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using QLBanMayViTinh.Classes;

namespace QLBanMayViTinh.Forms
{
    public partial class frmDanhMucHeThong : Form
    {
        public frmDanhMucHeThong()
        {
            InitializeComponent();
        }

        private void frmDanhMucHeThong_Load(object sender, EventArgs e)
        {
            if (this.DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                return;

            UIThemeHelper.StyleCard(pnlKH);
            UIThemeHelper.StyleCard(pnlNCC);
            UIThemeHelper.StyleCard(pnlNV);
            UIThemeHelper.StyleCard(pnlLK);

            UIThemeHelper.StyleDataGridView(dgvKH);
            UIThemeHelper.StyleDataGridView(dgvNCC);
            UIThemeHelper.StyleDataGridView(dgvNV);
            UIThemeHelper.StyleDataGridView(dgvLK);

            UIThemeHelper.StyleButton(btnThemKH, UIThemeHelper.BtnThemColor, Color.White);
            UIThemeHelper.StyleButton(btnSuaKH, UIThemeHelper.BtnSuaColor, Color.White);
            UIThemeHelper.StyleButton(btnXoaKH, UIThemeHelper.BtnXoaColor, Color.White);
            UIThemeHelper.StyleButton(btnLamMoiKH, Color.FromArgb(100, 116, 139), Color.White);

            UIThemeHelper.StyleButton(btnThemNCC, UIThemeHelper.BtnThemColor, Color.White);
            UIThemeHelper.StyleButton(btnSuaNCC, UIThemeHelper.BtnSuaColor, Color.White);
            UIThemeHelper.StyleButton(btnXoaNCC, UIThemeHelper.BtnXoaColor, Color.White);
            UIThemeHelper.StyleButton(btnLamMoiNCC, Color.FromArgb(100, 116, 139), Color.White);

            UIThemeHelper.StyleButton(btnThemNV, UIThemeHelper.BtnThemColor, Color.White);
            UIThemeHelper.StyleButton(btnSuaNV, UIThemeHelper.BtnSuaColor, Color.White);
            UIThemeHelper.StyleButton(btnXoaNV, UIThemeHelper.BtnXoaColor, Color.White);
            UIThemeHelper.StyleButton(btnLamMoiNV, Color.FromArgb(100, 116, 139), Color.White);

            UIThemeHelper.StyleButton(btnThemLK, UIThemeHelper.BtnThemColor, Color.White);
            UIThemeHelper.StyleButton(btnSuaLK, UIThemeHelper.BtnSuaColor, Color.White);
            UIThemeHelper.StyleButton(btnXoaLK, UIThemeHelper.BtnXoaColor, Color.White);
            UIThemeHelper.StyleButton(btnLamMoiLK, Color.FromArgb(100, 116, 139), Color.White);

            cboGioiTinhNV.SelectedIndex = 0;
            cboLoaiDanhMuc.SelectedIndex = 0;

            LoadDataKH();
            LoadDataNCC();
            LoadDataNV();
            LoadDataLK();
        }

        private void tabMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabMain.SelectedTab == tabKhachHang) LoadDataKH();
            else if (tabMain.SelectedTab == tabNCC) LoadDataNCC();
            else if (tabMain.SelectedTab == tabNhanVien) LoadDataNV();
            else if (tabMain.SelectedTab == tabLinhKien) LoadDataLK();
        }

        private void LoadDataKH()
        {
            DataTable dt = Functions.GetDataToTable("SELECT MaKhach, TenKhach, DiaChi, DienThoai FROM KhachHang");
            foreach (DataRow r in dt.Rows)
            {
                if (r["DiaChi"] != DBNull.Value) r["DiaChi"] = SecurityHelper.DecryptAES(r["DiaChi"].ToString());
                if (r["DienThoai"] != DBNull.Value) r["DienThoai"] = SecurityHelper.DecryptAES(r["DienThoai"].ToString());
            }
            dgvKH.DataSource = dt;
            if (dgvKH.Columns.Contains("MaKhach")) dgvKH.Columns["MaKhach"].HeaderText = "Mã Khách Hàng";
            if (dgvKH.Columns.Contains("TenKhach")) dgvKH.Columns["TenKhach"].HeaderText = "Tên Khách Hàng";
            if (dgvKH.Columns.Contains("DiaChi")) dgvKH.Columns["DiaChi"].HeaderText = "Địa Chỉ";
            if (dgvKH.Columns.Contains("DienThoai")) dgvKH.Columns["DienThoai"].HeaderText = "Điện Thoại";
        }

        private void dgvKH_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvKH.CurrentRow == null || dgvKH.CurrentRow.Index < 0) return;
            txtMaKH.Text = dgvKH.CurrentRow.Cells["MaKhach"].Value.ToString();
            txtTenKH.Text = dgvKH.CurrentRow.Cells["TenKhach"].Value.ToString();
            txtDiaChiKH.Text = dgvKH.CurrentRow.Cells["DiaChi"].Value.ToString();
            txtDienThoaiKH.Text = dgvKH.CurrentRow.Cells["DienThoai"].Value.ToString();
            txtMaKH.Enabled = false;
        }

        private void btnThemKH_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaKH.Text) || string.IsNullOrWhiteSpace(txtTenKH.Text))
            {
                MessageBox.Show("Vui lòng nhập Mã và Tên khách hàng!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (Functions.CheckKey("SELECT MaKhach FROM KhachHang WHERE MaKhach = N'" + txtMaKH.Text.Trim() + "'"))
            {
                MessageBox.Show("Mã khách hàng này đã tồn tại!", "Trùng Khóa", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string encDC = SecurityHelper.EncryptAES(txtDiaChiKH.Text.Trim());
            string encDT = SecurityHelper.EncryptAES(txtDienThoaiKH.Text.Trim());

            string sql = "INSERT INTO KhachHang (MaKhach, TenKhach, DiaChi, DienThoai) VALUES (@m, @t, @dc, @dt)";
            using (SqlCommand cmd = new SqlCommand(sql, Functions.Conn))
            {
                cmd.Parameters.AddWithValue("@m", txtMaKH.Text.Trim());
                cmd.Parameters.AddWithValue("@t", txtTenKH.Text.Trim());
                cmd.Parameters.AddWithValue("@dc", encDC);
                cmd.Parameters.AddWithValue("@dt", encDT);
                cmd.ExecuteNonQuery();
            }

            Functions.LogAction("Thêm khách hàng", "Thêm khách hàng: " + txtTenKH.Text.Trim());
            MessageBox.Show("Thêm mới khách hàng thành công!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnLamMoiKH.PerformClick();
            LoadDataKH();
        }

        private void btnSuaKH_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaKH.Text)) return;
            string encDC = SecurityHelper.EncryptAES(txtDiaChiKH.Text.Trim());
            string encDT = SecurityHelper.EncryptAES(txtDienThoaiKH.Text.Trim());

            string sql = "UPDATE KhachHang SET TenKhach = @t, DiaChi = @dc, DienThoai = @dt WHERE MaKhach = @m";
            using (SqlCommand cmd = new SqlCommand(sql, Functions.Conn))
            {
                cmd.Parameters.AddWithValue("@t", txtTenKH.Text.Trim());
                cmd.Parameters.AddWithValue("@dc", encDC);
                cmd.Parameters.AddWithValue("@dt", encDT);
                cmd.Parameters.AddWithValue("@m", txtMaKH.Text.Trim());
                cmd.ExecuteNonQuery();
            }

            Functions.LogAction("Sửa khách hàng", "Cập nhật khách hàng: " + txtMaKH.Text.Trim());
            MessageBox.Show("Cập nhật khách hàng thành công!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadDataKH();
        }

        private void btnXoaKH_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaKH.Text)) return;
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa khách hàng này?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Functions.RunSqlDel("DELETE FROM KhachHang WHERE MaKhach = N'" + txtMaKH.Text.Trim() + "'");
                btnLamMoiKH.PerformClick();
                LoadDataKH();
            }
        }

        private void btnLamMoiKH_Click(object sender, EventArgs e)
        {
            txtMaKH.Text = Functions.CreateKey("KH");
            txtMaKH.Enabled = true;
            txtTenKH.Text = "";
            txtDiaChiKH.Text = "";
            txtDienThoaiKH.Text = "";
            txtTenKH.Focus();
        }

        private void LoadDataNCC()
        {
            DataTable dt = Functions.GetDataToTable("SELECT MaNCC, TenNCC, DiaChi, DienThoai FROM NhaCungCap");
            dgvNCC.DataSource = dt;
            if (dgvNCC.Columns.Contains("MaNCC")) dgvNCC.Columns["MaNCC"].HeaderText = "Mã Nhà Cung Cấp";
            if (dgvNCC.Columns.Contains("TenNCC")) dgvNCC.Columns["TenNCC"].HeaderText = "Tên Nhà Cung Cấp";
            if (dgvNCC.Columns.Contains("DiaChi")) dgvNCC.Columns["DiaChi"].HeaderText = "Địa Chỉ";
            if (dgvNCC.Columns.Contains("DienThoai")) dgvNCC.Columns["DienThoai"].HeaderText = "Điện Thoại";
        }

        private void dgvNCC_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvNCC.CurrentRow == null || dgvNCC.CurrentRow.Index < 0) return;
            txtMaNCC.Text = dgvNCC.CurrentRow.Cells["MaNCC"].Value.ToString();
            txtTenNCC.Text = dgvNCC.CurrentRow.Cells["TenNCC"].Value.ToString();
            txtDiaChiNCC.Text = dgvNCC.CurrentRow.Cells["DiaChi"].Value.ToString();
            txtDienThoaiNCC.Text = dgvNCC.CurrentRow.Cells["DienThoai"].Value.ToString();
            txtMaNCC.Enabled = false;
        }

        private void btnThemNCC_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaNCC.Text) || string.IsNullOrWhiteSpace(txtTenNCC.Text))
            {
                MessageBox.Show("Vui lòng nhập Mã và Tên nhà cung cấp!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (Functions.CheckKey("SELECT MaNCC FROM NhaCungCap WHERE MaNCC = N'" + txtMaNCC.Text.Trim() + "'"))
            {
                MessageBox.Show("Mã nhà cung cấp này đã tồn tại!", "Trùng Khóa", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string sql = "INSERT INTO NhaCungCap (MaNCC, TenNCC, DiaChi, DienThoai) VALUES (@m, @t, @dc, @dt)";
            using (SqlCommand cmd = new SqlCommand(sql, Functions.Conn))
            {
                cmd.Parameters.AddWithValue("@m", txtMaNCC.Text.Trim());
                cmd.Parameters.AddWithValue("@t", txtTenNCC.Text.Trim());
                cmd.Parameters.AddWithValue("@dc", txtDiaChiNCC.Text.Trim());
                cmd.Parameters.AddWithValue("@dt", txtDienThoaiNCC.Text.Trim());
                cmd.ExecuteNonQuery();
            }

            Functions.LogAction("Thêm nhà cung cấp", "Thêm nhà cung cấp: " + txtTenNCC.Text.Trim());
            MessageBox.Show("Thêm mới nhà cung cấp thành công!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnLamMoiNCC.PerformClick();
            LoadDataNCC();
        }

        private void btnSuaNCC_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaNCC.Text)) return;
            string sql = "UPDATE NhaCungCap SET TenNCC = @t, DiaChi = @dc, DienThoai = @dt WHERE MaNCC = @m";
            using (SqlCommand cmd = new SqlCommand(sql, Functions.Conn))
            {
                cmd.Parameters.AddWithValue("@t", txtTenNCC.Text.Trim());
                cmd.Parameters.AddWithValue("@dc", txtDiaChiNCC.Text.Trim());
                cmd.Parameters.AddWithValue("@dt", txtDienThoaiNCC.Text.Trim());
                cmd.Parameters.AddWithValue("@m", txtMaNCC.Text.Trim());
                cmd.ExecuteNonQuery();
            }

            Functions.LogAction("Sửa nhà cung cấp", "Cập nhật nhà cung cấp: " + txtMaNCC.Text.Trim());
            MessageBox.Show("Cập nhật nhà cung cấp thành công!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadDataNCC();
        }

        private void btnXoaNCC_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaNCC.Text)) return;
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa nhà cung cấp này?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Functions.RunSqlDel("DELETE FROM NhaCungCap WHERE MaNCC = N'" + txtMaNCC.Text.Trim() + "'");
                btnLamMoiNCC.PerformClick();
                LoadDataNCC();
            }
        }

        private void btnLamMoiNCC_Click(object sender, EventArgs e)
        {
            txtMaNCC.Text = Functions.CreateKey("NCC");
            txtMaNCC.Enabled = true;
            txtTenNCC.Text = "";
            txtDiaChiNCC.Text = "";
            txtDienThoaiNCC.Text = "";
            txtTenNCC.Focus();
        }

        private void LoadDataNV()
        {
            DataTable dt = Functions.GetDataToTable("SELECT MaNV, TenNV, NgaySinh, GioiTinh, DiaChi, DienThoai FROM NhanVien");
            foreach (DataRow r in dt.Rows)
            {
                if (r["DiaChi"] != DBNull.Value) r["DiaChi"] = SecurityHelper.DecryptAES(r["DiaChi"].ToString());
                if (r["DienThoai"] != DBNull.Value) r["DienThoai"] = SecurityHelper.DecryptAES(r["DienThoai"].ToString());
            }
            dgvNV.DataSource = dt;
            if (dgvNV.Columns.Contains("MaNV")) dgvNV.Columns["MaNV"].HeaderText = "Mã Nhân Viên";
            if (dgvNV.Columns.Contains("TenNV")) dgvNV.Columns["TenNV"].HeaderText = "Tên Nhân Viên";
            if (dgvNV.Columns.Contains("NgaySinh")) dgvNV.Columns["NgaySinh"].HeaderText = "Ngày Sinh";
            if (dgvNV.Columns.Contains("GioiTinh")) dgvNV.Columns["GioiTinh"].HeaderText = "Giới Tính";
            if (dgvNV.Columns.Contains("DiaChi")) dgvNV.Columns["DiaChi"].HeaderText = "Địa Chỉ";
            if (dgvNV.Columns.Contains("DienThoai")) dgvNV.Columns["DienThoai"].HeaderText = "Điện Thoại";
        }

        private void dgvNV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvNV.CurrentRow == null || dgvNV.CurrentRow.Index < 0) return;
            txtMaNV.Text = dgvNV.CurrentRow.Cells["MaNV"].Value.ToString();
            txtTenNV.Text = dgvNV.CurrentRow.Cells["TenNV"].Value.ToString();
            if (dgvNV.CurrentRow.Cells["NgaySinh"].Value != DBNull.Value)
                dtpNgaySinhNV.Value = Convert.ToDateTime(dgvNV.CurrentRow.Cells["NgaySinh"].Value);
            cboGioiTinhNV.SelectedItem = dgvNV.CurrentRow.Cells["GioiTinh"].Value.ToString();
            txtDiaChiNV.Text = dgvNV.CurrentRow.Cells["DiaChi"].Value.ToString();
            txtDienThoaiNV.Text = dgvNV.CurrentRow.Cells["DienThoai"].Value.ToString();
            txtMaNV.Enabled = false;
        }

        private void btnThemNV_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaNV.Text) || string.IsNullOrWhiteSpace(txtTenNV.Text))
            {
                MessageBox.Show("Vui lòng nhập Mã và Tên nhân viên!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (Functions.CheckKey("SELECT MaNV FROM NhanVien WHERE MaNV = N'" + txtMaNV.Text.Trim() + "'"))
            {
                MessageBox.Show("Mã nhân viên này đã tồn tại!", "Trùng Khóa", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string encDC = SecurityHelper.EncryptAES(txtDiaChiNV.Text.Trim());
            string encDT = SecurityHelper.EncryptAES(txtDienThoaiNV.Text.Trim());

            string sql = "INSERT INTO NhanVien (MaNV, TenNV, NgaySinh, GioiTinh, DiaChi, DienThoai) VALUES (@m, @t, @ns, @gt, @dc, @dt)";
            using (SqlCommand cmd = new SqlCommand(sql, Functions.Conn))
            {
                cmd.Parameters.AddWithValue("@m", txtMaNV.Text.Trim());
                cmd.Parameters.AddWithValue("@t", txtTenNV.Text.Trim());
                cmd.Parameters.AddWithValue("@ns", dtpNgaySinhNV.Value);
                cmd.Parameters.AddWithValue("@gt", cboGioiTinhNV.SelectedItem != null ? cboGioiTinhNV.SelectedItem.ToString() : "Nam");
                cmd.Parameters.AddWithValue("@dc", encDC);
                cmd.Parameters.AddWithValue("@dt", encDT);
                cmd.ExecuteNonQuery();
            }

            Functions.LogAction("Thêm nhân viên", "Thêm nhân viên mới: " + txtTenNV.Text.Trim());
            MessageBox.Show("Thêm mới nhân viên thành công!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnLamMoiNV.PerformClick();
            LoadDataNV();
        }

        private void btnSuaNV_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaNV.Text)) return;
            string encDC = SecurityHelper.EncryptAES(txtDiaChiNV.Text.Trim());
            string encDT = SecurityHelper.EncryptAES(txtDienThoaiNV.Text.Trim());

            string sql = "UPDATE NhanVien SET TenNV = @t, NgaySinh = @ns, GioiTinh = @gt, DiaChi = @dc, DienThoai = @dt WHERE MaNV = @m";
            using (SqlCommand cmd = new SqlCommand(sql, Functions.Conn))
            {
                cmd.Parameters.AddWithValue("@t", txtTenNV.Text.Trim());
                cmd.Parameters.AddWithValue("@ns", dtpNgaySinhNV.Value);
                cmd.Parameters.AddWithValue("@gt", cboGioiTinhNV.SelectedItem != null ? cboGioiTinhNV.SelectedItem.ToString() : "Nam");
                cmd.Parameters.AddWithValue("@dc", encDC);
                cmd.Parameters.AddWithValue("@dt", encDT);
                cmd.Parameters.AddWithValue("@m", txtMaNV.Text.Trim());
                cmd.ExecuteNonQuery();
            }

            Functions.LogAction("Sửa nhân viên", "Cập nhật nhân viên: " + txtMaNV.Text.Trim());
            MessageBox.Show("Cập nhật thông tin nhân viên thành công!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadDataNV();
        }

        private void btnXoaNV_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaNV.Text)) return;
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa nhân viên này?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Functions.RunSqlDel("DELETE FROM NhanVien WHERE MaNV = N'" + txtMaNV.Text.Trim() + "'");
                btnLamMoiNV.PerformClick();
                LoadDataNV();
            }
        }

        private void btnLamMoiNV_Click(object sender, EventArgs e)
        {
            txtMaNV.Text = Functions.CreateKey("NV");
            txtMaNV.Enabled = true;
            txtTenNV.Text = "";
            txtDiaChiNV.Text = "";
            txtDienThoaiNV.Text = "";
            dtpNgaySinhNV.Value = new DateTime(1995, 1, 1);
            cboGioiTinhNV.SelectedIndex = 0;
            txtTenNV.Focus();
        }

        private string GetCurrentTableName(out string colMa, out string colTen)
        {
            string sel = cboLoaiDanhMuc.SelectedItem != null ? cboLoaiDanhMuc.SelectedItem.ToString() : "";
            if (sel.StartsWith("LoaiMay")) { colMa = "MaLoai"; colTen = "TenLoai"; return "LoaiMay"; }
            if (sel.StartsWith("Chip")) { colMa = "MaChip"; colTen = "TenChip"; return "Chip"; }
            if (sel.StartsWith("RAM")) { colMa = "MaRAM"; colTen = "TenRAM"; return "RAM"; }
            if (sel.StartsWith("OCung")) { colMa = "MaOCung"; colTen = "TenOCung"; return "OCung"; }
            if (sel.StartsWith("DungLuong")) { colMa = "MaDungLuong"; colTen = "TenDungLuong"; return "DungLuong"; }
            if (sel.StartsWith("TocDo")) { colMa = "MaTocDo"; colTen = "TenTocDo"; return "TocDo"; }
            if (sel.StartsWith("ManHinh")) { colMa = "MaManHinh"; colTen = "TenManHinh"; return "ManHinh"; }
            if (sel.StartsWith("CoMH")) { colMa = "MaCoMH"; colTen = "TenCoMH"; return "CoMH"; }
            if (sel.StartsWith("Chuot")) { colMa = "MaChuot"; colTen = "TenChuot"; return "Chuot"; }
            if (sel.StartsWith("BanPhim")) { colMa = "MaBanPhim"; colTen = "TenBanPhim"; return "BanPhim"; }
            if (sel.StartsWith("Loa")) { colMa = "MaLoa"; colTen = "TenLoa"; return "Loa"; }
            if (sel.StartsWith("OCD")) { colMa = "MaOCD"; colTen = "TenOCD"; return "OCD"; }
            if (sel.StartsWith("USB")) { colMa = "MaUSB"; colTen = "TenUSB"; return "USB"; }

            colMa = "MaHangSX"; colTen = "TenHangSX"; return "HangSanXuat";
        }

        private void cboLoaiDanhMuc_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDataLK();
        }

        private void LoadDataLK()
        {
            string colMa, colTen;
            string table = GetCurrentTableName(out colMa, out colTen);
            DataTable dt = Functions.GetDataToTable(string.Format("SELECT {0} AS Ma, {1} AS Ten FROM {2}", colMa, colTen, table));
            dgvLK.DataSource = dt;
            if (dgvLK.Columns.Contains("Ma")) dgvLK.Columns["Ma"].HeaderText = "Mã " + table;
            if (dgvLK.Columns.Contains("Ten")) dgvLK.Columns["Ten"].HeaderText = "Tên / Thông Số " + table;
            txtMaLK.Text = "";
            txtTenLK.Text = "";
            txtMaLK.Enabled = true;
        }

        private void dgvLK_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvLK.CurrentRow == null || dgvLK.CurrentRow.Index < 0) return;
            txtMaLK.Text = dgvLK.CurrentRow.Cells["Ma"].Value.ToString();
            txtTenLK.Text = dgvLK.CurrentRow.Cells["Ten"].Value.ToString();
            txtMaLK.Enabled = false;
        }

        private void btnThemLK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaLK.Text) || string.IsNullOrWhiteSpace(txtTenLK.Text))
            {
                MessageBox.Show("Vui lòng nhập Mã và Tên thông số linh kiện!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string colMa, colTen;
            string table = GetCurrentTableName(out colMa, out colTen);

            if (Functions.CheckKey(string.Format("SELECT {0} FROM {1} WHERE {0} = N'{2}'", colMa, table, txtMaLK.Text.Trim())))
            {
                MessageBox.Show("Mã mục này đã tồn tại trong bảng " + table + "!", "Trùng Lặp", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string sql = string.Format("INSERT INTO {0} ({1}, {2}) VALUES (N'{3}', N'{4}')",
                table, colMa, colTen, txtMaLK.Text.Trim(), txtTenLK.Text.Trim().Replace("'", "''"));
            Functions.RunSQL(sql);

            Functions.LogAction("Thêm danh mục", string.Format("Thêm mục [{0}] vào bảng {1}", txtTenLK.Text.Trim(), table));
            MessageBox.Show("Thêm mới vào bảng " + table + " thành công!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnLamMoiLK.PerformClick();
            LoadDataLK();
        }

        private void btnSuaLK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaLK.Text)) return;
            string colMa, colTen;
            string table = GetCurrentTableName(out colMa, out colTen);

            string sql = string.Format("UPDATE {0} SET {1} = N'{2}' WHERE {3} = N'{4}'",
                table, colTen, txtTenLK.Text.Trim().Replace("'", "''"), colMa, txtMaLK.Text.Trim());
            Functions.RunSQL(sql);

            Functions.LogAction("Sửa danh mục", string.Format("Cập nhật mã [{0}] trong bảng {1}", txtMaLK.Text.Trim(), table));
            MessageBox.Show("Cập nhật thành công!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadDataLK();
        }

        private void btnXoaLK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaLK.Text)) return;
            string colMa, colTen;
            string table = GetCurrentTableName(out colMa, out colTen);

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa mục này khỏi bảng " + table + "?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string sql = string.Format("DELETE FROM {0} WHERE {1} = N'{2}'", table, colMa, txtMaLK.Text.Trim());
                Functions.RunSqlDel(sql);
                btnLamMoiLK.PerformClick();
                LoadDataLK();
            }
        }

        private void btnLamMoiLK_Click(object sender, EventArgs e)
        {
            txtMaLK.Text = "";
            txtTenLK.Text = "";
            txtMaLK.Enabled = true;
            txtMaLK.Focus();
        }
    }
}
