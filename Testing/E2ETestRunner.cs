using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace QLBanMayViTinh.Testing
{
    public class E2ETestRunner
    {
        [STAThread]
        public static int Main(string[] args)
        {
            int filterTier = 0;
            bool verbose = false;
            string reportPath = "TEST_REPORT.txt";

            for (int i = 0; i < args.Length; i++)
            {
                string arg = args[i].ToLowerInvariant();
                if (arg == "--tier" && i + 1 < args.Length)
                {
                    int.TryParse(args[++i], out filterTier);
                }
                else if (arg == "--verbose" || arg == "-v")
                {
                    verbose = true;
                }
                else if (arg == "--report" && i + 1 < args.Length)
                {
                    reportPath = args[++i];
                }
            }

            Console.OutputEncoding = Encoding.UTF8;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("======================================================================");
            Console.WriteLine("            E2E OPAQUE-BOX AUTOMATED TEST SUITE RUNNER                ");
            Console.WriteLine("        QLBanMayViTinh Modernized .NET 4.8 Store Management           ");
            Console.WriteLine("======================================================================");
            Console.ResetColor();
            Console.WriteLine(string.Format("Timestamp : {0:yyyy-MM-dd HH:mm:ss}", DateTime.Now));
            Console.WriteLine(string.Format("Filter    : {0}", filterTier > 0 ? string.Format("Tier {0}", filterTier) : "All Tiers (1 - 4)"));
            Console.WriteLine(string.Format("Platform  : {0} ({1}-bit)", Environment.OSVersion, Environment.Is64BitProcess ? 64 : 32));
            Console.WriteLine("----------------------------------------------------------------------\n");

            List<TestCaseResult> results = null;
            try
            {
                results = E2ETestSuite.RunAllTests(filterTier);
            }
            catch (Exception fatalEx)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n[FATAL] Test runner encountered unhandled environment exception:");
                Console.WriteLine(fatalEx.ToString());
                Console.ResetColor();
                return 2;
            }

            int totalCount = results.Count;
            int passCount = 0;
            int failCount = 0;

            Dictionary<int, int> tierTotals = new Dictionary<int, int> { { 1, 0 }, { 2, 0 }, { 3, 0 }, { 4, 0 } };
            Dictionary<int, int> tierPasses = new Dictionary<int, int> { { 1, 0 }, { 2, 0 }, { 3, 0 }, { 4, 0 } };

            StringBuilder reportSb = new StringBuilder();
            reportSb.AppendLine("======================================================================");
            reportSb.AppendLine("                   E2E TEST EXECUTION SUMMARY REPORT                  ");
            reportSb.AppendLine(string.Format("Generated: {0:yyyy-MM-dd HH:mm:ss}", DateTime.Now));
            reportSb.AppendLine("======================================================================\n");

            foreach (TestCaseResult res in results)
            {
                if (tierTotals.ContainsKey(res.Tier))
                {
                    tierTotals[res.Tier]++;
                }

                if (res.Passed)
                {
                    passCount++;
                    if (tierPasses.ContainsKey(res.Tier))
                    {
                        tierPasses[res.Tier]++;
                    }

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("[PASS] ");
                    Console.ResetColor();
                    Console.WriteLine(string.Format("{0,-7} | {1,-45} | {2,5} ms | {3}", res.Id, res.Name, res.ElapsedMilliseconds, res.Message));

                    reportSb.AppendLine(string.Format("[PASS] {0,-7} | {1,-45} | {2,5} ms | {3}", res.Id, res.Name, res.ElapsedMilliseconds, res.Message));
                }
                else
                {
                    failCount++;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("[FAIL] ");
                    Console.ResetColor();
                    Console.WriteLine(string.Format("{0,-7} | {1,-45} | {2,5} ms | {3}", res.Id, res.Name, res.ElapsedMilliseconds, res.Message));

                    reportSb.AppendLine(string.Format("[FAIL] {0,-7} | {1,-45} | {2,5} ms | {3}", res.Id, res.Name, res.ElapsedMilliseconds, res.Message));
                    if (!string.IsNullOrEmpty(res.ErrorDetails) && verbose)
                    {
                        reportSb.AppendLine("       Error Details: " + res.ErrorDetails);
                    }
                }
            }

            Console.WriteLine("\n----------------------------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("                        TIER BREAKDOWN SUMMARY                        ");
            Console.ResetColor();
            Console.WriteLine("----------------------------------------------------------------------");

            string[] tierLabels = new string[]
            {
                "Tier 1: Feature Coverage (R1 - R5 Contracts)",
                "Tier 2: Boundary & Corner Cases (Adversarial)",
                "Tier 3: Cross-Feature Combinations (Integration)",
                "Tier 4: Real-World Scenarios (End-to-End Store)"
            };

            for (int t = 1; t <= 4; t++)
            {
                if (filterTier == 0 || filterTier == t)
                {
                    int total = tierTotals[t];
                    int pass = tierPasses[t];
                    double pct = total > 0 ? ((double)pass / total) * 100 : 0.0;
                    Console.WriteLine(string.Format("{0,-50}: {1,2}/{2,2} ({3,5:0.0}%)", tierLabels[t - 1], pass, total, pct));
                    reportSb.AppendLine(string.Format("{0,-50}: {1,2}/{2,2} ({3,5:0.0}%)", tierLabels[t - 1], pass, total, pct));
                }
            }

            double overallPct = totalCount > 0 ? ((double)passCount / totalCount) * 100 : 0.0;
            Console.WriteLine("======================================================================");
            if (failCount == 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(string.Format("OVERALL STATUS: SUCCESS | {0}/{1} PASSED ({2:0.0}%)", passCount, totalCount, overallPct));
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(string.Format("OVERALL STATUS: PENDING/PARTIAL | {0}/{1} PASSED ({2:0.0}%) - {3} REQUIRE IMPLEMENTATION",
                    passCount, totalCount, overallPct, failCount));
            }
            Console.ResetColor();
            Console.WriteLine("======================================================================\n");

            reportSb.AppendLine("======================================================================");
            reportSb.AppendLine(string.Format("TOTAL: {0}/{1} PASSED ({2:0.0}%) | FAILED/PENDING: {3}", passCount, totalCount, overallPct, failCount));
            reportSb.AppendLine("======================================================================");

            try
            {
                File.WriteAllText(reportPath, reportSb.ToString(), Encoding.UTF8);
                Console.WriteLine(string.Format("Detailed summary saved to: {0}", Path.GetFullPath(reportPath)));
            }
            catch
            {
            }

            return failCount == 0 ? 0 : 1;
        }
    }
}
