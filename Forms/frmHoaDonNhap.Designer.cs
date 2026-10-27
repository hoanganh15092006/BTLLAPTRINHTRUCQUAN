namespace QLBanMayViTinh.Forms
{
    partial class frmHoaDonNhap
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
            this.lblMaHDN = new System.Windows.Forms.Label();
            this.txtMaHDN = new System.Windows.Forms.TextBox();
            this.lblNgayNhap = new System.Windows.Forms.Label();
            this.dtpNgayNhap = new System.Windows.Forms.DateTimePicker();
            this.lblNhanVien = new System.Windows.Forms.Label();
            this.cboMaNV = new System.Windows.Forms.ComboBox();
            this.txtTenNV = new System.Windows.Forms.TextBox();
            this.lblNCC = new System.Windows.Forms.Label();
            this.cboMaNCC = new System.Windows.Forms.ComboBox();
            this.txtTenNCC = new System.Windows.Forms.TextBox();
            this.lblDiaChiNCC = new System.Windows.Forms.Label();
            this.txtDiaChiNCC = new System.Windows.Forms.TextBox();
            this.lblDienThoaiNCC = new System.Windows.Forms.Label();
            this.txtDienThoaiNCC = new System.Windows.Forms.TextBox();

            this.pnlDetailCard = new System.Windows.Forms.Panel();
            this.lblHeaderDetail = new System.Windows.Forms.Label();
            this.lblMaMay = new System.Windows.Forms.Label();
            this.cboMaMay = new System.Windows.Forms.ComboBox();
            this.lblTenMay = new System.Windows.Forms.Label();
            this.txtTenMay = new System.Windows.Forms.TextBox();
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
            this.lblTimHDN = new System.Windows.Forms.Label();
            this.cboTimHDN = new System.Windows.Forms.ComboBox();
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
            this.pnlMasterCard.Controls.Add(this.lblMaHDN);
            this.pnlMasterCard.Controls.Add(this.txtMaHDN);
            this.pnlMasterCard.Controls.Add(this.lblNgayNhap);
            this.pnlMasterCard.Controls.Add(this.dtpNgayNhap);
            this.pnlMasterCard.Controls.Add(this.lblNhanVien);
            this.pnlMasterCard.Controls.Add(this.cboMaNV);
            this.pnlMasterCard.Controls.Add(this.txtTenNV);
            this.pnlMasterCard.Controls.Add(this.lblNCC);
            this.pnlMasterCard.Controls.Add(this.cboMaNCC);
            this.pnlMasterCard.Controls.Add(this.txtTenNCC);
            this.pnlMasterCard.Controls.Add(this.lblDienThoaiNCC);
            this.pnlMasterCard.Controls.Add(this.txtDienThoaiNCC);
            this.pnlMasterCard.Controls.Add(this.lblDiaChiNCC);
            this.pnlMasterCard.Controls.Add(this.txtDiaChiNCC);
            this.pnlMasterCard.TabIndex = 0;

            this.lblHeaderMaster.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblHeaderMaster.Height = 24;
            this.lblHeaderMaster.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblHeaderMaster.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.lblHeaderMaster.Text = "📋  THÔNG TIN CHUNG PHIẾU NHẬP HÀNG (MASTER)";

            this.lblMaHDN.Location = new System.Drawing.Point(14, 36);
            this.lblMaHDN.Size = new System.Drawing.Size(65, 20);
            this.lblMaHDN.Text = "Mã HĐN:";
            this.txtMaHDN.Location = new System.Drawing.Point(82, 34);
            this.txtMaHDN.Size = new System.Drawing.Size(130, 25);
            this.txtMaHDN.ReadOnly = true;

            this.lblNgayNhap.Location = new System.Drawing.Point(230, 36);
            this.lblNgayNhap.Size = new System.Drawing.Size(75, 20);
            this.lblNgayNhap.Text = "Ngày nhập:";
            this.dtpNgayNhap.Location = new System.Drawing.Point(310, 34);
            this.dtpNgayNhap.Size = new System.Drawing.Size(145, 25);
            this.dtpNgayNhap.Format = System.Windows.Forms.DateTimePickerFormat.Short;

            this.lblNhanVien.Location = new System.Drawing.Point(475, 36);
            this.lblNhanVien.Size = new System.Drawing.Size(75, 20);
            this.lblNhanVien.Text = "Nhân viên:";
            this.cboMaNV.Location = new System.Drawing.Point(555, 34);
            this.cboMaNV.Size = new System.Drawing.Size(120, 25);
            this.cboMaNV.SelectedIndexChanged += new System.EventHandler(this.cboMaNV_SelectedIndexChanged);
            this.txtTenNV.Location = new System.Drawing.Point(682, 34);
            this.txtTenNV.Size = new System.Drawing.Size(180, 25);
            this.txtTenNV.ReadOnly = true;

            this.lblNCC.Location = new System.Drawing.Point(14, 72);
            this.lblNCC.Size = new System.Drawing.Size(65, 20);
            this.lblNCC.Text = "Nhà CC:";
            this.cboMaNCC.Location = new System.Drawing.Point(82, 70);
            this.cboMaNCC.Size = new System.Drawing.Size(130, 25);
            this.cboMaNCC.SelectedIndexChanged += new System.EventHandler(this.cboMaNCC_SelectedIndexChanged);
            this.txtTenNCC.Location = new System.Drawing.Point(218, 70);
            this.txtTenNCC.Size = new System.Drawing.Size(237, 25);
            this.txtTenNCC.ReadOnly = true;

            this.lblDienThoaiNCC.Location = new System.Drawing.Point(475, 72);
            this.lblDienThoaiNCC.Size = new System.Drawing.Size(75, 20);
            this.lblDienThoaiNCC.Text = "Điện thoại:";
            this.txtDienThoaiNCC.Location = new System.Drawing.Point(555, 70);
            this.txtDienThoaiNCC.Size = new System.Drawing.Size(120, 25);
            this.txtDienThoaiNCC.ReadOnly = true;

            this.lblDiaChiNCC.Location = new System.Drawing.Point(685, 72);
            this.lblDiaChiNCC.Size = new System.Drawing.Size(55, 20);
            this.lblDiaChiNCC.Text = "Địa chỉ:";
            this.txtDiaChiNCC.Location = new System.Drawing.Point(742, 70);
            this.txtDiaChiNCC.Size = new System.Drawing.Size(270, 25);
            this.txtDiaChiNCC.ReadOnly = true;

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
            this.lblHeaderDetail.Text = "📦  CHI TIẾT MẶT HÀNG NHẬP KHO (DETAIL)";

            this.lblMaMay.Location = new System.Drawing.Point(14, 38);
            this.lblMaMay.Size = new System.Drawing.Size(65, 20);
            this.lblMaMay.Text = "Chọn máy:";
            this.cboMaMay.Location = new System.Drawing.Point(82, 36);
            this.cboMaMay.Size = new System.Drawing.Size(130, 25);
            this.cboMaMay.SelectedIndexChanged += new System.EventHandler(this.cboMaMay_SelectedIndexChanged);

            this.lblTenMay.Location = new System.Drawing.Point(225, 38);
            this.lblTenMay.Size = new System.Drawing.Size(60, 20);
            this.lblTenMay.Text = "Tên máy:";
            this.txtTenMay.Location = new System.Drawing.Point(288, 36);
            this.txtTenMay.Size = new System.Drawing.Size(167, 25);
            this.txtTenMay.ReadOnly = true;

            this.lblSoLuong.Location = new System.Drawing.Point(475, 38);
            this.lblSoLuong.Size = new System.Drawing.Size(60, 20);
            this.lblSoLuong.Text = "Số lượng:";
            this.txtSoLuong.Location = new System.Drawing.Point(540, 36);
            this.txtSoLuong.Size = new System.Drawing.Size(65, 25);
            this.txtSoLuong.TextChanged += new System.EventHandler(this.txtSoLuong_TextChanged);

            this.lblDonGia.Location = new System.Drawing.Point(620, 38);
            this.lblDonGia.Size = new System.Drawing.Size(60, 20);
            this.lblDonGia.Text = "Đơn giá:";
            this.txtDonGia.Location = new System.Drawing.Point(682, 36);
            this.txtDonGia.Size = new System.Drawing.Size(120, 25);
            this.txtDonGia.TextChanged += new System.EventHandler(this.txtDonGia_TextChanged);

            this.lblThanhTien.Location = new System.Drawing.Point(815, 38);
            this.lblThanhTien.Size = new System.Drawing.Size(70, 20);
            this.lblThanhTien.Text = "Thành tiền:";
            this.txtThanhTien.Location = new System.Drawing.Point(888, 36);
            this.txtThanhTien.Size = new System.Drawing.Size(124, 25);
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
            this.lblGridHint.Text = "💡 Gợi ý: Nhấp đúp (Double-Click) vào một dòng trên bảng để xóa mặt hàng và hoàn trả lại số lượng tồn kho tự động.";
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
            this.pnlBottom.Controls.Add(this.cboTimHDN);
            this.pnlBottom.Controls.Add(this.lblTimHDN);
            this.pnlBottom.TabIndex = 3;

            this.btnThemHD.Location = new System.Drawing.Point(12, 10);
            this.btnThemHD.Size = new System.Drawing.Size(105, 36);
            this.btnThemHD.Text = "➕ Tạo Mới";
            this.btnThemHD.Click += new System.EventHandler(this.btnThemHD_Click);

            this.btnLuuHD.Location = new System.Drawing.Point(125, 10);
            this.btnLuuHD.Size = new System.Drawing.Size(105, 36);
            this.btnLuuHD.Text = "💾 Lưu Phiếu";
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

            this.lblTimHDN.Location = new System.Drawing.Point(12, 56);
            this.lblTimHDN.Size = new System.Drawing.Size(105, 20);
            this.lblTimHDN.Text = "🔍 Chọn mã HĐN:";
            this.lblTimHDN.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTimHDN.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);

            this.cboTimHDN.Location = new System.Drawing.Point(125, 54);
            this.cboTimHDN.Size = new System.Drawing.Size(218, 25);
            this.cboTimHDN.DropDown += new System.EventHandler(this.cboTimHDN_DropDown);
            this.cboTimHDN.SelectedIndexChanged += new System.EventHandler(this.cboTimHDN_SelectedIndexChanged);

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
            this.lblTongTienText.Text = "TỔNG TIỀN NHẬP:";
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
            this.Name = "frmHoaDonNhap";
            this.Text = "QUẢN LÝ HÓA ĐƠN NHẬP HÀNG";
            this.Load += new System.EventHandler(this.frmHoaDonNhap_Load);

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
        private System.Windows.Forms.Label lblMaHDN;
        private System.Windows.Forms.TextBox txtMaHDN;
        private System.Windows.Forms.Label lblNgayNhap;
        private System.Windows.Forms.DateTimePicker dtpNgayNhap;
        private System.Windows.Forms.Label lblNhanVien;
        private System.Windows.Forms.ComboBox cboMaNV;
        private System.Windows.Forms.TextBox txtTenNV;
        private System.Windows.Forms.Label lblNCC;
        private System.Windows.Forms.ComboBox cboMaNCC;
        private System.Windows.Forms.TextBox txtTenNCC;
        private System.Windows.Forms.Label lblDiaChiNCC;
        private System.Windows.Forms.TextBox txtDiaChiNCC;
        private System.Windows.Forms.Label lblDienThoaiNCC;
        private System.Windows.Forms.TextBox txtDienThoaiNCC;
        private System.Windows.Forms.Panel pnlDetailCard;
        private System.Windows.Forms.Label lblHeaderDetail;
        private System.Windows.Forms.Label lblMaMay;
        private System.Windows.Forms.ComboBox cboMaMay;
        private System.Windows.Forms.Label lblTenMay;
        private System.Windows.Forms.TextBox txtTenMay;
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
        private System.Windows.Forms.Label lblTimHDN;
        private System.Windows.Forms.ComboBox cboTimHDN;
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
