using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using QLBanMayViTinh.Classes;
using QLBanMayViTinh.Forms;

namespace QLBanMayViTinh.Testing
{
    public class ChallengerFinalHarness
    {
        [DllImport("user32.dll")]
        private static extern uint GetGuiResources(IntPtr hProcess, uint uiFlags);

        private const uint GR_GDIOBJECTS = 0;
        private const uint GR_USEROBJECTS = 1;

        private static int passedTests = 0;
        private static int failedTests = 0;
        private static List<string> findings = new List<string>();

        [STAThread]
        public static int Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("======================================================================");
            Console.WriteLine("     CHALLENGER EMPIRICAL ADVERSARIAL HARNESS - FINAL MILESTONE       ");
            Console.WriteLine("======================================================================");
            Console.WriteLine("Timestamp: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            Console.WriteLine();

            Functions.Connect();

            RunTest("ADV_01_LoginForm_EliminationOfPnlLeftAndMarketingLabels", Test01_LoginForm_EliminationOfPnlLeftAndMarketingLabels);
            RunTest("ADV_02_LoginForm_PasswordEyeToggle", Test02_LoginForm_PasswordEyeToggle);
            RunTest("ADV_03_LoginForm_InvalidAndEmptyCredentialsRejection", Test03_LoginForm_InvalidAndEmptyCredentialsRejection);
            RunTest("ADV_04_LoginForm_StartPositionCentered", Test04_LoginForm_StartPositionCentered);
            RunTest("ADV_05_ThemeEngine_100CycleRapidStressAndGdiLeakCheck", Test05_ThemeEngine_100CycleRapidStressAndGdiLeakCheck);
            RunTest("ADV_06_ThemeEngine_DatabasePersistenceAndStartupReload", Test06_ThemeEngine_DatabasePersistenceAndStartupReload);
            RunTest("ADV_07_ThemeEngine_LiveDispatchAcrossOpenAndChildForms", Test07_ThemeEngine_LiveDispatchAcrossOpenAndChildForms);
            RunTest("ADV_08_ThemeEngine_WCAGContrastRatios", Test08_ThemeEngine_WCAGContrastRatios);
            RunTest("ADV_09_ZeroComments_ForensicAudit", Test09_ZeroComments_ForensicAudit);

            Console.WriteLine();
            Console.WriteLine("======================================================================");
            Console.WriteLine(string.Format("SUMMARY: {0} PASSED, {1} FAILED", passedTests, failedTests));
            Console.WriteLine("======================================================================");

            if (findings.Count > 0)
            {
                Console.WriteLine("FINDINGS:");
                foreach (string f in findings)
                {
                    Console.WriteLine(" - " + f);
                }
            }

            return failedTests == 0 ? 0 : 1;
        }

        private static void RunTest(string testName, Action testMethod)
        {
            try
            {
                Stopwatch sw = Stopwatch.StartNew();
                testMethod();
                sw.Stop();
                passedTests++;
                Console.WriteLine(string.Format("[PASS] {0} ({1} ms)", testName, sw.ElapsedMilliseconds));
            }
            catch (Exception ex)
            {
                failedTests++;
                string err = string.Format("[FAIL] {0}: {1}", testName, ex.Message);
                findings.Add(err);
                Console.WriteLine(err);
                if (ex.InnerException != null)
                {
                    Console.WriteLine("       Inner: " + ex.InnerException.Message);
                }
            }
        }

        private static void AssertTrue(bool condition, string message)
        {
            if (!condition)
            {
                throw new Exception("Assertion failed: " + message);
            }
        }

        private static void AssertFalse(bool condition, string message)
        {
            if (condition)
            {
                throw new Exception("Assertion failed: " + message);
            }
        }

        private static void AssertEqual<T>(T expected, T actual, string message)
        {
            if (!EqualityComparer<T>.Default.Equals(expected, actual))
            {
                throw new Exception(string.Format("{0} - Expected: <{1}>, Actual: <{2}>", message, expected, actual));
            }
        }

        private static void AssertNotNull(object obj, string message)
        {
            if (obj == null)
            {
                throw new Exception("Assertion failed (Null): " + message);
            }
        }

