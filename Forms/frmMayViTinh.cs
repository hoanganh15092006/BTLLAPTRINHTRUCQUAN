using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using QLBanMayViTinh.Classes;

namespace QLBanMayViTinh.Forms
{
    public partial class frmMayViTinh : Form
    {
        private DataTable tblMayViTinh;

        public frmMayViTinh()
        {
            InitializeComponent();
        }

        private void frmMayViTinh_Load(object sender, EventArgs e)
        {
            UIThemeHelper.StyleDataGridView(dgvMayViTinh);
            UIThemeHelper.StyleButton(btnThem, UIThemeHelper.BtnThemColor, Color.White);
            UIThemeHelper.StyleButton(btnSua, UIThemeHelper.BtnSuaColor, Color.White);
            UIThemeHelper.StyleButton(btnXoa, UIThemeHelper.BtnXoaColor, Color.White);
            UIThemeHelper.StyleButton(btnLuu, UIThemeHelper.BtnLuuColor, Color.White);
            UIThemeHelper.StyleButton(btnHuy, UIThemeHelper.BtnHuyColor, Color.White);
            UIThemeHelper.StyleButton(btnChonAnh, Color.FromArgb(79, 70, 229), Color.White);
            UIThemeHelper.StyleButton(btnDong, Color.FromArgb(100, 116, 139), Color.White);

            LoadComboBoxes();
            ResetValues();
            LoadDataGridView();
        }

        private void LoadComboBoxes()
        {
            Functions.FillCombo("SELECT MaLoai, TenLoai FROM LoaiMay", cboMaLoai, "MaLoai", "TenLoai");
            Functions.FillCombo("SELECT MaChip, TenChip FROM Chip", cboMaChip, "MaChip", "TenChip");
            Functions.FillCombo("SELECT MaOCung, TenOCung FROM OCung", cboMaOCung, "MaOCung", "TenOCung");
            Functions.FillCombo("SELECT MaDungLuong, TenDungLuong FROM DungLuong", cboMaDungLuong, "MaDungLuong", "TenDungLuong");
            Functions.FillCombo("SELECT MaTocDo, TenTocDo FROM TocDo", cboMaTocDo, "MaTocDo", "TenTocDo");
            Functions.FillCombo("SELECT MaOCD, TenOCD FROM OCD", cboMaOCD, "MaOCD", "TenOCD");
            Functions.FillCombo("SELECT MaManHinh, TenManHinh FROM ManHinh", cboMaManHinh, "MaManHinh", "TenManHinh");
            Functions.FillCombo("SELECT MaCoMH, TenCoMH FROM CoMH", cboMaCoMH, "MaCoMH", "TenCoMH");
            Functions.FillCombo("SELECT MaChuot, TenChuot FROM Chuot", cboMaChuot, "MaChuot", "TenChuot");
            Functions.FillCombo("SELECT MaBanPhim, TenBanPhim FROM BanPhim", cboMaBanPhim, "MaBanPhim", "TenBanPhim");
            Functions.FillCombo("SELECT MaUSB, TenUSB FROM USB", cboMaUSB, "MaUSB", "TenUSB");
            Functions.FillCombo("SELECT MaRAM, TenRAM FROM RAM", cboMaRAM, "MaRAM", "TenRAM");
            Functions.FillCombo("SELECT MaLoa, TenLoa FROM Loa", cboMaLoa, "MaLoa", "TenLoa");
            Functions.FillCombo("SELECT MaHangSX, TenHangSX FROM HangSanXuat", cboMaHangSX, "MaHangSX", "TenHangSX");
        }

