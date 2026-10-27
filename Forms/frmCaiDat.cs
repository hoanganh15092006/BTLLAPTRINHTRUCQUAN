using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using QLBanMayViTinh.Classes;

namespace QLBanMayViTinh.Forms
{
    public partial class frmCaiDat : Form
    {
        public frmCaiDat()
        {
            InitializeComponent();
        }

        private void frmCaiDat_Load(object sender, EventArgs e)
        {
            if (this.DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                return;

            UIThemeHelper.StyleCard(pnlDbCard);
            UIThemeHelper.StyleCard(pnlThemeCard);
            UIThemeHelper.StyleCard(pnlStoreCard);

            UIThemeHelper.StyleButton(btnTestConnect, Color.FromArgb(79, 70, 229), Color.White);
            UIThemeHelper.StyleButton(btnLuuConnect, UIThemeHelper.BtnThemColor, Color.White);
            UIThemeHelper.StyleButton(btnLuuGiaoDien, UIThemeHelper.BtnLuuColor, Color.White);
            UIThemeHelper.StyleButton(btnLuuCuaHang, UIThemeHelper.BtnSuaColor, Color.White);

            rdoLight.Checked = !UIThemeHelper.IsDarkMode;
            rdoDark.Checked = UIThemeHelper.IsDarkMode;

            LoadStoreSettings();
        }

        private void LoadStoreSettings()
        {
            try
            {
                if (Functions.Conn != null && Functions.Conn.State == ConnectionState.Open)
                {
                    string sql = "SELECT KeyName, KeyValue FROM tblCauHinhHeThong";
                    DataTable dt = Functions.GetDataToTable(sql);
                    foreach (DataRow row in dt.Rows)
                    {
                        string k = row["KeyName"].ToString();
                        string v = row["KeyValue"].ToString();
                        if (k == "TenCuaHang") txtTenCuaHang.Text = v;
                        else if (k == "DiaChi") txtDiaChiCH.Text = v;
                        else if (k == "Hotline") txtHotline.Text = v;
                        else if (k == "LoiChao") txtLoiChao.Text = v;
                    }
                }
            }
            catch
            {
            }
        }

        private void btnTestConnect_Click(object sender, EventArgs e)
        {
            lblDbStatus.Text = "Đang kiểm tra kết nối...";
            lblDbStatus.ForeColor = Color.FromArgb(79, 70, 229);
            Application.DoEvents();

            string server = txtServer.Text.Trim();
            string db = txtDatabase.Text.Trim();
            string testConnStr = string.Format("Data Source={0};Initial Catalog={1};Integrated Security=True;Connect Timeout=4", server, db);

            try
            {
                using (SqlConnection conn = new SqlConnection(testConnStr))
                {
                    conn.Open();
                }
                lblDbStatus.Text = "Kết nối thành công tới SQL Server!";
                lblDbStatus.ForeColor = Color.FromArgb(16, 185, 129);
            }
            catch (Exception ex)
            {
                lblDbStatus.Text = "Lỗi kết nối: " + ex.Message;
                lblDbStatus.ForeColor = Color.FromArgb(239, 68, 68);
            }
        }

        private void btnLuuConnect_Click(object sender, EventArgs e)
        {
            string server = txtServer.Text.Trim();
            string db = txtDatabase.Text.Trim();
            string newConnStr = string.Format("Data Source={0};Initial Catalog={1};Integrated Security=True", server, db);

            try
            {
                using (SqlConnection conn = new SqlConnection(newConnStr))
                {
                    conn.Open();
                }

                Functions.ConnString = newConnStr;
                Functions.Disconnect();
                Functions.Connect();

                string encConn = SecurityHelper.EncryptAES(newConnStr);
                string sqlSave = @"IF EXISTS (SELECT 1 FROM tblCauHinhHeThong WHERE KeyName = 'ConnString')
                                   UPDATE tblCauHinhHeThong SET KeyValue = @v WHERE KeyName = 'ConnString'
                                   ELSE
                                   INSERT INTO tblCauHinhHeThong (KeyName, KeyValue) VALUES ('ConnString', @v)";
                using (SqlCommand cmd = new SqlCommand(sqlSave, Functions.Conn))
                {
                    cmd.Parameters.AddWithValue("@v", encConn);
                    cmd.ExecuteNonQuery();
                }

                Functions.LogAction("Cài đặt kết nối", "Lưu chuỗi kết nối SQL Server mới đã được mã hóa AES-256");
                MessageBox.Show("Lưu chuỗi kết nối thành công và đã được mã hóa AES-256 an toàn!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể lưu cấu hình vì kết nối không hợp lệ:\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLuuGiaoDien_Click(object sender, EventArgs e)
        {
            UIThemeHelper.IsDarkMode = rdoDark.Checked;
            Functions.LogAction("Đổi giao diện", "Chuyển giao diện sang: " + (UIThemeHelper.IsDarkMode ? "Dark Mode" : "Light Mode"));
            MessageBox.Show("Đã lưu chế độ giao diện: " + (UIThemeHelper.IsDarkMode ? "Dark Mode (Tối)" : "Light Mode (Sáng)") + "!\nGiao diện sẽ cập nhật khi chuyển đổi các phân hệ làm việc.",
                            "Thành Công", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnLuuCuaHang_Click(object sender, EventArgs e)
        {
            try
            {
                SaveConfigKey("TenCuaHang", txtTenCuaHang.Text.Trim());
                SaveConfigKey("DiaChi", txtDiaChiCH.Text.Trim());
                SaveConfigKey("Hotline", txtHotline.Text.Trim());
                SaveConfigKey("LoiChao", txtLoiChao.Text.Trim());

                Functions.LogAction("Cài đặt cửa hàng", "Cập nhật thông tin cửa hàng và mẫu in hóa đơn");
                MessageBox.Show("Lưu thông tin cửa hàng in trên hóa đơn thành công!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lưu cấu hình: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveConfigKey(string key, string val)
        {
            string sql = @"IF EXISTS (SELECT 1 FROM tblCauHinhHeThong WHERE KeyName = @k)
                           UPDATE tblCauHinhHeThong SET KeyValue = @v WHERE KeyName = @k
                           ELSE
                           INSERT INTO tblCauHinhHeThong (KeyName, KeyValue) VALUES (@k, @v)";
            using (SqlCommand cmd = new SqlCommand(sql, Functions.Conn))
            {
                cmd.Parameters.AddWithValue("@k", key);
                cmd.Parameters.AddWithValue("@v", val ?? "");
                cmd.ExecuteNonQuery();
            }
        }
    }
}
