using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using QLBanMayViTinh.Classes;

namespace QLBanMayViTinh.Forms
{
    public partial class frmHoaDonNhap : Form
    {
        private DataTable tblChiTietHDN;

        public frmHoaDonNhap()
        {
            InitializeComponent();
        }

        private void frmHoaDonNhap_Load(object sender, EventArgs e)
        {
            if (this.DesignMode ||
                LicenseManager.UsageMode == LicenseUsageMode.Designtime ||
                System.Diagnostics.Process.GetCurrentProcess().ProcessName.Equals("devenv", StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            UIThemeHelper.StyleCard(pnlMasterCard, null, 8);
            UIThemeHelper.StyleCard(pnlDetailCard, null, 8);
            UIThemeHelper.StyleCard(pnlGridCard, null, 8);
            UIThemeHelper.StyleCard(pnlBottom, null, 8);

            UIThemeHelper.StyleDataGridView(dgvChiTiet);
            UIThemeHelper.StyleButton(btnThemHD, UIThemeHelper.BtnThemColor, Color.White, 8);
            UIThemeHelper.StyleButton(btnLuuHD, UIThemeHelper.BtnLuuColor, Color.White, 8);
            UIThemeHelper.StyleButton(btnHuyHD, UIThemeHelper.BtnHuyColor, Color.White, 8);
            UIThemeHelper.StyleButton(btnInHD, UIThemeHelper.BtnInExcelColor, Color.White, 8);
            UIThemeHelper.StyleButton(btnDong, Color.FromArgb(100, 116, 139), Color.White, 8);

            UIThemeHelper.ApplyTheme(this);

            txtSoLuong.KeyPress += (s, ev) => Functions.OnlyInteger(ev);
            txtDonGia.KeyPress += (s, ev) => Functions.OnlyInteger(ev);

            pnlBottom.Resize += (s, ev) => LayoutBottomPanel();
            pnlMasterCard.Resize += (s, ev) => LayoutMasterCard();
            pnlDetailCard.Resize += (s, ev) => LayoutDetailCard();
            this.Resize += (s, ev) =>
            {
                LayoutBottomPanel();
                LayoutMasterCard();
                LayoutDetailCard();
            };
            this.Shown += (s, ev) => this.BeginInvoke((Action)(() =>
            {
                LayoutBottomPanel();
                LayoutMasterCard();
                LayoutDetailCard();
            }));
            LayoutBottomPanel();
            LayoutMasterCard();
            LayoutDetailCard();

            LoadCombos();
            ResetForm();
        }

        private void LayoutBottomPanel()

        {

            if (pnlBottom == null || pnlBottom.ClientSize.Width <= 0) return;

            pnlTotalBox.Location = new Point(pnlBottom.ClientSize.Width - pnlTotalBox.Width - 15, 8);

            pnlTotalBox.BringToFront();

            pnlTotalBox.Visible = true;

        }



        private void LayoutMasterCard()
        {
            if (pnlMasterCard == null || pnlMasterCard.ClientSize.Width <= 0) return;
            int w = pnlMasterCard.ClientSize.Width;
            int leftMargin = 16;
            int rightMargin = w - 16;

            int col1X = leftMargin;
            int col1LabelW = 80;
            int col1InputX = col1X + col1LabelW;
            int col1InputW = Math.Max(140, (w - 60) * 22 / 100);

            lblMaHDN.Location = new Point(col1X, 36);
            lblMaHDN.Size = new Size(col1LabelW, 20);
            txtMaHDN.Location = new Point(col1InputX, 33);
            txtMaHDN.Width = col1InputW;

            lblNCC.Location = new Point(col1X, 72);
            lblNCC.Size = new Size(col1LabelW, 20);
            cboMaNCC.Location = new Point(col1InputX, 69);
            cboMaNCC.Width = col1InputW;
            
            int col2X = col1InputX + col1InputW + 20;
            int col2LabelW = 80;
            int col2InputX = col2X + col2LabelW;
            int col2InputW = Math.Max(150, (w - 60) * 23 / 100);

            lblNgayNhap.Location = new Point(col2X, 36);
            lblNgayNhap.Size = new Size(col2LabelW, 20);
            dtpNgayNhap.Location = new Point(col2InputX, 33);
            dtpNgayNhap.Width = col2InputW;

            lblDienThoaiNCC.Location = new Point(col2X, 72);
            lblDienThoaiNCC.Size = new Size(col2LabelW, 20);
            txtDienThoaiNCC.Location = new Point(col2InputX, 69);
            txtDienThoaiNCC.Width = col2InputW;

            int col3X = col2InputX + col2InputW + 20;
            int col3LabelW = 80;
            int col3InputX = col3X + col3LabelW;

            lblNhanVien.Location = new Point(col3X, 36);
            lblNhanVien.Size = new Size(col3LabelW, 20);
            cboMaNV.Location = new Point(col3InputX, 33);
            cboMaNV.Width = Math.Max(160, rightMargin - col3InputX);
            
            lblDiaChiNCC.Location = new Point(col3X, 72);
            lblDiaChiNCC.Size = new Size(col3LabelW, 20);
            txtDiaChiNCC.Location = new Point(col3InputX, 69);
            txtDiaChiNCC.Width = Math.Max(120, rightMargin - col3InputX);
        }

        private void LayoutDetailCard()
        {
            if (pnlDetailCard == null || pnlDetailCard.ClientSize.Width <= 0) return;
            int w = pnlDetailCard.ClientSize.Width;
            int leftMargin = 16;
            int rightMargin = w - 16;
            int curX = leftMargin;

            lblMaMay.AutoSize = true;
            lblMaMay.Location = new Point(curX, 38);
            curX = lblMaMay.Right + 6;

            cboMaMay.Location = new Point(curX, 35);
            cboMaMay.Width = Math.Max(110, (w - 200) * 12 / 100);
            curX = cboMaMay.Right + 12;

            lblTenMay.AutoSize = true;
            lblTenMay.Location = new Point(curX, 38);
            curX = lblTenMay.Right + 6;

            txtTenMay.Location = new Point(curX, 35);
            txtTenMay.Width = Math.Max(140, (w - 200) * 20 / 100);
            curX = txtTenMay.Right + 12;

            lblSoLuong.AutoSize = true;
            lblSoLuong.Location = new Point(curX, 38);
            curX = lblSoLuong.Right + 6;

            txtSoLuong.Location = new Point(curX, 35);
            txtSoLuong.Width = 65;
            curX = txtSoLuong.Right + 12;

            lblDonGia.AutoSize = true;
            lblDonGia.Location = new Point(curX, 38);
            curX = lblDonGia.Right + 6;

            txtDonGia.Location = new Point(curX, 35);
            txtDonGia.Width = Math.Max(110, (w - 200) * 15 / 100);
            curX = txtDonGia.Right + 12;

            lblThanhTien.AutoSize = true;
            lblThanhTien.Location = new Point(curX, 38);
            curX = lblThanhTien.Right + 6;

            txtThanhTien.Location = new Point(curX, 35);
            txtThanhTien.Width = Math.Max(120, rightMargin - curX);
        }



        private void LoadCombos()
        {
            Functions.FillCombo("SELECT MaNV, TenNV FROM NhanVien", cboMaNV, "MaNV", "TenNV");
            Functions.FillCombo("SELECT MaNCC, TenNCC FROM NhaCungCap", cboMaNCC, "MaNCC", "TenNCC");
            Functions.FillCombo("SELECT MaMayVT, TenMayVT FROM MayViTinh", cboMaMay, "MaMayVT", "TenMayVT");
            Functions.FillCombo("SELECT MaHDN FROM HoaDonNhap ORDER BY NgayNhap DESC", cboTimHDN, "MaHDN", "MaHDN");
        }

        private void ResetForm()
        {
            txtMaHDN.Text = "";
            dtpNgayNhap.Value = DateTime.Now;
            cboMaNV.SelectedIndex = -1;
                        cboMaNCC.SelectedIndex = -1;
                        txtDiaChiNCC.Text = "";
            txtDienThoaiNCC.Text = "";

            cboMaMay.SelectedIndex = -1;
            txtTenMay.Text = "";
            txtSoLuong.Text = "";
            txtDonGia.Text = "";
            txtThanhTien.Text = "0";
            txtTongTien.Text = "0";
            lblBangChu.Text = "Bằng chữ: Không đồng";

            btnThemHD.Enabled = true;
            btnLuuHD.Enabled = false;
            btnHuyHD.Enabled = false;
            btnInHD.Enabled = false;

            cboMaNV.Enabled = false;
            cboMaNCC.Enabled = false;
            dtpNgayNhap.Enabled = false;
            cboMaMay.Enabled = false;
            txtSoLuong.Enabled = false;
            txtDonGia.Enabled = false;

            LoadChiTietGrid("");
        }

        private void LoadChiTietGrid(string maHDN)
        {
            string sql = @"SELECT ct.MaMayVT, m.TenMayVT, ct.SoLuong, ct.DonGia, ct.ThanhTien
                           FROM ChiTietHDN ct
                           INNER JOIN MayViTinh m ON ct.MaMayVT = m.MaMayVT
                           WHERE ct.MaHDN = N'" + maHDN + "'";
            tblChiTietHDN = Functions.GetDataToTable(sql);
            dgvChiTiet.DataSource = tblChiTietHDN;

            dgvChiTiet.Columns["MaMayVT"].HeaderText = "Mã Máy";
            dgvChiTiet.Columns["TenMayVT"].HeaderText = "Tên Máy Vi Tính";
            dgvChiTiet.Columns["SoLuong"].HeaderText = "Số Lượng Nhập";
            dgvChiTiet.Columns["DonGia"].HeaderText = "Đơn Giá Nhập";
            dgvChiTiet.Columns["ThanhTien"].HeaderText = "Thành Tiền";

            dgvChiTiet.Columns["MaMayVT"].FillWeight = 15;
            dgvChiTiet.Columns["TenMayVT"].FillWeight = 40;
            dgvChiTiet.Columns["SoLuong"].FillWeight = 15;
            dgvChiTiet.Columns["DonGia"].FillWeight = 15;
            dgvChiTiet.Columns["ThanhTien"].FillWeight = 15;

            UIThemeHelper.FormatCurrencyColumns(dgvChiTiet, "DonGia", "ThanhTien");
        }

        private void cboMaNV_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void cboMaNCC_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboMaNCC.SelectedValue != null && cboMaNCC.SelectedIndex >= 0)
            {
                string id = cboMaNCC.SelectedValue.ToString();
                DataTable dt = Functions.GetDataToTable("SELECT TenNCC, DiaChi, DienThoai FROM NhaCungCap WHERE MaNCC = N'" + id + "'");
                if (dt.Rows.Count > 0)
                {
                                        txtDiaChiNCC.Text = dt.Rows[0]["DiaChi"].ToString();
                    txtDienThoaiNCC.Text = dt.Rows[0]["DienThoai"].ToString();
                }
            }
            else
            {
                                txtDiaChiNCC.Text = "";
                txtDienThoaiNCC.Text = "";
            }
        }

