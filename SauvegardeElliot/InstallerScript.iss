; Script generated by the Inno Script Studio Wizard.
; SEE THE DOCUMENTATION FOR DETAILS ON CREATING INNO SETUP SCRIPT FILES!

#define MyAppName "ElectronSaver"
#define MyAppVersion "1.5"
#define MyAppPublisher "ImElectron"
#define MyAppURL "http://www.imelectron.fr/"
#define MyAppExeName "ElectronSaver.exe"

[Setup]
; NOTE: The value of AppId uniquely identifies this application.
; Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{BEF26DE5-DF8A-434E-BE0D-52AF97AD2849}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
;AppVerName={#MyAppName} {#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
DefaultDirName={pf}\{#MyAppName}
DefaultGroupName={#MyAppName}
AllowNoIcons=yes
LicenseFile=C:\Users\aluca\source\repos\SauvegardeElliot\SauvegardeElliot\bin\Release\net5.0-windows\win-x64\Licence.txt
OutputBaseFilename=setup
Compression=lzma
SolidCompression=yes
AppCopyright={#MyAppPublisher} 2021
ArchitecturesInstallIn64BitMode=x64

[Languages]
Name: "french"; MessagesFile: "compiler:Languages\French.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked
Name: "quicklaunchicon"; Description: "{cm:CreateQuickLaunchIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked; OnlyBelowVersion: 0,6.1

[Files]
Source: "C:\Users\aluca\source\repos\SauvegardeElliot\SauvegardeElliot\bin\Release\net5.0-windows\win-x64\ElectronSaver.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\aluca\source\repos\SauvegardeElliot\SauvegardeElliot\bin\Release\net5.0-windows\win-x64\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs
; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Icons]
Name: "{group}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{group}\{cm:ProgramOnTheWeb,{#MyAppName}}"; Filename: "{#MyAppURL}"
Name: "{group}\{cm:UninstallProgram,{#MyAppName}}"; Filename: "{uninstallexe}"
Name: "{commondesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon
Name: "{userappdata}\Microsoft\Internet Explorer\Quick Launch\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: quicklaunchicon

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent

[Registry]
Root: "HKCR"; Subkey: "Directory\Background\shell\ElectronSaver"; ValueType: expandsz; ValueName: "icon"; ValueData: """{app}\ElectronSaver.dll"",0"; Flags: createvalueifdoesntexist deletevalue uninsdeletekey
Root: "HKCR"; Subkey: "Directory\Background\shell\ElectronSaver"; ValueType: string; ValueName: "MUIVerb"; ValueData: "ElectronSaver"; Flags: createvalueifdoesntexist deletevalue uninsdeletekey
Root: "HKCR"; Subkey: "Directory\Background\shell\ElectronSaver"; ValueType: string; ValueName: "SubCommands"; ValueData: "ElectronSaver.Export;ElectronSaver.Modify;ElectronSaver.Restore;"; Flags: createvalueifdoesntexist  uninsdeletekey
Root: "HKCR"; Subkey: "Directory\shell\ElectronSaver"; ValueType: expandsz; ValueName: "icon"; ValueData: """{app}\ElectronSaver.dll\"",0"; Flags: createvalueifdoesntexist uninsdeletekey
Root: "HKCR"; Subkey: "Directory\shell\ElectronSaver"; ValueType: string; ValueName: "MUIVerb"; ValueData: "ElectronSaver"; Flags: createvalueifdoesntexist uninsdeletekey
Root: "HKCR"; Subkey: "Directory\shell\ElectronSaver"; ValueType: string; ValueName: "SubCommands"; ValueData: "ElectronSaver.export;ElectronSaver.modify;ElectronSaver.restore;"; Flags: createvalueifdoesntexist  uninsdeletekey
Root: "HKLM"; Subkey: "SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\CommandStore\shell\ElectronSaver.Export"; ValueType: expandsz; ValueName: "icon"; ValueData: """{app}\ElectronSaver.dll"",5"; Flags: createvalueifdoesntexist   uninsdeletekey
Root: "HKLM"; Subkey: "SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\CommandStore\shell\ElectronSaver.Export"; ValueType: expandsz; ValueName: "MUIVerb"; ValueData: "Sauvegarder"; Flags: createvalueifdoesntexist   uninsdeletekey
Root: "HKLM"; Subkey: "SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\CommandStore\shell\ElectronSaver.Export\Command"; ValueType: expandsz; ValueData: """{app}\{#MyAppExeName}"" ""-s"" ""%v"""; Flags: createvalueifdoesntexist uninsdeletekey
Root: "HKLM"; Subkey: "SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\CommandStore\shell\ElectronSaver.Restore"; ValueType: expandsz; ValueName: "icon"; ValueData: """{app}\ElectronSaver.dll"",4"; Flags: createvalueifdoesntexist   uninsdeletekey
Root: "HKLM"; Subkey: "SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\CommandStore\shell\ElectronSaver.Restore"; ValueType: expandsz; ValueName: "MUIVerb"; ValueData: "Restaurer"; Flags: createvalueifdoesntexist   uninsdeletekey
Root: "HKLM"; Subkey: "SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\CommandStore\shell\ElectronSaver.Restore\Command"; ValueType: expandsz; ValueData: """{app}\{#MyAppExeName}"" ""-r"" ""%v"""; Flags: createvalueifdoesntexist   uninsdeletekey
Root: "HKLM"; Subkey: "SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\CommandStore\shell\ElectronSaver.Modify"; ValueType: expandsz; ValueName: "icon"; ValueData: """{app}\ElectronSaver.dll"",3"; Flags: createvalueifdoesntexist   uninsdeletekey
Root: "HKLM"; Subkey: "SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\CommandStore\shell\ElectronSaver.Modify"; ValueType: expandsz; ValueName: "MUIVerb"; ValueData: "creer/modifier"; Flags: createvalueifdoesntexist   uninsdeletekey
Root: "HKLM"; Subkey: "SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\CommandStore\shell\ElectronSaver.Modify\Command"; ValueType: expandsz; ValueData: """{app}\{#MyAppExeName}"" ""-m"" ""%v"""; Flags: createvalueifdoesntexist   uninsdeletekey
