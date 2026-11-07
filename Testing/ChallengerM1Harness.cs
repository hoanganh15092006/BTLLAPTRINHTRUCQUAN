using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Windows.Forms;
using QLBanMayViTinh.Classes;
using QLBanMayViTinh.Forms;

namespace QLBanMayViTinh.Testing
{
    public class ChallengerM1Harness
    {
        [DllImport("user32.dll")]
        private static extern uint GetGuiResources(IntPtr hProcess, uint uiFlags);

        private static int passedTests = 0;
        private static int failedTests = 0;
        private static List<string> findings = new List<string>();

        [STAThread]
        public static int Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("======================================================================");
            Console.WriteLine("     CHALLENGER EMPIRICAL ADVERSARIAL HARNESS - MILESTONE 1           ");
            Console.WriteLine("======================================================================");
            Console.WriteLine("Timestamp: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            Console.WriteLine();

            Functions.Connect();

            RunTest("CH1.1_NullAndEmptyByteArrayHandling", Test1_1_NullAndEmptyByteArrayHandling);
            RunTest("CH1.2_SingleByteCorruptPayloads", Test1_2_SingleByteCorruptPayloads);
            RunTest("CH1.3_TruncatedImageHeaders", Test1_3_TruncatedImageHeaders);
            RunTest("CH1.4_RandomNoisePayloadsMultiScale", Test1_4_RandomNoisePayloadsMultiScale);
            RunTest("CH1.5_Large5MBBitmapConversionRoundTrip", Test1_5_Large5MBBitmapConversionRoundTrip);
            RunTest("CH1.6_NullImageToByteArrayHandling", Test1_6_NullImageToByteArrayHandling);
            RunTest("CH1.7_RapidStressLoop_1000Conversions_GdiLeakGuard", Test1_7_RapidStressLoop_1000Conversions_GdiLeakGuard);

            RunTest("CH2.1_RunSql_SqlInjectionImmunity", Test2_1_RunSql_SqlInjectionImmunity);
            RunTest("CH2.2_RunSql_DBNullParametersHandling", Test2_2_RunSql_DBNullParametersHandling);
            RunTest("CH2.3_RunSql_LargeBinaryBlobRoundTrip", Test2_3_RunSql_LargeBinaryBlobRoundTrip);

            RunTest("CH3.1_SQLServer_All32Models_ForensicsAndMagicBytes", Test3_1_SQLServer_All32Models_ForensicsAndMagicBytes);
            RunTest("CH3.2_SQLServer_CategoryDiversity_4Segments", Test3_2_SQLServer_CategoryDiversity_4Segments);

            RunTest("CH4.1_FrmMayViTinh_UIBindingAll32Models", Test4_1_FrmMayViTinh_UIBindingAll32Models);

            Console.WriteLine();
            Console.WriteLine("======================================================================");
            Console.WriteLine(string.Format("SUMMARY: {0} PASSED, {1} FAILED", passedTests, failedTests));
            Console.WriteLine("======================================================================");

            if (findings.Count > 0)
            {
                Console.WriteLine("FINDINGS & OBSERVATIONS:");
                foreach (string f in findings)
                {
                    Console.WriteLine(" - " + f);
                }
            }

            return failedTests == 0 ? 0 : 1;
        }

        private static void RunTest(string testName, Action testAction)
        {
            Stopwatch sw = Stopwatch.StartNew();
            try
            {
                testAction();
                sw.Stop();
                passedTests++;
                Console.WriteLine(string.Format("[PASS] {0,-45} | {1,5} ms", testName, sw.ElapsedMilliseconds));
            }
            catch (Exception ex)
            {
                sw.Stop();
                failedTests++;
                Console.WriteLine(string.Format("[FAIL] {0,-45} | {1,5} ms | Error: {2}", testName, sw.ElapsedMilliseconds, ex.Message));
            }
        }

