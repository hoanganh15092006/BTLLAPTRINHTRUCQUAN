using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using QLBanMayViTinh.Classes;

namespace QLBanMayViTinh.Forms
{
    public partial class frmTimKiem : Form
    {
        private DataTable tblKetQua;

        public frmTimKiem()
        {
            InitializeComponent();
        }

        private void frmTimKiem_Load(object sender, EventArgs e)
        {
            if (this.DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                return;

            UIThemeHelper.StyleCard(pnlTopCard);
            UIThemeHelper.StyleCard(pnlFilterCard);
            UIThemeHelper.StyleCard(pnlActions);
            UIThemeHelper.StyleCard(pnlGridCard);

            UIThemeHelper.StyleDataGridView(dgvKetQua);
            UIThemeHelper.StyleButton(btnTimKiem, UIThemeHelper.BtnTimColor, Color.White);
            UIThemeHelper.StyleButton(btnLamMoi, UIThemeHelper.BtnHuyColor, Color.White);
            UIThemeHelper.StyleButton(btnDong, Color.FromArgb(100, 116, 139), Color.White);

            LoadCombos();
            rdoMayViTinh.Checked = true;
            HienThiDieuKien();
        }

        private void LoadCombos()
        {
            Functions.FillCombo("SELECT MaLoai, TenLoai FROM LoaiMay", cboLoai, "MaLoai", "TenLoai");
            Functions.FillCombo("SELECT MaHangSX, TenHangSX FROM HangSanXuat", cboHangSX, "MaHangSX", "TenHangSX");
            Functions.FillCombo("SELECT MaNV, TenNV FROM NhanVien", cboNhanVien, "MaNV", "TenNV");
            Functions.FillCombo("SELECT MaKhach, TenKhach FROM KhachHang", cboKhachHang, "MaKhach", "TenKhach");
            Functions.FillCombo("SELECT MaNCC, TenNCC FROM NhaCungCap", cboNCC, "MaNCC", "TenNCC");
            Functions.FillCombo("SELECT MaMayVT, TenMayVT FROM MayViTinh", cboMatHang, "MaMayVT", "TenMayVT");
        }

        private void HienThiDieuKien()
        {
            if (rdoMayViTinh.Checked)
            {

                pnlMayTinh.Visible = true;
                pnlHoaDon.Visible = false;
                lblTieuDe.Text = "TÌM KIẾM SẢN PHẨM MÁY VI TÍNH (YÊU CẦU 4)";
            }
            else
            {

                pnlMayTinh.Visible = false;
                pnlHoaDon.Visible = true;
                lblTieuDe.Text = rdoHoaDonNhap.Checked
                    ? "TÌM KIẾM HÓA ĐƠN NHẬP HÀNG (YÊU CẦU 5)"
                    : "TÌM KIẾM HÓA ĐƠN BÁN HÀNG (YÊU CẦU 5)";

                lblDoiTac.Text = rdoHoaDonNhap.Checked ? "Nhà Cung Cấp:" : "Khách Hàng:";
                cboNCC.Visible = rdoHoaDonNhap.Checked;
                cboKhachHang.Visible = !rdoHoaDonNhap.Checked;
            }
        }

        private void rdoMayViTinh_CheckedChanged(object sender, EventArgs e)
        {
            HienThiDieuKien();
        }

        private void rdoHoaDonNhap_CheckedChanged(object sender, EventArgs e)
        {
            HienThiDieuKien();
        }

        private void rdoHoaDonBan_CheckedChanged(object sender, EventArgs e)
        {
            HienThiDieuKien();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            if (rdoMayViTinh.Checked)
            {

                string sql = @"SELECT m.MaMayVT, m.TenMayVT, l.TenLoai, h.TenHangSX, c.TenChip, r.TenRAM,
                                      m.SoLuong, m.GiaNhap, m.GiaBan, m.ThoiGianBH
                               FROM MayViTinh m
                               LEFT JOIN LoaiMay l ON m.MaLoai = l.MaLoai
                               LEFT JOIN HangSanXuat h ON m.MaHangSX = h.MaHangSX
                               LEFT JOIN Chip c ON m.MaChip = c.MaChip
                               LEFT JOIN RAM r ON m.MaRAM = r.MaRAM
                               WHERE 1=1";

                if (!string.IsNullOrWhiteSpace(txtMaMay.Text))
                    sql += " AND m.MaMayVT LIKE N'%" + txtMaMay.Text.Trim().Replace("'", "''") + "%'";

                if (!string.IsNullOrWhiteSpace(txtTenMay.Text))
                    sql += " AND m.TenMayVT LIKE N'%" + txtTenMay.Text.Trim().Replace("'", "''") + "%'";

                if (cboLoai.SelectedIndex >= 0 && cboLoai.SelectedValue != null)
                    sql += " AND m.MaLoai = N'" + cboLoai.SelectedValue.ToString() + "'";

                if (cboHangSX.SelectedIndex >= 0 && cboHangSX.SelectedValue != null)
                    sql += " AND m.MaHangSX = N'" + cboHangSX.SelectedValue.ToString() + "'";

                tblKetQua = Functions.GetDataToTable(sql);
                dgvKetQua.DataSource = tblKetQua;

                dgvKetQua.Columns["MaMayVT"].HeaderText = "Mã Máy";
                dgvKetQua.Columns["TenMayVT"].HeaderText = "Tên Máy Vi Tính";
                dgvKetQua.Columns["TenLoai"].HeaderText = "Loại Máy";
                dgvKetQua.Columns["TenHangSX"].HeaderText = "Hãng SX";
                dgvKetQua.Columns["TenChip"].HeaderText = "Chip (CPU)";
                dgvKetQua.Columns["TenRAM"].HeaderText = "RAM";
                dgvKetQua.Columns["SoLuong"].HeaderText = "Tồn Kho";
                dgvKetQua.Columns["GiaNhap"].HeaderText = "Giá Nhập";
                dgvKetQua.Columns["GiaBan"].HeaderText = "Giá Bán";
                dgvKetQua.Columns["ThoiGianBH"].HeaderText = "BH (Tháng)";

                UIThemeHelper.FormatCurrencyColumns(dgvKetQua, "GiaNhap", "GiaBan");
            }
            else if (rdoHoaDonNhap.Checked)
            {
                string sql = @"SELECT DISTINCT h.MaHDN, h.NgayNhap, nv.TenNV, ncc.TenNCC, h.TongTien
                               FROM HoaDonNhap h
                               INNER JOIN NhanVien nv ON h.MaNV = nv.MaNV
                               INNER JOIN NhaCungCap ncc ON h.MaNCC = ncc.MaNCC
                               LEFT JOIN ChiTietHDN ct ON h.MaHDN = ct.MaHDN
                               WHERE 1=1";

                if (!string.IsNullOrWhiteSpace(txtMaHD.Text))
                    sql += " AND h.MaHDN LIKE N'%" + txtMaHD.Text.Trim().Replace("'", "''") + "%'";

                if (cboMatHang.SelectedIndex >= 0 && cboMatHang.SelectedValue != null)
                    sql += " AND ct.MaMayVT = N'" + cboMatHang.SelectedValue.ToString() + "'";

                if (cboNhanVien.SelectedIndex >= 0 && cboNhanVien.SelectedValue != null)
                    sql += " AND h.MaNV = N'" + cboNhanVien.SelectedValue.ToString() + "'";

                if (cboNCC.SelectedIndex >= 0 && cboNCC.SelectedValue != null)
                    sql += " AND h.MaNCC = N'" + cboNCC.SelectedValue.ToString() + "'";

                if (chkNgay.Checked)
                {
                    sql += string.Format(" AND h.NgayNhap BETWEEN '{0:yyyy-MM-dd 00:00:00}' AND '{1:yyyy-MM-dd 23:59:59}'",
                        dtpTuNgay.Value, dtpDenNgay.Value);
                }

                sql += " ORDER BY h.NgayNhap DESC";
                tblKetQua = Functions.GetDataToTable(sql);
                dgvKetQua.DataSource = tblKetQua;

                dgvKetQua.Columns["MaHDN"].HeaderText = "Mã HĐ Nhập";
                dgvKetQua.Columns["NgayNhap"].HeaderText = "Ngày Nhập";
                dgvKetQua.Columns["TenNV"].HeaderText = "Nhân Viên Nhập";
                dgvKetQua.Columns["TenNCC"].HeaderText = "Nhà Cung Cấp";
                dgvKetQua.Columns["TongTien"].HeaderText = "Tổng Tiền Nhập";

                UIThemeHelper.FormatCurrencyColumns(dgvKetQua, "TongTien");
            }
            else
            {
                string sql = @"SELECT DISTINCT h.MaHDB, h.NgayBan, nv.TenNV, kh.TenKhach, h.TongTien
                               FROM HoaDonBan h
                               INNER JOIN NhanVien nv ON h.MaNV = nv.MaNV
                               INNER JOIN KhachHang kh ON h.MaKhach = kh.MaKhach
                               LEFT JOIN ChiTietHDB ct ON h.MaHDB = ct.MaHDB
                               WHERE 1=1";

                if (!string.IsNullOrWhiteSpace(txtMaHD.Text))
                    sql += " AND h.MaHDB LIKE N'%" + txtMaHD.Text.Trim().Replace("'", "''") + "%'";

                if (cboMatHang.SelectedIndex >= 0 && cboMatHang.SelectedValue != null)
                    sql += " AND ct.MaMayVT = N'" + cboMatHang.SelectedValue.ToString() + "'";

                if (cboNhanVien.SelectedIndex >= 0 && cboNhanVien.SelectedValue != null)
                    sql += " AND h.MaNV = N'" + cboNhanVien.SelectedValue.ToString() + "'";

                if (cboKhachHang.SelectedIndex >= 0 && cboKhachHang.SelectedValue != null)
                    sql += " AND h.MaKhach = N'" + cboKhachHang.SelectedValue.ToString() + "'";

                if (chkNgay.Checked)
                {
                    sql += string.Format(" AND h.NgayBan BETWEEN '{0:yyyy-MM-dd 00:00:00}' AND '{1:yyyy-MM-dd 23:59:59}'",
                        dtpTuNgay.Value, dtpDenNgay.Value);
                }

                sql += " ORDER BY h.NgayBan DESC";
                tblKetQua = Functions.GetDataToTable(sql);
                dgvKetQua.DataSource = tblKetQua;

                dgvKetQua.Columns["MaHDB"].HeaderText = "Mã HĐ Bán";
                dgvKetQua.Columns["NgayBan"].HeaderText = "Ngày Bán";
                dgvKetQua.Columns["TenNV"].HeaderText = "Nhân Viên Bán";
                dgvKetQua.Columns["TenKhach"].HeaderText = "Khách Hàng";
                dgvKetQua.Columns["TongTien"].HeaderText = "Tổng Tiền Bán";

                UIThemeHelper.FormatCurrencyColumns(dgvKetQua, "TongTien");
            }

            lblSoKetQua.Text = "Tìm thấy: " + tblKetQua.Rows.Count + " kết quả.";
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtMaMay.Text = "";
            txtTenMay.Text = "";
            cboLoai.SelectedIndex = -1;
            cboHangSX.SelectedIndex = -1;

            txtMaHD.Text = "";
            cboMatHang.SelectedIndex = -1;
            cboNhanVien.SelectedIndex = -1;
            cboNCC.SelectedIndex = -1;
            cboKhachHang.SelectedIndex = -1;
            chkNgay.Checked = false;
            dtpTuNgay.Value = DateTime.Now.AddMonths(-1);
            dtpDenNgay.Value = DateTime.Now;

            tblKetQua?.Clear();
            lblSoKetQua.Text = "Tìm thấy: 0 kết quả.";
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
