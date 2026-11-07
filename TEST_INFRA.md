# Test Infrastructure Specification: Quản lý Cửa hàng Bán máy vi tính

## 1. Overview and Architecture

The E2E Test Suite for the modernized Computer Store Management system is an opaque-box, requirement-driven test infrastructure. It verifies all 5 user requirements (R1: Database & Binary Images, R2: Modern Rounded UI & Designer Compatibility, R3: Harmonized Dark/Light Theming, R4: Streamlined Centered Login, R5: Zero Comments Compliance) and all 22 features identified in `PROJECT.md`.

### Technical Foundation
- **Platform**: .NET Framework 4.8 (x64 / AnyCPU).
- **Test Runner Executable**: `Testing/E2ETestRunner.exe` compiled from `Testing/E2ETestRunner.cs` and `Testing/E2ETestSuite.cs`.
- **CLI Automation Script**: `run_e2e_tests.ps1` (PowerShell 5.1+ / PowerShell Core) and `run_e2e_tests.bat`.
- **Database Target**: Microsoft SQL Server (Local instance: `.\SQLEXPRESS01`, `.\SQLEXPRESS`, `.` or `localhost`, catalog: `QLBanMayViTinh`).
- **Assertion Engine**: Built-in opaque-box test framework with isolated setup/teardown fixtures, granular assertions, timing benchmarks, and deterministic expected output derivations.
- **Strict Compliance**: Zero comments policy (Constraint R5) enforced across all `.cs` and `.sql` test files.

---

## 2. Four-Tier Test Architecture

The test suite is partitioned into four distinct validation tiers:

### Tier 1: Feature Coverage (Unit & Functional Verification)
Verifies discrete feature implementations against their interface contracts:
- **T1.1 - Database Schema & Catalog Volume**: Verifies presence of all 25 system & catalog tables, verifies that `MayViTinh` contains 32+ models, verifies 14 normalized component lookup tables have valid records.
- **T1.2 - VARBINARY(MAX) Image Storage**: Queries `MayViTinh.Anh` to verify data type is `VARBINARY(MAX)`, verifies stored images start with valid image magic bytes (e.g. PNG `0x89504E47` or JPEG `0xFFD8FFE0`), verifies non-null image payload sizes.
- **T1.3 - DAL Parameterized Operations & Conversion**: Tests `Functions.ByteArrayToImage` (independent GDI+ Bitmap creation) and `Functions.ImageToByteArray`, tests `Functions.RunSQL(string, SqlParameter[])` and `Functions.GetDataToTable(string, SqlParameter[])`.
- **T1.4 - UI Control Styles & Designer Purity**: Validates parameterless constructors for all 13 Forms, verifies absence of runtime execution during designtime, validates Guna2 / rounded control styling properties.
- **T1.5 - Recursive Theme Engine Tokens**: Validates `UIThemeHelper.ApplyTheme` recursively traverses control hierarchies, verifies distinct Dark Mode (`#1E293B` / `#334155`) vs Light Mode (`#F1F5F9` / `#FFFFFF`) token palettes.
- **T1.6 - Streamlined Centered Login Card**: Verifies `pnlLeft` and marketing labels are absent from `frmDangNhap`, verifies `pnlLoginCard` is centered, verifies input fields and toggle password function.
- **T1.7 - Security Cryptography**: Verifies PBKDF2 password hashing with distinct salt generation and verification, verifies AES-256 reversible encryption with `ENC:` prefix.

### Tier 2: Boundary & Corner Cases (Adversarial Robustness)
Validates system resiliency against edge cases, stress conditions, and invalid inputs:
- **T2.1 - Null & Malformed Image Payloads**: Passes `null`, empty byte array `byte[0]`, and corrupted byte arrays to `ByteArrayToImage` and `MayViTinh.Anh` column to verify graceful null handling without exceptions.
- **T2.2 - Extreme Financial & Numeric Boundaries**: Tests 0 VNĐ prices, decimal precision, maximum boundary prices (e.g. 999,999,999 VNĐ), and negative inventory input rejection.
- **T2.3 - Inventory Negative Stock Prevention**: Exercises trigger `trg_ChiTietHDB_AUID` by attempting to sell more items than available in stock, verifying transaction rollback and stock preservation.
- **T2.4 - Rapid Theme Toggling Stress**: Simulates 100 consecutive rapid toggles between Dark Mode and Light Mode within milliseconds to verify zero memory leaks, GDI handle stability, and consistent final state.
- **T2.5 - Authentication Edge Cases**: Tests invalid passwords, non-existent usernames, empty strings, SQL injection strings (`' OR '1'='1`) to verify zero security bypasses.

