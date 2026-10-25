using System;
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
            Functions.FillCombo("SELECT MaNCC, TenNCC FROM NhaCungCap", cboMaNCC, "MaNCC", "TenNCC");
            Functions.FillCombo("SELECT MaMayVT, TenMayVT FROM MayViTinh", cboMaMay, "MaMayVT", "TenMayVT");
            Functions.FillCombo("SELECT MaHDN FROM HoaDonNhap ORDER BY NgayNhap DESC", cboTimHDN, "MaHDN", "MaHDN");
        }

        private void ResetForm()
        {
            txtMaHDN.Text = "";
            dtpNgayNhap.Value = DateTime.Now;
            cboMaNV.SelectedIndex = -1;
            txtTenNV.Text = "";
            cboMaNCC.SelectedIndex = -1;
            txtTenNCC.Text = "";
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

        private void cboMaNCC_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboMaNCC.SelectedValue != null && cboMaNCC.SelectedIndex >= 0)
            {
                string id = cboMaNCC.SelectedValue.ToString();
                DataTable dt = Functions.GetDataToTable("SELECT TenNCC, DiaChi, DienThoai FROM NhaCungCap WHERE MaNCC = N'" + id + "'");
                if (dt.Rows.Count > 0)
                {
                    txtTenNCC.Text = dt.Rows[0]["TenNCC"].ToString();
                    txtDiaChiNCC.Text = dt.Rows[0]["DiaChi"].ToString();
                    txtDienThoaiNCC.Text = dt.Rows[0]["DienThoai"].ToString();
                }
            }
            else
            {
                txtTenNCC.Text = "";
                txtDiaChiNCC.Text = "";
                txtDienThoaiNCC.Text = "";
            }
        }

        private void cboMaMay_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboMaMay.SelectedValue != null && cboMaMay.SelectedIndex >= 0)
            {
                txtTenMay.Text = Functions.GetFieldValues("SELECT TenMayVT FROM MayViTinh WHERE MaMayVT = N'" + cboMaMay.SelectedValue.ToString() + "'");
            }
            else txtTenMay.Text = "";
        }

        private void TinhThanhTienLine()
        {
            if (int.TryParse(txtSoLuong.Text, out int sl) && decimal.TryParse(txtDonGia.Text, out decimal dg))
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

            cboMaMay.Enabled = true;
            txtSoLuong.Enabled = true;
            txtDonGia.Enabled = true;
            cboMaNV.Focus();
        }

        private void btnLuuHD_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaHDN.Text))
            {
                MessageBox.Show("Vui lòng nhấn [Thêm Hóa Đơn] để tạo mã hóa đơn nhập!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            if (cboMaMay.SelectedIndex == -1 || string.IsNullOrWhiteSpace(txtSoLuong.Text) || string.IsNullOrWhiteSpace(txtDonGia.Text))
            {
                MessageBox.Show("Vui lòng chọn Mặt hàng, nhập Số lượng và Đơn giá nhập!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int sl = int.Parse(txtSoLuong.Text.Trim());
            decimal dg = decimal.Parse(txtDonGia.Text.Trim().Replace(",", ""));
            decimal tt = sl * dg;

            // 1. Kiểm tra nếu Hóa đơn nhập chưa tồn tại thì INSERT vào HoaDonNhap
            string checkHD = "SELECT MaHDN FROM HoaDonNhap WHERE MaHDN = N'" + txtMaHDN.Text.Trim() + "'";
            if (!Functions.CheckKey(checkHD))
            {
                string sqlInsertHDN = string.Format(@"INSERT INTO HoaDonNhap (MaHDN, NgayNhap, MaNV, MaNCC, TongTien) 
                                                     VALUES (N'{0}', '{1:yyyy-MM-dd HH:mm:ss}', N'{2}', N'{3}', 0)",
                    txtMaHDN.Text.Trim(), dtpNgayNhap.Value, cboMaNV.SelectedValue, cboMaNCC.SelectedValue);
                Functions.RunSQL(sqlInsertHDN);
            }

            // 2. Kiểm tra mặt hàng đã có trong chi tiết chưa
            string checkItem = string.Format("SELECT MaMayVT FROM ChiTietHDN WHERE MaHDN = N'{0}' AND MaMayVT = N'{1}'",
                txtMaHDN.Text.Trim(), cboMaMay.SelectedValue);
            if (Functions.CheckKey(checkItem))
            {
                MessageBox.Show("Mặt hàng này đã có trong hóa đơn nhập, vui lòng nhấp đúp để xóa sửa!", "Trùng Hàng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 3. Thêm vào ChiTietHDN (Trigger SQL sẽ tự cộng tồn kho, cập nhật giá nhập và giá bán = 110% giá nhập)
            string sqlInsertCT = string.Format(@"INSERT INTO ChiTietHDN (MaHDN, MaMayVT, SoLuong, DonGia, ThanhTien) 
                                                 VALUES (N'{0}', N'{1}', {2}, {3}, {4})",
                txtMaHDN.Text.Trim(), cboMaMay.SelectedValue, sl, dg, tt);
            Functions.RunSQL(sqlInsertCT);

            // Cập nhật lại giao diện
            LoadChiTietGrid(txtMaHDN.Text.Trim());

            // Đọc lại tổng tiền do Trigger tính tự động
            string tongTienStr = Functions.GetFieldValues("SELECT TongTien FROM HoaDonNhap WHERE MaHDN = N'" + txtMaHDN.Text.Trim() + "'");
            if (decimal.TryParse(tongTienStr, out decimal tongTien))
            {
                txtTongTien.Text = string.Format("{0:N0}", tongTien);
                lblBangChu.Text = "Bằng chữ: " + Functions.ChuyenSoSangChu((double)tongTien);
            }

            // Xóa ô nhập dòng hàng để nhập tiếp mặt hàng khác
            cboMaMay.SelectedIndex = -1;
            txtTenMay.Text = "";
            txtSoLuong.Text = "";
            txtDonGia.Text = "";
            txtThanhTien.Text = "0";
            btnInHD.Enabled = true;
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
    }
}
