:: (c)Aleksandr Solovei
@echo off

::Переносим EMI_WM.exe
2>nul dir %source1%\release\ /a-d /b>nul&&copy /y %source1%\release\EMI_WM.exe %dest1%\[INSTALLDIR]


::Наполнение папки Windows
if exist %source2%\apke.pac copy /y %source2%\apke.pac %dest1%\Windows
if exist %source2%\pke_logofile copy /y %source2%\pke_logofile %dest1%\Windows
if exist /y %source2%\pkmgr.dll copy /y %source2%\pkmgr.dll %dest1%\Windows
if exist %source2%\set.alf copy /y %source2%\set.alf %dest1%\Windows

::Копирование js-кода в папку [INSTALLDIR]
xcopy %source2% %dest1%\[INSTALLDIR] /e 
xcopy %source3% %dest1%\[INSTALLDIR] /e

::Удаление лишнего
if exist %dest1%\[INSTALLDIR]\setup.dll del /q /f %dest1%\[INSTALLDIR]\setup.dll
if exist %dest1%\[INSTALLDIR]\apke.pac del /q /f %dest1%\[INSTALLDIR]\apke.pac
if exist %dest1%\[INSTALLDIR]\pke_logofile del /q /f %dest1%\[INSTALLDIR]\pke_logofile
if exist %dest1%\[INSTALLDIR]\pkmgr.dll del /q /f %dest1%\[INSTALLDIR]\pkmgr.dll
if exist %dest1%\[INSTALLDIR]\set.alf del /q /f %dest1%\[INSTALLDIR]\set.alf
::if exist %dest1%\[INSTALLDIR]\head.dat del /q /f %dest1%\[INSTALLDIR]\head.dat
::if exist %dest1%\[INSTALLDIR]\end.dat del /q /f %dest1%\[INSTALLDIR]\end.dat
If Exist "%dest1%\[INSTALLDIR]\.git" del /q /f "%dest1%\.git\"
If Exist "%dest1%\[INSTALLDIR]\.gitignore" del /q /f "%dest1%\[INSTALLDIR]\.gitignore"

If %source1%==\git\DEMO_EMI (
 RD /s/q %dest1%\[INSTALLDIR]\PHOTO
 RD /s/q %dest1%\[INSTALLDIR]\printer
 )

::Построение списков файлов и папок
cd /d %dest1%
dir /a:d-h /o:n /b /s >%dest2%\dirlist.txt"


::Копирование файлов данных
If exist %source2%\Setup.DLL copy /y %source2%\Setup.DLL %dest2%\Setup.DLL 
If exist %source2%\head.dat copy /y %source2%\head.dat %dest2%\head.dat
If exist %source2%\end.dat copy /y %source2%\end.dat %dest2%\end.dat

::Генерируем .inf
cd /d %dest2%
echo %appn%
call INFGen.exe %appn%


 