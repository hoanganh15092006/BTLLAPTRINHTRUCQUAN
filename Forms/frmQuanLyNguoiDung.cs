using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using QLBanMayViTinh.Classes;

namespace QLBanMayViTinh.Forms
{
    public partial class frmQuanLyNguoiDung : Form
    {
        private DataTable tblUsers;

        public frmQuanLyNguoiDung()
        {
            InitializeComponent();
        }

        private void frmQuanLyNguoiDung_Load(object sender, EventArgs e)
        {
            if (this.DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                return;

            UIThemeHelper.StyleCard(pnlDetailCard);
            UIThemeHelper.StyleCard(pnlGridCard);
            UIThemeHelper.StyleDataGridView(dgvTaiKhoan);

            UIThemeHelper.StyleButton(btnThem, UIThemeHelper.BtnThemColor, Color.White);
            UIThemeHelper.StyleButton(btnSua, UIThemeHelper.BtnSuaColor, Color.White);
            UIThemeHelper.StyleButton(btnKhoaMoKhoa, Color.FromArgb(239, 68, 68), Color.White);
            UIThemeHelper.StyleButton(btnDatLaiMK, Color.FromArgb(79, 70, 229), Color.White);
            UIThemeHelper.StyleButton(btnLamMoi, Color.FromArgb(100, 116, 139), Color.White);

            Functions.FillCombo("SELECT MaNV, (MaNV + ' - ' + TenNV) AS TenHienThi FROM NhanVien", cboMaNV, "MaNV", "TenHienThi");
            cboVaiTro.SelectedIndex = 1;
            cboTrangThai.SelectedIndex = 0;

            LoadGrid();
        }

        private void LoadGrid()
        {
            try
            {
                string sql = @"SELECT tk.TenDangNhap, nv.TenNV, tk.VaiTro, tk.TrangThai, tk.NgayTao, tk.MaNV
                               FROM tblTaiKhoan tk
                               LEFT JOIN NhanVien nv ON tk.MaNV = nv.MaNV
                               ORDER BY tk.NgayTao DESC";

                tblUsers = Functions.GetDataToTable(sql);
                dgvTaiKhoan.DataSource = tblUsers;

                if (dgvTaiKhoan.Columns.Contains("TenDangNhap"))
                    dgvTaiKhoan.Columns["TenDangNhap"].HeaderText = "Tên Đăng Nhập";
                if (dgvTaiKhoan.Columns.Contains("TenNV"))
                    dgvTaiKhoan.Columns["TenNV"].HeaderText = "Nhân Viên";
                if (dgvTaiKhoan.Columns.Contains("VaiTro"))
                    dgvTaiKhoan.Columns["VaiTro"].HeaderText = "Vai Trò (Role)";
                if (dgvTaiKhoan.Columns.Contains("TrangThai"))
                    dgvTaiKhoan.Columns["TrangThai"].HeaderText = "Trạng Thái";
                if (dgvTaiKhoan.Columns.Contains("NgayTao"))
                    dgvTaiKhoan.Columns["NgayTao"].HeaderText = "Ngày Khởi Tạo";
                if (dgvTaiKhoan.Columns.Contains("MaNV"))
                    dgvTaiKhoan.Columns["MaNV"].Visible = false;
            }
            catch
            {
            }
        }

        private void dgvTaiKhoan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvTaiKhoan.CurrentRow == null || dgvTaiKhoan.CurrentRow.Index < 0) return;

            txtTenDangNhap.Text = dgvTaiKhoan.CurrentRow.Cells["TenDangNhap"].Value.ToString();
            txtTenDangNhap.Enabled = false;

            string vaiTro = dgvTaiKhoan.CurrentRow.Cells["VaiTro"].Value.ToString();
            cboVaiTro.SelectedItem = vaiTro;

            string trangThai = dgvTaiKhoan.CurrentRow.Cells["TrangThai"].Value.ToString();
            cboTrangThai.SelectedItem = trangThai;

            object maNV = dgvTaiKhoan.CurrentRow.Cells["MaNV"].Value;
            if (maNV != null && maNV != DBNull.Value)
            {
                cboMaNV.SelectedValue = maNV.ToString();
            }
            else
            {
                cboMaNV.SelectedIndex = -1;
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string username = txtTenDangNhap.Text.Trim();
            string password = txtMatKhauMoi.Text;
            string role = cboVaiTro.SelectedItem != null ? cboVaiTro.SelectedItem.ToString() : "BanHang";
            string status = cboTrangThai.SelectedItem != null ? cboTrangThai.SelectedItem.ToString() : "Hoạt động";
            string maNV = cboMaNV.SelectedValue != null ? cboMaNV.SelectedValue.ToString() : null;

            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenDangNhap.Focus();
                return;
            }

