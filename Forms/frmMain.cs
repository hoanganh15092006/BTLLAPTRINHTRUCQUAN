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

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            // Kết nối DB ngay khi bật app
            Functions.Connect();

            // Style các nút trên Sidebar
            StyleSidebarButton(btnNavMayTinh);
            StyleSidebarButton(btnNavHDNhap);
            StyleSidebarButton(btnNavHDBan);
            StyleSidebarButton(btnNavTimKiem);
            StyleSidebarButton(btnNavBaoCao);
            StyleSidebarButton(btnNavAutoTest);
            StyleSidebarButton(btnThoat);

            btnNavAutoTest.BackColor = Color.FromArgb(79, 70, 229); // Nổi bật nút Auto-Test
            btnNavAutoTest.ForeColor = Color.White;

            // Mở mặc định form Máy vi tính
            OpenChildForm(new frmMayViTinh(), "QUẢN LÝ DANH MỤC MÁY VI TÍNH");
        }

        private void StyleSidebarButton(Button btn)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.ForeColor = Color.FromArgb(228, 228, 231);
            btn.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            btn.TextAlign = ContentAlignment.MiddleLeft;
            btn.Padding = new Padding(16, 0, 0, 0);
            btn.Cursor = Cursors.Hand;

            btn.MouseEnter += (s, e) =>
            {
                if (btn != btnNavAutoTest) btn.BackColor = UIThemeHelper.ColorSidebarHover;
            };
            btn.MouseLeave += (s, e) =>
            {
                if (btn != btnNavAutoTest) btn.BackColor = UIThemeHelper.ColorSidebar;
            };
        }

        public void OpenChildForm(Form childForm, string title)
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
        }

        private void btnNavMayTinh_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmMayViTinh(), "QUẢN LÝ DANH MỤC MÁY VI TÍNH");
        }

        private void btnNavHDNhap_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmHoaDonNhap(), "QUẢN LÝ HÓA ĐƠN NHẬP HÀNG (MASTER-DETAIL)");
        }

        private void btnNavHDBan_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmHoaDonBan(), "QUẢN LÝ HÓA ĐƠN BÁN HÀNG (MASTER-DETAIL)");
        }

        private void btnNavTimKiem_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmTimKiem(), "TÌM KIẾM ĐA ĐIỀU KIỆN (YÊU CẦU 4 & 5)");
        }

        private void btnNavBaoCao_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmBaoCaoThongKe(), "BÁO CÁO & THỐNG KÊ DOANH THU, TỒN KHO, TOP 5 (YÊU CẦU 6, 7, 8, 9)");
        }

        private void btnNavAutoTest_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            string testReport = SelfTestSuite.RunSelfTest();
            Cursor = Cursors.Default;

            // Hiển thị báo cáo trong hộp thoại lớn đẹp mắt
            using (Form frmReport = new Form())
            {
                frmReport.Text = "Kết Quả Tự Kiểm Thử Tự Động (Self-Test Suite)";
                frmReport.Size = new Size(820, 560);
                frmReport.StartPosition = FormStartPosition.CenterParent;
                frmReport.BackColor = Color.FromArgb(24, 24, 27);

                TextBox txt = new TextBox();
                txt.Multiline = true;
                txt.ReadOnly = true;
                txt.ScrollBars = ScrollBars.Vertical;
                txt.Dock = DockStyle.Fill;
                txt.BackColor = Color.FromArgb(15, 23, 42);
                txt.ForeColor = Color.FromArgb(52, 211, 153);
                txt.Font = new Font("Consolas", 10.5F);
                txt.Text = testReport;

                Button btnClose = new Button();
                btnClose.Text = "Đóng Báo Cáo";
                btnClose.Dock = DockStyle.Bottom;
                btnClose.Height = 42;
                btnClose.BackColor = Color.FromArgb(79, 70, 229);
                btnClose.ForeColor = Color.White;
                btnClose.FlatStyle = FlatStyle.Flat;
                btnClose.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
                btnClose.Click += (s, ev) => frmReport.Close();

                frmReport.Controls.Add(txt);
                frmReport.Controls.Add(btnClose);
                frmReport.ShowDialog(this);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn thoát ứng dụng không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Functions.Disconnect();
                Application.Exit();
            }
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Functions.Disconnect();
        }
    }
}
