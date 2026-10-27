using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using QLBanMayViTinh.Classes;

namespace QLBanMayViTinh.Forms
{
    public partial class frmHoSoCaNhan : Form
    {
        public frmHoSoCaNhan()
        {
            InitializeComponent();
        }

        private void frmHoSoCaNhan_Load(object sender, EventArgs e)
        {
            if (this.DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                return;

            UIThemeHelper.StyleCard(pnlInfoCard);
            UIThemeHelper.StyleCard(pnlPassCard);
            UIThemeHelper.StyleCard(pnlGridCard);
            UIThemeHelper.StyleDataGridView(dgvLichSu);

            UIThemeHelper.StyleButton(btnLuuThongTin, UIThemeHelper.ColorPrimary, Color.White);
            UIThemeHelper.StyleButton(btnDoiMatKhau, Color.FromArgb(16, 185, 129), Color.White);

            LoadUserProfile();
            LoadUserHistory();
        }

        private void LoadUserProfile()
        {
            lblUsername.Text = Functions.CurrentUsername;
            lblRoleBadge.Text = "Vai Trò: " + Functions.CurrentRole;

            if (!string.IsNullOrEmpty(Functions.CurrentMaNV))
            {
                string sql = "SELECT TenNV, GioiTinh, DiaChi, DienThoai FROM NhanVien WHERE MaNV = @nv";
                using (SqlCommand cmd = new SqlCommand(sql, Functions.Conn))
                {
                    cmd.Parameters.AddWithValue("@nv", Functions.CurrentMaNV);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            txtHoTen.Text = reader["TenNV"].ToString();
                            txtGioiTinh.Text = reader["GioiTinh"] != DBNull.Value ? reader["GioiTinh"].ToString() : "";
                            string rawAddr = reader["DiaChi"] != DBNull.Value ? reader["DiaChi"].ToString() : "";
                            string rawPhone = reader["DienThoai"] != DBNull.Value ? reader["DienThoai"].ToString() : "";

                            txtDiaChi.Text = SecurityHelper.DecryptAES(rawAddr);
                            txtDienThoai.Text = SecurityHelper.DecryptAES(rawPhone);
                        }
                    }
                }
            }
            else
            {
                txtHoTen.Text = Functions.CurrentFullName;
            }
        }

        private void LoadUserHistory()
        {
            try
            {
                string sql = @"SELECT TOP 20 HanhDong, ThoiGian, ChiTiet
                               FROM tblNhatKyHoatDong
                               WHERE TenDangNhap = @u
                               ORDER BY ThoiGian DESC";
                using (SqlCommand cmd = new SqlCommand(sql, Functions.Conn))
                {
                    cmd.Parameters.AddWithValue("@u", Functions.CurrentUsername);
                    using (SqlDataAdapter dap = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        dap.Fill(dt);
                        dgvLichSu.DataSource = dt;

                        if (dgvLichSu.Columns.Contains("HanhDong"))
                            dgvLichSu.Columns["HanhDong"].HeaderText = "Hành Động";
                        if (dgvLichSu.Columns.Contains("ThoiGian"))
                            dgvLichSu.Columns["ThoiGian"].HeaderText = "Thời Gian";
                        if (dgvLichSu.Columns.Contains("ChiTiet"))
                            dgvLichSu.Columns["ChiTiet"].HeaderText = "Chi Tiết Thao Tác";
                    }
                }
            }
            catch
            {
            }
        }

        private void btnLuuThongTin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Functions.CurrentMaNV))
            {
                MessageBox.Show("Tài khoản này là tài khoản hệ thống đặc biệt, không liên kết mã nhân viên!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                string encPhone = SecurityHelper.EncryptAES(txtDienThoai.Text.Trim());
                string encAddr = SecurityHelper.EncryptAES(txtDiaChi.Text.Trim());

                string sql = @"UPDATE NhanVien
                               SET TenNV = @ten, GioiTinh = @gt, DiaChi = @dc, DienThoai = @dt
                               WHERE MaNV = @nv";

                using (SqlCommand cmd = new SqlCommand(sql, Functions.Conn))
                {
                    cmd.Parameters.AddWithValue("@ten", txtHoTen.Text.Trim());
                    cmd.Parameters.AddWithValue("@gt", txtGioiTinh.Text.Trim());
                    cmd.Parameters.AddWithValue("@dc", encAddr);
                    cmd.Parameters.AddWithValue("@dt", encPhone);
                    cmd.Parameters.AddWithValue("@nv", Functions.CurrentMaNV);
                    cmd.ExecuteNonQuery();
                }

                Functions.CurrentFullName = txtHoTen.Text.Trim();
                Functions.LogAction("Cập nhật hồ sơ", "Người dùng [" + Functions.CurrentUsername + "] cập nhật hồ sơ cá nhân");
                MessageBox.Show("Cập nhật thông tin hồ sơ thành công! Dữ liệu nhạy cảm đã được mã hóa an toàn.", "Thành Công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadUserHistory();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi cập nhật: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDoiMatKhau_Click(object sender, EventArgs e)
        {
            string oldPass = txtMatKhauCu.Text;
            string newPass = txtMatKhauMoi.Text;
            string confirm = txtXacNhan.Text;

            if (string.IsNullOrEmpty(oldPass))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu hiện tại!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMatKhauCu.Focus();
                return;
            }

            if (string.IsNullOrEmpty(newPass) || newPass.Length < 6)
            {
                MessageBox.Show("Mật khẩu mới phải có ít nhất 6 ký tự!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMatKhauMoi.Focus();
                return;
            }

            if (newPass != confirm)
            {
                MessageBox.Show("Mật khẩu xác nhận không trùng khớp!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtXacNhan.Focus();
                return;
            }

            try
            {
                string sqlCheck = "SELECT MatKhauHash, Salt FROM tblTaiKhoan WHERE TenDangNhap = @u";
                string storedHash = null;
                string storedSalt = null;

                using (SqlCommand cmd = new SqlCommand(sqlCheck, Functions.Conn))
                {
                    cmd.Parameters.AddWithValue("@u", Functions.CurrentUsername);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            storedHash = reader["MatKhauHash"].ToString();
                            storedSalt = reader["Salt"].ToString();
                        }
                    }
                }

                if (!SecurityHelper.VerifyPassword(oldPass, storedHash, storedSalt))
                {
                    MessageBox.Show("Mật khẩu hiện tại không chính xác!", "Sai Mật Khẩu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMatKhauCu.Focus();
                    return;
                }

                string newSalt;
                string newHash = SecurityHelper.HashPassword(newPass, out newSalt);

                string sqlUpdate = "UPDATE tblTaiKhoan SET MatKhauHash = @h, Salt = @s WHERE TenDangNhap = @u";
                using (SqlCommand cmd = new SqlCommand(sqlUpdate, Functions.Conn))
                {
                    cmd.Parameters.AddWithValue("@h", newHash);
                    cmd.Parameters.AddWithValue("@s", newSalt);
                    cmd.Parameters.AddWithValue("@u", Functions.CurrentUsername);
                    cmd.ExecuteNonQuery();
                }

                txtMatKhauCu.Text = "";
                txtMatKhauMoi.Text = "";
                txtXacNhan.Text = "";

                Functions.LogAction("Đổi mật khẩu", "Người dùng [" + Functions.CurrentUsername + "] đổi mật khẩu thành công");
                MessageBox.Show("Đổi mật khẩu thành công!", "Thành Công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadUserHistory();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi đổi mật khẩu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
