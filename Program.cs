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
            Application.Run(new frmMain());
        }
    }
}
