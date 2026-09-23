Dim fso, filePath, fileContent, RegEx, shell, command, errorCode
dir = CreateObject("Scripting.FileSystemObject").GetParentFolderName(WScript.ScriptFullName)
filePath = dir & "\appsettings.json"

If Not WScript.Arguments.Named.Exists("elevate") Then
  CreateObject("Shell.Application").ShellExecute WScript.FullName _
    , """" & WScript.ScriptFullName & """ /elevate", "", "runas", 1
  WScript.Quit
End If

Set fso = CreateObject("Scripting.FileSystemObject")

If fso.FileExists(filePath) Then
	fileContent = fso.OpenTextFile(filePath,1).ReadAll
Else
	WScript.Echo("appsettings.json was not found at" & filePath)
	WScript.Quit
End If
set regEx = New RegExp
regEx.Pattern = "(" & Chr(34) & "filePath" & Chr(34) & "*: *" & Chr(34) & ").\/([\S-]+?)" & Chr(34)

fileContent = regEx.Replace(fileContent, "$1" & dir & "\$2" & Chr(34))

set regExpDir = new RegExp
regExpDir.Pattern = "\\+"
regExpDir.Global = True
fileContent = regExpDir.Replace(fileContent, "\\")

fso.OpenTextFile(filePath, 2, True).Write fileContent

Set shell = CreateObject("WScript.Shell")

createServiceCommand = "cmd.exe /c sc.exe create " & Chr(34) & "ReminderWorkerService" & Chr(34) & " binPath=" & Chr(34) & dir & "\ReminderBackgroundService.exe" & Chr(34)
startServiceCommand = "cmd.exe /c sc.exe start " & Chr(34) & "ReminderWorkerService" & Chr(34)
errorCodeCreate = shell.Run(createServiceCommand, 0, True)
If errorCodeCreate = 0 Then
	errorCodeStart = shell.Run(startServiceCommand, 0, True)
	If errorCodeStart = 0 Then
		WScript.Echo("Service Started!")
	Else
		Wscript.Echo("Could not start service! Code:" & errorCodeStart)
	End If
Else
	WScript.Echo("Could not create service! Code: " & errorCodeCreate)
End If
	