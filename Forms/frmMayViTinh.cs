using System;

using System.ComponentModel;

using System.Data;

using System.Data.SqlClient;

using System.Drawing;

using System.IO;

using System.Windows.Forms;

using QLBanMayViTinh.Classes;



namespace QLBanMayViTinh.Forms

{

    public partial class frmMayViTinh : Form

    {

        private DataTable tblMayViTinh;

        private byte[] currentImageBytes = null;



        public frmMayViTinh()

        {

            InitializeComponent();

        }



        private void frmMayViTinh_Load(object sender, EventArgs e)

        {

            if (this.DesignMode ||

                LicenseManager.UsageMode == LicenseUsageMode.Designtime ||

                System.Diagnostics.Process.GetCurrentProcess().ProcessName.Equals("devenv", StringComparison.OrdinalIgnoreCase))

            {

                return;

            }



            dgvMayViTinh.DataError += (s, ev) => { ev.ThrowException = false; };



            UIThemeHelper.StyleCard(pnlHardwareCard, null, 8);

            UIThemeHelper.StyleCard(pnlMediaCard, null, 8);

            UIThemeHelper.StyleCard(pnlToolbar, null, 8);

            UIThemeHelper.StyleCard(pnlGridCard, null, 8);



            UIThemeHelper.StyleDataGridView(dgvMayViTinh);

            UIThemeHelper.StyleButton(btnThem, UIThemeHelper.BtnThemColor, Color.White, 8);

            UIThemeHelper.StyleButton(btnSua, UIThemeHelper.BtnSuaColor, Color.White, 8);

            UIThemeHelper.StyleButton(btnXoa, UIThemeHelper.BtnXoaColor, Color.White, 8);

            UIThemeHelper.StyleButton(btnLuu, UIThemeHelper.BtnLuuColor, Color.White, 8);

            UIThemeHelper.StyleButton(btnHuy, UIThemeHelper.BtnHuyColor, Color.White, 8);

            UIThemeHelper.StyleButton(btnChonAnh, Color.FromArgb(79, 70, 229), Color.White, 8);

            UIThemeHelper.StyleButton(btnDong, Color.FromArgb(100, 116, 139), Color.White, 8);



            UIThemeHelper.ApplyTheme(this);

            pnlKpiBar.Resize += (s, ev) => UpdateKpiCards();

            pnlHardwareCard.Resize += (s, ev) => LayoutHardwareCard();

            this.Resize += (s, ev) => UpdateKpiCards();

            this.Shown += (s, ev) => this.BeginInvoke((Action)(() =>

            {

                UpdateKpiCards();

                LayoutHardwareCard();

            }));

            LayoutHardwareCard();



            LoadComboBoxes();

            ResetValues();

            LoadDataGridView();

        }



        private void UpdateKpiCards()

