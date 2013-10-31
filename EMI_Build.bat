@echo off

del /s/q %source1%\EMI_WM.exe

call "%VS90COMNTOOLS%vsvars32.bat" /useenv > nul

devenv.com /nologo /build release %source1%/EMI_WinMobile.sln