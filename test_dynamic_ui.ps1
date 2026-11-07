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

function CaptureForm($form, $filename) {
    [System.Windows.Forms.Application]::DoEvents()
    Start-Sleep -Milliseconds 150
    $bmp = New-Object System.Drawing.Bitmap($form.Width, $form.Height)
    $rect = New-Object System.Drawing.Rectangle(0, 0, $form.Width, $form.Height)
    $form.DrawToBitmap($bmp, $rect)
    $bmp.Save("$OutputDir\$filename.png", [System.Drawing.Imaging.ImageFormat]::Png)
    $bmp.Dispose()
}

[QLBanMayViTinh.Classes.UIThemeHelper]::IsDarkMode = $false

$fBaoCao = New-Object QLBanMayViTinh.Forms.frmBaoCaoThongKe
$fBaoCao.StartPosition = [System.Windows.Forms.FormStartPosition]::Manual
$fBaoCao.Location = New-Object System.Drawing.Point(0, 0)
$fBaoCao.Show()
[System.Windows.Forms.Application]::DoEvents()
[QLBanMayViTinh.Classes.UIThemeHelper]::ApplyTheme($fBaoCao)

$rdoNhap = $fBaoCao.Controls.Find("rdoNhapHang", $true)[0]
$rdoTop5 = $fBaoCao.Controls.Find("rdoTop5NV", $true)[0]
$rdoTonKho = $fBaoCao.Controls.Find("rdoTonKho", $true)[0]

$rdoNhap.PerformClick()
CaptureForm $fBaoCao "01_BaoCao_rdoNhapHang_Light"

[QLBanMayViTinh.Classes.UIThemeHelper]::IsDarkMode = $true
[QLBanMayViTinh.Classes.UIThemeHelper]::ApplyTheme($fBaoCao)
CaptureForm $fBaoCao "01_BaoCao_rdoNhapHang_Dark"

[QLBanMayViTinh.Classes.UIThemeHelper]::IsDarkMode = $false
[QLBanMayViTinh.Classes.UIThemeHelper]::ApplyTheme($fBaoCao)
$rdoTop5.PerformClick()
CaptureForm $fBaoCao "01_BaoCao_rdoTop5_Light"

$rdoTonKho.PerformClick()
CaptureForm $fBaoCao "01_BaoCao_rdoTonKho_Light"

$fBaoCao.Close()
$fBaoCao.Dispose()

$fTimKiem = New-Object QLBanMayViTinh.Forms.frmTimKiem
$fTimKiem.StartPosition = [System.Windows.Forms.FormStartPosition]::Manual
$fTimKiem.Location = New-Object System.Drawing.Point(0, 0)
$fTimKiem.Show()
[System.Windows.Forms.Application]::DoEvents()
[QLBanMayViTinh.Classes.UIThemeHelper]::ApplyTheme($fTimKiem)

$rdoHDN = $fTimKiem.Controls.Find("rdoHoaDonNhap", $true)[0]
$rdoHDB = $fTimKiem.Controls.Find("rdoHoaDonBan", $true)[0]
$rdoMVT = $fTimKiem.Controls.Find("rdoMayViTinh", $true)[0]
$btnTim = $fTimKiem.Controls.Find("btnTimKiem", $true)[0]

$rdoHDN.PerformClick()
CaptureForm $fTimKiem "02_TimKiem_rdoHDN_Expanded"

$rdoHDB.PerformClick()
CaptureForm $fTimKiem "02_TimKiem_rdoHDB_Expanded"

$rdoMVT.PerformClick()
$btnTim.PerformClick()
CaptureForm $fTimKiem "02_TimKiem_MVT_Searched"

$fTimKiem.Close()
$fTimKiem.Dispose()

$fDM = New-Object QLBanMayViTinh.Forms.frmDanhMucHeThong
$fDM.StartPosition = [System.Windows.Forms.FormStartPosition]::Manual
$fDM.Location = New-Object System.Drawing.Point(0, 0)
$fDM.Show()
[System.Windows.Forms.Application]::DoEvents()
[QLBanMayViTinh.Classes.UIThemeHelper]::ApplyTheme($fDM)

$tabMain = $fDM.Controls.Find("tabMain", $true)[0]

$tabMain.SelectedIndex = 1
CaptureForm $fDM "03_DanhMuc_Tab2_NCC"

$tabMain.SelectedIndex = 2
CaptureForm $fDM "03_DanhMuc_Tab3_NhanVien"

$tabMain.SelectedIndex = 3
CaptureForm $fDM "03_DanhMuc_Tab4_LinhKien"