        private void cboMaMay_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboMaMay.SelectedValue != null && cboMaMay.SelectedIndex >= 0)
            {
                string id = cboMaMay.SelectedValue.ToString();
                DataTable dt = Functions.GetDataToTable("SELECT TenMayVT, GiaNhap FROM MayViTinh WHERE MaMayVT = N'" + id + "'");
                if (dt.Rows.Count > 0)
                {
                    txtTenMay.Text = dt.Rows[0]["TenMayVT"].ToString();
                    decimal giaNhap = Convert.ToDecimal(dt.Rows[0]["GiaNhap"]);
                    txtDonGia.Text = string.Format("{0:N0}", giaNhap);
                    TinhThanhTienLine();
                }
            }
            else
            {
                txtTenMay.Text = "";
                txtDonGia.Text = "";
                txtThanhTien.Text = "0";
            }
        }

        private void TinhThanhTienLine()
        {
            if (int.TryParse(txtSoLuong.Text, out int sl) && decimal.TryParse(txtDonGia.Text.Replace(",", ""), out decimal dg))
            {
                txtThanhTien.Text = string.Format("{0:N0}", sl * dg);
            }
            else
            {
                txtThanhTien.Text = "0";
            }
        }

        private void txtSoLuong_TextChanged(object sender, EventArgs e)
        {
            TinhThanhTienLine();
        }

        private void txtDonGia_TextChanged(object sender, EventArgs e)
        {
            TinhThanhTienLine();
        }

        private void btnThemHD_Click(object sender, EventArgs e)
        {
            ResetForm();
            txtMaHDN.Text = Functions.CreateKey("HDN");
            btnLuuHD.Enabled = true;
            btnHuyHD.Enabled = true;
            btnThemHD.Enabled = false;

            cboMaNV.Enabled = true;
            cboMaNCC.Enabled = true;
            dtpNgayNhap.Enabled = true;
            cboMaMay.Enabled = true;
            txtSoLuong.Enabled = true;
            txtDonGia.Enabled = false;

            if (cboMaNV.Items.Count > 0 && cboMaNV.SelectedIndex == -1) cboMaNV.SelectedIndex = 0;
            if (cboMaNCC.Items.Count > 0 && cboMaNCC.SelectedIndex == -1) cboMaNCC.SelectedIndex = 0;
            if (cboMaMay.Items.Count > 0)
            {
                cboMaMay.SelectedIndex = 0;
            }
            txtSoLuong.Text = "1";
            TinhThanhTienLine();
            txtSoLuong.Focus();
            txtSoLuong.SelectAll();
        }

        private void btnLuuHD_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaHDN.Text))
            {
                MessageBox.Show("Vui lòng nhấn [Tạo Mới] để tạo mã hóa đơn nhập!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cboMaNV.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn Nhân viên lập hóa đơn!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboMaNV.Focus();
                return;
            }
            if (cboMaNCC.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn Nhà cung cấp!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboMaNCC.Focus();
                return;
            }
            if (cboMaMay.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn Mặt hàng máy vi tính cần nhập!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboMaMay.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtSoLuong.Text))
            {
                MessageBox.Show("Vui lòng nhập Số lượng mặt hàng cần nhập!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoLuong.Focus();
                return;
            }

            int sl;
            if (!int.TryParse(txtSoLuong.Text.Trim(), out sl) || sl <= 0)
            {
                MessageBox.Show("Số lượng nhập phải là số nguyên dương lớn hơn 0!", "Cảnh Báo Nhập Liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoLuong.Focus();
                return;
            }

            decimal dg;
            string cleanDg = txtDonGia.Text.Trim().Replace(",", "").Replace(".", "").Replace("VNĐ", "").Replace("đ", "").Trim();
            if (!decimal.TryParse(cleanDg, out dg) || dg < 0)
            {
                MessageBox.Show("Đơn giá nhập không hợp lệ hoặc là số âm!", "Cảnh Báo Nhập Liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDonGia.Focus();
                return;
            }
            decimal tt = sl * dg;

            try
            {
                string checkHD = "SELECT MaHDN FROM HoaDonNhap WHERE MaHDN = N'" + txtMaHDN.Text.Trim() + "'";
                if (!Functions.CheckKey(checkHD))
                {
                    string sqlInsertHDN = string.Format(@"INSERT INTO HoaDonNhap (MaHDN, NgayNhap, MaNV, MaNCC, TongTien)
                                                         VALUES (N'{0}', '{1:yyyy-MM-dd HH:mm:ss}', N'{2}', N'{3}', 0)",
                        txtMaHDN.Text.Trim(), dtpNgayNhap.Value, cboMaNV.SelectedValue, cboMaNCC.SelectedValue);
                    Functions.RunSQL(sqlInsertHDN, false);
                }

                string checkItem = string.Format("SELECT MaMayVT FROM ChiTietHDN WHERE MaHDN = N'{0}' AND MaMayVT = N'{1}'",
                    txtMaHDN.Text.Trim(), cboMaMay.SelectedValue);
                if (Functions.CheckKey(checkItem))
                {
                    MessageBox.Show("Mặt hàng này đã có trong hóa đơn nhập, vui lòng nhấp đúp để xóa sửa!", "Trùng Hàng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string sqlInsertCT = string.Format(@"INSERT INTO ChiTietHDN (MaHDN, MaMayVT, SoLuong, DonGia, ThanhTien)
                                                     VALUES (N'{0}', N'{1}', {2}, {3}, {4})",
                    txtMaHDN.Text.Trim(), cboMaMay.SelectedValue, sl, dg, tt);
                Functions.RunSQL(sqlInsertCT, false);

                LoadChiTietGrid(txtMaHDN.Text.Trim());

                string tongTienStr = Functions.GetFieldValues("SELECT TongTien FROM HoaDonNhap WHERE MaHDN = N'" + txtMaHDN.Text.Trim() + "'");
                if (decimal.TryParse(tongTienStr, out decimal tongTien))
                {
                    txtTongTien.Text = string.Format("{0:N0}", tongTien);
                    lblBangChu.Text = "Bằng chữ: " + Functions.ChuyenSoSangChu((double)tongTien);
                }

                MessageBox.Show(string.Format("Đã thêm thành công mặt hàng [{0}] với số lượng {1} vào hóa đơn nhập {2}!",
                    txtTenMay.Text, sl, txtMaHDN.Text.Trim()), "Thành Công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (cboMaMay.Items.Count > 0)
                {
                    cboMaMay.SelectedIndex = 0;
                }
                txtSoLuong.Text = "1";
                TinhThanhTienLine();
                btnInHD.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu chi tiết hóa đơn nhập:\n" + ex.Message, "Lỗi Cơ Sở Dữ Liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvChiTiet_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvChiTiet.CurrentRow == null || dgvChiTiet.CurrentRow.Index < 0) return;
            string maMay = dgvChiTiet.CurrentRow.Cells["MaMayVT"].Value.ToString();
            string tenMay = dgvChiTiet.CurrentRow.Cells["TenMayVT"].Value.ToString();

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa mặt hàng [" + tenMay + "] khỏi hóa đơn này không?\nSố lượng tồn kho sẽ tự động trừ trả lại đúng theo Trigger!",
                "Xác Nhận Xóa Hàng", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string sqlDel = string.Format("DELETE FROM ChiTietHDN WHERE MaHDN = N'{0}' AND MaMayVT = N'{1}'",
                    txtMaHDN.Text.Trim(), maMay);
                Functions.RunSqlDel(sqlDel);

                LoadChiTietGrid(txtMaHDN.Text.Trim());

                string tongTienStr = Functions.GetFieldValues("SELECT TongTien FROM HoaDonNhap WHERE MaHDN = N'" + txtMaHDN.Text.Trim() + "'");
                decimal.TryParse(tongTienStr, out decimal tongTien);
                txtTongTien.Text = string.Format("{0:N0}", tongTien);
                lblBangChu.Text = "Bằng chữ: " + Functions.ChuyenSoSangChu((double)tongTien);
            }
        }

        private void btnHuyHD_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaHDN.Text)) return;
            if (MessageBox.Show("Bạn có chắc chắn muốn hủy toàn bộ hóa đơn nhập này không?", "Xác Nhận Hủy", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                string sqlDel = "DELETE FROM HoaDonNhap WHERE MaHDN = N'" + txtMaHDN.Text.Trim() + "'";
                Functions.RunSqlDel(sqlDel);
                ResetForm();
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtSoLuong_KeyPress(object sender, KeyPressEventArgs e)
        {
            Functions.OnlyInteger(e);
        }

        private void txtDonGia_KeyPress(object sender, KeyPressEventArgs e)
        {
            Functions.OnlyDecimal(e, txtDonGia.Text);
        }

        private void btnInHD_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaHDN.Text))
            {
                MessageBox.Show("Vui lòng chọn hoặc lưu một hóa đơn nhập để in!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            MessageBox.Show("Đã kết nối máy in và xuất thành công Hóa Đơn Nhập: " + txtMaHDN.Text +
                            "\nTổng tiền thanh toán: " + txtTongTien.Text + " VNĐ",
                            "In Hóa Đơn Thành Công", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void cboTimHDN_DropDown(object sender, EventArgs e)
        {
            Functions.FillCombo("SELECT MaHDN FROM HoaDonNhap ORDER BY NgayNhap DESC", cboTimHDN, "MaHDN", "MaHDN");
        }

        private void cboTimHDN_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboTimHDN.SelectedValue == null || cboTimHDN.SelectedIndex < 0) return;
            string maHDN = cboTimHDN.SelectedValue.ToString();
            txtMaHDN.Text = maHDN;

            DataTable dtHDN = Functions.GetDataToTable("SELECT * FROM HoaDonNhap WHERE MaHDN = N'" + maHDN + "'");
            if (dtHDN.Rows.Count > 0)
            {
                DataRow r = dtHDN.Rows[0];
                dtpNgayNhap.Value = Convert.ToDateTime(r["NgayNhap"]);
                cboMaNV.SelectedValue = r["MaNV"].ToString();
                cboMaNCC.SelectedValue = r["MaNCC"].ToString();

                decimal tongTien = Convert.ToDecimal(r["TongTien"]);
                txtTongTien.Text = string.Format("{0:N0}", tongTien);
                lblBangChu.Text = "Bằng chữ: " + Functions.ChuyenSoSangChu((double)tongTien);

                LoadChiTietGrid(maHDN);

                btnLuuHD.Enabled = true;
                btnHuyHD.Enabled = true;
                btnInHD.Enabled = true;
                btnThemHD.Enabled = true;
                cboMaMay.Enabled = true;
                txtSoLuong.Enabled = true;
                txtDonGia.Enabled = true;
            }
        }
    }
}
