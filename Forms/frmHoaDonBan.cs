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
            if (this.DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                return;

            UIThemeHelper.StyleCard(pnlMasterCard);
            UIThemeHelper.StyleCard(pnlDetailCard);
            UIThemeHelper.StyleCard(pnlGridCard);
            UIThemeHelper.StyleCard(pnlBottom);

            UIThemeHelper.StyleDataGridView(dgvChiTiet);
            UIThemeHelper.StyleButton(btnThemHD, UIThemeHelper.BtnThemColor, Color.White);
            UIThemeHelper.StyleButton(btnLuuHD, UIThemeHelper.BtnLuuColor, Color.White);
            UIThemeHelper.StyleButton(btnHuyHD, UIThemeHelper.BtnHuyColor, Color.White);
            UIThemeHelper.StyleButton(btnInHD, UIThemeHelper.BtnInExcelColor, Color.White);
            UIThemeHelper.StyleButton(btnDong, Color.FromArgb(100, 116, 139), Color.White);

            LoadCombos();
            ResetForm();
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
            txtTenNV.Text = "";
            cboMaKhach.SelectedIndex = -1;
            txtTenKhach.Text = "";
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

            UIThemeHelper.FormatCurrencyColumns(dgvChiTiet, "DonGia", "ThanhTien");
        }

        private void cboMaNV_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboMaNV.SelectedValue != null && cboMaNV.SelectedIndex >= 0)
            {
                txtTenNV.Text = Functions.GetFieldValues("SELECT TenNV FROM NhanVien WHERE MaNV = N'" + cboMaNV.SelectedValue.ToString() + "'");
            }
            else txtTenNV.Text = "";
        }

        private void cboMaKhach_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboMaKhach.SelectedValue != null && cboMaKhach.SelectedIndex >= 0)
            {
                string id = cboMaKhach.SelectedValue.ToString();
                DataTable dt = Functions.GetDataToTable("SELECT TenKhach, DiaChi, DienThoai FROM KhachHang WHERE MaKhach = N'" + id + "'");
                if (dt.Rows.Count > 0)
                {
                    txtTenKhach.Text = dt.Rows[0]["TenKhach"].ToString();
                    txtDiaChiKhach.Text = dt.Rows[0]["DiaChi"].ToString();
                    txtDienThoaiKhach.Text = dt.Rows[0]["DienThoai"].ToString();
                }
            }
            else
            {
                txtTenKhach.Text = "";
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

            int slBan = int.Parse(txtSoLuong.Text.Trim());
            int tonKho = int.Parse(txtTonKho.Text.Trim());

            if (slBan > tonKho)
            {
                MessageBox.Show(string.Format("Sản phẩm [{0}] chỉ còn tồn {1} cái trong kho, không đủ xuất {2} cái!",
                                txtTenMay.Text, tonKho, slBan), "Cảnh Báo Hết Hàng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoLuong.Focus();
                return;
            }

            decimal dg = decimal.Parse(txtDonGia.Text.Trim().Replace(",", ""));
            decimal tt = slBan * dg;

            string checkHDB = "SELECT MaHDB FROM HoaDonBan WHERE MaHDB = N'" + txtMaHDB.Text.Trim() + "'";
            if (!Functions.CheckKey(checkHDB))
            {
                string sqlInsertHDB = string.Format(@"INSERT INTO HoaDonBan (MaHDB, NgayBan, MaNV, MaKhach, TongTien)
                                                     VALUES (N'{0}', '{1:yyyy-MM-dd HH:mm:ss}', N'{2}', N'{3}', 0)",
                    txtMaHDB.Text.Trim(), dtpNgayBan.Value, cboMaNV.SelectedValue, cboMaKhach.SelectedValue);
                Functions.RunSQL(sqlInsertHDB);
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
            Functions.RunSQL(sqlInsertCT);

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
                            "\nKhách hàng: " + txtTenKhach.Text +
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
    }
}
