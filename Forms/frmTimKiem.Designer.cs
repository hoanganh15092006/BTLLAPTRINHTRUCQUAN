namespace QLBanMayViTinh.Forms
{
    partial class frmTimKiem
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
            this.pnlTopCard = new System.Windows.Forms.Panel();
            this.lblTieuDe = new System.Windows.Forms.Label();
            this.lblSubTieuDe = new System.Windows.Forms.Label();
            this.pnlRadioSwitch = new System.Windows.Forms.Panel();
            this.rdoMayViTinh = new System.Windows.Forms.RadioButton();
            this.rdoHoaDonNhap = new System.Windows.Forms.RadioButton();
            this.rdoHoaDonBan = new System.Windows.Forms.RadioButton();

            this.pnlFilterCard = new System.Windows.Forms.Panel();
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

            this.pnlGridCard = new System.Windows.Forms.Panel();
            this.dgvKetQua = new System.Windows.Forms.DataGridView();

            this.pnlTopCard.SuspendLayout();
            this.pnlRadioSwitch.SuspendLayout();
            this.pnlFilterCard.SuspendLayout();
            this.pnlMayTinh.SuspendLayout();
            this.pnlHoaDon.SuspendLayout();
            this.pnlActions.SuspendLayout();
            this.pnlGridCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKetQua)).BeginInit();
            this.SuspendLayout();

            this.pnlTopCard.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTopCard.Height = 85;
            this.pnlTopCard.Location = new System.Drawing.Point(0, 0);
            this.pnlTopCard.Name = "pnlTopCard";
            this.pnlTopCard.BackColor = System.Drawing.Color.White;
            this.pnlTopCard.Padding = new System.Windows.Forms.Padding(16, 10, 16, 6);
            this.pnlTopCard.Controls.Add(this.pnlRadioSwitch);
            this.pnlTopCard.Controls.Add(this.lblSubTieuDe);
            this.pnlTopCard.Controls.Add(this.lblTieuDe);
            this.pnlTopCard.TabIndex = 0;

            this.lblTieuDe.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTieuDe.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.lblTieuDe.Location = new System.Drawing.Point(14, 8);
            this.lblTieuDe.Size = new System.Drawing.Size(460, 24);
            this.lblTieuDe.Text = "TÌM KIẾM SẢN PHẨM MÁY VI TÍNH (YÊU CẦU 4)";

            this.lblSubTieuDe.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblSubTieuDe.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblSubTieuDe.Location = new System.Drawing.Point(16, 32);
            this.lblSubTieuDe.Size = new System.Drawing.Size(460, 18);
            this.lblSubTieuDe.Text = "Chọn danh mục cần tra cứu bên dưới và nhập tiêu chí lọc thông minh";

            this.pnlRadioSwitch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlRadioSwitch.Location = new System.Drawing.Point(500, 12);
            this.pnlRadioSwitch.Size = new System.Drawing.Size(515, 45);
            this.pnlRadioSwitch.BackColor = System.Drawing.Color.FromArgb(241, 245, 249);
            this.pnlRadioSwitch.Controls.Add(this.rdoHoaDonBan);
            this.pnlRadioSwitch.Controls.Add(this.rdoHoaDonNhap);
            this.pnlRadioSwitch.Controls.Add(this.rdoMayViTinh);

            this.rdoMayViTinh.Checked = true;
            this.rdoMayViTinh.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.rdoMayViTinh.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.rdoMayViTinh.Location = new System.Drawing.Point(10, 10);
            this.rdoMayViTinh.Size = new System.Drawing.Size(160, 24);
            this.rdoMayViTinh.Text = "🖥️  Máy Vi Tính (YC4)";
            this.rdoMayViTinh.UseVisualStyleBackColor = true;
            this.rdoMayViTinh.CheckedChanged += new System.EventHandler(this.rdoMayViTinh_CheckedChanged);

            this.rdoHoaDonNhap.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.rdoHoaDonNhap.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.rdoHoaDonNhap.Location = new System.Drawing.Point(180, 10);
            this.rdoHoaDonNhap.Size = new System.Drawing.Size(155, 24);
            this.rdoHoaDonNhap.Text = "📥  Hóa Đơn Nhập";
            this.rdoHoaDonNhap.UseVisualStyleBackColor = true;
            this.rdoHoaDonNhap.CheckedChanged += new System.EventHandler(this.rdoHoaDonNhap_CheckedChanged);

            this.rdoHoaDonBan.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.rdoHoaDonBan.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.rdoHoaDonBan.Location = new System.Drawing.Point(345, 10);
            this.rdoHoaDonBan.Size = new System.Drawing.Size(165, 24);
            this.rdoHoaDonBan.Text = "📤  Hóa Đơn Bán (YC5)";
            this.rdoHoaDonBan.UseVisualStyleBackColor = true;
            this.rdoHoaDonBan.CheckedChanged += new System.EventHandler(this.rdoHoaDonBan_CheckedChanged);

            this.pnlFilterCard.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFilterCard.Height = 90;
            this.pnlFilterCard.Location = new System.Drawing.Point(0, 85);
            this.pnlFilterCard.Name = "pnlFilterCard";
            this.pnlFilterCard.BackColor = System.Drawing.Color.White;
            this.pnlFilterCard.Padding = new System.Windows.Forms.Padding(12, 6, 12, 6);
            this.pnlFilterCard.Controls.Add(this.pnlMayTinh);
            this.pnlFilterCard.Controls.Add(this.pnlHoaDon);
            this.pnlFilterCard.TabIndex = 1;

            this.pnlMayTinh.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMayTinh.Location = new System.Drawing.Point(0, 0);
            this.pnlMayTinh.Name = "pnlMayTinh";
            this.pnlMayTinh.Controls.Add(this.cboHangSX);
            this.pnlMayTinh.Controls.Add(this.lblHangSX);
            this.pnlMayTinh.Controls.Add(this.cboLoai);
            this.pnlMayTinh.Controls.Add(this.lblLoai);
            this.pnlMayTinh.Controls.Add(this.txtTenMay);
            this.pnlMayTinh.Controls.Add(this.lblTenMay);
            this.pnlMayTinh.Controls.Add(this.txtMaMay);
            this.pnlMayTinh.Controls.Add(this.lblMaMay);

            this.lblMaMay.Location = new System.Drawing.Point(14, 25);
            this.lblMaMay.Size = new System.Drawing.Size(65, 20);
            this.lblMaMay.Text = "Mã máy:";
            this.txtMaMay.Location = new System.Drawing.Point(82, 23);
            this.txtMaMay.Size = new System.Drawing.Size(140, 25);

            this.lblTenMay.Location = new System.Drawing.Point(245, 25);
            this.lblTenMay.Size = new System.Drawing.Size(65, 20);
            this.lblTenMay.Text = "Tên máy:";
            this.txtTenMay.Location = new System.Drawing.Point(315, 23);
            this.txtTenMay.Size = new System.Drawing.Size(180, 25);

            this.lblLoai.Location = new System.Drawing.Point(520, 25);
            this.lblLoai.Size = new System.Drawing.Size(65, 20);
            this.lblLoai.Text = "Loại máy:";
            this.cboLoai.Location = new System.Drawing.Point(590, 23);
            this.cboLoai.Size = new System.Drawing.Size(170, 25);

            this.lblHangSX.Location = new System.Drawing.Point(785, 25);
            this.lblHangSX.Size = new System.Drawing.Size(65, 20);
            this.lblHangSX.Text = "Hãng SX:";
            this.cboHangSX.Location = new System.Drawing.Point(855, 23);
            this.cboHangSX.Size = new System.Drawing.Size(155, 25);

            this.pnlHoaDon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlHoaDon.Location = new System.Drawing.Point(0, 0);
            this.pnlHoaDon.Name = "pnlHoaDon";
            this.pnlHoaDon.Controls.Add(this.dtpDenNgay);
            this.pnlHoaDon.Controls.Add(this.lblDenNgay);
            this.pnlHoaDon.Controls.Add(this.dtpTuNgay);
            this.pnlHoaDon.Controls.Add(this.chkNgay);
            this.pnlHoaDon.Controls.Add(this.cboKhachHang);
            this.pnlHoaDon.Controls.Add(this.cboNCC);
            this.pnlHoaDon.Controls.Add(this.lblDoiTac);
            this.pnlHoaDon.Controls.Add(this.cboNhanVien);
            this.pnlHoaDon.Controls.Add(this.lblNhanVien);
            this.pnlHoaDon.Controls.Add(this.cboMatHang);
            this.pnlHoaDon.Controls.Add(this.lblMatHang);
            this.pnlHoaDon.Controls.Add(this.txtMaHD);
            this.pnlHoaDon.Controls.Add(this.lblMaHD);

            this.lblMaHD.Location = new System.Drawing.Point(14, 12);
            this.lblMaHD.Size = new System.Drawing.Size(65, 20);
            this.lblMaHD.Text = "Mã HĐ:";
            this.txtMaHD.Location = new System.Drawing.Point(82, 10);
            this.txtMaHD.Size = new System.Drawing.Size(130, 25);

            this.lblMatHang.Location = new System.Drawing.Point(235, 12);
            this.lblMatHang.Size = new System.Drawing.Size(70, 20);
            this.lblMatHang.Text = "Mặt hàng:";
            this.cboMatHang.Location = new System.Drawing.Point(310, 10);
            this.cboMatHang.Size = new System.Drawing.Size(185, 25);

            this.lblNhanVien.Location = new System.Drawing.Point(520, 12);
            this.lblNhanVien.Size = new System.Drawing.Size(70, 20);
            this.lblNhanVien.Text = "Nhân viên:";
            this.cboNhanVien.Location = new System.Drawing.Point(595, 10);
            this.cboNhanVien.Size = new System.Drawing.Size(165, 25);

            this.lblDoiTac.Location = new System.Drawing.Point(785, 12);
            this.lblDoiTac.Size = new System.Drawing.Size(65, 20);
            this.lblDoiTac.Text = "Khách hàng:";

            this.cboNCC.Location = new System.Drawing.Point(855, 10);
            this.cboNCC.Size = new System.Drawing.Size(155, 25);

            this.cboKhachHang.Location = new System.Drawing.Point(855, 10);
            this.cboKhachHang.Size = new System.Drawing.Size(155, 25);

            this.chkNgay.Location = new System.Drawing.Point(14, 48);
            this.chkNgay.Size = new System.Drawing.Size(110, 24);
            this.chkNgay.Text = "📅 Lọc từ ngày:";
            this.chkNgay.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);

            this.dtpTuNgay.Location = new System.Drawing.Point(130, 48);
            this.dtpTuNgay.Size = new System.Drawing.Size(135, 25);
            this.dtpTuNgay.Format = System.Windows.Forms.DateTimePickerFormat.Short;

            this.lblDenNgay.Location = new System.Drawing.Point(280, 50);
            this.lblDenNgay.Size = new System.Drawing.Size(65, 20);
            this.lblDenNgay.Text = "đến ngày:";

            this.dtpDenNgay.Location = new System.Drawing.Point(350, 48);
            this.dtpDenNgay.Size = new System.Drawing.Size(145, 25);
            this.dtpDenNgay.Format = System.Windows.Forms.DateTimePickerFormat.Short;

            this.pnlActions.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlActions.Height = 52;
            this.pnlActions.Location = new System.Drawing.Point(0, 175);
            this.pnlActions.Name = "pnlActions";
            this.pnlActions.BackColor = System.Drawing.Color.White;
            this.pnlActions.Padding = new System.Windows.Forms.Padding(12, 8, 12, 8);
            this.pnlActions.Controls.Add(this.lblSoKetQua);
            this.pnlActions.Controls.Add(this.btnDong);
            this.pnlActions.Controls.Add(this.btnLamMoi);
            this.pnlActions.Controls.Add(this.btnTimKiem);
            this.pnlActions.TabIndex = 2;

            this.btnTimKiem.Location = new System.Drawing.Point(12, 8);
            this.btnTimKiem.Size = new System.Drawing.Size(135, 36);
            this.btnTimKiem.Text = "🔍 Bắt Đầu Tìm";
            this.btnTimKiem.Click += new System.EventHandler(this.btnTimKiem_Click);

            this.btnLamMoi.Location = new System.Drawing.Point(160, 8);
            this.btnLamMoi.Size = new System.Drawing.Size(120, 36);
            this.btnLamMoi.Text = "🔄 Đặt Lại";
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);

            this.btnDong.Location = new System.Drawing.Point(292, 8);
            this.btnDong.Size = new System.Drawing.Size(95, 36);
            this.btnDong.Text = "🚪 Đóng";
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);

            this.lblSoKetQua.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSoKetQua.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblSoKetQua.ForeColor = System.Drawing.Color.FromArgb(79, 70, 229);
            this.lblSoKetQua.Location = new System.Drawing.Point(620, 14);
            this.lblSoKetQua.Size = new System.Drawing.Size(395, 24);
            this.lblSoKetQua.Text = "🎯 Nhấn [Bắt Đầu Tìm] để xem kết quả";
            this.lblSoKetQua.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            this.pnlGridCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGridCard.Location = new System.Drawing.Point(0, 227);
            this.pnlGridCard.Name = "pnlGridCard";
            this.pnlGridCard.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.pnlGridCard.Controls.Add(this.dgvKetQua);
            this.pnlGridCard.TabIndex = 3;

            this.dgvKetQua.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvKetQua.Location = new System.Drawing.Point(0, 8);
            this.dgvKetQua.Name = "dgvKetQua";
            this.dgvKetQua.Size = new System.Drawing.Size(1030, 423);
            this.dgvKetQua.TabIndex = 0;

            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(241, 245, 249);
            this.ClientSize = new System.Drawing.Size(1030, 650);
            this.Controls.Add(this.pnlGridCard);
            this.Controls.Add(this.pnlActions);
            this.Controls.Add(this.pnlFilterCard);
            this.Controls.Add(this.pnlTopCard);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.Name = "frmTimKiem";
            this.Text = "TRA CỨU & TÌM KIẾM ĐA TIÊU CHÍ";
            this.Load += new System.EventHandler(this.frmTimKiem_Load);

            this.pnlTopCard.ResumeLayout(false);
            this.pnlRadioSwitch.ResumeLayout(false);
            this.pnlFilterCard.ResumeLayout(false);
            this.pnlMayTinh.ResumeLayout(false);
            this.pnlMayTinh.PerformLayout();
            this.pnlHoaDon.ResumeLayout(false);
            this.pnlHoaDon.PerformLayout();
            this.pnlActions.ResumeLayout(false);
            this.pnlGridCard.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvKetQua)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlTopCard;
        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.Label lblSubTieuDe;
        private System.Windows.Forms.Panel pnlRadioSwitch;
        private System.Windows.Forms.RadioButton rdoMayViTinh;
        private System.Windows.Forms.RadioButton rdoHoaDonNhap;
        private System.Windows.Forms.RadioButton rdoHoaDonBan;
        private System.Windows.Forms.Panel pnlFilterCard;
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
        private System.Windows.Forms.Panel pnlGridCard;
        private System.Windows.Forms.DataGridView dgvKetQua;
    }
}
