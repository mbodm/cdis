@echo off

set CURRENT_FOLDER=%cd%
set PROJECT_FOLDER=%CURRENT_FOLDER%\src\ControlDisplayInputSource\ControlDisplayInputSource
set PUBLISH_FOLDER=%PROJECT_FOLDER%\bin\Release\net48\publish
set RELEASE_FOLDER=%CURRENT_FOLDER%\release

cls
echo.
echo cdis.exe release script 1.0.0 (by MBODM 11/2024)
echo.

cd %PROJECT_FOLDER%
dotnet build -c Release && dotnet publish -c Release
echo.
cd %CURRENT_FOLDER%

if not exist %RELEASE_FOLDER% mkdir %RELEASE_FOLDER%
copy /B /V /Y %PUBLISH_FOLDER%\cdis.exe %RELEASE_FOLDER%
echo.

echo Have a nice day.

REM Show timeout when started via double click
REM From https://stackoverflow.com/questions/5859854/detect-if-bat-file-is-running-via-double-click-or-from-cmd-window
if /I %0 EQU "%~dpnx0" timeout /T 5
