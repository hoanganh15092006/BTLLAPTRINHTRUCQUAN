param(
    [int]$Tier = 0,
    [switch]$Verbose,
    [string]$Report = "TEST_REPORT.txt"
)

$ErrorActionPreference = "Stop"
$ScriptDir = Split-Path -Parent $MyInvocation.MyCommand.Definition
Set-Location $ScriptDir

$msbuild = & "${env:ProgramFiles(x86)}\Microsoft Visual Studio\Installer\vswhere.exe" -latest -products * -requires Microsoft.Component.MSBuild -find MSBuild\**\Bin\MSBuild.exe | Select-Object -First 1

if (-not $msbuild -or -not (Test-Path $msbuild)) {
    $fallbackPaths = @(
        "D:\Visual_studio\VS\MSBuild\Current\Bin\MSBuild.exe",
        "C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\MSBuild.exe",
        "C:\Program Files\Microsoft Visual Studio\2022\Enterprise\MSBuild\Current\Bin\MSBuild.exe",
        "C:\Program Files\Microsoft Visual Studio\2022\Professional\MSBuild\Current\Bin\MSBuild.exe"
    )
    foreach ($p in $fallbackPaths) {
        if (Test-Path $p) {
            $msbuild = $p
            break
        }
    }
}

if (-not $msbuild) {
    Write-Error "MSBuild executable not found on system."
    exit 2
}

& $msbuild QLBanMayViTinh.csproj /v:m /p:Configuration=Debug
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to build QLBanMayViTinh.csproj"
    exit $LASTEXITCODE
}

& $msbuild Testing\E2ETests.csproj /v:m /p:Configuration=Debug
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to build Testing\E2ETests.csproj"
    exit $LASTEXITCODE
}

$runnerExe = Join-Path $ScriptDir "bin\Debug\E2ETestRunner.exe"
if (-not (Test-Path $runnerExe)) {
    Write-Error "Test runner executable not found at $runnerExe"
    exit 2
}

$argList = @()
if ($Tier -gt 0) {
    $argList += "--tier"
    $argList += $Tier.ToString()
}
if ($Verbose) {
    $argList += "--verbose"
}
if ($Report) {
    $argList += "--report"
    $argList += $Report
}

& $runnerExe @argList
exit $LASTEXITCODE
