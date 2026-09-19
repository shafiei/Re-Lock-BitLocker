#ifndef MyAppVersion
  #define MyAppVersion "1.0.0"
#endif

[Setup]
AppName=Re-Lock BitLocker
AppVersion={#MyAppVersion}
AppPublisher=shafiei
AppPublisherURL=https://github.com/shafiei/Re-Lock-BitLocker
DefaultDirName={autopf}\Re-Lock BitLocker
DefaultGroupName=Re-Lock BitLocker
OutputDir=output
OutputBaseFilename=ReLockBitLocker-Setup
Compression=lzma2
SolidCompression=yes
PrivilegesRequired=admin
ArchitecturesAllowed=x64compatible
ArchitecturesInstallIn64BitMode=x64compatible
WizardStyle=modern
UninstallDisplayIcon={app}\ReLockBitLocker.exe

[Tasks]
Name: "desktopicon"; Description: "Create a desktop shortcut"; Flags: unchecked

[Files]
Source: "..\app\publish\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs

[Icons]
Name: "{group}\Re-Lock BitLocker"; Filename: "{app}\ReLockBitLocker.exe"
Name: "{autodesktop}\Re-Lock BitLocker"; Filename: "{app}\ReLockBitLocker.exe"; Tasks: desktopicon

[Run]
Filename: "{app}\ReLockBitLocker.exe"; Description: "Launch Re-Lock BitLocker"; Flags: nowait postinstall skipifsilent