        private static void Test1_1_NullAndEmptyByteArrayHandling()
        {
            Image nullImg = Functions.ByteArrayToImage(null);
            if (nullImg != null)
                throw new Exception("ByteArrayToImage(null) must return null");

            Image emptyImg = Functions.ByteArrayToImage(new byte[0]);
            if (emptyImg != null)
                throw new Exception("ByteArrayToImage(new byte[0]) must return null");
        }

        private static void Test1_2_SingleByteCorruptPayloads()
        {
            byte[] b0 = new byte[] { 0x00 };
            Image img0 = Functions.ByteArrayToImage(b0);
            if (img0 != null)
                throw new Exception("ByteArrayToImage(0x00) must return null");

            byte[] bFF = new byte[] { 0xFF };
            Image imgFF = Functions.ByteArrayToImage(bFF);
            if (imgFF != null)
                throw new Exception("ByteArrayToImage(0xFF) must return null");

            byte[] b89 = new byte[] { 0x89 };
            Image img89 = Functions.ByteArrayToImage(b89);
            if (img89 != null)
                throw new Exception("ByteArrayToImage(0x89) must return null");
        }

        private static void Test1_3_TruncatedImageHeaders()
        {
            byte[] truncPng = new byte[] { 0x89, 0x50, 0x4E, 0x47 };
            Image imgPng = Functions.ByteArrayToImage(truncPng);
            if (imgPng != null)
                throw new Exception("ByteArrayToImage(truncated PNG) must return null");

            byte[] truncJpg = new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 };
            Image imgJpg = Functions.ByteArrayToImage(truncJpg);
            if (imgJpg != null)
                throw new Exception("ByteArrayToImage(truncated JPEG) must return null");

            byte[] truncBmp = new byte[] { 0x42, 0x4D, 0x10, 0x00 };
            Image imgBmp = Functions.ByteArrayToImage(truncBmp);
            if (imgBmp != null)
                throw new Exception("ByteArrayToImage(truncated BMP) must return null");
        }

        private static void Test1_4_RandomNoisePayloadsMultiScale()
        {
            Random rnd = new Random(1337);
            int[] payloadSizes = new int[] { 16, 64, 256, 1024, 4096, 65536, 1048576 };

            foreach (int size in payloadSizes)
            {
                byte[] noise = new byte[size];
                rnd.NextBytes(noise);

                Image img = Functions.ByteArrayToImage(noise);
                if (img != null)
                {
                    img.Dispose();
                    throw new Exception(string.Format("Random noise of size {0} bytes unexpectedly yielded an Image", size));
                }
            }
        }

        private static void Test1_5_Large5MBBitmapConversionRoundTrip()
        {
            int width = 1600;
            int height = 1200;
            using (Bitmap bigBmp = new Bitmap(width, height, PixelFormat.Format32bppArgb))
            {
                BitmapData bmpData = bigBmp.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
                int byteCount = Math.Abs(bmpData.Stride) * height;
                byte[] rawPixels = new byte[byteCount];
                Random rnd = new Random(777);
                rnd.NextBytes(rawPixels);
                Marshal.Copy(rawPixels, 0, bmpData.Scan0, byteCount);
                bigBmp.UnlockBits(bmpData);

                byte[] pngBytes = Functions.ImageToByteArray(bigBmp);
                if (pngBytes == null || pngBytes.Length < 3000000)
                    throw new Exception(string.Format("Expected compressed large PNG payload > 3MB, got {0} bytes", pngBytes != null ? pngBytes.Length : 0));

                using (Image decoded = Functions.ByteArrayToImage(pngBytes))
                {
                    if (decoded == null)
                        throw new Exception("ByteArrayToImage failed to decode large valid PNG");

                    if (decoded.Width != width || decoded.Height != height)
                        throw new Exception(string.Format("Dimension mismatch: expected {0}x{1}, got {2}x{3}", width, height, decoded.Width, decoded.Height));
                }

                findings.Add(string.Format("Large Bitmap Round-Trip: {0}x{1} uncompressed 7.6MB bitmap converted to {2:N0} bytes PNG and cleanly reconstituted via ByteArrayToImage.", width, height, pngBytes.Length));
            }
        }