        private static void Test01_LoginForm_EliminationOfPnlLeftAndMarketingLabels()
        {
            using (frmDangNhap form = new frmDangNhap())
            {
                Control[] pnlLeft = form.Controls.Find("pnlLeft", true);
                AssertTrue(pnlLeft.Length == 0, "pnlLeft must be 100% eliminated from frmDangNhap");

                string[] forbiddenMarketingLabels = new string[]
                {
                    "lblFeature1", "lblFeature2", "lblFeature3",
                    "lblBrandTitle", "lblBrandSub", "lblVer", "lblBrandIcon"
                };

                foreach (string lblName in forbiddenMarketingLabels)
                {
                    Control[] found = form.Controls.Find(lblName, true);
                    AssertTrue(found.Length == 0, string.Format("Marketing label {0} must be eliminated from frmDangNhap", lblName));
                }

                Control[] cardFound = form.Controls.Find("pnlLoginCard", true);
                AssertTrue(cardFound.Length == 1, "pnlLoginCard must exist on frmDangNhap");

                Control card = cardFound[0];
                AssertEqual(390, card.Width, "Card width must be 390px");
                AssertEqual(485, card.Height, "Card height must be 485px");

                int leftPadding = card.Left;
                int rightPadding = form.ClientSize.Width - (card.Left + card.Width);
                int topPadding = card.Top;
                int bottomPadding = form.ClientSize.Height - (card.Top + card.Height);

                AssertEqual(35, leftPadding, "Card left padding must be 35px");
                AssertEqual(35, rightPadding, "Card right padding must be 35px");

                Console.WriteLine(string.Format("       Metrics: pnlLeft=0, ObsoleteLabels=0, CardSize=({0}x{1}), Padding=(L:{2}, R:{3}, T:{4}, B:{5})",
                    card.Width, card.Height, leftPadding, rightPadding, topPadding, bottomPadding));
            }
        }

        private static void Test02_LoginForm_PasswordEyeToggle()
        {
            using (frmDangNhap form = new frmDangNhap())
            {
                Control[] txtFound = form.Controls.Find("txtMatKhau", true);
                AssertTrue(txtFound.Length == 1, "txtMatKhau must exist");
                TextBox txtMatKhau = (TextBox)txtFound[0];

                Control[] btnFound = form.Controls.Find("btnTogglePass", true);
                AssertTrue(btnFound.Length == 1, "btnTogglePass must exist");
                Button btnTogglePass = (Button)btnFound[0];

                AssertTrue(txtMatKhau.UseSystemPasswordChar, "Initial password mode must be masked");

                MethodInfo toggleMethod = typeof(frmDangNhap).GetMethod("btnTogglePass_Click", BindingFlags.NonPublic | BindingFlags.Instance);
                AssertNotNull(toggleMethod, "btnTogglePass_Click handler must exist");

                toggleMethod.Invoke(form, new object[] { btnTogglePass, EventArgs.Empty });
                AssertFalse(txtMatKhau.UseSystemPasswordChar, "First toggle: password must be revealed");
                AssertEqual("🙈", btnTogglePass.Text, "First toggle: button icon must show eye-closed emoji");

                toggleMethod.Invoke(form, new object[] { btnTogglePass, EventArgs.Empty });
                AssertTrue(txtMatKhau.UseSystemPasswordChar, "Second toggle: password must be masked");
                AssertEqual("Hiện", btnTogglePass.Text, "Second toggle: button icon must show eye-open emoji");

                Console.WriteLine("       Metrics: Initial masked (True) -> Toggle 1: plain text (False, '🙈') -> Toggle 2: masked (True, 'Hiện')");
            }
        }

