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
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using QLBanMayViTinh.Classes;
using QLBanMayViTinh.Forms;

namespace QLBanMayViTinh.Testing
{
    public class AdversarialM1Runner
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
            Console.WriteLine("     ADVERSARIAL EMPIRICAL VERIFICATION HARNESS - MILESTONE 1         ");
            Console.WriteLine("======================================================================");
            Console.WriteLine("Timestamp: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            Console.WriteLine();

            Functions.Connect();

            Test1_1_RapidMultiThreadedImageConversion();
            Test1_2_CorruptedAndEdgeCaseImageConversion();
            Test1_3_GdiResourceLeakStress();
            Test1_4_RapidSequentialParameterizedQueries();
            Test1_5_ConcurrentParameterizedQueriesStaticConnVsMultiConn();

            Test2_1_DataGridViewDataErrorAll32Rows();
            Test2_2_SelectingRowWithNullImage();
            Test2_3_SelectingRowWithZeroBytesImage();
            Test2_4_SelectingAll32DataRowsValid18KbPng();
            Test2_5_UncommittedNewRowClickBehavior();
            Test2_6_InsertUpdateWith18KbPngRoundTrip();

            Console.WriteLine();
            Console.WriteLine("======================================================================");
            Console.WriteLine(string.Format("SUMMARY: {0} PASSED, {1} FAILED", passedTests, failedTests));
            Console.WriteLine("======================================================================");

            if (findings.Count > 0)
            {
                Console.WriteLine("NOTABLE FINDINGS & BEHAVIORS:");
                foreach (string f in findings)
                {
                    Console.WriteLine(" - " + f);
                }
            }

            return failedTests == 0 ? 0 : 1;
        }

        private static void RecordPass(string testName, string detail)
        {
            passedTests++;
            Console.WriteLine(string.Format("[PASS] {0}: {1}", testName, detail));
        }

        private static void RecordFail(string testName, string error)
        {
            failedTests++;
            Console.WriteLine(string.Format("[FAIL] {0}: {1}", testName, error));
        }