        private static void Test1_6_NullImageToByteArrayHandling()
        {
            byte[] res = Functions.ImageToByteArray(null);
            if (res != null)
                throw new Exception("ImageToByteArray(null) must return null");
        }

        private static void Test1_7_RapidStressLoop_1000Conversions_GdiLeakGuard()
        {
            IntPtr hProcess = Process.GetCurrentProcess().Handle;
            uint initialGdi = GetGuiResources(hProcess, 0);

            byte[] validPng;
            using (Bitmap sample = new Bitmap(128, 128))
            {
                using (Graphics g = Graphics.FromImage(sample))
                {
                    g.Clear(Color.SeaGreen);
                }
                validPng = Functions.ImageToByteArray(sample);
            }

            Random rnd = new Random(999);
            for (int i = 0; i < 1000; i++)
            {
                int pattern = i % 5;
                if (pattern == 0)
                {
                    Functions.ByteArrayToImage(null);
                }
                else if (pattern == 1)
                {
                    Functions.ByteArrayToImage(new byte[0]);
                }
                else if (pattern == 2)
                {
                    Functions.ByteArrayToImage(new byte[] { 0x01 });
                }
                else if (pattern == 3)
                {
                    byte[] noise = new byte[64];
                    rnd.NextBytes(noise);
                    Functions.ByteArrayToImage(noise);
                }
                else
                {
                    using (Image img = Functions.ByteArrayToImage(validPng))
                    {
                        if (img == null || img.Width != 128)
                            throw new Exception("Valid image conversion failed during stress loop at step " + i);
                    }
                }
            }

            GC.Collect();
            GC.WaitForPendingFinalizers();

            uint finalGdi = GetGuiResources(hProcess, 0);
            long deltaGdi = (long)finalGdi - (long)initialGdi;

            if (Math.Abs(deltaGdi) > 10)
                throw new Exception(string.Format("Excessive GDI handle leakage: initial={0}, final={1}, delta={2}", initialGdi, finalGdi, deltaGdi));

            findings.Add(string.Format("GDI Handle Leak Guard: 1,000 rapid conversions completed with delta {0} handles (initial: {1}, final: {2}).", deltaGdi, initialGdi, finalGdi));
        }

        private static void Test2_1_RunSql_SqlInjectionImmunity()
        {
            string[] payloads = new string[]
            {
                "' OR '1'='1",
                "'; DROP TABLE tblNhatKyHoatDong; --",
                "admin' --",
                "1' UNION SELECT @@version, NULL, NULL --",
                "'''",
                "test" + ""
            };

            foreach (string p in payloads)
            {
                string sql = "SELECT COUNT(*) FROM tblTaiKhoan WHERE TenDangNhap = @u";
                SqlParameter[] pars = new SqlParameter[]
                {
                    new SqlParameter("@u", p)
                };

                DataTable dt = Functions.GetDataToTable(sql, pars);
                if (dt == null || dt.Rows.Count == 0)
                    throw new Exception("GetDataToTable query failed on SQL injection probe: " + p);

                int count = Convert.ToInt32(dt.Rows[0][0]);
                if (count != 0)
                    throw new Exception("SQL injection vulnerability: matched user unexpectedly for payload: " + p);
            }

            string checkTable = "SELECT COUNT(*) FROM sys.tables WHERE name = 'tblNhatKyHoatDong'";
            DataTable dtTbl = Functions.GetDataToTable(checkTable);
            int tblExists = Convert.ToInt32(dtTbl.Rows[0][0]);
            if (tblExists != 1)
                throw new Exception("Critical flaw: table tblNhatKyHoatDong was dropped by injection payload!");
        }

