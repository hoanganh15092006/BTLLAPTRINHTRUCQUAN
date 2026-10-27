using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using QLBanMayViTinh.Classes;

namespace QLBanMayViTinh.Forms
{
    public partial class frmDangKy : Form
    {
        public frmDangKy()
        {
            InitializeComponent();
        }

        private void frmDangKy_Load(object sender, EventArgs e)
        {
            if (this.DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                return;

            UIThemeHelper.StyleButton(btnDangKy, UIThemeHelper.ColorPrimary, Color.White);
            UIThemeHelper.StyleButton(btnDong, Color.FromArgb(100, 116, 139), Color.White);

            Functions.FillCombo("SELECT MaNV, (MaNV + ' - ' + TenNV) AS TenHienThi FROM NhanVien", cboNhanVien, "MaNV", "TenHienThi");
            cboVaiTro.SelectedIndex = 0;
        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            lblThongBao.Text = "";
            string username = txtTenDangNhap.Text.Trim();
            string password = txtMatKhau.Text;
            string confirm = txtXacNhan.Text;
            string role = cboVaiTro.SelectedItem != null ? cboVaiTro.SelectedItem.ToString() : "BanHang";
            string maNV = cboNhanVien.SelectedValue != null ? cboNhanVien.SelectedValue.ToString() : null;

            if (string.IsNullOrEmpty(username))
            {
                lblThongBao.Text = "Vui lòng nhập tên đăng nhập!";
                txtTenDangNhap.Focus();
                return;
            }

            if (username.Length < 3)
            {
                lblThongBao.Text = "Tên đăng nhập phải có ít nhất 3 ký tự!";
                txtTenDangNhap.Focus();
                return;
            }

            if (string.IsNullOrEmpty(password) || password.Length < 6)
            {
                lblThongBao.Text = "Mật khẩu phải chứa ít nhất 6 ký tự!";
                txtMatKhau.Focus();
                return;
            }

            if (password != confirm)
            {
                lblThongBao.Text = "Mật khẩu xác nhận không khớp!";
                txtXacNhan.Focus();
                return;
            }

            try
            {
                Functions.Connect();
                string checkUser = "SELECT TenDangNhap FROM tblTaiKhoan WHERE TenDangNhap = @u";
                using (SqlCommand cmd = new SqlCommand(checkUser, Functions.Conn))
                {
                    cmd.Parameters.AddWithValue("@u", username);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            lblThongBao.Text = "Tên đăng nhập này đã được sử dụng. Vui lòng chọn tên khác!";
                            txtTenDangNhap.Focus();
                            return;
                        }
                    }
                }

                string salt;
                string hash = SecurityHelper.HashPassword(password, out salt);

                string insertSql = @"INSERT INTO tblTaiKhoan (TenDangNhap, MatKhauHash, Salt, MaNV, VaiTro, TrangThai, NgayTao)
                                     VALUES (@u, @h, @s, @nv, @v, N'Hoạt động', GETDATE())";

                using (SqlCommand cmd = new SqlCommand(insertSql, Functions.Conn))
                {
                    cmd.Parameters.AddWithValue("@u", username);
                    cmd.Parameters.AddWithValue("@h", hash);
                    cmd.Parameters.AddWithValue("@s", salt);
                    cmd.Parameters.AddWithValue("@nv", (object)maNV ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@v", role);
                    cmd.ExecuteNonQuery();
                }

                Functions.LogAction("Đăng ký tài khoản", "Tạo tài khoản mới: [" + username + "] với vai trò [" + role + "]");

                MessageBox.Show("Đăng ký tài khoản [" + username + "] thành công!\nBạn có thể đăng nhập ngay bây giờ.",
                                "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                lblThongBao.Text = "Lỗi đăng ký: " + ex.Message;
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
