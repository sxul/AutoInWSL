@echo off
pushd %~dp0
reg add HKCR\.sh /d "bashfile" /f
reg add HKLM\SOFTWARE\Classes\.sh /d "bashfile" /f
reg add HKLM\SOFTWARE\Classes\.sh /v "PerceivedType" /d "text" /f

reg add HKLM\SOFTWARE\Classes\.sh\ShellNew /v "FileName" /d "%cd%\ShellNew\Template.sh" /f

reg add HKLM\SOFTWARE\Classes\bashfile /d "Bash Script" /f

reg add HKLM\SOFTWARE\Classes\bashfile\DefaultIcon /d "%cd%\ShellNew\sh.ico" /f
reg add HKLM\SOFTWARE\Classes\bashfile\shell /f
reg add HKLM\SOFTWARE\Classes\bashfile\shell\open /f
reg add HKLM\SOFTWARE\Classes\bashfile\shell\open\command /d "\"%cd%\cmds\aiw.exe\" \""%%1\"" %%*" /f

echo %Path% | findstr "%cd%\cmds" >nul
if errorlevel 1 setx Path "%Path%;%cd%\cmds;%cd%\cmds\autocmd"
echo %PATHEXT% | findstr ".SH" >nul
if errorlevel 1 setx PATHEXT %PATHEXT%;.SH

mkdir cmds\autocmd

taskkill /f /im explorer.exe
start %windir%\explorer.exe

popd
pause