        private static void Test2_2_RunSql_DBNullParametersHandling()
        {
            string testId = "MVT_CH_DBNULL";
            try
            {
                Functions.RunSQL(string.Format("DELETE FROM MayViTinh WHERE MaMayVT = '{0}'", testId), false);

                string sqlInsert = @"INSERT INTO MayViTinh
                    (MaMayVT, TenMayVT, MaLoai, MaChip, MaOCung, MaDungLuong, MaTocDo, MaOCD, MaManHinh, MaCoMH,
                     MaChuot, MaBanPhim, MaUSB, MaRAM, MaLoa, MaHangSX, GiaNhap, GiaBan, ThoiGianBH, SoLuong, Anh, GhiChu)
                    VALUES (@id, N'Challenger Null Test', 'LM01', 'CPU01', 'OC01', 'DL01', 'TD01', 'CD01', 'MH01', 'CO01',
                            'CH01', 'BP01', 'USB01', 'RAM01', 'LOA01', 'HSX01', 12000000, 13200000, 12, 5, @Anh, @GhiChu)";

                SqlParameter[] pars = new SqlParameter[]
                {
                    new SqlParameter("@id", testId),
                    new SqlParameter("@Anh", SqlDbType.VarBinary, -1) { Value = DBNull.Value },
                    new SqlParameter("@GhiChu", SqlDbType.NVarChar, 500) { Value = DBNull.Value }
                };

                Functions.RunSQL(sqlInsert, pars, false);

                DataTable dt = Functions.GetDataToTable("SELECT Anh, GhiChu FROM MayViTinh WHERE MaMayVT = @id",
                    new SqlParameter[] { new SqlParameter("@id", testId) });

                if (dt.Rows.Count != 1)
                    throw new Exception("Record with DBNull was not inserted");

                if (dt.Rows[0]["Anh"] != DBNull.Value)
                    throw new Exception("Expected DBNull in database for Anh column");

                if (dt.Rows[0]["GhiChu"] != DBNull.Value)
                    throw new Exception("Expected DBNull in database for GhiChu column");
            }
            finally
            {
                Functions.RunSQL(string.Format("DELETE FROM MayViTinh WHERE MaMayVT = '{0}'", testId), false);
            }
        }

        private static void Test2_3_RunSql_LargeBinaryBlobRoundTrip()
        {
            string testId = "MVT_CH_BLOB";
            int blobSize = 2 * 1024 * 1024;
            byte[] syntheticBlob = new byte[blobSize];
            for (int i = 0; i < blobSize; i++)
            {
                syntheticBlob[i] = (byte)((i * 31) % 256);
            }

            byte[] expectedHash;
            using (SHA256 sha = SHA256.Create())
            {
                expectedHash = sha.ComputeHash(syntheticBlob);
            }

            try
            {
                Functions.RunSQL(string.Format("DELETE FROM MayViTinh WHERE MaMayVT = '{0}'", testId), false);

                string sqlInsert = @"INSERT INTO MayViTinh
                    (MaMayVT, TenMayVT, MaLoai, MaChip, MaOCung, MaDungLuong, MaTocDo, MaOCD, MaManHinh, MaCoMH,
                     MaChuot, MaBanPhim, MaUSB, MaRAM, MaLoa, MaHangSX, GiaNhap, GiaBan, ThoiGianBH, SoLuong, Anh, GhiChu)
                    VALUES (@id, N'Challenger Blob Test', 'LM01', 'CPU01', 'OC01', 'DL01', 'TD01', 'CD01', 'MH01', 'CO01',
                            'CH01', 'BP01', 'USB01', 'RAM01', 'LOA01', 'HSX01', 25000000, 27500000, 36, 3, @Anh, N'2MB BLOB test')";

                SqlParameter[] pars = new SqlParameter[]
                {
                    new SqlParameter("@id", testId),
                    new SqlParameter("@Anh", SqlDbType.VarBinary, -1) { Value = syntheticBlob }
                };

                Functions.RunSQL(sqlInsert, pars, false);

                DataTable dt = Functions.GetDataToTable("SELECT Anh, DATALENGTH(Anh) AS ByteLen FROM MayViTinh WHERE MaMayVT = @id",
                    new SqlParameter[] { new SqlParameter("@id", testId) });

                if (dt.Rows.Count != 1)
                    throw new Exception("Blob record was not inserted");

                int returnedLen = Convert.ToInt32(dt.Rows[0]["ByteLen"]);
                if (returnedLen != blobSize)
                    throw new Exception(string.Format("Blob length mismatch: expected {0}, got {1}", blobSize, returnedLen));

                byte[] returnedBytes = (byte[])dt.Rows[0]["Anh"];
                byte[] returnedHash;
                using (SHA256 sha = SHA256.Create())
                {
                    returnedHash = sha.ComputeHash(returnedBytes);
                }

                if (BitConverter.ToString(expectedHash) != BitConverter.ToString(returnedHash))
                    throw new Exception("SHA-256 integrity hash mismatch on returned binary blob");
            }
            finally
            {
                Functions.RunSQL(string.Format("DELETE FROM MayViTinh WHERE MaMayVT = '{0}'", testId), false);
            }
        }

