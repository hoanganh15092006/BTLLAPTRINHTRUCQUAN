using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using QLBanMayViTinh.Classes;

namespace QLBanMayViTinh.Forms
{
    public partial class frmBaoCaoThongKe : Form
    {
        private DataTable tblBaoCao;

        public frmBaoCaoThongKe()
        {
            InitializeComponent();
        }

        private void frmBaoCaoThongKe_Load(object sender, EventArgs e)
        {
            if (this.DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                return;

            UIThemeHelper.StyleCard(card1, Color.FromArgb(79, 70, 229));
            UIThemeHelper.StyleCard(card2, Color.FromArgb(2, 132, 199));
            UIThemeHelper.StyleCard(card3, Color.FromArgb(16, 185, 129));
            UIThemeHelper.StyleCard(card4, Color.FromArgb(245, 158, 11));
            UIThemeHelper.StyleCard(pnlFilterCard);
            UIThemeHelper.StyleCard(pnlGridCard);
            UIThemeHelper.StyleCard(pnlBottom);

            UIThemeHelper.StyleDataGridView(dgvBaoCao);
            UIThemeHelper.StyleButton(btnXemBaoCao, UIThemeHelper.BtnTimColor, Color.White);
            UIThemeHelper.StyleButton(btnXuatExcel, UIThemeHelper.BtnInExcelColor, Color.White);
            UIThemeHelper.StyleButton(btnDong, Color.FromArgb(100, 116, 139), Color.White);

            for (int i = 1; i <= 12; i++) cboThang.Items.Add(i.ToString());
            cboThang.SelectedIndex = DateTime.Now.Month - 1;

            cboQuy.Items.AddRange(new string[] { "Quý 1 (Tháng 1-3)", "Quý 2 (Tháng 4-6)", "Quý 3 (Tháng 7-9)", "Quý 4 (Tháng 10-12)" });
            cboQuy.SelectedIndex = 0;

            int currentYear = DateTime.Now.Year;
            for (int y = currentYear - 3; y <= currentYear + 1; y++) cboNam.Items.Add(y.ToString());
            cboNam.SelectedItem = currentYear.ToString();

            rdoTonKho.Checked = true;
            HienThiDieuKienLoc();
            CapNhatSummaryCards();
            btnXemBaoCao.PerformClick();
        }

        private void CapNhatSummaryCards()
        {

            string valTon = Functions.GetFieldValues("SELECT ISNULL(SUM(SoLuong * GiaNhap), 0) FROM MayViTinh");
            decimal.TryParse(valTon, out decimal giaTriTon);
            lblCardGiaTriTon.Text = string.Format("{0:N0} VNĐ", giaTriTon);

            string valNhap = Functions.GetFieldValues("SELECT ISNULL(SUM(TongTien), 0) FROM HoaDonNhap");
            decimal.TryParse(valNhap, out decimal tongNhap);
            lblCardTongNhap.Text = string.Format("{0:N0} VNĐ", tongNhap);

            string valBan = Functions.GetFieldValues("SELECT ISNULL(SUM(TongTien), 0) FROM HoaDonBan");
            decimal.TryParse(valBan, out decimal tongBan);
            lblCardTongBan.Text = string.Format("{0:N0} VNĐ", tongBan);

            decimal loiNhuan = tongBan - tongNhap;
            lblCardLoiNhuan.Text = string.Format("{0:N0} VNĐ", loiNhuan);
            lblCardLoiNhuan.ForeColor = loiNhuan >= 0 ? Color.FromArgb(16, 185, 129) : Color.FromArgb(239, 68, 68);
        }

        private void HienThiDieuKienLoc()
        {
            if (rdoTonKho.Checked)
            {
                pnlThoiGian.Visible = false;
                lblTieuDeBaoCao.Text = "BÁO CÁO DANH SÁCH MÁY VI TÍNH TỒN KHO (YÊU CẦU 6)";
            }
            else
            {
                pnlThoiGian.Visible = true;
                if (rdoNhapHang.Checked)
                    lblTieuDeBaoCao.Text = "BÁO CÁO DOANH THU NHẬP HÀNG THEO THỜI GIAN (YÊU CẦU 7)";
                else if (rdoBanHang.Checked)
                    lblTieuDeBaoCao.Text = "BÁO CÁO DOANH THU BÁN HÀNG THEO THỜI GIAN (YÊU CẦU 8)";
                else
                    lblTieuDeBaoCao.Text = "TOP 5 NHÂN VIÊN DOANH THU CAO NHẤT (YÊU CẦU 9)";
            }
        }

        private void rdoTonKho_CheckedChanged(object sender, EventArgs e)
        {
            HienThiDieuKienLoc();
        }

        private void rdoNhapHang_CheckedChanged(object sender, EventArgs e)
        {
            HienThiDieuKienLoc();
        }

        private void rdoBanHang_CheckedChanged(object sender, EventArgs e)
        {
            HienThiDieuKienLoc();
        }

        private void rdoTop5NV_CheckedChanged(object sender, EventArgs e)
        {
            HienThiDieuKienLoc();
        }

        private void rdoLocThoiGian_CheckedChanged(object sender, EventArgs e)
        {
            cboThang.Enabled = rdoLocThang.Checked;
            cboQuy.Enabled = rdoLocQuy.Checked;
            cboNam.Enabled = true;
        }

        private void btnXemBaoCao_Click(object sender, EventArgs e)
        {
            int nam = int.Parse(cboNam.SelectedItem.ToString());
            int thang = int.Parse(cboThang.SelectedItem.ToString());
            int quy = cboQuy.SelectedIndex + 1;

            if (rdoTonKho.Checked)
            {

                string sql = @"SELECT m.MaMayVT, m.TenMayVT, l.TenLoai, h.TenHangSX, m.SoLuong,
                                      m.GiaNhap, m.GiaBan, (m.SoLuong * m.GiaNhap) AS TongGiaTriTon
                               FROM MayViTinh m
                               LEFT JOIN LoaiMay l ON m.MaLoai = l.MaLoai
                               LEFT JOIN HangSanXuat h ON m.MaHangSX = h.MaHangSX
                               WHERE m.SoLuong >= 0
                               ORDER BY m.SoLuong DESC";
                tblBaoCao = Functions.GetDataToTable(sql);
                dgvBaoCao.DataSource = tblBaoCao;

                dgvBaoCao.Columns["MaMayVT"].HeaderText = "Mã Máy";
                dgvBaoCao.Columns["TenMayVT"].HeaderText = "Tên Máy Vi Tính";
                dgvBaoCao.Columns["TenLoai"].HeaderText = "Loại Máy";
                dgvBaoCao.Columns["TenHangSX"].HeaderText = "Hãng SX";
                dgvBaoCao.Columns["SoLuong"].HeaderText = "Số Lượng Tồn";
                dgvBaoCao.Columns["GiaNhap"].HeaderText = "Giá Nhập";
                dgvBaoCao.Columns["GiaBan"].HeaderText = "Giá Bán";
                dgvBaoCao.Columns["TongGiaTriTon"].HeaderText = "Tổng Trị Giá Tồn";

                UIThemeHelper.FormatCurrencyColumns(dgvBaoCao, "GiaNhap", "GiaBan", "TongGiaTriTon");

                decimal tongKho = 0;
                foreach (DataRow row in tblBaoCao.Rows) tongKho += Convert.ToDecimal(row["TongGiaTriTon"]);
                lblTongKet.Text = string.Format("Tổng mặt hàng: {0} | Tổng giá trị tồn: {1:N0} VNĐ", tblBaoCao.Rows.Count, tongKho);
            }
            else if (rdoNhapHang.Checked)
            {

                string whereTime = "";
                if (rdoLocThang.Checked)
                    whereTime = string.Format("MONTH(h.NgayNhap) = {0} AND YEAR(h.NgayNhap) = {1}", thang, nam);
                else if (rdoLocQuy.Checked)
                    whereTime = string.Format("DATEPART(QUARTER, h.NgayNhap) = {0} AND YEAR(h.NgayNhap) = {1}", quy, nam);
                else
                    whereTime = string.Format("YEAR(h.NgayNhap) = {0}", nam);

                string sql = string.Format(@"SELECT h.MaHDN, h.NgayNhap, nv.TenNV, ncc.TenNCC, h.TongTien
                                            FROM HoaDonNhap h
                                            INNER JOIN NhanVien nv ON h.MaNV = nv.MaNV
                                            INNER JOIN NhaCungCap ncc ON h.MaNCC = ncc.MaNCC
                                            WHERE {0}
                                            ORDER BY h.NgayNhap DESC", whereTime);
                tblBaoCao = Functions.GetDataToTable(sql);
                dgvBaoCao.DataSource = tblBaoCao;

                dgvBaoCao.Columns["MaHDN"].HeaderText = "Mã HĐ Nhập";
                dgvBaoCao.Columns["NgayNhap"].HeaderText = "Ngày Nhập";
                dgvBaoCao.Columns["TenNV"].HeaderText = "Nhân Viên Thực Hiện";
                dgvBaoCao.Columns["TenNCC"].HeaderText = "Nhà Cung Cấp";
                dgvBaoCao.Columns["TongTien"].HeaderText = "Tổng Tiền Nhập";

                UIThemeHelper.FormatCurrencyColumns(dgvBaoCao, "TongTien");

                decimal tongTien = 0;
                foreach (DataRow row in tblBaoCao.Rows) tongTien += Convert.ToDecimal(row["TongTien"]);
                lblTongKet.Text = string.Format("Tổng số hóa đơn nhập: {0} | Tổng tiền nhập: {1:N0} VNĐ", tblBaoCao.Rows.Count, tongTien);
            }
            else if (rdoBanHang.Checked)
            {

                string whereTime = "";
                if (rdoLocThang.Checked)
                    whereTime = string.Format("MONTH(h.NgayBan) = {0} AND YEAR(h.NgayBan) = {1}", thang, nam);
                else if (rdoLocQuy.Checked)
                    whereTime = string.Format("DATEPART(QUARTER, h.NgayBan) = {0} AND YEAR(h.NgayBan) = {1}", quy, nam);
                else
                    whereTime = string.Format("YEAR(h.NgayBan) = {0}", nam);

                string sql = string.Format(@"SELECT h.MaHDB, h.NgayBan, nv.TenNV, kh.TenKhach, h.TongTien
                                            FROM HoaDonBan h
                                            INNER JOIN NhanVien nv ON h.MaNV = nv.MaNV
                                            INNER JOIN KhachHang kh ON h.MaKhach = kh.MaKhach
                                            WHERE {0}
                                            ORDER BY h.NgayBan DESC", whereTime);
                tblBaoCao = Functions.GetDataToTable(sql);
                dgvBaoCao.DataSource = tblBaoCao;

                dgvBaoCao.Columns["MaHDB"].HeaderText = "Mã HĐ Bán";
                dgvBaoCao.Columns["NgayBan"].HeaderText = "Ngày Bán";
                dgvBaoCao.Columns["TenNV"].HeaderText = "Nhân Viên Bán";
                dgvBaoCao.Columns["TenKhach"].HeaderText = "Khách Hàng";
                dgvBaoCao.Columns["TongTien"].HeaderText = "Tổng Tiền Bán";

                UIThemeHelper.FormatCurrencyColumns(dgvBaoCao, "TongTien");

                decimal tongTien = 0;
                foreach (DataRow row in tblBaoCao.Rows) tongTien += Convert.ToDecimal(row["TongTien"]);
                lblTongKet.Text = string.Format("Tổng số hóa đơn bán: {0} | Tổng tiền bán: {1:N0} VNĐ", tblBaoCao.Rows.Count, tongTien);
            }
            else
            {

                string whereTime = "";
                if (rdoLocThang.Checked)
                    whereTime = string.Format("MONTH(h.NgayBan) = {0} AND YEAR(h.NgayBan) = {1}", thang, nam);
                else if (rdoLocQuy.Checked)
                    whereTime = string.Format("DATEPART(QUARTER, h.NgayBan) = {0} AND YEAR(h.NgayBan) = {1}", quy, nam);
                else
                    whereTime = string.Format("YEAR(h.NgayBan) = {0}", nam);

                string sql = string.Format(@"SELECT TOP 5 WITH TIES nv.MaNV, nv.TenNV, nv.DienThoai,
                                                    COUNT(h.MaHDB) AS SoHoaDonDaBan,
                                                    ISNULL(SUM(h.TongTien), 0) AS TongTienBan
                                            FROM NhanVien nv
                                            INNER JOIN HoaDonBan h ON nv.MaNV = h.MaNV
                                            WHERE {0}
                                            GROUP BY nv.MaNV, nv.TenNV, nv.DienThoai
                                            ORDER BY TongTienBan DESC", whereTime);
                tblBaoCao = Functions.GetDataToTable(sql);
                dgvBaoCao.DataSource = tblBaoCao;

                dgvBaoCao.Columns["MaNV"].HeaderText = "Mã NV";
                dgvBaoCao.Columns["TenNV"].HeaderText = "Họ Và Tên Nhân Viên";
                dgvBaoCao.Columns["DienThoai"].HeaderText = "Số Điện Thoại";
                dgvBaoCao.Columns["SoHoaDonDaBan"].HeaderText = "Số Lượng Đơn Bán";
                dgvBaoCao.Columns["TongTienBan"].HeaderText = "Tổng Doanh Thu Đạt Được";

                UIThemeHelper.FormatCurrencyColumns(dgvBaoCao, "TongTienBan");

                decimal tongTien = 0;
                foreach (DataRow row in tblBaoCao.Rows) tongTien += Convert.ToDecimal(row["TongTienBan"]);
                lblTongKet.Text = string.Format("Tổng doanh thu Top 5 nhân viên: {0:N0} VNĐ", tongTien);
            }

            CapNhatSummaryCards();
        }

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            if (tblBaoCao == null || tblBaoCao.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu báo cáo để xuất Excel!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Excel Files (*.xls)|*.xls";
                sfd.FileName = "BaoCao_" + DateTime.Now.ToString("ddMMyyyy_HHmmss") + ".xls";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.AppendLine("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />");
                        sb.AppendLine("<table border='1' style='font-family:Arial; font-size:11pt; border-collapse:collapse;'>");

                        sb.AppendLine(string.Format("<tr><th colspan='{0}' style='font-size:16pt; color:#1E293B; background-color:#E2E8F0; padding:10px;'>{1}</th></tr>",
                            dgvBaoCao.Columns.Count, lblTieuDeBaoCao.Text));
                        sb.AppendLine(string.Format("<tr><td colspan='{0}' style='font-style:italic; padding:5px;'>Thời gian xuất báo cáo: {1}</td></tr>",
                            dgvBaoCao.Columns.Count, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")));

                        sb.AppendLine("<tr style='background-color:#1E293B; color:#FFFFFF; font-weight:bold; height:30px;'>");
                        foreach (DataGridViewColumn col in dgvBaoCao.Columns)
                        {
                            if (col.Visible)
                                sb.AppendLine(string.Format("<th style='padding:6px; border:1px solid #CBD5E1;'>{0}</th>", col.HeaderText));
                        }
                        sb.AppendLine("</tr>");

                        foreach (DataGridViewRow row in dgvBaoCao.Rows)
                        {
                            sb.AppendLine("<tr>");
                            foreach (DataGridViewColumn col in dgvBaoCao.Columns)
                            {
                                if (col.Visible)
                                {
                                    object val = row.Cells[col.Name].Value;
                                    string text = val != null ? val.ToString() : "";
                                    if (val is decimal || val is double || val is int || val is long)
                                    {
                                        text = string.Format("{0:N0}", val);
                                        sb.AppendLine(string.Format("<td style='text-align:right; padding:5px; border:1px solid #CBD5E1;'>{0}</td>", text));
                                    }
                                    else
                                    {
                                        sb.AppendLine(string.Format("<td style='padding:5px; border:1px solid #CBD5E1;'>{0}</td>", text));
                                    }
                                }
                            }
                            sb.AppendLine("</tr>");
                        }

                        sb.AppendLine(string.Format("<tr style='font-weight:bold; background-color:#F1F5F9;'><td colspan='{0}' style='padding:8px;'>{1}</td></tr>",
                            dgvBaoCao.Columns.Count, lblTongKet.Text));

                        sb.AppendLine("</table>");

                        File.WriteAllText(sfd.FileName, sb.ToString(), Encoding.UTF8);
                        MessageBox.Show("Xuất báo cáo ra Excel thành công tại:\n" + sfd.FileName, "Xuất Excel Thành Công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        System.Diagnostics.Process.Start(sfd.FileName);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi xuất file Excel: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
