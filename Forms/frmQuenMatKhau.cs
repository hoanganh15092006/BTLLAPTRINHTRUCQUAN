using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using QLBanMayViTinh.Classes;

namespace QLBanMayViTinh.Forms
{
    public partial class frmQuenMatKhau : Form
    {
        public frmQuenMatKhau()
        {
            InitializeComponent();
        }

        private void frmQuenMatKhau_Load(object sender, EventArgs e)
        {
            if (this.DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                return;

            UIThemeHelper.StyleButton(btnDatLaiMatKhau, UIThemeHelper.ColorPrimary, Color.White);
            UIThemeHelper.StyleButton(btnDong, Color.FromArgb(100, 116, 139), Color.White);
        }

        private void btnDatLaiMatKhau_Click(object sender, EventArgs e)
        {
            lblThongBao.Text = "";
            string username = txtTenDangNhap.Text.Trim();
            string phone = txtDienThoai.Text.Trim();
            string newPass = txtMatKhauMoi.Text;
            string confirm = txtXacNhan.Text;

            if (string.IsNullOrEmpty(username))
            {
                lblThongBao.Text = "Vui lòng nhập tên đăng nhập!";
                txtTenDangNhap.Focus();
                return;
            }

            if (string.IsNullOrEmpty(phone))
            {
                lblThongBao.Text = "Vui lòng nhập số điện thoại xác minh!";
                txtDienThoai.Focus();
                return;
            }

            if (string.IsNullOrEmpty(newPass) || newPass.Length < 6)
            {
                lblThongBao.Text = "Mật khẩu mới phải có ít nhất 6 ký tự!";
                txtMatKhauMoi.Focus();
                return;
            }

            if (newPass != confirm)
            {
                lblThongBao.Text = "Mật khẩu xác nhận không trùng khớp!";
                txtXacNhan.Focus();
                return;
            }

            try
            {
                Functions.Connect();
                string sqlCheck = @"SELECT tk.TenDangNhap, tk.MaNV, nv.DienThoai
                                    FROM tblTaiKhoan tk
                                    LEFT JOIN NhanVien nv ON tk.MaNV = nv.MaNV
                                    WHERE tk.TenDangNhap = @u";

                string dbPhone = "";
                bool found = false;

                using (SqlCommand cmd = new SqlCommand(sqlCheck, Functions.Conn))
                {
                    cmd.Parameters.AddWithValue("@u", username);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            found = true;
                            if (reader["DienThoai"] != DBNull.Value)
                            {
                                dbPhone = reader["DienThoai"].ToString().Trim();
                            }
                        }
                    }
                }

                if (!found)
                {
                    lblThongBao.Text = "Tên đăng nhập không tồn tại!";
                    txtTenDangNhap.Focus();
                    return;
                }

                if (username != "admin" && dbPhone != phone)
                {
                    lblThongBao.Text = "Số điện thoại xác minh không khớp với thông tin nhân viên!";
                    txtDienThoai.Focus();
                    return;
                }

                string salt;
                string hash = SecurityHelper.HashPassword(newPass, out salt);

                string sqlUpdate = "UPDATE tblTaiKhoan SET MatKhauHash = @h, Salt = @s WHERE TenDangNhap = @u";
                using (SqlCommand cmd = new SqlCommand(sqlUpdate, Functions.Conn))
                {
                    cmd.Parameters.AddWithValue("@h", hash);
                    cmd.Parameters.AddWithValue("@s", salt);
                    cmd.Parameters.AddWithValue("@u", username);
                    cmd.ExecuteNonQuery();
                }

                Functions.LogAction("Khôi phục mật khẩu", "Tài khoản [" + username + "] đã đặt lại mật khẩu mới thành công");

                MessageBox.Show("Đặt lại mật khẩu cho tài khoản [" + username + "] thành công!\nBạn có thể đăng nhập bằng mật khẩu mới.",
                                "Thành Công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                lblThongBao.Text = "Lỗi khôi phục: " + ex.Message;
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
