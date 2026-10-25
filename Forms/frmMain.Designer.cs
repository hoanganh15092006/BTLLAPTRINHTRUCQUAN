namespace QLBanMayViTinh.Forms
{
    partial class frmMain
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel pnlSidebar;
        private System.Windows.Forms.Panel pnlLogo;
        private System.Windows.Forms.Label lblLogoText;
        private System.Windows.Forms.Button btnNavMayTinh;
        private System.Windows.Forms.Button btnNavHDNhap;
        private System.Windows.Forms.Button btnNavHDBan;
        private System.Windows.Forms.Button btnNavTimKiem;
        private System.Windows.Forms.Button btnNavBaoCao;
        private System.Windows.Forms.Button btnNavAutoTest;
        private System.Windows.Forms.Button btnThoat;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlContent;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlSidebar = new System.Windows.Forms.Panel();
            this.pnlLogo = new System.Windows.Forms.Panel();
            this.lblLogoText = new System.Windows.Forms.Label();
            this.btnNavMayTinh = new System.Windows.Forms.Button();
            this.btnNavHDNhap = new System.Windows.Forms.Button();
            this.btnNavHDBan = new System.Windows.Forms.Button();
            this.btnNavTimKiem = new System.Windows.Forms.Button();
            this.btnNavBaoCao = new System.Windows.Forms.Button();
            this.btnNavAutoTest = new System.Windows.Forms.Button();
            this.btnThoat = new System.Windows.Forms.Button();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.pnlSidebar.SuspendLayout();
            this.pnlLogo.SuspendLayout();
            this.pnlHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlSidebar
            // 
            this.pnlSidebar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(27)))));
            this.pnlSidebar.Controls.Add(this.btnThoat);
            this.pnlSidebar.Controls.Add(this.btnNavAutoTest);
            this.pnlSidebar.Controls.Add(this.btnNavBaoCao);
            this.pnlSidebar.Controls.Add(this.btnNavTimKiem);
            this.pnlSidebar.Controls.Add(this.btnNavHDBan);
            this.pnlSidebar.Controls.Add(this.btnNavHDNhap);
            this.pnlSidebar.Controls.Add(this.btnNavMayTinh);
            this.pnlSidebar.Controls.Add(this.pnlLogo);
            this.pnlSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlSidebar.Location = new System.Drawing.Point(0, 0);
            this.pnlSidebar.Name = "pnlSidebar";
            this.pnlSidebar.Size = new System.Drawing.Size(250, 750);
            this.pnlSidebar.TabIndex = 0;
            // 
            // pnlLogo
            // 
            this.pnlLogo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.pnlLogo.Controls.Add(this.lblLogoText);
            this.pnlLogo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlLogo.Location = new System.Drawing.Point(0, 0);
            this.pnlLogo.Name = "pnlLogo";
            this.pnlLogo.Size = new System.Drawing.Size(250, 75);
            this.pnlLogo.TabIndex = 0;
            // 
            // lblLogoText
            // 
            this.lblLogoText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLogoText.Font = new System.Drawing.Font("Segoe UI", 12.5F, System.Drawing.FontStyle.Bold);
            this.lblLogoText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(140)))), ((int)(((byte)(248)))));
            this.lblLogoText.Location = new System.Drawing.Point(0, 0);
            this.lblLogoText.Name = "lblLogoText";
            this.lblLogoText.Size = new System.Drawing.Size(250, 75);
            this.lblLogoText.TabIndex = 0;
            this.lblLogoText.Text = "💻 PC STORE PRO\r\nQuản Lý Máy Vi Tính";
            this.lblLogoText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnNavMayTinh
            // 
            this.btnNavMayTinh.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnNavMayTinh.Height = 52;
            this.btnNavMayTinh.Location = new System.Drawing.Point(0, 75);
            this.btnNavMayTinh.Name = "btnNavMayTinh";
            this.btnNavMayTinh.Size = new System.Drawing.Size(250, 52);
            this.btnNavMayTinh.TabIndex = 1;
            this.btnNavMayTinh.Text = "🖥️  Máy Vi Tính";
            this.btnNavMayTinh.UseVisualStyleBackColor = true;
            this.btnNavMayTinh.Click += new System.EventHandler(this.btnNavMayTinh_Click);
            // 
            // btnNavHDNhap
            // 
            this.btnNavHDNhap.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnNavHDNhap.Height = 52;
            this.btnNavHDNhap.Location = new System.Drawing.Point(0, 127);
            this.btnNavHDNhap.Name = "btnNavHDNhap";
            this.btnNavHDNhap.Size = new System.Drawing.Size(250, 52);
            this.btnNavHDNhap.TabIndex = 2;
            this.btnNavHDNhap.Text = "📥  Hóa Đơn Nhập";
            this.btnNavHDNhap.UseVisualStyleBackColor = true;
            this.btnNavHDNhap.Click += new System.EventHandler(this.btnNavHDNhap_Click);
            // 
            // btnNavHDBan
            // 
            this.btnNavHDBan.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnNavHDBan.Height = 52;
            this.btnNavHDBan.Location = new System.Drawing.Point(0, 179);
            this.btnNavHDBan.Name = "btnNavHDBan";
            this.btnNavHDBan.Size = new System.Drawing.Size(250, 52);
            this.btnNavHDBan.TabIndex = 3;
            this.btnNavHDBan.Text = "📤  Hóa Đơn Bán";
            this.btnNavHDBan.UseVisualStyleBackColor = true;
            this.btnNavHDBan.Click += new System.EventHandler(this.btnNavHDBan_Click);
            // 
            // btnNavTimKiem
            // 
            this.btnNavTimKiem.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnNavTimKiem.Height = 52;
            this.btnNavTimKiem.Location = new System.Drawing.Point(0, 231);
            this.btnNavTimKiem.Name = "btnNavTimKiem";
            this.btnNavTimKiem.Size = new System.Drawing.Size(250, 52);
            this.btnNavTimKiem.TabIndex = 4;
            this.btnNavTimKiem.Text = "🔍  Tìm Kiếm (YC 4-5)";
            this.btnNavTimKiem.UseVisualStyleBackColor = true;
            this.btnNavTimKiem.Click += new System.EventHandler(this.btnNavTimKiem_Click);
            // 
            // btnNavBaoCao
            // 
            this.btnNavBaoCao.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnNavBaoCao.Height = 52;
            this.btnNavBaoCao.Location = new System.Drawing.Point(0, 283);
            this.btnNavBaoCao.Name = "btnNavBaoCao";
            this.btnNavBaoCao.Size = new System.Drawing.Size(250, 52);
            this.btnNavBaoCao.TabIndex = 5;
            this.btnNavBaoCao.Text = "📊  Báo Cáo (YC 6-9)";
            this.btnNavBaoCao.UseVisualStyleBackColor = true;
            this.btnNavBaoCao.Click += new System.EventHandler(this.btnNavBaoCao_Click);
            // 
            // btnNavAutoTest
            // 
            this.btnNavAutoTest.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnNavAutoTest.Height = 52;
            this.btnNavAutoTest.Location = new System.Drawing.Point(0, 335);
            this.btnNavAutoTest.Name = "btnNavAutoTest";
            this.btnNavAutoTest.Size = new System.Drawing.Size(250, 52);
            this.btnNavAutoTest.TabIndex = 6;
            this.btnNavAutoTest.Text = "⚡  Tự Kiểm Thử (Auto-Test)";
            this.btnNavAutoTest.UseVisualStyleBackColor = true;
            this.btnNavAutoTest.Click += new System.EventHandler(this.btnNavAutoTest_Click);
            // 
            // btnThoat
            // 
            this.btnThoat.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnThoat.Height = 52;
            this.btnThoat.Location = new System.Drawing.Point(0, 698);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(250, 52);
            this.btnThoat.TabIndex = 7;
            this.btnThoat.Text = "🚪  Thoát Ứng Dụng";
            this.btnThoat.UseVisualStyleBackColor = true;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.White;
            this.pnlHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(250, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1034, 55);
            this.pnlHeader.TabIndex = 1;
            // 
            // lblTitle
            // 
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.lblTitle.Size = new System.Drawing.Size(1032, 53);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "BẢNG ĐIỀU KHIỂN HỆ THỐNG";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlContent
            // 
            this.pnlContent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(245)))));
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(250, 55);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(1034, 695);
            this.pnlContent.TabIndex = 2;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1284, 750);
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.pnlSidebar);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.MinimumSize = new System.Drawing.Size(1200, 720);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hệ Thống Quản Lý Cửa Hàng Bán Máy Vi Tính - BTL Lập Trình Trực Quan C#";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.pnlSidebar.ResumeLayout(false);
            this.pnlLogo.ResumeLayout(false);
            this.pnlHeader.ResumeLayout(false);
            this.ResumeLayout(false);

        }
    }
}
