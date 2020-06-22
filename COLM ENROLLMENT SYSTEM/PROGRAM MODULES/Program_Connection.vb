Module Program_Connection

    'Public StringConnection As String = "Server=192.168.0.117\SQLEXPRESS;Database=PROGRAM_TEST_2;User Id=sa;Password=sa;"
    Public StringConnection As String = "Data Source=hgws12.win.hostgator.com;Initial Catalog=colmpulilan_enrollment;Persist Security Info=True;User ID=colmpulilan_admin;Password=Admin.c0lm2o18"
    'Public StringConnection As String = "Server=CYREKXS\SQLEXPRESS;Database=PROGRAM_TEST_2;User Id=sa;Password=sa;"
    'Public StringConnection As String = LoadConnection()
    Private Function LoadConnection()

        If StringConnection <> String.Empty Then
            Return StringConnection
            Exit Function
        End If

        Dim File_Name As String = Application.StartupPath & "\Connection_Source.txt"
        Dim Text_Line As String = String.Empty

        If System.IO.File.Exists(File_Name) Then
            Dim reader As New System.IO.StreamReader(File_Name)
            Text_Line = reader.ReadLine
        Else
            MsgBox("Cannot Locate Connection!", MsgBoxStyle.Critical)
        End If

        Return Text_Line

    End Function
End Module
