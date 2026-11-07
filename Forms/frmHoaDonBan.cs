using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using QLBanMayViTinh.Classes;

namespace QLBanMayViTinh.Forms
{
    public partial class frmHoaDonBan : Form
    {
        private DataTable tblChiTietHDB;

        public frmHoaDonBan()
        {
            InitializeComponent();
        }

        private void frmHoaDonBan_Load(object sender, EventArgs e)
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

            lblMaHDB.Location = new Point(col1X, 36);
            lblMaHDB.Size = new Size(col1LabelW, 20);
            txtMaHDB.Location = new Point(col1InputX, 33);
            txtMaHDB.Width = col1InputW;

            lblKhachHang.Location = new Point(col1X, 72);
            lblKhachHang.Size = new Size(col1LabelW, 20);
            cboMaKhach.Location = new Point(col1InputX, 69);
            cboMaKhach.Width = col1InputW;
            
            int col2X = col1InputX + col1InputW + 20;
            int col2LabelW = 80;
            int col2InputX = col2X + col2LabelW;
            int col2InputW = Math.Max(150, (w - 60) * 23 / 100);

            lblNgayBan.Location = new Point(col2X, 36);
            lblNgayBan.Size = new Size(col2LabelW, 20);
            dtpNgayBan.Location = new Point(col2InputX, 33);
            dtpNgayBan.Width = col2InputW;

            lblDienThoaiKhach.Location = new Point(col2X, 72);
            lblDienThoaiKhach.Size = new Size(col2LabelW, 20);
            txtDienThoaiKhach.Location = new Point(col2InputX, 69);
            txtDienThoaiKhach.Width = col2InputW;

            int col3X = col2InputX + col2InputW + 20;
            int col3LabelW = 80;
            int col3InputX = col3X + col3LabelW;

            lblNhanVien.Location = new Point(col3X, 36);
            lblNhanVien.Size = new Size(col3LabelW, 20);
            cboMaNV.Location = new Point(col3InputX, 33);
            cboMaNV.Width = Math.Max(160, rightMargin - col3InputX);
            
            lblDiaChiKhach.Location = new Point(col3X, 72);
            lblDiaChiKhach.Size = new Size(col3LabelW, 20);
            txtDiaChiKhach.Location = new Point(col3InputX, 69);
            txtDiaChiKhach.Width = Math.Max(120, rightMargin - col3InputX);
        }

        private void LayoutDetailCard()
        {
            if (pnlDetailCard == null || pnlDetailCard.ClientSize.Width <= 0) return;
            int w = pnlDetailCard.ClientSize.Width;
            int leftMargin = 12;
            int rightMargin = w - 12;
            int curX = leftMargin;

            lblMaMay.AutoSize = true;
            lblMaMay.Location = new Point(curX, 38);
            curX = lblMaMay.Right + 5;

            cboMaMay.Location = new Point(curX, 35);
            cboMaMay.Width = Math.Max(105, (w - 200) * 11 / 100);
            curX = cboMaMay.Right + 12;

            lblTenMay.AutoSize = true;
            lblTenMay.Location = new Point(curX, 38);
            curX = lblTenMay.Right + 5;

            txtTenMay.Location = new Point(curX, 35);
            txtTenMay.Width = Math.Max(120, (w - 200) * 15 / 100);
            curX = txtTenMay.Right + 12;

            lblTonKho.AutoSize = true;
            lblTonKho.Location = new Point(curX, 38);
            curX = lblTonKho.Right + 5;

            txtTonKho.Location = new Point(curX, 35);
            txtTonKho.Width = 48;
            curX = txtTonKho.Right + 12;

            lblSoLuong.AutoSize = true;
            lblSoLuong.Location = new Point(curX, 38);
            curX = lblSoLuong.Right + 5;

            txtSoLuong.Location = new Point(curX, 35);
            txtSoLuong.Width = 48;
            curX = txtSoLuong.Right + 12;

            lblDonGia.AutoSize = true;
            lblDonGia.Location = new Point(curX, 38);
            curX = lblDonGia.Right + 5;

            txtDonGia.Location = new Point(curX, 35);
            txtDonGia.Width = Math.Max(95, (w - 200) * 12 / 100);
            curX = txtDonGia.Right + 12;

            lblThanhTien.AutoSize = true;
            lblThanhTien.Location = new Point(curX, 38);
            curX = lblThanhTien.Right + 5;

            txtThanhTien.Location = new Point(curX, 35);
            txtThanhTien.Width = Math.Max(110, rightMargin - curX);
        }



