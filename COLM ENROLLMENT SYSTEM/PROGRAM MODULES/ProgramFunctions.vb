Module ProgramFunctions
    'Declarations of Windows API functions.
    Declare Function OpenIcon Lib "user32" (ByVal hwnd As Long) As Long
    Declare Function SetForegroundWindow Lib "user32" (ByVal hwnd As Long) As Long

    Sub ActivatePrevInstance(ByVal argStrAppToFind As String)

        Dim PrevHndl As Long
        Dim result As Long

        'Variable to hold individual Process.
        Dim objProcess As New Process()

        'Collection of all the Processes running on local machine
        Dim objProcesses() As Process

        'Get all processes into the collection
        objProcesses = Process.GetProcesses()

        For Each objProcess In objProcesses
            'Check and exit if we have SMS running already
            If UCase(objProcess.MainWindowTitle) = UCase(argStrAppToFind) Then
                MessageBox.Show(argStrAppToFind & " is already running.", "System", MessageBoxButtons.OK, MessageBoxIcon.Information)
                PrevHndl = objProcess.MainWindowHandle.ToInt32()
                Exit For
            End If
        Next

        'If no previous instance found exit the application.
        If PrevHndl = 0 Then Exit Sub

        'If previous instance found.
        result = OpenIcon(PrevHndl) 'Restore the program.
        result = SetForegroundWindow(PrevHndl) 'Activate the application.

        'End the current instance of the application.
        End

    End Sub
End Module
