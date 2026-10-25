namespace QLBanMayViTinh.Forms
{
    partial class frmTimKiem
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.RadioButton rdoMayViTinh;
        private System.Windows.Forms.RadioButton rdoHoaDonNhap;
        private System.Windows.Forms.RadioButton rdoHoaDonBan;
        private System.Windows.Forms.Panel pnlMayTinh;
        private System.Windows.Forms.Label lblMaMay;
        private System.Windows.Forms.TextBox txtMaMay;
        private System.Windows.Forms.Label lblTenMay;
        private System.Windows.Forms.TextBox txtTenMay;
        private System.Windows.Forms.Label lblLoai;
        private System.Windows.Forms.ComboBox cboLoai;
        private System.Windows.Forms.Label lblHangSX;
        private System.Windows.Forms.ComboBox cboHangSX;

        private System.Windows.Forms.Panel pnlHoaDon;
        private System.Windows.Forms.Label lblMaHD;
        private System.Windows.Forms.TextBox txtMaHD;
        private System.Windows.Forms.Label lblMatHang;
        private System.Windows.Forms.ComboBox cboMatHang;
        private System.Windows.Forms.Label lblNhanVien;
        private System.Windows.Forms.ComboBox cboNhanVien;
        private System.Windows.Forms.Label lblDoiTac;
        private System.Windows.Forms.ComboBox cboNCC;
        private System.Windows.Forms.ComboBox cboKhachHang;
        private System.Windows.Forms.CheckBox chkNgay;
        private System.Windows.Forms.DateTimePicker dtpTuNgay;
        private System.Windows.Forms.Label lblDenNgay;
        private System.Windows.Forms.DateTimePicker dtpDenNgay;

        private System.Windows.Forms.Panel pnlActions;
        private System.Windows.Forms.Button btnTimKiem;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.Button btnDong;
        private System.Windows.Forms.Label lblSoKetQua;
        private System.Windows.Forms.DataGridView dgvKetQua;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlTop = new System.Windows.Forms.Panel();
            this.lblTieuDe = new System.Windows.Forms.Label();
            this.rdoMayViTinh = new System.Windows.Forms.RadioButton();
            this.rdoHoaDonNhap = new System.Windows.Forms.RadioButton();
            this.rdoHoaDonBan = new System.Windows.Forms.RadioButton();

            this.pnlMayTinh = new System.Windows.Forms.Panel();
            this.lblMaMay = new System.Windows.Forms.Label();
            this.txtMaMay = new System.Windows.Forms.TextBox();
            this.lblTenMay = new System.Windows.Forms.Label();
            this.txtTenMay = new System.Windows.Forms.TextBox();
            this.lblLoai = new System.Windows.Forms.Label();
            this.cboLoai = new System.Windows.Forms.ComboBox();
            this.lblHangSX = new System.Windows.Forms.Label();
            this.cboHangSX = new System.Windows.Forms.ComboBox();

            this.pnlHoaDon = new System.Windows.Forms.Panel();
            this.lblMaHD = new System.Windows.Forms.Label();
            this.txtMaHD = new System.Windows.Forms.TextBox();
            this.lblMatHang = new System.Windows.Forms.Label();
            this.cboMatHang = new System.Windows.Forms.ComboBox();
            this.lblNhanVien = new System.Windows.Forms.Label();
            this.cboNhanVien = new System.Windows.Forms.ComboBox();
            this.lblDoiTac = new System.Windows.Forms.Label();
            this.cboNCC = new System.Windows.Forms.ComboBox();
            this.cboKhachHang = new System.Windows.Forms.ComboBox();
            this.chkNgay = new System.Windows.Forms.CheckBox();
            this.dtpTuNgay = new System.Windows.Forms.DateTimePicker();
            this.lblDenNgay = new System.Windows.Forms.Label();
            this.dtpDenNgay = new System.Windows.Forms.DateTimePicker();

            this.pnlActions = new System.Windows.Forms.Panel();
            this.btnTimKiem = new System.Windows.Forms.Button();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.btnDong = new System.Windows.Forms.Button();
            this.lblSoKetQua = new System.Windows.Forms.Label();
            this.dgvKetQua = new System.Windows.Forms.DataGridView();

            this.pnlTop.SuspendLayout();
            this.pnlMayTinh.SuspendLayout();
            this.pnlHoaDon.SuspendLayout();
            this.pnlActions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKetQua)).BeginInit();
            this.SuspendLayout();

            // pnlTop
            this.pnlTop.BackColor = System.Drawing.Color.White;
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Height = 85;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(1030, 85);
            this.pnlTop.TabIndex = 0;

            this.lblTieuDe.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTieuDe.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(70)))), ((int)(((byte)(229)))));
            this.lblTieuDe.Location = new System.Drawing.Point(15, 10);
            this.lblTieuDe.Size = new System.Drawing.Size(700, 25);
            this.lblTieuDe.Text = "TÌM KIẾM ĐA ĐIỀU KIỆN (YÊU CẦU 4 & 5)";

            this.rdoMayViTinh.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.rdoMayViTinh.Location = new System.Drawing.Point(20, 45);
            this.rdoMayViTinh.Size = new System.Drawing.Size(220, 25);
            this.rdoMayViTinh.Text = "💻 Tìm Máy Vi Tính (YC 4)";
            this.rdoMayViTinh.CheckedChanged += new System.EventHandler(this.rdoMayViTinh_CheckedChanged);

            this.rdoHoaDonNhap.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.rdoHoaDonNhap.Location = new System.Drawing.Point(260, 45);
            this.rdoHoaDonNhap.Size = new System.Drawing.Size(230, 25);
            this.rdoHoaDonNhap.Text = "📥 Tìm Hóa Đơn Nhập (YC 5)";
            this.rdoHoaDonNhap.CheckedChanged += new System.EventHandler(this.rdoHoaDonNhap_CheckedChanged);

            this.rdoHoaDonBan.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.rdoHoaDonBan.Location = new System.Drawing.Point(510, 45);
            this.rdoHoaDonBan.Size = new System.Drawing.Size(230, 25);
            this.rdoHoaDonBan.Text = "📤 Tìm Hóa Đơn Bán (YC 5)";
            this.rdoHoaDonBan.CheckedChanged += new System.EventHandler(this.rdoHoaDonBan_CheckedChanged);

            this.pnlTop.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblTieuDe, this.rdoMayViTinh, this.rdoHoaDonNhap, this.rdoHoaDonBan
            });

            // pnlMayTinh (YC 4)
            this.pnlMayTinh.BackColor = System.Drawing.Color.White;
            this.pnlMayTinh.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMayTinh.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlMayTinh.Height = 90;
            this.pnlMayTinh.Location = new System.Drawing.Point(0, 85);
            this.pnlMayTinh.Name = "pnlMayTinh";
            this.pnlMayTinh.Size = new System.Drawing.Size(1030, 90);
            this.pnlMayTinh.TabIndex = 1;

            this.lblMaMay.Location = new System.Drawing.Point(15, 18);
            this.lblMaMay.Size = new System.Drawing.Size(80, 20);
            this.lblMaMay.Text = "Mã máy:";
            this.txtMaMay.Location = new System.Drawing.Point(95, 15);
            this.txtMaMay.Size = new System.Drawing.Size(160, 25);

            this.lblTenMay.Location = new System.Drawing.Point(280, 18);
            this.lblTenMay.Size = new System.Drawing.Size(80, 20);
            this.lblTenMay.Text = "Tên máy:";
            this.txtTenMay.Location = new System.Drawing.Point(360, 15);
            this.txtTenMay.Size = new System.Drawing.Size(200, 25);

            this.lblLoai.Location = new System.Drawing.Point(15, 52);
            this.lblLoai.Size = new System.Drawing.Size(80, 20);
            this.lblLoai.Text = "Loại máy:";
            this.cboLoai.Location = new System.Drawing.Point(95, 49);
            this.cboLoai.Size = new System.Drawing.Size(160, 25);
            this.cboLoai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            this.lblHangSX.Location = new System.Drawing.Point(280, 52);
            this.lblHangSX.Size = new System.Drawing.Size(80, 20);
            this.lblHangSX.Text = "Hãng SX:";
            this.cboHangSX.Location = new System.Drawing.Point(360, 49);
            this.cboHangSX.Size = new System.Drawing.Size(200, 25);
            this.cboHangSX.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            this.pnlMayTinh.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblMaMay, this.txtMaMay, this.lblTenMay, this.txtTenMay,
                this.lblLoai, this.cboLoai, this.lblHangSX, this.cboHangSX
            });

            // pnlHoaDon (YC 5)
            this.pnlHoaDon.BackColor = System.Drawing.Color.White;
            this.pnlHoaDon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlHoaDon.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHoaDon.Height = 90;
            this.pnlHoaDon.Location = new System.Drawing.Point(0, 175);
            this.pnlHoaDon.Name = "pnlHoaDon";
            this.pnlHoaDon.Size = new System.Drawing.Size(1030, 90);
            this.pnlHoaDon.TabIndex = 2;

            this.lblMaHD.Location = new System.Drawing.Point(15, 18);
            this.lblMaHD.Size = new System.Drawing.Size(80, 20);
            this.lblMaHD.Text = "Mã HĐ:";
            this.txtMaHD.Location = new System.Drawing.Point(95, 15);
            this.txtMaHD.Size = new System.Drawing.Size(140, 25);

            this.lblMatHang.Location = new System.Drawing.Point(250, 18);
            this.lblMatHang.Size = new System.Drawing.Size(75, 20);
            this.lblMatHang.Text = "Mã hàng:";
            this.cboMatHang.Location = new System.Drawing.Point(330, 15);
            this.cboMatHang.Size = new System.Drawing.Size(170, 25);
            this.cboMatHang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            this.lblNhanVien.Location = new System.Drawing.Point(520, 18);
            this.lblNhanVien.Size = new System.Drawing.Size(80, 20);
            this.lblNhanVien.Text = "Nhân viên:";
            this.cboNhanVien.Location = new System.Drawing.Point(600, 15);
            this.cboNhanVien.Size = new System.Drawing.Size(170, 25);
            this.cboNhanVien.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            this.lblDoiTac.Location = new System.Drawing.Point(15, 52);
            this.lblDoiTac.Size = new System.Drawing.Size(80, 20);
            this.lblDoiTac.Text = "Đối tác:";
            this.cboNCC.Location = new System.Drawing.Point(95, 49);
            this.cboNCC.Size = new System.Drawing.Size(140, 25);
            this.cboNCC.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            this.cboKhachHang.Location = new System.Drawing.Point(95, 49);
            this.cboKhachHang.Size = new System.Drawing.Size(140, 25);
            this.cboKhachHang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            this.chkNgay.Location = new System.Drawing.Point(250, 52);
            this.chkNgay.Size = new System.Drawing.Size(85, 20);
            this.chkNgay.Text = "Từ ngày:";

            this.dtpTuNgay.Location = new System.Drawing.Point(330, 49);
            this.dtpTuNgay.Size = new System.Drawing.Size(120, 25);
            this.dtpTuNgay.Format = System.Windows.Forms.DateTimePickerFormat.Short;

            this.lblDenNgay.Location = new System.Drawing.Point(460, 52);
            this.lblDenNgay.Size = new System.Drawing.Size(70, 20);
            this.lblDenNgay.Text = "Đến ngày:";

            this.dtpDenNgay.Location = new System.Drawing.Point(535, 49);
            this.dtpDenNgay.Size = new System.Drawing.Size(120, 25);
            this.dtpDenNgay.Format = System.Windows.Forms.DateTimePickerFormat.Short;

            this.pnlHoaDon.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblMaHD, this.txtMaHD, this.lblMatHang, this.cboMatHang,
                this.lblNhanVien, this.cboNhanVien, this.lblDoiTac, this.cboNCC, this.cboKhachHang,
                this.chkNgay, this.dtpTuNgay, this.lblDenNgay, this.dtpDenNgay
            });

            // pnlActions
            this.pnlActions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(245)))));
            this.pnlActions.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlActions.Height = 50;
            this.pnlActions.Location = new System.Drawing.Point(0, 265);
            this.pnlActions.Name = "pnlActions";
            this.pnlActions.Size = new System.Drawing.Size(1030, 50);
            this.pnlActions.TabIndex = 3;

            this.btnTimKiem.Location = new System.Drawing.Point(15, 8);
            this.btnTimKiem.Size = new System.Drawing.Size(130, 34);
            this.btnTimKiem.Text = "🔍 Tìm Kiếm";
            this.btnTimKiem.Click += new System.EventHandler(this.btnTimKiem_Click);

            this.btnLamMoi.Location = new System.Drawing.Point(155, 8);
            this.btnLamMoi.Size = new System.Drawing.Size(130, 34);
            this.btnLamMoi.Text = "🔄 Làm Mới";
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);

            this.btnDong.Location = new System.Drawing.Point(295, 8);
            this.btnDong.Size = new System.Drawing.Size(100, 34);
            this.btnDong.Text = "❌ Đóng";
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);

            this.lblSoKetQua.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblSoKetQua.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblSoKetQua.Location = new System.Drawing.Point(420, 15);
            this.lblSoKetQua.Size = new System.Drawing.Size(300, 25);
            this.lblSoKetQua.Text = "Tìm thấy: 0 kết quả.";

            this.pnlActions.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.btnTimKiem, this.btnLamMoi, this.btnDong, this.lblSoKetQua
            });

            // dgvKetQua
            this.dgvKetQua.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvKetQua.Location = new System.Drawing.Point(0, 315);
            this.dgvKetQua.Name = "dgvKetQua";
            this.dgvKetQua.Size = new System.Drawing.Size(1030, 380);
            this.dgvKetQua.TabIndex = 4;

            // frmTimKiem
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1030, 695);
            this.Controls.Add(this.dgvKetQua);
            this.Controls.Add(this.pnlActions);
            this.Controls.Add(this.pnlHoaDon);
            this.Controls.Add(this.pnlMayTinh);
            this.Controls.Add(this.pnlTop);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "frmTimKiem";
            this.Text = "Tìm Kiếm Đa Điều Kiện";
            this.Load += new System.EventHandler(this.frmTimKiem_Load);
            this.pnlTop.ResumeLayout(false);
            this.pnlMayTinh.ResumeLayout(false);
            this.pnlMayTinh.PerformLayout();
            this.pnlHoaDon.ResumeLayout(false);
            this.pnlHoaDon.PerformLayout();
            this.pnlActions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvKetQua)).EndInit();
            this.ResumeLayout(false);
        }
    }
}
