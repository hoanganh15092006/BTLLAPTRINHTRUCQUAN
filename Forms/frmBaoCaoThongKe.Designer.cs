namespace QLBanMayViTinh.Forms
{
    partial class frmBaoCaoThongKe
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
            this.pnlCards = new System.Windows.Forms.Panel();
            this.card1 = new System.Windows.Forms.Panel();
            this.lblIconCard1 = new System.Windows.Forms.Label();
            this.lblCardGiaTriTon = new System.Windows.Forms.Label();
            this.lblTitleCard1 = new System.Windows.Forms.Label();
            this.lblSubCard1 = new System.Windows.Forms.Label();

            this.card2 = new System.Windows.Forms.Panel();
            this.lblIconCard2 = new System.Windows.Forms.Label();
            this.lblCardTongNhap = new System.Windows.Forms.Label();
            this.lblTitleCard2 = new System.Windows.Forms.Label();
            this.lblSubCard2 = new System.Windows.Forms.Label();

            this.card3 = new System.Windows.Forms.Panel();
            this.lblIconCard3 = new System.Windows.Forms.Label();
            this.lblCardTongBan = new System.Windows.Forms.Label();
            this.lblTitleCard3 = new System.Windows.Forms.Label();
            this.lblSubCard3 = new System.Windows.Forms.Label();

            this.card4 = new System.Windows.Forms.Panel();
            this.lblIconCard4 = new System.Windows.Forms.Label();
            this.lblCardLoiNhuan = new System.Windows.Forms.Label();
            this.lblTitleCard4 = new System.Windows.Forms.Label();
            this.lblSubCard4 = new System.Windows.Forms.Label();

            this.pnlFilterCard = new System.Windows.Forms.Panel();
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

            this.pnlGridCard = new System.Windows.Forms.Panel();
            this.lblTieuDeBaoCao = new System.Windows.Forms.Label();
            this.dgvBaoCao = new System.Windows.Forms.DataGridView();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.lblTongKet = new System.Windows.Forms.Label();

            this.pnlCards.SuspendLayout();
            this.card1.SuspendLayout();
            this.card2.SuspendLayout();
            this.card3.SuspendLayout();
            this.card4.SuspendLayout();
            this.pnlFilterCard.SuspendLayout();
            this.pnlControls.SuspendLayout();
            this.pnlThoiGian.SuspendLayout();
            this.pnlGridCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBaoCao)).BeginInit();
            this.pnlBottom.SuspendLayout();
            this.SuspendLayout();

            this.pnlCards.BackColor = System.Drawing.Color.FromArgb(241, 245, 249);
            this.pnlCards.Controls.Add(this.card4);
            this.pnlCards.Controls.Add(this.card3);
            this.pnlCards.Controls.Add(this.card2);
            this.pnlCards.Controls.Add(this.card1);
            this.pnlCards.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCards.Height = 85;
            this.pnlCards.Location = new System.Drawing.Point(0, 0);
            this.pnlCards.Name = "pnlCards";
            this.pnlCards.Padding = new System.Windows.Forms.Padding(0, 0, 0, 8);
            this.pnlCards.TabIndex = 0;

            this.card1.BackColor = System.Drawing.Color.White;
            this.card1.Location = new System.Drawing.Point(0, 0);
            this.card1.Size = new System.Drawing.Size(245, 76);
            this.card1.Controls.Add(this.lblIconCard1);
            this.card1.Controls.Add(this.lblTitleCard1);
            this.card1.Controls.Add(this.lblCardGiaTriTon);
            this.card1.Controls.Add(this.lblSubCard1);

            this.lblIconCard1.Text = "📦";
            this.lblIconCard1.Font = new System.Drawing.Font("Segoe UI", 18F);
            this.lblIconCard1.Location = new System.Drawing.Point(10, 16);
            this.lblIconCard1.Size = new System.Drawing.Size(42, 42);
            this.lblIconCard1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            this.lblTitleCard1.Text = "TỔNG GIÁ TRỊ TỒN KHO";
            this.lblTitleCard1.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblTitleCard1.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblTitleCard1.Location = new System.Drawing.Point(54, 10);
            this.lblTitleCard1.Size = new System.Drawing.Size(185, 16);

            this.lblCardGiaTriTon.Text = "0 VNĐ";
            this.lblCardGiaTriTon.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblCardGiaTriTon.ForeColor = System.Drawing.Color.FromArgb(79, 70, 229);
            this.lblCardGiaTriTon.Location = new System.Drawing.Point(54, 26);
            this.lblCardGiaTriTon.Size = new System.Drawing.Size(185, 26);

            this.lblSubCard1.Text = "Định giá theo giá nhập kho";
            this.lblSubCard1.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this.lblSubCard1.ForeColor = System.Drawing.Color.FromArgb(148, 163, 184);
            this.lblSubCard1.Location = new System.Drawing.Point(54, 52);
            this.lblSubCard1.Size = new System.Drawing.Size(185, 16);

            this.card2.BackColor = System.Drawing.Color.White;
            this.card2.Location = new System.Drawing.Point(255, 0);
            this.card2.Size = new System.Drawing.Size(245, 76);
            this.card2.Controls.Add(this.lblIconCard2);
            this.card2.Controls.Add(this.lblTitleCard2);
            this.card2.Controls.Add(this.lblCardTongNhap);
            this.card2.Controls.Add(this.lblSubCard2);

            this.lblIconCard2.Text = "📥";
            this.lblIconCard2.Font = new System.Drawing.Font("Segoe UI", 18F);
            this.lblIconCard2.Location = new System.Drawing.Point(10, 16);
            this.lblIconCard2.Size = new System.Drawing.Size(42, 42);
            this.lblIconCard2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            this.lblTitleCard2.Text = "TỔNG CHI PHÍ NHẬP";
            this.lblTitleCard2.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblTitleCard2.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblTitleCard2.Location = new System.Drawing.Point(54, 10);
            this.lblTitleCard2.Size = new System.Drawing.Size(185, 16);

            this.lblCardTongNhap.Text = "0 VNĐ";
            this.lblCardTongNhap.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblCardTongNhap.ForeColor = System.Drawing.Color.FromArgb(2, 132, 199);
            this.lblCardTongNhap.Location = new System.Drawing.Point(54, 26);
            this.lblCardTongNhap.Size = new System.Drawing.Size(185, 26);

            this.lblSubCard2.Text = "Tổng từ phiếu nhập hàng";
            this.lblSubCard2.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this.lblSubCard2.ForeColor = System.Drawing.Color.FromArgb(148, 163, 184);
            this.lblSubCard2.Location = new System.Drawing.Point(54, 52);
            this.lblSubCard2.Size = new System.Drawing.Size(185, 16);

            this.card3.BackColor = System.Drawing.Color.White;
            this.card3.Location = new System.Drawing.Point(510, 0);
            this.card3.Size = new System.Drawing.Size(245, 76);
            this.card3.Controls.Add(this.lblIconCard3);
            this.card3.Controls.Add(this.lblTitleCard3);
            this.card3.Controls.Add(this.lblCardTongBan);
            this.card3.Controls.Add(this.lblSubCard3);

            this.lblIconCard3.Text = "📤";
            this.lblIconCard3.Font = new System.Drawing.Font("Segoe UI", 18F);
            this.lblIconCard3.Location = new System.Drawing.Point(10, 16);
            this.lblIconCard3.Size = new System.Drawing.Size(42, 42);
            this.lblIconCard3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            this.lblTitleCard3.Text = "TỔNG DOANH THU BÁN";
            this.lblTitleCard3.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblTitleCard3.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblTitleCard3.Location = new System.Drawing.Point(54, 10);
            this.lblTitleCard3.Size = new System.Drawing.Size(185, 16);

            this.lblCardTongBan.Text = "0 VNĐ";
            this.lblCardTongBan.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblCardTongBan.ForeColor = System.Drawing.Color.FromArgb(16, 185, 129);
            this.lblCardTongBan.Location = new System.Drawing.Point(54, 26);
            this.lblCardTongBan.Size = new System.Drawing.Size(185, 26);

            this.lblSubCard3.Text = "Tổng từ hóa đơn bán lẻ";
            this.lblSubCard3.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this.lblSubCard3.ForeColor = System.Drawing.Color.FromArgb(148, 163, 184);
            this.lblSubCard3.Location = new System.Drawing.Point(54, 52);
            this.lblSubCard3.Size = new System.Drawing.Size(185, 16);

            this.card4.BackColor = System.Drawing.Color.White;
            this.card4.Location = new System.Drawing.Point(765, 0);
            this.card4.Size = new System.Drawing.Size(255, 76);
            this.card4.Controls.Add(this.lblIconCard4);
            this.card4.Controls.Add(this.lblTitleCard4);
            this.card4.Controls.Add(this.lblCardLoiNhuan);
            this.card4.Controls.Add(this.lblSubCard4);

            this.lblIconCard4.Text = "📈";
            this.lblIconCard4.Font = new System.Drawing.Font("Segoe UI", 18F);
            this.lblIconCard4.Location = new System.Drawing.Point(10, 16);
            this.lblIconCard4.Size = new System.Drawing.Size(42, 42);
            this.lblIconCard4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            this.lblTitleCard4.Text = "ƯỚC TÍNH LỢI NHUẬN";
            this.lblTitleCard4.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblTitleCard4.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblTitleCard4.Location = new System.Drawing.Point(54, 10);
            this.lblTitleCard4.Size = new System.Drawing.Size(195, 16);

            this.lblCardLoiNhuan.Text = "0 VNĐ";
            this.lblCardLoiNhuan.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblCardLoiNhuan.ForeColor = System.Drawing.Color.FromArgb(245, 158, 11);
            this.lblCardLoiNhuan.Location = new System.Drawing.Point(54, 26);
            this.lblCardLoiNhuan.Size = new System.Drawing.Size(195, 26);

            this.lblSubCard4.Text = "Dựa trên chênh lệch giá bán 110%";
            this.lblSubCard4.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this.lblSubCard4.ForeColor = System.Drawing.Color.FromArgb(148, 163, 184);
            this.lblSubCard4.Location = new System.Drawing.Point(54, 52);
            this.lblSubCard4.Size = new System.Drawing.Size(195, 16);

            this.pnlFilterCard.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFilterCard.Height = 105;
            this.pnlFilterCard.Location = new System.Drawing.Point(0, 85);
            this.pnlFilterCard.Name = "pnlFilterCard";
            this.pnlFilterCard.BackColor = System.Drawing.Color.White;
            this.pnlFilterCard.Padding = new System.Windows.Forms.Padding(12, 6, 12, 6);
            this.pnlFilterCard.Controls.Add(this.btnDong);
            this.pnlFilterCard.Controls.Add(this.btnXuatExcel);
            this.pnlFilterCard.Controls.Add(this.btnXemBaoCao);
            this.pnlFilterCard.Controls.Add(this.pnlThoiGian);
            this.pnlFilterCard.Controls.Add(this.pnlControls);
            this.pnlFilterCard.TabIndex = 1;

            this.pnlControls.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlControls.Height = 36;
            this.pnlControls.Location = new System.Drawing.Point(12, 6);
            this.pnlControls.Name = "pnlControls";
            this.pnlControls.Controls.Add(this.rdoTop5NV);
            this.pnlControls.Controls.Add(this.rdoBanHang);
            this.pnlControls.Controls.Add(this.rdoNhapHang);
            this.pnlControls.Controls.Add(this.rdoTonKho);

            this.rdoTonKho.Checked = true;
            this.rdoTonKho.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.rdoTonKho.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.rdoTonKho.Location = new System.Drawing.Point(4, 6);
            this.rdoTonKho.Size = new System.Drawing.Size(225, 24);
            this.rdoTonKho.Text = "📦 Sản Phẩm Tồn Kho (YC6)";
            this.rdoTonKho.UseVisualStyleBackColor = true;
            this.rdoTonKho.CheckedChanged += new System.EventHandler(this.rdoTonKho_CheckedChanged);

            this.rdoNhapHang.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.rdoNhapHang.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.rdoNhapHang.Location = new System.Drawing.Point(240, 6);
            this.rdoNhapHang.Size = new System.Drawing.Size(235, 24);
            this.rdoNhapHang.Text = "📥 HĐ Nhập Hàng Theo Quý/Năm (YC7)";
            this.rdoNhapHang.UseVisualStyleBackColor = true;
            this.rdoNhapHang.CheckedChanged += new System.EventHandler(this.rdoNhapHang_CheckedChanged);

            this.rdoBanHang.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.rdoBanHang.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.rdoBanHang.Location = new System.Drawing.Point(485, 6);
            this.rdoBanHang.Size = new System.Drawing.Size(235, 24);
            this.rdoBanHang.Text = "📤 HĐ Bán Hàng Theo Quý/Năm (YC8)";
            this.rdoBanHang.UseVisualStyleBackColor = true;
            this.rdoBanHang.CheckedChanged += new System.EventHandler(this.rdoBanHang_CheckedChanged);

            this.rdoTop5NV.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.rdoTop5NV.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.rdoTop5NV.Location = new System.Drawing.Point(730, 6);
            this.rdoTop5NV.Size = new System.Drawing.Size(260, 24);
            this.rdoTop5NV.Text = "🏆 Top 5 Nhân Viên Doanh Thu (YC9)";
            this.rdoTop5NV.UseVisualStyleBackColor = true;
            this.rdoTop5NV.CheckedChanged += new System.EventHandler(this.rdoTop5NV_CheckedChanged);

            this.pnlThoiGian.Location = new System.Drawing.Point(12, 50);
            this.pnlThoiGian.Size = new System.Drawing.Size(650, 42);
            this.pnlThoiGian.Controls.Add(this.cboNam);
            this.pnlThoiGian.Controls.Add(this.rdoLocNam);
            this.pnlThoiGian.Controls.Add(this.cboQuy);
            this.pnlThoiGian.Controls.Add(this.rdoLocQuy);
            this.pnlThoiGian.Controls.Add(this.cboThang);
            this.pnlThoiGian.Controls.Add(this.rdoLocThang);

            this.rdoLocThang.Location = new System.Drawing.Point(4, 10);
            this.rdoLocThang.Size = new System.Drawing.Size(70, 22);
            this.rdoLocThang.Text = "Tháng:";
            this.rdoLocThang.Checked = true;
            this.rdoLocThang.CheckedChanged += new System.EventHandler(this.rdoLocThoiGian_CheckedChanged);

            this.cboThang.Location = new System.Drawing.Point(76, 8);
            this.cboThang.Size = new System.Drawing.Size(70, 25);
            this.cboThang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            this.rdoLocQuy.Location = new System.Drawing.Point(165, 10);
            this.rdoLocQuy.Size = new System.Drawing.Size(55, 22);
            this.rdoLocQuy.Text = "Quý:";
            this.rdoLocQuy.CheckedChanged += new System.EventHandler(this.rdoLocThoiGian_CheckedChanged);

            this.cboQuy.Location = new System.Drawing.Point(222, 8);
            this.cboQuy.Size = new System.Drawing.Size(150, 25);
            this.cboQuy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            this.rdoLocNam.Location = new System.Drawing.Point(390, 10);
            this.rdoLocNam.Size = new System.Drawing.Size(60, 22);
            this.rdoLocNam.Text = "Năm:";
            this.rdoLocNam.CheckedChanged += new System.EventHandler(this.rdoLocThoiGian_CheckedChanged);

            this.cboNam.Location = new System.Drawing.Point(452, 8);
            this.cboNam.Size = new System.Drawing.Size(85, 25);
            this.cboNam.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            this.btnXemBaoCao.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnXemBaoCao.Location = new System.Drawing.Point(670, 52);
            this.btnXemBaoCao.Size = new System.Drawing.Size(130, 36);
            this.btnXemBaoCao.Text = "📈 Xem Báo Cáo";
            this.btnXemBaoCao.Click += new System.EventHandler(this.btnXemBaoCao_Click);

            this.btnXuatExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnXuatExcel.Location = new System.Drawing.Point(808, 52);
            this.btnXuatExcel.Size = new System.Drawing.Size(130, 36);
            this.btnXuatExcel.Text = "📊 Xuất Excel";
            this.btnXuatExcel.Click += new System.EventHandler(this.btnXuatExcel_Click);

            this.btnDong.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDong.Location = new System.Drawing.Point(945, 52);
            this.btnDong.Size = new System.Drawing.Size(75, 36);
            this.btnDong.Text = "🚪 Đóng";
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);

            this.pnlGridCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGridCard.Location = new System.Drawing.Point(0, 190);
            this.pnlGridCard.Name = "pnlGridCard";
            this.pnlGridCard.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.pnlGridCard.Controls.Add(this.dgvBaoCao);
            this.pnlGridCard.Controls.Add(this.lblTieuDeBaoCao);
            this.pnlGridCard.TabIndex = 2;

            this.lblTieuDeBaoCao.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTieuDeBaoCao.Height = 32;
            this.lblTieuDeBaoCao.BackColor = System.Drawing.Color.FromArgb(238, 242, 255);
            this.lblTieuDeBaoCao.ForeColor = System.Drawing.Color.FromArgb(67, 56, 202);
            this.lblTieuDeBaoCao.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTieuDeBaoCao.Text = "📊  BÁO CÁO THỐNG KÊ CHI TIẾT";
            this.lblTieuDeBaoCao.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTieuDeBaoCao.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);

            this.dgvBaoCao.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvBaoCao.Location = new System.Drawing.Point(0, 40);
            this.dgvBaoCao.Name = "dgvBaoCao";
            this.dgvBaoCao.Size = new System.Drawing.Size(1030, 415);
            this.dgvBaoCao.TabIndex = 1;

            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Height = 45;
            this.pnlBottom.Location = new System.Drawing.Point(0, 605);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.BackColor = System.Drawing.Color.White;
            this.pnlBottom.Padding = new System.Windows.Forms.Padding(12, 10, 12, 10);
            this.pnlBottom.Controls.Add(this.lblTongKet);
            this.pnlBottom.TabIndex = 3;

            this.lblTongKet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTongKet.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.lblTongKet.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.lblTongKet.Text = "TỔNG CỘNG: 0 Bản ghi";
            this.lblTongKet.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(241, 245, 249);
            this.ClientSize = new System.Drawing.Size(1030, 650);
            this.Controls.Add(this.pnlGridCard);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.pnlFilterCard);
            this.Controls.Add(this.pnlCards);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.Name = "frmBaoCaoThongKe";
            this.Text = "BÁO CÁO & THỐNG KÊ DOANH THU";
            this.Load += new System.EventHandler(this.frmBaoCaoThongKe_Load);

            this.pnlCards.ResumeLayout(false);
            this.card1.ResumeLayout(false);
            this.card2.ResumeLayout(false);
            this.card3.ResumeLayout(false);
            this.card4.ResumeLayout(false);
            this.pnlFilterCard.ResumeLayout(false);
            this.pnlControls.ResumeLayout(false);
            this.pnlThoiGian.ResumeLayout(false);
            this.pnlGridCard.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBaoCao)).EndInit();
            this.pnlBottom.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlCards;
        private System.Windows.Forms.Panel card1;
        private System.Windows.Forms.Label lblIconCard1;
        private System.Windows.Forms.Label lblCardGiaTriTon;
        private System.Windows.Forms.Label lblTitleCard1;
        private System.Windows.Forms.Label lblSubCard1;
        private System.Windows.Forms.Panel card2;
        private System.Windows.Forms.Label lblIconCard2;
        private System.Windows.Forms.Label lblCardTongNhap;
        private System.Windows.Forms.Label lblTitleCard2;
        private System.Windows.Forms.Label lblSubCard2;
        private System.Windows.Forms.Panel card3;
        private System.Windows.Forms.Label lblIconCard3;
        private System.Windows.Forms.Label lblCardTongBan;
        private System.Windows.Forms.Label lblTitleCard3;
        private System.Windows.Forms.Label lblSubCard3;
        private System.Windows.Forms.Panel card4;
        private System.Windows.Forms.Label lblIconCard4;
        private System.Windows.Forms.Label lblCardLoiNhuan;
        private System.Windows.Forms.Label lblTitleCard4;
        private System.Windows.Forms.Label lblSubCard4;
        private System.Windows.Forms.Panel pnlFilterCard;
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
        private System.Windows.Forms.Panel pnlGridCard;
        private System.Windows.Forms.Label lblTieuDeBaoCao;
        private System.Windows.Forms.DataGridView dgvBaoCao;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.Label lblTongKet;
    }
}
