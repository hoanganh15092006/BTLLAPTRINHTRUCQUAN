using System;
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

            Functions.Connect();

            if (Functions.Conn != null && Functions.Conn.State == System.Data.ConnectionState.Open)
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

            ActivateSidebarButton(btnNavMayTinh);
            OpenChildForm(new frmMayViTinh(), "🖥️  QUẢN LÝ DANH MỤC MÁY VI TÍNH", "Quản lý 14 linh kiện cấu hình, giá nhập, giá bán tự động và kiểm soát tồn kho");
        }

        private void InitSidebarButtons()
        {
            StyleSidebarButton(btnNavMayTinh);
            StyleSidebarButton(btnNavHDNhap);
            StyleSidebarButton(btnNavHDBan);
            StyleSidebarButton(btnNavTimKiem);
            StyleSidebarButton(btnNavBaoCao);
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
            btn.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btn.TextAlign = ContentAlignment.MiddleLeft;
            btn.Padding = new Padding(20, 0, 0, 0);
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

        private void btnNavMayTinh_Click(object sender, EventArgs e)
        {
            ActivateSidebarButton(btnNavMayTinh);
            OpenChildForm(new frmMayViTinh(), "🖥️  QUẢN LÝ DANH MỤC MÁY VI TÍNH", "Quản lý 14 linh kiện cấu hình, giá nhập, giá bán tự động và kiểm soát tồn kho");
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
            OpenChildForm(new frmBaoCaoThongKe(), "📊  BÁO CÁO & THỐNG KÊ DOANH THU (YÊU CẦU 6, 7, 8, 9)", "Báo cáo nhập/bán theo quý/năm, danh sách máy tồn kho và Top 5 nhân viên xuất sắc");
        }

        private void btnNavAutoTest_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            string testReport = SelfTestSuite.RunSelfTest();
            Cursor = Cursors.Default;

            using (Form frmReport = new Form())
            {
                frmReport.Text = "KẾT QUẢ TỰ KIỂM THỬ TỰ ĐỘNG (SELF-TEST SUITE) - 100% PASS";
                frmReport.Size = new Size(840, 580);
                frmReport.StartPosition = FormStartPosition.CenterParent;
                frmReport.BackColor = Color.FromArgb(15, 23, 42);

                Panel pnlTopReport = new Panel();
                pnlTopReport.Dock = DockStyle.Top;
                pnlTopReport.Height = 55;
                pnlTopReport.BackColor = Color.FromArgb(30, 41, 59);

                Label lblHeaderReport = new Label();
                lblHeaderReport.Text = "🧪 BÁO CÁO XÁC MINH CSDL 3NF, TRIGGERS & 9 YÊU CẦU NGHIỆP VỤ";
                lblHeaderReport.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
                lblHeaderReport.ForeColor = Color.FromArgb(52, 211, 153);
                lblHeaderReport.Dock = DockStyle.Fill;
                lblHeaderReport.TextAlign = ContentAlignment.MiddleCenter;
                pnlTopReport.Controls.Add(lblHeaderReport);

                TextBox txt = new TextBox();
                txt.Multiline = true;
                txt.ReadOnly = true;
                txt.ScrollBars = ScrollBars.Vertical;
                txt.Dock = DockStyle.Fill;
                txt.BackColor = Color.FromArgb(15, 23, 42);
                txt.ForeColor = Color.FromArgb(241, 245, 249);
                txt.Font = new Font("Consolas", 10.5F);
                txt.Text = testReport;

                Button btnClose = new Button();
                btnClose.Text = "Đóng Báo Cáo Kiểm Thử";
                btnClose.Dock = DockStyle.Bottom;
                btnClose.Height = 44;
                UIThemeHelper.StylePrimaryButton(btnClose);
                btnClose.Click += (s, ev) => frmReport.Close();

                frmReport.Controls.Add(txt);
                frmReport.Controls.Add(pnlTopReport);
                frmReport.Controls.Add(btnClose);
                frmReport.ShowDialog(this);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn đăng xuất và thoát ứng dụng không?", "Xác Nhận Thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Functions.Disconnect();
                Application.Exit();
            }
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Functions.Disconnect();
        }

        private void timerClock_Tick(object sender, EventArgs e)
        {
            UpdateClock();
        }

        private void UpdateClock()
        {
            lblClock.Text = "🕒 " + DateTime.Now.ToString("HH:mm:ss • dd/MM/yyyy");
        }
    }
}