        private static void Test03_LoginForm_InvalidAndEmptyCredentialsRejection()
        {
            using (frmDangNhap form = new frmDangNhap())
            {
                TextBox txtUser = (TextBox)form.Controls.Find("txtTenDangNhap", true)[0];
                TextBox txtPass = (TextBox)form.Controls.Find("txtMatKhau", true)[0];
                Label lblStatus = (Label)form.Controls.Find("lblStatus", true)[0];
                Button btnDangNhap = (Button)form.Controls.Find("btnDangNhap", true)[0];

                MethodInfo loginMethod = typeof(frmDangNhap).GetMethod("btnDangNhap_Click", BindingFlags.NonPublic | BindingFlags.Instance);
                AssertNotNull(loginMethod, "btnDangNhap_Click handler must exist");

                txtUser.Text = "";
                txtPass.Text = "";
                loginMethod.Invoke(form, new object[] { btnDangNhap, EventArgs.Empty });
                AssertTrue(lblStatus.Text.Contains("Vui lòng nhập tên đăng nhập"), "Empty username must be rejected");
                AssertTrue(form.DialogResult != DialogResult.OK, "DialogResult must not be OK for empty username");
                string msg1 = lblStatus.Text;

                txtUser.Text = "admin";
                txtPass.Text = "";
                loginMethod.Invoke(form, new object[] { btnDangNhap, EventArgs.Empty });
                AssertTrue(lblStatus.Text.Contains("Vui lòng nhập mật khẩu"), "Empty password must be rejected");
                AssertTrue(form.DialogResult != DialogResult.OK, "DialogResult must not be OK for empty password");
                string msg2 = lblStatus.Text;

                txtUser.Text = "adversarial_ghost_user_999";
                txtPass.Text = "AnyPassword#123";
                loginMethod.Invoke(form, new object[] { btnDangNhap, EventArgs.Empty });
                AssertTrue(lblStatus.Text.Contains("không tồn tại") || lblStatus.Text.Contains("Lỗi"), "Non-existent user must be rejected");
                AssertTrue(form.DialogResult != DialogResult.OK, "DialogResult must not be OK for non-existent user");
                string msg3 = lblStatus.Text;

                txtUser.Text = "admin";
                txtPass.Text = "Adversarial_Totally_Wrong_Password_2026!";
                loginMethod.Invoke(form, new object[] { btnDangNhap, EventArgs.Empty });
                AssertTrue(lblStatus.Text.Contains("không chính xác") || lblStatus.Text.Contains("Lỗi"), "Wrong password for admin must be rejected");
                AssertTrue(form.DialogResult != DialogResult.OK, "DialogResult must not be OK for incorrect password");
                string msg4 = lblStatus.Text;

                txtUser.Text = "' OR '1'='1";
                txtPass.Text = "' OR '1'='1";
                loginMethod.Invoke(form, new object[] { btnDangNhap, EventArgs.Empty });
                AssertTrue(lblStatus.Text.Contains("không tồn tại") || lblStatus.Text.Contains("Lỗi"), "SQL injection attack must be rejected");
                AssertTrue(form.DialogResult != DialogResult.OK, "DialogResult must not be OK for SQL injection");
                string msg5 = lblStatus.Text;

                Console.WriteLine(string.Format("       Rejection 1 (Empty User): '{0}'", msg1));
                Console.WriteLine(string.Format("       Rejection 2 (Empty Pass): '{0}'", msg2));
                Console.WriteLine(string.Format("       Rejection 3 (Ghost User): '{0}'", msg3));
                Console.WriteLine(string.Format("       Rejection 4 (Wrong Pass): '{0}'", msg4));
                Console.WriteLine(string.Format("       Rejection 5 (SQLi Probe): '{0}'", msg5));
            }
        }

        private static void Test04_LoginForm_StartPositionCentered()
        {
            using (frmDangNhap form = new frmDangNhap())
            {
                AssertEqual(FormStartPosition.CenterScreen, form.StartPosition, "frmDangNhap StartPosition must be CenterScreen");
                AssertEqual(FormBorderStyle.FixedSingle, form.FormBorderStyle, "frmDangNhap FormBorderStyle must be FixedSingle");
                AssertFalse(form.MaximizeBox, "frmDangNhap MaximizeBox must be false");
                AssertEqual(460, form.ClientSize.Width, "frmDangNhap ClientSize Width must be 460px");
                AssertEqual(550, form.ClientSize.Height, "frmDangNhap ClientSize Height must be 550px");

                Console.WriteLine(string.Format("       Metrics: StartPosition={0}, FormBorderStyle={1}, MaximizeBox={2}, ClientSize={3}x{4}",
                    form.StartPosition, form.FormBorderStyle, form.MaximizeBox, form.ClientSize.Width, form.ClientSize.Height));
            }
        }

        private static void Test05_ThemeEngine_100CycleRapidStressAndGdiLeakCheck()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            IntPtr procHandle = Process.GetCurrentProcess().Handle;
            uint initialGdi = GetGuiResources(procHandle, GR_GDIOBJECTS);
            uint initialUser = GetGuiResources(procHandle, GR_USEROBJECTS);

