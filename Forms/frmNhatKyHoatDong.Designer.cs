namespace QLBanMayViTinh.Forms
{
    partial class frmNhatKyHoatDong
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

        private void InitializeComponent()
        {
            this.pnlTop = new System.Windows.Forms.Panel();
            this.lblSub = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlFilterCard = new System.Windows.Forms.Panel();
            this.btnXoaNhatKy = new System.Windows.Forms.Button();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.btnLoc = new System.Windows.Forms.Button();
            this.cboHanhDong = new System.Windows.Forms.ComboBox();
            this.lblHanhDong = new System.Windows.Forms.Label();
            this.txtNguoiDung = new System.Windows.Forms.TextBox();
            this.lblNguoiDung = new System.Windows.Forms.Label();
            this.pnlLogsCard = new System.Windows.Forms.Panel();
            this.dgvLogs = new System.Windows.Forms.DataGridView();
            this.lblLogsTitle = new System.Windows.Forms.Label();
            this.pnlAlertCard = new System.Windows.Forms.Panel();
            this.dgvAlerts = new System.Windows.Forms.DataGridView();
            this.lblAlertTitle = new System.Windows.Forms.Label();
            this.pnlTop.SuspendLayout();
            this.pnlFilterCard.SuspendLayout();
            this.pnlLogsCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLogs)).BeginInit();
            this.pnlAlertCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAlerts)).BeginInit();
            this.SuspendLayout();
            this.pnlTop.BackColor = System.Drawing.Color.White;
            this.pnlTop.Controls.Add(this.lblSub);
            this.pnlTop.Controls.Add(this.lblTitle);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(1020, 70);
            this.pnlTop.TabIndex = 0;
            this.lblSub.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSub.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblSub.Location = new System.Drawing.Point(24, 40);
            this.lblSub.Name = "lblSub";
            this.lblSub.Size = new System.Drawing.Size(700, 20);
            this.lblSub.TabIndex = 1;
            this.lblSub.Text = "Theo dõi nhật ký đăng nhập, cập nhật CSDL và giám sát tự động cảnh báo tồn kho thấp";
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.lblTitle.Location = new System.Drawing.Point(22, 10);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(500, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "NHẬT KÝ HOẠT ĐỘNG & THÔNG BÁO HỆ THỐNG";
            this.pnlFilterCard.BackColor = System.Drawing.Color.White;
            this.pnlFilterCard.Controls.Add(this.btnXoaNhatKy);
            this.pnlFilterCard.Controls.Add(this.btnLamMoi);
            this.pnlFilterCard.Controls.Add(this.btnLoc);
            this.pnlFilterCard.Controls.Add(this.cboHanhDong);
            this.pnlFilterCard.Controls.Add(this.lblHanhDong);
            this.pnlFilterCard.Controls.Add(this.txtNguoiDung);
            this.pnlFilterCard.Controls.Add(this.lblNguoiDung);
            this.pnlFilterCard.Location = new System.Drawing.Point(24, 85);
            this.pnlFilterCard.Name = "pnlFilterCard";
            this.pnlFilterCard.Size = new System.Drawing.Size(971, 75);
            this.pnlFilterCard.TabIndex = 1;
            this.btnXoaNhatKy.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnXoaNhatKy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXoaNhatKy.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnXoaNhatKy.ForeColor = System.Drawing.Color.White;
            this.btnXoaNhatKy.Location = new System.Drawing.Point(795, 22);
            this.btnXoaNhatKy.Name = "btnXoaNhatKy";
            this.btnXoaNhatKy.Size = new System.Drawing.Size(150, 34);
            this.btnXoaNhatKy.TabIndex = 6;
            this.btnXoaNhatKy.Text = "🗑️ DỌN DẸP LOG";
            this.btnXoaNhatKy.UseVisualStyleBackColor = true;
            this.btnXoaNhatKy.Click += new System.EventHandler(this.btnXoaNhatKy_Click);
            this.btnLamMoi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLamMoi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLamMoi.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnLamMoi.ForeColor = System.Drawing.Color.White;
            this.btnLamMoi.Location = new System.Drawing.Point(665, 22);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(120, 34);
            this.btnLamMoi.TabIndex = 5;
            this.btnLamMoi.Text = "LÀM MỚI";
            this.btnLamMoi.UseVisualStyleBackColor = true;
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            this.btnLoc.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLoc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoc.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnLoc.ForeColor = System.Drawing.Color.White;
            this.btnLoc.Location = new System.Drawing.Point(535, 22);
            this.btnLoc.Name = "btnLoc";
            this.btnLoc.Size = new System.Drawing.Size(120, 34);
            this.btnLoc.TabIndex = 4;
            this.btnLoc.Text = "🔍 TRA CỨU";
            this.btnLoc.UseVisualStyleBackColor = true;
            this.btnLoc.Click += new System.EventHandler(this.btnLoc_Click);
            this.cboHanhDong.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboHanhDong.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboHanhDong.FormattingEnabled = true;
            this.cboHanhDong.Items.AddRange(new object[] {
            "-- Tất cả hành động --",
            "Đăng nhập",
            "Cập nhật hồ sơ",
            "Đổi mật khẩu",
            "Thêm tài khoản",
            "Khóa/Mở khóa",
            "Reset mật khẩu",
            "Thêm khách hàng",
            "Sửa khách hàng",
            "Thêm nhà cung cấp",
            "Thêm nhân viên",
            "Cài đặt kết nối",
            "Đổi giao diện"});
            this.cboHanhDong.Location = new System.Drawing.Point(280, 27);
            this.cboHanhDong.Name = "cboHanhDong";
            this.cboHanhDong.Size = new System.Drawing.Size(235, 25);
            this.cboHanhDong.TabIndex = 3;
            this.lblHanhDong.AutoSize = true;
            this.lblHanhDong.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblHanhDong.Location = new System.Drawing.Point(280, 8);
            this.lblHanhDong.Name = "lblHanhDong";
            this.lblHanhDong.Size = new System.Drawing.Size(107, 15);
            this.lblHanhDong.TabIndex = 2;
            this.lblHanhDong.Text = "Hành Động / Loại:";
            this.txtNguoiDung.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtNguoiDung.Location = new System.Drawing.Point(25, 27);
            this.txtNguoiDung.Name = "txtNguoiDung";
            this.txtNguoiDung.Size = new System.Drawing.Size(235, 25);
            this.txtNguoiDung.TabIndex = 1;
            this.lblNguoiDung.AutoSize = true;
            this.lblNguoiDung.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblNguoiDung.Location = new System.Drawing.Point(25, 8);
            this.lblNguoiDung.Name = "lblNguoiDung";
            this.lblNguoiDung.Size = new System.Drawing.Size(126, 15);
            this.lblNguoiDung.TabIndex = 0;
            this.lblNguoiDung.Text = "Tài Khoản Thao Tác:";
            this.pnlLogsCard.BackColor = System.Drawing.Color.White;
            this.pnlLogsCard.Controls.Add(this.dgvLogs);
            this.pnlLogsCard.Controls.Add(this.lblLogsTitle);
            this.pnlLogsCard.Location = new System.Drawing.Point(24, 175);
            this.pnlLogsCard.Name = "pnlLogsCard";
            this.pnlLogsCard.Size = new System.Drawing.Size(610, 460);
            this.pnlLogsCard.TabIndex = 2;
            this.dgvLogs.AllowUserToAddRows = false;
            this.dgvLogs.AllowUserToDeleteRows = false;
            this.dgvLogs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLogs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLogs.Location = new System.Drawing.Point(0, 35);
            this.dgvLogs.Name = "dgvLogs";
            this.dgvLogs.ReadOnly = true;
            this.dgvLogs.Size = new System.Drawing.Size(610, 425);
            this.dgvLogs.TabIndex = 1;
            this.lblLogsTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblLogsTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblLogsTitle.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.lblLogsTitle.Location = new System.Drawing.Point(0, 0);
            this.lblLogsTitle.Name = "lblLogsTitle";
            this.lblLogsTitle.Padding = new System.Windows.Forms.Padding(15, 8, 0, 0);
            this.lblLogsTitle.Size = new System.Drawing.Size(610, 35);
            this.lblLogsTitle.TabIndex = 0;
            this.lblLogsTitle.Text = "📋 NHẬT KÝ THAO TÁC HỆ THỐNG (AUDIT LOGS)";
            this.pnlAlertCard.BackColor = System.Drawing.Color.White;
            this.pnlAlertCard.Controls.Add(this.dgvAlerts);
            this.pnlAlertCard.Controls.Add(this.lblAlertTitle);
            this.pnlAlertCard.Location = new System.Drawing.Point(645, 175);
            this.pnlAlertCard.Name = "pnlAlertCard";
            this.pnlAlertCard.Size = new System.Drawing.Size(350, 460);
            this.pnlAlertCard.TabIndex = 3;
            this.dgvAlerts.AllowUserToAddRows = false;
            this.dgvAlerts.AllowUserToDeleteRows = false;
            this.dgvAlerts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAlerts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAlerts.Location = new System.Drawing.Point(0, 35);
            this.dgvAlerts.Name = "dgvAlerts";
            this.dgvAlerts.ReadOnly = true;
            this.dgvAlerts.Size = new System.Drawing.Size(350, 425);
            this.dgvAlerts.TabIndex = 1;
            this.lblAlertTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblAlertTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblAlertTitle.ForeColor = System.Drawing.Color.FromArgb(239, 68, 68);
            this.lblAlertTitle.Location = new System.Drawing.Point(0, 0);
            this.lblAlertTitle.Name = "lblAlertTitle";
            this.lblAlertTitle.Padding = new System.Windows.Forms.Padding(15, 8, 0, 0);
            this.lblAlertTitle.Size = new System.Drawing.Size(350, 35);
            this.lblAlertTitle.TabIndex = 0;
            this.lblAlertTitle.Text = "⚠️ CẢNH BÁO TỒN KHO THẤP (<= 5 CÁI)";
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(241, 245, 249);
            this.ClientSize = new System.Drawing.Size(1020, 650);
            this.Controls.Add(this.pnlAlertCard);
            this.Controls.Add(this.pnlLogsCard);
            this.Controls.Add(this.pnlFilterCard);
            this.Controls.Add(this.pnlTop);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmNhatKyHoatDong";
            this.Text = "Nhật Ký Hoạt Động";
            this.Load += new System.EventHandler(this.frmNhatKyHoatDong_Load);
            this.pnlTop.ResumeLayout(false);
            this.pnlFilterCard.ResumeLayout(false);
            this.pnlFilterCard.PerformLayout();
            this.pnlLogsCard.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLogs)).EndInit();
            this.pnlAlertCard.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAlerts)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSub;
        private System.Windows.Forms.Panel pnlFilterCard;
        private System.Windows.Forms.Label lblNguoiDung;
        private System.Windows.Forms.TextBox txtNguoiDung;
        private System.Windows.Forms.Label lblHanhDong;
        private System.Windows.Forms.ComboBox cboHanhDong;
        private System.Windows.Forms.Button btnLoc;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.Button btnXoaNhatKy;
        private System.Windows.Forms.Panel pnlLogsCard;
        private System.Windows.Forms.Label lblLogsTitle;
        private System.Windows.Forms.DataGridView dgvLogs;
        private System.Windows.Forms.Panel pnlAlertCard;
        private System.Windows.Forms.Label lblAlertTitle;
        private System.Windows.Forms.DataGridView dgvAlerts;
    }
}
