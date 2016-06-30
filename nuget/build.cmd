@echo Off
set config=%1
if "%config%" == "" (
   set config=Release
)
set version=
if not "%PackageVersion%" == "" (
   set version=-Version %PackageVersion%
) else (
   set version=-Version 1.0.0-beta
)
REM Determine msbuild path
set msbuildtmp="%ProgramFiles%\MSBuild\14.0\bin\msbuild"
if exist %msbuildtmp% set msbuild=%msbuildtmp%
set msbuildtmp="%ProgramFiles(x86)%\MSBuild\14.0\bin\msbuild"
if exist %msbuildtmp% set msbuild=%msbuildtmp%
set VisualStudioVersion=14.0

REM Package restore
echo.
echo Running package restore...
call :ExecuteCmd ..\tools\nuget.exe restore ..\Wheatech.EmitMapper.sln -OutputDirectory ..\packages -NonInteractive -ConfigFile nuget.config
IF %ERRORLEVEL% NEQ 0 goto error

echo Building solution...
call :ExecuteCmd %msbuild% "..\Wheatech.EmitMapper.sln" /p:Configuration="%config%" /m /v:M /fl /flp:LogFile=msbuild.log;Verbosity=Normal /nr:false
IF %ERRORLEVEL% NEQ 0 goto error

REM Run tests
echo.
echo Run tests...
call :ExecuteCmd ..\tools\nuget.exe install xunit.runner.console -Version 2.1.0 -OutputDirectory ..\packages
IF %ERRORLEVEL% NEQ 0 goto error
call :ExecuteCmd ..\packages\xunit.runner.console.2.1.0\tools\xunit.console.exe ..\tests\bin\%config%\Wheatech.EmitMapper.UnitTests.dll
IF %ERRORLEVEL% NEQ 0 goto error

echo Packaging...
set libtmp=%cd%\lib
set packagestmp="%cd%\packages"
if not exist %libtmp% mkdir %libtmp%
if not exist %packagestmp% mkdir %packagestmp%

if not exist %libtmp%\net461 mkdir %libtmp%\net461
copy ..\src\bin\%config%\Wheatech.EmitMapper.dll %libtmp%\net461 /Y
copy ..\src\bin\%config%\Wheatech.EmitMapper.xml %libtmp%\net461 /Y

call :ExecuteCmd ..\tools\nuget.exe pack "%cd%\Wheatech.EmitMapper.nuspec" -OutputDirectory %packagestmp% %version%
IF %ERRORLEVEL% NEQ 0 goto error

rmdir %libtmp% /S /Q

goto end

:: Execute command routine that will echo out when error
:ExecuteCmd
setlocal
set _CMD_=%*
call %_CMD_%
if "%ERRORLEVEL%" NEQ "0" echo Failed exitCode=%ERRORLEVEL%, command=%_CMD_%
exit /b %ERRORLEVEL%

:error
endlocal
echo An error has occurred during build.
call :exitSetErrorLevel
call :exitFromFunction 2>nul

:exitSetErrorLevel
exit /b 1

:exitFromFunction
()

:end
endlocal
echo Build finished successfully.