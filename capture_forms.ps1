param(
    [string]$OutputDir = "Screenshots"
)

if (-not (Test-Path $OutputDir)) {
    New-Item -ItemType Directory -Force -Path $OutputDir | Out-Null
}

[System.Reflection.Assembly]::LoadWithPartialName("System.Windows.Forms") | Out-Null
[System.Reflection.Assembly]::LoadWithPartialName("System.Drawing") | Out-Null
[System.Reflection.Assembly]::LoadFrom((Resolve-Path "bin\Debug\Guna.UI2.dll").Path) | Out-Null
[System.Reflection.Assembly]::LoadFrom((Resolve-Path "bin\Debug\QLBanMayViTinh.exe").Path) | Out-Null

[QLBanMayViTinh.Classes.Functions]::Connect()
[QLBanMayViTinh.Classes.UIThemeHelper]::IsDarkMode = $false

$forms = @(
    "frmDangNhap",
    "frmDangKy",
    "frmQuenMatKhau",
    "frmMain",
    "frmMayViTinh",
    "frmDanhMucHeThong",
    "frmHoaDonNhap",
    "frmHoaDonBan",
    "frmTimKiem",
    "frmBaoCaoThongKe",
    "frmQuanLyNguoiDung",
    "frmHoSoCaNhan",
    "frmNhatKyHoatDong",
    "frmCaiDat"
)

foreach ($name in $forms) {
    try {
        $type = [QLBanMayViTinh.Forms.frmMain].Assembly.GetType("QLBanMayViTinh.Forms.$name")
        if (-not $type) {
            $type = [QLBanMayViTinh.Forms.frmMain].Assembly.GetType("QLBanMayViTinh.$name")
        }
        if (-not $type) {
            Write-Host "Could not find type for $name"
            continue
        }

        [QLBanMayViTinh.Classes.UIThemeHelper]::IsDarkMode = $false
        $form = [Activator]::CreateInstance($type)
        $form.StartPosition = [System.Windows.Forms.FormStartPosition]::Manual
        $form.Location = New-Object System.Drawing.Point(0, 0)
        
        $form.Show()
        [System.Windows.Forms.Application]::DoEvents()
        Start-Sleep -Milliseconds 250
        [QLBanMayViTinh.Classes.UIThemeHelper]::ApplyTheme($form)
        [System.Windows.Forms.Application]::DoEvents()
        Start-Sleep -Milliseconds 150

        $bmp = New-Object System.Drawing.Bitmap($form.Width, $form.Height)
        $rect = New-Object System.Drawing.Rectangle(0, 0, $form.Width, $form.Height)
        $form.DrawToBitmap($bmp, $rect)
        $bmp.Save("$OutputDir\${name}_Light.png", [System.Drawing.Imaging.ImageFormat]::Png)
        $bmp.Dispose()

        [QLBanMayViTinh.Classes.UIThemeHelper]::IsDarkMode = $true
        [QLBanMayViTinh.Classes.UIThemeHelper]::ApplyTheme($form)
        [System.Windows.Forms.Application]::DoEvents()
        Start-Sleep -Milliseconds 150

        $bmpDark = New-Object System.Drawing.Bitmap($form.Width, $form.Height)
        $form.DrawToBitmap($bmpDark, $rect)
        $bmpDark.Save("$OutputDir\${name}_Dark.png", [System.Drawing.Imaging.ImageFormat]::Png)
        $bmpDark.Dispose()

        $form.Close()
        $form.Dispose()
        Write-Host "Captured $name in Light and Dark mode"
    }
    catch {
        Write-Host "Error capturing ${name}: $_"
    }
}

