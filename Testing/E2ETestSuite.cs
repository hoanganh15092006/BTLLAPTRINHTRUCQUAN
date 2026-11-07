using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using QLBanMayViTinh.Classes;
using QLBanMayViTinh.Forms;

namespace QLBanMayViTinh.Testing
{
    public class TestCaseResult
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Tier { get; set; }
        public bool Passed { get; set; }
        public string Message { get; set; }
        public string ErrorDetails { get; set; }
        public long ElapsedMilliseconds { get; set; }
    }

    public class TestAssertionException : Exception
    {
        public TestAssertionException(string message) : base(message)
        {
        }
    }

    public static class E2ETestSuite
    {
        private static byte[] CreateSyntheticPng(int width, int height, Color color)
        {
            using (Bitmap bmp = new Bitmap(width, height))
            {
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    using (SolidBrush brush = new SolidBrush(color))
                    {
                        g.FillRectangle(brush, 0, 0, width, height);
                    }
                }
                using (MemoryStream ms = new MemoryStream())
                {
                    bmp.Save(ms, ImageFormat.Png);
                    return ms.ToArray();
                }
            }
        }

        private static void AssertTrue(bool condition, string message)
        {
            if (!condition)
            {
                throw new TestAssertionException(message);
            }
        }

        private static void AssertFalse(bool condition, string message)
        {
            if (condition)
            {
                throw new TestAssertionException(message);
            }
        }

        private static void AssertNotNull(object obj, string message)
        {
            if (obj == null)
            {
                throw new TestAssertionException(message);
            }
        }

        private static void AssertNull(object obj, string message)
        {
            if (obj != null)
            {
                throw new TestAssertionException(message);
            }
        }

        private static void SafeApplyTheme(Control c)
        {
            if (c == null) return;
            MethodInfo m = typeof(UIThemeHelper).GetMethod("ApplyTheme", BindingFlags.Public | BindingFlags.Static);
            if (m != null)
            {
                m.Invoke(null, new object[] { c });
            }
        }

        private static void AssertEqual<T>(T expected, T actual, string message)
        {
            if (!EqualityComparer<T>.Default.Equals(expected, actual))
            {
                throw new TestAssertionException(string.Format("{0}. Expected: [{1}], Actual: [{2}]", message, expected, actual));
            }
        }

        public static List<TestCaseResult> RunAllTests(int filterTier = 0)
        {
            List<TestCaseResult> results = new List<TestCaseResult>();
            List<Func<TestCaseResult>> tests = new List<Func<TestCaseResult>>
            {
                T1_01_DatabaseTablesCount,
                T1_02_ComputerModelsVolume,
                T1_03_ComputerCategoriesDiversity,
                T1_04_ImageVarbinaryDataType,
                T1_05_ImageBinaryMagicBytes,
                T1_06_SafeBinaryImageConversion,
                T1_07_ParameterizedSqlSupport,
                T1_08_DesignerCompatibilityFormsPurity,
                T1_09_ThemeEngineColorPalettes,
                T1_10_StreamlinedLoginLayout,
                T1_11_SecurityPasswordHashing,
                T1_12_SecurityDataEncryption,
                T2_01_NullAndEmptyImageHandling,
                T2_02_MalformedCorruptImageHandling,
                T2_03_ZeroAndExtremePricing,
                T2_04_NegativeStockTriggerPrevention,
                T2_05_RapidThemeToggleStress,
                T2_06_AuthenticationInvalidCredentials,
                T3_01_BinaryImageInsertSearchAndTheme,
                T3_02_LiveThemeDispatchMultipleChildForms,
                T3_03_ThemePersistenceInDatabase,
                T3_04_WholesalePricingTriggerRecalculation,
                T4_01_CompleteStoreWorkflowLifecycle,
                T4_02_InventoryRestorationOnSaleItemRemoval,
                T4_03_ZeroCommentComplianceAudit
            };

            foreach (Func<TestCaseResult> test in tests)
            {
                int methodTier = 1;
                string methodName = test.Method.Name;
                if (methodName.StartsWith("T1_")) methodTier = 1;
                else if (methodName.StartsWith("T2_")) methodTier = 2;
                else if (methodName.StartsWith("T3_")) methodTier = 3;
                else if (methodName.StartsWith("T4_")) methodTier = 4;

                string formattedId = methodName.Length >= 5 ? methodName.Substring(0, 5).Replace('_', '.') : methodName;

                System.Diagnostics.Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();
                TestCaseResult res = null;
                try
                {
                    res = test();
                }
                catch (TestAssertionException ex)
                {
                    res = new TestCaseResult
                    {
                        Id = formattedId,
                        Name = methodName,
                        Tier = methodTier,
                        Passed = false,
                        Message = ex.Message,
                        ErrorDetails = ex.ToString()
                    };
                }
                catch (Exception ex)
                {
                    res = new TestCaseResult
                    {
                        Id = formattedId,
                        Name = methodName,
                        Tier = methodTier,
                        Passed = false,
                        Message = "Unexpected exception: " + ex.Message,
                        ErrorDetails = ex.ToString()
                    };
                }
                sw.Stop();
                res.ElapsedMilliseconds = sw.ElapsedMilliseconds;

                if (filterTier == 0 || res.Tier == filterTier)
                {
                    results.Add(res);
                }
            }

            return results;
        }

        public static TestCaseResult T1_01_DatabaseTablesCount()
        {
            Functions.Connect();
            AssertNotNull(Functions.Conn, "SQL Connection must not be null");
            AssertTrue(Functions.Conn.State == ConnectionState.Open, "SQL Connection must be open");

            string sql = @"SELECT COUNT(TABLE_NAME) FROM INFORMATION_SCHEMA.TABLES
                           WHERE TABLE_TYPE = 'BASE TABLE' AND TABLE_NAME IN (
                           'LoaiMay', 'Chip', 'OCung', 'DungLuong', 'TocDo', 'OCD',
                           'ManHinh', 'CoMH', 'Chuot', 'BanPhim', 'USB', 'RAM',
                           'Loa', 'HangSanXuat', 'NhanVien', 'KhachHang', 'NhaCungCap',
                           'MayViTinh', 'HoaDonNhap', 'ChiTietHDN', 'HoaDonBan', 'ChiTietHDB',
                           'tblTaiKhoan', 'tblNhatKyHoatDong', 'tblCauHinhHeThong')";

            string countStr = Functions.GetFieldValues(sql);
            int count = 0;
            int.TryParse(countStr, out count);

            AssertTrue(count >= 25, string.Format("Database must contain at least 25 normalized tables. Found: {0}", count));

            return new TestCaseResult
            {
                Id = "T1.01",
                Name = "Database Tables Count (25 3NF Tables)",
                Tier = 1,
                Passed = true,
                Message = string.Format("Found {0} normalized tables in QLBanMayViTinh schema.", count)
            };
        }

        public static TestCaseResult T1_02_ComputerModelsVolume()
        {
            Functions.Connect();
            string sql = "SELECT COUNT(*) FROM MayViTinh";
            string countStr = Functions.GetFieldValues(sql);
            int count = 0;
            int.TryParse(countStr, out count);

            AssertTrue(count >= 32, string.Format("Requirement R1 mandates over 30 computer models. Expected >= 32, Found: {0}", count));

            return new TestCaseResult
            {
                Id = "T1.02",
                Name = "Computer Models Volume (>= 32 Records)",
                Tier = 1,
                Passed = true,
                Message = string.Format("Verified {0} computer models present in MayViTinh table.", count)
            };
        }

        public static TestCaseResult T1_03_ComputerCategoriesDiversity()
        {
            Functions.Connect();
            DataTable dt = Functions.GetDataToTable("SELECT DISTINCT MaLoai FROM MayViTinh");
            List<string> loaiList = new List<string>();
            foreach (DataRow r in dt.Rows)
            {
                loaiList.Add(r["MaLoai"].ToString().Trim());
            }

            AssertTrue(loaiList.Contains("LM01"), "Models must cover Laptop category (LM01)");
            AssertTrue(loaiList.Contains("LM02"), "Models must cover Gaming PC category (LM02)");
            AssertTrue(loaiList.Contains("LM03"), "Models must cover All-in-One / Office category (LM03)");
            AssertTrue(loaiList.Contains("LM04"), "Models must cover Workstation category (LM04)");

            return new TestCaseResult
            {
                Id = "T1.03",
                Name = "Computer Categories Diversity (4 Segments)",
                Tier = 1,
                Passed = true,
                Message = "Verified all 4 segments (LM01, LM02, LM03, LM04) are populated."
            };
        }

        public static TestCaseResult T1_04_ImageVarbinaryDataType()
        {
            Functions.Connect();
            string sql = "SELECT DATA_TYPE, CHARACTER_MAXIMUM_LENGTH FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME='MayViTinh' AND COLUMN_NAME='Anh'";
            DataTable dt = Functions.GetDataToTable(sql);

            AssertTrue(dt.Rows.Count > 0, "Column Anh must exist in MayViTinh");
            string dataType = dt.Rows[0]["DATA_TYPE"].ToString().ToLowerInvariant();

            AssertEqual("varbinary", dataType, "Requirement R1 mandates VARBINARY storage for MayViTinh.Anh");

            return new TestCaseResult
            {
                Id = "T1.04",
                Name = "Image Column Data Type (VARBINARY)",
                Tier = 1,
                Passed = true,
                Message = "Verified MayViTinh.Anh is VARBINARY(MAX)."
            };
        }

        public static TestCaseResult T1_05_ImageBinaryMagicBytes()
        {
            Functions.Connect();
            DataTable dt = Functions.GetDataToTable("SELECT TOP 5 MaMayVT, Anh FROM MayViTinh WHERE Anh IS NOT NULL");
            AssertTrue(dt.Rows.Count > 0, "MayViTinh must contain records with non-null Anh binary image");

            int validHeaderCount = 0;
            foreach (DataRow row in dt.Rows)
            {
                if (row["Anh"] is byte[] bytes && bytes.Length >= 4)
                {
                    bool isPng = bytes[0] == 0x89 && bytes[1] == 0x50 && bytes[2] == 0x4E && bytes[3] == 0x47;
                    bool isJpg = bytes[0] == 0xFF && bytes[1] == 0xD8;
                    bool isBmp = bytes[0] == 0x42 && bytes[1] == 0x4D;
                    if (isPng || isJpg || isBmp)
                    {
                        validHeaderCount++;
                    }
                }
            }

            AssertTrue(validHeaderCount > 0, "Stored images in MayViTinh must contain valid image format magic bytes (PNG/JPEG/BMP)");

            return new TestCaseResult
            {
                Id = "T1.05",
                Name = "Image Binary Magic Bytes Verification",
                Tier = 1,
                Passed = true,
                Message = string.Format("Verified {0} models have valid image magic headers.", validHeaderCount)
            };
        }

        public static TestCaseResult T1_06_SafeBinaryImageConversion()
        {
            MethodInfo mToImage = typeof(Functions).GetMethod("ByteArrayToImage", BindingFlags.Public | BindingFlags.Static);
            MethodInfo mToBytes = typeof(Functions).GetMethod("ImageToByteArray", BindingFlags.Public | BindingFlags.Static);

            AssertNotNull(mToImage, "Functions.ByteArrayToImage must be implemented per Interface Contract");
            AssertNotNull(mToBytes, "Functions.ImageToByteArray must be implemented per Interface Contract");

            byte[] originalBytes = CreateSyntheticPng(32, 24, Color.Blue);
            Image img = (Image)mToImage.Invoke(null, new object[] { originalBytes });

            AssertNotNull(img, "ByteArrayToImage must produce a non-null Image for valid PNG bytes");
            AssertEqual(32, img.Width, "Image width must match 32px");
            AssertEqual(24, img.Height, "Image height must match 24px");

            byte[] serialized = (byte[])mToBytes.Invoke(null, new object[] { img });
            img.Dispose();

            AssertNotNull(serialized, "ImageToByteArray must produce non-null byte array");
            AssertTrue(serialized.Length > 0, "Serialized byte array must have length > 0");

            return new TestCaseResult
            {
                Id = "T1.06",
                Name = "Safe Binary Image Conversion in DAL",
                Tier = 1,
                Passed = true,
                Message = "ByteArrayToImage and ImageToByteArray round-trip successfully."
            };
        }

        public static TestCaseResult T1_07_ParameterizedSqlSupport()
        {
            MethodInfo mRunSqlParam = typeof(Functions).GetMethod("RunSQL",
                BindingFlags.Public | BindingFlags.Static, null,
                new Type[] { typeof(string), typeof(SqlParameter[]), typeof(bool) }, null);

            if (mRunSqlParam == null)
            {
                mRunSqlParam = typeof(Functions).GetMethod("RunSQL",
                    BindingFlags.Public | BindingFlags.Static, null,
                    new Type[] { typeof(string), typeof(SqlParameter[]) }, null);
            }

            AssertNotNull(mRunSqlParam, "Functions.RunSQL(string, SqlParameter[]) overload must be implemented");

            string testKey = "PARAM_TEST_CONFIG";
            byte[] testData = new byte[] { 0xAA, 0xBB, 0xCC, 0xDD };

            try
            {
                Functions.RunSQL(string.Format("DELETE FROM tblCauHinhHeThong WHERE KeyName = '{0}'", testKey), false);

                string insertSql = "INSERT INTO tblCauHinhHeThong (KeyName, KeyValue) VALUES (@k, @v)";
                SqlParameter[] p = new SqlParameter[]
                {
                    new SqlParameter("@k", testKey),
                    new SqlParameter("@v", Convert.ToBase64String(testData))
                };

                if (mRunSqlParam.GetParameters().Length == 3)
                {
                    mRunSqlParam.Invoke(null, new object[] { insertSql, p, false });
                }
                else
                {
                    mRunSqlParam.Invoke(null, new object[] { insertSql, p });
                }

                string retrieved = Functions.GetFieldValues(string.Format("SELECT KeyValue FROM tblCauHinhHeThong WHERE KeyName = '{0}'", testKey));
                AssertEqual(Convert.ToBase64String(testData), retrieved, "Retrieved value must match parameterized inserted value");

                return new TestCaseResult
                {
                    Id = "T1.07",
                    Name = "Parameterized SQL Support in DAL",
                    Tier = 1,
                    Passed = true,
                    Message = "Parameterized query executed and verified with SqlParameter."
                };
            }
            finally
            {
                Functions.RunSQL(string.Format("DELETE FROM tblCauHinhHeThong WHERE KeyName = '{0}'", testKey), false);
            }
        }

        public static TestCaseResult T1_08_DesignerCompatibilityFormsPurity()
        {
            Type[] formTypes = new Type[]
            {
                typeof(frmDangNhap),
                typeof(frmDangKy),
                typeof(frmQuenMatKhau),
                typeof(frmHoSoCaNhan),
                typeof(frmQuanLyNguoiDung),
                typeof(frmDanhMucHeThong),
                typeof(frmCaiDat),
                typeof(frmNhatKyHoatDong),
                typeof(frmMayViTinh),
                typeof(frmHoaDonNhap),
                typeof(frmHoaDonBan),
                typeof(frmTimKiem),
                typeof(frmBaoCaoThongKe)
            };

            List<string> successful = new List<string>();
            foreach (Type t in formTypes)
            {
                ConstructorInfo ctor = t.GetConstructor(Type.EmptyTypes);
                AssertNotNull(ctor, string.Format("Form {0} must have a public parameterless constructor for VS Designer", t.Name));

                using (Form f = (Form)ctor.Invoke(null))
                {
                    AssertNotNull(f, string.Format("Form {0} must instantiate without error", t.Name));
                    successful.Add(t.Name);
                }
            }

            return new TestCaseResult
            {
                Id = "T1.08",
                Name = "VS Designer Compatibility (13 Forms Purity)",
                Tier = 1,
                Passed = true,
                Message = string.Format("All {0} forms instantiated with zero designtime exceptions.", successful.Count)
            };
        }

        public static TestCaseResult T1_09_ThemeEngineColorPalettes()
        {
            PropertyInfo propIsDark = typeof(UIThemeHelper).GetProperty("IsDarkMode", BindingFlags.Public | BindingFlags.Static);
            FieldInfo fieldIsDark = typeof(UIThemeHelper).GetField("IsDarkMode", BindingFlags.Public | BindingFlags.Static);

            AssertTrue(propIsDark != null || fieldIsDark != null, "UIThemeHelper must expose IsDarkMode property or field");

            Action<bool> setMode = (isDark) =>
            {
                if (propIsDark != null) propIsDark.SetValue(null, isDark);
                else fieldIsDark.SetValue(null, isDark);
            };

            setMode(false);
            Color lightBg = UIThemeHelper.ColorBackground;
            Color lightCard = UIThemeHelper.ColorCard;
            Color lightText = UIThemeHelper.ColorTextPrimary;

            setMode(true);
            Color darkBg = UIThemeHelper.ColorBackground;
            Color darkCard = UIThemeHelper.ColorCard;
            Color darkText = UIThemeHelper.ColorTextPrimary;

            setMode(false);

            AssertTrue(lightBg.GetBrightness() > 0.6f, "Light mode background must be high brightness");
            AssertTrue(darkBg.GetBrightness() < 0.4f, "Dark mode background must be low brightness");
            AssertTrue(lightText.GetBrightness() < darkText.GetBrightness(), "Dark mode text must be brighter than Light mode text to prevent dark-on-dark");

            return new TestCaseResult
            {
                Id = "T1.09",
                Name = "Theme Engine Color Palettes & Contrast",
                Tier = 1,
                Passed = true,
                Message = "Verified high-contrast color tokens for Dark and Light modes."
            };
        }

        public static TestCaseResult T1_10_StreamlinedLoginLayout()
        {
            using (frmDangNhap form = new frmDangNhap())
            {
                Control[] pnlLeftControls = form.Controls.Find("pnlLeft", true);
                AssertTrue(pnlLeftControls.Length == 0, "Requirement R4 mandates elimination of pnlLeft intro panel");

                Control[] obsoleteLabels = form.Controls.Find("lblFeature1", true);
                AssertTrue(obsoleteLabels.Length == 0, "Marketing feature labels must be removed from login form");

                Control loginCard = null;
                Control[] foundCards = form.Controls.Find("pnlLoginCard", true);
                if (foundCards.Length > 0)
                {
                    loginCard = foundCards[0];
                }
                else
                {
                    Control[] foundRights = form.Controls.Find("pnlRight", true);
                    if (foundRights.Length > 0) loginCard = foundRights[0];
                }

                AssertNotNull(loginCard, "Login form must contain centered login card panel");

                return new TestCaseResult
                {
                    Id = "T1.10",
                    Name = "Streamlined Login Form Layout (R4)",
                    Tier = 1,
                    Passed = true,
                    Message = "Login intro panel removed and login controls structured into card layout."
                };
            }
        }

        public static TestCaseResult T1_11_SecurityPasswordHashing()
        {
            string password = "EnterprisePassword#2026";
            string salt1, salt2;

            string hash1 = SecurityHelper.HashPassword(password, out salt1);
            string hash2 = SecurityHelper.HashPassword(password, out salt2);

            AssertFalse(string.IsNullOrEmpty(salt1), "Salt must not be empty");
            AssertFalse(salt1 == salt2, "Each password hash must generate a unique cryptographic salt");
            AssertTrue(SecurityHelper.VerifyPassword(password, hash1, salt1), "VerifyPassword must succeed for valid password and salt");
            AssertFalse(SecurityHelper.VerifyPassword("IncorrectPassword", hash1, salt1), "VerifyPassword must reject incorrect password");

            return new TestCaseResult
            {
                Id = "T1.11",
                Name = "Security PBKDF2 Password Hashing",
                Tier = 1,
                Passed = true,
                Message = "PBKDF2 hashing, salting, and verification verified."
            };
        }

        public static TestCaseResult T1_12_SecurityDataEncryption()
        {
            string plainText = "Bảo mật thông tin: Khách hàng VIP 0987.654.321";
            string encrypted = SecurityHelper.EncryptAES(plainText);

            AssertTrue(encrypted.StartsWith("ENC:"), "AES encrypted string must carry ENC: prefix");
            AssertFalse(encrypted.Contains("0987.654.321"), "Encrypted ciphertext must not expose plaintext data");

            string decrypted = SecurityHelper.DecryptAES(encrypted);
            AssertEqual(plainText, decrypted, "Decrypted text must match original plaintext exactly");

            return new TestCaseResult
            {
                Id = "T1.12",
                Name = "Security AES-256 Data Encryption",
                Tier = 1,
                Passed = true,
                Message = "AES-256 encryption and decryption round-trip verified."
            };
        }

        public static TestCaseResult T2_01_NullAndEmptyImageHandling()
        {
            MethodInfo mToImage = typeof(Functions).GetMethod("ByteArrayToImage", BindingFlags.Public | BindingFlags.Static);
            AssertNotNull(mToImage, "Functions.ByteArrayToImage must exist");

            Image imgNull = (Image)mToImage.Invoke(null, new object[] { null });
            AssertNull(imgNull, "ByteArrayToImage(null) must return null without throwing exception");

            Image imgEmpty = (Image)mToImage.Invoke(null, new object[] { new byte[0] });
            AssertNull(imgEmpty, "ByteArrayToImage(new byte[0]) must return null without throwing exception");

            return new TestCaseResult
            {
                Id = "T2.01",
                Name = "Null and Empty Image Handling",
                Tier = 2,
                Passed = true,
                Message = "Null and empty byte arrays handled safely."
            };
        }

        public static TestCaseResult T2_02_MalformedCorruptImageHandling()
        {
            MethodInfo mToImage = typeof(Functions).GetMethod("ByteArrayToImage", BindingFlags.Public | BindingFlags.Static);
            AssertNotNull(mToImage, "Functions.ByteArrayToImage must exist");

            byte[] corrupt = new byte[] { 0x01, 0x02, 0x03, 0xFF, 0xFE, 0xAA, 0xBB };
            Image img = (Image)mToImage.Invoke(null, new object[] { corrupt });

            AssertNull(img, "ByteArrayToImage with corrupt bytes must gracefully return null");

            return new TestCaseResult
            {
                Id = "T2.02",
                Name = "Malformed Corrupted Image Payload Handling",
                Tier = 2,
                Passed = true,
                Message = "Corrupted byte payload handled gracefully without GDI+ crash."
            };
        }

        public static TestCaseResult T2_03_ZeroAndExtremePricing()
        {
            Functions.Connect();
            string testIdZero = "E2E_PRICE_ZERO";
            string testIdExtreme = "E2E_PRICE_MAX";

            try
            {
                Functions.RunSQL(string.Format("DELETE FROM MayViTinh WHERE MaMayVT IN ('{0}', '{1}')", testIdZero, testIdExtreme), false);

                string sqlZero = string.Format(@"INSERT INTO MayViTinh (MaMayVT, TenMayVT, MaLoai, MaChip, MaOCung, MaDungLuong,
                    MaTocDo, MaOCD, MaManHinh, MaCoMH, MaChuot, MaBanPhim, MaUSB, MaRAM, MaLoa, MaHangSX,
                    GiaNhap, GiaBan, ThoiGianBH, SoLuong, Anh, GhiChu)
                    VALUES ('{0}', N'Test 0 VND', 'LM01', 'CPU01', 'OC01', 'DL01', 'TD01', 'CD01',
                    'MH01', 'CO01', 'CH01', 'BP01', 'USB01', 'RAM01', 'LOA01', 'HSX01',
                    0, 0, 12, 0, NULL, N'Zero Price')", testIdZero);
                Functions.RunSQL(sqlZero, false);

                decimal extremePrice = 999999999m;
                string sqlExtreme = string.Format(@"INSERT INTO MayViTinh (MaMayVT, TenMayVT, MaLoai, MaChip, MaOCung, MaDungLuong,
                    MaTocDo, MaOCD, MaManHinh, MaCoMH, MaChuot, MaBanPhim, MaUSB, MaRAM, MaLoa, MaHangSX,
                    GiaNhap, GiaBan, ThoiGianBH, SoLuong, Anh, GhiChu)
                    VALUES ('{0}', N'Test Max Price', 'LM01', 'CPU01', 'OC01', 'DL01', 'TD01', 'CD01',
                    'MH01', 'CO01', 'CH01', 'BP01', 'USB01', 'RAM01', 'LOA01', 'HSX01',
                    {1}, {1}, 24, 1, NULL, N'Extreme Price')", testIdExtreme, extremePrice);
                Functions.RunSQL(sqlExtreme, false);

                string readZero = Functions.GetFieldValues(string.Format("SELECT GiaNhap FROM MayViTinh WHERE MaMayVT = '{0}'", testIdZero));
                string readMax = Functions.GetFieldValues(string.Format("SELECT GiaNhap FROM MayViTinh WHERE MaMayVT = '{0}'", testIdExtreme));

                AssertEqual("0", Convert.ToDecimal(readZero).ToString("0"), "Zero price must persist accurately");
                AssertEqual("999999999", Convert.ToDecimal(readMax).ToString("0"), "Extreme price must persist accurately without decimal overflow");

                return new TestCaseResult
                {
                    Id = "T2.03",
                    Name = "Zero and Extreme Financial Price Boundaries",
                    Tier = 2,
                    Passed = true,
                    Message = "0 VND and 999,999,999 VND boundaries verified."
                };
            }
            finally
            {
                Functions.RunSQL(string.Format("DELETE FROM MayViTinh WHERE MaMayVT IN ('{0}', '{1}')", testIdZero, testIdExtreme), false);
            }
        }

        public static TestCaseResult T2_04_NegativeStockTriggerPrevention()
        {
            Functions.Connect();
            string testMay = "E2E_NEG_PC";
            string testHdb = "E2E_NEG_HDB";

            try
            {
                Functions.RunSQL(string.Format("DELETE FROM ChiTietHDB WHERE MaHDB = '{0}'", testHdb), false);
                Functions.RunSQL(string.Format("DELETE FROM HoaDonBan WHERE MaHDB = '{0}'", testHdb), false);
                Functions.RunSQL(string.Format("DELETE FROM MayViTinh WHERE MaMayVT = '{0}'", testMay), false);

                string sqlAddMay = string.Format(@"INSERT INTO MayViTinh (MaMayVT, TenMayVT, MaLoai, MaChip, MaOCung, MaDungLuong,
                    MaTocDo, MaOCD, MaManHinh, MaCoMH, MaChuot, MaBanPhim, MaUSB, MaRAM, MaLoa, MaHangSX,
                    GiaNhap, GiaBan, ThoiGianBH, SoLuong, Anh, GhiChu)
                    VALUES ('{0}', N'Test Trigger Neg', 'LM01', 'CPU01', 'OC01', 'DL01', 'TD01', 'CD01',
                    'MH01', 'CO01', 'CH01', 'BP01', 'USB01', 'RAM01', 'LOA01', 'HSX01',
                    10000000, 11000000, 12, 2, NULL, N'Inventory 2 units')", testMay);
                Functions.RunSQL(sqlAddMay, false);

                string sqlAddHdb = string.Format("INSERT INTO HoaDonBan (MaHDB, NgayBan, MaNV, MaKhach, TongTien) VALUES ('{0}', GETDATE(), 'NV01', 'KH01', 0)", testHdb);
                Functions.RunSQL(sqlAddHdb, false);

                bool exceptionCaught = false;
                try
                {
                    string sqlOversell = string.Format("INSERT INTO ChiTietHDB (MaHDB, MaMayVT, SoLuong, DonGia, ThanhTien) VALUES ('{0}', '{1}', 10, 11000000, 0)", testHdb, testMay);
                    Functions.RunSQL(sqlOversell, false);
                }
                catch
                {
                    exceptionCaught = true;
                }

                AssertTrue(exceptionCaught, "Database trigger trg_ChiTietHDB_AUID must reject overselling when quantity exceeds inventory");

                string remaining = Functions.GetFieldValues(string.Format("SELECT SoLuong FROM MayViTinh WHERE MaMayVT = '{0}'", testMay));
                AssertEqual("2", remaining, "Inventory must remain unchanged at 2 units after failed transaction");

                return new TestCaseResult
                {
                    Id = "T2.04",
                    Name = "Negative Stock Trigger Prevention",
                    Tier = 2,
                    Passed = true,
                    Message = "Database trigger trg_ChiTietHDB_AUID successfully blocked negative inventory."
                };
            }
            finally
            {
                Functions.RunSQL(string.Format("DELETE FROM ChiTietHDB WHERE MaHDB = '{0}'", testHdb), false);
                Functions.RunSQL(string.Format("DELETE FROM HoaDonBan WHERE MaHDB = '{0}'", testHdb), false);
                Functions.RunSQL(string.Format("DELETE FROM MayViTinh WHERE MaMayVT = '{0}'", testMay), false);
            }
        }

        public static TestCaseResult T2_05_RapidThemeToggleStress()
        {
            PropertyInfo propIsDark = typeof(UIThemeHelper).GetProperty("IsDarkMode", BindingFlags.Public | BindingFlags.Static);
            FieldInfo fieldIsDark = typeof(UIThemeHelper).GetField("IsDarkMode", BindingFlags.Public | BindingFlags.Static);

            Action<bool> setMode = (isDark) =>
            {
                if (propIsDark != null) propIsDark.SetValue(null, isDark);
                else fieldIsDark.SetValue(null, isDark);
            };

            using (frmMayViTinh form = new frmMayViTinh())
            {
                for (int i = 0; i < 50; i++)
                {
                    setMode(i % 2 == 0);
                    SafeApplyTheme(form);
                }
            }

            setMode(false);

            return new TestCaseResult
            {
                Id = "T2.05",
                Name = "Rapid Theme Toggle Stress (50 Iterations)",
                Tier = 2,
                Passed = true,
                Message = "Form withstood 50 consecutive theme cycles with zero GDI errors."
            };
        }

        public static TestCaseResult T2_06_AuthenticationInvalidCredentials()
        {
            string salt;
            string hash = SecurityHelper.HashPassword("CorrectPassword@123", out salt);

            AssertFalse(SecurityHelper.VerifyPassword("WrongPassword", hash, salt), "Wrong password must be rejected");
            AssertFalse(SecurityHelper.VerifyPassword("", hash, salt), "Empty password must be rejected");
            AssertFalse(SecurityHelper.VerifyPassword("' OR '1'='1", hash, salt), "SQL injection pattern in password must be rejected");
            AssertFalse(SecurityHelper.VerifyPassword(null, hash, salt), "Null password must be rejected");

            return new TestCaseResult
            {
                Id = "T2.06",
                Name = "Authentication Invalid Credentials & SQLi Guard",
                Tier = 2,
                Passed = true,
                Message = "PBKDF2 verification rejected invalid and adversarial credentials."
            };
        }

        public static TestCaseResult T3_01_BinaryImageInsertSearchAndTheme()
        {
            Functions.Connect();
            string testId = "E2E_MVT_T301";
            byte[] syntheticPng = CreateSyntheticPng(48, 48, Color.DarkGreen);

            try
            {
                Functions.RunSQL(string.Format("DELETE FROM MayViTinh WHERE MaMayVT = '{0}'", testId), false);

                string insertSql = @"INSERT INTO MayViTinh (MaMayVT, TenMayVT, MaLoai, MaChip, MaOCung, MaDungLuong,
                    MaTocDo, MaOCD, MaManHinh, MaCoMH, MaChuot, MaBanPhim, MaUSB, MaRAM, MaLoa, MaHangSX,
                    GiaNhap, GiaBan, ThoiGianBH, SoLuong, Anh, GhiChu)
                    VALUES (@id, N'Cross Feature PC', 'LM01', 'CPU01', 'OC01', 'DL01', 'TD01', 'CD01',
                    'MH01', 'CO01', 'CH01', 'BP01', 'USB01', 'RAM01', 'LOA01', 'HSX01',
                    15000000, 16500000, 12, 5, @img, N'E2E Cross Feature')";

                using (SqlCommand cmd = new SqlCommand(insertSql, Functions.Conn))
                {
                    cmd.Parameters.AddWithValue("@id", testId);
                    cmd.Parameters.Add("@img", SqlDbType.VarBinary, -1).Value = syntheticPng;
                    cmd.ExecuteNonQuery();
                }

                DataTable dt = Functions.GetDataToTable(string.Format("SELECT MaMayVT, Anh FROM MayViTinh WHERE MaMayVT = '{0}'", testId));
                AssertTrue(dt.Rows.Count > 0, "Inserted test model must be searchable");

                byte[] loadedBytes = dt.Rows[0]["Anh"] as byte[];
                AssertNotNull(loadedBytes, "Loaded Anh column must be non-null byte array");
                AssertEqual(syntheticPng.Length, loadedBytes.Length, "Loaded byte length must match inserted PNG length");

                MethodInfo mToImage = typeof(Functions).GetMethod("ByteArrayToImage", BindingFlags.Public | BindingFlags.Static);
                if (mToImage != null)
                {
                    using (Image img = (Image)mToImage.Invoke(null, new object[] { loadedBytes }))
                    {
                        AssertNotNull(img, "Decoded image must not be null");
                        AssertEqual(48, img.Width, "Image width must be 48px");
                    }
                }

                return new TestCaseResult
                {
                    Id = "T3.01",
                    Name = "Binary Image Insert, Search, and DAL Decode",
                    Tier = 3,
                    Passed = true,
                    Message = "Inserted synthetic image, queried record, and verified binary integrity."
                };
            }
            finally
            {
                Functions.RunSQL(string.Format("DELETE FROM MayViTinh WHERE MaMayVT = '{0}'", testId), false);
            }
        }

        public static TestCaseResult T3_02_LiveThemeDispatchMultipleChildForms()
        {
            MethodInfo mApply = typeof(UIThemeHelper).GetMethod("ApplyTheme", BindingFlags.Public | BindingFlags.Static);
            AssertNotNull(mApply, "UIThemeHelper.ApplyTheme(Control) must be implemented per Milestone 3 contract");

            using (frmMain mainForm = new frmMain())
            using (frmMayViTinh f1 = new frmMayViTinh())
            using (frmHoaDonBan f2 = new frmHoaDonBan())
            using (frmTimKiem f3 = new frmTimKiem())
            {
                SafeApplyTheme(mainForm);
                SafeApplyTheme(f1);
                SafeApplyTheme(f2);
                SafeApplyTheme(f3);

                AssertNotNull(mainForm, "frmMain instantiated");
                AssertNotNull(f1, "frmMayViTinh instantiated");
                AssertNotNull(f2, "frmHoaDonBan instantiated");
                AssertNotNull(f3, "frmTimKiem instantiated");

                return new TestCaseResult
                {
                    Id = "T3.02",
                    Name = "Live Theme Dispatch Across Multiple Forms",
                    Tier = 3,
                    Passed = true,
                    Message = "Multiple child forms themed concurrently without exceptions."
                };
            }
        }

        public static TestCaseResult T3_03_ThemePersistenceInDatabase()
        {
            Functions.Connect();
            string testKey = "ThemeMode";
            string origValue = Functions.GetFieldValues(string.Format("SELECT KeyValue FROM tblCauHinhHeThong WHERE KeyName = '{0}'", testKey));

            try
            {
                Functions.RunSQL(string.Format("IF EXISTS (SELECT 1 FROM tblCauHinhHeThong WHERE KeyName = '{0}') " +
                    "UPDATE tblCauHinhHeThong SET KeyValue = 'Dark' WHERE KeyName = '{0}' " +
                    "ELSE INSERT INTO tblCauHinhHeThong (KeyName, KeyValue) VALUES ('{0}', 'Dark')", testKey), false);

                string readDark = Functions.GetFieldValues(string.Format("SELECT KeyValue FROM tblCauHinhHeThong WHERE KeyName = '{0}'", testKey));
                AssertEqual("Dark", readDark, "Theme persistence must store and retrieve 'Dark'");

                Functions.RunSQL(string.Format("UPDATE tblCauHinhHeThong SET KeyValue = 'Light' WHERE KeyName = '{0}'", testKey), false);
                string readLight = Functions.GetFieldValues(string.Format("SELECT KeyValue FROM tblCauHinhHeThong WHERE KeyName = '{0}'", testKey));
                AssertEqual("Light", readLight, "Theme persistence must store and retrieve 'Light'");

                return new TestCaseResult
                {
                    Id = "T3.03",
                    Name = "Theme Mode Persistence Round-Trip in Database",
                    Tier = 3,
                    Passed = true,
                    Message = "Theme state persisted and retrieved from tblCauHinhHeThong."
                };
            }
            finally
            {
                if (!string.IsNullOrEmpty(origValue))
                {
                    Functions.RunSQL(string.Format("UPDATE tblCauHinhHeThong SET KeyValue = '{0}' WHERE KeyName = '{1}'", origValue, testKey), false);
                }
            }
        }

        public static TestCaseResult T3_04_WholesalePricingTriggerRecalculation()
        {
            Functions.Connect();
            string testMay = "E2E_AUTO_PRICE_PC";
            string testHdn = "E2E_AUTO_HDN";

            try
            {
                Functions.RunSQL(string.Format("DELETE FROM ChiTietHDN WHERE MaHDN = '{0}'", testHdn), false);
                Functions.RunSQL(string.Format("DELETE FROM HoaDonNhap WHERE MaHDN = '{0}'", testHdn), false);
                Functions.RunSQL(string.Format("DELETE FROM MayViTinh WHERE MaMayVT = '{0}'", testMay), false);

                string sqlMay = string.Format(@"INSERT INTO MayViTinh (MaMayVT, TenMayVT, MaLoai, MaChip, MaOCung, MaDungLuong,
                    MaTocDo, MaOCD, MaManHinh, MaCoMH, MaChuot, MaBanPhim, MaUSB, MaRAM, MaLoa, MaHangSX,
                    GiaNhap, GiaBan, ThoiGianBH, SoLuong, Anh, GhiChu)
                    VALUES ('{0}', N'Test Auto Price', 'LM01', 'CPU01', 'OC01', 'DL01', 'TD01', 'CD01',
                    'MH01', 'CO01', 'CH01', 'BP01', 'USB01', 'RAM01', 'LOA01', 'HSX01',
                    0, 0, 12, 0, NULL, N'Init zero')", testMay);
                Functions.RunSQL(sqlMay, false);

                string sqlHdn = string.Format("INSERT INTO HoaDonNhap (MaHDN, NgayNhap, MaNV, MaNCC, TongTien) VALUES ('{0}', GETDATE(), 'NV01', 'NCC01', 0)", testHdn);
                Functions.RunSQL(sqlHdn, false);

                decimal donGiaNhap = 18000000m;
                string sqlCthdn = string.Format("INSERT INTO ChiTietHDN (MaHDN, MaMayVT, SoLuong, DonGia, ThanhTien) VALUES ('{0}', '{1}', 5, {2}, 0)", testHdn, testMay, donGiaNhap);
                Functions.RunSQL(sqlCthdn, false);

                DataTable dt = Functions.GetDataToTable(string.Format("SELECT GiaNhap, GiaBan FROM MayViTinh WHERE MaMayVT = '{0}'", testMay));
                AssertTrue(dt.Rows.Count > 0, "Record must exist");

                decimal giaNhapActual = Convert.ToDecimal(dt.Rows[0]["GiaNhap"]);
                decimal giaBanActual = Convert.ToDecimal(dt.Rows[0]["GiaBan"]);
                decimal giaBanExpected = Math.Round(donGiaNhap * 1.10m, 0);

                AssertEqual(donGiaNhap, giaNhapActual, "GiaNhap must be updated to match import unit price");
                AssertEqual(giaBanExpected, giaBanActual, "GiaBan must be automatically calculated as 110% of GiaNhap");

                return new TestCaseResult
                {
                    Id = "T3.04",
                    Name = "Wholesale Pricing Trigger Recalculation (110%)",
                    Tier = 3,
                    Passed = true,
                    Message = string.Format("Verified 110% pricing trigger: GiaNhap = {0:N0}, GiaBan = {1:N0} VND.", giaNhapActual, giaBanActual)
                };
            }
            finally
            {
                Functions.RunSQL(string.Format("DELETE FROM ChiTietHDN WHERE MaHDN = '{0}'", testHdn), false);
                Functions.RunSQL(string.Format("DELETE FROM HoaDonNhap WHERE MaHDN = '{0}'", testHdn), false);
                Functions.RunSQL(string.Format("DELETE FROM MayViTinh WHERE MaMayVT = '{0}'", testMay), false);
            }
        }

        public static TestCaseResult T4_01_CompleteStoreWorkflowLifecycle()
        {
            Functions.Connect();
            string testModel = "E2E_STORE_PC_401";
            string testHdn = "E2E_STORE_HDN_401";
            string testHdb = "E2E_STORE_HDB_401";

            try
            {
                Functions.RunSQL(string.Format("DELETE FROM ChiTietHDB WHERE MaHDB = '{0}'", testHdb), false);
                Functions.RunSQL(string.Format("DELETE FROM HoaDonBan WHERE MaHDB = '{0}'", testHdb), false);
                Functions.RunSQL(string.Format("DELETE FROM ChiTietHDN WHERE MaHDN = '{0}'", testHdn), false);
                Functions.RunSQL(string.Format("DELETE FROM HoaDonNhap WHERE MaHDN = '{0}'", testHdn), false);
                Functions.RunSQL(string.Format("DELETE FROM MayViTinh WHERE MaMayVT = '{0}'", testModel), false);

                string authHash = Functions.GetFieldValues("SELECT MatKhauHash FROM tblTaiKhoan WHERE TenDangNhap = 'admin'");
                string authSalt = Functions.GetFieldValues("SELECT Salt FROM tblTaiKhoan WHERE TenDangNhap = 'admin'");
                if (!string.IsNullOrEmpty(authHash) && !string.IsNullOrEmpty(authSalt))
                {
                    bool authOk = SecurityHelper.VerifyPassword("Admin@123", authHash, authSalt);
                    AssertTrue(authOk, "Stage 1: Admin credentials must authenticate");
                }

                byte[] modelImage = CreateSyntheticPng(64, 64, Color.Purple);
                string insertModelSql = @"INSERT INTO MayViTinh (MaMayVT, TenMayVT, MaLoai, MaChip, MaOCung, MaDungLuong,
                    MaTocDo, MaOCD, MaManHinh, MaCoMH, MaChuot, MaBanPhim, MaUSB, MaRAM, MaLoa, MaHangSX,
                    GiaNhap, GiaBan, ThoiGianBH, SoLuong, Anh, GhiChu)
                    VALUES (@id, N'Store Lifecycle PC', 'LM01', 'CPU01', 'OC01', 'DL01', 'TD01', 'CD01',
                    'MH01', 'CO01', 'CH01', 'BP01', 'USB01', 'RAM01', 'LOA01', 'HSX01',
                    0, 0, 24, 0, @img, N'E2E Store Workflow Model')";

                using (SqlCommand cmd = new SqlCommand(insertModelSql, Functions.Conn))
                {
                    cmd.Parameters.AddWithValue("@id", testModel);
                    cmd.Parameters.Add("@img", SqlDbType.VarBinary, -1).Value = modelImage;
                    cmd.ExecuteNonQuery();
                }

                int initialStock = Convert.ToInt32(Functions.GetFieldValues(string.Format("SELECT SoLuong FROM MayViTinh WHERE MaMayVT = '{0}'", testModel)));
                AssertEqual(0, initialStock, "Stage 2: Initial inventory must be 0");

                int importUnits = 15;
                decimal unitCost = 22000000m;
                Functions.RunSQL(string.Format("INSERT INTO HoaDonNhap (MaHDN, NgayNhap, MaNV, MaNCC, TongTien) VALUES ('{0}', GETDATE(), 'NV01', 'NCC01', 0)", testHdn), false);
                Functions.RunSQL(string.Format("INSERT INTO ChiTietHDN (MaHDN, MaMayVT, SoLuong, DonGia, ThanhTien) VALUES ('{0}', '{1}', {2}, {3}, 0)",
                    testHdn, testModel, importUnits, unitCost), false);

                int stockAfterImport = Convert.ToInt32(Functions.GetFieldValues(string.Format("SELECT SoLuong FROM MayViTinh WHERE MaMayVT = '{0}'", testModel)));
                decimal retailPrice = Convert.ToDecimal(Functions.GetFieldValues(string.Format("SELECT GiaBan FROM MayViTinh WHERE MaMayVT = '{0}'", testModel)));

                AssertEqual(15, stockAfterImport, "Stage 3: Inventory must increment to 15 after import");
                AssertEqual(Math.Round(unitCost * 1.10m, 0), retailPrice, "Stage 3: Retail price must be 110% of wholesale unit cost");

                int soldUnits = 6;
                Functions.RunSQL(string.Format("INSERT INTO HoaDonBan (MaHDB, NgayBan, MaNV, MaKhach, TongTien) VALUES ('{0}', GETDATE(), 'NV01', 'KH01', 0)", testHdb), false);
                Functions.RunSQL(string.Format("INSERT INTO ChiTietHDB (MaHDB, MaMayVT, SoLuong, DonGia, ThanhTien) VALUES ('{0}', '{1}', {2}, {3}, 0)",
                    testHdb, testModel, soldUnits, retailPrice), false);

                int stockAfterSale = Convert.ToInt32(Functions.GetFieldValues(string.Format("SELECT SoLuong FROM MayViTinh WHERE MaMayVT = '{0}'", testModel)));
                decimal hdbTotal = Convert.ToDecimal(Functions.GetFieldValues(string.Format("SELECT TongTien FROM HoaDonBan WHERE MaHDB = '{0}'", testHdb)));

                AssertEqual(9, stockAfterSale, "Stage 4: Inventory must decrement from 15 to 9 after selling 6 units");
                AssertEqual(soldUnits * retailPrice, hdbTotal, "Stage 4: Invoice total must equal sold quantity * retail unit price");

                return new TestCaseResult
                {
                    Id = "T4.01",
                    Name = "Complete Store POS & Inventory Lifecycle",
                    Tier = 4,
                    Passed = true,
                    Message = string.Format("End-to-end POS lifecycle validated: 15 imported, 6 sold, 9 remaining, Total HDB: {0:N0} VND.", hdbTotal)
                };
            }
            finally
            {
                Functions.RunSQL(string.Format("DELETE FROM ChiTietHDB WHERE MaHDB = '{0}'", testHdb), false);
                Functions.RunSQL(string.Format("DELETE FROM HoaDonBan WHERE MaHDB = '{0}'", testHdb), false);
                Functions.RunSQL(string.Format("DELETE FROM ChiTietHDN WHERE MaHDN = '{0}'", testHdn), false);
                Functions.RunSQL(string.Format("DELETE FROM HoaDonNhap WHERE MaHDN = '{0}'", testHdn), false);
                Functions.RunSQL(string.Format("DELETE FROM MayViTinh WHERE MaMayVT = '{0}'", testModel), false);
            }
        }

        public static TestCaseResult T4_02_InventoryRestorationOnSaleItemRemoval()
        {
            Functions.Connect();
            string testModel = "E2E_RESTORE_PC";
            string testHdn = "E2E_RESTORE_HDN";
            string testHdb = "E2E_RESTORE_HDB";

            try
            {
                Functions.RunSQL(string.Format("DELETE FROM ChiTietHDB WHERE MaHDB = '{0}'", testHdb), false);
                Functions.RunSQL(string.Format("DELETE FROM HoaDonBan WHERE MaHDB = '{0}'", testHdb), false);
                Functions.RunSQL(string.Format("DELETE FROM ChiTietHDN WHERE MaHDN = '{0}'", testHdn), false);
                Functions.RunSQL(string.Format("DELETE FROM HoaDonNhap WHERE MaHDN = '{0}'", testHdn), false);
                Functions.RunSQL(string.Format("DELETE FROM MayViTinh WHERE MaMayVT = '{0}'", testModel), false);

                string sqlMay = string.Format(@"INSERT INTO MayViTinh (MaMayVT, TenMayVT, MaLoai, MaChip, MaOCung, MaDungLuong,
                    MaTocDo, MaOCD, MaManHinh, MaCoMH, MaChuot, MaBanPhim, MaUSB, MaRAM, MaLoa, MaHangSX,
                    GiaNhap, GiaBan, ThoiGianBH, SoLuong, Anh, GhiChu)
                    VALUES ('{0}', N'Restore Test PC', 'LM01', 'CPU01', 'OC01', 'DL01', 'TD01', 'CD01',
                    'MH01', 'CO01', 'CH01', 'BP01', 'USB01', 'RAM01', 'LOA01', 'HSX01',
                    10000000, 11000000, 12, 10, NULL, N'Init 10 units')", testModel);
                Functions.RunSQL(sqlMay, false);

                Functions.RunSQL(string.Format("INSERT INTO HoaDonBan (MaHDB, NgayBan, MaNV, MaKhach, TongTien) VALUES ('{0}', GETDATE(), 'NV01', 'KH01', 0)", testHdb), false);
                Functions.RunSQL(string.Format("INSERT INTO ChiTietHDB (MaHDB, MaMayVT, SoLuong, DonGia, ThanhTien) VALUES ('{0}', '{1}', 4, 11000000, 0)", testHdb, testModel), false);

                int stockAfterSale = Convert.ToInt32(Functions.GetFieldValues(string.Format("SELECT SoLuong FROM MayViTinh WHERE MaMayVT = '{0}'", testModel)));
                AssertEqual(6, stockAfterSale, "Stock must drop to 6 after selling 4 units");

                Functions.RunSQL(string.Format("DELETE FROM ChiTietHDB WHERE MaHDB = '{0}' AND MaMayVT = '{1}'", testHdb, testModel), false);
                int stockAfterDelete = Convert.ToInt32(Functions.GetFieldValues(string.Format("SELECT SoLuong FROM MayViTinh WHERE MaMayVT = '{0}'", testModel)));
                AssertEqual(10, stockAfterDelete, "Deleting item from invoice must trigger automatic return of 4 units back to inventory");

                return new TestCaseResult
                {
                    Id = "T4.02",
                    Name = "Inventory Restoration on Invoice Item Delete",
                    Tier = 4,
                    Passed = true,
                    Message = "Inventory successfully restored from 6 back to 10 upon removing sale item."
                };
            }
            finally
            {
                Functions.RunSQL(string.Format("DELETE FROM ChiTietHDB WHERE MaHDB = '{0}'", testHdb), false);
                Functions.RunSQL(string.Format("DELETE FROM HoaDonBan WHERE MaHDB = '{0}'", testHdb), false);
                Functions.RunSQL(string.Format("DELETE FROM MayViTinh WHERE MaMayVT = '{0}'", testModel), false);
            }
        }

        public static TestCaseResult T4_03_ZeroCommentComplianceAudit()
        {
            string rootDir = AppDomain.CurrentDomain.BaseDirectory;
            DirectoryInfo dir = new DirectoryInfo(rootDir);
            while (dir != null && !File.Exists(Path.Combine(dir.FullName, "QLBanMayViTinh.csproj")))
            {
                dir = dir.Parent;
            }

            AssertNotNull(dir, "Project root directory containing QLBanMayViTinh.csproj must be located");

            string[] csFiles = Directory.GetFiles(dir.FullName, "*.cs", SearchOption.AllDirectories);
            string[] sqlFiles = Directory.GetFiles(dir.FullName, "*.sql", SearchOption.AllDirectories);

            List<string> violationFiles = new List<string>();

            foreach (string file in csFiles)
            {
                if (file.Contains("\\obj\\") || file.Contains("\\bin\\") || file.Contains("\\.git\\"))
                    continue;

                string[] lines = File.ReadAllLines(file);
                for (int i = 0; i < lines.Length; i++)
                {
                    string trimmed = lines[i].Trim();
                    if (trimmed.StartsWith("//") || trimmed.StartsWith("/*") || trimmed.StartsWith("///"))
                    {
                        violationFiles.Add(string.Format("{0}:{1}", Path.GetFileName(file), i + 1));
                        break;
                    }
                }
            }

            foreach (string file in sqlFiles)
            {
                if (file.Contains("\\obj\\") || file.Contains("\\bin\\") || file.Contains("\\.git\\"))
                    continue;

                string[] lines = File.ReadAllLines(file);
                for (int i = 0; i < lines.Length; i++)
                {
                    string trimmed = lines[i].Trim();
                    if (trimmed.StartsWith("--") || trimmed.StartsWith("/*"))
                    {
                        violationFiles.Add(string.Format("{0}:{1}", Path.GetFileName(file), i + 1));
                        break;
                    }
                }
            }

            AssertTrue(violationFiles.Count == 0, string.Format("Constraint R5 violation: found comments in {0} files: {1}",
                violationFiles.Count, string.Join(", ", violationFiles)));

            return new TestCaseResult
            {
                Id = "T4.03",
                Name = "Zero Comments Compliance Audit (R5)",
                Tier = 4,
                Passed = true,
                Message = string.Format("Scanned {0} C# and {1} SQL files. 100% clean with 0 comments.", csFiles.Length, sqlFiles.Length)
            };
        }
    }
}
