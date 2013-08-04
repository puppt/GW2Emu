@echo off
%windir%\Microsoft.net\Framework\v4.0.30319\msbuild.exe GameRevision.GW2Emu.sln /m /l:FileLogger,Microsoft.Build;logfile=Build.log
echo.
pause