        private static void Test3_1_SQLServer_All32Models_ForensicsAndMagicBytes()
        {
            string sql = @"SELECT MaMayVT, TenMayVT, MaLoai, Anh, DATALENGTH(Anh) AS ByteLength
                           FROM MayViTinh
                           ORDER BY MaMayVT";

            DataTable dt = Functions.GetDataToTable(sql);
            if (dt.Rows.Count != 32)
                throw new Exception("Expected exactly 32 models in MayViTinh, found " + dt.Rows.Count);

            byte[] pngMagic = new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A };

            int verifiedModels = 0;
            int minBytes = int.MaxValue;
            int maxBytes = 0;

            foreach (DataRow row in dt.Rows)
            {
                string id = row["MaMayVT"].ToString();
                string name = row["TenMayVT"].ToString();
                object rawObj = row["Anh"];

                if (rawObj == null || rawObj == DBNull.Value)
                    throw new Exception(string.Format("Model {0} ({1}) has NULL Anh column", id, name));

                byte[] bytes = rawObj as byte[];
                if (bytes == null || bytes.Length == 0)
                    throw new Exception(string.Format("Model {0} ({1}) Anh is not a valid byte[] or is empty", id, name));

                if (bytes.Length < 10000)
                    throw new Exception(string.Format("Model {0} ({1}) image is suspiciously small ({2} bytes)", id, name, bytes.Length));

                if (bytes.Length < minBytes) minBytes = bytes.Length;
                if (bytes.Length > maxBytes) maxBytes = bytes.Length;

                for (int i = 0; i < pngMagic.Length; i++)
                {
                    if (bytes[i] != pngMagic[i])
                    {
                        throw new Exception(string.Format("Model {0} ({1}) magic byte {2} is 0x{3:X2}, expected 0x{4:X2}",
                            id, name, i, bytes[i], pngMagic[i]));
                    }
                }

                using (Image img = Functions.ByteArrayToImage(bytes))
                {
                    if (img == null)
                        throw new Exception(string.Format("ByteArrayToImage failed for model {0} ({1})", id, name));

                    if (img.Width <= 0 || img.Height <= 0)
                        throw new Exception(string.Format("Model {0} ({1}) invalid dimensions: {2}x{3}", id, name, img.Width, img.Height));

                    byte[] reEncoded = Functions.ImageToByteArray(img);
                    if (reEncoded == null || reEncoded.Length == 0)
                        throw new Exception(string.Format("ImageToByteArray round-trip failed for model {0} ({1})", id, name));
                }

                verifiedModels++;
            }

            if (verifiedModels != 32)
                throw new Exception("Verified models count mismatch: " + verifiedModels);

