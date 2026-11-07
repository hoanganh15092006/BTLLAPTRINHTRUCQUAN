@echo off
setlocal
powershell.exe -NoProfile -ExecutionPolicy Bypass -File "%~dp0run_e2e_tests.ps1" %*
exit /b %ERRORLEVEL%
