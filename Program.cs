using System;
using System.Windows.Forms;
using QLBanMayViTinh.Forms;

namespace QLBanMayViTinh
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            using (frmDangNhap login = new frmDangNhap())
            {
                if (login.ShowDialog() == DialogResult.OK)
                {
                    Application.Run(new frmMain());
                }
            }
        }
    }
}
