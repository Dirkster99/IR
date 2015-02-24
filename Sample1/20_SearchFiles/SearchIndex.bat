ECHO OFF
pushd "%~dp0"
ECHO.
ECHO Searching index...
ECHO.
ECHO Sample query: contents:AvalonDock
ECHO.

.\SearchFiles\bin\Release\SearchFiles.exe "..\00_IDX"

@IF %ERRORLEVEL% NEQ 0 GOTO err
@exit /B 0
:err
@PAUSE
@exit /B 1
