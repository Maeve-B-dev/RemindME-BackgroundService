Dim shell, command, errorCode

' Check if the program is run as admin. If it isn't, run it as admin.
If Not WScript.Arguments.Named.Exists("elevate") Then
  CreateObject("Shell.Application").ShellExecute WScript.FullName _
    , """" & WScript.ScriptFullName & """ /elevate", "", "runas", 1
  WScript.Quit
End If

' Start shell to execute sc.exe and start the service.
Set shell = CreateObject("WScript.Shell")

' Set commands to stop and delete the service.
stopServiceCommand = "cmd.exe /c sc.exe stop " & Chr(34) & "ReminderWorkerService" & Chr(34)
deleteServiceCommand = "cmd.exe /c sc.exe delete " & Chr(34) & "ReminderWorkerService" & Chr(34)
errorCodeStop = shell.Run(stopServiceCommand, 0, True)
If errorCodeStop = 0 Then
	errorCodeDelete = shell.Run(deleteServiceCommand, 0, True)
	If errorCodeDelete = 0 Then
		WScript.Echo("Service Stopped & Deleted!")
	Else
		Wscript.Echo("Could not delete service! Code:" & errorCodeDelete)
	End If
Else
	WScript.Echo("Could not stop service! Code: " & errorCodeStop)
End If