        {

            pnlKpiBar.Controls.Clear();

            int totalModels = tblMayViTinh != null ? tblMayViTinh.Rows.Count : 0;

            int totalStock = 0;

            decimal totalValuation = 0;



            if (tblMayViTinh != null)

            {

                foreach (DataRow r in tblMayViTinh.Rows)

                {

                    int sl = 0;

                    decimal gia = 0;

                    if (r["SoLuong"] != DBNull.Value) int.TryParse(r["SoLuong"].ToString(), out sl);

                    if (r["GiaNhap"] != DBNull.Value) decimal.TryParse(r["GiaNhap"].ToString(), out gia);

                    totalStock += sl;

                    totalValuation += sl * gia;

                }

            }



            int kpiGap = 15;

            int barW = pnlKpiBar.ClientSize.Width > 0 ? pnlKpiBar.ClientSize.Width : pnlKpiBar.Width;

            int kpiW = Math.Max(220, (barW - (kpiGap * 3)) / 4);



            Panel kpi1 = UIThemeHelper.CreateKpiCard("Mẫu Sản Phẩm", totalModels + " Mẫu", "Đang quản lý trên hệ thống", Color.FromArgb(79, 70, 229), "", kpiW, 76);

            Panel kpi2 = UIThemeHelper.CreateKpiCard("Tổng Tồn Kho", totalStock + " Chiếc", "Sẵn sàng xuất bán lẻ", Color.FromArgb(16, 185, 129), "", kpiW, 76);

            Panel kpi3 = UIThemeHelper.CreateKpiCard("Tổng Giá Trị Kho", string.Format("{0:N0} đ", totalValuation), "Định giá theo giá nhập", Color.FromArgb(245, 158, 11), "", kpiW, 76);

            Panel kpi4 = UIThemeHelper.CreateKpiCard("Cấu Hình Chuẩn 3NF", "14 Linh Kiện", "Toàn vẹn khóa ngoại CSDL", Color.FromArgb(14, 165, 233), "", kpiW, 76);



            kpi1.Location = new Point(0, 0);

            kpi2.Location = new Point(kpiW + kpiGap, 0);

            kpi3.Location = new Point((kpiW + kpiGap) * 2, 0);

            kpi4.Location = new Point((kpiW + kpiGap) * 3, 0);



            pnlKpiBar.Controls.Add(kpi1);

            pnlKpiBar.Controls.Add(kpi2);

            pnlKpiBar.Controls.Add(kpi3);

            pnlKpiBar.Controls.Add(kpi4);

        }



        private void LayoutHardwareCard()
        {
            if (pnlHardwareCard == null || pnlHardwareCard.ClientSize.Width <= 0) return;
            int w = pnlHardwareCard.ClientSize.Width;
            int margin = 12;
            int gap = 16;
            int colW = Math.Max(180, (w - (margin * 2) - (gap * 2)) / 3);

            int col1X = margin;
            int col1LabelW = 68;
            int col1InputX = col1X + col1LabelW + 4;
            int col1InputW = colW - col1LabelW - 4;

            txtMaMay.Location = new Point(col1InputX, 34); txtMaMay.Width = col1InputW;
            txtTenMay.Location = new Point(col1InputX, 62); txtTenMay.Width = col1InputW;
            cboMaLoai.Location = new Point(col1InputX, 90); cboMaLoai.Width = col1InputW;
            cboMaHangSX.Location = new Point(col1InputX, 118); cboMaHangSX.Width = col1InputW;
            txtGiaNhap.Location = new Point(col1InputX, 146); txtGiaNhap.Width = col1InputW;
            txtGiaBan.Location = new Point(col1InputX, 174); txtGiaBan.Width = col1InputW;
            txtSoLuong.Location = new Point(col1InputX, 202);
            int bhLabelX = col1InputX + txtSoLuong.Width + 6;
            lblBaoHanh.Location = new Point(bhLabelX, 205);
            int bhInputX = bhLabelX + lblBaoHanh.Width + 4;
            txtBaoHanh.Location = new Point(bhInputX, 202);
            txtBaoHanh.Width = Math.Max(40, (col1X + colW) - bhInputX);

            int col2X = col1X + colW + gap;
            int col2LabelW = 85;
            int col2InputX = col2X + col2LabelW + 4;
            int col2InputW = colW - col2LabelW - 4;

            lblChip.Location = new Point(col2X, 36); lblChip.Size = new Size(col2LabelW, 20);
            cboMaChip.Location = new Point(col2InputX, 34); cboMaChip.Width = col2InputW;

            lblRAM.Location = new Point(col2X, 64); lblRAM.Size = new Size(col2LabelW, 20);
            cboMaRAM.Location = new Point(col2InputX, 62); cboMaRAM.Width = col2InputW;

            lblOCung.Location = new Point(col2X, 92); lblOCung.Size = new Size(col2LabelW, 20);
            cboMaOCung.Location = new Point(col2InputX, 90); cboMaOCung.Width = col2InputW;

            lblDungLuong.Location = new Point(col2X, 120); lblDungLuong.Size = new Size(col2LabelW, 20);
            cboMaDungLuong.Location = new Point(col2InputX, 118); cboMaDungLuong.Width = col2InputW;

            lblTocDo.Location = new Point(col2X, 148); lblTocDo.Size = new Size(col2LabelW, 20);
            cboMaTocDo.Location = new Point(col2InputX, 146); cboMaTocDo.Width = col2InputW;

            lblOCD.Location = new Point(col2X, 176); lblOCD.Size = new Size(col2LabelW, 20);
            cboMaOCD.Location = new Point(col2InputX, 174); cboMaOCD.Width = col2InputW;

            int col3X = col2X + colW + gap;
            int col3LabelW = 75;
            int col3InputX = col3X + col3LabelW + 4;
            int col3InputW = colW - col3LabelW - 4;

            lblManHinh.Location = new Point(col3X, 36); lblManHinh.Size = new Size(col3LabelW, 20);
            cboMaManHinh.Location = new Point(col3InputX, 34); cboMaManHinh.Width = col3InputW;

            lblCoMH.Location = new Point(col3X, 64); lblCoMH.Size = new Size(col3LabelW, 20);
            cboMaCoMH.Location = new Point(col3InputX, 62); cboMaCoMH.Width = col3InputW;

            lblChuot.Location = new Point(col3X, 92); lblChuot.Size = new Size(col3LabelW, 20);
            cboMaChuot.Location = new Point(col3InputX, 90); cboMaChuot.Width = col3InputW;

            lblBanPhim.Location = new Point(col3X, 120); lblBanPhim.Size = new Size(col3LabelW, 20);
            cboMaBanPhim.Location = new Point(col3InputX, 118); cboMaBanPhim.Width = col3InputW;

            lblUSB.Location = new Point(col3X, 148); lblUSB.Size = new Size(col3LabelW, 20);
            cboMaUSB.Location = new Point(col3InputX, 146); cboMaUSB.Width = col3InputW;

            lblLoa.Location = new Point(col3X, 176); lblLoa.Size = new Size(col3LabelW, 20);
            cboMaLoa.Location = new Point(col3InputX, 174); cboMaLoa.Width = col3InputW;
        }