$fDM.Close()
$fDM.Dispose()

$fHDB = New-Object QLBanMayViTinh.Forms.frmHoaDonBan
$fHDB.StartPosition = [System.Windows.Forms.FormStartPosition]::Manual
$fHDB.Location = New-Object System.Drawing.Point(0, 0)
$fHDB.Show()
[System.Windows.Forms.Application]::DoEvents()
[QLBanMayViTinh.Classes.UIThemeHelper]::ApplyTheme($fHDB)

$btnThemHDB = $fHDB.Controls.Find("btnThemHD", $true)[0]
$btnThemHDB.PerformClick()
CaptureForm $fHDB "04_HoaDonBan_ClickTaoMoi"

$cboMaMay = $fHDB.Controls.Find("cboMaMay", $true)[0]
if ($cboMaMay.Items.Count -gt 1) {
    $cboMaMay.SelectedIndex = 1
}
$txtSoLuong = $fHDB.Controls.Find("txtSoLuong", $true)[0]
$txtSoLuong.Text = "2"
CaptureForm $fHDB "04_HoaDonBan_SelectProduct_Calculated"

[QLBanMayViTinh.Classes.UIThemeHelper]::IsDarkMode = $true
[QLBanMayViTinh.Classes.UIThemeHelper]::ApplyTheme($fHDB)
CaptureForm $fHDB "04_HoaDonBan_Dark"

$fHDB.Close()
$fHDB.Dispose()

$fHDN = New-Object QLBanMayViTinh.Forms.frmHoaDonNhap
$fHDN.StartPosition = [System.Windows.Forms.FormStartPosition]::Manual
$fHDN.Location = New-Object System.Drawing.Point(0, 0)
$fHDN.Show()
[System.Windows.Forms.Application]::DoEvents()
[QLBanMayViTinh.Classes.UIThemeHelper]::ApplyTheme($fHDN)

$btnThemHDN = $fHDN.Controls.Find("btnThemHD", $true)[0]
$btnThemHDN.PerformClick()
CaptureForm $fHDN "05_HoaDonNhap_ClickTaoMoi"

$cboMaMayN = $fHDN.Controls.Find("cboMaMay", $true)[0]
if ($cboMaMayN.Items.Count -gt 1) {
    $cboMaMayN.SelectedIndex = 1
}
$txtSoLuongN = $fHDN.Controls.Find("txtSoLuong", $true)[0]
$txtSoLuongN.Text = "5"
CaptureForm $fHDN "05_HoaDonNhap_SelectProduct_Calculated"

$fHDN.Close()
$fHDN.Dispose()

$fDangNhap = New-Object QLBanMayViTinh.Forms.frmDangNhap
$fDangNhap.StartPosition = [System.Windows.Forms.FormStartPosition]::Manual
$fDangNhap.Location = New-Object System.Drawing.Point(0, 0)
$fDangNhap.Show()
[System.Windows.Forms.Application]::DoEvents()
[QLBanMayViTinh.Classes.UIThemeHelper]::ApplyTheme($fDangNhap)
CaptureForm $fDangNhap "06_DangNhap_Fixed"

$btnEye = $fDangNhap.Controls.Find("btnTogglePass", $true)[0]
$btnEye.PerformClick()
CaptureForm $fDangNhap "06_DangNhap_EyeToggled"

$fDangNhap.Close()
$fDangNhap.Dispose()

$fNhatKy = New-Object QLBanMayViTinh.Forms.frmNhatKyHoatDong
$fNhatKy.StartPosition = [System.Windows.Forms.FormStartPosition]::Manual
$fNhatKy.Location = New-Object System.Drawing.Point(0, 0)
$fNhatKy.Show()
[System.Windows.Forms.Application]::DoEvents()
[QLBanMayViTinh.Classes.UIThemeHelper]::ApplyTheme($fNhatKy)
CaptureForm $fNhatKy "07_NhatKy_Fixed"

$fNhatKy.Close()
$fNhatKy.Dispose()

$fCaiDat = New-Object QLBanMayViTinh.Forms.frmCaiDat
$fCaiDat.StartPosition = [System.Windows.Forms.FormStartPosition]::Manual
$fCaiDat.Location = New-Object System.Drawing.Point(0, 0)
$fCaiDat.Show()
[System.Windows.Forms.Application]::DoEvents()
[QLBanMayViTinh.Classes.UIThemeHelper]::ApplyTheme($fCaiDat)
CaptureForm $fCaiDat "08_CaiDat_Fixed"

$fCaiDat.Close()
$fCaiDat.Dispose()

Write-Host "All multi-state dynamic verifications captured successfully."