### Tier 3: Cross-Feature Combinations (Integration Workflows)
Validates interactions across multiple subsystems:
- **T3.1 - Binary Image Addition -> Catalog Search -> UI Inspection**: Inserts a new computer with a valid synthetic PNG binary image -> searches by hardware criteria (CPU/RAM/Price) via search logic -> verifies matching record returns complete image binary.
- **T3.2 - Live Theme Dispatch Across Multiple Forms**: Instantiates `frmMain` with open child forms (`frmMayViTinh`, `frmHoaDonBan`, `frmTimKiem`) -> dispatches `UIThemeHelper.OnThemeChanged` -> verifies all child controls update background and foreground tokens synchronously.
- **T3.3 - Theme Preference Persistence Round-Trip**: Saves Dark Mode preference to `tblCauHinhHeThong` -> restarts theme state -> loads preference from database -> verifies persistence across sessions.
- **T3.4 - Auto Pricing Trigger Calculation**: Inserts an import invoice record -> verifies trigger `trg_ChiTietHDN_AUID` automatically updates `MayViTinh.GiaNhap` and calculates `GiaBan = ROUND(GiaNhap * 1.10, 0)`.

### Tier 4: Real-World Scenarios (End-to-End Store Workflows)
Validates end-to-end user journeys mirroring practical store operations:
- **T4.1 - Complete Store POS Lifecycle**:
  1. Authenticate user (`admin` / default credentials) via PBKDF2 verification.
  2. Audit initial catalog stock level for target model.
  3. Execute wholesale import invoice (`HoaDonNhap` + `ChiTietHDN`) adding 10 units.
  4. Verify warehouse inventory and automated 110% price recalculation.
  5. Execute retail POS checkout (`HoaDonBan` + `ChiTietHDB`) selling 4 units.
  6. Verify real-time stock deduction (initial + 10 - 4).
  7. Generate financial sales report and confirm invoice total equals unit quantity * selling price.
  8. Clean up test records, verifying foreign key cascade integrity and final database consistency.

---

## 3. Test Runner Specification

### CLI Usage and Arguments
```powershell
# Compile and run all tests via PowerShell harness
powershell -ExecutionPolicy Bypass -File .\run_e2e_tests.ps1

# Direct execution of compiled runner
.\bin\Debug\E2ETestRunner.exe

# Filter by tier
.\bin\Debug\E2ETestRunner.exe --tier 1
.\bin\Debug\E2ETestRunner.exe --tier 2
.\bin\Debug\E2ETestRunner.exe --tier 3
.\bin\Debug\E2ETestRunner.exe --tier 4

# Run with verbose trace output
.\bin\Debug\E2ETestRunner.exe --verbose
```

### Exit Codes
- `0`: All executed test cases PASSED.
- `1`: One or more test cases FAILED.
- `2`: Environment failure (e.g., SQL Server unreachable, compiler missing).

### Output Report Format
The test runner outputs human-readable console diagnostics and generates a structured summary file `TEST_REPORT.txt` containing:
- Test execution timestamp and host environment metadata.
- Tier breakdown with test case identifiers, descriptions, elapsed execution times, and PASS/FAIL status.
- Detailed failure diagnostics (expected vs. actual output, stack trace).
- Aggregate summary: Total tests, Passed count, Failed count, Success percentage.

---

## 4. Verification and Validation Rules

1. **Isolation**: Every test creating database records uses unique test keys prefixed with `E2E_TEST_` and cleans up in a `finally` block or dedicated teardown routine.
2. **Deterministic Expectations**: Test expectations are derived directly from database schema definitions, business rules (110% margin, PBKDF2 cryptography, PNG magic bytes `89-50-4E-47`), and requirement specifications.
3. **No Facades**: All tests query real SQL Server tables, invoke actual DAL routines, instantiate real Windows Forms controls, and verify live GDI+ / Theme states.
4. **Zero Comments Policy**: All test code files strictly enforce Constraint R5.