        private void txtTimKiemNhanh_TextChanged(object sender, EventArgs e)

        {

            if (tblMayViTinh == null) return;

            string keyword = txtTimKiemNhanh.Text.Trim().Replace("'", "''");

            if (string.IsNullOrEmpty(keyword))

            {

                tblMayViTinh.DefaultView.RowFilter = "";

            }

            else

            {

                tblMayViTinh.DefaultView.RowFilter = string.Format("MaMayVT LIKE '%{0}%' OR TenMayVT LIKE '%{0}%' OR TenLoai LIKE '%{0}%' OR TenHangSX LIKE '%{0}%'", keyword);

            }

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



            string[] hiddenCols = { "Anh", "GhiChu", "MaLoai", "MaChip", "MaOCung", "MaDungLuong", "MaTocDo",

                                    "MaOCD", "MaManHinh", "MaCoMH", "MaChuot", "MaBanPhim", "MaUSB", "MaRAM", "MaLoa", "MaHangSX" };

            foreach (var col in hiddenCols)

            {

                if (dgvMayViTinh.Columns.Contains(col))

                    dgvMayViTinh.Columns[col].Visible = false;

            }



            UIThemeHelper.FormatCurrencyColumns(dgvMayViTinh, "GiaNhap", "GiaBan");

            UpdateKpiCards();

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

            currentImageBytes = null;

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



            currentImageBytes = null;

            if (r.Cells["Anh"].Value != null && r.Cells["Anh"].Value != DBNull.Value)

            {

                if (r.Cells["Anh"].Value is byte[] bytes && bytes.Length > 0)

                {

                    currentImageBytes = bytes;

                    picAnh.Image = Functions.ByteArrayToImage(bytes);

                    txtAnh.Text = string.Format("[Ảnh nhị phân: {0:N0} bytes]", bytes.Length);

                }

                else

                {

                    picAnh.Image = null;

                    txtAnh.Text = "[Chưa có ảnh]";

                }

            }

            else

            {

                picAnh.Image = null;

                txtAnh.Text = "[Chưa có ảnh]";

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



            string sqlInsert = @"INSERT INTO MayViTinh

                (MaMayVT, TenMayVT, MaLoai, MaChip, MaOCung, MaDungLuong, MaTocDo, MaOCD, MaManHinh, MaCoMH,

                 MaChuot, MaBanPhim, MaUSB, MaRAM, MaLoa, MaHangSX, GiaNhap, GiaBan, ThoiGianBH, SoLuong, Anh, GhiChu)

                VALUES (@MaMayVT, @TenMayVT, @MaLoai, @MaChip, @MaOCung, @MaDungLuong, @MaTocDo, @MaOCD, @MaManHinh, @MaCoMH,

                        @MaChuot, @MaBanPhim, @MaUSB, @MaRAM, @MaLoa, @MaHangSX, 0, 0, @ThoiGianBH, 0, @Anh, @GhiChu)";



            int bh = 12;

            int.TryParse(txtBaoHanh.Text.Trim(), out bh);



            SqlParameter[] parameters = new SqlParameter[]

            {

                new SqlParameter("@MaMayVT", txtMaMay.Text.Trim()),

                new SqlParameter("@TenMayVT", txtTenMay.Text.Trim()),

                new SqlParameter("@MaLoai", cboMaLoai.SelectedValue ?? "LM01"),

                new SqlParameter("@MaChip", cboMaChip.SelectedValue ?? "CPU01"),

                new SqlParameter("@MaOCung", cboMaOCung.SelectedValue ?? "OC01"),

                new SqlParameter("@MaDungLuong", cboMaDungLuong.SelectedValue ?? "DL01"),

                new SqlParameter("@MaTocDo", cboMaTocDo.SelectedValue ?? "TD01"),

                new SqlParameter("@MaOCD", cboMaOCD.SelectedValue ?? "CD01"),

                new SqlParameter("@MaManHinh", cboMaManHinh.SelectedValue ?? "MH01"),

                new SqlParameter("@MaCoMH", cboMaCoMH.SelectedValue ?? "CO01"),

                new SqlParameter("@MaChuot", cboMaChuot.SelectedValue ?? "CH01"),

                new SqlParameter("@MaBanPhim", cboMaBanPhim.SelectedValue ?? "BP01"),

                new SqlParameter("@MaUSB", cboMaUSB.SelectedValue ?? "USB01"),

                new SqlParameter("@MaRAM", cboMaRAM.SelectedValue ?? "RAM01"),

                new SqlParameter("@MaLoa", cboMaLoa.SelectedValue ?? "LOA01"),

                new SqlParameter("@MaHangSX", cboMaHangSX.SelectedValue ?? "HSX01"),

                new SqlParameter("@ThoiGianBH", bh),

                new SqlParameter("@Anh", SqlDbType.VarBinary, -1) { Value = (object)currentImageBytes ?? DBNull.Value },

                new SqlParameter("@GhiChu", txtGhiChu.Text.Trim())

            };



            Functions.RunSQL(sqlInsert, parameters);

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



            string sqlUpdate = @"UPDATE MayViTinh SET

                TenMayVT = @TenMayVT, MaLoai = @MaLoai, MaChip = @MaChip, MaOCung = @MaOCung,

                MaDungLuong = @MaDungLuong, MaTocDo = @MaTocDo, MaOCD = @MaOCD, MaManHinh = @MaManHinh,

                MaCoMH = @MaCoMH, MaChuot = @MaChuot, MaBanPhim = @MaBanPhim, MaUSB = @MaUSB,

                MaRAM = @MaRAM, MaLoa = @MaLoa, MaHangSX = @MaHangSX, ThoiGianBH = @ThoiGianBH,

                Anh = @Anh, GhiChu = @GhiChu

                WHERE MaMayVT = @MaMayVT";



            int bh = 12;

            int.TryParse(txtBaoHanh.Text.Trim(), out bh);



            SqlParameter[] parameters = new SqlParameter[]

            {

                new SqlParameter("@MaMayVT", txtMaMay.Text.Trim()),

                new SqlParameter("@TenMayVT", txtTenMay.Text.Trim()),

                new SqlParameter("@MaLoai", cboMaLoai.SelectedValue ?? "LM01"),

                new SqlParameter("@MaChip", cboMaChip.SelectedValue ?? "CPU01"),

                new SqlParameter("@MaOCung", cboMaOCung.SelectedValue ?? "OC01"),

                new SqlParameter("@MaDungLuong", cboMaDungLuong.SelectedValue ?? "DL01"),

                new SqlParameter("@MaTocDo", cboMaTocDo.SelectedValue ?? "TD01"),

                new SqlParameter("@MaOCD", cboMaOCD.SelectedValue ?? "CD01"),

                new SqlParameter("@MaManHinh", cboMaManHinh.SelectedValue ?? "MH01"),

                new SqlParameter("@MaCoMH", cboMaCoMH.SelectedValue ?? "CO01"),

                new SqlParameter("@MaChuot", cboMaChuot.SelectedValue ?? "CH01"),

                new SqlParameter("@MaBanPhim", cboMaBanPhim.SelectedValue ?? "BP01"),

                new SqlParameter("@MaUSB", cboMaUSB.SelectedValue ?? "USB01"),

                new SqlParameter("@MaRAM", cboMaRAM.SelectedValue ?? "RAM01"),

                new SqlParameter("@MaLoa", cboMaLoa.SelectedValue ?? "LOA01"),

                new SqlParameter("@MaHangSX", cboMaHangSX.SelectedValue ?? "HSX01"),

                new SqlParameter("@ThoiGianBH", bh),

                new SqlParameter("@Anh", SqlDbType.VarBinary, -1) { Value = (object)currentImageBytes ?? DBNull.Value },

                new SqlParameter("@GhiChu", txtGhiChu.Text.Trim())

            };



            Functions.RunSQL(sqlUpdate, parameters);

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

                    try

                    {

                        byte[] raw = File.ReadAllBytes(ofd.FileName);

                        currentImageBytes = raw;

                        picAnh.Image = Functions.ByteArrayToImage(raw);

                        txtAnh.Text = Path.GetFileName(ofd.FileName) + string.Format(" ({0:N0} bytes)", raw.Length);

                    }

                    catch (Exception ex)

                    {

                        MessageBox.Show("Lỗi đọc tệp hình ảnh: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }

                }

            }

        }



        private void txtBaoHanh_KeyPress(object sender, KeyPressEventArgs e)

        {

            Functions.OnlyInteger(e);

        }



        private void pnlHardwareCard_Paint(object sender, PaintEventArgs e)

        {



        }



        private void cboMaOCung_SelectedIndexChanged(object sender, EventArgs e)

        {



        }



        private void cboMaDungLuong_SelectedIndexChanged(object sender, EventArgs e)

        {



        }



        private void cboMaTocDo_SelectedIndexChanged(object sender, EventArgs e)

        {



        }



        private void cboMaOCD_SelectedIndexChanged(object sender, EventArgs e)

        {



        }



        private void cboMaChip_SelectedIndexChanged(object sender, EventArgs e)

        {



        }



        private void cboMaRAM_SelectedIndexChanged(object sender, EventArgs e)

        {



        }



        private void cboMaCoMH_SelectedIndexChanged(object sender, EventArgs e)

        {



        }



        private void cboMaChuot_SelectedIndexChanged(object sender, EventArgs e)

        {



        }



        private void cboMaBanPhim_SelectedIndexChanged(object sender, EventArgs e)

        {



        }



        private void cboMaUSB_SelectedIndexChanged(object sender, EventArgs e)

        {



        }



        private void cboMaLoa_SelectedIndexChanged(object sender, EventArgs e)

        {



        }



        private void lblHeaderHardware_Click(object sender, EventArgs e)

        {



        }

    }

}

