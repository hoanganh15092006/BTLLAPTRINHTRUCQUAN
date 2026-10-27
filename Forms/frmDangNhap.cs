using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using QLBanMayViTinh.Classes;

namespace QLBanMayViTinh.Forms
{
    public partial class frmDangNhap : Form
    {
        private bool isPasswordHidden = true;

        public frmDangNhap()
        {
            InitializeComponent();
        }

        private void frmDangNhap_Load(object sender, EventArgs e)
        {
            if (this.DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                return;

            UIThemeHelper.StyleButton(btnDangNhap, UIThemeHelper.ColorPrimary, Color.White);
            UIThemeHelper.StyleButton(btnDangKy, Color.FromArgb(241, 245, 249), Color.FromArgb(79, 70, 229));
            btnDangKy.FlatAppearance.BorderSize = 1;
            btnDangKy.FlatAppearance.BorderColor = Color.FromArgb(226, 232, 240);
            UIThemeHelper.StyleButton(btnCaiDatDB, Color.Transparent, Color.FromArgb(100, 116, 139));
            btnCaiDatDB.FlatAppearance.BorderSize = 0;

            btnTogglePass.FlatStyle = FlatStyle.Flat;
            btnTogglePass.FlatAppearance.BorderSize = 0;
            btnTogglePass.BackColor = Color.FromArgb(241, 245, 249);

            try
            {
                Functions.Connect();
            }
            catch
            {
            }
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            lblStatus.Text = "";
            string username = txtTenDangNhap.Text.Trim();
            string password = txtMatKhau.Text;

            if (string.IsNullOrEmpty(username))
            {
                lblStatus.Text = "Vui lòng nhập tên đăng nhập!";
                txtTenDangNhap.Focus();
                return;
            }

            if (string.IsNullOrEmpty(password))
            {
                lblStatus.Text = "Vui lòng nhập mật khẩu!";
                txtMatKhau.Focus();
                return;
            }

            try
            {
                Functions.Connect();
                if (Functions.Conn == null || Functions.Conn.State != ConnectionState.Open)
                {
                    lblStatus.Text = "Không thể kết nối đến SQL Server. Vui lòng bấm [Cấu hình CSDL]!";
                    return;
                }

                string sql = "SELECT MatKhauHash, Salt, MaNV, VaiTro, TrangThai FROM tblTaiKhoan WHERE TenDangNhap = @u";
                string storedHash = null;
                string storedSalt = null;
                string maNV = null;
                string vaiTro = "BanHang";
                string trangThai = "Hoạt động";

                using (SqlCommand cmd = new SqlCommand(sql, Functions.Conn))
                {
                    cmd.Parameters.AddWithValue("@u", username);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            storedHash = reader["MatKhauHash"].ToString();
                            storedSalt = reader["Salt"].ToString();
                            maNV = reader["MaNV"] != DBNull.Value ? reader["MaNV"].ToString() : "";
                            vaiTro = reader["VaiTro"].ToString();
                            trangThai = reader["TrangThai"].ToString();
                        }
                    }
                }

                if (string.IsNullOrEmpty(storedHash))
                {
                    lblStatus.Text = "Tên đăng nhập không tồn tại trong hệ thống!";
                    txtTenDangNhap.Focus();
                    return;
                }

                if (trangThai == "Khóa")
                {
                    lblStatus.Text = "Tài khoản này hiện đang bị khóa. Vui lòng liên hệ Admin!";
                    return;
                }

                bool isValid = SecurityHelper.VerifyPassword(password, storedHash, storedSalt);
                if (!isValid)
                {
                    lblStatus.Text = "Mật khẩu không chính xác. Vui lòng thử lại!";
                    txtMatKhau.Focus();
                    return;
                }

                Functions.CurrentUsername = username;
                Functions.CurrentRole = vaiTro;
                Functions.CurrentMaNV = maNV;

                if (!string.IsNullOrEmpty(maNV))
                {
                    string fullName = Functions.GetFieldValues("SELECT TenNV FROM NhanVien WHERE MaNV = N'" + maNV + "'");
                    Functions.CurrentFullName = !string.IsNullOrEmpty(fullName) ? fullName : username;
                }
                else
                {
                    Functions.CurrentFullName = username;
                }

                Functions.LogAction("Đăng nhập", "Người dùng [" + username + "] đăng nhập thành công với vai trò [" + vaiTro + "]");

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Lỗi xác thực: " + ex.Message;
            }
        }

        private void btnTogglePass_Click(object sender, EventArgs e)
        {
            isPasswordHidden = !isPasswordHidden;
            txtMatKhau.UseSystemPasswordChar = isPasswordHidden;
            btnTogglePass.Text = isPasswordHidden ? "👁️" : "🙈";
        }

        private void lnkQuenMatKhau_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (frmQuenMatKhau frm = new frmQuenMatKhau())
            {
                frm.ShowDialog();
            }
        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            using (frmDangKy frm = new frmDangKy())
            {
                frm.ShowDialog();
            }
        }

        private void btnCaiDatDB_Click(object sender, EventArgs e)
        {
            using (frmCaiDat frm = new frmCaiDat())
            {
                frm.ShowDialog();
            }
        }

        private void txtTenDangNhap_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtMatKhau.Focus();
            }
        }

        private void txtMatKhau_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnDangNhap.PerformClick();
            }
        }
    }
}
