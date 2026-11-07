using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using QLBanMayViTinh.Classes;
using QLBanMayViTinh.Testing;

namespace QLBanMayViTinh.Forms
{
    public partial class frmMain : Form
    {
        private Form currentChildForm = null;
        private Button currentActiveBtn = null;

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            if (this.DesignMode ||
                LicenseManager.UsageMode == LicenseUsageMode.Designtime ||
                System.Diagnostics.Process.GetCurrentProcess().ProcessName.Equals("devenv", StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            Functions.Connect();
            UIThemeHelper.LoadThemeFromDatabase();
            UIThemeHelper.OnThemeChanged -= OnThemeChangedHandler;
            UIThemeHelper.OnThemeChanged += OnThemeChangedHandler;

            if (Functions.Conn != null && Functions.Conn.State == ConnectionState.Open)
            {
                lblDbStatus.Text = "CSDL: Sẵn Sàng (SQL Server Connected)";
                lblDbStatus.ForeColor = Color.FromArgb(16, 185, 129);
            }
            else
            {
                lblDbStatus.Text = "CSDL: Ngắt Kết Nối";
                lblDbStatus.ForeColor = Color.FromArgb(239, 68, 68);
            }

            UpdateClock();
            InitSidebarButtons();
            ApplyBentoStyles();
            OnThemeChangedHandler();
            ApplyRBAC();

            lblUserRole.Text = "" + Functions.CurrentFullName + " (" + Functions.CurrentRole + ")";

            pnlHeader.Resize += (s, ev) =>
            {
                btnToggleTheme.Location = new Point(pnlHeader.ClientSize.Width - 430, 15);
                lblClock.Location = new Point(pnlHeader.ClientSize.Width - 305, 10);
                lblDbStatus.Location = new Point(pnlHeader.ClientSize.Width - 305, 34);
            };
            this.Resize += (s, ev) => LayoutBentoDashboard();
            pnlContent.Resize += (s, ev) => LayoutBentoDashboard();
            pnlBentoDashboard.Resize += (s, ev) => LayoutBentoDashboard();

            ShowDashboard();
            ActivateSidebarButton(btnNavDashboard);
            this.Shown += (s, ev) => this.BeginInvoke((Action)(() => LayoutBentoDashboard()));
        }

        private void ApplyRBAC()
        {
            string role = Functions.CurrentRole;
            if (role == "BanHang")
            {
                btnNavHDNhap.Visible = false;
                btnNavNguoiDung.Visible = false;
                btnNavCaiDat.Visible = false;
                btnBentoHDNhap.Enabled = false;
            }
            else if (role == "ThuKho")
            {
                btnNavHDBan.Visible = false;
                btnNavNguoiDung.Visible = false;
                btnBentoHDBan.Enabled = false;
            }
        }

        private void ApplyBentoStyles()
        {
            UIThemeHelper.StyleHeroCard(pnlBentoHero, 10);
            UIThemeHelper.StyleCard(pnlBentoStock, null, 8);
            UIThemeHelper.StyleCard(pnlBentoSec, null, 8);
            UIThemeHelper.StyleCard(pnlBentoActions, null, 8);
            UIThemeHelper.StyleCard(pnlBentoAlerts, null, 8);
            UIThemeHelper.StyleCard(pnlBentoGridCard, null, 8);

            UIThemeHelper.StyleButton(btnBentoHDBan, UIThemeHelper.ColorPrimary, Color.White, 8);
            UIThemeHelper.StyleButton(btnBentoHDNhap, Color.FromArgb(16, 185, 129), Color.White, 8);
            UIThemeHelper.StyleButton(btnBentoMayTinh, Color.FromArgb(2, 132, 199), Color.White, 8);
            UIThemeHelper.StyleButton(btnBentoBaoCao, Color.FromArgb(124, 58, 237), Color.White, 8);
            UIThemeHelper.StyleButton(btnBentoRefresh, Color.FromArgb(100, 116, 139), Color.White, 8);

            UIThemeHelper.StyleButton(btnToggleTheme, UIThemeHelper.ColorCard, UIThemeHelper.ColorTextPrimary, 12);

            UIThemeHelper.StyleDataGridView(dgvBentoOverview);

            LayoutBentoDashboard();
        }

        private void LayoutBentoDashboard()
        {
            if (pnlBentoDashboard == null || pnlBentoDashboard.ClientSize.Width <= 0) return;

            int totalW = pnlBentoDashboard.ClientSize.Width - 30;
            int totalH = pnlBentoDashboard.ClientSize.Height - 30;
            if (totalW < 960) totalW = 960;

            int gap = 15;
            int row1RemW = totalW - (gap * 2);
            int stockW = Math.Max(230, row1RemW * 26 / 100);
            int secW = Math.Max(230, row1RemW * 26 / 100);
            int heroW = row1RemW - stockW - secW;

            pnlBentoHero.Location = new Point(15, 15);
            pnlBentoHero.Size = new Size(heroW, 145);

            lblHeroBadge.Location = new Point(heroW - lblHeroBadge.Width - 18, 12);
            lblHeroRevenue.Width = heroW - 30;
            lblHeroProfit.Width = heroW - 30;
            lblHeroTrend.Width = heroW - 30;

            pnlBentoStock.Location = new Point(15 + heroW + gap, 15);
            pnlBentoStock.Size = new Size(stockW, 145);
            lblStockTotalModels.Width = stockW - 30;
            lblStockTotalUnits.Width = stockW - 30;
            lblStockValuation.Width = stockW - 30;

            pnlBentoSec.Location = new Point(15 + heroW + gap + stockW + gap, 15);
            pnlBentoSec.Size = new Size(secW, 145);
            lblSecStatus.Width = secW - 30;
            lblSecDbName.Width = secW - 30;
            lblSecHash.Width = secW - 30;

            int actionsW = Math.Max(580, (totalW - gap) * 60 / 100);
            int alertsW = totalW - gap - actionsW;

            pnlBentoActions.Location = new Point(15, 175);
            pnlBentoActions.Size = new Size(actionsW, 145);

            lblQuickHeader.Width = actionsW - 30;
            lblQuickSub.Width = actionsW - 30;

            int btnGap = 12;
            int btnW = (actionsW - 30 - (btnGap * 3)) / 4;
            btnBentoHDBan.Location = new Point(15, 55);
            btnBentoHDBan.Size = new Size(btnW, 75);

            btnBentoHDNhap.Location = new Point(15 + (btnW + btnGap), 55);
            btnBentoHDNhap.Size = new Size(btnW, 75);

            btnBentoMayTinh.Location = new Point(15 + (btnW + btnGap) * 2, 55);
            btnBentoMayTinh.Size = new Size(btnW, 75);

            btnBentoBaoCao.Location = new Point(15 + (btnW + btnGap) * 3, 55);
            btnBentoBaoCao.Size = new Size(btnW, 75);

            pnlBentoAlerts.Location = new Point(15 + actionsW + gap, 175);
            pnlBentoAlerts.Size = new Size(alertsW, 145);
            lblAlertHeader.Width = alertsW - 30;
            lblTopStaffTitle.Width = alertsW - 30;
            lblTopStaffVal.Width = alertsW - 30;
            lblLowStockAlert.Width = alertsW - 30;
            lblAutoPricing.Width = alertsW - 30;

            pnlBentoGridCard.Location = new Point(15, 335);
            pnlBentoGridCard.Width = totalW;
            int gridH = totalH - 335 + 15;
            if (gridH < 350) gridH = 350;
            pnlBentoGridCard.Height = gridH;
            btnBentoRefresh.Location = new Point(pnlBentoGridCard.Width - btnBentoRefresh.Width - 15, 8);
            lblGridTitle.Width = Math.Max(200, pnlBentoGridCard.Width - btnBentoRefresh.Width - 40);
        }

        private void InitSidebarButtons()
        {
            StyleSidebarButton(btnNavDashboard);
            StyleSidebarButton(btnNavMayTinh);
            StyleSidebarButton(btnNavDanhMuc);
            StyleSidebarButton(btnNavHDNhap);
            StyleSidebarButton(btnNavHDBan);
            StyleSidebarButton(btnNavTimKiem);
            StyleSidebarButton(btnNavBaoCao);
            StyleSidebarButton(btnNavHoSo);
            StyleSidebarButton(btnNavNguoiDung);
            StyleSidebarButton(btnNavNhatKy);
            StyleSidebarButton(btnNavCaiDat);
         
            StyleSidebarButton(btnThoat);

            
        }

        private void StyleSidebarButton(Button btn)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = UIThemeHelper.ColorSidebar;
            btn.ForeColor = Color.FromArgb(203, 213, 225);
            btn.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btn.TextAlign = ContentAlignment.MiddleLeft;
            btn.Padding = new Padding(18, 0, 0, 0);
            btn.Cursor = Cursors.Hand;
            UIThemeHelper.MakeRoundedControl(btn, 6);

            btn.Paint += (s, e) =>
            {
                if (btn == currentActiveBtn && e.Graphics != null)
                {
                    e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    Rectangle rect = new Rectangle(0, 0, btn.Width - 1, btn.Height - 1);
                    using (Pen p = new Pen(Color.FromArgb(165, 180, 252), 2f))
                    using (GraphicsPath path = UIThemeHelper.GetRoundedRectanglePath(rect, 6))
                    {
                        e.Graphics.DrawPath(p, path);
                    }
                    using (SolidBrush ind = new SolidBrush(Color.White))
                    {
                        e.Graphics.FillRectangle(ind, new Rectangle(2, 6, 4, btn.Height - 12));
                    }
                }
            };

           
        }

