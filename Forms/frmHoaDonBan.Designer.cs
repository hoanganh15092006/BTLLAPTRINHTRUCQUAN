namespace QLBanMayViTinh.Forms
{
    partial class frmHoaDonBan
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
            this.pnlMasterCard = new System.Windows.Forms.Panel();
            this.lblHeaderMaster = new System.Windows.Forms.Label();
            this.lblMaHDB = new System.Windows.Forms.Label();
            this.txtMaHDB = new System.Windows.Forms.TextBox();
            this.lblNgayBan = new System.Windows.Forms.Label();
            this.dtpNgayBan = new System.Windows.Forms.DateTimePicker();
            this.lblNhanVien = new System.Windows.Forms.Label();
            this.cboMaNV = new System.Windows.Forms.ComboBox();
            this.txtTenNV = new System.Windows.Forms.TextBox();
            this.lblKhachHang = new System.Windows.Forms.Label();
            this.cboMaKhach = new System.Windows.Forms.ComboBox();
            this.txtTenKhach = new System.Windows.Forms.TextBox();
            this.lblDiaChiKhach = new System.Windows.Forms.Label();
            this.txtDiaChiKhach = new System.Windows.Forms.TextBox();
            this.lblDienThoaiKhach = new System.Windows.Forms.Label();
            this.txtDienThoaiKhach = new System.Windows.Forms.TextBox();

            this.pnlDetailCard = new System.Windows.Forms.Panel();
            this.lblHeaderDetail = new System.Windows.Forms.Label();
            this.lblMaMay = new System.Windows.Forms.Label();
            this.cboMaMay = new System.Windows.Forms.ComboBox();
            this.lblTenMay = new System.Windows.Forms.Label();
            this.txtTenMay = new System.Windows.Forms.TextBox();
            this.lblTonKho = new System.Windows.Forms.Label();
            this.txtTonKho = new System.Windows.Forms.TextBox();
            this.lblSoLuong = new System.Windows.Forms.Label();
            this.txtSoLuong = new System.Windows.Forms.TextBox();
            this.lblDonGia = new System.Windows.Forms.Label();
            this.txtDonGia = new System.Windows.Forms.TextBox();
            this.lblThanhTien = new System.Windows.Forms.Label();
            this.txtThanhTien = new System.Windows.Forms.TextBox();

            this.pnlGridCard = new System.Windows.Forms.Panel();
            this.lblGridHint = new System.Windows.Forms.Label();
            this.dgvChiTiet = new System.Windows.Forms.DataGridView();

            this.pnlBottom = new System.Windows.Forms.Panel();
            this.lblTimHDB = new System.Windows.Forms.Label();
            this.cboTimHDB = new System.Windows.Forms.ComboBox();
            this.btnThemHD = new System.Windows.Forms.Button();
            this.btnLuuHD = new System.Windows.Forms.Button();
            this.btnHuyHD = new System.Windows.Forms.Button();
            this.btnInHD = new System.Windows.Forms.Button();
            this.btnDong = new System.Windows.Forms.Button();

            this.pnlTotalBox = new System.Windows.Forms.Panel();
            this.lblTongTienText = new System.Windows.Forms.Label();
            this.txtTongTien = new System.Windows.Forms.TextBox();
            this.lblBangChu = new System.Windows.Forms.Label();

            this.pnlMasterCard.SuspendLayout();
            this.pnlDetailCard.SuspendLayout();
            this.pnlGridCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTiet)).BeginInit();
            this.pnlBottom.SuspendLayout();
            this.pnlTotalBox.SuspendLayout();
            this.SuspendLayout();

            this.pnlMasterCard.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlMasterCard.Height = 120;
            this.pnlMasterCard.Location = new System.Drawing.Point(0, 0);
            this.pnlMasterCard.Name = "pnlMasterCard";
            this.pnlMasterCard.BackColor = System.Drawing.Color.White;
            this.pnlMasterCard.Padding = new System.Windows.Forms.Padding(12, 6, 12, 6);
            this.pnlMasterCard.Controls.Add(this.lblHeaderMaster);
            this.pnlMasterCard.Controls.Add(this.lblMaHDB);
            this.pnlMasterCard.Controls.Add(this.txtMaHDB);
            this.pnlMasterCard.Controls.Add(this.lblNgayBan);
            this.pnlMasterCard.Controls.Add(this.dtpNgayBan);
            this.pnlMasterCard.Controls.Add(this.lblNhanVien);
            this.pnlMasterCard.Controls.Add(this.cboMaNV);
            this.pnlMasterCard.Controls.Add(this.txtTenNV);
            this.pnlMasterCard.Controls.Add(this.lblKhachHang);
            this.pnlMasterCard.Controls.Add(this.cboMaKhach);
            this.pnlMasterCard.Controls.Add(this.txtTenKhach);
            this.pnlMasterCard.Controls.Add(this.lblDienThoaiKhach);
            this.pnlMasterCard.Controls.Add(this.txtDienThoaiKhach);
            this.pnlMasterCard.Controls.Add(this.lblDiaChiKhach);
            this.pnlMasterCard.Controls.Add(this.txtDiaChiKhach);
            this.pnlMasterCard.TabIndex = 0;

            this.lblHeaderMaster.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblHeaderMaster.Height = 24;
            this.lblHeaderMaster.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblHeaderMaster.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.lblHeaderMaster.Text = "📋  THÔNG TIN HÓA ĐƠN & KHÁCH HÀNG (MASTER)";

            this.lblMaHDB.Location = new System.Drawing.Point(14, 36);
            this.lblMaHDB.Size = new System.Drawing.Size(65, 20);
            this.lblMaHDB.Text = "Mã HDB:";
            this.txtMaHDB.Location = new System.Drawing.Point(82, 34);
            this.txtMaHDB.Size = new System.Drawing.Size(130, 25);
            this.txtMaHDB.ReadOnly = true;

            this.lblNgayBan.Location = new System.Drawing.Point(230, 36);
            this.lblNgayBan.Size = new System.Drawing.Size(75, 20);
            this.lblNgayBan.Text = "Ngày bán:";
            this.dtpNgayBan.Location = new System.Drawing.Point(310, 34);
            this.dtpNgayBan.Size = new System.Drawing.Size(145, 25);
            this.dtpNgayBan.Format = System.Windows.Forms.DateTimePickerFormat.Short;

            this.lblNhanVien.Location = new System.Drawing.Point(475, 36);
            this.lblNhanVien.Size = new System.Drawing.Size(75, 20);
            this.lblNhanVien.Text = "Nhân viên:";
            this.cboMaNV.Location = new System.Drawing.Point(555, 34);
            this.cboMaNV.Size = new System.Drawing.Size(120, 25);
            this.cboMaNV.SelectedIndexChanged += new System.EventHandler(this.cboMaNV_SelectedIndexChanged);
            this.txtTenNV.Location = new System.Drawing.Point(682, 34);
            this.txtTenNV.Size = new System.Drawing.Size(180, 25);
            this.txtTenNV.ReadOnly = true;

            this.lblKhachHang.Location = new System.Drawing.Point(14, 72);
            this.lblKhachHang.Size = new System.Drawing.Size(65, 20);
            this.lblKhachHang.Text = "Mã khách:";
            this.cboMaKhach.Location = new System.Drawing.Point(82, 70);
            this.cboMaKhach.Size = new System.Drawing.Size(130, 25);
            this.cboMaKhach.SelectedIndexChanged += new System.EventHandler(this.cboMaKhach_SelectedIndexChanged);
            this.txtTenKhach.Location = new System.Drawing.Point(218, 70);
            this.txtTenKhach.Size = new System.Drawing.Size(237, 25);
            this.txtTenKhach.ReadOnly = true;

            this.lblDienThoaiKhach.Location = new System.Drawing.Point(475, 72);
            this.lblDienThoaiKhach.Size = new System.Drawing.Size(75, 20);
            this.lblDienThoaiKhach.Text = "Điện thoại:";
            this.txtDienThoaiKhach.Location = new System.Drawing.Point(555, 70);
            this.txtDienThoaiKhach.Size = new System.Drawing.Size(120, 25);
            this.txtDienThoaiKhach.ReadOnly = true;

            this.lblDiaChiKhach.Location = new System.Drawing.Point(685, 72);
            this.lblDiaChiKhach.Size = new System.Drawing.Size(55, 20);
            this.lblDiaChiKhach.Text = "Địa chỉ:";
            this.txtDiaChiKhach.Location = new System.Drawing.Point(742, 70);
            this.txtDiaChiKhach.Size = new System.Drawing.Size(270, 25);
            this.txtDiaChiKhach.ReadOnly = true;

            this.pnlDetailCard.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlDetailCard.Height = 85;
            this.pnlDetailCard.Location = new System.Drawing.Point(0, 120);
            this.pnlDetailCard.Name = "pnlDetailCard";
            this.pnlDetailCard.BackColor = System.Drawing.Color.White;
            this.pnlDetailCard.Padding = new System.Windows.Forms.Padding(12, 4, 12, 4);
            this.pnlDetailCard.Controls.Add(this.lblHeaderDetail);
            this.pnlDetailCard.Controls.Add(this.lblMaMay);
            this.pnlDetailCard.Controls.Add(this.cboMaMay);
            this.pnlDetailCard.Controls.Add(this.lblTenMay);
            this.pnlDetailCard.Controls.Add(this.txtTenMay);
            this.pnlDetailCard.Controls.Add(this.lblTonKho);
            this.pnlDetailCard.Controls.Add(this.txtTonKho);
            this.pnlDetailCard.Controls.Add(this.lblSoLuong);
            this.pnlDetailCard.Controls.Add(this.txtSoLuong);
            this.pnlDetailCard.Controls.Add(this.lblDonGia);
            this.pnlDetailCard.Controls.Add(this.txtDonGia);
            this.pnlDetailCard.Controls.Add(this.lblThanhTien);
            this.pnlDetailCard.Controls.Add(this.txtThanhTien);
            this.pnlDetailCard.TabIndex = 1;

            this.lblHeaderDetail.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblHeaderDetail.Height = 22;
            this.lblHeaderDetail.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblHeaderDetail.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.lblHeaderDetail.Text = "🛒  CHỌN SẢN PHẨM & BÁN HÀNG (DETAIL - CHỐNG XUẤT ÂM)";

            this.lblMaMay.Location = new System.Drawing.Point(14, 38);
            this.lblMaMay.Size = new System.Drawing.Size(65, 20);
            this.lblMaMay.Text = "Chọn máy:";
            this.cboMaMay.Location = new System.Drawing.Point(82, 36);
            this.cboMaMay.Size = new System.Drawing.Size(130, 25);
            this.cboMaMay.SelectedIndexChanged += new System.EventHandler(this.cboMaMay_SelectedIndexChanged);

            this.lblTenMay.Location = new System.Drawing.Point(220, 38);
            this.lblTenMay.Size = new System.Drawing.Size(58, 20);
            this.lblTenMay.Text = "Tên máy:";
            this.txtTenMay.Location = new System.Drawing.Point(280, 36);
            this.txtTenMay.Size = new System.Drawing.Size(150, 25);
            this.txtTenMay.ReadOnly = true;

            this.lblTonKho.Location = new System.Drawing.Point(440, 38);
            this.lblTonKho.Size = new System.Drawing.Size(55, 20);
            this.lblTonKho.Text = "Tồn kho:";
            this.txtTonKho.Location = new System.Drawing.Point(498, 36);
            this.txtTonKho.Size = new System.Drawing.Size(55, 25);
            this.txtTonKho.ReadOnly = true;
            this.txtTonKho.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.txtTonKho.ForeColor = System.Drawing.Color.FromArgb(16, 185, 129);
            this.txtTonKho.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;

            this.lblSoLuong.Location = new System.Drawing.Point(565, 38);
            this.lblSoLuong.Size = new System.Drawing.Size(55, 20);
            this.lblSoLuong.Text = "Số bán:";
            this.txtSoLuong.Location = new System.Drawing.Point(622, 36);
            this.txtSoLuong.Size = new System.Drawing.Size(55, 25);
            this.txtSoLuong.TextChanged += new System.EventHandler(this.txtSoLuong_TextChanged);
            this.txtSoLuong.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSoLuong_KeyPress);

            this.lblDonGia.Location = new System.Drawing.Point(688, 38);
            this.lblDonGia.Size = new System.Drawing.Size(55, 20);
            this.lblDonGia.Text = "Giá bán:";
            this.txtDonGia.Location = new System.Drawing.Point(745, 36);
            this.txtDonGia.Size = new System.Drawing.Size(110, 25);
            this.txtDonGia.ReadOnly = true;

            this.lblThanhTien.Location = new System.Drawing.Point(865, 38);
            this.lblThanhTien.Size = new System.Drawing.Size(40, 20);
            this.lblThanhTien.Text = "Tiền:";
            this.txtThanhTien.Location = new System.Drawing.Point(908, 36);
            this.txtThanhTien.Size = new System.Drawing.Size(104, 25);
            this.txtThanhTien.ReadOnly = true;
            this.txtThanhTien.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.txtThanhTien.ForeColor = System.Drawing.Color.FromArgb(79, 70, 229);

            this.pnlGridCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGridCard.Location = new System.Drawing.Point(0, 205);
            this.pnlGridCard.Name = "pnlGridCard";
            this.pnlGridCard.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.pnlGridCard.Controls.Add(this.dgvChiTiet);
            this.pnlGridCard.Controls.Add(this.lblGridHint);
            this.pnlGridCard.TabIndex = 2;

            this.lblGridHint.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblGridHint.Height = 24;
            this.lblGridHint.BackColor = System.Drawing.Color.FromArgb(238, 242, 255);
            this.lblGridHint.ForeColor = System.Drawing.Color.FromArgb(67, 56, 202);
            this.lblGridHint.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblGridHint.Text = "💡 Gợi ý: Nhấp đúp (Double-Click) vào một dòng trên bảng để hủy mặt hàng và hoàn trả lại số lượng tồn kho tự động.";
            this.lblGridHint.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblGridHint.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);

            this.dgvChiTiet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvChiTiet.Location = new System.Drawing.Point(0, 30);
            this.dgvChiTiet.Name = "dgvChiTiet";
            this.dgvChiTiet.Size = new System.Drawing.Size(1030, 280);
            this.dgvChiTiet.TabIndex = 1;
            this.dgvChiTiet.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvChiTiet_CellDoubleClick);

            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Height = 90;
            this.pnlBottom.Location = new System.Drawing.Point(0, 560);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.BackColor = System.Drawing.Color.White;
            this.pnlBottom.Padding = new System.Windows.Forms.Padding(12, 8, 12, 8);
            this.pnlBottom.Controls.Add(this.pnlTotalBox);
            this.pnlBottom.Controls.Add(this.btnDong);
            this.pnlBottom.Controls.Add(this.btnInHD);
            this.pnlBottom.Controls.Add(this.btnHuyHD);
            this.pnlBottom.Controls.Add(this.btnLuuHD);
            this.pnlBottom.Controls.Add(this.btnThemHD);
            this.pnlBottom.Controls.Add(this.cboTimHDB);
            this.pnlBottom.Controls.Add(this.lblTimHDB);
            this.pnlBottom.TabIndex = 3;

            this.btnThemHD.Location = new System.Drawing.Point(12, 10);
            this.btnThemHD.Size = new System.Drawing.Size(105, 36);
            this.btnThemHD.Text = "➕ Tạo Mới";
            this.btnThemHD.Click += new System.EventHandler(this.btnThemHD_Click);

            this.btnLuuHD.Location = new System.Drawing.Point(125, 10);
            this.btnLuuHD.Size = new System.Drawing.Size(105, 36);
            this.btnLuuHD.Text = "💾 Lưu & Bán";
            this.btnLuuHD.Click += new System.EventHandler(this.btnLuuHD_Click);

            this.btnHuyHD.Location = new System.Drawing.Point(238, 10);
            this.btnHuyHD.Size = new System.Drawing.Size(105, 36);
            this.btnHuyHD.Text = "🗑️ Hủy Phiếu";
            this.btnHuyHD.Click += new System.EventHandler(this.btnHuyHD_Click);

            this.btnInHD.Location = new System.Drawing.Point(351, 10);
            this.btnInHD.Size = new System.Drawing.Size(105, 36);
            this.btnInHD.Text = "🖨️ In Hóa Đơn";
            this.btnInHD.Click += new System.EventHandler(this.btnInHD_Click);

            this.btnDong.Location = new System.Drawing.Point(464, 10);
            this.btnDong.Size = new System.Drawing.Size(95, 36);
            this.btnDong.Text = "🚪 Đóng";
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);

            this.lblTimHDB.Location = new System.Drawing.Point(12, 56);
            this.lblTimHDB.Size = new System.Drawing.Size(105, 20);
            this.lblTimHDB.Text = "🔍 Chọn mã HDB:";
            this.lblTimHDB.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTimHDB.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);

            this.cboTimHDB.Location = new System.Drawing.Point(125, 54);
            this.cboTimHDB.Size = new System.Drawing.Size(218, 25);
            this.cboTimHDB.DropDown += new System.EventHandler(this.cboTimHDB_DropDown);
            this.cboTimHDB.SelectedIndexChanged += new System.EventHandler(this.cboTimHDB_SelectedIndexChanged);

            this.pnlTotalBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlTotalBox.Location = new System.Drawing.Point(590, 8);
            this.pnlTotalBox.Size = new System.Drawing.Size(425, 74);
            this.pnlTotalBox.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            this.pnlTotalBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlTotalBox.Controls.Add(this.lblBangChu);
            this.pnlTotalBox.Controls.Add(this.txtTongTien);
            this.pnlTotalBox.Controls.Add(this.lblTongTienText);

            this.lblTongTienText.Location = new System.Drawing.Point(10, 10);
            this.lblTongTienText.Size = new System.Drawing.Size(120, 24);
            this.lblTongTienText.Text = "TỔNG TIỀN BÁN:";
            this.lblTongTienText.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTongTienText.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);

            this.txtTongTien.Location = new System.Drawing.Point(135, 6);
            this.txtTongTien.Size = new System.Drawing.Size(278, 28);
            this.txtTongTien.ReadOnly = true;
            this.txtTongTien.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTongTien.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.txtTongTien.ForeColor = System.Drawing.Color.FromArgb(79, 70, 229);
            this.txtTongTien.BackColor = System.Drawing.Color.White;

            this.lblBangChu.Location = new System.Drawing.Point(10, 42);
            this.lblBangChu.Size = new System.Drawing.Size(403, 22);
            this.lblBangChu.Text = "Bằng chữ: Không đồng";
            this.lblBangChu.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Italic);
            this.lblBangChu.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);

            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(241, 245, 249);
            this.ClientSize = new System.Drawing.Size(1030, 650);
            this.Controls.Add(this.pnlGridCard);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.pnlDetailCard);
            this.Controls.Add(this.pnlMasterCard);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.Name = "frmHoaDonBan";
            this.Text = "HÓA ĐƠN BÁN HÀNG & THU NGÂN (POS)";
            this.Load += new System.EventHandler(this.frmHoaDonBan_Load);

            this.pnlMasterCard.ResumeLayout(false);
            this.pnlMasterCard.PerformLayout();
            this.pnlDetailCard.ResumeLayout(false);
            this.pnlDetailCard.PerformLayout();
            this.pnlGridCard.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTiet)).EndInit();
            this.pnlBottom.ResumeLayout(false);
            this.pnlTotalBox.ResumeLayout(false);
            this.pnlTotalBox.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlMasterCard;
        private System.Windows.Forms.Label lblHeaderMaster;
        private System.Windows.Forms.Label lblMaHDB;
        private System.Windows.Forms.TextBox txtMaHDB;
        private System.Windows.Forms.Label lblNgayBan;
        private System.Windows.Forms.DateTimePicker dtpNgayBan;
        private System.Windows.Forms.Label lblNhanVien;
        private System.Windows.Forms.ComboBox cboMaNV;
        private System.Windows.Forms.TextBox txtTenNV;
        private System.Windows.Forms.Label lblKhachHang;
        private System.Windows.Forms.ComboBox cboMaKhach;
        private System.Windows.Forms.TextBox txtTenKhach;
        private System.Windows.Forms.Label lblDiaChiKhach;
        private System.Windows.Forms.TextBox txtDiaChiKhach;
        private System.Windows.Forms.Label lblDienThoaiKhach;
        private System.Windows.Forms.TextBox txtDienThoaiKhach;
        private System.Windows.Forms.Panel pnlDetailCard;
        private System.Windows.Forms.Label lblHeaderDetail;
        private System.Windows.Forms.Label lblMaMay;
        private System.Windows.Forms.ComboBox cboMaMay;
        private System.Windows.Forms.Label lblTenMay;
        private System.Windows.Forms.TextBox txtTenMay;
        private System.Windows.Forms.Label lblTonKho;
        private System.Windows.Forms.TextBox txtTonKho;
        private System.Windows.Forms.Label lblSoLuong;
        private System.Windows.Forms.TextBox txtSoLuong;
        private System.Windows.Forms.Label lblDonGia;
        private System.Windows.Forms.TextBox txtDonGia;
        private System.Windows.Forms.Label lblThanhTien;
        private System.Windows.Forms.TextBox txtThanhTien;
        private System.Windows.Forms.Panel pnlGridCard;
        private System.Windows.Forms.Label lblGridHint;
        private System.Windows.Forms.DataGridView dgvChiTiet;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.Label lblTimHDB;
        private System.Windows.Forms.ComboBox cboTimHDB;
        private System.Windows.Forms.Button btnThemHD;
        private System.Windows.Forms.Button btnLuuHD;
        private System.Windows.Forms.Button btnHuyHD;
        private System.Windows.Forms.Button btnInHD;
        private System.Windows.Forms.Button btnDong;
        private System.Windows.Forms.Panel pnlTotalBox;
        private System.Windows.Forms.Label lblTongTienText;
        private System.Windows.Forms.TextBox txtTongTien;
        private System.Windows.Forms.Label lblBangChu;
    }
}