        private void LoadCombos()
        {
            Functions.FillCombo("SELECT MaNV, TenNV FROM NhanVien", cboMaNV, "MaNV", "TenNV");
            Functions.FillCombo("SELECT MaKhach, TenKhach FROM KhachHang", cboMaKhach, "MaKhach", "TenKhach");
            Functions.FillCombo("SELECT MaMayVT, TenMayVT FROM MayViTinh", cboMaMay, "MaMayVT", "TenMayVT");
            Functions.FillCombo("SELECT MaHDB FROM HoaDonBan ORDER BY NgayBan DESC", cboTimHDB, "MaHDB", "MaHDB");
        }

        private void ResetForm()
        {
            txtMaHDB.Text = "";
            dtpNgayBan.Value = DateTime.Now;
            cboMaNV.SelectedIndex = -1;
                        cboMaKhach.SelectedIndex = -1;
                        txtDiaChiKhach.Text = "";
            txtDienThoaiKhach.Text = "";

            cboMaMay.SelectedIndex = -1;
            txtTenMay.Text = "";
            txtTonKho.Text = "0";
            txtSoLuong.Text = "";
            txtDonGia.Text = "0";
            txtThanhTien.Text = "0";
            txtTongTien.Text = "0";
            lblBangChu.Text = "Bằng chữ: Không đồng";

            btnThemHD.Enabled = true;
            btnLuuHD.Enabled = false;
            btnHuyHD.Enabled = false;
            btnInHD.Enabled = false;

            cboMaNV.Enabled = false;
            cboMaKhach.Enabled = false;
            dtpNgayBan.Enabled = false;
            cboMaNV.Enabled = false;
            cboMaKhach.Enabled = false;
            dtpNgayBan.Enabled = false;
            cboMaMay.Enabled = false;
            txtSoLuong.Enabled = false;

            LoadChiTietGrid("");
        }

