; Re-Lock BitLocker installer. Build with build.bat, or: ISCC installer\setup.iss
; The version defaults to the value below; CI passes /DMyAppVersion=<tag>.

#ifndef MyAppVersion
  #define MyAppVersion "1.0.1"
#endif
#define MyAppName "Re-Lock BitLocker"
#define MyAppExe "ReLockBitLocker.exe"
#define MyAppURL "https://github.com/shafiei/Re-Lock-BitLocker"

[Setup]
; Never change AppId: it is how future versions upgrade this one in place.
AppId={{B8ED29AE-6094-4875-B627-6E3463D82491}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
AppVerName={#MyAppName} {#MyAppVersion}
AppPublisher=shafiei
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}/issues
AppUpdatesURL={#MyAppURL}/releases
DefaultDirName={autopf}\{#MyAppName}
DisableProgramGroupPage=yes
OutputDir=output
OutputBaseFilename=ReLockBitLocker-Setup
SetupIconFile=..\app\Assets\app.ico
UninstallDisplayIcon={app}\{#MyAppExe}
UninstallDisplayName={#MyAppName}
WizardStyle=modern
WizardImageFile=assets\wizard-164.bmp,assets\wizard-246.bmp,assets\wizard-328.bmp
WizardSmallImageFile=assets\small-55.bmp,assets\small-83.bmp,assets\small-110.bmp
Compression=lzma2
SolidCompression=yes
PrivilegesRequired=admin
MinVersion=10.0
ArchitecturesAllowed=x64compatible
ArchitecturesInstallIn64BitMode=x64compatible

[Tasks]
Name: "desktopicon"; Description: "Create a desktop shortcut"; Flags: unchecked

[Files]
Source: "..\app\publish\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs

[Icons]
Name: "{autoprograms}\{#MyAppName}"; Filename: "{app}\{#MyAppExe}"
Name: "{autodesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExe}"; Tasks: desktopicon

[Run]
Filename: "{app}\{#MyAppExe}"; Description: "Launch {#MyAppName}"; Flags: nowait postinstall skipifsilent runascurrentuser