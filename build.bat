@echo off
setlocal
cd /d "%~dp0"

echo Building the app...
dotnet publish app\BitLockerLock.csproj -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -p:IncludeNativeLibrariesForSelfExtract=true -p:EnableCompressionInSingleFile=true -p:DebugType=None -p:DebugSymbols=false -o app\publish
if errorlevel 1 (
    echo.
    echo Build failed.
    exit /b 1
)

set "ISCC=%ProgramFiles(x86)%\Inno Setup 6\ISCC.exe"
if exist "%ISCC%" (
    echo.
    echo Building the installer...
    "%ISCC%" installer\setup.iss
    if errorlevel 1 (
        echo Installer build failed.
        exit /b 1
    )
    echo.
    echo Done: installer\output\ReLockBitLocker-Setup.exe
) else (
    echo.
    echo Inno Setup 6 not found - skipping the installer. The app is in app\publish.
)