        private void LoadDataGridView()
        {
            string sql = @"SELECT m.MaMayVT, m.TenMayVT, l.TenLoai, h.TenHangSX, c.TenChip, r.TenRAM, 
                                  m.SoLuong, m.GiaNhap, m.GiaBan, m.ThoiGianBH, m.Anh, m.GhiChu,
                                  m.MaLoai, m.MaChip, m.MaOCung, m.MaDungLuong, m.MaTocDo, m.MaOCD,
                                  m.MaManHinh, m.MaCoMH, m.MaChuot, m.MaBanPhim, m.MaUSB, m.MaRAM, m.MaLoa, m.MaHangSX
                           FROM MayViTinh m
                           LEFT JOIN LoaiMay l ON m.MaLoai = l.MaLoai
                           LEFT JOIN HangSanXuat h ON m.MaHangSX = h.MaHangSX
                           LEFT JOIN Chip c ON m.MaChip = c.MaChip
                           LEFT JOIN RAM r ON m.MaRAM = r.MaRAM";
            tblMayViTinh = Functions.GetDataToTable(sql);
            dgvMayViTinh.DataSource = tblMayViTinh;

            // Đặt tên cột hiển thị
            dgvMayViTinh.Columns["MaMayVT"].HeaderText = "Mã Máy";
            dgvMayViTinh.Columns["TenMayVT"].HeaderText = "Tên Máy Vi Tính";
            dgvMayViTinh.Columns["TenLoai"].HeaderText = "Loại Máy";
            dgvMayViTinh.Columns["TenHangSX"].HeaderText = "Hãng SX";
            dgvMayViTinh.Columns["TenChip"].HeaderText = "Vi Xử Lý (CPU)";
            dgvMayViTinh.Columns["TenRAM"].HeaderText = "Bộ Nhớ RAM";
            dgvMayViTinh.Columns["SoLuong"].HeaderText = "Tồn Kho";
            dgvMayViTinh.Columns["GiaNhap"].HeaderText = "Giá Nhập";
            dgvMayViTinh.Columns["GiaBan"].HeaderText = "Giá Bán (110%)";
            dgvMayViTinh.Columns["ThoiGianBH"].HeaderText = "BH (Tháng)";

            // Ẩn bớt các cột mã chi tiết để lưới thoáng đẹp
            string[] hiddenCols = { "Anh", "GhiChu", "MaLoai", "MaChip", "MaOCung", "MaDungLuong", "MaTocDo", 
                                    "MaOCD", "MaManHinh", "MaCoMH", "MaChuot", "MaBanPhim", "MaUSB", "MaRAM", "MaLoa", "MaHangSX" };
            foreach (var col in hiddenCols)
            {
                if (dgvMayViTinh.Columns.Contains(col))
                    dgvMayViTinh.Columns[col].Visible = false;
            }

            UIThemeHelper.FormatCurrencyColumns(dgvMayViTinh, "GiaNhap", "GiaBan");
        }

        private void ResetValues()
        {
            txtMaMay.Text = "";
            txtTenMay.Text = "";
            txtGiaNhap.Text = "0";
            txtGiaBan.Text = "0";
            txtSoLuong.Text = "0";
            txtBaoHanh.Text = "12";
            txtGhiChu.Text = "";
            txtAnh.Text = "";
            picAnh.Image = null;

            cboMaLoai.SelectedIndex = -1;
            cboMaChip.SelectedIndex = -1;
            cboMaOCung.SelectedIndex = -1;
            cboMaDungLuong.SelectedIndex = -1;
            cboMaTocDo.SelectedIndex = -1;
            cboMaOCD.SelectedIndex = -1;
            cboMaManHinh.SelectedIndex = -1;
            cboMaCoMH.SelectedIndex = -1;
            cboMaChuot.SelectedIndex = -1;
            cboMaBanPhim.SelectedIndex = -1;
            cboMaUSB.SelectedIndex = -1;
            cboMaRAM.SelectedIndex = -1;
            cboMaLoa.SelectedIndex = -1;
            cboMaHangSX.SelectedIndex = -1;

            txtMaMay.Enabled = false;
            btnLuu.Enabled = false;
            btnHuy.Enabled = false;
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
        }

        private void dgvMayViTinh_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvMayViTinh.CurrentRow == null || dgvMayViTinh.CurrentRow.Index < 0) return;

            DataGridViewRow r = dgvMayViTinh.CurrentRow;
            txtMaMay.Text = r.Cells["MaMayVT"].Value.ToString();
            txtTenMay.Text = r.Cells["TenMayVT"].Value.ToString();
            txtSoLuong.Text = r.Cells["SoLuong"].Value.ToString();
            txtGiaNhap.Text = string.Format("{0:N0}", r.Cells["GiaNhap"].Value);
            txtGiaBan.Text = string.Format("{0:N0}", r.Cells["GiaBan"].Value);
            txtBaoHanh.Text = r.Cells["ThoiGianBH"].Value.ToString();
            txtGhiChu.Text = r.Cells["GhiChu"].Value?.ToString() ?? "";
            txtAnh.Text = r.Cells["Anh"].Value?.ToString() ?? "";