            using (frmMain fMain = new frmMain())
            using (frmMayViTinh fMvt = new frmMayViTinh())
            using (frmHoaDonBan fHdb = new frmHoaDonBan())
            using (frmDangNhap fLogin = new frmDangNhap())
            {
                for (int i = 0; i < 100; i++)
                {
                    UIThemeHelper.IsDarkMode = (i % 2 == 0);
                    UIThemeHelper.ApplyTheme(fMain);
                    UIThemeHelper.ApplyTheme(fMvt);
                    UIThemeHelper.ApplyTheme(fHdb);
                    UIThemeHelper.ApplyTheme(fLogin);
                }
            }

            UIThemeHelper.IsDarkMode = false;

            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            uint finalGdi = GetGuiResources(procHandle, GR_GDIOBJECTS);
            uint finalUser = GetGuiResources(procHandle, GR_USEROBJECTS);

            long gdiDelta = (long)finalGdi - (long)initialGdi;
            long userDelta = (long)finalUser - (long)initialUser;

            AssertTrue(gdiDelta <= 5, string.Format("GDI delta after 100 theme cycles must be <= 5 (measured: {0})", gdiDelta));

            Console.WriteLine(string.Format("       Stress Results: 100 Cycles (400 form theme traversals) -> GDI Before: {0}, After: {1}, Delta: {2} | User Objects Delta: {3}",
                initialGdi, finalGdi, gdiDelta, userDelta));
        }

        private static void Test06_ThemeEngine_DatabasePersistenceAndStartupReload()
        {
            Functions.Connect();

            string origTheme = Functions.GetFieldValues("SELECT TOP 1 KeyValue FROM tblCauHinhHeThong WHERE KeyName = 'ThemeMode'");

            try
            {
                UIThemeHelper.SaveThemeToDatabase(true);

                string dbValTheme = Functions.GetFieldValues("SELECT KeyValue FROM tblCauHinhHeThong WHERE KeyName = 'ThemeMode'");
                string dbValGiaoDien = Functions.GetFieldValues("SELECT KeyValue FROM tblCauHinhHeThong WHERE KeyName = 'GiaoDien'");

                AssertEqual("Dark", dbValTheme, "tblCauHinhHeThong.ThemeMode must be 'Dark'");
                AssertEqual("Dark", dbValGiaoDien, "tblCauHinhHeThong.GiaoDien must be 'Dark'");

                UIThemeHelper.IsDarkMode = false;
                bool loadedDark = UIThemeHelper.LoadThemeFromDatabase();
                AssertTrue(loadedDark, "LoadThemeFromDatabase must return true for Dark");
                AssertTrue(UIThemeHelper.IsDarkMode, "UIThemeHelper.IsDarkMode must become true after loading Dark");

                UIThemeHelper.SaveThemeToDatabase(false);

                dbValTheme = Functions.GetFieldValues("SELECT KeyValue FROM tblCauHinhHeThong WHERE KeyName = 'ThemeMode'");
                dbValGiaoDien = Functions.GetFieldValues("SELECT KeyValue FROM tblCauHinhHeThong WHERE KeyName = 'GiaoDien'");

                AssertEqual("Light", dbValTheme, "tblCauHinhHeThong.ThemeMode must be 'Light'");
                AssertEqual("Light", dbValGiaoDien, "tblCauHinhHeThong.GiaoDien must be 'Light'");

                UIThemeHelper.IsDarkMode = true;
                bool loadedLight = UIThemeHelper.LoadThemeFromDatabase();
                AssertFalse(loadedLight, "LoadThemeFromDatabase must return false for Light");
                AssertFalse(UIThemeHelper.IsDarkMode, "UIThemeHelper.IsDarkMode must become false after loading Light");

                Console.WriteLine("       Round-Trip Verified: Dark Save -> DB confirms ('Dark','Dark') -> Load restores IsDarkMode=True; Light Save -> DB confirms ('Light','Light') -> Load restores IsDarkMode=False");
            }
            finally
            {
                if (!string.IsNullOrEmpty(origTheme))
                {
                    bool origDark = origTheme.Equals("Dark", StringComparison.OrdinalIgnoreCase);
                    UIThemeHelper.SaveThemeToDatabase(origDark);
                    UIThemeHelper.IsDarkMode = origDark;
                }
            }
        }

