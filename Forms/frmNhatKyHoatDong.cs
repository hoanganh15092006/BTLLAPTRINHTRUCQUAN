using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using QLBanMayViTinh.Classes;

namespace QLBanMayViTinh.Forms
{
    public partial class frmNhatKyHoatDong : Form
    {
        public frmNhatKyHoatDong()
        {
            InitializeComponent();
        }

        private void frmNhatKyHoatDong_Load(object sender, EventArgs e)
        {
            if (this.DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                return;

            UIThemeHelper.StyleCard(pnlFilterCard);
            UIThemeHelper.StyleCard(pnlLogsCard);
            UIThemeHelper.StyleCard(pnlAlertCard, Color.FromArgb(239, 68, 68));

            UIThemeHelper.StyleDataGridView(dgvLogs);
            UIThemeHelper.StyleDataGridView(dgvAlerts);

            UIThemeHelper.StyleButton(btnLoc, UIThemeHelper.BtnTimColor, Color.White);
            UIThemeHelper.StyleButton(btnLamMoi, Color.FromArgb(100, 116, 139), Color.White);
            UIThemeHelper.StyleButton(btnXoaNhatKy, UIThemeHelper.BtnXoaColor, Color.White);

            btnXoaNhatKy.Visible = (Functions.CurrentRole == "Admin");
            cboHanhDong.SelectedIndex = 0;

            LoadLogs();
            LoadLowStockAlerts();
        }

        private void LoadLogs()
        {
            try
            {
                string sql = "SELECT TOP 100 Id, TenDangNhap, HanhDong, ThoiGian, ChiTiet FROM tblNhatKyHoatDong WHERE 1=1";

                if (!string.IsNullOrWhiteSpace(txtNguoiDung.Text))
                {
                    sql += " AND TenDangNhap LIKE N'%" + txtNguoiDung.Text.Trim().Replace("'", "''") + "%'";
                }

                if (cboHanhDong.SelectedIndex > 0)
                {
                    sql += " AND HanhDong = N'" + cboHanhDong.SelectedItem.ToString().Replace("'", "''") + "'";
                }

                sql += " ORDER BY ThoiGian DESC";

                DataTable dt = Functions.GetDataToTable(sql);
                dgvLogs.DataSource = dt;

                if (dgvLogs.Columns.Contains("Id")) dgvLogs.Columns["Id"].Width = 50;
                if (dgvLogs.Columns.Contains("TenDangNhap")) dgvLogs.Columns["TenDangNhap"].HeaderText = "Tài Khoản";
                if (dgvLogs.Columns.Contains("HanhDong")) dgvLogs.Columns["HanhDong"].HeaderText = "Hành Động";
                if (dgvLogs.Columns.Contains("ThoiGian")) dgvLogs.Columns["ThoiGian"].HeaderText = "Thời Gian";
                if (dgvLogs.Columns.Contains("ChiTiet")) dgvLogs.Columns["ChiTiet"].HeaderText = "Chi Tiết Thao Tác";
            }
            catch
            {
            }
        }

        private void LoadLowStockAlerts()
        {
            try
            {
                string sql = @"SELECT MaMayVT, TenMayVT, SoLuong
                               FROM MayViTinh
                               WHERE SoLuong <= 5
                               ORDER BY SoLuong ASC";

                DataTable dt = Functions.GetDataToTable(sql);
                dgvAlerts.DataSource = dt;

                if (dgvAlerts.Columns.Contains("MaMayVT")) dgvAlerts.Columns["MaMayVT"].HeaderText = "Mã";
                if (dgvAlerts.Columns.Contains("TenMayVT")) dgvAlerts.Columns["TenMayVT"].HeaderText = "Tên Máy Vi Tính";
                if (dgvAlerts.Columns.Contains("SoLuong"))
                {
                    dgvAlerts.Columns["SoLuong"].HeaderText = "Tồn";
                    dgvAlerts.Columns["SoLuong"].Width = 60;
                    dgvAlerts.Columns["SoLuong"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgvAlerts.Columns["SoLuong"].DefaultCellStyle.ForeColor = Color.FromArgb(239, 68, 68);
                    dgvAlerts.Columns["SoLuong"].DefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
                }
            }
            catch
            {
            }
        }

        private void btnLoc_Click(object sender, EventArgs e)
        {
            LoadLogs();
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtNguoiDung.Text = "";
            cboHanhDong.SelectedIndex = 0;
            LoadLogs();
            LoadLowStockAlerts();
        }

        private void btnXoaNhatKy_Click(object sender, EventArgs e)
        {
            if (Functions.CurrentRole != "Admin")
            {
                MessageBox.Show("Chỉ tài khoản Quản Trị Viên (Admin) mới có quyền xóa nhật ký hệ thống!", "Từ Chối Quyền", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Bạn có chắc chắn muốn dọn dẹp xóa sạch toàn bộ lịch sử thao tác không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Functions.RunSQL("TRUNCATE TABLE tblNhatKyHoatDong");
                Functions.LogAction("Dọn dẹp nhật ký", "Admin xóa toàn bộ nhật ký hệ thống");
                LoadLogs();
            }
        }
    }
}