            // Chọn giá trị 14 ComboBox linh kiện
            cboMaLoai.SelectedValue = r.Cells["MaLoai"].Value?.ToString() ?? "";
            cboMaChip.SelectedValue = r.Cells["MaChip"].Value?.ToString() ?? "";
            cboMaOCung.SelectedValue = r.Cells["MaOCung"].Value?.ToString() ?? "";
            cboMaDungLuong.SelectedValue = r.Cells["MaDungLuong"].Value?.ToString() ?? "";
            cboMaTocDo.SelectedValue = r.Cells["MaTocDo"].Value?.ToString() ?? "";
            cboMaOCD.SelectedValue = r.Cells["MaOCD"].Value?.ToString() ?? "";
            cboMaManHinh.SelectedValue = r.Cells["MaManHinh"].Value?.ToString() ?? "";
            cboMaCoMH.SelectedValue = r.Cells["MaCoMH"].Value?.ToString() ?? "";
            cboMaChuot.SelectedValue = r.Cells["MaChuot"].Value?.ToString() ?? "";
            cboMaBanPhim.SelectedValue = r.Cells["MaBanPhim"].Value?.ToString() ?? "";
            cboMaUSB.SelectedValue = r.Cells["MaUSB"].Value?.ToString() ?? "";
            cboMaRAM.SelectedValue = r.Cells["MaRAM"].Value?.ToString() ?? "";
            cboMaLoa.SelectedValue = r.Cells["MaLoa"].Value?.ToString() ?? "";
            cboMaHangSX.SelectedValue = r.Cells["MaHangSX"].Value?.ToString() ?? "";