            findings.Add(string.Format("All 32 computer models verified: 100% valid PNG headers, width > 0, height > 0 (Byte range: {0:N0} to {1:N0} bytes).", minBytes, maxBytes));
        }

        private static void Test3_2_SQLServer_CategoryDiversity_4Segments()
        {
            string sql = "SELECT DISTINCT MaLoai FROM MayViTinh";
            DataTable dt = Functions.GetDataToTable(sql);

            List<string> categories = new List<string>();
            foreach (DataRow r in dt.Rows)
            {
                categories.Add(r["MaLoai"].ToString().Trim());
            }

            string[] requiredCategories = new string[] { "LM01", "LM02", "LM03", "LM04" };
            foreach (string cat in requiredCategories)
            {
                if (!categories.Contains(cat))
                    throw new Exception("Missing required category segment: " + cat);
            }

            findings.Add("Segment Diversity Verified: All 4 segments (LM01-Laptop, LM02-Gaming PC, LM03-Office/AIO, LM04-Workstation) are populated in database.");
        }

        private static void Test4_1_FrmMayViTinh_UIBindingAll32Models()
        {
            using (frmMayViTinh form = new frmMayViTinh())
            {
                MethodInfo loadMethod = typeof(frmMayViTinh).GetMethod("frmMayViTinh_Load", BindingFlags.NonPublic | BindingFlags.Instance);
                loadMethod.Invoke(form, new object[] { form, EventArgs.Empty });

                FieldInfo dgvField = typeof(frmMayViTinh).GetField("dgvMayViTinh", BindingFlags.NonPublic | BindingFlags.Instance);
                DataGridView dgv = (DataGridView)dgvField.GetValue(form);

                FieldInfo picField = typeof(frmMayViTinh).GetField("picAnh", BindingFlags.NonPublic | BindingFlags.Instance);
                PictureBox pic = (PictureBox)picField.GetValue(form);

                FieldInfo txtAnhField = typeof(frmMayViTinh).GetField("txtAnh", BindingFlags.NonPublic | BindingFlags.Instance);
                TextBox txtAnh = (TextBox)txtAnhField.GetValue(form);

                FieldInfo imgBytesField = typeof(frmMayViTinh).GetField("currentImageBytes", BindingFlags.NonPublic | BindingFlags.Instance);

                MethodInfo cellClickMethod = typeof(frmMayViTinh).GetMethod("dgvMayViTinh_CellClick", BindingFlags.NonPublic | BindingFlags.Instance);

                int dataRowCount = dgv.AllowUserToAddRows ? dgv.Rows.Count - 1 : dgv.Rows.Count;
                if (dataRowCount != 32)
                    throw new Exception("DataGridView data row count expected 32, got " + dataRowCount);

                if (dgv.Columns["Anh"].Visible)
                    throw new Exception("Binary column 'Anh' should be hidden in DataGridView");

                for (int r = 0; r < dataRowCount; r++)
                {
                    string id = dgv.Rows[r].Cells["MaMayVT"].Value.ToString();
                    dgv.CurrentCell = dgv.Rows[r].Cells["MaMayVT"];
                    cellClickMethod.Invoke(form, new object[] { dgv, new DataGridViewCellEventArgs(0, r) });

                    byte[] loadedBytes = (byte[])imgBytesField.GetValue(form);
                    Image loadedImage = pic.Image;

                    if (loadedBytes == null || loadedBytes.Length < 10000)
                        throw new Exception(string.Format("Row {0} ({1}): currentImageBytes not populated properly", r, id));

                    if (loadedImage == null || loadedImage.Width <= 0 || loadedImage.Height <= 0)
                        throw new Exception(string.Format("Row {0} ({1}): picAnh.Image was not instantiated", r, id));

                    string expectedSnippet = string.Format("{0:N0} bytes", loadedBytes.Length);
                    if (!txtAnh.Text.Contains(expectedSnippet))
                        throw new Exception(string.Format("Row {0} ({1}): txtAnh text '{2}' does not contain '{3}'", r, id, txtAnh.Text, expectedSnippet));
                }

                findings.Add(string.Format("UI Binding Verified: Clicked all 32 rows in DataGridView. Each row correctly loaded byte[], set picAnh.Image (Width/Height > 0), and formatted txtAnh.", dataRowCount));
            }
        }
    }
}