        private void ActivateSidebarButton(Button btn)
        {
            if (currentActiveBtn != null)
            {
                currentActiveBtn.BackColor = UIThemeHelper.ColorSidebar;
                currentActiveBtn.ForeColor = Color.FromArgb(203, 213, 225);
                currentActiveBtn.Invalidate();
            }

            currentActiveBtn = btn;
            if (btn != null)
            {
                btn.BackColor = UIThemeHelper.ColorPrimary;
                btn.ForeColor = Color.White;
                btn.Invalidate();
            }
        }

        public void OpenChildForm(Form childForm, string title, string subTitle)
        {
            if (currentChildForm != null)
            {
                currentChildForm.Close();
                currentChildForm.Dispose();
                currentChildForm = null;
            }

            pnlBentoDashboard.Visible = false;

            currentChildForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            pnlContent.Controls.Add(childForm);
            childForm.BringToFront();
            childForm.Show();
            UIThemeHelper.ApplyTheme(childForm);

            lblTitle.Text = title;
            lblSubTitle.Text = subTitle;
        }

        private void ShowDashboard()
        {
            if (currentChildForm != null)
            {
                currentChildForm.Close();
                currentChildForm.Dispose();
                currentChildForm = null;
            }

            pnlBentoDashboard.Visible = true;
            pnlBentoDashboard.BringToFront();

            lblTitle.Text = "BENTO GRID DASHBOARD - HỆ THỐNG PC STORE PRO";
            lblSubTitle.Text = "Tổng quan kinh doanh, kho hàng trực quan, cảnh báo tự động và điều hành nhanh";

            RefreshBentoData();
            LayoutBentoDashboard();
        }

