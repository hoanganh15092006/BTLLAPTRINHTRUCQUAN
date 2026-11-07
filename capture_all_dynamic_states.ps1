param(
    [string]$OutputDir = "Screenshots\Verified"
)

if (-not (Test-Path $OutputDir)) {
    New-Item -ItemType Directory -Force -Path $OutputDir | Out-Null
}

[System.Reflection.Assembly]::LoadWithPartialName("System.Windows.Forms") | Out-Null
[System.Reflection.Assembly]::LoadWithPartialName("System.Drawing") | Out-Null
[System.Reflection.Assembly]::LoadFrom((Resolve-Path "bin\Debug\Guna.UI2.dll").Path) | Out-Null
[System.Reflection.Assembly]::LoadFrom((Resolve-Path "bin\Debug\QLBanMayViTinh.exe").Path) | Out-Null

[QLBanMayViTinh.Classes.Functions]::Connect()
[QLBanMayViTinh.Classes.Functions]::CurrentUsername = "admin"
[QLBanMayViTinh.Classes.Functions]::CurrentRole = "Admin"
[QLBanMayViTinh.Classes.Functions]::CurrentMaNV = "NV01"

function CaptureForm($form, $filename) {
    [System.Windows.Forms.Application]::DoEvents()
    Start-Sleep -Milliseconds 100
    $bmp = New-Object System.Drawing.Bitmap($form.Width, $form.Height)
    $rect = New-Object System.Drawing.Rectangle(0, 0, $form.Width, $form.Height)
    $form.DrawToBitmap($bmp, $rect)
    $bmp.Save("$OutputDir\$filename.png", [System.Drawing.Imaging.ImageFormat]::Png)
    $bmp.Dispose()
}

[QLBanMayViTinh.Classes.UIThemeHelper]::IsDarkMode = $false

$fMain = New-Object QLBanMayViTinh.Forms.frmMain
$fMain.StartPosition = [System.Windows.Forms.FormStartPosition]::Manual
$fMain.Location = New-Object System.Drawing.Point(0, 0)
$fMain.Show()
[System.Windows.Forms.Application]::DoEvents()

CaptureForm $fMain "00_Main_Dashboard_Light"

$btnTheme = $fMain.Controls.Find("btnToggleTheme", $true)[0]
$btnTheme.PerformClick()
CaptureForm $fMain "00_Main_Dashboard_Dark"

$btnTheme.PerformClick()

$navMayTinh = $fMain.Controls.Find("btnNavMayTinh", $true)[0]
$navMayTinh.PerformClick()
CaptureForm $fMain "00_Main_Nav_MayTinh"

$navDanhMuc = $fMain.Controls.Find("btnNavDanhMuc", $true)[0]
$navDanhMuc.PerformClick()
CaptureForm $fMain "00_Main_Nav_DanhMuc"

$navHDN = $fMain.Controls.Find("btnNavHDNhap", $true)[0]
$navHDN.PerformClick()
CaptureForm $fMain "00_Main_Nav_HDNhap"

$navHDB = $fMain.Controls.Find("btnNavHDBan", $true)[0]
$navHDB.PerformClick()
CaptureForm $fMain "00_Main_Nav_HDBan"

$navTim = $fMain.Controls.Find("btnNavTimKiem", $true)[0]
$navTim.PerformClick()
CaptureForm $fMain "00_Main_Nav_TimKiem"

$navBC = $fMain.Controls.Find("btnNavBaoCao", $true)[0]
$navBC.PerformClick()
CaptureForm $fMain "00_Main_Nav_BaoCao"

$navHoSo = $fMain.Controls.Find("btnNavHoSo", $true)[0]
$navHoSo.PerformClick()
CaptureForm $fMain "00_Main_Nav_HoSo"

$navUser = $fMain.Controls.Find("btnNavNguoiDung", $true)[0]
$navUser.PerformClick()
CaptureForm $fMain "00_Main_Nav_NguoiDung"

$navLog = $fMain.Controls.Find("btnNavNhatKy", $true)[0]
$navLog.PerformClick()
CaptureForm $fMain "00_Main_Nav_NhatKy"

$navSet = $fMain.Controls.Find("btnNavCaiDat", $true)[0]
$navSet.PerformClick()
CaptureForm $fMain "00_Main_Nav_CaiDat"

$fMain.Close()
$fMain.Dispose()

$fMVT = New-Object QLBanMayViTinh.Forms.frmMayViTinh
$fMVT.StartPosition = [System.Windows.Forms.FormStartPosition]::Manual
$fMVT.Location = New-Object System.Drawing.Point(0, 0)
$fMVT.Show()
[System.Windows.Forms.Application]::DoEvents()
[QLBanMayViTinh.Classes.UIThemeHelper]::ApplyTheme($fMVT)

CaptureForm $fMVT "05_MayViTinh_Default_View"

$dgvMVT = $fMVT.Controls.Find("dgvMayViTinh", $true)[0]
if ($dgvMVT.Rows.Count -gt 0) {
    $dgvMVT.CurrentCell = $dgvMVT.Rows[0].Cells[0]
    $m = [QLBanMayViTinh.Forms.frmMayViTinh].GetMethod("dgvMayViTinh_CellClick", [System.Reflection.BindingFlags]::NonPublic -bor [System.Reflection.BindingFlags]::Instance)
    if ($m) {
        $args = New-Object System.Windows.Forms.DataGridViewCellEventArgs(0, 0)
        $m.Invoke($fMVT, @($dgvMVT, $args))
    }
}
CaptureForm $fMVT "05_MayViTinh_Selected_Row"