            if (string.IsNullOrEmpty(password) || password.Length < 6)
            {
                MessageBox.Show("Vui lòng nhập mật khẩu khởi tạo từ 6 ký tự trở lên!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMatKhauMoi.Focus();
                return;
            }

            if (Functions.CheckKey("SELECT TenDangNhap FROM tblTaiKhoan WHERE TenDangNhap = N'" + username + "'"))
            {
                MessageBox.Show("Tên đăng nhập này đã tồn tại, vui lòng chọn tên khác!", "Trùng Lặp", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenDangNhap.Focus();
                return;
            }

            try
            {
                string salt;
                string hash = SecurityHelper.HashPassword(password, out salt);

                string sql = @"INSERT INTO tblTaiKhoan (TenDangNhap, MatKhauHash, Salt, MaNV, VaiTro, TrangThai, NgayTao)
                               VALUES (@u, @h, @s, @nv, @v, @t, GETDATE())";

                using (SqlCommand cmd = new SqlCommand(sql, Functions.Conn))
                {
                    cmd.Parameters.AddWithValue("@u", username);
                    cmd.Parameters.AddWithValue("@h", hash);
                    cmd.Parameters.AddWithValue("@s", salt);
                    cmd.Parameters.AddWithValue("@nv", (object)maNV ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@v", role);
                    cmd.Parameters.AddWithValue("@t", status);
                    cmd.ExecuteNonQuery();
                }

                Functions.LogAction("Thêm tài khoản", "Admin thêm tài khoản mới: [" + username + "] vai trò [" + role + "]");
                MessageBox.Show("Thêm mới tài khoản [" + username + "] thành công!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnLamMoi.PerformClick();
                LoadGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thêm tài khoản: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string username = txtTenDangNhap.Text.Trim();
            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("Vui lòng chọn tài khoản cần sửa trong danh sách!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string role = cboVaiTro.SelectedItem != null ? cboVaiTro.SelectedItem.ToString() : "BanHang";
            string status = cboTrangThai.SelectedItem != null ? cboTrangThai.SelectedItem.ToString() : "Hoạt động";
            string maNV = cboMaNV.SelectedValue != null ? cboMaNV.SelectedValue.ToString() : null;

            try
            {
                string sql = @"UPDATE tblTaiKhoan
                               SET VaiTro = @v, TrangThai = @t, MaNV = @nv
                               WHERE TenDangNhap = @u";

                using (SqlCommand cmd = new SqlCommand(sql, Functions.Conn))
                {
                    cmd.Parameters.AddWithValue("@v", role);
                    cmd.Parameters.AddWithValue("@t", status);
                    cmd.Parameters.AddWithValue("@nv", (object)maNV ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@u", username);
                    cmd.ExecuteNonQuery();
                }

                Functions.LogAction("Sửa tài khoản", "Admin cập nhật quyền của [" + username + "] sang [" + role + "], trạng thái [" + status + "]");
                MessageBox.Show("Cập nhật quyền tài khoản [" + username + "] thành công!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi cập nhật: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnKhoaMoKhoa_Click(object sender, EventArgs e)
        {
            string username = txtTenDangNhap.Text.Trim();
            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("Vui lòng chọn tài khoản cần Khóa/Mở khóa!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (username.ToLower() == Functions.CurrentUsername.ToLower())
            {
                MessageBox.Show("Bạn không thể tự khóa tài khoản của chính mình!", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string currentStatus = cboTrangThai.SelectedItem != null ? cboTrangThai.SelectedItem.ToString() : "Hoạt động";
            string newStatus = currentStatus == "Hoạt động" ? "Khóa" : "Hoạt động";

            try
            {
                string sql = "UPDATE tblTaiKhoan SET TrangThai = @t WHERE TenDangNhap = @u";
                using (SqlCommand cmd = new SqlCommand(sql, Functions.Conn))
                {
                    cmd.Parameters.AddWithValue("@t", newStatus);
                    cmd.Parameters.AddWithValue("@u", username);
                    cmd.ExecuteNonQuery();
                }

                Functions.LogAction("Khóa/Mở khóa", "Admin đổi trạng thái tài khoản [" + username + "] thành [" + newStatus + "]");
                MessageBox.Show("Đã đổi trạng thái tài khoản [" + username + "] thành: " + newStatus, "Thành Công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDatLaiMK_Click(object sender, EventArgs e)
        {
            string username = txtTenDangNhap.Text.Trim();
            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("Vui lòng chọn tài khoản cần cấp lại mật khẩu!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string newPass = string.IsNullOrWhiteSpace(txtMatKhauMoi.Text) ? "123456" : txtMatKhauMoi.Text.Trim();

            try
            {
                string salt;
                string hash = SecurityHelper.HashPassword(newPass, out salt);

                string sql = "UPDATE tblTaiKhoan SET MatKhauHash = @h, Salt = @s WHERE TenDangNhap = @u";
                using (SqlCommand cmd = new SqlCommand(sql, Functions.Conn))
                {
                    cmd.Parameters.AddWithValue("@h", hash);
                    cmd.Parameters.AddWithValue("@s", salt);
                    cmd.Parameters.AddWithValue("@u", username);
                    cmd.ExecuteNonQuery();
                }

                Functions.LogAction("Reset mật khẩu", "Admin cấp lại mật khẩu cho tài khoản [" + username + "]");
                MessageBox.Show("Đã cấp lại mật khẩu cho tài khoản [" + username + "] thành công!\nMật khẩu mới là: " + newPass,
                                "Thành Công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMatKhauMoi.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi reset mật khẩu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtTenDangNhap.Text = "";
            txtTenDangNhap.Enabled = true;
            txtMatKhauMoi.Text = "";
            cboVaiTro.SelectedIndex = 1;
            cboTrangThai.SelectedIndex = 0;
            cboMaNV.SelectedIndex = -1;
            txtTenDangNhap.Focus();
        }
    }
}
