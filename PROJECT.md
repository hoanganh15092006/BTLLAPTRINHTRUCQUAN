# Project: Quản lý Cửa hàng Bán máy vi tính (.NET WinForms Modern Upgrade)

## Architecture
- **Platform**: .NET Framework 4.8, Windows Forms, C# 7.3+, MSBuild Legacy AnyCPU.
- **UI Framework**: Modernized WinForms with Guna.UI2.WinForms (v2.0.4.6) for rounded buttons, textboxes, comboboxes, panels, and data grids, strictly adhering to Visual Studio Designer compatibility rules.
- **Theme Architecture**: Centralized `UIThemeHelper` with recursive `ApplyTheme(Control root)`, event-driven dispatch `OnThemeChanged`, high-contrast color palettes (Dark / Light), and persistent storage in `tblCauHinhHeThong`.
- **Database & Data Access**: Microsoft SQL Server (LocalDB / SQLEXPRESS), ADO.NET with parameterized queries in `Functions.cs`, `VARBINARY(MAX)` binary image storage for `MayViTinh.Anh`, GDI+ memory-safe conversion (`ByteArrayToImage` / `ImageToByteArray`).
- **Clean Code Standard**: Zero comments policy (Constraint R5) strictly enforced across all `.cs` and `.sql` files.

## Feature Inventory
| # | Feature | Description | Milestone | Source |
|---|---------|-------------|-----------|--------|
| 1 | Database Schema Update to VARBINARY(MAX) | Alter `MayViTinh.Anh` to `VARBINARY(MAX) NULL`, ensure backward migration in `EnsureSystemTables()` | M1 | Survey DB / R1 |
| 2 | Component Catalog Expansion | Expand lookup tables for CPUs, RAM, Hard Drives, Screens, Manufacturers to support 3NF specs | M1 | Survey DB / R1 |
| 3 | 32+ Diverse Computer Models Dataset | Seed 32 realistic models across Gaming, Workstation, Office, Student with complete specs and embedded hex PNG images | M1 | Survey DB / R1 |
| 4 | Safe Binary Image Conversion in DAL | Implement `ByteArrayToImage` (bitmap clone) and `ImageToByteArray` in `Functions.cs` | M1 | Survey DB / R1 |
| 5 | Parameterized SQL Execution | Add `RunSQL(string, SqlParameter[])` and `GetDataToTable(string, SqlParameter[])` to `Functions.cs` | M1 | Survey DB / R1 |
| 6 | Computer Management Form Binary Image UI | Load `byte[]` in `dgvMayViTinh_CellClick`, image pick via `File.ReadAllBytes`, parameterized save in `frmMayViTinh.cs` | M1 | Survey DB / R1 |
| 7 | SelfTestSuite Unit Test Type Safety | Update unit test insert in `Testing/SelfTestSuite.cs` to pass `NULL` instead of `N''` for binary `Anh` | M1 | Survey DB / R1 |
| 8 | Guna2 UI Library Integration | Add `<PackageReference Include="Guna.UI2.WinForms" Version="2.0.4.6" />` to `QLBanMayViTinh.csproj` | M2 | Survey UI / R2 |
| 9 | Visual Studio Designer Compatibility | Enforce 7 Designer Rules: Constructor Purity, Triple Designtime Guard, AnyCPU, parameterless constructors | M2 | Survey UI / R2 |
| 10 | Rounded UI Controls on Main Dashboard | Modernize `frmMain` sidebar, header, Bento cards with rounded corners and high aesthetic appeal | M2 | Survey UI / R2 |
| 11 | Rounded UI on Product & Catalog Forms | Restyle `frmMayViTinh` and `frmDanhMucHeThong` with rounded cards, textboxes, comboboxes, and styled data grids | M2 | Survey UI / R2 |
| 12 | Rounded UI on Transaction Forms (POS & Import) | Restyle `frmHoaDonBan` and `frmHoaDonNhap` with modern inputs and rounded summary cards | M2 | Survey UI / R2 |
| 13 | Rounded UI on Search, Reports & Settings Forms | Restyle `frmTimKiem`, `frmBaoCaoThongKe`, and `frmCaiDat` with modern rounded components | M2 | Survey UI / R2 |
| 14 | Recursive Theme Engine | Implement `UIThemeHelper.ApplyTheme(Control root)` traversing all child containers, labels, inputs, and grids | M3 | Survey Theme / R3 |
| 15 | High-Contrast Dark/Light Color Tokens | Replace all hardcoded navy `(15, 23, 42)` and black labels with dynamic theme tokens to eliminate dark-on-dark | M3 | Survey Theme / R3 |
| 16 | Event-Driven Theme Dispatch & Persistence | Implement `OnThemeChanged` event, sync active child form immediately, persist theme state in `tblCauHinhHeThong` | M3 | Survey Theme / R3 |
| 17 | Synchronized Form Navigation Theming | Apply active theme automatically in `OpenChildForm` and form load handlers | M3 | Survey Theme / R3 |
| 18 | Elimination of Login Banner/Intro Panel | Remove `pnlLeft` and 7 promotional labels from `frmDangNhap` per R4 | M4 | Survey Theme / R4 |
| 19 | Centered Modern Login Card Layout | Center `pnlLoginCard` in `460 x 550` window with modern inputs, eye toggle, remember checkbox, and action buttons | M4 | Survey Theme / R4 |
| 20 | Zero-Comment Code Compliance (R5) | Ensure 0 comments (`//`, `/* */`, `///`, `--`) in all modified or generated code files | M5 | Survey All / R5 |
| 21 | Full Solution Build & SelfTestSuite Validation | Compile with 0 warnings/errors via MSBuild, pass 100% of the 12 SelfTestSuite unit tests | M5 | Project Gate |
| 22 | Comprehensive E2E Verification | Execute opaque-box test scenarios covering DB, UI, Theme switching, Login, and POS workflows | M5 | Project Gate |

