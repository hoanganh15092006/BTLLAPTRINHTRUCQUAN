using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
            if (this.DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                return;

            Functions.Connect();

            if (Functions.Conn != null && Functions.Conn.State == ConnectionState.Open)
            {
                lblDbStatus.Text = "🟢 CSDL: Sẵn Sàng (SQL Server Connected)";
                lblDbStatus.ForeColor = Color.FromArgb(16, 185, 129);
            }
            else
            {
                lblDbStatus.Text = "🔴 CSDL: Ngắt Kết Nối";
                lblDbStatus.ForeColor = Color.FromArgb(239, 68, 68);
            }

            UpdateClock();
            InitSidebarButtons();
            ApplyRBAC();

            lblUserRole.Text = "👤 " + Functions.CurrentFullName + " (" + Functions.CurrentRole + ")";

            btnNavDashboard.PerformClick();
        }

        private void ApplyRBAC()
        {
            string role = Functions.CurrentRole;
            if (role == "BanHang")
            {
                btnNavHDNhap.Visible = false;
                btnNavNguoiDung.Visible = false;
                btnNavCaiDat.Visible = false;
            }
            else if (role == "ThuKho")
            {
                btnNavHDBan.Visible = false;
                btnNavNguoiDung.Visible = false;
            }
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
            StyleSidebarButton(btnNavAutoTest);
            StyleSidebarButton(btnThoat);

            btnNavAutoTest.ForeColor = Color.FromArgb(165, 180, 252);
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

            btn.MouseEnter += (s, e) =>
            {
                if (btn != currentActiveBtn && btn != btnNavAutoTest)
                {
                    btn.BackColor = UIThemeHelper.ColorSidebarHover;
                    btn.ForeColor = Color.White;
                }
            };

            btn.MouseLeave += (s, e) =>
            {
                if (btn != currentActiveBtn && btn != btnNavAutoTest)
                {
                    btn.BackColor = UIThemeHelper.ColorSidebar;
                    btn.ForeColor = Color.FromArgb(203, 213, 225);
                }
            };
        }

        private void ActivateSidebarButton(Button btn)
        {
            if (currentActiveBtn != null)
            {
                currentActiveBtn.BackColor = UIThemeHelper.ColorSidebar;
                currentActiveBtn.ForeColor = Color.FromArgb(203, 213, 225);
            }

            currentActiveBtn = btn;
            if (btn != null)
            {
                btn.BackColor = UIThemeHelper.ColorPrimary;
                btn.ForeColor = Color.White;
            }
        }

        public void OpenChildForm(Form childForm, string title, string subTitle)
        {
            if (currentChildForm != null)
            {
                currentChildForm.Close();
                currentChildForm.Dispose();
            }

            currentChildForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            pnlContent.Controls.Clear();
            pnlContent.Controls.Add(childForm);
            pnlContent.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();

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

            pnlContent.Controls.Clear();
            lblTitle.Text = "📊  TỔNG QUAN HỆ THỐNG (DASHBOARD)";
            lblSubTitle.Text = "Bảng điều khiển kinh doanh, số liệu thống kê thời gian thực và cảnh báo tồn kho";

            Panel pnlDash = new Panel();
            pnlDash.Dock = DockStyle.Fill;
            pnlDash.AutoScroll = true;
            pnlDash.BackColor = UIThemeHelper.ColorBackground;

            decimal totalRevenue = 0;
            string revStr = Functions.GetFieldValues("SELECT ISNULL(SUM(TongTien), 0) FROM HoaDonBan");
            decimal.TryParse(revStr, out totalRevenue);

            int orderCount = 0;
            string ordStr = Functions.GetFieldValues("SELECT COUNT(*) FROM HoaDonBan");
            int.TryParse(ordStr, out orderCount);

            int productCount = 0;
            string prdStr = Functions.GetFieldValues("SELECT COUNT(*) FROM MayViTinh");
            int.TryParse(prdStr, out productCount);

            int lowStockCount = 0;
            string lowStr = Functions.GetFieldValues("SELECT COUNT(*) FROM MayViTinh WHERE SoLuong <= 5");
            int.TryParse(lowStr, out lowStockCount);

            Panel card1 = UIThemeHelper.CreateKpiCard("Doanh Thu Bán Hàng", string.Format("{0:N0} đ", totalRevenue), "Tổng tiền bán thực tế", Color.FromArgb(79, 70, 229), "💰", 240, 85);
            Panel card2 = UIThemeHelper.CreateKpiCard("Tổng Đơn Hàng", orderCount + " Đơn", "Đã thanh toán thành công", Color.FromArgb(16, 185, 129), "🧾", 240, 85);
            Panel card3 = UIThemeHelper.CreateKpiCard("Mẫu Máy Vi Tính", productCount + " Mẫu", "Trong danh mục sản phẩm", Color.FromArgb(14, 165, 233), "🖥️", 240, 85);
            Panel card4 = UIThemeHelper.CreateKpiCard("Cảnh Báo Tồn Kho", lowStockCount + " Máy", "Cần nhập thêm hàng (<=5)", Color.FromArgb(239, 68, 68), "⚠️", 240, 85);

            card1.Location = new Point(20, 20);
            card2.Location = new Point(275, 20);
            card3.Location = new Point(530, 20);
            card4.Location = new Point(785, 20);

            pnlDash.Controls.Add(card1);
            pnlDash.Controls.Add(card2);
            pnlDash.Controls.Add(card3);
            pnlDash.Controls.Add(card4);

            Panel pnlTableCard = new Panel();
            pnlTableCard.Location = new Point(20, 125);
            pnlTableCard.Size = new Size(1005, 520);
            pnlTableCard.BackColor = Color.White;
            UIThemeHelper.StyleCard(pnlTableCard);

            Label lblTableTitle = new Label();
            lblTableTitle.Text = "🔥 MÁY VI TÍNH SẮP HẾT HÀNG & MỚI NHẬP TRÊN HỆ THỐNG";
            lblTableTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblTableTitle.ForeColor = Color.FromArgb(15, 23, 42);
            lblTableTitle.Dock = DockStyle.Top;
            lblTableTitle.Height = 40;
            lblTableTitle.Padding = new Padding(15, 10, 0, 0);

            DataGridView dgvOverview = new DataGridView();
            dgvOverview.Dock = DockStyle.Fill;
            dgvOverview.AllowUserToAddRows = false;
            dgvOverview.AllowUserToDeleteRows = false;
            dgvOverview.ReadOnly = true;
            UIThemeHelper.StyleDataGridView(dgvOverview);

            DataTable dtOverview = Functions.GetDataToTable(
                @"SELECT m.MaMayVT, m.TenMayVT, l.TenLoai, h.TenHangSX, m.SoLuong, m.GiaNhap, m.GiaBan
                  FROM MayViTinh m
                  LEFT JOIN LoaiMay l ON m.MaLoai = l.MaLoai
                  LEFT JOIN HangSanXuat h ON m.MaHangSX = h.MaHangSX
                  ORDER BY m.SoLuong ASC");

            dgvOverview.DataSource = dtOverview;

            if (dgvOverview.Columns.Contains("MaMayVT")) dgvOverview.Columns["MaMayVT"].HeaderText = "Mã Máy";
            if (dgvOverview.Columns.Contains("TenMayVT")) dgvOverview.Columns["TenMayVT"].HeaderText = "Tên Máy Vi Tính";
            if (dgvOverview.Columns.Contains("TenLoai")) dgvOverview.Columns["TenLoai"].HeaderText = "Loại Máy";
            if (dgvOverview.Columns.Contains("TenHangSX")) dgvOverview.Columns["TenHangSX"].HeaderText = "Hãng Sản Xuất";
            if (dgvOverview.Columns.Contains("SoLuong")) dgvOverview.Columns["SoLuong"].HeaderText = "Tồn Kho";
            if (dgvOverview.Columns.Contains("GiaNhap")) dgvOverview.Columns["GiaNhap"].HeaderText = "Giá Nhập";
            if (dgvOverview.Columns.Contains("GiaBan")) dgvOverview.Columns["GiaBan"].HeaderText = "Giá Bán";

            UIThemeHelper.FormatCurrencyColumns(dgvOverview, "GiaNhap", "GiaBan");

            pnlTableCard.Controls.Add(dgvOverview);
            pnlTableCard.Controls.Add(lblTableTitle);

            pnlDash.Controls.Add(pnlTableCard);

            pnlContent.Controls.Add(pnlDash);
        }

        private void btnNavDashboard_Click(object sender, EventArgs e)
        {
            ActivateSidebarButton(btnNavDashboard);
            ShowDashboard();
        }

        private void btnNavMayTinh_Click(object sender, EventArgs e)
        {
            ActivateSidebarButton(btnNavMayTinh);
            OpenChildForm(new frmMayViTinh(), "🖥️  QUẢN LÝ DANH MỤC MÁY VI TÍNH", "Quản lý 14 linh kiện cấu hình, giá nhập, giá bán tự động và kiểm soát tồn kho");
        }

        private void btnNavDanhMuc_Click(object sender, EventArgs e)
        {
            ActivateSidebarButton(btnNavDanhMuc);
            OpenChildForm(new frmDanhMucHeThong(), "📁  QUẢN LÝ DANH MỤC DÙNG CHUNG", "Quản lý Khách hàng, Nhà cung cấp, Nhân viên và các bảng linh kiện");
        }

        private void btnNavHDNhap_Click(object sender, EventArgs e)
        {
            ActivateSidebarButton(btnNavHDNhap);
            OpenChildForm(new frmHoaDonNhap(), "📥  QUẢN LÝ HÓA ĐƠN NHẬP HÀNG (MASTER-DETAIL)", "Nhập hàng từ nhà cung cấp, tự động cộng tồn kho, cập nhật giá nhập và tính giá bán 110%");
        }

        private void btnNavHDBan_Click(object sender, EventArgs e)
        {
            ActivateSidebarButton(btnNavHDBan);
            OpenChildForm(new frmHoaDonBan(), "📤  HÓA ĐƠN BÁN HÀNG & THU NGÂN (POS)", "Lập hóa đơn bán lẻ cho khách hàng, tự động trừ tồn kho và ngăn chặn xuất kho âm");
        }

        private void btnNavTimKiem_Click(object sender, EventArgs e)
        {
            ActivateSidebarButton(btnNavTimKiem);
            OpenChildForm(new frmTimKiem(), "🔍  TRA CỨU & TÌM KIẾM ĐA TIÊU CHÍ (YÊU CẦU 4 & 5)", "Tìm kiếm máy vi tính theo cấu hình phần cứng hoặc tra cứu hóa đơn bán hàng linh hoạt");
        }

        private void btnNavBaoCao_Click(object sender, EventArgs e)
        {
            ActivateSidebarButton(btnNavBaoCao);
            OpenChildForm(new frmBaoCaoThongKe(), "📈  BÁO CÁO & THỐNG KÊ DOANH THU (YÊU CẦU 6, 7, 8, 9)", "Báo cáo nhập/bán theo quý/năm, danh sách máy tồn kho và Top 5 nhân viên xuất sắc");
        }

        private void btnNavHoSo_Click(object sender, EventArgs e)
        {
            ActivateSidebarButton(btnNavHoSo);
            OpenChildForm(new frmHoSoCaNhan(), "👤  HỒ SƠ CÁ NHÂN & BẢO MẬT", "Thông tin tài khoản, đổi mật khẩu và xem nhật ký thao tác cá nhân");
        }

        private void btnNavNguoiDung_Click(object sender, EventArgs e)
        {
            ActivateSidebarButton(btnNavNguoiDung);
            OpenChildForm(new frmQuanLyNguoiDung(), "👥  QUẢN TRỊ NGƯỜI DÙNG & PHÂN QUYỀN (RBAC)", "Quản lý tài khoản, phân quyền Admin/Bán Hàng/Thủ Kho và trạng thái truy cập");
        }

        private void btnNavNhatKy_Click(object sender, EventArgs e)
        {
            ActivateSidebarButton(btnNavNhatKy);
            OpenChildForm(new frmNhatKyHoatDong(), "📋  NHẬT KÝ HOẠT ĐỘNG & THÔNG BÁO HỆ THỐNG", "Xem lịch sử thay đổi CSDL và cảnh báo tự động tồn kho thấp");
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

        private void btnToggleTheme_Click(object sender, EventArgs e)
        {
            UIThemeHelper.IsDarkMode = !UIThemeHelper.IsDarkMode;
            btnToggleTheme.Text = UIThemeHelper.IsDarkMode ? "☀️ Light" : "🌙 Dark";
            if (currentChildForm == null)
            {
                ShowDashboard();
            }
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
            lblClock.Text = string.Format("🕒 {0:HH:mm:ss} • {1:dd/MM/yyyy}", DateTime.Now, DateTime.Now);
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Functions.Disconnect();
        }
    }
}
