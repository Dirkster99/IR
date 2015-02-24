ECHO OFF
pushd "%~dp0"
ECHO.
ECHO Building index...
ECHO.

SET TOOL=".\IndexBuildTest\bin\x64\Release\IndexBuildTest.exe"

%TOOL% "..\00_IDX" "..\00_SampleData\CP_Articles"

@IF %ERRORLEVEL% NEQ 0 GOTO err
@exit /B 0
:err
@PAUSE
@exit /B 1