        private void RefreshBentoData()
        {
            try
            {
                DataTable dtRev = Functions.GetDataToTable("SELECT ISNULL(SUM(TongTien), 0) AS Revenue, COUNT(*) AS OrderCount FROM HoaDonBan");
                if (dtRev.Rows.Count > 0)
                {
                    decimal rev = Convert.ToDecimal(dtRev.Rows[0]["Revenue"]);
                    int count = Convert.ToInt32(dtRev.Rows[0]["OrderCount"]);
                    decimal profit = rev * 0.1m;

                    lblHeroRevenue.Text = string.Format("₫ {0:N0}", rev);
                    lblHeroProfit.Text = string.Format("Lợi nhuận ước tính (10%): ₫ {0:N0}", profit);
                    lblHeroTrend.Text = string.Format("▲ Tăng trưởng ổn định • {0} hóa đơn đã hoàn tất", count);
                }

                DataTable dtStock = Functions.GetDataToTable("SELECT COUNT(*) AS ModelCount, ISNULL(SUM(SoLuong), 0) AS TotalUnits, ISNULL(SUM(SoLuong * GiaBan), 0) AS Valuation FROM MayViTinh");
                if (dtStock.Rows.Count > 0)
                {
                    int models = Convert.ToInt32(dtStock.Rows[0]["ModelCount"]);
                    int units = Convert.ToInt32(dtStock.Rows[0]["TotalUnits"]);
                    decimal val = Convert.ToDecimal(dtStock.Rows[0]["Valuation"]);

                    lblStockTotalModels.Text = models + " Dòng Máy";
                    lblStockTotalUnits.Text = "Tổng tồn kho: " + units + " chiếc";
                    lblStockValuation.Text = string.Format("Định giá: ₫ {0:N0}", val);
                }

                if (Functions.Conn != null && Functions.Conn.State == ConnectionState.Open)
                {
                    lblSecDbName.Text = "CSDL: " + Functions.Conn.Database;
                }

                DataTable dtLowStock = Functions.GetDataToTable("SELECT COUNT(*) FROM MayViTinh WHERE SoLuong <= 5");
                if (dtLowStock.Rows.Count > 0)
                {
                    int lowCount = Convert.ToInt32(dtLowStock.Rows[0][0]);
                    lblLowStockAlert.Text = "Cảnh báo tồn kho: " + lowCount + " sản phẩm có tồn kho <= 5 chiếc";
                }

                DataTable dtTopStaff = Functions.GetDataToTable(
                    @"SELECT TOP 1 nv.TenNV, ISNULL(SUM(hdb.TongTien), 0) AS DoanhThu, COUNT(hdb.MaHDB) AS SoDon
                      FROM NhanVien nv
                      JOIN HoaDonBan hdb ON nv.MaNV = hdb.MaNV
                      GROUP BY nv.TenNV
                      ORDER BY DoanhThu DESC");

                if (dtTopStaff.Rows.Count > 0)
                {
                    string staffName = dtTopStaff.Rows[0]["TenNV"].ToString();
                    decimal staffRev = Convert.ToDecimal(dtTopStaff.Rows[0]["DoanhThu"]);
                    int staffOrders = Convert.ToInt32(dtTopStaff.Rows[0]["SoDon"]);
                    lblTopStaffTitle.Text = "Top 1 Nhân Viên: " + staffName;
                    lblTopStaffVal.Text = string.Format("Doanh thu: ₫ {0:N0} • {1} đơn", staffRev, staffOrders);
                }

                DataTable dtOverview = Functions.GetDataToTable(
                    @"SELECT m.MaMayVT, m.TenMayVT, l.TenLoai, h.TenHangSX, m.SoLuong, m.GiaNhap, m.GiaBan,
                             CASE WHEN m.SoLuong <= 5 THEN N'CẦN NHẬP (<=5)' ELSE N'SẴN SÀNG' END AS TrangThai
                      FROM MayViTinh m
                      LEFT JOIN LoaiMay l ON m.MaLoai = l.MaLoai
                      LEFT JOIN HangSanXuat h ON m.MaHangSX = h.MaHangSX
                      ORDER BY m.SoLuong ASC");

                dgvBentoOverview.DataSource = dtOverview;

                if (dgvBentoOverview.Columns.Contains("MaMayVT")) dgvBentoOverview.Columns["MaMayVT"].HeaderText = "Mã Máy";
                if (dgvBentoOverview.Columns.Contains("TenMayVT")) dgvBentoOverview.Columns["TenMayVT"].HeaderText = "Tên Máy Vi Tính";
                if (dgvBentoOverview.Columns.Contains("TenLoai")) dgvBentoOverview.Columns["TenLoai"].HeaderText = "Loại Máy";
                if (dgvBentoOverview.Columns.Contains("TenHangSX")) dgvBentoOverview.Columns["TenHangSX"].HeaderText = "Hãng Sản Xuất";
                if (dgvBentoOverview.Columns.Contains("SoLuong")) dgvBentoOverview.Columns["SoLuong"].HeaderText = "Tồn Kho";
                if (dgvBentoOverview.Columns.Contains("GiaNhap")) dgvBentoOverview.Columns["GiaNhap"].HeaderText = "Giá Nhập";
                if (dgvBentoOverview.Columns.Contains("GiaBan")) dgvBentoOverview.Columns["GiaBan"].HeaderText = "Giá Bán (110%)";
                if (dgvBentoOverview.Columns.Contains("TrangThai")) dgvBentoOverview.Columns["TrangThai"].HeaderText = "Tình Trạng";

                if (dgvBentoOverview.Columns.Contains("MaMayVT")) dgvBentoOverview.Columns["MaMayVT"].FillWeight = 10;
                if (dgvBentoOverview.Columns.Contains("TenMayVT")) dgvBentoOverview.Columns["TenMayVT"].FillWeight = 25;
                if (dgvBentoOverview.Columns.Contains("TenLoai")) dgvBentoOverview.Columns["TenLoai"].FillWeight = 13;
                if (dgvBentoOverview.Columns.Contains("TenHangSX")) dgvBentoOverview.Columns["TenHangSX"].FillWeight = 14;
                if (dgvBentoOverview.Columns.Contains("SoLuong")) dgvBentoOverview.Columns["SoLuong"].FillWeight = 8;
                if (dgvBentoOverview.Columns.Contains("GiaNhap")) dgvBentoOverview.Columns["GiaNhap"].FillWeight = 11;
                if (dgvBentoOverview.Columns.Contains("GiaBan")) dgvBentoOverview.Columns["GiaBan"].FillWeight = 11;
                if (dgvBentoOverview.Columns.Contains("TrangThai")) dgvBentoOverview.Columns["TrangThai"].FillWeight = 12;

                UIThemeHelper.FormatCurrencyColumns(dgvBentoOverview, "GiaNhap", "GiaBan");
            }
            catch
            {
            }
        }

        private void btnNavDashboard_Click(object sender, EventArgs e)
        {
            ActivateSidebarButton(btnNavDashboard);
            ShowDashboard();
        }

        private void btnNavMayTinh_Click(object sender, EventArgs e)
        {
            ActivateSidebarButton(btnNavMayTinh);
            OpenChildForm(new frmMayViTinh(), " QUẢN LÝ DANH MỤC MÁY VI TÍNH", "Quản lý 14 linh kiện cấu hình, giá nhập, giá bán tự động và kiểm soát tồn kho");
        }

        private void btnNavDanhMuc_Click(object sender, EventArgs e)
        {
            ActivateSidebarButton(btnNavDanhMuc);
            OpenChildForm(new frmDanhMucHeThong(), "📁  QUẢN LÝ DANH MỤC DÙNG CHUNG", "Quản lý Khách hàng, Nhà cung cấp, Nhân viên và các bảng linh kiện");
        }

        private void btnNavHDNhap_Click(object sender, EventArgs e)
        {
            ActivateSidebarButton(btnNavHDNhap);
            OpenChildForm(new frmHoaDonNhap(), " QUẢN LÝ HÓA ĐƠN NHẬP HÀNG (MASTER-DETAIL)", "Nhập hàng từ nhà cung cấp, tự động cộng tồn kho, cập nhật giá nhập và tính giá bán 110%");
        }

        private void btnNavHDBan_Click(object sender, EventArgs e)
        {
            ActivateSidebarButton(btnNavHDBan);
            OpenChildForm(new frmHoaDonBan(), " HÓA ĐƠN BÁN HÀNG & THU NGÂN (POS)", "Lập hóa đơn bán lẻ cho khách hàng, tự động trừ tồn kho và ngăn chặn xuất kho âm");
        }

        private void btnNavTimKiem_Click(object sender, EventArgs e)
        {
            ActivateSidebarButton(btnNavTimKiem);
            OpenChildForm(new frmTimKiem(), " TRA CỨU & TÌM KIẾM ĐA TIÊU CHÍ (YÊU CẦU 4 & 5)", "Tìm kiếm máy vi tính theo cấu hình phần cứng hoặc tra cứu hóa đơn bán hàng linh hoạt");
        }

        private void btnNavBaoCao_Click(object sender, EventArgs e)
        {
            ActivateSidebarButton(btnNavBaoCao);
            OpenChildForm(new frmBaoCaoThongKe(), "📈  BÁO CÁO & THỐNG KÊ DOANH THU (YÊU CẦU 6, 7, 8, 9)", "Báo cáo nhập/bán theo quý/năm, danh sách máy tồn kho và Top 5 nhân viên xuất sắc");
        }

        private void btnNavHoSo_Click(object sender, EventArgs e)
        {
            ActivateSidebarButton(btnNavHoSo);
            OpenChildForm(new frmHoSoCaNhan(), " HỒ SƠ CÁ NHÂN & BẢO MẬT", "Thông tin tài khoản, đổi mật khẩu và xem nhật ký thao tác cá nhân");
        }

        private void btnNavNguoiDung_Click(object sender, EventArgs e)
        {
            ActivateSidebarButton(btnNavNguoiDung);
            OpenChildForm(new frmQuanLyNguoiDung(), "👥  QUẢN TRỊ NGƯỜI DÙNG & PHÂN QUYỀN (RBAC)", "Quản lý tài khoản, phân quyền Admin/Bán Hàng/Thủ Kho và trạng thái truy cập");
        }

        private void btnNavNhatKy_Click(object sender, EventArgs e)
        {
            ActivateSidebarButton(btnNavNhatKy);
            OpenChildForm(new frmNhatKyHoatDong(), " NHẬT KÝ HOẠT ĐỘNG & THÔNG BÁO HỆ THỐNG", "Xem lịch sử thay đổi CSDL và cảnh báo tự động tồn kho thấp");
        }

        private void btnNavCaiDat_Click(object sender, EventArgs e)
        {
            ActivateSidebarButton(btnNavCaiDat);
            OpenChildForm(new frmCaiDat(), "⚙️  CÀI ĐẶT HỆ THỐNG", "Cấu hình chuỗi kết nối SQL Server, chủ đề giao diện và thông tin hóa đơn");
        }

        private void btnNavAutoTest_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            string testReport = SelfTestSuite.RunSelfTest();
            Cursor = Cursors.Default;

            using (Form frmReport = new Form())
            {
                frmReport.Text = "KẾT QUẢ TỰ ĐỘNG KIỂM THỬ HỆ THỐNG (SELF-TEST SUITE)";
                frmReport.Size = new Size(800, 600);
                frmReport.StartPosition = FormStartPosition.CenterParent;
                frmReport.BackColor = Color.FromArgb(15, 23, 42);

                TextBox txt = new TextBox();
                txt.Multiline = true;
                txt.ReadOnly = true;
                txt.Dock = DockStyle.Fill;
                txt.BackColor = Color.FromArgb(15, 23, 42);
                txt.ForeColor = Color.FromArgb(248, 250, 252);
                txt.Font = new Font("Consolas", 10.5F);
                txt.ScrollBars = ScrollBars.Vertical;
                txt.Text = testReport;

                frmReport.Controls.Add(txt);
                frmReport.ShowDialog(this);
            }
        }

        private void btnBentoHDBan_Click(object sender, EventArgs e)
        {
            btnNavHDBan.PerformClick();
        }

        private void btnBentoHDNhap_Click(object sender, EventArgs e)
        {
            btnNavHDNhap.PerformClick();
        }

        private void btnBentoMayTinh_Click(object sender, EventArgs e)
        {
            btnNavMayTinh.PerformClick();
        }

        private void btnBentoBaoCao_Click(object sender, EventArgs e)
        {
            btnNavBaoCao.PerformClick();
        }

        private void btnBentoRefresh_Click(object sender, EventArgs e)
        {
            RefreshBentoData();
            LayoutBentoDashboard();
        }

        private void OnThemeChangedHandler()
        {
            btnToggleTheme.Text = UIThemeHelper.IsDarkMode ? "Light" : "Dark";
            pnlBentoDashboard.BackColor = UIThemeHelper.ColorBackground;
            pnlContent.BackColor = UIThemeHelper.ColorBackground;
            ApplyBentoStyles();
            UIThemeHelper.ApplyTheme(this);
            if (currentChildForm != null)
            {
                UIThemeHelper.ApplyTheme(currentChildForm);
            }
        }

        private void btnToggleTheme_Click(object sender, EventArgs e)
        {
            UIThemeHelper.IsDarkMode = !UIThemeHelper.IsDarkMode;
            OnThemeChangedHandler();
            UIThemeHelper.SaveThemeToDatabase(UIThemeHelper.IsDarkMode);
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn đăng xuất khỏi hệ thống?",
                                "Xác Nhận Đăng Xuất", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Functions.LogAction("Đăng xuất", "Người dùng [" + Functions.CurrentUsername + "] đăng xuất");
                this.Hide();
                using (frmDangNhap login = new frmDangNhap())
                {
                    if (login.ShowDialog() == DialogResult.OK)
                    {
                        this.Show();
                        frmMain_Load(this, EventArgs.Empty);
                    }
                    else
                    {
                        Application.Exit();
                    }
                }
            }
        }

        private void timerClock_Tick(object sender, EventArgs e)
        {
            UpdateClock();
        }

        private void UpdateClock()
        {
            lblClock.Text = string.Format("{0:HH:mm:ss} • {1:dd/MM/yyyy}", DateTime.Now, DateTime.Now);
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            UIThemeHelper.OnThemeChanged -= OnThemeChangedHandler;
            Functions.Disconnect();
        }

        private void lblGridTitle_Click(object sender, EventArgs e)
        {

        }
    }
}