        private void LoadChiTietGrid(string maHDB)
        {
            string sql = @"SELECT ct.MaMayVT, m.TenMayVT, ct.SoLuong, ct.DonGia, ct.ThanhTien
                           FROM ChiTietHDB ct
                           INNER JOIN MayViTinh m ON ct.MaMayVT = m.MaMayVT
                           WHERE ct.MaHDB = N'" + maHDB + "'";
            tblChiTietHDB = Functions.GetDataToTable(sql);
            dgvChiTiet.DataSource = tblChiTietHDB;

            dgvChiTiet.Columns["MaMayVT"].HeaderText = "Mã Máy";
            dgvChiTiet.Columns["TenMayVT"].HeaderText = "Tên Máy Vi Tính";
            dgvChiTiet.Columns["SoLuong"].HeaderText = "Số Lượng Bán";
            dgvChiTiet.Columns["DonGia"].HeaderText = "Đơn Giá Bán";
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

        private void cboMaKhach_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboMaKhach.SelectedValue != null && cboMaKhach.SelectedIndex >= 0)
            {
                string id = cboMaKhach.SelectedValue.ToString();
                DataTable dt = Functions.GetDataToTable("SELECT TenKhach, DiaChi, DienThoai FROM KhachHang WHERE MaKhach = N'" + id + "'");
                if (dt.Rows.Count > 0)
                {
                                        txtDiaChiKhach.Text = dt.Rows[0]["DiaChi"].ToString();
                    txtDienThoaiKhach.Text = dt.Rows[0]["DienThoai"].ToString();
                }
            }
            else
            {
                                txtDiaChiKhach.Text = "";
                txtDienThoaiKhach.Text = "";
            }
        }

        private void cboMaMay_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboMaMay.SelectedValue != null && cboMaMay.SelectedIndex >= 0)
            {
                string id = cboMaMay.SelectedValue.ToString();
                DataTable dt = Functions.GetDataToTable("SELECT TenMayVT, SoLuong, GiaBan FROM MayViTinh WHERE MaMayVT = N'" + id + "'");
                if (dt.Rows.Count > 0)
                {
                    txtTenMay.Text = dt.Rows[0]["TenMayVT"].ToString();
                    txtTonKho.Text = dt.Rows[0]["SoLuong"].ToString();
                    decimal giaBan = Convert.ToDecimal(dt.Rows[0]["GiaBan"]);
                    txtDonGia.Text = string.Format("{0:N0}", giaBan);
                    txtSoLuong_TextChanged(null, null);
                }
            }
            else
            {
                txtTenMay.Text = "";
                txtTonKho.Text = "0";
                txtDonGia.Text = "0";
            }
        }

        private void txtSoLuong_TextChanged(object sender, EventArgs e)
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

        private void btnThemHD_Click(object sender, EventArgs e)
        {
            ResetForm();
            txtMaHDB.Text = Functions.CreateKey("HDB");
            btnLuuHD.Enabled = true;
            btnHuyHD.Enabled = true;
            btnThemHD.Enabled = false;

            cboMaNV.Enabled = true;
            cboMaKhach.Enabled = true;
            dtpNgayBan.Enabled = true;
            cboMaMay.Enabled = true;
            txtSoLuong.Enabled = true;
            cboMaNV.Focus();
        }

        private void btnLuuHD_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaHDB.Text))
            {
                MessageBox.Show("Vui lòng nhấn [Thêm Hóa Đơn] để tạo mã hóa đơn bán!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cboMaNV.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn Nhân viên bán hàng!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboMaNV.Focus();
                return;
            }
            if (cboMaKhach.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn Khách hàng!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboMaKhach.Focus();
                return;
            }
            if (cboMaMay.SelectedIndex == -1 || string.IsNullOrWhiteSpace(txtSoLuong.Text))
            {
                MessageBox.Show("Vui lòng chọn Mặt hàng và nhập Số lượng bán!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int slBan;
            if (!int.TryParse(txtSoLuong.Text.Trim(), out slBan) || slBan <= 0)
            {
                MessageBox.Show("Số lượng bán phải là số nguyên dương lớn hơn 0!", "Cảnh Báo Nhập Liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoLuong.Focus();
                return;
            }

            int tonKho;
            if (!int.TryParse(txtTonKho.Text.Trim(), out tonKho)) tonKho = 0;

            if (slBan > tonKho)
            {
                MessageBox.Show(string.Format("Sản phẩm [{0}] chỉ còn tồn {1} cái trong kho, không đủ xuất {2} cái!",
                                txtTenMay.Text, tonKho, slBan), "Cảnh Báo Hết Hàng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoLuong.Focus();
                return;
            }

            decimal dg;
            string cleanDg = txtDonGia.Text.Trim().Replace(",", "").Replace(".", "").Replace("VNĐ", "").Replace("đ", "").Trim();
            if (!decimal.TryParse(cleanDg, out dg) || dg < 0)
            {
                MessageBox.Show("Đơn giá bán không hợp lệ hoặc là số âm!", "Cảnh Báo Nhập Liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDonGia.Focus();
                return;
            }
            decimal tt = slBan * dg;

            try
            {
                string checkHDB = "SELECT MaHDB FROM HoaDonBan WHERE MaHDB = N'" + txtMaHDB.Text.Trim() + "'";
                if (!Functions.CheckKey(checkHDB))
                {
                    string sqlInsertHDB = string.Format(@"INSERT INTO HoaDonBan (MaHDB, NgayBan, MaNV, MaKhach, TongTien)
                                                         VALUES (N'{0}', '{1:yyyy-MM-dd HH:mm:ss}', N'{2}', N'{3}', 0)",
                        txtMaHDB.Text.Trim(), dtpNgayBan.Value, cboMaNV.SelectedValue, cboMaKhach.SelectedValue);
                    Functions.RunSQL(sqlInsertHDB, false);
                }

                string checkItem = string.Format("SELECT MaMayVT FROM ChiTietHDB WHERE MaHDB = N'{0}' AND MaMayVT = N'{1}'",
                    txtMaHDB.Text.Trim(), cboMaMay.SelectedValue);
                if (Functions.CheckKey(checkItem))
                {
                    MessageBox.Show("Mặt hàng này đã có trong giỏ hàng hóa đơn này, vui lòng nhấp đúp để xóa sửa!", "Trùng Hàng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string sqlInsertCT = string.Format(@"INSERT INTO ChiTietHDB (MaHDB, MaMayVT, SoLuong, DonGia, ThanhTien)
                                                     VALUES (N'{0}', N'{1}', {2}, {3}, {4})",
                    txtMaHDB.Text.Trim(), cboMaMay.SelectedValue, slBan, dg, tt);
                Functions.RunSQL(sqlInsertCT, false);

                LoadChiTietGrid(txtMaHDB.Text.Trim());

                string tongTienStr = Functions.GetFieldValues("SELECT TongTien FROM HoaDonBan WHERE MaHDB = N'" + txtMaHDB.Text.Trim() + "'");
                if (decimal.TryParse(tongTienStr, out decimal tongTien))
                {
                    txtTongTien.Text = string.Format("{0:N0}", tongTien);
                    lblBangChu.Text = "Bằng chữ: " + Functions.ChuyenSoSangChu((double)tongTien);
                }

                txtTonKho.Text = (tonKho - slBan).ToString();

                cboMaMay.SelectedIndex = -1;
                txtTenMay.Text = "";
                txtSoLuong.Text = "";
                txtDonGia.Text = "";
                txtThanhTien.Text = "0";
                btnInHD.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu chi tiết hóa đơn bán:\n" + ex.Message, "Lỗi Cơ Sở Dữ Liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            txtSoLuong.Text = "";
            txtThanhTien.Text = "0";
            btnInHD.Enabled = true;
        }

        private void dgvChiTiet_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvChiTiet.CurrentRow == null || dgvChiTiet.CurrentRow.Index < 0) return;
            string maMay = dgvChiTiet.CurrentRow.Cells["MaMayVT"].Value.ToString();
            string tenMay = dgvChiTiet.CurrentRow.Cells["TenMayVT"].Value.ToString();

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa mặt hàng [" + tenMay + "] khỏi hóa đơn bán này không?\nSố lượng bán sẽ được hoàn trả lại tồn kho tự động bởi Trigger!",
                "Xác Nhận Hủy Món", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string sqlDel = string.Format("DELETE FROM ChiTietHDB WHERE MaHDB = N'{0}' AND MaMayVT = N'{1}'",
                    txtMaHDB.Text.Trim(), maMay);
                Functions.RunSqlDel(sqlDel);

                LoadChiTietGrid(txtMaHDB.Text.Trim());

                string tongTienStr = Functions.GetFieldValues("SELECT TongTien FROM HoaDonBan WHERE MaHDB = N'" + txtMaHDB.Text.Trim() + "'");
                decimal.TryParse(tongTienStr, out decimal tongTien);
                txtTongTien.Text = string.Format("{0:N0}", tongTien);
                lblBangChu.Text = "Bằng chữ: " + Functions.ChuyenSoSangChu((double)tongTien);
            }
        }

        private void btnHuyHD_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaHDB.Text)) return;
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa/hủy toàn bộ hóa đơn bán này không?", "Xác Nhận Hủy", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                string sqlDel = "DELETE FROM HoaDonBan WHERE MaHDB = N'" + txtMaHDB.Text.Trim() + "'";
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

        private void btnInHD_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaHDB.Text))
            {
                MessageBox.Show("Vui lòng chọn hoặc lập một hóa đơn bán để in!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            MessageBox.Show("Đã kết nối máy in nhiệt POS và xuất Hóa Đơn Bán Lẻ: " + txtMaHDB.Text +
                            "\nKhách hàng: " + (cboMaKhach.Text ?? "") +
                            "\nTổng tiền thanh toán: " + txtTongTien.Text + " VNĐ",
                            "In Hóa Đơn Bán Lẻ Thành Công", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void cboTimHDB_DropDown(object sender, EventArgs e)
        {
            Functions.FillCombo("SELECT MaHDB FROM HoaDonBan ORDER BY NgayBan DESC", cboTimHDB, "MaHDB", "MaHDB");
        }

        private void cboTimHDB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboTimHDB.SelectedValue == null || cboTimHDB.SelectedIndex < 0) return;
            string maHDB = cboTimHDB.SelectedValue.ToString();
            txtMaHDB.Text = maHDB;

            DataTable dtHDB = Functions.GetDataToTable("SELECT * FROM HoaDonBan WHERE MaHDB = N'" + maHDB + "'");
            if (dtHDB.Rows.Count > 0)
            {
                DataRow r = dtHDB.Rows[0];
                dtpNgayBan.Value = Convert.ToDateTime(r["NgayBan"]);
                cboMaNV.SelectedValue = r["MaNV"].ToString();
                cboMaKhach.SelectedValue = r["MaKhach"].ToString();

                decimal tongTien = Convert.ToDecimal(r["TongTien"]);
                txtTongTien.Text = string.Format("{0:N0}", tongTien);
                lblBangChu.Text = "Bằng chữ: " + Functions.ChuyenSoSangChu((double)tongTien);

                LoadChiTietGrid(maHDB);

                btnLuuHD.Enabled = true;
                btnHuyHD.Enabled = true;
                btnInHD.Enabled = true;
                btnThemHD.Enabled = true;
                cboMaMay.Enabled = true;
                txtSoLuong.Enabled = true;
            }
        }

        private void dgvChiTiet_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
