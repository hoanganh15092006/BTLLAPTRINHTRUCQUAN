namespace QLBanMayViTinh.Forms
{
    partial class frmMayViTinh
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.pnlKpiBar = new System.Windows.Forms.Panel();
            this.pnlMainCard = new System.Windows.Forms.Panel();
            this.pnlHardwareCard = new System.Windows.Forms.Panel();
            this.lblHeaderHardware = new System.Windows.Forms.Label();
            this.pnlMediaCard = new System.Windows.Forms.Panel();
            this.lblHeaderMedia = new System.Windows.Forms.Label();
            this.picAnh = new System.Windows.Forms.PictureBox();
            this.btnChonAnh = new System.Windows.Forms.Button();
            this.txtAnh = new System.Windows.Forms.TextBox();
            this.lblGhiChu = new System.Windows.Forms.Label();
            this.txtGhiChu = new System.Windows.Forms.TextBox();

            this.lblMaMay = new System.Windows.Forms.Label();
            this.txtMaMay = new System.Windows.Forms.TextBox();
            this.lblTenMay = new System.Windows.Forms.Label();
            this.txtTenMay = new System.Windows.Forms.TextBox();
            this.lblLoai = new System.Windows.Forms.Label();
            this.cboMaLoai = new System.Windows.Forms.ComboBox();
            this.lblHangSX = new System.Windows.Forms.Label();
            this.cboMaHangSX = new System.Windows.Forms.ComboBox();
            this.lblGiaNhap = new System.Windows.Forms.Label();
            this.txtGiaNhap = new System.Windows.Forms.TextBox();
            this.lblGiaBan = new System.Windows.Forms.Label();
            this.txtGiaBan = new System.Windows.Forms.TextBox();
            this.lblSoLuong = new System.Windows.Forms.Label();
            this.txtSoLuong = new System.Windows.Forms.TextBox();
            this.lblBaoHanh = new System.Windows.Forms.Label();
            this.txtBaoHanh = new System.Windows.Forms.TextBox();

            this.lblChip = new System.Windows.Forms.Label();
            this.cboMaChip = new System.Windows.Forms.ComboBox();
            this.lblRAM = new System.Windows.Forms.Label();
            this.cboMaRAM = new System.Windows.Forms.ComboBox();
            this.lblOCung = new System.Windows.Forms.Label();
            this.cboMaOCung = new System.Windows.Forms.ComboBox();
            this.lblDungLuong = new System.Windows.Forms.Label();
            this.cboMaDungLuong = new System.Windows.Forms.ComboBox();
            this.lblTocDo = new System.Windows.Forms.Label();
            this.cboMaTocDo = new System.Windows.Forms.ComboBox();
            this.lblOCD = new System.Windows.Forms.Label();
            this.cboMaOCD = new System.Windows.Forms.ComboBox();

            this.lblManHinh = new System.Windows.Forms.Label();
            this.cboMaManHinh = new System.Windows.Forms.ComboBox();
            this.lblCoMH = new System.Windows.Forms.Label();
            this.cboMaCoMH = new System.Windows.Forms.ComboBox();
            this.lblChuot = new System.Windows.Forms.Label();
            this.cboMaChuot = new System.Windows.Forms.ComboBox();
            this.lblBanPhim = new System.Windows.Forms.Label();
            this.cboMaBanPhim = new System.Windows.Forms.ComboBox();
            this.lblUSB = new System.Windows.Forms.Label();
            this.cboMaUSB = new System.Windows.Forms.ComboBox();
            this.lblLoa = new System.Windows.Forms.Label();
            this.cboMaLoa = new System.Windows.Forms.ComboBox();

            this.pnlToolbar = new System.Windows.Forms.Panel();
            this.btnThem = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnLuu = new System.Windows.Forms.Button();
            this.btnHuy = new System.Windows.Forms.Button();
            this.btnDong = new System.Windows.Forms.Button();
            this.lblQuickSearch = new System.Windows.Forms.Label();
            this.txtTimKiemNhanh = new System.Windows.Forms.TextBox();

            this.pnlGridCard = new System.Windows.Forms.Panel();
            this.dgvMayViTinh = new System.Windows.Forms.DataGridView();

            this.pnlMainCard.SuspendLayout();
            this.pnlHardwareCard.SuspendLayout();
            this.pnlMediaCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picAnh)).BeginInit();
            this.pnlToolbar.SuspendLayout();
            this.pnlGridCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMayViTinh)).BeginInit();
            this.SuspendLayout();

            this.pnlKpiBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlKpiBar.Height = 84;
            this.pnlKpiBar.Location = new System.Drawing.Point(0, 0);
            this.pnlKpiBar.Name = "pnlKpiBar";
            this.pnlKpiBar.Padding = new System.Windows.Forms.Padding(0, 0, 0, 8);
            this.pnlKpiBar.TabIndex = 0;

            this.pnlMainCard.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlMainCard.Height = 270;
            this.pnlMainCard.Location = new System.Drawing.Point(0, 84);
            this.pnlMainCard.Name = "pnlMainCard";
            this.pnlMainCard.Padding = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.pnlMainCard.Controls.Add(this.pnlHardwareCard);
            this.pnlMainCard.Controls.Add(this.pnlMediaCard);
            this.pnlMainCard.TabIndex = 1;

            this.pnlMediaCard.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlMediaCard.Width = 280;
            this.pnlMediaCard.Location = new System.Drawing.Point(750, 0);
            this.pnlMediaCard.Name = "pnlMediaCard";
            this.pnlMediaCard.BackColor = System.Drawing.Color.White;
            this.pnlMediaCard.Controls.Add(this.txtGhiChu);
            this.pnlMediaCard.Controls.Add(this.lblGhiChu);
            this.pnlMediaCard.Controls.Add(this.txtAnh);
            this.pnlMediaCard.Controls.Add(this.btnChonAnh);
            this.pnlMediaCard.Controls.Add(this.picAnh);
            this.pnlMediaCard.Controls.Add(this.lblHeaderMedia);
            this.pnlMediaCard.TabIndex = 1;

            this.lblHeaderMedia.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblHeaderMedia.Height = 32;
            this.lblHeaderMedia.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblHeaderMedia.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.lblHeaderMedia.Text = "🖼️  ẢNH SẢN PHẨM & GHI CHÚ";
            this.lblHeaderMedia.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblHeaderMedia.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);

            this.picAnh.Location = new System.Drawing.Point(12, 38);
            this.picAnh.Size = new System.Drawing.Size(256, 110);
            this.picAnh.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picAnh.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picAnh.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);

            this.btnChonAnh.Location = new System.Drawing.Point(12, 153);
            this.btnChonAnh.Size = new System.Drawing.Size(100, 30);
            this.btnChonAnh.Text = "📷 Chọn Ảnh";
            this.btnChonAnh.Click += new System.EventHandler(this.btnChonAnh_Click);

            this.txtAnh.Location = new System.Drawing.Point(118, 155);
            this.txtAnh.Size = new System.Drawing.Size(150, 25);
            this.txtAnh.ReadOnly = true;

            this.lblGhiChu.Location = new System.Drawing.Point(12, 188);
            this.lblGhiChu.Size = new System.Drawing.Size(80, 18);
            this.lblGhiChu.Text = "Ghi chú:";
            this.lblGhiChu.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);

            this.txtGhiChu.Location = new System.Drawing.Point(12, 206);
            this.txtGhiChu.Size = new System.Drawing.Size(256, 48);
            this.txtGhiChu.Multiline = true;
            this.txtGhiChu.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;

            this.pnlHardwareCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlHardwareCard.Location = new System.Drawing.Point(0, 0);
            this.pnlHardwareCard.Name = "pnlHardwareCard";
            this.pnlHardwareCard.BackColor = System.Drawing.Color.White;
            this.pnlHardwareCard.Padding = new System.Windows.Forms.Padding(12, 0, 12, 0);
            this.pnlHardwareCard.Controls.Add(this.lblHeaderHardware);
            this.pnlHardwareCard.TabIndex = 0;

            this.lblHeaderHardware.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblHeaderHardware.Height = 32;
            this.lblHeaderHardware.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblHeaderHardware.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.lblHeaderHardware.Text = "📋  THÔNG TIN CƠ BẢN & 14 CẤU HÌNH PHẦN CỨNG (CHUẨN 3NF)";
            this.lblHeaderHardware.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            this.lblMaMay.Location = new System.Drawing.Point(14, 36);
            this.lblMaMay.Size = new System.Drawing.Size(68, 20);
            this.lblMaMay.Text = "Mã máy:";
            this.txtMaMay.Location = new System.Drawing.Point(85, 34);
            this.txtMaMay.Size = new System.Drawing.Size(145, 25);

            this.lblTenMay.Location = new System.Drawing.Point(14, 64);
            this.lblTenMay.Size = new System.Drawing.Size(68, 20);
            this.lblTenMay.Text = "Tên máy:";
            this.txtTenMay.Location = new System.Drawing.Point(85, 62);
            this.txtTenMay.Size = new System.Drawing.Size(145, 25);

            this.lblLoai.Location = new System.Drawing.Point(14, 92);
            this.lblLoai.Size = new System.Drawing.Size(68, 20);
            this.lblLoai.Text = "Loại máy:";
            this.cboMaLoai.Location = new System.Drawing.Point(85, 90);
            this.cboMaLoai.Size = new System.Drawing.Size(145, 25);

            this.lblHangSX.Location = new System.Drawing.Point(14, 120);
            this.lblHangSX.Size = new System.Drawing.Size(68, 20);
            this.lblHangSX.Text = "Hãng SX:";
            this.cboMaHangSX.Location = new System.Drawing.Point(85, 118);
            this.cboMaHangSX.Size = new System.Drawing.Size(145, 25);

            this.lblGiaNhap.Location = new System.Drawing.Point(14, 148);
            this.lblGiaNhap.Size = new System.Drawing.Size(68, 20);
            this.lblGiaNhap.Text = "Giá nhập:";
            this.txtGiaNhap.Location = new System.Drawing.Point(85, 146);
            this.txtGiaNhap.Size = new System.Drawing.Size(145, 25);

            this.lblGiaBan.Location = new System.Drawing.Point(14, 176);
            this.lblGiaBan.Size = new System.Drawing.Size(68, 20);
            this.lblGiaBan.Text = "Giá bán:";
            this.txtGiaBan.Location = new System.Drawing.Point(85, 174);
            this.txtGiaBan.Size = new System.Drawing.Size(145, 25);

            this.lblSoLuong.Location = new System.Drawing.Point(14, 204);
            this.lblSoLuong.Size = new System.Drawing.Size(68, 20);
            this.lblSoLuong.Text = "Tồn kho:";
            this.txtSoLuong.Location = new System.Drawing.Point(85, 202);
            this.txtSoLuong.Size = new System.Drawing.Size(65, 25);
            this.txtSoLuong.ReadOnly = true;

            this.lblBaoHanh.Location = new System.Drawing.Point(155, 204);
            this.lblBaoHanh.Size = new System.Drawing.Size(30, 20);
            this.lblBaoHanh.Text = "BH:";
            this.txtBaoHanh.Location = new System.Drawing.Point(187, 202);
            this.txtBaoHanh.Size = new System.Drawing.Size(43, 25);

            this.lblChip.Location = new System.Drawing.Point(250, 36);
            this.lblChip.Size = new System.Drawing.Size(75, 20);
            this.lblChip.Text = "Vi xử lý:";
            this.cboMaChip.Location = new System.Drawing.Point(328, 34);
            this.cboMaChip.Size = new System.Drawing.Size(145, 25);

            this.lblRAM.Location = new System.Drawing.Point(250, 64);
            this.lblRAM.Size = new System.Drawing.Size(75, 20);
            this.lblRAM.Text = "Bộ nhớ RAM:";
            this.cboMaRAM.Location = new System.Drawing.Point(328, 62);
            this.cboMaRAM.Size = new System.Drawing.Size(145, 25);

            this.lblOCung.Location = new System.Drawing.Point(250, 92);
            this.lblOCung.Size = new System.Drawing.Size(75, 20);
            this.lblOCung.Text = "Ổ đĩa cứng:";
            this.cboMaOCung.Location = new System.Drawing.Point(328, 90);
            this.cboMaOCung.Size = new System.Drawing.Size(145, 25);

            this.lblDungLuong.Location = new System.Drawing.Point(250, 120);
            this.lblDungLuong.Size = new System.Drawing.Size(75, 20);
            this.lblDungLuong.Text = "Dung lượng:";
            this.cboMaDungLuong.Location = new System.Drawing.Point(328, 118);
            this.cboMaDungLuong.Size = new System.Drawing.Size(145, 25);

            this.lblTocDo.Location = new System.Drawing.Point(250, 148);
            this.lblTocDo.Size = new System.Drawing.Size(75, 20);
            this.lblTocDo.Text = "Tốc độ Bus:";
            this.cboMaTocDo.Location = new System.Drawing.Point(328, 146);
            this.cboMaTocDo.Size = new System.Drawing.Size(145, 25);

            this.lblOCD.Location = new System.Drawing.Point(250, 176);
            this.lblOCD.Size = new System.Drawing.Size(75, 20);
            this.lblOCD.Text = "Ổ đĩa quang:";
            this.cboMaOCD.Location = new System.Drawing.Point(328, 174);
            this.cboMaOCD.Size = new System.Drawing.Size(145, 25);

            this.lblManHinh.Location = new System.Drawing.Point(495, 36);
            this.lblManHinh.Size = new System.Drawing.Size(75, 20);
            this.lblManHinh.Text = "Màn hình:";
            this.cboMaManHinh.Location = new System.Drawing.Point(573, 34);
            this.cboMaManHinh.Size = new System.Drawing.Size(145, 25);

            this.lblCoMH.Location = new System.Drawing.Point(495, 64);
            this.lblCoMH.Size = new System.Drawing.Size(75, 20);
            this.lblCoMH.Text = "Kích cỡ MH:";
            this.cboMaCoMH.Location = new System.Drawing.Point(573, 62);
            this.cboMaCoMH.Size = new System.Drawing.Size(145, 25);

            this.lblChuot.Location = new System.Drawing.Point(495, 92);
            this.lblChuot.Size = new System.Drawing.Size(75, 20);
            this.lblChuot.Text = "Chuột:";
            this.cboMaChuot.Location = new System.Drawing.Point(573, 90);
            this.cboMaChuot.Size = new System.Drawing.Size(145, 25);

            this.lblBanPhim.Location = new System.Drawing.Point(495, 120);
            this.lblBanPhim.Size = new System.Drawing.Size(75, 20);
            this.lblBanPhim.Text = "Bàn phím:";
            this.cboMaBanPhim.Location = new System.Drawing.Point(573, 118);
            this.cboMaBanPhim.Size = new System.Drawing.Size(145, 25);

            this.lblUSB.Location = new System.Drawing.Point(495, 148);
            this.lblUSB.Size = new System.Drawing.Size(75, 20);
            this.lblUSB.Text = "Cổng USB:";
            this.cboMaUSB.Location = new System.Drawing.Point(573, 146);
            this.cboMaUSB.Size = new System.Drawing.Size(145, 25);

            this.lblLoa.Location = new System.Drawing.Point(495, 176);
            this.lblLoa.Size = new System.Drawing.Size(75, 20);
            this.lblLoa.Text = "Loa âm thanh:";
            this.cboMaLoa.Location = new System.Drawing.Point(573, 174);
            this.cboMaLoa.Size = new System.Drawing.Size(145, 25);

            this.pnlHardwareCard.Controls.Add(this.txtMaMay);
            this.pnlHardwareCard.Controls.Add(this.lblMaMay);
            this.pnlHardwareCard.Controls.Add(this.txtTenMay);
            this.pnlHardwareCard.Controls.Add(this.lblTenMay);
            this.pnlHardwareCard.Controls.Add(this.cboMaLoai);
            this.pnlHardwareCard.Controls.Add(this.lblLoai);
            this.pnlHardwareCard.Controls.Add(this.cboMaHangSX);
            this.pnlHardwareCard.Controls.Add(this.lblHangSX);
            this.pnlHardwareCard.Controls.Add(this.txtGiaNhap);
            this.pnlHardwareCard.Controls.Add(this.lblGiaNhap);
            this.pnlHardwareCard.Controls.Add(this.txtGiaBan);
            this.pnlHardwareCard.Controls.Add(this.lblGiaBan);
            this.pnlHardwareCard.Controls.Add(this.txtSoLuong);
            this.pnlHardwareCard.Controls.Add(this.lblSoLuong);
            this.pnlHardwareCard.Controls.Add(this.txtBaoHanh);
            this.pnlHardwareCard.Controls.Add(this.lblBaoHanh);

            this.pnlHardwareCard.Controls.Add(this.cboMaChip);
            this.pnlHardwareCard.Controls.Add(this.lblChip);
            this.pnlHardwareCard.Controls.Add(this.cboMaRAM);
            this.pnlHardwareCard.Controls.Add(this.lblRAM);
            this.pnlHardwareCard.Controls.Add(this.cboMaOCung);
            this.pnlHardwareCard.Controls.Add(this.lblOCung);
            this.pnlHardwareCard.Controls.Add(this.cboMaDungLuong);
            this.pnlHardwareCard.Controls.Add(this.lblDungLuong);
            this.pnlHardwareCard.Controls.Add(this.cboMaTocDo);
            this.pnlHardwareCard.Controls.Add(this.lblTocDo);
            this.pnlHardwareCard.Controls.Add(this.cboMaOCD);
            this.pnlHardwareCard.Controls.Add(this.lblOCD);

            this.pnlHardwareCard.Controls.Add(this.cboMaManHinh);
            this.pnlHardwareCard.Controls.Add(this.lblManHinh);
            this.pnlHardwareCard.Controls.Add(this.cboMaCoMH);
            this.pnlHardwareCard.Controls.Add(this.lblCoMH);
            this.pnlHardwareCard.Controls.Add(this.cboMaChuot);
            this.pnlHardwareCard.Controls.Add(this.lblChuot);
            this.pnlHardwareCard.Controls.Add(this.cboMaBanPhim);
            this.pnlHardwareCard.Controls.Add(this.lblBanPhim);
            this.pnlHardwareCard.Controls.Add(this.cboMaUSB);
            this.pnlHardwareCard.Controls.Add(this.lblUSB);
            this.pnlHardwareCard.Controls.Add(this.cboMaLoa);
            this.pnlHardwareCard.Controls.Add(this.lblLoa);

            this.pnlToolbar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolbar.Height = 52;
            this.pnlToolbar.Location = new System.Drawing.Point(0, 354);
            this.pnlToolbar.Name = "pnlToolbar";
            this.pnlToolbar.BackColor = System.Drawing.Color.White;
            this.pnlToolbar.Padding = new System.Windows.Forms.Padding(8);
            this.pnlToolbar.Controls.Add(this.btnDong);
            this.pnlToolbar.Controls.Add(this.txtTimKiemNhanh);
            this.pnlToolbar.Controls.Add(this.lblQuickSearch);
            this.pnlToolbar.Controls.Add(this.btnHuy);
            this.pnlToolbar.Controls.Add(this.btnLuu);
            this.pnlToolbar.Controls.Add(this.btnXoa);
            this.pnlToolbar.Controls.Add(this.btnSua);
            this.pnlToolbar.Controls.Add(this.btnThem);
            this.pnlToolbar.TabIndex = 2;

            this.btnThem.Location = new System.Drawing.Point(8, 8);
            this.btnThem.Size = new System.Drawing.Size(95, 36);
            this.btnThem.Text = "➕ Thêm";
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);

            this.btnSua.Location = new System.Drawing.Point(110, 8);
            this.btnSua.Size = new System.Drawing.Size(90, 36);
            this.btnSua.Text = "✏️ Sửa";
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);

            this.btnXoa.Location = new System.Drawing.Point(206, 8);
            this.btnXoa.Size = new System.Drawing.Size(90, 36);
            this.btnXoa.Text = "🗑️ Xóa";
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);

            this.btnLuu.Location = new System.Drawing.Point(302, 8);
            this.btnLuu.Size = new System.Drawing.Size(90, 36);
            this.btnLuu.Text = "💾 Lưu";
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);

            this.btnHuy.Location = new System.Drawing.Point(398, 8);
            this.btnHuy.Size = new System.Drawing.Size(90, 36);
            this.btnHuy.Text = "❌ Hủy";
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);

            this.lblQuickSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblQuickSearch.Location = new System.Drawing.Point(600, 16);
            this.lblQuickSearch.Size = new System.Drawing.Size(115, 20);
            this.lblQuickSearch.Text = "🔍 Tìm nhanh:";
            this.lblQuickSearch.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblQuickSearch.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);

            this.txtTimKiemNhanh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTimKiemNhanh.Location = new System.Drawing.Point(718, 14);
            this.txtTimKiemNhanh.Size = new System.Drawing.Size(195, 25);
            this.txtTimKiemNhanh.TextChanged += new System.EventHandler(this.txtTimKiemNhanh_TextChanged);

            this.btnDong.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDong.Location = new System.Drawing.Point(925, 8);
            this.btnDong.Size = new System.Drawing.Size(95, 36);
            this.btnDong.Text = "🚪 Đóng";
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);

            this.pnlGridCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGridCard.Location = new System.Drawing.Point(0, 406);
            this.pnlGridCard.Name = "pnlGridCard";
            this.pnlGridCard.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.pnlGridCard.Controls.Add(this.dgvMayViTinh);
            this.pnlGridCard.TabIndex = 3;

            this.dgvMayViTinh.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMayViTinh.Location = new System.Drawing.Point(0, 10);
            this.dgvMayViTinh.Name = "dgvMayViTinh";
            this.dgvMayViTinh.Size = new System.Drawing.Size(1030, 290);
            this.dgvMayViTinh.TabIndex = 0;
            this.dgvMayViTinh.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMayViTinh_CellClick);

            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(241, 245, 249);
            this.ClientSize = new System.Drawing.Size(1030, 700);
            this.Controls.Add(this.pnlGridCard);
            this.Controls.Add(this.pnlToolbar);
            this.Controls.Add(this.pnlMainCard);
            this.Controls.Add(this.pnlKpiBar);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.Name = "frmMayViTinh";
            this.Text = "QUẢN LÝ DANH MỤC MÁY VI TÍNH";
            this.Load += new System.EventHandler(this.frmMayViTinh_Load);

            this.pnlMainCard.ResumeLayout(false);
            this.pnlHardwareCard.ResumeLayout(false);
            this.pnlHardwareCard.PerformLayout();
            this.pnlMediaCard.ResumeLayout(false);
            this.pnlMediaCard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picAnh)).EndInit();
            this.pnlToolbar.ResumeLayout(false);
            this.pnlToolbar.PerformLayout();
            this.pnlGridCard.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMayViTinh)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlKpiBar;
        private System.Windows.Forms.Panel pnlMainCard;
        private System.Windows.Forms.Panel pnlHardwareCard;
        private System.Windows.Forms.Label lblHeaderHardware;
        private System.Windows.Forms.Panel pnlMediaCard;
        private System.Windows.Forms.Label lblHeaderMedia;
        private System.Windows.Forms.PictureBox picAnh;
        private System.Windows.Forms.Button btnChonAnh;
        private System.Windows.Forms.TextBox txtAnh;
        private System.Windows.Forms.Label lblGhiChu;
        private System.Windows.Forms.TextBox txtGhiChu;
        private System.Windows.Forms.Label lblMaMay;
        private System.Windows.Forms.TextBox txtMaMay;
        private System.Windows.Forms.Label lblTenMay;
        private System.Windows.Forms.TextBox txtTenMay;
        private System.Windows.Forms.Label lblLoai;
        private System.Windows.Forms.ComboBox cboMaLoai;
        private System.Windows.Forms.Label lblHangSX;
        private System.Windows.Forms.ComboBox cboMaHangSX;
        private System.Windows.Forms.Label lblGiaNhap;
        private System.Windows.Forms.TextBox txtGiaNhap;
        private System.Windows.Forms.Label lblGiaBan;
        private System.Windows.Forms.TextBox txtGiaBan;
        private System.Windows.Forms.Label lblSoLuong;
        private System.Windows.Forms.TextBox txtSoLuong;
        private System.Windows.Forms.Label lblBaoHanh;
        private System.Windows.Forms.TextBox txtBaoHanh;
        private System.Windows.Forms.Label lblChip;
        private System.Windows.Forms.ComboBox cboMaChip;
        private System.Windows.Forms.Label lblRAM;
        private System.Windows.Forms.ComboBox cboMaRAM;
        private System.Windows.Forms.Label lblOCung;
        private System.Windows.Forms.ComboBox cboMaOCung;
        private System.Windows.Forms.Label lblDungLuong;
        private System.Windows.Forms.ComboBox cboMaDungLuong;
        private System.Windows.Forms.Label lblTocDo;
        private System.Windows.Forms.ComboBox cboMaTocDo;
        private System.Windows.Forms.Label lblOCD;
        private System.Windows.Forms.ComboBox cboMaOCD;
        private System.Windows.Forms.Label lblManHinh;
        private System.Windows.Forms.ComboBox cboMaManHinh;
        private System.Windows.Forms.Label lblCoMH;
        private System.Windows.Forms.ComboBox cboMaCoMH;
        private System.Windows.Forms.Label lblChuot;
        private System.Windows.Forms.ComboBox cboMaChuot;
        private System.Windows.Forms.Label lblBanPhim;
        private System.Windows.Forms.ComboBox cboMaBanPhim;
        private System.Windows.Forms.Label lblUSB;
        private System.Windows.Forms.ComboBox cboMaUSB;
        private System.Windows.Forms.Label lblLoa;
        private System.Windows.Forms.ComboBox cboMaLoa;
        private System.Windows.Forms.Panel pnlToolbar;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnLuu;
        private System.Windows.Forms.Button btnHuy;
        private System.Windows.Forms.Button btnDong;
        private System.Windows.Forms.Label lblQuickSearch;
        private System.Windows.Forms.TextBox txtTimKiemNhanh;
        private System.Windows.Forms.Panel pnlGridCard;
        private System.Windows.Forms.DataGridView dgvMayViTinh;
    }
}