        private static void Test07_ThemeEngine_LiveDispatchAcrossOpenAndChildForms()
        {
            using (frmMain mainForm = new frmMain())
            {
                UIThemeHelper.IsDarkMode = false;
                UIThemeHelper.ApplyTheme(mainForm);

                frmMayViTinh child1 = new frmMayViTinh();
                mainForm.OpenChildForm(child1, "Quản Lý Máy Vi Tính", "Danh mục máy tính");

                Color lightBg = child1.BackColor;
                AssertEqual(UIThemeHelper.ColorBackground, lightBg, "Child form must immediately adopt active Light theme background");

                MethodInfo onThemeChanged = typeof(frmMain).GetMethod("OnThemeChangedHandler", BindingFlags.NonPublic | BindingFlags.Instance);
                AssertNotNull(onThemeChanged, "frmMain.OnThemeChangedHandler must exist");

                UIThemeHelper.IsDarkMode = true;
                onThemeChanged.Invoke(mainForm, null);

                Color darkBg = child1.BackColor;
                AssertEqual(UIThemeHelper.ColorBackground, darkBg, "Child form must immediately adopt active Dark theme background upon toggle");

                frmHoaDonBan child2 = new frmHoaDonBan();
                mainForm.OpenChildForm(child2, "Hóa Đơn Bán Hàng", "POS");

                Color newChildDarkBg = child2.BackColor;
                AssertEqual(UIThemeHelper.ColorBackground, newChildDarkBg, "Newly opened child form must immediately adopt active Dark theme background");

                UIThemeHelper.IsDarkMode = false;
                onThemeChanged.Invoke(mainForm, null);

                Color restoredLightBg = child2.BackColor;
                AssertEqual(UIThemeHelper.ColorBackground, restoredLightBg, "Child form must switch back to Light theme background");

                Console.WriteLine(string.Format("       Live Dispatch Verified: Child1 Light Bg={0} -> Dark Bg={1}; Child2 Initial Dark Bg={2} -> Revert Light Bg={3}",
                    lightBg, darkBg, newChildDarkBg, restoredLightBg));
            }
        }

        private static double CalculateLuminance(Color c)
        {
            double r = c.R / 255.0;
            double g = c.G / 255.0;
            double b = c.B / 255.0;

            r = (r <= 0.03928) ? r / 12.92 : Math.Pow((r + 0.055) / 1.055, 2.4);
            g = (g <= 0.03928) ? g / 12.92 : Math.Pow((g + 0.055) / 1.055, 2.4);
            b = (b <= 0.03928) ? b / 12.92 : Math.Pow((b + 0.055) / 1.055, 2.4);

            return 0.2126 * r + 0.7152 * g + 0.0722 * b;
        }

        private static double ContrastRatio(Color c1, Color c2)
        {
            double l1 = CalculateLuminance(c1);
            double l2 = CalculateLuminance(c2);
            double bright = Math.Max(l1, l2);
            double dark = Math.Min(l1, l2);
            return (bright + 0.05) / (dark + 0.05);
        }

