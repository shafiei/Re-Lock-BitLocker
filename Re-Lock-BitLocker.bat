@echo off
setlocal EnableExtensions EnableDelayedExpansion
title Re-Lock BitLocker

:: --- Make sure we are running as administrator (relaunch elevated if not) ---
net session >nul 2>&1
if errorlevel 1 (
    echo Requesting administrator privileges...
    powershell -NoProfile -Command "Start-Process -FilePath '%~f0' -Verb RunAs"
    exit /b
)

:menu
cls
echo ==========================================
echo    Re-Lock BitLocker
echo ==========================================
echo.

:: --- List unlocked BitLocker data drives (the Windows drive can't be locked) ---
set "count=0"
for /f "usebackq tokens=1,2" %%A in (`powershell -NoProfile -Command "foreach ($v in Get-BitLockerVolume) { if ($v.VolumeType -eq 'Data' -and $v.LockStatus -eq 'Unlocked') { '{0} {1}' -f $v.MountPoint, [math]::Round($v.CapacityGB) } }" 2^>nul`) do (
    set /a count+=1
    set "drive!count!=%%A"
    echo    [!count!]  %%A   %%B GB
)

if %count%==0 (
    echo    No unlocked BitLocker drives found.
    echo.
    pause
    exit /b
)

echo.
echo    [A]  Lock all
echo    [Q]  Quit
echo.
set "choice="
set /p "choice=Choose a number, A or Q: "

if /i "!choice!"=="Q" exit /b

if /i "!choice!"=="A" (
    for /l %%i in (1,1,%count%) do call :lockdrive !drive%%i!
    goto :done
)

set "target="
for /l %%i in (1,1,%count%) do if "!choice!"=="%%i" set "target=!drive%%i!"

if not defined target (
    echo.
    echo    That choice isn't in the list.
    timeout /t 2 >nul
    goto menu
)

call :lockdrive %target%

:done
echo.
pause
goto menu

:lockdrive
echo.
echo    Locking %~1 ...
manage-bde -lock -ForceDismount %~1 >nul 2>&1
if errorlevel 1 (
    echo    Could not lock %~1. Close anything using it and try again.
) else (
    echo    %~1 is locked.
)
exit /b
