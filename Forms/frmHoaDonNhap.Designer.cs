namespace QLBanMayViTinh.Forms
{
    partial class frmHoaDonNhap
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.GroupBox grpChung;
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

        private System.Windows.Forms.GroupBox grpChiTiet;
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

        private System.Windows.Forms.DataGridView dgvChiTiet;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.Label lblTongTien;
        private System.Windows.Forms.TextBox txtTongTien;
        private System.Windows.Forms.Label lblBangChu;
        private System.Windows.Forms.Button btnThemHD;
        private System.Windows.Forms.Button btnLuuHD;
        private System.Windows.Forms.Button btnHuyHD;
        private System.Windows.Forms.Button btnInHD;
        private System.Windows.Forms.Button btnDong;
        private System.Windows.Forms.ComboBox cboTimHDN;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.grpChung = new System.Windows.Forms.GroupBox();
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

            this.grpChiTiet = new System.Windows.Forms.GroupBox();
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

            this.dgvChiTiet = new System.Windows.Forms.DataGridView();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.lblTongTien = new System.Windows.Forms.Label();
            this.txtTongTien = new System.Windows.Forms.TextBox();
            this.lblBangChu = new System.Windows.Forms.Label();
            this.btnThemHD = new System.Windows.Forms.Button();
            this.btnLuuHD = new System.Windows.Forms.Button();
            this.btnHuyHD = new System.Windows.Forms.Button();
            this.btnInHD = new System.Windows.Forms.Button();
            this.btnDong = new System.Windows.Forms.Button();
            this.cboTimHDN = new System.Windows.Forms.ComboBox();

            this.grpChung.SuspendLayout();
            this.grpChiTiet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTiet)).BeginInit();
            this.pnlBottom.SuspendLayout();
            this.SuspendLayout();

            // grpChung
            this.grpChung.BackColor = System.Drawing.Color.White;
            this.grpChung.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpChung.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.grpChung.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.grpChung.Location = new System.Drawing.Point(0, 0);
            this.grpChung.Name = "grpChung";
            this.grpChung.Size = new System.Drawing.Size(1030, 130);
            this.grpChung.TabIndex = 0;
            this.grpChung.TabStop = false;
            this.grpChung.Text = "THÔNG TIN CHUNG HÓA ĐƠN NHẬP";

            this.lblMaHDN.Location = new System.Drawing.Point(15, 28);
            this.lblMaHDN.Size = new System.Drawing.Size(95, 20);
            this.lblMaHDN.Text = "Mã HĐ nhập:";
            this.txtMaHDN.Location = new System.Drawing.Point(115, 25);
            this.txtMaHDN.Size = new System.Drawing.Size(140, 25);
            this.txtMaHDN.ReadOnly = true;

            this.lblNgayNhap.Location = new System.Drawing.Point(15, 60);
            this.lblNgayNhap.Size = new System.Drawing.Size(95, 20);
            this.lblNgayNhap.Text = "Ngày nhập:";
            this.dtpNgayNhap.Location = new System.Drawing.Point(115, 57);
            this.dtpNgayNhap.Size = new System.Drawing.Size(140, 25);
            this.dtpNgayNhap.Format = System.Windows.Forms.DateTimePickerFormat.Short;

            this.lblNhanVien.Location = new System.Drawing.Point(15, 92);
            this.lblNhanVien.Size = new System.Drawing.Size(95, 20);
            this.lblNhanVien.Text = "Nhân viên:";
            this.cboMaNV.Location = new System.Drawing.Point(115, 89);
            this.cboMaNV.Size = new System.Drawing.Size(140, 25);
            this.cboMaNV.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMaNV.SelectedIndexChanged += new System.EventHandler(this.cboMaNV_SelectedIndexChanged);

            this.txtTenNV.Location = new System.Drawing.Point(265, 89);
            this.txtTenNV.Size = new System.Drawing.Size(180, 25);
            this.txtTenNV.ReadOnly = true;

            this.lblNCC.Location = new System.Drawing.Point(490, 28);
            this.lblNCC.Size = new System.Drawing.Size(95, 20);
            this.lblNCC.Text = "Nhà cung cấp:";
            this.cboMaNCC.Location = new System.Drawing.Point(590, 25);
            this.cboMaNCC.Size = new System.Drawing.Size(140, 25);
            this.cboMaNCC.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMaNCC.SelectedIndexChanged += new System.EventHandler(this.cboMaNCC_SelectedIndexChanged);

            this.txtTenNCC.Location = new System.Drawing.Point(740, 25);
            this.txtTenNCC.Size = new System.Drawing.Size(260, 25);
            this.txtTenNCC.ReadOnly = true;

            this.lblDiaChiNCC.Location = new System.Drawing.Point(490, 60);
            this.lblDiaChiNCC.Size = new System.Drawing.Size(95, 20);
            this.lblDiaChiNCC.Text = "Địa chỉ:";
            this.txtDiaChiNCC.Location = new System.Drawing.Point(590, 57);
            this.txtDiaChiNCC.Size = new System.Drawing.Size(410, 25);
            this.txtDiaChiNCC.ReadOnly = true;

            this.lblDienThoaiNCC.Location = new System.Drawing.Point(490, 92);
            this.lblDienThoaiNCC.Size = new System.Drawing.Size(95, 20);
            this.lblDienThoaiNCC.Text = "Điện thoại:";
            this.txtDienThoaiNCC.Location = new System.Drawing.Point(590, 89);
            this.txtDienThoaiNCC.Size = new System.Drawing.Size(200, 25);
            this.txtDienThoaiNCC.ReadOnly = true;

            this.grpChung.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblMaHDN, this.txtMaHDN, this.lblNgayNhap, this.dtpNgayNhap,
                this.lblNhanVien, this.cboMaNV, this.txtTenNV,
                this.lblNCC, this.cboMaNCC, this.txtTenNCC,
                this.lblDiaChiNCC, this.txtDiaChiNCC, this.lblDienThoaiNCC, this.txtDienThoaiNCC
            });

            // grpChiTiet
            this.grpChiTiet.BackColor = System.Drawing.Color.White;
            this.grpChiTiet.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpChiTiet.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.grpChiTiet.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.grpChiTiet.Location = new System.Drawing.Point(0, 130);
            this.grpChiTiet.Name = "grpChiTiet";
            this.grpChiTiet.Size = new System.Drawing.Size(1030, 95);
            this.grpChiTiet.TabIndex = 1;
            this.grpChiTiet.TabStop = false;
            this.grpChiTiet.Text = "CHI TIẾT MẶT HÀNG NHẬP KHO";

            this.lblMaMay.Location = new System.Drawing.Point(15, 28);
            this.lblMaMay.Size = new System.Drawing.Size(75, 20);
            this.lblMaMay.Text = "Mã máy:";
            this.cboMaMay.Location = new System.Drawing.Point(95, 25);
            this.cboMaMay.Size = new System.Drawing.Size(140, 25);
            this.cboMaMay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMaMay.SelectedIndexChanged += new System.EventHandler(this.cboMaMay_SelectedIndexChanged);

            this.lblTenMay.Location = new System.Drawing.Point(245, 28);
            this.lblTenMay.Size = new System.Drawing.Size(75, 20);
            this.lblTenMay.Text = "Tên máy:";
            this.txtTenMay.Location = new System.Drawing.Point(325, 25);
            this.txtTenMay.Size = new System.Drawing.Size(200, 25);
            this.txtTenMay.ReadOnly = true;

            this.lblSoLuong.Location = new System.Drawing.Point(15, 60);
            this.lblSoLuong.Size = new System.Drawing.Size(75, 20);
            this.lblSoLuong.Text = "Số lượng:";
            this.txtSoLuong.Location = new System.Drawing.Point(95, 57);
            this.txtSoLuong.Size = new System.Drawing.Size(140, 25);
            this.txtSoLuong.TextChanged += new System.EventHandler(this.txtSoLuong_TextChanged);
            this.txtSoLuong.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSoLuong_KeyPress);

            this.lblDonGia.Location = new System.Drawing.Point(245, 60);
            this.lblDonGia.Size = new System.Drawing.Size(75, 20);
            this.lblDonGia.Text = "Đơn giá:";
            this.txtDonGia.Location = new System.Drawing.Point(325, 57);
            this.txtDonGia.Size = new System.Drawing.Size(140, 25);
            this.txtDonGia.TextChanged += new System.EventHandler(this.txtDonGia_TextChanged);
            this.txtDonGia.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDonGia_KeyPress);

            this.lblThanhTien.Location = new System.Drawing.Point(480, 60);
            this.lblThanhTien.Size = new System.Drawing.Size(85, 20);
            this.lblThanhTien.Text = "Thành tiền:";
            this.txtThanhTien.Location = new System.Drawing.Point(570, 57);
            this.txtThanhTien.Size = new System.Drawing.Size(160, 25);
            this.txtThanhTien.ReadOnly = true;

            this.grpChiTiet.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblMaMay, this.cboMaMay, this.lblTenMay, this.txtTenMay,
                this.lblSoLuong, this.txtSoLuong, this.lblDonGia, this.txtDonGia,
                this.lblThanhTien, this.txtThanhTien
            });

            // dgvChiTiet
            this.dgvChiTiet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvChiTiet.Location = new System.Drawing.Point(0, 225);
            this.dgvChiTiet.Name = "dgvChiTiet";
            this.dgvChiTiet.Size = new System.Drawing.Size(1030, 360);
            this.dgvChiTiet.TabIndex = 2;
            this.dgvChiTiet.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvChiTiet_CellDoubleClick);

            // pnlBottom
            this.pnlBottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(245)))));
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Height = 110;
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(1030, 110);
            this.pnlBottom.TabIndex = 3;

            this.lblTongTien.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.lblTongTien.Location = new System.Drawing.Point(620, 12);
            this.lblTongTien.Size = new System.Drawing.Size(140, 25);
            this.lblTongTien.Text = "Tổng tiền HĐN:";

            this.txtTongTien.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.txtTongTien.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.txtTongTien.Location = new System.Drawing.Point(760, 9);
            this.txtTongTien.Size = new System.Drawing.Size(240, 28);
            this.txtTongTien.ReadOnly = true;
            this.txtTongTien.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;

            this.lblBangChu.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Italic);
            this.lblBangChu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblBangChu.Location = new System.Drawing.Point(15, 12);
            this.lblBangChu.Size = new System.Drawing.Size(590, 25);
            this.lblBangChu.Text = "Bằng chữ: Không đồng";

            this.btnThemHD.Location = new System.Drawing.Point(15, 55);
            this.btnThemHD.Size = new System.Drawing.Size(140, 38);
            this.btnThemHD.Text = "➕ Thêm HĐ";
            this.btnThemHD.Click += new System.EventHandler(this.btnThemHD_Click);

            this.btnLuuHD.Location = new System.Drawing.Point(165, 55);
            this.btnLuuHD.Size = new System.Drawing.Size(140, 38);
            this.btnLuuHD.Text = "💾 Lưu & Nhập";
            this.btnLuuHD.Click += new System.EventHandler(this.btnLuuHD_Click);

            this.btnHuyHD.Location = new System.Drawing.Point(315, 55);
            this.btnHuyHD.Size = new System.Drawing.Size(120, 38);
            this.btnHuyHD.Text = "🗑️ Hủy HĐ";
            this.btnHuyHD.Click += new System.EventHandler(this.btnHuyHD_Click);

            this.btnInHD.Location = new System.Drawing.Point(445, 55);
            this.btnInHD.Size = new System.Drawing.Size(130, 38);
            this.btnInHD.Text = "🖨️ In Hóa Đơn";

            this.btnDong.Location = new System.Drawing.Point(585, 55);
            this.btnDong.Size = new System.Drawing.Size(110, 38);
            this.btnDong.Text = "❌ Đóng";
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);

            this.pnlBottom.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblTongTien, this.txtTongTien, this.lblBangChu,
                this.btnThemHD, this.btnLuuHD, this.btnHuyHD, this.btnInHD, this.btnDong
            });

            // frmHoaDonNhap
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1030, 695);
            this.Controls.Add(this.dgvChiTiet);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.grpChiTiet);
            this.Controls.Add(this.grpChung);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "frmHoaDonNhap";
            this.Text = "Quản Lý Hóa Đơn Nhập Hàng";
            this.Load += new System.EventHandler(this.frmHoaDonNhap_Load);
            this.grpChung.ResumeLayout(false);
            this.grpChung.PerformLayout();
            this.grpChiTiet.ResumeLayout(false);
            this.grpChiTiet.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTiet)).EndInit();
            this.pnlBottom.ResumeLayout(false);
            this.pnlBottom.PerformLayout();
            this.ResumeLayout(false);
        }
    }
}
