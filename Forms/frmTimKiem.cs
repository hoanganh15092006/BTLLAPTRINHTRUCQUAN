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
            if (this.DesignMode ||
                LicenseManager.UsageMode == LicenseUsageMode.Designtime ||
                System.Diagnostics.Process.GetCurrentProcess().ProcessName.Equals("devenv", StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            UIThemeHelper.StyleCard(pnlTopCard, null, 8);
            UIThemeHelper.StyleCard(pnlFilterCard, null, 8);
            UIThemeHelper.StyleCard(pnlActions, null, 8);
            UIThemeHelper.StyleCard(pnlGridCard, null, 8);

            UIThemeHelper.StyleDataGridView(dgvKetQua);
            UIThemeHelper.StyleButton(btnTimKiem, UIThemeHelper.BtnTimColor, Color.White, 8);
            UIThemeHelper.StyleButton(btnLamMoi, UIThemeHelper.BtnHuyColor, Color.White, 8);
            UIThemeHelper.StyleButton(btnDong, Color.FromArgb(100, 116, 139), Color.White, 8);

            UIThemeHelper.ApplyTheme(this);

            pnlRadioSwitch.BorderStyle = BorderStyle.None;
            pnlRadioSwitch.BackColor = Color.Transparent;
            pnlMayTinh.BorderStyle = BorderStyle.None;
            pnlMayTinh.BackColor = Color.Transparent;
            pnlHoaDon.BorderStyle = BorderStyle.None;
            pnlHoaDon.BackColor = Color.Transparent;

            if (!this.TopLevel)
            {
                lblTieuDe.Visible = false;
                lblSubTieuDe.Visible = false;
                pnlTopCard.Height = 55;
            }

            LoadCombos();
            rdoMayViTinh.Checked = true;
            HienThiDieuKien();

            this.Resize += (s, ev) => LayoutDynamicControls();
            this.Shown += (s, ev) => this.BeginInvoke((Action)(() => LayoutDynamicControls()));
            LayoutDynamicControls();
        }

        private void LayoutDynamicControls()
        {
            if (pnlTopCard != null && pnlRadioSwitch != null && pnlTopCard.ClientSize.Width > 0)
            {
                pnlRadioSwitch.Width = 560;
                pnlRadioSwitch.Location = new Point(Math.Max(300, pnlTopCard.ClientSize.Width - pnlRadioSwitch.Width - 15), 6);

                rdoMayViTinh.AutoSize = true;
                rdoMayViTinh.Location = new Point(10, 10);

                rdoHoaDonNhap.AutoSize = true;
                rdoHoaDonNhap.Location = new Point(rdoMayViTinh.Right + 12, 10);

                rdoHoaDonBan.AutoSize = true;
                rdoHoaDonBan.Location = new Point(rdoHoaDonNhap.Right + 12, 10);
            }

            if (pnlHoaDon != null && pnlHoaDon.ClientSize.Width > 0)
            {
                int curX = 14;
                lblMaHD.AutoSize = true;
                lblMaHD.Location = new Point(curX, 12);
                curX = lblMaHD.Right + 5;

                txtMaHD.Location = new Point(curX, 9);
                txtMaHD.Width = 110;
                curX = txtMaHD.Right + 16;

                lblMatHang.AutoSize = true;
                lblMatHang.Location = new Point(curX, 12);
                curX = lblMatHang.Right + 5;

                cboMatHang.Location = new Point(curX, 9);
                cboMatHang.Width = 160;
                curX = cboMatHang.Right + 16;

                lblNhanVien.AutoSize = true;
                lblNhanVien.Location = new Point(curX, 12);
                curX = lblNhanVien.Right + 5;

                cboNhanVien.Location = new Point(curX, 9);
                cboNhanVien.Width = 150;
                curX = cboNhanVien.Right + 16;

                lblDoiTac.AutoSize = true;
                lblDoiTac.Location = new Point(curX, 12);
                curX = lblDoiTac.Right + 6;

                cboDoiTac.Location = new Point(curX, 9);
                int remW = pnlHoaDon.ClientSize.Width - curX - 15;
                cboDoiTac.Width = Math.Max(140, remW);

                chkNgay.AutoSize = true;
                chkNgay.Location = new Point(14, 48);
                dtpTuNgay.Location = new Point(chkNgay.Right + 6, 46);
                dtpTuNgay.Width = 125;

                lblDenNgay.AutoSize = true;
                lblDenNgay.Location = new Point(dtpTuNgay.Right + 16, 48);
                dtpDenNgay.Location = new Point(lblDenNgay.Right + 6, 46);
                dtpDenNgay.Width = 125;
            }

            if (pnlMayTinh != null && pnlMayTinh.ClientSize.Width > 0)
            {
                int curX = 14;
                lblMaMay.AutoSize = true;
                lblMaMay.Location = new Point(curX, 20);
                curX = lblMaMay.Right + 5;

                txtMaMay.Location = new Point(curX, 18);
                txtMaMay.Width = 130;
                curX = txtMaMay.Right + 16;

                lblTenMay.AutoSize = true;
                lblTenMay.Location = new Point(curX, 20);
                curX = lblTenMay.Right + 5;

                txtTenMay.Location = new Point(curX, 18);
                txtTenMay.Width = 170;
                curX = txtTenMay.Right + 16;

                lblLoai.AutoSize = true;
                lblLoai.Location = new Point(curX, 20);
                curX = lblLoai.Right + 5;

                cboLoai.Location = new Point(curX, 18);
                cboLoai.Width = 160;
                curX = cboLoai.Right + 16;

                lblHangSX.AutoSize = true;
                lblHangSX.Location = new Point(curX, 20);
                curX = lblHangSX.Right + 5;

                cboHangSX.Location = new Point(curX, 18);
                int remW = pnlMayTinh.ClientSize.Width - curX - 15;
                cboHangSX.Width = Math.Max(150, remW);
            }
        }

        private void LoadCombos()
        {
            Functions.FillCombo("SELECT MaLoai, TenLoai FROM LoaiMay", cboLoai, "MaLoai", "TenLoai");
            Functions.FillCombo("SELECT MaHangSX, TenHangSX FROM HangSanXuat", cboHangSX, "MaHangSX", "TenHangSX");
            Functions.FillCombo("SELECT MaNV, TenNV FROM NhanVien", cboNhanVien, "MaNV", "TenNV");
            Functions.FillCombo("SELECT MaMayVT, TenMayVT FROM MayViTinh", cboMatHang, "MaMayVT", "TenMayVT");
        }

        private void HienThiDieuKien()
        {
            if (rdoMayViTinh.Checked)
            {
                pnlMayTinh.Visible = true;
                pnlHoaDon.Visible = false;
                pnlFilterCard.Height = 80;
                lblTieuDe.Text = "TÌM KIẾM SẢN PHẨM MÁY VI TÍNH";
            }
            else
            {
                pnlMayTinh.Visible = false;
                pnlHoaDon.Visible = true;
                pnlFilterCard.Height = 98;
                if (rdoHoaDonNhap.Checked)
                {
                    lblTieuDe.Text = "TÌM KIẾM HÓA ĐƠN NHẬP HÀNG";
                    lblDoiTac.Text = "Nhà Cung Cấp:";
                    Functions.FillCombo("SELECT MaNCC, TenNCC FROM NhaCungCap", cboDoiTac, "MaNCC", "TenNCC");
                }
                else
                {
                    lblTieuDe.Text = "TÌM KIẾM HÓA ĐƠN BÁN HÀNG";
                    lblDoiTac.Text = "Khách Hàng:";
                    Functions.FillCombo("SELECT MaKhach, TenKhach FROM KhachHang", cboDoiTac, "MaKhach", "TenKhach");
                }
                cboDoiTac.SelectedIndex = -1;
            }
            LayoutDynamicControls();
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
                dgvKetQua.Columns["SoLuong"].HeaderText = "Số Lượng Tồn (Chiếc)";
                dgvKetQua.Columns["GiaNhap"].HeaderText = "Giá Nhập";
                dgvKetQua.Columns["GiaBan"].HeaderText = "Giá Bán";
                dgvKetQua.Columns["ThoiGianBH"].HeaderText = "BH (Tháng)";

                UIThemeHelper.FormatCurrencyColumns(dgvKetQua, "GiaNhap", "GiaBan");

                int tongTon = 0;
                foreach (DataRow r in tblKetQua.Rows)
                {
                    tongTon += Convert.ToInt32(r["SoLuong"]);
                }
                lblSoKetQua.Text = string.Format("Tìm thấy: {0} sản phẩm | Tổng tồn kho: {1:N0} chiếc", tblKetQua.Rows.Count, tongTon);
            }
            else if (rdoHoaDonNhap.Checked)
            {
                string sql = @"SELECT h.MaHDN, h.NgayNhap, nv.TenNV, ncc.TenNCC, m.TenMayVT, ct.SoLuong, ct.DonGia, ct.ThanhTien
                               FROM HoaDonNhap h
                               INNER JOIN NhanVien nv ON h.MaNV = nv.MaNV
                               INNER JOIN NhaCungCap ncc ON h.MaNCC = ncc.MaNCC
                               INNER JOIN ChiTietHDN ct ON h.MaHDN = ct.MaHDN
                               INNER JOIN MayViTinh m ON ct.MaMayVT = m.MaMayVT
                               WHERE 1=1";

                if (!string.IsNullOrWhiteSpace(txtMaHD.Text))
                    sql += " AND h.MaHDN LIKE N'%" + txtMaHD.Text.Trim().Replace("'", "''") + "%'";

                if (cboMatHang.SelectedIndex >= 0 && cboMatHang.SelectedValue != null)
                    sql += " AND ct.MaMayVT = N'" + cboMatHang.SelectedValue.ToString() + "'";

                if (cboNhanVien.SelectedIndex >= 0 && cboNhanVien.SelectedValue != null)
                    sql += " AND h.MaNV = N'" + cboNhanVien.SelectedValue.ToString() + "'";

                if (cboDoiTac.SelectedIndex >= 0 && cboDoiTac.SelectedValue != null)
                    sql += " AND h.MaNCC = N'" + cboDoiTac.SelectedValue.ToString() + "'";

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
                dgvKetQua.Columns["TenMayVT"].HeaderText = "Tên Máy Vi Tính";
                dgvKetQua.Columns["SoLuong"].HeaderText = "Số Lượng (Chiếc)";
                dgvKetQua.Columns["DonGia"].HeaderText = "Đơn Giá Nhập";
                dgvKetQua.Columns["ThanhTien"].HeaderText = "Thành Tiền";

                UIThemeHelper.FormatCurrencyColumns(dgvKetQua, "DonGia", "ThanhTien");

                int tongMay = 0;
                decimal tongTien = 0;
                foreach (DataRow r in tblKetQua.Rows)
                {
                    tongMay += Convert.ToInt32(r["SoLuong"]);
                    tongTien += Convert.ToDecimal(r["ThanhTien"]);
                }
                lblSoKetQua.Text = string.Format("Tìm thấy: {0} dòng | Tổng máy nhập: {1:N0} chiếc | Tổng tiền: {2:N0} VNĐ", tblKetQua.Rows.Count, tongMay, tongTien);
            }
            else
            {
                string sql = @"SELECT h.MaHDB, h.NgayBan, nv.TenNV, kh.TenKhach, m.TenMayVT, ct.SoLuong, ct.DonGia, ct.ThanhTien
                               FROM HoaDonBan h
                               INNER JOIN NhanVien nv ON h.MaNV = nv.MaNV
                               INNER JOIN KhachHang kh ON h.MaKhach = kh.MaKhach
                               INNER JOIN ChiTietHDB ct ON h.MaHDB = ct.MaHDB
                               INNER JOIN MayViTinh m ON ct.MaMayVT = m.MaMayVT
                               WHERE 1=1";

                if (!string.IsNullOrWhiteSpace(txtMaHD.Text))
                    sql += " AND h.MaHDB LIKE N'%" + txtMaHD.Text.Trim().Replace("'", "''") + "%'";

                if (cboMatHang.SelectedIndex >= 0 && cboMatHang.SelectedValue != null)
                    sql += " AND ct.MaMayVT = N'" + cboMatHang.SelectedValue.ToString() + "'";

                if (cboNhanVien.SelectedIndex >= 0 && cboNhanVien.SelectedValue != null)
                    sql += " AND h.MaNV = N'" + cboNhanVien.SelectedValue.ToString() + "'";

                if (cboDoiTac.SelectedIndex >= 0 && cboDoiTac.SelectedValue != null)
                    sql += " AND h.MaKhach = N'" + cboDoiTac.SelectedValue.ToString() + "'";

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
                dgvKetQua.Columns["TenMayVT"].HeaderText = "Tên Máy Vi Tính";
                dgvKetQua.Columns["SoLuong"].HeaderText = "Số Lượng (Chiếc)";
                dgvKetQua.Columns["DonGia"].HeaderText = "Đơn Giá Bán";
                dgvKetQua.Columns["ThanhTien"].HeaderText = "Thành Tiền";

                UIThemeHelper.FormatCurrencyColumns(dgvKetQua, "DonGia", "ThanhTien");

                int tongMay = 0;
                decimal tongTien = 0;
                foreach (DataRow r in tblKetQua.Rows)
                {
                    tongMay += Convert.ToInt32(r["SoLuong"]);
                    tongTien += Convert.ToDecimal(r["ThanhTien"]);
                }
                lblSoKetQua.Text = string.Format("Tìm thấy: {0} dòng | Tổng máy bán: {1:N0} chiếc | Tổng tiền: {2:N0} VNĐ", tblKetQua.Rows.Count, tongMay, tongTien);
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
            cboDoiTac.SelectedIndex = -1;
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
