  @echo off

::set drive1=E:


echo %1
echo %2

if "%1"=="" (

::параметр не задан - вводится вручную
set source2=\git\MTKD_Mobile
set newnm=MTKD_Mobile.inf
echo %newnm%
) else (

::параметр-название проекта в консоли
set source2=\git\%1
set newnm=%1%.inf
echo %newnm%
)


::Создание папок
IF EXIST \tmp\EMI RD /s/q \tmp\EMI
md \tmp\EMI
md \tmp\EMI\[INSTALLDIR]
md \tmp\EMI\Windows

::Глобальные переменные-пути
::set source1=%drive1%\git\EMI_WinMobile
if "%1"=="mtkd_mobile" (

set source1=\git\DEMO_EMI
) else (

 
echo 
::set source1=%drive1%\git\EMI_WM
set source1=\git\EMI_WinMobile

)
echo %source1%
set dest1=\tmp\EMI
set dest2=\git\CAB_Maker
set source3=\git\CAB_Maker\Addfiles

::Построение

::Проверка необходимости перекомпиляции ядра
::if "%2"=="reb" (

::if exist %source1%\EMI_WM.exe del /q /f %source1%\EMI_WM.exe
::)

if "%2"=="reb" (

call EMI_Build %source1
)
set appn=%1
echo %appn%
call listmake %source1 %source2 %source3 %dest1 %dest2 %appn
call cabgen %dest1% %dest2% %newnm%