## Milestones
| # | Name | Scope | Dependencies | Status |
|---|------|-------|-------------|--------|
| M1 | Database & Binary Image Storage (R1) | Schema alter to VARBINARY(MAX), 32-model dataset with embedded hex PNGs, DAL parameterized methods, frmMayViTinh binary binding, SelfTestSuite fix | none | DONE |
| M2 | Modern UI Library & Rounded Styling (R2) | Guna.UI2.WinForms v2.0.4.6 integration, 7 VS Designer compatibility rules, rounded restyling of all 9 main forms | M1 | DONE |
| M3 | Dark/Light Mode Harmonization (R3) | Centralized recursive theme engine, contrast fix (replace hardcoded dark labels), event dispatch, persistence | M2 | DONE |
| M4 | Streamlined Login Form (R4) | Remove intro banner panel, center modern card layout on frmDangNhap | M2, M3 | DONE |
| M5 | E2E Testing, Adversarial Hardening & Forensic Audit (R5) | 100% E2E test pass, 100% SelfTestSuite pass, zero comments verification, forensic integrity audit | M1, M2, M3, M4 | DONE |

## Interface Contracts

### DAL (Functions.cs) ↔ Forms (frmMayViTinh, frmMain, etc.)
- `public static Image ByteArrayToImage(byte[] byteArray)`: Converts `byte[]` from `MayViTinh.Anh` to an independent GDI+ `Bitmap`, avoiding file locking or stream disposal errors. Returns `null` on null/empty.
- `public static byte[] ImageToByteArray(Image image)`: Serializes `Image` to PNG `byte[]`.
- `public static void RunSQL(string sql, SqlParameter[] parameters, bool showMessageBox = true)`: Parameterized command execution supporting `SqlDbType.VarBinary`.
- `public static DataTable GetDataToTable(string sql, SqlParameter[] parameters = null)`: Parameterized query returning `DataTable`.

### Theme Engine (UIThemeHelper.cs) ↔ All Forms & Controls
- `public static bool IsDarkMode { get; set; }`
- `public static event Action OnThemeChanged;`
- `public static void ApplyTheme(Control root)`: Recursively traverses `root` and sets appropriate `BackColor`, `ForeColor`, and child styles for Forms, Panels, Labels, TextBoxes, ComboBoxes, and DataGridViews.
- `public static void SaveThemeToDatabase(bool isDark)` / `public static bool LoadThemeFromDatabase()`: Persists theme preference to `tblCauHinhHeThong`.

### VS Designer Compatibility Contract
- Every Form/UserControl must have a public parameterless constructor calling ONLY `InitializeComponent()`.
- No database connections, theme loops, or runtime logic during designtime:
  `if (this.DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;`

## Code Layout
- `QLBanMayViTinh.csproj` — Project file with Guna.UI2.WinForms package reference.
- `Database_QLBanMayViTinh.sql` — Schema definition and 32+ models seed script.
- `Classes/Functions.cs` — Database connection, parameterized queries, and image binary conversions.
- `Classes/UIThemeHelper.cs` — Theme engine, color palettes, recursive control styler, and theme event.
- `Forms/frmDangNhap.cs`, `frmDangNhap.Designer.cs` — Streamlined modern centered login form.
- `Forms/frmMain.cs`, `frmMain.Designer.cs` — Main container, navigation sidebar, Bento dashboard.
- `Forms/frmMayViTinh.cs`, `frmMayViTinh.Designer.cs` — Computer management with binary image loader.
- `Forms/frmDanhMucHeThong.cs`, `frmDanhMucHeThong.Designer.cs` — Shared catalogs (brands, specs).
- `Forms/frmHoaDonNhap.cs`, `frmHoaDonBan.cs` — Inventory import and POS sales invoices.
- `Forms/frmTimKiem.cs`, `frmBaoCaoThongKe.cs`, `frmCaiDat.cs` — Search, reporting, and settings forms.
- `Testing/SelfTestSuite.cs` — In-process unit test suite.
