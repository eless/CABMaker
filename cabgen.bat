@echo off

::Задаем имя для каба из параметра-имени проекта

if exist %3 del /q /f %3
if exist EMIapp.inf REN EMIapp.inf %3

::Создание каба
Cabwiz.exe %3 /compress /err "errfile.txt"
echo %3
::Чистим временные файлы версии
if exist %dest2%\dirlist.txt del /s/q %dest2%\dirlist.txt
if exist %dest2%\Setup.DLL del /s/q %dest2%\Setup.DLL
if exist %dest1% RD /s/q %dest1%
if exist %dest2%\head.dat del /q /f %dest2%\head.dat
if exist %dest2%\end.dat del /q /f %dest2%\end.dat
