Public Class student_information_student_type

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim StudentType As String = String.Empty
        If RadioButton1.Checked = True Then
            StudentType = "NEW STUDENT"
        ElseIf RadioButton2.Checked = True Then
            StudentType = "TRANSFEREE"
        ElseIf RadioButton3.Checked = True Then
            StudentType = "OLD STUDENT"
        Else
            MsgBox("Please select Student Type!", MsgBoxStyle.Critical)
            Exit Sub
        End If

        With student_information_entry
            .StudentType = StudentType
            .SavingStatus = "NEW"
            .StartPosition = FormStartPosition.CenterScreen
            .ShowDialog()
            Me.Close()
        End With
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
        Me.Dispose()
    End Sub
End Class