        private static void Test08_ThemeEngine_WCAGContrastRatios()
        {
            UIThemeHelper.IsDarkMode = true;
            Color darkBg = UIThemeHelper.ColorBackground;
            Color darkCard = UIThemeHelper.ColorCard;
            Color darkTextPrimary = UIThemeHelper.ColorTextPrimary;
            Color darkTextSecondary = UIThemeHelper.ColorTextSecondary;

            double rDarkPriBg = ContrastRatio(darkTextPrimary, darkBg);
            AssertTrue(rDarkPriBg >= 10.0, "Dark Mode Primary Text vs Background ratio must be >= 10.0:1");

            double rDarkPriCard = ContrastRatio(darkTextPrimary, darkCard);
            AssertTrue(rDarkPriCard >= 10.0, "Dark Mode Primary Text vs Card ratio must be >= 10.0:1");

            double rDarkSecCard = ContrastRatio(darkTextSecondary, darkCard);
            AssertTrue(rDarkSecCard >= 4.5, "Dark Mode Secondary Text vs Card ratio must be >= 4.5:1");

            UIThemeHelper.IsDarkMode = false;
            Color lightBg = UIThemeHelper.ColorBackground;
            Color lightCard = UIThemeHelper.ColorCard;
            Color lightTextPrimary = UIThemeHelper.ColorTextPrimary;
            Color lightTextSecondary = UIThemeHelper.ColorTextSecondary;

            double rLightPriBg = ContrastRatio(lightTextPrimary, lightBg);
            AssertTrue(rLightPriBg >= 10.0, "Light Mode Primary Text vs Background ratio must be >= 10.0:1");

            double rLightPriCard = ContrastRatio(lightTextPrimary, lightCard);
            AssertTrue(rLightPriCard >= 10.0, "Light Mode Primary Text vs Card ratio must be >= 10.0:1");

            double rLightSecCard = ContrastRatio(lightTextSecondary, lightCard);
            AssertTrue(rLightSecCard >= 4.5, "Light Mode Secondary Text vs Card ratio must be >= 4.5:1");

            UIThemeHelper.IsDarkMode = true;
            double rDarkGridHeader = ContrastRatio(Color.White, UIThemeHelper.ColorHeaderGrid);
            AssertTrue(rDarkGridHeader >= 10.0, "Dark Grid Header contrast must be >= 10.0:1");

            double rDarkGridSelected = ContrastRatio(UIThemeHelper.ColorRowSelectedText, UIThemeHelper.ColorRowSelected);
            AssertTrue(rDarkGridSelected >= 4.5, "Dark Grid Selected row contrast must be >= 4.5:1");

            UIThemeHelper.IsDarkMode = false;
            double rLightGridHeader = ContrastRatio(Color.White, UIThemeHelper.ColorHeaderGrid);
            AssertTrue(rLightGridHeader >= 10.0, "Light Grid Header contrast must be >= 10.0:1");

            double rLightGridSelected = ContrastRatio(UIThemeHelper.ColorRowSelectedText, UIThemeHelper.ColorRowSelected);
            AssertTrue(rLightGridSelected >= 4.5, "Light Grid Selected row contrast must be >= 4.5:1");

            Console.WriteLine(string.Format("       Dark Mode Contrast: Primary/Bg={0:F2}:1, Primary/Card={1:F2}:1, Secondary/Card={2:F2}:1, GridHeader={3:F2}:1, GridSel={4:F2}:1",
                rDarkPriBg, rDarkPriCard, rDarkSecCard, rDarkGridHeader, rDarkGridSelected));
            Console.WriteLine(string.Format("       Light Mode Contrast: Primary/Bg={0:F2}:1, Primary/Card={1:F2}:1, Secondary/Card={2:F2}:1, GridHeader={3:F2}:1, GridSel={4:F2}:1",
                rLightPriBg, rLightPriCard, rLightSecCard, rLightGridHeader, rLightGridSelected));
        }

        private static void Test09_ZeroComments_ForensicAudit()
        {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            DirectoryInfo dir = new DirectoryInfo(baseDir);
            while (dir != null && !File.Exists(Path.Combine(dir.FullName, "QLBanMayViTinh.csproj")))
            {
                dir = dir.Parent;
            }

            AssertNotNull(dir, "Project root must be found");

            string[] csFiles = Directory.GetFiles(dir.FullName, "*.cs", SearchOption.AllDirectories);
            string[] sqlFiles = Directory.GetFiles(dir.FullName, "*.sql", SearchOption.AllDirectories);

            List<string> violations = new List<string>();
            int checkedFiles = 0;

            foreach (string file in csFiles)
            {
                if (file.Contains("\\obj\\") || file.Contains("\\bin\\") || file.Contains("\\.git\\"))
                    continue;

                checkedFiles++;
                string[] lines = File.ReadAllLines(file);
                for (int i = 0; i < lines.Length; i++)
                {
                    string t = lines[i].Trim();
                    if (t.StartsWith("//") || t.StartsWith("/*") || t.StartsWith("///"))
                    {
                        violations.Add(string.Format("{0}:{1} -> {2}", Path.GetFileName(file), i + 1, t));
                    }
                }
            }

            foreach (string file in sqlFiles)
            {
                if (file.Contains("\\obj\\") || file.Contains("\\bin\\") || file.Contains("\\.git\\"))
                    continue;

                checkedFiles++;
                string[] lines = File.ReadAllLines(file);
                for (int i = 0; i < lines.Length; i++)
                {
                    string t = lines[i].Trim();
                    if (t.StartsWith("--") || t.StartsWith("/*"))
                    {
                        violations.Add(string.Format("{0}:{1} -> {2}", Path.GetFileName(file), i + 1, t));
                    }
                }
            }

            AssertEqual(0, violations.Count, string.Format("Zero comments violation detected in {0} locations", violations.Count));
            Console.WriteLine(string.Format("       Forensic Audit: Scanned {0} files (C# and SQL). Exactly 0 comments detected.", checkedFiles));
        }
    }
}
