@echo Off
set config=%1
if "%config%" == "" (
   set config=Release
)
set version=
if not "%PackageVersion%" == "" (
   set version=-Version %PackageVersion%
) else (
   set version=-Version 1.0.0-rc1
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
call :ExecuteCmd nuget.exe restore ..\PowerMapper.sln -OutputDirectory ..\packages -NonInteractive -ConfigFile nuget.config
IF %ERRORLEVEL% NEQ 0 goto error

echo Building solution...
call :ExecuteCmd %msbuild% "..\build\net35\PowerMapper.net35.csproj" /p:Configuration="%config%" /m /v:M /fl /flp:LogFile=msbuild.log;Verbosity=Normal /nr:false
IF %ERRORLEVEL% NEQ 0 goto error
call :ExecuteCmd %msbuild% "..\build\net40\PowerMapper.net40.csproj" /p:Configuration="%config%" /m /v:M /fl /flp:LogFile=msbuild.log;Verbosity=Normal /nr:false
IF %ERRORLEVEL% NEQ 0 goto error
call :ExecuteCmd %msbuild% "..\netcore\PowerMapper\PowerMapper.netcore.xproj" /p:Configuration="%config%" /m /v:M /fl /flp:LogFile=msbuild.log;Verbosity=Normal /nr:false
IF %ERRORLEVEL% NEQ 0 goto error

echo Packaging...
set libtmp=%cd%\lib
set packagestmp="%cd%\packages"
if not exist %libtmp% mkdir %libtmp%
if not exist %packagestmp% mkdir %packagestmp%

if not exist %libtmp%\net35 mkdir %libtmp%\net35
copy ..\build\net35\bin\%config%\PowerMapper.dll %libtmp%\net35 /Y
copy ..\build\net35\bin\%config%\PowerMapper.xml %libtmp%\net35 /Y

if not exist %libtmp%\net40 mkdir %libtmp%\net40
copy ..\build\net40\bin\%config%\PowerMapper.dll %libtmp%\net40 /Y
copy ..\build\net40\bin\%config%\PowerMapper.xml %libtmp%\net40 /Y

if not exist %libtmp%\netstandard1.5 mkdir %libtmp%\netstandard1.5
copy ..\netcore\PowerMapper\bin\%config%\netstandard1.5\PowerMapper.dll %libtmp%\netstandard1.5 /Y
copy ..\netcore\PowerMapper\bin\%config%\netstandard1.5\PowerMapper.xml %libtmp%\netstandard1.5 /Y
copy ..\netcore\PowerMapper\bin\%config%\netstandard1.5\PowerMapper.deps.json %libtmp%\netstandard1.5 /Y

call :ExecuteCmd nuget.exe pack "%cd%\PowerMapper.nuspec" -OutputDirectory %packagestmp% %version%
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