            // Hiển thị ảnh không gây lock file
            string imgFile = txtAnh.Text.Trim();
            if (!string.IsNullOrEmpty(imgFile))
            {
                string fullPath = Path.Combine(Application.StartupPath, "Images", imgFile);
                if (File.Exists(fullPath))
                    picAnh.Image = Functions.LoadImageWithoutLock(fullPath);
                else
                    picAnh.Image = null;
            }
            else
            {
                picAnh.Image = null;
            }

            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnHuy.Enabled = true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            ResetValues();
            txtMaMay.Enabled = true;
            txtMaMay.Focus();
            btnLuu.Enabled = true;
            btnHuy.Enabled = true;
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaMay.Text))
            {
                MessageBox.Show("Vui lòng nhập Mã máy vi tính!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaMay.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtTenMay.Text))
            {
                MessageBox.Show("Vui lòng nhập Tên máy vi tính!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenMay.Focus();
                return;
            }
            if (cboMaLoai.SelectedIndex == -1 || cboMaHangSX.SelectedIndex == -1 || cboMaChip.SelectedIndex == -1 || cboMaRAM.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn đầy đủ các thông số cơ bản (Loại máy, Hãng SX, Chip, RAM)!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string checkSql = "SELECT MaMayVT FROM MayViTinh WHERE MaMayVT = N'" + txtMaMay.Text.Trim() + "'";
            if (Functions.CheckKey(checkSql))
            {
                MessageBox.Show("Mã máy vi tính này đã tồn tại, vui lòng nhập mã khác!", "Trùng Khóa", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaMay.Focus();
                return;
            }

            // Lưu sản phẩm mới: Số lượng ban đầu = 0, Giá nhập = 0 (Trigger sẽ tự tính khi nhập hàng)
            string sqlInsert = string.Format(@"INSERT INTO MayViTinh 
                (MaMayVT, TenMayVT, MaLoai, MaChip, MaOCung, MaDungLuong, MaTocDo, MaOCD, MaManHinh, MaCoMH, 
                 MaChuot, MaBanPhim, MaUSB, MaRAM, MaLoa, MaHangSX, GiaNhap, GiaBan, ThoiGianBH, SoLuong, Anh, GhiChu)
                VALUES (N'{0}', N'{1}', N'{2}', N'{3}', N'{4}', N'{5}', N'{6}', N'{7}', N'{8}', N'{9}',
                        N'{10}', N'{11}', N'{12}', N'{13}', N'{14}', N'{15}', 0, 0, {16}, 0, N'{17}', N'{18}')",
                txtMaMay.Text.Trim(),
                txtTenMay.Text.Trim().Replace("'", "''"),
                cboMaLoai.SelectedValue ?? "LM01",
                cboMaChip.SelectedValue ?? "CPU01",
                cboMaOCung.SelectedValue ?? "OC01",
                cboMaDungLuong.SelectedValue ?? "DL01",
                cboMaTocDo.SelectedValue ?? "TD01",
                cboMaOCD.SelectedValue ?? "CD01",
                cboMaManHinh.SelectedValue ?? "MH01",
                cboMaCoMH.SelectedValue ?? "CO01",
                cboMaChuot.SelectedValue ?? "CH01",
                cboMaBanPhim.SelectedValue ?? "BP01",
                cboMaUSB.SelectedValue ?? "USB01",
                cboMaRAM.SelectedValue ?? "RAM01",
                cboMaLoa.SelectedValue ?? "LOA01",
                cboMaHangSX.SelectedValue ?? "HSX01",
                string.IsNullOrEmpty(txtBaoHanh.Text) ? "12" : txtBaoHanh.Text.Trim(),
                txtAnh.Text.Trim(),
                txtGhiChu.Text.Trim().Replace("'", "''")
            );

            Functions.RunSQL(sqlInsert);
            MessageBox.Show("Thêm mới máy vi tính thành công!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadDataGridView();
            ResetValues();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaMay.Text))
            {
                MessageBox.Show("Vui lòng chọn máy vi tính cần sửa trên danh sách!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string sqlUpdate = string.Format(@"UPDATE MayViTinh SET 
                TenMayVT = N'{1}', MaLoai = N'{2}', MaChip = N'{3}', MaOCung = N'{4}', MaDungLuong = N'{5}',
                MaTocDo = N'{6}', MaOCD = N'{7}', MaManHinh = N'{8}', MaCoMH = N'{9}', MaChuot = N'{10}',
                MaBanPhim = N'{11}', MaUSB = N'{12}', MaRAM = N'{13}', MaLoa = N'{14}', MaHangSX = N'{15}',
                ThoiGianBH = {16}, Anh = N'{17}', GhiChu = N'{18}'
                WHERE MaMayVT = N'{0}'",
                txtMaMay.Text.Trim(),
                txtTenMay.Text.Trim().Replace("'", "''"),
                cboMaLoai.SelectedValue ?? "LM01",
                cboMaChip.SelectedValue ?? "CPU01",
                cboMaOCung.SelectedValue ?? "OC01",
                cboMaDungLuong.SelectedValue ?? "DL01",
                cboMaTocDo.SelectedValue ?? "TD01",
                cboMaOCD.SelectedValue ?? "CD01",
                cboMaManHinh.SelectedValue ?? "MH01",
                cboMaCoMH.SelectedValue ?? "CO01",
                cboMaChuot.SelectedValue ?? "CH01",
                cboMaBanPhim.SelectedValue ?? "BP01",
                cboMaUSB.SelectedValue ?? "USB01",
                cboMaRAM.SelectedValue ?? "RAM01",
                cboMaLoa.SelectedValue ?? "LOA01",
                cboMaHangSX.SelectedValue ?? "HSX01",
                string.IsNullOrEmpty(txtBaoHanh.Text) ? "12" : txtBaoHanh.Text.Trim(),
                txtAnh.Text.Trim(),
                txtGhiChu.Text.Trim().Replace("'", "''")
            );

            Functions.RunSQL(sqlUpdate);
            MessageBox.Show("Cập nhật thông tin máy vi tính thành công!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadDataGridView();
            ResetValues();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaMay.Text))
            {
                MessageBox.Show("Vui lòng chọn máy vi tính cần xóa!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa máy vi tính [" + txtTenMay.Text + "] không?",
                "Xác Nhận Xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string sqlDel = "DELETE FROM MayViTinh WHERE MaMayVT = N'" + txtMaMay.Text.Trim() + "'";
                Functions.RunSqlDel(sqlDel);
                LoadDataGridView();
                ResetValues();
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            ResetValues();
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnChonAnh_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files (*.jpg; *.jpeg; *.png; *.bmp)|*.jpg;*.jpeg;*.png;*.bmp";
                ofd.Title = "Chọn hình ảnh máy vi tính";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string targetFolder = Path.Combine(Application.StartupPath, "Images");
                    if (!Directory.Exists(targetFolder))
                        Directory.CreateDirectory(targetFolder);

                    string fileName = Path.GetFileName(ofd.FileName);
                    string targetPath = Path.Combine(targetFolder, fileName);

                    try
                    {
                        if (!File.Exists(targetPath))
                            File.Copy(ofd.FileName, targetPath, true);

                        txtAnh.Text = fileName;
                        picAnh.Image = Functions.LoadImageWithoutLock(targetPath);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi sao chép ảnh: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void txtBaoHanh_KeyPress(object sender, KeyPressEventArgs e)
        {
            Functions.OnlyInteger(e);
        }
    }
}
