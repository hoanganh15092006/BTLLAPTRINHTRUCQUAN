namespace QLBanMayViTinh.Forms
{
    partial class frmBaoCaoThongKe
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel pnlCards;
        private System.Windows.Forms.Panel card1;
        private System.Windows.Forms.Label lblCardGiaTriTon;
        private System.Windows.Forms.Label lblTitleCard1;
        private System.Windows.Forms.Panel card2;
        private System.Windows.Forms.Label lblCardTongNhap;
        private System.Windows.Forms.Label lblTitleCard2;
        private System.Windows.Forms.Panel card3;
        private System.Windows.Forms.Label lblCardTongBan;
        private System.Windows.Forms.Label lblTitleCard3;
        private System.Windows.Forms.Panel card4;
        private System.Windows.Forms.Label lblCardLoiNhuan;
        private System.Windows.Forms.Label lblTitleCard4;

        private System.Windows.Forms.Panel pnlControls;
        private System.Windows.Forms.RadioButton rdoTonKho;
        private System.Windows.Forms.RadioButton rdoNhapHang;
        private System.Windows.Forms.RadioButton rdoBanHang;
        private System.Windows.Forms.RadioButton rdoTop5NV;

        private System.Windows.Forms.Panel pnlThoiGian;
        private System.Windows.Forms.RadioButton rdoLocThang;
        private System.Windows.Forms.ComboBox cboThang;
        private System.Windows.Forms.RadioButton rdoLocQuy;
        private System.Windows.Forms.ComboBox cboQuy;
        private System.Windows.Forms.RadioButton rdoLocNam;
        private System.Windows.Forms.ComboBox cboNam;

        private System.Windows.Forms.Button btnXemBaoCao;
        private System.Windows.Forms.Button btnXuatExcel;
        private System.Windows.Forms.Button btnDong;
        private System.Windows.Forms.Label lblTieuDeBaoCao;
        private System.Windows.Forms.DataGridView dgvBaoCao;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.Label lblTongKet;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlCards = new System.Windows.Forms.Panel();
            this.card1 = new System.Windows.Forms.Panel();
            this.lblCardGiaTriTon = new System.Windows.Forms.Label();
            this.lblTitleCard1 = new System.Windows.Forms.Label();
            this.card2 = new System.Windows.Forms.Panel();
            this.lblCardTongNhap = new System.Windows.Forms.Label();
            this.lblTitleCard2 = new System.Windows.Forms.Label();
            this.card3 = new System.Windows.Forms.Panel();
            this.lblCardTongBan = new System.Windows.Forms.Label();
            this.lblTitleCard3 = new System.Windows.Forms.Label();
            this.card4 = new System.Windows.Forms.Panel();
            this.lblCardLoiNhuan = new System.Windows.Forms.Label();
            this.lblTitleCard4 = new System.Windows.Forms.Label();

            this.pnlControls = new System.Windows.Forms.Panel();
            this.rdoTonKho = new System.Windows.Forms.RadioButton();
            this.rdoNhapHang = new System.Windows.Forms.RadioButton();
            this.rdoBanHang = new System.Windows.Forms.RadioButton();
            this.rdoTop5NV = new System.Windows.Forms.RadioButton();

            this.pnlThoiGian = new System.Windows.Forms.Panel();
            this.rdoLocThang = new System.Windows.Forms.RadioButton();
            this.cboThang = new System.Windows.Forms.ComboBox();
            this.rdoLocQuy = new System.Windows.Forms.RadioButton();
            this.cboQuy = new System.Windows.Forms.ComboBox();
            this.rdoLocNam = new System.Windows.Forms.RadioButton();
            this.cboNam = new System.Windows.Forms.ComboBox();

            this.btnXemBaoCao = new System.Windows.Forms.Button();
            this.btnXuatExcel = new System.Windows.Forms.Button();
            this.btnDong = new System.Windows.Forms.Button();
            this.lblTieuDeBaoCao = new System.Windows.Forms.Label();

            this.dgvBaoCao = new System.Windows.Forms.DataGridView();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.lblTongKet = new System.Windows.Forms.Label();

            this.pnlCards.SuspendLayout();
            this.card1.SuspendLayout();
            this.card2.SuspendLayout();
            this.card3.SuspendLayout();
            this.card4.SuspendLayout();
            this.pnlControls.SuspendLayout();
            this.pnlThoiGian.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBaoCao)).BeginInit();
            this.pnlBottom.SuspendLayout();
            this.SuspendLayout();

            // pnlCards (Summary Cards)
            this.pnlCards.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(245)))));
            this.pnlCards.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCards.Height = 85;
            this.pnlCards.Location = new System.Drawing.Point(0, 0);
            this.pnlCards.Name = "pnlCards";
            this.pnlCards.Padding = new System.Windows.Forms.Padding(10, 8, 10, 8);
            this.pnlCards.Size = new System.Drawing.Size(1030, 85);
            this.pnlCards.TabIndex = 0;

            // card1: Giá trị kho hàng
            this.card1.BackColor = System.Drawing.Color.White;
            this.card1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.card1.Controls.Add(this.lblCardGiaTriTon);
            this.card1.Controls.Add(this.lblTitleCard1);
            this.card1.Location = new System.Drawing.Point(12, 8);
            this.card1.Size = new System.Drawing.Size(235, 68);
            this.lblTitleCard1.Text = "📦 TỔNG GIÁ TRỊ KHO HÀNG";
            this.lblTitleCard1.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblTitleCard1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblTitleCard1.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitleCard1.Padding = new System.Windows.Forms.Padding(8, 6, 0, 0);
            this.lblCardGiaTriTon.Text = "0 VNĐ";
            this.lblCardGiaTriTon.Font = new System.Drawing.Font("Segoe UI", 11.5F, System.Drawing.FontStyle.Bold);
            this.lblCardGiaTriTon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(70)))), ((int)(((byte)(229)))));
            this.lblCardGiaTriTon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCardGiaTriTon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // card2: Tổng tiền nhập
            this.card2.BackColor = System.Drawing.Color.White;
            this.card2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.card2.Controls.Add(this.lblCardTongNhap);
            this.card2.Controls.Add(this.lblTitleCard2);
            this.card2.Location = new System.Drawing.Point(260, 8);
            this.card2.Size = new System.Drawing.Size(235, 68);
            this.lblTitleCard2.Text = "📥 TỔNG TIỀN NHẬP HÀNG";
            this.lblTitleCard2.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblTitleCard2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblTitleCard2.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitleCard2.Padding = new System.Windows.Forms.Padding(8, 6, 0, 0);
            this.lblCardTongNhap.Text = "0 VNĐ";
            this.lblCardTongNhap.Font = new System.Drawing.Font("Segoe UI", 11.5F, System.Drawing.FontStyle.Bold);
            this.lblCardTongNhap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.lblCardTongNhap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCardTongNhap.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // card3: Tổng tiền bán
            this.card3.BackColor = System.Drawing.Color.White;
            this.card3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.card3.Controls.Add(this.lblCardTongBan);
            this.card3.Controls.Add(this.lblTitleCard3);
            this.card3.Location = new System.Drawing.Point(510, 8);
            this.card3.Size = new System.Drawing.Size(235, 68);
            this.lblTitleCard3.Text = "📤 TỔNG DOANH THU BÁN";
            this.lblTitleCard3.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblTitleCard3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblTitleCard3.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitleCard3.Padding = new System.Windows.Forms.Padding(8, 6, 0, 0);
            this.lblCardTongBan.Text = "0 VNĐ";
            this.lblCardTongBan.Font = new System.Drawing.Font("Segoe UI", 11.5F, System.Drawing.FontStyle.Bold);
            this.lblCardTongBan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.lblCardTongBan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCardTongBan.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // card4: Lợi nhuận gộp
            this.card4.BackColor = System.Drawing.Color.White;
            this.card4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.card4.Controls.Add(this.lblCardLoiNhuan);
            this.card4.Controls.Add(this.lblTitleCard4);
            this.card4.Location = new System.Drawing.Point(760, 8);
            this.card4.Size = new System.Drawing.Size(245, 68);
            this.lblTitleCard4.Text = "💰 LỢI NHUẬN GỘP";
            this.lblTitleCard4.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblTitleCard4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblTitleCard4.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitleCard4.Padding = new System.Windows.Forms.Padding(8, 6, 0, 0);
            this.lblCardLoiNhuan.Text = "0 VNĐ";
            this.lblCardLoiNhuan.Font = new System.Drawing.Font("Segoe UI", 11.5F, System.Drawing.FontStyle.Bold);
            this.lblCardLoiNhuan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCardLoiNhuan.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            this.pnlCards.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.card1, this.card2, this.card3, this.card4
            });

            // pnlControls
            this.pnlControls.BackColor = System.Drawing.Color.White;
            this.pnlControls.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlControls.Height = 115;
            this.pnlControls.Location = new System.Drawing.Point(0, 85);
            this.pnlControls.Name = "pnlControls";
            this.pnlControls.Size = new System.Drawing.Size(1030, 115);
            this.pnlControls.TabIndex = 1;

            this.rdoTonKho.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.rdoTonKho.Location = new System.Drawing.Point(15, 10);
            this.rdoTonKho.Size = new System.Drawing.Size(210, 24);
            this.rdoTonKho.Text = "📦 1. Tồn Kho Máy VT (YC 6)";
            this.rdoTonKho.CheckedChanged += new System.EventHandler(this.rdoTonKho_CheckedChanged);

            this.rdoNhapHang.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.rdoNhapHang.Location = new System.Drawing.Point(235, 10);
            this.rdoNhapHang.Size = new System.Drawing.Size(210, 24);
            this.rdoNhapHang.Text = "📥 2. HĐ Nhập Hàng (YC 7)";
            this.rdoNhapHang.CheckedChanged += new System.EventHandler(this.rdoNhapHang_CheckedChanged);

            this.rdoBanHang.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.rdoBanHang.Location = new System.Drawing.Point(455, 10);
            this.rdoBanHang.Size = new System.Drawing.Size(210, 24);
            this.rdoBanHang.Text = "📤 3. HĐ Bán Hàng (YC 8)";
            this.rdoBanHang.CheckedChanged += new System.EventHandler(this.rdoBanHang_CheckedChanged);

            this.rdoTop5NV.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.rdoTop5NV.Location = new System.Drawing.Point(675, 10);
            this.rdoTop5NV.Size = new System.Drawing.Size(240, 24);
            this.rdoTop5NV.Text = "🏆 4. Top 5 NV Bán Chạy (YC 9)";
            this.rdoTop5NV.CheckedChanged += new System.EventHandler(this.rdoTop5NV_CheckedChanged);

            // pnlThoiGian
            this.pnlThoiGian.Controls.Add(this.rdoLocThang);
            this.pnlThoiGian.Controls.Add(this.cboThang);
            this.pnlThoiGian.Controls.Add(this.rdoLocQuy);
            this.pnlThoiGian.Controls.Add(this.cboQuy);
            this.pnlThoiGian.Controls.Add(this.rdoLocNam);
            this.pnlThoiGian.Controls.Add(this.cboNam);
            this.pnlThoiGian.Location = new System.Drawing.Point(12, 40);
            this.pnlThoiGian.Size = new System.Drawing.Size(650, 35);

            this.rdoLocThang.Location = new System.Drawing.Point(5, 5);
            this.rdoLocThang.Size = new System.Drawing.Size(68, 24);
            this.rdoLocThang.Text = "Tháng:";
            this.rdoLocThang.Checked = true;
            this.cboThang.Location = new System.Drawing.Point(75, 5);
            this.cboThang.Size = new System.Drawing.Size(60, 25);
            this.cboThang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            this.rdoLocQuy.Location = new System.Drawing.Point(160, 5);
            this.rdoLocQuy.Size = new System.Drawing.Size(55, 24);
            this.rdoLocQuy.Text = "Quý:";
            this.cboQuy.Location = new System.Drawing.Point(220, 5);
            this.cboQuy.Size = new System.Drawing.Size(145, 25);
            this.cboQuy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            this.rdoLocNam.Location = new System.Drawing.Point(385, 5);
            this.rdoLocNam.Size = new System.Drawing.Size(58, 24);
            this.rdoLocNam.Text = "Năm:";
            this.cboNam.Location = new System.Drawing.Point(445, 5);
            this.cboNam.Size = new System.Drawing.Size(85, 25);
            this.cboNam.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            // Buttons
            this.btnXemBaoCao.Location = new System.Drawing.Point(675, 40);
            this.btnXemBaoCao.Size = new System.Drawing.Size(130, 35);
            this.btnXemBaoCao.Text = "📊 Xem Báo Cáo";
            this.btnXemBaoCao.Click += new System.EventHandler(this.btnXemBaoCao_Click);

            this.btnXuatExcel.Location = new System.Drawing.Point(815, 40);
            this.btnXuatExcel.Size = new System.Drawing.Size(120, 35);
            this.btnXuatExcel.Text = "📗 Xuất Excel";
            this.btnXuatExcel.Click += new System.EventHandler(this.btnXuatExcel_Click);

            this.btnDong.Location = new System.Drawing.Point(945, 40);
            this.btnDong.Size = new System.Drawing.Size(75, 35);
            this.btnDong.Text = "❌ Đóng";
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);

            this.lblTieuDeBaoCao.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTieuDeBaoCao.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblTieuDeBaoCao.Location = new System.Drawing.Point(15, 82);
            this.lblTieuDeBaoCao.Size = new System.Drawing.Size(800, 25);
            this.lblTieuDeBaoCao.Text = "BÁO CÁO DANH SÁCH MÁY VI TÍNH TỒN KHO (YÊU CẦU 6)";

            this.pnlControls.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.rdoTonKho, this.rdoNhapHang, this.rdoBanHang, this.rdoTop5NV,
                this.pnlThoiGian, this.btnXemBaoCao, this.btnXuatExcel, this.btnDong, this.lblTieuDeBaoCao
            });

            // dgvBaoCao
            this.dgvBaoCao.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvBaoCao.Location = new System.Drawing.Point(0, 200);
            this.dgvBaoCao.Name = "dgvBaoCao";
            this.dgvBaoCao.Size = new System.Drawing.Size(1030, 445);
            this.dgvBaoCao.TabIndex = 2;

            // pnlBottom
            this.pnlBottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(245)))));
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Height = 50;
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(1030, 50);
            this.pnlBottom.TabIndex = 3;

            this.lblTongKet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTongKet.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.lblTongKet.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblTongKet.Location = new System.Drawing.Point(0, 0);
            this.lblTongKet.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.lblTongKet.Size = new System.Drawing.Size(1030, 50);
            this.lblTongKet.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTongKet.Text = "Tổng số lượng:";

            this.pnlBottom.Controls.Add(this.lblTongKet);

            // frmBaoCaoThongKe
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1030, 695);
            this.Controls.Add(this.dgvBaoCao);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.pnlControls);
            this.Controls.Add(this.pnlCards);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "frmBaoCaoThongKe";
            this.Text = "Báo Cáo & Thống Kê Doanh Thu";
            this.Load += new System.EventHandler(this.frmBaoCaoThongKe_Load);
            this.pnlCards.ResumeLayout(false);
            this.card1.ResumeLayout(false);
            this.card2.ResumeLayout(false);
            this.card3.ResumeLayout(false);
            this.card4.ResumeLayout(false);
            this.pnlControls.ResumeLayout(false);
            this.pnlThoiGian.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBaoCao)).EndInit();
            this.pnlBottom.ResumeLayout(false);
            this.ResumeLayout(false);
        }
    }
}
