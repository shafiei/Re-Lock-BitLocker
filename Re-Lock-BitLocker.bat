@echo off

:: Check for administrator privileges
>nul 2>&1 "%SYSTEMROOT%\system32\cacls.exe" "%SYSTEMROOT%\system32\config\system"

if '%errorlevel%' NEQ '0' (
    echo Requesting administrative privileges...
    powershell -Command "Start-Process cmd -ArgumentList '/c, %~dpnx0' -Verb RunAs"
    exit /b
)

:: Prompt the user for a drive letter
set /p driveLetter=Enter the drive letter for lock (BitLocker) (e.g., C, D, E): 

:: Ensure the user input is uppercase
set "driveLetter=%driveLetter:~0,1%"
set "driveLetter=%driveLetter: =%"
set "driveLetter=%driveLetter:*.=%"

:: Check if the user entered a valid drive letter
if "%driveLetter%"=="" (
    echo Invalid input. Please enter a valid drive letter to lock.
    exit /b
)

:: Run the manage-bde command with the user's input
manage-bde -lock -ForceDismount %driveLetter%:

pause