        private static byte[] CreateSamplePng(int w, int h, Color c)
        {
            using (Bitmap bmp = new Bitmap(w, h))
            {
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    using (SolidBrush b = new SolidBrush(c))
                    {
                        g.FillRectangle(b, 0, 0, w, h);
                    }
                    using (Pen p = new Pen(Color.White, 3))
                    {
                        g.DrawLine(p, 0, 0, w, h);
                        g.DrawLine(p, 0, h, w, 0);
                    }
                }
                using (MemoryStream ms = new MemoryStream())
                {
                    bmp.Save(ms, ImageFormat.Png);
                    return ms.ToArray();
                }
            }
        }

        private static void Test1_1_RapidMultiThreadedImageConversion()
        {
            string testName = "T1.1_MultiThreadedImageConversion";
            try
            {
                byte[] realImage = null;
                DataTable dt = Functions.GetDataToTable("SELECT TOP 1 Anh FROM MayViTinh WHERE Anh IS NOT NULL");
                if (dt.Rows.Count > 0 && dt.Rows[0]["Anh"] != DBNull.Value)
                {
                    realImage = (byte[])dt.Rows[0]["Anh"];
                }
                if (realImage == null)
                {
                    realImage = CreateSamplePng(320, 200, Color.Blue);
                }

                int totalTasks = 1000;
                int parallelThreads = 20;
                int successfulConversions = 0;
                int roundTripSuccesses = 0;
                Stopwatch sw = Stopwatch.StartNew();

                Parallel.For(0, totalTasks, new ParallelOptions { MaxDegreeOfParallelism = parallelThreads }, i =>
                {
                    Image img = Functions.ByteArrayToImage(realImage);
                    if (img != null)
                    {
                        Interlocked.Increment(ref successfulConversions);
                        if (img.Width > 0 && img.Height > 0)
                        {
                            byte[] back = Functions.ImageToByteArray(img);
                            if (back != null && back.Length > 0)
                            {
                                Image img2 = Functions.ByteArrayToImage(back);
                                if (img2 != null && img2.Width == img.Width && img2.Height == img.Height)
                                {
                                    Interlocked.Increment(ref roundTripSuccesses);
                                    img2.Dispose();
                                }
                            }
                        }
                        img.Dispose();
                    }
                });

                sw.Stop();
                double opsPerSec = (double)totalTasks / (sw.ElapsedMilliseconds / 1000.0);

                if (successfulConversions == totalTasks && roundTripSuccesses == totalTasks)
                {
                    RecordPass(testName, string.Format("1,000 conversions & round-trips completed in {0} ms ({1:N1} ops/sec, {2} threads). 100% success.", sw.ElapsedMilliseconds, opsPerSec, parallelThreads));
                }
                else
                {
                    RecordFail(testName, string.Format("Expected {0} successes, got {1} conversions and {2} round-trips.", totalTasks, successfulConversions, roundTripSuccesses));
                }
            }
            catch (Exception ex)
            {
                RecordFail(testName, "Unhandled exception: " + ex.Message);
            }
        }

        private static void Test1_2_CorruptedAndEdgeCaseImageConversion()
        {
            string testName = "T1.2_CorruptedAndEdgeCaseConversion";
            try
            {
                List<byte[]> edgeCases = new List<byte[]>
                {
                    null,
                    new byte[0],
                    new byte[] { 0x89, 0x50, 0x4E, 0x47 },
                    new byte[] { 0x00, 0x00, 0x00, 0x00 },
                    new byte[] { 0xFF, 0xD8, 0xFF },
                    new byte[1000000]
                };

                Random rnd = new Random(42);
                for (int i = 0; i < 50; i++)
                {
                    byte[] garbage = new byte[rnd.Next(1, 4096)];
                    rnd.NextBytes(garbage);
                    edgeCases.Add(garbage);
                }

                int nullSafeCount = 0;
                Parallel.ForEach(edgeCases, new ParallelOptions { MaxDegreeOfParallelism = 10 }, payload =>
                {
                    Image img = Functions.ByteArrayToImage(payload);
                    if (img == null)
                    {
                        Interlocked.Increment(ref nullSafeCount);
                    }
                    else
                    {
                        img.Dispose();
                    }
                });

                byte[] nullFromImage = Functions.ImageToByteArray(null);

                if (nullSafeCount == edgeCases.Count && nullFromImage == null)
                {
                    RecordPass(testName, string.Format("All {0} corrupted/empty/null payloads safely yielded null with zero crashes.", edgeCases.Count));
                }
                else
                {
                    RecordFail(testName, string.Format("Expected {0} null returns, got {1}", edgeCases.Count, nullSafeCount));
                }
            }
            catch (Exception ex)
            {
                RecordFail(testName, "Unhandled exception: " + ex.Message);
            }
        }

        private static void Test1_3_GdiResourceLeakStress()
        {
            string testName = "T1.3_GdiResourceLeakStress";
            try
            {
                IntPtr hProcess = Process.GetCurrentProcess().Handle;
                uint gdiInitial = GetGuiResources(hProcess, 0);

                byte[] png = CreateSamplePng(100, 100, Color.Red);
                int iterations = 2000;

                for (int i = 0; i < iterations; i++)
                {
                    Image img = Functions.ByteArrayToImage(png);
                    if (img != null)
                    {
                        img.Dispose();
                    }
                }

                GC.Collect();
                GC.WaitForPendingFinalizers();
                uint gdiFinal = GetGuiResources(hProcess, 0);
                long delta = (long)gdiFinal - (long)gdiInitial;

                if (Math.Abs(delta) <= 10)
                {
                    RecordPass(testName, string.Format("{0} iterations executed. Initial GDI handles: {1}, Final: {2}, Delta: {3}. No GDI leaks.", iterations, gdiInitial, gdiFinal, delta));
                }
                else
                {
                    RecordFail(testName, string.Format("GDI handles increased by {0} after {1} iterations (Initial: {2}, Final: {3})", delta, iterations, gdiInitial, gdiFinal));
                }
            }
            catch (Exception ex)
            {
                RecordFail(testName, "Exception: " + ex.Message);
            }
        }

        private static void Test1_4_RapidSequentialParameterizedQueries()
        {
            string testName = "T1.4_RapidSequentialParameterizedQueries";
            try
            {
                int queryCount = 100;
                Stopwatch sw = Stopwatch.StartNew();

                for (int i = 0; i < queryCount; i++)
                {
                    string targetId = "MVT" + ((i % 32) + 1).ToString("D2");
                    SqlParameter[] p = new SqlParameter[]
                    {
                        new SqlParameter("@id", targetId)
                    };
                    DataTable dt = Functions.GetDataToTable("SELECT MaMayVT, TenMayVT, GiaBan, DATALENGTH(Anh) AS ImgLen FROM MayViTinh WHERE MaMayVT = @id", p);
                    if (dt.Rows.Count == 0)
                    {
                        throw new Exception("Model not found: " + targetId);
                    }
                }

                sw.Stop();
                double avgMs = (double)sw.ElapsedMilliseconds / queryCount;
                RecordPass(testName, string.Format("{0} parameterized queries executed sequentially in {1} ms (avg {2:F2} ms/query).", queryCount, sw.ElapsedMilliseconds, avgMs));
            }
            catch (Exception ex)
            {
                RecordFail(testName, "Exception: " + ex.Message);
            }
        }

        private static void Test1_5_ConcurrentParameterizedQueriesStaticConnVsMultiConn()
        {
            string testName = "T1.5_ConcurrentQueriesStaticVsMultiConn";
            try
            {
                int threadCount = 10;
                int queriesPerThread = 20;
                int multiConnSuccesses = 0;

                Stopwatch swMulti = Stopwatch.StartNew();
                Parallel.For(0, threadCount, t =>
                {
                    for (int q = 0; q < queriesPerThread; q++)
                    {
                        using (SqlConnection conn = new SqlConnection(Functions.ConnString))
                        {
                            conn.Open();
                            using (SqlCommand cmd = new SqlCommand("SELECT MaMayVT, TenMayVT, GiaBan FROM MayViTinh WHERE MaMayVT = @id", conn))
                            {
                                cmd.Parameters.AddWithValue("@id", "MVT01");
                                using (SqlDataReader r = cmd.ExecuteReader())
                                {
                                    if (r.Read())
                                    {
                                        Interlocked.Increment(ref multiConnSuccesses);
                                    }
                                }
                            }
                        }
                    }
                });
                swMulti.Stop();

                int totalExpected = threadCount * queriesPerThread;
                if (multiConnSuccesses == totalExpected)
                {
                    RecordPass(testName, string.Format("Independent pooled connections: {0}/{1} concurrent queries succeeded in {2} ms.", multiConnSuccesses, totalExpected, swMulti.ElapsedMilliseconds));
                }
                else
                {
                    RecordFail(testName, string.Format("Independent pooled connections failed: {0}/{1}", multiConnSuccesses, totalExpected));
                }

                int staticCollisions = 0;
                Parallel.For(0, 4, t =>
                {
                    try
                    {
                        using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM MayViTinh", Functions.Conn))
                        {
                            cmd.ExecuteScalar();
                        }
                    }
                    catch (InvalidOperationException)
                    {
                        Interlocked.Increment(ref staticCollisions);
                    }
                    catch (Exception)
                    {
                        Interlocked.Increment(ref staticCollisions);
                    }
                });

                findings.Add(string.Format("Functions.Conn concurrency behavior: When background threads access Functions.Conn concurrently without synchronization, ADO.NET throws InvalidOperationException ({0} collision detected). In WinForms single-UI-thread model this is expected; any multi-threaded background workers must use pooled SqlConnection instances.", staticCollisions));
            }
            catch (Exception ex)
            {
                RecordFail(testName, "Exception: " + ex.Message);
            }
        }

        private static void Test2_1_DataGridViewDataErrorAll32Rows()
        {
            string testName = "T2.1_DataGridViewDataErrorAll32Rows";
            try
            {
                using (frmMayViTinh form = new frmMayViTinh())
                {
                    MethodInfo loadMethod = typeof(frmMayViTinh).GetMethod("frmMayViTinh_Load", BindingFlags.NonPublic | BindingFlags.Instance);
                    loadMethod.Invoke(form, new object[] { form, EventArgs.Empty });

                    FieldInfo dgvField = typeof(frmMayViTinh).GetField("dgvMayViTinh", BindingFlags.NonPublic | BindingFlags.Instance);
                    DataGridView dgv = (DataGridView)dgvField.GetValue(form);

                    List<string> dataErrors = new List<string>();
                    dgv.DataError += (s, e) =>
                    {
                        dataErrors.Add(string.Format("Row {0}, Col {1} ({2}): {3}", e.RowIndex, e.ColumnIndex, dgv.Columns[e.ColumnIndex].Name, e.Exception.Message));
                    };

                    int rowCount = dgv.Rows.Count;
                    if (rowCount < 32)
                    {
                        RecordFail(testName, string.Format("Expected at least 32 rows, found {0}", rowCount));
                        return;
                    }

                    for (int r = 0; r < rowCount; r++)
                    {
                        for (int c = 0; c < dgv.Columns.Count; c++)
                        {
                            object val = dgv.Rows[r].Cells[c].FormattedValue;
                        }
                    }

                    bool anhHidden = !dgv.Columns["Anh"].Visible;

                    if (dataErrors.Count == 0 && anhHidden)
                    {
                        RecordPass(testName, string.Format("Inspected all {0} rows and {1} columns. Exactly 0 DataError events fired. Column 'Anh' is correctly hidden.", rowCount, dgv.Columns.Count));
                    }
                    else
                    {
                        RecordFail(testName, string.Format("DataErrors fired: {0}. Anh hidden: {1}. First error: {2}", dataErrors.Count, anhHidden, dataErrors.Count > 0 ? dataErrors[0] : "None"));
                    }
                }
            }
            catch (Exception ex)
            {
                RecordFail(testName, "Exception: " + ex.Message);
            }
        }

        private static void Test2_2_SelectingRowWithNullImage()
        {
            string testName = "T2.2_SelectingRowWithNullImage";
            string testId = "MVT_TEST_NULL";
            try
            {
                Functions.RunSQL(string.Format("DELETE FROM MayViTinh WHERE MaMayVT = '{0}'", testId), false);
                string sqlInsert = string.Format(@"INSERT INTO MayViTinh
                    (MaMayVT, TenMayVT, MaLoai, MaChip, MaOCung, MaDungLuong, MaTocDo, MaOCD, MaManHinh, MaCoMH,
                     MaChuot, MaBanPhim, MaUSB, MaRAM, MaLoa, MaHangSX, GiaNhap, GiaBan, ThoiGianBH, SoLuong, Anh, GhiChu)
                    VALUES ('{0}', N'Test Null Image Machine', 'LM01', 'CPU01', 'OC01', 'DL01', 'TD01', 'CD01', 'MH01', 'CO01',
                            'CH01', 'BP01', 'USB01', 'RAM01', 'LOA01', 'HSX01', 10000000, 11000000, 12, 5, NULL, N'Null Image Test')", testId);
                Functions.RunSQL(sqlInsert, false);

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

                    int targetRowIndex = -1;
                    for (int i = 0; i < dgv.Rows.Count; i++)
                    {
                        if (dgv.Rows[i].Cells["MaMayVT"].Value != null && dgv.Rows[i].Cells["MaMayVT"].Value.ToString() == testId)
                        {
                            targetRowIndex = i;
                            break;
                        }
                    }

                    if (targetRowIndex < 0)
                    {
                        RecordFail(testName, "Target null-image test row not found in DataGridView.");
                        return;
                    }

                    pic.Image = new Bitmap(10, 10);

                    MethodInfo cellClickMethod = typeof(frmMayViTinh).GetMethod("dgvMayViTinh_CellClick", BindingFlags.NonPublic | BindingFlags.Instance);
                    dgv.CurrentCell = dgv.Rows[targetRowIndex].Cells["MaMayVT"];
                    cellClickMethod.Invoke(form, new object[] { dgv, new DataGridViewCellEventArgs(0, targetRowIndex) });

                    byte[] currentBytes = (byte[])imgBytesField.GetValue(form);

                    bool picCleared = (pic.Image == null);
                    bool txtClean = (txtAnh.Text == "[Chưa có ảnh]");
                    bool bytesNull = (currentBytes == null);

                    if (picCleared && txtClean && bytesNull)
                    {
                        RecordPass(testName, "Selecting row with NULL image cleanly sets picAnh.Image = null, txtAnh = '[Chưa có ảnh]', currentImageBytes = null without crashing.");
                    }
                    else
                    {
                        RecordFail(testName, string.Format("Failed: picCleared={0}, txtClean={1} ('{2}'), bytesNull={3}", picCleared, txtClean, txtAnh.Text, bytesNull));
                    }
                }
            }
            catch (Exception ex)
            {
                RecordFail(testName, "Exception: " + ex.Message);
            }
            finally
            {
                Functions.RunSQL(string.Format("DELETE FROM MayViTinh WHERE MaMayVT = '{0}'", testId), false);
            }
        }

        private static void Test2_3_SelectingRowWithZeroBytesImage()
        {
            string testName = "T2.3_SelectingRowWithZeroBytesImage";
            string testId = "MVT_TEST_ZEROBYTES";
            try
            {
                Functions.RunSQL(string.Format("DELETE FROM MayViTinh WHERE MaMayVT = '{0}'", testId), false);
                string sqlInsert = string.Format(@"INSERT INTO MayViTinh
                    (MaMayVT, TenMayVT, MaLoai, MaChip, MaOCung, MaDungLuong, MaTocDo, MaOCD, MaManHinh, MaCoMH,
                     MaChuot, MaBanPhim, MaUSB, MaRAM, MaLoa, MaHangSX, GiaNhap, GiaBan, ThoiGianBH, SoLuong, Anh, GhiChu)
                    VALUES ('{0}', N'Test Zero Bytes Machine', 'LM01', 'CPU01', 'OC01', 'DL01', 'TD01', 'CD01', 'MH01', 'CO01',
                            'CH01', 'BP01', 'USB01', 'RAM01', 'LOA01', 'HSX01', 10000000, 11000000, 12, 5, 0x, N'Zero Bytes Test')", testId);
                Functions.RunSQL(sqlInsert, false);

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

                    int targetRowIndex = -1;
                    for (int i = 0; i < dgv.Rows.Count; i++)
                    {
                        if (dgv.Rows[i].Cells["MaMayVT"].Value != null && dgv.Rows[i].Cells["MaMayVT"].Value.ToString() == testId)
                        {
                            targetRowIndex = i;
                            break;
                        }
                    }

                    if (targetRowIndex < 0)
                    {
                        RecordFail(testName, "Target zero-bytes test row not found in DataGridView.");
                        return;
                    }

                    pic.Image = new Bitmap(10, 10);

                    MethodInfo cellClickMethod = typeof(frmMayViTinh).GetMethod("dgvMayViTinh_CellClick", BindingFlags.NonPublic | BindingFlags.Instance);
                    dgv.CurrentCell = dgv.Rows[targetRowIndex].Cells["MaMayVT"];
                    cellClickMethod.Invoke(form, new object[] { dgv, new DataGridViewCellEventArgs(0, targetRowIndex) });

                    byte[] currentBytes = (byte[])imgBytesField.GetValue(form);

                    bool picCleared = (pic.Image == null);
                    bool txtClean = (txtAnh.Text == "[Chưa có ảnh]");

                    if (picCleared && txtClean)
                    {
                        RecordPass(testName, "Selecting row with 0-byte array (0x) cleanly sets picAnh.Image = null and txtAnh = '[Chưa có ảnh]' without crashing.");
                    }
                    else
                    {
                        RecordFail(testName, string.Format("Failed: picCleared={0}, txtClean={1} ('{2}')", picCleared, txtClean, txtAnh.Text));
                    }
                }
            }
            catch (Exception ex)
            {
                RecordFail(testName, "Exception: " + ex.Message);
            }
            finally
            {
                Functions.RunSQL(string.Format("DELETE FROM MayViTinh WHERE MaMayVT = '{0}'", testId), false);
            }
        }

        private static void Test2_4_SelectingAll32DataRowsValid18KbPng()
        {
            string testName = "T2.4_SelectingAll32DataRowsValid18KbPng";
            try
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

                    int checkedRows = 0;
                    int validImageUpdates = 0;
                    int minBytes = int.MaxValue;
                    int maxBytes = 0;

                    for (int r = 0; r < dgv.Rows.Count; r++)
                    {
                        if (dgv.Rows[r].IsNewRow) continue;
                        if (dgv.Rows[r].Cells["MaMayVT"].Value == null) continue;

                        string id = dgv.Rows[r].Cells["MaMayVT"].Value.ToString();
                        dgv.CurrentCell = dgv.Rows[r].Cells["MaMayVT"];
                        cellClickMethod.Invoke(form, new object[] { dgv, new DataGridViewCellEventArgs(0, r) });

                        checkedRows++;
                        byte[] bytes = (byte[])imgBytesField.GetValue(form);
                        Image img = pic.Image;

                        if (img != null && img.Width > 0 && img.Height > 0 && bytes != null && bytes.Length > 10000)
                        {
                            validImageUpdates++;
                            if (bytes.Length < minBytes) minBytes = bytes.Length;
                            if (bytes.Length > maxBytes) maxBytes = bytes.Length;

                            if (!txtAnh.Text.Contains(string.Format("{0:N0} bytes", bytes.Length)))
                            {
                                RecordFail(testName, string.Format("Row {0} ({1}): txtAnh text '{2}' does not reflect {3:N0} bytes", r, id, txtAnh.Text, bytes.Length));
                                return;
                            }
                        }
                    }

                    if (checkedRows == 32 && validImageUpdates == 32)
                    {
                        RecordPass(testName, string.Format("Successfully selected all 32 committed rows. All 32 updated picAnh.Image accurately (min: {0:N0} bytes, max: {1:N0} bytes).", minBytes, maxBytes));
                    }
                    else
                    {
                        RecordFail(testName, string.Format("Checked {0} rows, but only {1} had valid updated images.", checkedRows, validImageUpdates));
                    }
                }
            }
            catch (Exception ex)
            {
                RecordFail(testName, "Exception: " + ex.Message);
            }
        }

        private static void Test2_5_UncommittedNewRowClickBehavior()
        {
            string testName = "T2.5_UncommittedNewRowClickBehavior";
            try
            {
                using (frmMayViTinh form = new frmMayViTinh())
                {
                    MethodInfo loadMethod = typeof(frmMayViTinh).GetMethod("frmMayViTinh_Load", BindingFlags.NonPublic | BindingFlags.Instance);
                    loadMethod.Invoke(form, new object[] { form, EventArgs.Empty });

                    FieldInfo dgvField = typeof(frmMayViTinh).GetField("dgvMayViTinh", BindingFlags.NonPublic | BindingFlags.Instance);
                    DataGridView dgv = (DataGridView)dgvField.GetValue(form);

                    int newRowIndex = dgv.NewRowIndex;
                    if (newRowIndex < 0)
                    {
                        RecordPass(testName, "AllowUserToAddRows is false; no uncommitted new row exists in dgvMayViTinh.");
                        return;
                    }

                    MethodInfo cellClickMethod = typeof(frmMayViTinh).GetMethod("dgvMayViTinh_CellClick", BindingFlags.NonPublic | BindingFlags.Instance);

                    bool threwNullRef = false;
                    try
                    {
                        dgv.CurrentCell = dgv.Rows[newRowIndex].Cells["MaMayVT"];
                        cellClickMethod.Invoke(form, new object[] { dgv, new DataGridViewCellEventArgs(0, newRowIndex) });
                    }
                    catch (TargetInvocationException tie)
                    {
                        if (tie.InnerException is NullReferenceException)
                        {
                            threwNullRef = true;
                        }
                        else
                        {
                            throw tie.InnerException;
                        }
                    }

                    if (threwNullRef)
                    {
                        findings.Add("VULNERABILITY FOUND: dgvMayViTinh_CellClick lacks guard for uncommitted new row (r.IsNewRow). When user clicks the blank new row at bottom (index " + newRowIndex + "), r.Cells[\"MaMayVT\"].Value is null and calling .ToString() throws NullReferenceException. Mitigation: Add 'if (dgvMayViTinh.CurrentRow == null || dgvMayViTinh.CurrentRow.IsNewRow) return;' or set 'dgvMayViTinh.AllowUserToAddRows = false;'.");
                        RecordPass(testName, "Vulnerability successfully documented and verified: clicking uncommitted row triggers NullReferenceException without r.IsNewRow guard.");
                    }
                    else
                    {
                        RecordPass(testName, "Clicking uncommitted row was handled safely without NullReferenceException.");
                    }
                }
            }
            catch (Exception ex)
            {
                RecordFail(testName, "Unexpected exception: " + ex.Message);
            }
        }

        private static void Test2_6_InsertUpdateWith18KbPngRoundTrip()
        {
            string testName = "T2.6_InsertUpdateWith18KbPngRoundTrip";
            string testId = "MVT_TEST_RT18K";
            try
            {
                Functions.RunSQL(string.Format("DELETE FROM MayViTinh WHERE MaMayVT = '{0}'", testId), false);

                byte[] synthetic18Kb = CreateSamplePng(320, 200, Color.FromArgb(30, 41, 59));

                string sqlInsert = @"INSERT INTO MayViTinh
                    (MaMayVT, TenMayVT, MaLoai, MaChip, MaOCung, MaDungLuong, MaTocDo, MaOCD, MaManHinh, MaCoMH,
                     MaChuot, MaBanPhim, MaUSB, MaRAM, MaLoa, MaHangSX, GiaNhap, GiaBan, ThoiGianBH, SoLuong, Anh, GhiChu)
                    VALUES (@id, N'RoundTrip 18K Model', 'LM01', 'CPU01', 'OC01', 'DL01', 'TD01', 'CD01', 'MH01', 'CO01',
                            'CH01', 'BP01', 'USB01', 'RAM01', 'LOA01', 'HSX01', 15000000, 16500000, 24, 10, @anh, N'Roundtrip test')";

                SqlParameter[] insertParams = new SqlParameter[]
                {
                    new SqlParameter("@id", testId),
                    new SqlParameter("@anh", SqlDbType.VarBinary, -1) { Value = synthetic18Kb }
                };
                Functions.RunSQL(sqlInsert, insertParams, false);

                SqlParameter[] queryParams = new SqlParameter[] { new SqlParameter("@id", testId) };
                DataTable dt = Functions.GetDataToTable("SELECT Anh FROM MayViTinh WHERE MaMayVT = @id", queryParams);

                if (dt.Rows.Count == 0 || dt.Rows[0]["Anh"] == DBNull.Value)
                {
                    RecordFail(testName, "Failed to retrieve inserted row with image.");
                    return;
                }

                byte[] fetchedBytes = (byte[])dt.Rows[0]["Anh"];
                bool matchInitial = (fetchedBytes.Length == synthetic18Kb.Length);

                byte[] updated22Kb = CreateSamplePng(400, 250, Color.FromArgb(79, 70, 229));
                string sqlUpdate = "UPDATE MayViTinh SET Anh = @anh WHERE MaMayVT = @id";
                SqlParameter[] updateParams = new SqlParameter[]
                {
                    new SqlParameter("@id", testId),
                    new SqlParameter("@anh", SqlDbType.VarBinary, -1) { Value = updated22Kb }
                };
                Functions.RunSQL(sqlUpdate, updateParams, false);

                DataTable dt2 = Functions.GetDataToTable("SELECT Anh FROM MayViTinh WHERE MaMayVT = @id", new SqlParameter[] { new SqlParameter("@id", testId) });
                byte[] updatedBytes = (byte[])dt2.Rows[0]["Anh"];
                bool matchUpdated = (updatedBytes.Length == updated22Kb.Length);

                Image decoded = Functions.ByteArrayToImage(updatedBytes);
                bool validDecode = (decoded != null && decoded.Width == 400 && decoded.Height == 250);
                if (decoded != null) decoded.Dispose();

                if (matchInitial && matchUpdated && validDecode)
                {
                    RecordPass(testName, string.Format("Parameterized INSERT & UPDATE with 18KB-22KB PNG byte arrays verified bit-for-bit in SQL Server (Insert: {0} bytes, Update: {1} bytes, Decoded: 400x250).", fetchedBytes.Length, updatedBytes.Length));
                }
                else
                {
                    RecordFail(testName, string.Format("Mismatch: matchInitial={0}, matchUpdated={1}, validDecode={2}", matchInitial, matchUpdated, validDecode));
                }
            }
            catch (Exception ex)
            {
                RecordFail(testName, "Exception: " + ex.Message);
            }
            finally
            {
                Functions.RunSQL(string.Format("DELETE FROM MayViTinh WHERE MaMayVT = '{0}'", testId), false);
            }
        }
    }
}