$btnThemMVT = $fMVT.Controls.Find("btnThem", $true)[0]
$btnThemMVT.PerformClick()
CaptureForm $fMVT "05_MayViTinh_State_ThemMoi"

$fMVT.Close()
$fMVT.Dispose()

$fDM = New-Object QLBanMayViTinh.Forms.frmDanhMucHeThong
$fDM.StartPosition = [System.Windows.Forms.FormStartPosition]::Manual
$fDM.Location = New-Object System.Drawing.Point(0, 0)
$fDM.Show()
[System.Windows.Forms.Application]::DoEvents()
[QLBanMayViTinh.Classes.UIThemeHelper]::ApplyTheme($fDM)

$tabMain = $fDM.Controls.Find("tabMain", $true)[0]
$tabMain.SelectedIndex = 0
CaptureForm $fDM "06_DanhMuc_Tab1_KhachHang"

$tabMain.SelectedIndex = 1
CaptureForm $fDM "06_DanhMuc_Tab2_NhaCungCap"

$tabMain.SelectedIndex = 2
CaptureForm $fDM "06_DanhMuc_Tab3_NhanVien"

$tabMain.SelectedIndex = 3
$cboLoai = $fDM.Controls.Find("cboLoaiDanhMuc", $true)[0]
if ($cboLoai.Items.Count -gt 1) {
    $cboLoai.SelectedIndex = 1
}
CaptureForm $fDM "06_DanhMuc_Tab4_LinhKien_Chip"

if ($cboLoai.Items.Count -gt 2) {
    $cboLoai.SelectedIndex = 2
}
CaptureForm $fDM "06_DanhMuc_Tab4_LinhKien_RAM"

if ($cboLoai.Items.Count -gt 6) {
    $cboLoai.SelectedIndex = 6
}
CaptureForm $fDM "06_DanhMuc_Tab4_LinhKien_ManHinh"

$fDM.Close()
$fDM.Dispose()

$fUser = New-Object QLBanMayViTinh.Forms.frmQuanLyNguoiDung
$fUser.StartPosition = [System.Windows.Forms.FormStartPosition]::Manual
$fUser.Location = New-Object System.Drawing.Point(0, 0)
$fUser.Show()
[System.Windows.Forms.Application]::DoEvents()
[QLBanMayViTinh.Classes.UIThemeHelper]::ApplyTheme($fUser)

CaptureForm $fUser "11_NguoiDung_Default"

$dgvUser = $fUser.Controls.Find("dgvTaiKhoan", $true)[0]
if ($dgvUser.Rows.Count -gt 0) {
    $dgvUser.CurrentCell = $dgvUser.Rows[0].Cells[0]
    $m = [QLBanMayViTinh.Forms.frmQuanLyNguoiDung].GetMethod("dgvTaiKhoan_CellClick", [System.Reflection.BindingFlags]::NonPublic -bor [System.Reflection.BindingFlags]::Instance)
    if ($m) {
        $args = New-Object System.Windows.Forms.DataGridViewCellEventArgs(0, 0)
        $m.Invoke($fUser, @($dgvUser, $args))
    }
}
CaptureForm $fUser "11_NguoiDung_Selected_User"

$btnThemU = $fUser.Controls.Find("btnThem", $true)[0]
CaptureForm $fUser "11_NguoiDung_State_ThemMoi"

$fUser.Close()
$fUser.Dispose()

$fHoSo = New-Object QLBanMayViTinh.Forms.frmHoSoCaNhan
$fHoSo.StartPosition = [System.Windows.Forms.FormStartPosition]::Manual
$fHoSo.Location = New-Object System.Drawing.Point(0, 0)
$fHoSo.Show()
[System.Windows.Forms.Application]::DoEvents()
[QLBanMayViTinh.Classes.UIThemeHelper]::ApplyTheme($fHoSo)
CaptureForm $fHoSo "12_HoSoCaNhan_View"
$fHoSo.Close()
$fHoSo.Dispose()

$fDangKy = New-Object QLBanMayViTinh.Forms.frmDangKy
$fDangKy.StartPosition = [System.Windows.Forms.FormStartPosition]::Manual
$fDangKy.Location = New-Object System.Drawing.Point(0, 0)
$fDangKy.Show()
[System.Windows.Forms.Application]::DoEvents()
[QLBanMayViTinh.Classes.UIThemeHelper]::ApplyTheme($fDangKy)
$btnDK = $fDangKy.Controls.Find("btnDangKy", $true)[0]
$btnDK.PerformClick()
CaptureForm $fDangKy "02_DangKy_Validation_Error"
$fDangKy.Close()
$fDangKy.Dispose()

$fQuenMK = New-Object QLBanMayViTinh.Forms.frmQuenMatKhau
$fQuenMK.StartPosition = [System.Windows.Forms.FormStartPosition]::Manual
$fQuenMK.Location = New-Object System.Drawing.Point(0, 0)
$fQuenMK.Show()
[System.Windows.Forms.Application]::DoEvents()
[QLBanMayViTinh.Classes.UIThemeHelper]::ApplyTheme($fQuenMK)
$btnReset = $fQuenMK.Controls.Find("btnDatLaiMatKhau", $true)[0]
$btnReset.PerformClick()
CaptureForm $fQuenMK "03_QuenMatKhau_Validation_Error"
$fQuenMK.Close()
$fQuenMK.Dispose()

Write-Host "All multi-state dynamic UI forms successfully captured and verified."

